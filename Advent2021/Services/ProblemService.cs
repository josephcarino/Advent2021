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

        public IProblem GetProblemById(int problemId) => problemFactory.GetProblemById(problemId);

        public Task<string> AnswerProblem(int problemId, string[] input, ProblemPart part)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            IProblem problem = GetProblemById(problemId);

            return part == ProblemPart.part1 ? problem.RunPart1(input) : problem.RunPart2(input);
        }

    }

    public enum ProblemPart
    {
        part1,
        part2
    }
}
