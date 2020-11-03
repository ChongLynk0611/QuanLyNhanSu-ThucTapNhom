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
using QuanLyNhanSu.Class;
namespace QuanLyNhanSu
{
    public partial class frmchonbcluong : RadForm
    {
        public SqlConnection sc = frmMain.me.sql.connectSQL();
        public SqlDataAdapter sda = new SqlDataAdapter();
        public DataSet ds = new DataSet();
        clsdmpb cphong = new clsdmpb();
        clsDMTo cto = new clsDMTo();
        static public string thangbc = "";
        static public string nambc = "";
        static public string thangbh = "";
        static public string nambh = "";
        static public string maphong = "";
        static public string mato = "";
        public frmchonbcluong()
        {
            InitializeComponent();

            for (int i = 1; i <= 12; i++)
            {
                cbothangbh.Items.Add(i);
            }
            for (int i = 2008; i <= 2100; i++)
            {
                cbonambh.Items.Add(i);
            }
            for (int i = 1; i <= 12; i++)
            {
                cbothangbc.Items.Add(i);
            }
            for (int i = 2008; i <= 2100; i++)
            {
                cbonambc.Items.Add(i);
            }
            cbophong.DataSource = cphong.taocombo();
            cbophong.DisplayMember = "tbl_phongban.tenphong";
            cbophong.ValueMember = "tbl_phongban.maphong";
        }
        //kiểm tra bảng lương có tồn tại hay không
        private bool kiemtra(string thang, string nam)
        {
            ds.Clear();
            string sel = "select * from tbl_BangLuong where thang ='" + thang
                          + "' and nam ='" + nam + "'";
            sc.Open();
            SqlCommand scmd = new SqlCommand(sel, sc);
            object o = scmd.ExecuteScalar();
            sc.Close();
            if (o != null)
                return true;
            else
                return false;
        }

        static public DataSet bcbaohiem(string thang, string nam)
        {
            SqlConnection sc = frmMain.me.sql.connectSQL();
            SqlDataAdapter sda = new SqlDataAdapter();
            DataSet ds = new DataSet();
            ds.Clear();
            sc.Open();
            string sel = "select * from tbl_BangLuong where thang ='" + thang
                      + "' and nam ='" + nam + "'";
            SqlCommand scmd = new SqlCommand(sel, sc);
            sda.SelectCommand = scmd;
            sda.Fill(ds, "baohiem");
            string sel1 = "select * from tbl_nhanvien";
            SqlCommand scmd1 = new SqlCommand(sel1, sc);
            sda.SelectCommand = scmd1;
            sda.Fill(ds, "nhanvien");
            sc.Close();
            ds.WriteXmlSchema("bcbaohiem.xml");
            return ds;
        }
        private void cbophong_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboto.DataSource = cto.taocombo(cbophong.SelectedValue.ToString());
            cboto.DisplayMember = "DanhMucto.tento";
            cboto.ValueMember = "DanhMucto.mato";
            cboto.Text = "";
        }



    }
}
