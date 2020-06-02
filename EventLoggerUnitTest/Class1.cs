using System;
using Xunit;

namespace EventLoggerUnitTest
{
    public class Class1
    {

        [Fact]
        public void PassTest()
        {
            Assert.Equal(4, Add(2, 2));
        }

        [Fact]
        public void FaildTest()
        {
            Assert.Equal(5, Add(2, 2));
        }

        private int Add(int x, int y)
        {
            return x + y;        
        }
    }
}
