using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WordParser.Infrastructure;

namespace WordParser.Controllers
{
    public class SearchResultController : Controller
    {
        // GET: SearchResult
        [HttpPost]
        public ViewResult Index(string searchString, int documentTypeId)
        {
            if (searchString.Trim() != "")
            {
                Models.SearchResultViewModel searchResultViewModel = new Models.SearchResultViewModel();
                searchResultViewModel.Documents = Repository.DocumentRepository().GetAll().Where(d => d.Name.ToUpper().Contains(searchString.ToUpper()) && d.DocumentTypeId == documentTypeId).ToList();
                searchResultViewModel.Paragraphs = Repository.ParagraphRepository().GetAll().Where(p => p.Name.Replace("'", "").ToUpper().Contains(searchString.ToUpper()) || p.Content.Replace("'", "").ToUpper().Contains(searchString.ToUpper()) && p.Document.DocumentTypeId == documentTypeId).ToList();
                searchResultViewModel.SearchString = searchString;
                ViewBag.selectedDocumentTypeId = documentTypeId;
                return View("Index", searchResultViewModel);
            }
            else
            {
                ViewBag.AramaHata = "Aranacak kelimeyi giriniz...";
                ViewBag.selectedDocumentTypeId = documentTypeId;
                return View("Index");
            }
        }
    }
}