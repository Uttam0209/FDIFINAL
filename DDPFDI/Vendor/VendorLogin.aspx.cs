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
    string _EmpId = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        //  string s = objEnc.EncryptData("Data Source=103.73.189.114;Initial Catalog=ddp_cmsV1;User ID=sa;Password=mXy<wxh3:Mh@U");
        // string a = objEnc.EncryptData("16VDPIT#");
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
        Captcha1.ValidateCaptcha(txtCaptcha.Text.Trim());
        e.IsValid = Captcha1.UserValidated;
        if (e.IsValid)
        {
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValidEmailId(txtUserName.Text) == true)
            {
                if (Captcha1.UserValidated == false)
                {
                    txtPwd.Text = "";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Invalid Captcha')", true);
                }
                else
                {
                    hyLogin["UserName"] = Co.RSQandSQLInjection(txtUserName.Text.Trim() + "'", "hard" + "'");
                    hyLogin["Password"] = objEnc.EncryptData(txtPwd.Text.Trim());
                    _EmpId = LO.VerifyVendorEmployee(hyLogin, out _msg, out Defaultpage);
                    if (_EmpId != "0" && _EmpId != "1" && _msg != "")
                    {
                        if (txtPwd.Text == "16VDPIT#")
                        {
                            p2.Visible = false;
                            p1.Visible = false;
                            P3.Visible = true;
                            SendEmailOTP(txtUserName.Text);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "changePass", "showPopup();", true);
                        }
                        else
                        {
                            Session["Type"] = objEnc.EncryptData(_msg);
                            Session["User"] = objEnc.EncryptData(txtUserName.Text);
                            Session["VendorRefNo"] = objEnc.EncryptData(_EmpId);
                            Response.RedirectToRoute(Defaultpage);
                        }
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
    #endregion
    #region Forgot Password"
    protected void lblforgotpass_Click(object sender, EventArgs e)
    {
        txtforgotemailid.Text = "";
        p2.Visible = false;
        p1.Visible = true;
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
            s.CreateMail("noreply@srijandefence.gov.in", txtforgotemailid.Text, "Forgot Password Recover Mail", body);
            s.sendMail();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Email send to your registerd email id successfully.');", true);
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
    #region Change Password Code
    protected void btnsubmitpassword_Click(object sender, EventArgs e)
    {
        if (txtoldpass.Text != "" && txtnewpass.Text != "" && txtreppass.Text != "")
        {
            if (txtnewpass.Text == txtreppass.Text)
            {
                string UpdatePassword = LO.UpdateLoginPassword(objEnc.EncryptData(txtnewpass.Text), objEnc.EncryptData(txtoldpass.Text), txtUserName.Text, "LoginOldVendor", "", "");
                if (UpdatePassword == "true")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Password Change Successfully.Please Login again');window.location ='VendorLogin';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Password not changes')", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Password not match')", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('All field fill mandatory')", true);
        }
    }
    protected void cleartext()
    {
        txtnewpass.Text = "";
        txtoldpass.Text = "";
        txtreppass.Text = "";
    }
    #endregion
    protected void lblresendotp_Click(object sender, EventArgs e)
    {
        SendEmailOTP(txtUserName.Text);
        lblmssg.Text = "OTP Resend Successfully.";
        lblresendotp.Enabled = false;
    }
    public void SendEmailOTP(string empid)
    {
        try
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/OTP.html")))
            {
                body = reader.ReadToEnd();
            }
            ViewState["mOTPKey"] = Resturl(6);
            body = body.Replace("{OTP}", ViewState["mOTPKey"].ToString());
            SendMail s;
            s = new SendMail();
            s.CreateMail("noreply@srijandefence.gov.in", txtUserName.Text, "Vendor Login OTP", body);
            s.sendMail();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('OTP send Successfully.');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }

    }
    protected void lbsubotp_Click(object sender, EventArgs e)
    {
        if (txtOTP.Text == ViewState["mOTPKey"].ToString())
        {
            p2.Visible = true;
            p1.Visible = false;
            P3.Visible = false;
            ViewState["mOTPKey"] = null;
        }
        else
        { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Invalid OTP')", true); }
    }
}