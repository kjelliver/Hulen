using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulen.BusinessServices.ServiceModel
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SingleRoomPrice { get; set; }
        public int DoubleRoomPrice { get; set; }
        public int TripleRoomPrice { get; set; }
        public int GroupRoomPrice { get; set; }
        public int ExtraBedPrice { get; set; }
        public bool IsActive { get; set; } 
    }
}
