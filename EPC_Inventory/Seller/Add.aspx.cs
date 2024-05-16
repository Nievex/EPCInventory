using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPC_Inventory
{
    public partial class Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["SHOP_ID"] == null)
                {
                    Response.Redirect("../ShopLogin.aspx");
                }
            }
        }

        protected void AddBtn(object sender, EventArgs e)
        {
            int productID;
            if (!int.TryParse(ProductIdField.Text, out productID))
            {
                Response.Write("Invalid product ID format.");
                return;
            }

            string productName = ProductNameField.Text;
            decimal price;
            if (!decimal.TryParse(PriceField.Text, out price))
            {
                Response.Write("Invalid price format.");
                return;
            }

            int stocks;
            if (!int.TryParse(StocksField.Text, out stocks))
            {
                Response.Write("Invalid stocks format.");
                return;
            }

            string category = CategoryList.SelectedValue;
            if (string.IsNullOrEmpty(category))
            {
                Response.Write("Please select a category.");
                return;
            }

            HttpPostedFile imageFile = Request.Files["inputFile"];
            if (imageFile != null)
            {
                string fileName = Path.GetFileName(imageFile.FileName);
                string fileExtension = Path.GetExtension(fileName).ToLower();

                if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
                {
                    byte[] imageBytes = new byte[imageFile.ContentLength];
                    imageFile.InputStream.Read(imageBytes, 0, imageFile.ContentLength);
                    InsertProduct(productID, productName, price, imageBytes, category, stocks);
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

            ProductIdField.Text = "";
            ProductNameField.Text = "";
            PriceField.Text = "";
            StocksField.Text = "";
            CategoryList.Text = "Dog Food";
        }

        protected void InsertProduct(int productID, string productName, decimal price, byte[] imageBytes, string category, int stocks)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            try
            {
                int shopId = Convert.ToInt32(Session["SHOP_ID"]);
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    string insertQuery = "INSERT INTO PRODUCTS (PRODUCT_ID, NAME, CATEGORY, STOCKS, PRICE, IMAGEURL, SHOP_ID) " +
                                          "VALUES (:ProductId, :ProductName, :Category, :Stocks, :Price, :Image, :ShopId)";

                    using (OracleCommand command = new OracleCommand(insertQuery, connection))
                    {
                        command.Parameters.Add("ProductId", OracleDbType.Int32).Value = productID;
                        command.Parameters.Add("ProductName", OracleDbType.Varchar2).Value = productName;
                        command.Parameters.Add("Category", OracleDbType.Varchar2).Value = category;
                        command.Parameters.Add("Stocks", OracleDbType.Int32).Value = stocks;
                        command.Parameters.Add("Price", OracleDbType.Decimal).Value = price;
                        command.Parameters.Add("Image", OracleDbType.Blob).Value = imageBytes;
                        command.Parameters.Add("ShopId", OracleDbType.Int32).Value = shopId;

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "SuccessAlert", "alert('Product added successfully.');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorAlert", $"alert('An error occurred while inserting product: {ex.Message}');", true);
            }
        }
    }
}