﻿using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Encryption;

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
        divregistration.Visible = false;
        P3.Visible = false;
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
            s.CreateMail("noreply-srijandefence@gov.in", txtforgotemailid.Text, "Forgot Password Recover Mail", body);
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
            s.CreateMail("noreply-srijandefence@gov.in", txtUserName.Text, "Vendor Login OTP", body);
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
            divregistration.Visible = false;
            ViewState["mOTPKey"] = null;
        }
        else
        { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Invalid OTP')", true); }
    }
    protected void lbvenregis_Click(object sender, EventArgs e)
    {
        p2.Visible = false;
        p1.Visible = false;
        divregistration.Visible = true;
        P3.Visible = false;
        BindTypeOfBusiness();
        Bindbusinesssector();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "changePass", "showPopup();", true);
    }
    protected void BindTypeOfBusiness()
    {
        DataTable DtMasterCategroyBusiness = LO.RetriveMasterData(1, "", "", 0, "", "", "TypeofBusiness");
        if (DtMasterCategroyBusiness.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddltypeofbusiness, DtMasterCategroyBusiness, "VendorSubCatName", "VendorSubCatID");
            ddltypeofbusiness.Items.Insert(0, "Select");
        }
    }
    protected void Bindbusinesssector()
    {
        DataTable DtMasterCategroyBusiness = LO.RetriveMasterData(2, "", "", 0, "", "", "BuisnessSector");
        if (DtMasterCategroyBusiness.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlbusinesssector, DtMasterCategroyBusiness, "VendorSubCatName", "VendorSubCatID");
            ddlbusinesssector.Items.Insert(0, "Select");
        }
    }
    protected void txtbusinessname_TextChanged(object sender, EventArgs e)
    {
        if (hfpanname.Value == txtbusinessname.Text)
        {
            lblbusinessname.Text = "";
            check.Attributes.Add("Class", "fa fa-check");
        }
        else
        {
            check.Attributes.Add("Class", "fa fa-cross");
            lblbusinessname.Text = "Invalid Company name as per pan-card";
        }
    }    
    #region PanCard Verification Code
    protected void ddlpan_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlpan.SelectedItem.Text == "YES")
        { divpan.Visible = true; }
        else
        { divpan.Visible = false; }
    }
    protected void PanVerification()
    {
        string URL = ConfigurationManager.AppSettings["URL"];
        string PFXPassword = ConfigurationManager.AppSettings["PFXPassword"];
        string Certificatename = ConfigurationManager.AppSettings["Certificatename"];
        System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
        WriteTextFileLog("Application Started");
        try
        {
            X509Certificate2 m = new X509Certificate2(HttpContext.Current.Server.MapPath("/PFX/") + Certificatename, PFXPassword);
            byte[] bytes = encoding.GetBytes("V0224301^" + txtpanno.Text);
            byte[] sig = Sign(bytes, m);
            String sigi = Convert.ToBase64String(sig);
            try
            {
                StringBuilder postData = new StringBuilder();
                postData.Append("data=V0224301^" + txtpanno.Text);
                postData.Append("&signature=" + System.Web.HttpUtility.UrlEncode(sigi));
                postData.Append("&version=" + 2);
                byte[] data = encoding.GetBytes(postData.ToString());
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                    | SecurityProtocolType.Tls11
                                                    | SecurityProtocolType.Tls12
                                                    | SecurityProtocolType.Ssl3;
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(URL);
                myRequest.Method = "POST";
                myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.ContentLength = data.Length;
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
                ServicePointManager.Expect100Continue = true;
                Stream newStream = myRequest.GetRequestStream();
                newStream.Write(data, 0, data.Length);
                Console.WriteLine("Send");
                HttpWebResponse WebResp = (HttpWebResponse)myRequest.GetResponse();
                Stream Answer = WebResp.GetResponseStream();
                StreamReader _Answer = new StreamReader(Answer);
                Console.WriteLine("Received");
                string Response = _Answer.ReadToEnd();
                string[] splitString = Response.ToString().Split('^');
                hfpanname.Value = splitString[8];
                if (txtpanno.Text == splitString[1] && splitString[2] == "E")
                {
                    panverifi.Attributes.Add("Class", "fa fa-check");
                    lblmsgpan.ForeColor = System.Drawing.Color.Green;
                    lblmsgpan.Text = "Valid Pan";
                    lblmsgpan.Visible = true;
                }
                else
                {
                    panverifi.Attributes.Add("Class", "fa fa-cross");
                    lblmsgpan.ForeColor = System.Drawing.Color.Red;
                    lblmsgpan.Text = "Invalid Pan";
                    lblmsgpan.Visible = true;
                }
                newStream.Close();
            }
            catch (Exception ex)
            {
                WriteTextFileLog(ex.Message);
            }
        }
        catch (Exception ex)
        {
            WriteTextFileLog(ex.Message);
        }
        WriteTextFileLog("Application Ended");
    }
    public static byte[] Sign(byte[] data, X509Certificate2 certificate)
    {
        if (data == null)
            throw new ArgumentNullException("data");
        if (certificate == null)
            throw new ArgumentNullException("certificate");
        ContentInfo content = new ContentInfo(data);
        SignedCms signedCms = new SignedCms(content, false);
        CmsSigner signer = new CmsSigner(SubjectIdentifierType.IssuerAndSerialNumber, certificate);
        signedCms.ComputeSignature(signer);
        return signedCms.Encode();
    }
    public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }
    public static void WriteTextFileLog(string errorMessage, string type = "")
    {
        try
        {

            string path = HttpContext.Current.Server.MapPath("/Logs/") + DateTime.Today.ToString("dd-MM-yy") + ".txt";
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            using (StreamWriter w = File.AppendText(path))
            {
                string err = "Message : " + errorMessage;
                if (string.IsNullOrEmpty(type))
                {
                    w.WriteLine("Log Entry : " + DateTime.Now.ToString(CultureInfo.InvariantCulture) + " | " + err);
                    //w.WriteLine(err)
                    if (errorMessage.Contains("Requested item not found."))
                    {
                        //HttpContext.Current.Session["Error"] = "Access Denied"
                    }
                }
                else if (type == "E")
                {
                    w.WriteLine("______________________________________________________________________________");
                }
                w.Flush();
                w.Close();
            }
        }
        catch (System.Exception ex)
        {
        }
    }
    protected void txtpanno_TextChanged(object sender, EventArgs e)
    {
        PanVerification();
    }
    protected void ddldebarredgovtcont_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldebarredgovtcont.SelectedItem.Text == "Yes")
        { chkcontracts.Visible = true; }
        else
        { chkcontracts.Visible = false; }
    }
    protected void chkcontracts_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < chkcontracts.Items.Count; i++)
        {
            if (chkcontracts.Items[i].Value == "1" && chkcontracts.Items[i].Selected == true)
            {
                divfin.Visible = true;
            }
            else if (chkcontracts.Items[i].Value == "2" && chkcontracts.Items[i].Selected == true)
            {
                div12.Visible = true;
            }
            else if (chkcontracts.Items[i].Value == "3" && chkcontracts.Items[i].Selected == true)
            {
                div13.Visible = true;
            }
            else if (chkcontracts.Items[i].Value == "4" && chkcontracts.Items[i].Selected == true)
            {
                div14.Visible = true;
            }
            else
            {
                if (chkcontracts.Items[i].Value == "1" && chkcontracts.Items[i].Selected == false)
                { divfin.Visible = false; }
                else if (chkcontracts.Items[i].Value == "2" && chkcontracts.Items[i].Selected == false)
                { div12.Visible = false; }
                else if (chkcontracts.Items[i].Value == "3" && chkcontracts.Items[i].Selected == false)
                { div13.Visible = false; }
                else if (chkcontracts.Items[i].Value == "4" && chkcontracts.Items[i].Selected == false)
                { div14.Visible = false; }
            }
        }
    }
    #endregion
}