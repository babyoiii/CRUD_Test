using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TinhHieu
{
    [TestFixture]
    internal class Bai2
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
        [TestCase(10, 5, 8, 3, 7)]
        [TestCase(-5, -10, 0, 3, 13)]//Âm
        [TestCase(0, 0, 0, 0, 0)]//Số 0
        [TestCase(int.MaxValue, int.MinValue, 0, 1, -1)]//Biên
        [TestCase()]//Mảng rỗng
        public void Difference_ReturnsCorrectResult(int a, int b, int c, int d, int kq)
        {
            int result = _math.TinhHieu(a, b, c, d);
            Assert.That(result, Is.EqualTo(kq));
        }
        public class Class
        {
            public int[] mang;
            public int TinhHieu(int a, int b, int c, int d)
            {
                mang = new int[] { a, b, c, d };
                int max = mang.Max();
                int min = mang.Min();
                return max - min;
            }
        }
    }

}
