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
    
    public partial class NGUOIDUNG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NGUOIDUNG()
        {
            this.DANHGIA_PHONG = new HashSet<DANHGIA_PHONG>();
            this.DANHGIA_UNGDUNG = new HashSet<DANHGIA_UNGDUNG>();
            this.PHONGs = new HashSet<PHONG>();
            this.TINTUCs = new HashSet<TINTUC>();
        }
    
        public string MAND { get; set; }
        public string MALTK { get; set; }
        public string TENND { get; set; }
        public string EMAIL { get; set; }
        public string TENTK { get; set; }
        public string MATKHAU { get; set; }
        public string SDT { get; set; }
        public string DIACHI { get; set; }
        public Nullable<System.DateTime> THOIGIANDANGNHAPLANCUOI { get; set; }
        public string TENFILEANH { get; set; }
        public Nullable<int> TRANGTHAI { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DANHGIA_PHONG> DANHGIA_PHONG { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DANHGIA_UNGDUNG> DANHGIA_UNGDUNG { get; set; }
        public virtual LOAITAIKHOAN LOAITAIKHOAN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHONG> PHONGs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TINTUC> TINTUCs { get; set; }
    }
}
