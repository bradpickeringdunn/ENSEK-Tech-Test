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
    public async Task When_RowDateInvalid_Then_AddError()
    {
        var row = new Dictionary<string, string>
        {
            {MeterReadingCsvValidation.AccountIdHeader , "12"},
            {MeterReadingCsvValidation.MeterReadingDateTimeHeader , "123456"},
            {MeterReadingCsvValidation.MeterReadValueHeader , "1.12"}
        };

        var result = _CsvValidator.ValidateRows(row);
    }
}
