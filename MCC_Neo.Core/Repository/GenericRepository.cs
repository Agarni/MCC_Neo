using MCC_Neo.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCC_Neo.Core.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly NeoDbContext _context = null;
        private readonly DbSet<T> table = null;
        
        public GenericRepository()
        {
            this._context = new NeoDbContext();
            table = _context.Set<T>();
        }

        public GenericRepository(NeoDbContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }

        public IEnumerable<T> ListarTodos()
        {
            return table.ToList();
        }

        public T LocalizarPorId(object id)
        {
            return table.Find(id);
        }

        public RetornoAcao Insert(T obj)
        {
            var retorno = new RetornoAcao() { Sucesso = true };

            try
            {
                table.Add(obj);
            }
            catch (Exception ex)
            {
                retorno.Sucesso = false;
                retorno.MensagemRetorno = ex.Message;
                retorno.ExceptionRetorno = ex;
            }

            return retorno;
        }

        public RetornoAcao Update(T obj)
        {
            var retorno = new RetornoAcao { Sucesso = true };

            try
            {
                table.Attach(obj);
                _context.Entry(obj).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                retorno.Sucesso = false;
                retorno.MensagemRetorno = ex.Message;
                retorno.ExceptionRetorno = ex;
            }

            return retorno;
        }

        public RetornoAcao Delete(object id)
        {
            var retorno = new RetornoAcao { Sucesso = true };

            try
            {
                T existing = table.Find(id);
                table.Remove(existing);
            }
            catch (Exception ex)
            {
                retorno.Sucesso = false;
                retorno.MensagemRetorno = ex.Message;
                retorno.ExceptionRetorno = ex;
            }

            return retorno;
        }

        public async IAsyncEnumerable<T> ListarTodosAsync()
        {
            await foreach (var result in table.ToAsyncEnumerable())
            {
                yield return result;
            }
        }
    }
}
