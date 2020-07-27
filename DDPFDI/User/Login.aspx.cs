using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void log_Click(object sender, EventArgs e)
    {
        if (txtpass.Text.Trim() == "Dpit@#_$@2020")
        {
            Session["U_User"] = txtpass.Text.Trim();
            Response.Redirect("ProductList");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Invalid detail.');", true);
        }
    }
}