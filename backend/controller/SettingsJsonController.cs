using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using settings.Models;
using System.Text.Json;

[Route("api/[controller]")]
[ApiController]
public class SettingsController : ControllerBase
{
    private readonly string _jsonFilePath;
        public SettingsController()
        {
            // Get the parent directory, with a null check
            var parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory());
            if (parentDirectory == null)
            {
                // Handle the case where there's no parent directory (root directory)
                // You might want to throw an exception or fall back to another directory
                throw new DirectoryNotFoundException("Parent directory not found.");
            }

            // Combine the parent directory path with the desired file name (data.json)
            _jsonFilePath = Path.Combine(parentDirectory.FullName, "settings.json");
        }

    // GET: api/json
    [HttpGet("getFolderLocation")]
    public async Task<IActionResult> GetJson()
    {
        if (System.IO.File.Exists(_jsonFilePath))
        {
            var jsonData = await System.IO.File.ReadAllTextAsync(_jsonFilePath);
            return Ok(jsonData);
        }
        else
        {
        // If the file doesn't exist, create it with a default structure
        var defaultData = new { logsFolderLocation = "" }; // Default data structure
        var jsonToSave = System.Text.Json.JsonSerializer.Serialize(defaultData);
        await System.IO.File.WriteAllTextAsync(_jsonFilePath, jsonToSave);
        
        // Return the default structure to the client
        return Ok(jsonToSave);
        }
    }

    // POST: api/json
    [HttpPost("setFolderLocation")]
    public async Task<IActionResult> SaveJson([FromBody] JsonData jsonData)
    {
        if (jsonData == null || string.IsNullOrEmpty(jsonData.LogsFolderLocation))
        {
            return BadRequest("Invalid data.");
        }

        try
        {
                // Check if the JSON file exists
                if (System.IO.File.Exists(_jsonFilePath))
                {
                    // Read the current JSON data
                    var currentJson = await System.IO.File.ReadAllTextAsync(_jsonFilePath);
                    var jsonDoc = JsonDocument.Parse(currentJson);
                    var rootElement = jsonDoc.RootElement;

                    // Update the logsFolderLocation field
                    var updatedJson = new
                    {
                        logsFolderLocation = jsonData.LogsFolderLocation, // Update the field
                        // Preserve other fields if necessary
                        //otherField1 = rootElement.GetProperty("otherField1").GetString(), // Example of preserving other fields
                        //otherField2 = rootElement.GetProperty("otherField2").GetString() // Example of preserving other fields
                    };

                    // Serialize the updated JSON object
                    var updatedJsonString = JsonSerializer.Serialize(updatedJson);

                    // Write the updated data back to the file
                    await System.IO.File.WriteAllTextAsync(_jsonFilePath, updatedJsonString);

                    return Ok(new { message = "logsFolderLocation updated successfully", data = updatedJson });
                }
                else
                {
                    return NotFound("JSON file not found.");
                }
        }
        catch (System.Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}