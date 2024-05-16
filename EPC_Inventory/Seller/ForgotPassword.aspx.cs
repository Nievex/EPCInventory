using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace EPC_Inventory.Seller
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Submit_Click(object sender, EventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            string email = GetShopEmailByUsername(username);

            if (!string.IsNullOrEmpty(email))
            {
                string newPassword = GenerateRandomPassword();
                UpdateShopPassword(username, newPassword);
                SendPasswordEmail(email, newPassword);
                MessageLabel.Text = "Password reset email sent. Please check your email.";
            }
            else
            {
                MessageLabel.Text = "Username not found.";
            }
        }

        private string GetShopEmailByUsername(string username)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            string query = "SELECT SHOP_EMAIL FROM SHOP_PROFILE WHERE SHOP_USERNAME = :Username";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add("Username", OracleDbType.Varchar2).Value = username;

                    connection.Open();
                    object result = command.ExecuteScalar();
                    return result != null ? result.ToString() : null;
                }
            }
        }

        private string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void UpdateShopPassword(string username, string newPassword)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            string query = "UPDATE SHOP_PROFILE SET SHOP_PASSWORD = :NewPassword WHERE SHOP_USERNAME = :Username";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add("NewPassword", OracleDbType.Varchar2).Value = newPassword;
                    command.Parameters.Add("Username", OracleDbType.Varchar2).Value = username;

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageLabel.Text = "Password updated succesfully!";
                    }
                    else
                    {
                        MessageLabel.Text = "Failed to update password.";
                    }
                }
            }
        }

        private void SendPasswordEmail(string email, string newPassword)
        {
            try
            {
                string senderEmail = "easypetcare.shop@gmail.com";
                string senderPassword = "rnyl byum hkxf vqoc";

                MailMessage mail = new MailMessage(senderEmail, email);
                SmtpClient client = new SmtpClient();

                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);

                mail.Subject = "Password Reset";
                mail.Body = $"Your new password is: {newPassword}\r\n" +
                            $"Use this new password to reset your old password in your Profile page.\r\n" +
                            "\r\n" +
                            $"Best regards,\r\n" +
                            $"Easy Pet Care";

                client.Send(mail);
            }
            catch (Exception ex)
            {
                // Log or handle the exception more descriptively
                MessageLabel.Text = $"An error occurred while sending the password reset email: {ex.Message}";
                return;
            }

            // Set the success message if the email was sent successfully
            MessageLabel.Text = "Your password was sent to your email";
        }

    }
}
