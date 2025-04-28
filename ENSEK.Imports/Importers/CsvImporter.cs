using ENSEK.DataAccess;
using ENSEK.DataAccess.Entities;
using ENSEK.Imports.Dtos.MeterReading;
using Microsoft.EntityFrameworkCore;

namespace ENSEK.Imports.Importers;

public class CsvImporter : ICsvImporter
{
    private readonly DataAccess.ENSEKDbContext _dbContext;

    public CsvImporter(ENSEKDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task UpsertImports(IList<MeterReadingDto> records, CancellationToken cancellationToken)
    {
        var dates = records.Select(x => x.DateTime).ToList();
        var accountIds = records.Select(x =>x.AccountId).ToList();
        var existingRecords = await _dbContext.MeterReadings.Where(x => dates.Contains(x.MeterReadingDateTime) && accountIds.Contains(x.AccountId))
            .GroupBy(x => x.AccountId)
            .ToDictionaryAsync(x => x.Key, y => y.ToList(), cancellationToken);
            
        foreach (var record in records)
        {
            if (existingRecords.TryGetValue(record.AccountId, out var existingDates))
            {
                var existingRecord = existingDates.First(x => x.MeterReadingDateTime == record.DateTime);
                existingRecord.MeterReadValue = record.Value;
            }
            else
            {
                _dbContext.MeterReadings.Add(new MeterReading
                {
                    Id = Guid.NewGuid(),
                    AccountId = record.AccountId,
                    MeterReadingDateTime = record.DateTime,
                    MeterReadValue = record.Value
                });
            }
        }

        _dbContext.SaveChanges();
    }
}
