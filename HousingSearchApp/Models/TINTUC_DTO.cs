using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HousingSearchApp.Models
{
    public class TINTUC_DTO
    {
        public string MaTinTuc { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public DateTime NgayDang { get; set; }
        public string MaNguoiDung { get; set; }
        public string TenFileAnh { get; set; }
    }
}