using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.BusinessServices.Interfaces;
using Hulen.BusinessServices.Modelmapper.Interfaces;
using Hulen.BusinessServices.ServiceModel;
using Hulen.Storage.Interfaces;

namespace Hulen.BusinessServices.Services
{
    public class FixedArrangementCostsService : IFixedArrangementCostsService
    {
        private readonly IFixedArrangementCostsRepository _repository;
        private readonly IFixedArrangementCostsModelMapper _modelMapper;

        public FixedArrangementCostsService(IFixedArrangementCostsRepository repository, IFixedArrangementCostsModelMapper modelMapper)
        {
            _repository = repository;
            _modelMapper = modelMapper;
        }

        public FixedArrangementCosts GetFixedArrangementCosts()
        {
            return _modelMapper.ToServiceModel(_repository.GetOne());
        }

        public void UpdateFixedArrangementCosts(FixedArrangementCosts fixedArrangementCosts)
        {
            fixedArrangementCosts.UpdateGeneratedDate();
            _repository.UpdateOne(_modelMapper.ToDTO(fixedArrangementCosts));
        }
    }
}
