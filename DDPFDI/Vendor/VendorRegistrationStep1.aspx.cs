using System;
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

public partial class Vendor_VendorRegistrationStep1 : System.Web.UI.Page
{
    Logic Lo = new Logic();
    HybridDictionary HySaveVendor = new HybridDictionary();
    DataUtility Co = new DataUtility();
    Cryptography Encrypt = new Cryptography();
    string _sysMsg = string.Empty;
    string _msg = string.Empty;
    Int64 MId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindTypeOfBusiness();
            Bindbusinesssector();
            BindCountry();
        }
    }
    //protected void ddlregisterdgst_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlregisterdgst.SelectedItem.Text == "YES")
    //    { divgst.Visible = true; }
    //    else
    //    { divgst.Visible = false; }
    //}
    protected void ddlpan_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlpan.SelectedItem.Text == "YES")
        { divpan.Visible = true; }
        else
        { divpan.Visible = false; }
    }
    protected void cleartext()
    {
        txtbusinessname.Text = "";
        txtcity.Text = "";
        ddlcountry.SelectedIndex = 0;
        txtemail.Text = "";
        txtfaxphoneno.Text = "";
        txtfaxstdcode.Text = "";
        txtfirstname.Text = "";
        //  txtgstno.Text = "";
        txtpanno.Text = "";
        txtlastname.Text = "";
        txtmiddlename.Text = "";
        txtphoneno.Text = "";
        txtpostalzipcode.Text = "";
        txtstateprovince.Text = "";
        txtstdcode.Text = "";
        txtstreetaddress.Text = "";
        txtstreetaddressline2.Text = "";
        ddlbusinesssector.SelectedIndex = 0;
        // ddlregisterdgst.SelectedIndex = 0;
        ddlpan.SelectedIndex = 0;
        ddltypeofbusiness.SelectedIndex = 0;
    }
    protected void SaveCode()
    {
        HySaveVendor["VendorID"] = MId;
        HySaveVendor["IsPan"] = ddlpan.SelectedItem.Value;
        HySaveVendor["PanNo"] = txtpanno.Text;
        HySaveVendor["IsGST"] = "";// ddlregisterdgst.SelectedItem.Value;
        HySaveVendor["GSTNo"] = "";// txtgstno.Text.Trim();
        HySaveVendor["BusinessName"] = txtbusinessname.Text.Trim();
        HySaveVendor["NodalOfficerPrefix"] = ddltittle.SelectedItem.Text;
        HySaveVendor["NodalOfficerFirstName"] = txtfirstname.Text.Trim();
        HySaveVendor["NodalOfficerMiddleName"] = txtmiddlename.Text.Trim();
        HySaveVendor["NodalOfficerLastName"] = txtlastname.Text.Trim();
        HySaveVendor["NodalOfficerEmail"] = txtemail.Text.Trim();
        HySaveVendor["TypeOfBuisness"] = ddltypeofbusiness.SelectedItem.Value;
        HySaveVendor["BusinessSector"] = ddlbusinesssector.SelectedItem.Value;
        HySaveVendor["StreetAddress"] = txtstreetaddress.Text.Trim();
        HySaveVendor["StreetAddressLine2"] = txtstreetaddressline2.Text.Trim();
        HySaveVendor["City"] = txtcity.Text.Trim();
        HySaveVendor["State"] = txtstateprovince.Text.Trim();
        HySaveVendor["ZipCode"] = txtpostalzipcode.Text.Trim();
        HySaveVendor["Country"] = ddlcountry.SelectedItem.Value;
        HySaveVendor["ContactNo"] = txtstdcode.Text + "-" + txtphoneno.Text.Trim();
        HySaveVendor["FaxNo"] = txtfaxstdcode.Text.Trim() + "-" + txtfaxphoneno.Text.Trim();
        string str = Lo.SaveVendorRegis(HySaveVendor, out _sysMsg, out _msg);
        if (str != "")
        {
            SendEmailCode(str);
            cleartext();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "changePass", "showPopup();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modelfail", "showPopup1();", true);
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (txtfirstname.Text != "" && txtemail.Text != "" && lblmsgpan.Text != "Invalid Pan" && lblbusinessname.Text == "")
        {
            if (IsValidEmailId(txtemail.Text))
            {
                SaveCode();
            }
            else
            { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('email format not valid')", true); }
        }
        else
        { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Please fill mandatory field')", true); }
    }
    public static bool IsValidEmailId(string InputEmail)
    {
        string pattern = @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}$";
        Match match = Regex.Match(InputEmail.Trim(), pattern, RegexOptions.IgnoreCase);
        if (match.Success)
            return true;
        else
            return false;
    }
    protected void BindTypeOfBusiness()
    {
        DataTable DtMasterCategroyBusiness = Lo.RetriveMasterData(1, "", "", 0, "", "", "TypeofBusiness");
        if (DtMasterCategroyBusiness.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddltypeofbusiness, DtMasterCategroyBusiness, "VendorSubCatName", "VendorSubCatID");
            ddltypeofbusiness.Items.Insert(0, "Select");
        }
    }
    protected void Bindbusinesssector()
    {
        DataTable DtMasterCategroyBusiness = Lo.RetriveMasterData(2, "", "", 0, "", "", "BuisnessSector");
        if (DtMasterCategroyBusiness.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlbusinesssector, DtMasterCategroyBusiness, "VendorSubCatName", "VendorSubCatID");
            ddlbusinesssector.Items.Insert(0, "Select");
        }
    }
    #region For Country
    protected void BindCountry()
    {
        DataTable DtCountry = Lo.RetriveCountry(0, "Select");
        if (DtCountry.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlcountry, DtCountry, "CountryName", "CountryID");
            ddlcountry.Items.Insert(0, "Select");
        }
    }
    #endregion
    #region Send Mail
    public void SendEmailCode(string empid)
    {
        try
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/VenRegisOrPassGenerateLink.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", txtemail.Text);
            body = body.Replace("{refno}", HttpUtility.UrlEncode(Encrypt.EncryptData(empid)));
            body = body.Replace("{mcurid}", Resturl(56));
            SendMail s;
            s = new SendMail();
            s.CreateMail("noreply-srijandefence@gov.in", txtemail.Text, "Create Password Email", body);
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
    #region PanCard Verification Code
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
    #endregion
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
}