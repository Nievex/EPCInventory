using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EPC_Inventory
{
    public partial class SellerDashboard : System.Web.UI.MasterPage
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
        protected void LogoutBtnClick(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("../ShopLogin.aspx");
        }
    }
}