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
        private static SqlConnection conn;
        private SqlCommand cmd;
        private bool flag = false;
        static sqlAdapter()
        {
            conn = new SqlConnection(DataPool.conString);
        }
        public sqlAdapter()
        {
            sqlAdapter.conn.Open();
        }
        ~sqlAdapter()
        {
            conn.Close();
            this.Dispose();
        }
        public void Dispose()
        {
            conn.Close();
        }
        public void setCommand(string command)
        {
            this.cmd = new SqlCommand(command, conn);
        }
        public System.Object ExecuteScalar(string command)
        {
            if (command == null)
                return null;
            cmd = new SqlCommand(command, conn);
            return cmd.ExecuteScalar();
        }
        public void getExecuteReader(out SqlDataReader ds, string command)
        {
            cmd = new SqlCommand(command, conn);
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
        public SqlConnection Connection
        {
            get { return conn; }
        }
        public SqlCommand Command
        {
            get { return this.cmd; }
        }
    }

}
