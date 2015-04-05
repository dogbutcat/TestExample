using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebBrowser
{
    public class SQL_Kufang:SQL_Base,IKufang
    {
        public DataTable SelectKufang()
        {
            string sqlQuery = "SELECT centercode,localcode,kufangname,kufangaddress FROM YKT_Kufang";
            return SQLHelper.ExecuteDataset(accConnectionString, CommandType.Text, sqlQuery).Tables[0];
        }
    }

    public class SQLHelper
    {
        public SQLHelper()
        {

        }

        public static DataSet ExecuteDataset(string connectionString,CommandType commandType,string commandText)
        {
            return ExecuteDataset(connectionString, commandType, commandText, (SqlParameter[])null);
        }

        private static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, SqlParameter[] sqlParameter)
        {
            if (connectionString==null || connectionString.Length==0)
            {
                throw new ArgumentException("connectionString");
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                return ExecuteDataset(conn, commandType, commandText, sqlParameter);
            }
        }

        private static DataSet ExecuteDataset(SqlConnection conn, CommandType commandType, string commandText, SqlParameter[] sqlParameter)
        {
            if (conn==null)
            {
                throw new ArgumentException("conn");
            }

            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            PrepareCommand(cmd, conn, (SqlTransaction)null, commandType, commandText, sqlParameter, out mustCloseConnection);
            using (SqlDataAdapter da=new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.Parameters.Clear();
                return ds;
            }
        }

        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction sqlTransaction, CommandType commandType, string commandText, SqlParameter[] sqlParameter, out bool mustCloseConnection)
        {
            if (cmd==null)
            {
                throw new ArgumentException("cmd");
            }
            if (conn.State !=ConnectionState.Open)
            {
                mustCloseConnection = true;
                conn.Open();
            }
            else
            {
                mustCloseConnection = false;
            }

            cmd.Connection = conn;
            cmd.CommandText = commandText;
            if (sqlParameter!=null)
            {

            }
        }
    }
}
