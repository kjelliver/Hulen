using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Hulen.WebCode.MvcBase
{
    public class HulenController : Controller
    {
        public IEnumerable<int> GetYearsForCombobox()
        {
            return new List<int> {2005, 2006, 2007, 2008, 2009, 2010, 2011, 2012, 2013};
        }
    }
}
