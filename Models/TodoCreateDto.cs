using System.ComponentModel.DataAnnotations;

namespace net_core_based.Models
{
    public class TodoCreateDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(2000)]
        public string Content { get; set; } = string.Empty;
    }
}
