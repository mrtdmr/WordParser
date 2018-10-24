using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WordParser.Models
{
    public class Document
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Döküman adı giriniz.")]
        public string Name { get; set; }
        public string Path { get; set; }
        [Required(ErrorMessage = "Döküman tipini seçiniz.")]
        public int DocumentTypeId { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual ICollection<Paragraph> Paragraphs { get; set; }
    }
}