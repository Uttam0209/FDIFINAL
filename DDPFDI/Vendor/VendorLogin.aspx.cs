using BusinessLayer;
using Encryption;
using System;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using context = System.Web.HttpContext;

public partial class Vendor_VendorLogin : System.Web.UI.Page
{
    #region "Variables"
    Logic LO = new Logic();
    DataUtility Co = new DataUtility();
    Cryptography objEnc = new Cryptography();
    HybridDictionary hyLogin = new HybridDictionary();
    HybridDictionary HySaveVendor = new HybridDictionary();
    string VName = string.Empty;
    string _msg = string.Empty;
    string Defaultpage = string.Empty;
    string CompanyName = string.Empty;
    string _sysMsg = string.Empty;
    string notvalidate = string.Empty;
    string _EmpId = string.Empty;
    string AuthFilePath = string.Empty;
    string IdentityPath = string.Empty;
    bool MsgStatus;
    Int64 MId = 0;
    #endregion  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["User"] != null)
                txtUserName.Text = Request.Cookies["User"].Value;
            if (Request.Cookies["pwd"] != null)
                txtPwd.Attributes.Add("value", Request.Cookies["pwd"].Value);
            if (Request.Cookies["User"] != null && Request.Cookies["pwd"] != null)
                remberme.Checked = true;
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
    public bool IsValidEmailRegister(string InputEmail)
    {
        string DtEmailValidate = LO.VerifyEmailandCompany(InputEmail, "VEmail", out _msg);
        if (_msg.ToString() == InputEmail.ToString())
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
                    txtPwd.Text = "";
                    lblmsg.Text = "Invalid Captcha.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
                }
                else
                {
                    hyLogin["UserName"] = Co.RSQandSQLInjection(txtUserName.Text.Trim() + "'", "hard" + "'");
                    string _VerEmail = LO.VerifyEmailVendor(hyLogin, out _msg, out _sysMsg);
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
                            string _EmpId = LO.VerifyVendorEmployee(hyLogin, out _msg, out Defaultpage, out CompanyName, out VName);
                            if (_EmpId != "0" && _EmpId != "1" && _msg != "")
                            {
                                Session["VType"] = objEnc.EncryptData(_msg);
                                Session["VUserEmail"] = objEnc.EncryptData(txtUserName.Text);
                                Session["VUser"] = objEnc.EncryptData(VName);
                                Session["VendorRefNo"] = objEnc.EncryptData(_EmpId);
                                Session["VCompName"] = CompanyName;
                                string guid = Guid.NewGuid().ToString();
                                Session["SFToken"] = guid;
                                Response.Cookies.Add(new HttpCookie("SFToken", guid));
                                if (remberme.Checked == true)
                                {
                                    Response.Cookies["User"].Value = txtUserName.Text;
                                    Response.Cookies["pwd"].Value = txtPwd.Text;
                                    Response.Cookies["User"].Expires = DateTime.Now.AddDays(15);
                                    Response.Cookies["pwd"].Expires = DateTime.Now.AddDays(15);
                                }
                                else
                                {
                                    Response.Cookies["User"].Expires = DateTime.Now.AddDays(-1);
                                    Response.Cookies["pwd"].Expires = DateTime.Now.AddDays(-1);
                                }
                                hfredirec.Value = Defaultpage.ToString();
                                timer1.Enabled = true;
                                lblmsg.ForeColor = System.Drawing.Color.Green;
                                lblmsg.Text = "Successfully Login.Please wait while we redirect you to HomePage.";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
                            }
                            else
                            {
                                FillCapctha();
                                lblmsg.Text = "Invalid Login. Either user id or password or both are incorrect.";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
                            }
                        }
                        else
                        {
                            FillCapctha();
                            lblmsg.Text = "Invalid Password.";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
                        }
                    }
                    else
                    {
                        FillCapctha();
                        lblmsg.Text = "Invalid User.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
                    }
                }
            }
            else
            {
                lblmsg.Text = "Invalid email format.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
        }
    }
    protected void timer1_Tick(object sender, EventArgs e)
    {
        timer1.Enabled = false;
        Response.Redirect(hfredirec.Value);
    }
    #endregion
    #region Forgot Password" 
    protected void lbforgot_Click(object sender, EventArgs e)
    {
        am.Visible = false;
        lbmsgforgot.Text = "";
        txtforgotemailid.Text = "";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "exampleModal", "showPopup1();", true);
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
                        txtforgotemailid.Text = "";
                        am.Visible = true;
                        lbmsgforgot.Text = "A reset password mail send to your registerd mail id successfully.Please click on given link and update your password.";
                    }
                    else
                    {
                        am.Visible = true;
                        lbmsgforgot.Text = "Email-Id not registerd with us.";
                    }
                }
                else
                {
                    am.Visible = true;
                    lbmsgforgot.Text = "Invalid Email-Id format.";
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
            s.CreateMail("noreply-srijandefence@gov.in", txtforgotemailid.Text, "Forgot Password Recover Mail", body);
            // s.sendMail();
            s.sendMail("smtp.gmail.com", 587, "noreply.gipinfosystems@gmail.com", "Gip@1234##");
        }
        catch (Exception ex)
        {
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
    #region Captchs Code
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
            imgCaptcha.ImageUrl = "~/GenerateCaptcha.aspx?" + DateTime.Now.Ticks.ToString();
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
}