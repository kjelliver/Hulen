using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.BusinessServices.Interfaces;
using Hulen.Objects.DTO;
using Hulen.Storage.Interfaces;
using Hulen.Storage.Repositories;

namespace Hulen.BusinessServices.Services
{
    public class DropDownService : IDropDownService
    {
        private IAccountInfoPartsRepository _partsRepository = new AccountInfoPartsRepository();
        private IAccountInfoResultsRepository _resultsRepository = new AccountInfoResultsRepository();
        private IAccountInfoWeekRepository _weekRepository = new AccountInfoWeekRepository();

        public List<string> GetDropDownStrings(string content)
        {
            if(content == "PARTS")
                return GetAccountInfoParts();
            if (content == "RESULT")
                return GetAccountInfoResults();
            if (content == "WEEK")
                return GetAccountInfoWeek();
            return new List<string>();
        }

        private List<string> GetAccountInfoParts()
        {
            List<string> strings = new List<string>();
            var parts = _partsRepository.GetAll();
            foreach (AccountInfoPartsDTO part in parts)
            {
                strings.Add(part.Name);
            }
            return strings;
        }

        private List<string> GetAccountInfoResults()
        {
            List<string> strings = new List<string>();
            var parts = _resultsRepository.GetAll();
            foreach (AccountInfoResultsDTO part in parts)
            {
                strings.Add(part.Name);
            }
            return strings;
        }

        private List<string> GetAccountInfoWeek()
        {
            List<string> strings = new List<string>();
            var parts = _weekRepository.GetAll();
            foreach (AccountInfoWeekDTO part in parts)
            {
                strings.Add(part.Name);
            }
            return strings;
        }
    }
}
