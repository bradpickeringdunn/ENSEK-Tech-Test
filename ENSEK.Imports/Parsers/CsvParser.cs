using ENSEK.Imports.Dtos;
using ENSEK.Imports.Dtos.MeterReading;
using ENSEK.Imports.Validations;
using ENSEK.Imports.Validators;

namespace ENSEK.Imports.Parsers;

public class CsvParser : ICsvParser
{
    private readonly ICsvValidator<MeterReadingCsvValidation> _csvValidator;

    public CsvParser(ICsvValidator<MeterReadingCsvValidation> csvValidator)
    {
        _csvValidator = csvValidator;
    }

    public async Task<ImportResult> ParseCsv(string csvContent)
    {
        var records = new List<MeterReadingDto>();
        using var reader = new StringReader(csvContent);

        // Read the header row
        var headerLine = reader.ReadLine();
        if (headerLine == null)
        {
            throw new InvalidOperationException("The CSV file is empty.");
        }

        var headers = headerLine.Split(',');
        var validationResult = await _csvValidator.ValidateHeaders(headers);

        if (validationResult.Any())
            return new ImportResult
            {
                Errors = validationResult
            };

        // Read each subsequent row
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            var values = line.Split(',');
            var record = new Dictionary<string, string>();

            for (int i = 0; i < headers.Length; i++)
            {
                record[headers[i]] = i < values.Length ? values[i] : string.Empty;
            }

       //     records.Add(ToDto(record));
        }

        return new ImportResult
        {
            Errors = validationResult
        };
    }

    //private TDto ToDto(Dictionary<string, string> record)
    //{
    //    new TDto();
    //    //return new MeterReadingDto
    //    //{
    //    //    AccountId = int.Parse(record["AccountId"]),
    //    //    DateTime = DateTime.ParseExact(record["MeterReadingDateTime"], "dd/MM/yyyy hh:mm", CultureInfo.InvariantCulture),
    //    //    Value = decimal.Parse(record["MeterReadValue"]),
    //    //};
    //}
}
