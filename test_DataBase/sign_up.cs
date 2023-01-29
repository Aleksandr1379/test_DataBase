using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Text;
using System.Windows.Forms.VisualStyles;

namespace test_DataBase
{
    public partial class sign_up : Form
    {
        DataBase dataBase = new DataBase();
        public sign_up()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void sign_up_Load(object sender, EventArgs e)
        {
            textBoxPass2.PasswordChar = '*';
            pictureBoxPas2.Visible = false;
        }

        private void buttonReg_Click(object sender, EventArgs e)
        {
            DataBase dataBase = new DataBase();

            var login = textBoxLogin2.Text;
            var password = textBoxPass2.Text;

            string querystring = $"insert into register(login_user, password_user) values('{login}', '{password}')";

            SqlCommand command = new SqlCommand(querystring, dataBase.GetConnection());

            dataBase.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт успешно создан!", "Успех!");
                log_in frm_login = new log_in();
                this.Hide();
                frm_login.ShowDialog();
            }
            else
            {
                MessageBox.Show("Аккаунт не создан!");
            }
            dataBase.closedConnection();
        }
             private Boolean checkuser()
            {
                var loginUser = textBoxLogin2.Text;
                var passUser = textBoxPass2.Text;

                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();
                string querystring = $"select id_user, login_user, password_user from register where login_user = '{loginUser}' and password_user = '{passUser}'";

                SqlCommand command = new SqlCommand(querystring, dataBase.GetConnection());

                adapter.SelectCommand = command;
                adapter.Fill(table);

                if(table.Rows.Count > 0 )
                {
                    MessageBox.Show("Пользователь уже существует!");
                    return true;
                }
                else 
                { 
                    return false;
                }
            }

        private void buttonClean_Click(object sender, EventArgs e)
        {
            textBoxLogin2.Text = "";
            textBoxPass2.Text = "";
        }

        private void pictureBoxPas1_Click(object sender, EventArgs e)
        {
            textBoxPass2.UseSystemPasswordChar = false;
            pictureBoxPas1.Visible = false;
            pictureBoxPas2.Visible = true;
        }

        private void pictureBoxPas2_Click(object sender, EventArgs e)
        {
            textBoxPass2.UseSystemPasswordChar = true;
            pictureBoxPas1.Visible = true;
            pictureBoxPas2.Visible = false;
        }
    }
}
