﻿using System.Collections.Generic;

namespace DAL
{
    public interface IModelRepository<T>
    {
        void Add(T item);
        void Remove(T item);
        void Update(T item);
        IEnumerable<T> Items { get; }
        void SaveChanges(); 
    }
}