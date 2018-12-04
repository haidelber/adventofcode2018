namespace ConsoleApp
{
    public interface IChallengeSolver
    {
        string InputPath { get; set; }

        object SolveChallange();
    }
}