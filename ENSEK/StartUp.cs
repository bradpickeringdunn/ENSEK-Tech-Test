using ENSEK.DataAccess;
using ENSEK.Imports.Importers;
using ENSEK.Imports.Parsers;
using ENSEK.Imports.Validators;
using Microsoft.EntityFrameworkCore;

namespace ENSEK;

public class StartUp
{
    public IConfiguration Configuration {  get; init; }

    public StartUp(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection serviceCollection)
    {
        //Configure EF
        serviceCollection.AddDbContext<DataAccess.ENSEKDbContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("ENSEKDb"), sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure();
                sqlOptions.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds);
            });
        });

        serviceCollection.AddScoped<ICsvParser, CsvParser>();
        serviceCollection.AddScoped<ICsvImporter, CsvImporter>();
        serviceCollection.AddScoped<ICsvValidator, CsvValidator>();

    }
}