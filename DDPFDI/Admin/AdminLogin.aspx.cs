using System;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Text;
using BusinessLayer;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Encryption;

public partial class Admin_AdminLogin : System.Web.UI.Page
{
    #region "Variables"
    Logic LO = new Logic();
    DataUtility Co = new DataUtility();
    Cryptography objEnc = new Cryptography();
    HybridDictionary hyLogin = new HybridDictionary();
    string _msg = string.Empty;
    string _sysMsg = string.Empty;
    string notvalidate = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
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
                    string _EmpId = LO.VerifyEmployee(hyLogin, out _msg);
                    if (_EmpId != "0" && _EmpId != "1" && _msg != "0")
                    {
                        Session["User"] = objEnc.EncryptData(txtUserName.Text);
                        Response.RedirectToRoute("Detail-Company");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Invalid Login.');", true);
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
            string message = ex.Message;
            Response.Redirect("Error.aspx?string=" + message);
        }
    }
    protected void btnCaptchaNew_Click(object sender, EventArgs e)
    {
        Image2.ImageUrl = "~/Admin/CaptchaCall.aspx?random=" + DateTime.Now.Ticks.ToString();
    }
    #endregion
}