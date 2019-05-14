using System;
using System.Data;
using BusinessLayer;
using Encryption;
using System.Collections.Specialized;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using System.Net;
using System.Web.UI.WebControls;

public partial class Admin_AddCompanyCategory : System.Web.UI.Page
{
    Logic Lo = new Logic();
    HybridDictionary HySave = new HybridDictionary();
    Cryptography objCrypto = new Cryptography();
    Int64 Mid = 0;
    DataUtility Co = new DataUtility();
    string _msg = string.Empty;
    string _sysMsg = string.Empty;
    DataTable DtView = new DataTable();
    string currentPage = "";
    string lbltypelogin = "";
    private string Categoryintrestedare = "";
    DataTable DtCompanyDDL = new DataTable();
    DataTable dtViewDefault = new DataTable();
    string strCRole, strDRole, strURole;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["id"] != null)
                {
                    string strid = Request.QueryString["id"].ToString().Replace(" ", "+");
                    string strPageName = objCrypto.DecryptData(strid);
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
                }
                currentPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
                hidType.Value = objCrypto.DecryptData(Session["Type"].ToString());
                hfcomprefno.Value = Session["CompanyRefNo"].ToString();
                BindCompany();
                BindMasterCategory();
            }
            catch (Exception ex)
            {
                Response.RedirectToRoute("Login");
            }
        }
    }
    protected void BindMasterCategory()
    {
        DataTable DtMasterCategroy = Lo.RetriveMasterCategoryDate(0, "", "", "", "", "Select");
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlmastercategory, DtMasterCategroy, "MCategoryName", "MCategoryID");
            ddlmastercategory.Items.Insert(0, "Select");
        }
        else
        {
            ddlmastercategory.Items.Insert(0, "Select");
        }
    }
    protected void ddlmastercategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlmastercategory.SelectedItem.Text != "Select")
        {
            BindMasterInnerSubCategory();
            level1.Visible = true;
        }
        else
        {
            level1.Visible = false;
        }
    }
    protected void BindMasterInnerSubCategory()
    {
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(ddlmastercategory.SelectedItem.Value), "", "", "SelectInnerMaster", "");
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillCheckBox(chkSubCategory, DtMasterCategroy, "SCategoryName", "SCategoryId");
            level1.Visible = true;
        }
        else
        {
            level1.Visible = false;
        }
    }
    #region DropDownList
    protected void BindCompany()
    {
        if (hidType.Value == "SuperAdmin")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, "", "", 0, "", "", "Select");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Items.Insert(0, "Select");
                ddlcompany.Enabled = true;
            }
            else
            {
                ddlcompany.Enabled = false;
            }
        }
        else if (hidType.Value == "Admin")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, "", "", 0, "", "", "Select");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Items.Insert(0, "Select");
                ddlcompany.Enabled = true;
            }
            else
            {
                ddlcompany.Enabled = false;
            }
        }
        else if (hidType.Value == "Company")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, hfcomprefno.Value, "Company", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = false;
            }
            else
            {
                ddlcompany.Enabled = false;
            }
        }
        else if (hidType.Value == "Factory")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, hfcomprefno.Value, "Company1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = false;
            }
            else
            {
                ddlcompany.Enabled = false;
            }
        }
        else if (hidType.Value == "Unit")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, hfcomprefno.Value, "Company2", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = false;
            }
            else
            {
                ddlcompany.Enabled = false;
            }
        }
    }
    #endregion
    protected void SaveCompanyMenu()
    {
        HybridDictionary hyMasterCategory = new HybridDictionary();
        hyMasterCategory["CompCatRelationId"] = 0;
        if (ddlcompany.SelectedItem.Text != "Select")
        {
            hyMasterCategory["CompanyRefNo"] = Co.RSQandSQLInjection(ddlcompany.SelectedItem.Value, "soft");
        }
        hyMasterCategory["MCategoryId"] = Co.RSQandSQLInjection(ddlmastercategory.SelectedItem.Value, "soft");
        foreach (ListItem li in chkSubCategory.Items)
        {
            if (li.Selected == true)
            {
                Categoryintrestedare = Categoryintrestedare + "," + li.Value;
            }
        }
        hyMasterCategory["SCategoryId"] = Co.RSQandSQLInjection(Categoryintrestedare.Substring(1).ToString(), "soft");
        string mStrCategory = Lo.SaveMasterCategroyMenu(hyMasterCategory, out _sysMsg, out _msg);
        if (mStrCategory == "Save")
        {
            cleartext();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Master Category saved successfully.')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not saved')", true);
        }
    }
    protected void btndemofirst_Click(object sender, EventArgs e)
    {
        if (ddlcompany.SelectedItem.Text != "Select")
        {
            if (btndemofirst.Text == "Save")
            {
                SaveCompanyMenu();
                string StrSaveFDIComp = Lo.SaveMasterCompany(HySave, out _msg, out _sysMsg);
                if (StrSaveFDIComp != "0" && StrSaveFDIComp != "-1")
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Record save successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not save successfully.')", true);
                }
            }
        }
    }
    protected void cleartext()
    {
        foreach (ListItem li in chkSubCategory.Items)
        {
            if (li.Selected == true)
            {
                li.Selected = false;
            }
        }
        BindMasterCategory();
    }
}