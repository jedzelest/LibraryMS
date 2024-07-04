using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Npgsql;
namespace LibraryManagementSys
{
    public partial class UserForm : Form
    {
       
        public UserForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            showUsers();
        }

        private void addUserBtn_Click(object sender, EventArgs e)
        {
            string name = nameTxt.Text;
            string email = emailTxt.Text;
            string phone_number = phoneTxt.Text;
            User user = new User();

            if (inputFieldsNotEmpty())
            {
                user.addUser(name, email, phone_number);
                clear();
                showUsers();
            }
            else
            {
                MessageBox.Show("Kindly Fill out all the fields correctly!");
            }
        }

        private void clear()
        {
            nameTxt.Text = "";
            emailTxt.Text = "";
            phoneTxt.Text = "";
        }

        private bool inputFieldsNotEmpty()
        {
            bool flag = false;
            if(nameTxt.Text.Length > 1 && emailTxt.Text.Length > 1 && phoneTxt.Text.Length > 1)
            {
                flag = true;
            }
            return flag;
        }

        private void showUsers()
        {
            User user = new User();
            dataGridView1.DataSource = user.returnUsersList();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                nameTxt.Text = row.Cells["name"].Value.ToString();
                emailTxt.Text = row.Cells["email"].Value.ToString();
                emailTxt.ReadOnly = true;
                phoneTxt.Text = row.Cells["phone_number"].Value.ToString();
            }
        }

        private void updateUserBtn_Click(object sender, EventArgs e)
        {
            User user = new User();
            string name = nameTxt.Text;
            string email = emailTxt.Text;
            string phone_number = phoneTxt.Text;
            int id = user.fetchUserID(email);

            if (inputFieldsNotEmpty())
            {
                user.updateUser(id, name, email, phone_number);
                clear();
                showUsers();
            }
            else
            {
                MessageBox.Show("Kindly Fill out all the fields correctly!");
            }
        }

        private void deleteUserBtn_Click(object sender, EventArgs e)
        {
            User user = new User();
            string email = emailTxt.Text;
            int id = user.fetchUserID(email);

           if (inputFieldsNotEmpty())
            {
                var confirmResult = MessageBox.Show("Are you sure you want to delete this user?", "Confirm Delete", MessageBoxButtons.YesNoCancel);
                if (confirmResult == DialogResult.Yes)
                {
                    user.deleteUser(id);
                    clear();
                    showUsers();
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
