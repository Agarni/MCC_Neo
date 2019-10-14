using System;
using System.Collections.Generic;
using System.Text;
using MCC_Neo.Core.Models;

namespace MCC_Neo.Core.Repository
{
    public interface IResponsavelRepository : IGenericRepository<Responsavel>
    {
        IAsyncEnumerable<Responsavel> ListarPorCidade(int idCursilho);
    }
}
