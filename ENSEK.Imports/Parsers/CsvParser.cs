using ENSEK.Imports.Dtos;
using ENSEK.Imports.Validators;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENSEK.Imports.Parsers;

public class CsvParser : ICsvParser
{
    private readonly ICsvValidator _csvValidator;

    public CsvParser(ICsvValidator csvValidator)
    {
        _csvValidator = csvValidator;
    }

    public List<MeterReadingDto> ParseCsv(string csvContent)
    {
        var records = new List<MeterReadingDto>();
        using var reader = new StringReader(csvContent);

        // Read the header row
        var headerLine = reader.ReadLine();
        if (headerLine == null)
        {
            throw new InvalidOperationException("The CSV file is empty.");
        }

        var headers = _csvValidator.ValidateHeaders(headerLine.Split(',')); 

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

            records.Add(ToDto(record));
        }

        return records;
    }

    private MeterReadingDto ToDto(Dictionary<string, string> record)
    {
        return new MeterReadingDto
        {
            AccountId = int.Parse(record["AccountId"]),
            DateTime = DateTime.ParseExact(record["MeterReadingDateTime"], "dd/MM/yyyy hh:mm", CultureInfo.InvariantCulture),
            Value = decimal.Parse(record["MeterReadValue"]),
        };
    }
}
