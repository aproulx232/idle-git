using Application;
using Octokit.Webhooks;
using Octokit.Webhooks.AspNetCore;

namespace Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<WebhookEventProcessor, MyWebhookEventProcessor>();
            services.AddSingleton<IScoreProvider, ScoreProvider>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGitHubWebhooks();
            });
        }
    }
}
