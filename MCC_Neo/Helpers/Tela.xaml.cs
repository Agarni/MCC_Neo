using System;
using System.Collections.Generic;
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

namespace MCC_Neo.Helpers
{
    /// <summary>
    /// Interaction logic for Tela.xaml
    /// </summary>
    public partial class Tela : UserControl
    {
        public Tela()
        {
            InitializeComponent();
        }

        //private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    if (this.DataContext != null && this.DataContext is MenuItem)
        //    {
        //        var _context = (this.DataContext as MenuItem);

        //        pnlConteudo.Children.Clear();
        //        pnlConteudo.Children.Add(_context.Tag as UserControl);
        //    }
        //}
    }
}
