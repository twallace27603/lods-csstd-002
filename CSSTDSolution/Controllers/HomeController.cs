using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace CSSTDSolution.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(): base()
        {
            ViewBag.Advanced = ConfigurationManager.AppSettings["Advanced"];
            ViewBag.TestType = int.Parse(ConfigurationManager.AppSettings["TestType"]);
            if(ViewBag.Advanced.ToString().ToLower()=="true")
            {
                ViewBag.StorageConnection = ConfigurationManager.AppSettings["StorageConnection"];
                ViewBag.SQLConnection = ConfigurationManager.AppSettings["SQLConnection"];
                ViewBag.MySQLConnection = ConfigurationManager.AppSettings["MySQLConnection"];
                ViewBag.CosmosDBConnection = ConfigurationManager.AppSettings["CosmosDBConnection"];
                ViewBag.SearchConnection = ConfigurationManager.AppSettings["SearchConnection"];

            }
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Storage()
        {
            return View();
        }
        public ActionResult Relational()
        {
            return View();
        }
        public ActionResult NoSQL()
        {
            return View();
        }

 
    }
}