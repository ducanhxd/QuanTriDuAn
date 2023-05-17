using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPPHongHa.Model
{
    internal class HoaDon
    {
        //Tạo cái fields
        private string _MaHoaDon;
        private string _MaSanPham;
        private string _MaNhanVien;
        private string _MaKhachHang;
        private string _GiaTien;

        //Tạo các Properties để truy cập các fields (getter / setter)

        public string MaHoaDon { get => _MaHoaDon; set => _MaHoaDon = value; }
        public string MaSanPham { get => _MaSanPham; set => _MaSanPham = value; }
        public string MaNhanVien { get => _MaNhanVien; set => _MaNhanVien = value; }
        public string MaKhachHang { get => _MaKhachHang; set => _MaKhachHang = value; }
        public string GiaTien { get => _GiaTien; set => _GiaTien = value; }

        public HoaDon() { }
    }
}
