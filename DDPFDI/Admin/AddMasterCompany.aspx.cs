﻿using System;
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

public partial class Admin_AddMasterCompany : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Cryptography Enc = new Cryptography();
    DataUtility Co = new DataUtility();
    private Int64 id = 0;
    private string _sysMsg = string.Empty;
    private string _msg = string.Empty;
    private string comprefno = "";
    private string intrestedare = "";
    private string Masterallowed = "";
    private string role = "";
    HybridDictionary HySave = new HybridDictionary();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Enc.DecryptData(Request.QueryString["mu"].ToString()) == "Panel1")
            {
                mastercompany.Visible = true;
                BindMasterCompany();
                BindMasterData();
                Intrested.Visible = true;
                MenuAlot.Visible = true;
                Role.Visible = true;
            }
            else if (Enc.DecryptData(Request.QueryString["mu"].ToString()) == "Panel2")
            {
                mastercompany.Visible = true;
                BindMasterCompany();
                Intrested.Visible = false;
                MenuAlot.Visible = false;
                Role.Visible = false;

            }
            else
            {
                mastercompany.Visible = false;
                BindMasterData();
                Intrested.Visible = true;
                MenuAlot.Visible = true;
                Role.Visible = true;
            }

        }
    }
    protected void BindMasterCompany()
    {
        DataTable Dtchkintrestedarea = Lo.RetriveCompany("InterestedArea", 0, Session["CompanyRefNo"].ToString(), 0);
        if (Dtchkintrestedarea != null)
        {
            Co.FillDropdownlist(ddlmaster, Dtchkintrestedarea, "CompanyName", "CompanyId");
            ddlmaster.Enabled = false;
        }
        else
        {
            ddlmaster.Items.Insert(0, "Select Company");
            ddlmaster.Enabled = false;
        }
    }
    protected void BindMasterData()
    {
        DataTable Dtchkintrestedarea = Lo.RetriveCompany("IntrestedAreaCheck", 0, "I", 0);
        if (Dtchkintrestedarea != null)
        {
            Co.FillCheckBox(chkintrestedarea, Dtchkintrestedarea, "InterestArea", "Id");
        }
        DataTable Dtchkmastermenuallot = Lo.RetriveCompany("IntrestedAreaCheck", 0, "M", 0);
        if (Dtchkmastermenuallot != null)
        {
            Co.FillCheckBox(chkmastermenuallot, Dtchkmastermenuallot, "InterestArea", "Id");
        }
    }
    protected void Cleartext()
    {
        txtemail.Text = "";
        txtcomp.Text = "";
        comprefno = "";
        Masterallowed = "";
        role = "";
    }
    protected void SaveComp()
    {
        if (hfid.Value != "")
        {
            HySave["CompanyID"] = Convert.ToInt64(hfid.Value);
        }
        else
        {
            HySave["CompanyID"] = id;
        }
        HySave["IsJointVenture"] = "";
        HySave["CompanyName"] = Co.RSQandSQLInjection(txtcomp.Text.Trim(), "soft");
        HySave["Address"] = "";

        HySave["State"] = "";

        HySave["District"] = "";
        HySave["Pincode"] = "";
        HySave["ContactPersonName"] = "";
        HySave["ContactPersonEmailID"] = Co.RSQandSQLInjection(txtemail.Text.Trim(), "soft");

        HySave["ContactPersonContactNo"] = "";

        HySave["CINNo"] = "";
        HySave["PANNo"] = "";
        HySave["HSNo"] = "";
        HySave["IsDefenceActivity"] = "";
        HySave["CEOName"] = "";
        HySave["CEOEmail"] = "";
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
        HySave["Role"] = Co.RSQandSQLInjection(role.Substring(1).ToString(), "soft");

        string StrSaveComp = Lo.SaveMasterCompany(HySave, out _sysMsg, out _msg);
        if (StrSaveComp != "")
        {
            Cleartext();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record Saved.')", true);
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
        SaveComp();
    }
}