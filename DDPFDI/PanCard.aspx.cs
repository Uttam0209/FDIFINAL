using BusinessLayer;
using Encryption;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using Encryption;

public partial class PanCard : System.Web.UI.Page
{
    Cryptography Enc = new Cryptography();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public Boolean AcceptAllCertifications(Object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }
    protected void txtpanno_TextChanged(object sender, EventArgs e)
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
            char[] deli = { '^'};
            string[] split = str.Split(deli);
            string a = split[0];
            string b = split[1];
            string d = split[3];
            string E = split[4];    
            string g = split[6];
            string h = split[7];
            string i = split[8];         
            string k = split[10]; 
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message.ToString() + "')", true);
        }
    }

    protected void btnenc_Click(object sender, EventArgs e)
    {
        if (TXTDEC.Text != "")
            lblmsg.Text= Enc.EncryptData(TXTDEC.Text);
    }

    protected void btndec_Click(object sender, EventArgs e)
    {
        if(TXTDEC.Text!="")
        lblmsg.Text = Enc.DecryptData(TXTDEC.Text);
    }
}