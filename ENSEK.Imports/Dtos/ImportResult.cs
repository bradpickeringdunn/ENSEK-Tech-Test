using ENSEK.Imports.Dtos.MeterReading;

namespace ENSEK.Imports.Dtos;

public class ImportResult
{
    public IList<string> Errors { get; init; } = new List<string>();

    public IList<MeterReadingDto> Records { get; init; } = new List<MeterReadingDto>();
}
