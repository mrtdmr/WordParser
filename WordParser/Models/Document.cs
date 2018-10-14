using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WordParser.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Paragraph> Paragraphs { get; set; }
    }
}