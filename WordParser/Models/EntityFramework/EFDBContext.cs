using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace WordParser.Models.EntityFramework
{
    public class EFDBContext : DbContext
    {
        public DbSet<Document> Documents { get; set; }
        public DbSet<Paragraph> Paragraphs { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}