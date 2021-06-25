using System;
using System.Web.UI;
using Encryption;
using BusinessLayer;
using System.Web.Helpers;

public partial class User_Change_password : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Cryptography objEnc = new Cryptography();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Request.QueryString["User"] != "")
        {
            lblPageName.Text = objEnc.DecryptData(Request.QueryString["User"].ToString());
        }
      
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        if (txtnewpass.Text != "" && pwd.Text != "")
        {
            if (txtnewpass.Text == pwd.Text)
            {
                if (txtnewpass.Text.Length >= 8)
                {
                   
                    GenerateHash HashAndSalt = new GenerateHash();
                    string GetSalt = HashAndSalt.CreateSalt(10);
                    string hashString = HashAndSalt.GenarateHash(txtnewpass.Text, GetSalt);
                    string UpdatePassword = Lo.UpdateLoginPassword(hashString, "", lblPageName.Text, "LoginPasspolicy", txtnewpass.Text, GetSalt);
                    if (UpdatePassword == "true")
                    {
                        lblPageName.Text = "";
                        txtnewpass.Text = "";
                        pwd.Text = "";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Password created successfully.Please login with new password.We will redirected to you Login page');window.location ='Login';", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Password is not created. Some error occured.')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Minimum Length is (8) character')", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Password mismatch')", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Enter password')", true);
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