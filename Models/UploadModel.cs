using net_core_based.Helpers.Attributes;
using System.ComponentModel.DataAnnotations;

namespace net_core_based.Models
{
    public class UploadModel
    {
        [Display(Name = "File")]
        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024, ErrorMessage = "Maximum allowed file size is 5 mb.")]
        [AllowedExtensions(new string[] { ".jpg", ".png" }, ErrorMessage = "This file extension is not allowed.")]
        public IFormFile File { get; set; }
    }
}
