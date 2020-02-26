using MCC_Neo.Core.Models;
using MCC_Neo.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace MCC_Neo.Core.Persistencia
{
    public class CandidatoRepository : GenericRepository<Candidato>, ICandidatoRepository
    {
        public CandidatoRepository(Data.NeoDbContext context) : base(context)
        {
        }

        public IAsyncEnumerable<Candidato> ListarPorCidade(int idCidade)
        {
            return ListarPorExpressao(x => x.CandidatoCidadeId == idCidade).ToAsyncEnumerable();
        }

        public IAsyncEnumerable<Candidato> ListarPorCursilho(int idCursilho)
        {
            return ListarPorExpressao(x => x.CursilhoId == idCursilho).ToAsyncEnumerable();
        }

        public IEnumerable<Candidato> ListarPorExpressao(Func<Candidato, bool> expressao)
        {
            return table.Include(c => c.CandidatoCidade)
                .Include(c => c.Grupo)
                .Where(expressao);
        }

        public IAsyncEnumerable<Candidato> ListarPorCursilhoCidade(int idCursilho, int idCidade)
        {
            return ListarPorExpressao(x => x.CursilhoId == idCursilho && 
                x.CandidatoCidade.CidadeId == idCidade).ToAsyncEnumerable();
        }
    }
}
