using System;
using System.Collections.Generic;
using Hulen.Objects.ViewModels;

namespace Hulen.WebCode.Models
{
    public class AccountInfoWebModel
    {
        public IEnumerable<AccountInfoViewModel> AccountInfos { get; set; }
        public AccountInfoViewModel AccountInfo { get; set; }
        

        public List<String> PartsCategories { get; set; }
        public List<String> ResultCategories { get; set; }
        public List<String> WeekCategories { get; set; }
        public List<String> IsIncomes { get; set; }
    }
}