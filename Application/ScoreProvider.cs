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

        public Task GetScore()
        {
            throw new NotImplementedException();
        }
    }
}