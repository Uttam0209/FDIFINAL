using BusinessLayer;
using Encryption;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_NatoCodeSearch : System.Web.UI.Page
{
    #region Load Variable
    private Cryptography objEnc = new Cryptography();
    private DataUtility Co = new DataUtility();
    private Logic Lo = new Logic();
    DataTable DtNSFIIG = new DataTable();
    private string _msg = string.Empty;
    private string _sysMsg = string.Empty;
    private DataTable DtCompanyDDL = new DataTable();
    #endregion
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
                        string strPageName = objEnc.DecryptData(strid);
                        StringBuilder strheadPage = new StringBuilder();
                        strheadPage.Append("<ul class='breadcrumb'>");
                        string[] MCateg = strPageName.Split(new string[] { ">>" }, StringSplitOptions.RemoveEmptyEntries);
                        string MmCval = "";
                        for (int x = 0; x < MCateg.Length; x++)
                        {
                            MmCval = MCateg[x];
                            if (MmCval == " View ")
                            {
                                MmCval = "Add";
                            }

                            strheadPage.Append("<li class=''><span>" + MmCval + "</span></li>");
                        }
                        divHeadPage.InnerHtml = strheadPage.ToString().Trim();
                        strheadPage.Append("</ul");
                        ViewState["UserLoginEmail"] = objEnc.DecryptData(Session["User"].ToString()).Trim();
                        if (Request.QueryString["mcurrentcompRefNo"] != null)
                        {
                            hidType.Value = objEnc.DecryptData(Request.QueryString["mrcreaterole"].ToString().Trim());
                            hfcomprefno.Value = objEnc.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString().Trim());
                        }
                        else
                        {
                            hidType.Value = objEnc.DecryptData(Session["Type"].ToString().Trim());
                            hfcomprefno.Value = Session["CompanyRefNo"].ToString().Trim();
                        }
                        BindMasterCategory();
                        BindMasterSubCategory();
                        BindMaster3levelSubCategory();
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "ErrorMssgPopup('" + ex.Message + "')", true);
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "ErrorMssgPopup('Session Expired,Please login again');window.location='Login'", true);
        }
    }
    #region For ProductCode
    protected void BindMasterCategory()
    {
        DataTable DtMasterCategroy = new DataTable();
        if (hfcomprefno.Value != "")
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "NSN GROUP", "", "SelectProductCat", hfcomprefno.Value, "");
        }
        else
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "NSN GROUP", "", "SelectProductCat", "", "");
        }
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlmastercategory, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlmastercategory.Items.Insert(0, "Select");
            ddlsubcategory.SelectedIndex = -1;
            ddllevel3product.SelectedIndex = -1;
        }
        else
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "NSN GROUP", "", "SelectProductCat", "", "");
            Co.FillDropdownlist(ddlmastercategory, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlmastercategory.Items.Insert(0, "Select");
            ddlsubcategory.SelectedIndex = -1;
            ddllevel3product.SelectedIndex = -1;
        }
    }
    protected void BindMasterSubCategory()
    {
        DataTable DtMasterCategroy = new DataTable();
        if (ddlmastercategory.SelectedItem.Text != "Select")
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlmastercategory.SelectedItem.Value), "", "", "SubSelectID", "", "");
        }
        else
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "", "", "SubSelectSec", "", "");
        }
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlsubcategory, DtMasterCategroy, "SCategoryName", "SCategoryId");
            ddlsubcategory.Items.Insert(0, "Select");
            ddllevel3product.SelectedIndex = -1;
        }
        else
        {
            ddlsubcategory.Items.Clear();
            ddlsubcategory.Items.Insert(0, "Select");
        }
    }
    protected void BindMaster3levelSubCategory()
    {
        DataTable DtMasterCategroyLevel3 = new DataTable();
        //if (ddlsubcategory.SelectedItem.Value != null || ddlsubcategory.SelectedItem.Text != "Select")
        //{
        if (ddlsubcategory.SelectedIndex != 0)
        { DtMasterCategroyLevel3 = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlsubcategory.SelectedItem.Value), "", "", "SubSelectID", "", ""); }
        else
        { DtMasterCategroyLevel3 = Lo.RetriveMasterSubCategoryDate(0, "", "", "SubSelectthr", "", ""); }
        if (DtMasterCategroyLevel3.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddllevel3product, DtMasterCategroyLevel3, "SCategoryName", "SCategoryId");
            ddllevel3product.Items.Insert(0, "Select");
        }
        else
        {
            ddllevel3product.Items.Clear();
            ddllevel3product.Items.Insert(0, "Select");
            ddllevel3product.Items.Insert(1, "NA");
        }
        //}
    }
    #endregion
    #region DropDownList Code
    protected void ddlmastercategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblfinalname.Text = ddlmastercategory.SelectedItem.Text;
        BindMasterSubCategory();
    }
    protected void ddlsubcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        string m = ddlsubcategory.SelectedItem.Value;
        DataTable dt2 = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlsubcategory.SelectedItem.Value), "", "", "2to2", "", "");
        if (dt2.Rows.Count > 0)
        {
            DataTable dt1sr = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlsubcategory.SelectedItem.Value), "", "", "3to21", "", "");
            ddlmastercategory.SelectedValue = dt1sr.Rows[0]["SCategoryId"].ToString();
            Co.FillDropdownlist(ddlsubcategory, dt2, "SCategoryName", "SCategoryId");
            ddlsubcategory.Items.Insert(0, "Select");
            ddlsubcategory.SelectedValue = m;
            DataTable dtbindvalue = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlsubcategory.SelectedItem.Value), "", "", "SubSelectID", "", "");
            if (dtbindvalue.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddllevel3product, dtbindvalue, "SCategoryName", "SCategoryId");
                ddllevel3product.Items.Insert(0, "Select");
                lblfinalname.Text = ddlmastercategory.SelectedItem.Text + " - " + ddlsubcategory.SelectedItem.Text;
            }
            else
            {
                ddllevel3product.Items.Clear();
                ddllevel3product.Items.Insert(0, "Select");
                ddllevel3product.Items.Insert(1, "NA");
            }
        }
    }
    protected void ddllevel3product_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddllevel3product.SelectedItem.Text != "Select" && ddllevel3product.SelectedItem.Text != "NA")
        {
            DataTable dt1 = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddllevel3product.SelectedItem.Value), "", "", "3to2", "", "");
            if (dt1.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlsubcategory, dt1, "SCategoryName", "SCategoryId");
                ddlsubcategory.Items.Insert(0, "Select");
                DataTable dtbindvalue = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddllevel3product.SelectedItem.Value), "", "", "3to21", "", "");
                ddlsubcategory.SelectedValue = dtbindvalue.Rows[0]["SCategoryId"].ToString();
                DataTable dt1sr = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlsubcategory.SelectedItem.Value), "", "", "3to21", "", "");
                ddlmastercategory.SelectedValue = dt1sr.Rows[0]["SCategoryId"].ToString();
                lblfinalname.Text = ddlmastercategory.SelectedItem.Text + " - " + ddlsubcategory.SelectedItem.Text + " - " + ddllevel3product.SelectedItem.Text;
            }
            else
            {
                ddllevel3product.SelectedValue = "Select";
            }
        }
    }

    #endregion
}