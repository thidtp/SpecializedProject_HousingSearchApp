using HousingSearchApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PagedList;
using System.IO;

namespace HousingSearchApp.Controllers
{
    public class TinTucController : Controller
    {
        QL_UDNHATROEntities db = new QL_UDNHATROEntities();
        // GET: TinTuc
        public ActionResult Index(int? page)
        {
            int pageSize = 9;
            int pageNumber = (page ?? 1);

            var newsData = db.TINTUCs
            .Include(r => r.HINHANHTINTUCs)
            .OrderBy(r => r.MATINTUC)
            .Select(r => new TINTUC_DTO
            {
                MaTinTuc = r.MATINTUC,
                TieuDe = r.TIEUDE,
                NoiDung = r.NOIDUNG,
                NgayDang = (DateTime)r.NGAYDANG,
                TenFileAnh = r.HINHANHTINTUCs.Select(h => h.TENFILEANH).FirstOrDefault()
            })
            .ToPagedList(pageNumber, pageSize);
            return View(newsData);
        }
        public ActionResult ChiTietTinTuc(string maTT) 
        {
            var tintuc = db.TINTUCs
                .Include(r => r.HINHANHTINTUCs)
                .FirstOrDefault(r => r.MATINTUC == maTT);

            if (tintuc == null)
            {
                return HttpNotFound();
            }
            return View(tintuc);
        }
        [HttpPost]
        public ActionResult ThemTinTuc(string TieuDe, string NoiDung, HttpPostedFileBase[] Images)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var ngayHienTai = DateTime.Now;
                    var tinTucMoi = new TINTUC
                    {
                        MATINTUC = "TT00001",
                        MAND = Session["MaNguoiDung"] as string,
                        TIEUDE = TieuDe,
                        NOIDUNG = NoiDung,
                        NGAYDANG = ngayHienTai
                    };

                    db.TINTUCs.Add(tinTucMoi);
                    db.SaveChanges();

                    string maTT = db.TINTUCs
                    .Where(p => p.NGAYDANG.HasValue
                                && p.NGAYDANG.Value.Year == ngayHienTai.Year
                                && p.NGAYDANG.Value.Month == ngayHienTai.Month
                                && p.NGAYDANG.Value.Day == ngayHienTai.Day
                                && p.NGAYDANG.Value.Hour == ngayHienTai.Hour
                                && p.NGAYDANG.Value.Minute == ngayHienTai.Minute
                                && p.NGAYDANG.Value.Second == ngayHienTai.Second)
                    .Select(p => p.MATINTUC)
                    .FirstOrDefault();

                    if (Images != null)
                    {
                        string maHATT = "HATT00001";

                        foreach (var image in Images)
                        {
                            if (image != null && image.ContentLength > 0)
                            {
                                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                                var path = Path.Combine(Server.MapPath("~/Image/DuLieu/TinTuc/"), fileName);

                                image.SaveAs(path);

                                var hinhAnhMoi = new HINHANHTINTUC
                                {
                                    MAHTT = maHATT,
                                    MATINTUC = maTT,
                                    TENFILEANH = fileName
                                };

                                db.HINHANHTINTUCs.Add(hinhAnhMoi);
                            }
                        }
                        db.SaveChanges();
                    }

                    return Json(new { success = true, message = "Tin tức đã được thêm thành công." });
                }

                var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return Json(new { success = false, message = "ModelState is not valid. Errors: " + string.Join(", ", errors) });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Đã xảy ra lỗi khi thêm phòng. Chi tiết lỗi: {ex.Message}" });
            }
        }
    }
}