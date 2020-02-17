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

public partial class Vendor_VendorLogin : System.Web.UI.Page
{
    #region "Variables"
    Logic LO = new Logic();
    DataUtility Co = new DataUtility();
    Cryptography objEnc = new Cryptography();
    HybridDictionary hyLogin = new HybridDictionary();
    HybridDictionary HySaveVendor = new HybridDictionary();
    string _msg = string.Empty;
    string Defaultpage = string.Empty;
    string _sysMsg = string.Empty;
    string notvalidate = string.Empty;
    string _EmpId = string.Empty;
    Int64 MId = 0;
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
    public bool IsValidMobile(string InputMobile)
    {
        string DtEmailValidate = LO.VerifyEmailandCompany(InputMobile, "VPhone", out _msg);
        if (_msg.ToString() == InputMobile.ToString())
        {
            return false;
        }
        else
        {
            return true;
        }
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
    public void SendEmailCodeRegis(string empid)
    {
        try
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/VenRegisOrPassGenerateLink.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", txtnodelemail.Text);
            body = body.Replace("{refno}", HttpUtility.UrlEncode(objEnc.EncryptData(empid)));
            body = body.Replace("{mcurid}", Resturl(56));
            SendMail s;
            s = new SendMail();
            s.CreateMail("noreply-srijandefence@gov.in", txtnodelemail.Text, "Create Password Email", body);
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
    #region PanCard Verification Code
    protected void ddlpan_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlpan.SelectedItem.Text == "YES")
        { divpan.Visible = true; }
        else
        { divpan.Visible = false; }
    }
    public Boolean AcceptAllCertifications(Object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }
    protected void PanVerification()
    {
        try
        {
            string requestUristring = string.Format("http://maketheindia.in/Pan-Verification?pancardNO=V0224301^" + txtpanno.Text);
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
                string C = split[3];
                string D = split[4];
                string E = split[6];
                string F = split[7];
                string G = split[8];
                string H = split[10];
                if (split[4].ToString() == "")
                {
                    hfpanname.Value = split[3].ToString();
                }
                else
                {
                    hfpanname.Value = split[4] + " " + split[3].ToString();
                }
                panverifi.Attributes.Add("Class", "fa fa-check");
                lblmsgpan.ForeColor = System.Drawing.Color.Green;
                lblmsgpan.Text = "Valid Pan";
                lblmsgpan.Visible = true;
            }
            else
            {
                panverifi.Attributes.Add("Class", "fa fa-times");
                lblmsgpan.ForeColor = System.Drawing.Color.Red;
                lblmsgpan.Text = "Invalid Pan";
                lblmsgpan.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message.ToString() + "')", true);
        }
    }
    protected void txtpanno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            PanVerification();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message.ToString() + "')", true);
        }
    }
    #endregion
    #region Registration Code
    protected void binddpsu()
    {
        DataTable DtCompanyDDL = LO.RetriveMasterData(0, "", "Company", 0, "", "", "Select");
        if (DtCompanyDDL.Rows.Count > 0)
        {
            Co.FillCheckBox(chkdpsu, DtCompanyDDL, "CompanyName", "CompanyName");
        }
    }
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
        binddpsu();
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
        if (hfpanname.Value == txtbusinessname.Text && lblmsgpan.Text == "Valid Pan")
        {
            lblbusinessname.Text = "";
            check.Attributes.Add("Class", "fa fa-check");
            lblbusinessname.Text = "Valid Company.";
            lblbusinessname.Visible = true;
        }
        else
        {
            if (lblmsgpan.Text == "Valid Pan")
            {
                txtbusinessname.Text = hfpanname.Value;
                check.Attributes.Add("Class", "fa fa-check");
                lblbusinessname.Text = "Valid Company.";
                lblbusinessname.Visible = true;
            }
            else
            {
                lblbusinessname.Text = "Invalid Company.";
                check.Attributes.Add("Class", "fa fa-times");
                lblbusinessname.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void clearregistrationtext()
    {
        ddlpan.SelectedIndex = -1;
        ddlregiscategory.SelectedIndex = -1;
        txtpanno.Text = "";
        txtbusinessname.Text = "";
        ddltypeofbusiness.SelectedIndex = -1;
        ddlbusinesssector.SelectedIndex = -1;
        txtnodalname.Text = "";
        txtnodelemail.Text = "";
        txtMobileNodal.Text = "";
        txtstreetaddress.Text = "";
        txtstreetaddressline2.Text = "";
        txtcity.Text = "";
        txtstateprovince.Text = "";
        txtpostalzipcode.Text = "";
        // ddlcountry.SelectedIndex = -1;
        ddlwoundedup.SelectedIndex = -1;
        ddljudicialofficer.SelectedIndex = -1;
        ddlbusinesssuspended.SelectedIndex = -1;
        ddlforgingreasone.SelectedIndex = -1;
        ddldebarredgovtcont.SelectedIndex = -1;
        chkbuisness.SelectedIndex = -1;
        lblbusinessname.Text = "";
        lblmsgpan.Text = "";
        hfpanname.Value = "";
    }
    string mdpsuid;
    protected void SaveCode()
    {
        HySaveVendor["VendorID"] = MId;
        HySaveVendor["PanNo"] = txtpanno.Text;
        HySaveVendor["GSTNo"] = "";// txtgstno.Text.Trim();
        HySaveVendor["V_CompName"] = txtbusinessname.Text.Trim();
        foreach (ListItem lie in chkdpsu.Items)
        {
            if (lie.Selected == true)
            {
                mdpsuid = mdpsuid + lie.Value + ",";
            }
        }
        HySaveVendor["V_RegisterdDPSU"] = mdpsuid.ToString().TrimEnd(',');
        HySaveVendor["RegistrationCategory"] = ddlregiscategory.SelectedItem.Text;
        HySaveVendor["TypeOfBuisness"] = ddltypeofbusiness.SelectedItem.Value;
        HySaveVendor["BusinessSector"] = ddlbusinesssector.SelectedItem.Value;
        HySaveVendor["NodalOfficerName"] = txtnodalname.Text.Trim();
        HySaveVendor["NodalOfficerEmail"] = txtnodelemail.Text.Trim();
        HySaveVendor["ContactNo"] = txtMobileNodal.Text.Trim();
        HySaveVendor["StreetAddress"] = txtstreetaddress.Text.Trim();
        HySaveVendor["StreetAddressLine2"] = txtstreetaddressline2.Text.Trim();
        HySaveVendor["City"] = txtcity.Text.Trim();
        HySaveVendor["State"] = txtstateprovince.Text.Trim();
        HySaveVendor["ZipCode"] = txtpostalzipcode.Text.Trim();
        //HySaveVendor["Country"] = ddlcountry.SelectedItem.Value;
        HySaveVendor["CheckStatus"] = ddlwoundedup.SelectedItem.Text + "," + ddljudicialofficer.SelectedItem.Text + "," + ddlbusinesssuspended.SelectedItem.Text + "," + ddlforgingreasone.SelectedItem.Text + "," + ddldebarredgovtcont.SelectedItem.Text;
        string str = LO.SaveVendorRegis(HySaveVendor, out _sysMsg, out _msg);
        if (str != "")
        {
            SendEmailCodeRegis(str);
            clearregistrationtext();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record save successfully.Password releted mail send to your registerd email id.')", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#changePass').modal('hide')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not save successfully.')", true);
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtnodalname.Text != "" && txtnodelemail.Text != "")
            {
                if (lblmsgpan.Text != "Invalid Pan" && lblbusinessname.Text == "Valid Company.")
                {
                    if (ddlwoundedup.SelectedValue != "0" && ddljudicialofficer.SelectedValue != "0" && ddlbusinesssuspended.SelectedValue != "0" && ddlforgingreasone.SelectedValue != "0" && ddldebarredgovtcont.SelectedValue != "0")
                    {
                        if (IsValidEmailId(txtnodelemail.Text))
                        {
                            if (IsValidEmailRegister(txtnodelemail.Text))
                            {
                                if (IsValidEmailRegister(txtnodelemail.Text))
                                {
                                    foreach (ListItem lie in chkdpsu.Items)
                                    {
                                        if (lie.Selected == true)
                                        {
                                            mdpsuid = mdpsuid + lie.Value + ",";
                                        }
                                    }
                                    if (mdpsuid.ToString() != "")
                                    {
                                        SaveCode();
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Check alteast one dpsu.')", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Mobile already registerd.')", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('email already registerd')", true);
                            }
                        }
                        else
                        { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('email format not valid')", true); }
                    }
                    else { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Please select term and condition')", true); }
                }
                else
                { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Invalid pan or company name')", true); }
            }
            else
            { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Please fill mandatory field')", true); }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message.ToString() + "')", true);
        }
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        clearregistrationtext();
    }
    #endregion
}