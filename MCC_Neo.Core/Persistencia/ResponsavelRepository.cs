using MCC_Neo.Core.Models;
using MCC_Neo.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCC_Neo.Core.Persistencia
{
    internal class ResponsavelRepository : GenericRepository<Responsavel>, IResponsavelRepository
    {
        public ResponsavelRepository(Data.NeoDbContext context) : base(context)
        {
        }

        public override IEnumerable<Responsavel> Listar()
        {
            return base.Listar();
        }

        public IAsyncEnumerable<Responsavel> ListarPorCidade(int idCidade)
        {
            return table.AsEnumerable().Where(r => r.CidadeId == idCidade).ToAsyncEnumerable();
        }

        public IAsyncEnumerable<Responsavel> ListarPorCursilho(int idCursilho)
        {
            return table.AsEnumerable().Where(r => r.CursilhoId == idCursilho).ToAsyncEnumerable();
        }
    }
}
