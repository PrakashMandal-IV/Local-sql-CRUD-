using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace db_test
{
    public partial class Form1 : Form
    {
        SqlConnection connection;
        string connecionString;
        public Form1()
        {
            InitializeComponent();
            connecionString = ConfigurationManager.ConnectionStrings["db_test.Properties.Settings.DBTestConnectionString"].ConnectionString;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PopulaterUser();           
        }
        private void PopulaterUser()
        {
            using (connection = new SqlConnection(connecionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM [User]", connection))
            {
                DataTable userTable = new DataTable();
                adapter.Fill(userTable);

                UserBox.DisplayMember = "Name";
                UserBox.ValueMember = "Id";
                UserBox.DataSource = userTable;
            }                  
        }
        private void PopulatePasswords()
        {
            string query = "SELECT a.Name FROM Password a " +
                "INNER JOIN UserPassword b ON a.Id = b.PasswordId " +
                "WHERE b.UserId = @UserId";
            using (connection = new SqlConnection(connecionString))             
            using(SqlCommand command = new SqlCommand(query, connection))
            using(SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@UserId",UserBox.SelectedValue);
                DataTable Passwordtable = new DataTable();
                adapter.Fill(Passwordtable);

                PasswordBox.DisplayMember = "Name";
                PasswordBox.ValueMember = "Id";
                PasswordBox.DataSource = Passwordtable;
            }
        }   
        
        private void UserBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulatePasswords();
        }

        private void PasswordBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
