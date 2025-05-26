using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Npgsql;

namespace Kinar_Bakery.Autentiikasi
{
    public class Autentikasi
    {
        private readonly DatabaseConnection dbConnection;

        public Autentikasi()
        {
            dbConnection = new DatabaseConnection();
        }

        public bool RegisterUser(string namaLengkap, string username, string password, string nomorTelepon, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrEmpty(namaLengkap) || string.IsNullOrEmpty(username) || 
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(nomorTelepon))
            {
                errorMessage = "Semua kolom harus diisi!";
                return false;
            }

            if (username.ToLower() == "admin" || username.ToLower() == "kasir")
            {
                errorMessage = "Username 'admin' atau 'kasir' tidak diperbolehkan!";
                return false;
            }

            try
            {
                string checkQuery = "SELECT * FROM users WHERE username = @username";
                NpgsqlParameter[] checkParameters = { new NpgsqlParameter("@username", username) };
                var checkResult = dbConnection.ExecuteQuery(checkQuery, checkParameters);

                if (checkResult.Rows.Count > 0)
                {
                    errorMessage = "Username sudah terdaftar!";
                    return false;
                }

                string insertQuery = "INSERT INTO users (nama_lengkap, username, password, nomor_telepon) VALUES (@nama_lengkap, @username, @password, @nomor_telepon)";
                NpgsqlParameter[] insertParameters = {
                    new NpgsqlParameter("@nama_lengkap", namaLengkap),
                    new NpgsqlParameter("@username", username),
                    new NpgsqlParameter("@password", password), 
                    new NpgsqlParameter("@nomor_telepon", nomorTelepon)
                };

                int rowsAffected = dbConnection.ExecuteNonQuery(insertQuery, insertParameters);

                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    errorMessage = "Registrasi gagal!";
                    return false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public bool LoginUser(string username, string password, out string errorMessage, out string role)
        {
            errorMessage = string.Empty;
            role = string.Empty;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                errorMessage = "Username dan password harus diisi!";
                return false;
            }

            try
            {
                string query = "SELECT * FROM users WHERE username = @username AND password = @password";
                NpgsqlParameter[] parameters = {
                    new NpgsqlParameter("@username", username),
                    new NpgsqlParameter("@password", password)
                };

                var result = dbConnection.ExecuteQuery(query, parameters);

                if (result.Rows.Count > 0)
                {
                    role = username.ToLower() == "admin" ? "Admin" :
                           username.ToLower() == "kasir" ? "Kasir" : "Pelanggan";
                    return true;
                }
                else
                {
                    errorMessage = "Username atau password salah!";
                    return false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}