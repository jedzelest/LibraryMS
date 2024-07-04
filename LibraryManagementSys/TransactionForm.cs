using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSys
{
    public partial class TransactionForm : Form
    {
        public TransactionForm()
        {
            InitializeComponent();
        }

        private void TransactionForm_Load(object sender, EventArgs e)
        {
            User user = new User();
            userComboBox.DataSource = user.generateUserNames();
            userComboBox.DisplayMember = "name";

            Book book = new Book();
            bookComboBox.DataSource = book.generateBookTitles();
            bookComboBox.DisplayMember = "title";
            readTransactions();
        }

        private void returnBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }

        private void transactBtn_Click(object sender, EventArgs e)
        {
            User user = new User();
            Book book = new Book();

            string name = userComboBox.Text;
            int userID = user.returnUserID(name);

            string title = bookComboBox.Text;
            int bookID = book.returnBookID(title);

            DateTime checkoutDate = DateTime.Now;

            dateTimePicker1.Value = checkoutDate;

            DateTime selectedReturnDate = rentDateTimePicker.Value;

            int quantity = Convert.ToInt32(qtyTxt.Text);

            if (checkFields())
            { 
                int existingQuantity = book.retrieveQuantity(bookID);
                if (quantity > existingQuantity)
                {
                    MessageBox.Show("Asking quantity exceeded!");
                }
                else
                {
                    Transaction transaction = new Transaction();
                    transaction.addTransaction(bookID, userID, checkoutDate, selectedReturnDate, quantity);
                    int newQuantity = existingQuantity - quantity;
                    book.updateQuantity(bookID, newQuantity);
                    readTransactions();
                }
                
            }
        }

        private bool checkFields()
        {
            bool flag = false;
            if(Convert.ToInt32(qtyTxt.Text) > 0 && qtyTxt.Text.Length > 0)
            {
                flag = true;
            }

            return flag;
        }

        private void readTransactions()
        {
            Transaction transaction = new Transaction();
            dataGridView1.DataSource = transaction.readTransactions();
        }
    }
}
