using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOWTrackerService
{
    public class SearchClass
    {
        public string ResourceName { get; set; }
        public string ProjectName { get; set; }
        public string PartnerName { get; set; }
        public List<ResourceInfo> EmployeeList { get; set; }
    }
}