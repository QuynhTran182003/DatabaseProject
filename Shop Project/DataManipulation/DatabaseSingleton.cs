using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Project
{
    public class DatabaseSingleton
    {
        private static SqlConnection conn = null;

        private DatabaseSingleton()
        {

        }

        public static SqlConnection GetInstance()
        {
            if (conn == null)
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myDatabase"].ConnectionString);
                conn.Open();
            }
            return conn;
        }

        public static void CloseConnection()
        {
            try
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            catch { }
            finally
            {
                conn = null;
            }
        }

    }
}
