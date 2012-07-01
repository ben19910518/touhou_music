using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using touhou_music.datas;
using System.Data.SqlClient;
using System.Diagnostics;

namespace touhou_music
{
    public partial class Form5 : Form
    {
        private SqlConnection conn;
        private SqlCommand cmd;

        public Form5()
        {
            InitializeComponent();
            textBox1.Text = DataPool.server;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataPool.server = textBox1.Text;
            //Trace.WriteLine(DataPool.server);
            DataPool.conString = "Data Source=" + textBox1.Text + ";Initial Catalog=touhou music;User ID=thview";
            try
            {

                connectSQL();
                closeSQL();
            }
            catch { MessageBox.Show("连接失败！", "提示"); return; }
            MessageBox.Show("连接成功！", "提示");
        }
   



            private void connectSQL()
        {
          //  conn = new SqlConnection("Data Source="+textBox1.Text+";Initial Catalog=touhou music;User ID=thview");
            conn = new SqlConnection(DataPool.conString);

            conn.Open();
        }

        private void closeSQL()
        {
            conn.Close();
        }
    }
}




