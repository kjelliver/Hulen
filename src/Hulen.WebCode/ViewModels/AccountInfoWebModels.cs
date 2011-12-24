using System.Collections.Generic;
using Hulen.BusinessServices.ServiceModel;

namespace Hulen.WebCode.ViewModels
{
    public class AccountInfoIndexModel
    {
        public IEnumerable<AccountInfo> AccountInfos { get; set; }
        

        public string SelectedYear { get; set; }
        public string DefaultYear { get; set; }

        public List<string> Years { get; set; }
    }

    public class AccountInfoEditModel
    {
        public AccountInfo AccountInfo { get; set; }
        public List<string> PartsCategories { get; set; }
        public List<string> ResultCategories { get; set; }
        public List<string> WeekCategories { get; set; }
        public List<string> IsIncomes { get; set; }

        public void FillDropDownLists()
        {
            ResultCategories = new List<string> { "Udefinert", "Salgsinntekter", "AndreInntekter", "Varekjøp", "Personalkostnader", "Driftskostnader", "Finansielle" };
            PartsCategories = new List<string> { "Udefinert", "Bar", "Arrangement", "Personalkostnader", "PublicRelations", "Tilskudd", "Økonomi", "Driftskostnader" };
            WeekCategories = new List<string> { "Udefinert", "PublicRelations" };
            IsIncomes = new List<string> { "Inntekt", "Utgift" };
        }
    }

    public class AccountInfoCopyModel
    {
        public List<string> CopyFromYears { get; set; }
        public List<string> CopyToYears { get; set; }

        public string SelectedCopyFromYear { get; set; }
        public string SelectedCopyToYear { get; set; }

        public void FillYears()
        {
            CopyFromYears = new List<string> { "2010", "2011", "2012" };
            CopyToYears = new List<string> { "2010", "2011", "2012" };            
        }
    }

    public class AccountInfoImportModel
    {
        public string AccountInfoYear { get; set; }
    }
}