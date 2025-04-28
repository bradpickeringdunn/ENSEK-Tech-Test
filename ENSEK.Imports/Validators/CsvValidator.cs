using ENSEK.Imports.Validations;

namespace ENSEK.Imports.Validators;

public class CsvValidator<T> : ICsvValidator<T> where T : class
{
    public async Task<IList<string>> ValidateHeaders(string[] strings)
    {
        var errors = new List<string>();

        foreach (var header in MeterReadingCsvValidation.Headers)
        {
            if(strings.All(x => x != header))
                errors.Add($"The header {header} is missing from the csv.");
        }

        return await Task.FromResult(errors);
    }

    public async Task<IList<string>> ValidateRows(Dictionary<string, string> record)
    {
        var errors = new List<string>();

        if (!record.TryGetValue(MeterReadingCsvValidation.MeterReadValueHeader, out var meterReadValue))
            errors.Add("Can't find value for Meter Read Value");
        else
            if (!decimal.TryParse(meterReadValue, out var value))
                errors.Add($"Can't parse value {meterReadValue} as Meter Read Value");

        if (!record.TryGetValue(MeterReadingCsvValidation.AccountIdHeader, out var accountId))
            errors.Add("Can't find value for Account Id");
        else
            if (!int.TryParse(accountId, out var id))
            errors.Add($"Can't parse value {accountId} as Account Id");

        if (!record.TryGetValue(MeterReadingCsvValidation.MeterReadingDateTimeHeader, out var meterReadingDateTime))
            errors.Add("Can't find value for Meter Reading DateTime");
        else
            if (!DateTime.TryParse(meterReadingDateTime, out var dateTime))
                errors.Add($"Can't parse value {meterReadingDateTime} as Meter Reading DateTime");

        return await Task.FromResult(errors);
    }
}
