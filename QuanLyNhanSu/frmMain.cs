using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Telerik.WinControls;
using Telerik.Data;
using QuanLyNhanSu.Class;

namespace QuanLyNhanSu
{
    public partial class frmMain : RadRibbonForm
    {
        //#
        public static frmMain me;
        public QuanLyNhanSu.Class.sql sql;
        //#
        public frmMain()
        {
            readFile c = new readFile();
            c.readfile();
            string strServerName = "";
            string strDatabase = "";
            if (c.strLine1.Contains("Server:") == true) strServerName = c.strLine1.Substring(7);
            if (c.strLine2.Contains("Database:") == true) strDatabase = c.strLine2.Substring(9);

            InitializeComponent();
            sql = new sql(strServerName, strDatabase,
                "U0", "123");
            me = this;

        }
        private Form KiemTraTonTai(Type formType)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == formType)
                    return f;
            }
            return null;
        }
        private string ChuoiTruyCapForm(string ten)
        {
            return "Select * from tbl_user_form U inner join " +
                  "tbl_DMform F on U.IDform=F.ID where U.username='" + sql.GetUser() + "' and  F.tenform='" + ten + "' and U.rights=1";

        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
           

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            frmConfig obj = new frmConfig();
            obj.ShowDialog();



        }
        public string layngay()
        {
            string sel = "select top 1 percent Start from Appointments order by Start desc";
            SqlConnection con = frmMain.me.sql.connectSQL();
            con.Open();
            SqlCommand sel1 = new SqlCommand(sel, con);
            object o = sel1.ExecuteScalar();
            con.Close();
            return o.ToString();

        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
           
        }

        private void btnTyLeLuongThang_Click(object sender, EventArgs e)
        {
        }

        private void btnChamCong_Click(object sender, EventArgs e)
        {
            
        }

        private void btnChucVu_Click(object sender, EventArgs e)
        {
            
        }

        private void btnPhanQuyen_Click(object sender, EventArgs e)
        {
           
        }

        private void btnBCLuongThang_Click(object sender, EventArgs e)
        {
           
        }

        private void btnBCNhanSu_Click(object sender, EventArgs e)
        {
           
        }

        private void btnHDHetHan_Click(object sender, EventArgs e)
        {
           
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {



        }

        private void btnDoiMatKau_Click(object sender, EventArgs e)
        {
           
        }

        private void btnTonGiao_Click(object sender, EventArgs e)
        {
           
        }

        private void btnDanToc_Click(object sender, EventArgs e)
        {
            
        }

        private void btnTrinhDo_Click(object sender, EventArgs e)
        {
           
        }

        private void btnChuyenMon_Click(object sender, EventArgs e)
        {
           
        }

        private void btnNgoaiNgu_Click(object sender, EventArgs e)
        {
           
        }

        private void btnPhongBan_Click(object sender, EventArgs e)
        {
            
        }

        private void btnTo_Click(object sender, EventArgs e)
        {
           
        }

        private void btnLoaiHopDong_Click(object sender, EventArgs e)
        {
           
        }

        private void btnTaoNguoiDung_Click(object sender, EventArgs e)
        {
           
        }

        private void btnThongTin_Click(object sender, EventArgs e)
        {
        }

        private void btnDMHieuQua_Click(object sender, EventArgs e)
        {
          
        }

        private void radImageButtonElement1_Click(object sender, EventArgs e)
        {
           
        }

        private void radMenuItem1_Click(object sender, EventArgs e)
        {
           
        }


    }
}
