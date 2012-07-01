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
using System.Security.Cryptography; 
namespace touhou_music
{
    public partial class Form8 : Form
    {
        private string userid;
        private string password;
        private string password2;
        private SqlConnection conn;
        private SqlCommand cmd;

        private string MD5Create(string STR) //STR为待加密的string  
        {
            string pwd = "";
            //pwd为加密结果  
            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.Unicode.GetBytes(STR));
            //这里的UTF8是编码方式，你可以采用你喜欢的方式进行，比如UNcode等等  
            for (int i = 0; i < s.Length; i++)
            {
                pwd = pwd + s[i].ToString();
            }
            return pwd;
        }  
        public Form8()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            userid = textBox1.Text;
            if (userid == string.Empty)
            {
                MessageBox.Show("请输入用户名", "提示");
                return;
            }
            for (int i = 0; i < userid.Length; i++)
            {
                if (userid[i] == '\'')
                {
                    MessageBox.Show("用户名不存在!", "提示信息");
                    return;
                }
            }
            connectSQL();
            string sql = "select quest from [user] where username = '" + userid + "'";
            cmd = new SqlCommand(sql, conn);

            string quest= cmd.ExecuteScalar() as string;
            textBox2.Text = quest;
            closeSQL();
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

        private void button2_Click(object sender, EventArgs e)
        {
            password = textBox4.Text;
            password2 = textBox5.Text;
            if (password == string.Empty || password2 == string.Empty)
            {
                MessageBox.Show("请输入密码", "提示");
                return;
            }
            connectSQL();
            password = MD5Create(password);
            string sql = "select ans from [user] where username = '" + userid + "'";
            cmd = new SqlCommand(sql, conn);

            string ans = cmd.ExecuteScalar() as string;
            if (ans == textBox3.Text)
            {
                sql = "update [user] set password='" + password + "' where username='" + userid + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteScalar();
                closeSQL();
                MessageBox.Show("修改密码成功！", "消息");

            }
            else
            {
                closeSQL();
                MessageBox.Show("验证失败！", "消息");
            }
        }
    }
}
