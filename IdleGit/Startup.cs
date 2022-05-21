using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdleGit;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace IdleGit
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = InitializeConfiguration(builder.Services.BuildServiceProvider());
        }

        protected virtual IConfiguration InitializeConfiguration(IServiceProvider provider)
        {
            var config = provider.GetService<IConfiguration>();

            config = new ConfigurationBuilder()
                .AddConfiguration(config)
                .Build();

            return config;
        }
    }
}
