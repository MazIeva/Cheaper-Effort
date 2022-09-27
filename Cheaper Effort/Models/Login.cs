using System.ComponentModel.DataAnnotations;

namespace Cheaper_Effort.Models
{
    public class Login
    {
        public uint Id { get; set; }

        [Required]
        public string Username { get; set; } = String.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = String.Empty;

    }
}
