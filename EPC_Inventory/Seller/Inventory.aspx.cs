using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPC_Inventory
{
    public partial class MasterInventory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["SHOP_ID"] != null)
                {
                    ViewState["SortDirection"] = "ASC"; // Default sort direction
                    BindInventoryData(Convert.ToInt32(Session["SHOP_ID"]));
                }
                else
                {
                    Response.Redirect("../ShopLogin.aspx");
                }
            }
        }

        protected void BindInventoryData(int userId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    string sortOrder = ViewState["SortDirection"].ToString();
                    string query = $@"SELECT Name, PRODUCT_ID, Stocks, Price, Category, ImageURL 
                                      FROM PRODUCTS 
                                      WHERE SHOP_ID = :UserId 
                                      ORDER BY Stocks {sortOrder}";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("UserId", OracleDbType.Decimal).Value = userId;
                        connection.Open();
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                DataTable dt = new DataTable();
                                dt.Load(reader);
                                RepeaterInventory.DataSource = dt;
                                RepeaterInventory.DataBind();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        protected string GetBase64Image(object imageData)
        {
            if (imageData == DBNull.Value)
            {
                return "";
            }

            byte[] bytes = (byte[])imageData;
            string base64String = Convert.ToBase64String(bytes);
            string imageUrl = "data:image/jpeg;base64," + base64String;
            return imageUrl;
        }

        protected string GetStockColor(object stock)
        {
            int stockAmount = Convert.ToInt32(stock);

            if (stockAmount > 750)
            {
                return "color: green;";
            }
            else if (stockAmount >= 500 && stockAmount <= 749)
            {
                return "color: yellow;";
            }
            else if (stockAmount >= 300 && stockAmount <= 499)
            {
                return "color: orange;";
            }
            else if (stockAmount >= 1 && stockAmount <= 299)
            {
                return "color: red;";
            }
            else
            {
                return "color: black;";
            }
        }

        protected void DeleteProduct(object sender, EventArgs e)
        {
            try
            {
                Button deleteButton = (Button)sender;
                string productId = deleteButton.CommandArgument;

                int shopId = Convert.ToInt32(Session["SHOP_ID"]);

                string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
                string query = "DELETE FROM PRODUCTS WHERE PRODUCT_ID = :ProductId AND SHOP_ID = :ShopId";

                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add("ProductId", OracleDbType.Decimal).Value = Convert.ToDecimal(productId);
                        command.Parameters.Add("ShopId", OracleDbType.Decimal).Value = shopId;

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Product deleted successfully.'); window.location.href = window.location.href;", true);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Failed to delete product.');", true);
                        }
                    }
                }
                BindInventoryData(shopId);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occurred while deleting the product.');", true);
            }
        }

        protected void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchQuery = searchTextBox.Text.Trim().ToLower();
                string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    string whereClause = " WHERE ";

                    if (searchQuery.StartsWith("id:"))
                    {
                        string productId = searchQuery.Substring(3);
                        whereClause += "PRODUCT_ID = :ProductId";
                    }
                    else
                    {
                        whereClause += "LOWER(Name) LIKE '%' || :SearchQuery || '%' OR LOWER(Category) LIKE '%' || :SearchQuery || '%'";
                    }

                    string query = "SELECT Name, PRODUCT_ID, Stocks, Price, Category, ImageURL FROM PRODUCTS " + whereClause;

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        if (!searchQuery.StartsWith("id:"))
                        {
                            command.Parameters.Add("SearchQuery", OracleDbType.Varchar2).Value = searchQuery;
                        }
                        else
                        {
                            string productId = searchQuery.Substring(3);
                            command.Parameters.Add("ProductId", OracleDbType.Varchar2).Value = productId;
                        }

                        connection.Open();

                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                DataTable dt = new DataTable();
                                dt.Load(reader);
                                RepeaterInventory.DataSource = dt;
                                RepeaterInventory.DataBind();
                            }
                            else
                            {
                                RepeaterInventory.DataSource = null;
                                RepeaterInventory.DataBind();
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No matching records found.');", true);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('An error occurred while searching.');", true);
            }
        }

        protected void SortByStock(object sender, EventArgs e)
        {
            string currentSortDirection = ViewState["SortDirection"].ToString();
            string newSortDirection = currentSortDirection == "ASC" ? "DESC" : "ASC";
            ViewState["SortDirection"] = newSortDirection;

            if (Session["SHOP_ID"] != null)
            {
                BindInventoryData(Convert.ToInt32(Session["SHOP_ID"]));
            }
        }
    }
}
