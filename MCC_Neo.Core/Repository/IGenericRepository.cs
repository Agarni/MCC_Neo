using System;
using System.Collections.Generic;
using System.Text;

namespace MCC_Neo.Core.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IAsyncEnumerable<T> ListarTodosAsync();
        IEnumerable<T> ListarTodos();
        T LocalizarPorId(object id);
        RetornoAcao Insert(T obj);
        RetornoAcao Update(T obj);
        RetornoAcao Delete(object id);
    }
}
