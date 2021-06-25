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
using System.Net.Mail;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Security.Cryptography;

public partial class Vendor_Notes_gethashforesign_VendorRegistration : System.Web.UI.Page
{
    #region "Variables"
    Logic LO = new Logic();
    DataUtility Co = new DataUtility();
    Cryptography objEnc = new Cryptography();
    HybridDictionary hyLogin = new HybridDictionary();
    HybridDictionary HySaveVendor = new HybridDictionary();
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
        try
        {
            if (!IsPostBack)
            {
                PageLoadCode();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('UserError:- E-Sign In failed. Technical Error:-" + ex.Message.ToString() + "');", true);
        }
    }  
    #region  Savecode
    protected void SaveCode()
    {
        HySaveVendor["VendorID"] = 0;
        if (Session["PanNo"].ToString() != "")
        {
            HySaveVendor["PanNo"] = Session["PanNo"].ToString();
        }
        else
        {
            HySaveVendor["GSTNo"] = Session["GSTNo"].ToString();
        }
        HySaveVendor["DPSU"] = Session["DPSU"].ToString();
        HySaveVendor["V_CompName"] = Session["V_CompName"].ToString();
        HySaveVendor["NodalOfficerName"] = Session["NodalOfficerName"].ToString();
        HySaveVendor["NodalOfficerEmail"] = Session["NodalOfficerEmail"].ToString();
        HySaveVendor["AuthPath"] = Session["AuthFilePath"].ToString();
        HySaveVendor["IsEsignVerify"] = "Y";
        string str = LO.SaveVendorRegis(HySaveVendor, out _sysMsg, out _msg);
        if (str != "")
        {
            SendEmailCodeRegis(str);
            clearregistrationtext();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Registration done successfully.Password created releted mail send to your registerd nodal email id.');window.location ='Registration';", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Registration for cvr portal failed.Try again');window.location ='Registration';", true);
        }
    }
    #endregion
    #region Send Mail
    public void SendEmailCodeRegis(string empid)
    {
        try
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/VenRegisOrPassGenerateLink.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", Session["NodalOfficerEmail"].ToString());
            body = body.Replace("{refno}", HttpUtility.UrlEncode(objEnc.EncryptData(empid)));
            body = body.Replace("{mcurid}", Resturl(56));
            SendMail s;
            s = new SendMail();
            s.CreateMail("noreply-srijandefence@gov.in", Session["NodalOfficerEmail"].ToString(), "Create Password Email", body);
            //s.sendMail();
            s.sendMail("smtp.gmail.com", 587, "noreply.gipinfosystems@gmail.com", "Gip@1234##");
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
        }
    }
    public bool SendEmailForOTP(string OTP)
    {
        try
        {
            string body = "Dear,\n\nThis is an  Your OTP : '" + OTP + "' For Email Validation. ";
            SendMail s;
            s = new SendMail();
            s.CreateMail("noreply-srijandefence@gov.in", txtnodelemail.Text, "OTP Verification For Email-Id", body);
            //s.sendMail();
            s.sendMail("smtp.gmail.com", 587, "noreply.gipinfosystems@gmail.com", "Gip@1234##");
            MsgStatus = true;
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
        }
        return MsgStatus;
    }
    #endregion
    #region Buttoncode     
    protected void btnValidateEmailId_Click(object sender, EventArgs e)
    {
        if (txtotp.Text == Session["OTP"].ToString() && txtotp.Text != "")
        {
            Session["VendorID"] = 0;
            if (txtpanno.Text.Length == 10)
            {
                Session["PanNo"] = Co.RSQandSQLInjection(txtpanno.Text.Trim(), "soft");
            }
            else
            {
                Session["GSTNo"] = Co.RSQandSQLInjection(txtpanno.Text.Trim(), "soft");
            }
            Session["DPSU"] = Co.RSQandSQLInjection(ddldpsu.SelectedItem.Value.Trim(), "soft");
            Session["V_CompName"] = Co.RSQandSQLInjection(txtbusinessname.Text.Trim(), "soft");
            Session["NodalOfficerName"] = Co.RSQandSQLInjection(txtnodalname.Text.Trim(), "soft");
            Session["NodalOfficerEmail"] = Co.RSQandSQLInjection(txtnodelemail.Text.Trim(), "soft");
            string base_urlapi = "http://srijandefence.gov.in/aadhar/StartPage.aspx";           
            string Retunurl = "http://localhost:19213/Registration";
            string Type = "data";
            string filePath = dataHash(txtnodalname.Text.Trim());
            string url = base_urlapi + "?filePath=" + filePath + "&Type=" + Type + "&Retunurl=" + Retunurl;
            Response.Redirect(url);
        }
        else
        {
            lblmsgotp.Attributes.Add("Class", "alert alert-danger");
            lblmsgotp.Text = "Invalid-OTP.";
        }
    }
    protected void lbuploadfile_Click(object sender, EventArgs e)
    {
        if (FileUpload1.PostedFile != null)
        {
            string strAuthFname = DateTime.Now.ToString("hh_mm_ss") + "-" + FileUpload1.FileName;
            Int32 filelenth = Convert.ToInt32(FileUpload1.FileName.Length);
            string ext = Path.GetExtension(strAuthFname);
            if (ext == ".pdf")
            {
                if (filelenth < 1024)
                {
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Upload/" + strAuthFname));
                    string AuthPath = "~/Upload/" + strAuthFname.ToString();
                    Session["AuthFilePath"] = AuthPath;
                    AuthFilePath = Session["AuthFilePath"].ToString();
                    fupmsg.ForeColor = System.Drawing.Color.Green;
                    fupmsg.Text = "File Upload Successfully.";
                    lbnext.Enabled = true;
                }
                else
                {
                    fupmsg.ForeColor = System.Drawing.Color.Red;
                    fupmsg.Text = "Invalid file lenth only 1 mb file allowed";
                }
            }
            else
            {
                fupmsg.ForeColor = System.Drawing.Color.Red;
                fupmsg.Text = "Invalid file extentrion only .pdf file allowed";
            }
        }
    }
    protected void lbnext_Click(object sender, EventArgs e)
    {
        if (txtbusinessname.Text != "" && txtnodalname.Text != "" && txtnodelemail.Text != "" && txtpanno.Text != "")
        {
            if (txtnodelemail.Text != "")
            {
                if (IsValidEmailId(txtnodelemail.Text) == true)
                {
                    if (IsValidEmailRegister(txtnodelemail.Text) == true)
                    {
                        lblmsgotp.Attributes.Add("Class", "");
                        lblmsgotp.Text = "";
                        GenerateOTP();
                        txtotp.Text = Session["OTP"].ToString();
                        SendEmailForOTP(Session["OTP"].ToString());
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "verifyemail", "showPopup1();", true);
                    }
                    else
                    {
                        lblmsg.Text = "Email already registerd.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
                    }
                }
                else
                {
                    lblmsg.Text = "Invalid email format";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
                }
            }
        }
        else
        {
            lblmsg.Text = "All field fill mandatory";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
        }
    }
    protected void lbresentotp_Click(object sender, EventArgs e)
    {
        string OTP = GenerateOTP();
        bool msg = SendEmailForOTP(OTP);
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        clearregistrationtext();
    }
    #endregion
    #region Master Code 
    public string GenerateOTP()
    {
        string VOTP = "";
        try
        {
            string numbers = "1234567890";
            string characters = numbers;
            int length = int.Parse("6");
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            Session["OTP"] = otp;
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        return VOTP;
    }
    protected void clearregistrationtext()
    {
        panverifi.Visible = false;
        txtpanno.Text = "";
        txtbusinessname.Text = "";
        txtnodalname.Text = "";
        txtnodelemail.Text = "";
        lblbusinessname.Text = "";
        lblmsgpan.Text = "";
        hfpanname.Value = "";
        fupmsg.Text = "";
        ddldpsu.SelectedIndex = -1;
        Session.RemoveAll();
        Session.Clear();
    }
    protected void binddpsu()
    {
        DataTable DtCompanyDDL = LO.RetriveMasterData(0, "", "Company", 0, "", "", "Select");
        if (DtCompanyDDL.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddldpsu, DtCompanyDDL, "CompanyName", "CompanyRefNo");
            ddldpsu.Items.Insert(0, "Select");
        }
    }
    protected void PageLoadCode()
    {
        binddpsu();
        if (Request.QueryString["Date"] != null && Request.QueryString["signedby"] != null)
        {
            if (Session["NodalOfficerName"].ToString() == Request.QueryString["signedby"].ToString())
            {
                SaveCode();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('E-Sign Verification Failed.');window.location ='Registration';", true);
            }
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
    #region "Validation Code"
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
    #endregion
    #region PanCard Verification Code
    public Boolean AcceptAllCertifications(Object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }
    protected void PanVerification(string panno)
    {
        try
        {
            string requestUristring = string.Format("http://maketheindia.in/Pan-Verification?pancardNO=V0224301^" + panno.ToString());
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(AcceptAllCertifications);
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(requestUristring);
            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = respStreamReader.ReadToEnd();
            string str = responseString.ToString();
            char[] deli = { '^' };
            string[] split = str.Split(deli);
            string A = split[0];
            if (A.Substring(13) == "1")
            {
                string B = split[1];
                if (B != null)
                {
                    Session["VendorRefID"] = B.ToString();
                }
                else
                {
                    Session["VendorRefID"] = "";
                }

                string C = split[3];
                string D = split[4];
                string E = split[6];
                string F = split[7];
                string G = split[8];
                if (G.ToString() != "")
                {
                    Session["PANCOMPNAME"] = G.ToString();
                }
                string H = split[10];
                if (split[4].ToString() == "")
                {
                    hfpanname.Value = split[3].ToString();
                }
                else
                {
                    hfpanname.Value = split[4] + " " + split[3].ToString();
                }
                Session["PANCOMPNAME"] = hfpanname.Value;
                panverifi.Attributes.Add("Class", "fa fa-check");
                lblmsgpan.ForeColor = System.Drawing.Color.Green;
                if (txtpanno.Text.Length == 10)
                {
                    lblmsgpan.Text = "Valid Pan";
                }
                else
                {
                    lblmsgpan.Text = "Valid GST";
                }
                lblmsgpan.Visible = true;
            }
            else
            {
                panverifi.Attributes.Add("Class", "fa fa-times");
                lblmsgpan.ForeColor = System.Drawing.Color.Red;
                if (txtpanno.Text.Length == 10)
                {
                    lblmsgpan.Text = "Invalid Pan";
                }
                else
                {
                    lblmsgpan.Text = "Invalid GST";
                }
                lblmsgpan.Visible = true;
                txtpanno.Focus();
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
        }
    }
    protected void txtpanno_TextChanged(object sender, EventArgs e)
    {
        if (txtpanno.Text.Length == 10)
        {
            try
            {
                PanVerification(txtpanno.Text);
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
            }
        }
        else
        {
            try
            {
                string pno = txtpanno.Text.Substring(2, 10);
                PanVerification(pno.ToString());
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
            }
        }
    }
    #endregion
    #region selectindexchange texto=and other code
    protected void txtbusinessname_TextChanged(object sender, EventArgs e)
    {
        if (lblmsgpan.Text == "Valid Pan")
        {
            if (txtbusinessname.Text.ToUpper() == Session["PANCOMPNAME"].ToString())
            {
                panverifi.Attributes.Add("Class", "fa fa-check");
                lblbusinessname.Text = "Valid Company.";
                lblbusinessname.Visible = true;
                lblbusinessname.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblbusinessname.Text = "Invalid Company.";
                panverifi.Attributes.Add("Class", "fa fa-times");
                lblbusinessname.ForeColor = System.Drawing.Color.Red;
                txtbusinessname.Focus();
            }
        }
        else if (lblmsgpan.Text == "Valid GST")
        {
            if (txtbusinessname.Text.ToUpper() == Session["PANCOMPNAME"].ToString())
            {
                panverifi.Attributes.Add("Class", "fa fa-check");
                lblbusinessname.Text = "Valid Company.";
                lblbusinessname.Visible = true;
                lblbusinessname.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblbusinessname.Text = "Invalid Company.";
                panverifi.Attributes.Add("Class", "fa fa-times");
                lblbusinessname.ForeColor = System.Drawing.Color.Red;
                txtbusinessname.Focus();
            }
        }
        else
        {
            lblbusinessname.Text = "Invalid Company.";
            panverifi.Attributes.Add("Class", "fa fa-times");
            lblbusinessname.ForeColor = System.Drawing.Color.Red;

        }
    }
    protected void txtnodelemail_TextChanged(object sender, EventArgs e)
    {
        if (txtnodelemail.Text != "")
        {
            if (IsValidEmailId(txtnodelemail.Text) == true)
            {
                if (IsValidEmailRegister(txtnodelemail.Text) == true)
                {
                    lblmsgotp.Attributes.Add("Class", "");
                    lblmsgotp.Text = "";
                    GenerateOTP();
                    txtotp.Text = Session["OTP"].ToString();
                    SendEmailForOTP(Session["OTP"].ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "verifyemail", "showPopup1();", true);
                }
                else
                {
                    lblmsg.Text = "Email already registerd.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
                }
            }
            else
            {
                lblmsg.Text = "Invalid email format";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
            }
        }
    }
    #endregion
    #region esign
    public static string dataHash(string val)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // ComputeHash - returns byte array  
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(val));

            // Convert byte array to a string   
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
    #endregion    
}