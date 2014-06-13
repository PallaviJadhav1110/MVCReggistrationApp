using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sqlConn
{
    class RegistrationModel
    {
        public int SOWNo { get; set; }
        
        public string SignedStatus { get; set; }
       
        public string JoiningStatus { get; set; }
        
        public DateTime SOWDate { get; set; }
       
        public string PartnerName { get; set; }
       
        public string ClientName { get; set; }
        
        public string ProjectName { get; set; }
        
        public string ResourceName { get; set; }
       
        public string Technology { get; set; }
        
        public string FixedBidOrTM { get; set; }
        
        public string ReportingManager { get; set; }
       
        public DateTime AssignmentStartDate { get; set; }
       
        public DateTime TentativeEndDate { get; set; }
        
        public double EstimatedRateValue { get; set; }
        public int Duration { get; set; }
        public double ApproxRateperDay { get; set; }
        public double ApproxPartnerCost { get; set; }
        public string EstimateRate { get; set; }
    }
}
