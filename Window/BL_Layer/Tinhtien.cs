using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Window.UI.User;

namespace Window.BL_Layer
{
    internal class Tinhtien
    {
        QLKSCKEntities1 db = new QLKSCKEntities1();
        public int tienphong(int maDatPhong)
        {
            var datPhong = db.DatPhongs.FirstOrDefault(dp => dp.MaDatPhong == maDatPhong);

            if (datPhong != null && datPhong.NgayDat != null && datPhong.NgayTra != null)
            {
                var phong = db.Phongs.FirstOrDefault(p => p.MaPhong == datPhong.MaPhong);

                if (phong != null && phong.LoaiPhong != null)
                {
                    var loaiPhong = db.LoaiPhongs.FirstOrDefault(lp => lp.MaLoaiPhong == phong.LoaiPhong);

                    if (loaiPhong != null && loaiPhong.Gia != null)
                    {
                        TimeSpan thoiGianDat = (TimeSpan)(datPhong.NgayTra - datPhong.NgayDat ?? TimeSpan.Zero);
                        int soNgayDat = (int)thoiGianDat.TotalDays;
                        if (soNgayDat >= 0)
                        {
                            int tienPhong = soNgayDat * loaiPhong.Gia.Value;
                            return tienPhong;
                        }
                    }
                }
            }
            return -1;
        }


        public int tiendichvu(string maDichVu, int soLuong)
        {
            int tongTienDichVu = 0;
            var dichVu = db.DichVus.FirstOrDefault(dv => dv.MaDV == maDichVu);

            if (dichVu != null)
            {
                tongTienDichVu = soLuong * Convert.ToInt32(dichVu.Gia);
            }
            return tongTienDichVu;
        }
    }
}
