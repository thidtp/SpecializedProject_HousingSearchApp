using HousingSearchApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace HousingSearchApp.Controllers
{
    public class NguoiDungController : Controller
    {
        QL_UDNHATROEntities db = new QL_UDNHATROEntities();
        // GET: NguoiDung
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(NGUOIDUNG_DTO nguoiDung)
        {
            if (ModelState.IsValid)
            {
                var nguoidung = db.NGUOIDUNGs.SingleOrDefault(nd => nd.TENTK == nguoiDung.TenTK && nd.MATKHAU == nguoiDung.MatKhau);

                if (nguoidung != null && nguoidung.TRANGTHAI == 1)
                {
                    System.Web.HttpContext.Current.Session["MaNguoiDung"] = nguoidung.MAND;
                    System.Web.HttpContext.Current.Session["LoaiTK"] = nguoidung.MALTK;
                    System.Web.HttpContext.Current.Session["TenNguoiDung"] = nguoidung.TENND;
                    System.Web.HttpContext.Current.Session["SoDienThoai"] = nguoidung.SDT;
                    System.Web.HttpContext.Current.Session["TenFileAnh"] = nguoidung.TENFILEANH;
                    System.Web.HttpContext.Current.Session["DangNhapThanhCong"] = "true";
                    return RedirectToAction("Home", "Home");
                }
                else if (nguoidung == null)
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc Mật khẩu không đúng. Vui lòng thử lại !!!");
                }
                else if (nguoidung.TRANGTHAI != 1)
                {
                    ModelState.AddModelError("", "Tài khoản của bạn đã bị khóa. Vui lòng liên hệ quản trị viên để biết thêm chi tiết.");
                }
            }

            return View(nguoiDung);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(string tentaiKhoan, string tenNguoiDung, string EmAil, string soDienThoai, string matKhau, string selectedOption)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string tenFileAnh = "default_image.jpg";
                    int trangthai = 1;
                        if (db.NGUOIDUNGs.Any(u => u.TENTK == tentaiKhoan))
                        {
                            ModelState.AddModelError("TenTaiKhoan", "Tên tài khoản đã tồn tại.");
                            return View(); 
                        }
                    var nguoiDungMoi = new NGUOIDUNG
                    {
                        MAND = "ND0001",
                        TENTK = tentaiKhoan,
                        TENND = tenNguoiDung,
                        EMAIL = EmAil,
                        SDT = soDienThoai,
                        MATKHAU = matKhau,
                        MALTK = selectedOption,
                        TENFILEANH = tenFileAnh,
                        TRANGTHAI = trangthai,
                        };

                        db.NGUOIDUNGs.Add(nguoiDungMoi);
                        db.SaveChanges();

                        return RedirectToAction("DangNhap", "NguoiDung");
                 }
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                throw; 
            }
            return View();
        }
        public ActionResult DangXuat()
        {
            // Xóa các Session liên quan đến đăng nhập
            Session["MaNguoiDung"] = null;
            Session["TenNguoiDung"] = null;
            Session["TenFileAnh"] = null;
            Session["DangNhapThanhCong"] = null;

            // Các thao tác khác khi đăng xuất

            return RedirectToAction("Home", "Home");
        }
        public ActionResult QuanLyTinDang()
        {
            return View();
        }
        public ActionResult getTinDang(string trangthai,string maNguoiDung)
        {
            int trangThaiValue = Convert.ToInt32(trangthai);
                var danhSachTinDang = db.PHONGs
                    .Include(r => r.HINHANHs)
                    .Where(r => (r.TRANGTHAI == trangThaiValue) && (r.MAND == maNguoiDung))
                    .OrderBy(r => r.MAPHONG)
                    .ToList()
                    .Select(r => new PHONG_DTO
                    {
                        MaPhong = r.MAPHONG,
                        MaNguoiDung = r.MAND,
                        MaLoaiPhong = r.MALP,
                        TieuDe = r.TIEUDE,
                        DiaChi = r.DIACHI,
                        MoTa = r.MOTA,
                        GiaThue = r.GIATHUE,
                        TienIch = r.TIENICH,
                        DienTich = r.DIENTICH,
                        TenLoaiPhong = r.LOAIPHONG.TENLP,
                        TenNguoiDung = r.NGUOIDUNG.TENND,
                        TenFileAnh = r.HINHANHs.Select(h => h.TENFILEANH).FirstOrDefault(),
                        ThoiGianDang = ((DateTime)r.THOIGIANDANG).ToString("dd/MM/yyyy HH:mm")
                    }).ToList();
                return PartialView("getTinDang", danhSachTinDang);
        }
        public ActionResult Chitietphong(string maPhong)
        {
            var listPhong = db.PHONGs
                .Include(r => r.HINHANHs)
                .FirstOrDefault(r => r.MAPHONG.Equals(maPhong));
            return View(listPhong);
        }
        public ActionResult ThongTinTaiKhoan()
        {
            var maNguoiDung = Session["MaNguoiDung"] as string;
            var thongtinNguoiDung = db.NGUOIDUNGs.Where(r => r.MAND.Equals(maNguoiDung)).ToList();
            return View(thongtinNguoiDung);
        }
        public ActionResult CapNhatThongTin(NGUOIDUNG_DTO nguoiDung)
        {
            try
            {
                if (Session["MaNguoiDung"] != null)
                {
                    var user_id = Session["MaNguoiDung"].ToString();
                    var user = db.NGUOIDUNGs.FirstOrDefault(x => x.MAND == user_id);

                    if (user == null)
                    {
                        return RedirectToAction("DangNhap", "NguoiDung");
                    }

                    if (nguoiDung.MatKhau != nguoiDung.XacNhanMatKhau)
                    {
                        TempData["Message"] = "Mật khẩu xác nhận không đúng";
                        TempData["Success"] = false;
                        return RedirectToAction("ThongTinTaiKhoan", "NguoiDung");
                    }
                    if (!string.IsNullOrEmpty(nguoiDung.MatKhau))
                    {
                        user.MATKHAU = nguoiDung.MatKhau;
                    }

                    if (!string.IsNullOrEmpty(nguoiDung.email))
                    {
                        user.EMAIL = nguoiDung.email;
                    }

                    if (!string.IsNullOrEmpty(nguoiDung.DiaChi))
                    {
                        user.DIACHI = nguoiDung.DiaChi;
                    }

                    if (!string.IsNullOrEmpty(nguoiDung.SoDienThoai))
                    {
                        user.SDT = nguoiDung.SoDienThoai;
                    }

                    if (!string.IsNullOrEmpty(nguoiDung.TenNguoiDung))
                    {
                        user.TENND = nguoiDung.TenNguoiDung;
                    }

                    if (nguoiDung.File != null && nguoiDung.File.ContentLength > 0)
                    {
                        string filePath = Server.MapPath(user.TENFILEANH);
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }

                        var fileName = Path.GetFileName(nguoiDung.File.FileName);
                        var fPath = Path.Combine(Server.MapPath("~/Image/DuLieu/NguoiDung"), fileName);
                        user.TENFILEANH = $"~/Image/DuLieu/NguoiDung/{fileName}";
                        Session["TenFileAnh"] = $"~/Image/DuLieu/NguoiDung/{fileName}";
                        nguoiDung.File.SaveAs(fPath);
                    }

                    db.SaveChanges();
                    TempData["Message"] = "Cập nhật thông tin thành công";
                    TempData["Success"] = true;

                    return RedirectToAction("ThongTinTaiKhoan", "NguoiDung");
                }

                return RedirectToAction("DangNhap", "NguoiDung");
            }
            catch (Exception e)
            {
                TempData["Message"] = "Lỗi cập nhật thông tin tài khoản";
                TempData["Success"] = false;
                return RedirectToAction("ThongTinTaiKhoan", "NguoiDung");
            }
        }
        [HttpPost]
        public ActionResult CapNhatMatKhau(string matKhauHienTai, string matKhauMoi)
        {
            try
            {
                if (Session["MaNguoiDung"] != null)
                {
                    var user_id = Session["MaNguoiDung"].ToString();
                    var user = db.NGUOIDUNGs.FirstOrDefault(x => x.MAND == user_id);

                    if (user == null)
                    {
                        return Json(new { Message = "Người dùng không tồn tại." });
                    }

                    // Kiểm tra mật khẩu hiện tại
                    if (user.MATKHAU != matKhauHienTai)
                    {
                        return Json(new { Message = "Mật khẩu hiện tại không đúng." });
                    }

                    // Cập nhật mật khẩu mới
                    user.MATKHAU = matKhauMoi;
                    db.SaveChanges();

                    return Json(new { Message = "Cập nhật mật khẩu thành công." });
                }
                else
                {
                    return Json(new { Message = "Người dùng chưa đăng nhập." });
                }
            }
            catch (Exception e)
            {
                // Xử lý nếu có lỗi xảy ra
                return Json(new { Message = "Có lỗi xảy ra khi cập nhật mật khẩu." });
            }
        }
        public ActionResult DangTin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemPhong(string TieuDe, string District, string Ward, string SoDuong, string DiaChi, int dientich, string tienich, string giathue, string mota, HttpPostedFileBase[] Images, string selectedRoomType)
        {
            try
            {
                int giaThue;
                if (!int.TryParse(giathue, out giaThue))
                {
                    throw new ArgumentException("Giá thuê không hợp lệ.");
                }

                if (ModelState.IsValid)
                {
                    var ngayHienTai = DateTime.Now;
                    var phongMoi = new PHONG
                    {
                        MAPHONG = "PH00001",
                        MAND = Session["MaNguoiDung"] as string,
                        MALP = selectedRoomType,
                        TIEUDE = TieuDe,
                        DIACHI = DiaChi,
                        MOTA = mota,
                        GIATHUE = giaThue,
                        DIENTICH = dientich,
                        THOIGIANDANG = ngayHienTai,
                        TRANGTHAI = 0,
                        TIENICH = tienich
                    };

                    db.PHONGs.Add(phongMoi);
                    db.SaveChanges();

                    string maPhong = db.PHONGs
                    .Where(p => p.THOIGIANDANG.HasValue
                             && p.THOIGIANDANG.Value.Year == ngayHienTai.Year
                             && p.THOIGIANDANG.Value.Month == ngayHienTai.Month
                             && p.THOIGIANDANG.Value.Day == ngayHienTai.Day
                             && p.THOIGIANDANG.Value.Hour == ngayHienTai.Hour
                             && p.THOIGIANDANG.Value.Minute == ngayHienTai.Minute
                             && p.THOIGIANDANG.Value.Second == ngayHienTai.Second
                             && p.THOIGIANDANG.Value.Millisecond == ngayHienTai.Millisecond)
                    .Select(p => p.MAPHONG)
                    .FirstOrDefault();

                    if (Images != null)
                    {
                        string maHA = "HA00001";

                        foreach (var image in Images)
                        {
                            if (image != null && image.ContentLength > 0)
                            {
                                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                                var path = Path.Combine(Server.MapPath("~/Image/DuLieu/Phong/"), fileName);

                                image.SaveAs(path);

                                var hinhAnhMoi = new HINHANH
                                {
                                    MAHA = maHA,
                                    MAPHONG = maPhong,
                                    TENFILEANH = fileName
                                };

                                db.HINHANHs.Add(hinhAnhMoi);
                            }
                        }
                        db.SaveChanges();
                    }

                    return Json(new { success = true, message = "Phòng đã được thêm thành công." });
                }

                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return Json(new { success = false, message = "ModelState is not valid. Errors: " + string.Join(", ", errors) });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Đã xảy ra lỗi khi thêm phòng. Chi tiết lỗi: {ex.Message}" });
            }
        }
        [HttpPost]
        public ActionResult XoaPhong(string maPhong)
        {
            try
            {
                var hinhAnhList = db.HINHANHs.Where(h => h.MAPHONG == maPhong).ToList();
                foreach (var hinhAnh in hinhAnhList)
                {
                    db.HINHANHs.Remove(hinhAnh);
                }
                var phong = db.PHONGs.FirstOrDefault(p => p.MAPHONG == maPhong);

                if (phong != null)
                {
                    db.PHONGs.Remove(phong);
                    db.SaveChanges();
                    return RedirectToAction("QuanLyTinDang","NguoiDung");
                }

                return Json(new { success = false, message = "Không tìm thấy phòng để xóa." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Đã xảy ra lỗi khi xóa phòng và hình ảnh. Chi tiết lỗi: {ex.Message}" });
            }
        }

    }
}