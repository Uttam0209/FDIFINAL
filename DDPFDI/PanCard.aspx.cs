using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.UI;

public partial class PanCard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnsub_Click(object sender, EventArgs e)
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

                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    lblmsg.Text = "Valid Pan";
                    lblmsg.Visible = true;
                }
                else
                {

                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Invalid Pan";
                    lblmsg.Visible = true;
                }
                newStream.Close();
            }
            catch (Exception ex)
            {
                WriteTextFileLog(ex.Message);
                lblmsg.Text = ex.Message;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message.ToString() + "')", true);
            }
        }
        catch (Exception ex)
        {
            WriteTextFileLog(ex.Message);
            lblmsg.Text = ex.Message;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message.ToString() + "')", true);
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
        catch (Exception ex)
        {
        }
    }
    #endregion
}