using System;
using System.Data;
using BusinessLayer;
using Encryption;
using System.Collections.Specialized;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;

public partial class CompanyDetail : System.Web.UI.Page
{
    Logic Lo = new Logic();
    HybridDictionary HySave = new HybridDictionary();
    Cryptography objCrypto = new Cryptography();
    Int64 Mid = 0;
    DataUtility Co = new DataUtility();
    string _msg = string.Empty;
    string _sysMsg = string.Empty;
    DataTable DtView = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindState();
            EditCOde();
        }
    }
    protected void EditCOde()
    {
        if (Request.QueryString["mcurrentID"] != null)
        {
            hfid.Value = objCrypto.DecryptData(Request.QueryString["mcurrentID"].ToString());
            DtView = Lo.RetriveGridView(Convert.ToInt64(hfid.Value));
            if (DtView.Rows.Count > 0)
            {
                seljvventure.Text = DtView.Rows[0]["IsJointVenture"].ToString();
                tcompanyname.Text = DtView.Rows[0]["CompanyName"].ToString();
                taddress.Text = DtView.Rows[0]["Address"].ToString();
                selstate.Text = DtView.Rows[0]["State"].ToString();
                txtceoname.Text = DtView.Rows[0]["CEOName"].ToString();
                tpincode.Text = DtView.Rows[0]["Pincode"].ToString();
                tpersonname.Text = DtView.Rows[0]["ContactPersonName"].ToString();
                temailid.Text = DtView.Rows[0]["ContactPersonEmailID"].ToString();
                tcontactno.Text = DtView.Rows[0]["ContactPersonContactNo"].ToString();
                tcinno.Text = DtView.Rows[0]["CINNo"].ToString();
                tpanno.Text = DtView.Rows[0]["PANNo"].ToString();
                tgstno.Text = DtView.Rows[0]["GSTNo"].ToString();
                txtCEOEmailId.Text = DtView.Rows[0]["CEOEmail"].ToString();
                companyengaged.Text = DtView.Rows[0]["IsDefenceActivity"].ToString();
            }
        }
    }
    protected void BindState()
    {
        DataTable Dt = Lo.RetriveState("Select");
        if (Dt.Rows.Count > 0 && Dt != null)
        {
            Co.FillDropdownlist(selstate, Dt, "StateName", "StateID");
            selstate.Items.Insert(0, "Select State");
        }
        else
        {
            selstate.Items.Insert(0, "Select State");
        }
    }
    protected void SaveFDI()
    {
        if (hfid.Value != "")
        {
            HySave["CompanyID"] = Convert.ToInt64(hfid.Value);
        }
        else
        {
            HySave["CompanyID"] = Mid;
        }
        HySave["IsJointVenture"] = Co.RSQandSQLInjection(seljvventure.SelectedItem.Value, "soft");
        HySave["CompanyName"] = Co.RSQandSQLInjection(tcompanyname.Text.Trim(), "soft");
        HySave["Address"] = Co.RSQandSQLInjection(taddress.Text.Trim(), "soft");
        if (selstate.SelectedItem.Text == "Select State")
        {
            HySave["State"] = null;
        }
        else
        {
            HySave["State"] = Co.RSQandSQLInjection(selstate.SelectedItem.Value, "soft");
        }
        HySave["District"] = Co.RSQandSQLInjection(seldistrict.Text.Trim(), "soft");
        HySave["Pincode"] = Co.RSQandSQLInjection(tpincode.Text.Trim(), "soft");
        HySave["ContactPersonName"] = Co.RSQandSQLInjection(tpersonname.Text.Trim(), "soft");
        HySave["ContactPersonEmailID"] = Co.RSQandSQLInjection(temailid.Text.Trim(), "soft");
        if (tcontactno.Text == "")
        {
            HySave["ContactPersonContactNo"] = null;
        }
        else
        {
            HySave["ContactPersonContactNo"] = Co.RSQandSQLInjection(tcontactno.Text.Trim(), "soft");
        }
        HySave["CINNo"] = Co.RSQandSQLInjection(tcinno.Text.Trim(), "soft");
        HySave["PANNo"] = Co.RSQandSQLInjection(tpanno.Text.Trim(), "soft");
        HySave["GSTNo"] = Co.RSQandSQLInjection(tgstno.Text.Trim(), "soft");
        HySave["HSNo"] = Co.RSQandSQLInjection(thssnono.Text.Trim(), "soft");
        HySave["IsDefenceActivity"] = Co.RSQandSQLInjection(companyengaged.SelectedItem.Value, "soft");
        HySave["CEOName"] = Co.RSQandSQLInjection(txtceoname.Text, "soft");
        HySave["CEOEmail"] = Co.RSQandSQLInjection(txtCEOEmailId.Text, "soft");
    }
    public string ValidatePreview()
    {
        string mCon = "";
        if (tcompanyname.Text == "")
        {
            mCon = "Company name is mandatory";
        }
        if (temailid.Text == "")
        {
            mCon = "Email is mandatory";
        }
        if (IsValidEmailId(temailid.Text) == true)
        {
        }
        else
        {
            mCon = "Email format is invalid";
        }
        if (txtCEOEmailId.Text != "")
        {
            if (IsValidEmailId(txtCEOEmailId.Text) == true)
            {
            }
            else
            {
                mCon = "Email format is invalid";
            }
        }
        if (tcinno.Text != "" && hfid.Value == "")
        {
            DataTable Dt = Lo.RetriveCompany("CheckPanMo", 0, tcinno.Text);
            if (Dt.Rows.Count > 0 && Dt != null)
            {
                mCon = "CIN No already registerd.";
            }
        }
        if (temailid.Text != "" && hfid.Value == "")
        {
            DataTable Dt = Lo.RetriveCompany("CheckEmailNodel", 0, temailid.Text);
            if (Dt.Rows.Count > 0 && Dt != null)
            {
                mCon = "Nodel person email-id already registerd";
            }
        }
        return mCon;
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
    protected void btndemofirst_Click(object sender, EventArgs e)
    {
        if (tcompanyname.Text != "" && temailid.Text != "" && tcinno.Text != "")
        {
            string msg = this.ValidatePreview();
            if (msg != "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + msg + "');", true);
            }
            else
            {
                SaveFDI();
                string StrSaveFDIComp = Lo.SaveFDIComp(HySave, out _msg, out _sysMsg);
                if (StrSaveFDIComp != "0" && StrSaveFDIComp != "-1")
                {
                    if (hfid.Value != "")
                    {
                        cleartext();
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Record updated successfully');window.location='Detail-Company';", true);
                    }
                    else
                    {
                        cleartext();
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Record save successfully');window.location='Detail-Company';", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not save successfully.')", true);
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Please fill all mandatory field.');", true);
        }
    }
    protected void cleartext()
    {
        seljvventure.SelectedIndex = 0;
        tcompanyname.Text = "";
        taddress.Text = "";
        selstate.SelectedIndex = 0;
        seldistrict.Text = "";
        tpincode.Text = "";
        tpersonname.Text = "";
        temailid.Text = "";
        tcontactno.Text = "";
        tcinno.Text = "";
        tpanno.Text = "";
        tgstno.Text = "";
        thssnono.Text = "";
        companyengaged.SelectedIndex = 0;
        hfid.Value = "";
        txtCEOEmailId.Text = "";
        txtceoname.Text = "";
    }
  

}