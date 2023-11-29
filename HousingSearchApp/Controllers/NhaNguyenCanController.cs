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
    public class NhaNguyenCanController : Controller
    {
        // GET: NhaNguyenCan
        QL_UDNHATROEntities2 db = new QL_UDNHATROEntities2();

        public ActionResult Index(int? page)
        {
            string maLoaiPhong = "LP00002";
            int pageSize = 9;
            int pageNumber = (page ?? 1);

            var roomData = db.PHONGs
            .Include(r => r.HINHANHs)
            .Where(r => r.MALP == maLoaiPhong)
            .OrderBy(r => r.MAPHONG)
            .Select(r => new Phong_DTO
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
    }
}