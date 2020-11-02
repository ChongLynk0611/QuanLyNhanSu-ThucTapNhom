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
using Telerik.WinControls.UI;
using Telerik.Data;
using QuanLyNhanSu.Classes;

namespace QuanLyNhanSu
{
    public partial class frmchonbcnv : RadForm
    {
        public SqlConnection sc = frmMain.me.sql.connectSQL();
        public SqlDataAdapter sda = new SqlDataAdapter();
        public DataSet ds = new DataSet();
        clsdmpb cphong = new clsdmpb();
        clsDMTo cto = new clsDMTo();
        static public string maphong = "";
        static public string mato = "";
        static public string manv = "";
        public frmchonbcnv()
        {
            InitializeComponent();
            cbophong.DataSource = cphong.taocombo();
            cbophong.DisplayMember = "tbl_phongban.tenphong";
            cbophong.ValueMember = "tbl_phongban.maphong";
        }
        //kiểm tra có tồn tại nhân viên nào thuộc tổ này hay không
        private bool kiemtra(string maphong, string mato)
        {
            ds.Clear();
            string sel = "select * from tbl_nhanvien where maphong ='" + maphong
                          + "' and mato ='" + mato + "'";
            sc.Open();
            SqlCommand scmd = new SqlCommand(sel, sc);
            object o = scmd.ExecuteScalar();
            sc.Close();
            if (o != null)
                return true;
            else
                return false;
        }
        //kiểm tra có tồn tại nhân viên nào thuộc tổ này hay không
        private bool kiemtra(string maphong)
        {
            ds.Clear();
            string sel = "select * from tbl_nhanvien where maphong ='" + maphong + "'";
            sc.Open();
            SqlCommand scmd = new SqlCommand(sel, sc);
            object o = scmd.ExecuteScalar();
            sc.Close();
            if (o != null)
                return true;
            else
                return false;
        }
        //kiểm tra xem nhân viên đó có trong bảng tbl_nhân viên hay không?
        private bool kiemtranv(string manv)
        {
            ds.Clear();
            string sel = "select * from tbl_nhanvien where manv ='" + manv + "'";
            sc.Open();
            SqlCommand scmd = new SqlCommand(sel, sc);
            object o = scmd.ExecuteScalar();
            sc.Close();
            if (o != null)
                return true;
            else
                return false;
        }
    }
}
