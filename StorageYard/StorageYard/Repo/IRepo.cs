using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageYard.Repo
{
    public interface IRepo : IDisposable
    {
        T Insert<T>(T item, bool saveNow)
            where T : class;

        T Update<T>(T item, bool saveNow)
            where T : class;

        T Delete<T>(T item, bool saveNow)
            where T : class;

        IQueryable<T> Select<T>()
            where T : class;

        int Save();
    }
}
