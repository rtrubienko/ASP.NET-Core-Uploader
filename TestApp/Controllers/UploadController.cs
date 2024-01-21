using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json;
using TestApp.Data;
using TestApp.Data.Models;

namespace TestApp.Controllers
{
    public class UploadController : Controller
    {
        private readonly AppDbContent _context;

        public UploadController(AppDbContent context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(Upload upload)
        {
            if (!ModelState.IsValid || upload.File == null)
            {
                return View("Index");
            }

            var fileName = Path.GetFileName(upload.File.FileName);
            var fileExtension = Path.GetExtension(fileName)?.ToLower();

            try
            {
                string jsonData;

                using (var streamReader = new StreamReader(upload.File.OpenReadStream()))
                {
                    var textData = await streamReader.ReadToEndAsync();
                    jsonData = fileExtension == ".json" ? textData : ParseTextToJson(textData).ToString();

                    using (JsonDocument.Parse(jsonData)) { }

                    var uploadJson = new UploadJson
                    {
                        jsonName = Path.ChangeExtension(fileName, "json"),
                        jsonData = jsonData
                    };

                    _context.UploadJson.Add(uploadJson);
                    await _context.SaveChangesAsync();
                }
            }
            catch (System.Text.Json.JsonException ex)
            {
                ModelState.AddModelError("File", $"Invalid JSON format: {ex.Message}");
                return View("Index");
            }

            return Redirect("/");
        }

        private JToken ParseTextToJson(string text)
        {
            var result = new JObject();
            var lines = text.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var segments = line.Split(':', StringSplitOptions.RemoveEmptyEntries);

                if (segments.Length < 2)
                {
                    continue;
                }

                var current = result;

                for (int i = 0; i < segments.Length - 2; i++)
                {
                    var key = segments[i];

                    if (!current.ContainsKey(key))
                    {
                        current[key] = new JObject();
                    }

                    current = (JObject)current[key];
                }

                var lastKey = segments[segments.Length - 2].Trim();
                lastKey = lastKey.Replace("\r", string.Empty).Replace("\n", string.Empty);

                var value = segments[segments.Length - 1].Trim();

                current[lastKey] = value;
            }

            return result;
        }
    }
}
