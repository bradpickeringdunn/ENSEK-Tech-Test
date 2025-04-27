using ENSEK.Imports.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENSEK.Imports.Parsers;

public class CsvParser : ICsvParser
{
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

        var headers = headerLine.Split(',');

        // Read each subsequent row
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            //var values = line.Split(',');
            //var record = new Dictionary<string, string>();

            //for (int i = 0; i < headers.Length; i++)
            //{
            //    record[headers[i]] = i < values.Length ? values[i] : string.Empty;
            //}

            //records.Add(record);
        }

        return records;
    }
}
