namespace Application
{
    public interface IScoreProvider
    {
        Task GetScore();
    }

    public class ScoreProvider : IScoreProvider
    {
        public ScoreProvider()
        {
            
        }

        public async Task GetScore()
        {
            await Task.Yield();
        }
    }
}