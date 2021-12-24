using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using josephcarino.Advent2021.Services;
using Xunit;
using Moq;
using AutoFixture;
using josephcarino.Advent2021.Services.Problems;

namespace josephcarino.Advent2021.Tests.Services
{
    public class ProblemServiceTests : TestBase
    {
        private ProblemService _sut;
        private Mock<IProblemFactory> _problemFactory;

        public ProblemServiceTests() : base()
        {
            _problemFactory = _fixture.Create<Mock<IProblemFactory>>();
            _fixture.Inject(_problemFactory.Object);
            _sut = _fixture.Create<ProblemService>();
        }

        [Fact]
        public void GetProblemIds_ShouldReturnProblemIds()
        {
            IEnumerable<int> expected = new List<int>() { 1, 2, 3};
            _problemFactory.Setup(x => x.GetProblemIds()).Returns(expected).Verifiable();
            
            IEnumerable<int> ret = _sut.GetProblemIds();

            _problemFactory.VerifyAll();
            Assert.Same(expected, ret);
        }

        [Theory]
        [InlineData(ProblemPart.part1)]
        [InlineData(ProblemPart.part2)]
        public async Task AnswerProblem_ShouldReturnCorrectPartOfCorrectProblem(ProblemPart part)
        {
            int problemId = 1;
            string expectedOutput1 = "Output 1";
            string expectedOutput2 = "Output 2";
            string[] input = new[] { "Test Input" };
            Mock<IProblem> problem = _fixture.Create<Mock<IProblem>>();
            _problemFactory.Setup(x => x.GetProblemById(problemId)).Returns(problem.Object);
            problem.Setup(x => x.RunPart1(input)).ReturnsAsync(expectedOutput1);
            problem.Setup(x => x.RunPart2(input)).ReturnsAsync(expectedOutput2);

            string ret = await _sut.AnswerProblem(problemId, input, part);

            _problemFactory.Verify(x => x.GetProblemById(problemId), Times.Once);
            switch (part)
            {
                case ProblemPart.part1:
                    problem.Verify(x => x.RunPart1(input), Times.Once());
                    problem.Verify(x => x.RunPart2(input), Times.Never());
                    Assert.Equal(expectedOutput1, ret);
                    break;
                case ProblemPart.part2:
                    problem.Verify(x => x.RunPart1(input), Times.Never());
                    problem.Verify(x => x.RunPart2(input), Times.Once());
                    Assert.Equal(expectedOutput2, ret);
                    break;
                default: throw new ArgumentException();
            }
        }
    }
}
