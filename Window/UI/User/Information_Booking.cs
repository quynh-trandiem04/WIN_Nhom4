using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Window.BL_Layer;
using Window.UI_Giaodien;

namespace Window.UI.User
{
    public partial class Information_Booking : Form
    {
        private UC_Item_Booking_User ucItemBooking;
        string loggedInUsername = Giaodien.TenDangNhap_User.LoggedInUsername;
        int maphong;
        Datphong datPhong = new Datphong();
        public Information_Booking(UC_Item_Booking_User ucItemBooking)
        {
            InitializeComponent();
            string hoTen =  datPhong.HoTen(loggedInUsername);
            lb_tenkh.Text = hoTen;
            this.ucItemBooking = ucItemBooking;
            maphong = ucItemBooking.Maphong;
            string maloaiphong = ucItemBooking.Maloaiphong;
            int gia = ucItemBooking.Gia;
            lb_maphong.Text = maphong.ToString();
            lb_maloaiphong.Text = maloaiphong;
            lb_gia.Text = gia.ToString();
            lb_thoigiandat.Text = DateTime.Now.ToString();
        }
        private void Information_Booking_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn muốn thoát?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btn_datphong_Click_1(object sender, EventArgs e)
        {
            DateTime ngayDat = time_ngaydat.Value;
            DateTime ngayTra = time_ngaytra.Value;
            int songuoidat;
            if (!int.TryParse(txt_songuoi.Text, out songuoidat))
            {
                MessageBox.Show("Vui lòng nhập số người hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int makh = datPhong.GetMaKHFromTenDangNhap(loggedInUsername);
            int result = datPhong.DatPhongUser(makh, maphong, ngayDat,ngayTra, songuoidat);
            if (result == 1)
            {
                this.Close();
            }
        }

    }
}
