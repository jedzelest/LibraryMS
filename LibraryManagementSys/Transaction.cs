using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using Npgsql;

namespace LibraryManagementSys
{
    class Transaction
    {
        private PostgresDBConnection db;
        public Transaction()
        {
            db = new PostgresDBConnection();
        }

        public void addTransaction(int bookID, int userID, DateTime checkoutDate, DateTime returnDate, int quantity_borrowed)
        {
            db.openConnection();
            string query = "INSERT INTO transaction_tbl (\"bookID\", \"userID\", \"checkoutDate\", \"returnDate\", \"quantity_borrowed\") VALUES (@bookID, @userID, @checkoutDate, @returnDate, @quantity_borrowed)";
            using (NpgsqlCommand cmd = new NpgsqlCommand(query, db.Connection()))
            {
                cmd.Parameters.AddWithValue("@bookID", bookID);
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.Parameters.AddWithValue("@checkoutDate", checkoutDate);
                cmd.Parameters.AddWithValue("@returnDate", returnDate);
                cmd.Parameters.AddWithValue("@quantity_borrowed", quantity_borrowed);

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Transaction Saved Successfully!");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error adding transaction: " + e.Message);
                }
            }
            db.closeConnection();
        }

        public DataTable readTransactions()
        {
            db.openConnection();
            string query = @"SELECT transaction_tbl.""transactionID"", 
                        book_tbl.title, 
                        user_tbl.name, 
                        transaction_tbl.""checkoutDate"", 
                        transaction_tbl.""returnDate"", 
                        transaction_tbl.""quantity_borrowed"" 
                 FROM transaction_tbl 
                 INNER JOIN book_tbl ON transaction_tbl.""bookID"" = book_tbl.""bookID"" 
                 INNER JOIN user_tbl ON transaction_tbl.""userID"" = user_tbl.""userID""
                 ORDER BY transaction_tbl.""transactionID"" DESC;";
            using (NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(query, db.Connection()))
            {
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                return dataTable;
                db.closeConnection();
            }
        }
    }
}
