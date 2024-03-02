using HousingSearchApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PagedList;
using Antlr.Runtime;
using System.IO;
using System.Data.Entity.Infrastructure;
using System.Web.Helpers;
using System.Data.Entity.Validation;

namespace HousingSearchApp.Controllers
{
    public class QuanLyController : Controller
    {
        QL_UDNHATROEntities db = new QL_UDNHATROEntities();
        // GET: QuanLy
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BaiChuaKiemDuyet()
        {
            var baiChuaKiemDuyet = db.PHONGs
            .Include(r => r.HINHANHs)
            .Where(r => r.TRANGTHAI == 0)
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
            })
            .ToList();
            return PartialView("BaiChuaKiemDuyet", baiChuaKiemDuyet);
        }
        public ActionResult BaiDaKiemDuyet()
        {
            var baiDaKiemDuyet = db.PHONGs
            .Include(r => r.HINHANHs)
            .Where(r => r.TRANGTHAI == 1)
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
            })
            .ToList();
            return PartialView("BaiDaKiemDuyet", baiDaKiemDuyet);
        }
        public ActionResult Chitietphong(string maPhong)
        {
            var listPhong = db.PHONGs
                .Include(r => r.HINHANHs)
                .FirstOrDefault(r => r.MAPHONG.Equals(maPhong));
            return View(listPhong);
        }
        [HttpPost]
        public ActionResult KiemDuyetPhong(string maphong)
        {
            try
            {
                var phong = db.PHONGs.FirstOrDefault(p => p.MAPHONG == maphong);

                if (phong != null)
                {
                    phong.TRANGTHAI = (phong.TRANGTHAI == 0) ? 1 : 0;
                    db.SaveChanges();

                    return Json(new { success = true, message = "Duyệt thành công" });
                }

                return Json(new { success = false, message = "Không tìm thấy phòng để kiểm duyệt." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Đã xảy ra lỗi: {ex.Message}" });
            }
        }



        public ActionResult QuanLyTaiKhoan()
        {
            return View();

        }
        [HttpGet]
        public ActionResult getTaiKhoan(string maLTK)
        {
            List<NGUOIDUNG> danhSachTaiKhoan;

            if (maLTK == "1")
            {
                // Lấy hết tài khoản
               danhSachTaiKhoan = db.NGUOIDUNGs.ToList();
            }
            else
            {
                // Lấy tài khoản theo mã loại tài khoản
                danhSachTaiKhoan = db.NGUOIDUNGs.Where(u => u.MALTK == maLTK).ToList();
            }

            return PartialView("getTaiKhoan", danhSachTaiKhoan);
        }
        public ActionResult ThemTaiKhoan()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TaoTaiKhoan(string tenTK, string email, string matkhau, string xacnhanmatkhau, string loaiTK, string tenND, string SoDienThoai, string diachi, HttpPostedFileBase avatar)
        {
            try
            {
                string fileName = "default_image.jpg"; 
                if (ModelState.IsValid)
                {
                    if (db.NGUOIDUNGs.Any(u => u.TENTK == tenTK))
                    {
                        return Json(new { success = false, message = "Tên tài khoản đã tồn tại." });
                    }

                    if (avatar != null && avatar.ContentLength > 0)
                    {
                        fileName = Guid.NewGuid().ToString() + Path.GetExtension(avatar.FileName);
                        var path = Path.Combine(Server.MapPath("~/Image/DuLieu/NguoiDung/"), fileName);
                        avatar.SaveAs(path);
                    }
                    else
                    {
                        return Json(new { success = false, message = "Ảnh đại diện không được để trống." });
                    }

                    var nguoiDungMoi = new NGUOIDUNG
                    {
                        MAND = "ND0001",
                        TENTK = tenTK,
                        TENND = tenND,
                        EMAIL = email,
                        SDT = SoDienThoai,
                        MATKHAU = matkhau,
                        MALTK = loaiTK,
                        DIACHI = diachi,
                        TRANGTHAI = 1,
                        TENFILEANH = fileName
                    };

                    db.NGUOIDUNGs.Add(nguoiDungMoi);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Thêm thành công" });
                }

                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return Json(new { success = false, message = "Dữ liệu không hợp lệ", errors });
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Console.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }

                return Json(new { success = false, message = "Có lỗi xảy ra khi thêm tài khoản. Lỗi validation.", errors = ex.EntityValidationErrors });
            }
        }


        public ActionResult DangKy(string tentaiKhoan, string tenNguoiDung, string EmAil, string soDienThoai, string matKhau, string selectedOption)
        {
            try
            {
                if (ModelState.IsValid)
                {
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
        [HttpPost]
        public ActionResult KhoaTaiKhoan(List<string> selectedAccounts)
        {
            try
            {
                foreach (var mand in selectedAccounts)
                {
                   
                    var taiKhoan = db.NGUOIDUNGs.FirstOrDefault(u => u.MAND == mand);
                    if (taiKhoan != null)
                    {
                        taiKhoan.TRANGTHAI = 0; 
                    }
                }

                db.SaveChanges(); 

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu cần
                return Json(new { success = false, error = ex.Message });
            }
        }
        public ActionResult ThongTinTaiKhoan()
        {
            var maNguoiDung = Session["MaNguoiDung"] as string;
            var thongtinNguoiDung = db.NGUOIDUNGs.Where(r => r.MAND.Equals(maNguoiDung)).ToList();
            return View(thongtinNguoiDung);
        }
        public ActionResult DangTin()
        {
            return View();
        }
    }
}