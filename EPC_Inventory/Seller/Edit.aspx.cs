using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace EPC_Inventory.Seller
{
    public partial class Edit : System.Web.UI.Page
    {
        private byte[] existingImageData;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["productId"] != null)
                {
                    string productId = Request.QueryString["productId"];
                    FetchAndPopulateProductDetails(productId);
                }

                if (Session["SHOP_ID"] == null)
                {
                    Response.Redirect("../ShopLogin.aspx");
                }
            }
        }

        private void FetchAndPopulateProductDetails(string productId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            string query = "SELECT * FROM PRODUCTSINV WHERE PRODUCTID = :ProductId";
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.BindByName = true;
                    command.Parameters.Add("ProductId", OracleDbType.Decimal).Value = Convert.ToDecimal(productId);
                    connection.Open();
                    OracleDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        BreadcrumbID.Text = reader["NAME"].ToString();
                        ProductIdField.Text = reader["PRODUCTID"].ToString();
                        ProductNameField.Text = reader["NAME"].ToString();
                        CategoryList.Text = reader["CATEGORY"].ToString();
                        StocksField.Text = reader["STOCKS"].ToString();
                        PriceField.Text = reader["PRICE"].ToString();

                        if (reader["IMAGEURL"] != DBNull.Value)
                        {
                            existingImageData = (byte[])reader["IMAGEURL"];
                            string base64Image = Convert.ToBase64String(existingImageData);
                            string imageUrl = "data:image/jpeg;base64," + base64Image;
                            ImagePreview.ImageUrl = imageUrl;
                        }
                        else
                        {
                            existingImageData = null;
                            ImagePreview.ImageUrl = string.Empty;
                        }
                    }
                    reader.Close();
                }
            }
        }

        protected void UpdateBtn(object sender, EventArgs e)
        {
            decimal productId;
            bool productIdConversionSuccess = Decimal.TryParse(ProductIdField.Text, out productId);

            if (!productIdConversionSuccess)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid product ID input.');", true);
                return;
            }

            string productName = ProductNameField.Text;
            int stocks = Convert.ToInt32(StocksField.Text);
            decimal price;
            bool priceConversionSuccess = Decimal.TryParse(PriceField.Text, out price);

            if (!priceConversionSuccess)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid price input.');", true);
                return;
            }

            byte[] imageBytes = null;
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
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Incorrect image format');", true);
                    return;
                }
            }

            string category = CategoryList.SelectedValue;
            if (string.IsNullOrEmpty(category))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please select a category.');", true);
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            string query = "UPDATE PRODUCTSINV SET NAME = :ProductName, CATEGORY = :Category, STOCKS = :Stocks, PRICE = :Price";

            if (imageBytes != null)
            {
                query += ", IMAGEURL = :ImageData";
            }

            query += " WHERE PRODUCTID = :ProductId";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.BindByName = true;
                    command.Parameters.Add("ProductId", OracleDbType.Decimal).Value = productId;
                    command.Parameters.Add("ProductName", OracleDbType.Varchar2).Value = productName;
                    command.Parameters.Add("Category", OracleDbType.Varchar2).Value = category;
                    command.Parameters.Add("Stocks", OracleDbType.Decimal).Value = stocks;
                    command.Parameters.Add("Price", OracleDbType.Decimal).Value = price;

                    if (imageBytes != null)
                    {
                        command.Parameters.Add("ImageData", OracleDbType.Blob).Value = imageBytes;
                    }

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Clear form fields and display success message
                        ProductIdField.Text = string.Empty;
                        ProductNameField.Text = string.Empty;
                        CategoryList.SelectedIndex = -1; // Clear selected category
                        StocksField.Text = string.Empty;
                        PriceField.Text = string.Empty;
                        ImagePreview.ImageUrl = string.Empty;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Product updated successfully!');", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Failed to update product');", true);
                    }
                }
            }
        }


    }
}