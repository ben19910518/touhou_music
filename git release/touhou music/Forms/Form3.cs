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
    public partial class Form3 : Form
    {
        private string userid;
        private string password;
        private string password2;

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

        public Form3()
        {

            InitializeComponent();
            DataPool.Form3 = this;
            button1.Enabled = checkBox1.Checked;
            textBox1.Select(textBox1.Text.Length,0 );
            
        }



        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = checkBox1.Checked;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            userid = textBox2.Text;
            password = textBox3.Text;
            password2 = textBox4.Text;
            if (password != password2)
            {
                MessageBox.Show("两次密码输入不一致!", "提示信息");
                return;
            }
            if (userid == string.Empty)
            {
                MessageBox.Show("请输入用户名", "提示");
                return;
            }
            if (userid.Contains("\'"))
            {
                MessageBox.Show("非法用户名!", "警告");
                return;
            }
            if (password == string.Empty || password2==string.Empty)
            {
                MessageBox.Show("请输入密码", "提示");
                return;
            }
            if (textBox5.Text == string.Empty || textBox6.Text == string.Empty)
            {
                MessageBox.Show("请输入找回密码必要的信息", "提示");
                return;
            }
            password = MD5Create(password);
            ////////////////////////////////////////////////////////////////
            using (sqlAdapter sqladp = new sqlAdapter())
            {
                string sql = "select username from [user] where username = '" + userid + "'";
                object obj = sqladp.ExecuteScalar(sql);
                //对数据库查询出的值进行判断
                if (obj != null)
                {
                    MessageBox.Show("用户名已存在!", "提示信息");
                    return;
                }
                else
                {
                    sql = "insert into [user] values ('" + userid + "','" + password + "','3 ','" + textBox5.Text + "','" + textBox6.Text + "')";
                    sqladp.ExecuteScalar(sql);
                    MessageBox.Show("注册成功!", "提示信息");
                    this.Dispose();
                    return;
                    /*if (password == obj.ToString())
                    {
                
                        //MessageBox.Show("登录成功", "提示");
                        DataPool.currentID = userid;
                        DataPool.currentMD5Password = obj.ToString();
                        //this.Visible = false;
                        this.Dispose();

                    }
                    else
                        MessageBox.Show("密码错误", "提示");
                  
                     */
                }

            }
            ////////////////////////////////////////////////////////////////////



        }

        private void textBox1_MouseMove(object sender, MouseEventArgs e)
        {
            textBox1.SelectionLength = 0;
        }
  
    }
}
