using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.BusinessServices.ServiceModel;

namespace Hulen.WebCode.ViewModels
{
    public class HotelViewModel
    {
        public IEnumerable<Hotel> Hotels { get; set; }
        public Hotel Hotel { get; set; }
    }
}
