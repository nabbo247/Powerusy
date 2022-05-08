using PowerusyData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ifreighters.Controllers
{
    public class CompanyController : Controller
    {
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Index()
        {
            CompleteRegViewModel vm = new CompleteRegViewModel();
            //vm.Mode = "Add";
            //vm.IsStep1 = true;
            vm.HandleRequest();
            return View(vm);
        }
        public ActionResult IndexAgent(int? page)
        {
            CompleteRegViewModel vm = new CompleteRegViewModel();

            vm.Mode = "Add";
            vm.IsStep1 = true;
            vm.UserId = Session[SessionKeys.UserId].ToString();

            //vm.Mode = "Add";
            //vm.IsStep1 = true;
            //vm.UserId = Session["usrID"].ToString();

            vm.HandleRequest();
            if(vm.Entity==null)
            {
                vm.Mode = "Add";
                vm.IsStep1 = true;
            }
            return View(vm);
        }
        [HttpPost]
        public ActionResult IndexAgent(CompleteRegViewModel vm)
        {
            //vm.Email = Session[SessionKeys.Email].ToString();
            vm.IsValid = ModelState.IsValid;
            vm.IsDetailAreaVisible = true;
            vm.IsSearchAreaVisible = false;
            vm.IsListAreaVisible = false;
            int UsrId = Convert.ToInt32(Session["usrID"].ToString());
            //vm.Mode = "Add";
            //vm.url = ConfigurationManager.AppSettings["url"];
            vm.UserId = Session[SessionKeys.Username].ToString();
            //vm.UserId = "nabbo247@gmail.com";
            if (vm.IsStep2 == true || vm.EventCommand == "save")
            {
                if (vm.CertificateOfIncorporation != null && vm.CertificateOfIncorporation.ContentLength > 0)
                {
                    Random rsm = new Random();
                    string _FileName = Path.GetFileName(UsrId + "_" + rsm.Next(10009, 99999) + vm.CertificateOfIncorporation.FileName);
                    string _path = Path.Combine(Server.MapPath("~/upload"), _FileName);
                    vm.CertificateOfIncorporation.SaveAs(_path);
                    vm.CertificateOfIncorporationName = _FileName;
                }
                if (vm.MemorandumofAssociation != null && vm.MemorandumofAssociation.ContentLength > 0)
                {
                    Random rsm = new Random();
                    string _FileName = Path.GetFileName(UsrId + "_" + rsm.Next(10009, 99999) + vm.MemorandumofAssociation.FileName);
                    string _path = Path.Combine(Server.MapPath("~/upload"), _FileName);
                    vm.MemorandumofAssociation.SaveAs(_path);
                    vm.MemorandumofAssociationName = _FileName;
                }
                if (vm.ArticlesofAssociation != null && vm.ArticlesofAssociation.ContentLength > 0)
                {
                    Random rsm = new Random();
                    string _FileName = Path.GetFileName(UsrId + "_" + rsm.Next(10009, 99999) + vm.ArticlesofAssociation.FileName);
                    string _path = Path.Combine(Server.MapPath("~/upload"), _FileName);
                    vm.ArticlesofAssociation.SaveAs(_path);
                    vm.ArticlesofAssociationName = _FileName;
                }
                if (vm.PowerofAttorney != null && vm.PowerofAttorney.ContentLength > 0)
                {
                    Random rsm = new Random();
                    string _FileName = Path.GetFileName(UsrId + "_" + rsm.Next(10009, 99999) + vm.PowerofAttorney.FileName);
                    string _path = Path.Combine(Server.MapPath("~/upload"), _FileName);
                    vm.PowerofAttorney.SaveAs(_path);
                    vm.PowerofAttorneyName = _FileName;
                }
                if (vm.Validbusinesslicense != null && vm.Validbusinesslicense.ContentLength > 0)
                {
                    Random rsm = new Random();
                    string _FileName = Path.GetFileName(UsrId + "_" + rsm.Next(10009, 99999) + vm.Validbusinesslicense.FileName);
                    string _path = Path.Combine(Server.MapPath("~/upload"), _FileName);
                    vm.Validbusinesslicense.SaveAs(_path);
                    vm.ValidbusinesslicenseName = _FileName;
                }
                if (vm.Auditedfinancial != null && vm.Auditedfinancial.ContentLength > 0)
                {
                    Random rsm = new Random();
                    string _FileName = Path.GetFileName(UsrId + "_" + rsm.Next(10009, 99999) + vm.Auditedfinancial.FileName);
                    string _path = Path.Combine(Server.MapPath("~/upload"), _FileName);
                    vm.Auditedfinancial.SaveAs(_path);
                    vm.AuditedfinancialName = _FileName;
                }
                if (vm.Taxclearance != null && vm.Taxclearance.ContentLength > 0)
                {
                    Random rsm = new Random();
                    string _FileName = Path.GetFileName(UsrId + "_" + rsm.Next(10009, 99999) + vm.Taxclearance.FileName);
                    string _path = Path.Combine(Server.MapPath("~/upload"), _FileName);
                    vm.Taxclearance.SaveAs(_path);
                    vm.TaxclearanceName = _FileName;
                }
                if (vm.Others != null && vm.Others.ContentLength > 0)
                {
                    Random rsm = new Random();
                    string _FileName = Path.GetFileName(UsrId + "_" + rsm.Next(10009, 99999) + vm.Others.FileName);
                    string _path = Path.Combine(Server.MapPath("~/upload"), _FileName);
                    vm.Others.SaveAs(_path);
                    vm.OthersName = _FileName;
                }
            }
            if(vm.EventCommand=="edit")
            {
                vm.IsStep1 = true;
            }
            vm.HandleRequest();
            if (vm.IsValid)
            {
                TempData["Msg"] = vm.Msg;
                // NOTE: Must clear the model state in order to bind
                //       the @Html helpers to the new model values
                ModelState.Clear();
            }
            else
            {
                foreach (KeyValuePair<string, string> item in vm.ValidationErrors)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }
            }
            return View(vm);
        }

        public ActionResult Bookmarked(int? page)
        {
            BookmarkedViewModel vm = new BookmarkedViewModel();
            vm.UserId = Session["usrID"].ToString();
            vm.ListView = true;
            vm.pageSize = 10;
            vm.pageNumber = 1;
            vm.HandleRequest();
            return View(vm);
        }
        [HttpPost]
        public ActionResult Bookmarked(BookmarkedViewModel vm)
        {
            //vm.Email = Session["Email"].ToString();
            vm.IsValid = ModelState.IsValid;
            vm.ListView = true;
            vm.UserId = Session["usrID"].ToString();
            if (vm.EventCommand == "resetsearch" || vm.EventCommand == "save" || vm.EventCommand == "cancel")
            {
                vm.pageSize = 10;
                vm.pageNumber = 1;
            }
            vm.HandleRequest();
            if (vm.IsValid)
            {
                TempData["Msg"] = vm.Msg;
                // NOTE: Must clear the model state in order to bind
                //       the @Html helpers to the new model values
                ModelState.Clear();
            }
            else
            {
                foreach (KeyValuePair<string, string> item in vm.ValidationErrors)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }
            }
            return View(vm);
        }

        public ActionResult Activebids(int? page)
        {
            ActivebidsViewModel vm = new ActivebidsViewModel();
            vm.UserId = Session["usrID"].ToString();
            vm.ListView = true;
            vm.pageSize = 10;
            vm.pageNumber = 1;
            vm.HandleRequest();
            return View(vm);
        }
        [HttpPost]
        public ActionResult Activebids(ActivebidsViewModel vm)
        {
            //vm.Email = Session["Email"].ToString();
            vm.IsValid = ModelState.IsValid;
            vm.ListView = true;
            vm.UserId = Session["usrID"].ToString();
            if (vm.EventCommand == "resetsearch" || vm.EventCommand == "save" || vm.EventCommand == "cancel")
            {
                vm.pageSize = 10;
                vm.pageNumber = 1;
            }
            vm.HandleRequest();
            if (vm.IsValid)
            {
                TempData["Msg"] = vm.Msg;
                // NOTE: Must clear the model state in order to bind
                //       the @Html helpers to the new model values
                ModelState.Clear();
            }
            else
            {
                foreach (KeyValuePair<string, string> item in vm.ValidationErrors)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }
            }
            return View(vm);
        }
    }
}