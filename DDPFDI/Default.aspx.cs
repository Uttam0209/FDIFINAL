﻿using System;
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
using System.Net;


public partial class _Default : System.Web.UI.Page
{
    #region "Variables"
    Logic LO = new Logic();
    DataUtility Co = new DataUtility();
    Cryptography objEnc = new Cryptography();
    HybridDictionary hyLogin = new HybridDictionary();
    HybridDictionary hyLog = new HybridDictionary();
    HybridDictionary HyLoginStatus = new HybridDictionary();
    string _msg = string.Empty;
    string Defaultpage = string.Empty;
    string _sysMsg = string.Empty;
    string notvalidate = string.Empty;
    string GetIp = "";
    string Getsystem = "";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
         //string a = objEnc.EncryptData("Data Source=192.168.0.48;Initial Catalog=ddp_fdifinal;User ID=sa;Password=dpsql@123");
       // string b = objEnc.DecryptData("aL88ocdv5/Ixdr/+Dtk9vguy4XrszcqvQRVrxD/UURYbfxfZoNQ/slH40ZNMn79SQzU69Y+PJE2GhK6b37iKbSavaXlq/Ptl6qjBEDY0BJF2LUCW1YbxmEnDlPlryODw");
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
                    string _VerEmail = LO.VerifyEmail(hyLogin, out _msg, out _sysMsg);
                    if (_VerEmail != "0" && _VerEmail != "1" && _msg != "0" && _sysMsg != "")
                    {
                        string HashedPassword = _msg.ToString();
                        string Salt = _sysMsg.ToString();
                        byte[] passwordAndSalt = System.Text.Encoding.UTF8.GetBytes(txtPwd.Text + Salt);
                        byte[] hashPass = new System.Security.Cryptography.SHA256Managed().ComputeHash(passwordAndSalt);
                        string hashCode = Convert.ToBase64String(hashPass);
                        if (hashCode == HashedPassword)
                        {
                            hyLogin["Password"] = _msg.ToString();
                            string _EmpId = LO.VerifyEmployee(hyLogin, out _msg, out Defaultpage);
                            if (_EmpId != "0" && _EmpId != "1" && _msg != "")
                            {
                                Session["Type"] = objEnc.EncryptData(_msg);
                                Session["User"] = objEnc.EncryptData(txtUserName.Text);
                                Session["CompanyRefNo"] = _EmpId;
                                Logoutstatus();
                                UserLoginLog();
                                Response.RedirectToRoute(Defaultpage);
                            }
                            else
                            {
                                txtPwd.Text = "";
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "ErrorMssgPopup('Invalid Login. Either user id or password or both are incorrect.')", true);
                            }
                        }
                        else
                        {
                            txtPwd.Text = "";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "ErrorMssgPopup('Invalid Login. Either user id or password or both are incorrect.')", true);
                        }
                    }
                    else
                    {
                        txtPwd.Text = "";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "ErrorMssgPopup('Invalid User.Email id not registerd with us.')", true);
                    }
                }
            }
            else
            {
                txtPwd.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "ErrorMssgPopup('Invalid email format.')", true);
            }
        }
        catch (Exception ex)
        {
            Response.RedirectToRoute("login");
        }
    }
    #endregion
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
                    DataTable DtSendMailForgotpassword = LO.RetriveForgotPasswordEmail(Co.RSQandSQLInjection(txtforgotemailid.Text, "soft"), "ForgotPassword");
                    if (DtSendMailForgotpassword.Rows.Count > 0)
                    {
                        string EmpCode = DtSendMailForgotpassword.Rows[0]["NodalOfficerRefNo"].ToString();
                        SendEmailCode(EmpCode);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "ErrorMssgPopup('Email send to your registerd email id successfully.')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "ErrorMssgPopup('Email id not registerd with us.')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "ErrorMssgPopup('Invalid email format.')", true);
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    #region Send Mail
    public void SendEmailCode(string empid)
    {
        try
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/ForgotPassword.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", txtforgotemailid.Text);
            body = body.Replace("{refno}", HttpUtility.UrlEncode(objEnc.EncryptData(empid)));
            body = body.Replace("{mcurid}", Resturl(56));
            SendMail s;
            s = new SendMail();
            s.CreateMail("noreply@srijandefence.gov.in", txtforgotemailid.Text, "Reset Password Email", body);
            s.sendMail();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "ErrorMssgPopup('" + ex.Message + "')", true);
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
    protected void UserLoginLog()
    {
        try
        {
            hyLog["UserId"] = objEnc.DecryptData(Session["User"].ToString());
            GetIpAddress();
            hyLog["Form"] = Path.GetFileName(Request.Url.AbsolutePath);
            hyLog["Activity"] = "Login";
            DateTime Date = Convert.ToDateTime(DateTime.Now.ToString());
            string mDate = Date.ToString("dd-MM-yyyy");
            hyLog["LoginDate"] = mDate;
            DateTime Time = Convert.ToDateTime(DateTime.Now.ToString());
            string mTime = Time.ToString("hh:mm:ss");
            hyLog["LoginTime"] = mTime;
            string InsertLog = LO.SaveLog(hyLog, out  _sysMsg, out  _msg);
        }
        catch (Exception ex)
        { }
    }
    public void GetIpAddress()  // Get IP Address
    {
        IPHostEntry ipEntry = Dns.GetHostEntry(GetCompCode());
        IPAddress[] addr = ipEntry.AddressList;
        string ip = "";
        ip = addr[1].ToString();
        string s = ipEntry.HostName.ToString();
        hyLog["SystemName"] = s.ToString();
        hyLog["IPAddress"] = ip.ToString();
        //return ip;        
    }
    public static string GetCompCode()  // Get Computer Name
    {
        string strHostName = "";
        strHostName = Dns.GetHostName();
        return strHostName;
    }
    protected void Logoutstatus()
    {
        try
        {
            HyLoginStatus["LoginUser"] = txtUserName.Text;
            HyLoginStatus["IsLogedIn"] = "Y";
            DateTime Date = Convert.ToDateTime(DateTime.Now);
            string dateformat = Date.ToString("yyyy-MM-dd hh:mm:ss");
            HyLoginStatus["IsLogedOutTime"] = dateformat.ToString();
            string InsertLogOutStatus = LO.SaveLogoutstatus(HyLoginStatus, out  _sysMsg, out  _msg);
        }
        catch (Exception ex)
        { }
    }
}