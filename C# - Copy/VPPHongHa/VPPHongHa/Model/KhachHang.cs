using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPPHongHa.Model
{
    internal class KhachHang
    {
        private string _MaKhachHang;
        private string _TenKhachHang;
        private string _SDT;

        public string MaKhachHang { get => _MaKhachHang; set => _MaKhachHang = value; }
        public string TenKhachHang { get => _TenKhachHang; set => _TenKhachHang = value; }
        public string SDT { get => _SDT; set => _SDT = value; }

        public KhachHang() {  }
    }
}
