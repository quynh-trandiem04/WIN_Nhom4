//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Window
{
    using System;
    using System.Collections.Generic;
    
    public partial class TongDichVu
    {
        public int MaSuDung { get; set; }
        public Nullable<int> MaDatPhong { get; set; }
        public string MaDV { get; set; }
        public Nullable<int> SoLuong { get; set; }
    
        public virtual DatPhong DatPhong { get; set; }
        public virtual DichVu DichVu { get; set; }
    }
}