using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PunsApi;
using PunsApi.Controllers;
using PunsApi.Data;
using PunsApi.Services;
using PunsApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PUNS.TESTS
{
    public class DependencySetupFixture
    {
        public ServiceProvider ServiceProvider { get; private set; }
        public DependencySetupFixture()
        {
            var serviceCollection = new ServiceCollection();
            // serviceCollection.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(databaseName: "TestDatabase"));
            serviceCollection.AddTransient(typeof(BaseController));
            serviceCollection.AddTransient<IGamesService, GamesService>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
            Params.IsTest = true;
            
        }
    }
}
