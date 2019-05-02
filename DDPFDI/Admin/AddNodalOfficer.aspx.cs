﻿using BusinessLayer;
using Encryption;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.UI;

public partial class Admin_AddNodalOfficer : System.Web.UI.Page
{
    Cryptography objEnc = new Cryptography();
    DataUtility Co = new DataUtility();
    Logic Lo = new Logic();
    private string DisplayPanel = "";
    private string SessionID = "";
    private string _sysMsg = string.Empty;
    private string _msg = string.Empty;
    DataTable DtGrid = new DataTable();
    DataTable DtCompanyDDL = new DataTable();
    HybridDictionary hySaveNodal = new HybridDictionary();
    string UserName;
    string RefNo;
    string UserEmail;
    string currentPage = "";
    private string mType = "";
    private string mRefNo = "";
    private Int16 Mid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                string strid = Request.QueryString["id"].ToString().Replace(" ", "+");
                string strPageName = objEnc.DecryptData(strid);
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
                // currentPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
                mType = objEnc.DecryptData(Session["Type"].ToString());
                mRefNo = Session["CompanyRefNo"].ToString();
                BindCompany();
                BindMasterDesignation();
                BindMasterDepartment();
            }
        }
    }
    #region Load
    protected void BindCompany()
    {
        if (mType == "SuperAdmin")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, "", "", 0, "", "", "Select");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Items.Insert(0, "All");
                ddlcompany.Enabled = true;
            }
            else
            {
                ddlcompany.Enabled = false;
            }

            lblselectdivison.Visible = false;
            lblselectunit.Visible = false;
        }
        else if (mType == "Admin")
        {
        }
        else if (mType == "Company")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = false;
            }
            else
            {
                ddlcompany.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                ddldivision.Items.Insert(0, "All");
                if (mType == "Company")
                {
                    lblselectdivison.Visible = true;
                    ddldivision.Enabled = true;
                    ddlunit.Visible = false;
                }
                else
                {
                    ddldivision.Enabled = false;
                }
            }
            else
            {
                ddldivision.Enabled = false;
            }
        }
        else if (mType == "Factory")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = false;
            }
            else
            {
                ddlcompany.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                lblselectdivison.Visible = true;
                ddldivision.Enabled = false;
            }
            else
            {
                ddldivision.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "Unit1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
                ddlunit.Items.Insert(0, "All");
                ddlunit.Enabled = true;
                lblselectunit.Visible = true;
            }
            else
            {
                ddlunit.Visible = false;
            }
        }
        else if (mType == "Unit")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company2", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = false;
            }
            else
            {
                ddlcompany.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                lblselectdivison.Visible = true;
                ddldivision.Enabled = false;
            }
            else
            {
                ddldivision.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "Unit1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
                ddlunit.Enabled = false;
                lblselectunit.Visible = true;
            }
            else
            {
                ddlunit.Enabled = false;
            }
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
    #region DropDownList Code
    protected void ddlcompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcompany.SelectedItem.Text != "All")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "", 0, "", "", "FactoryCompanyID");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                ddldivision.Items.Insert(0, "All");
                lblselectdivison.Visible = true;
                ddldivision.Visible = true;
            }
            else
            {
                ddldivision.Visible = false;
                lblselectdivison.Visible = false;
            }
        }
        else if (ddlcompany.SelectedItem.Text == "All")
        {
            lblselectdivison.Visible = false;
            lblselectunit.Visible = false;
        }
    }
    protected void ddldivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldivision.SelectedItem.Text != "All")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "", 0, "", "", "UnitSelectID");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
                ddlunit.Items.Insert(0, "All");
                ddlunit.Visible = true;
                lblselectunit.Visible = true;
            }
            else
            {
                lblselectunit.Visible = false;
                ddlunit.Visible = false;
            }
        }
        else if (ddldivision.SelectedItem.Text == "All")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "", 0, "", "", "FactoryCompanyID");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                ddldivision.Items.Insert(0, "All");
                lblselectdivison.Visible = true;
                ddldivision.Visible = true;
                lblselectdivison.Visible = false;
            }
        }
    }
    #endregion
    #region For Department or Designation
    protected void BindMasterDesignation()
    {
        ddldesignation.Items.Insert(0, "Designation");
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, ddldesignation.SelectedItem.Value, "", "SelectProductCat", mRefNo.ToString());
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddldesignation, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddldesignation.Items.Insert(0, "Designation");
        }
    }
    protected void BindMasterDepartment()
    {
        ddldepartment.Items.Insert(0, "Department");
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, ddldepartment.SelectedItem.Value, "", "SelectProductCat", mRefNo.ToString());
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddldepartment, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddldepartment.Items.Insert(0, "Department");
        }
    }
    #endregion
    #region Save code
    protected void SaveNodal()
    {
        if (Mid == 0)
        {
            hySaveNodal["NodalOfficerID"] = Mid;
        }
        else
        {
            hySaveNodal["NodalOfficerID"] = Mid;
        }
        if (objEnc.DecryptData(Session["Type"].ToString()) == "SuperAdmin")
        {
            if (ddlcompany.SelectedItem.Text != "All")
            {
                hySaveNodal["NodalOfficerRefNo"] = Co.RSQandSQLInjection(ddlcompany.SelectedItem.Value, "soft");
            }
            else if (ddldivision.SelectedItem.Text != "All")
            {
                hySaveNodal["NodalOfficerRefNo"] = Co.RSQandSQLInjection(ddlcompany.SelectedItem.Value, "soft");
            }
            else if (ddlunit.SelectedItem.Text != "All")
            {
                hySaveNodal["NodalOfficerRefNo"] = Co.RSQandSQLInjection(ddlcompany.SelectedItem.Value, "soft");
            }
        }
        if (objEnc.DecryptData(Session["Type"].ToString()) == "Company")
        {
            hySaveNodal["NodalOfficerRefNo"] = Co.RSQandSQLInjection(ddlcompany.SelectedItem.Value, "soft");
        }
        else if (objEnc.DecryptData(Session["Type"].ToString()) == "Factory")
        {
            hySaveNodal["NodalOfficerRefNo"] = Co.RSQandSQLInjection(ddldivision.SelectedItem.Value, "soft");
        }
        if (objEnc.DecryptData(Session["Type"].ToString()) == "Unit")
        {
            hySaveNodal["NodalOfficerRefNo"] = Co.RSQandSQLInjection(ddlunit.SelectedItem.Value, "soft");
        }
        hySaveNodal["NodalOficerName"] = Co.RSQandSQLInjection(txtname.Text, "soft");
        hySaveNodal["NodalOfficerDepartment"] = Co.RSQandSQLInjection(ddldepartment.SelectedItem.Value, "soft");
        hySaveNodal["NodalOfficerDesignation"] = Co.RSQandSQLInjection(ddldesignation.SelectedItem.Value, "soft");
        hySaveNodal["NodalOfficerEmail"] = Co.RSQandSQLInjection(txtemailid.Text, "soft");
        hySaveNodal["NodalOfficerMobile"] = Co.RSQandSQLInjection(txtmobile.Text, "soft");
        hySaveNodal["NodalOfficerTelephone"] = Co.RSQandSQLInjection(txttelephone.Text, "soft");
        hySaveNodal["NodalOfficerFax"] = Co.RSQandSQLInjection(txtfax.Text, "soft");
        string Str = Lo.SaveMasterNodal(hySaveNodal, out _sysMsg, out _msg);
        if (Str == "Save")
        {
            Cleartext();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record saved successsfully')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not saved successsfully')", true);
        }
    }
    #endregion
    protected void btnsub_Click(object sender, EventArgs e)
    {
        SaveNodal();
    }
    protected void Cleartext()
    {
        txtfax.Text = "";
        txtemailid.Text = "";
        txtmobile.Text = "";
        txtname.Text = "";
        txttelephone.Text = "";
        ddldepartment.SelectedIndex = 0;
        ddldesignation.SelectedIndex = 0;
    }
}