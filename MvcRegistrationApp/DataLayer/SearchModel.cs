using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace DataLayer
{
   public class SearchModel
    {
        public string searchBy { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string ResourceName { get; set; }
        public string ProjectName { get; set; }
        public string PartnerName { get; set; }
        public List<ResourceInfo> EmployeeList { get; set; }
        public List<ListItem> DisplayColumnsList { get; set; }
    }
}
