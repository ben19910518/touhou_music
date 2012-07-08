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
        }
        ~sqlAdapter()
        {
            this.Dispose();
        }
        public void Dispose()
        {
            conn.Dispose();
        }
        public System.Object ExecuteScalar(string command)
        {
            conn.Open();
            if(command == null)
                return null;
            cmd = new SqlCommand(command, this.conn);
            System.Object tmp = cmd.ExecuteScalar();
            cmd = null;
            conn.Close();
            return tmp;
        }
        public SqlDataReader ExecuteReader(string command)
        {
            conn.Open();
            cmd = new SqlCommand(command, this.conn);
            SqlDataReader tmp = cmd.ExecuteReader();
            cmd = null;
            conn.Close();
            return tmp;
        }
        public void getDataSet(string cmd, out DataSet ds)
        {
            conn.Open();
            ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            da.Fill(ds);
            conn.Close();
        }
        public void getDataSet(string cmd, out DataSet ds,string ex)
        {
            conn.Open();
            ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            da.Fill(ds,ex);
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
