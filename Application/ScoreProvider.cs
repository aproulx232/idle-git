using Infrastructure;

namespace Application
{
    public interface IScoreProvider
    {
        Task<int> GetScore(string login);
    }

    public class ScoreProvider : IScoreProvider
    {
        private readonly IGitHubService _gitHubService;

        public ScoreProvider(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService ?? throw new ArgumentNullException(nameof(gitHubService));
        }

        public async Task<int> GetScore(string login)
        {
            var repositories = await _gitHubService.GetRepositories(login);

            return repositories.Count;
        }
    }
}