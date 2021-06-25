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

public partial class Vendor_confirmProfileMigration : System.Web.UI.Page
{
    public string VendorID;
    public string NodalOfficerName;
    public string NodalOfficerEmail;
    public string ContactNo;
    public string StreetAddress;
    public string StreetAddressLine2;
    public string City;
    public string vState;
    public string ZipCode;
    public string status1;
    public string status2;
    public string authrizlatt;
    public string indetityCard;
    Cryptography ObjEnc = new Cryptography();
    Logic Lo = new Logic();
    DataUtility Co = new DataUtility();
    private object divregistration;
    bool MsgStatus;
    Int64 MId = 0;
    Cryptography Enc = new Cryptography();
    public string emailID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["mcref"] != "" && Request.QueryString["mcurid"] != "")
        {
            ViewState["Refno"] = Enc.DecryptData(Request.QueryString["mcref"].ToString());
            emailID = Request.QueryString["emi"];
        }
        else
        {
            Response.Redirect("Login");
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (txtpassword.Text != "" && txtrepeatpassword.Text != "")
        {
            if (txtpassword.Text == txtrepeatpassword.Text)
            {
                string sType = "ProfileMigrationpass";
                string Updatepass = Lo.UpdateLoginPassword(Enc.EncryptData(txtpassword.Text), emailID, ViewState["Refno"].ToString(), sType, txtpassword.Text, "");
                if (Updatepass == "true")
                { 
                    GetMigrationDetails();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('We have successfully Migrate Profile.Please login with new password. We will redirected to you login page');window.location ='VendorLogin';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Password not created. Some error occured.')", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Password mismatch')", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Enter password')", true);
        }
    }


    protected void GetMigrationDetails()
    {
        #region Registration Date Retrive
        DataTable DtGetRegisVendor1 = Lo.RetriveProMigrationDetals(ViewState["Refno"].ToString(), emailID);
        if (DtGetRegisVendor1.Rows.Count > 0)
        {

            VendorID = DtGetRegisVendor1.Rows[0]["VendorID"].ToString();
            NodalOfficerName = DtGetRegisVendor1.Rows[0]["NodalOfficerName"].ToString();
            NodalOfficerEmail = DtGetRegisVendor1.Rows[0]["NodalOfficerEmail"].ToString();
            ContactNo = DtGetRegisVendor1.Rows[0]["ContactNo"].ToString();
            StreetAddress = DtGetRegisVendor1.Rows[0]["StreetAddress"].ToString();
            StreetAddressLine2 = DtGetRegisVendor1.Rows[0]["StreetAddressLine2"].ToString();
            City = DtGetRegisVendor1.Rows[0]["City"].ToString();
            vState = DtGetRegisVendor1.Rows[0]["vState"].ToString();
            ZipCode = DtGetRegisVendor1.Rows[0]["ZipCode"].ToString();
            authrizlatt = DtGetRegisVendor1.Rows[0]["authrizlatt"].ToString();
            indetityCard = DtGetRegisVendor1.Rows[0]["indetityCard"].ToString();
           
        }

        Lo.UpdateNewNodalofficerinfo(VendorID, NodalOfficerName, NodalOfficerEmail, ContactNo, StreetAddress, StreetAddressLine2 , City, vState, ZipCode , authrizlatt, indetityCard);
        #endregion
    }
}