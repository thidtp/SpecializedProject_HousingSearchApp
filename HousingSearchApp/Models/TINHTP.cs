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
    
    public partial class TINHTP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TINHTP()
        {
            this.QUANHUYENs = new HashSet<QUANHUYEN>();
        }
    
        public string MATINHTP { get; set; }
        public string TENTINHTP { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QUANHUYEN> QUANHUYENs { get; set; }
    }
}
