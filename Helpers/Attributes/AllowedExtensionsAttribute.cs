using System.ComponentModel.DataAnnotations;

namespace net_core_based.Helpers.Attributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        public override bool IsValid(object? value)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file!.FileName);
                if (!_extensions.Contains(extension.ToLower())) return false;

            }            
            return true;
        }
    }
}
