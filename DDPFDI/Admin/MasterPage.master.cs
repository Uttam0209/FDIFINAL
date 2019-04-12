using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Encryption;
using BusinessLayer;

public partial class Admin_MasterPage : System.Web.UI.MasterPage
{
    Logic Lo = new Logic();
    Cryptography ObjEnc = new Cryptography();
    string UType = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] != null)
        {
            UType = ObjEnc.DecryptData(Session["Type"].ToString());
            lblusername.Text = ObjEnc.DecryptData(Session["User"].ToString());
            if (UType == "Admin")
            {
                liadmin.Visible = true;
                liuser.Visible = false;
            }
            else
            {
                liadmin.Visible = false;
                liuser.Visible = true;
            }
        }
        else
        {
            Response.RedirectToRoute("Login");
        }
    }
    protected void lbllogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.RedirectToRoute("Login");
    }

}
