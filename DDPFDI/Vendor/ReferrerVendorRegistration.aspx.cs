using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Encryption;
using BusinessLayer;
using System.Net.Mail;
using System.IO;
using System.Text;
using System.Net;
using System.Drawing;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public partial class Vendor_ReferrerVendorRegistration : System.Web.UI.Page
{
    public string RefferEmailID;
    Cryptography ObjEnc = new Cryptography();
    Logic Lo = new Logic();
    DataUtility Co = new DataUtility();
    private object divregistration;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        { }
    }
    protected void ReffEmail(object sender, EventArgs e)
    {
        RefferEmailID = VEmailID.Text;
        RefferEmailID = RefferEmailID + "," + "dobhal.naveen17@gmail.com";
        MailMessage mm = new MailMessage("" + ConfigurationManager.AppSettings["UserName"] + "", RefferEmailID);
        mm.Subject = "Congratulations! Your account is Cedited";
        mm.Body = string.Format("<body bgcolor='#A7A7A7'> test link</table><body>");
        mm.IsBodyHtml = true;
        SmtpClient smtp = new SmtpClient();
        smtp.Host = ConfigurationManager.AppSettings["Host"];
        smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
        //smtpClient.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback = delegate (object c, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
        NetworkCredential NetworkCred = new NetworkCredential();
        NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
        NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
        smtp.UseDefaultCredentials = true;
        smtp.Credentials = NetworkCred;
        smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
        smtp.Send(mm);
        // Lo.UpdateContactVendor(mobno, ObjEnc.DecryptData(Session["User"].ToString()));
    }
    protected DataTable GetvendorIDDetail()
    {
        #region Registration Date Retrive
        DataTable DtGetRegisVendor1 = Lo.RetriveVendorDetal(ObjEnc.DecryptData(Session["User"].ToString()));
        if (DtGetRegisVendor1.Rows.Count > 0)
        {
            // VendorID = DtGetRegisVendor1.Rows[0]["VendorID"].ToString();
            // SvendorID = Session["VendorID"].ToString();
        }
        return DtGetRegisVendor1;
        #endregion
    }
    protected void lblresendotp_Click(object sender, EventArgs e)
    {
        SendEmailOTP(ObjEnc.DecryptData(Session["User"].ToString()));
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
            s.CreateMail("noreply-srijandefence@gov.in", ObjEnc.DecryptData(Session["User"].ToString()), "Vendor Login OTP", body);
            s.sendMail();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('OTP send Successfully.');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }

    }
    private object Resturl(int v)
    {
        throw new NotImplementedException();
    }
    protected void lbsubotp_Click(object sender, EventArgs e)
    {
        if (txtOTP.Text == ViewState["mOTPKey"].ToString())
        {
            P3.Visible = false;
            //divregistration.Visible = false;
            ViewState["mOTPKey"] = null;
        }
        else
        { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Invalid OTP')", true); }
    }
    protected void lbvenregis_Click(object sender, EventArgs e)
    {
        P3.Visible = false;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "changePass", "showPopup();", true);
    }
}