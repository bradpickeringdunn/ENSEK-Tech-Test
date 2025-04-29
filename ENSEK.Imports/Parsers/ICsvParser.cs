using ENSEK.Imports.Dtos;

namespace ENSEK.Imports.Parsers;

public interface ICsvParser
{
    Task<ImportResult> ParseCsv(string csvContent);
}
