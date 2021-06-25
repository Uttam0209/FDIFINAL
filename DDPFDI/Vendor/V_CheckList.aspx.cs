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

public partial class Vendor_V_CheckList1 : System.Web.UI.Page
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
        if (Session["VUser"] != null)
        {
            if (!Page.IsPostBack)
            {
                lbcomp.Text = Session["VCompName"].ToString();
                bindDropDown();
                PageLoadM();
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
            DataTable DtGetRegisVendor1 = Lo.RetriveVendor(0, Enc.DecryptData(Session["VendorRefNo"].ToString()), "", "RVendGInfo"); 
            Co.FillDropdownlist(ddltypeofchk, DtDrop, "CategoryName", "ListID");           
            ddltypeofchk.SelectedItem.Text = DtGetRegisVendor1.Rows[0]["RegistrationCategory"].ToString();
            Catid.Value = ddltypeofchk.SelectedValue;
            int CatId = Convert.ToInt32(Catid.Value);
            bindDropDownWithID(CatId);
        }
    }
    protected void bindDropDownWithID(int catid)
    {
        DataTable Dtchk = Lo.RetriveVendor(catid, "", "", "CheckListID");
        if (Dtchk.Rows.Count > 0)
        {
            Co.FillCheckBox(CheckBoxList3, Dtchk, "Name", "mID");
            CheckBoxList3.Visible = true;
        }
        else
        {
            CheckBoxList3.Visible = false;
        }
    }
    protected void ddltypeofchk_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltypeofchk.SelectedItem.Text != "Select")
        {
            CheckBoxList3.Visible = true;
        }
        else
        { CheckBoxList3.Visible = false; }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        String ChecklistID = string.Empty;
        try
        {
            if (ddltypeofchk.SelectedItem.Text != "Select")
            {
                foreach (ListItem lst in CheckBoxList3.Items)
                {
                    if (lst.Selected == true)
                    {
                        ChecklistID += lst.Value + ',';
                    }
                }
                string str = Lo.SaveVendorCheckList(ChecklistID, Enc.DecryptData(Session["VendorRefNo"].ToString()), out _sysMsg, out _msg);
                if (str != "")
                {
                    if (btnsubmit.Text == "Update")
                    {
                        PageLoadM();
                        lblmsg.Text = "Successfully update checkList information";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup2();", true);
                    }
                    else
                    {
                        lblmsg.Text = "Successfully save checkList information";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup2();", true);
                    }
                }
                else
                {
                    lblmsg.Text = "Record not saved";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup2();", true);
                }
            }
            else
            {
                lblmsg.Text = "Please select  list to check checkbox file uploaded";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup2();", true);
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup2();", true);
        }
    }
    protected void PageLoadM()
    {
        DataTable DtCheckSavedetails = Lo.RetriveVendor(0, Enc.DecryptData(Session["VendorRefNo"].ToString()), "", "CheckListDetail");
        if (DtCheckSavedetails.Rows.Count > 0)
        {
            string[] checklistv = DtCheckSavedetails.Rows[0]["CheckListID"].ToString().Split(',');
            btnsubmit.Text = "Update";
            foreach (string checklistid in checklistv)
            {
                if (checklistid.Trim() != "")
                {
                    foreach (ListItem liqa in CheckBoxList3.Items)
                    {
                        if (liqa.Value == checklistid.ToString())
                        {
                            liqa.Selected = true;
                        }
                    }
                }
            }
        }
    }
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        Response.Redirect("https://srijandefence.gov.in/FinancialInformation?mu=MRtCwN+7N6dMmohOhVozbQ==&id=YUM6Wog/7cKd56S2dApVEg==");

    }
   
}