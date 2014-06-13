using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DAL
    {
        string str = String.Empty;

        public DAL()
        {
            str = ConfigurationManager.ConnectionStrings[constants.connection].ConnectionString;
        }

        public int InsertUpdate(ResourceInfo model)
        {
            string year = Convert.ToString(DateTime.Now.Year);           
            string SOWNo = "FWW/"+year+"/SOW";
            int res = 0;
            model.Flag = "True";
            using (SqlConnection con = new SqlConnection(str))
            {
                SqlCommand cmd = new SqlCommand(constants.SpInsert, con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (model.UpdatewithExt == "false")
                {
                    cmd.Parameters.Add(constants.SOWNo, SqlDbType.VarChar).Value = SOWNo;
                }
                else
                {
                    cmd.Parameters.Add(constants.SOWNo, SqlDbType.VarChar).Value = model.SOWNo;
                }
                cmd.Parameters.Add(constants.TechStream, SqlDbType.VarChar).Value = model.TechStream.Trim();
                cmd.Parameters.Add(constants.FinanceEmailAddress, SqlDbType.VarChar).Value = model.FinanceEmailAddress.Trim();
                cmd.Parameters.Add(constants.OnBoardEmailAddress, SqlDbType.VarChar).Value = model.OnBoardEmailAddress.Trim();
                cmd.Parameters.Add(constants.SignedStatus, SqlDbType.VarChar).Value = model.SignedStatus.Trim();
                cmd.Parameters.Add(constants.JoiningStatus, SqlDbType.VarChar).Value = model.JoiningStatus.Trim();
                cmd.Parameters.Add(constants.SOWDate, SqlDbType.DateTime).Value = model.SOWDate;
                cmd.Parameters.Add(constants.PartnerName, SqlDbType.VarChar).Value = model.PartnerName.Trim();
                cmd.Parameters.Add(constants.ClientName, SqlDbType.VarChar).Value = model.ClientName.Trim();
                cmd.Parameters.Add(constants.ProjectName, SqlDbType.VarChar).Value = model.ProjectName.Trim();
                cmd.Parameters.Add(constants.ResourceName, SqlDbType.VarChar).Value = model.ResourceName.Trim();
                cmd.Parameters.Add(constants.Technology, SqlDbType.VarChar).Value = model.Technology.Trim();
                cmd.Parameters.Add(constants.FixedBidTM, SqlDbType.VarChar).Value = model.FixedBidOrTM.Trim();
                cmd.Parameters.Add(constants.ReportingManager, SqlDbType.VarChar).Value = model.ReportingManager.Trim();
                cmd.Parameters.Add(constants.AssignmentStartDate, SqlDbType.DateTime).Value = model.AssignmentStartDate;
                cmd.Parameters.Add(constants.AssignmentEndDate, SqlDbType.DateTime).Value = model.TentativeEndDate;
                cmd.Parameters.Add(constants.EstimatedRateValue, SqlDbType.VarChar).Value = model.EstimatedRateValue.Trim();
                cmd.Parameters.Add(constants.WorkLocation, SqlDbType.VarChar).Value = model.WorkLocation.Trim();
                cmd.Parameters.Add(constants.BussinessUnit, SqlDbType.VarChar).Value = model.BussinessUnit.Trim();
                cmd.Parameters.Add(constants.Designation, SqlDbType.VarChar).Value = model.Designation.Trim();
                cmd.Parameters.Add(constants.RecruiterName, SqlDbType.VarChar).Value = model.RecruiterName.Trim();
                cmd.Parameters.Add(constants.Flag, SqlDbType.VarChar).Value = model.Flag;
                cmd.Parameters.Add(constants.ReportingTime, SqlDbType.VarChar).Value = model.ReportingTime.Trim();
                cmd.Parameters.Add(constants.HardwareRequirement, SqlDbType.VarChar).Value = model.HardwareRequirement.Trim();
                cmd.Parameters.Add(constants.SoftwareRequirement, SqlDbType.VarChar).Value = model.SoftwareRequirement.Trim();
                cmd.Parameters.Add(constants.GuestAccomodation, SqlDbType.VarChar).Value = model.GuestAccomodation;
                cmd.Parameters.Add(constants.Remarks, SqlDbType.VarChar).Value = model.Remarks;
                cmd.Parameters.Add(constants.Update, SqlDbType.VarChar).Value = model.UpdatewithExt;
                cmd.Parameters.Add(constants.ActualSOW, SqlDbType.Int).Value = 0;
                cmd.Parameters.Add(constants.File1, SqlDbType.VarChar).Value = model.AttachFile1.Trim();

                try
                {
                    con.Open();
                    res = Convert.ToInt32(cmd.ExecuteNonQuery());
                    con.Close();

                    string MailBody = "";


                    if (!string.IsNullOrEmpty(model.FinanceEmailAddress))
                    {
                        MailBody = GetMailContent(model, constants.FormatMailTextFileAccount);
                        SendMail(model.FinanceEmailAddress, MailBody, model,"Finance");
                    }
                    if (!string.IsNullOrEmpty(model.OnBoardEmailAddress))
                    {
                        MailBody = GetMailContent(model, constants.FormatMailTextFileOnBoard);
                        SendMail(model.OnBoardEmailAddress, MailBody, model,"OnBoard");
                    }


                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message );
                }
                return res;

            }
        }

        public int Update(ResourceInfo model)
        {

            int res = 0;

            using (SqlConnection con = new SqlConnection(str))
            {
                SqlCommand cmd = new SqlCommand(constants.SpUpdate, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(constants.SOWNo, SqlDbType.VarChar).Value = model.SOWNo.Trim();
                cmd.Parameters.Add(constants.TechStream, SqlDbType.VarChar).Value = model.TechStream.Trim();
                cmd.Parameters.Add(constants.FinanceEmailAddress, SqlDbType.VarChar).Value = model.FinanceEmailAddress.Trim();
                cmd.Parameters.Add(constants.OnBoardEmailAddress, SqlDbType.VarChar).Value = model.OnBoardEmailAddress.Trim();
                cmd.Parameters.Add(constants.SignedStatus, SqlDbType.VarChar).Value = model.SignedStatus.Trim();
                cmd.Parameters.Add(constants.JoiningStatus, SqlDbType.VarChar).Value = model.JoiningStatus.Trim();
                cmd.Parameters.Add(constants.SOWDate, SqlDbType.DateTime).Value = model.SOWDate;
                cmd.Parameters.Add(constants.PartnerName, SqlDbType.VarChar).Value = model.PartnerName.Trim();
                cmd.Parameters.Add(constants.ClientName, SqlDbType.VarChar).Value = model.ClientName.Trim();
                cmd.Parameters.Add(constants.ProjectName, SqlDbType.VarChar).Value = model.ProjectName.Trim();
                cmd.Parameters.Add(constants.ResourceName, SqlDbType.VarChar).Value = model.ResourceName.Trim();
                cmd.Parameters.Add(constants.Technology, SqlDbType.VarChar).Value = model.Technology.Trim();
                cmd.Parameters.Add(constants.FixedBidTM, SqlDbType.VarChar).Value = model.FixedBidOrTM.Trim();
                cmd.Parameters.Add(constants.ReportingManager, SqlDbType.VarChar).Value = model.ReportingManager.Trim();
                cmd.Parameters.Add(constants.AssignmentStartDate, SqlDbType.DateTime).Value = model.AssignmentStartDate;
                cmd.Parameters.Add(constants.AssignmentEndDate, SqlDbType.DateTime).Value = model.TentativeEndDate;
                cmd.Parameters.Add(constants.EstimatedRateValue, SqlDbType.VarChar).Value = model.EstimatedRateValue.Trim();
                cmd.Parameters.Add(constants.WorkLocation, SqlDbType.VarChar).Value = model.WorkLocation.Trim();
                cmd.Parameters.Add(constants.BussinessUnit, SqlDbType.VarChar).Value = model.BussinessUnit.Trim();
                cmd.Parameters.Add(constants.Designation, SqlDbType.VarChar).Value = model.Designation.Trim();
                cmd.Parameters.Add(constants.RecruiterName, SqlDbType.VarChar).Value = model.RecruiterName.Trim();
                cmd.Parameters.Add(constants.ReportingTime, SqlDbType.VarChar).Value = model.ReportingTime.Trim();
                cmd.Parameters.Add(constants.HardwareRequirement, SqlDbType.VarChar).Value = model.HardwareRequirement.Trim();
                cmd.Parameters.Add(constants.SoftwareRequirement, SqlDbType.VarChar).Value = model.SoftwareRequirement.Trim();
                cmd.Parameters.Add(constants.GuestAccomodation, SqlDbType.VarChar).Value = model.GuestAccomodation;
                cmd.Parameters.Add(constants.Remarks, SqlDbType.VarChar).Value = model.Remarks.Trim();
                //if (!string.IsNullOrEmpty(model.AttachFile1))
                //{
                //    model.AttachFile1 = "," + model.AttachFile1;
                //}
                cmd.Parameters.Add(constants.File1, SqlDbType.VarChar).Value = model.AttachFile1.Trim();

                try
                {
                    con.Open();
                    res = Convert.ToInt32(cmd.ExecuteNonQuery());
                    con.Close();

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + "   in Update()");
                }


            }
            return res;

        }

        private void SendMail(string MailTo, String MailBody, ResourceInfo model,string team)
        {


            using (MailMessage msg = new MailMessage())
            {
                string subject = string.Empty;
                string strFrom = ConfigurationManager.AppSettings[constants.sender];
                msg.From = new MailAddress(strFrom);
                //Console.WriteLine(strFrom);
                if (team == "Finance")
                {
                    msg.To.Add(ConfigurationManager.AppSettings[constants.ReceiverAccounts]);
                    subject = ConfigurationManager.AppSettings[constants.SubjectAccount];
                }
                else
                {
                    msg.To.Add(ConfigurationManager.AppSettings[constants.Receiveronbording]);
                    subject = ConfigurationManager.AppSettings[constants.SubjectOnBoarding];
                }
                msg.CC.Add(MailTo);
                
                subject =subject.Replace(constants.subCandidateName, model.ResourceName).Replace(constants.subClientName, model.ClientName).Replace(constants.subProjectName, model.ProjectName).Replace(constants.SubTechnology, model.Technology);
                msg.Subject = subject;
                msg.Body = MailBody;
                msg.IsBodyHtml = true;
                string mailserver = ConfigurationManager.AppSettings[constants.Host];
                int port = Convert.ToInt32(ConfigurationManager.AppSettings[constants.port]);
                using (SmtpClient smtpclient = new SmtpClient(mailserver, port))
                {
                    smtpclient.UseDefaultCredentials = false;
                    smtpclient.EnableSsl = false;
                    smtpclient.Credentials = new System.Net.NetworkCredential(strFrom, ConfigurationManager.AppSettings[constants.password]);
                    smtpclient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    try
                    {
                        smtpclient.Send(msg);
                    }
                    catch(Exception ex)
                    {
                        throw new Exception(ex.Message + "   in SendMail()");
                    }
                }

            }

        }
        private string GetMailContent(ResourceInfo model, string FileName)
        {
            string content = "";

            if (FileName == "TableFormatOnBoard")
            {
                using (StreamReader Reader = new StreamReader(ConfigurationManager.AppSettings[FileName]))
                {
                    //model.GuestAccomodation.ToString().ToLower() == "false"
                    //    model.GuestAccomodation = "No";
                    //else

                    content = Reader.ReadToEnd();

                    content = content.Replace(constants.TCandidateName, model.ResourceName).Replace(constants.TResourceName, model.ResourceName).Replace(constants.TdateOfJoining, Convert.ToString(model.AssignmentStartDate.ToShortDateString())).Replace(constants.TBussinessUnit, model.BussinessUnit).Replace(constants.TDesignation, model.Designation).Replace(constants.TSkill, model.Technology).Replace(constants.TReportingTime, model.ReportingTime).Replace(constants.TRecruiterName, model.RecruiterName).Replace(constants.TWorkLocation, model.WorkLocation).Replace(constants.TReportingManager, model.ReportingManager).Replace(constants.TGuestHouseAcc, model.IsGuestAccomodated).Replace(constants.TRemarks, model.Remarks).Replace(constants.THardware, model.HardwareRequirement).Replace(constants.TSoftwareRequirement, model.SoftwareRequirement);




                }
            }
            if (FileName == "TableFormatAccount")
            {
                using (StreamReader Reader = new StreamReader(ConfigurationManager.AppSettings[FileName]))
                {


                    content = Reader.ReadToEnd();

                    content = content.Replace(constants.TCandidateName, model.ResourceName).Replace(constants.TResourceName, model.ResourceName).Replace(constants.Tcontractrate, model.EstimatedRateValue).Replace(constants.TPartenerName, model.PartnerName).Replace(constants.TProjectName, model.ProjectName).Replace(constants.TClientName, model.ClientName).Replace(constants.TTechnology, model.Technology).Replace(constants.TFixedBid, model.FixedBidOrTM).Replace(constants.TWorkLocation, model.WorkLocation).Replace(constants.TReportingManager, model.ReportingManager).Replace(constants.TAssignStartDate, Convert.ToString(model.AssignmentStartDate.ToShortDateString())).Replace(constants.TAssignEndDate, Convert.ToString(model.TentativeEndDate.ToShortDateString()));




                }
            }




            return content;
        }



        

        public List<ResourceInfo> GetListAllrecord(SearchModel model)
        {

            List<ResourceInfo> EmployeeList = new List<ResourceInfo>();
            using (SqlConnection con = new SqlConnection(str))
            {
                SqlCommand cmd = null;
                cmd = new SqlCommand(constants.SpSearchByResourceName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(constants.ResourceName, SqlDbType.VarChar).Value = model.ResourceName;
                cmd.Parameters.Add(constants.PartnerName, SqlDbType.VarChar).Value = model.PartnerName;
                cmd.Parameters.Add(constants.ProjectName, SqlDbType.VarChar).Value = model.ProjectName;
                cmd.Parameters.Add(constants.FromDate, SqlDbType.DateTime).Value = model.FromDate;
                cmd.Parameters.Add(constants.Todate, SqlDbType.DateTime).Value = model.ToDate;
                cmd.Parameters.Add(constants.SearchBy, SqlDbType.VarChar).Value = model.searchBy;
                try
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        EmployeeList.Add(new ResourceInfo
                        {
                            id = Convert.ToInt32(dr[constants.id]),
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
                            TechStream =Convert.ToString(dr[constants.TechStream]),
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
                            OnBoardEmailAddress = Convert.ToString(dr[constants.OnBoardEmailAddress]),


                        });
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + "  in GetListAllrecord()");
                }
            }
            return EmployeeList;
        }

        public int checkSowNo(string id)
        {
            int res = 0;
            using (SqlConnection con = new SqlConnection(str))
            {
                SqlCommand cmd = new SqlCommand(constants.SpCheckSowNoForCreateExt, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(constants.id, SqlDbType.Int).Value = Convert.ToInt32(id);
                try
                {
                    con.Open();
                    res = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + "  in checkSowNo()");
                }

            }
            return res;
        }


        public int Auntheticate(AuthenticationClass model)
        {
            int userId;
            try
            {
                using (SqlConnection con = new SqlConnection(str))
                {
                    SqlCommand cmd = null;
                    cmd = new SqlCommand(constants.SpAuthenticate, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(constants.UserName, SqlDbType.VarChar).Value = model.UserName;
                    cmd.Parameters.Add(constants.Password, SqlDbType.VarChar).Value = model.Password;
                    con.Open();
                    userId = Convert.ToInt32(cmd.ExecuteScalar());
                }
                return userId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "  in Auntheticate()");
            }
        }

        public ResourceInfo GetDetails(string Id)
        {
            ResourceInfo model = new ResourceInfo();
            using (SqlConnection con = new SqlConnection(str))
            {
                SqlCommand cmd = null;
                cmd = new SqlCommand(constants.SelectById, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(constants.id, SqlDbType.Int).Value = Convert.ToInt32(Id);
                try
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        model.TechStream = Convert.ToString(dr[constants.TechStream]);
                        model.SOWNo = Convert.ToString(dr[constants.SOWNo]);
                        model.SignedStatus = Convert.ToString(dr[constants.SignedStatus]);
                        model.JoiningStatus = Convert.ToString(dr[constants.JoiningStatus]);
                        model.SOWDate = Convert.ToDateTime(dr[constants.SOWDate]);
                        model.ClientName = Convert.ToString(dr[constants.ClientName]);
                        model.ProjectName = Convert.ToString(dr[constants.ProjectName]);
                        model.ResourceName = Convert.ToString(dr[constants.ResourceName]);
                        model.WorkLocation = Convert.ToString(dr[constants.WorkLocation]);
                        model.BussinessUnit = Convert.ToString(dr[constants.BussinessUnit]);
                        model.Designation = Convert.ToString(dr[constants.Designation]);
                        model.ReportingTime = Convert.ToString(dr[constants.ReportingTime]);
                        model.RecruiterName = Convert.ToString(dr[constants.RecruiterName]);
                        model.HardwareRequirement = Convert.ToString(dr[constants.HardwareRequirement]);
                        model.SoftwareRequirement = Convert.ToString(dr[constants.SoftwareRequirement]);

                        model.Remarks = Convert.ToString(dr[constants.Remarks]);
                        model.AssignmentStartDate = Convert.ToDateTime(dr[constants.AssignmentStartDate]);
                        model.TentativeEndDate = Convert.ToDateTime(dr[constants.AssignmentEndDate]);
                        model.Technology = Convert.ToString(dr[constants.Technology]);

                        model.EstimatedRateValue = Convert.ToString(dr[constants.EstimatedRateValue]);

                        model.FixedBidOrTM = Convert.ToString(dr[constants.FixedBidTM]);
                        model.PartnerName = Convert.ToString(dr[constants.PartnerName]);
                        model.ReportingManager = Convert.ToString(dr[constants.ReportingManager]);
                        model.FinanceEmailAddress = Convert.ToString(dr[constants.FinanceEmailAddress]);
                        model.OnBoardEmailAddress = Convert.ToString(dr[constants.OnBoardEmailAddress]);
                        model.GuestAccomodation = Convert.ToBoolean(dr[constants.GuestAccomodation]);
                        model.AttachFile1 = Convert.ToString(dr[constants.File1]);


                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + "  in GetDetails()");
                }

            }

            return model;
        }

        //public List<ExportClass> GetList(string resourceName, string partnerName, string projectName)
        public List<ExportClass> GetList(SearchModel model)
        {

            List<ExportClass> EmployeeList = new List<ExportClass>();
            using (SqlConnection con = new SqlConnection(str))
            {
                SqlCommand cmd = null;
                cmd = new SqlCommand(constants.SpSearchByResourceName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(constants.ResourceName, SqlDbType.VarChar).Value = model.ResourceName;
                cmd.Parameters.Add(constants.PartnerName, SqlDbType.VarChar).Value = model.PartnerName;
                cmd.Parameters.Add(constants.ProjectName, SqlDbType.VarChar).Value = model.ProjectName;
                cmd.Parameters.Add(constants.FromDate, SqlDbType.DateTime).Value = model.FromDate;
                cmd.Parameters.Add(constants.Todate, SqlDbType.DateTime).Value = model.ToDate;
                cmd.Parameters.Add(constants.SearchBy, SqlDbType.VarChar).Value = model.searchBy;

                try
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        EmployeeList.Add(new ExportClass
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
                            OnBoardEmailAddress = Convert.ToString(dr[constants.OnBoardEmailAddress]),

                        });
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + "  in GetList()");
                }

            }

            return EmployeeList;
        }
    }
}
