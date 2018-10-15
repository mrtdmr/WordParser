using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WordParser.Models
{
    public class Paragraph
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int DocumentId { get; set; }
        public virtual Document Document { get; set; }
    }
}