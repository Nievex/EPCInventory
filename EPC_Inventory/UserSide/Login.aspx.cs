using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using System.Web.UI;

namespace EPC_Inventory.UserSide
{
    public partial class Login : System.Web.UI.Page
    {
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string email = Email.Text;
            string password = Password.Text;

            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            string query = "SELECT * FROM CUSTOMER_PROFILE WHERE EMAIL = :Email AND PASSWORD = :Password";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add("Email", OracleDbType.Varchar2).Value = email;
                    command.Parameters.Add("Password", OracleDbType.Varchar2).Value = password;

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int customerId = Convert.ToInt32(reader["CUSTOMER_ID"]);
                            string customerName = reader["CUSTOMER_NAME"].ToString();
                            string address = reader["CUSTOMER_ADDRESS"].ToString();
                            string contactNumber = reader["CONTACT_NUMBER"].ToString();

                            Session["CUSTOMER_ID"] = customerId;
                            Session["CUSTOMER_NAME"] = customerName;
                            Session["CUSTOMER_ADDRESS"] = address;
                            Session["CONTACT_NUMBER"] = contactNumber;

                            Response.Redirect("Menu.aspx");
                        }
                        else
                        {
                            ErrorMessage.Text = "Invalid email or password!";
                        }
                    }
                }
            }
        }
    }
}
