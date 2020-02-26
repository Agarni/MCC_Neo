using MaterialDesignThemes.Wpf;
using MCC_Neo.Core;
using MCC_Neo.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MCC_Neo.Views
{
    /// <summary>
    /// Interaction logic for CandidatosView.xaml
    /// </summary>
    public partial class CandidatosView : UserControl
    {
        private CadastroCandidatoView candidatoView;
        private readonly CandidatosViewModel candidatosViewModel;

        public CandidatosView()
        {
            InitializeComponent();

            this.DataContext = new CandidatosViewModel(Servicos.ServiceProvider);
            candidatosViewModel = (this.DataContext as CandidatosViewModel);
        }

        private async void BtnCandidato_Click(object sender, RoutedEventArgs e)
        {
            if (candidatoView == null)
            {
                candidatoView = new CadastroCandidatoView();
            }

            var idCandidato = candidatosViewModel.CandidatoSelecionado?.Id ?? 0;

            if ((sender as Button).Name.Equals("btnNovoCandidato"))
            {
                candidatosViewModel.CandidatoSelecionado = new Core.Models.Candidato
                {
                    CandidatoCidadeId = candidatosViewModel.CidadeSelecionada.CidadeId
                };
            }
            else
            {
                if (idCandidato > 0)
                {
                    candidatosViewModel.CandidatoSelecionado = candidatosViewModel.ListaCandidatos
                        .FirstOrDefault(x => x.Id.Equals(idCandidato));
                }
            }

            candidatoView.DataContext = candidatosViewModel.CandidatoSelecionado;
            var tela = new Dialogs.TelaCadastro
            {
                TituloTela = "Cadastro de candidato",
                ConteudoTela = candidatoView
            };

            var t = await DialogHost.Show(tela, "MainDialogHost",
                async (object sender, DialogClosingEventArgs e) =>
                {
                    void carregarCandidatos()
                    {
                        candidatosViewModel.CarregarCandidatos(candidatosViewModel.CidadeSelecionada.CidadeId);
                        candidatosViewModel.CandidatoSelecionado = candidatosViewModel.ListaCandidatos
                            .FirstOrDefault(x => x.Id.Equals(idCandidato));
                    }

                    if ((Dialogs.DialogResult)e.Parameter == Dialogs.DialogResult.Ok)
                    {

                        // Atualizar candidato
                        var novo = (candidatosViewModel.CandidatoSelecionado?.Id ?? 0) == 0;
                        var atualizacao = candidatosViewModel.AtualizarCandidato();
                        if (!atualizacao.Sucesso)
                        {
                            e.Cancel();

                            tela.CadastroHabilitado = false;

                            await DialogHost.Show(new Dialogs.ShowDialogMessage
                            {
                                Mensagem = "Erro ao atualizar!" + Environment.NewLine + Environment.NewLine +
                                    atualizacao.MensagemRetorno,
                                MessageBoxButton = Dialogs.ShowDialogButton.OK,
                                MessageBoxImage = Dialogs.ShowDialogImage.Error
                            }, "ConteudoDialogHost");

                            tela.CadastroHabilitado = true;
                        }
                        else
                        {
                            if (!e.Session.IsEnded)
                                e.Session.Close(Dialogs.DialogResult.Cancela);

                            if (novo)
                                carregarCandidatos();
                        }
                    }
                    else
                    {
                        carregarCandidatos();
                    }
                });
        }
    }
}
