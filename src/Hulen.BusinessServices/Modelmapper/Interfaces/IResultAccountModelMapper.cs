using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.BusinessServices.ServiceModel;
using Hulen.Storage.DTO;

namespace Hulen.BusinessServices.Modelmapper.Interfaces
{
    public interface IResultAccountModelMapper
    {
        ResultAccountDTO ToDTO(ResultAccount model);
        ResultAccount FromDTO(ResultAccountDTO dto);
    }
}
