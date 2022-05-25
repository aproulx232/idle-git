using Infrastructure;
using Infrastructure.TableService;

namespace Application
{
    public interface IScoreProvider
    {
        Task<int> GetScore(string login);
    }

    public class ScoreProvider : IScoreProvider
    {
        private readonly IGitHubService _gitHubService;
        private readonly ITableService _tableService;

        public ScoreProvider(IGitHubService gitHubService, ITableService tableService)
        {
            _gitHubService = gitHubService ?? throw new ArgumentNullException(nameof(gitHubService));
            _tableService = tableService ?? throw new ArgumentNullException(nameof(tableService));
        }

        public async Task<int> GetScore(string login)
        {
            var scoreState = await _tableService.GetScoreState(login);

            var repositories = await _gitHubService.GetRepositories(login);

            return repositories.Count;
        }
    }
}