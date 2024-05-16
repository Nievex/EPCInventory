using System;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Web.UI.WebControls;

namespace EPC_Inventory.Seller
{
    public partial class Orders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["SHOP_ID"] != null)
                {
                    int shopId = Convert.ToInt32(Session["SHOP_ID"]);
                    LoadOrders(shopId, null);
                }
                else
                {
                    Response.Redirect("../ShopLogin.aspx");
                }
            }
        }

        private void LoadOrders(int shopId, string status)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            string query = "SELECT * FROM ORDERS WHERE SHOP_ID = :ShopId";
            if (!string.IsNullOrEmpty(status))
            {
                query += " AND ORDER_STATUS = :OrderStatus";
            }

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add("ShopId", OracleDbType.Int32).Value = shopId;
                    if (!string.IsNullOrEmpty(status))
                    {
                        command.Parameters.Add("OrderStatus", OracleDbType.Varchar2).Value = status;
                    }

                    connection.Open();
                    OracleDataReader reader = command.ExecuteReader();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    OrderRepeater.DataSource = dataTable;
                    OrderRepeater.DataBind();
                }
            }
        }

        protected void OrderRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "UpdateStatus")
            {
                int orderId = Convert.ToInt32(e.CommandArgument);
                DropDownList statusDropdown = (DropDownList)e.Item.FindControl("StatusDropdown");
                string newStatus = statusDropdown.SelectedValue;

                UpdateOrderStatus(orderId, newStatus);
            }
        }

        private void UpdateOrderStatus(int orderId, string newStatus)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            string query = "UPDATE ORDERS SET ORDER_STATUS = :OrderStatus WHERE ORDER_ID = :OrderId";

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add("OrderStatus", OracleDbType.Varchar2).Value = newStatus;
                    command.Parameters.Add("OrderId", OracleDbType.Int32).Value = orderId;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            int shopId = Convert.ToInt32(Session["SHOP_ID"]);
            LoadOrders(shopId, null);
        }

        protected void StatusAnchor_Click(object sender, EventArgs e)
        {
            LinkButton statusAnchor = (LinkButton)sender;
            string status = statusAnchor.Text;
            int shopId = Convert.ToInt32(Session["SHOP_ID"]);
            LoadOrders(shopId, status);
        }

        protected void OrderRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView row = (DataRowView)e.Item.DataItem;
                Image productImage = (Image)e.Item.FindControl("ProductImagePreview");

                if (row["IMAGEURL"] != DBNull.Value)
                {
                    byte[] imageData = (byte[])row["IMAGEURL"];
                    string base64Image = Convert.ToBase64String(imageData);
                    productImage.ImageUrl = "data:image/jpeg;base64," + base64Image;
                }
            }
        }
    }
}
