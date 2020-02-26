using MCC_Neo.Core.Models;
using MCC_Neo.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace MCC_Neo.Core.Persistencia
{
    public class CidadeCursilhoRepository : GenericRepository<CidadeCursilho>, ICidadeCursilhoRepository
    {
        public CidadeCursilhoRepository(Data.NeoDbContext context) : base(context)
        {
        }

        public override IEnumerable<CidadeCursilho> Listar()
        {
            return table.Include(c => c.Cidade)
                .Include(c => c.Cursilho).ToList();
        }

        public override async IAsyncEnumerable<CidadeCursilho> ListarAsync()
        {
            await foreach (var result in table.Include(c => c.Cidade)
                .Include(c => c.Cursilho)
                .AsNoTracking().ToAsyncEnumerable())
            {
                yield return result;
            }
        }

        public IAsyncEnumerable<CidadeCursilho> ListarPorCursilho(int idCursilho)
        {
            var cidades = table.Include(c => c.Cidade)
                .Include(c => c.Cursilho).Where(x => x.CursilhoId == idCursilho)
                .OrderBy(x => x.Cidade.NomeCidade);

            return cidades.ToAsyncEnumerable();
        }
    }
}
