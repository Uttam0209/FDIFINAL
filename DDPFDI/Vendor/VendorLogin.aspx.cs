using System;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Text;
using BusinessLayer;
using System.Web.UI.WebControls;
using Encryption;
using System.Web;

public partial class Vendor_VendorLogin : System.Web.UI.Page
{
    #region "Variables"
    Logic LO = new Logic();
    DataUtility Co = new DataUtility();
    Cryptography objEnc = new Cryptography();
    HybridDictionary hyLogin = new HybridDictionary();
    string _msg = string.Empty;
    string Defaultpage = string.Empty;
    string _sysMsg = string.Empty;
    string notvalidate = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        // string s = objEnc.EncryptData("Data Source=103.73.189.114;Initial Catalog=ddp_cmsV1;User ID=sa;Password=mXy<wxh3:Mh@U");
        // string a = objEnc.DecryptData("aL88ocdv5/Ixdr/+Dtk9vguy4XrszcqvQRVrxD/UURYbfxfZoNQ/slH40ZNMn79SQzU69Y+PJE2GhK6b37iKbSavaXlq/Ptl6qjBEDY0BJF2LUCW1YbxmEnDlPlryODw");
    }
    #region "Login Code"
    public static bool IsValidEmailId(string InputEmail)
    {
        string pattern = @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}$";
        Match match = Regex.Match(InputEmail.Trim(), pattern, RegexOptions.IgnoreCase);
        if (match.Success)
            return true;
        else
            return false;
    }
    protected void ValidateCaptcha(object sender, ServerValidateEventArgs e)
    {
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValidEmailId(txtUserName.Text) == true)
            {
                if (Session["ChkCaptcha"].ToString().ToLower() != txtCaptcha.Text.ToLower())
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Invalid Captcha')", true);
                }
                else
                {
                    hyLogin["UserName"] = Co.RSQandSQLInjection(txtUserName.Text.Trim() + "'", "hard" + "'");
                    hyLogin["Password"] = objEnc.EncryptData(txtPwd.Text.Trim());
                    string _EmpId = LO.VerifyVendorEmployee(hyLogin, out _msg, out Defaultpage);
                    if (_EmpId != "0" && _EmpId != "1" && _msg != "")
                    {
                        Session["Type"] = objEnc.EncryptData(_msg);
                        Session["User"] = objEnc.EncryptData(txtUserName.Text);
                        Session["CompanyRefNo"] = objEnc.EncryptData(_EmpId);
                        Response.RedirectToRoute(Defaultpage);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Invalid Login. Either user id or password or both are incorrect.');", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Invalid email format.');", true);
            }
        }
        catch (Exception ex)
        {
            Response.RedirectToRoute("login");
        }
    }
    protected void btnCaptchaNew_Click(object sender, EventArgs e)
    {
        Image2.ImageUrl = "~/CaptchaCall.aspx?random=" + DateTime.Now.Ticks.ToString();
    }
    #endregion
    #region Forgot Password"
    protected void lblforgotpass_Click(object sender, EventArgs e)
    {
        txtforgotemailid.Text = "";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "changePass", "showPopup();", true);
    }
    protected void btnsendmail_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtforgotemailid.Text != "")
            {
                if (IsValidEmailId(txtforgotemailid.Text) == true)
                {
                    DataTable DtSendMailForgotpassword = LO.RetriveForgotPasswordEmail(Co.RSQandSQLInjection(txtforgotemailid.Text, "soft"), "ForgotVenPassword");
                    if (DtSendMailForgotpassword.Rows.Count > 0)
                    {
                        string EmpCode = DtSendMailForgotpassword.Rows[0]["VendorRefNo"].ToString();
                        SendEmailCode(EmpCode);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Email send to your registerd email id successfully.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Email id not registerd with us.');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Invalid email format.');", true);
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    #endregion
    #region Send Mail
    public void SendEmailCode(string empid)
    {
        try
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/VendorForgotPassword.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", txtforgotemailid.Text);
            body = body.Replace("{refno}", HttpUtility.UrlEncode(objEnc.EncryptData(empid)));
            body = body.Replace("{mcurid}", Resturl(56));
            SendMail s;
            s = new SendMail();
            s.CreateMail("aeroindia-ddp@gov.in", txtforgotemailid.Text, "Forgot Password Recover Mail", body);
            s.sendMail();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }

    }
    #endregion
    #region ReturnUrl Long"
    public string Resturl(int length)
    {
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        StringBuilder res = new StringBuilder();
        Random rnd = new Random();
        while (0 < length--)
        {
            res.Append(valid[rnd.Next(valid.Length)]);
        }
        return res.ToString();
    }
    #endregion
}