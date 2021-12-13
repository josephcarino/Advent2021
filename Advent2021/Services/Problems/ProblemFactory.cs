using System.Reflection;
using josephcarino.Advent2021.Services.Problems.Implementation;

namespace josephcarino.Advent2021.Services.Problems
{
    public class ProblemFactory : IProblemFactory
    {
        private IDictionary<int, IProblem> problemsDict;
        public ProblemFactory(Func<IEnumerable<IProblem?>> problemCreateFunc)
        {
            problemsDict = problemCreateFunc().Where(x => x != null).GroupBy(x => x!.ProblemId).ToDictionary(k => k.Key, v => v.Last()!);
        }

        public IEnumerable<int> GetProblemIds() => problemsDict.Keys.OrderBy(x => x);

        public IProblem GetProblemById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Incorrect ProblemId specified");

            if (problemsDict.ContainsKey(id))
                return problemsDict[id];
            
            throw new NotImplementedException($"Problem with id={id} is not implemented");
        }
    }
}
