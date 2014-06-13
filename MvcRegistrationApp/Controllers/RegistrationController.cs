using DataLayer;
using MvcRegistrationApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace MvcRegistrationApp.Controllers
{

    public class RegistrationController : Controller
    {
        //
        // GET: /Registration/
        string str = ConfigurationManager.ConnectionStrings[DataLayer.constants.connection].ConnectionString;

        public int UserId
        {

            get
            {
                return (Session["UserId"] != null ? Convert.ToInt32(Session["UserId"]) : 0);
            }

        }

        [HttpGet]
        public ActionResult Authentication()
        {
            if (UserId > 0)
                return RedirectToAction("UserDetails");
            TempData["message"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult Authentication(AuthenticationClass model)
        {
            //if ((model.UserName == "james") && (model.Password == "fulcrum#1"))
            //{
            //    Session["UserId"] = Convert.ToString(1);
            //    return RedirectToAction("UserDetails");
            //}
            //else
            //{
            //    return View();
            //}

            DAL datalogic = new DAL();
            int UserId = 0;
            try
            {
                UserId = datalogic.Auntheticate(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            if (UserId != 0)
            {
                Session["UserId"] = Convert.ToString(UserId);
                return RedirectToAction("DisplayDashBoard", "DashBoard");
            }
            else
            {
                TempData["message"] = "Invalid User Name or Password";
                //ModelState.AddModelError(string.Empty, );
                return View(model);
            }
        }



        [HttpGet]
        public ActionResult UserDetails(string id, string update)
        {
            DAL datalogic = new DAL();




            if (update == "false")
            {
                //int res = 0;
                //int res = datalogic.checkSowNo(id);
                SearchModel smodel = new SearchModel();
                smodel.EmployeeList = GetListAllrecord(smodel);
                var ab = smodel.EmployeeList.Find(x => x.id == Convert.ToInt32(id));
               
                if (ab.SOWNo.Contains("Extension"))
                {
             
                var b = smodel.EmployeeList.FindAll(x => x.SOWNo.Contains(ab.SOWNo.Substring(0,ab.SOWNo.Length-1)+(Convert.ToInt32(ab.SOWNo.Substring(ab.SOWNo.Length-1))+1)));
                if (b.Count >0)
                {
                    TempData["message"] = "Select Latest Extension";
                    return RedirectToAction("SearchView");
                }
                }
                else if (!ab.SOWNo.Contains("Extension"))
                {
                    var b = smodel.EmployeeList.FindAll(x => x.SOWNo.Contains(ab.SOWNo));
                    if (b.Count > 1)
                    {
                        TempData["message"] = "Select Latest Extension";
                        return RedirectToAction("SearchView");
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(update))
                        update = "false";
                    if (UserId > 0)
                    {
                        if (!string.IsNullOrEmpty(id))
                        {
                            ResourceInfo model = new ResourceInfo();
                            model = datalogic.GetDetails(id);
                            model.update = update;
                           
                            return View(model);
                        }
                        else
                        {
                            ResourceInfo model = new ResourceInfo();
                            model.update = update;
                            return View(model);
                        }
                    }
                    else
                        return RedirectToAction("Authentication");
                }
            }

            if (string.IsNullOrEmpty(update))
                update = "false";
            if (UserId > 0)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    ResourceInfo model = new ResourceInfo();
                    model = datalogic.GetDetails(id);
                    model.update = update;
                    return View(model);
                }
                else
                {
                    ResourceInfo model = new ResourceInfo();
                    model.update = update;
                    return View(model);
                }
            }
            else
                return RedirectToAction("Authentication");


        }

        [HttpPost]
        public string CheckFileExists(string file)
        {
            string message = string.Empty;
            string[] arr = file.Split(',');
            string Filename = string.Empty;
            for (int i = 0; i < arr.Length; i++)
            {
                Filename = arr[i];
                string path = ConfigurationManager.AppSettings["path"].ToString() + Filename;
                if (System.IO.File.Exists(path))
                {
                    message = "file already esixts.Incase you want to proceed please click on save";

                }

            }
            return message;

        }


        [HttpPost]
        public ActionResult saveData(ResourceInfo model, HttpPostedFileBase[] files)
        {
            //model.GuestAccomodation = Convert.ToBoolean("true");
            if (string.IsNullOrEmpty(model.AttachFile1))
            {
                model.AttachFile1 = string.Empty;
            }
            string accomodation = Request.Form["Accomodation"];
            if (accomodation == "yes")
            {
                model.GuestAccomodation = Convert.ToBoolean("true");
            }
            else
            {
                model.Remarks = string.Empty;

                model.GuestAccomodation = Convert.ToBoolean("false");
            }
            var AllFiles = string.Empty;
            try
            {
                foreach (HttpPostedFileBase file in files)
                {

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["DocumentPath"].ToString()), fileName);
                        file.SaveAs(path);
                        AllFiles = fileName + "," + AllFiles;
                        
                    }
                }
                if (!string.IsNullOrEmpty(model.AttachFile1) || !string.IsNullOrEmpty(AllFiles))
                {
                    if (!string.IsNullOrEmpty(AllFiles))
                    {
                        model.AttachFile1 = model.AttachFile1 +","+AllFiles;
                    }
                    if (model.AttachFile1.Contains("undefined"))
                    {
                       model.AttachFile1= model.AttachFile1.Replace(",undefined", "");
                    }
                }

                
                if (string.IsNullOrEmpty(model.AttachFile1))
                {
                    model.AttachFile1 = string.Empty;
                }
            }
            catch (Exception ex)
            {
               TempData["message"]= ex.Message;
               return RedirectToAction("UserDetails");
            }
            
            if (string.IsNullOrEmpty(model.OnBoardEmailAddress))
                model.OnBoardEmailAddress = string.Empty;
            if (string.IsNullOrEmpty(model.FinanceEmailAddress))
                model.FinanceEmailAddress = string.Empty;
            int res = 0;
            DAL datalogic = new DAL();
           

                if (UserId > 0)
                {
                    if (model.update == "false")
                    {
                        //insert and insert with extension
                        model.UpdatewithExt = "false";
                        if (!string.IsNullOrEmpty(model.SOWNo))
                        {
                            model.UpdatewithExt = "true";
                        }


                        try
                        {
                            res = datalogic.InsertUpdate(model);
                        }
                        catch (Exception ex)
                        {
                            TempData["message"] = ex.Message;
                            return RedirectToAction("UserDetails");
                        }
                    }
                    else
                    {
                        //update
                        try
                        {
                            res = datalogic.Update(model);
                        }
                        catch (Exception ex)
                        {
                            TempData["message"] = ex.Message;
                            return RedirectToAction("UserDetails");
                        }


                    }
                    ResourceInfo resmodel = new ResourceInfo();

                    if (res >= 1)
                    {
                        TempData["SuccessMessage"] = DataLayer.constants.Message;

                    }

                    return RedirectToAction("UserDetails");
                }
                else
                    return RedirectToAction("Authentication");
            

        }

        [HttpGet]
        public ActionResult SearchView()
        {

            if (UserId > 0)
            {
                SearchModel model = new SearchModel();
                model.DisplayColumnsList = new List<ListItem> { new ListItem {Text = "tech", Value = "Technology" }, new ListItem { Text = "MT", Value = "Main Technology" }, new ListItem { Text = "FT", Value = "FixedBidOrTM" },
                    new ListItem { Text = "RM", Value = "Reporting Manager" },new ListItem { Text = "ASD", Value = "Assignment Start Date" },
                    new ListItem { Text = "TED", Value = "Tentative End Date" },new ListItem { Text = "SS", Value = "Signed Status" },
                    new ListItem { Text = "JS", Value = "Joining Status" },new ListItem { Text = "SOWDate", Value = "SOW Date" },
                    new ListItem { Text = "WL", Value = "Work Location" }, new ListItem { Text = "BU", Value = "Bussiness Unit" },
                    new ListItem { Text = "D", Value = "Designation" },new ListItem { Text = "RT", Value = "Reporting Time" },new ListItem { Text = "RN", Value = "Recruiter Name" },
                    new ListItem { Text = "HR", Value = "Hardware Requirement" },new ListItem { Text = "SR", Value = "Software Requirement" },new ListItem { Text = "GAR", Value = " GA Remarks" }};

                try
                {
                    model.EmployeeList = GetListAllrecord(model);
                }
                catch (Exception ex)
                {
                    TempData["message"] = ex.Message;
                    return RedirectToAction("UserDetails");
                }
                model.FromDate = Convert.ToDateTime("01/01/0001");
                model.ToDate = Convert.ToDateTime("01/01/0001");
                return View("SearchView", model);
            }
            else
            {
                //TempData["message"] = "Session End ";
                return RedirectToAction("Authentication");
            }
           
        }

        [HttpPost]
        public ActionResult Search(SearchModel model)
        //public ActionResult Search(string resourceName, string partnerName, string projectName,DateTime? fromDate,DateTime? toDate,string SearchByStartOrEnd)
        {
            if (UserId < 1)
                return RedirectToAction("UserDetails");
            else
            {
                if (model.FromDate == Convert.ToDateTime("01/01/0001 00:00:00"))
                    model.FromDate = null;
                if (model.ToDate == Convert.ToDateTime("01/01/0001 00:00:00"))
                    model.ToDate = null;
                //if (model.ResourceName == null)
                //    //model.ResourceName = string.Empty;
                //if (model.PartnerName == null)
                //    //model.PartnerName = string.Empty;
                //if (model.ProjectName == null)
                    //model.ProjectName = string.Empty;
                //SearchModel model = new SearchModel();
                try
                {
                    model.EmployeeList = GetListAllrecord(model);
                }
                catch (Exception ex)
                {
                    TempData["message"] = ex.Message;
                    return RedirectToAction("SearchView");
                }
                return PartialView("EmployeeData", model);
            }

        }


        public List<ResourceInfo> GetListAllrecord(SearchModel  model)
        {
            //SearchModel model = new SearchModel();
            DAL datalogic = new DAL();
            try
            {
                model.EmployeeList = datalogic.GetListAllrecord(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return model.EmployeeList;
        }

        [HttpGet]
        public ActionResult EditData(string id)
        {
            if (UserId < 1)
                return RedirectToAction("Authentication");
            else
            {
                ResourceInfo model = new ResourceInfo();
                DAL datalogic = new DAL();
                model = datalogic.GetDetails(id);
                return View(model);
            }

        }
        [HttpPost]
        public ActionResult EditData(ResourceInfo model)
        {
            if (UserId < 1)
                return RedirectToAction("Authentication");
            else
            {
                int res = 0;
                model.UpdatewithExt = "true";
                DAL datalogic = new DAL();
                try
                {
                    res = datalogic.InsertUpdate(model);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View("UserDetails");
                }

                return View();
            }
        }

        public ActionResult ExportData(SearchModel model)
        //public ActionResult ExportData(string resourceName, string partnerName, string projectName)
        {
            //if (string.IsNullOrEmpty(resourceName))
            //    resourceName = string.Empty;
            //if (string.IsNullOrEmpty(partnerName))
            //    partnerName = string.Empty;
            //if (string.IsNullOrEmpty(projectName))
            //    projectName = string.Empty;
            if (model.FromDate == Convert.ToDateTime("01/01/0001 00:00:00"))
                model.FromDate = null;
            if (model.ToDate == Convert.ToDateTime("01/01/0001 00:00:00"))
                model.ToDate = null;
            DAL datalogic = new DAL();
            GridView gv = new GridView();
            //gv.DataSource = datalogic.GetList(resourceName.Trim(), partnerName.Trim(), projectName.Trim()); 
            gv.DataSource = datalogic.GetList(model);
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=FulcrumData.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return RedirectToAction("SearchView");
        }



        public ActionResult Logout()
        {
            Session["UserId"] = "0";
            return RedirectToAction("Authentication");
        }
    }
}
