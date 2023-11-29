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
    public class TinTucController : Controller
    {
        // GET: TinTuc
        QL_UDNHATROEntities2 db = new QL_UDNHATROEntities2();
        public ActionResult Index(int? page)
        {
            int pageSize = 9;
            int pageNumber = (page ?? 1);

            var newsData = db.TINTUCs
            .Include(r => r.HINHANHTINTUCs)
            .OrderBy(r => r.MATINTUC)
            .Select(r => new TinTuc_DTO
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
    }
}