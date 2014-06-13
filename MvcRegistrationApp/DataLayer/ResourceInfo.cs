using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DataLayer
{
    public class ResourceInfo
    {   
        public string  message { get; set; }
        public string AttachFile1 { get; set; }
        public int count { get; set; }
        public string SOWNo { get; set; }
        [Required(ErrorMessage = "Enter FinanceEmailAddress")]
        public string FinanceEmailAddress  { get; set; }
        [Required(ErrorMessage = "Enter OnBoardEmailAddress")]
        public string OnBoardEmailAddress { get; set; }
        [Required(ErrorMessage = "select SignedStatus ")]
        public string SignedStatus { get; set; }
        [Required(ErrorMessage = "Select TechStream")]
        public string TechStream { get; set; }
       [Required(ErrorMessage = "Select JoiningStatus")]
        public string JoiningStatus { get; set; }
        [Required(ErrorMessage = "Enter SOWDate")]
        [DataType(DataType.Date)]
        public DateTime SOWDate { get; set; }
        [Required(ErrorMessage = "Enter PartnerName")]
        public string PartnerName { get; set; }
      [Required(ErrorMessage = "Enter ClientName")]
        public string ClientName { get; set; }
       [Required(ErrorMessage = "Enter ProjectName")]
        public string ProjectName { get; set; }
        [Required(ErrorMessage = "Enter ResourceName")]
        public string ResourceName { get; set; }
        [Required(ErrorMessage = "Enter Technology")]
        public string Technology { get; set; }
       [Required(ErrorMessage = "Select FixedBidOrTM")]
        public string FixedBidOrTM { get; set; }
      [Required(ErrorMessage = "Enter ReportingManager")]
        public string ReportingManager { get; set; }
        public int id { get; set; }
        public string UpdatewithExt { get; set; }
       [Required(ErrorMessage = "Enter AssignmentStartDate")]
        public DateTime AssignmentStartDate { get; set; }
        [Required(ErrorMessage = "Enter TentativeEndDate")]
        public DateTime TentativeEndDate { get; set; }
        public string Flag { get; set; }
        [Required(ErrorMessage = "Enter EstimatedRateValue")]
        public string EstimatedRateValue { get; set; }
        //public int Duration { get; set; }
        //public double ApproxRateperDay { get; set; }
        //public double ApproxPartnerCost { get; set; }
        //public string EstimateRate { get; set; }
        [Required(ErrorMessage = "Enter WorkLocation")]
        public string WorkLocation { get; set; }
       [Required(ErrorMessage = "Enter BussinessUnit")]
        public string BussinessUnit { get; set; }
       [Required(ErrorMessage = "Enter Designation")]
        public string Designation { get; set; }
        [Required(ErrorMessage = "Enter ReportingTime")]
        public string ReportingTime { get; set; }
        [Required(ErrorMessage = "Enter RecruiterName")]
        public string RecruiterName { get; set; }
        [Required(ErrorMessage = "Enter HardwareRequirement")]
        public string HardwareRequirement { get; set; }
         [Required(ErrorMessage = "Enter SoftwareRequirement")]
        public string SoftwareRequirement { get; set; }

        public bool GuestAccomodation { get; set; }

        public string update { get; set; }

        public string IsGuestAccomodated
        {
            get { return GuestAccomodation ?  "Yes": "No"; }
        }
        
        public string Remarks { get; set; }

        //hidden property for EngagementSummary view to maintain month in hidden field

        public string month_Year { get; set; }
        

    }
}