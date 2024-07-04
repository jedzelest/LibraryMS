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
    public partial class BookForm : Form
    {
        public BookForm()
        {
            InitializeComponent();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            string title = titleTxt.Text;
            string author = authorTxt.Text;
            string genre = genreTxt.Text;
            int quantity = Convert.ToInt32(qtyTxt.Text);
            Book book = new Book();

            if (inputFieldsNotEmpty())
            {
                book.addBook(title, author, genre, quantity);
                clear();
                showBooks();
            }
            else
            {
                MessageBox.Show("Kindly Fill out all the fields correctly!");
            }
        }

        private void clear()
        {
            idTxt.Text = "";
            titleTxt.Text = "";
            authorTxt.Text = "";
            genreTxt.Text = "";
            qtyTxt.Text = "";
        }

        private bool inputFieldsNotEmpty()
        {
            bool flag = false;
            if(titleTxt.Text.Length > 1 && authorTxt.Text.Length > 1 && genreTxt.Text.Length > 1 && qtyTxt.Text.Length >= 1)
            {
                flag = true;
            }
            return flag;
        } 

        private void showBooks()
        {
            Book book = new Book();
            dataGridView1.DataSource = book.returnBooksList();
        }

        private void BookForm_Load(object sender, EventArgs e)
        {
            showBooks();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                idTxt.Text = row.Cells["bookID"].Value.ToString();
                titleTxt.Text = row.Cells["title"].Value.ToString();
                authorTxt.Text = row.Cells["author"].Value.ToString();
                genreTxt.Text = row.Cells["genre"].Value.ToString();
                qtyTxt.Text = row.Cells["quantity"].Value.ToString();
            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(idTxt.Text);
            string title = titleTxt.Text;
            string author = authorTxt.Text;
            string genre = genreTxt.Text;
            int quantity = Convert.ToInt32(qtyTxt.Text);
            Book book = new Book();

            if (inputFieldsNotEmpty())
            {
                book.updateBook(id, title, author, genre, quantity);
                clear();
                showBooks();
            }
            else
            {
                MessageBox.Show("Kindly Fill out all the fields correctly!");
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(idTxt.Text);
            Book book = new Book();

            if (inputFieldsNotEmpty())
            {
                var confirmResult = MessageBox.Show("Are you sure you want to delete this book?", "Confirm Delete", MessageBoxButtons.YesNoCancel);
                if(confirmResult == DialogResult.Yes)
                {
                    book.deleteBook(id);
                    clear();
                    showBooks();
                }
            }
        }

        private void returnBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }
    }
}
