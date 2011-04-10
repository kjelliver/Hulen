using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Hulen.Objects.DTO;

namespace Hulen.BusinessServices.Interfaces
{
    public interface IDropDownService
    {
        List<string> GetDropDownStrings(string content);
        IEnumerable<AccountInfoPartsDTO> GetAllPartsDTO();
        IEnumerable<AccountInfoResultsDTO> GetAllResultsDTO();
        IEnumerable<AccountInfoWeekDTO> GetAllWeeksDTO();
    }
}
