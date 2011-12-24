using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.BusinessServices.Modelmapper.Interfaces;
using Hulen.BusinessServices.ServiceModel;
using Hulen.Storage.DTO;

namespace Hulen.BusinessServices.Modelmapper
{
    public class BudgetModelMapper : IBudgetModelMapper
    {
        public BudgetDTO ToDTO(Budget model)
        {
            throw new NotImplementedException();
        }

        public Budget FromDTO(BudgetDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
