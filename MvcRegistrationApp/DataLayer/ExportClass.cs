using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
     public class ExportClass
    {
         public string SOWNo { get; set; }
         //public int id { get; set; }
       
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
        
        public string EstimatedRateValue { get; set; }
       
        public string WorkLocation { get; set; }
        public string BussinessUnit { get; set; }
        public string Designation { get; set; }
        public string ReportingTime { get; set; }
        public string RecruiterName { get; set; }
        public string HardwareRequirement { get; set; }
        public string SoftwareRequirement { get; set; }
        public string FinanceEmailAddress { get; set; }
        public string OnBoardEmailAddress { get; set; }

       
        public string IsGuestAccomodated
        {
            get { return GuestAccomodation ? "Yes" : "No"; }
        }

        public string Remarks { get; set; }


        public string AttachFile1 { get; set; }

        
        public bool GuestAccomodation { get; set; }
    }
}
