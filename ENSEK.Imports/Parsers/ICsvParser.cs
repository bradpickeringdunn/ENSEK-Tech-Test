using ENSEK.Imports.Dtos;
using ENSEK.Imports.Dtos.MeterReading;

namespace ENSEK.Imports.Parsers;

public interface ICsvParser
{
    Task<ImportResult> ParseCsv(string csvContent);
}
