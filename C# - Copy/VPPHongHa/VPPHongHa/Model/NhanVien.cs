using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPPHongHa.Model
{
    internal class NhanVien
    {
        private string _MaNhanVien;
        private string _TenNhanVien;
        private string _Tuoi;
        private string _QueQuan;



        public string MaNhanVien { get => _MaNhanVien; set => _MaNhanVien = value; }
        public string TenNhanVien { get => _TenNhanVien; set => _TenNhanVien = value; }
        public string Tuoi { get => _Tuoi; set => _Tuoi = value; }
        public string QueQuan { get => _QueQuan; set => _QueQuan = value; }

        public NhanVien() { }
    }
}
