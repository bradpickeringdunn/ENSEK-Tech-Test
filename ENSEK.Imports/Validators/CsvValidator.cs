using ENSEK.Imports.Validations;

namespace ENSEK.Imports.Validators;

public class CsvValidator<T> : ICsvValidator<T> where T : class
{
    public async Task<IList<string>> ValidateHeaders(string[] strings)
    {
        var errors = new List<string>();

        if (strings.Length > Validations.MeterReadingCsvValidation.Headers.Count)
            errors.Add("There are too many columns in the csv.");

        foreach (var header in MeterReadingCsvValidation.Headers)
        {
            if(strings.Any(x => x != header))
                errors.Add($"The header {header} is missing from the csv.");
        }

        return await Task.FromResult(errors);
    }
}
