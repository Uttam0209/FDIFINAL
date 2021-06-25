using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Encryption;

public partial class Vendor_confirmEmailupdation : System.Web.UI.Page
{
    public string emailID;
    Logic Lo = new Logic();
    Cryptography Enc = new Cryptography();
    DataUtility Co = new DataUtility();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["mcref"] != "" && Request.QueryString["mcurid"] != "")
        {
            ViewState["Refno"] = Enc.DecryptData(Request.QueryString["mcref"].ToString());
            //ViewStateme["IDNO"] = Enc.DecryptData(Request.QueryString["mcurid"].ToString());
            emailID = Request.QueryString["emi"];
        }
        else
        {
            Response.Redirect("Login");
        }
    }

    //string pass = "1234@";
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (txtpassword.Text != "" && txtpassword.Text != "")
        {
            if (txtpassword.Text == txtpassword.Text)
            {
                string sType = "UpdateNodoffiEmail";
                string Updatepass = Lo.UpdateLoginPassword(emailID, Enc.EncryptData(txtpassword.Text), ViewState["Refno"].ToString(), sType, "", "");
                if (Updatepass == "true")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Email Change successfully.Please login with new New EmailID. We will redirected to you login page');window.location ='VendorLogin';", true);
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