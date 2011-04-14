using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IResultAccountService
    {
        void ImportFile(Stream inputStream, string month, string year);
    }
}
