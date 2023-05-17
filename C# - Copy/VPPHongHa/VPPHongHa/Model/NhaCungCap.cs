using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPPHongHa.Model
{
    internal class NhaCungCap
    {
        private string _MaNCC;
        private string _TenNCC;

        public string MaNCC { get => _MaNCC; set => _MaNCC = value; }
        public string TenNCC { get => _TenNCC; set => _TenNCC = value; }

        public NhaCungCap() { }
    }
}
