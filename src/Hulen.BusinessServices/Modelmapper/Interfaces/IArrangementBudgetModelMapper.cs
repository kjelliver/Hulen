using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.BusinessServices.ServiceModel;
using Hulen.Storage.DTO;

namespace Hulen.BusinessServices.Modelmapper.Interfaces
{
    public interface IArrangementBudgetModelMapper
    {
        ArrangementBudgetDTO ToDTO(ArrangementBudget model);
        ArrangementBudget FromDTO(ArrangementBudgetDTO dto);
    }
}
