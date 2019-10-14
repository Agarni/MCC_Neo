using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Text;
using MCC_Neo.Core.Models;
using MCC_Neo.Core.Persistencia;
using MCC_Neo.Core.Repository;
using Microsoft.EntityFrameworkCore.Storage;

namespace MCC_Neo.Core.Data
{
    public class NeoUnitOfWork : INeoUnitOfWork
    {
        #region Variáveis globais
        private readonly NeoDbContext _context;
        private bool _disposed;
        private IDbContextTransaction _objTran;
        private string _errorMessage = string.Empty;
        #endregion Variáveis globais

        #region Propriedades
        public IGenericRepository<Cursilho> Cursilhos { get; private set; }

        public ICidadeRepository Cidades { get; private set; }

        public ICandidatoRepository Candidatos { get; private set; }

        public IResponsavelRepository Responsaveis { get; private set; }

        public IGenericRepository<Mensagem> Mensagens { get; private set; }

        public IGenericRepository<Funcao> Funcoes { get; private set; }

        public IGenericRepository<Parametrizacao> Parametros { get; private set; }
        public bool TransactionAtiva { get; private set; }
        public object Context { get { return _context; } }
        #endregion Propriedades

        public NeoUnitOfWork()
        {
            _context = new NeoDbContext();
            CriarRepositorios(_context);
        }

        public NeoUnitOfWork(NeoDbContext context)
        {
            _context = context;
            CriarRepositorios(_context);
        }

        private void CriarRepositorios(NeoDbContext context)
        {
            Cursilhos = new GenericRepository<Cursilho>(context);
            Mensagens = new GenericRepository<Mensagem>(context);
            Funcoes = new GenericRepository<Funcao>(context);
            Cidades = new CidadeRepository(context);
            Candidatos = new CandidatoRepository(context);
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _context.Dispose();
            _disposed = true;
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

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        _errorMessage += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                throw new Exception(_errorMessage, dbEx);
            }
        }
    }
}
