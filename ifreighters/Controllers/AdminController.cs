using PowerusyData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ifreighters.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Dashboard()
        {
            var dash = new DashboardViewModel();
            dash.Username = $"{Session[SessionKeys.Username]}";
            dash.Get2();
            Session[SessionKeys.Dashboard] = dash;
            return View();
        }
        
        public ActionResult VerificationShipper(int? page)
        {
            VerificationShipperViewModel vm = new VerificationShipperViewModel();
            vm.HandleRequest();
            return View(vm);
        }
        [HttpPost]
        public ActionResult VerificationShipper(VerificationShipperViewModel vm)
        {
            //vm.Email = Session["Email"].ToString();
            vm.IsValid = ModelState.IsValid;
            //var sss = vm.EventCommand;
            //vm.Mode = "Add";
            //vm.url = ConfigurationManager.AppSettings["url"];
            vm.UserId = Session["usrID"].ToString();

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
        public ActionResult ManageJobsA(int? page)
        {
            AdminBiddingViewModel vm = new AdminBiddingViewModel();
            vm.UserId = Session["usrID"].ToString();
            //vm.UserId = "eunicee";
            //vm.Email = Session["Email"].ToString();
            //vm.IsDetailAreaVisible = true;
            //vm.IsSearchAreaVisible = false;
            //vm.IsListAreaVisible = false;
            vm.HandleRequest();
            return View(vm);
        }
        [HttpPost]
        public ActionResult ManageJobsA(AdminBiddingViewModel vm)
        {
            vm.UserId = Session["usrID"].ToString();
            vm.IsValid = ModelState.IsValid;
            vm.IsDetailAreaVisible = true;
            vm.IsSearchAreaVisible = false;
            vm.IsListAreaVisible = false;
            vm.UserId = Session["userid"].ToString();
            //vm.UserId = "nabbo247@gmail.com";
            
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

        public ActionResult ManageCustomer(int? page)
        {
            ManageCustomerViewModel vm = new ManageCustomerViewModel();
            //vm.pageInfo = "This page allows Relationship Officer to Track his/ her Request";
            vm.pageSize = 100;
            vm.pageNumber = (page ?? 1);
            vm.UserId = Session["usrID"].ToString();
            vm.HandleRequest();
            return View(vm);
        }
        [HttpPost]
        public ActionResult ManageCustomer(ManageCustomerViewModel vm)
        {
            vm.IsValid = ModelState.IsValid;
            if (vm.EventCommand == "resetsearch" || vm.EventCommand == "save" || vm.EventCommand == "cancel")
            {
                vm.pageSize = 100;
                vm.pageNumber = 1;
            }
            vm.UserId = Session["usrID"].ToString();
            //vm.UserId = "eunicee";
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
        //ManageCustomer

        public ActionResult ManageCar(int? page)
        {
            ManageCarViewModel vm = new ManageCarViewModel();
            //vm.pageInfo = "This page allows Relationship Officer to Track his/ her Request";
            vm.pageSize = 100;
            vm.pageNumber = (page ?? 1);
            vm.UserId = Session["usrID"].ToString();
            vm.HandleRequest();
            return View(vm);
        }
        [HttpPost]
        public ActionResult ManageCar(ManageCarViewModel vm)
        {
            vm.IsValid = ModelState.IsValid;
            if (vm.EventCommand == "resetsearch" || vm.EventCommand == "save" || vm.EventCommand == "cancel")
            {
                vm.pageSize = 100;
                vm.pageNumber = 1;
            }
            vm.UserId = Session["usrID"].ToString();
            //vm.UserId = "eunicee";
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
        //ManageCar

        public ActionResult ManagePorts(int? page)
        {
            ManagePortsViewModels vm = new ManagePortsViewModels();
            //vm.pageInfo = "This page allows Relationship Officer to Track his/ her Request";
            vm.pageSize = 100;
            vm.pageNumber = (page ?? 1);
            vm.UserId = Session["usrID"].ToString();
            vm.HandleRequest();
            return View(vm);
        }
        [HttpPost]
        public ActionResult ManagePorts(ManagePortsViewModels vm)
        {
            vm.IsValid = ModelState.IsValid;
            if (vm.EventCommand == "resetsearch" || vm.EventCommand == "save" || vm.EventCommand == "cancel")
            {
                vm.pageSize = 100;
                vm.pageNumber = 1;
            }
            vm.UserId = Session["usrID"].ToString();
            //vm.UserId = "eunicee";
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
        //ManagePorts
    }
}