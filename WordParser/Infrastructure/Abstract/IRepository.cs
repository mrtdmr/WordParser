﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WordParser.Infrastructure.Abstract
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(params object[] parametreler);
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
        T GetById(int id);
    }
}
