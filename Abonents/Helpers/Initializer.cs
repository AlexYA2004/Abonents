using DataBaseLogic;
using DataBaseLogic.Entities;
using DataBaseLogic.Repositories.ConcrateRepositories;
using DataBaseLogic.Repositories.Interfaces;
using DataBaseLogic.Services.Implementations;
using DataBaseLogic.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abonents.Helpers
{

    public static class Initializer
    {
        public static IServiceProvider InitializeApp(this IServiceCollection services, string DbConnection)
        {
            services.AddScoped(o => new BaseOptions() { ConnectionString = DbConnection });

            services.AddScoped<IDataService, DataService>();

            services.AddScoped<IBaseRepository<Abonent>, AbonentRepository>();

            services.AddScoped<IBaseRepository<PhoneNumber>, PhoneNubmerRepository>();

            services.AddScoped<IBaseRepository<Address>, AddressRepository>();

            services.AddScoped<IBaseRepository<Street>, StreetRepository>();

            services.AddScoped<MainWindow>();

            return services.BuildServiceProvider();
        }


    }
}
