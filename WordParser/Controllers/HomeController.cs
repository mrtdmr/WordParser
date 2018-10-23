﻿using System.IO;
using System.Web;
using System.Web.Mvc;
using WordParser.Models.EntityFramework;
//using Microsoft.Office.Interop.Word;
using System;
using System.Linq;
using Spire.Doc;
using Spire.Doc.Documents;
using System.Text;

namespace WordParser.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        EFDBContext context = new EFDBContext();
        public ViewResult Index()
        {
            //ViewBag.DocumentTypeId = new SelectList(context.DocumentTypes, "Id", "Name");
            return View(context.Documents.ToList());
        }
        public ViewResult Detail(int documentId, int paragraphId)
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
        public ViewResult Add()
        {
            ViewBag.DocumentTypeId = new SelectList(context.DocumentTypes, "Id", "Name", "");
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public RedirectToRouteResult Add(HttpPostedFileBase file, Models.Document d)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    ViewBag.FileError = "Dosya Seçiniz.";
                    string path = Path.Combine(Server.MapPath("~/files"),
                                       Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    d.Path = file.FileName;
                    context.Documents.Add(d);
                    Document document = new Document();
                    document.LoadFromFile(path);
                    string paragraphText = "", paragraphName = "", paragraphContent = "";

                    foreach (Section section in document.Sections)
                    {
                        foreach (Paragraph paragraph in section.Body.Paragraphs)
                        {
                            paragraphText = paragraph.Text.Trim();

                            if (paragraphText != "")
                            {
                                if (
                                    paragraph.StyleName == "Heading1" ||
                                    paragraph.StyleName == "Heading2" ||
                                    paragraph.StyleName == "Heading3" ||
                                    paragraph.StyleName == "Heading4" ||
                                    paragraph.StyleName == "Heading5" ||
                                    paragraph.StyleName == "Heading6" ||
                                    paragraph.StyleName == "Heading7" ||
                                    paragraph.StyleName == "Heading8" ||
                                    paragraph.StyleName == "Heading9")
                                {
                                    if (paragraphName != "" && paragraphContent != "")
                                    {
                                        Models.Paragraph p = new Models.Paragraph { Name = paragraphName, Content = paragraphContent, DocumentId = d.Id };
                                        context.Paragraphs.Add(p);
                                        paragraphName = "";
                                        paragraphContent = "";
                                    }
                                    paragraphName = paragraphText;
                                }
                                else
                                {
                                    paragraphContent += paragraphText.Replace("\r", "").Replace("\a", "") + "<br />";
                                }
                            }
                        }
                        if (paragraphName != "" && paragraphContent != "")
                        {
                            Models.Paragraph p = new Models.Paragraph { Name = paragraphName, Content = paragraphContent, DocumentId = d.Id };
                            context.Paragraphs.Add(p);
                            paragraphName = "";
                            paragraphContent = "";
                        }
                        context.SaveChanges();
                        foreach (Table table in section.Body.Tables)
                        {

                        }
                    }
                }
                else
                {
                    ViewBag.FileError = "Dosya Seçiniz";
                }
            }
            ViewBag.DocumentTypeId = new SelectList(context.DocumentTypes, "Id", "Name", "");
            return RedirectToAction("Index", "Home");
        }
        public ViewResult Update(int documentId)
        {
            Models.Document document = context.Documents.Find(documentId);
            if (document != null)
            {
                return View(document);
            }
            else
            {
                return View("Index", context.Documents.ToList());
            }
        }
        public ViewResult Delete(int documentId)
        {
            Models.Document document = context.Documents.Find(documentId);
            if (document != null)
            {
                return View(document);
            }
            else
            {
                return View("Index", context.Documents.ToList());
            }
        }
        [HttpPost, ActionName("Delete")]
        public RedirectToRouteResult DeleteConfirmed(int documentId)
        {
            Models.Document document = context.Documents.Find(documentId);
            if (document != null)
            {
                context.Documents.Remove(document);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
        static void ExtractTextFromTables(Table table, StreamWriter sw)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                TableRow row = table.Rows[i];
                for (int j = 0; j < row.Cells.Count; j++)
                {
                    TableCell cell = row.Cells[j];
                    foreach (Paragraph paragraph in cell.Paragraphs)
                    {
                        sw.Write(paragraph.Text);
                    }
                }
            }
        }
        private void WordDocViewer(string fileName)
        {
            try
            {
                System.Diagnostics.Process.Start(fileName);
            }
            catch { }
        }
        /*
        private void Eski()
        {
            Application application = new Application();
            Document document = null;
            Style style = null;
            string paragraphText = "", paragraphName = "", paragraphContent = "";
            try
            {
                Models.Document d = new Models.Document { Name = name, Path = file.FileName };
                context.Documents.Add(d);
                document = application.Documents.Open(path);
                foreach (Paragraph paragraph in document.Paragraphs)
                {
                    paragraphText = paragraph.Range.Text.Trim();

                    if (paragraphText != "")
                    {
                        style = paragraph.get_Style();
                        if (
                            style.NameLocal == "Heading 1" ||
                            style.NameLocal == "Heading 2" ||
                            style.NameLocal == "Heading 3" ||
                            style.NameLocal == "Heading 4" ||
                            style.NameLocal == "Heading 5" ||
                            style.NameLocal == "Heading 6" ||
                            style.NameLocal == "Heading 7" ||
                            style.NameLocal == "Heading 8" ||
                            style.NameLocal == "Heading 9")
                        {
                            if (paragraphName != "" && paragraphContent != "")
                            {
                                Models.Paragraph p = new Models.Paragraph { Name = paragraphName, Content = paragraphContent, DocumentId = d.Id };
                                context.Paragraphs.Add(p);
                                paragraphName = "";
                                paragraphContent = "";
                            }
                            paragraphName = paragraphText;
                        }
                        else
                        {
                            paragraphContent += paragraphText.Replace("\r", "").Replace("\a", "") + "<br />";
                        }
                    }
                }
                if (paragraphName != "" && paragraphContent != "")
                {
                    Models.Paragraph p = new Models.Paragraph { Name = paragraphName, Content = paragraphContent, DocumentId = d.Id };
                    context.Paragraphs.Add(p);
                    paragraphName = "";
                    paragraphContent = "";
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                document.Close();
            }
        }
        */
    }
}