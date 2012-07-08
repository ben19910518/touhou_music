using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using touhou_music.datas;

namespace touhou_music
{
    class sqlAdapter:IDisposable
    {
        private SqlConnection conn;
        sqlAdapter(string connectCommand)
        {
            conn = new SqlConnection(connectCommand);
            conn.Open();
        }
        ~sqlAdapter()
        {
            if(conn != null)
                conn.Close();
        }
        void Dispose()
        {
            if (conn != null)
                conn.Close();
        }
    }
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
    }
}
