using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Windows.Forms;
using System.Data;

namespace LibraryManagementSys
{
    class Book
    {
        private PostgresDBConnection db;

        public Book()
        {
            db = new PostgresDBConnection();
        }

        public void addBook(string title, string author, string genre, int quantity) 
        {
            db.openConnection();
            string query = "INSERT INTO book_tbl (title, author, genre, \"quantity\") VALUES (@title, @author, @genre, @quantity)";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, db.Connection()))
            {
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@author", author);
                cmd.Parameters.AddWithValue("@genre", genre);
                cmd.Parameters.AddWithValue("@quantity", quantity);

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Books Added Successfully!");
                }
                catch(Exception e)
                {
                    MessageBox.Show("Error adding of Books: " + e.Message);
                }
            }
            db.closeConnection();
        }


        public DataTable returnBooksList()
        {
            db.openConnection();
            string query = "SELECT * FROM book_tbl ORDER BY \"bookID\" DESC";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(query, db.Connection());
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
            db.closeConnection();
        }

        public void updateBook(int id, string title, string author, string genre, int quantity)
        {
            db.openConnection();
            string query = "UPDATE book_tbl SET title=@title, author=@author, genre=@genre, quantity=@quantity WHERE \"bookID\"=@bookID";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, db.Connection()))
            {
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@author", author);
                cmd.Parameters.AddWithValue("@genre", genre);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@bookID", id);

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book Updated Successfully!");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error updating book: " + e.Message);
                }
            }
            db.closeConnection();
        }

        public void deleteBook(int id)
        {
            db.openConnection();
            string query = "DELETE FROM book_tbl WHERE \"bookID\"=@bookID";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, db.Connection()))
            {
                cmd.Parameters.AddWithValue("@bookID", id);

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book Deleted Successfully!");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error deleting a book: " + e.Message);
                }
            }
            db.closeConnection();
        }

        public DataTable generateBookTitles()
        {
            db.openConnection();
            string query = "SELECT title FROM book_tbl WHERE \"quantity\">0";
            using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(query, db.Connection()))
            {
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                db.closeConnection();
                return dataTable;
            }
        }

        public int returnBookID(string title)
        {
            db.openConnection();
            int bookID = 0;
            string query = "SELECT \"bookID\" FROM book_tbl WHERE title=@title";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, db.Connection()))
            {
                cmd.Parameters.AddWithValue("@title", title);

                try
                {
                    var result = cmd.ExecuteScalar();
                    if(result != null)
                    {
                        bookID = Convert.ToInt32(result);
                    }
                    else
                    {
                        MessageBox.Show("ID with the given title not found!");
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Could not fetch ID: " + e.Message);
                }
            }
            db.closeConnection();
            return bookID;
        }

        public void updateQuantity(int bookID, int quantity)
        {
            db.openConnection();
            string query = "UPDATE book_tbl SET quantity=@quantity WHERE \"bookID\"=@bookID";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, db.Connection()))
            {
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@bookID", bookID);

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Book Quantity Updated!");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Failure to update quantity: " + e.Message);
                }
            }
            db.closeConnection();
        }

        public int retrieveQuantity(int bookID)
        {
            int quantity = 0;
            db.openConnection();
            string query = "SELECT \"quantity\" FROM book_tbl WHERE \"bookID\"=@bookID";
            using(NpgsqlCommand cmd = new NpgsqlCommand(query, db.Connection()))
            {
                cmd.Parameters.AddWithValue("@bookID", bookID);

                try
                {
                    var result = cmd.ExecuteScalar();
                    if(result != null)
                    {
                        quantity = Convert.ToInt32(result);
                    }
                    else
                    {
                        MessageBox.Show("Quantity with the corresponding ID does not exist!");
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show("Failure to retrieve quantity: " + e.Message);
                }
            }
            db.closeConnection();
            return quantity;
        }
    }
}
