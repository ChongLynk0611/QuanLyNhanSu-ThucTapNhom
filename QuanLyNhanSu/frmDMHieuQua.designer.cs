﻿namespace QuanLyNhanSu
{
    partial class frmDMHieuQua
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDMHieuQua));
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgv_DMhieuqua = new Telerik.WinControls.UI.RadGridView();
            this.txtphucap = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtmahq = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.grpbox = new System.Windows.Forms.GroupBox();
            this.cmdluu = new System.Windows.Forms.Button();
            this.cmdthoat = new System.Windows.Forms.Button();
            this.cmdthem = new System.Windows.Forms.Button();
            this.cmdCapnhat = new System.Windows.Forms.Button();
            this.cmdxoa = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DMhieuqua)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DMhieuqua.MasterGridViewTemplate)).BeginInit();
            this.grpbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.AliceBlue;
            this.panel1.Controls.Add(this.dgv_DMhieuqua);
            this.panel1.Controls.Add(this.txtphucap);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtmahq);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(9, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(243, 211);
            this.panel1.TabIndex = 0;
            // 
            // dgv_DMhieuqua
            // 
            this.dgv_DMhieuqua.Location = new System.Drawing.Point(6, 68);
            // 
            // 
            // 
            this.dgv_DMhieuqua.MasterGridViewTemplate.AllowDragToGroup = false;
            this.dgv_DMhieuqua.MasterGridViewTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgv_DMhieuqua.MasterGridViewTemplate.EnableGrouping = false;
            this.dgv_DMhieuqua.Name = "dgv_DMhieuqua";
            this.dgv_DMhieuqua.ReadOnly = true;
            this.dgv_DMhieuqua.Size = new System.Drawing.Size(233, 139);
            this.dgv_DMhieuqua.TabIndex = 19;
            // 
            // txtphucap
            // 
            this.txtphucap.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtphucap.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtphucap.Location = new System.Drawing.Point(140, 40);
            this.txtphucap.MaxLength = 4;
            this.txtphucap.Name = "txtphucap";
            this.txtphucap.ReadOnly = true;
            this.txtphucap.Size = new System.Drawing.Size(53, 22);
            this.txtphucap.TabIndex = 2;
            this.txtphucap.Tag = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(49, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 15);
            this.label2.TabIndex = 18;
            this.label2.Text = "Tỉ Lệ Lương";
            // 
            // txtmahq
            // 
            this.txtmahq.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtmahq.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmahq.Location = new System.Drawing.Point(140, 8);
            this.txtmahq.MaxLength = 2;
            this.txtmahq.Name = "txtmahq";
            this.txtmahq.ReadOnly = true;
            this.txtmahq.Size = new System.Drawing.Size(53, 22);
            this.txtmahq.TabIndex = 1;
            this.txtmahq.Tag = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(49, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 15);
            this.label3.TabIndex = 16;
            this.label3.Text = "Mã Hiệu Quả";
            // 
            // grpbox
            // 
            this.grpbox.BackColor = System.Drawing.Color.Transparent;
            this.grpbox.Controls.Add(this.cmdluu);
            this.grpbox.Controls.Add(this.cmdthoat);
            this.grpbox.Controls.Add(this.cmdthem);
            this.grpbox.Controls.Add(this.cmdCapnhat);
            this.grpbox.Controls.Add(this.cmdxoa);
            this.grpbox.Location = new System.Drawing.Point(254, 3);
            this.grpbox.Name = "grpbox";
            this.grpbox.Size = new System.Drawing.Size(85, 215);
            this.grpbox.TabIndex = 14;
            this.grpbox.TabStop = false;
            // 
            // cmdluu
            // 
            this.cmdluu.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmdluu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdluu.Image = ((System.Drawing.Image)(resources.GetObject("cmdluu.Image")));
            this.cmdluu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdluu.Location = new System.Drawing.Point(6, 130);
            this.cmdluu.Name = "cmdluu";
            this.cmdluu.Size = new System.Drawing.Size(73, 30);
            this.cmdluu.TabIndex = 6;
            this.cmdluu.Text = "Lưu";
            this.cmdluu.UseVisualStyleBackColor = true;
            this.cmdluu.Click += new System.EventHandler(this.cmdluu_Click);
            // 
            // cmdthoat
            // 
            this.cmdthoat.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmdthoat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdthoat.Image = ((System.Drawing.Image)(resources.GetObject("cmdthoat.Image")));
            this.cmdthoat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdthoat.Location = new System.Drawing.Point(6, 165);
            this.cmdthoat.Name = "cmdthoat";
            this.cmdthoat.Size = new System.Drawing.Size(73, 30);
            this.cmdthoat.TabIndex = 7;
            this.cmdthoat.Text = "Thoát";
            this.cmdthoat.UseVisualStyleBackColor = true;
            this.cmdthoat.Click += new System.EventHandler(this.cmdthoat_Click);
            // 
            // cmdthem
            // 
            this.cmdthem.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmdthem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdthem.Image = ((System.Drawing.Image)(resources.GetObject("cmdthem.Image")));
            this.cmdthem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdthem.Location = new System.Drawing.Point(6, 20);
            this.cmdthem.Name = "cmdthem";
            this.cmdthem.Size = new System.Drawing.Size(73, 30);
            this.cmdthem.TabIndex = 3;
            this.cmdthem.Text = "Thêm";
            this.cmdthem.UseVisualStyleBackColor = true;
            this.cmdthem.Click += new System.EventHandler(this.cmdthem_Click);
            // 
            // cmdCapnhat
            // 
            this.cmdCapnhat.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmdCapnhat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCapnhat.Image = ((System.Drawing.Image)(resources.GetObject("cmdCapnhat.Image")));
            this.cmdCapnhat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdCapnhat.Location = new System.Drawing.Point(7, 56);
            this.cmdCapnhat.Name = "cmdCapnhat";
            this.cmdCapnhat.Size = new System.Drawing.Size(73, 30);
            this.cmdCapnhat.TabIndex = 4;
            this.cmdCapnhat.Text = "Sửa";
            this.cmdCapnhat.UseVisualStyleBackColor = true;
            this.cmdCapnhat.Click += new System.EventHandler(this.cmdCapnhat_Click);
            // 
            // cmdxoa
            // 
            this.cmdxoa.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmdxoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdxoa.Image = ((System.Drawing.Image)(resources.GetObject("cmdxoa.Image")));
            this.cmdxoa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdxoa.Location = new System.Drawing.Point(6, 93);
            this.cmdxoa.Name = "cmdxoa";
            this.cmdxoa.Size = new System.Drawing.Size(73, 30);
            this.cmdxoa.TabIndex = 5;
            this.cmdxoa.Text = "Xóa";
            this.cmdxoa.UseVisualStyleBackColor = true;
            this.cmdxoa.Click += new System.EventHandler(this.cmdxoa_Click);
            // 
            // frmDMHieuQua
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Honeydew;
            this.ClientSize = new System.Drawing.Size(355, 228);
            this.Controls.Add(this.grpbox);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDMHieuQua";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Danh Mục Hiệu Quả";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.frmDMHieuQua_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DMhieuqua.MasterGridViewTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DMhieuqua)).EndInit();
            this.grpbox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox grpbox;
        private System.Windows.Forms.Button cmdluu;
        private System.Windows.Forms.Button cmdthoat;
        private System.Windows.Forms.Button cmdthem;
        private System.Windows.Forms.Button cmdCapnhat;
        private System.Windows.Forms.Button cmdxoa;
        private System.Windows.Forms.TextBox txtphucap;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtmahq;
        private System.Windows.Forms.Label label3;
        private Telerik.WinControls.UI.RadGridView dgv_DMhieuqua;

    }
}