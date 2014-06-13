using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    class MailFields
    {
        public string PartnerName { get; set; }
        public string ClientName { get; set; }
        public string ProjectName { get; set; }
        public string ResourceName { get; set; }
        public string Technology { get; set; }
        public string FixedBidTM { get; set; }
        public string ReportingManager { get; set; }
        public DateTime AssignmentStartDate { get; set; }
        public DateTime AssignmentEndDate { get; set; }
        public string EstimatedRateValue { get; set; }
       

    }
}
