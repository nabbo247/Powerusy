using System;
using System.Collections.Generic;
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
            return View();
        }
        public ActionResult ManageJobs()
        {
            return View();
        }
        public ActionResult MyProfile()
        {
            return View();
        }
        
    }
}