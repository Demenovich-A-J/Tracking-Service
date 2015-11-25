using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAL.ReposytoryModel.Interfaces
{
    public interface IGenericDataRepository<T> where T : class
    {
        IList<T> GetAll();
        IList<T> GetList(Func<T, bool> where);
        T GetSingle(Func<T, bool> where);
        void Add(T item);
        void Remove(T item);
        void Update(T item);
    }
}