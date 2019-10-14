using MCC_Neo.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCC_Neo.Core.Repository
{
    public interface ICidadeRepository : IGenericRepository<Cidade>
    {
        IAsyncEnumerable<Cidade> ListarPorCursilho(int idCursilho);
    }
}
