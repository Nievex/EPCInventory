using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;

namespace EPC_Inventory.Shop
{
    public partial class ShopLogin : System.Web.UI.Page
    {
        protected void LoginBtnClick(object sender, EventArgs e)
        {
            string username = UsernameText.Text.Trim();
            string password = PasswordText.Text.Trim();

            int shopId = AuthenticateUser(username, password);

            if (shopId != -1)
            {
                Session["SHOP_ID"] = shopId;
                Response.Redirect("Seller/Inventory.aspx");
            }
            else
            {
                ValidationLabel.Text = "Invalid username or password.";
            }
        }

        private int AuthenticateUser(string username, string password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            string query = "SELECT SHOP_ID FROM SHOP_PROFILE WHERE SHOP_USERNAME = :Username AND SHOP_PASSWORD = :Password";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add("Username", OracleDbType.Varchar2).Value = username;
                    command.Parameters.Add("Password", OracleDbType.Varchar2).Value = password;

                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }
    }
}