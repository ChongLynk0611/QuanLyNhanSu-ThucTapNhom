﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace QuanLyNhanSu
{
    public partial class frmAbout : TransDialog//RadForm
    {
        public frmAbout()
        {
            InitializeComponent();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("dxdiag");
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:ngothanhdat1989@gmail.com");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(this.linkLabel2.Text);
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            
            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
          
        }
    }
}
