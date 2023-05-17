using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPPHongHa.Model
{
    internal class SanPham
    {
        private string _MaSanPham;
        private string _TenSanPham;
        private string _MaNCC;
        private string _GiaNhap;
        private string _GiaBan;
        private string _SoLuong;

        public string MaSanPham { get => _MaSanPham; set => _MaSanPham = value; }
        public string TenSanPham { get => _TenSanPham; set => _TenSanPham = value; }
        public string MaNCC { get => _MaNCC; set => _MaNCC = value; }
        public string GiaNhap { get => _GiaNhap; set => _GiaNhap = value; }
        public string GiaBan { get => _GiaBan; set => _GiaBan = value; }
        public string SoLuong { get => _SoLuong; set => _SoLuong = value; }
    }
}
