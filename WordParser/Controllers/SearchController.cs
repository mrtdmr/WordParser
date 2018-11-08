using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WordParser.Infrastructure;

namespace WordParser.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public PartialViewResult Search(string selectedDocumentTypeId)
        {
            ViewBag.DocumentTypeId = new SelectList(Repository.DocumentTypeRepository().GetAll(), "Id", "Name", selectedDocumentTypeId != null ? selectedDocumentTypeId : "");
            return PartialView();
        }
    }
}