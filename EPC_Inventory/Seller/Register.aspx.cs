using System;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.IO;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Web;
using System.Web.UI;

namespace EPC_Inventory.Seller
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RegisterShopBtn_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                try
                {
                    byte[] imageBytes = null;
                    byte[] shopImageBytes = null;
                    HttpPostedFile imageFile = Request.Files["inputFile"];
                    if (imageFile != null && imageFile.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(imageFile.FileName);
                        string fileExtension = Path.GetExtension(fileName).ToLower();

                        if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
                        {
                            imageBytes = new byte[imageFile.ContentLength];
                            imageFile.InputStream.Read(imageBytes, 0, imageFile.ContentLength);
                        }
                        else
                        {
                            Console.WriteLine("Incorrect image format.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("File is a null");
                    }

                    HttpPostedFile shopImageFile = Request.Files["shopRequirementFile"];
                    if (shopImageFile != null && shopImageFile.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(shopImageFile.FileName);
                        string fileExtension = Path.GetExtension(fileName).ToLower();

                        if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
                        {
                            shopImageBytes = new byte[shopImageFile.ContentLength];
                            shopImageFile.InputStream.Read(shopImageBytes, 0, shopImageFile.ContentLength);
                            FileUploaded.Text = "File Uploaded";
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
                    Random rand = new Random();
                    int shopRegId = rand.Next(10000000, 99999999);
                    string shopName = ShopNameField.Text;
                    string address = AddressField.Text;
                    string contactNo = ContactNoField.Text;
                    string email = EmailField.Text;
                    string firstName = FirstNameField.Text;
                    string lastName = LastNameField.Text;
                    string shopUsername = ShopUsernameField.Text;
                    string password = PasswordField.Text;
                    byte[] shopRequirements = shopImageBytes;
                    byte[] shopLogo = imageBytes;
                    DateTime creationDate = DateTime.Now;

                    string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
                    using (OracleConnection connection = new OracleConnection(connectionString))
                    {
                        connection.Open();
                        string query = @"INSERT INTO SHOP_REGISTRATION (SHOP_REG_ID, SHOP_NAME, SHOP_ADDRESS, SHOP_CONTACT_NUMBER, SHOP_EMAIL, SHOP_OWNER_FN, SHOP_OWNER_LN, SHOP_USERNAME, SHOP_PASSWORD, SHOP_STATUS, SHOP_REQUIREMENTS, SHOP_LOGO, CREATION_DATE)
                                 VALUES (:ShopRegId, :ShopName, :Address, :ContactNo, :Email, :FirstName, :LastName, :ShopUsername, :Password, 'Active', :ShopRequirements, :ShopLogo, :CreationDate)";
                        using (OracleCommand command = new OracleCommand(query, connection))
                        {
                            command.Parameters.Add("ShopRegId", OracleDbType.Decimal).Value = shopRegId;
                            command.Parameters.Add("ShopName", OracleDbType.Varchar2).Value = shopName;
                            command.Parameters.Add("Address", OracleDbType.Varchar2).Value = address;
                            command.Parameters.Add("ContactNo", OracleDbType.Varchar2).Value = contactNo;
                            command.Parameters.Add("Email", OracleDbType.Varchar2).Value = email;
                            command.Parameters.Add("FirstName", OracleDbType.Varchar2).Value = firstName;
                            command.Parameters.Add("LastName", OracleDbType.Varchar2).Value = lastName;
                            command.Parameters.Add("ShopUsername", OracleDbType.Varchar2).Value = shopUsername;
                            command.Parameters.Add("Password", OracleDbType.Varchar2).Value = password;
                            command.Parameters.Add("ShopRequirements", OracleDbType.Blob).Value = shopRequirements;
                            command.Parameters.Add("ShopLogo", OracleDbType.Blob).Value = shopLogo;
                            command.Parameters.Add("CreationDate", OracleDbType.Date).Value = creationDate;

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "SuccessAlert", "alert('Registered successfully!');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "SuccessAlert", "alert('Registration failed');", true);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessageLabel.Text = "An error occurred: " + ex.Message;
                }
            }
            else
            {
                ErrorMessageLabel.Text = "Please fill in all required fields.";
            }
        }

        private bool IsValidData()
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(ShopNameField.Text))
            {
                ShopNameField.CssClass = "invalid-field";
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(AddressField.Text))
            {
                AddressField.CssClass = "invalid-field";
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(ContactNoField.Text))
            {
                ContactNoField.CssClass = "invalid-field";
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(EmailField.Text))
            {
                EmailField.CssClass = "invalid-field";
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(FirstNameField.Text))
            {
                FirstNameField.CssClass = "invalid-field";
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(LastNameField.Text))
            {
                LastNameField.CssClass = "invalid-field";
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(ShopUsernameField.Text))
            {
                ShopUsernameField.CssClass = "invalid-field";
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(PasswordField.Text))
            {
                PasswordField.CssClass = "invalid-field";
                isValid = false;
            }

            return isValid;
        }
    }
}
