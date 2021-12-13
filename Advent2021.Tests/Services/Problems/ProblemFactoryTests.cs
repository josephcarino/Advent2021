using AutoFixture;
using josephcarino.Advent2021.Services.Problems;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace josephcarino.Advent2021.Tests.Services.Problems
{
    public class ProblemFactoryTests : TestBase
    {
        private Mock<Func<IEnumerable<IProblem?>>> _problemCreateFunc;

        public ProblemFactoryTests() : base()
        {
            _problemCreateFunc = _fixture.Create<Mock<Func<IEnumerable<IProblem?>>>>();
            _fixture.Inject(_problemCreateFunc.Object);
        }

        [Theory]
        [MemberData(nameof(ProblemIdsData))]
        public void GetProblemIds_ShouldReturnNonNullKeys(List<IProblem?> input, IEnumerable<int> expectedOutput)
        {
            _problemCreateFunc.Setup(x => x()).Returns(input);
            var sut = _fixture.Create<ProblemFactory>(); //have to create here as func above is used in constructor, and so needs to be Setup before creating sut

            var ret = sut.GetProblemIds();

            Assert.NotNull(ret);
            Assert.True(expectedOutput.SequenceEqual(ret));
        }

        [Theory]
        [InlineData(0, typeof(ArgumentException))]
        [InlineData(-1, typeof(ArgumentException))]
        [InlineData(2, typeof(NotImplementedException))]
        [InlineData(1, null)]
        [InlineData(5, null)]
        public void GetProblemById_ShouldReturnCorrectProblem(int id, Type? exceptionType)
        {
            var input = new List<IProblem?> { null, CreateMockProblemWithId(1), CreateMockProblemWithId(5) };
            _problemCreateFunc.Setup(x => x()).Returns(input);
            var sut = _fixture.Create<ProblemFactory>(); //have to create here as func above is used in constructor, and so needs to be Setup before creating sut

            if (exceptionType is not null)
            {
                Exception thrownException = Assert.ThrowsAny<Exception>(() => sut.GetProblemById(id));
                Assert.Equal(exceptionType, thrownException.GetType());
            }
            else
            {
                var returnedProblem = sut.GetProblemById(id);
                Assert.Same(input.Where(x => x != null && x.ProblemId == id).First(), returnedProblem);
            }
        }


        public static IEnumerable<object[]> ProblemIdsData() =>
            new List<object[]>
            {
                new object[] {new List<IProblem?> { null }, new List<int> { } },
                new object[] {new List<IProblem?> { CreateMockProblemWithId(1) }, new List<int> { 1 } },
                new object[] {new List<IProblem?> { CreateMockProblemWithId(1), CreateMockProblemWithId(5) }, new List<int> { 1, 5 } },
                new object[] {new List<IProblem?> { CreateMockProblemWithId(1), CreateMockProblemWithId(5), CreateMockProblemWithId(3) }, new List<int> { 1, 3, 5 } },
                new object[] {new List<IProblem?> { CreateMockProblemWithId(1), CreateMockProblemWithId(1) }, new List<int> { 1 } },
                new object[] {new List<IProblem?> { CreateMockProblemWithId(1), null }, new List<int> { 1 } },
            };

        private static IProblem CreateMockProblemWithId(int id)
        {
            var problem = new Mock<IProblem>();
            problem.SetupGet(x => x.ProblemId).Returns(id);

            return problem.Object;
        }
            
    }
}
