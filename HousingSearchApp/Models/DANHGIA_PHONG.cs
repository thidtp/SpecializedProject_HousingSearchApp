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
    
    public partial class DANHGIA_PHONG
    {
        public string MADGPHONG { get; set; }
        public string MAND { get; set; }
        public string MAPHONG { get; set; }
        public Nullable<int> DANHGIA { get; set; }
        public string BINHLUAN { get; set; }
    
        public virtual NGUOIDUNG NGUOIDUNG { get; set; }
        public virtual PHONG PHONG { get; set; }
    }
}
