using System.IO;
using System.Web;
using System.Web.Mvc;
using WordParser.Models.EntityFramework;
using Microsoft.Office.Interop.Word;
using System;

namespace WordParser.Controllers
{
    public class DocumentController : Controller
    {
        // GET: Document
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
            Application application = new Application();
            Document document = null;
            Style style = null;
            string paragraphText = "", paragraphName = "", paragraphContent = "";
            try
            {
                EFDBContext context = new EFDBContext();
                Models.Document d = new Models.Document { Name = file.FileName };
                context.Documents.Add(d);
                document = application.Documents.Open(path);
                foreach (Paragraph paragraph in document.Paragraphs)
                {
                    paragraphText = paragraph.Range.Text.Trim();
                    if (paragraphText != "")
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
                            if (paragraphName != "" && paragraphContent != "")
                            {
                                Models.Paragraph p = new Models.Paragraph { Name = paragraphName, Content = paragraphContent, DocumentId = d.Id };
                                context.Paragraphs.Add(p);
                                paragraphName = "";
                                paragraphContent = "";
                            }
                            paragraphName = paragraphText;
                        }
                        else
                        {
                            paragraphContent += paragraphText + "<br />";
                        }
                    }
                }
                if (paragraphName != "" && paragraphContent != "")
                {
                    Models.Paragraph p = new Models.Paragraph { Name = paragraphName, Content = paragraphContent, DocumentId = d.Id };
                    context.Paragraphs.Add(p);
                    paragraphName = "";
                    paragraphContent = "";
                }
                context.SaveChanges();
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