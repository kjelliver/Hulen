using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Hulen.WebCode.Models
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
}
