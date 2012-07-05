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
    public partial class Form12 : Form
    {
        private byte[] imagebytes;
        private string fullpath;
        private SqlConnection conn;
        private SqlCommand cmd;
        private string modacode;
        private string modaname;
        private string modtime;
        private string modgcode;
        private string modscode;
        private string modgname;
        private string modoricode;
        private string modorigin;
        private string modsname;
        private string modtrack;
        private string modarranger;
        private string modlyric;
        private string modvocal;
        private string modstyle;
        private string modacode2;
        private string modaname2;
        private string modtime2;
        private string modgcode2;
        private string modscode2;
        private string modgname2;
        private string modoricode2;
        private string modorigin2;
        private string modsname2;
        private string modtrack2;
        private string modarranger2;
        private string modlyric2;
        private string modvocal2;
        private string modstyle2;
        private string adduser;
        public Form12()
        {
            InitializeComponent();
            connectSQL();
            DataSet ds2 = new DataSet();
            SqlDataAdapter da2 = new SqlDataAdapter("select distinct acode from [album]", conn);

            da2.Fill(ds2);
            for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
            {
                comboBox2.Items.Add(ds2.Tables[0].Rows[i][0]);
            }

            comboBox2.SelectedIndex = 0;


//////////////////
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
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql;
           
      //      modacode2 = comboBox2.Text;
      //      modtrack2 = comboBox6.Text;
       //     modscode2 = modacode2 + modtrack2;
       //     modgcode2 = comboBox1.Text;
            modgname2 = comboBox3.Text.Replace("'", "''");
            if (modgname2 != modgname)
            {
                connectSQL();
                sql = "update [group] set gname='" + modgname2 + "' where gcode='" + modgcode + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
               // cmd.ExecuteScalar();
                closeSQL();
                MessageBox.Show("修改社团名成功！", "消息");
            }



        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sql;
            modaname2 = comboBox4.Text.Replace("'", "''");
            modtime2 = comboBox5.Text;
          /*  if (modaname2 != modaname)
            {
                connectSQL();
                sql = "update [album] set aname='" + modaname2 + "' where acode='" + modacode + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteScalar();
                closeSQL();
                MessageBox.Show("修改专辑名成功！", "消息");
            }
            if (modtime2 != modtime)
            {
                connectSQL();
                sql = "update [album] set time='" + modtime2 + "' where acode='" + modacode + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteScalar();
                closeSQL();
                MessageBox.Show("修改首发展会成功！", "消息");
            }
            connectSQL();
            sql = "update [album] set cover='@cover' where acode='" + modacode + "'";
            cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("cover", SqlDbType.Image);

            cmd.Parameters["cover"].Value = imagebytes;
            cmd.ExecuteScalar();
            closeSQL();
            MessageBox.Show("修改专辑图片成功！", "消息");
*/
            connectSQL();

            sql = "delete from [album] where acode='" + modacode + "'";
            cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            sql = "insert into [album] values ('" + modacode + "','" +modaname2 + "','" + modtime2 + "',@cover)";
            cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("cover", SqlDbType.Image);

            cmd.Parameters["cover"].Value = imagebytes;

            cmd.ExecuteNonQuery();
            closeSQL();
            MessageBox.Show("修改成功！", "消息");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sql;
            modlyric2 = comboBox7.Text.Replace("'", "''");
            modsname2 = comboBox8.Text.Replace("'", "''");
            modarranger2 = comboBox9.Text.Replace("'", "''");
            modstyle2 = comboBox10.Text.Replace("'", "''");
            modvocal2 = comboBox11.Text.Replace("'", "''");
            modorigin2 = comboBox12.Text.Replace("'", "''");
            if (modlyric2 != modlyric)
            {
                connectSQL();
                sql = "update [song] set lyric='" + modlyric2 + "' where scode='" + modscode + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteScalar();
                closeSQL();
                MessageBox.Show("修改作词成功！", "消息");
            }
            if (modsname2 != modsname)
            {
                connectSQL();
                sql = "update [song] set sname='" + modsname2 + "' where scode='" + modscode + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteScalar();
                closeSQL();
                MessageBox.Show("修改歌曲名成功！", "消息");
            }
            if (modarranger2 != modarranger)
            {
                connectSQL();
                sql = "update [song] set arranger='" + modarranger2 + "' where scode='" + modscode + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteScalar();
                closeSQL();
                MessageBox.Show("修改编曲成功！", "消息");
            }
            if (modstyle2 != modstyle)
            {
                connectSQL();
                sql = "update [song] set style='" + modstyle2 + "' where scode='" + modscode + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteScalar();
                closeSQL();
                MessageBox.Show("修改曲风成功！", "消息");
            }
            if (modvocal2 != modvocal)
            {
                connectSQL();
                sql = "update [song] set vocal='" + modvocal2 + "' where scode='" + modscode + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteScalar();
                closeSQL();
                MessageBox.Show("修改歌手成功！", "消息");
            }

            connectSQL();
             sql = "select oricode from [ori] where origin = '" + modorigin2 + "'";
            cmd = new SqlCommand(sql, conn);
            modoricode2 = cmd.ExecuteScalar() as string;
            closeSQL();
            if (modoricode2 != modoricode)
            {
                connectSQL();
                sql = "update [song] set oricode='" + modoricode2 + "' where scode='" + modscode + "'";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteScalar();
                closeSQL();
                MessageBox.Show("修改原曲成功！", "消息");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds6 = new DataSet();

            comboBox1.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            comboBox5.Items.Clear();

            comboBox7.Items.Clear();
            comboBox8.Items.Clear();
            comboBox10.Items.Clear();
            comboBox11.Items.Clear();
            comboBox12.Items.Clear();
            comboBox9.Items.Clear();


            modacode = comboBox2.Text;
            modgcode = modacode.Substring(0, 4);
            connectSQL();
            DataSet ds = new DataSet();

            DataSet ds3 = new DataSet();
            DataSet ds4 = new DataSet();
            DataSet ds5 = new DataSet();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select distinct gcode from [gas] where acode='" + modacode + "'", conn);

                da.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    comboBox1.Items.Add(ds.Tables[0].Rows[i][0]);
                }

                comboBox1.SelectedIndex = 0;
                modgcode = comboBox1.Text;

            }
            catch { MessageBox.Show("无此曲目", "提示"); closeSQL(); return; }
            SqlDataAdapter da3 = new SqlDataAdapter("select gname from [group] where gcode='" + modgcode + "'", conn);

            da3.Fill(ds3);
            for (int i = 0; i < ds3.Tables[0].Rows.Count; i++)
            {
                comboBox3.Items.Add(ds3.Tables[0].Rows[i][0]);
            }

            comboBox3.SelectedIndex = 0;
            modgname = comboBox3.Text;

            SqlDataAdapter da4 = new SqlDataAdapter("select aname from [album] where acode='" + modacode + "'", conn);

            da4.Fill(ds4);
            for (int i = 0; i < ds4.Tables[0].Rows.Count; i++)
            {
                comboBox4.Items.Add(ds4.Tables[0].Rows[i][0]);
            }

            comboBox4.SelectedIndex = 0;
            modaname = comboBox4.Text;

            SqlDataAdapter da5 = new SqlDataAdapter("select [time] from [album] where acode='" + modacode + "'", conn);

            da5.Fill(ds5);
            for (int i = 0; i < ds5.Tables[0].Rows.Count; i++)
            {
                comboBox5.Items.Add(ds5.Tables[0].Rows[i][0]);
            }

            comboBox5.SelectedIndex = 0;
            modtime = comboBox5.Text;
            comboBox6.Items.Clear();

            SqlDataAdapter da6 = new SqlDataAdapter("select distinct track from [song],gas where gas.scode=song.scode and acode='" + modacode + "'", conn);

            da6.Fill(ds6);
            for (int i = 0; i < ds6.Tables[0].Rows.Count; i++)
            {
                comboBox6.Items.Add(ds6.Tables[0].Rows[i][0]);
            }

            comboBox6.SelectedIndex = 0;

            closeSQL();







            

            pictureBox1.Image = null;
          //  byte[] imagebytes = null;

            connectSQL();

            SqlCommand com = new SqlCommand("select cover from album where acode='" + modacode + "'", conn);
            //" + tempaname + "
            SqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {

                imagebytes1 = (byte[])dr.GetValue(0);

            }

            dr.Close();

            com.Clone();
            closeSQL();
            if (imagebytes1 != null)
            {
                if (imagebytes1[0] == 0)
                {
                    pictureBox1.Image = touhou_music.Properties.Resources.x;
                    return;
                }


                MemoryStream ms = new MemoryStream(imagebytes1);

                Bitmap bmpt = new Bitmap(ms);
                //Image image = Image.FromStream(ms, true);
                //dr.Close();
                //closeSQL();
                pictureBox1.Image = bmpt;



            




            }
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {



            comboBox7.Items.Clear();
            comboBox8.Items.Clear();
            comboBox10.Items.Clear();
            comboBox11.Items.Clear();
            comboBox12.Items.Clear();
            comboBox9.Items.Clear();
            modtrack = comboBox6.Text;
            modscode = modacode + modtrack;
            connectSQL();


            DataSet ds7 = new DataSet();
            DataSet ds8 = new DataSet();
            DataSet ds9 = new DataSet();
            DataSet ds10 = new DataSet();
            DataSet ds11 = new DataSet();
            DataSet ds12 = new DataSet();
            DataSet ds122 = new DataSet();
            try
            {
                SqlDataAdapter da7 = new SqlDataAdapter("select lyric from [song] where scode='" + modscode + "'", conn);

                da7.Fill(ds7);
                for (int i = 0; i < ds7.Tables[0].Rows.Count; i++)
                {
                    comboBox7.Items.Add(ds7.Tables[0].Rows[i][0]);
                }

                comboBox7.SelectedIndex = 0;
                modlyric = comboBox7.Text;

                SqlDataAdapter da8 = new SqlDataAdapter("select sname from [song] where scode='" + modscode + "'", conn);

                da8.Fill(ds8);
                for (int i = 0; i < ds8.Tables[0].Rows.Count; i++)
                {
                    comboBox8.Items.Add(ds8.Tables[0].Rows[i][0]);
                }

                comboBox8.SelectedIndex = 0;
                modsname = comboBox8.Text;

                SqlDataAdapter da9 = new SqlDataAdapter("select arranger from [song] where scode='" + modscode + "'", conn);

                da9.Fill(ds9);
                for (int i = 0; i < ds9.Tables[0].Rows.Count; i++)
                {
                    comboBox9.Items.Add(ds9.Tables[0].Rows[i][0]);
                }

                comboBox9.SelectedIndex = 0;
                modarranger = comboBox9.Text;

                SqlDataAdapter da10 = new SqlDataAdapter("select style from [song] where scode='" + modscode + "'", conn);

                da10.Fill(ds10);
                for (int i = 0; i < ds10.Tables[0].Rows.Count; i++)
                {
                    comboBox10.Items.Add(ds10.Tables[0].Rows[i][0]);
                }

                comboBox10.SelectedIndex = 0;
                modstyle = comboBox10.Text;

                SqlDataAdapter da11 = new SqlDataAdapter("select vocal from [song] where scode='" + modscode + "'", conn);

                da11.Fill(ds11);
                for (int i = 0; i < ds11.Tables[0].Rows.Count; i++)
                {
                    comboBox11.Items.Add(ds11.Tables[0].Rows[i][0]);
                }

                comboBox11.SelectedIndex = 0;
                modvocal = comboBox11.Text;

                SqlDataAdapter da12 = new SqlDataAdapter("select origin from [ori],song where ori.oricode=song.oricode and scode='" + modscode + "'", conn);

                da12.Fill(ds12);
                for (int i = 0; i < ds12.Tables[0].Rows.Count; i++)
                {
                    comboBox12.Items.Add(ds12.Tables[0].Rows[i][0]);
                }

                comboBox12.SelectedIndex = 0;
                modorigin = comboBox12.Text;


                SqlDataAdapter da122 = new SqlDataAdapter("select distinct origin from [ori]", conn);

                da122.Fill(ds122);
                for (int i = 0; i < ds122.Tables[0].Rows.Count; i++)
                {
                    comboBox12.Items.Add(ds122.Tables[0].Rows[i][0]);
                }

                comboBox12.SelectedIndex = 0;

                string sql = "select oricode from [ori] where origin = '" + modorigin + "'";
                cmd = new SqlCommand(sql, conn);

                modoricode = cmd.ExecuteScalar() as string;
                closeSQL();




            }
            catch { MessageBox.Show("无此曲目", "提示"); closeSQL(); return; }
        }

        private void button1_Click_1(object sender, EventArgs e)
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
                  private byte[] ImageToStream(string fileName)
        {
            Bitmap image = new Bitmap(fileName);
            MemoryStream stream = new MemoryStream();
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
            return stream.ToArray();
        }

                  public byte[] imagebytes1 { get; set; }
    }
}
