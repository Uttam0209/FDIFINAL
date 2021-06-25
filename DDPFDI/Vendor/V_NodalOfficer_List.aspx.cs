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

public partial class Vendor_V_NodalOfficer_List : System.Web.UI.Page
{
    public string VendorID5;
    public string VendorRefNo;
    public string UserrName;
    public string userrEmail;
    public string ContactNo;
    public string StreetAddress;
    public string StreetAddressLine2;
    public string City;
    public string State;
    public string ZipCode;
    public string Country;
    public string IsActive;
    public string Updatemobno;
    public string EmnailID;
    public string useremaiID;
    public string userID;
    public string SvendorID;
    public string authri;
    public string identi;
    Cryptography ObjEnc = new Cryptography();
    Logic Lo = new Logic();
    DataUtility Co = new DataUtility();
    private object divregistration;
    public string lblusername;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblusername = ObjEnc.DecryptData(Session["User"].ToString());
        GetProfileDetail();
    }

    protected void GetProfileDetail()
    {
        #region Registration User Date Retrive
        DataTable dt = new DataTable();
        dt = GetvendorIDDetail();
        //GetProfileDetail();
        VendorID5 = dt.Rows[0]["VendorID"].ToString();
        string Type = "NewVendorid";
        DataTable DtGetRegisVendor1 = Lo.RetriveUsersDetals(VendorID5, Type);
        if (DtGetRegisVendor1.Rows.Count > 0)
        {
            UserrName = DtGetRegisVendor1.Rows[0]["UserName"].ToString();
            userrEmail = DtGetRegisVendor1.Rows[0]["UserEmail"].ToString();
            ContactNo = DtGetRegisVendor1.Rows[0]["UserMobile"].ToString();
            StreetAddress = DtGetRegisVendor1.Rows[0]["Address1"].ToString();
            StreetAddressLine2 = DtGetRegisVendor1.Rows[0]["Address2"].ToString();
            City = DtGetRegisVendor1.Rows[0]["City"].ToString();
            State = DtGetRegisVendor1.Rows[0]["State"].ToString();
            ZipCode = DtGetRegisVendor1.Rows[0]["Postalcode"].ToString();
            //VendorID = DtGetRegisVendor1.Rows[0]["VendorID"].ToString();
            //SvendorID = Session["VendorID"].ToString();
        }
        #endregion
    }

    //protected void updateDetails(object sender, EventArgs e)
    //{
    //    DataTable dt = new DataTable();
    //    dt = GetvendorIDDetail();
    //    GetProfileDetail();
    //    VendorID5 = dt.Rows[0]["VendorID"].ToString();
    //    Updatemobno = Co.RSQandSQLInjection(Server.HtmlEncode(VmobileNO.Text), "insrt");

    //    if (Updatemobno == "")
    //    {
    //        Updatemobno = MobNo.Text;
    //    }
    //    EmnailID = Co.RSQandSQLInjection(Server.HtmlEncode(VEmailID.Text), "insrt");
    //    if (EmnailID == "")
    //    {
    //        EmnailID = ObjEnc.DecryptData(Session["User"].ToString());
    //    }
    //    StreetAddress = Co.RSQandSQLInjection(Server.HtmlEncode(NolOffADDRE1.Text), "insrt");

    //    if (StreetAddress == "")
    //    {
    //        StreetAddress = NolOffADDRE.Text.Split(',')[0];
    //    }
    //    StreetAddressLine2 = Co.RSQandSQLInjection(Server.HtmlEncode(NolOffADDRE2.Text), "insrt");

    //    if (StreetAddressLine2 == "")
    //    {
    //        StreetAddressLine2 = NolOffADDRE.Text.Split(',')[1];
    //    }

    //    City = Co.RSQandSQLInjection(Server.HtmlEncode(City1.Text), "insrt");
    //    if (City == "")
    //    {
    //        City = NolOffCity.Text.Split('/')[0];
    //    }
    //    State = Co.RSQandSQLInjection(Server.HtmlEncode(State1.Text), "insrt");
    //    if (State == "")
    //    {
    //        City = NolOffCity.Text.Split('/')[1];
    //    }
    //    ZipCode = Co.RSQandSQLInjection(Server.HtmlEncode(Pincode1.Text), "insrt");

    //    if (ZipCode == "")
    //    {
    //        ZipCode = NolOffCity.Text.Split('/')[2];
    //    }
    //    //SendEmailOTP(ObjEnc.DecryptData(Session["User"].ToString()));
    //    //if (txtOTP.Text == Session["OTP"].ToString())
    //    //{
    //    Lo.UpdateContactVendor(Updatemobno, ObjEnc.DecryptData(Session["User"].ToString()), StreetAddress, StreetAddressLine2, City, State, ZipCode, VendorID5);
    //    Label1.ForeColor = System.Drawing.Color.DarkGreen;
    //    Label1.Text = "Your Changes Successfully Updated";
    //    //}

    //    //else
    //    //{
    //    //    lblmssg.Text = "Please enter a Vailed OTP.";
    //    //}
    //    //Sett
    //}


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