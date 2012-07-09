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
    public partial class Form7 : Form
    {
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

        public Form7()
        {
            InitializeComponent();
            connectSQL();
            DataSet ds2 = new DataSet();
            DataSet ds6 = new DataSet();
            SqlDataAdapter da2 = new SqlDataAdapter("select distinct acode from [album]", conn);

            da2.Fill(ds2);
            for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
            {
                comboBox2.Items.Add(ds2.Tables[0].Rows[i][0]);
            }

            comboBox2.SelectedIndex = 0;


            SqlDataAdapter da6 = new SqlDataAdapter("select distinct track from [song]", conn);

            da6.Fill(ds6);
            for (int i = 0; i < ds6.Tables[0].Rows.Count; i++)
            {
                comboBox6.Items.Add(ds6.Tables[0].Rows[i][0]);
            }

            comboBox6.SelectedIndex = 0;
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

            comboBox3.Items.Clear(); 
            comboBox4.Items.Clear();
            comboBox5.Items.Clear();
            comboBox7.Items.Clear();
            comboBox8.Items.Clear();
            comboBox10.Items.Clear();
            comboBox11.Items.Clear();
            comboBox12.Items.Clear();

           

            modacode = comboBox2.Text;
            modtrack = comboBox6.Text;
            modscode = modacode + modtrack;
            modgcode = modacode.Substring(0, 4);
            connectSQL();
            DataSet ds = new DataSet();

            DataSet ds3 = new DataSet();
            DataSet ds4 = new DataSet();
            DataSet ds5 = new DataSet();

            DataSet ds7 = new DataSet();
            DataSet ds8 = new DataSet();
            DataSet ds9 = new DataSet();
            DataSet ds10 = new DataSet();
            DataSet ds11 = new DataSet();
            DataSet ds12 = new DataSet();
            try
            {
               

            
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

            SqlDataAdapter da7 = new SqlDataAdapter("select lyric from [song] where scode='" + modscode + "'", conn);

            da7.Fill(ds7);
            for (int i = 0; i < ds7.Tables[0].Rows.Count; i++)
            {
                comboBox7.Items.Add(ds7.Tables[0].Rows[i][0]);
            }

            comboBox7.SelectedIndex = 0;

            SqlDataAdapter da8 = new SqlDataAdapter("select sname from [song] where scode='" + modscode + "'", conn);

            da8.Fill(ds8);
            for (int i = 0; i < ds8.Tables[0].Rows.Count; i++)
            {
                comboBox8.Items.Add(ds8.Tables[0].Rows[i][0]);
            }

            comboBox8.SelectedIndex = 0;

            SqlDataAdapter da9 = new SqlDataAdapter("select arranger from [song] where scode='" + modscode + "'", conn);

            da9.Fill(ds9);
            for (int i = 0; i < ds9.Tables[0].Rows.Count; i++)
            {
                comboBox9.Items.Add(ds9.Tables[0].Rows[i][0]);
            }

            comboBox9.SelectedIndex = 0;

            SqlDataAdapter da10 = new SqlDataAdapter("select style from [song] where scode='" + modscode + "'", conn);

            da10.Fill(ds10);
            for (int i = 0; i < ds10.Tables[0].Rows.Count; i++)
            {
                comboBox10.Items.Add(ds10.Tables[0].Rows[i][0]);
            }

            comboBox10.SelectedIndex = 0;

            SqlDataAdapter da11 = new SqlDataAdapter("select vocal from [song] where scode='" + modscode + "'", conn);

            da11.Fill(ds11);
            for (int i = 0; i < ds11.Tables[0].Rows.Count; i++)
            {
                comboBox11.Items.Add(ds11.Tables[0].Rows[i][0]);
            }

            comboBox11.SelectedIndex = 0;

            SqlDataAdapter da12 = new SqlDataAdapter("select origin from [ori],song where ori.oricode=song.oricode and scode='" + modscode + "'", conn);

            da12.Fill(ds12);
            for (int i = 0; i < ds12.Tables[0].Rows.Count; i++)
            {
                comboBox12.Items.Add(ds12.Tables[0].Rows[i][0]);
            }

            comboBox12.SelectedIndex = 0;

          //  modacode = comboBox2.Text;

            }
            catch
            {MessageBox.Show("无此曲目", "提示"); closeSQL();return; }

          //  modtrack = comboBox6.Text;

            closeSQL();
          


        }

        private void button2_Click(object sender, EventArgs e)
        {
            connectSQL();
            string  sql = "delete from [gas] where scode='" + modscode + "'";
            cmd = new SqlCommand(sql, conn);
            cmd.ExecuteScalar();

            sql = "delete from [song] where scode='" + modscode + "'";
            cmd = new SqlCommand(sql, conn);
            cmd.ExecuteScalar();

       
                sql = " select gcode from gas where gcode='"+modgcode+"'";
                cmd = new SqlCommand(sql, conn);
                object obj = cmd.ExecuteScalar();

                if (obj == null)
                {
                    sql = "delete from [group] where gcode='" + modgcode + "'";
                    cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteScalar();
                }

                sql = " select acode from gas where acode='" + modacode + "'";
                cmd = new SqlCommand(sql, conn);
                object obj1 = cmd.ExecuteScalar();
     
                if (obj1 == null)
                {
                    sql = "delete from [album] where acode='" + modacode + "'";
                    cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteScalar();
                }

                closeSQL();

            MessageBox.Show("已删除", "提示");
            this.Dispose();
        }
    }
}
