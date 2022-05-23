using Octokit;

namespace Infrastructure
{
    public interface IGitHubService
    {
        Task<IReadOnlyList<Repository>> GetRepositories(string login);
    }

    public class GitHubService : IGitHubService
    {
        private readonly GitHubClient _gitHubClient;

        public GitHubService(GitHubClient gitHubClient)
        {
            _gitHubClient = gitHubClient ?? throw new ArgumentNullException(nameof(gitHubClient));
        }

        public async Task<IReadOnlyList<Repository>> GetRepositories(string login)
        {
            var repositories = await _gitHubClient.Repository.GetAllForUser(login);
            return repositories;
        }
    }
}