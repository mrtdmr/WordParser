using System.IO;
using System.Web;
using System.Web.Mvc;
using WordParser.Models.EntityFramework;
using Microsoft.Office.Interop.Word;
using System;
using System.Linq;

namespace WordParser.Controllers
{
    public class DocumentController : Controller
    {
        EFDBContext context = new EFDBContext();
        // GET: Document
        public ActionResult Index(int documentId, int paragraphId)
        {
            Models.Document document = context.Documents.Find(documentId);
            if (paragraphId != 0)
            {
                Models.Paragraph paragraph = document.Paragraphs.ToList().Find(p => p.Id == paragraphId);
                ViewBag.Content = paragraph.Content;
                ViewBag.ParagraphName = paragraph.Name;
            }
            return View(document);
        }
    }
}