using System.Collections.Generic;
using Hulen.BusinessServices.ServiceModel;

namespace Hulen.WebCode.ViewModels
{
    public class UserWebModel
    {
        public string UserNameStoredInDb { get; set; }
        public IEnumerable<User> Users { get; set; }
        public User User { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
