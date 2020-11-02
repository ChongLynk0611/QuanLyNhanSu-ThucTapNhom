using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using Telerik.Data;
using Telerik.WinControls.UI;
using QuanLyNhanSu.Class;
using Telerik.WinControls;
using Telerik.WinControls.UI.Localization;
using QuanLyNhanSu.Class;


namespace QuanLyNhanSu
{
    public partial class frmNhanvien : RadForm
    {
        RadGridLocalizationProvider oldProvider;

        clsDMTo cTo = new clsDMTo();
        clsdmpb cPhong = new clsdmpb();
        clsnhanvien cnhanvien = new clsnhanvien();
        clsDMDanToc cDanToc = new clsDMDanToc();
        clsdmchucvu cchucvu = new clsdmchucvu();
        clsdmtongiao ctongiao = new clsdmtongiao();
        clschitietchuyenmon cchuyenmon = new clschitietchuyenmon();
        clschitietngoaingu cngoaingu = new clschitietngoaingu();
        clsdmchuyenmon cdmcm = new clsdmchuyenmon();
        clsdmngoaingu cdmnn = new clsdmngoaingu();
        clsdmhd cloaihd = new clsdmhd();
        clsdmtrinhdo cdmtd = new clsdmtrinhdo();
        clschitiethopdong ccthopdong = new clschitiethopdong();
        clsdmhd cdmhd = new clsdmhd();
        clshosoluong chsluong = new clshosoluong();
        static public string bienmanv;
        string filename = "";//ten file hinh anh
        int kqh = 0;//khoi tao gia tri de biet trang thai luu hoac them QuanHeGiaDinh
        int ksy = 0;//khoi tao gia tri de biet trang thai luu hoac them SoYeuLyLich
        int khd = 0;//khoi tao gia tri de biet trang thai luu hoac them HopDong
        int kluong = 0; //khoi tao gia tri de biet trang thai luu hoac them Luong

        QuanLyNhanSu.Class.sql sql = new QuanLyNhanSu.Class.sql();


        DataSet ds = new DataSet();
        DataSet ds1;
        DataSet ds_NV;
        SqlConnection con = null;
        public SqlDataAdapter sda;

        public frmNhanvien()
        {
            InitializeComponent();
            cboPhong.DataSource = cPhong.taocombo();
            cboPhong.DisplayMember = "tbl_PhongBan.TenPhong";
            cboPhong.ValueMember = "tbl_PhongBan.MaPhong";
            oldProvider = RadGridLocalizationProvider.CurrentProvider;

            RadGridLocalizationProvider.CurrentProvider = new MyVNRadGridLocalizationProvider();
        }
        public class MyVNRadGridLocalizationProvider : RadGridLocalizationProvider
        {
            public override string GetLocalizedString(string id)
            {
                switch (id)
                {
                    case RadGridStringId.AddNewRowString: return "Click vào đây để thêm dòng mới";
                    case RadGridStringId.SortAscendingMenuItem: return "Sắp xếp theo hướng tăng dần";
                    case RadGridStringId.SortDescendingMenuItem: return "Sắp xếp theo hướng giảm dần dần";
                    case RadGridStringId.ClearSortingMenuItem: return "Bỏ sắp xếp";
                    case RadGridStringId.ConditionalFormattingMenuItem: return "Định dạng dưa theo điều kiện";
                    case RadGridStringId.GroupByThisColumnMenuItem: return "Nhóm theo cột này";
                    case RadGridStringId.UngroupThisColumn: return "Bỏ nhóm cột này";
                    case RadGridStringId.ColumnChooserMenuItem: return "Chọn cột";
                    case RadGridStringId.HideMenuItem: return "Ẩn";

                    case RadGridStringId.BestFitMenuItem: return "Canh cột";
                    case RadGridStringId.PasteMenuItem: return "Dán";
                    case RadGridStringId.EditMenuItem: return "Sửa";
                    case RadGridStringId.CopyMenuItem: return "Sao chép";
                    default:
                        return base.GetLocalizedString(id);
                }
            }
        }
        public void LoadCmbPhongBan()
        {
            con = sql.connectSQL();
            this.cboPhong.DataSource = sql.LoadDataToCombobox(this.cboPhong, "select * from tbl_PhongBan", "tbl_PhongBan", con);
            this.cboPhong.DisplayMember = "tbl_PhongBan.TenPhong";
            this.cboPhong.ValueMember = "tbl_PhongBan.MaPhong";
            con.Close();
            this.cboPhong.SelectedIndex = 0;
        }
        private void lamrong()
        {
            foreach (Control c in grpsoyeu1.Controls)
                if (c.Name.Contains("txt") == true || c.Name.Contains("cbo") == true)
                {
                    c.Text = "";
                }
            foreach (Control c in grpsoyeu2.Controls)
                if (c.Name.Contains("txt") == true || c.Name.Contains("cbo") == true)
                    c.Text = "";
            foreach (Control c in grpTTTD.Controls)
                if (c.Name.Contains("txt") == true || c.Name.Contains("cbo") == true)
                    c.Text = "";
            foreach (Control c in grphsl.Controls)
                if (c.Name.Contains("txt") == true || c.Name.Contains("cbo") == true)
                    c.Text = "";
            foreach (Control c in grphopdong.Controls)
                if (c.Name.Contains("txt") == true || c.Name.Contains("cbo") == true)
                    c.Text = "";
        }

        private void frmNhanvien_Load(object sender, EventArgs e)
        {
            cbosytinhtrang.Items.AddRange(new string[] { "Độc Thân", "Có Gia Đình", "Đã Kết Hôn" });
            setcontrolsy(false);
            setcontrolhd(false);
            setcontrolhsl(false);
            txthslngayll.Enabled = false;
            ksy = 0; khd = 0; kluong = 0;
            setcmd(true);
            setcmdhd(true);
            setcmdhsl(true);
            bienmanv = "";
        }
        //
        //List Nhân Viên
        //
        //Hiển thị thông tin nhân viên vào list nhân viên
        private void HienThiListNV(DataTable dt, ListView lv)
        {
            foreach (DataRow row in dt.Rows)
            {
                ListViewItem item = new ListViewItem();
                item.Text = row["MaNV"].ToString();
                item.SubItems.Add(row["Ho"].ToString());
                item.SubItems.Add(row["Ten"].ToString());
                lv.Items.Add(item);
            }
        }

        private void lstNhanvien_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ma = lstNhanvien.FocusedItem.SubItems[0].Text;
            string ho = lstNhanvien.FocusedItem.SubItems[1].Text;
            string ten = lstNhanvien.FocusedItem.SubItems[2].Text;
            bienmanv = ma;
            //trang so yeu ly lich
            HienthiTTNhanVien(ma);
            //trang trình độ
            txtknmanv.Text = ma;
            txtknhotennv.Text = ho + " " + ten;
            HienThiDLKN(ma);
            //hiển thị trang quan hệ
            HienThiTTQuanHe(ma);
            //trang hợp đồng
            txthdmanv.Text = ma;
            txthdhotennv.Text = ho + " " + ten;
            HienThiTTHopDong(ma);
            // trang hồ sơ lương
            txthslmanv.Text = ma;
            txthslhotennv.Text = ho + " " + ten;
            HienThiTTHoSoLuong(ma);
            this.cboPhong.SelectedValue = this.cbosyphong.SelectedValue;
            if (this.cbosyphong.SelectedValue != null)
            {
                cboTo.DataSource = cTo.taocombo(cboPhong.SelectedValue.ToString());
                cboTo.DisplayMember = "DanhMucTo.TenTo";
                cboTo.ValueMember = "DanhMucTo.MaTo";

                this.cboTo.SelectedValue = this.cbosyto.SelectedValue;
            }
            else
            {
                //cboTo.DataSource = cTo.taocombo(cboPhong.SelectedValue.ToString());
                this.cboTo.Text = "";

            }
            //MessageBox.Show(this.cbosyto.SelectedValue.ToString());

        }

        private void cboPhong_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cboTo.DataSource = cTo.taocombo(cboPhong.SelectedValue.ToString());
            cboTo.DisplayMember = "DanhMucTo.TenTo";
            cboTo.ValueMember = "DanhMucTo.MaTo";
            cboTo.Text = "";
            lstNhanvien.Items.Clear();
            HienThiListNV(cPhong.laydlList(cboPhong.SelectedValue.ToString()), lstNhanvien);
            lamrong();
        }

        private void cboTo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            lamrong();
            lstNhanvien.Items.Clear();
            HienThiListNV(cTo.laydlList(cboPhong.SelectedValue.ToString(), cboTo.SelectedValue.ToString()), lstNhanvien);

        }

        private void setlist(bool b)
        {
            lstNhanvien.Enabled = b;
            cboPhong.Enabled = b;
            cboTo.Enabled = b;
        }
    }
}