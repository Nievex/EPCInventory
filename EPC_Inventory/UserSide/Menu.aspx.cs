using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;

public partial class Menu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CUSTOMER_ID"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        if (!IsPostBack)
        {
            BindProductData();
        }
    }

    private void BindProductData()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        string query = "SELECT * FROM PRODUCTS";

        using (OracleConnection connection = new OracleConnection(connectionString))
        {
            connection.Open();
            using (OracleCommand command = new OracleCommand(query, connection))
            {
                using (OracleDataReader reader = command.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    ProductRepeater.DataSource = dt;
                    ProductRepeater.DataBind();
                }
            }
        }
    }

    protected void BuyButton_Command(object sender, CommandEventArgs e)
    {
        int productId = Convert.ToInt32(e.CommandArgument);
        int customerId = Convert.ToInt32(Session["CUSTOMER_ID"]);
        string customerName = Session["CUSTOMER_NAME"].ToString();
        string customerAddress = Session["CUSTOMER_ADDRESS"].ToString();
        string customerContactNumber = Session["CONTACT_NUMBER"].ToString();
        string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        RepeaterItem item = (sender as Button).NamingContainer as RepeaterItem;
        TextBox quantityTextBox = item.FindControl("QuantityTextBox") as TextBox;
        int quantity = int.Parse(quantityTextBox.Text);

        DropDownList paymentMethodDropDown = item.FindControl("PaymentMethodDropDown") as DropDownList;
        string paymentMethod = paymentMethodDropDown.SelectedValue;

        using (OracleConnection connection = new OracleConnection(connectionString))
        {
            connection.Open();

            // Get product details
            string productQuery = "SELECT * FROM PRODUCTS WHERE PRODUCT_ID = :ProductId";
            OracleCommand productCommand = new OracleCommand(productQuery, connection);
            productCommand.Parameters.Add("ProductId", OracleDbType.Decimal).Value = productId;
            OracleDataReader productReader = productCommand.ExecuteReader();

            if (productReader.Read())
            {
                string productName = productReader["NAME"].ToString();
                decimal price = Convert.ToDecimal(productReader["PRICE"]);
                byte[] imageUrl = (byte[])productReader["IMAGEURL"];
                int shopId = Convert.ToInt32(productReader["SHOP_ID"]);
                decimal subtotal = price * quantity;

                // Insert order
                string insertQuery = @"INSERT INTO ORDERS (ORDER_ID, SHOP_ID, PRODUCT_ID, CUSTOMER_ID, IMAGEURL, PRODUCT_NAME, QTY, SUBTOTAL, PAYMENT_METHOD, CUSTOMER_NAME, CUSTOMER_ADDRESS, CUSTOMER_CONTACT_NUMBER, ORDER_DATE, ORDER_STATUS)
                        VALUES (ORDERS_SEQ.NEXTVAL, :ShopId, :ProductId, :CustomerId, :ImageUrl, :ProductName, :Qty, :Subtotal, :PaymentMethod, :CustomerName, :CustomerAddress, :ContactNumber, :OrderDate, 'Pending')";
                OracleCommand insertCommand = new OracleCommand(insertQuery, connection);
                insertCommand.Parameters.Add("ShopId", OracleDbType.Decimal).Value = shopId;
                insertCommand.Parameters.Add("ProductId", OracleDbType.Decimal).Value = productId;
                insertCommand.Parameters.Add("CustomerId", OracleDbType.Decimal).Value = customerId;
                insertCommand.Parameters.Add("ImageUrl", OracleDbType.Blob).Value = imageUrl;
                insertCommand.Parameters.Add("ProductName", OracleDbType.Varchar2).Value = productName;
                insertCommand.Parameters.Add("Qty", OracleDbType.Decimal).Value = quantity;
                insertCommand.Parameters.Add("Subtotal", OracleDbType.Decimal).Value = subtotal;
                insertCommand.Parameters.Add("PaymentMethod", OracleDbType.Varchar2).Value = paymentMethod;
                insertCommand.Parameters.Add("CustomerName", OracleDbType.Varchar2).Value = customerName;
                insertCommand.Parameters.Add("CustomerAddress", OracleDbType.Varchar2).Value = customerAddress;
                insertCommand.Parameters.Add("ContactNumber", OracleDbType.Varchar2).Value = customerContactNumber;
                insertCommand.Parameters.Add("OrderDate", OracleDbType.Date).Value = DateTime.Now;

                int rowsAffected = insertCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Response.Write("<script>alert('Order placed successfully!');</script>");
                }
                else
                {
                    Response.Write("<script>alert('Order placement failed.');</script>");
                }
            }
        }
    }
}

