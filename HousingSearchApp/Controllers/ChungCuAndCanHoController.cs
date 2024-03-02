using HousingSearchApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PagedList;

namespace HousingSearchApp.Controllers
{
    public class ChungCuAndCanHoController : Controller
    {
        // GET: ChungCuAndCanHo
        QL_UDNHATROEntities db = new QL_UDNHATROEntities();

        public ActionResult Index(int? page)
        {
            string maLoaiPhong = "LP00003";
            int pageSize = 9;
            int pageNumber = (page ?? 1);

            var roomData = db.PHONGs
            .Include(r => r.HINHANHs)
            .Where(r => r.MALP == maLoaiPhong)
            .OrderBy(r => r.MAPHONG)
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
                TenFileAnh = r.HINHANHs.Select(h => h.TENFILEANH).FirstOrDefault()
            })
            .ToPagedList(pageNumber, pageSize);


            return View(roomData);
        }
        public ActionResult ChiTietPhong(string maPhong)
        {
            var phong = db.PHONGs
                .Include(r => r.HINHANHs)
                .FirstOrDefault(r => r.MAPHONG == maPhong);

            if (phong == null)
            {
                return HttpNotFound();
            }
            return View(phong);
        }
        private string tachQuanPhuong(string diaChi)
        {
            string[] quan = diaChi.Split(',');
            if (quan.Length > 0)
            {
                return quan[1].Trim();
            }
            return "Không xác định";
        }
        public ActionResult phongLienQuan(string maPhong)
        {
            var phong = db.PHONGs.Find(maPhong);

            if (phong == null)
            {
                return HttpNotFound();
            }

            string dc = tachQuanPhuong(phong.DIACHI);
            string maLP = phong.MALP;
            int giaThue = (int)phong.GIATHUE;

            var phongLienQuan = db.PHONGs
                .Include(r => r.HINHANHs)
                .Where(r => r.MAPHONG != maPhong && DbFunctions.Like(r.DIACHI, "%" + dc + "%") || (r.MALP == maLP && r.GIATHUE <= giaThue)).Take(5)
                .ToList();
            return PartialView("phongLienQuan", phongLienQuan);
        }
    }
}