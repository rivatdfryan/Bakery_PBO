using Kinar_Bakery.Autentiikasi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Kinar_Bakery
{
    public partial class Login : Form
    {
        private readonly Autentikasi autentikasi;

        public Login()
        {
            try
            {
                InitializeComponent();
                autentikasi = new Autentikasi();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal menginisialisasi form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      
            

        private void Login_Load(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Register register = new Register();
            register.ShowDialog();
            this.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnLogin_Click_1(object sender, EventArgs e)
        {
            try
            {
                string username = Username.Text;
                string password = Password.Text;

                string errorMessage;
                string role;
                bool success = autentikasi.LoginUser(username, password, out errorMessage, out role);

                if (success)
                {
                    MessageBox.Show("Login berhasil!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    switch (role)
                    {
                        case "Admin":
                            this.Hide();
                            HomeDasboardAdmin homeDasboardAdmin = new HomeDasboardAdmin();
                            homeDasboardAdmin.ShowDialog();
                            this.Show();
                            break;
                        case "Kasir":
                            //new HomeKasirDashboard().Show();
                            break;
                        default:
                            //new HomePelangganDashboard().Show();
                            break;
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show(errorMessage, "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saat login: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
    }
    }
}