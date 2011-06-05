using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private readonly IAccountInfoPartsRepository _partsRepository;
        private readonly IAccountInfoResultsRepository _resultsRepository;
        private readonly IAccountInfoWeekRepository _weekRepository;

        public DropDownService(IAccountInfoPartsRepository partsRepository, IAccountInfoResultsRepository resultsRepository, IAccountInfoWeekRepository weekRepository)
        {
            _partsRepository = partsRepository;
            _resultsRepository = resultsRepository;
            _weekRepository = weekRepository;
        }

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

        public IEnumerable<AccountInfoPartsDTO> GetAllPartsDTO()
        {
            return _partsRepository.GetAll();
        }

        public IEnumerable<AccountInfoResultsDTO> GetAllResultsDTO()
        {
            return _resultsRepository.GetAll();
        }

        public IEnumerable<AccountInfoWeekDTO> GetAllWeeksDTO()
        {
            return _weekRepository.GetAll();
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
