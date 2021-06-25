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
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Serialization;

public partial class Vendor_V_Add_User : System.Web.UI.Page
{
    public string VendorEmailID;
    public string NodalOfficerName;
    public string NodalOfficerEmail;
    public string ContactNo;
    public string StreetAddress;
    public string StreetAddressLine2;
    public string City;
    public string State;
    public string ZipCode;
    public string VendorIMGID;
    public string NodelOfficercurrentEmailID;
    public string liveNewsurl;
    public string trackip;
    public string trackcountry;
    public string trackstate;
    public string vendorRefferNO;
    Cryptography ObjEnc = new Cryptography();
    Logic Lo = new Logic();
    DataUtility Co = new DataUtility();
    private object divregistration;
    bool MsgStatus;
    Int64 MId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetAllState();
            VmobileNO.Attributes.Add("maxlength", "10");
            NolOffName.Attributes.Add("maxlength", "30");
            VEmailID.Attributes.Add("maxlength", "50");
            NolOffADDRE1.Attributes.Add("maxlength", "500");
            NolOffADDRE2.Attributes.Add("maxlength", "250");
            sugcity.Attributes.Add("maxlength", "30");
            sugstate.Attributes.Add("maxlength", "30");
            sugpin.Attributes.Add("maxlength", "6");
            txtotpVery.Attributes.Add("maxlength", "6");
          

        }
        liveNewsurl = HttpContext.Current.Request.Url.AbsoluteUri;
        NodelOfficercurrentEmailID = ObjEnc.DecryptData(Session["User"].ToString());
    }

    protected void profileMigrat(object sender, EventArgs e)
    {
        MYotpPM.Visible = true;
        GenerateOTP();
        sendMailOTP2(NodelOfficercurrentEmailID);
        //bool msg = mytestemail();
        //if (msg == true)
        //{
        //    MYotpPM.Visible = true;
        //    ScriptManager.RegisterStartupScript(this, GetType(), "modelotpPM", "openmypopup();", true);
        //}
        DataTable dt = new DataTable();
        dt = GetvendorIDDetail();
        VendorIMGID = dt.Rows[0]["VendorID"].ToString();
        NodalOfficerName = Co.RSQandSQLInjection(Server.HtmlEncode(NolOffName.Text), "insrt");
        ContactNo = Co.RSQandSQLInjection(Server.HtmlEncode(VmobileNO.Text), "insrt");
        VendorEmailID = Co.RSQandSQLInjection(Server.HtmlEncode(VEmailID.Text), "insrt");
        StreetAddress = Co.RSQandSQLInjection(Server.HtmlEncode(NolOffADDRE1.Text), "insrt");
        StreetAddressLine2 = Co.RSQandSQLInjection(Server.HtmlEncode(NolOffADDRE2.Text), "insrt");
        //City = Co.RSQandSQLInjection(Server.HtmlEncode(NolOffCity.Text), "insrt");
        //State = Co.RSQandSQLInjection(Server.HtmlEncode(NolOffstate.Text), "insrt");
        //ZipCode = Co.RSQandSQLInjection(Server.HtmlEncode(NolOffpincode.Text), "insrt");


        City = Co.RSQandSQLInjection(Server.HtmlEncode(sugcity.Text), "insrt");

        if (City == "")
        {
            City = ddlCity.SelectedItem.Text;
        }
        State = Co.RSQandSQLInjection(Server.HtmlEncode(sugstate.Text), "insrt");
        if (State == "")
        {
            State = ddlstate.SelectedItem.Text;
        }
        ZipCode = Co.RSQandSQLInjection(Server.HtmlEncode(sugpin.Text), "insrt");

        if (ZipCode == "")
        {
            ZipCode = ddlPincode.SelectedItem.Text;
        }
        string Authorization = string.Empty;
        if (FileUpload1.HasFile)
        {
            Authorization = System.DateTime.Now.ToString("ddMMyyhhmmss") + FileUpload1.FileName.ToString();

            //string ext = Path.GetExtension(Authorization);

            //if (ext == ".pdf")
            //{
            string uploadFolderPath = "~/Upload/";
            string filePath = HttpContext.Current.Server.MapPath(uploadFolderPath);
            FileUpload1.SaveAs(filePath + "\\" + Authorization);

            Session["Auth"] = Authorization;
            //}
            //else 
            //{
            //    Label1.Text = "Please select PDF File";
            //}
        }
        string Identity = string.Empty;
        if (FileUpload2.HasFile)
        {
            Identity = System.DateTime.Now.ToString("ddMMyyhhmmss") + FileUpload2.FileName.ToString();
            //string ext = Path.GetExtension(Identity);

            //if (ext == ".pdf")
            //{
            string uploadFolderPath1 = "~/Upload/";
            string filePath = HttpContext.Current.Server.MapPath(uploadFolderPath1);
            FileUpload2.SaveAs(filePath + "\\" + Identity);
            Session["iden"] = Identity;
            //}
            //else {
            //    Label1.Text = "Please select PDF File";
            //}
        }

       


        if (Session["OTPM"].ToString() == txtotpVery.Text)
        {

            //GenerateOTP();
            //sendMailOTP();
            //sendMailOTP2();
            //ScriptManager.RegisterStartupScript(this, GetType(), "modelotp", "showPopup1();", true);
            // Lo.ProfileMigrationVendor(NodalOfficerName, ContactNo, VendorEmailID, StreetAddress, StreetAddressLine2, City, State, ZipCode, VendorIMGID, Authorization, Identity);

            Label1.ForeColor = System.Drawing.Color.DarkGreen;
            Label1.Text = "<div class='alert alert-success alert-dismissible ' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button><strong>!</strong> Record Successfully Inserted</div>";
        }
        else
        {
            Label1.Text = "We have sent an OTP to registered email id";
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

            Session["companyName"] = DtGetRegisVendor1.Rows[0]["V_CompName"].ToString();
            Session["panCard"] = DtGetRegisVendor1.Rows[0]["PanNo"].ToString();
            Session["GstNo"] = DtGetRegisVendor1.Rows[0]["GSTNo"].ToString();
            //SvendorID = Session["VendorID"].ToString();
        }
        return DtGetRegisVendor1;
        #endregion
    }

    protected void lbsubmit_Click(object sender, EventArgs e)
    {
        if (txtotp.Text != "" && txtotp.Text == PMotp.Value)
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
            PMotp.Value = otp;
            Session["OTPM"] = PMotp.Value;
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
        body = body.Replace("{OTP}", PMotp.Value);
        SendMail s;
        s = new SendMail();
        s.CreateMail("noreply-srijandefence@gov.in", ObjEnc.DecryptData(Session["User"].ToString()), "OTP Verification Profile Migration", body);
        s.sendMail();
    }

    private void sendMailOTP2(string mailid)
    {
        try
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/VenOTP.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{OTP}", PMotp.Value);
            //body = body.Replace("{UserIP}", Session["TIP"].ToString());
            //body = body.Replace("{Usercity}", Session["TST"].ToString());
            //body = body.Replace("{UserCountry}", Session["TCOU"].ToString());
            SendMail s;
            s = new SendMail();
            s.CreateMail("noreply-srijandefence@gov.in", mailid, "OTP Verification Add New User", body);
            s.sendMail();
           // MYotpPM.Visible = true;

        }
        
         catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }

    private void sendMailOTP3(string mailid)
    {
        try
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/Userupdatepassword.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", VendorEmailID);
            body = body.Replace("{refno}", HttpUtility.UrlEncode(ObjEnc.EncryptData(Session["NewVendorid"].ToString())));
            body = body.Replace("{mcurid}", Resturl(56));
            SendMail s;
            s = new SendMail();
            s.CreateMail("noreply-srijandefence@gov.in", VendorEmailID, "New User : RAKSHA UDYOG MITRA PORTAL", body);
            s.sendMail();
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }

    }
    public void SendEmailCodeRegis(string EmailID)
    {
        try
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/VenOTP.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{OTP}", PMotp.Value);
            body = body.Replace("{UserIP}", Session["TIP"].ToString());
            body = body.Replace("{Usercity}", Session["TST"].ToString());
            body = body.Replace("{UserCountry}", Session["TCOU"].ToString());

            SendMail s;
            s = new SendMail();
            s.CreateMail("noreply-srijandefence@gov.in", ObjEnc.DecryptData(Session["User"].ToString()), "OTP Verification Add User", body);
            s.sendMail();
            MYotpPM.Visible = true;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }

    }

    public void SendEmailCodeRegistwo(string EmailID2)
    {
        try
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/Userupdatepassword.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", VendorEmailID);
            body = body.Replace("{refno}", HttpUtility.UrlEncode(ObjEnc.EncryptData(Session["NewVendorid"].ToString())));
            body = body.Replace("{mcurid}", Resturl(56));
            SendMail s;
            s = new SendMail();
            s.CreateMail("noreply-srijandefence@gov.in", VendorEmailID, "New User : RAKSHA UDYOG MITRA PORTAL", body);
            s.sendMail();
            MYotpPM.Visible = true;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }

    }
    private void sendMailOTP2()
    {
        string body;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/OTP.html")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{OTP}", PMotp.Value);
        SendMail M;
        M = new SendMail();
        M.CreateMail("noreply-srijandefence@gov.in", VendorEmailID, "We have Migrating Profile With You Please follow Profile Migration", body);
        M.sendMail();
    }
    protected void lbresendotp_Click(object sender, EventArgs e)
    {
        GenerateOTP();
        sendMailOTP();
    }
    #endregion
    protected void btn_VOTP_Click(object sender, EventArgs e)
    {
        verifyemailid(VEmailID.Text);
        if (Session["verifuEmail"].ToString() == "")
        {
            vendorRefferNO = "25125" + "" + Session["OTPM"].ToString();
            Session["NewVendorid"] = vendorRefferNO;
            string registCAT = "MANUFACTURER";
            string TypeOfBuisness = "1";
            string BusinessSector = "11";
            string Country = "97";
            string MasterAllowed = "21,20";
            string IsActive = "Y";
            string IsLoginActive = "Y";
            string DefaultPage = "Profile_Management";
            string Type = "User";
            string RecInsTime = "2021-10-31 16:09:13.953";

            string otpProfileM = Co.RSQandSQLInjection(Server.HtmlEncode(txtotpVery.Text), "insrt");
            string abc = Session["OTPM"].ToString();
            if (abc == Session["OTPM"].ToString())
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Email Change successfully.Please login with new New EmailID. We will redirected to you login page');", abc == Session["OTPM"].ToString());
                DataTable dt = new DataTable();
                dt = GetvendorIDDetail();
                VendorIMGID = dt.Rows[0]["VendorID"].ToString();
                NodalOfficerName = Co.RSQandSQLInjection(Server.HtmlEncode(NolOffName.Text), "insrt");
                ContactNo = Co.RSQandSQLInjection(Server.HtmlEncode(VmobileNO.Text), "insrt");
                VendorEmailID = Co.RSQandSQLInjection(Server.HtmlEncode(VEmailID.Text), "insrt");
                StreetAddress = Co.RSQandSQLInjection(Server.HtmlEncode(NolOffADDRE1.Text), "insrt");
                StreetAddressLine2 = Co.RSQandSQLInjection(Server.HtmlEncode(NolOffADDRE2.Text), "insrt");
                string CompanyName = Session["companyName"].ToString();
                string PanNO = Session["panCard"].ToString();
                string GSTno = Session["GstNo"].ToString();


                City = Co.RSQandSQLInjection(Server.HtmlEncode(sugcity.Text), "insrt");

                if (City == "")
                {
                    City = ddlCity.SelectedItem.Text;
                }
                State = Co.RSQandSQLInjection(Server.HtmlEncode(sugstate.Text), "insrt");
                if (State == "")
                {
                    State = ddlstate.SelectedItem.Text;
                }
                ZipCode = Co.RSQandSQLInjection(Server.HtmlEncode(sugpin.Text), "insrt");

                if (ZipCode == "")
                {
                    ZipCode = ddlPincode.SelectedItem.Text;
                }
                //Userpassword = Co.RSQandSQLInjection(Server.HtmlEncode(Password.Text), "insrt");
                string Authorization = string.Empty;
                if (FileUpload1.HasFile)
                {
                    Authorization = System.DateTime.Now.ToString("ddMMyyhhmmss") + FileUpload1.FileName.ToString();

                    //string ext = Path.GetExtension(Authorization);

                    //if (ext == ".pdf")
                    //{
                    string uploadFolderPath = "~/Upload/";
                    string filePath = HttpContext.Current.Server.MapPath(uploadFolderPath);
                    FileUpload1.SaveAs(filePath + "\\" + Authorization);
                    //}
                    //else
                    //{
                    //    Label1.Text = "Please select PDF File";
                    //}
                }
                string Identity = string.Empty;
                if (FileUpload2.HasFile)
                {
                    Identity = System.DateTime.Now.ToString("ddMMyyhhmmss") + FileUpload2.FileName.ToString();
                    //string ext = Path.GetExtension(Identity);

                    //if (ext == ".pdf")
                    //{
                    string uploadFolderPath1 = "~/Upload/";
                    string filePath = HttpContext.Current.Server.MapPath(uploadFolderPath1);
                    FileUpload2.SaveAs(filePath + "\\" + Identity);
                    //}
                    //else
                    //{
                    //    Label1.Text = "Please select PDF File";
                    //}
                }

                Lo.NewVendorregistrationVendor(NodalOfficerName, ContactNo, VendorEmailID, StreetAddress, StreetAddressLine2, City, State, ZipCode, VendorIMGID, Session["Auth"].ToString(), Session["iden"].ToString(), vendorRefferNO, registCAT, TypeOfBuisness, BusinessSector, Country, MasterAllowed, IsActive, IsLoginActive, DefaultPage, Type, RecInsTime, CompanyName, PanNO, GSTno);


                sendMailOTP3(VendorEmailID);
                //Label1.ForeColor = System.Drawing.Color.DarkGreen;
                //Label1.Text = "<font color='Green'><b>We have sent a confirmation email to New User</b></font>";

               // bool msg = mytestemail2();
                MYotpPM.Visible = false;
                //SendEmailCodeRegistwo(VendorEmailID);
                //sendMailOTP3(VendorEmailID);
                //Response.Redirect(liveNewsurl);
                //bool msg = mytestemail2();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('We have sent a confirmation email to New User.'); window.location.href='" + liveNewsurl + "';", true);
               


            }
            else
            {
                mylable.Text = "Please enter correct OTP";
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('This email already exist in our portal. Please use another email'); window.location.href='" + liveNewsurl + "';", true);

        }
    }

    public bool mytestemail()
    {
        string to = ObjEnc.DecryptData(Session["User"].ToString()); //To address    
        string from = "dobhal.naveen17@gmail.com"; //From address    
        MailMessage message = new MailMessage(from, to);
        string mailbody = "";
        using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/VenOTP.html")))
        {
            mailbody = reader.ReadToEnd();
        }
        mailbody = mailbody.Replace("{OTP}", PMotp.Value);
        Session["OTPM"] = PMotp.Value;
     
        message.Subject = "Profile Migration";
        message.Body = mailbody;
        message.BodyEncoding = Encoding.UTF8;
        message.IsBodyHtml = true;
        SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
        System.Net.NetworkCredential basicCredential1 = new
        System.Net.NetworkCredential("dobhal.naveen17@gmail.com", "#");
        client.EnableSsl = true;
        client.UseDefaultCredentials = false;
        client.Credentials = basicCredential1;
        try
        {
            client.Send(message);
            MsgStatus = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return MsgStatus;
    }

    public bool mytestemail2()
    {
        string to = VendorEmailID; //To address    
        string from = "dobhal.naveen17@gmail.com"; //From address    
        MailMessage message = new MailMessage(from, to);
        string mailbody = "";
        using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/Userupdatepassword.html")))
        {
            mailbody = reader.ReadToEnd();
        }
        mailbody = mailbody.Replace("{UserName}", VendorEmailID);
        mailbody = mailbody.Replace("{refno}", HttpUtility.UrlEncode(ObjEnc.EncryptData(Session["NewVendorid"].ToString())));
        mailbody = mailbody.Replace("{mcurid}", Resturl(56));
        SendMail s;
        s = new SendMail();
        //mailbody = mailbody.Replace("{UserName}", ObjEnc.DecryptData(Session["User"].ToString()));
        //mailbody = mailbody.Replace("{refno}", HttpUtility.UrlEncode(ObjEnc.DecryptData(Session["User"].ToString())));
        mailbody = mailbody.Replace("{mcurid}", Resturl(56));
        message.Subject = "New User : RAKSHA UDYOG MITRA PORTAL";
        message.Body = mailbody;
        message.BodyEncoding = Encoding.UTF8;
        message.IsBodyHtml = true;
        SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
        System.Net.NetworkCredential basicCredential1 = new
        System.Net.NetworkCredential("dobhal.naveen17@gmail.com", "#");
        client.EnableSsl = true;
        client.UseDefaultCredentials = false;
        client.Credentials = basicCredential1;
        try
        {
            client.Send(message);
            MsgStatus = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return MsgStatus;
    }


    #region ReturnUrl"
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
    protected void closebtn(object sender, EventArgs e)
    {
        Response.Redirect(liveNewsurl);
    }
    public void GetAllPincode()
    {
        DataTable dtpincode = Lo.GetPincode();
        ddlPincode.DataSource = dtpincode;
        ddlPincode.DataTextField = "PinCode";
        ddlPincode.DataValueField = "PinCode";
        ddlPincode.DataBind();
        ddlPincode.Items.Insert(0, "Select");
    }
    public void GetAllCity()
    {
        DataTable dtAllCity = Lo.GetCity();
        ddlCity.DataSource = dtAllCity;
        ddlCity.DataTextField = "CityName";
        ddlCity.DataValueField = "CityCode";
        ddlCity.DataBind();
        ddlCity.Items.Insert(0, "Select");
    }
    public void GetAllState()
    {
        DataTable dtAllState = Lo.GetState();
        ddlstate.DataSource = dtAllState;
        ddlstate.DataTextField = "StateName";
        ddlstate.DataValueField = "StateId";
        ddlstate.DataBind();
        ddlstate.Items.Insert(0, "Select");
    }
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtState = Lo.GetStateByCity(ddlCity.SelectedValue);
        ddlPincode.DataSource = dtState;
        ddlPincode.DataTextField = "PinCode";
        ddlPincode.DataValueField = "PinCode";
        ddlPincode.DataBind();

        ddlPincode.Items.Insert(0, "Select");
    }

    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtCity = Lo.GetCitybyPin(ddlstate.SelectedValue);
        ddlCity.DataSource = dtCity;
        ddlCity.DataTextField = "CityName";
        ddlCity.DataValueField = "CityCode";
        ddlCity.DataBind();
        ddlCity.Items.Insert(0, "Select");
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



    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetPin(string prefix, int parentId)
    {
        SqlCommand cmd = new SqlCommand();
        string query = "select PinCode,PinCode from tbl_mst_PinCode where PinCode LIKE @Prefix + '%'";
        cmd.Parameters.AddWithValue("@Prefix", prefix);
        cmd.CommandText = query;
        return PopulateAutoComplete(cmd);
    }



    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetCities(string prefix, string parentId)
    {
        SqlCommand cmd = new SqlCommand();
        string query = "select CityName,StateId from tbl_mst_CityMaster where CityName LIKE @Prefix + '%' and PinCode=@Pincode";
        cmd.Parameters.AddWithValue("@Prefix", prefix);
        cmd.Parameters.AddWithValue("@Pincode", parentId);
        cmd.CommandText = query;
        return PopulateAutoComplete(cmd);



    }

    private static string[] PopulateAutoComplete(SqlCommand cmd)
    {
        Cryptography objCrypto1 = new Cryptography();
        List<string> autocompleteItems = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            // conn.ConnectionString = ConfigurationManager.ConnectionStrings["connectiondb"].ConnectionString;
            conn.ConnectionString = objCrypto1.DecryptData(ConfigurationManager.ConnectionStrings["connectiondb"].ConnectionString);
            cmd.Connection = conn;
            conn.Open();
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                while (sdr.Read())
                {
                    autocompleteItems.Add(string.Format("{0}-{1}", sdr[0], sdr[1]));
                }
            }
            conn.Close();
        }
        return autocompleteItems.ToArray();
    }



    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetPinCodeService1(string prefix)
    {
        Cryptography objCrypto1 = new Cryptography();
        List<string> customers = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = objCrypto1.DecryptData(ConfigurationManager.ConnectionStrings["connectiondb"].ConnectionString);
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select PinCode,PinCode from tbl_mst_PinCode where PinCode like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefix);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}", sdr["PinCode"], sdr["PinCode"]));
                        //customers.Add(string.Format("{0}", sdr["PinCode"]));
                    }
                }
                conn.Close();
            }
        }
        return customers.ToArray();
    }
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetCityService1(string prefix)
    {
        Cryptography objCrypto1 = new Cryptography();
        List<string> customers1 = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = objCrypto1.DecryptData(ConfigurationManager.ConnectionStrings["connectiondb"].ConnectionString);
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select CityName,StateId from tbl_mst_CityMaster where CityName like '%' + @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefix);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers1.Add(string.Format("{0}", sdr["CityName"]));
                    }
                }
                conn.Close();
            }
        }
        return customers1.ToArray();
    }

    private void verifyemailid(string mailid)
    {
        #region Get email ID Verifier
        DataTable DtGetverifyemaiid = Lo.verifyemailIDVendor(mailid);
        if (DtGetverifyemaiid.Rows.Count > 0)
        {
            Session["verifuEmail"] = DtGetverifyemaiid.Rows[0]["VendorID"].ToString();

        }
        else
        {

            Session["verifuEmail"] = DtGetverifyemaiid.ToString();

        }

        #endregion
    }

}