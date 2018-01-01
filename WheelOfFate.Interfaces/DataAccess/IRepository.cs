﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WheelOfFate.Interfaces.DataAccess
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity item);

        void Add(IEnumerable<TEntity> items);

        TEntity FindById(int id);

        IEnumerable<TEntity> Get();

        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);

        void Remove(TEntity item);

        void Update(TEntity item);
    }
}
