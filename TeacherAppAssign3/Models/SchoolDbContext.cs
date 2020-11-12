//REFERENCE: Code used is from Christine Bittle BlogProjext_1 accessed Nov 10 2020
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

/// <summary>
/// The following provides Access to a Mysql server
/// User: what the username is in MySql
/// Password: What the Password is
/// Database: Which database to access in MySql
/// Server: What server to use ( in this case localhost)
/// Port: the port number
/// ConnectionString: A string that when accessed collects all the previous
/// information and accesses the database
/// via a get request
/// </summary>

namespace TeacherAppAssign3.Models
{
    public class SchoolDbContext
        
       
    {///
        private static string User { get { return "Journey"; } }
        private static string Password { get { return "Journey1"; } }
        private static string Database { get { return "teacherdatabase"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }
        protected static string ConnectionString
        {
            get 
            {
                return "server = " + Server
                + "; user = " + User
                + "; database = " + Database
                + "; port =" + Port
                + "; password =" + Password;
            }
        }
        /// <summary>
        /// The following instantiates the Connection String allowing for accesses 
        /// to the database from anywhere else in the codes fule path
        /// </summary>
        /// <returns> a MySqlConnection String</returns>
        public MySqlConnection AccessDatabase()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
    
}//Results: The method defined on this page allows for access to the mysql teacher database and all of the information within it. 