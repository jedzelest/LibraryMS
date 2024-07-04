using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data;

namespace LibraryManagementSys
{
    class User
    {
        private PostgresDBConnection db;
        // all related to users
        public User()
        {
            db = new PostgresDBConnection();
        }

        public void addUser(string name, string email, string phone_number)
        {
            db.openConnection();
            string query = "INSERT INTO user_tbl (name, email, phone_number) VALUES (@name, @email, @phone_number)";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, db.Connection()))
            {
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@phone_number", phone_number);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Added Successfully!");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error adding user: " + e.Message);
                }
            }
            db.closeConnection();
        }

        public DataTable returnUsersList()
        {
            db.openConnection();
            string query = "SELECT * FROM user_tbl ORDER BY \"userID\" DESC";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(query, db.Connection());
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
            db.closeConnection();
        }

        public void updateUser(int id, string name, string email, string phone_number)
        {
            db.openConnection();
            string query = "UPDATE user_tbl SET name=@name, email=@email, phone_number=@phone_number WHERE \"userID\" = @id";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, db.Connection()))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@phone_number", phone_number);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Updated Successfully!");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error updating user: " + e.Message);
                }
            }
            db.closeConnection();
        }

        public void deleteUser(int id)
        {
            db.openConnection();
            string query = "DELETE FROM user_tbl WHERE \"userID\" = @id";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, db.Connection()))
            {
                cmd.Parameters.AddWithValue("@id", id);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Deleted Successfully!");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error deleting user: " + e.Message);
                }
            }
            db.closeConnection();
        }

        public int fetchUserID(string email)
        {
            int userId = 0;
            db.openConnection();
            string query = "SELECT \"userID\" FROM user_tbl WHERE email=@email";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, db.Connection()))
            {
                cmd.Parameters.AddWithValue("@email", email);
                try 
                {
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        userId = Convert.ToInt32(result);
                    }
                    else
                    {
                        MessageBox.Show("No user found with the specified email.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error finding user: " + ex.Message);
                }
            }
            db.closeConnection();
            return userId;
        }

        public DataTable generateUserNames()
        {
            db.openConnection();
            string query = "SELECT name from user_tbl;";
            using(NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(query, db.Connection()))
            {
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                db.closeConnection();
                return dataTable;
            }
        }

        public int returnUserID(string name)
        {
            db.openConnection();
            int userID = 0;
            string query = "SELECT \"userID\" FROM user_tbl WHERE name=@name";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, db.Connection()))
            {
                cmd.Parameters.AddWithValue("@name", name);

                try
                {
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        userID = Convert.ToInt32(result);
                    }
                    else
                    {
                        MessageBox.Show("No id found with the specified name.");
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show("Error finding an ID: " + e.Message);
                }
            }
            db.closeConnection();
            return userID;
        }

    }
}
