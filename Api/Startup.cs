using Application;
using Microsoft.OpenApi.Models;
using Octokit.Webhooks;
using Octokit.Webhooks.AspNetCore;

namespace Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private const string ApiTitle = "Idle Git API";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<WebhookEventProcessor, MyWebhookEventProcessor>();
            services.AddSingleton<IScoreProvider, ScoreProvider>();
            ConfigureSwagger(services);
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
    }
}
