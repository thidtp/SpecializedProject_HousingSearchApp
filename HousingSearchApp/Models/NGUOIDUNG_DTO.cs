using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HousingSearchApp.Models
{
    public class NGUOIDUNG_DTO
    {
        public string MaNguoiDung { get; set; }
        public string MaLTK { get; set; }
        public string TenNguoiDung { get; set; }
        public string email { get; set; }
        public string TenTK { get; set; }
        public string MatKhau { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string MatKhauMoi { get; set; }
        public string XacNhanMatKhau { get; set; }  
        public string SelectedOption { get; set; }
        public Nullable<System.DateTime> ThoiGianDangNhapLanCuoi { get; set; }
        public string TenFileAnh { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}