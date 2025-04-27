using System;
using System.Collections.Generic;

namespace ENSEK.DataAccess.Entities;

public partial class MeterReading
{
    public Guid Id { get; set; }

    public int AccountId { get; set; }

    public DateTime MeterReadingDateTime { get; set; }

    public decimal MeterReadValue { get; set; }

    public virtual Account Account { get; set; } = null!;
}
