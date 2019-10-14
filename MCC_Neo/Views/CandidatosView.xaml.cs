using MCC_Neo.Core.ViewModels;
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

namespace MCC_Neo.Views
{
    /// <summary>
    /// Interaction logic for CandidatosView.xaml
    /// </summary>
    public partial class CandidatosView : UserControl
    {
        public CandidatosView()
        {
            InitializeComponent();

            this.DataContext = new CandidatosViewModel();
        }

        private async void BtnCandidato_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
