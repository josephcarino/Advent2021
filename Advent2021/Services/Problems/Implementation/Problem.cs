using josephcarino.Advent2021.Helpers;
using josephcarino.Advent2021.Services.Problems;

namespace josephcarino.Advent2021.Services.Problems.Implementation
{
    public abstract class Problem : IProblem
    {
        private ProblemSettings _settings;
        private readonly int _problemId;
        private readonly string _description1;
        private readonly string _description2;

        public Problem(int problemId, ProblemSettings problemSettings, IFileHelper fileHelper)
        {
            _settings = problemSettings;
            _problemId = problemId;
            _description1 = fileHelper.ReadDescriptionFromFile(_settings.BaseDescriptionsPath, ProblemId, 1);
            _description2 = fileHelper.ReadDescriptionFromFile(_settings.BaseDescriptionsPath, ProblemId, 2);
        }

        public int ProblemId { get => _problemId; }
        public string Description1 { get => _description1; }
        public string Description2 { get => _description2; }

        public abstract Task<string> RunPart1(string[] input);

        public abstract Task<string> RunPart2(string[] input);
    }
}
