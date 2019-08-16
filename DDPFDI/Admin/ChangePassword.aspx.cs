using System;
using System.Web.UI;
using Encryption;
using BusinessLayer;
using System.Web.Helpers;

public partial class Admin_ChangePassword : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Cryptography ObjEnc = new Cryptography();
    Cryptography objEnc = new Cryptography();
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Request.QueryString["id"].ToString() != null)
        // {
        //string id = Request.QueryString["id"].ToString().Replace(" ", "+");
        //lblPageName.Text = objEnc.DecryptData(id);
        //   }
        //   else
        //   {
        // ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert",
        //        "alert('Request you generate is false we will redirect you to login page');window.location='Login'", true);
        //   }
    }
    protected void btnsub_Click(object sender, EventArgs e)
    {
        if (txtoldpass.Text != "" && txtnewpass.Text != "" && txtreppass.Text != "")
        {
            if (txtnewpass.Text == txtreppass.Text)
            {
                if (txtreppass.Text.Length >= 8)
                {
                    GenerateHash HashAndSalt = new GenerateHash();
                    string GetSalt = HashAndSalt.CreateSalt(10);
                    string hashString = HashAndSalt.GenarateHash(txtnewpass.Text, GetSalt);
                    string UpdatePassword = Lo.UpdateLoginPassword(hashString, txtoldpass.Text, ObjEnc.DecryptData(Session["User"].ToString()), "LoginOld", txtnewpass.Text, GetSalt);
                    if (UpdatePassword == "true")
                    {
                        divmsg.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Password change successfully.You just changed your password.Please login with your new password we will redirect you to login page');window.location ='Login';", true);
                    }
                    else
                    {
                        divmsg.InnerHtml = "Password not change";
                        divmsg.Attributes.Add("Class", "alert alert-danger");
                        divmsg.Visible = true;
                    }
                }
                else
                {
                    divmsg.InnerHtml = "Minimum Length is (8) charactor";
                    divmsg.Attributes.Add("Class", "alert alert-warning");
                    divmsg.Visible = true;
                }
            }
            else
            {
                divmsg.InnerHtml = "Password not match.";
                divmsg.Attributes.Add("Class", "alert alert-warning");
                divmsg.Visible = true;

            }
        }
        else
        {
            divmsg.InnerHtml = "All field fill mandatory.";
            divmsg.Attributes.Add("Class", "alert alert-warning");
            divmsg.Visible = true;

        }
    }
    protected void btncan_Click(object sender, EventArgs e)
    {
        cleartext();
    }
    protected void cleartext()
    {
        txtnewpass.Text = "";
        txtoldpass.Text = "";
        txtreppass.Text = "";
        divmsg.InnerHtml = "";
        divmsg.Visible = false;
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