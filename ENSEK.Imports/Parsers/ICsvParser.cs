using ENSEK.Imports.Dtos;

namespace ENSEK.Imports.Parsers;

public interface ICsvParser
{
    List<MeterReadingDto> ParseCsv(string csvContent);
}
