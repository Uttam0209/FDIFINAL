﻿using System;
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

using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Security.AccessControl;


public partial class _Default : System.Web.UI.Page
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
        string s = objEnc.EncryptData(@"Data Source=DE73P-DBAERO-00\SQLEXPRESS;Initial Catalog=Gip_AeroIndia2018_new;User ID=sa;Password=Adm@23SqlServ1");

        string d = objEnc.DecryptData("aL88ocdv5/LvxKi0O2Gs6kF35uJ5Iz4xWbJBsJ8R+marLTVA2W7Pt0PDHgFG4Wx3HJgCG5QjEr1C1Q7WGTiNwa2AB1N5OvU+45sa48G+2HZnZapUUB4NgatRxGyMc5ZecSf34VN2rLqINQzCMknoOQ==");
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
                    string _EmpId = LO.VerifyEmployee(hyLogin, out _msg, out Defaultpage);
                    if (_EmpId != "0" && _EmpId != "1" && _msg != "")
                    {
                        Session["Type"] = objEnc.EncryptData(_msg);
                        Session["User"] = objEnc.EncryptData(txtUserName.Text);
                        Session["CompanyRefNo"] = _EmpId;
                        Response.RedirectToRoute(Defaultpage);
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
        Image2.ImageUrl = "~/CaptchaCall.aspx?random=" + DateTime.Now.Ticks.ToString();
    }
    #endregion
}