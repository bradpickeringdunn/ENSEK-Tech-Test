namespace ENSEK.Imports.Validations;

public class MeterReadingCsvValidation
{
    public const string AccountIdHeader = "AccountId";

    public const string MeterReadingDateTimeHeader = "MeterReadingDateTime";

    public const string MeterReadValueHeader = "MeterReadValue";

    public static IList<string> Headers
    {
        get
        {
            return new List<string>
            {
                AccountIdHeader,
                MeterReadingDateTimeHeader,
                MeterReadValueHeader
            };
        }
    }
}
