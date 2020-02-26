using MCC_Neo.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCC_Neo.Core.Repository
{
    public interface ICidadeCursilhoRepository : IGenericRepository<CidadeCursilho>
    {
        IAsyncEnumerable<CidadeCursilho> ListarPorCursilho(int idCursilho);
    }
}
