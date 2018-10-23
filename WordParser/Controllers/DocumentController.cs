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
            Models.DocumentViewModel documentViewModel = new Models.DocumentViewModel();
            Models.Document document = context.Documents.Find(documentId);
            if (document != null)
            {
                documentViewModel.Document = document;
                if (paragraphId != 0)
                {
                    documentViewModel.Paragraph = document.Paragraphs.ToList().Find(p => p.Id == paragraphId);
                }
            }

            return View(documentViewModel);
        }
    }
}