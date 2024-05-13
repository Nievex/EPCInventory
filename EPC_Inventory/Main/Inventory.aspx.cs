using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using System.Text;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace EPC_Inventory
{
    public partial class Inventory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["SHOP_ID"] != null)
                {
                    BindInventoryData(Convert.ToInt32(Session["SHOP_ID"]));
                }
                else
                {
                    Response.Redirect("../Products/ShopLogin.aspx");
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
                    string query = "SELECT Name, ProductID, Stocks, Price, Category, ImageURL FROM PRODUCTSINV WHERE SHOP_ID = :UserId";
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

        protected void DeleteProduct(object sender, EventArgs e)
        {
            try
            {
                Button deleteButton = (Button)sender;
                string productId = deleteButton.CommandArgument;

                int shopId = Convert.ToInt32(Session["SHOP_ID"]);

                string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
                string query = "DELETE FROM PRODUCTSINV WHERE ProductID = :ProductId AND SHOP_ID = :ShopId";

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
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Product deleted successfully.');", true);
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
                        whereClause += "ProductID = :ProductId";
                    }
                    else
                    {
                        whereClause += "LOWER(Name) LIKE '%' || :SearchQuery || '%' OR LOWER(Category) LIKE '%' || :SearchQuery || '%'";
                    }


                    string query = "SELECT Name, ProductID, Stocks, Price, Category, ImageURL FROM PRODUCTSINV " + whereClause;

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

        protected void LogoutBtnClick(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("../Products/ShopLogin.aspx");
        }
    }
}
