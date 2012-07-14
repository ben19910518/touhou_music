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
using System.IO;

namespace touhou_music
{
    public partial class Form4 : Form
    {
        private byte[] imagebytes;
        private string fullpath;
        private string addacode;
        private string addaname;
        private string addtime;
        private string addgcode;
        private string addscode;
        private string addgname;
        private string addoricode;
        private string addorigin;
        private string addsname;
        private string addtrack;
        private string addarranger;
        private string addlyric;
        private string addvocal;
        private string addstyle;

        public Form4()
        {
            InitializeComponent();
        }

        private byte[] ImageToStream(string fileName)
        {
            Bitmap image = new Bitmap(fileName);
            MemoryStream stream = new MemoryStream();
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
            return stream.ToArray();
        }
        private void Form4_Load(object sender, EventArgs e)
        {

         //  imagebytes = ImageToStream(".\\x.jpg");
         //  imagebytes[0] =0;
            imagebytes = new byte[1];
            imagebytes[0] = 0;

            //DataSet ds;
            //DataSet ds2;
            //DataSet ds3;
            //DataSet ds4;
            //DataSet ds5;
            //DataSet ds6;
            //DataSet ds7;
            //DataSet ds8;
            //DataSet ds9;
            //DataSet ds10;
            //DataSet ds11;
            //DataSet ds12;

            List<ComboBox> CB = new List<ComboBox>(11);
            DataTable[] tp;
            CB.Add(comboBox2);
            CB.Add(comboBox3);
            CB.Add(comboBox4);
            CB.Add(comboBox5);
            CB.Add(comboBox6);
            CB.Add(comboBox7);
            CB.Add(comboBox8);
            CB.Add(comboBox9);
            CB.Add(comboBox10);
            CB.Add(comboBox11);
            CB.Add(comboBox12);
            using (sqlAdapter sqladp = new sqlAdapter(DataPool.conString))
            {
                try
                {
                     tp = sqladp.getDataTables("select distinct acode from [album]",
                        "select distinct gname from [group]", "select distinct aname from [album]",
                        "select distinct [time] from [album]", "select distinct track from [song]",
                        "select distinct lyric from [song]", "select distinct sname from [song]",
                        "select distinct arranger from [song]", "select distinct style from [song]",
                        "select distinct vocal from [song]", "select distinct origin from [ori]");
                     for (int i = 0; i < tp.Length; i++)
                     {
                         try
                         {
                             for (int j = 0; j < tp[i].Rows.Count; j++)
                             {
                                 CB[i].Items.Add(tp[i].Rows[j][0]);
                             }
                             CB.ElementAt(i).SelectedIndex = 0;
                         }
                         catch (System.Exception ex)
                         {
                             System.Diagnostics.Debug.Write(ex.Message);
                         }
                     }
                }
                catch (System.Exception ex)
                {
                    System.Diagnostics.Debug.Write(ex.Message);
                }
            }

            //comboBox2.SelectedIndex = 0;

            //for (int i = 0; i < ds3.Tables[0].Rows.Count; i++)
            //{
            //    comboBox3.Items.Add(ds3.Tables[0].Rows[i][0]);
            //}

            //comboBox3.SelectedIndex = 0;

            //for (int i = 0; i < ds4.Tables[0].Rows.Count; i++)
            //{
            //    comboBox4.Items.Add(ds4.Tables[0].Rows[i][0]);
            //}

            //comboBox4.SelectedIndex = 0;

            //for (int i = 0; i < ds5.Tables[0].Rows.Count; i++)
            //{
            //    comboBox5.Items.Add(ds5.Tables[0].Rows[i][0]);
            //}

            //comboBox5.SelectedIndex = 0;

            //for (int i = 0; i < ds6.Tables[0].Rows.Count; i++)
            //{
            //    comboBox6.Items.Add(ds6.Tables[0].Rows[i][0]);
            //}

            //comboBox6.SelectedIndex = 0;

            //for (int i = 0; i < ds7.Tables[0].Rows.Count; i++)
            //{
            //    comboBox7.Items.Add(ds7.Tables[0].Rows[i][0]);
            //}

            //comboBox7.SelectedIndex = 0;

            //for (int i = 0; i < ds8.Tables[0].Rows.Count; i++)
            //{
            //    comboBox8.Items.Add(ds8.Tables[0].Rows[i][0]);
            //}

            //comboBox8.SelectedIndex = 0;

            //for (int i = 0; i < ds9.Tables[0].Rows.Count; i++)
            //{
            //    comboBox9.Items.Add(ds9.Tables[0].Rows[i][0]);
            //}

            //comboBox9.SelectedIndex = 0;

            //for (int i = 0; i < ds10.Tables[0].Rows.Count; i++)
            //{
            //    comboBox10.Items.Add(ds10.Tables[0].Rows[i][0]);
            //}

            //comboBox10.SelectedIndex = 0;

            //for (int i = 0; i < ds11.Tables[0].Rows.Count; i++)
            //{
            //    comboBox11.Items.Add(ds11.Tables[0].Rows[i][0]);
            //}

            //comboBox11.SelectedIndex = 0;

            //for (int i = 0; i < ds12.Tables[0].Rows.Count; i++)
            //{
            //    comboBox12.Items.Add(ds12.Tables[0].Rows[i][0]);
            //}

            //comboBox12.SelectedIndex = 0;
            
        }

        private byte[] ImageToStream(Image image)
        {
            throw new NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (comboBox2.Text == "" || comboBox4.Text == "" || comboBox6.Text == "" || comboBox3.Text == "" || comboBox8.Text == "" || comboBox9.Text == "")
            {
                MessageBox.Show("请填写完整带“*”的数据", "提示");
                return;
            }
            //________________________________________________________________________________________________
            addacode = comboBox2.Text;
            addaname = comboBox4.Text.Replace("'", "''");
            addtime = comboBox5.Text;
            addgcode = addacode.Substring(0, 4);
            addtrack = comboBox6.Text;
            addscode = addacode + addtrack;
            addgname = comboBox3.Text.Replace("'", "''");
            addorigin = comboBox12.Text;
            addsname = comboBox8.Text.Replace("'", "''");
            addarranger = comboBox9.Text.Replace("'", "''");
            addlyric = comboBox7.Text.Replace("'", "''");
            addvocal = comboBox11.Text.Replace("'", "''");
            addstyle = comboBox10.Text.Replace("'", "''");

            using (sqlAdapter sqladp = new sqlAdapter(DataPool.conString))
            {

                string sql = "select oricode from [ori] where origin = '" + addorigin + "'";
                addoricode = sqladp.ExecuteScalar(sql) as string;
                if (addoricode == null)
                {
                    MessageBox.Show("没有找到原曲！", "消息提示");
                    Form9 Form9 = new Form9();
                    Form9.ShowDialog();
                    return;
                }

                sql = "select acode from [album] where acode = '" + addacode + "'";
                object obj = sqladp.ExecuteScalar(sql);

                if (obj == null)
                {
                    sql = "insert into [album] values ('" + addacode + "','" + addaname + "','" + addtime + "',@cover)";
                    sqladp.setCommand(sql);
                    sqladp.Command().Parameters.Add("cover", SqlDbType.Image);
                    sqladp.Command().Parameters["cover"].Value = imagebytes;
                    sqladp.Command().ExecuteNonQuery();
                }

                sql = "select gcode from [group] where gcode = '" + addgcode + "'";
                obj = sqladp.ExecuteScalar(sql);
                //对数据库查询出的值进行判断
                if (obj == null)
                {
                    sql = "insert into [group] values ('" + addgname + "','" + addgcode + "')";
                    sqladp.ExecuteScalar(sql);

                }

                try
                {
                    sql = "insert into [song] values ('" + addsname + "','" + addtrack + "','" + addarranger + "','" + addlyric + "','" + addvocal + "','" + addoricode + "','" + addstyle + "','" + addscode + "','" + DataPool.currentID + "')";
                    sqladp.ExecuteScalar(sql);

                    sql = "insert into [gas] values ('" + addgcode + "','" + addacode + "','" + addscode + "')";
                    sqladp.ExecuteScalar(sql);

                    MessageBox.Show("提交成功!", "提示信息");
                }
                catch (Exception)
                {
                    MessageBox.Show("错误", "提示信息");
                    return;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string autho;
            using (sqlAdapter sqladp = new sqlAdapter(DataPool.conString))
            {
                string sql = "select autho from [user] where username = '" + DataPool.currentID + "'";
                autho = sqladp.ExecuteScalar(sql) as string;
            }
            if (autho == "1" || autho == "0")
            {
                Form7 Form7 = new Form7();
                Form7.ShowDialog();
            }
            else
            {
                MessageBox.Show("权限不足", "提示");
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string autho;
            using (sqlAdapter sqladp = new sqlAdapter(DataPool.conString))
            {
                string sql = "select autho from [user] where username = '" + DataPool.currentID + "'";
                autho = sqladp.ExecuteScalar(sql) as string;
            }
            if (autho == "1" || autho == "0")
            {
                Form9 Form9 = new Form9();
                Form9.ShowDialog();
            }
            else
            {
                MessageBox.Show("权限不足", "提示");
                return;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string autho;
            using (sqlAdapter sqladp = new sqlAdapter(DataPool.conString))
            {
                string sql = "select autho from [user] where username = '" + DataPool.currentID + "'";
                autho = sqladp.ExecuteScalar(sql) as string;
            }
            if (autho == "1" || autho == "0")
            {
                Form12 Form12 = new Form12();
                Form12.ShowDialog();
            }
            else
            {
                MessageBox.Show("权限不足", "提示");
                return;
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "*jpg|*.JPG|*.gif|*.GIF|*.bmp|*.BMP";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fullpath = openFileDialog1.FileName;//文件路径

                //   FileStream fs = new FileStream(fullpath, FileMode.Open);

                //   byte[] imagebytes = new byte[fs.Length];

                //      BinaryReader br = new BinaryReader(fs);

                //     imagebytes = br.ReadBytes(Convert.ToInt32(fs.Length));

                //打开数据库
                imagebytes = ImageToStream(fullpath);
              
                MemoryStream ms = new MemoryStream(imagebytes);

                Bitmap bmpt = new Bitmap(ms);

                pictureBox1.Image = bmpt;


            }
        }


        
    }
}
