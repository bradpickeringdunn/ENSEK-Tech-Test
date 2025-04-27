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
    public async Task Wehn_ValidateHeaders_Return_Success()
    {
        var result = await  _CsvValidator.ValidateHeaders(new List<string>
        {
            "x"
        }.ToArray());

        Assert.IsNotNull(result);
        Assert.AreEqual(result.Count, 1);

    }
}
