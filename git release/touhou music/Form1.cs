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
using System.Runtime.InteropServices;

namespace touhou_music
{



    public partial class Form1 : Form
    {
        [DllImport("Kernel32.dll")]
        public static extern bool WritePrivateProfileString(string strAppName,string strKeyName,string strString,string strFileName);
        [DllImport("Kernel32.dll")]
        public static extern int GetPrivateProfileString(string strAppName,string strKeyName,string strDefault,StringBuilder sbReturnString,int nSize,string strFileName);

        private int flag=0;
        private string userid;
        private string password;
        private SqlConnection conn;
        private SqlCommand cmd;
        private StringBuilder userid1;
        private StringBuilder password1;

        public Form1()
        {  
            StringBuilder userid1 = new StringBuilder(256);
            StringBuilder password1 = new StringBuilder(256);


            InitializeComponent();
            DataPool.Form1 = this;
        GetPrivateProfileString("user", "username","" ,userid1,256,".\\THMconfig.ini");
        GetPrivateProfileString("user","password","" ,password1,256,".\\THMconfig.ini");
           
            textBox1.Text = Convert.ToString( userid1);
            textBox2.Text = Convert.ToString(password1);

            flag = 1;

            checkBox1.Checked = true;

        }
        public string MD5kamiro(string password)
        {
            string pwd = "";
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(password));

            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符
                //pwd = pwd + s[i].ToString("x");
                pwd = pwd + s[i].ToString("x");
            }
            return pwd;
        }

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

        private void button1_Click(object sender, EventArgs e)
        {
            userid = textBox1.Text;
            password = textBox2.Text;
            

            if (userid == string.Empty)
            {
                MessageBox.Show("请输入用户名", "提示");
                return;
            }

            for (int i = 0; i < userid.Length; i++)
            {
                if (userid[i] == '\'')
                {
                    MessageBox.Show("用户名不存在!", "提示");
                    return;
                }
            }



            if (flag == 1)
            {
                try
                {
                    connectSQL();
                    // connectkamiroSQL();
                    string sql = "select password from [user] where username = '" + userid + "'";
                    cmd = new SqlCommand(sql, conn);

                    string obj = cmd.ExecuteScalar() as string;
                    //对数据库查询出的值进行判断
                    if (obj == null)
                    {
                        connectkamiroSQL();
                        sql = "select password from [userTable] where userName = '" + userid + "'";
                        cmd = new SqlCommand(sql, conn);

                        string kamiropass = cmd.ExecuteScalar() as string;
                        if (password == kamiropass)
                        {

                            //MessageBox.Show("登录成功", "提示");
                            DataPool.currentID = userid;
                            DataPool.currentMD5Password = obj;
                            //this.Visible = false;
                            MessageBox.Show("欢迎来自KCM的账号！", "消息");
                            if (checkBox1.Checked == true)
                            {
                                WritePrivateProfileString("user", "username", DataPool.currentID, ".\\THMconfig.ini");
                                WritePrivateProfileString("user", "password", DataPool.currentMD5Password, ".\\THMconfig.ini");
                            }
                            else 
                            {
                                WritePrivateProfileString("user", "username", "", ".\\THMconfig.ini");
                                WritePrivateProfileString("user", "password", "", ".\\THMconfig.ini");
                            }

                            this.Dispose();


                        }
                        else
                            MessageBox.Show("密码错误", "提示");


                        closeSQL();

                    }
                    else
                    {
                        if (password == obj)
                        {

                            //MessageBox.Show("登录成功", "提示");
                            DataPool.currentID = userid;
                            DataPool.currentMD5Password = obj;
                            //this.Visible = false;
                            if (checkBox1.Checked == true)
                            {
                                WritePrivateProfileString("user", "username", DataPool.currentID, ".\\THMconfig.ini");
                                WritePrivateProfileString("user", "password", DataPool.currentMD5Password, ".\\THMconfig.ini");
                            }
                            else
                            {
                                WritePrivateProfileString("user", "username", " ", ".\\THMconfig.ini");
                                WritePrivateProfileString("user", "password", " ", ".\\THMconfig.ini");
                            }
                            this.Dispose();


                        }
                        else
                            MessageBox.Show("密码错误", "提示");
                    }

                    closeSQL();
                }
                catch { MessageBox.Show("连接失败！", "提示"); return; }

            }


            else
            {


                try
                {
                    connectSQL();
                    // connectkamiroSQL();
                    string sql = "select password from [user] where username = '" + userid + "'";
                    cmd = new SqlCommand(sql, conn);

                    string obj = cmd.ExecuteScalar() as string;
                    //对数据库查询出的值进行判断
                    if (obj == null)
                    {
                        connectkamiroSQL();
                        sql = "select password from [userTable] where userName = '" + userid + "'";
                        cmd = new SqlCommand(sql, conn);

                        string kamiropass = cmd.ExecuteScalar() as string;
                        if (MD5kamiro(password) == kamiropass)
                        {

                            //MessageBox.Show("登录成功", "提示");
                            DataPool.currentID = userid;
                            DataPool.currentMD5Password = obj;
                            //this.Visible = false;
                            MessageBox.Show("欢迎来自KCM的账号！", "消息");
                            if (checkBox1.Checked == true)
                            {
                                WritePrivateProfileString("user", "username", DataPool.currentID, ".\\THMconfig.ini");
                                WritePrivateProfileString("user", "password", DataPool.currentMD5Password, ".\\THMconfig.ini");
                            }
                            else
                            {
                                WritePrivateProfileString("user", "username", "", ".\\THMconfig.ini");
                                WritePrivateProfileString("user", "password", "", ".\\THMconfig.ini");
                            }
                            this.Dispose();


                        }
                        else
                            MessageBox.Show("密码错误", "提示");


                        closeSQL();

                    }
                    else
                    {
                        if (MD5Create(password) == obj)
                        {

                            //MessageBox.Show("登录成功", "提示");
                            DataPool.currentID = userid;
                            DataPool.currentMD5Password = obj;
                            //this.Visible = false;
                            if (checkBox1.Checked == true)
                            {
                                WritePrivateProfileString("user", "username", DataPool.currentID, ".\\THMconfig.ini");
                                WritePrivateProfileString("user", "password", DataPool.currentMD5Password, ".\\THMconfig.ini");
                            }
                            else
                            {
                                WritePrivateProfileString("user", "username", "", ".\\THMconfig.ini");
                                WritePrivateProfileString("user", "password", "", ".\\THMconfig.ini");
                            }

                            this.Dispose();


                        }
                        else
                            MessageBox.Show("密码错误", "提示");
                    }

                    closeSQL();
                }
                catch { MessageBox.Show("连接失败！", "提示"); return; }
            }
        }
        private void connectkamiroSQL()
        {
            conn = new SqlConnection(DataPool.conkamiro);

            conn.Open();
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

        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (DataPool.currentID == string.Empty)
                Application.Exit();
        }

        private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                button1_Click(sender, e);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form3 Form3 = new Form3();
            Form3.ShowDialog();
        }

   /*     private void button4_Click(object sender, EventArgs e)
        {
            /*   connectSQL();

               string sql = "select autho from [user] where username = '" + userid + "'";
               cmd = new SqlCommand(sql, conn);

               string autho = cmd.ExecuteScalar() as string;
               closeSQL();
               if (autho == "3")
               {
                   MessageBox.Show("没有权限进入此版块", "消息");
                   return;
               }



               }

               
            userid = textBox1.Text;
            password = textBox2.Text;

            if (userid == string.Empty)
            {
                MessageBox.Show("请输入用户名", "提示");
                return;
            }

            for (int i = 0; i < userid.Length; i++)
            {
                if (userid[i] == '\'')
                {
                    MessageBox.Show("用户名不存在!", "提示");
                    return;
                }
            }
            
            connectSQL();

            string sql = "select password from [user] where username = '" + userid + "'";
            cmd = new SqlCommand(sql, conn);

            object  obj = cmd.ExecuteScalar();
            //对数据库查询出的值进行判断
            if (obj == null)
            {
                MessageBox.Show("用户名不存在!", "提示");
                return;
            }
            else
            {
                if (MD5Create(password) == obj.ToString())
                {

                    //MessageBox.Show("登录成功", "提示");
                    DataPool.currentID = userid;
                    DataPool.currentMD5Password = obj.ToString();
                    //this.Visible = false;
                    string sql1 = "select autho from [user] where username = '" + userid + "'";
                    cmd = new SqlCommand(sql1, conn);
                    string autho = cmd.ExecuteScalar() as string;
                    closeSQL();

                    if (autho == "3")
                    {
                        MessageBox.Show("没有权限", "提示");
                        return;
                    }
                    else
                    {
                        Form4 Form4 = new Form4();
                        Form4.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("密码错误", "提示");
                    closeSQL();
                }
            }

      
      

        }
*/
        private void button3_Click(object sender, EventArgs e)
        {
            Form5 Form5 = new Form5();
            Form5.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form8 Form8 = new Form8();
            Form8.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            userid = textBox1.Text;
            password = textBox2.Text;

            if (userid == string.Empty)
            {
                MessageBox.Show("请输入用户名", "提示");
                return;
            }

            for (int i = 0; i < userid.Length; i++)
            {
                if (userid[i] == '\'')
                {
                    MessageBox.Show("用户名不存在!", "提示");
                    return;
                }
            }

            if (flag == 1)
            {
                connectSQL();

                string sql = "select password from [user] where username = '" + userid + "'";
                cmd = new SqlCommand(sql, conn);

                object obj = cmd.ExecuteScalar();
                //对数据库查询出的值进行判断
                if (obj == null)
                {
                    closeSQL();
                    MessageBox.Show("用户名不存在!", "提示");
                    return;
                }
                else
                {
                    if (password == obj.ToString())
                    {

                        //MessageBox.Show("登录成功", "提示");
                        DataPool.currentID = userid;
                        DataPool.currentMD5Password = obj.ToString();
                        //this.Visible = false;
                        string sql1 = "select autho from [user] where username = '" + userid + "'";
                        cmd = new SqlCommand(sql1, conn);
                        string autho = cmd.ExecuteScalar() as string;
                        closeSQL();

                        if (autho == "0")
                        {
                            Form10 Form10 = new Form10();
                            Form10.ShowDialog();

                        }
                        else
                        {

                            MessageBox.Show("没有权限", "提示");
                            return;

                        }
                    }
                    else
                    {
                        MessageBox.Show("密码错误", "提示");
                        closeSQL();
                    }
                }
            }
            else
            {
                connectSQL();

                string sql = "select password from [user] where username = '" + userid + "'";
                cmd = new SqlCommand(sql, conn);

                object obj = cmd.ExecuteScalar();
                //对数据库查询出的值进行判断
                if (obj == null)
                {
                    closeSQL();
                    MessageBox.Show("用户名不存在!", "提示");
                    return;
                }
                else
                {
                    if (MD5Create(password) == obj.ToString())
                    {

                        //MessageBox.Show("登录成功", "提示");
                        DataPool.currentID = userid;
                        DataPool.currentMD5Password = obj.ToString();
                        //this.Visible = false;
                        string sql1 = "select autho from [user] where username = '" + userid + "'";
                        cmd = new SqlCommand(sql1, conn);
                        string autho = cmd.ExecuteScalar() as string;
                        closeSQL();

                        if (autho == "0")
                        {
                            Form10 Form10 = new Form10();
                            Form10.ShowDialog();

                        }
                        else
                        {

                            MessageBox.Show("没有权限", "提示");
                            return;

                        }
                    }
                    else
                    {
                        MessageBox.Show("密码错误", "提示");
                        closeSQL();
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            flag = 0;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            flag = 0;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        
        }
    }
}
  
