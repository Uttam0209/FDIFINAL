using System;
using System.Web.UI;
using BusinessLayer;
using Encryption;
using System.Web.Helpers;

public partial class Admin_CreatePasswordCompany : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Cryptography Enc = new Cryptography();
    DataUtility Co = new DataUtility();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["mcref"] != "" && Request.QueryString["mcurid"] != "")
        //   if (Request.QueryString["mcref"] != "")
        {
            ViewState["Refno"] = Enc.DecryptData(Request.QueryString["mcref"].ToString()); //Request.QueryString["mcref"].ToString();
        }
        else
        {
            Response.Redirect("Login");
        }
    }
    protected void btnchangepass_Click(object sender, EventArgs e)
    {
        if (txtpassword.Text != "" && txttnewpass.Text != "")
        {
            if (txtpassword.Text == txttnewpass.Text)
            {
                if (txtpassword.Text.Length >= 8)
                {
                    string sType = "LoginNew";
                    GenerateHash HashAndSalt = new GenerateHash();
                    string GetSalt = HashAndSalt.CreateSalt(10);
                    string hashString = HashAndSalt.GenarateHash(txtpassword.Text, GetSalt);
                    string Updatepass = Lo.UpdateLoginPassword(hashString, "", ViewState["Refno"].ToString(), sType, txtpassword.Text, GetSalt);
                    if (Updatepass == "true")
                    {
                        ViewState["Refno"] = null;
                        txtpassword.Text = "";
                        txttnewpass.Text = "";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Password created successfully.Please login with new password.We will redirected to you login page');window.location ='Login';", true);
                       
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Password not created. Some error occured.')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Minimum Length is (8) charactor')", true);
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