using Microsoft.Data.SqlClient;
using System.Data;

namespace Core
{
    internal class DataManager
    {
        
        //Local Host
        //FileStream sr =  File.Open("d:\\Auth\\authcodes.txt", FileMode.Open,FileAccess.Read);

        //Home Build
        FileStream sr = File.Open("e:\\Auth\\authcodes.txt", FileMode.Open, FileAccess.Read);

        //Dev Server -- Production
        //FileStream sr = File.Open("e:\\Auth\\authcodes.txt", FileMode.Open, FileAccess.Read);

        //private string msSqlServer = "webserver-1.alkota.com\\AlkotaCS"; //New Production database server
        private string msSqlServer = "webdev-1.alkota.com\\SqlWebDev"; // Dev server
        private string msSqlDataBase = "CornerBargins";
        private string msSqlUserName = "sa";
        private string msSqlPassword = string.Empty;
        private string msSqlConnectionString = string.Empty;

        private List<SqlParameter> DBParameters;

        public DataManager() {
            StreamReader reader = new StreamReader(sr);
            string line = reader.ReadLine();
            msSqlPassword = line;
            msSqlConnectionString = "Data Source=" + msSqlServer + "; User ID=" + msSqlUserName + "; Password=" + msSqlPassword + "; Initial Catalog=" + msSqlDataBase + ";TrustServerCertificate=true";
            DBConn =  new SqlConnection(msSqlConnectionString);
            
            reader.Close();
            reader.Dispose();
        }


        public Boolean  GetDataProc(string DBProc, Dictionary<string,string>? ProcParameters = null )
        {
            CreateParameters(ProcParameters);

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
            catch (SqlException error)
            {
                return false;
            }

        }

        private void CreateParameters(Dictionary<string, string>? ProcParameters)
        {
            DBParameters = new List<SqlParameter>();
            //Create Parameters
            if (ProcParameters != null && ProcParameters.Count > 0)
            {
                for (int x = 0; x < ProcParameters.Count; x++)
                {
                    KeyValuePair<string, string> param = ProcParameters.ElementAt(x);
                    DBParameters.Add(new SqlParameter(param.Key, param.Value));
                }
            }
        }

        public Boolean GetDataProc(string DBProc) {

            return false;
        }


        public T InsertDataProc<T>(string DBProc, Dictionary<string, string>? ProcParameters = null)
        {
            CreateParameters(ProcParameters);
            CreateCommand(DBProc, DBParameters);

            DBCmd.Connection.Open();
            SqlDataReader reader = DBCmd.ExecuteReader();

            reader.Read();
            if (reader.HasRows)
            {

                var returnValue = (T)reader[0];
                DBCmd.Connection.Close();
                return returnValue;

            }

            return default(T);
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
