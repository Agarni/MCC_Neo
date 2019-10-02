using MaterialDesignThemes.Wpf;
using MCC_Neo.ViewModels;
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
                new ItemMenu("Candidatos", PackIconKind.People, new UserControlProviders()),
                new ItemMenu("Impressões", PackIconKind.Printer, null),
                new ItemMenu("Cadastros", PackIconKind.Home, null)
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
    }
}

