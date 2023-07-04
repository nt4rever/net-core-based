using System.ComponentModel.DataAnnotations;

namespace net_core_based.Helpers.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        public override bool IsValid(object? value)
        {
            if (value is IFormFile file)
            {
                if (file.Length > _maxFileSize)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
