using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void Iframcode()
    {
        aa.Src = "http://localhost:19213/Popup";      
    }
    protected void a_Click(object sender, EventArgs e)
    {
        Iframcode();
        ScriptManager.RegisterStartupScript(this, GetType(), "ProductCompany", "showPopup();", true);
    }
}