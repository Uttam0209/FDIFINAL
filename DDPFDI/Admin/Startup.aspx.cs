using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Encryption;

public partial class Admin_Startup : System.Web.UI.Page
{
    Cryptography objEnc = new Cryptography();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            string id = Request.QueryString["id"].ToString().Replace(" ", "+");
            lblPageName.Text = objEnc.DecryptData(id);
        }
    }
}