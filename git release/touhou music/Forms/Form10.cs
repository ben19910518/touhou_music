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
        private SqlConnection conn;
        private SqlCommand cmd;
        //private string username;
        //private string autho;
        private void connectSQL()
        {
            conn = new SqlConnection(DataPool.conString);

            conn.Open();
        }

        private void closeSQL()
        {
            conn.Close();
        }
        public Form10()
        {
            InitializeComponent();
            connectSQL(); 
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter("select distinct username from [user]", conn);

            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                comboBox1.Items.Add(ds.Tables[0].Rows[i][0]);
            }

            comboBox1.SelectedIndex = 0;
            closeSQL();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            connectSQL();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter("select autho from [user] where username='"+comboBox1.Text+"'", conn);

 
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                comboBox2.Items.Add(ds.Tables[0].Rows[i][0]);
            }

            comboBox2.SelectedIndex = 0;
            closeSQL();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connectSQL();

            string sql= "update [user] set autho='" + comboBox2.Text + "' where username='" + comboBox1.Text + "'";

            cmd = new SqlCommand(sql, conn);
            cmd.ExecuteScalar();
            closeSQL();
            MessageBox.Show("修改成功！", "消息");
        }
    }
}
