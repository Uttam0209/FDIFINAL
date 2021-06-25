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

public partial class Vendor_ViewProfileMigrationList : System.Web.UI.Page
{
    public string VendorID5;
    public string VendorRefNo;
    
    Cryptography ObjEnc = new Cryptography();
    Logic Lo = new Logic();
    DataUtility Co = new DataUtility();
    private object divregistration;
    public string lblusername;
    public string CVRUsertype;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblusername = ObjEnc.DecryptData(Session["User"].ToString());
        //Usertype = ObjEnc.DecryptData(Session["Type"].ToString());
        CVRUsertype = "Vendor";
        //CVRUsertype = "Admin";
        //CVRUsertype = "User";
        GetProfileDetail();
    }

    protected void GetProfileDetail()
    {
        #region Registration User Date Retrive
        DataTable dt = new DataTable();
        dt = GetvendorIDDetail();
        //GetProfileDetail();
        VendorID5 = dt.Rows[0]["VendorID"].ToString();
        string usingdata = "DataMigrationList";
        DataTable DtGetRegisVendor2 = Lo.RetriveProMigrationDetals(VendorID5, usingdata);
        if (DtGetRegisVendor2.Rows.Count > 0)
        {
            GridView1.DataSource = DtGetRegisVendor2;
            GridView1.DataBind();
            GridView2.DataSource = DtGetRegisVendor2;
            GridView2.DataBind();
            GridView3.DataSource = DtGetRegisVendor2;
            GridView3.DataBind();
            
        }
        #endregion
    }
   
    protected DataTable GetvendorIDDetail()
    {
        #region Registration Date Retrive
        DataTable DtGetRegisVendor1 = Lo.RetriveVendorDetal(lblusername);
        if (DtGetRegisVendor1.Rows.Count > 0)
        {
            VendorID5 = DtGetRegisVendor1.Rows[0]["VendorID"].ToString();
            //SvendorID = Session["VendorID"].ToString();
        }
        return DtGetRegisVendor1;
        #endregion
    }

    protected void lbsubmit_Click(object sender, EventArgs e)
    {
        if (txtotp.Text != "" && txtotp.Text == hfotp.Value)
        {
            try
            {
                //saveInfo();
                // cleartext();
                Session.Clear();
                Session.Abandon();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Your interest sent to item releted dpsu's We will contact you soon.'); window.location.href='Productlist';", true);
            }
            catch (Exception ex)
            { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Error occured in send mail please contact admin person.')", true); }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Invalid OTP.')", true);
        }
    }

    #region OTPCode
    protected void GenerateOTP()
    {
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
            hfotp.Value = otp;
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    private void sendMailOTP()
    {
        string body;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/OTP.html")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{OTP}", hfotp.Value);
        SendMail s;
        s = new SendMail();
        s.CreateMail("noreply-srijandefence@gov.in", ObjEnc.DecryptData(Session["User"].ToString()), "OTP Verification Cart.", body);
        s.sendMail();
    }
    protected void lbresendotp_Click(object sender, EventArgs e)
    {
        GenerateOTP();
        sendMailOTP();
    }
    #endregion
}