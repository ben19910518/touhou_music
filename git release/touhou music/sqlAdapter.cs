using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using touhou_music.datas;

namespace touhou_music
{
    class sqlAdapter : IDisposable
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
            if (command == null)
                return null;
            cmd = new SqlCommand(command, this.conn);
            return cmd.ExecuteScalar();
        }
        public void getExecuteReader(out SqlDataReader ds, string command)
        {
            cmd = new SqlCommand(command, this.conn);
            ds = cmd.ExecuteReader();
            cmd = null;
        }
        public void getDataSet(string cmd, out DataSet ds)
        {
            ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            da.Fill(ds);
        }
        public void getDataSet(string cmd, out DataSet ds, string ex)
        {
            ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            da.Fill(ds, ex);
        }
    }

}
