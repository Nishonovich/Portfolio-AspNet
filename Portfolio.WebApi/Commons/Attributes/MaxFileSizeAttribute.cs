using Portfolio.WebApi.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.WebApi.Commons.Attributes
{
    public class MaxFileSizeAttribute:ValidationAttribute
    {
        private readonly int _maxFileSize;
        private readonly bool _isFileNullable;

        public MaxFileSizeAttribute(int maxFileSize, bool isFileNullable = false)
        {
            _maxFileSize = maxFileSize;
            _isFileNullable = isFileNullable;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null && _isFileNullable)
                return ValidationResult.Success;

            if (value is null)
                return new ValidationResult("Value cannot be null");

            var file = (IFormFile)value;

            return FileSizeHelper.ByteToMB(file.Length) > _maxFileSize
                ? new ValidationResult($"Image must be less than {_maxFileSize} MB")
                : ValidationResult.Success;
        }
    }
}
