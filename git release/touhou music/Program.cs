using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using touhou_music.datas;

namespace touhou_music
{
    class sqlAdapter:IDisposable
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        public sqlAdapter(string connectCommand)
        {
            conn = new SqlConnection(connectCommand);
            conn.Open();
        }
        ~sqlAdapter()
        {
            this.Dispose();
        }
        public void Dispose()
        {
            conn.Close();
            conn.Dispose();
        }
        public System.Object ExecuteScalar(string command)
        {
            if(command == null)
                return null;
            cmd = new SqlCommand(command, this.conn);
            return cmd.ExecuteScalar();
        }
        public void getExecuteReader(out SqlDataReader ds,string command)
        {
            cmd = new SqlCommand(command, this.conn);
            ds= cmd.ExecuteReader();
            cmd = null;
        }
        public void getDataSet(string cmd, out DataSet ds)
        {
            ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            da.Fill(ds);
        }
        public void getDataSet(string cmd, out DataSet ds,string ex)
        {
            ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            da.Fill(ds,ex);
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
