using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.ReposytoryModel.Interfaces;
using EntytiModel;

namespace DAL.ReposytoryModel.AbstractClasses
{
    public abstract class GenericDataRepitory<T, K> : IGenericDataRepository<T>
        where T : class
        where K : class
    {
        protected abstract K ObjectToEntity(T item);

        protected abstract T EntityToObject(K item);

        public virtual IList<T> GetAll()
        {
            List<T> list;
            using (var context = new ManagerSaleDBEntities())
            {
                list = context
                    .Set<K>()
                    .AsNoTracking()
                    .Select(x => EntityToObject(x))
                    .ToList();
            }
            return list;
        }

        public virtual IList<T> GetList(Func<T, bool> where)
        {
            List<T> list;
            using (var context = new ManagerSaleDBEntities())
            {
                list = context
                    .Set<K>()
                    .AsNoTracking()
                    .Select(x => EntityToObject(x))
                    .ToList();
            }
            return list;
        }

        public virtual T GetSingle(Func<T, bool> where)
        {
            T item;
            using (var context = new ManagerSaleDBEntities())
            {
                item = context
                .Set<K>()
                .AsNoTracking()
                .Select(x => EntityToObject(x))
                .FirstOrDefault(where);
            }
            return item;
        }

        public virtual void Add(T item)
        {
            using (var context = new ManagerSaleDBEntities())
            {
                context.Entry(ObjectToEntity(item)).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public virtual void Add(IEnumerable<T> items)
        {
            using (var context = new ManagerSaleDBEntities())
            {
                foreach (var item in items)
                {
                    context.Entry(ObjectToEntity(item)).State = EntityState.Added;
                }
                context.SaveChanges();
            }
        }

        public virtual void Remove(T item)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(T item)
        {
            throw new NotImplementedException();
        }
    }
}