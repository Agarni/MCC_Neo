using MCC_Neo.Core.Models;
using MCC_Neo.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace MCC_Neo.Core.Persistencia
{
    public class CidadeRepository : GenericRepository<Cidade>, ICidadeRepository
    {
        //private List<Cidade> cidadesFake = new List<Cidade>()
        //    {
        //        new Cidade()
        //        {
        //            CidadeId = 0, NomeCidade = "Todas cidades"
        //        },
        //        new Cidade
        //        {
        //            CidadeId = 1, NomeCidade = "Jaboticabal"
        //        },
        //        new Cidade
        //        {
        //            CidadeId = 2, NomeCidade = "Monte Alto"
        //        },
        //        new Cidade
        //        {
        //            CidadeId = 3, NomeCidade = "Taquaral"
        //        },
        //        new Cidade
        //        {
        //            CidadeId = 4, NomeCidade = "Santa Rita do Passa Quatro"
        //        },
        //        new Cidade
        //        {
        //            CidadeId = 5, NomeCidade = "Vista Alegre do Alto"
        //        }
        //    };

        private IGenericRepository<CidadeCursilho> cidadesCursilho;

        //public CidadeRepository() : base()
        //{
        //}

        public CidadeRepository(Data.NeoDbContext context) : base(context)
        {
        }
    }
}
