using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Core
{
    internal class DataManager
    {
        private string msSqlServer = "webserver-1.alkota.com\\AlkotaCS"; //New Production database server
        //private string msSqlServer = "webdev-1.alkota.com\\SqlWebDev"; // Dev server
        private string msSqlDataBase = "Helix";
        private string msSqlUserName = "sa";
        private string msSqlPassword = "alk0t@im@g3";
        private string msSqlConnectionString = string.Empty;


        public DataManager() {
            msSqlConnectionString = "Data Source=" + msSqlServer + "; User ID=" + msSqlUserName + "; Password=" + msSqlPassword + "; Initial Catalog=" + msSqlDataBase + ";TrustServerCertificate=true";
            DBConn =  new SqlConnection(msSqlConnectionString);
        }


        public Boolean  GetDataProc(string DBProc, Dictionary<string,string>? ProcParameters = null )
        {
            List<SqlParameter> DBParameters = new List<SqlParameter>();
            //Create Parameters
            if (ProcParameters != null && ProcParameters.Count > 0)
            {
                for(int x=0; x<ProcParameters.Count; x++)
                {
                    KeyValuePair<string,string> param = ProcParameters.ElementAt(x);
                    DBParameters.Add(new SqlParameter(param.Key, param.Value));
                }
            }

            CreateCommand(DBProc, DBParameters);
            SqlDataAdapter da = new SqlDataAdapter(DBCmd);

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                DBConn.Open();
                RowsAffected = da.Fill(ds);
                DBData = ds;
                DBConn.Close();
                return true;
            }
            catch(SqlException error)
            {
                return false;
            }
            
        }

        public Boolean GetDataProc(string DBProc) {

            return false;
        }


        public Boolean InsertDataProc(string DBProc, List<SqlParameter>? DBParameters = null)
        {
            return false;
        }

        public Boolean InsertDataProc(string DBProc)
        {
            return false;
        }

        private void CreateCommand(string DBProc, List<SqlParameter>? DBParameters)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 4000;
            cmd.CommandText = DBProc;
            cmd.Connection = DBConn;
            if(DBParameters != null)
            {
                foreach(SqlParameter param in DBParameters)
                {
                    cmd.Parameters.Add(param);
                }
            }

            DBCmd = cmd;

        }
        private SqlConnection DBConn { get; set; }
        private SqlCommand? DBCmd { get; set; }

        public int RowsAffected { get; protected set; }
        public DataSet? DBData { get; protected set; }
    }
}
