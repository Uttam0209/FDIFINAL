using System;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web.UI;
using BusinessLayer;
using Encryption;

public partial class Admin_RegisterdAs : System.Web.UI.Page
{
    Cryptography objEnc = new Cryptography();
    DataUtility Co = new DataUtility();
    Logic Lo = new Logic();
    private string DisplayPanel = "";
    HybridDictionary hySave = new HybridDictionary();
    private string mType = "";
    private string mRefNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != null)
        {
            if (!IsPostBack)
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
                mType = objEnc.DecryptData(Session["Type"].ToString());
                mRefNo = Session["CompanyRefNo"].ToString();
                DisplayPanel = objEnc.DecryptData(Request.QueryString["mu"].ToString().Replace(" ", "+"));
                ShowHidePanel();
                BindGridView();
            }
        }
    }
    protected void BindGridView(string sortExpression = null)
    {
        try
        {
            if (mType == "SuperAdmin")
            {
                DataTable DtGrid = Lo.RetriveMasterCategoryDate(0, "", "", "", "", "Select");
                if (DtGrid.Rows.Count > 0)
                {

                    gvCategory.DataSource = DtGrid;
                    gvCategory.DataBind();
                }
            }
            else if (mType == "Company" && mRefNo != "")
            {
                DataTable DtGrid = Lo.RetriveMasterSubCategoryDate(0, "", "", "SubCompCat", mRefNo);
                if (DtGrid.Rows.Count > 0)
                {

                    gvCategory.DataSource = DtGrid;
                    gvCategory.DataBind();
                }
            }
            else if (mType == "Factroy" && mRefNo != "")
            {
                DataTable DtGrid = Lo.RetriveMasterSubCategoryDate(0, "", "", "SubCompCat", mRefNo);
                if (DtGrid.Rows.Count > 0)
                {

                    gvCategory.DataSource = DtGrid;
                    gvCategory.DataBind();
                }
            }
            else if (mType == "Unit" && mRefNo != "")
            {
                DataTable DtGrid = Lo.RetriveMasterSubCategoryDate(0, "", "", "SubCompCat", mRefNo);
                if (DtGrid.Rows.Count > 0)
                {

                    gvCategory.DataSource = DtGrid;
                    gvCategory.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void ShowHidePanel()
    {
        if (DisplayPanel.ToString() == "Panel1")
        {
            divcategory1textbox.Visible = true;
            divflag.Visible = true;
            divActive.Visible = true;
            btnsave.Text = "Save Label";
        }
        else if (DisplayPanel.ToString() == "Panel2")
        {
            divcategory1dropdown.Visible = true;
            divcategory2textbox.Visible = true;
            divcategory1textbox.Visible = false;
            divActive.Visible = false;
            divflag.Visible = false;
            btnsave.Text = "Save Level 1";
            BindMasterCategory();
            ddlmastercategory.AutoPostBack = false;
        }
        else if (DisplayPanel.ToString() == "Panel3")
        {
            divcategory1dropdown.Visible = true;
            divActive.Visible = false;
            divcategory2ddl.Visible = true;
            divcategory3textbox.Visible = true;
            divflag.Visible = false;
            btnsave.Text = "Save Level 2";
            BindMasterCategory();
            ddlmastercategory.AutoPostBack = true;
        }
        else if (DisplayPanel.ToString() == "Panel4")
        {
            divcategory1dropdown.Visible = true;
            divActive.Visible = false;
            divcategory2ddl.Visible = true;
            divcategory3textbox.Visible = false;
            divflag.Visible = false;
            divlabel2drop.Visible = true;
            divlevel3.Visible = true;
            btnsave.Text = "Save Level 3";
            BindMasterCategory();
            ddlmastercategory.AutoPostBack = true;
            ddlcategroy2.AutoPostBack = true;
        }
    }
    protected void cleartext()
    {
        txtcategory3.Text = "";
        txtmastercategory.Text = "";
        txtsubcategory.Text = "";
        txtlevel3.Text = "";
        ddlmastercategory.SelectedValue = "Select";
        if (ddlcategroy2.Visible = true)
        {
            ddlcategroy2.SelectedValue = "Select";
        }
        if (ddllabel2.Visible = true)
        {
            ddllabel2.SelectedValue = "Select";
        }
    }
    protected void btncancle_Click(object sender, EventArgs e)
    {
        cleartext();
    }
    protected void SaveCode()
    {
        DataTable StrCat = Lo.RetriveMasterCategoryDate(0, Co.RSQandSQLInjection(txtmastercategory.Text, "soft"), "", rbflag.SelectedItem.Value, rbactive.SelectedItem.Value, "Insert");
        if (StrCat != null)
        {
            cleartext();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record saved')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not saved')", true);
        }
    }
    protected void SaveCodeSub()
    {
        DataTable StrCat = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(ddlmastercategory.SelectedItem.Value), Co.RSQandSQLInjection(txtsubcategory.Text, "soft"), "0", "InsertInnerID", "");
        if (StrCat != null)
        {
            cleartext();// ddlcategroy2.SelectedIndex = 0;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record saved')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not saved')", true);
        }
    }
    protected void SaveCodeInnerSub()
    {
        DataTable StrCat = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(ddlcategroy2.SelectedItem.Value), Co.RSQandSQLInjection(txtcategory3.Text, "soft"), "0", "InsertInnerSubID", "");
        if (StrCat != null)
        {
            cleartext();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record saved')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not saved')", true);
        }
    }
    protected void SaveCodeInnerSubSub()
    {
        DataTable StrCat = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(ddllabel2.SelectedItem.Value), Co.RSQandSQLInjection(txtlevel3.Text, "soft"), "0", "InsertInnerSubID", "");
        if (StrCat != null)
        {
            cleartext();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record saved')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not saved')", true);
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (btnsave.Text == "Save Label")
        {
            if (txtmastercategory.Text != "")
            {
                SaveCode();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record saved successfully')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('All field fill mandatory')", true);
            }
        }
        else if (btnsave.Text == "Save Level 1")
        {
            if (ddlmastercategory.SelectedIndex != 0 && txtsubcategory.Text != "")
            {
                SaveCodeSub();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record saved successfully')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('All field fill mandatory')", true);
            }
        }
        else if (btnsave.Text == "Save Level 2")
        {
            if (ddlmastercategory.SelectedIndex != 0 && ddlcategroy2.SelectedIndex != 0 && txtcategory3.Text != "")
            {
                SaveCodeInnerSub();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record saved successfully')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('All field fill mandatory')", true);
            }
        }
        else if (btnsave.Text == "Save Level 3")
        {
            if (ddlmastercategory.SelectedIndex != 0 && ddlcategroy2.SelectedIndex != 0 && ddllabel2.SelectedIndex != 0 && txtlevel3.Text != "")
            {
                SaveCodeInnerSubSub();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record saved successfully')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('All field fill mandatory')", true);
            }
        }
    }
    protected void BindMasterCategory()
    {
        DataTable DtMasterCategroy = new DataTable();
        if (DisplayPanel.ToString() == "Panel2")
        {
            DtMasterCategroy = Lo.RetriveMasterCategoryDate(0, "", "", "", "", "Select");
        }
        else if (DisplayPanel.ToString() == "Panel3")
        {
            DtMasterCategroy = Lo.RetriveMasterCategoryDate(0, "2", "", "", "", "SelectFlag");
        }
        else if (DisplayPanel.ToString() == "Panel4")
        {
            DtMasterCategroy = Lo.RetriveMasterCategoryDate(0, "3", "", "", "", "SelectFlag3");
        }
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
    protected void BindMasterInnerSubCategory()
    {
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(ddlmastercategory.SelectedItem.Value), "", "", "SelectInnerMaster", "");
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlcategroy2, DtMasterCategroy, "SCategoryName", "SCategoryId");
            ddlcategroy2.Items.Insert(0, "Select");
        }
        else
        {
            ddlcategroy2.Items.Insert(0, "Select");
        }
    }
    protected void BindMasterInnerSubCategorySub()
    {
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(ddlcategroy2.SelectedItem.Value), "", "", "SubSelectID", "");
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddllabel2, DtMasterCategroy, "SCategoryName", "SCategoryId");
            ddllabel2.Items.Insert(0, "Select");
        }
        else
        {
            ddllabel2.Items.Insert(0, "Select");
        }
    }
    protected void ddlmastercategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMasterInnerSubCategory();
    }
    protected void ddlcategroy2_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMasterInnerSubCategorySub();
    }
}