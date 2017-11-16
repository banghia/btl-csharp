using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace _3Layer.DAL
{
    class DataHelper
    {

        public static SqlConnection connection = null;
        public static String connectionString = @"Data Source=DESKTOP-AIQFAH5\SQLEXPRESS;Initial Catalog=DB_ALTP;Integrated Security=True";

        public DataHelper() {
            connection = new SqlConnection(connectionString);
        }

        public DataTable ExecuteQuery(string query) {
            DataTable dataTable = new DataTable();
            try
            {
                connection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, connection);
                sqlDataAdapter.Fill(dataTable);
            }
            finally
            {
                if (connection != null) {
                    connection.Close();
                }
            }
            return dataTable;
        }

        public bool ExecuteNonQuery(string query) {
            try
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.ExecuteNonQuery();
            }catch(Exception){
                return false;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
            return true;
        }

    }
}
