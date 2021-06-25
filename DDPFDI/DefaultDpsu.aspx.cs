using BusinessLayer;
using Encryption;
using System;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using context = System.Web.HttpContext;

public partial class _DefaultD : System.Web.UI.Page
{
    #region "Variables"
    UserIPAnalytics userip = new UserIPAnalytics();
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
        if (!IsPostBack)
        {
            userip.GetAnalytics();
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
    public static bool IsPasswordvalid(string password)
    {
        var pattern = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$#^@$!%*?&])[A-Za-z\d$@$#^!%*?&]{8,64}");
        if (!pattern.IsMatch(password))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValidEmailId(txtUserName.Text) == true)
            {
                if (txtCaptcha.Text != Session["captcha"].ToString())
                {
                    if (CheckBlockAccount(txtUserName.Text) == true)
                    {
                        txtPwd.Text = "";
                        FillCapctha();
                        Logoutstatus("Login failed (Due to invalid captcha login failed)");
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Invalid Captcha')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('You Reached Maximum login Attempts. Your account has been locked for " + mCount.ToString() + "')", true);
                        Logoutstatus("Login failed (" + mCount + " Max Limit Attempt)");
                        SendMailBLockAccount(mCount.ToString());
                        mCount = "";
                    }
                }
                else
                {
                    //if (Request.QueryString["mhelpdesk"] != null)
                   // {
                       // if (objEnc.DecryptData(Request.QueryString["mhelpdesk"].ToString()) == "HelpDeskLogin")
                       // {
                        //    userip.GetAnalytics();
                        //    hyLogin["UserName"] = Co.RSQandSQLInjection(Server.HtmlEncode(txtUserName.Text.Trim()) + "'", "hard" + "'");
                        //    hyLogin["Password"] = Co.RSQandSQLInjection(Server.HtmlEncode(txtPwd.Text.Trim()), "hard");
                        //    hyLogin["Brow"] = System.Web.HttpContext.Current.Request.Browser.Type.ToString();
                        //    hyLogin["ip"] = System.Web.HttpContext.Current.Request.UserHostAddress;
                        //    string _mEmpId = LO.VerifyHelpdeskEmployee(hyLogin, out _msg, out Defaultpage);
                        //    if (_mEmpId != "0" && _msg != "")
                        //    {
                        //        Session["GType"] = objEnc.EncryptData(_msg);
                        //        Session["GUser"] = objEnc.EncryptData(Server.HtmlEncode(txtUserName.Text));
                        //        Session["Gid"] = objEnc.EncryptData(_mEmpId);
                        //        string guid = Guid.NewGuid().ToString();
                        //        Session["SFToken"] = guid;
                        //        Response.Cookies.Add(new HttpCookie("SFToken", guid));
                        //        Response.RedirectToRoute(Defaultpage);
                        //    }
                        //    else
                        //    {
                        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Invalid Login. Either user id or password or both are incorrect.');", true);
                        //    }
                        //}
                        //else
                       // {
                            DataTable dt = LO.RetriveForgotPasswordEmail(txtUserName.Text.Trim(), "checkpass");
                            if (dt.Rows.Count > 0)
                            {
                                if (IsPasswordvalid(dt.Rows[0]["tempref"].ToString()) == false)
                                {
                                    string Path = "PasswordPolicy?User=" + objEnc.EncryptData(txtUserName.Text);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your Password has been expired !!!Please create new password.');window.location='" + Path + "';", true);
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
                                                if (CheckBlockAccount(txtUserName.Text) == true)
                                                {
                                                    Session["Type"] = objEnc.EncryptData(_msg);
                                                    Session["User"] = objEnc.EncryptData(txtUserName.Text);
                                                    Session["CompanyRefNo"] = _EmpId;
                                                    string guid = Guid.NewGuid().ToString();
                                                    Session["SFToken"] = guid;
                                                    Response.Cookies.Add(new HttpCookie("SFToken", guid));
                                                    Logoutstatus("Succesfully Login");
                                                    UserLoginLog();
                                                    Response.RedirectToRoute(Defaultpage);
                                                }
                                                else
                                                {
                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('You Reached Maximum login Attempts. Your account has been locked for " + mCount.ToString() + "')", true);
                                                    Logoutstatus("Login failed (" + mCount + " Max Limit Attempt)"); SendMailBLockAccount(mCount.ToString()); mCount = "";
                                                }
                                            }
                                            else
                                            {
                                                if (CheckBlockAccount(txtUserName.Text) == true)
                                                {
                                                    Logoutstatus("Login failed (Due to invalid email/password login failed.Either user not active.)");
                                                    txtPwd.Text = "";
                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "ErrorMssgPopup('Invalid Login. Either user id or password or both are incorrect.')", true);
                                                }
                                                else
                                                {
                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('You Reached Maximum login Attempts. Your account has been locked for " + mCount.ToString() + "')", true);
                                                    Logoutstatus("Login failed (" + mCount + " Max Limit Attempt)"); SendMailBLockAccount(mCount.ToString()); mCount = "";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (CheckBlockAccount(txtUserName.Text) == true)
                                            {
                                                Logoutstatus("Login failed (Due to invalid password login failed)");
                                                txtPwd.Text = "";
                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "ErrorMssgPopup('Invalid Login. Either user id or password or both are incorrect.')", true);
                                            }
                                            else
                                            {
                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('You Reached Maximum login Attempts. Your account has been locked for " + mCount.ToString() + "')", true);
                                                Logoutstatus("Login failed (" + mCount + " Max Limit Attempt)"); SendMailBLockAccount(mCount.ToString()); mCount = "";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (CheckBlockAccount(txtUserName.Text) == true)
                                        {
                                            Logoutstatus("Login failed (Due to invalid email login failed)");
                                            txtPwd.Text = "";
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "ErrorMssgPopup('Invalid User.Email id not registerd with us.')", true);
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('You Reached Maximum login Attempts. Your account has been locked for " + mCount.ToString() + "')", true);
                                            Logoutstatus("Login failed (" + mCount + " Max Limit Attempt)"); SendMailBLockAccount(mCount.ToString()); mCount = "";
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (CheckBlockAccount(txtUserName.Text) == true)
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Invalid User.')", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('You Reached Maximum login Attempts. Your account has been locked for " + mCount.ToString() + "')", true);
                                    Logoutstatus("Login failed (" + mCount + " Max Limit Attempt)"); SendMailBLockAccount(mCount.ToString());
                                    mCount = "";
                                }
                            }
                      //  }
                  //  }
                    //else
                    //{
                    //    DataTable dt = LO.RetriveForgotPasswordEmail(txtUserName.Text.Trim(), "checkpass");
                    //    if (dt.Rows.Count > 0)
                    //    {
                    //        if (IsPasswordvalid(dt.Rows[0]["tempref"].ToString()) == false)
                    //        {
                    //            string Path = "PasswordPolicy?User=" + objEnc.EncryptData(txtUserName.Text);
                    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your Password has been expired !!!Please create new password.');window.location='" + Path + "';", true);
                    //        }
                    //        else
                    //        {
                    //            hyLogin["UserName"] = Co.RSQandSQLInjection(txtUserName.Text.Trim() + "'", "hard" + "'");
                    //            string _VerEmail = LO.VerifyEmail(hyLogin, out _msg, out _sysMsg);
                    //            if (_VerEmail != "0" && _VerEmail != "1" && _msg != "0" && _sysMsg != "")
                    //            {
                    //                string HashedPassword = _msg.ToString();
                    //                string Salt = _sysMsg.ToString();
                    //                byte[] passwordAndSalt = System.Text.Encoding.UTF8.GetBytes(txtPwd.Text + Salt);
                    //                byte[] hashPass = new System.Security.Cryptography.SHA256Managed().ComputeHash(passwordAndSalt);
                    //                string hashCode = Convert.ToBase64String(hashPass);
                    //                if (hashCode == HashedPassword)
                    //                {
                    //                    hyLogin["Password"] = _msg.ToString();
                    //                    string _EmpId = LO.VerifyEmployee(hyLogin, out _msg, out Defaultpage);
                    //                    if (_EmpId != "0" && _EmpId != "1" && _msg != "")
                    //                    {
                    //                        if (CheckBlockAccount(txtUserName.Text) == true)
                    //                        {
                    //                            Session["Type"] = objEnc.EncryptData(_msg);
                    //                            Session["User"] = objEnc.EncryptData(txtUserName.Text);
                    //                            Session["CompanyRefNo"] = _EmpId;
                    //                            string guid = Guid.NewGuid().ToString();
                    //                            Session["SFToken"] = guid;
                    //                            Response.Cookies.Add(new HttpCookie("SFToken", guid));
                    //                            Logoutstatus("Succesfully Login");
                    //                            UserLoginLog();
                    //                            Response.RedirectToRoute(Defaultpage);
                    //                        }
                    //                        else
                    //                        {
                    //                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('You Reached Maximum login Attempts. Your account has been locked for " + mCount.ToString() + "')", true);
                    //                            Logoutstatus("Login failed (" + mCount + " Max Limit Attempt)"); SendMailBLockAccount(mCount.ToString()); mCount = "";
                    //                        }
                    //                    }
                    //                    else
                    //                    {
                    //                        if (CheckBlockAccount(txtUserName.Text) == true)
                    //                        {
                    //                            Logoutstatus("Login failed (Due to invalid email/password login failed.Either user not active.)");
                    //                            txtPwd.Text = "";
                    //                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "ErrorMssgPopup('Invalid Login. Either user id or password or both are incorrect.')", true);
                    //                        }
                    //                        else
                    //                        {
                    //                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('You Reached Maximum login Attempts. Your account has been locked for " + mCount.ToString() + "')", true);
                    //                            Logoutstatus("Login failed (" + mCount + " Max Limit Attempt)"); SendMailBLockAccount(mCount.ToString()); mCount = "";
                    //                        }
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    if (CheckBlockAccount(txtUserName.Text) == true)
                    //                    {
                    //                        Logoutstatus("Login failed (Due to invalid password login failed)");
                    //                        txtPwd.Text = "";
                    //                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "ErrorMssgPopup('Invalid Login. Either user id or password or both are incorrect.')", true);
                    //                    }
                    //                    else
                    //                    {
                    //                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('You Reached Maximum login Attempts. Your account has been locked for " + mCount.ToString() + "')", true);
                    //                        Logoutstatus("Login failed (" + mCount + " Max Limit Attempt)"); SendMailBLockAccount(mCount.ToString()); mCount = "";
                    //                    }
                    //                }
                    //            }
                    //            else
                    //            {
                    //                if (CheckBlockAccount(txtUserName.Text) == true)
                    //                {
                    //                    Logoutstatus("Login failed (Due to invalid email login failed)");
                    //                    txtPwd.Text = "";
                    //                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "ErrorMssgPopup('Invalid User.Email id not registerd with us.')", true);
                    //                }
                    //                else
                    //                {
                    //                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('You Reached Maximum login Attempts. Your account has been locked for " + mCount.ToString() + "')", true);
                    //                    Logoutstatus("Login failed (" + mCount + " Max Limit Attempt)"); SendMailBLockAccount(mCount.ToString()); mCount = "";
                    //                }
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (CheckBlockAccount(txtUserName.Text) == true)
                    //        {
                    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Invalid User.')", true);
                    //        }
                    //        else
                    //        {
                    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('You Reached Maximum login Attempts. Your account has been locked for " + mCount.ToString() + "')", true);
                    //            Logoutstatus("Login failed (" + mCount + " Max Limit Attempt)"); SendMailBLockAccount(mCount.ToString());
                    //            mCount = "";
                    //        }
                    //    }
                    //}
                }
            }
            else
            {
                Logoutstatus("Login failed (Due to invalid email format login failed)");
                txtPwd.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "ErrorMssgPopup('Invalid email format.')", true);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
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
                    LO.SaveForgetExp(txtforgotemailid.Text);
                    DataTable DtSendMailForgotpassword = LO.RetriveForgotPasswordEmail(Co.RSQandSQLInjection(txtforgotemailid.Text, "soft"), "ForgotPassword");
                    if (DtSendMailForgotpassword.Rows.Count > 0)
                    {
                        string EmpCode = DtSendMailForgotpassword.Rows[0]["NodalOfficerRefNo"].ToString();
                        SendEmailCode(EmpCode);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "ErrorMssgPopup('Email send to your registered email id successfully.')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "ErrorMssgPopup('Email id not registered with us.')", true);
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
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "ErrorMssgPopup('" + ex.Message + "')", true);
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
            s.CreateMail("noreply-srijandefence@gov.in", txtforgotemailid.Text, "Reset Password Email", body);
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
            string InsertLog = LO.SaveLog(hyLog, out _sysMsg, out _msg);
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
        HyLoginStatus["SystemName"] = s.ToString();
        HyLoginStatus["IPAddress"] = ip.ToString();
        //return ip;        
    }
    public static string GetCompCode()  // Get Computer Name
    {
        string strHostName = "";
        strHostName = Dns.GetHostName();
        return strHostName;
    }
    protected void Logoutstatus(string Activivty)
    {
        try
        {
            HyLoginStatus["LoginUser"] = txtUserName.Text;
            HyLoginStatus["IsLogedIn"] = "Y";
            DateTime Date = Convert.ToDateTime(DateTime.Now);
            string dateformat = Date.ToString("yyyy-MM-dd hh:mm:ss");
            HyLoginStatus["IsLogedOutTime"] = dateformat.ToString();
            GetIpAddress();
            HyLoginStatus["Activity"] = Activivty.ToString();
            string InsertLogOutStatus = LO.SaveLogoutstatus(HyLoginStatus, out _sysMsg, out _msg);

        }
        catch (Exception ex)
        { }
    }
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
            imgCaptcha.ImageUrl = "GenerateCaptcha.aspx?" + DateTime.Now.Ticks.ToString();
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
    #region TryCatchLog
    public static class ExceptionLogging
    {
        private static String ErrorlineNo, Errormsg, extype, exurl, hostIp, ErrorLocation, HostAdd;
        public static void SendErrorToText(Exception ex)
        {
            var line = Environment.NewLine + Environment.NewLine;
            ErrorlineNo = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
            Errormsg = ex.GetType().Name.ToString();
            extype = ex.GetType().ToString();
            exurl = context.Current.Request.Url.ToString();
            ErrorLocation = ex.Message.ToString();
            try
            {
                string filepath = context.Current.Server.MapPath("/Logs/");  //Text File Path
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                filepath = filepath + DateTime.Today.ToString("dd-MM-yy") + ".txt";   //Text File Name
                if (!File.Exists(filepath))
                {
                    File.Create(filepath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    string error = "Log Written Date:" + " " + DateTime.Now.ToString() + line + "Error Line No :" + " " + ErrorlineNo + line + "Error Message:" + " " + Errormsg + line + "Exception Type:" + " " + extype + line + "Error Location :" + " " + ErrorLocation + line + " Error Page Url:" + " " + exurl + line + "User Host IP:" + " " + hostIp + line;
                    sw.WriteLine("-----------Exception Details on " + " " + DateTime.Now.ToString() + "-----------------");
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                    sw.WriteLine(line);
                    sw.WriteLine(error);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    sw.WriteLine(line);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception exm)
            {
                exm.Message.ToString();
            }
        }
    }
    #endregion
    #region CheckBlockAccount
    string mCount;
    public bool CheckBlockAccount(string InputEmail)
    {
        DataTable DtChkBlock = LO.NewRetriveFilterCode("BlockAccount", InputEmail, "", "", "", 0, 0, 0);
        if (DtChkBlock.Rows.Count > 0)
        {
            if (Convert.ToInt32(DtChkBlock.Rows[0]["FailedCount"]) == 5)
            {
                mCount = "1 hour";
                return false;
            }
            else if (Convert.ToInt32(DtChkBlock.Rows[0]["FailedCount"]) <= 10 && Convert.ToInt32(DtChkBlock.Rows[0]["FailedCount"]) > 5)
            { mCount = "8 hour"; return false; }
            else if (Convert.ToInt32(DtChkBlock.Rows[0]["FailedCount"]) > 10)
            { mCount = "24 hours"; return false; }
            else
            {
                mCount = "0";
                return true;
            }
        }
        else
        {
            return false;
        }
    }
    #endregion
    public void SendMailBLockAccount(string mtimeblock)
    {
        try
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/AccountBlock.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", txtUserName.Text);
            body = body.Replace("{timeblock}", mtimeblock.ToString());
            SendMail s;
            s = new SendMail();
            s.CreateMail("noreply-srijandefence@gov.in", txtUserName.Text, "Account Block", body);
            s.sendMail();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "ErrorMssgPopup('" + ex.Message + "')", true);
        }
    }

}





