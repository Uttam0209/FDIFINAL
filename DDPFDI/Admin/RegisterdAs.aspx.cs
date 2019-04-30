﻿using System;
using System.Collections.Specialized;
using System.Data;
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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != null)
        {
            if (!IsPostBack)
            {
                string id = objEnc.DecryptData(Request.QueryString["id"].ToString().Replace(" ", "+"));
                DisplayPanel = objEnc.DecryptData(Request.QueryString["mu"].ToString().Replace(" ", "+"));
                lblPageName.Text = id;
                ShowHidePanel();
            }
        }
    }
    protected void ShowHidePanel()
    {
        if (DisplayPanel.ToString() == "Panel1")
        {
            divcategory1textbox.Visible = true;
            btnsave.Text = "Save Category";
        }
        else if (DisplayPanel.ToString() == "Panel2")
        {
            divcategory1dropdown.Visible = true;
            divcategory2textbox.Visible = true;
            divcategory1textbox.Visible = false;
            btnsave.Text = "Save SubCategory";
            BindMasterCategory();
            ddlmastercategory.AutoPostBack = false;
        }
        else if (DisplayPanel.ToString() == "Panel3")
        {
            divcategory1dropdown.Visible = true;
            divcategory2ddl.Visible = true;
            divcategory3textbox.Visible = true;
            btnsave.Text = "Save";
            BindMasterInnerSubCategory();
         //   BindMasterSubCategory();
            ddlmastercategory.AutoPostBack = true;
        }
    }
    protected void cleartext()
    {
        txtcategory3.Text = "";
        txtmastercategory.Text = "";
        txtsubcategory.Text = "";
    }
    protected void btncancle_Click(object sender, EventArgs e)
    {
        cleartext();
    }
    protected void SaveCode()
    {
        DataTable StrCat = Lo.RetriveMasterCategoryDate(0, Co.RSQandSQLInjection(txtmastercategory.Text, "soft"), "", "Insert");
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
        DataTable StrCat = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(ddlmastercategory.SelectedItem.Value), Co.RSQandSQLInjection(txtsubcategory.Text, "soft"), "0", "InsertInnerID");
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
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (btnsave.Text == "Save Category")
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
        else if (btnsave.Text == "Save SubCategory")
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
        else if (btnsave.Text == "Save")
        {
            if (ddlmastercategory.SelectedIndex != 0 && ddlcategroy2.SelectedIndex != 0 && txtcategory3.Text != "")
            {
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
        DataTable DtMasterCategroy = Lo.RetriveMasterCategoryDate(0, "", "", "Select");
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlmastercategory, DtMasterCategroy, "MCategoryName", "MCategoryID");
            ddlmastercategory.Items.Insert(0, "Select Category");
        }
        else
        {
            ddlmastercategory.Items.Insert(0, "Select Category");
        }
    }
    protected void BindMasterSubCategory()
    {
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(ddlmastercategory.SelectedItem.Value), "", "", "Select");
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlcategroy2, DtMasterCategroy, "SCategoryName", "SCategoryId");
            ddlcategroy2.Items.Insert(0, "Select Label 2");
        }
        else
        {
            ddlcategroy2.Items.Insert(0, "Select Label 2");
        }
    }
    protected void BindMasterInnerSubCategory()
    {
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "", "", "SelectInnerMaster");
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlmastercategory, DtMasterCategroy, "SCategoryName", "SCategoryId");
            ddlmastercategory.Items.Insert(0, "Select Level 1");
        }
        else
        {
            ddlmastercategory.Items.Insert(0, "Select Level 1");
        }
    }

    protected void ddlmastercategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMasterSubCategory();
    }
}