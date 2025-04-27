using ENSEK.Imports.Dtos.MeterReading;

namespace ENSEK.Imports.Importers;

public interface ICsvImporter
{
    Task Import(IList<MeterReadingDto> records);
}
