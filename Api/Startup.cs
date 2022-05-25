using Application;
using Infrastructure;
using Infrastructure.TableService;
using Microsoft.Extensions.Azure;
using Microsoft.OpenApi.Models;
using Octokit;
using Octokit.Webhooks;
using Octokit.Webhooks.AspNetCore;

namespace Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private const string ProjectTitle = "IdleGit";
        private const string ApiTitle = $"{ProjectTitle} API";
        private const string Version = "v0.1";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<WebhookEventProcessor, MyWebhookEventProcessor>();
            services.AddSingleton<IScoreProvider, ScoreProvider>();
            services.AddSingleton<IGitHubService, GitHubService>();
            services.AddSingleton(new GitHubClient(new ProductHeaderValue(ProjectTitle, Version)));
            ConfigureSwagger(services);
            ConfigureTableService(services);
            services.AddAzureClients(azureClientFactoryBuilder =>
            {
                azureClientFactoryBuilder.AddTableServiceClient(Configuration.GetConnectionString("StorageAccount"));
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGitHubWebhooks();
                endpoints.MapControllers();
            });

            UseSwagger(app);
        }

        private static void UseSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", ApiTitle);
            });
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = ApiTitle,
                    Version = "v1"
                });
            });
        }

        private void ConfigureTableService(IServiceCollection services)
        {
            services.AddSingleton<ITableServiceConfiguration>(
                new TableServiceConfiguration(Configuration.GetValue<string>("TableName")));
            services.AddSingleton<ITableService, TableService>();
        }
    }
}
