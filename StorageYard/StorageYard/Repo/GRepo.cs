using StorageYard.FContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageYard.Repo
{
    public class GRepo<TContext> : IRepo
        where TContext : DbContext, new()
    {

        public TContext Context { get; private set; }

        public GRepo()
        {
            Context = new TContext();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public T Delete<T>(T item, bool saveNow) where T : class
        {
            Context.Entry(item).State = EntityState.Deleted;
            if (saveNow)
                Context.SaveChanges();
            return item;
        }

        public T Insert<T>(T item, bool saveNow) where T : class
        {
            Context.Entry(item).State = EntityState.Added;
            if (saveNow)
                Context.SaveChanges();
            return item;
        }

        public T Update<T>(T item, bool saveNow) where T : class
        {
            Context.Entry(item).State = EntityState.Modified;
            if (saveNow)
                Context.SaveChanges();
            return item;
        }

        public IQueryable<T> Select<T>() where T : class
        {
            IQueryable<T> query = Context.Set<T>();

            return query;

        }

        public int Save()
        {
            return Context.SaveChanges();
        }
    }
}
