using MCC_Neo.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCC_Neo.Core
{
    public static class Servicos
    {
        private static IServiceProvider serviceProvider;
        public static IServiceProvider ServiceProvider
        {
            get
            {
                if (serviceProvider == null)
                    RegistrarServicos();

                return serviceProvider;
            }
        }

        public static void RegistrarServicos()
        {
            serviceProvider = new ServiceCollection()
                .AddDbContext<NeoDbContext>(options => options.UseInMemoryDatabase(databaseName: "MCC_CUR"))
                .AddSingleton<INeoUnitOfWork, NeoUnitOfWork>()
                .BuildServiceProvider();

            DataGenerator.Initilize(ServiceProvider);
        }
    }
}
