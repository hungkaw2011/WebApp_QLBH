﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        T GetFirstOrDefault(Expression<Func<T, bool>> filter);
        IEnumerable<T> GetAll();
        void Add(T item);
        void Remove(T item);
        void RemoveRange(IEnumerable<T> items);
    }
}
