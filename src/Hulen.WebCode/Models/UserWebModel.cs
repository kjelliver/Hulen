using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Objects.DTO;

namespace Hulen.WebCode.Models
{
    public class UserWebModel
    {
        public string UserNameStoredInDb { get; set; }
        public IEnumerable<UserDTO> Users { get; set; }
        public UserDTO User { get; set; }
    }
}
