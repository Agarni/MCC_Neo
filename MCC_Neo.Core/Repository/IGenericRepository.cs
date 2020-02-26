using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCC_Neo.Core.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IAsyncEnumerable<T> ListarAsync();
        IEnumerable<T> Listar();
        IAsyncEnumerable<T> ListarAsync(Func<T, bool> where);
        IEnumerable<T> Listar(Func<T, bool> where);
        T LocalizarPorId(object id);
        RetornoAcao Insert(T obj);
        RetornoAcao Update(T obj);
        RetornoAcao Delete(object id);
        bool Any();
        void AddRange(IEnumerable<T> lista);
        T FirstOrDefault(Func<T, bool> condicao);
    }
}
