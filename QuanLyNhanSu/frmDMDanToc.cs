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
    public partial class frmDMDanToc : RadForm
    {
        //khai báo biến toàn cục
        clsDMDanToc  c = new clsDMDanToc();
        int k = 0;
        public frmDMDanToc()
        {
            
            InitializeComponent();
            c.laydl("tbl_DMDanToc");
            //lấy dữ liệu ra textbox
            txtmadantoc.DataBindings.Add("text",c.ds , "tbl_DMDanToc.MaDT");
            txttendantoc.DataBindings.Add("text",c.ds, "tbl_DMDanToc.TenDT");
            dgv_DMDanToc.DataSource = c.ds;
            dgv_DMDanToc.DataMember = "tbl_DMDanToc";
            dgv_DMDanToc.Columns[0].HeaderText = "Mã dân tộc";
            dgv_DMDanToc.Columns[1].HeaderText = "Tên dân tộc";
            

        }
        //set bộ nút
        public void setbutton(bool b)
        {
            cmdCapnhat.Enabled = b;
            cmdthem.Enabled = b;
            cmdthoat.Enabled = b;
            cmdxoa.Enabled = b;
            cmdluu.Enabled = !b;
        }

        private void frmDMDanToc_Load(object sender, EventArgs e)
        {
            setbutton(true);
        }

        private void txtmadantoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar))
            {
                RadMessageBox.Show("\nBạn phải nhập số !\n", "Thông báo", MessageBoxButtons.OK, RadMessageIcon.Exclamation);       
            }       
        }
        private void cmdthem_Click_1(object sender, EventArgs e)
        {
            txtmadantoc.Text = "";
            txttendantoc.Text = "";
            txtmadantoc.ReadOnly = false;
            txttendantoc.ReadOnly = false;
            txtmadantoc.Focus();
            setbutton(false);
            k = 1;
        }

        private void cmdthoat_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdCapnhat_Click_1(object sender, EventArgs e)
        {
            if (dgv_DMDanToc.SelectedRows.Count == 0)
            {
                RadMessageBox.Show("\nBạn phải chọn dòng để cập nhật !\n", "Thông Báo", MessageBoxButtons.OK, RadMessageIcon.Exclamation);
                return;
            }
            else
            {
                txttendantoc.ReadOnly = false;
                txttendantoc.Focus();
                setbutton(false);
                k = 2;
            }
        }

        private void cmdxoa_Click_1(object sender, EventArgs e)
        {
            if (dgv_DMDanToc.SelectedRows.Count == 0)
            {
                RadMessageBox.Show("\nBạn phải chọn dòng để xóa !\n", "Thông Báo", MessageBoxButtons.OK, RadMessageIcon.Exclamation);
                return;
            }
            else
            {
                DialogResult rs = RadMessageBox.Show("\nBạn thật sự muốn xóa thông tin này không ?\n", "Thông Báo", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (rs == DialogResult.Yes)
                {

                    c.xoa("tbl_DMDanToc", int.Parse(txtmadantoc.Text.Trim()), "MaDT");
                    c.laydl("tbl_DMDanToc");

                }
            }
        }
        private void cmdluu_Click_1(object sender, EventArgs e)
        {
            DialogResult rs = RadMessageBox.Show("\nBạn thật sự muốn muốn lưu thông tin này không ?\n", "Thông Báo", MessageBoxButtons.YesNo, RadMessageIcon.Question);
            if (rs == DialogResult.Yes)
            {
                if (k == 1)
                {
                    if (txtmadantoc.Text == "" || txttendantoc.Text == "")
                    {
                        RadMessageBox.Show(this, "\nBạn phải nhập đầy đủ thông tin vào !\n", "Thông báo", MessageBoxButtons.OK, RadMessageIcon.Exclamation);
                        return;
                    }
                    else
                    {
                        c.them("tbl_DMDanToc", int.Parse(txtmadantoc.Text.Trim()), txttendantoc.Text.Trim());
                        c.laydl("tbl_DMDanToc");
                        txtmadantoc.ReadOnly = true;
                        txttendantoc.ReadOnly = true;
                        k = 0;
                        setbutton(true); 
                    }
                }
                else if (k == 2)
                    if (txtmadantoc.Text == "" || txttendantoc.Text == "")
                    {
                        RadMessageBox.Show(this, "\nBạn phải nhập đầy đủ thông tin vào !\n", "Thông báo", MessageBoxButtons.OK, RadMessageIcon.Exclamation);
                        return;
                    }
                    else
                    {
                        c.sua("tbl_DMDanToc", int.Parse(txtmadantoc.Text.Trim()), txttendantoc.Text.Trim(), "MaDT", "TenDT");
                    } 
            }
            c.laydl("tbl_DMDanToc");
            txtmadantoc.ReadOnly = true;
            txttendantoc.ReadOnly = true;
            setbutton(true);
            k = 0;
        }
    }
}