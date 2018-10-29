using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WordParser.Infrastructure.Abstract;
using WordParser.Models.EntityFramework;

namespace WordParser.Infrastructure
{
    public class Repository
    {
        static EFDBContext _dbContext;
        static IRepository<Models.Document> _documentRepository;
        static IRepository<Models.DocumentType> _dtRepository;
        static IRepository<Models.Paragraph> _paragraphRepository;
        static Repository()
        {
            if (_dbContext==null)
            {
                _dbContext = new EFDBContext();
            }
        }
        public static IRepository<Models.Document> DocumentRepository()
        {
            if (_documentRepository == null)
            {
                _documentRepository = new EFRepository<Models.Document>(_dbContext);
            }
            return _documentRepository;
        }
        public static IRepository<Models.DocumentType> DocumentTypeRepository()
        {
            if (_dtRepository == null)
            {
                _dtRepository = new EFRepository<Models.DocumentType>(_dbContext);
            }
            return _dtRepository;
        }
        public static IRepository<Models.Paragraph> ParagraphRepository()
        {
            if (_paragraphRepository == null)
            {
                _paragraphRepository = new EFRepository<Models.Paragraph>(_dbContext);
            }
            return _paragraphRepository;
        }
    }
}