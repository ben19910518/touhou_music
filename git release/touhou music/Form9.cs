﻿using System;
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
    public partial class Form9 : Form
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        private string addoricode;
        private string addorigin;
        public Form9()
        {
            InitializeComponent();
        }
        private void connectSQL()
        {
            conn = new SqlConnection(DataPool.conString);

            conn.Open();
        }

        private void closeSQL()
        {
            conn.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            addoricode = textBox1.Text;
            addorigin = textBox2.Text.Replace("'", "''");
            if (addoricode == string.Empty || addorigin == string.Empty)
            {
                MessageBox.Show("请输入数据", "提示");
                return;
            }
            try
            {
                connectSQL();
                string sql = "insert into [ori] values ('" + addoricode + "','" + addorigin + "')";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteScalar();
                closeSQL();
            }
            catch { MessageBox.Show("错误", "消息"); closeSQL(); return; }
            MessageBox.Show("添加成功！","消息");

        }
    }
}
