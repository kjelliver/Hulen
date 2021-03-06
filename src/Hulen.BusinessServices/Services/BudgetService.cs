﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Excel;
using Hulen.BusinessServices.Interfaces;
using Hulen.BusinessServices.Modelmapper.Interfaces;
using Hulen.BusinessServices.ServiceModel;
using Hulen.Storage.DTO;
using Hulen.Storage.Interfaces;
using Hulen.Storage.Repositories;

namespace Hulen.BusinessServices.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly IBudgetAccountModelMapper _budgetAccountModelMapper;
        private readonly IBudgetModelMapper _budgetModelMapper;


        public BudgetService(IBudgetRepository budgetRepository, IBudgetAccountModelMapper budgetAccountModelMapper, IBudgetModelMapper budgetModelMapper)
        {
            _budgetRepository = budgetRepository;
            _budgetModelMapper = budgetModelMapper;
            _budgetAccountModelMapper = budgetAccountModelMapper;
        }

        public IEnumerable<Budget> GetOverview()
        {
            var result = new List<Budget>();
            var fromDb = _budgetRepository.GetOverview();
            foreach(var dto in fromDb)
            {
                result.Add(_budgetModelMapper.FromDTO(dto));
            }
            return result;
        }

        public Budget GetOneBudgetByYearAndStatus(int year, string status)
        {
            return _budgetModelMapper.FromDTO(_budgetRepository.GetOverviewByYearAndStatus(year, status));
        }

        public void DeleteAllBudgetsByYearAndStatus(int year, string budgetStatus)
        {
            _budgetRepository.DeleteExistingBudgetByYearAndStatus(year, GetBudgetStatus(budgetStatus));
            _budgetRepository.DeleteExistingBudgetBudgetOverview(year, budgetStatus);
        }

        public void ImportFile(Stream inputStream, string year, string budgetStatus, string comment)
        {
            DeleteAllBudgetsByYearAndStatus(Convert.ToInt32(year), budgetStatus);
            var dataSet = ConvertStreamToDataSet(inputStream);
            List<BudgetAccountDTO> budgets = ConvertDataSetToObjectCollection(dataSet, Convert.ToInt32(year), budgetStatus);
            _budgetRepository.Add(budgets);
            SaveInBudgetOverView(year, budgetStatus, comment);
        }

        public IEnumerable<BudgetAccount> GetAllBudgetAccountsByYearAndStatus(int year, string budgetStatus)
        {
            var result = new List<BudgetAccount>();
            var fromDb = _budgetRepository.GetBudgetAccountsByYearAndStatus(year, GetBudgetStatus(budgetStatus));
            foreach(var dto in fromDb)
            {
                result.Add(_budgetAccountModelMapper.FromDTO(dto));
            }
            return result;
        }

        private DataSet ConvertStreamToDataSet(Stream inputStream)
        {
            IExcelDataReader reader = ExcelReaderFactory.CreateBinaryReader(inputStream);
            return reader.AsDataSet();
        }

        private List<BudgetAccountDTO> ConvertDataSetToObjectCollection(DataSet dataSet, int year, string budgetStatus)
        {
            var budgets = new List<BudgetAccountDTO>();

            foreach (DataRow dataRow in dataSet.Tables[GetSheetName(budgetStatus)].Rows)
            {
                var temp = new BudgetAccountDTO();
                if (dataRow[0].ToString() != "")
                {
                    temp.AccountNumber = Convert.ToInt32(dataRow[0]) ;
                    temp.Year = year;
                    temp.BudgetStatus = GetBudgetStatus(budgetStatus);
                    temp.YearAmount = Convert.ToDouble(dataRow[2]);
                    temp.JanuaryAmount = Convert.ToDouble(dataRow[3]);
                    temp.FebruaryAmount = Convert.ToDouble(dataRow[4]);
                    temp.MarchAmount = Convert.ToDouble(dataRow[5]);
                    temp.AprilAmount = Convert.ToDouble(dataRow[6]);
                    temp.MayAmount = Convert.ToDouble(dataRow[7]);
                    temp.JuneAmount = Convert.ToDouble(dataRow[8]);
                    temp.JulyAmount = Convert.ToDouble(dataRow[9]);
                    temp.AugustAmount = Convert.ToDouble(dataRow[10]);
                    temp.SeptemberAmount = Convert.ToDouble(dataRow[11]);
                    temp.OctoberAmount = Convert.ToDouble(dataRow[12]);
                    temp.NovemberAmount = Convert.ToDouble(dataRow[13]);
                    temp.DecemberAmount = Convert.ToDouble(dataRow[14]);
                    budgets.Add(temp);
                }
            }
            return budgets;
        }

        private int GetBudgetStatus(string budgetStatus)
        {
            if (budgetStatus == "Revidert")
                return 1;
            return 0;
        }

        private string GetSheetName(string budgetStatus)
        {
            if (budgetStatus == "Revidert")
                return "Revidert_mnd";
            return "Budsjett_mnd";
        }

        private void SaveInBudgetOverView(string year, string budgetStatus, string comment)
        {
            _budgetRepository.SaveOneOverView(new BudgetDTO { Year = Convert.ToInt32(year), BudgetStatus = budgetStatus, Comment = comment });
        }
    }
}
