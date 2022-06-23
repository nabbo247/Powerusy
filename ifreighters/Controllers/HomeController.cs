using PowerusyData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ifreighters.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            SearchjobViewModel vm = new SearchjobViewModel();
            //vm.UserId = Session["userid"].ToString();
            //vm.UserId = "eunicee";
            //vm.Email = Session["Email"].ToString();
            vm.ListView = true;
            vm.homesearch = true;
            vm.pageSize = 10;
            vm.pageNumber = 1;
            //vm.IsSearchAreaVisible = false;
            //vm.IsListAreaVisible = false;
            vm.HandleRequest();
            return View(vm);
        }
        [HttpPost]
        public ActionResult Index(SearchjobViewModel vm)
        {
            //vm.Email = Session["Email"].ToString();
            //vm.IsValid = ModelState.IsValid;
            //vm.IsDetailAreaVisible = true;
            //vm.IsSearchAreaVisible = false;
            //vm.IsListAreaVisible = false;
            //vm.EventArgument = "";
            //vm.EventCommand = "save";
            //vm.Mode = "Add";
            //vm.url = ConfigurationManager.AppSettings["url"];
            //vm.UserId = Session["userid"].ToString();
            vm.homesearch = true;
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
        public JsonResult GetModel(string MakeId)
        {
            SearchjobViewModel vm = new SearchjobViewModel();
            var retu = vm.GetModel(MakeId);
            return Json(retu);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}