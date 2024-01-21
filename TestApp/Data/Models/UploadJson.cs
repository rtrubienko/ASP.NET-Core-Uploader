using System.ComponentModel.DataAnnotations;

namespace TestApp.Data.Models
{
    public class UploadJson
    {
        [Key]
        public int id { get; set; }
        public string jsonData { get; set; }
        public string jsonName { get; set; }
    }
}
