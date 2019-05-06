using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using BusinessLayer;
using Encryption;
using System.IO;

public partial class Admin_AddMasterCompany : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Cryptography Enc = new Cryptography();
    DataUtility Co = new DataUtility();
    private Int64 id = 0;
    private string _sysMsg = string.Empty;
    private string _msg = string.Empty;
    private string intrestedare = "";
    private string Masterallowed = "";
    private string role = "";
    private DropDownList mddlControl;
    HybridDictionary HySave = new HybridDictionary();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["mu"] != null)
            {
                string strid = Request.QueryString["id"].ToString().Replace(" ", "+");
                string strPageName = Enc.DecryptData(strid);
                StringBuilder strheadPage = new StringBuilder();
                strheadPage.Append("<ul class='breadcrumb'>");
                string[] MCateg = strPageName.Split(new string[] { ">>" }, StringSplitOptions.RemoveEmptyEntries);
                string MmCval = "";
                for (int x = 0; x < MCateg.Length; x++)
                {
                        MmCval = MCateg[x];
                        strheadPage.Append("<li class=''><span>" + MmCval + "</span></li>");
                }
                divHeadPage.InnerHtml = strheadPage.ToString();
                strheadPage.Append("</ul");
                divOfficerEmail.Visible = false;
                if (Enc.DecryptData(Request.QueryString["mu"].ToString()) == "Panel1")
                {
                    mastercompany.Visible = true;
                    masterfacotry.Visible = false;
                    lblName.Text = "Company Name";
                    btnsubmit.Text = "Save Company";
                    BindMasterCompany();
                    BindMasterData();
                    Intrested.Visible = true;
                    MenuAlot.Visible = true;
                    divRole.Visible = true;
                }
                else if (Enc.DecryptData(Request.QueryString["mu"].ToString()) == "Panel2")
                {
                    mastercompany.Visible = true;
                    masterfacotry.Visible = false;
                    BindMasterCompany();
                    lblName.Text = "Divison/Plant Name";
                    btnsubmit.Text = "Save Divison";
                    Intrested.Visible = false;
                    MenuAlot.Visible = false;
                    divRole.Visible = false;

                }
                else if (Enc.DecryptData(Request.QueryString["mu"].ToString()) == "Panel3")
                {
                    mastercompany.Visible = true;
                    masterfacotry.Visible = true;
                    BindMasterCompany();
                    this.ddlmaster_SelectedIndexChanged(sender, e);
                    lblName.Text = "Unit Name";
                    btnsubmit.Text = "Save Unit";
                    Intrested.Visible = false;
                    MenuAlot.Visible = false;
                    divRole.Visible = false;
                }
                else
                {
                    mastercompany.Visible = false;
                    masterfacotry.Visible = false;
                    lblName.Text = "Company/Organization Name";
                    BindMasterData();
                    BindMasterCompany();
                    Intrested.Visible = true;
                    MenuAlot.Visible = true;
                    divRole.Visible = true;
                }
                lblMastcompany.Text = "Select Company ";
                lblfactoryName.Text = "Select Divison/Plant ";
            }
        }
    }
    protected void BindMasterCompany()
    {
        string sType = "", sName = "", sID = "", mSID = "";
        Int16 id = 0;
        string sRole = "";
        if (Enc.DecryptData(Session["Type"].ToString()) == "SuperAdmin")
        {
            sType = "Select";
            mSID = "";
            id = 0;
            sRole = "SuperAdmin";

        }
        else if (Enc.DecryptData(Session["Type"].ToString()) == "Company")
        {

            sType = "CompanyName";
            mSID = Session["CompanyRefNo"].ToString();
            id = 2;
            sRole = "Company";
        }
        else if (Enc.DecryptData(Session["Type"].ToString()) == "Factory")
        {

            sType = "CompanyName";
            mSID = Session["CompanyRefNo"].ToString();
            id = 3;
            sRole = "Factory";
        }
        else if (Enc.DecryptData(Session["Type"].ToString()) == "Unit")
        {

            sType = "CompanyName";
            mSID = Session["CompanyRefNo"].ToString();
            id = 4;
            sRole = "Unit";
        }
        else
        {
            sType = "CompanyName";
            mSID = Session["CompanyRefNo"].ToString();
        }
        sName = "CompanyName";
        sID = "CompanyRefNo";
        mddlControl = ddlmaster;
        DataTable Dtchkintrestedarea = Lo.RetriveMasterData(id, mSID, sRole, 0, "", "", sType);
        if (Dtchkintrestedarea.Rows.Count > 0 && Dtchkintrestedarea != null)
        {
            Co.FillDropdownlist(mddlControl, Dtchkintrestedarea, sName, sID);
        }

    }
    protected void BindMasterData()
    {
        DataTable Dtchkintrestedarea = Lo.RetriveMasterData(0, "", "", 0, "", "I", "IntrestedAreaCheck");
        if (Dtchkintrestedarea != null)
        {
            Co.FillCheckBox(chkintrestedarea, Dtchkintrestedarea, "InterestArea", "Id");
        }
        DataTable Dtchkmastermenuallot = Lo.RetriveMasterData(0, "", "", 0, "", "M", "IntrestedAreaCheck");
        if (Dtchkmastermenuallot != null)
        {
            Co.FillCheckBox(chkmastermenuallot, Dtchkmastermenuallot, "InterestArea", "Id");
        }
    }
    protected void Cleartext()
    {
        txtemail.Text = "";
        txtcomp.Text = "";
        Masterallowed = "";
        role = "";
    }
    protected void SaveComp()
    {
        string StrSaveComp = "";
        HySave["CompanyID"] = id;
        HySave["CompanyName"] = Co.RSQandSQLInjection(txtcomp.Text.Trim(), "soft");
        HySave["ContactPersonEmailID"] = Co.RSQandSQLInjection(txtemail.Text.Trim(), "soft");
        if (btnsubmit.Text == "Save Divison")
        {
            HySave["CompanyRefNo"] = ddlmaster.SelectedItem.Value;
            HySave["Role"] = "Factory";
            StrSaveComp = Lo.SaveFactoryComp(HySave, out _sysMsg, out _msg);
        }
        else if (btnsubmit.Text == "Save Unit")
        {
            HySave["CompanyRefNo"] = ddlfacotry.SelectedItem.Value;
            HySave["Role"] = "Unit";
            StrSaveComp = Lo.SaveUnitComp(HySave, out _sysMsg, out _msg);
        }
        else
        {
            foreach (ListItem li in chkintrestedarea.Items)
            {
                if (li.Selected == true)
                {
                    intrestedare = intrestedare + "," + li.Value;
                }
            }
            HySave["InterestedArea"] = Co.RSQandSQLInjection(intrestedare.Substring(1).ToString(), "soft");
            foreach (ListItem chkmasallow in chkmastermenuallot.Items)
            {
                if (chkmasallow.Selected == true)
                {
                    Masterallowed = Masterallowed + "," + chkmasallow.Value;
                }
            }
            HySave["MasterAllowed"] = Co.RSQandSQLInjection(Masterallowed.Substring(1).ToString(), "soft");
            foreach (ListItem chkro in chkrole.Items)
            {
                if (chkro.Selected == true)
                {
                    role = role + "," + chkro.Value;
                }
            }
            lblName.Text = "Company Name";
            HySave["Role"] = Co.RSQandSQLInjection(role.Substring(1).ToString(), "soft");

            StrSaveComp = Lo.SaveMasterComp(HySave, out _sysMsg, out _msg);
        }
        if (StrSaveComp != "")
        {
            Cleartext();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + btnsubmit.Text + " Save successfully !')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not saved.')", true);
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Cleartext();
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (txtcomp.Text.Trim() != "")
        {
            string strIsEmail = Lo.VerifyEmailandCompany(txtemail.Text, txtcomp.Text, out _msg);
            if (_msg != "0" && _msg != "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + txtcomp.Text + " name already exist !')", true);
            }
            else
            {
                SaveComp();
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + lblName.Text + " name can not be empty !')", true);
        }
    }
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
    protected void ddlmaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable DtBindSubFactory = Lo.RetriveMasterData(0, ddlmaster.SelectedItem.Value, "", 0, "", "M", "FactoryJoin1");
        if (DtBindSubFactory.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlfacotry, DtBindSubFactory, "FactoryName", "FactoryRefNo");

        }
        else
        {
            //ddlfacotry.Items.Insert(0, "Select Factory");
        }

    }
}