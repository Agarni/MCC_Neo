﻿using MCC_Neo.Core.Models;
using MCC_Neo.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCC_Neo.Core.Persistencia
{
    public class CandidatoRepository : ICandidatoRepository
    {
        private readonly Data.NeoDbContext _context;

        // provisório ---->
        List<Candidato> candidatosFake = new List<Candidato>
            {
                new Candidato
                {
                    Id = 1,
                    Nome = "Sergio Luiz",
                    NomeCracha = "Sergio",
                    DataNascimento = new DateTime(1973, 08, 20),
                    CandidatoCidade = new Cidade { CidadeId = 1 },
                    CursilhoId = 1,
                    SexoPessoa = Sexo.Masculino
                },
                new Candidato
                {
                    Id = 2,
                    Nome = "Luiz Carlos da Silva",
                    NomeCracha = "Luiz",
                    DataNascimento = new DateTime(1980, 01, 10),
                    CandidatoCidade = new Cidade { CidadeId = 2 },
                    CursilhoId = 1,
                    SexoPessoa = Sexo.Masculino
                },
                new Candidato
                {
                    Id = 3,
                    Nome = "Maria Helena de Castro",
                    NomeCracha = "Lena",
                    DataNascimento = new DateTime(1980, 01, 10),
                    CandidatoCidade = new Cidade { CidadeId = 3 },
                    CursilhoId = 1,
                    SexoPessoa = Sexo.Feminino
                }
            };

        public CandidatoRepository(Data.NeoDbContext context)
        {
            _context = context;
        }

        public RetornoAcao Delete(object id)
        {
            throw new NotImplementedException();
        }

        public RetornoAcao Insert(Candidato obj)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<Candidato> ListarPorCidade(int idCursilho)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<Candidato> ListarPorCursilho(int idCursilho)
        {
            return candidatosFake.Where(x => x.CursilhoId == idCursilho).ToAsyncEnumerable();
        }

        public IAsyncEnumerable<Candidato> ListarPorCursilhoCidade(int idCursilho, int idCidade)
        {
            return candidatosFake.Where(x => x.CursilhoId == idCursilho && 
                x.CandidatoCidade.CidadeId == idCidade).ToAsyncEnumerable();
        }

        public IEnumerable<Candidato> ListarTodos()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<Candidato> ListarTodosAsync()
        {
            throw new NotImplementedException();
        }

        public Candidato LocalizarPorId(object id)
        {
            throw new NotImplementedException();
        }

        public RetornoAcao Save()
        {
            throw new NotImplementedException();
        }

        public RetornoAcao Update(Candidato obj)
        {
            throw new NotImplementedException();
        }
    }
}