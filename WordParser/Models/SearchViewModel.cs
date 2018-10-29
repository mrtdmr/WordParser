using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WordParser.Models
{
    public class SearchViewModel
    {
        public string SearchString { get; set; }
        public IEnumerable<Models.Document> Documents { get; set; }
        public IEnumerable<Models.Paragraph> Paragraphs { get; set; }
    }
}