using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.IO;
using ENSEK.Imports.Parsers;
using ENSEK.Imports.Importers;
using ENSEK.Imports.Dtos.MeterReading;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Authorization;

namespace ENSEK.Controllers
{
    //[Authorize] Given time id add authentication
    [ApiController]
    [Route("[controller]")]
    public class CsvImportController : Controller
    {
        private readonly ICsvParser _csvParser;
        private readonly ICsvImporter _csvImporter;
        private readonly ILogger<CsvImportController> _logger;

        public CsvImportController(ICsvParser csvParser, ICsvImporter csvImporter, ILogger<CsvImportController> logger)
        {
            _csvParser = csvParser;
            _csvImporter = csvImporter;
            _logger = logger;
        }

        [HttpPost("meter-reading-uploads")]
        public async Task<IActionResult> Post(IFormFile file, CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file was uploaded or the file is empty.");
            
            if (!file.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                return BadRequest("The uploaded file is not a CSV file.");
            
            try
            {
                using var streamReader = new StreamReader(file.OpenReadStream(), Encoding.UTF8);
                var csvContent = await streamReader.ReadToEndAsync();

                var result = await _csvParser.ParseCsv(csvContent);
                await _csvImporter.UpsertImports(result.Records, cancellationToken);

                return Ok(new
                {
                    successfulUploadCount = result.Records.Count,
                    FailedUploadCount = result.Records.Count,
                    ErrorMessage = result.Errors.Any() ? $"The following errors occured: {result.Errors.ToString()}" : string.Empty
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while processing the csv import");
            }
        }

    }
}
