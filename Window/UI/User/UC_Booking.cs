using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Window.BL_Layer;
using Window.UI_Giaodien;

namespace Window.UI.User
{
    public partial class UC_Booking : UserControl
    {
        private Datphong datphong;

        public UC_Booking()
        {
            InitializeComponent();
            Datphong datphong = new Datphong();
            datphong.DSBooking(flowLayoutPanel1);
        }
    }
}
