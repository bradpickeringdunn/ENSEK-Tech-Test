using ENSEK.DataAccess;
using ENSEK.DataAccess.Entities;
using ENSEK.Imports.Dtos;

namespace ENSEK.Imports.Importers;

public class CsvImporter : ICsvImporter
{
    private readonly DataAccess.ENSEKDbContext _dbContext;

    public CsvImporter(ENSEKDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Import(List<MeterReadingDto> records)
    {
        foreach (var record in records)
        {
            _dbContext.MeterReadings.Add(new MeterReading
            {
                Id = Guid.NewGuid(),
                AccountId = record.AccountId,
                MeterReadingDateTime = record.DateTime,
                MeterReadValue = record.Value
            });
        }

        _dbContext.SaveChanges();
    }
}
