using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace sunamo.Tests.Helpers.FileSystem
{
    public class FS
    {
        [Fact]
        public void GetNameWithoutSeries()
        {
            string excepted1 = "abc(1).txt";
            string result1 = FS.GetNameWithoutSeries()
        }
    }
}
