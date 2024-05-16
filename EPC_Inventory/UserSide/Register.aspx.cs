using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPC_Inventory.UserSide
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            string name = CustomerName.Text;
            int age = int.Parse(Age.Text);
            string birthday = Birthday.Text;
            string address = Address.Text;
            long contactNumber = long.Parse(ContactNumber.Text);
            string email = Email.Text;
            string password = Password.Text;

            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            string checkUserQuery = "SELECT COUNT(*) FROM CUSTOMER_PROFILE WHERE EMAIL = :Email";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                using (OracleCommand checkUserCommand = new OracleCommand(checkUserQuery, connection))
                {
                    checkUserCommand.Parameters.Add("Email", OracleDbType.Varchar2).Value = email;
                    int userCount = Convert.ToInt32(checkUserCommand.ExecuteScalar());

                    if (userCount > 0)
                    {
                        ErrorMessage.Text = "Email already exists!";
                        return;
                    }
                }

                string query = @"INSERT INTO CUSTOMER_PROFILE 
                            (CUSTOMER_ID, CUSTOMER_NAME, AGE, BIRTHDAY, ADDRESS, CONTACT_NUMBER, EMAIL, PASSWORD) 
                            VALUES (CUSTOMER_PROFILE_SEQ.NEXTVAL, :Name, :Age, :Birthday, :Address, :ContactNumber, :Email, :Password)";

                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add("Name", OracleDbType.Varchar2).Value = name;
                    command.Parameters.Add("Age", OracleDbType.Int32).Value = age;
                    command.Parameters.Add("Birthday", OracleDbType.Varchar2).Value = birthday;
                    command.Parameters.Add("Address", OracleDbType.Varchar2).Value = address;
                    command.Parameters.Add("ContactNumber", OracleDbType.Int64).Value = contactNumber;
                    command.Parameters.Add("Email", OracleDbType.Varchar2).Value = email;
                    command.Parameters.Add("Password", OracleDbType.Varchar2).Value = password;

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        ErrorMessage.Text = "Registration failed!";
                    }
                }
            }
        }
    }
}