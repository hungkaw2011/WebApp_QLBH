using System;
using System.Collections.Generic;

#nullable disable

namespace Bai12_Hung660_P1.Models
{
    public partial class HoaDon
    {
        public string MaHd { get; set; }
        public DateTime? NgayLap { get; set; }
        public string MaKh { get; set; }
        public string NguoiLap { get; set; }

        public virtual KhachHang MaKhNavigation { get; set; }
    }
}
