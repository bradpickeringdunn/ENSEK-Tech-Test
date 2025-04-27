using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.IO;
using ENSEK.Imports.Parsers;
using ENSEK.Imports.Importers;
using ENSEK.Imports.Dtos.MeterReading;

namespace ENSEK.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CsvImportController : Controller
    {
        private readonly ICsvParser _csvParser;
        private readonly ICsvImporter _csvImporter;

        public CsvImportController(ICsvParser csvParser, ICsvImporter csvImporter)
        {
            _csvParser = csvParser;
            _csvImporter = csvImporter;
        }

        [HttpPost("upload-csv")]
        public async Task<IActionResult> UploadCsv(IFormFile file, CancellationToken cancellationToken)
        {
            // Validate the file
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file was uploaded or the file is empty.");
            }

            if (!file.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("The uploaded file is not a CSV file.");
            }

            try
            {
                // Read the CSV file content
                using var streamReader = new StreamReader(file.OpenReadStream(), Encoding.UTF8);
                var csvContent = await streamReader.ReadToEndAsync();

                // Process the CSV content
                var result = await _csvParser.ParseCsv(csvContent);

                await _csvImporter.Import(result.Records);

                // Return the parsed records as a response (or process them further)
                return Ok(new
                {
                    Message = "CSV file processed successfully.",
                    Records = result.Records
                });
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during file processing
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while processing the file: {ex.Message}");
            }
        }

    }
}
