using ENSEK.Imports.Dtos.MeterReading;

namespace ENSEK.Imports.Importers;

public interface ICsvImporter
{
    Task UpsertImports(IList<MeterReadingDto> records, CancellationToken cancellationToken);
}
