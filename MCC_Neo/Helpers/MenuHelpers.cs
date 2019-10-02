using MaterialDesignThemes.Wpf;
using MCC_Neo.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace MCC_Neo.Helpers
{
    public class MenuHelpers
    {
        private readonly ContentControl _containerTela;
        private readonly Brush _backgroundMenu;
        private readonly Tela tela = new Tela();
        public List<ItemMenu> Itens { get; private set; }

        public MenuHelpers(Brush backgroundMenu, ContentControl containerTela)
        {
            _containerTela = containerTela;
            _backgroundMenu = backgroundMenu;
        }

        public UserControlMenuItem NovoMenu(List<ItemMenu> itensMenu)
        {
            // Criando menu
            UserControlMenuItem menu = null;
            Itens = itensMenu;

            // Criando itens do menu
            if (itensMenu?.Count > 0)
            {
                menu = new UserControlMenuItem(this);
                menu.CarregarListaMenus();
            }

            return menu;
        }

        public void MenuItem_Click(object sender, SelectionChangedEventArgs e)
        {
            var item = (ItemMenu)((ListView)sender).SelectedItem;

            if (_containerTela == null || item?.Screen == null)
                return;

            tela.DataContext = item;
            _containerTela.Content = tela;
        }
    }
}
