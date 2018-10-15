using System.IO;
using System.Web;
using System.Web.Mvc;
using WordParser.Models.EntityFramework;
using System;
using System.Linq;

namespace WordParser.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        EFDBContext context = new EFDBContext();
        public ActionResult Index()
        {
            return View(context.Documents.ToList());
        }
    }
}