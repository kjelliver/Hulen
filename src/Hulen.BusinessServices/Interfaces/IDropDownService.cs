using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IDropDownService
    {
        List<string> GetDropDownStrings(string content);
    }
}
