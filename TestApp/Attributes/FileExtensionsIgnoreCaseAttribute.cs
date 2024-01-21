using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace TestApp.Attributes
{
    public class FileExtensionsIgnoreCaseAttribute : ValidationAttribute
    {
        private readonly string[] _validExtensions;

        public FileExtensionsIgnoreCaseAttribute(params string[] validExtensions)
        {
            _validExtensions = validExtensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var fileExtension = Path.GetExtension(file.FileName);
                foreach (var validExtension in _validExtensions)
                {
                    if (string.Equals(fileExtension, validExtension, StringComparison.OrdinalIgnoreCase))
                    {
                        return ValidationResult.Success;
                    }
                }

                return new ValidationResult($"Invalid file type. Allowed types: {string.Join(", ", _validExtensions)}");
            }

            return ValidationResult.Success;
        }
    }
}
