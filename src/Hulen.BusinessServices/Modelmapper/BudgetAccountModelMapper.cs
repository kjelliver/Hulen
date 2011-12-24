using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.BusinessServices.Modelmapper.Interfaces;
using Hulen.BusinessServices.ServiceModel;
using Hulen.Storage.DTO;

namespace Hulen.BusinessServices.Modelmapper
{
    public class BudgetAccountModelMapper : IBudgetAccountModelMapper
    {
        public BudgetAccountDTO ToDTO(BudgetAccount model)
        {
            return new BudgetAccountDTO
                       {
                            Id = model.Id,
                            AccountNumber = model.AccountNumber,
                            Year = model.Year,
                            BudgetStatus = model.BudgetStatus,
                            YearAmount = model.YearAmount,
                            JanuaryAmount = model.JanuaryAmount,
                            FebruaryAmount = model.FebruaryAmount,
                            MarchAmount = model.MarchAmount,
                            AprilAmount = model.AprilAmount,
                            MayAmount = model.MayAmount,
                            JuneAmount = model.JuneAmount,
                            JulyAmount = model.JulyAmount,
                            AugustAmount = model.AugustAmount,
                            SeptemberAmount = model.SeptemberAmount,
                            OctoberAmount = model.OctoberAmount,
                            NovemberAmount = model.NovemberAmount,
                            DecemberAmount = model.DecemberAmount
                       };
        }

        public BudgetAccount FromDTO(BudgetAccountDTO dto)
        {
            return new BudgetAccount
                       {
                           Id = dto.Id,
                           AccountNumber = dto.AccountNumber,
                           Year = dto.Year,
                           BudgetStatus = dto.BudgetStatus,
                           YearAmount = dto.YearAmount,
                           JanuaryAmount = dto.JanuaryAmount,
                           FebruaryAmount = dto.FebruaryAmount,
                           MarchAmount = dto.MarchAmount,
                           AprilAmount = dto.AprilAmount,
                           MayAmount = dto.MayAmount,
                           JuneAmount = dto.JuneAmount,
                           JulyAmount = dto.JulyAmount,
                           AugustAmount = dto.AugustAmount,
                           SeptemberAmount = dto.SeptemberAmount,
                           OctoberAmount = dto.OctoberAmount,
                           NovemberAmount = dto.NovemberAmount,
                           DecemberAmount = dto.DecemberAmount
                       };
        }
    }
}
