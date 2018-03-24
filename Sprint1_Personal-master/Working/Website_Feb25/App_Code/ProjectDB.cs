using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/*This class will contain most of the methods 
 * for interacting with the database */
namespace database
{
    public class ProjectDB
    {
        public static SqlConnection connectToDB()
        {
            try
            {
                SqlConnection conn = new SqlConnection
                {
                    ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Sprint1Dev"].ConnectionString;
                };
                conn.Open();
                return conn;
            }
            catch (Exception)
            {
                return null;
            }
        }

        
    }

    
}