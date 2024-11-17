using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CRUD_nhanvienss
{
    internal class Bai4
    {
        static void Main(string[] args)
        {
        }
        private NhanVien CRUD;

        [SetUp]
        public void Setup()
        {
            CRUD = new NhanVien();
        }

        [Test]
        [TestCase(1, "Nguyen", "DanhLam", "nguyen.danh@lam.com")]
        [TestCase(4, "Nguyen123", "DanhLam", "nguyen.danh@lam.com")]//tên chứa số
        [TestCase(2, "He", "He", "He")]
        [TestCase(3)]//rỗng
        public void ThemNV(int id, string firstName, string lastName, string email)
        {
            var nhanviens = new nhanviens
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };
            CRUD.Them(nhanviens);
            Assert.That(CRUD.DanhSach().Contains(nhanviens), Is.True);
        }

        [Test]
        [TestCase(1, "Danh", "Lâm", "danh.lam@gmail.com")]
        public void SuaNV(int id, string firstName, string lastName, string email)
        {
            var nhanviens = new nhanviens
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };
            CRUD.Them(nhanviens);

            var suanhanvien = new nhanviens
            {
                Id = id,
                FirstName = "Sua" + firstName,
                LastName = "Sua " + lastName,
                Email = "sua." + email
            };
            CRUD.Sua(id, suanhanvien);

            var emp = CRUD.DanhSach().Find(e => e.Id == id);
            Assert.That(emp.FirstName, Is.EqualTo(suanhanvien.FirstName));
            Assert.That(emp.LastName, Is.EqualTo(suanhanvien.LastName));
            Assert.That(emp.Email, Is.EqualTo(suanhanvien.Email));
        }

        [Test]
        [TestCase(1)]
        public void XoaNV(int id)
        {
            var nhanviens = new nhanviens
            {
                Id = id,
                FirstName = "Test",
                LastName = "nhanviens",
                Email = "test.nhanviens@example.com"
            };
            CRUD.Them(nhanviens);
            CRUD.Xoa(id);
            Assert.That(CRUD.DanhSach().Contains(nhanviens), Is.False);
        }

        [Test]
        [TestCase(3, "Lam123", "Danh", "lam.danh@gmail.com")] 
        [TestCase(4, "Danh", "Lam", "email")]
        public void ThemNhanVien_ThongBao(int id, string firstName, string lastName, string email)
        {
            var nhanviens = new nhanviens
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };

            Assert.Throws<Exception>(() => CRUD.Them(nhanviens));
        }

        public class nhanviens
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
        }

        public class NhanVien
        {
            private List<nhanviens> _nhanvienss = new List<nhanviens>();

            public void Them(nhanviens nhanviens)
            {
                ThongBao(nhanviens);
                _nhanvienss.Add(nhanviens);
            }

            public void Sua(int id, nhanviens suanhanvien)
            {
                ThongBao(suanhanvien);
                var nhanviens = _nhanvienss.Find(e => e.Id == id);
                if (nhanviens != null)
                {
                    nhanviens.FirstName = suanhanvien.FirstName;
                    nhanviens.LastName = suanhanvien.LastName;
                    nhanviens.Email = suanhanvien.Email;
                }
                else
                {
                    throw new Exception($"Không tìm thấy nhân viên có ID {id}");
                }
            }

            public void Xoa(int id)
            {
                var nhanviens = _nhanvienss.Find(e => e.Id == id);
                if (nhanviens != null)
                {
                    _nhanvienss.Remove(nhanviens);
                }
                else
                {
                    throw new Exception($"Không tìm thấy nhân viên có ID {id}");
                }
            }

            public List<nhanviens> DanhSach()
            {
                return _nhanvienss;
            }

            private void ThongBao(nhanviens nhanviens)
            {
                if (string.IsNullOrWhiteSpace(nhanviens.FirstName) ||
                    string.IsNullOrWhiteSpace(nhanviens.LastName) ||
                    !EmailHopLe(nhanviens.Email) ||
                    ChuaSo(nhanviens.FirstName) ||
                    ChuaSo(nhanviens.LastName))
                {
                    throw new Exception("Thông tin nhân viên không hợp lệ.");
                }
            }

            private bool EmailHopLe(string email)
            {
                return email.Contains("@");
            }

            private bool ChuaSo(string str)
            {
                return Regex.IsMatch(str, @"\d");
            }
        }
    }
}
