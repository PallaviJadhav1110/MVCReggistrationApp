using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SOWTrackerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AddResource" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AddResource.svc or AddResource.svc.cs at the Solution Explorer and start debugging.
    public class AddResource : IAddResource
    {
        string str = ConfigurationManager.ConnectionStrings[constants.connection].ConnectionString;

        //public void AddResourceDetails(ResourceInfo model)
        //{
        //    //calculate ApproxRateperDay
        //    if (model.EstimateRate == "Per Day")
        //    {
        //        model.ApproxRateperDay = Math.Round((model.EstimatedRateValue / 168) * 8);
        //    }
        //    else if (model.EstimateRate == "Per Man Hr")
        //    {
        //        model.ApproxRateperDay = Math.Round(model.EstimatedRateValue * 8);
        //    }

        //    int res = 0;
        //    //calculate duration by formula=totalworkingdays(not weekoff days)+1+difference between weakdays of both date
        //    TimeSpan span = model.TentativeEndDate - model.AssignmentStartDate;
        //    int totalWorkingDays = (Convert.ToInt32(span.TotalDays) / 7) * 5;
        //    int weekdayDiff = Math.Max(Convert.ToInt32(model.TentativeEndDate.DayOfWeek - model.AssignmentStartDate.DayOfWeek), 0);
        //    model.Duration = weekdayDiff + 1 + totalWorkingDays;

        //    //calculate ApproxPartnerCost
        //    model.ApproxPartnerCost = Math.Round(model.Duration * model.ApproxRateperDay);


        //    using (SqlConnection con = new SqlConnection(str))
        //    {
        //        SqlCommand cmd = new SqlCommand(constants.SpInsert, con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Add(constants.SOWNo, SqlDbType.VarChar).Value = model.SOWNo;
        //        cmd.Parameters.Add(constants.SignedStatus, SqlDbType.VarChar).Value = model.SignedStatus;
        //        cmd.Parameters.Add(constants.JoiningStatus, SqlDbType.VarChar).Value = model.JoiningStatus;
        //        cmd.Parameters.Add(constants.SOWDate, SqlDbType.VarChar).Value = model.SOWDate;
        //        cmd.Parameters.Add(constants.PartnerName, SqlDbType.VarChar).Value = model.PartnerName;
        //        cmd.Parameters.Add(constants.ClientName, SqlDbType.VarChar).Value = model.ClientName;
        //        cmd.Parameters.Add(constants.ProjectName, SqlDbType.VarChar).Value = model.ProjectName;
        //        cmd.Parameters.Add(constants.ResourceName, SqlDbType.VarChar).Value = model.ResourceName;
        //        cmd.Parameters.Add(constants.Technology, SqlDbType.VarChar).Value = model.Technology;
        //        cmd.Parameters.Add(constants.FixedBidTM, SqlDbType.VarChar).Value = model.FixedBidOrTM;
        //        cmd.Parameters.Add(constants.ReportingManager, SqlDbType.VarChar).Value = model.ReportingManager;
        //        cmd.Parameters.Add(constants.AssignmentStartDate, SqlDbType.DateTime).Value = model.AssignmentStartDate;
        //        cmd.Parameters.Add(constants.AssignmentEndDate, SqlDbType.DateTime).Value = model.TentativeEndDate;
        //        cmd.Parameters.Add(constants.EstimatedRateValue, SqlDbType.Float).Value = model.EstimatedRateValue;
        //        cmd.Parameters.Add(constants.Duration, SqlDbType.Int).Value = model.Duration;
        //        cmd.Parameters.Add(constants.EstimateRate, SqlDbType.VarChar).Value = model.EstimateRate;
        //        cmd.Parameters.Add(constants.ApproxRateperDay, SqlDbType.Float).Value = model.ApproxRateperDay;
        //        cmd.Parameters.Add(constants.ApproxPartnerCost, SqlDbType.Float).Value = model.ApproxPartnerCost;
        //        try
        //        {
        //            con.Open();
        //            res = Convert.ToInt32(cmd.ExecuteNonQuery());
        //        }
        //        catch (FaultException ex)
        //        {
        //            throw new FaultException(ex.Message);
        //        }
        //    }
        //}

        //public int AddResourceDetails(ResourceInfo model)
        //{
           
        //}



        public int AddResourceDetails(ResourceInfo model)
        {
            //calculate ApproxRateperDay
            if (model.EstimateRate == "Per Day")
            {
                model.ApproxRateperDay = Math.Round((model.EstimatedRateValue / 168) * 8);
            }
            else if (model.EstimateRate == "Per Man Hr")
            {
                model.ApproxRateperDay = Math.Round(model.EstimatedRateValue * 8);
            }

            int res = 0;
            //calculate duration by formula=totalworkingdays(not weekoff days)+1+difference between weakdays of both date
            TimeSpan span = model.TentativeEndDate - model.AssignmentStartDate;
            int totalWorkingDays = (Convert.ToInt32(span.TotalDays) / 7) * 5;
            int weekdayDiff = Math.Max(Convert.ToInt32(model.TentativeEndDate.DayOfWeek - model.AssignmentStartDate.DayOfWeek), 0);
            model.Duration = weekdayDiff + 1 + totalWorkingDays;

            //calculate ApproxPartnerCost
            model.ApproxPartnerCost = Math.Round(model.Duration * model.ApproxRateperDay);


            using (SqlConnection con = new SqlConnection(str))
            {
                SqlCommand cmd = new SqlCommand(constants.SpInsert, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(constants.SOWNo, SqlDbType.VarChar).Value = model.SOWNo;
                cmd.Parameters.Add(constants.SignedStatus, SqlDbType.VarChar).Value = model.SignedStatus;
                cmd.Parameters.Add(constants.JoiningStatus, SqlDbType.VarChar).Value = model.JoiningStatus;
                cmd.Parameters.Add(constants.SOWDate, SqlDbType.VarChar).Value = model.SOWDate;
                cmd.Parameters.Add(constants.PartnerName, SqlDbType.VarChar).Value = model.PartnerName;
                cmd.Parameters.Add(constants.ClientName, SqlDbType.VarChar).Value = model.ClientName;
                cmd.Parameters.Add(constants.ProjectName, SqlDbType.VarChar).Value = model.ProjectName;
                cmd.Parameters.Add(constants.ResourceName, SqlDbType.VarChar).Value = model.ResourceName;
                cmd.Parameters.Add(constants.Technology, SqlDbType.VarChar).Value = model.Technology;
                cmd.Parameters.Add(constants.FixedBidTM, SqlDbType.VarChar).Value = model.FixedBidOrTM;
                cmd.Parameters.Add(constants.ReportingManager, SqlDbType.VarChar).Value = model.ReportingManager;
                cmd.Parameters.Add(constants.AssignmentStartDate, SqlDbType.DateTime).Value = model.AssignmentStartDate;
                cmd.Parameters.Add(constants.AssignmentEndDate, SqlDbType.DateTime).Value = model.TentativeEndDate;
                cmd.Parameters.Add(constants.EstimatedRateValue, SqlDbType.Float).Value = model.EstimatedRateValue;
                cmd.Parameters.Add(constants.Duration, SqlDbType.Int).Value = model.Duration;
                cmd.Parameters.Add(constants.EstimateRate, SqlDbType.VarChar).Value = model.EstimateRate;
                cmd.Parameters.Add(constants.ApproxRateperDay, SqlDbType.Float).Value = model.ApproxRateperDay;
                cmd.Parameters.Add(constants.ApproxPartnerCost, SqlDbType.Float).Value = model.ApproxPartnerCost;
                try
                {
                    con.Open();
                    res = Convert.ToInt32(cmd.ExecuteNonQuery());
                }
                catch (FaultException ex)
                {
                    throw new FaultException(ex.Message);
                }
                return res;
            }
        }
    }
}
