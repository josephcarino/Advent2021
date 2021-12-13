using josephcarino.Advent2021.Services;
using josephcarino.Advent2021.Services.Problems;
using Microsoft.AspNetCore.Mvc;

namespace josephcarino.Advent2021.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProblemController : ControllerBase
    {
        private readonly ProblemService problemService;

        public ProblemController(ProblemService problemService)
        {
            this.problemService = problemService;
        }

        [HttpGet]
        public IEnumerable<int> GetProblems()
        {
            return problemService.GetProblemIds();
        }

        [HttpGet("{id}/description/{partId}")]
        public ContentResult GetProblemDescription(int id, int partId)
        {
            return base.Content(problemService.GetProblemDescription(id, GetProblemPartByInt(partId)), "text/html");
        }

        [HttpPost("{id}/{partId}/answer")]
        public async Task<ContentResult> AnswerProblem(int id, int partId, [FromBody] string[] input)
        {
            ProblemPart problemPart = GetProblemPartByInt(partId);

            return base.Content(await problemService.AnswerProblem(id, input, problemPart));
        }

        private ProblemPart GetProblemPartByInt(int partId) => partId switch
        {
            1 => ProblemPart.part1,
            2 => ProblemPart.part2,
            _ => throw new ArgumentOutOfRangeException(nameof(partId)),
        };
    }
}
