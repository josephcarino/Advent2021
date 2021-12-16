using AutoFixture;
using AutoFixture.Kernel;
using josephcarino.Advent2021.Helpers;
using josephcarino.Advent2021.Services.Problems;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace josephcarino.Advent2021.Tests.Services.Problems.Implementation
{
    public abstract class ProblemTestBase<TProblem> : TestBase where TProblem : IProblem
    {
        private Mock<IFileHelper> _fileHelper;
        private ProblemSettings _problemSettings;
        private TProblem _sut;

        protected ProblemTestBase()
        {
            _fileHelper = _fixture.Create<Mock<IFileHelper>>();
            _fixture.Inject(_fileHelper.Object);

            _problemSettings = _fixture.Freeze<ProblemSettings>();

            _sut = _fixture.Create<TProblem>();
        }

        [Theory]
        [MemberData("Part1TestData")] //unfortunately there is a bug in xunit 2.4.1 which causes this to throw, so we're stuck on 2.4.0. See https://github.com/xunit/xunit/issues/1983
        public async Task Part1Test(string[] input, string expectedOutput)
        {
            string output = await _sut.RunPart1(input);

            Assert.Equal(expectedOutput, output);
        }

        [Theory]
        [MemberData("Part2TestData")]
        public async Task Part2Test(string[] input, string expectedOutput)
        {
            string output = await _sut.RunPart2(input);

            Assert.Equal(expectedOutput, output);
        }
    }
}
