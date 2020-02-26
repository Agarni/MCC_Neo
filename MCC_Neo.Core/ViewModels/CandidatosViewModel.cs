using MCC_Neo.Core.Models;
using MvvmCross.ViewModels;
using MvvmCross.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using Utileco;
using MCC_Neo.Core.Repository;
using MCC_Neo.Core.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace MCC_Neo.Core.ViewModels
{
    public class CandidatosViewModel : MvxViewModel
    {
        #region Propriedades
        public Candidato CandidatoOriginal = new Candidato();
        #endregion Propriedades

        #region Propriedades de binding
        private bool _IsCandidatoOpen;
        public bool IsCandidatoOpen
        {
            get => _IsCandidatoOpen;
            set
            {
                if (value == _IsCandidatoOpen)
                    return;

                _IsCandidatoOpen = value;
                RaisePropertyChanged(() => IsCandidatoOpen);
            }
        }
        private string _nomeBusca;
        public string NomeBusca
        {
            get => _nomeBusca;
            set
            {
                if (_nomeBusca == value)
                    return;

                _nomeBusca = value;
                RaisePropertyChanged(() => NomeBusca);

                if (_nomeBusca.Trim() == "")
                {
                    ListaCandidatos = new BindingList<Candidato>(candidatosConsulta);
                }
                else
                {
                    ListaCandidatos = new BindingList<Candidato>(candidatosConsulta.Where(
                        x => x.Nome.ToUpper().RemoveAccents()
                        .Contains(_nomeBusca.ToUpper().RemoveAccents())).ToList());
                }
            }
        }
        private BindingList<Cidade> _listaCidades;
        public BindingList<Cidade> ListaCidades
        {
            get => _listaCidades;
            set
            {
                if (_listaCidades == value)
                    return;

                _listaCidades = value;
                RaisePropertyChanged(() => ListaCidades);

                ListaCidadesSelecionaveis = new BindingList<Cidade>(ListaCidades
                    .Where(x => x.CidadeId > 0 && x.CidadeAtiva == 1).ToList());
            }
        }
        private BindingList<Cidade> _ListaCidadesSelecionaveis;
        public BindingList<Cidade> ListaCidadesSelecionaveis
        {
            get => _ListaCidadesSelecionaveis;
            set
            {
                if (value == _ListaCidadesSelecionaveis)
                    return;

                _ListaCidadesSelecionaveis = value;
                RaisePropertyChanged(() => ListaCidadesSelecionaveis);
            }
        }
        private Cidade _cidadeSelecionada;
        public Cidade CidadeSelecionada
        {
            get => _cidadeSelecionada;
            set
            {
                if (_cidadeSelecionada == value)
                    return;

                _cidadeSelecionada = value;
                RaisePropertyChanged(() => CidadeSelecionada);

                VerCidadeGrid = (value.CidadeId == 0 ? Visibility.Visible : Visibility.Hidden);

                // Carregando candidatos
                CarregarCandidatos(_cidadeSelecionada.CidadeId);
            }
        }
        private int _OrdemCidadePadrinho = -1;
        public int OrdemCidadePadrinho
        {
            get => _OrdemCidadePadrinho;
            set
            {
                if (value == _OrdemCidadePadrinho)
                    return;

                _OrdemCidadePadrinho = value;
                RaisePropertyChanged(() => OrdemCidadePadrinho);
            }
        }
        private BindingList<Candidato> _listaCandidatos;
        public BindingList<Candidato> ListaCandidatos
        {
            get => _listaCandidatos;
            set
            {
                if (_listaCandidatos == value)
                    return;

                _listaCandidatos = value;
                RaisePropertyChanged(() => ListaCandidatos);
                CalculaTotais();
            }
        }
        private int _totalFeminino;
        public int TotalFeminino
        {
            get => _totalFeminino;
            set
            {
                if (_totalFeminino != value)
                {
                    _totalFeminino = value;
                    RaisePropertyChanged(() => TotalFeminino);
                }
            }
        }
        private int _totalMasculino;
        public int TotalMasculino
        {
            get => _totalMasculino;
            set
            {
                if (_totalMasculino != value)
                {
                    _totalMasculino = value;
                    RaisePropertyChanged(() => TotalMasculino);
                }
            }
        }
        private Candidato _candidatoSelecionado;
        public Candidato CandidatoSelecionado
        {
            get => _candidatoSelecionado;
            set
            {
                SetProperty(ref _candidatoSelecionado, value);
                PermiteExcluir = (value?.Id > 0);
            }
        }
        private bool _permiteIncluir = false;
        public bool PermiteIncluir
        {
            get => _permiteIncluir;
            set
            {
                if (value == _permiteIncluir)
                    return;

                _permiteIncluir = value;
                RaisePropertyChanged(() => PermiteIncluir);
            }
        }
        private bool _permiteExcluir = false;
        public bool PermiteExcluir
        {
            get => _permiteExcluir;
            set
            {
                SetProperty(ref _permiteExcluir, value, nameof(PermiteExcluir));
            }
        }
        private Cidade _padrinhoCidade;
        public Cidade PadrinhoCidadeSelecionado
        {
            get => _padrinhoCidade;
            set
            {
                if (value == _padrinhoCidade)
                    return;

                _padrinhoCidade = value;
                RaisePropertyChanged(() => PadrinhoCidadeSelecionado);
            }
        }
        private Visibility _verCidadeGrid = Visibility.Collapsed;
        public Visibility VerCidadeGrid
        {
            get => _verCidadeGrid;
            set
            {
                if (_verCidadeGrid != value)
                {
                    _verCidadeGrid = value;

                    RaisePropertyChanged(() => VerCidadeGrid);
                }
            }
        }
        private BindingList<Grupo> _ListaGruposSelecionaveis;
        public BindingList<Grupo> ListaGruposSelecionaveis
        {
            get => _ListaGruposSelecionaveis;
            set
            {
                if (_ListaGruposSelecionaveis != value)
                {
                    _ListaGruposSelecionaveis = value;

                    RaisePropertyChanged(() => ListaGruposSelecionaveis);
                }
            }
        }

        public IMvxCommand CmdExcluirCandidato => new MvxCommand(ExcluirCandidato);
        #endregion Propriedades de binding

        #region Variáveis globais
        private List<Candidato> _candidatos = new List<Candidato>();
        private List<Candidato> candidatosConsulta;
        private readonly INeoUnitOfWork context;
        #endregion Variáveis globais

        public CandidatosViewModel(IServiceProvider context)
        {
            IsCandidatoOpen = false;
            this.context = context.GetRequiredService<INeoUnitOfWork>();

            //CmdExcluirCandidato = new MvxCommand(ExcluirCandidato, () => CanExcluirCandidato());

            CarregarCidades();
        }

        #region Métodos de consulta
        public async void CarregarCidades()
        {
            ListaCidades = new BindingList<Cidade>()
                {
                    new Cidade { CidadeId = 0, NomeCidade = "## Todas cidades ##" }
                };

            await foreach (var item in context.CidadesCursilho.ListarPorCursilho(App.IdCursilho))
            {
                ListaCidades.Add(item.Cidade);
            }

            CidadeSelecionada = ListaCidades.FirstOrDefault();
        }

        public async void CarregarCandidatos(int idCidade = 0)
        {
            //using (var DbContext = new NeoUnitOfWork(Servicos.ServiceProvider))
            //{
            _candidatos = new List<Candidato>();

            await foreach (Candidato item in (idCidade == 0 ?
                context.Candidatos.ListarPorCursilho(App.IdCursilho) :
                context.Candidatos.ListarPorCursilhoCidade(App.IdCursilho, idCidade)))
            {
                _candidatos.Add(item);
            }

            if (_candidatos?.Count > 0)
            {
                candidatosConsulta = _candidatos.OrderBy(x => x.Nome).ToList();
                ListaCandidatos = new BindingList<Candidato>(candidatosConsulta);
                CandidatoSelecionado = ListaCandidatos.FirstOrDefault();
            }
            else
            {
                ListaCandidatos = new BindingList<Candidato>();
                candidatosConsulta = new List<Candidato>();
                CandidatoSelecionado = null;
            }

            NomeBusca = "";
            PermiteIncluir = CanNovoCandidato();

            CarregarGrupos();
        }

        public void CarregarGrupos()
        {
            // ajustar
            //using (var ctx = new DBCursilho())
            //{
            //    ListaGruposSelecionaveis = new BindingList<Grupo>(ctx.Grupos
            //        .Where(g => g.Cursilho.CursilhoId == App.Parametros.CursilhoId)
            //        .OrderBy(g => g.Nome).ToList());
            //}
        }
        #endregion Métodos de consulta

        #region Manutenção de candidatos
        public bool CanNovoCandidato()
        {
            return (ListaCidades.Count > 0 && CidadeSelecionada?.CidadeId > 0);
        }

        public void NovoCandidato()
        {
            // Ajustar
            //PadrinhoCidadeSelecionado = ListaCidadesSelecionaveis
            //    .FirstOrDefault(p => p.CidadeId == CidadeSelecionada.CidadeId);
            //CandidatoSelecionado = new Candidato()
            //{
            //    CursilhoId = App.Parametros.CursilhoId,
            //    CandidatoCidade = CidadeSelecionada,
            //    PadrinhoCidade = PadrinhoCidadeSelecionado,
            //    Grupo = ListaGruposSelecionaveis.FirstOrDefault(g => g.Nome.Contains("#"))
            //};
        }

        public RetornoAcao AtualizarCandidato()
        {
            RetornoAcao retorno;
            var novo = (CandidatoSelecionado.Id == 0);
            CandidatoSelecionado.CursilhoId = App.IdCursilho;
            CandidatoSelecionado.CandidatoCidade = CidadeSelecionada;

            // Nome no crachá
            CandidatoSelecionado.Nome = CandidatoSelecionado.Nome?.Trim();
            if (string.IsNullOrWhiteSpace(CandidatoSelecionado.NomeCracha) && !string.IsNullOrWhiteSpace(CandidatoSelecionado.Nome))
            {
                CandidatoSelecionado.NomeCracha = CandidatoSelecionado.Nome.Trim().Contains(" ") ? CandidatoSelecionado.Nome
                    .Substring(0, CandidatoSelecionado.Nome.IndexOf(" ")) : CandidatoSelecionado.Nome.Trim();
            }
            
            retorno = (novo ? context.Candidatos.Insert(CandidatoSelecionado) : context.Candidatos.Update(CandidatoSelecionado));
            
            if (retorno.Sucesso)
            {
                retorno = context.Save();
            }

            return retorno;
        }

        private void CalculaTotais()
        {
            TotalFeminino = ListaCandidatos.Count(x => x.SexoPessoa == Sexo.Feminino);
            TotalMasculino = ListaCandidatos.Count(x => x.SexoPessoa == Sexo.Masculino);
        }

        public bool CanExcluirCandidato()
        {
            return (CandidatoSelecionado != null && CandidatoSelecionado.Id != 0);
        }

        public void ExcluirCandidato()
        {
            //MainWindow wPrincipal = (MainWindow)Application.Current.MainWindow;

            //var result = MsgBox.Show($"Tem certeza que deseja excluir \"{CandidatoSelecionado.Nome}\"?", "Excluir candidato (a)",
            //    Buttons.YesNo, Icons.Question, AnimateStyle.ZoomIn, wPrincipal);

            //if (result != System.Windows.Forms.DialogResult.Yes)
            //    return;

            //try
            //{
            //    using (var ctx = new DBCursilho())
            //    {
            //        ctx.Entry(CandidatoSelecionado).State = EntityState.Deleted;
            //        ctx.SaveChanges();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MsgBox.Show("Erro ao excluir Candidato! Verifique: " + Environment.NewLine +
            //        ex.Message, "Exclusão de candidato", Buttons.OK, Icons.Error);
            //}

            //CarregarCandidatos(CidadeSelecionada.CidadeId);
        }
        #endregion Atualizações de candidatos
    }
}
