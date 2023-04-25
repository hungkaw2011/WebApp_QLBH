using System;
using System.Collections.Generic;

#nullable disable

namespace Bai12_Hung660_P1.Models
{
    public partial class CthoaDon
    {
        public string MaSp { get; set; }
        public string MaHd { get; set; }
        public int? SoLuongMua { get; set; }

        public virtual HoaDon MaHdNavigation { get; set; }
        public virtual SanPham MaSpNavigation { get; set; }
    }
}
