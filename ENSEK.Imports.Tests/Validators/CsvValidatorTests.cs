using ENSEK.Imports.Validations;
using ENSEK.Imports.Validators;
using System.Threading.Tasks;

namespace ENSEK.Imports.Tests;

[TestClass]
public class CsvValidatorTests
{
    private CsvValidator<MeterReadingCsvValidation> _CsvValidator;

    [TestInitialize]
    public void Init()
    {
        _CsvValidator = new CsvValidator<MeterReadingCsvValidation>();
    }

    [TestMethod]
    [DataRow("Iteam1")]
    [DataRow("Iteam1, Iteam2")]
    [DataRow("Iteam1, Iteam2, Iteam3")]
    public async Task When_MissingHeaders_Return_Errors(string headers)
    {
        var result = await  _CsvValidator.ValidateHeaders(headers.Split(','));

        Assert.IsNotNull(result);
        Assert.IsTrue(result.All(x => x.Contains("is missing from the csv.")));
        Assert.AreEqual(result.Count, result.Distinct().Count());
    }


    [TestMethod]
    [DataRow("date")]
    [DataRow("10102010")]
    [DataRow("2010-11/22")]
    public async Task When_RowDateInvalid_Then_AddError(string date)
    {
        var result = await _CsvValidator.ValidateRows(new Dictionary<string, string>
        {
            {MeterReadingCsvValidation.AccountIdHeader , "12"},
            {MeterReadingCsvValidation.MeterReadingDateTimeHeader , date},
            {MeterReadingCsvValidation.MeterReadValueHeader , "1,2.0"}
        });

        Assert.IsNotNull(result);
        Assert.IsTrue(result.All(x => x.Contains("as Meter Reading DateTime")));
    }
}
