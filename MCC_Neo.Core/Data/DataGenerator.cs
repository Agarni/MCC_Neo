using MCC_Neo.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCC_Neo.Core.Data
{
    public class DataGenerator
    {
        public static void Initilize(IServiceProvider serviceProvider)
        {
            using (var ctx = new NeoDbContext(serviceProvider.GetRequiredService<DbContextOptions<NeoDbContext>>()))
            {
                var contextWork = serviceProvider.GetRequiredService<INeoUnitOfWork>();

                // Cria novas cidades quando ainda não foram cadastradas
                if (!contextWork.Cidades.Any())
                {
                    contextWork.Cidades.AddRange(new List<Cidade>{
                            new Cidade { NomeCidade = "Bebedouro", CidadeAtiva = 1 },
                            new Cidade { NomeCidade = "Jaboticabal", CidadeAtiva = 1 },
                            new Cidade { NomeCidade = "Monte Alto", CidadeAtiva = 1 },
                            new Cidade { NomeCidade = "Monte Azul Paulista", CidadeAtiva = 1 },
                            new Cidade { NomeCidade = "Pirangi", CidadeAtiva = 1 },
                            new Cidade { NomeCidade = "Taquaral", CidadeAtiva = 1 },
                            new Cidade { NomeCidade = "Taquaritinga", CidadeAtiva = 1 },
                            new Cidade { NomeCidade = "Viradouro", CidadeAtiva = 1 },
                            new Cidade { NomeCidade = "Vista Alegre do Alto", CidadeAtiva = 1 }
                    });

                    contextWork.Save();
                }

                // Cria o cadastro do novo cursilho caso ainda não exista
                if (!contextWork.Cursilhos.Any())
                {
                    contextWork.Cursilhos.Insert(new Cursilho
                    {
                        DescricaoCursilho = "Cursilho de Cristandade",
                        DataInicioCUR = DateTime.Now.AddDays(15),
                        DataFimCUR = DateTime.Now.AddDays(17),
                        TemaCursilho = "A nossa sorte está em fazer parte da família de Deus"
                    });

                    contextWork.Save();
                    var cursilho = contextWork.Cursilhos.Listar().FirstOrDefault();
                    App.IdCursilho = cursilho.CursilhoId;
                    App.DescricaoCursilho = cursilho.DescricaoCursilho;

                    // Associa as atuais cidades com cursilho criado
                    foreach (var cidade in contextWork.Cidades.Listar())
                    {
                        contextWork.CidadesCursilho.Insert(new CidadeCursilho
                        {
                            CursilhoId = cursilho.CursilhoId,
                            CidadeId = cidade.CidadeId
                        });
                    }

                    contextWork.Save();
                }
                else
                {
                    var cursilho = contextWork.Cursilhos.Listar().FirstOrDefault();
                    App.IdCursilho = cursilho.CursilhoId;
                    App.DescricaoCursilho = cursilho.DescricaoCursilho;
                }

                // Cria cadastro de candidatos quando não houver
                if (!contextWork.Candidatos.Any())
                {
                    var cidades = contextWork.CidadesCursilho.ListarPorCursilho(App.IdCursilho)
                        .ToListAsync().Result;

                    contextWork.Candidatos.AddRange(new List<Candidato>
                        {
                            new Candidato
                            {
                                Nome = "Simão",
                                NomeCracha = "Pedro",
                                DataNascimento = new DateTime(1973, 08, 20),
                                CandidatoCidade = cidades[0].Cidade,
                                CursilhoId = App.IdCursilho,
                                SexoPessoa = Sexo.Masculino,
                                Escolaridade = NivelEscolaridade.Superior,
                                EstadoCivil = EstadoCivil.Casado,
                                NomePadrinho = "Jesus Cristo",
                                PadrinhoCidade = "Belém"
                            },
                            new Candidato
                            {
                                Nome = "André",
                                NomeCracha = "André",
                                DataNascimento = new DateTime(1980, 01, 10),
                                CandidatoCidade = cidades[Math.Min(1, cidades.Count -1)].Cidade,
                                CursilhoId = App.IdCursilho,
                                SexoPessoa = Sexo.Masculino,
                                Escolaridade = NivelEscolaridade.Medio,
                                EstadoCivil = EstadoCivil.Solteiro,
                                NomePadrinho = "Jesus Cristo",
                                PadrinhoCidade = "Belém"
                            },
                            new Candidato
                            {
                                Nome = "Tiago de Zebedeu",
                                NomeCracha = "Tiago",
                                DataNascimento = new DateTime(1980, 01, 10),
                                CandidatoCidade = cidades[Math.Min(2, cidades.Count -1)].Cidade,
                                CursilhoId = App.IdCursilho,
                                SexoPessoa = Sexo.Masculino,
                                Escolaridade = NivelEscolaridade.PosGraduado,
                                EstadoCivil = EstadoCivil.Casado,
                                NomePadrinho = "Jesus Cristo",
                                PadrinhoCidade = "Belém"
                            },
                            new Candidato
                            {
                                Nome = "Maria Madalena",
                                NomeCracha = "Maria",
                                DataNascimento = new DateTime(1980, 01, 10),
                                CandidatoCidade = cidades[Math.Min(3, cidades.Count -1)].Cidade,
                                CursilhoId = App.IdCursilho,
                                SexoPessoa = Sexo.Feminino,
                                Escolaridade = NivelEscolaridade.PosGraduado,
                                EstadoCivil = EstadoCivil.Casado,
                                NomePadrinho = "Jesus Cristo",
                                PadrinhoCidade = "Belém"
                            }
                        });

                    contextWork.Save();
                }

                // Cria um responsável quando não houver na base
                if (!contextWork.Responsaveis.Any())
                {
                    contextWork.Responsaveis.Insert(new Responsavel
                    {
                        Nome = "Jesus de Nazaré",
                        NomeCracha = "Jesus",
                        Profissao = "Carpinteiro",
                        CursilhoId = App.IdCursilho,
                        Equipe = Equipe.Vanguarda,
                        CidadeId = 1 //,
                                     //Grupo = contextWork.Grupos.FirstOrDefault(),
                                     //Mensagem = contextWork.Mensagens.FirstOrDefault(x => x.NomeMensagem.Contains("Esperança"))
                    });

                    contextWork.Save();
                }
            }
        }
    }
}
