using josephcarino.Advent2021.Services.Problems;

namespace josephcarino.Advent2021.Services
{
    public class ProblemService
    {
        private Dictionary<int, Problem> problems;

        public ProblemService(IEnumerable<Problem> problems)
        {
            this.problems = problems.ToDictionary(k => k.ProblemId, v => v);
        }

        public IEnumerable<int> GetProblemIds()
        {
            return problems.Keys;
        }

        public string GetProblemDescription(int problemId, ProblemPart part) => part switch
        {
            ProblemPart.part1 => GetProblemFromId(problemId).Description1,
            ProblemPart.part2 => GetProblemFromId(problemId).Description2,
            _ => throw new ArgumentOutOfRangeException(nameof(problemId)),
        };

        public Task<string> AnswerProblem(int problemId, string[] input, ProblemPart part)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            Problem problem = GetProblemFromId(problemId);

            return part == ProblemPart.part1 ? problem.RunPart1(input) : problem.RunPart2(input);
        }

        private Problem GetProblemFromId(int problemId)
        {
            if (problemId <= 0)
                throw new ArgumentException("Incorrect ProblemId specified");

            if (!problems.TryGetValue(problemId, out Problem? problem))
            {
                throw new NotImplementedException($"Problem with id={problemId} is not implemented");
            }
            else
            {
                return problem;
            }
        }
    }

    public enum ProblemPart
    {
        part1,
        part2
    }
}
