using MCC_Neo.Helpers;
using MCC_Neo.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace MCC_Neo
{
    /// <summary>
    /// Interaction logic for UserControlMenuItem.xaml
    /// </summary>
    public partial class UserControlMenuItem : UserControl, INotifyPropertyChanged
    {
        //private List<ItemMenu> _subItens = new List<ItemMenu>();
        //public List<ItemMenu> SubItens
        //{
        //    get
        //    {
        //        return _subItens;
        //    }
        //    set
        //    {
        //        if (value == _subItens) return;

        //        _subItens = value;
        //        //OnPropertyRaised("SubItens");
        //    }
        //}

        public UserControlMenuItem(MenuHelpers context)
        {
            InitializeComponent();

            DataContext = context;
            ListViewMenu.SelectionChanged += context.MenuItem_Click;
        }

        public void CarregarListaMenus()
        {
            ListViewMenu.ItemsSource = ((MenuHelpers)DataContext).Itens;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        internal void Selecionar(int index)
        {
            ListViewMenu.SelectedIndex = index;
        }
    }
}
