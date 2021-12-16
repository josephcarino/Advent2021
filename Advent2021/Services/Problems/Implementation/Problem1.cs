using josephcarino.Advent2021.Helpers;

namespace josephcarino.Advent2021.Services.Problems.Implementation
{
    public class Problem1 : Problem
    {
        public Problem1(ProblemSettings problemSettings, IFileHelper fileHelper) : base(1, problemSettings, fileHelper)
        {
        }

        public override Task<string> RunPart1(string[] input)
        {
            return Task.FromResult(input.FirstOrDefault() ?? "");
        }

        public override Task<string> RunPart2(string[] input)
        {
            return Task.FromResult(input.LastOrDefault() ?? "");
        }
    }
}
