using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Window.BL_Layer;
using static Window.BL_Layer.Datphong;

namespace Window.UI.User
{
    public partial class UC_Item_Booking_User : UserControl
    {
        private string appDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;

        // Khai báo các thuộc tính public để lưu thông tin của phòng
        public int Maphong { get; private set; }
        public int Gia { get; private set; }
        public string Maloaiphong { get; private set; }

        public UC_Item_Booking_User(Phong f)
        {
            InitializeComponent();
            Maphong = f.MaPhong;
            Gia = f.LoaiPhong1.Gia ?? 0;
            Maloaiphong = f.LoaiPhong1.MaLoaiPhong.ToString();
            lb_maphong.Text = Maphong.ToString();
            lb_maloaiphong.Text = Maloaiphong;
            lb_gia.Text = Gia.ToString();
            string image1 = Path.Combine(appDirectory, f.LoaiPhong1.Anh);
            pic_Anh.Image = Image.FromFile(image1);
        }

        private void btn_booking_Click(object sender, EventArgs e)
        {
            Information_Booking f = new Information_Booking(this);
            f.Show();
        }
    }
}
