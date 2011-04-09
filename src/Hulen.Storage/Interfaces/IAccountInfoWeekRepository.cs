using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Objects.DTO;

namespace Hulen.Storage.Interfaces
{
    public interface IAccountInfoWeekRepository
    {
        ICollection<AccountInfoWeekDTO> GetAll();
    }
}
