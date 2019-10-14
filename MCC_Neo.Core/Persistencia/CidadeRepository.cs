using MCC_Neo.Core.Models;
using MCC_Neo.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCC_Neo.Core.Persistencia
{
    public class CidadeRepository : ICidadeRepository
    {
        private readonly Data.NeoDbContext _context;

        private List<Cidade> cidadesFake = new List<Cidade>()
            {
                new Cidade()
                {
                    CidadeId = 0, NomeCidade = "Todas cidades"
                },
                new Cidade
                {
                    CidadeId = 1, NomeCidade = "Jaboticabal"
                },
                new Cidade
                {
                    CidadeId = 2, NomeCidade = "Monte Alto"
                },
                new Cidade
                {
                    CidadeId = 3, NomeCidade = "Taquaral"
                },
                new Cidade
                {
                    CidadeId = 4, NomeCidade = "Santa Rita do Passa Quatro"
                },
                new Cidade
                {
                    CidadeId = 5, NomeCidade = "Vista Alegre do Alto"
                }
            };

        public CidadeRepository(Data.NeoDbContext context)
        {
            _context = context;
        }

        public RetornoAcao Delete(object id)
        {
            throw new NotImplementedException();
        }

        public RetornoAcao Insert(Cidade obj)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<Cidade> ListarPorCursilho(int idCursilho)
        {
            return cidadesFake.ToAsyncEnumerable();
        }

        public IEnumerable<Cidade> ListarTodos()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<Cidade> ListarTodosAsync()
        {
            return cidadesFake.ToAsyncEnumerable();
        }

        public Cidade LocalizarPorId(object id)
        {
            throw new NotImplementedException();
        }

        public RetornoAcao Save()
        {
            throw new NotImplementedException();
        }

        public RetornoAcao Update(Cidade obj)
        {
            throw new NotImplementedException();
        }
    }
}
