using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Collections.Specialized;
using Encryption;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

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
    protected void ddlregisterdgst_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlregisterdgst.SelectedItem.Text == "YES")
        { divgst.Visible = true; }
        else
        { divgst.Visible = false; }
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
        txtgstno.Text = "";
        txtlastname.Text = "";
        txtmiddlename.Text = "";
        txtphoneno.Text = "";
        txtpostalzipcode.Text = "";
        txtstateprovince.Text = "";
        txtstdcode.Text = "";
        txtstreetaddress.Text = "";
        txtstreetaddressline2.Text = "";
        ddlbusinesssector.SelectedIndex = 0;
        ddlregisterdgst.SelectedIndex = 0;
        ddltypeofbusiness.SelectedIndex = 0;
    }
    protected void SaveCode()
    {
        HySaveVendor["VendorID"] = MId;
        HySaveVendor["IsGST"] = ddlregisterdgst.SelectedItem.Value;
        HySaveVendor["GSTNo"] = txtgstno.Text.Trim();
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
        if (txtfirstname.Text != "" && txtemail.Text != "")
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
            s.CreateMail("aeroindia-ddp@gov.in", txtemail.Text, "Create Password Email", body);
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
}