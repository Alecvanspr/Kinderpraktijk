using System;
using Xunit;
using src.Models;

namespace tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Message message = new Message();
            Assert.IsType<User>(message);
        }
    }
}
