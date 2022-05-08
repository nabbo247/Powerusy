using PowerusyData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ifreighters.Controllers
{

    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registration(int? page)
        {
            RegistrationViewModel vm = new RegistrationViewModel();
            //vm.UserId = Session[SessionKeys.Username].ToString();
            //vm.UserId = "eunicee";
            //vm.Email = Session[SessionKeys.Email].ToString();
            //vm.IsDetailAreaVisible = true;
            //vm.IsSearchAreaVisible = false;
            //vm.IsListAreaVisible = false;
            vm.HandleRequest();
            return View(vm);
        }
        [HttpPost]
        public ActionResult Registration(RegistrationViewModel vm)
        {
            //vm.Email = Session[SessionKeys.Email].ToString();
            vm.IsValid = ModelState.IsValid;
            vm.IsDetailAreaVisible = true;
            vm.IsSearchAreaVisible = false;
            vm.IsListAreaVisible = false;
            //vm.EventArgument = "";
            //vm.EventCommand = "save";
            vm.Mode = "Add";
            //vm.url = ConfigurationManager.AppSettings["url"];
            //vm.UserId = Session[SessionKeys.Username].ToString();
            //vm.UserId = "eunicee";
            if (vm.uploadedImage != null && vm.uploadedImage.ContentLength > 0)
            {
                string _FileName = Path.GetFileName(vm.uploadedImage.FileName);
                string _path = Path.Combine(Server.MapPath("~/upload"), _FileName);
                vm.uploadedImage.SaveAs(_path);
                //file path 
                vm.Entity.Logo = _FileName;
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
        public ActionResult Login(int? page)
        {
            LoginViewModel vm = new LoginViewModel();
            //vm.UserId = Session[SessionKeys.Username].ToString();
            //vm.UserId = "eunicee";
            //vm.Email = Session[SessionKeys.Email].ToString();
            vm.IsDetailAreaVisible = true;
            //vm.IsSearchAreaVisible = false;
            vm.IsValid = true;
            vm.HandleRequest();
            return View(vm);
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel vm)
        {
            //vm.Email = Session[SessionKeys.Email].ToString();
            vm.IsValid = ModelState.IsValid;
            vm.IsDetailAreaVisible = true;
            vm.IsSearchAreaVisible = false;
            vm.IsListAreaVisible = false;
            vm.Mode = "validate";
            vm.HandleRequest();
            if (vm.ValidationErrors.Count > 0)
            {
                foreach (KeyValuePair<string, string> item in vm.ValidationErrors)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }
                //TempData["Msg"] = "Invalid User, Check your username and password";
                return View(vm);
            }

            Session["view_model"] = vm;

            Session[SessionKeys.Username] = vm.Entity.username;// vm.UserId;
            Session[SessionKeys.FName] = vm.Entity.firstname;
            Session[SessionKeys.Email] = vm.Entity.email;
            Session[SessionKeys.Role] = vm.Entity.roleid;
            Session[SessionKeys.UserId] = vm.Entity.id;
            if (vm.IsValid)
            {
                TempData["Msg"] = vm.Msg;
                // NOTE: Must clear the model state in order to bind
                //       the @Html helpers to the new model values
                ModelState.Clear();
                //if (vm.Entity.roleid == 1)
                //    return RedirectToAction("VerificationShipper", "Company");
                //if (vm.Entity.roleid == 2)
                //    return RedirectToAction("CompleteReg", "Company");
                switch (vm.Entity.roleid)//switch (vm.Entity.StatusId)
                {
                    case 1:
                        RedirectToAction("CompleteReg", "Admin");
                        break;
                    case 2:
                        return RedirectToAction("IndexAgent", "Company");
                        break;
                    default:
                        return RedirectToAction("Dashboard", "Customer");
                        /*
                          case 3:
                        return RedirectToAction("IndexAgent", "Company"); //Shipper/Importer FreightForwarder
                        break;
                        */
                        //case 2:
                        //    RedirectToAction("Index", "Company");
                        //    break;
                        //case 3:
                        //    return RedirectToAction("IndexShipper", "Company"); //Shipper/Importer FreightForwarder
                        //    break;
                        //case 4:
                        //    return RedirectToAction("FreightForwarder", "Company"); //Shipper/Importer 
                        //    break;
                        //default:
                        //    return RedirectToAction("IndexForwarder", "Company");
                }
            }
            else
            {
                foreach (KeyValuePair<string, string> item in vm.ValidationErrors)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }
            }
            //return RedirectToAction("CompleteReg", "Shipper");
            return null;
        }
        public ActionResult LogOff()
        {
            //AuthenticationManager.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ForgetPassword(int? page)
        {
            LoginViewModel vm = new LoginViewModel();
            //vm.UserId = Session[SessionKeys.Username].ToString();
            //vm.UserId = "eunicee";
            //vm.Email = Session[SessionKeys.Email].ToString();
            vm.IsDetailAreaVisible = true;
            //vm.IsSearchAreaVisible = false;
            vm.IsValid = true;
            vm.HandleRequest();
            return View(vm);
        }
        [HttpPost]
        public ActionResult ForgetPassword(LoginViewModel vm)
        {
            //vm.Email = Session[SessionKeys.Email].ToString();
            vm.IsValid = ModelState.IsValid;
            vm.IsDetailAreaVisible = true;
            vm.IsSearchAreaVisible = false;
            vm.IsListAreaVisible = false;
            //vm.EventArgument = "";
            //vm.EventCommand = "save";
            //vm.Mode = "Add";
            //vm.url = ConfigurationManager.AppSettings["url"];

            //vm.UserId = Session[SessionKeys.Username].ToString();
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

    }
}