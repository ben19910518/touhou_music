using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using touhou_music.datas;

namespace touhou_music
{

    static class Program
    {
        
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form2());
          
        }
        public static void fillIn(ref List<DataTable> dt, ref List<ComboBox> CB)
        {
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
                }
            }
        }
    }
}
