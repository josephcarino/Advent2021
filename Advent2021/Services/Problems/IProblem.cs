namespace josephcarino.Advent2021.Services.Problems
{
    public interface IProblem
    {
        public int ProblemId { get; }
        public string Description1 { get; }
        public string Description2 { get; }
        public abstract Task<string> RunPart1(string[] input);
        public abstract Task<string> RunPart2(string[] input);
    }
}
