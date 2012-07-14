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
        public void setCommand(string command)
        {
            this.cmd = new SqlCommand(command, this.conn);
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
        public void fillDataTable(string cmd, ref DataTable dt)
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            da.Fill(dt);
        }
        public int getDataTables(out List<DataTable> dt, params string[] cmd)
        {
            dt = new List<DataTable>();
            SqlDataAdapter da;
            DataTable tp = new DataTable();
            foreach(string co in cmd)
            {
                da = new SqlDataAdapter(co, conn);
                da.Fill(tp);
                dt.Add(tp);
                tp = new DataTable();
            }
            return dt.Count;
        }
        public SqlConnection Connection()
        {
            return this.conn;
        }
        public SqlCommand Command()
        {
            return this.cmd;
        }
    }

}
