using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using QuanLyNhanSu.Class;
namespace QuanLyNhanSu
{
    public class clsDMDanToc :clsdm
    {
        public DataSet taocombo()
        {
            clsDMDanToc  c = new clsDMDanToc();
            c.sc.Open();
            string sql1 = "Select * from tbl_DMDanToc";
            SqlCommand scmd = new SqlCommand(sql1, c.sc);
            c.sda.SelectCommand = scmd;
            c.sda.Fill(c.ds, "DMDanToc");
            c.sc.Close();
            c.ds.WriteXmlSchema("rptdantoc.xml");
            return c.ds;
        }
        
    }
}
