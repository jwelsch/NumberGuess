namespace NumberGuess
{
    public interface INumberGuessGameTrackerFactory
    {
        INumberGuessGameTracker Create();
    }

    public class NumberGuessGameTrackerFactory : INumberGuessGameTrackerFactory
    {
        public INumberGuessGameTracker Create() => new NumberGuessGameTracker();
    }
}
