using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace EPC_Inventory
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
                        ProductIdField.Text = reader["PRODUCTID"].ToString();
                        ProductNameField.Text = reader["NAME"].ToString();
                        CategoryField.Text = reader["CATEGORY"].ToString();
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
                            ImagePreview.ImageUrl = string.Empty; // Or you can set a default image URL
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
                // Handle invalid product ID input
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid product ID input.');", true);
                return;
            }

            string productName = ProductNameField.Text;
            string category = CategoryField.Text;
            int stocks = Convert.ToInt32(StocksField.Text);
            decimal price;
            bool priceConversionSuccess = Decimal.TryParse(PriceField.Text, out price);

            if (!priceConversionSuccess)
            {
                // Handle invalid price input
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid price input.');", true);
                return;
            }

            byte[] imageData = existingImageData; // Use the existing image data by default

            if (ImageFileUpload.HasFile)
            {
                // If a new image is provided, update the image data
                HttpPostedFile imageFile = ImageFileUpload.PostedFile;
                using (BinaryReader reader = new BinaryReader(imageFile.InputStream))
                {
                    imageData = reader.ReadBytes((int)imageFile.InputStream.Length);
                }
            }

            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            string query = "UPDATE PRODUCTSINV SET NAME = :ProductName, CATEGORY = :Category, STOCKS = :Stocks, PRICE = :Price";

            // Only add the image data parameter if a new image is provided
            if (ImageFileUpload.HasFile || imageData != existingImageData)
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

                    // Only set the image data parameter if a new image is provided or if existing image data is different
                    if (ImageFileUpload.HasFile || imageData != existingImageData)
                    {
                        command.Parameters.Add("ImageData", OracleDbType.Blob).Value = imageData;
                    }

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        ProductIdField.Text = string.Empty;
                        ProductNameField.Text = string.Empty;
                        CategoryField.Text = string.Empty;
                        StocksField.Text = string.Empty;
                        PriceField.Text = string.Empty;
                        ImagePreview.ImageUrl = string.Empty;
                        Response.Redirect("Inventory.aspx");
                        Session["UpdateMessage"] = "Product updated successfully.";
                    }
                    else
                    {
                        Session["UpdateMessage"] = "Failed to update product.";
                    }
                }
            }

        }

    }
}