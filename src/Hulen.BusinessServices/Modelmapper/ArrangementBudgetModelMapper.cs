using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.BusinessServices.Modelmapper.Interfaces;
using Hulen.BusinessServices.ServiceModel;
using Hulen.Storage.DTO;

namespace Hulen.BusinessServices.Modelmapper
{
    public class ArrangementBudgetModelMapper : IArrangementBudgetModelMapper
    {
        public ArrangementBudgetDTO ToDTO(ArrangementBudget model)
        {
            try
            {
                return new ArrangementBudgetDTO
                           {
                               Id = model.Id,
                               Artist = model.Artist,
                               Date = model.Date,
                               ArtistFee = model.ArtistFee,
                               Status = model.Status,
                               BookerInCharge = model.BookerInCharge
                           };
            }
            catch (Exception)
            {
                throw new Exception("Error when mapping arrangement budget service model to dto");
            }
        }

        public ArrangementBudget FromDTO(ArrangementBudgetDTO dto)
        {
            try
            {
                return new ArrangementBudget
                           {
                               Id = dto.Id,
                               Artist = dto.Artist,
                               Date = dto.Date,
                               ArtistFee = dto.ArtistFee,
                               Status = dto.Status,
                               BookerInCharge = dto.BookerInCharge
                           };
            }
            catch (Exception)
            {
                throw new Exception("Error when mapping arrangement budget dto to service model.");                
            }
        }
    }
}
