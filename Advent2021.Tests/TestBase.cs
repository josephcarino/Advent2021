using AutoFixture;
using AutoFixture.AutoMoq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace josephcarino.Advent2021.Tests
{
    public abstract class TestBase
    {
        protected IFixture _fixture;

        protected TestBase()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
        }
    }
}
