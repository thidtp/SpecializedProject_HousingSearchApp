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
    public class HomeController : Controller
    {
        QL_UDNHATROEntities db = new QL_UDNHATROEntities();
        public ActionResult Home()
        {
            // Lấy danh sách 9 phòng cuối cùng được thêm vào
            var roomData = db.PHONGs
                            .Include(r => r.HINHANHs)
                            .OrderByDescending(r => r.THOIGIANDANG) // Sắp xếp giảm dần theo thời gian thêm mới
                            .Take(9)
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
                            .ToList();
            return View(roomData);
        }
        public ActionResult getTinTuc()
        {
            var newsData = db.TINTUCs
                            .Include(r => r.HINHANHTINTUCs)
                            .OrderByDescending(r => r.NGAYDANG)
                            .Take(5)
                            .Select(r => new TINTUC_DTO
                            {
                                MaTinTuc = r.MATINTUC,
                                TieuDe = r.TIEUDE,
                                NoiDung = r.NOIDUNG,
                                NgayDang = (DateTime)r.NGAYDANG,
                                TenFileAnh = r.HINHANHTINTUCs.Select(h => h.TENFILEANH).FirstOrDefault()
                            })
                            .ToList();

            return PartialView("getTinTuc", newsData);
        }
        public ActionResult timKiemNangCao()
        {
            return PartialView();
        }
        public ActionResult getQuanHuyen()
        {
            var quanHuyenList = db.QUANHUYENs.Select(r => new QUANHUYEN_DTO
            {
                maQuanHuyen = r.MAQUANHUYEN,
                tenQuanHuyen = r.TENQUANHUYEN
            }).ToList();
            return PartialView("getQuanHuyen", quanHuyenList);

        }
        public ActionResult getPhuongXa(string maQuanHuyen)
        {
            var phuongXaList = db.PHUONGXAs.Where(r => r.MAQUANHUYEN == maQuanHuyen).Select(r => new PHUONGXA_DTO
            {
                MaPhuongXa = r.MAPHUONGXA,
                TenPhuongXa = r.TENPHUONGXA,
                MaQuanHuyen = r.MAQUANHUYEN
            }).ToList();
            return PartialView("getPhuongXa", phuongXaList);
        }
        public ActionResult getLoaiPhong()
        {
            var loaiPhongList = db.LOAIPHONGs.Select(r => new LOAIPHONG_DTO
            {
                MaLoaiPhong = r.MALP,
                TenLoaiPhong = r.TENLP
            }).ToList();
            return PartialView("getLoaiPhong", loaiPhongList);
        }
    }
}