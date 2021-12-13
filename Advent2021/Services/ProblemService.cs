using josephcarino.Advent2021.Services.Problems;

namespace josephcarino.Advent2021.Services
{
    public class ProblemService
    {
        private IProblemFactory problemFactory;

        public ProblemService(IProblemFactory problemFactory)
        {
            this.problemFactory = problemFactory;
        }

        public IEnumerable<int> GetProblemIds() => problemFactory.GetProblemIds();

        public string GetProblemDescription(int problemId, ProblemPart part) => part switch
        {
            ProblemPart.part1 => GetProblemById(problemId).Description1,
            ProblemPart.part2 => GetProblemById(problemId).Description2,
            _ => throw new ArgumentOutOfRangeException(nameof(part)),
        };

        public Task<string> AnswerProblem(int problemId, string[] input, ProblemPart part)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            IProblem problem = GetProblemById(problemId);

            return part == ProblemPart.part1 ? problem.RunPart1(input) : problem.RunPart2(input);
        }

        private IProblem GetProblemById(int problemId) => problemFactory.GetProblemById(problemId);
    }

    public enum ProblemPart
    {
        part1,
        part2
    }
}
