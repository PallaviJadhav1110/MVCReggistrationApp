using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace DataLayer
{
    public class DashBoard
    {
        string str = String.Empty;

        public DashBoard()
        {
            str = ConfigurationManager.ConnectionStrings[constants.connection].ConnectionString;
        }

        public EngagementSummary GetEngagementSummary()
        {
            EngagementSummary ObjSummary = new EngagementSummary();
            ObjSummary.CompletedEngagements = new Months();
           

            using (SqlConnection con = new SqlConnection(str))
            {
                SqlCommand cmd = null;
                cmd = new SqlCommand(constants.SpGetEngagementCompleted, con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                try
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    
                    while (dr.Read())
                    {
                        var property = ObjSummary.CompletedEngagements.GetType().GetProperty(dr[0].ToString());
                        property.SetValue(ObjSummary.CompletedEngagements, Convert.ToInt32(dr[1]));                       
                        
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + "  in GetListAllrecord()");
                }
            }
            ObjSummary.ExtentedEngagements = new Months();
            using (SqlConnection con = new SqlConnection(str))
            {
                SqlCommand cmd = null;
                cmd = new SqlCommand(constants.SpGetEngagementExtended, con);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        var property = ObjSummary.ExtentedEngagements.GetType().GetProperty(dr[0].ToString());
                        property.SetValue(ObjSummary.ExtentedEngagements, Convert.ToInt32(dr[1]));

                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + "  in GetListAllrecord()");
                }
            }
            ObjSummary.ExistingEngagements = new Months();
            using (SqlConnection con = new SqlConnection(str))
            {
                SqlCommand cmd = null;
                cmd = new SqlCommand(constants.SpGetEngagementStarted, con);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        var property = ObjSummary.ExistingEngagements.GetType().GetProperty(dr[0].ToString());
                        property.SetValue(ObjSummary.ExistingEngagements, Convert.ToInt32(dr[1]));

                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + "  in GetListAllrecord()");
                }
            }

            return ObjSummary;
        }

        public List<Existing_CurrentEngagementClass> GetEngagementList_ExistingOrOverInCurrentWeek(string id)
        {
            EngagementSummary ObjSummary = new EngagementSummary();
            ObjSummary.ExistingEngagementList = new List<Existing_CurrentEngagementClass>();


            using (SqlConnection con = new SqlConnection(str))
            {
                SqlCommand cmd = null;
                if (id == "ExistingEngagement")
                {
                    cmd = new SqlCommand(constants.SPGetListOfExistingEngagements, con);
                }
                else if (id == "EngagementsOverInNextWeek")
                {
                    cmd = new SqlCommand(constants.SpGetEngagementsOverInNextWeek, con);
                }
                else if (id == "EngagementsOverInCurrentweek")
                {
                    cmd = new SqlCommand(constants.SpEngagementsOverInCurrentWeek, con);
                }
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        ObjSummary.ExistingEngagementList.Add(new Existing_CurrentEngagementClass
                        { 
                            id=Convert.ToInt32(dr[constants.id]),                           
                            ResourceName=Convert.ToString(dr[constants.ResourceName]),
                            Technology=Convert.ToString(dr[constants.Technology]),
                            ClientName=Convert.ToString(dr[constants.ClientName]),
                            ProjectName=Convert.ToString(dr[constants.ProjectName]),
                            EstimatedRateValue=Convert.ToString(dr[constants.EstimatedRateValue]),
                            TentativeEndDate=Convert.ToDateTime(dr[constants.AssignmentEndDate]),
                           
                        });

                    }
                    
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + "  in GetExistingEngagementList()");
                }
            }

            return ObjSummary.ExistingEngagementList;
        }

        //public List<Existing_CurrentEngagementClass> GetEngagementsOverInCurrentWeekList()
        //{
        //    EngagementSummary ObjSummary = new EngagementSummary();
        //    ObjSummary.ListEngagementsOverInCurrentWeek = new List<Existing_CurrentEngagementClass>();


        //    using (SqlConnection con = new SqlConnection(str))
        //    {
        //        SqlCommand cmd = null;
        //        cmd = new SqlCommand(constants.SpEngagementsOverInCurrentWeek, con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        try
        //        {
        //            con.Open();
        //            SqlDataReader dr = cmd.ExecuteReader();

        //            while (dr.Read())
        //            {
        //                ObjSummary.ListEngagementsOverInCurrentWeek.Add(new Existing_CurrentEngagementClass
        //                {
        //                    id = Convert.ToInt32(dr[constants.id]),
        //                    ResourceName = Convert.ToString(dr[constants.ResourceName]),
        //                    Technology = Convert.ToString(dr[constants.Technology]),
        //                    ClientName = Convert.ToString(dr[constants.ClientName]),
        //                    ProjectName = Convert.ToString(dr[constants.ProjectName]),
        //                    EstimatedRateValue = Convert.ToString(dr[constants.EstimatedRateValue]),
        //                    TentativeEndDate = Convert.ToDateTime(dr[constants.AssignmentEndDate]),
                            
        //                });

        //            }
                    
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception(ex.Message + "  in GetEngagementsOverInCurrentWeekList()");
        //        }
        //    }

        //    return ObjSummary.ListEngagementsOverInCurrentWeek;
        //}


        public List<ResourceInfo> GetMonthlyRecordList(string month_year)
        {
            EngagementSummary ObjSummary = new EngagementSummary();
            ObjSummary.MonthlyRecordList = new List<ResourceInfo>();
            string[] values=new string[3] ;
            int month=0;
            int year=0;
            if (month_year.Contains("_"))
            {

                values = month_year.Split('_');
                month = DateTime.ParseExact(values[1], "MMMM", CultureInfo.CurrentCulture).Month;
                year = Convert.ToInt32(values[2]);
            }
            using (SqlConnection con = new SqlConnection(str))
            {
                SqlCommand cmd = null;
                if (month_year.Contains("_"))
                {
                    cmd = new SqlCommand(constants.SPGetMontlyRecord, con);
                    cmd.Parameters.Add("ExistExtendComplete", SqlDbType.VarChar).Value = values[0];
                    cmd.Parameters.Add("Month", SqlDbType.Int).Value = month;
                    cmd.Parameters.Add("year", SqlDbType.Int).Value = year;
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                else
                {
                    cmd = new SqlCommand(constants.SpGetRecordById, con);
                    cmd.Parameters.Add("Id", SqlDbType.VarChar).Value = month_year;
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                try
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        ObjSummary.MonthlyRecordList.Add(new ResourceInfo
                        {
                            //ResourceName = Convert.ToString(dr[constants.ResourceName]),
                            //Technology = Convert.ToString(dr[constants.Technology]),
                            //ClientName = Convert.ToString(dr[constants.ClientName]),
                            //ProjectName = Convert.ToString(dr[constants.ProjectName]),
                            //EstimatedRateValue = Convert.ToString(dr[constants.EstimatedRateValue]),
                            //TentativeEndDate = Convert.ToDateTime(dr[constants.AssignmentEndDate]),
                            SOWNo = Convert.ToString(dr[constants.SOWNo]),
                            SignedStatus = Convert.ToString(dr[constants.SignedStatus]),
                            JoiningStatus = Convert.ToString(dr[constants.JoiningStatus]),
                            SOWDate = Convert.ToDateTime(dr[constants.SOWDate]),
                            ClientName = Convert.ToString(dr[constants.ClientName]),
                            ProjectName = Convert.ToString(dr[constants.ProjectName]),
                            ResourceName = Convert.ToString(dr[constants.ResourceName]),
                            WorkLocation = Convert.ToString(dr[constants.WorkLocation]),
                            BussinessUnit = Convert.ToString(dr[constants.BussinessUnit]),
                            Designation = Convert.ToString(dr[constants.Designation]),
                            ReportingTime = Convert.ToString(dr[constants.ReportingTime]),
                            RecruiterName = Convert.ToString(dr[constants.RecruiterName]),
                            HardwareRequirement = Convert.ToString(dr[constants.HardwareRequirement]),
                            SoftwareRequirement = Convert.ToString(dr[constants.SoftwareRequirement]),

                            Remarks = Convert.ToString(dr[constants.Remarks]),

                            AssignmentStartDate = Convert.ToDateTime(dr[constants.AssignmentStartDate]),
                            TentativeEndDate = Convert.ToDateTime(dr[constants.AssignmentEndDate]),
                            Technology = Convert.ToString(dr[constants.Technology]),
                            AttachFile1 = Convert.ToString(dr[constants.File1]),
                            EstimatedRateValue = Convert.ToString(dr[constants.EstimatedRateValue]),
                            FixedBidOrTM = Convert.ToString(dr[constants.FixedBidTM]),
                            PartnerName = Convert.ToString(dr[constants.PartnerName]),
                            ReportingManager = Convert.ToString(dr[constants.ReportingManager]),
                            GuestAccomodation = Convert.ToBoolean(dr[constants.GuestAccomodation]),
                            FinanceEmailAddress = Convert.ToString(dr[constants.FinanceEmailAddress]),
                            OnBoardEmailAddress = Convert.ToString(dr[constants.OnBoardEmailAddress])

                        });

                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + "  in GetMonthlyRecordList()");
                }
            }

            return ObjSummary.MonthlyRecordList;
        }

        public List<ListItem> GetTechnologyList()
        {
            EngagementSummary ObjSummary = new EngagementSummary();
            ObjSummary.DisplayTechnologyList = new List<ListItem>();

            using (SqlConnection con = new SqlConnection(str))
            {
                SqlCommand cmd = null;
                cmd = new SqlCommand(constants.SpGetPieChartData, con);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        ObjSummary.DisplayTechnologyList.Add(new ListItem
                            {
                                Text = Convert.ToString(dr["TechStream"]),
                                Value = Convert.ToString(dr["Record"])
                            });

                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + "  in GetListAllrecord()");
                }
            }


            return ObjSummary.DisplayTechnologyList;
        }


        public List<ListItem> GetVendorList()
        {
            EngagementSummary ObjSummary = new EngagementSummary();
            ObjSummary.VendorLIst = new List<ListItem>();

            using (SqlConnection con = new SqlConnection(str))
            {
                SqlCommand cmd = null;
                cmd = new SqlCommand(constants.SpVendorPiechartInfo, con);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        ObjSummary.VendorLIst.Add(new ListItem
                            {
                                Text = Convert.ToString(dr["PartnerName"]),
                                Value = Convert.ToString(dr["Record"])
                            });

                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + "  in GetListAllrecord()");
                }
            }


            return ObjSummary.VendorLIst;
        }

        public List<ExportClass> ExportMonthlyRecordList(string month_year)
        {
            EngagementSummary ObjSummary = new EngagementSummary();
            ObjSummary.ExportMonthlyDatalist = new List<ExportClass>();
            string[] values = new string[3];
            int month = 0;
            int year = 0;
            if (month_year.Contains("_"))
            {

                values = month_year.Split('_');
                month = DateTime.ParseExact(values[1], "MMMM", CultureInfo.CurrentCulture).Month;
                year = Convert.ToInt32(values[2]);
            }
            using (SqlConnection con = new SqlConnection(str))
            {
                SqlCommand cmd = null;
                
                    cmd = new SqlCommand(constants.SPGetMontlyRecord, con);
                    cmd.Parameters.Add("ExistExtendComplete", SqlDbType.VarChar).Value = values[0];
                    cmd.Parameters.Add("Month", SqlDbType.Int).Value = month;
                    cmd.Parameters.Add("year", SqlDbType.Int).Value = year;
                    cmd.CommandType = CommandType.StoredProcedure;
                
                try
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        ObjSummary.ExportMonthlyDatalist.Add(new ExportClass
                        {
                            
                            SOWNo = Convert.ToString(dr[constants.SOWNo]),
                            SignedStatus = Convert.ToString(dr[constants.SignedStatus]),
                            JoiningStatus = Convert.ToString(dr[constants.JoiningStatus]),
                            SOWDate = Convert.ToDateTime(dr[constants.SOWDate]),
                            ClientName = Convert.ToString(dr[constants.ClientName]),
                            ProjectName = Convert.ToString(dr[constants.ProjectName]),
                            ResourceName = Convert.ToString(dr[constants.ResourceName]),
                            WorkLocation = Convert.ToString(dr[constants.WorkLocation]),
                            BussinessUnit = Convert.ToString(dr[constants.BussinessUnit]),
                            Designation = Convert.ToString(dr[constants.Designation]),
                            ReportingTime = Convert.ToString(dr[constants.ReportingTime]),
                            RecruiterName = Convert.ToString(dr[constants.RecruiterName]),
                            HardwareRequirement = Convert.ToString(dr[constants.HardwareRequirement]),
                            SoftwareRequirement = Convert.ToString(dr[constants.SoftwareRequirement]),

                            Remarks = Convert.ToString(dr[constants.Remarks]),

                            AssignmentStartDate = Convert.ToDateTime(dr[constants.AssignmentStartDate]),
                            TentativeEndDate = Convert.ToDateTime(dr[constants.AssignmentEndDate]),
                            Technology = Convert.ToString(dr[constants.Technology]),
                            AttachFile1 = Convert.ToString(dr[constants.File1]),
                            EstimatedRateValue = Convert.ToString(dr[constants.EstimatedRateValue]),
                            FixedBidOrTM = Convert.ToString(dr[constants.FixedBidTM]),
                            PartnerName = Convert.ToString(dr[constants.PartnerName]),
                            ReportingManager = Convert.ToString(dr[constants.ReportingManager]),
                            GuestAccomodation = Convert.ToBoolean(dr[constants.GuestAccomodation]),
                            FinanceEmailAddress = Convert.ToString(dr[constants.FinanceEmailAddress]),
                            OnBoardEmailAddress = Convert.ToString(dr[constants.OnBoardEmailAddress])

                        });

                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + "  in GetMonthlyRecordList()");
                }
            }

            return ObjSummary.ExportMonthlyDatalist;
        }

        public List<ExportClass> ExportExistingEngagementList()
        {
            EngagementSummary ObjSummary = new EngagementSummary();
            ObjSummary.ExportExistingEngagementList = new List<ExportClass>();


            using (SqlConnection con = new SqlConnection(str))
            {
                SqlCommand cmd = null;
                cmd = new SqlCommand(constants.SPGetListOfExistingEngagements, con);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        ObjSummary.ExportExistingEngagementList.Add(new ExportClass
                        {
                            SOWNo = Convert.ToString(dr[constants.SOWNo]),
                            SignedStatus = Convert.ToString(dr[constants.SignedStatus]),
                            JoiningStatus = Convert.ToString(dr[constants.JoiningStatus]),
                            SOWDate = Convert.ToDateTime(dr[constants.SOWDate]),
                            ClientName = Convert.ToString(dr[constants.ClientName]),
                            ProjectName = Convert.ToString(dr[constants.ProjectName]),
                            ResourceName = Convert.ToString(dr[constants.ResourceName]),
                            WorkLocation = Convert.ToString(dr[constants.WorkLocation]),
                            BussinessUnit = Convert.ToString(dr[constants.BussinessUnit]),
                            Designation = Convert.ToString(dr[constants.Designation]),
                            ReportingTime = Convert.ToString(dr[constants.ReportingTime]),
                            RecruiterName = Convert.ToString(dr[constants.RecruiterName]),
                            HardwareRequirement = Convert.ToString(dr[constants.HardwareRequirement]),
                            SoftwareRequirement = Convert.ToString(dr[constants.SoftwareRequirement]),

                            Remarks = Convert.ToString(dr[constants.Remarks]),

                            AssignmentStartDate = Convert.ToDateTime(dr[constants.AssignmentStartDate]),
                            TentativeEndDate = Convert.ToDateTime(dr[constants.AssignmentEndDate]),
                            Technology = Convert.ToString(dr[constants.Technology]),
                            AttachFile1 = Convert.ToString(dr[constants.File1]),
                            EstimatedRateValue = Convert.ToString(dr[constants.EstimatedRateValue]),
                            FixedBidOrTM = Convert.ToString(dr[constants.FixedBidTM]),
                            PartnerName = Convert.ToString(dr[constants.PartnerName]),
                            ReportingManager = Convert.ToString(dr[constants.ReportingManager]),
                            GuestAccomodation = Convert.ToBoolean(dr[constants.GuestAccomodation]),
                            FinanceEmailAddress = Convert.ToString(dr[constants.FinanceEmailAddress]),
                            OnBoardEmailAddress = Convert.ToString(dr[constants.OnBoardEmailAddress])

                        });

                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + "  in ExportExistingEngagementList()");
                }
            }

            return ObjSummary.ExportExistingEngagementList;
        }

        //public List<ResourceInfo> GetRecordInDetail(string id)
        //{
        //    EngagementSummary ObjSummary = new EngagementSummary();
        //    ObjSummary.MonthlyRecordList = new List<ResourceInfo>();
        //    //string[] values = new string[3];
        //    //int month = 0;
        //    //int year = 0;
        //    //if (month_year.Contains("_"))
        //    //{

        //    //    values = month_year.Split('_');
        //    //    month = DateTime.ParseExact(values[1], "MMMM", CultureInfo.CurrentCulture).Month;
        //    //    year = Convert.ToInt32(values[2]);
        //    //}
        //    using (SqlConnection con = new SqlConnection(str))
        //    {
        //        SqlCommand cmd = null;
        //        //if (month_year.Contains("_"))
        //        //{
        //        //    cmd = new SqlCommand(constants.SPGetMontlyRecord, con);
        //        //    cmd.Parameters.Add("ExistExtendComplete", SqlDbType.VarChar).Value = values[0];
        //        //    cmd.Parameters.Add("Month", SqlDbType.Int).Value = month;
        //        //    cmd.Parameters.Add("year", SqlDbType.Int).Value = year;
        //        //    cmd.CommandType = CommandType.StoredProcedure;
        //        //}
        //        //else
        //        //{
        //            cmd = new SqlCommand(constants.SpGetRecordById, con);
        //            cmd.Parameters.Add("Id", SqlDbType.VarChar).Value = id;
        //            cmd.CommandType = CommandType.StoredProcedure;
        //        //}
        //        try
        //        {
        //            con.Open();
        //            SqlDataReader dr = cmd.ExecuteReader();

        //            while (dr.Read())
        //            {
        //                ObjSummary.MonthlyRecordList.Add(new ResourceInfo
        //                {
        //                    //ResourceName = Convert.ToString(dr[constants.ResourceName]),
        //                    //Technology = Convert.ToString(dr[constants.Technology]),
        //                    //ClientName = Convert.ToString(dr[constants.ClientName]),
        //                    //ProjectName = Convert.ToString(dr[constants.ProjectName]),
        //                    //EstimatedRateValue = Convert.ToString(dr[constants.EstimatedRateValue]),
        //                    //TentativeEndDate = Convert.ToDateTime(dr[constants.AssignmentEndDate]),
        //                    SOWNo = Convert.ToString(dr[constants.SOWNo]),
        //                    SignedStatus = Convert.ToString(dr[constants.SignedStatus]),
        //                    JoiningStatus = Convert.ToString(dr[constants.JoiningStatus]),
        //                    SOWDate = Convert.ToDateTime(dr[constants.SOWDate]),
        //                    ClientName = Convert.ToString(dr[constants.ClientName]),
        //                    ProjectName = Convert.ToString(dr[constants.ProjectName]),
        //                    ResourceName = Convert.ToString(dr[constants.ResourceName]),
        //                    WorkLocation = Convert.ToString(dr[constants.WorkLocation]),
        //                    BussinessUnit = Convert.ToString(dr[constants.BussinessUnit]),
        //                    Designation = Convert.ToString(dr[constants.Designation]),
        //                    ReportingTime = Convert.ToString(dr[constants.ReportingTime]),
        //                    RecruiterName = Convert.ToString(dr[constants.RecruiterName]),
        //                    HardwareRequirement = Convert.ToString(dr[constants.HardwareRequirement]),
        //                    SoftwareRequirement = Convert.ToString(dr[constants.SoftwareRequirement]),

        //                    Remarks = Convert.ToString(dr[constants.Remarks]),

        //                    AssignmentStartDate = Convert.ToDateTime(dr[constants.AssignmentStartDate]),
        //                    TentativeEndDate = Convert.ToDateTime(dr[constants.AssignmentEndDate]),
        //                    Technology = Convert.ToString(dr[constants.Technology]),
        //                    AttachFile1 = Convert.ToString(dr[constants.File1]),
        //                    EstimatedRateValue = Convert.ToString(dr[constants.EstimatedRateValue]),
        //                    FixedBidOrTM = Convert.ToString(dr[constants.FixedBidTM]),
        //                    PartnerName = Convert.ToString(dr[constants.PartnerName]),
        //                    ReportingManager = Convert.ToString(dr[constants.ReportingManager]),
        //                    GuestAccomodation = Convert.ToBoolean(dr[constants.GuestAccomodation]),
        //                    FinanceEmailAddress = Convert.ToString(dr[constants.FinanceEmailAddress]),
        //                    OnBoardEmailAddress = Convert.ToString(dr[constants.OnBoardEmailAddress])

        //                });

        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception(ex.Message + "  in GetMonthlyRecordList()");
        //        }
        //    }

        //    return ObjSummary.MonthlyRecordList;
        //}

    }
}
