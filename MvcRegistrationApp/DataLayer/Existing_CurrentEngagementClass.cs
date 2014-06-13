using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
   public class Existing_CurrentEngagementClass
    {
        public int id { get; set; }
       
        public string ClientName { get; set; }

        public string ProjectName { get; set; }

        public string ResourceName { get; set; }

        public string Technology { get; set; }
        public DateTime TentativeEndDate { get; set; }
       
        public string EstimatedRateValue { get; set; }
    }
}
