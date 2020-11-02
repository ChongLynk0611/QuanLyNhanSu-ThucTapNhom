﻿using System;
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
        //
        //tab sơ yếu lý lịch
        //
        //setcontrol so yeu ly lich
        private void setcontrolsy(bool b)
        {
            foreach (Control c in grpsoyeu1.Controls)
            {
                if (c.Name.Contains("txtsy") == true || c.Name.Contains("cbosy") == true)
                    c.Enabled = b;
            }
            foreach (Control c in grpsoyeu2.Controls)
            {
                if (c.Name.Contains("txtsy") == true || c.Name.Contains("cbosy") == true)
                    c.Enabled = b;
            }

        }

        //phương thức làm mờ bộ nút của tabsoyeu
        private void setcmd(bool b)
        {
            btnbrowser.Enabled = !b;
            cmdthem.Enabled = b;
            cmdCapnhat.Enabled = b;
            cmdxoa.Enabled = b;
            cmdluu.Enabled = !b;
            btnInNhanvien.Enabled = b;
        }

        //đưa trạng thái textbox về rỗng tabsoyeu
        private void settextboxrong()
        {
            foreach (Control c in grpsoyeu1.Controls)
            {
                if (c.Name.Contains("txtsy") == true)
                    c.Text = "";
            }
            foreach (Control c in grpsoyeu2.Controls)
            {
                if (c.Name.Contains("txtsy") == true)
                    c.Text = "";
            }
        }

        //khởi tạo dữ liệu cho các combobox cho tabsoyeu
        private void taocombosy()
        {
            //đưa dữ liệu vào cbosyphong
            cbosyphong.DataSource = cPhong.taocombo();
            cbosyphong.DisplayMember = "tbl_PhongBan.TenPhong";
            cbosyphong.ValueMember = "tbl_PhongBan.MaPhong";
            if (txtsymanv.Text != "")
                cbosyphong.SelectedValue = laydulieutheoma(txtsymanv.Text.Trim(), "MaPhong");
            //đưa dữ liệu vào cbosydantoc
            cbosydantoc.DataSource = cDanToc.taocombo();
            cbosydantoc.DisplayMember = "DMDanToc.TenDT";
            cbosydantoc.ValueMember = "DMDanToc.MaDT";
            if (txtsymanv.Text != "")
                cbosydantoc.SelectedValue = int.Parse(laydulieutheoma(txtsymanv.Text.Trim(), "MaDT"));
            //đưa dữ liệu vào cbosychucvu
            cbosychucvu.DataSource = cchucvu.taocombo();
            cbosychucvu.DisplayMember = "DMChucVu.TenChucVu";
            cbosychucvu.ValueMember = "DMChucVu.MaChucVu";
            if (txtsymanv.Text != "")
                cbosychucvu.SelectedValue = int.Parse(laydulieutheoma(txtsymanv.Text.Trim(), "MaChucVu"));
            //đưa dữ liệu vào cbosytongiao
            cbosytongiao.DataSource = ctongiao.taocombo();
            cbosytongiao.DisplayMember = "DMTonGiao.TenTG";
            cbosytongiao.ValueMember = "DMTonGiao.MaTG";
            if (txtsymanv.Text != "")
                cbosytongiao.SelectedValue = int.Parse(laydulieutheoma(txtsymanv.Text.Trim(), "MaTG"));
        }

        //hiển thị dữ liệu nhân viên vào trang sơ yếu lý lịch
        private void HienthiTTNhanVien(string ma)
        {
            cnhanvien.laydl(ma);

            //
            //lấy dữ liệu vào các textbox
            //
            try
            {
                txtsymanv.Text = cnhanvien.ds.Tables["nhanvien"].Rows[0]["MaNV"].ToString();
                txtsyhonv.Text = cnhanvien.ds.Tables["nhanvien"].Rows[0]["Ho"].ToString();
                txtsytennv.Text = cnhanvien.ds.Tables["nhanvien"].Rows[0]["Ten"].ToString();
                txtsyngaysinh.Text = sql.NgayToString(cnhanvien.ds.Tables["nhanvien"].Rows[0]["Ngaysinh"].ToString());
                txtsynoisinh.Text = cnhanvien.ds.Tables["nhanvien"].Rows[0]["Noisinh"].ToString();
                txtsysocmnd.Text = cnhanvien.ds.Tables["nhanvien"].Rows[0]["SoCMND"].ToString();
                txtsyhokhau.Text = cnhanvien.ds.Tables["nhanvien"].Rows[0]["HokhauTT"].ToString();
                txtsychohientai.Text = cnhanvien.ds.Tables["nhanvien"].Rows[0]["ChoOHienTai"].ToString();
                txtsydienthoai.Text = cnhanvien.ds.Tables["nhanvien"].Rows[0]["DienThoai"].ToString();
                txtsydtdd.Text = cnhanvien.ds.Tables["nhanvien"].Rows[0]["DTDD"].ToString();
                txtsysoBHXH.Text = cnhanvien.ds.Tables["nhanvien"].Rows[0]["SoBHXH"].ToString();
                txtsysoBHYT.Text = cnhanvien.ds.Tables["nhanvien"].Rows[0]["SoBHYT"].ToString();
                txtsysotheATM.Text = cnhanvien.ds.Tables["nhanvien"].Rows[0]["SotheATM"].ToString();
                txtsyngayvl.Text = sql.NgayToString(cnhanvien.ds.Tables["nhanvien"].Rows[0]["Ngayvaolam"].ToString());
                txtsyghichu.Text = cnhanvien.ds.Tables["nhanvien"].Rows[0]["GhiChu"].ToString();
                txtsysEmail.Text = cnhanvien.ds.Tables["nhanvien"].Rows[0]["Email"].ToString();
                //
                //lấy dữ liệu vào các combobox
                //khác với phương thức taocombo ở trên vì đây là đi theo mỗi nhân viên
                cbosydantoc.DataSource = cnhanvien.ds;
                cbosydantoc.ValueMember = "nhanvien.madt";
                cbosydantoc.DisplayMember = "nhanvien.tendt";
                cbosytongiao.DataSource = cnhanvien.ds;
                cbosytongiao.ValueMember = "nhanvien.MaTG";
                cbosytongiao.DisplayMember = "nhanvien.TenTG";
                cbosytinhtrang.Text = cnhanvien.ds.Tables["nhanvien"].Rows[0]["Tinhtranghonnhan"].ToString();
                cbosyphong.DataSource = cnhanvien.ds;
                cbosyphong.ValueMember = "nhanvien.MaPhong";
                cbosyphong.DisplayMember = "nhanvien.tenphong";
                cbosyto.DataSource = cnhanvien.ds;
                cbosyto.ValueMember = "nhanvien.Mato";
                cbosyto.DisplayMember = "nhanvien.TenTo";
                cbosychucvu.DataSource = cnhanvien.ds;
                cbosychucvu.ValueMember = "nhanvien.MaChucVu";
                cbosychucvu.DisplayMember = "NhanVien.TenChucVu";
                //
                //lấy dữ liệu vào optgioitinh
                //
                if (cnhanvien.ds.Tables["nhanvien"].Rows[0]["GioiTinh"].ToString() == "1")
                    optNam.Checked = true;
                else
                    optNu.Checked = true;
                //hien thi hinh anh

                filename = cnhanvien.ds.Tables["nhanvien"].Rows[0]["hinhanh"].ToString();
                if (filename != "")
                    picHinh.Image = new Bitmap(Application.StartupPath + @"\Hinh\" + filename);
                else
                {
                    filename = "";
                    picHinh.Image = null;
                }
            }
            catch { }
        }

        //lấy dữ liệu cho các combobox trong tabsoyeu theo mã nhân viên
        private string laydulieutheoma(string ma, string macombo)
        {
            cnhanvien.sc.Open();
            string sql = "select " + macombo + " from Tbl_nhanvien where MaNV='" + ma + "'";
            SqlCommand cmd = new SqlCommand(sql, cnhanvien.sc);
            object o = cmd.ExecuteScalar();
            cnhanvien.sc.Close();
            return o.ToString();
        }

        //lấy mã nhân viên tự động
        private string laymatudong(string s)
        {
            string kq = "";
            int so = int.Parse(s.Substring(2));
            if ((so + 1) < 10)
                kq = "NV000" + (so + 1);
            if ((so + 1) < 100 && (so + 1) >= 10)
                kq = "NV00" + (so + 1);
            if ((so + 1) <= 999 && (so + 1) >= 100)
                kq = "NV0" + (so + 1);
            else
                kq = "NV" + (so + 1);


            return kq;
        }

    }
}