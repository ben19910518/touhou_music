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
        private string modaname2;
        private string modtime2;
        private string modgname2;
        private string modoricode2;
        private string modorigin2;
        private string modsname2;
        private string modarranger2;
        private string modlyric2;
        private string modvocal2;
        private string modstyle2;
        //private string modgcode2;
        //private string modscode2;
        //private string modacode2;
        //private string modtrack2;
        //private string adduser;
        public Form12()
        {
            InitializeComponent();
            using (sqlAdapter sqladp = new sqlAdapter())
            {
                DataSet ds2;
                sqladp.getDataSet("select distinct acode from [album]", out ds2);
                for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                {
                    comboBox2.Items.Add(ds2.Tables[0].Rows[i][0]);
                }
                comboBox2.SelectedIndex = 0;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
      //      modacode2 = comboBox2.Text;
      //      modtrack2 = comboBox6.Text;
       //     modscode2 = modacode2 + modtrack2;
       //     modgcode2 = comboBox1.Text;
            modgname2 = comboBox3.Text.Replace("'", "''");
            if (modgname2 != modgname)
            {
                using (sqlAdapter sqladp = new sqlAdapter())
                {
                    sqladp.ExecuteNonQuery("update [group] set gname='" + modgname2 + "' where gcode='" + modgcode + "'");
                }
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
            using (sqlAdapter sqladp = new sqlAdapter())
            {
                sql = "delete from [album] where acode='" + modacode + "'";
                sqladp.ExecuteNonQuery(sql);
                sql = "insert into [album] values ('" + modacode + "','" + modaname2 + "','" + modtime2 + "',@cover)";
                sqladp.setCommand(sql);
                sqladp.Command.Parameters.Add("cover", SqlDbType.Image);
                sqladp.Command.Parameters["cover"].Value = imagebytes;
                sqladp.Command.ExecuteNonQuery();
            }
            MessageBox.Show("修改成功！", "消息");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            modlyric2 = comboBox7.Text.Replace("'", "''");
            modsname2 = comboBox8.Text.Replace("'", "''");
            modarranger2 = comboBox9.Text.Replace("'", "''");
            modstyle2 = comboBox10.Text.Replace("'", "''");
            modvocal2 = comboBox11.Text.Replace("'", "''");
            modorigin2 = comboBox12.Text.Replace("'", "''");
            if (modlyric2 != modlyric)
            {
                using (sqlAdapter sqladp = new sqlAdapter())
                {
                    sqladp.ExecuteScalar("update [song] set lyric='" + modlyric2 + "' where scode='" + modscode + "'");
                }
                MessageBox.Show("修改作词成功！", "消息");
            }
            if (modsname2 != modsname)
            {
                using (sqlAdapter sqladp = new sqlAdapter())
                {
                    sqladp.ExecuteScalar("update [song] set sname='" + modsname2 + "' where scode='" + modscode + "'");
                }
                MessageBox.Show("修改歌曲名成功！", "消息");
            }
            if (modarranger2 != modarranger)
            {
                using (sqlAdapter sqladp = new sqlAdapter())
                {
                    sqladp.ExecuteScalar("update [song] set arranger='" + modarranger2 + "' where scode='" + modscode + "'");
                }
                MessageBox.Show("修改编曲成功！", "消息");
            }
            if (modstyle2 != modstyle)
            {
                using (sqlAdapter sqladp = new sqlAdapter())
                {
                    sqladp.ExecuteScalar("update [song] set style='" + modstyle2 + "' where scode='" + modscode + "'");
                }
                MessageBox.Show("修改曲风成功！", "消息");
            }
            if (modvocal2 != modvocal)
            {
                using (sqlAdapter sqladp = new sqlAdapter())
                {
                    sqladp.ExecuteScalar("update [song] set vocal='" + modvocal2 + "' where scode='" + modscode + "'");
                }
                MessageBox.Show("修改歌手成功！", "消息");
            }

            using (sqlAdapter sqladp = new sqlAdapter())
            {
                modoricode2 = sqladp.ExecuteScalar("select oricode from [ori] where origin = '" + modorigin2 + "'") as string;
            }
            if (modoricode2 != modoricode)
            {
                using (sqlAdapter sqladp = new sqlAdapter())
                {
                    sqladp.ExecuteScalar("update [song] set oricode='" + modoricode2 + "' where scode='" + modscode + "'");
                }
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
            List<DataTable> dt;
            List<ComboBox> CB = new List<ComboBox>(4);
            CB.Add(comboBox3);
            CB.Add(comboBox4);
            CB.Add(comboBox5);
            CB.Add(comboBox6);

            using (sqlAdapter sqladp = new sqlAdapter())
            {
                try
                {
                    sqladp.getDataTables(out dt,"select distinct gcode from [gas] where acode='" + modacode + "'");
                    for (int i = 0; i < dt[0].Rows.Count; i++)
                    {
                        comboBox1.Items.Add(dt[0].Rows[i][0]);
                    }
                    comboBox1.SelectedIndex = 0;
                    modgcode = comboBox1.Text;
                    sqladp.getDataTables(out dt, "select gname from [group] where gcode='" + modgcode + "'", "select aname from [album] where acode='" + modacode + "'",
                        "select [time] from [album] where acode='" + modacode + "'",
                        "select distinct track from [song],gas where gas.scode=song.scode and acode='" + modacode + "'");
                    comboBox6.Items.Clear();
                    Program.fillIn(ref dt, ref CB);
                    modgcode = comboBox1.Text;
                    modgname = comboBox3.Text;
                    modaname = comboBox4.Text;
                    modtime = comboBox5.Text;
                    comboBox6.SelectedIndex = 0;
                }
                catch(Exception) { MessageBox.Show("无此曲目", "提示"); }
            }

            pictureBox1.Image = null;
          //  byte[] imagebytes = null;

            using (sqlAdapter sqladp = new sqlAdapter())
            {
                SqlDataReader dr;
                sqladp.getExecuteReader(out dr,"select cover from album where acode='" + modacode + "'");
                while (dr.Read())
                {
                    imagebytes1 = (byte[])dr.GetValue(0);
                }
                dr.Close();
            }
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

            List<DataTable> dt;
            List<ComboBox> CB = new List<ComboBox>(6);
            CB.Add(comboBox7);
            CB.Add(comboBox8);
            CB.Add(comboBox9);
            CB.Add(comboBox10);
            CB.Add(comboBox11);
            CB.Add(comboBox12);
            DataSet modOrigin = new DataSet();
            try
            {
                using (sqlAdapter sqladp = new sqlAdapter())
                {
                    sqladp.getDataSet("select origin from [ori],song where ori.oricode=song.oricode and scode='" + modscode + "'",out modOrigin);
                    modorigin = modOrigin.Tables[0].Rows[0][0].ToString();

                    sqladp.getDataTables(out dt, "select lyric from [song] where scode='" + modscode + "'",
                        "select sname from [song] where scode='" + modscode + "'", "select arranger from [song] where scode='" + modscode + "'",
                        "select style from [song] where scode='" + modscode + "'", "select vocal from [song] where scode='" + modscode + "'",
                        "select distinct origin from [ori]");
                    string sql = "select oricode from [ori] where origin = '" + modorigin + "'";
                    modoricode = sqladp.ExecuteScalar(sql) as string;
                }
                Program.fillIn(ref dt, ref CB);
                
                comboBox12.SelectedIndex = comboBox12.Items.IndexOf(modorigin);
            }
            catch(Exception) { MessageBox.Show("无此曲目", "提示"); }
        }

        private void button1_Click(object sender, EventArgs e)
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
