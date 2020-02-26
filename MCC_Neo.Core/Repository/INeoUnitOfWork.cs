using MCC_Neo.Core.Models;
using MCC_Neo.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCC_Neo.Core
{
    public interface INeoUnitOfWork //: IDisposable
    {
        void CreateTransaction();
        bool TransactionAtiva { get; }
        void Commit();
        void Rollback();
        RetornoAcao Save();
        object Context { get; }

        // Repositórios
        IGenericRepository<Cursilho> Cursilhos { get; }
        ICidadeRepository Cidades { get; }
        ICandidatoRepository Candidatos { get; }
        IResponsavelRepository Responsaveis { get; }
        IGenericRepository<Mensagem> Mensagens { get; }
        IGenericRepository<Funcao> Funcoes { get; }
        IGenericRepository<Parametrizacao> Parametros { get; }
        ICidadeCursilhoRepository CidadesCursilho { get; }
    }
}
