using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace POS_ASP.Server
{
    public class Server
    {
        public  static SqlConnection con { get; set; }
        public  SqlCommand command { get; set; } = new SqlCommand();
        public  DataTable table { get; set; } = new DataTable();
        public  SqlDataAdapter adapter { get; set; } = new SqlDataAdapter();


        /// <summary>
        /// GET CONNECTION INSTANCE
        /// </summary>
        /// <returns></returns>
        public SqlConnection Connection()
        {
            try
            {
                var constr = ConfigurationManager.ConnectionStrings["asp_pos"].ConnectionString;
                con = new SqlConnection(constr);
                return con;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return con;
            }
        }

        /// <summary>
        /// OPEN THE SERVER INSTANCE CONNECTION
        /// </summary>
        public void OpenConnection()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            else
            {
                con.Open();
            }
        }

        /// <summary>
        /// CLOSE THE INSTANCE OF THE SQL CONNECTION
        /// </summary>
        public void CloseConnection()
        {
            if (con.State == ConnectionState.Open) {
                con.Close();
            }
            else
            {
                con.Close();
            }
        }
    }
}