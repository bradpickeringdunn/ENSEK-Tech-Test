using ENSEK.Imports.Dtos;

namespace ENSEK.Imports.Validators;

public interface ICsvValidator<T> where T : class
{
    Task<IList<string>> ValidateHeaders(string[] strings);
    Task<IList<string>> ValidateRows(Dictionary<string, string> record);
}
