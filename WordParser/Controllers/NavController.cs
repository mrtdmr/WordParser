using System.IO;
using System.Web;
using System.Web.Mvc;
using WordParser.Models.EntityFramework;
using Microsoft.Office.Interop.Word;
using System;
using System.Linq;

namespace WordParser.Controllers
{
    public class NavController : Controller
    {
        EFDBContext context = new EFDBContext();
        // GET: Nav
        public PartialViewResult Menu(int documentId, int paragraphId)
        {
            ViewBag.SelectedParagraphId = paragraphId;
            Models.Document document = context.Documents.Find(documentId);
            return PartialView(document.Paragraphs);
        }
    }
}