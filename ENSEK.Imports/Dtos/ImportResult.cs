using ENSEK.Imports.Dtos.MeterReading;

namespace ENSEK.Imports.Dtos;

public class ImportResult
{
    public IList<string> Errors { get; init; }

    public IList<MeterReadingDto> Imports { get; init; }
}
