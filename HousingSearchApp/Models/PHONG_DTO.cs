using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HousingSearchApp.Models
{
    public class PHONG_DTO
    {
        public string MaPhong { get; set; }
        public string MaNguoiDung { get; set; }
        public string MaLoaiPhong { get; set; }
        public string TieuDe { get; set; }
        public string DiaChi { get; set; }
        public string MoTa { get; set; }
        public int? GiaThue { get; set; }
        public string TienIch { get; set; }
        public int? DienTich { get; set; }
        public string TenLoaiPhong { get; set; }
        public string TenNguoiDung { get; set; }
        public string TenFileAnh { get; set; }
        public int TrangThai { get; set; }
        public string ThoiGianDang { get; set; }
    }
}