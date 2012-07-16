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
        private string modacode;
        private string modaname;
        private string modtime;
        private string modgcode;
        private string modscode;
        private string modgname;
        //private string modoricode;
        //private string modorigin;
        //private string modsname;
        private string modtrack;
        //private string modarranger;
        //private string modlyric;
        //private string modvocal;
        //private string modstyle;

        public Form7()
        {
            InitializeComponent();
            using (sqlAdapter sqladp = new sqlAdapter())
            {
                DataSet ds2;
                DataSet ds6;
                sqladp.getDataSet("select distinct acode from [album]", out ds2);

                for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                {
                    comboBox2.Items.Add(ds2.Tables[0].Rows[i][0]);
                }
                comboBox2.SelectedIndex = 0;

                sqladp.getDataSet("select distinct track from [song]", out ds6);
                for (int i = 0; i < ds6.Tables[0].Rows.Count; i++)
                {
                    comboBox6.Items.Add(ds6.Tables[0].Rows[i][0]);
                }
                comboBox6.SelectedIndex = 0;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            comboBox3.Items.Clear(); 
            comboBox4.Items.Clear();
            comboBox5.Items.Clear();
            comboBox7.Items.Clear();
            comboBox8.Items.Clear();
            comboBox9.Items.Clear();
            comboBox10.Items.Clear();
            comboBox11.Items.Clear();
            comboBox12.Items.Clear();

            List<ComboBox> CB = new List<ComboBox>(9);
            List<DataTable> dt;
            CB.Add(comboBox3);
            CB.Add(comboBox4);
            CB.Add(comboBox5);
            CB.Add(comboBox7);
            CB.Add(comboBox8);
            CB.Add(comboBox9);
            CB.Add(comboBox10);
            CB.Add(comboBox11);
            CB.Add(comboBox12);
           
            modacode = comboBox2.Text;
            modtrack = comboBox6.Text;
            modscode = modacode + modtrack;
            modgcode = modacode.Substring(0, 4);
            using (sqlAdapter sqladp = new sqlAdapter())
            {
                try
                {
                     sqladp.getDataTables(out dt, "select gname from [group] where gcode='" + modgcode + "'",
                         "select aname from [album] where acode='" + modacode + "'", "select [time] from [album] where acode='" + modacode + "'",
                         "select lyric from [song] where scode='" + modscode + "'","select sname from [song] where scode='" + modscode + "'",
                         "select arranger from [song] where scode='" + modscode + "'","select style from [song] where scode='" + modscode + "'",
                         "select vocal from [song] where scode='" + modscode + "'",
                         "select origin from [ori],song where ori.oricode=song.oricode and scode='" + modscode + "'");
                     for (int i = 0; i < dt.Count; i++)
                     {
                         try
                         {
                             for (int j = 0; j < dt[i].Rows.Count; j++)
                             {
                                 CB[i].Items.Add(dt[i].Rows[j][0]);
                             }
                                 CB.ElementAt(i).SelectedIndex = 0;
                         }
                         catch (System.Exception ex)
                         {
                             System.Diagnostics.Debug.Write(ex.Message);
                             MessageBox.Show("无此曲目", "提示");
                         }
                     }
                }
                catch (System.Exception ex)
                {
                    System.Diagnostics.Debug.Write(ex.Message);
                }
                    modgname = comboBox3.Text;
                    modaname = comboBox4.Text;
                    modtime = comboBox5.Text;

                    //  modacode = comboBox2.Text;
                    //  modtrack = comboBox6.Text;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (sqlAdapter sqladp = new sqlAdapter())
            {
                string sql = "delete from [gas] where scode='" + modscode + "'";
                sqladp.ExecuteScalar(sql);

                sql = "delete from [song] where scode='" + modscode + "'";
                sqladp.ExecuteScalar(sql);

                sql = " select gcode from gas where gcode='" + modgcode + "'";
                object obj = sqladp.ExecuteScalar(sql);
                
                if (obj == null)
                {
                    sql = "delete from [group] where gcode='" + modgcode + "'";
                    sqladp.ExecuteScalar(sql);
                }

                sql = " select acode from gas where acode='" + modacode + "'";
                object obj1 = sqladp.ExecuteScalar(sql);

                if (obj1 == null)
                {
                    sql = "delete from [album] where acode='" + modacode + "'";
                    sqladp.ExecuteScalar(sql);
                }
            }
            MessageBox.Show("已删除", "提示");
            this.Dispose();
        }
    }
}
