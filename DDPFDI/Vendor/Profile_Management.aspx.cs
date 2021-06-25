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
using System.Web.SessionState;
public partial class Vendor_Profile_Management : System.Web.UI.Page
{
    public string VendorID5;
    public string VendorRefNo;
    public string NodalOfficerName;
    public string NodalOfficerEmail;
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
    public string otpProfileM;
    public string trackip;
    public string trackcountry;
    public string trackstate;
    Cryptography ObjEnc = new Cryptography();
    Logic Lo = new Logic();
    DataUtility Co = new DataUtility();
    private object divregistration;
    bool MsgStatus;
    Int64 MId = 0;
    public string liveNewsurl;
    public object NolOff { get; private set; }
    public string usertype;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] != null)
        {
            if (!IsPostBack)
            {
                VmobileNO.Attributes.Add("maxlength", "10");
                NolOffName.Attributes.Add("maxlength", "30");
                VEmailID.Attributes.Add("maxlength", "50");
                NolOffADDRE1.Attributes.Add("maxlength", "500");
                NolOffADDRE2.Attributes.Add("maxlength", "250");
                NCity.Attributes.Add("maxlength", "30");
                NState.Attributes.Add("maxlength", "30");
                Npincode1.Attributes.Add("maxlength", "6");
                txtotpVery.Attributes.Add("maxlength", "6");
                userID = Session["User"].ToString();
                lblusername.Text = ObjEnc.DecryptData(Session["User"].ToString());
                GetProfileDetail();
              GetAllState();


            }
        }
        usertype = ObjEnc.DecryptData(Session["Type"].ToString());
        liveNewsurl = HttpContext.Current.Request.Url.AbsoluteUri;
    }
    protected void GetProfileDetail()
    {
        #region Registration Date Retrive
        DataTable DtGetRegisVendor1 = Lo.RetriveVendorDetal(ObjEnc.DecryptData(Session["User"].ToString()));
        if (DtGetRegisVendor1.Rows.Count > 0)
        {
            MobNo.Text = DtGetRegisVendor1.Rows[0]["ContactNo"].ToString();
            NolOffName.Text = DtGetRegisVendor1.Rows[0]["NodalOfficerName"].ToString();
            NolOffName1.Text = DtGetRegisVendor1.Rows[0]["NodalOfficerName"].ToString();
            NolOffADDRE.Text = DtGetRegisVendor1.Rows[0]["StreetAddress"].ToString() + " , " + DtGetRegisVendor1.Rows[0]["StreetAddressLine2"].ToString();
            NolOffCity.Text = DtGetRegisVendor1.Rows[0]["City"].ToString() + " / " + DtGetRegisVendor1.Rows[0]["State"].ToString() + " / " + DtGetRegisVendor1.Rows[0]["ZipCode"].ToString();
            authri = DtGetRegisVendor1.Rows[0]["AuthorizationLetter"].ToString();
            identi = DtGetRegisVendor1.Rows[0]["IdentityCard"].ToString();
            //NCity = DtGetRegisVendor1.Rows[0]["City"].ToString();
            //NCity = NolOffCity.Text;
            City = NolOffCity.Text.Split('/')[0];
            State = NolOffCity.Text.Split('/')[1];
            ZipCode = NolOffCity.Text.Split('/')[2];
            NolOffName1.Attributes.Add("readonly", "readonly");
            // NolOffCity.Text = DtGetRegisVendor1.Rows[0]["City"].ToString();// City;
            //VendorID = DtGetRegisVendor1.Rows[0]["VendorID"].ToString();
            //SvendorID = Session["VendorID"].ToString();
        }
        #endregion
    }
    protected void sendemaliforOTP(object sender, EventArgs e)
    {
        GenerateOTP();
        bool msg = mytestemail();
        if (msg == true)
        {
            MYotpPM.Visible = true;
            //ScriptManager.RegisterStartupScript(this, GetType(), "modelotpPM", "openmypopup();", true);
        }

    }

    protected void updateDetails(object sender, EventArgs e)
    {
        MYotpPM.Visible = true;
        //otpProfileM = txtotpVery.Text;
        GenerateOTP();
        sendMailOTP2(lblusername.Text);
        //bool msg = mytestemail();
        //if (msg == true)
        //{
        //   // MYotpPM.Visible = true;
        //    //ScriptManager.RegisterStartupScript(this, GetType(), "modelotpPM", "openmypopup();", true);
        //}

        DataTable dt = new DataTable();
        dt = GetvendorIDDetail();
        GetProfileDetail();
        VendorID5 = dt.Rows[0]["VendorID"].ToString();
        Updatemobno = Co.RSQandSQLInjection(Server.HtmlEncode(VmobileNO.Text), "insrt");

        if (Updatemobno == "")
        {
            Updatemobno = MobNo.Text;
        }
        EmnailID = Co.RSQandSQLInjection(Server.HtmlEncode(VEmailID.Text), "insrt");
        if (EmnailID == "")
        {
            EmnailID = ObjEnc.DecryptData(Session["User"].ToString());
        }
        
        StreetAddress = Co.RSQandSQLInjection(Server.HtmlEncode(NolOffADDRE1.Text), "insrt");

        if (StreetAddress == "")
        {
            StreetAddress = NolOffADDRE.Text.Split(',')[0];
        }
        StreetAddressLine2 = Co.RSQandSQLInjection(Server.HtmlEncode(NolOffADDRE2.Text), "insrt");

        if (StreetAddressLine2 == "")
        {
            StreetAddressLine2 = NolOffADDRE.Text.Split(',')[1];
        }

        if (ddlstate.SelectedItem.Text != "Select")
        {
            City = ddlCity.SelectedItem.Text;
            if (City == "")
            {
                City = NolOffCity.Text.Split('/')[0];
            }
            State = ddlstate.SelectedItem.Text;
            if (State == "")
            {
                State = NolOffCity.Text.Split('/')[1];
            }
            ZipCode = ddlPincode.SelectedItem.Text;

            if (ZipCode == "")
            {
                ZipCode = NolOffCity.Text.Split('/')[2];
            }
        }

        

        //GenerateOTP();
        //    bool msg = mytestemail();
        //    if (msg == true)
        //    {
        //        MYotpPM.Visible = true;
        //        //ScriptManager.RegisterStartupScript(this, GetType(), "modelotpPM", "openmypopup();", true);
        //    }
        //SendEmailOTP(ObjEnc.DecryptData(Session["User"].ToString()));
        //if (txtOTP.Text == Session["OTP"].ToString())
        //{

        //if (lblusername.Text == "")
        //{PMotp

        //}
        //else
        //{
        //GenerateOTP();
        //sendMailOTP();
        if (Session["OTPM"].ToString() == txtotpVery.Text)
        {
            Lo.UpdateContactVendor(Updatemobno, ObjEnc.DecryptData(Session["User"].ToString()), StreetAddress, StreetAddressLine2, City, State, ZipCode, VendorID5);
            Label1.ForeColor = System.Drawing.Color.DarkGreen;
            Label1.Text = "<div class='alert alert-success alert-dismissible ' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button><strong>!</strong>Your Change Successfully Updated</div>";
        }
        else
        {
            Label1.Text = "";
            //ScriptManager.RegisterStartupScript(this, GetType(), "modelotpPM", "openmypopup();", true);
        }
        //}
        //}

        //else
        //{
        //    lblmssg.Text = "Please enter a Vailed OTP.";
        //}
        //Sett
    }
    
    //protected void uploadAuthrizationL(object sender, EventArgs e)
    //{
    //    //GenerateOTP();
    //    //bool msg = mytestemail();
    //    //if (msg == true)
    //    //{
    //    //    MYotpPM.Visible = true;
    //    //    //ScriptManager.RegisterStartupScript(this, GetType(), "modelotpPM", "openmypopup();", true);
    //    //}
    //    string fileName = string.Empty;
    //    if (FileUpload1.HasFile == false)

    //    {

    //        Label2.ForeColor = System.Drawing.Color.DarkGreen;
    //        Label2.Text = "<div class='alert alert-success alert-dismissible ' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button><strong>!</strong>Please select File</div>";
    //    }
    //    else
    //    {
    //        fileName = System.DateTime.Now.ToString("ddMMyyhhmmss") + FileUpload1.FileName.ToString();
    //        string uploadFolderPath = "~/AutoNewsIMG/";
    //        string filePath = HttpContext.Current.Server.MapPath(uploadFolderPath);
    //        FileUpload1.SaveAs(filePath + "\\" + fileName);

    //        Lo.UpdateAuthrizationLVendor(fileName, ObjEnc.DecryptData(Session["User"].ToString()));

    //        Label2.ForeColor = System.Drawing.Color.DarkGreen;
    //        Label2.Text = "<div class='alert alert-success alert-dismissible ' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button><strong>!</strong>Authorization Letter Successfully Uploded</div>";

    //    }

    //    //if (Session["OTPM"].ToString() == txtotpVery.Text)
    //    //{
    //    //Lo.UpdateAuthrizationLVendor(fileName, ObjEnc.DecryptData(Session["User"].ToString()));

    //    //Label2.ForeColor = System.Drawing.Color.DarkGreen;
    //    //Label2.Text = "<div class='alert alert-success alert-dismissible ' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button><strong>!</strong>Authorization Letter Successfully Uploded</div>";
        
    //}

    //protected void uploadidentity(object sender, EventArgs e)
    //{
    //    string Identity = string.Empty;
    //    if (FileUpload2.HasFile)
    //    {
    //        Identity = System.DateTime.Now.ToString("ddMMyyhhmmss") + FileUpload2.FileName.ToString();
    //        //string ext = Path.GetExtension(Identity);

    //        //if (ext == ".pdf")
    //        //{
    //            string uploadFolderPath = "~/Uploaded/";
    //            string filePath = HttpContext.Current.Server.MapPath(uploadFolderPath);
    //            FileUpload2.SaveAs(filePath + "\\" + Identity);
    //        //}

    //        //else
    //        //{
    //        //    Label2.Text = "Please select PDF file only";
    //        //}

    //    }

    //    Lo.UpdateIdentityVendor(Identity, ObjEnc.DecryptData(Session["User"].ToString()));
    //    Label5.ForeColor = System.Drawing.Color.DarkGreen;
    //    Label5.Text = "<div class='alert alert-success alert-dismissible ' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button><strong>!</strong>Identity Card Successfully Uploded</div>";
    //}


    protected DataTable GetvendorIDDetail()
    {
        #region Registration Date Retrive
        DataTable DtGetRegisVendor1 = Lo.RetriveVendorDetal(ObjEnc.DecryptData(Session["User"].ToString()));
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
        if (txtotpVery.Text != "" && txtotpVery.Text == PMotp.Value)
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

    private void sendMailOTP2(string mailid)
    {
        try { 
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
        s.CreateMail("noreply-srijandefence@gov.in", mailid, "OTP Verification Profile Management", body);
        s.sendMail();
        //MYotpPM.Visible = true;
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
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/ChangeNodalOfficerEmail.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", VEmailID.Text);
            body = body.Replace("{refno}", HttpUtility.UrlEncode(ObjEnc.EncryptData(VendorID5)));
            body = body.Replace("{mcurid}", Resturl(56));
            SendMail s;
            s = new SendMail();
            s.CreateMail("noreply-srijandefence@gov.in", VEmailID.Text, "We have add your email ID, Please click on approval Link", body);
            s.sendMail();
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
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
        //mailbody = mailbody.Replace("{UserIP}", Session["TIP"].ToString());
        //mailbody = mailbody.Replace("{Usercity}", Session["TST"].ToString());
        //mailbody = mailbody.Replace("{UserCountry}", Session["TCOU"].ToString());
        message.Subject = "OTP for are Updating Your Profile";
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

    public bool mytestemailNew()
    {
        string to = VEmailID.Text; //To address    
        string from = "dobhal.naveen17@gmail.com"; //From address    
        MailMessage message = new MailMessage(from, to);
        string mailbody = "";
        using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/ChangeNodalOfficerEmail.html")))
        {
            mailbody = reader.ReadToEnd();
        }
        mailbody = mailbody.Replace("{UserName}", VEmailID.Text);
        mailbody = mailbody.Replace("{refno}", HttpUtility.UrlEncode(ObjEnc.EncryptData(VendorID5)));
        mailbody = mailbody.Replace("{mcurid}", Resturl(56));
        SendMail s;
        s = new SendMail();
        //mailbody = mailbody.Replace("{UserName}", ObjEnc.DecryptData(Session["User"].ToString()));
        //mailbody = mailbody.Replace("{refno}", HttpUtility.UrlEncode(ObjEnc.DecryptData(Session["User"].ToString())));
        mailbody = mailbody.Replace("{mcurid}", Resturl(56));
        message.Subject = "Change Nodal Officer Email ID";
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


    protected void lbresendotp_Click(object sender, EventArgs e)
    {
        GenerateOTP();
        
    }
    #endregion
    

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

    //    public void GetAllState()
    //    {
    //        DataTable dtAllState = Lo.GetState();
    //        ddlstate.DataSource = dtAllState;
    //        ddlstate.DataTextField = "StateName";
    //        ddlstate.DataValueField = "StateName";
    //        ddlstate.DataBind();
    //        ddlstate.Items.Insert(0, "Select");
    //    }
    //#endregion

    //    protected void txtcity_TextChanged(object sender, EventArgs e)
    //    {
    //        DataTable dtState = Lo.GetStateByCity(txtcity.Text);
    //        ddlstate.DataSource = dtState;
    //        ddlstate.DataTextField = "StateName";
    //        ddlstate.DataValueField = "StateName";
    //        ddlstate.DataBind();
    //        ddlstate.Items.Insert(0, "Select");
    //    }

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

    protected void btn_VOTP_Click(object sender, EventArgs e)
    {
        otpProfileM = Co.RSQandSQLInjection(Server.HtmlEncode(txtotpVery.Text), "insrt");
       // string otpProfileMB = Co.RSQandSQLInjection(Server.HtmlEncode(txtotpVery1.Text), "insrt");
        string abc = Session["OTPM"].ToString();
        if (abc == Session["OTPM"].ToString())
        {
            DataTable dt = new DataTable();
            dt = GetvendorIDDetail();
            GetProfileDetail();
            VendorID5 = dt.Rows[0]["VendorID"].ToString();
            Updatemobno = Co.RSQandSQLInjection(Server.HtmlEncode(VmobileNO.Text), "insrt");

            if (Updatemobno == "")
            {
                Updatemobno = MobNo.Text;
            }
            
            EmnailID = Co.RSQandSQLInjection(Server.HtmlEncode(VEmailID.Text), "insrt");
            if (EmnailID == "")
            {
                EmnailID = ObjEnc.DecryptData(Session["User"].ToString());
            }
            else
            {
                verifyemailid(VEmailID.Text);
                if (Session["verifuEmail"].ToString() == "")
                {
                    //SendEmailCodeRegistwo(VEmailID.Text);
                    //mytestemailNew();
                    sendMailOTP3(VEmailID.Text);
                }

                else {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('This email already exist in our portal. Please use another email'); window.location.href='" + liveNewsurl + "';", true);

                }
            }

            StreetAddress = Co.RSQandSQLInjection(Server.HtmlEncode(NolOffADDRE1.Text), "insrt");

            if (StreetAddress == "")
            {
                StreetAddress = NolOffADDRE.Text.Split(',')[0];
            }
            StreetAddressLine2 = Co.RSQandSQLInjection(Server.HtmlEncode(NolOffADDRE2.Text), "insrt");

            if (StreetAddressLine2 == "")
            {
                StreetAddressLine2 = NolOffADDRE.Text.Split(',')[1];
            }
            if (ddlstate.SelectedItem.Text != "Select")
            {
                City = ddlCity.SelectedItem.Text;
                if (City == "")
                {
                    City = NolOffCity.Text.Split('/')[0];
                }
                State = ddlstate.SelectedItem.Text;
                if (State == "")
                {
                    State = NolOffCity.Text.Split('/')[1];
                }
                ZipCode = ddlPincode.SelectedItem.Text;

                if (ZipCode == "")
                {
                    ZipCode = NolOffCity.Text.Split('/')[2];
                }
            }


            Lo.UpdateContactVendor(Updatemobno, ObjEnc.DecryptData(Session["User"].ToString()), StreetAddress, StreetAddressLine2, City, State, ZipCode, VendorID5);
            //Label1.ForeColor = System.Drawing.Color.DarkGreen;
            //Label1.Text = "<font color='Green'><b>Changes Updated</b></font>";
            
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('We have update your changes.'); window.location.href='"+ liveNewsurl + "';", true);
            
            MYotpPM.Visible = false;
        }
        else
        {
            mylable.Text = "Please enter correct OTP";
        }
    }


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
        else {

           Session["verifuEmail"] = DtGetverifyemaiid.ToString();

        }

        #endregion
    }
}