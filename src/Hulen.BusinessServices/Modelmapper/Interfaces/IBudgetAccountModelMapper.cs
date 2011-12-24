using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.BusinessServices.ServiceModel;
using Hulen.Storage.DTO;

namespace Hulen.BusinessServices.Modelmapper.Interfaces
{
    public interface IBudgetAccountModelMapper
    {
        BudgetAccountDTO ToDTO(BudgetAccount model);
        BudgetAccount FromDTO(BudgetAccountDTO dto);
    }
}
