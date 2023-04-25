using System;
using System.Collections.Generic;

#nullable disable

namespace Bai12_Hung660_P1.Models
{
    public partial class SanPham
    {
        public string MaSp { get; set; }
        public string TenSp { get; set; }
        public string MaLoai { get; set; }
        public int? SoLuong { get; set; }
        public int? Gia { get; set; }

        public virtual LoaiSp MaLoaiNavigation { get; set; }
    }
}
