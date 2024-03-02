using HousingSearchApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PagedList;
using System.Data.Entity.Validation;


namespace HousingSearchApp.Controllers
{
    public class DanhGiaController : Controller
    {
        QL_UDNHATROEntities db = new QL_UDNHATROEntities();
        // GET: DanhGia
        public ActionResult Index(string maPhong)
        {
            var danhGiaList = db.DANHGIA_PHONG.Where(r => r.MAPHONG.Equals(maPhong))
                .Join(db.NGUOIDUNGs, dg => dg.MAND, nd => nd.MAND, (dg, nd) => new { dg, nd })
                .Select(dg1 => new DANHGIA_DTO
                {
                    MADGphong = dg1.dg.MADGPHONG,
                    MaNguoiDung = dg1.dg.MAND,
                    MaPhong = dg1.dg.MAPHONG,
                    DanhGia = (int)dg1.dg.DANHGIA,
                    BinhLuan = dg1.dg.BINHLUAN,
                    tenNguoiDung = dg1.nd.TENND,
                    tenFileAnh = dg1.nd.TENFILEANH
                }).ToList();
            ViewBag.MaPhong = maPhong;
            return PartialView("Index", danhGiaList);
        }
        [HttpPost]
        public ActionResult ThemBinhLuan(string binhluan, int danhgia, string maNguoiDung, string maPhong)
        {
            try
            {
                var newDanhGia = new DANHGIA_PHONG
                {
                    MADGPHONG = "MADGP000", 
                    MAND = maNguoiDung,
                    MAPHONG = maPhong,
                    DANHGIA = danhgia,
                    BINHLUAN = binhluan,
                };
                db.DANHGIA_PHONG.Add(newDanhGia);
                db.SaveChanges();
                return Json(new { success = true, message = "Thêm bình luận thành công" });
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
    
                        Console.WriteLine($"Property: {validationError.PropertyName}, Error: {validationError.ErrorMessage}");
                    }
                }
                return RedirectToAction("Error", "Home");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return Json(new { success = false, message = "Xóa bình luận không thành công. Lỗi: " + ex.Message });
            }

        }
        [HttpPost]
        public ActionResult XoaBinhLuan(string madgphong)
        {
            try
            {
                var mand = System.Web.HttpContext.Current.Session["MaNguoiDung"];
                var danhGiaToDelete = db.DANHGIA_PHONG.FirstOrDefault(r => r.MADGPHONG == madgphong);

                if (danhGiaToDelete == null)
                {
                    return HttpNotFound();
                }
                if (danhGiaToDelete.MAND.ToString() == mand.ToString())
                {
                    db.DANHGIA_PHONG.Remove(danhGiaToDelete);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Xóa bình luận thành công" });
                }
                else
                {
                    return Json(new { success = false, message = "Bạn không có quyền xóa bình luận này" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Xóa bình luận không thành công. Lỗi: " + ex.Message });
            }
        }

    }
}