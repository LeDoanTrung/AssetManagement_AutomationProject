using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace DemoNunitFW
{
    public class SeleniumTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        //[Category("Test")]
        [Ignore("Ignore")]
        public void Test1()
        {
            Assert.Pass();
        }

        [TestCase("param1")]
        [TestCase("param2")]
        [Ignore("Ignore")]
        public void Test2(string param)
        {
            Console.WriteLine(param);
            Assert.Pass();
        }
    }
}