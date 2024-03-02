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
    public class TimKiemController : Controller
    {
        QL_UDNHATROEntities db = new QL_UDNHATROEntities();
        // GET: TimKiem
        [HttpGet]
        public ActionResult TimKiem(string quanHuyen, string phuongXa, string maloaiPhong, int giaNhoNhat, int giaLonNhat, int dienTichNhoNhat, int dienTichLonNhat, int? page)
        {
            int pageSize = 9;
            int pageNumber = page ?? 1;

            IQueryable<PHONG_DTO> query = db.PHONGs
                .Include(r => r.HINHANHs)
                .OrderBy(r => r.MAPHONG)
                .Select(t => new PHONG_DTO
                {
                    MaPhong = t.MAPHONG,
                    MaNguoiDung = t.MAND,
                    MaLoaiPhong = t.MALP,
                    TieuDe = t.TIEUDE,
                    DiaChi = t.DIACHI,
                    MoTa = t.MOTA,
                    GiaThue = t.GIATHUE,
                    TienIch = t.TIENICH,
                    DienTich = t.DIENTICH,
                    TenLoaiPhong = t.LOAIPHONG.TENLP,
                    TenNguoiDung = t.NGUOIDUNG.TENND,
                    TenFileAnh = t.HINHANHs.Select(h => h.TENFILEANH).ToList().FirstOrDefault()
                });

            if (quanHuyen != "null")
            {
                query = query.Where(r => r.DiaChi.ToUpper().Contains(quanHuyen.ToUpper()));
            }

            if (phuongXa != "null")
            {
                query = query.Where(r => r.DiaChi.ToUpper().Contains(phuongXa.ToUpper()));
            }

            if (maloaiPhong != "null")
            {
                query = query.Where(r => r.MaLoaiPhong == maloaiPhong);
            }

            if (giaNhoNhat >= 0)
            {
                query = query.Where(r => r.GiaThue >= giaNhoNhat);
            }

            if (giaLonNhat > 0)
            {
                query = query.Where(r => r.GiaThue <= giaLonNhat);
            }

            if (dienTichNhoNhat > 0)
            {
                query = query.Where(r => r.DienTich >= dienTichNhoNhat);
            }

            if (dienTichLonNhat > 0)
            {
                query = query.Where(r => r.DienTich <= dienTichLonNhat);
            }

            var timKiemList = query.ToPagedList(pageNumber, pageSize);
            return View("TimKiem", timKiemList);
        }

    }
}