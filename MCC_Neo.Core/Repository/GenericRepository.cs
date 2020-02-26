using MCC_Neo.Core.Data;
using MCC_Neo.Core.Helpers;
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
        protected readonly DbSet<T> table = null;
        
        //public GenericRepository()
        //{
        //    this._context = new NeoDbContext();
        //    table = _context.Set<T>();
        //}

        public GenericRepository(NeoDbContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }

        public virtual IEnumerable<T> Listar()
        {
            return table.ToList();
        }

        public virtual IEnumerable<T> Listar(Func<T, bool> where)
        {
            return table.AsEnumerable().Where(where);
        }

        public virtual IAsyncEnumerable<T> ListarAsync(Func<T, bool> where)
        {
            return table.AsEnumerable().Where(where).ToAsyncEnumerable();
        }

        public virtual async IAsyncEnumerable<T> ListarAsync()
        {
            await foreach (var result in table.AsNoTracking().ToAsyncEnumerable())
            {
                yield return result;
            }
        }

        public virtual T LocalizarPorId(object id)
        {
            return table.Find(id);
        }

        public RetornoAcao Insert(T obj)
        {
            var retorno = new RetornoAcao() { Sucesso = true };

            try
            {
                var errosModel = ValidarModel.GetErrors(obj);

                if (errosModel?.Count() > 0)
                {
                    var erros = new StringBuilder();
                    foreach (var erroEncontrado in errosModel)
                    {
                        erros.AppendLine(erroEncontrado.MensagemErro);
                    }

                    retorno.Sucesso = false;
                    retorno.MensagemRetorno = erros.ToString();

                    return retorno;
                }

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

        public bool Any()
        {
            return table.Any();
        }

        public void AddRange(IEnumerable<T> lista)
        {
            table.AddRange(lista);
        }

        public virtual T FirstOrDefault(Func<T, bool> condicao)
        {
            return table.FirstOrDefault(condicao);
        }
    }
}
