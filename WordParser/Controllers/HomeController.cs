using System.IO;
using System.Web;
using System.Web.Mvc;
using WordParser.Models.EntityFramework;
using Microsoft.Office.Interop.Word;
using System;

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
            Application application = new Application();
            Document document = null;
            Style style = null;
            string paragraphName = "",paragraphText = "";
            EFDBContext context = new EFDBContext();
            Models.Document d = new WordParser.Models.Document { Name = file.FileName };
            context.Documents.Add(d);
            try
            {
                document = application.Documents.Open(path);
                foreach (Paragraph paragraph in document.Paragraphs)
                {
                    style = paragraph.get_Style();
                    if (
                        style.NameLocal == "Heading 1" ||
                        style.NameLocal == "Heading 2" ||
                        style.NameLocal == "Heading 3" ||
                        style.NameLocal == "Heading 4" ||
                        style.NameLocal == "Heading 5" ||
                        style.NameLocal == "Heading 6" ||
                        style.NameLocal == "Heading 7" ||
                        style.NameLocal == "Heading 8" ||
                        style.NameLocal == "Heading 9")
                    {
                        paragraphName = paragraph.Range.Text.Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                document.Close();
            }
            return View("Index");
        }
    }
}