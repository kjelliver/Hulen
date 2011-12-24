using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hulen.WebCode.ViewModels
{
    public class LogInModel
    {
        [Required]
        [DisplayName("Brukernavn")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Passord")]
        public string Password { get; set; }
    }

    public class NewPasswordModel
    {
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("NewPassord")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("RepeatPassord")]
        public string RepeatPassword { get; set; }
    }
}
