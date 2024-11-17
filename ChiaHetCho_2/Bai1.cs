using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChiaHetCho_2
{
    internal class Bai1
    {
        static void Main(string[] args)
        {
        }
        private Class _math;

        [SetUp]
        public void Setup()
        {
            _math = new Class();
        }

        [Test]
        [TestCase(0, true)]
        [TestCase(4, true)]
        [TestCase(5, false)]
        [TestCase(6, true)]
        [TestCase(18, true)]
        [TestCase(10, true)]
        [TestCase(9, false)]
        [TestCase(21, false)]
        [TestCase(25, false)]
        [TestCase(40, false)]
        public void SoChan(int number, bool expected)
        {
            var result = _math.Chan(number);
            Assert.That(result, Is.EqualTo(expected));
        }
        [Test]
        [TestCase(-1)]
        [TestCase(int.MinValue)]
        public void SoChan_Throws(int number)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _math.Chan(number));
        }

        public class Class
        {
            public bool Chan(int a)
            {
                if (a < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(a), "Số không được âm.");
                }
                return a % 2 == 0;
            }
        }
    }
}
