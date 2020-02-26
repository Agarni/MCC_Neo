using MvvmCross.Platforms.Wpf.Core;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MCC_Neo.Core.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace MCC_Neo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : MvxApplication
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            this.RegisterSetupType<MvxWpfSetup<Core.App>>();
            Core.Servicos.RegistrarServicos();

            base.OnStartup(e);
        }
    }
}
