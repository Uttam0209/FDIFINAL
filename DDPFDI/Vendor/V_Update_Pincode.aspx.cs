﻿using System;
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
public partial class Vendor_V_Update_Pincode : System.Web.UI.Page
{
    public string VendorEmailID;
    public string UserName;
    public string NodalOfficerEmail;
    public string ContactNo;
    public string StreetAddress;
    public string StreetAddressLine2;
    public string City;
    public string State;
    public string ZipCode;
    public string VendorIMGID;
    public string Userpassword;
    public string NodelOfficercurrentEmailID;
    public string userType;
    public string postaladdress;
    Cryptography ObjEnc = new Cryptography();
    Logic Lo = new Logic();
    DataUtility Co = new DataUtility();
    private object divregistration;

    public object LO { get; private set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            sugpin.Attributes.Add("maxlength", "6");
            Postofficename.Attributes.Add("maxlength", "300");
            sugcity.Attributes.Add("maxlength", "30");
            sugstate.Attributes.Add("maxlength", "30");
            NodelOfficercurrentEmailID = ObjEnc.DecryptData(Session["User"].ToString());
            GetAllPincode();
        }
    }

    protected void AddCity(object sender, EventArgs e)
    {

        DataTable dt = new DataTable();
        dt = GetvendorIDDetail();
        VendorIMGID = dt.Rows[0]["VendorID"].ToString();
        

        City = Co.RSQandSQLInjection(Server.HtmlEncode(sugcity.Text), "insrt");

        
        State = Co.RSQandSQLInjection(Server.HtmlEncode(sugstate.Text), "insrt");
        
        ZipCode = ddlPin.SelectedItem.Text;


        if (NodelOfficercurrentEmailID == "")
        {

        }
        //SendEmailOTP(ObjEnc.DecryptData(Session["User"].ToString()));
        //if (txtOTP.Text == Session["OTP"].ToString())
        //{
        else
        {

            //GenerateOTP();
            //sendMailOTP();
            ScriptManager.RegisterStartupScript(this, GetType(), "modelotp", "showPopup1();", true);
            Lo.AddLatestCity(City, State, ZipCode);

            Label1.ForeColor = System.Drawing.Color.DarkGreen;
            Label1.Text = "<div class='alert alert-success alert-dismissible ' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button><strong!</strong>Latest City with Postal Code successfully inserted</div>";
        }

        //}

        //else {
        //    lblmssg.Text = "Please enter a Vailed OTP.";
        //}
        //Sett
    }

    protected void AddPincode(object sender, EventArgs e)
    {

        DataTable dt = new DataTable();
        dt = GetvendorIDDetail();
        VendorIMGID = dt.Rows[0]["VendorID"].ToString();

        postaladdress = Co.RSQandSQLInjection(Server.HtmlEncode(Postofficename.Text), "insrt");

        ZipCode = Co.RSQandSQLInjection(Server.HtmlEncode(sugpin.Text), "insrt");


        if (NodelOfficercurrentEmailID == "")
        {

        }
        //SendEmailOTP(ObjEnc.DecryptData(Session["User"].ToString()));
        //if (txtOTP.Text == Session["OTP"].ToString())
        //{
        else
        {

            //GenerateOTP();
            //sendMailOTP();
            ScriptManager.RegisterStartupScript(this, GetType(), "modelotp", "showPopup1();", true);
            Lo.AddLatestPincode(postaladdress, ZipCode);

            Label2.ForeColor = System.Drawing.Color.DarkGreen;
            Label2.Text = "<div class='alert alert-success alert-dismissible ' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button><strong!</strong> Postal Code successfully inserted</div>";
        }

        //}

        //else {
        //    lblmssg.Text = "Please enter a Vailed OTP.";
        //}
        //Sett
    }
    protected DataTable GetvendorIDDetail()
    {
        #region Registration Date Retrive
        DataTable DtGetRegisVendor1 = Lo.RetriveVendorDetal(ObjEnc.DecryptData(Session["User"].ToString()));
        if (DtGetRegisVendor1.Rows.Count > 0)
        {
            VendorIMGID = DtGetRegisVendor1.Rows[0]["VendorID"].ToString();
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

    public void GetAllPincode()
    {
        DataTable dtpincode = Lo.GetPincode();
        ddlPin.DataSource = dtpincode;
        ddlPin.DataTextField = "PinCode";
        ddlPin.DataBind();
        ddlPin.Items.Insert(0, "Select");
    }
    protected void ddlPincode_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtCity = Lo.GetCitybyPin(ddlPin.SelectedItem.Text);  
    }
    #region AutoComplete PinCode
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetPincode(string prefix)
    {
        Cryptography objCrypto1 = new Cryptography();
        List<string> customers = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = objCrypto1.DecryptData(ConfigurationManager.ConnectionStrings["connectiondb"].ConnectionString);
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select PinCode  from tbl_mst_PinCode  where PinCode like @SearchText + '%' and IsActive='Y'";
                cmd.Parameters.AddWithValue("@SearchText", prefix);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}", sdr["OEMName"], sdr["ProductID"]));
                    }
                }
                conn.Close();
            }
        }
        return customers.ToArray();
    }
    #endregion


}