using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
//using System.Data.Entity.Validation;
using System.Text;
using MCC_Neo.Core.Models;
using MCC_Neo.Core.Persistencia;
using MCC_Neo.Core.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace MCC_Neo.Core.Data
{
    public class NeoUnitOfWork : INeoUnitOfWork
    {
        #region Variáveis globais
        private readonly NeoDbContext _context;
        private IDbContextTransaction _objTran;
        private StringBuilder _errorMessage = new StringBuilder();
        #endregion Variáveis globais

        #region Propriedades
        public IGenericRepository<Cursilho> Cursilhos { get; private set; }
        public ICidadeRepository Cidades { get; private set; }
        public ICandidatoRepository Candidatos { get; private set; }
        public IResponsavelRepository Responsaveis { get; private set; }
        public IGenericRepository<Mensagem> Mensagens { get; private set; }
        public IGenericRepository<Funcao> Funcoes { get; private set; }
        public ICidadeCursilhoRepository CidadesCursilho { get; private set; }

        public IGenericRepository<Parametrizacao> Parametros { get; private set; }
        public bool TransactionAtiva { get; private set; }
        public object Context { get { return _context; } }
        #endregion Propriedades

        public NeoUnitOfWork(IServiceProvider serviceProvider)
        {
            _context = new NeoDbContext(serviceProvider
                .GetRequiredService<DbContextOptions<NeoDbContext>>());

            CriarRepositorios(_context);
        }

        private void CriarRepositorios(NeoDbContext context)
        {
            Cursilhos = new GenericRepository<Cursilho>(context);
            Mensagens = new GenericRepository<Mensagem>(context);
            Funcoes = new GenericRepository<Funcao>(context);
            Cidades = new CidadeRepository(context);
            Candidatos = new CandidatoRepository(context);
            CidadesCursilho = new CidadeCursilhoRepository(context);
            Responsaveis = new ResponsavelRepository(context);
        }

        public void Commit()
        {
            if (TransactionAtiva && _objTran != null)
            {
                _objTran.Commit();
            }

            TransactionAtiva = false;
        }

        public void CreateTransaction()
        {
            if (!TransactionAtiva)
            {
                _objTran = _context.Database.BeginTransaction();
                TransactionAtiva = true;
            }
        }

        public void Rollback()
        {
            if (TransactionAtiva && _objTran != null)
            {
                _objTran.Rollback();
                _objTran.Dispose();
            }

            TransactionAtiva = false;
        }

        public RetornoAcao Save()
        {
            var retorno = new RetornoAcao();

            try
            {
                _context.SaveChanges();
                retorno.Sucesso = true;
            }
            catch (DbEntityValidationException dbEx)
            {
                _errorMessage.Clear();

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        _errorMessage.AppendLine(string.Format("Property: {0} Error: {1}", 
                            validationError.PropertyName, validationError.ErrorMessage));
                    }
                }

                retorno.Sucesso = false;
                retorno.MensagemRetorno = _errorMessage.ToString();
                retorno.ExceptionRetorno = dbEx;
            }

            return retorno;
        }
    }
}
