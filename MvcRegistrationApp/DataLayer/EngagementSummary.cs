using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace DataLayer
{
     public class EngagementSummary
    {
        public Months ExistingEngagements { get; set; }
        public Months ExtentedEngagements { get; set; }
        public Months CompletedEngagements { get; set; }
        public List<Existing_CurrentEngagementClass> ExistingEngagementList { get; set; }
        public List<Existing_CurrentEngagementClass> ListEngagementsOverInCurrentWeek { get; set; }
       
        public List<ResourceInfo> MonthlyRecordList { get; set; }
        public List<ListItem> DisplayTechnologyList { get; set; }
        public List<ListItem> VendorLIst { get; set; }
        public List<ExportClass> ExportMonthlyDatalist { get; set; }
        public List<ExportClass> ExportExistingEngagementList { get; set; }
        public string month_year  { get; set; }
    }
}
