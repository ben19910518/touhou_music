using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using touhou_music.datas;
using System.Diagnostics;
namespace touhou_music
{
    public partial class Form10 : Form
    {
        //private string username;
        //private string autho;
        public Form10()
        {
            InitializeComponent();
            using (sqlAdapter sqladp = new sqlAdapter())
            {
                DataSet ds;
                sqladp.getDataSet("select distinct username from [user]", out ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    comboBox1.Items.Add(ds.Tables[0].Rows[i][0]);
                }
                comboBox1.SelectedIndex = 0;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            using (sqlAdapter sqladp = new sqlAdapter())
            {
                DataSet ds;
                sqladp.getDataSet("select autho from [user] where username='" + comboBox1.Text + "'", out ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    comboBox2.Items.Add(ds.Tables[0].Rows[i][0]);
                }
                comboBox2.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (sqlAdapter sqladp = new sqlAdapter())
            {
                string sql = "update [user] set autho='" + comboBox2.Text + "' where username='" + comboBox1.Text + "'";
                sqladp.ExecuteScalar(sql);
            }
            MessageBox.Show("修改成功！", "消息");
        }
    }
}
