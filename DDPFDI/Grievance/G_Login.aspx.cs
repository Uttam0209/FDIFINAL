using System;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Encryption;
using System.Net.Security;

public partial class Grievance_G_Login : System.Web.UI.Page
{
    #region "Variables"
    Logic LO = new Logic();
    DataUtility Co = new DataUtility();
    Cryptography objEnc = new Cryptography();
    HybridDictionary hyLogin = new HybridDictionary();
    HybridDictionary HySaveGRegis = new HybridDictionary();
    UserIPAnalytics userip = new UserIPAnalytics();
    string _msg = string.Empty;
    string Defaultpage = string.Empty;
    string _sysMsg = string.Empty;
    string _EmpId = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillCapctha();
        }
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
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValidEmailId(Server.HtmlEncode(txtUserName.Text)) == true)
            {
                if (txtCaptcha.Text != Session["captcha"].ToString())
                {
                    txtPwd.Text = "";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Invalid Captcha')", true);
                }
                else
                {
                    userip.GetAnalytics();
                    hyLogin["UserName"] = Co.RSQandSQLInjection(Server.HtmlEncode(txtUserName.Text.Trim()) + "'", "hard" + "'");
                    hyLogin["Password"] = Co.RSQandSQLInjection(Server.HtmlEncode(txtPwd.Text.Trim()), "hard");
                    hyLogin["Brow"] = System.Web.HttpContext.Current.Request.Browser.Type.ToString();
                    hyLogin["ip"] = System.Web.HttpContext.Current.Request.UserHostAddress;
                    _EmpId = LO.VerifyHelpdeskEmployee(hyLogin, out _msg, out Defaultpage);
                    if (_EmpId != "0" && _msg != "")
                    {
                        Session["GType"] = objEnc.EncryptData(_msg);
                        Session["GUser"] = objEnc.EncryptData(Server.HtmlEncode(txtUserName.Text));
                        Session["Gid"] = objEnc.EncryptData(_EmpId);
                        string guid = Guid.NewGuid().ToString();
                        Session["SFToken"] = guid;
                        Response.Cookies.Add(new HttpCookie("SFToken", guid));
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
            Response.RedirectToRoute("GOfficiallogin");
        }
    }
    #endregion
    #region Forgot Password"
    protected void lblforgotpass_Click(object sender, EventArgs e)
    {
        txtforgotemailid.Text = "";
        p1.Visible = true;
        divregistration.Visible = false;
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
                    DataTable DtSendMailForgotpassword = LO.RetriveForgotPasswordEmail(Co.RSQandSQLInjection(txtforgotemailid.Text, "soft"), "ForgotGriPassword");
                    if (DtSendMailForgotpassword.Rows.Count > 0)
                    {
                        string EmpCode = DtSendMailForgotpassword.Rows[0]["Password"].ToString();
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
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/HelpdeskForgotpassword.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", txtforgotemailid.Text);
            body = body.Replace("{refno}", empid);
            SendMail s;
            s = new SendMail();
            s.CreateMail("noreply-srijandefence@gov.in", txtforgotemailid.Text, "Forgot Password Recover Mail", body);
            s.sendMail();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Email send to your registerd email id successfully.');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }

    }
    public void SendEmailCodeRegis(string empid, string email)
    {
        try
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/HelpDeskRegis.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", email);
            body = body.Replace("{refno}", empid);
            SendMail s;
            s = new SendMail();
            s.CreateMail("noreply-srijandefence@gov.in", txtnodelemail.Text, "Registration Email", body);
            s.sendMail();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }

    }
    #endregion        
    #region Captcha   
    void FillCapctha()
    {
        try
        {
            Random random = new Random();
            string combination = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder captcha = new StringBuilder();
            for (int i = 0; i < 6; i++)
                captcha.Append(combination[random.Next(combination.Length)]);
            Session["captcha"] = captcha.ToString();
            imgCaptcha.ImageUrl = "../GenerateCaptcha.aspx?" + DateTime.Now.Ticks.ToString();
        }
        catch
        {

            throw;
        }
    }
    protected void btnCaptchaNew_Click(object sender, EventArgs e)
    {
        FillCapctha();
    }
    #endregion    
    #region Registration Code
    protected void lbgregis_Click(object sender, EventArgs e)
    {
        p1.Visible = false;
        divregistration.Visible = true;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "changePass", "showPopup();", true);
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddltype.SelectedIndex != -1 && txtnodelemail.Text != "" && txtmobileNo.Text != "" && txtname.Text != "")
            {
                if (IsValidEmailId(txtnodelemail.Text) != true)
                { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Invalid email format.');", true); }
                else
                {
                    HySaveGRegis["Type"] = Co.RSQandSQLInjection(ddltype.SelectedItem.Value, "soft");
                    HySaveGRegis["Name"] = Co.RSQandSQLInjection(txtname.Text.Trim(), "soft");
                    HySaveGRegis["UserName"] = Co.RSQandSQLInjection(txtnodelemail.Text.Trim(), "soft");
                    HySaveGRegis["MobileNo"] = Co.RSQandSQLInjection(txtmobileNo.Text.Trim(), "soft");
                    string str = LO.SaveGUser(HySaveGRegis, out _msg, out _sysMsg);
                    if (str != "" && _sysMsg == "Save")
                    {
                        SendEmailCodeRegis(str.ToString(), txtnodelemail.Text);
                        ddltype.SelectedIndex = -1;
                        txtname.Text = "";
                        txtnodelemail.Text = "";
                        txtmobileNo.Text = "";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Thankyou for registration with us  your password releted email has been sent " +
                            "to your registerd email id.Please login with that email id and password.'); window.location='GOfficialLogin';", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Invalid detail registrations failed.');", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('All field fill mandatory.');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(" + ex.Message + ");", true);
        }
    }
    #endregion
}