using System;
using System.Data;
using BusinessLayer;
using Encryption;
using System.Collections.Specialized;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using System.Web;
using System.Web.Helpers;

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
        if (Session["User"] != null)
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
                    ViewState["UserLoginEmail"] = Session["User"].ToString();
                    BindCompany();
                    BindMasterCategory();
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();
                    string Page = Request.Url.AbsolutePath.ToString();
                    Response.Redirect("Error?techerror=" + HttpUtility.UrlEncode(objCrypto.EncryptData(error)) +
                                      "&page=" + HttpUtility.UrlEncode(objCrypto.EncryptData(Page)));
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert",
                "ErrorMssgPopup('Session Expired,Please login again');window.location='Login'", true);
        }
    }
    protected void BindMasterCategory()
    {
        try
        {
            DataTable DtMasterCategroy = Lo.RetriveMasterCategoryDate(0, "", "", "", "", "Select", "");
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
        catch (Exception ex)
        {
            string error = ex.ToString();
            string Page = Request.Url.AbsolutePath.ToString();
            Response.Redirect("Error?techerror=" + HttpUtility.UrlEncode(objCrypto.EncryptData(error)) + "&page=" + HttpUtility.UrlEncode(objCrypto.EncryptData(Page)));
        }
    }
    protected void ddlmastercategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlmastercategory.SelectedItem.Text != "Select")
            {
                BindMasterInnerSubCategory();
                level1.Visible = true;
                CheckAlreadySelectedMenu();
            }
            else
            {
                level1.Visible = false;
            }
        }
        catch (Exception ex)
        {
            string error = ex.ToString();
            string Page = Request.Url.AbsolutePath.ToString();
            Response.Redirect("Error?techerror=" + HttpUtility.UrlEncode(objCrypto.EncryptData(error)) + "&page=" + HttpUtility.UrlEncode(objCrypto.EncryptData(Page)));
        }
    }
    protected void BindMasterInnerSubCategory()
    {
        try
        {
            DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(ddlmastercategory.SelectedItem.Value), "", "", "SelectInnerMaster", "", "");
            if (DtMasterCategroy.Rows.Count > 0)
            {
                Co.FillCheckBox(chkSubCategory, DtMasterCategroy, "SCategoryName", "SCategoryId");
                level1.Visible = true;
                chkSubCategory.Visible = true;
            }
            else
            {
                chkSubCategory.Visible = false;
                level1.Visible = false;
            }
        }
        catch (Exception ex)
        {
            string error = ex.ToString();
            string Page = Request.Url.AbsolutePath.ToString();
            Response.Redirect("Error?techerror=" + HttpUtility.UrlEncode(objCrypto.EncryptData(error)) + "&page=" + HttpUtility.UrlEncode(objCrypto.EncryptData(Page)));
        }
    }
    protected void CheckAlreadySelectedMenu()
    {
        try
        {
            DataTable DtMasterCategroy1 = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(ddlmastercategory.SelectedItem.Value), "", "", "CompanyCatID", ddlcompany.SelectedItem.Value, "");
            if (DtMasterCategroy1.Rows.Count > 0)
            {
                for (int i = 0; DtMasterCategroy1.Rows.Count > i; i++)
                {
                    foreach (ListItem chk in chkSubCategory.Items)
                    {
                        if (DtMasterCategroy1.Rows[i]["SCategoryId"].ToString() == chk.Value)
                        {
                            chk.Enabled = false;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string error = ex.ToString();
            string Page = Request.Url.AbsolutePath.ToString();
            Response.Redirect("Error?techerror=" + HttpUtility.UrlEncode(objCrypto.EncryptData(error)) + "&page=" + HttpUtility.UrlEncode(objCrypto.EncryptData(Page)));
        }
    }
    #region DropDownList
    protected void BindCompany()
    {
        try
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
            else if (hidType.Value == "Factory" || hidType.Value == "Division")
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
        catch (Exception ex)
        {
            string error = ex.ToString();
            string Page = Request.Url.AbsolutePath.ToString();
            Response.Redirect("Error?techerror=" + HttpUtility.UrlEncode(objCrypto.EncryptData(error)) + "&page=" + HttpUtility.UrlEncode(objCrypto.EncryptData(Page)));
        }
    }
    #endregion
    protected void SaveCompanyMenu()
    {
        try
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
                if (li.Selected == true && li.Enabled == true)
                {
                    Categoryintrestedare = Categoryintrestedare + "," + li.Value;
                }
            }
            hyMasterCategory["SCategoryId"] = Co.RSQandSQLInjection(Categoryintrestedare.Substring(1).ToString() + ",", "soft");
            hyMasterCategory["CreatedBy"] = objCrypto.DecryptData(ViewState["UserLoginEmail"].ToString());
            string mStrCategory = Lo.SaveMasterCategroyMenu(hyMasterCategory, out _sysMsg, out _msg);
            if (mStrCategory == "Save")
            {
                cleartext();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Master Category saved successfully.')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Record not saved')", true);
            }
        }
        catch (Exception ex)
        {
            string error = ex.ToString();
            string Page = Request.Url.AbsolutePath.ToString();
            Response.Redirect("Error?techerror=" + HttpUtility.UrlEncode(objCrypto.EncryptData(error)) + "&page=" + HttpUtility.UrlEncode(objCrypto.EncryptData(Page)));
        }
    }
    protected void btndemofirst_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlcompany.SelectedItem.Text != "Select")
            {
                if (btndemofirst.Text == "Save")
                {
                    SaveCompanyMenu();
                    string StrSaveFDIComp = Lo.SaveMasterCompany(HySave, out _msg, out _sysMsg);
                    if (StrSaveFDIComp != "0" && StrSaveFDIComp != "-1")
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "SuccessfullPop('Record save successfully')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Record not save successfully.')", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Please select any dropdown to save record.')", true);
            }

        }
        catch (Exception ex)
        {
            string error = ex.ToString();
            string Page = Request.Url.AbsolutePath.ToString();
            Response.Redirect("Error?techerror=" + HttpUtility.UrlEncode(objCrypto.EncryptData(error)) + "&page=" + HttpUtility.UrlEncode(objCrypto.EncryptData(Page)));
        }
    }
    protected void cleartext()
    {
        try
        {
            foreach (ListItem li in chkSubCategory.Items)
            {
                if (li.Selected == true)
                {
                    li.Selected = false;
                }
            }
            chkSubCategory.Visible = false;
            BindMasterCategory();
        }
        catch (Exception ex)
        {
            string error = ex.ToString();
            string Page = Request.Url.AbsolutePath.ToString();
            Response.Redirect("Error?techerror=" + HttpUtility.UrlEncode(objCrypto.EncryptData(error)) + "&page=" + HttpUtility.UrlEncode(objCrypto.EncryptData(Page)));
        }
    }
}