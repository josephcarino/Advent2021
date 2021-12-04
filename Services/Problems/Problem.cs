namespace josephcarino.Advent2021.Services.Problems
{
    public abstract class Problem
    {
        public Problem(int problemId, string description1, string description2)
        {
            ProblemId = problemId;
            Description1 = File.ReadAllText(description1);
            Description2 = File.ReadAllText(description2);
        }

        public int ProblemId { get; set; }
        public string Description1 { get; set; }
        public string Description2 { get; set; }

        public abstract Task<string> Run(string[] input, ProblemPart part);
    }

    public enum ProblemPart
    {
        part1,
        part2
    }
}
