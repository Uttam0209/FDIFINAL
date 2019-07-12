using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Encryption;
public partial class Vendor_CreateVendorPassword : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Cryptography Enc = new Cryptography();
    DataUtility Co = new DataUtility();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["mcref"] != "" && Request.QueryString["mcurid"] != "")
        {
            ViewState["Refno"] = Enc.DecryptData(Request.QueryString["mcref"].ToString());
        }
        else
        {
            Response.Redirect("Login");
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (txtpassword.Text != "" && txtrepeatpassword.Text != "")
        {
            if (txtpassword.Text == txtrepeatpassword.Text)
            {
                string sType = "LoginNewVenPass";
                string Updatepass = Lo.UpdateLoginPassword(Enc.EncryptData(txtpassword.Text), "", ViewState["Refno"].ToString(), sType);
                if (Updatepass == "true")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Password created successfully.Please login with new password.We will redirected to you login page');window.location ='VendorLogin';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Password not created. Some error occured.')", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Password mismatch')", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Enter password')", true);
        }
    }
}