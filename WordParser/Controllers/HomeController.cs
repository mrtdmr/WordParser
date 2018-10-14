using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WordParser.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Parse(HttpPostedFileBase file)
        {
            string path = Path.Combine(Server.MapPath("~/files"),
                                       Path.GetFileName(file.FileName));
            file.SaveAs(path);
            ViewBag.Message = "File uploaded successfully";

            return View("Index");
        }
    }
}