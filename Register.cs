using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Windows.Forms;
using Kinar_Bakery.Autentiikasi;

namespace Kinar_Bakery
{
    public partial class Register : Form
    {
        private readonly Autentikasi autentikasi;

        public Register()
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string namaLengkap = NamaLengkap.Text;
                string username = Username.Text;
                string password = Password.Text;
                string nomorTelepon = NomorTelepon.Text;

                string errorMessage;
                bool success = autentikasi.RegisterUser(namaLengkap, username, password, nomorTelepon, out errorMessage);

                if (success)
                {
                    MessageBox.Show("Registrasi berhasil!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    new Login().Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(errorMessage, "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saat registrasi: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Login().Show();
            this.Close();
        }

        private void Register_Load(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
            this.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}