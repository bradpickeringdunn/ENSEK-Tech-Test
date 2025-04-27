
namespace ENSEK.Imports.Dtos;

public class MeterReadingDto
{
    public int AccountId { get; internal set; }
    public DateTime DateTime { get; internal set; }
    public decimal Value { get; internal set; }
}
