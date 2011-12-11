using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.BusinessServices.ServiceModel;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IFixedArrangementCostsService
    {
        FixedArrangementCosts GetFixedArrangementCosts();
        void UpdateFixedArrangementCosts(FixedArrangementCosts fixedArrangementCosts);
    }
}
