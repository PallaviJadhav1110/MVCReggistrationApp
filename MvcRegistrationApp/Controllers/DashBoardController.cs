using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using System.Web.Helpers;

using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
//using System.Web.UI.DataVisualization.Charting.Chart;
//using System.IO;

namespace MvcRegistrationApp.Controllers
{
    public class DashBoardController : Controller
    {
        //
        // GET: /DashBoard/
        public int UserId
        {

            get
            {
                return (Session["UserId"] != null ? Convert.ToInt32(Session["UserId"]) : 0);
            }

        }

        [HttpGet]
        public ActionResult DisplayDashBoard()
        {
            if (UserId > 0)
            {
                EngagementSummary summary = new EngagementSummary();

                DashBoard db = new DashBoard();
                summary = db.GetEngagementSummary();
                summary.ExistingEngagementList = db.GetExistingEngagementList();
                summary.ListEngagementsOverInCurrentWeek = db.GetEngagementsOverInCurrentWeekList();
                //summary.MonthlyRecordList = db.GetMonthlyRecordList(id);
                summary.DisplayTechnologyList = db.GetTechnologyList();
                summary.VendorLIst = db.GetVendorList();

                return View("DisplayDashBoard", summary);
            }
            else
            {
                return RedirectToAction("Authentication", "Registration");
            }
        }

        public ActionResult GetMonthlyRecord(string id)
        {
            if (UserId > 0)
            {
                EngagementSummary summary = new EngagementSummary();

                DashBoard db = new DashBoard();
                summary.MonthlyRecordList = db.GetMonthlyRecordList(id);

                return PartialView("MonthlyRecordDialog", summary.MonthlyRecordList);
            }
            else
            {
                return RedirectToAction("Authentication", "Registration");
            }
        }

        public ActionResult GetRecordDetails(string id)
        {
            if (UserId > 0)
            {
                EngagementSummary summary = new EngagementSummary();

                DashBoard db = new DashBoard();
                summary.MonthlyRecordList = db.GetMonthlyRecordList(id);
                return PartialView("DisplayMonthlyRecord", summary.MonthlyRecordList);
            }
            else
            {
                return RedirectToAction("Authentication", "Registration");
            }
        }

        public ActionResult ExportDataExistingEngagements()
        {
            if (UserId > 0)
            {
                EngagementSummary summary = new EngagementSummary();
                DashBoard obj = new DashBoard();
                summary.ExportExistingEngagementList = obj.ExportExistingEngagementList();


                GridView gv = new GridView();
                try
                {
                    gv.DataSource = summary.ExportExistingEngagementList;
                    gv.DataBind();
                    Response.ClearContent();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment; filename=ExixtingEngagement.xls");
                    Response.ContentType = "application/ms-excel";
                    Response.Charset = "";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    gv.RenderControl(htw);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
                catch (Exception ex)
                {

                }

                return RedirectToAction("DisplayDashBoard");
            }
            else
            {
                return RedirectToAction("Authentication", "Registration");
            }
        }
        public ActionResult ExportMonthlyData()
        {
            if (UserId > 0)
            {
                string month_year = Request.Form["Month_Year"].ToString();
                EngagementSummary summary = new EngagementSummary();
                DashBoard obj = new DashBoard();
                summary.ExportMonthlyDatalist = obj.ExportMonthlyRecordList(month_year);


                GridView gv = new GridView();
                try
                {
                    gv.DataSource = summary.ExportMonthlyDatalist;
                    gv.DataBind();
                    Response.ClearContent();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment; filename='" + month_year + "'.xls");
                    Response.ContentType = "application/ms-excel";
                    Response.Charset = "";
                    StringWriter sw = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    gv.RenderControl(htw);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
                catch (Exception ex)
                {

                }

                return RedirectToAction("DisplayDashBoard");
            }
            else
            {
                return RedirectToAction("Authentication", "Registration");
            }
        }

        public ActionResult GetExistingEngagement()
        {
            if (UserId > 0)
            {
                EngagementSummary summary = new EngagementSummary();

                DashBoard db = new DashBoard();
                summary.ExistingEngagementList = db.GetExistingEngagementList();
                return PartialView("ExistingEngagementsDialog", summary.ExistingEngagementList);
            }
            else
            {
                return RedirectToAction("Authentication", "Registration");
            }
        }


    }
}
