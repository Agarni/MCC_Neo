using MCC_Neo.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCC_Neo.Core.Repository
{
    public interface ICandidatoRepository : IGenericRepository<Candidato>
    {
        IAsyncEnumerable<Candidato> ListarPorCidade(int idCidade);
        IAsyncEnumerable<Candidato> ListarPorCursilho(int idCursilho);
        IAsyncEnumerable<Candidato> ListarPorCursilhoCidade(int idCursilho, int idCidade);
    }
}
