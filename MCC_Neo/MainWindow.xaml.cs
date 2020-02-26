using MaterialDesignThemes.Wpf;
using MCC_Neo.ViewModels;
using MCC_Neo.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MCC_Neo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : WindowBase
    {
        public MainWindow()
        {
            InitializeComponent();

            var mnu = new List<ItemMenu>
            {
                new ItemMenu("Candidatos", PackIconKind.People, new CandidatosView()),
                new ItemMenu("Impressões", PackIconKind.Printer, new UserControlProviders()),
                new ItemMenu("Cadastros", PackIconKind.DesktopMacDashboard, new CadastrosView())
            };

            var menu = new Helpers.MenuHelpers(menuPrincipal.Background, StackPanelMain);
            menuPrincipal.Children.Clear();
            menuPrincipal.Children.Add(menu.NovoMenu(mnu));
        }

        internal void SwitchScreen(object sender)
        {
            var screen = ((UserControl)sender);

            if (screen != null)
            {
                StackPanelMain.Content = screen;
            }
        }

        private void BtnLogo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StackPanelMain.Content = stpLogo;
            if (menuPrincipal.Children.Count > 0)
            {
                ((UserControlMenuItem)menuPrincipal.Children[0]).Selecionar(-1);
            }
        }

        public void BloquearTela(bool bloquear)
        {
            StackPanelMain.IsHitTestVisible = !bloquear;
        }

        private void DialogHost_DialogOpened(object sender, DialogOpenedEventArgs eventArgs)
        {
            BloquearTela(true);
        }

        private void DialogHost_DialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            BloquearTela(false);
        }
    }
}