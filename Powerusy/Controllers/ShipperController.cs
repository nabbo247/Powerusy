using PowerusyData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Powerusy.Controllers
{
    public class ShipperController : Controller
    {
        // GET: Shipper
        
        public ActionResult CompleteReg(int? page)
        {
            CompleteRegViewModel vm = new CompleteRegViewModel();
            vm.Mode = "Add";
            vm.HandleRequest();
            return View(vm);
        }
        [HttpPost]
        public ActionResult CompleteReg(CompleteRegViewModel vm)
        {
            //vm.Email = Session["Email"].ToString();
            vm.IsValid = ModelState.IsValid;
            vm.IsDetailAreaVisible = true;
            vm.IsSearchAreaVisible = false;
            vm.IsListAreaVisible = false;
            var sss = vm.EventCommand ;
            //vm.Mode = "Add";
            //vm.url = ConfigurationManager.AppSettings["url"];
            //vm.UserId = Session["userid"].ToString();
            //vm.UserId = "eunicee";
            if (vm.uploadedImage != null && vm.uploadedImage.ContentLength > 0)
            {
                string _FileName = Path.GetFileName(vm.uploadedImage.FileName);
                string _path = Path.Combine(Server.MapPath("~/upload"), _FileName);
                vm.uploadedImage.SaveAs(_path);
                //file path 
                //vm.PosR.ApplicationForm1 = _FileName;
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