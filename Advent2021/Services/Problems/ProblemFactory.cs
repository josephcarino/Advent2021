using System.Reflection;
using josephcarino.Advent2021.Helpers;
using josephcarino.Advent2021.Services.Problems.Implementation;

namespace josephcarino.Advent2021.Services.Problems
{
    public class ProblemFactory : IProblemFactory
    {
        private IDictionary<int, IProblem> problemsDict;
        public ProblemFactory(Func<IList<object>, IEnumerable<IProblem?>> problemCreateFunc, IFileHelper fileHelper)
        {
            problemsDict = problemCreateFunc(new List<object>() { fileHelper }).Where(x => x != null).GroupBy(x => x!.ProblemId).ToDictionary(k => k.Key, v => v.Last()!);
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
