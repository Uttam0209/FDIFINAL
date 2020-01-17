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

public partial class Vendor_V_Declaration : System.Web.UI.Page
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
            }
        }
        else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert",
                   "alert('Session Expired,Please login again');window.location='VendorLogin'", true);
    }
    #endregion
    protected void ddldebarredgovtcont_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldebarredgovtcont.SelectedItem.Text == "Yes")
        { chkcontracts.Visible = true; }
        else
        { chkcontracts.Visible = false; }
    }
    protected void chkcontracts_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < chkcontracts.Items.Count; i++)
        {
            if (chkcontracts.Items[i].Value == "1" && chkcontracts.Items[i].Selected == true)
            {
                divfin.Visible = true;
            }
            else if (chkcontracts.Items[i].Value == "2" && chkcontracts.Items[i].Selected == true)
            {
                div12.Visible = true;
            }
            else if (chkcontracts.Items[i].Value == "3" && chkcontracts.Items[i].Selected == true)
            {
                div13.Visible = true;
            }
            else if (chkcontracts.Items[i].Value == "4" && chkcontracts.Items[i].Selected == true)
            {
                div14.Visible = true;
            }
            else
            {
                if (chkcontracts.Items[i].Value == "1" && chkcontracts.Items[i].Selected == false)
                { divfin.Visible = false; }
                else if (chkcontracts.Items[i].Value == "2" && chkcontracts.Items[i].Selected == false)
                { div12.Visible = false; }
                else if (chkcontracts.Items[i].Value == "3" && chkcontracts.Items[i].Selected == false)
                { div13.Visible = false; }
                else if (chkcontracts.Items[i].Value == "4" && chkcontracts.Items[i].Selected == false)
                { div14.Visible = false; }
            }
        }
    }
}