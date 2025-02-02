using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Diagnostics;
using System.ServiceProcess;
using QuanLyNhanSu.Class;
namespace QuanLyNhanSu
{
    public partial class frmDangNhap : Form
    {
        QuanLyNhanSu.Class.readFile c = new QuanLyNhanSu.Class.readFile();
        QuanLyNhanSu.Class.sql sql = new sql();
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {

            if (txtUserName.Text == "")
            {

                RadMessageBox.Show("\nTên đăng nhập không được bỏ trống !\n", "Thông Báo", MessageBoxButtons.OK, RadMessageIcon.Exclamation);
                txtUserName.Focus();


            }
            else
            {
                if (txtPassword.Text == "")
                {
                    RadMessageBox.Show("\nMật khẩu không được bỏ trống !\n", "Thông Báo", MessageBoxButtons.OK, RadMessageIcon.Exclamation);
                    txtUserName.Focus();

                }
                else
                {
                    string u = txtUserName.Text.Trim();
                    string p = txtPassword.Text.Trim();
                    SqlConnection con = sql.connectSQL();
                    con.Open();
                    SqlCommand scmd = new SqlCommand(
                    "select ID,pass from tbl_user where " +
                    " ID = '" + u + "' and " +
                    " pass = '" + p + "' ", con);
                    SqlDataReader sdr =
                        scmd.ExecuteReader();


                    if (sdr.Read())
                    {
                        string strServerName = "";
                        string strDatabase = "";
                        string strUserName = "";
                        string strPassword = "";

                        string dbu = sdr.GetString(0).Trim();
                        string dbp = sdr.GetString(1).Trim();

                        sdr.Close();
                        con.Close();
                        c.readfile();

                        if (c.strLine1.Contains("Server:") == true) strServerName = c.strLine1.Substring(7);
                        if (c.strLine2.Contains("Database:") == true) strDatabase = c.strLine2.Substring(9);
                        if (c.strLine3.Contains("Username:") == true) strUserName = c.strLine3.Substring(9);
                        if (c.strLine4.Contains("Password:") == true) strPassword = c.strLine4.Substring(9);

                        frmMain.me.sql =
                            new sql(strServerName, strDatabase, dbu, dbp);

                        if (dbu != "administrator")
                        {
                            frmMain.me.btnPhanQuyen.Enabled = false;
                            frmMain.me.btnTaoNguoiDung.Enabled = false;
                            //MessageBox.Show(dbu+" "+dbp);
                        }
                        else
                        {
                            //MessageBox.Show(dbu + " " + dbp);
                            frmMain.me.btnPhanQuyen.Enabled = true;
                            frmMain.me.btnTaoNguoiDung.Enabled = true;
                        }
                        frmMain.me.lbNguoiDung.Text = "Người đăng nhập :" + u;
                        frmMain.me.lbTrangThai.Text = "Trạng Thái : Sẳn Sàng";
                        frmMain.me.lbTrangThai.Image = null;
                        this.Close();

                    }
                    else
                    {
                        sdr.Close();
                        con.Close();
                        RadMessageBox.Show("\nBạn Chưa đăng ký hoặc mật mã không đúng !\n", "Thông báo",MessageBoxButtons.OK,RadMessageIcon.Exclamation);

                    }
                }


            }
           
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();

           

        }
        public int a;
        public bool b = false;
       
        private void button1_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("osk");
            if (b == false)
            {
                
                a = System.Diagnostics.Process.Start("osk").Id;
                this.txtPassword.Text = "";
                b = true;
                this.txtPassword.Focus();
            }
            else
            {
                System.Diagnostics.Process.GetProcessById(a).Kill();
                b = false;
            }
         
          

        }

        private void frmDangNhap_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (b == true && a !=null)
            {
                System.Diagnostics.Process.GetProcessById(a).Kill();
                b = false;
            }
            
        }

     

      



       

       

      

        

        
    }
}