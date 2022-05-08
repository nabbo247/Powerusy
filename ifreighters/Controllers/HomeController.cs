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
            //vm.UserId = Session[SessionKeys.Username].ToString();
            //vm.UserId = "eunicee";
            //vm.Email = Session[SessionKeys.Email].ToString();
            vm.ListView = true;
            vm.pageSize = 10;
            vm.pageNumber = 1;
            //vm.IsSearchAreaVisible = false;
            //vm.IsListAreaVisible = false;
            vm.HandleRequest();
            return View(vm);
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