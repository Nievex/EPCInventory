using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPC_Inventory.Seller
{
    public partial class ShopProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["SHOP_ID"] == null)
                {
                    Response.Redirect("../ShopLogin.aspx");
                }

                int shopId = Convert.ToInt32(Session["SHOP_ID"]);
                PopulateShopProfile(shopId);
            }
        }

        private void PopulateShopProfile(int shopId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    string query = "SELECT * FROM SHOP_PROFILE WHERE SHOP_ID = :ShopId";
                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("ShopId", OracleDbType.Int32).Value = shopId;
                        connection.Open();
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                ShopNameLabel.Text = reader["SHOP_NAME"].ToString();
                                ShopName.Text = reader["SHOP_NAME"].ToString();
                                ShopAddress.Text = reader["SHOP_ADDRESS"].ToString();
                                ShopContactNumber.Text = reader["SHOP_CONTACT_NUMBER"].ToString();
                                ShopEmail.Text = reader["SHOP_EMAIL"].ToString();
                                ShopUsername.Text = reader["SHOP_USERNAME"].ToString();
                                ShopOwnerFN.Text = reader["SHOP_OWNER_FN"].ToString();
                                ShopOwnerLN.Text = reader["SHOP_OWNER_LN"].ToString();
                                if (reader["SHOP_LOGO"] != DBNull.Value)
                                {
                                    byte[] logoBytes = (byte[])reader["SHOP_LOGO"];
                                    string base64String = Convert.ToBase64String(logoBytes, 0, logoBytes.Length);
                                    ShopLogoImagePreview.ImageUrl = "data:image/png;base64," + base64String;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void UpdateProfile_Click(object sender, EventArgs e)
        {
            try
            {
                int shopId = Convert.ToInt32(Session["SHOP_ID"]);
                string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    string updateQuery = "UPDATE SHOP_PROFILE SET " +
                                         "SHOP_NAME = :ShopName, " +
                                         "SHOP_ADDRESS = :Address, " +
                                         "SHOP_CONTACT_NUMBER = :ContactNumber, " +
                                         "SHOP_EMAIL = :Email, " +
                                         "SHOP_USERNAME = :Username, " +
                                         "SHOP_OWNER_FN = :OwnerFirstName, " +
                                         "SHOP_OWNER_LN = :OwnerLastName " +
                                         "WHERE SHOP_ID = :ShopId";

                    using (OracleCommand command = new OracleCommand(updateQuery, connection))
                    {
                        command.Parameters.Add("ShopName", OracleDbType.Varchar2).Value = ShopName.Text;
                        command.Parameters.Add("Address", OracleDbType.Varchar2).Value = ShopAddress.Text;
                        command.Parameters.Add("ContactNumber", OracleDbType.Varchar2).Value = ShopContactNumber.Text;
                        command.Parameters.Add("Email", OracleDbType.Varchar2).Value = ShopEmail.Text;
                        command.Parameters.Add("Username", OracleDbType.Varchar2).Value = ShopUsername.Text;
                        command.Parameters.Add("OwnerFirstName", OracleDbType.Varchar2).Value = ShopOwnerFN.Text;
                        command.Parameters.Add("OwnerLastName", OracleDbType.Varchar2).Value = ShopOwnerLN.Text;
                        command.Parameters.Add("ShopId", OracleDbType.Int32).Value = shopId;

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Response.Write("Shop profile updated successfully.");
                        }
                        else
                        {
                            Response.Write("Failed to update shop profile.");
                        }
                    }

                    byte[] logoBytes = null;
                    HttpPostedFile imageFile = Request.Files["inputFile"];
                    if (imageFile != null)
                    {
                        string fileName = Path.GetFileName(imageFile.FileName);
                        string fileExtension = Path.GetExtension(fileName).ToLower();

                        if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
                        {
                            logoBytes = new byte[imageFile.ContentLength];
                            imageFile.InputStream.Read(logoBytes, 0, imageFile.ContentLength);
                            UpdateShopLogo(connection, shopId, logoBytes);
                        }
                        else
                        {
                            Response.Write("Incorrect image format.");
                        }
                    }
                    else
                    {
                        Response.Write("File is a null");
                    }
                }
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            catch (Exception ex)
            {
                Response.Write("An error occurred while updating shop profile: " + ex.Message);
            }
        }


        private void UpdateShopLogo(OracleConnection connection, int shopId, byte[] logoBytes)
        {
            try
            {
                string updateLogoQuery = "UPDATE SHOP_PROFILE SET SHOP_LOGO = :Logo WHERE SHOP_ID = :ShopId";

                using (OracleCommand command = new OracleCommand(updateLogoQuery, connection))
                {
                    command.Parameters.Add("Logo", OracleDbType.Blob).Value = logoBytes;
                    command.Parameters.Add("ShopId", OracleDbType.Int32).Value = shopId;

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Response.Write("Shop logo updated successfully.");
                    }
                    else
                    {
                        Response.Write("Failed to update shop logo.");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("An error occurred while updating shop logo: " + ex.Message);
            }
        }

        protected void ChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                int shopId = Convert.ToInt32(Session["SHOP_ID"]);
                string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

                string oldPassword = OldPassword.Text;
                string newPassword = NewPassword.Text;
                string confirmPassword = ConfirmPassword.Text;

                // Implement logic to validate old password against database
                bool isOldPasswordValid = ValidateOldPassword(shopId, oldPassword);
                if (!isOldPasswordValid)
                {
                    Response.Write("Incorrect old password.");
                    return;
                }

                // Validate new password and confirm password match
                if (newPassword != confirmPassword)
                {
                    Response.Write("New password and confirm password do not match.");
                    return;
                }

                // Implement logic to update password in the database
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    string updatePasswordQuery = "UPDATE SHOP_PROFILE SET SHOP_PASSWORD = :NewPassword WHERE SHOP_ID = :ShopId";

                    using (OracleCommand command = new OracleCommand(updatePasswordQuery, connection))
                    {
                        command.Parameters.Add("NewPassword", OracleDbType.Varchar2).Value = newPassword;
                        command.Parameters.Add("ShopId", OracleDbType.Int32).Value = shopId;

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Response.Write("Password updated successfully.");
                        }
                        else
                        {
                            Response.Write("Failed to update password.");
                        }
                    }
                }

                // Clear form fields
                OldPassword.Text = "";
                NewPassword.Text = "";
                ConfirmPassword.Text = "";

                // Close the modal
                ScriptManager.RegisterStartupScript(this, GetType(), "CloseModalScript", "closeModal();", true);
            }
            catch (Exception ex)
            {
                Response.Write("An error occurred while updating password: " + ex.Message);
            }
        }

        private bool ValidateOldPassword(int shopId, string oldPassword)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                string query = "SELECT 1 FROM SHOP_PROFILE WHERE SHOP_ID = :ShopId AND SHOP_PASSWORD = :OldPassword";
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add("ShopId", OracleDbType.Int32).Value = shopId;
                    command.Parameters.Add("OldPassword", OracleDbType.Varchar2).Value = oldPassword;

                    connection.Open();
                    object result = command.ExecuteScalar();
                    return result != null;
                }
            }
        }

    }
}
