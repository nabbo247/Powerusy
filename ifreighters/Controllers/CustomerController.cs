﻿using PowerusyData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ifreighters.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Dashboard()
        {
            var dash = new DashboardViewModel();
            dash.Username = $"{Session[SessionKeys.Username]}";
            dash.HandleRequest();
            Session[SessionKeys.Dashboard] = dash;
            return View();
        }
        public ActionResult MyProfile(int? page)
        {
            MyProfileViewModel vm = new MyProfileViewModel();
            //vm.UserId = Session["userid"].ToString();
            //vm.UserId = "eunicee";
            //vm.Email = "nabbo247@gmail.com";
            //vm.IsDetailAreaVisible = true;
            //vm.IsSearchAreaVisible = false;
            //vm.IsListAreaVisible = false;
            vm.EventArgument = Session["usrID"].ToString();
            vm.EventCommand = "Edit";
            vm.HandleRequest();
            return View(vm);
        }
        [HttpPost]
        public ActionResult MyProfile(MyProfileViewModel vm)
        {
            //vm.Email = Session["Email"].ToString();
            vm.IsValid = ModelState.IsValid;
            vm.IsDetailAreaVisible = true;
            vm.IsSearchAreaVisible = false;
            vm.IsListAreaVisible = false;
            //vm.EventArgument = "";
            //vm.EventCommand = "save";
            //vm.Mode = "Add";
            //vm.url = ConfigurationManager.AppSettings["url"];

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
        public ActionResult ManageJobs(int? page)
        {
            BiddingViewModel vm = new BiddingViewModel();
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
        public ActionResult ManageJobs(BiddingViewModel vm)
        {
            //vm.Email = Session["Email"].ToString();
            vm.IsValid = ModelState.IsValid;
            vm.IsDetailAreaVisible = true;
            vm.IsSearchAreaVisible = false;
            vm.IsListAreaVisible = false;
            //vm.EventArgument = "";
            //vm.EventCommand = "save";
            //vm.Mode = "Add";
            //vm.url = ConfigurationManager.AppSettings["url"];
            vm.UserId = Session["userid"].ToString();
            //vm.UserId = "nabbo247@gmail.com";
            if (vm.BillofLading != null && vm.BillofLading.ContentLength > 0)
            {
                Random rsm = new Random();
                string _FileName = Path.GetFileName( rsm.Next(10009, 99999) + vm.BillofLading.FileName);
                string _path = Path.Combine(Server.MapPath("~/upload"), _FileName);
                vm.BillofLading.SaveAs(_path);
                vm.BillofLadingName = _FileName;
            }
            if (vm.PackagingLists != null && vm.PackagingLists.ContentLength > 0)
            {
                Random rsm = new Random();
                string _FileName = Path.GetFileName(rsm.Next(10009, 99999) + vm.PackagingLists.FileName);
                string _path = Path.Combine(Server.MapPath("~/upload"), _FileName);
                vm.PackagingLists.SaveAs(_path);
                vm.PackagingListsName = _FileName;
            }
            if (vm.Proformainvoice != null && vm.Proformainvoice.ContentLength > 0)
            {
                Random rsm = new Random();
                string _FileName = Path.GetFileName( rsm.Next(10009, 99999) + vm.Proformainvoice.FileName);
                string _path = Path.Combine(Server.MapPath("~/upload"), _FileName);
                vm.Proformainvoice.SaveAs(_path);
                vm.ProformainvoiceName = _FileName;
            }
            if (vm.Others != null && vm.Others.ContentLength > 0)
            {
                Random rsm = new Random();
                string _FileName = Path.GetFileName( rsm.Next(10009, 99999) + vm.Others.FileName);
                string _path = Path.Combine(Server.MapPath("~/upload"), _FileName);
                vm.Others.SaveAs(_path);
                vm.OthersName = _FileName;
            }
            if (vm.ItemPix != null && vm.ItemPix.ContentLength > 0)
            {
                Random rsm = new Random();
                string _FileName = Path.GetFileName(rsm.Next(10009, 99999) + vm.ItemPix.FileName);
                string _path = Path.Combine(Server.MapPath("~/upload"), _FileName);
                vm.ItemPix.SaveAs(_path);
                vm.ItemPixName = _FileName;
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
        public ActionResult Jobdetail(int? page)
        {
            JobdetailViewModel vm = new JobdetailViewModel();
            if(Session["Role"]!=null&& Session["Role"].ToString()=="2")
            {
                vm.Owner = false;
            }else
            vm.Owner = true;
            if(Session["usrID"]!=null)
              vm.UserId = Session["usrID"].ToString();
            vm.EventArgument = page.ToString();
            vm.EventCommand = "Edit";
            vm.JobBid.BidID = Convert.ToInt32(page.ToString());
            vm.HandleRequest();
            return View(vm);
        }
        [HttpPost]
        public ActionResult Jobdetail(JobdetailViewModel vm)
        {
            if (Session["Role"] != null && Session["Role"].ToString() == "2")
            {
                vm.Owner = false;
            }
            else
                vm.Owner = true;
            if (Session["usrID"] != null)
                vm.UserId = Session["usrID"].ToString();
             vm.EventArgument = vm.JobBid.BidID.ToString();
            //
            //vm.JobBid.BidID = Convert.ToInt32(page.ToString());
            vm.HandleRequest();
            vm.EventCommand = "Edit";
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
        public ActionResult BookmarkJob(int bid)
        {
            JobdetailViewModel vm = new JobdetailViewModel();
            if (Session[SessionKeys.Role] != null && Session[SessionKeys.Role].ToString() == "2")
            {
                vm.Owner = false;
            }
            else
                vm.Owner = true;
            if (Session[SessionKeys.UserId] != null)
                vm.UserId = Session[SessionKeys.UserId].ToString();
            vm.EventArgument = bid.ToString();
            vm.EventCommand = "Edit";
            vm.JobBid.BidID = Convert.ToInt32(bid.ToString());
            vm.HandleRequest();
            vm.BookmarkJob(bid);

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
            return View("Jobdetail", vm);
        }

        public ActionResult Activebids(int? page)
        {
            BiddingViewModel vm = new BiddingViewModel();
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
        public ActionResult Activebids(BiddingViewModel vm)
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
        public ActionResult Managebidders(int? page)
        {
            BiddingViewModel vm = new BiddingViewModel();
            //vm.UserId = Session["userid"].ToString();
            //vm.UserId = "eunicee";
            //vm.Email = Session["Email"].ToString();
            //vm.IsDetailAreaVisible = true;
            //vm.IsSearchAreaVisible = false;
            //vm.IsListAreaVisible = false;
            vm.HandleRequest();
            return View(vm);
        }
        public ActionResult Messages(int? page)
        {
            BiddingViewModel vm = new BiddingViewModel();
            //vm.UserId = Session["userid"].ToString();
            //vm.UserId = "eunicee";
            //vm.Email = Session["Email"].ToString();
            //vm.IsDetailAreaVisible = true;
            //vm.IsSearchAreaVisible = false;
            //vm.IsListAreaVisible = false;
            vm.HandleRequest();
            return View(vm);
        }
        
    }
}