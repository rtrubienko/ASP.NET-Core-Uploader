using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using TestApp.Attributes;

namespace TestApp.Data.Models
{
    public class Upload
    {
        [Required(ErrorMessage = "Required")]
        [FileExtensionsIgnoreCase(".json", ".txt", ErrorMessage = "Invalid file type. Allowed types: .json, .txt")]
        public IFormFile File { get; set; }
    }
}
