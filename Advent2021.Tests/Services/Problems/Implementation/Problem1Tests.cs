using josephcarino.Advent2021.Services.Problems.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace josephcarino.Advent2021.Tests.Services.Problems.Implementation
{
    public class Problem1Tests : ProblemTestBase<Problem1>
    {
        public static IEnumerable<object[]> Part1TestData = new List<object[]>
        {
            new object[] { new string[] { "1", "2" }, "1" },
            new object[] { new string[] { "2" }, "2" },
            new object[] { new string[] { "" }, "" },
            new object[] { new string[] { }, "" },
        };

        public static IEnumerable<object[]> Part2TestData = new List<object[]>
        {
            new object[] { new string[] { "1", "2" }, "2" },
            new object[] { new string[] { "2" }, "2" },
            new object[] { new string[] { "" }, "" },
            new object[] { new string[] { }, "" },
        };
    }
}
