using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Window.UI.User;
using Window.UI_Giaodien;
using System.Drawing;
using System.Collections.Generic;

namespace Window.BL_Layer
{
    
    internal class Datphong
    {

        QLKSCKEntities1 db = new QLKSCKEntities1();
        string appDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        
        public static class Maphong_User
        {
            public static int Maphong_booking { get; set; }
        }

        public void SaveImage(PictureBox image, out string filename)
        {
            filename = string.Empty;
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                image.Image = Image.FromFile(opf.FileName);
                filename = Path.GetFileName(opf.FileName);
                string appDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
                string dest = Path.Combine(appDirectory, filename);
                File.Copy(opf.FileName, dest, true);
            }
        }
        public int GetMaKHFromTenDangNhap(string tenDangNhap)
        {
            var khachHang = db.KhachHangs.FirstOrDefault(kh => kh.TenDangNhap == tenDangNhap);
            return khachHang != null ? khachHang.MaKH : 0;
        }

        public (string MaLoaiPhong, int Gia) GetLoaiPhongGiaFromMaphong(int maphong)
        {
            using (var context = new QLKSCKEntities1())
            {
                var query = from p in context.Phongs
                            join lp in context.LoaiPhongs on p.LoaiPhong equals lp.MaLoaiPhong
                            where p.MaPhong == maphong
                            select new { lp.MaLoaiPhong, lp.Gia };

                var result = query.FirstOrDefault();

                if (result != null)
                {
                    return (result.MaLoaiPhong, result.Gia??0);
                }
                else
                {
                    return (null, 0);
                }
            }
        }
        public void DSBooking(FlowLayoutPanel f)
        {
            var roomsDetails = (from room1 in db.Phongs
                                join room2 in db.DatPhongs
                                on room1.MaPhong equals room2.MaPhong into room2Group
                                from room2 in room2Group.DefaultIfEmpty()
                                where room1.TinhTrang == "empty" && (room2 == null || room2.TinhTrang == "finish")
                                select room1).Distinct().ToList();
            List<int> addedRoomIds = new List<int>();

            foreach (var room in roomsDetails)
            {
                if (!addedRoomIds.Contains(room.MaPhong))
                {
                    UC_Item_Booking_User bookingRoomItem = new UC_Item_Booking_User(room);
                    f.Controls.Add(bookingRoomItem);
                    addedRoomIds.Add(room.MaPhong);
                }
            }
        }

        public int DatPhongUser(int makh, int maPhong, DateTime ngayDat, DateTime ngayTra, int songuoidat)
        {
            if (ngayTra < ngayDat)
            {
                MessageBox.Show("Ngày trả phòng phải lớn hơn hoặc bằng ngày đặt phòng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            if (ngayDat >= DateTime.Now)
            {
                MessageBox.Show("Ngày đặt phòng phải lớn hơn thời gian hiện tại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }

            KhachHang khachHang = db.KhachHangs.FirstOrDefault(kh => kh.MaKH == makh);
            if (khachHang == null)
            {
                MessageBox.Show("Không tìm thấy thông tin tài khoản khách hàng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }

            Phong phong = db.Phongs.FirstOrDefault(p => p.MaPhong == maPhong);
            if (phong == null)
            {
                MessageBox.Show("Không tìm thấy thông tin phòng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }

            LoaiPhong loaiPhong = db.LoaiPhongs.FirstOrDefault(lp => lp.MaLoaiPhong == phong.LoaiPhong);
            if (loaiPhong == null)
            {
                MessageBox.Show("Không tìm thấy thông tin loại phòng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }

            if (songuoidat > loaiPhong.SoNguoi)
            {
                MessageBox.Show("Số người đặt phòng vượt quá sức chứa của phòng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            DatPhong datPhong = new DatPhong
            {
                MaKH = khachHang.MaKH,
                MaPhong = maPhong,
                NgayDat = ngayDat,
                NgayTra = ngayTra,
                ThoiGianDat = DateTime.Now
            };
            db.DatPhongs.Add(datPhong);
            phong.TinhTrang = "wait";
            datPhong.TinhTrang = "unfinish";
            db.SaveChanges();

            MessageBox.Show("Đặt phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return 1;
        }
        public string HoTen(string tenDangNhap)
        {
            KhachHang khachHang = db.KhachHangs.FirstOrDefault(kh => kh.TenDangNhap == tenDangNhap);
            return khachHang != null ? khachHang.HoTen : null;
        }
    }
}
