using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;
using Encryption;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Specialized;

public partial class Vendor_V_CheckList : System.Web.UI.Page
{
    #region Variable
    Logic Lo = new Logic();
    Cryptography Enc = new Cryptography();
    DataUtility Co = new DataUtility();
    HybridDictionary HySaveVendorRegisdetail = new HybridDictionary();
    private string _msg = string.Empty;
    private string _sysMsg = string.Empty;
    Int64 Mid = 0;
    #endregion
    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] != null)
        {
            if (!Page.IsPostBack)
            {
                bindDropDown();
            }
        }
        else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert",
                   "alert('Session Expired,Please login again');window.location='VendorLogin'", true);
    }
    #endregion
    protected void bindDropDown()
    {
        DataTable DtDrop = Lo.RetriveVendor(0, "", "", "CheckList");
        if (DtDrop.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddltypeofchk, DtDrop, "CategoryName", "ListID");
            ddltypeofchk.Items.Insert(0, "Select");
        }
    }
    protected void bindDropDownWithID()
    {
        DataTable Dtchk = Lo.RetriveVendor(Convert.ToInt64(ddltypeofchk.SelectedItem.Value), "", "", "CheckListID");
        if (Dtchk.Rows.Count > 0)
        {
            Co.FillCheckBox(CheckBoxList3, Dtchk, "Name", "mID");
        }
    }
    protected void ddltypeofchk_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltypeofchk.SelectedItem.Text != "Select")
        {
            CheckBoxList3.Visible = true;
            bindDropDownWithID();
        }
        else
        { CheckBoxList3.Visible = false; }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (ddltypeofchk.SelectedItem.Text != "Select")
        { }
        else
        { }
    }
}