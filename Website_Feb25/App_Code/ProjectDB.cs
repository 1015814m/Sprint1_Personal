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
                SqlConnection con = new SqlConnection();
                con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Sprint1Dev"].ConnectionString;

                con.Open();
                return con;
            }
            catch (Exception)
            {
                return null;
            }
        }

        
    }

    
}