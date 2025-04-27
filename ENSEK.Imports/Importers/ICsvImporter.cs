using ENSEK.Imports.Dtos;

namespace ENSEK.Imports.Importers;

public interface ICsvImporter
{
    Task Import(List<MeterReadingDto> records);
}
