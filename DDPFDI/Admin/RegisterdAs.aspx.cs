using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
            string id = objEnc.DecryptData(Request.QueryString["id"].ToString().Replace(" ", "+"));
            DisplayPanel = objEnc.DecryptData(Request.QueryString["mu"].ToString().Replace(" ", "+"));
            lblPageName.Text = id;
            ShowHidePanel();
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
            btnsave.Text = "Save SubCategroy";
        }
        else if (DisplayPanel.ToString() == "Panel3")
        {
            divcategory1dropdown.Visible = true;
            divcategory2ddl.Visible = true;
            divcategory3textbox.Visible = true;
            btnsave.Text = "Save";
        }
    }
    protected void cleartext()
    {
        txtcategory3.Text = "";
        txtmastercategory.Text = "";
        txtsubcategory.Text = "";
        ddlcategroy2.SelectedIndex = 0;
        ddlmastercategory.SelectedIndex = 0;
    }
    protected void btncancle_Click(object sender, EventArgs e)
    {
        cleartext();
    }
    protected void SaveCode()
    {
        hySave[""] = "";
        hySave[""] = "";
        hySave[""] = "";
        hySave[""] = "";
        hySave[""] = "";
        hySave[""] = "";
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (btnsave.Text == "Save Category")
        {
            if (txtmastercategory.Text != "")
            {
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
        //DataTable DtMasterCategroy = Lo.RetriveMasterData("");
        //if (DtMasterCategroy.Rows.Count > 0)
        //{
        //    Co.FillDropdownlist(ddlmastercategory,DtMasterCategroy,"CategroyName","CategoryID");
        //}
        //else
        //{
        //}
    }
}