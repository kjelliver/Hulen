using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulen.Objects.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password {get; set; }
        public string Name { get; set; }
        public bool Disabled { get; set; }

        //Access controllers
        public bool HomeAccessTo { get; set; }
        public bool UserAdminAccessTo { get; set; }
        public bool AccountInfoAccessTo { get; set; }
        public bool FileImportAccessTo { get; set; }
        public bool ReportsAccessTo { get; set; }

        //Visible menu items
        public bool AdminAccessTo { get; set; }
    }
}
