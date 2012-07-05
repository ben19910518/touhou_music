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
        private SqlConnection conn;
        private SqlCommand cmd;
        private string            addacode;
        private string     addaname;
          private string   addtime;

      private string       addgcode ;
     private string        addscode ;

      private string       addgname ;

      private string       addoricode  ;
     private string        addorigin ;

      private string       addsname ;
       private string      addtrack ;
     private string        addarranger;
       private string      addlyric ;
      private string       addvocal;
      private string       addstyle;

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

            connectSQL();
            DataSet ds = new DataSet();
            DataSet ds2 = new DataSet();
            DataSet ds3 = new DataSet();
            DataSet ds4 = new DataSet();
            DataSet ds5 = new DataSet();
            DataSet ds6 = new DataSet();
            DataSet ds7 = new DataSet();
            DataSet ds8 = new DataSet();
            DataSet ds9 = new DataSet();
            DataSet ds10 = new DataSet();
            DataSet ds11 = new DataSet();
            DataSet ds12 = new DataSet();



            SqlDataAdapter da2 = new SqlDataAdapter("select distinct acode from [album]", conn);

            da2.Fill(ds2);
            for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
            {
                comboBox2.Items.Add(ds2.Tables[0].Rows[i][0]);
            }

            comboBox2.SelectedIndex = 0;

            SqlDataAdapter da3 = new SqlDataAdapter("select distinct gname from [group]", conn);

            da3.Fill(ds3);
            for (int i = 0; i < ds3.Tables[0].Rows.Count; i++)
            {
                comboBox3.Items.Add(ds3.Tables[0].Rows[i][0]);
            }

            comboBox3.SelectedIndex = 0;

            SqlDataAdapter da4 = new SqlDataAdapter("select distinct aname from [album]", conn);

            da4.Fill(ds4);
            for (int i = 0; i < ds4.Tables[0].Rows.Count; i++)
            {
                comboBox4.Items.Add(ds4.Tables[0].Rows[i][0]);
            }

            comboBox4.SelectedIndex = 0;

            SqlDataAdapter da5 = new SqlDataAdapter("select distinct [time] from [album]", conn);

            da5.Fill(ds5);
            for (int i = 0; i < ds5.Tables[0].Rows.Count; i++)
            {
                comboBox5.Items.Add(ds5.Tables[0].Rows[i][0]);
            }

            comboBox5.SelectedIndex = 0;

            SqlDataAdapter da6 = new SqlDataAdapter("select distinct track from [song]", conn);

            da6.Fill(ds6);
            for (int i = 0; i < ds6.Tables[0].Rows.Count; i++)
            {
                comboBox6.Items.Add(ds6.Tables[0].Rows[i][0]);
            }

            comboBox6.SelectedIndex = 0;

            SqlDataAdapter da7 = new SqlDataAdapter("select distinct lyric from [song]", conn);

            da7.Fill(ds7);
            for (int i = 0; i < ds7.Tables[0].Rows.Count; i++)
            {
                comboBox7.Items.Add(ds7.Tables[0].Rows[i][0]);
            }

            comboBox7.SelectedIndex = 0;

            SqlDataAdapter da8 = new SqlDataAdapter("select distinct sname from [song]", conn);

            da8.Fill(ds8);
            for (int i = 0; i < ds8.Tables[0].Rows.Count; i++)
            {
                comboBox8.Items.Add(ds8.Tables[0].Rows[i][0]);
            }

            comboBox8.SelectedIndex = 0;

            SqlDataAdapter da9 = new SqlDataAdapter("select distinct arranger from [song]", conn);

            da9.Fill(ds9);
            for (int i = 0; i < ds9.Tables[0].Rows.Count; i++)
            {
                comboBox9.Items.Add(ds9.Tables[0].Rows[i][0]);
            }

            comboBox9.SelectedIndex = 0;

            SqlDataAdapter da10 = new SqlDataAdapter("select distinct style from [song]", conn);

            da10.Fill(ds10);
            for (int i = 0; i < ds10.Tables[0].Rows.Count; i++)
            {
                comboBox10.Items.Add(ds10.Tables[0].Rows[i][0]);
            }

            comboBox10.SelectedIndex = 0;

            SqlDataAdapter da11 = new SqlDataAdapter("select distinct vocal from [song]", conn);

            da11.Fill(ds11);
            for (int i = 0; i < ds11.Tables[0].Rows.Count; i++)
            {
                comboBox11.Items.Add(ds11.Tables[0].Rows[i][0]);
            }

            comboBox11.SelectedIndex = 0;

            SqlDataAdapter da12 = new SqlDataAdapter("select distinct origin from [ori]", conn);

            da12.Fill(ds12);
            for (int i = 0; i < ds12.Tables[0].Rows.Count; i++)
            {
                comboBox12.Items.Add(ds12.Tables[0].Rows[i][0]);
            }

            comboBox12.SelectedIndex = 0;
            
            
            
            
            
            
            
            closeSQL();
        }

        private byte[] ImageToStream(Image image)
        {
            throw new NotImplementedException();
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

            if (comboBox2.Text == "" || comboBox4.Text == "" || comboBox6.Text == "" || comboBox3.Text == "" || comboBox8.Text == "" || comboBox9.Text == "")
            { MessageBox.Show("请填写完整带“*”的数据", "提示"); return; }
            
            

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


            connectSQL();

                string sqlori = "select oricode from [ori] where origin = '" + addorigin + "'";
                cmd = new SqlCommand(sqlori, conn);

                addoricode = cmd.ExecuteScalar() as string;

            if(addoricode==null)
            {  MessageBox.Show("没有找到原曲！","消息提示");
            closeSQL();
            Form9 Form9 = new Form9();
            Form9.ShowDialog();

            return;}


                string sql = "select acode from [album] where acode = '" + addacode + "'";
            cmd = new SqlCommand(sql, conn);
                        object obj = cmd.ExecuteScalar();
            //对数据库查询出的值进行判断
            if (obj == null)
            {
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

                sql = "insert into [album] values ('" + addacode + "','" + addaname + "','" + addtime + "',@cover)";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("cover", SqlDbType.Image);

                cmd.Parameters["cover"].Value = imagebytes;

                cmd.ExecuteNonQuery();



                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~




                
            }

             sql = "select gcode from [group] where gcode = '" + addgcode + "'";
            cmd = new SqlCommand(sql, conn);
             obj = cmd.ExecuteScalar();
            //对数据库查询出的值进行判断
            if (obj == null)
            {
                sql = "insert into [group] values ('" + addgname + "','" + addgcode + "')";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteScalar();
                
            }

            try
            {
                sql = "insert into [song] values ('" + addsname + "','" + addtrack + "','" + addarranger + "','" + addlyric + "','" + addvocal + "','" + addoricode + "','" + addstyle + "','" + addscode + "','" + DataPool.currentID + "')";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteScalar();


                sql = "insert into [gas] values ('" + addgcode + "','" + addacode + "','" + addscode + "')";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteScalar();
            }
            catch { MessageBox.Show("错误", "提示信息"); closeSQL(); return; }

            closeSQL();
            MessageBox.Show("提交成功!", "提示信息");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            connectSQL();
            string sql1 = "select autho from [user] where username = '" + DataPool.currentID + "'";
            cmd = new SqlCommand(sql1, conn);
            string autho = cmd.ExecuteScalar() as string;
            closeSQL();
            if (autho == "1"||autho=="0")
            {
                Form7 Form7 = new Form7();
                Form7.ShowDialog();                
            }
            else
            {
                MessageBox.Show("没有权限", "提示");
                return;
            }



        }

        private void button3_Click(object sender, EventArgs e)
        {
            connectSQL();
            string sql1 = "select autho from [user] where username = '" + DataPool.currentID + "'";
            cmd = new SqlCommand(sql1, conn);
            string autho = cmd.ExecuteScalar() as string;
            closeSQL();
            if (autho == "1" || autho == "0")
            {
                Form9 Form9 = new Form9();
                Form9.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限", "提示");
                return;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            connectSQL();
            string sql1 = "select autho from [user] where username = '" + DataPool.currentID + "'";
            cmd = new SqlCommand(sql1, conn);
            string autho = cmd.ExecuteScalar() as string;
            closeSQL();
            if (autho == "1" || autho == "0")
            {
                Form12 Form12 = new Form12();
                Form12.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有权限", "提示");
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
