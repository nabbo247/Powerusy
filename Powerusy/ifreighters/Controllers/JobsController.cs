using PowerusyData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ifreighters.Controllers
{
    public class JobsController : Controller
    {
        // GET: Jobs
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SearchJob()
        {
             SearchjobViewModel vm = new SearchjobViewModel();
            //vm.UserId = Session["userid"].ToString();
            //vm.UserId = "eunicee";
            //vm.Email = Session["Email"].ToString();
            vm.ListView = true;
            vm.pageSize = 10;
            vm.pageNumber = 1;
            //vm.IsSearchAreaVisible = false;
            //vm.IsListAreaVisible = false;
            vm.HandleRequest();
            //vm.UsrLst.Count
            return View(vm);
        }
        [HttpPost]
        public ActionResult SearchJob(SearchjobViewModel vm)
        {
            //vm.Email = Session["Email"].ToString();
            vm.IsValid = ModelState.IsValid;
            //vm.IsDetailAreaVisible = true;
            //vm.IsSearchAreaVisible = false;
            //vm.IsListAreaVisible = false;
            //vm.EventArgument = "";
            //vm.EventCommand = "save";
            //vm.url = ConfigurationManager.AppSettings["url"];
            //vm.UserId = Session["userid"].ToString();
            //vm.UserId = "eunicee";
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