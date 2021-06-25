using System;
using System.Web.UI;
using BusinessLayer;
using Encryption;
using System.Web.Helpers;
using System.Data;


public partial class Vendor_CreateVendorPassword : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Cryptography Enc = new Cryptography();
    DataUtility Co = new DataUtility();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["mcref"] != null && Request.QueryString["curid"] != null)
        {
            ViewState["Refno"] = Enc.DecryptData(Request.QueryString["mcref"].ToString());
            DataTable dtCreate = Lo.RetriveForgotPasswordEmail(ViewState["Refno"].ToString(), "LinkexpVen");
            DateTime dtNow = Convert.ToDateTime(dtCreate.Rows[0]["PasswordChangeValidTime"].ToString());
            DateTime dtCExp = DateTime.Now;
            DateTime dtexp = dtNow.AddHours(2);
            if (dtCExp > dtexp)
            {
                lblmsg.Text = "Link has been expired.Please contact our helpdesk team";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
            }
        }
        else
        {
            lblmsg.Text = "Invalid Request";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
            Response.RedirectToRoute("VendorLogin");
        }
    }
    protected void btncreatepassword_Click(object sender, EventArgs e)
    {
        if (txtpassword.Text != "" && txttnewpass.Text != "")
        {
            if (txtpassword.Text == txttnewpass.Text)
            {
                if (txtpassword.Text.Length >= 8)
                {
                    GenerateHash HashAndSalt = new GenerateHash();
                    string GetSalt = HashAndSalt.CreateSalt(10);
                    string hashString = HashAndSalt.GenarateHash(txtpassword.Text, GetSalt);
                    string Updatepass = Lo.UpdateLoginPassword(hashString, "", ViewState["Refno"].ToString(), "LoginNewVenPass", txtpassword.Text, GetSalt);
                    if (Updatepass == "true")
                    {
                        ViewState["Refno"] = null;
                        txtpassword.Text = "";
                        txttnewpass.Text = "";
                        lblogin.Visible = true;
                        lblmsg.Text = "Password created successfully.Please click on back to login for login into your account.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
                    }
                    else
                    {
                        lblmsg.Text = "Password not created. Some error occured.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
                    }
                }
                else
                {
                    lblmsg.Text = "Minimum Length is (8) character.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
                }
            }
            else
            {
                lblmsg.Text = "Password mismatch.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
            }
        }
        else
        {
            lblmsg.Text = "Enter password.";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
        }
    }
    class GenerateHash
    {
        public string CreateSalt(int SaltSize)
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            byte[] Salt = new byte[SaltSize];
            rng.GetBytes(Salt);
            return Convert.ToBase64String(Salt);
        }
        public string GenarateHash(string UserPassword, string salt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(UserPassword + salt);
            byte[] PasswordHash = new System.Security.Cryptography.SHA256Managed().ComputeHash(bytes);
            return Convert.ToBase64String(PasswordHash);
        }
    }


}