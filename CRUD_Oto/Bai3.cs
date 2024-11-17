using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Oto
{
    [TestFixture]
    internal class Bai3
    {
        static void Main(string[] args)
        {
        }
        private CRUD _car;

        [SetUp]
        public void Setup()
        {
            _car = new CRUD();
        }
        [Test]
        [TestCase(1, "Toyota Camry", 25000, "Xe sedan hạng trung")]
        [TestCase(2, "Honda Civic", -20000.66, "Xe compact")]//float
        [TestCase(3, "Ford Mustang", 35000)]
        [TestCase(5)]//Rỗng
        public void ThemXe(int ma, string ten, int gia, string ghiChu)
        {
            var xeMoi = new Car
            {
                Ma = ma,
                Ten = ten,
                Gia = gia,
                Ghichu = ghiChu
            };
            _car.Them(xeMoi);
            Assert.That(_car.DanhSach().Contains(xeMoi), Is.True);
        }
        [Test]
        [TestCase(1, "Toyota Camry", 30000, "Xe sedan hạng trung")]
        [TestCase(2, "Honda Civic", -22000.55, "Xe compact")]//float
        [TestCase(3, "Ford Mustang", 40000)]
        [TestCase(4, "Toyota Corolla", 25000, "Xe sedan hạng trung")]//Không thấy id
        public void SuaXe(int ma, string ten, int gia, string ghiChu)
        {
            var xeMoi = new Car
            {
                Ma = ma,
                Ten = ten,
                Gia = gia,
                Ghichu = ghiChu
            };
            _car.Them(xeMoi);

            var xeSua = new Car
            {
                Ma = ma,
                Ten = "Updated " + ten,
                Gia = gia + 5000,
                Ghichu = "Updated " + ghiChu
            };
            _car.Sua(ma, xeSua);

            var car = _car.DanhSach().Find(c => c.Ma == ma);
            Assert.That(car.Ten, Is.EqualTo(xeSua.Ten));
            Assert.That(car.Gia, Is.EqualTo(xeSua.Gia));
            Assert.That(car.Ghichu, Is.EqualTo(xeSua.Ghichu));
        }
        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(4)]//Không thấy id
        public void XoaXe_(int ma)
        {
            var xeMoi = new Car
            {
                Ma = ma,
                Ten = "Test Car",
                Gia = 10000,
                Ghichu = "Test"
            };
            _car.Them(xeMoi);
            _car.Xoa(ma);
            Assert.That(_car.DanhSach().Contains(xeMoi), Is.False);
        }
        public class Car
        {
            public int Ma { get; set; }
            public string Ten { get; set; }
            public int Gia { get; set; }
            public string Ghichu { get; set; }
        }
        public class CRUD
        {
            private List<Car> _cars = new List<Car>();

            public void Them(Car car)
            {
                _cars.Add(car);
            }
            public void Sua(int ma, Car sua)
            {
                var car = _cars.Find(c => c.Ma == ma);
                if (car != null)
                {
                    car.Ten = sua.Ten;
                    car.Gia = sua.Gia;
                    car.Ghichu = sua.Ghichu;
                }
                else
                {
                    throw new Exception($"Không tìm thấy xe có mã {ma}");
                }
            }
            public void Xoa(int ma)
            {
                var car = _cars.Find(c => c.Ma == ma);
                if (car != null)
                {
                    _cars.Remove(car);
                }
                else
                {
                    throw new Exception($"Không tìm thấy xe có mã {ma}");
                }
            }
            public List<Car> DanhSach()
            {
                return _cars;
            }
        }
    }
}
