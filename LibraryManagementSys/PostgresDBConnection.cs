using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Npgsql;

namespace LibraryManagementSys
{
    class PostgresDBConnection
    {
        // Define Connection String
        private string connString = "Host=localhost;Username=postgres;Password=blaziken567;Database=LibraryDB";

        // Create a connection object
        private NpgsqlConnection conn;

        // initiate conn variable connection in the constructor
        public PostgresDBConnection()
        {
            conn = new NpgsqlConnection(connString);
        }


        public NpgsqlConnection Connection()
        {
            return this.conn;
        }

        // function for opening the connection
        public void openConnection()
        {
            try
            {
                conn.Open();
                Debug.WriteLine("Connection to the database opened successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error opening connection: " + ex.Message); 
            }
        }

        // function for closing the connection
        public void closeConnection()
        {
            try
            {
                conn.Close();
                Debug.WriteLine("Connection to the database closed successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error closing connection: " + ex.Message);
            }
        }

    }
}
