//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HousingSearchApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PHONG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PHONG()
        {
            this.DANHGIA_PHONG = new HashSet<DANHGIA_PHONG>();
            this.HINHANHs = new HashSet<HINHANH>();
        }
    
        public string MAPHONG { get; set; }
        public string MAND { get; set; }
        public string MALP { get; set; }
        public string TIEUDE { get; set; }
        public string DIACHI { get; set; }
        public string MOTA { get; set; }
        public Nullable<int> GIATHUE { get; set; }
        public string TIENICH { get; set; }
        public Nullable<int> DIENTICH { get; set; }
        public Nullable<System.DateTime> THOIGIANDANG { get; set; }
        public Nullable<int> TRANGTHAI { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DANHGIA_PHONG> DANHGIA_PHONG { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HINHANH> HINHANHs { get; set; }
        public virtual LOAIPHONG LOAIPHONG { get; set; }
        public virtual NGUOIDUNG NGUOIDUNG { get; set; }
    }
}
