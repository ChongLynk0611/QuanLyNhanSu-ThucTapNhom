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
        //bộ nút của tab sơ yếu lý lịch

        private void cmdluu_Click(object sender, EventArgs e)
        {
            if (this.txtsyhonv.Text.Trim() != String.Empty && this.txtsytennv.Text.Trim() != String.Empty && this.txtsyngaysinh.Text.Trim() != String.Empty)
            {

                if (this.txtsysEmail.Text.Trim() != String.Empty)
                {
                    if (sql.IsEmail(this.txtsysEmail.Text) == true)
                    {
                        DialogResult rs = RadMessageBox.Show("\nBạn có muốn lưu thông tin này ?\n", "Thông báo", MessageBoxButtons.OKCancel, RadMessageIcon.Question);
                        if (rs == DialogResult.OK)
                        {
                            if (ksy == 1)
                            {
                                if (cbosyto.SelectedItem != null)
                                    //them nhan vien neu nhu phong do co to
                                    cnhanvien.ThemNV(txtsymanv.Text.Trim(),
                                             txtsyhonv.Text.Trim(),
                                             txtsytennv.Text.Trim(),
                                             optNam.Checked ? '1' : '0',
                                             txtsysocmnd.Text.Trim(),
                                             txtsyngaysinh.Text.Trim() == "" ? DateTime.Today : sql.TraVeNgay(txtsyngaysinh.Text.Trim(), 1),
                                             txtsynoisinh.Text.Trim(),
                                             int.Parse(cbosytongiao.SelectedValue.ToString()),
                                             int.Parse(cbosydantoc.SelectedValue.ToString()),
                                             txtsydienthoai.Text.Trim(),
                                             txtsydtdd.Text.Trim(),
                                             txtsyhokhau.Text.Trim(),
                                             txtsychohientai.Text.Trim(),
                                             cbosytinhtrang.Text.Trim(),
                                             filename.ToString(),
                                             int.Parse(cbosychucvu.SelectedValue.ToString()),
                                             cbosyphong.SelectedValue.ToString(),
                                             cbosyto.SelectedValue.ToString(),
                                             txtsyngayvl.Text.Trim() == "" ? DateTime.Today : sql.TraVeNgay(txtsyngayvl.Text.Trim(), 1),
                                             txtsysoBHYT.Text.Trim(), txtsysoBHXH.Text.Trim(),
                                             txtsysotheATM.Text.Trim(), txtsyghichu.Text.Trim(), txtsysEmail.Text.Trim());
                                else
                                    //them nhan vien neu nhu phong do khong co to
                                    cnhanvien.ThemNV(txtsymanv.Text.Trim(),
                                             txtsyhonv.Text.Trim(),
                                             txtsytennv.Text.Trim(),
                                             optNam.Checked ? '1' : '0',
                                             txtsysocmnd.Text.Trim(),
                                             txtsyngaysinh.Text.Trim() == "" ? DateTime.Today : sql.TraVeNgay(txtsyngaysinh.Text.Trim(), 1),
                                             txtsynoisinh.Text.Trim(),
                                             int.Parse(cbosytongiao.SelectedValue.ToString()),
                                             int.Parse(cbosydantoc.SelectedValue.ToString()),
                                             txtsydienthoai.Text.Trim(),
                                             txtsydtdd.Text.Trim(),
                                             txtsyhokhau.Text.Trim(),
                                             txtsychohientai.Text.Trim(),
                                             cbosytinhtrang.Text.Trim(),
                                             filename.ToString(),
                                             int.Parse(cbosychucvu.SelectedValue.ToString()),
                                             cbosyphong.SelectedValue.ToString(),
                                             txtsyngayvl.Text.Trim() == "" ? DateTime.Today : sql.TraVeNgay(txtsyngayvl.Text.Trim(), 1),
                                             txtsysoBHYT.Text.Trim(), txtsysoBHXH.Text.Trim(),
                                             txtsysotheATM.Text.Trim(), txtsyghichu.Text.Trim(), txtsysEmail.Text.Trim());
                            }
                            else if (ksy == 2)
                            {
                                if (cbosyto.SelectedItem != null)
                                    if (cbosyto.Text != "")
                                        //sua nhan vien neu nhu nhan vien do co to
                                        cnhanvien.SuaNV(txtsymanv.Text.Trim(),
                                                txtsyhonv.Text.Trim(),
                                                txtsytennv.Text.Trim(),
                                                optNam.Checked ? '1' : '0',
                                                txtsysocmnd.Text.Trim(),
                                                sql.TraVeNgay(txtsyngaysinh.Text.Trim(), 1),
                                                txtsynoisinh.Text.Trim(),
                                                int.Parse(cbosytongiao.SelectedValue.ToString()),
                                                int.Parse(cbosydantoc.SelectedValue.ToString()),
                                                txtsydienthoai.Text.Trim(),
                                                txtsydtdd.Text.Trim(),
                                                txtsyhokhau.Text.Trim(),
                                                txtsychohientai.Text.Trim(),
                                                cbosytinhtrang.Text.Trim(),
                                                filename.ToString(),
                                                int.Parse(cbosychucvu.SelectedValue.ToString()),
                                                cbosyphong.SelectedValue.ToString(),
                                                cbosyto.SelectedValue.ToString(),
                                                sql.TraVeNgay(txtsyngayvl.Text.Trim(), 1),
                                                txtsysoBHYT.Text.Trim(), txtsysoBHXH.Text.Trim(),
                                                txtsysotheATM.Text.Trim(), txtsyghichu.Text.Trim(), txtsysEmail.Text.Trim());
                                    else
                                    {
                                        //sua nhan vien neu nhu nhan vien do khong co ma to
                                        cnhanvien.SuaNV(txtsymanv.Text.Trim(),
                                                txtsyhonv.Text.Trim(),
                                                txtsytennv.Text.Trim(),
                                                optNam.Checked ? '1' : '0',
                                                txtsysocmnd.Text.Trim(),
                                                sql.TraVeNgay(txtsyngaysinh.Text.Trim(), 1),
                                                txtsynoisinh.Text.Trim(),
                                                int.Parse(cbosytongiao.SelectedValue.ToString()),
                                                int.Parse(cbosydantoc.SelectedValue.ToString()),
                                                txtsydienthoai.Text.Trim(),
                                                txtsydtdd.Text.Trim(),
                                                txtsyhokhau.Text.Trim(),
                                                txtsychohientai.Text.Trim(),
                                                cbosytinhtrang.Text.Trim(),
                                                filename.ToString(),
                                                int.Parse(cbosychucvu.SelectedValue.ToString()),
                                                cbosyphong.SelectedValue.ToString(),
                                                sql.TraVeNgay(txtsyngayvl.Text.Trim(), 1),
                                                txtsysoBHYT.Text.Trim(), txtsysoBHXH.Text.Trim(),
                                                txtsysotheATM.Text.Trim(), txtsyghichu.Text.Trim(), txtsysEmail.Text.Trim());
                                        cnhanvien.sc.Open();
                                        string ins_mato = "update tbl_nhanvien set mato =null where MaNV='" + txtsymanv.Text.Trim() + "'";
                                        SqlCommand cmd_ins = new SqlCommand(ins_mato, cnhanvien.sc);
                                        try
                                        {
                                            cmd_ins.ExecuteNonQuery();
                                        }
                                        catch (SqlException ex)
                                        {
                                            MessageBox.Show(ex.Errors + " " + ex.Number);
                                        }
                                        cnhanvien.sc.Close();
                                    }
                                else
                                { //sua nhan vien neu nhu nhan vien do khong co ma to
                                    cnhanvien.SuaNV(txtsymanv.Text.Trim(),
                                            txtsyhonv.Text.Trim(),
                                            txtsytennv.Text.Trim(),
                                            optNam.Checked ? '1' : '0',
                                            txtsysocmnd.Text.Trim(),
                                            sql.TraVeNgay(txtsyngaysinh.Text.Trim(), 1),
                                            txtsynoisinh.Text.Trim(),
                                            int.Parse(cbosytongiao.SelectedValue.ToString()),
                                            int.Parse(cbosydantoc.SelectedValue.ToString()),
                                            txtsydienthoai.Text.Trim(),
                                            txtsydtdd.Text.Trim(),
                                            txtsyhokhau.Text.Trim(),
                                            txtsychohientai.Text.Trim(),
                                            cbosytinhtrang.Text.Trim(),
                                            filename.ToString(),
                                            int.Parse(cbosychucvu.SelectedValue.ToString()),
                                            cbosyphong.SelectedValue.ToString(),
                                            sql.TraVeNgay(txtsyngayvl.Text.Trim(), 1),
                                            txtsysoBHYT.Text.Trim(), txtsysoBHXH.Text.Trim(),
                                            txtsysotheATM.Text.Trim(), txtsyghichu.Text.Trim(), txtsysEmail.Text.Trim());
                                    cnhanvien.sc.Open();
                                    string ins_mato = "update tbl_nhanvien set mato =null where MaNV='" + txtsymanv.Text.Trim() + "'";
                                    SqlCommand cmd_ins = new SqlCommand(ins_mato, cnhanvien.sc);
                                    try
                                    {
                                        cmd_ins.ExecuteNonQuery();
                                    }
                                    catch (SqlException ex)
                                    {
                                        MessageBox.Show(ex.Errors + " " + ex.Number);
                                    }
                                    cnhanvien.sc.Close();
                                }
                            }
                        }
                        else
                        {
                            HienthiTTNhanVien(bienmanv);
                        }
                        ksy = 0;
                        setcontrolsy(false);
                        setlist(true);
                        setcmd(true);
                        lstNhanvien.Items.Clear();
                        HienThiListNV(cPhong.laydlList(cboPhong.SelectedValue.ToString()), lstNhanvien);
                    }
                    else
                    {
                        RadMessageBox.Show("\nEmail sai định dạng !\n", "Thông báo", MessageBoxButtons.OK, RadMessageIcon.Error);
                    }
                }
                else
                {
                    DialogResult rs = RadMessageBox.Show("\nBạn có muốn lưu thông tin này ?\n", "Thông báo", MessageBoxButtons.OKCancel, RadMessageIcon.Question);
                    if (rs == DialogResult.OK)
                    {
                        if (ksy == 1)
                        {
                            if (cbosyto.SelectedItem != null)
                                //them nhan vien neu nhu phong do co to
                                cnhanvien.ThemNV(txtsymanv.Text.Trim(),
                                         txtsyhonv.Text.Trim(),
                                         txtsytennv.Text.Trim(),
                                         optNam.Checked ? '1' : '0',
                                         txtsysocmnd.Text.Trim(),
                                         txtsyngaysinh.Text.Trim() == "" ? DateTime.Today : sql.TraVeNgay(txtsyngaysinh.Text.Trim(), 1),
                                         txtsynoisinh.Text.Trim(),
                                         int.Parse(cbosytongiao.SelectedValue.ToString()),
                                         int.Parse(cbosydantoc.SelectedValue.ToString()),
                                         txtsydienthoai.Text.Trim(),
                                         txtsydtdd.Text.Trim(),
                                         txtsyhokhau.Text.Trim(),
                                         txtsychohientai.Text.Trim(),
                                         cbosytinhtrang.Text.Trim(),
                                         filename.ToString(),
                                         int.Parse(cbosychucvu.SelectedValue.ToString()),
                                         cbosyphong.SelectedValue.ToString(),
                                         cbosyto.SelectedValue.ToString(),
                                         txtsyngayvl.Text.Trim() == "" ? DateTime.Today : sql.TraVeNgay(txtsyngayvl.Text.Trim(), 1),
                                         txtsysoBHYT.Text.Trim(), txtsysoBHXH.Text.Trim(),
                                         txtsysotheATM.Text.Trim(), txtsyghichu.Text.Trim(), txtsysEmail.Text.Trim());
                            else
                                //them nhan vien neu nhu phong do khong co to
                                cnhanvien.ThemNV(txtsymanv.Text.Trim(),
                                         txtsyhonv.Text.Trim(),
                                         txtsytennv.Text.Trim(),
                                         optNam.Checked ? '1' : '0',
                                         txtsysocmnd.Text.Trim(),
                                         txtsyngaysinh.Text.Trim() == "" ? DateTime.Today : sql.TraVeNgay(txtsyngaysinh.Text.Trim(), 1),
                                         txtsynoisinh.Text.Trim(),
                                         int.Parse(cbosytongiao.SelectedValue.ToString()),
                                         int.Parse(cbosydantoc.SelectedValue.ToString()),
                                         txtsydienthoai.Text.Trim(),
                                         txtsydtdd.Text.Trim(),
                                         txtsyhokhau.Text.Trim(),
                                         txtsychohientai.Text.Trim(),
                                         cbosytinhtrang.Text.Trim(),
                                         filename.ToString(),
                                         int.Parse(cbosychucvu.SelectedValue.ToString()),
                                         cbosyphong.SelectedValue.ToString(),
                                         txtsyngayvl.Text.Trim() == "" ? DateTime.Today : sql.TraVeNgay(txtsyngayvl.Text.Trim(), 1),
                                         txtsysoBHYT.Text.Trim(), txtsysoBHXH.Text.Trim(),
                                         txtsysotheATM.Text.Trim(), txtsyghichu.Text.Trim(), txtsysEmail.Text.Trim());
                        }
                        else if (ksy == 2)
                        {
                            if (cbosyto.SelectedItem != null)
                                if (cbosyto.Text != "")
                                    //sua nhan vien neu nhu nhan vien do co to
                                    cnhanvien.SuaNV(txtsymanv.Text.Trim(),
                                            txtsyhonv.Text.Trim(),
                                            txtsytennv.Text.Trim(),
                                            optNam.Checked ? '1' : '0',
                                            txtsysocmnd.Text.Trim(),
                                            sql.TraVeNgay(txtsyngaysinh.Text.Trim(), 1),
                                            txtsynoisinh.Text.Trim(),
                                            int.Parse(cbosytongiao.SelectedValue.ToString()),
                                            int.Parse(cbosydantoc.SelectedValue.ToString()),
                                            txtsydienthoai.Text.Trim(),
                                            txtsydtdd.Text.Trim(),
                                            txtsyhokhau.Text.Trim(),
                                            txtsychohientai.Text.Trim(),
                                            cbosytinhtrang.Text.Trim(),
                                            filename.ToString(),
                                            int.Parse(cbosychucvu.SelectedValue.ToString()),
                                            cbosyphong.SelectedValue.ToString(),
                                            cbosyto.SelectedValue.ToString(),
                                            sql.TraVeNgay(txtsyngayvl.Text.Trim(), 1),
                                            txtsysoBHYT.Text.Trim(), txtsysoBHXH.Text.Trim(),
                                            txtsysotheATM.Text.Trim(), txtsyghichu.Text.Trim(), txtsysEmail.Text.Trim());
                                else
                                {
                                    //sua nhan vien neu nhu nhan vien do khong co ma to
                                    cnhanvien.SuaNV(txtsymanv.Text.Trim(),
                                            txtsyhonv.Text.Trim(),
                                            txtsytennv.Text.Trim(),
                                            optNam.Checked ? '1' : '0',
                                            txtsysocmnd.Text.Trim(),
                                            sql.TraVeNgay(txtsyngaysinh.Text.Trim(), 1),
                                            txtsynoisinh.Text.Trim(),
                                            int.Parse(cbosytongiao.SelectedValue.ToString()),
                                            int.Parse(cbosydantoc.SelectedValue.ToString()),
                                            txtsydienthoai.Text.Trim(),
                                            txtsydtdd.Text.Trim(),
                                            txtsyhokhau.Text.Trim(),
                                            txtsychohientai.Text.Trim(),
                                            cbosytinhtrang.Text.Trim(),
                                            filename.ToString(),
                                            int.Parse(cbosychucvu.SelectedValue.ToString()),
                                            cbosyphong.SelectedValue.ToString(),
                                            sql.TraVeNgay(txtsyngayvl.Text.Trim(), 1),
                                            txtsysoBHYT.Text.Trim(), txtsysoBHXH.Text.Trim(),
                                            txtsysotheATM.Text.Trim(), txtsyghichu.Text.Trim(), txtsysEmail.Text.Trim());
                                    cnhanvien.sc.Open();
                                    string ins_mato = "update tbl_nhanvien set mato =null where MaNV='" + txtsymanv.Text.Trim() + "'";
                                    SqlCommand cmd_ins = new SqlCommand(ins_mato, cnhanvien.sc);
                                    try
                                    {
                                        cmd_ins.ExecuteNonQuery();
                                    }
                                    catch (SqlException ex)
                                    {
                                        MessageBox.Show(ex.Errors + " " + ex.Number);
                                    }
                                    cnhanvien.sc.Close();
                                }
                            else
                            { //sua nhan vien neu nhu nhan vien do khong co ma to
                                cnhanvien.SuaNV(txtsymanv.Text.Trim(),
                                        txtsyhonv.Text.Trim(),
                                        txtsytennv.Text.Trim(),
                                        optNam.Checked ? '1' : '0',
                                        txtsysocmnd.Text.Trim(),
                                        sql.TraVeNgay(txtsyngaysinh.Text.Trim(), 1),
                                        txtsynoisinh.Text.Trim(),
                                        int.Parse(cbosytongiao.SelectedValue.ToString()),
                                        int.Parse(cbosydantoc.SelectedValue.ToString()),
                                        txtsydienthoai.Text.Trim(),
                                        txtsydtdd.Text.Trim(),
                                        txtsyhokhau.Text.Trim(),
                                        txtsychohientai.Text.Trim(),
                                        cbosytinhtrang.Text.Trim(),
                                        filename.ToString(),
                                        int.Parse(cbosychucvu.SelectedValue.ToString()),
                                        cbosyphong.SelectedValue.ToString(),
                                        sql.TraVeNgay(txtsyngayvl.Text.Trim(), 1),
                                        txtsysoBHYT.Text.Trim(), txtsysoBHXH.Text.Trim(),
                                        txtsysotheATM.Text.Trim(), txtsyghichu.Text.Trim(), txtsysEmail.Text.Trim());
                                cnhanvien.sc.Open();
                                string ins_mato = "update tbl_nhanvien set mato =null where MaNV='" + txtsymanv.Text.Trim() + "'";
                                SqlCommand cmd_ins = new SqlCommand(ins_mato, cnhanvien.sc);
                                try
                                {
                                    cmd_ins.ExecuteNonQuery();
                                }
                                catch (SqlException ex)
                                {
                                    MessageBox.Show(ex.Errors + " " + ex.Number);
                                }
                                cnhanvien.sc.Close();
                            }
                        }
                    }
                    else
                    {
                        HienthiTTNhanVien(bienmanv);
                    }
                    ksy = 0;
                    setcontrolsy(false);
                    setlist(true);
                    setcmd(true);
                    lstNhanvien.Items.Clear();
                    HienThiListNV(cPhong.laydlList(cboPhong.SelectedValue.ToString()), lstNhanvien);
                }
            }
            else
            {
                RadMessageBox.Show("\nHọ tên và ngày sinh nhân viên không được bỏ trống !\n", "Thông báo", MessageBoxButtons.OK, RadMessageIcon.Error);
            }

        }
        private void cmdthem_Click(object sender, EventArgs e)
        {
            //if (cbosyto.DataSource !=null)
            //cbosyto.Items.Clear();
            settextboxrong();
            setcontrolsy(true);
            setlist(false);
            taocombosy();
            txtsyhonv.Focus();
            setcmd(false);
            ksy = 1;
            filename = "noavatar.jpg";
            if (filename != "")
                picHinh.Image = new Bitmap(Application.StartupPath + @"\Hinh\" + filename);
            else
            {
                filename = "";
                picHinh.Image = null;
            }
            if (int.Parse(laymatudong(cnhanvien.laynvcuoi()).Substring(2)) > 999)
            {
                RadMessageBox.Show("\nChương trình chỉ được nhận dưới 1000 nhân viên !\n", "Thông báo", MessageBoxButtons.OK, RadMessageIcon.Exclamation);
            }
            else txtsymanv.Text = laymatudong(cnhanvien.laynvcuoi());
        }

        private void txtsymanv_Validated(object sender, EventArgs e)
        {
            txtsymanv.Text = txtsymanv.Text.Trim().ToUpper();
        }

        private void cbosyphong_Leave(object sender, EventArgs e)
        {
            cbosyto.DataSource = cTo.taocombo(cbosyphong.SelectedValue.ToString());
            cbosyto.DisplayMember = "DanhMucTo.TenTo";
            cbosyto.ValueMember = "DanhMucTo.MaTo";
        }

        private void cmdxoa_Click(object sender, EventArgs e)
        {
            DialogResult rs = RadMessageBox.Show("\nBạn muốn xóa mẩu tin này không ?\n", "Thông Báo", MessageBoxButtons.YesNo, RadMessageIcon.Question);
            if (rs == DialogResult.Yes)
                cnhanvien.XoaNV(txtsymanv.Text.Trim());
            lstNhanvien.Items.Clear();
            HienThiListNV(cPhong.laydlList(cboPhong.SelectedValue.ToString()), lstNhanvien);
        }

        private void cmdCapnhat_Click(object sender, EventArgs e)
        {
            if (txtsymanv.Text == "")
                RadMessageBox.Show("\nBạn phải chọn nhân viên cập nhật !\n", "Thông Báo", MessageBoxButtons.OK, RadMessageIcon.Exclamation);
            else
            {
                ksy = 2;
                setcontrolsy(true);
                setlist(false);
                taocombosy();
                txtsyhonv.Focus();
                setcmd(false);
            }
        }

        private void btnbrowser_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Images (*.png|*.jpg|*.bmp|*.gif)";

            //dlg.InitialDirectory = Application.StartupPath + @"\hinh";
            //try
            //{
            //    File.Copy(dlg.in,
            //              dirListBox2.Path + "\\" + fileListBox1.FileName, true);



            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error : " + ex.Message,
            //    "Error",
            //    MessageBoxButtons.OK,
            //    MessageBoxIcon.Error);
            //}

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show(dlg.FileName.ToString());

                filename = dlg.FileName.Substring(dlg.FileName.LastIndexOf("\\") + 1, dlg.FileName.Length - dlg.FileName.LastIndexOf("\\") - 1);
                try
                {

                    File.Copy(dlg.FileName, Application.StartupPath + @"\hinh\" + filename, true);
                }
                catch (IOException ex)
                {
                    //RadMessageBox.Show("\nTên file ảnh bạn chọn đã có trong dữ liệu, xin vui lòng đổi tên lại !\n","Thông báo",MessageBoxButtons.OK,RadMessageIcon.Exclamation);
                }
                try
                {
                    picHinh.Image = new Bitmap(Application.StartupPath + @"\hinh\" + filename);
                    //picHinh.Scale(new SizeF(90,110));
                }
                catch
                {
                    RadMessageBox.Show(this, "\nHình chọn không đúng !\n", "Thông báo", MessageBoxButtons.OK, RadMessageIcon.Exclamation);
                }

            }


        }
        //kiểm tra việc nhập ngày sinh
        private void txtsyngaysinh_Validated(object sender, EventArgs e)
        {
            string s = txtsyngaysinh.Text.Trim();
            if (s != "")
            {
                if (!sql.KiemTraNgay(s))
                {
                    RadMessageBox.Show("\nBạn đã nhập ngày sai, xin vui lòng kiểm tra lại !\n", "Thông báo", MessageBoxButtons.OK, RadMessageIcon.Exclamation);
                    txtsyngaysinh.Text = "";
                    txtsyngaysinh.Focus();
                }
            }
            else
            {
                RadMessageBox.Show("\nBạn chưa nhập ngày, thông tin này không được bỏ trống !\n", "Thông báo", MessageBoxButtons.OK, RadMessageIcon.Exclamation);
                txtsyngaysinh.Text = "";
                txtsyngaysinh.Focus();
            }
        }

        //kiểm tra việc nhập ngày vào làm
        private void txtsyngayvl_Validated(object sender, EventArgs e)
        {
            string s = txtsyngayvl.Text.Trim();
            if (s != "")
            {
                if (!sql.KiemTraNgay(s))
                {
                    RadMessageBox.Show("\nBạn nhập ngày sai, xin vui lòng xem lại !\n", "Thông báo", MessageBoxButtons.OK, RadMessageIcon.Exclamation);
                    txtsyngayvl.Text = "";
                    txtsyngayvl.Focus();
                }
            }
            else
            {
                RadMessageBox.Show("\nBạn chưa nhập ngày vào làm, thông tin này không được bỏ trống  !\n", "Thông báo", MessageBoxButtons.OK, RadMessageIcon.Exclamation);
                txtsyngayvl.Text = "";
                txtsyngayvl.Focus();
            }
        }
        //
        //tab Trình độ
        //

        //Hiển thị dữ liệu vào trang kinh nghiệm
        private void HienThiDLKN(string ma)
        {
            cngoaingu.CapNhatNN(ma);
            cchuyenmon.CapNhatCM(ma);
            dgv_ChitietCM.DataSource = cchuyenmon.ds;
            dgv_ChitietCM.DataMember = "NV_ChuyenMon1";

            dgv_ChitietNN.DataSource = cngoaingu.ds;
            dgv_ChitietNN.DataMember = "NV_NgoaiNgu1";

            //tạo combo ngoại ngữ trong dgv_ChiTietNN
            GridViewComboBoxColumn grdCCNN = new GridViewComboBoxColumn("Tên ngoại ngữ", "mangoaingu");
            grdCCNN.HeaderText = "Tên ngoại ngữ";
            grdCCNN.Width = 140;
            grdCCNN.DataSource = cdmnn.taocombonn();
            grdCCNN.DisplayMember = "tenngoaingu";
            grdCCNN.ValueMember = "mangoaingu";

            dgv_ChitietNN.Columns.RemoveAt(2);
            dgv_ChitietNN.Columns.Insert(2, grdCCNN);
            dgv_ChitietNN.Columns[0].HeaderText = "ID";
            dgv_ChitietNN.Columns[1].IsVisible = false;
            dgv_ChitietNN.Columns[3].HeaderText = "Trình độ";
            dgv_ChitietNN.Columns[4].HeaderText = "Nơi cấp";
            dgv_ChitietNN.Columns[5].HeaderText = "Ngày cấp";
            //dgv_ChitietNN.Columns[4].HeaderText = "ID";



            //Tạo combo chuyên môn trong dgv_ChiTietChuyenMon
            GridViewComboBoxColumn grdCCCM = new GridViewComboBoxColumn("Tên chuyên môn", "machuyenmon");
            dgv_ChitietCM.Columns.RemoveAt(2);
            dgv_ChitietCM.Columns.RemoveAt(2);
            grdCCCM.HeaderText = "Tên chuyên môn";
            grdCCCM.Width = 150;
            grdCCCM.DataSource = cdmcm.taocombocm();
            grdCCCM.DisplayMember = "tenchuyenmon";
            grdCCCM.ValueMember = "machuyenmon";

            dgv_ChitietCM.Columns[1].IsVisible = false;
            dgv_ChitietCM.Columns.Insert(2, grdCCCM);
            dgv_ChitietCM.Columns[3].HeaderText = "Ngày cấp";
            dgv_ChitietCM.Columns[4].HeaderText = "Trường";

            //Tạo combo trình độ trong dgv_ChiTietChuyenMon
            GridViewComboBoxColumn grdCCTD = new GridViewComboBoxColumn("Tên Trình Độ", "matrinhdo");
            grdCCTD.HeaderText = " Tên Trình Độ";
            grdCCTD.Width = 140;
            grdCCTD.DataSource = cdmtd.taocombotd();
            grdCCTD.DisplayMember = "tentrinhdo";
            grdCCTD.ValueMember = "matrinhdo";
            dgv_ChitietCM.Columns.Insert(3, grdCCTD);


        }

        private void cmdcapnhatnn_Click(object sender, EventArgs e)
        {
            cngoaingu.sc.Open();
            try
            {

                cngoaingu.sda.Update(cngoaingu.ds, "NV_ngoaingu1");
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    RadMessageBox.Show("\nID này đang tồn tại, không thể cập nhật !\n", "Thông Báo", MessageBoxButtons.OK, RadMessageIcon.Error);
                else
                    RadMessageBox.Show("\nDữ liệu nhập vào không đúng,vui lòng kiểm tra lại !\n", "Thông Báo", MessageBoxButtons.OK, RadMessageIcon.Error);
                //RadMessageBox.Show("\nKhông Thực Hiện Được\n", "Thông Báo");
            }
            cngoaingu.sc.Close();
        }

        private void cmdcapnhatbc_Click(object sender, EventArgs e)
        {
            cchuyenmon.sc.Open();
            try
            {
                cchuyenmon.sda.Update(cchuyenmon.ds, "NV_ChuyenMon1");
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    RadMessageBox.Show("\n ID này đang tồn tại, không thể cập nhật !\n", "Thông Báo", MessageBoxButtons.OK, RadMessageIcon.Error);
                else
                    RadMessageBox.Show("\n Dữ liệu nhập vào không được rỗng !\n Vui lòng kiểm tra lại !\n", "Thông Báo", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
            cchuyenmon.sc.Close();
        }

        private void dgv_ChitietNN_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e != null) RadMessageBox.Show("  \n Bạn đã nhập sai kiểu dữ liệu \n Xem hướng đẫn phần tên cột\n", "Thông báo");
        }

        private void dgv_ChitietCM_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e != null) RadMessageBox.Show("  \n Bạn đã nhập sai kiểu dữ liệu \n Xem hướng đẫn phần tên cột\n", "Thông báo");
        }
    }
}