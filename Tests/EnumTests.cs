using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeoSmart.Linq;

namespace Tests
{
    [TestClass]
    public class EnumTests
    {
        enum TestEnum
        {
            Foo,
            Bar
        }

        [TestMethod]
        public void EnumEnumeration()
        {
            Enum<TestEnum>.ToArray();
        }
    }
}
