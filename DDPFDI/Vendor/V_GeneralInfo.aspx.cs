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

public partial class Vendor_V_GeneralInfo : System.Web.UI.Page
{
    #region Variable
    Logic Lo = new Logic();
    Cryptography Enc = new Cryptography();
    DataUtility Co = new DataUtility();
    HybridDictionary HySaveVendorRegisdetail = new HybridDictionary();
    private string _msg = string.Empty;
    private string _sysMsg = string.Empty;
    #endregion
    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] != null)
        {
            if (!IsPostBack)
            {
                #region PageLoad Grid Functions
                SetInitialRow();
                BindTypeOfBusiness();
                Bindbusinesssector();
                LoadCheckStatus();
                #endregion
            }
        }
        else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert",
                   "alert('Session Expired,Please login again');window.location='VendorLogin'", true);
    }
    protected void LoadCheckStatus()
    {
        #region Registration Date Retrive
        DataTable DtGetRegisVendor1 = Lo.RetriveVendor(0, Enc.DecryptData(Session["VendorRefNo"].ToString()), "", "RVendGInfo");
        if (DtGetRegisVendor1.Rows.Count > 0)
        {
            if (DtGetRegisVendor1.Rows[0]["RegistrationCategory"].ToString() != "")
            {
                ddlregiscategory.Text = DtGetRegisVendor1.Rows[0]["RegistrationCategory"].ToString();
                ddlregiscategory.Enabled = false;
            }
            if (DtGetRegisVendor1.Rows[0]["TypeOfBuisness"].ToString() != "")
            {
                ddltypeofbusiness.Items.FindByValue(DtGetRegisVendor1.Rows[0]["TypeOfBuisness"].ToString()).Selected = true;
                ddltypeofbusiness.Enabled = false;
            }
            if (DtGetRegisVendor1.Rows[0]["BusinessSector"].ToString() != "")
            {
                ddlbusinesssector.Items.FindByValue(DtGetRegisVendor1.Rows[0]["BusinessSector"].ToString()).Selected = true;
                ddlbusinesssector.Enabled = false;
            }
        }
        DataTable DtGetRegisVendor = Lo.RetriveVendor(0, Enc.DecryptData(Session["VendorRefNo"].ToString()), "", "RetriveData");
        if (DtGetRegisVendor.Rows.Count > 0)
        {
            if (DtGetRegisVendor.Rows[0]["LandlineNo"].ToString() != "" && DtGetRegisVendor.Rows[0]["FaxNo"].ToString() != "")
            {
                if (DtGetRegisVendor.Rows[0]["Date_Incorportaion_Company"].ToString() != "")
                {
                    DateTime DtInc = Convert.ToDateTime(DtGetRegisVendor.Rows[0]["Date_Incorportaion_Company"].ToString());
                    string DTIncString = DtInc.ToString("MM/dd/yyyy");
                    txtdateofincorofthecompany.Text = DTIncString.ToString();
                }
                else
                {
                    txtdateofincorofthecompany.Text = "";
                }
                txtstdcode.Text = DtGetRegisVendor.Rows[0]["LandlineNo"].ToString().Substring(0, 3);
                txtphoneno.Text = DtGetRegisVendor.Rows[0]["LandlineNo"].ToString().Substring(4);
                txtfaxstdcode.Text = DtGetRegisVendor.Rows[0]["FaxNo"].ToString().Substring(0, 3);
                txtfaxphoneno.Text = DtGetRegisVendor.Rows[0]["FaxNo"].ToString().Substring(4);
                DataTable DtMultigrid1 = Lo.RetriveVendor(0, Enc.DecryptData(Session["VendorRefNo"].ToString()), "", "MulGrid1");
                if (DtMultigrid1.Rows.Count > 0)
                {
                    ViewState["EnterNameof"] = DtMultigrid1;
                    gvgridNameof.DataSource = DtMultigrid1;
                    gvgridNameof.DataBind();
                    gvgridNameof.Visible = true;
                    gridNameof.Visible = false;
                }
                else
                {
                    gvgridNameof.Visible = false;
                    gridNameof.Visible = true;
                }
                hfGenInfoID.Value = DtGetRegisVendor.Rows[0]["VendorID"].ToString();
                btnsubmit.Text = "Update";
                btncancel.Visible = false;
            }
            else
            {
                btnsubmit.Text = "Submit";
                btncancel.Visible = true;
            }
        }
        #endregion
    }
    #endregion
    protected void BindTypeOfBusiness()
    {
        DataTable DtMasterCategroyBusiness = Lo.RetriveMasterData(1, "", "", 0, "", "", "TypeofBusiness");
        if (DtMasterCategroyBusiness.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddltypeofbusiness, DtMasterCategroyBusiness, "VendorSubCatName", "VendorSubCatID");
            ddltypeofbusiness.Items.Insert(0, "Select");
        }
    }
    protected void Bindbusinesssector()
    {
        DataTable DtMasterCategroyBusiness = Lo.RetriveMasterData(2, "", "", 0, "", "", "BuisnessSector");
        if (DtMasterCategroyBusiness.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlbusinesssector, DtMasterCategroyBusiness, "VendorSubCatName", "VendorSubCatID");
            ddlbusinesssector.Items.Insert(0, "Select");
        }
    }
    protected void ddltypeofbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltypeofbusiness.SelectedItem.Text == "MSME")
        { divmsmetypeofbuisness.Visible = true; }
        else if (ddltypeofbusiness.SelectedItem.Value == "10")
        { divothersdetails.Visible = true; }
        else
        { divothersdetails.Visible = false; divmsmetypeofbuisness.Visible = false; }
    }
    protected void ddlscaleofbuisness_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlscaleofbuisness.SelectedItem.Text == "Small")
        { cermsme.Visible = true; }
        else
        { cermsme.Visible = false; }
    }
    protected void chkownership_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < chkownership.Items.Count; i++)
        {
            if (chkownership.Items[i].Value == "1" && chkownership.Items[i].Selected == true)
            {
                per1.Visible = true;
                chkownership.Items[1].Enabled = false;
            }
            else if (chkownership.Items[i].Value == "2" && chkownership.Items[i].Selected == true)
            {
                chkownership.Items[0].Enabled = false;
            }
            else if (chkownership.Items[i].Value == "3" && chkownership.Items[i].Selected == true)
            {
                per2.Visible = true;
            }
            else
            {
                if (chkownership.Items[i].Value == "1" && chkownership.Items[i].Selected == false)
                {
                    per1.Visible = false;
                    chkownership.Items[1].Enabled = true;
                }
                else if (chkownership.Items[i].Value == "2" && chkownership.Items[i].Selected == true)
                {
                    chkownership.Items[0].Enabled = true;
                }
                else if (chkownership.Items[i].Value == "3" && chkownership.Items[i].Selected == false)
                { per2.Visible = false; }
            }
        }
    }
    //Add Grid of Please Enter Name of Code
    #region Grid of Please Enter Name of Code
    private void SetInitialRow()
    {
        try
        {
            //Create false table
            DataTable dtenternameof = new DataTable();
            DataRow drenternameof = null;
            dtenternameof.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dtenternameof.Columns.Add(new DataColumn("EnterName", typeof(string)));
            dtenternameof.Columns.Add(new DataColumn("Name", typeof(string)));
            dtenternameof.Columns.Add(new DataColumn("Designation", typeof(string)));
            dtenternameof.Columns.Add(new DataColumn("DinNo", typeof(string)));
            dtenternameof.Columns.Add(new DataColumn("MobileNo", typeof(long)));
            drenternameof = dtenternameof.NewRow();
            drenternameof["RowNumber"] = 1;
            drenternameof["EnterName"] = string.Empty;
            drenternameof["Name"] = string.Empty;
            drenternameof["Designation"] = string.Empty;
            drenternameof["DinNo"] = string.Empty;
            drenternameof["MobileNo"] = 0;
            dtenternameof.Rows.Add(drenternameof);
            ViewState["EnterNameof"] = dtenternameof;
            gridNameof.DataSource = dtenternameof;
            gridNameof.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void AddNewRowToGrid()
    {
        try
        {
            //int rowIndex = 0;
            if (ViewState["EnterNameof"] != null)
            {
                DataTable dtCurrentTableNameCode = (DataTable)ViewState["EnterNameof"];
                DataRow drCurrentRowNameCode = null;
                if (dtCurrentTableNameCode.Rows.Count > 0)
                {
                    drCurrentRowNameCode = dtCurrentTableNameCode.NewRow();
                    drCurrentRowNameCode["RowNumber"] = dtCurrentTableNameCode.Rows.Count + 1;
                    dtCurrentTableNameCode.Rows.Add(drCurrentRowNameCode);
                    ViewState["EnterNameof"] = dtCurrentTableNameCode;
                    for (int i = 0; i < dtCurrentTableNameCode.Rows.Count - 1; i++)
                    {
                        DropDownList ddlenternameof = (DropDownList)gridNameof.Rows[i].Cells[1].FindControl("ddlenternameof");
                        TextBox TextBox1 = (TextBox)gridNameof.Rows[i].Cells[2].FindControl("txtEnterNameof");
                        TextBox TextBox2 = (TextBox)gridNameof.Rows[i].Cells[3].FindControl("txtdesignation");
                        TextBox TextBox3 = (TextBox)gridNameof.Rows[i].Cells[4].FindControl("txtdinno");
                        TextBox TextBox4 = (TextBox)gridNameof.Rows[i].Cells[5].FindControl("txtmobno");

                        dtCurrentTableNameCode.Rows[i]["EnterName"] = ddlenternameof.Text;
                        dtCurrentTableNameCode.Rows[i]["Name"] = TextBox1.Text;
                        dtCurrentTableNameCode.Rows[i]["Designation"] = TextBox2.Text;
                        dtCurrentTableNameCode.Rows[i]["DinNo"] = TextBox3.Text;
                        dtCurrentTableNameCode.Rows[i]["MobileNo"] = Convert.ToInt64(TextBox4.Text);
                    }
                    gridNameof.DataSource = dtCurrentTableNameCode;
                    gridNameof.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void SetPreviousData()
    {
        try
        {
            int rowIndex = 0;
            if (ViewState["EnterNameof"] != null)
            {
                DataTable dtEnterNameof = (DataTable)ViewState["EnterNameof"];
                if (dtEnterNameof.Rows.Count > 0)
                {
                    for (int i = 0; i < dtEnterNameof.Rows.Count; i++)
                    {
                        DropDownList ddlNameof_1 = (DropDownList)gridNameof.Rows[rowIndex].Cells[1].FindControl("ddlenternameof");
                        TextBox TextBox_1 = (TextBox)gridNameof.Rows[rowIndex].Cells[2].FindControl("txtEnterNameof");
                        TextBox TextBox_2 = (TextBox)gridNameof.Rows[rowIndex].Cells[3].FindControl("txtdesignation");
                        TextBox TextBox_3 = (TextBox)gridNameof.Rows[rowIndex].Cells[4].FindControl("txtdinno");
                        TextBox TextBox_4 = (TextBox)gridNameof.Rows[rowIndex].Cells[5].FindControl("txtmobno");
                        if (i < dtEnterNameof.Rows.Count - 1)
                        {
                            ddlNameof_1.ClearSelection();
                            ddlNameof_1.Items.FindByValue(dtEnterNameof.Rows[i]["EnterName"].ToString()).Selected = true;
                            TextBox_1.Text = dtEnterNameof.Rows[i]["Name"].ToString();
                            TextBox_2.Text = dtEnterNameof.Rows[i]["Designation"].ToString();
                            TextBox_3.Text = dtEnterNameof.Rows[i]["DinNo"].ToString();
                            TextBox_4.Text = dtEnterNameof.Rows[i]["MobileNo"].ToString();
                        }
                        rowIndex++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void ButtonAddEnterNameof_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }
    protected void gridNameof_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dtgridNameofrowcreated = (DataTable)ViewState["EnterNameof"];
                LinkButton lb = (LinkButton)e.Row.FindControl("LinkButton1");
                if (lb != null)
                {
                    if (dtgridNameofrowcreated.Rows.Count > 1)
                    {
                        if (e.Row.RowIndex == dtgridNameofrowcreated.Rows.Count - 1)
                        {
                            lb.Visible = false;
                        }
                    }
                    else
                    {
                        lb.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            if (ViewState["EnterNameof"] != null)
            {
                DataTable dtremovegridNameof = (DataTable)ViewState["EnterNameof"];
                if (dtremovegridNameof.Rows.Count > 1)
                {
                    if (gvRow.RowIndex < dtremovegridNameof.Rows.Count - 1)
                    {
                        dtremovegridNameof.Rows.Remove(dtremovegridNameof.Rows[rowID]);
                        ResetRowID(dtremovegridNameof);
                    }
                }
                ViewState["EnterNameof"] = dtremovegridNameof;
                gridNameof.DataSource = dtremovegridNameof;
                gridNameof.DataBind();
            }
            SetPreviousData();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void ResetRowID(DataTable dtremovecount)
    {
        try
        {
            int rowNumber = 1;
            if (dtremovecount.Rows.Count > 0)
            {
                foreach (DataRow row in dtremovecount.Rows)
                {
                    row[0] = rowNumber;
                    rowNumber++;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    DataTable dtSaveNameof = new DataTable();
    protected void SaveCodeForNameof()
    {
        int rowIndex = 0;
        DataTable dtSaveNameof = new DataTable();
        dtSaveNameof.Columns.Add(new DataColumn("RowNumber", typeof(Int16)));
        dtSaveNameof.Columns.Add(new DataColumn("EnterName", typeof(string)));
        dtSaveNameof.Columns.Add(new DataColumn("Name", typeof(string)));
        dtSaveNameof.Columns.Add(new DataColumn("Designation", typeof(string)));
        dtSaveNameof.Columns.Add(new DataColumn("DinNo", typeof(string)));
        dtSaveNameof.Columns.Add(new DataColumn("MobileNo", typeof(string)));
        DataRow drCurrentRowNameCode = null;
        for (int i = 0; gridNameof.Rows.Count > i; i++)
        {
            DropDownList ddlsavenameof1 = (DropDownList)gridNameof.Rows[i].Cells[1].FindControl("ddlenternameof");
            TextBox txtsavenameof1 = (TextBox)gridNameof.Rows[i].Cells[2].FindControl("txtEnterNameof");
            TextBox txtsavenameof2 = (TextBox)gridNameof.Rows[i].Cells[3].FindControl("txtdesignation");
            TextBox txtsavenameof3 = (TextBox)gridNameof.Rows[i].Cells[4].FindControl("txtdinno");
            TextBox txtsavenameof4 = (TextBox)gridNameof.Rows[i].Cells[5].FindControl("txtmobno");
            if (ddlsavenameof1.SelectedItem.Text != "Select" && txtsavenameof1.Text != "")
            {
                drCurrentRowNameCode = dtSaveNameof.NewRow();
                drCurrentRowNameCode["RowNumber"] = i + 1;
                drCurrentRowNameCode["EnterName"] = ddlsavenameof1.SelectedItem.Text;
                drCurrentRowNameCode["Name"] = txtsavenameof1.Text;
                drCurrentRowNameCode["Designation"] = txtsavenameof2.Text;
                drCurrentRowNameCode["DinNo"] = txtsavenameof3.Text;
                drCurrentRowNameCode["MobileNo"] = txtsavenameof4.Text;
                dtSaveNameof.Rows.Add(drCurrentRowNameCode);
            }
        }
        ViewState["EnterNameof"] = dtSaveNameof;
    }
    #endregion
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlbusinesssector.SelectedItem.Text != "Select")
            {
                SaveRegistration();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Fill all details mandatory.')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        cleartext();
    }
    protected void SaveRegistration()
    {
        if (hfGenInfoID.Value != "")
        { HySaveVendorRegisdetail["VendorDetailID"] = hfGenInfoID.Value; }
        else
        {
            HySaveVendorRegisdetail["VendorDetailID"] = 0;
        }
        HySaveVendorRegisdetail["VendorRefNo"] = Enc.DecryptData(Session["VendorRefNo"].ToString());
        HySaveVendorRegisdetail["RegistrationCategory"] = Co.RSQandSQLInjection(ddlregiscategory.SelectedItem.Text, "soft");
        HySaveVendorRegisdetail["TypeOfOwnership"] = Co.RSQandSQLInjection(ddltypeofbusiness.SelectedItem.Value, "soft");
        if (ddltypeofbusiness.SelectedItem.Text == "")
        {
            HySaveVendorRegisdetail["ScaleofBuisness"] = Co.RSQandSQLInjection(ddlscaleofbuisness.SelectedItem.Text, "soft");
            HySaveVendorRegisdetail["Ownership"] = Co.RSQandSQLInjection(chkownership.SelectedItem.Value, "soft");
            HySaveVendorRegisdetail["PercentofOwnership"] = Co.RSQandSQLInjection(txtpercent1.Text + "_" + txtpercent2.Text, "soft");
            string FilePathName = Enc.DecryptData(Session["VendorRefNo"].ToString()) + "_" + DateTime.Now.ToString("hh_mm_ss") + fun.FileName;
            fun.SaveAs(HttpContext.Current.Server.MapPath("/Upload/VendorImage") + "\\" + FilePathName);
            HySaveVendorRegisdetail["FileofOwnership"] = Co.RSQandSQLInjection(FilePathName, "soft");
        }
        else
        {
            HySaveVendorRegisdetail["ScaleofBuisness"] = "";
            HySaveVendorRegisdetail["Ownership"] = "";
            HySaveVendorRegisdetail["PercentofOwnership"] = "";
            HySaveVendorRegisdetail["FileofOwnership"] = "";
        }
        HySaveVendorRegisdetail["BuisnessSector"] = Co.RSQandSQLInjection(ddlbusinesssector.SelectedItem.Value, "soft");
        if (txtdateofincorofthecompany.Text != "")
        {
            DateTime DtIcor_Comp = Convert.ToDateTime(txtdateofincorofthecompany.Text);
            string Date_Icor_Comp = DtIcor_Comp.ToString("dd-MMM-yyyy");
            HySaveVendorRegisdetail["Date_Incorportaion_Company"] = Co.RSQandSQLInjection(Date_Icor_Comp.ToString(), "soft");
        }
        else
        { HySaveVendorRegisdetail["Date_Incorportaion_Company"] = null; }

        HySaveVendorRegisdetail["ContactNo"] = Co.RSQandSQLInjection(txtstdcode.Text + "_" + txtphoneno.Text, "soft");
        HySaveVendorRegisdetail["FaxNo"] = Co.RSQandSQLInjection(txtfaxstdcode.Text + "_" + txtfaxphoneno.Text, "soft");
        HySaveVendorRegisdetail["Type"] = "Save_FP";
        SaveCodeForNameof();
        DataTable DtFIrstGrid = (DataTable)ViewState["EnterNameof"];
        string str = Lo.SaveVendorGeneralInfo(HySaveVendorRegisdetail, DtFIrstGrid, out _sysMsg, out _msg);
        if (str != "")
        {
            LoadCheckStatus();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Successfully save general information')", true);
        }
        else
        { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not saved.')", true); }
    }
    protected void cleartext()
    {
        chkownership.ClearSelection();
        txtpercent1.Text = "";
        txtpercent2.Text = "";
        fun.Attributes.Clear();
        txtdateofincorofthecompany.Text = "";
        txtstdcode.Text = "";
        txtphoneno.Text = "";
        txtfaxstdcode.Text = "";
        txtfaxphoneno.Text = "";
        ViewState["EnterName"] = null;
        gridNameof.DataSource = null;
        gridNameof.DataBind();
        SetInitialRow();
    }
    protected void BindUpdatePopupCode()
    {
        DataTable DtMultigrid1 = Lo.RetriveVendor(0, Enc.DecryptData(Session["VendorRefNo"].ToString()), "", "MulGrid1");
        if (DtMultigrid1.Rows.Count > 0)
        {
            ViewState["EnterNameof"] = DtMultigrid1;
            gvgridNameof.DataSource = DtMultigrid1;
            gvgridNameof.DataBind();
            gvgridNameof.Visible = true;
            gridNameof.Visible = false;
        }
        else
        {
            gvgridNameof.Visible = false;
            gridNameof.Visible = true;
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnupdate.Text == "Edit & Update")
            {
                if (txtdinnoedit.Text != "")
                {
                    Int32 ESaveID = Lo.UpdateStatusEdit(Convert.ToInt64(ViewState["editidgrid"]), Enc.DecryptData(Session["VendorRefNo"].ToString()), ddlenternameedit.SelectedItem.Text, txtnameedit.Text, txtdesignationedit.Text, txtdinnoedit.Text, txtmobnoedit.Text);
                    if (ESaveID != 0)
                    {
                        ddlenternameedit.SelectedIndex = -1;
                        txtnameedit.Text = "";
                        txtdesignationedit.Text = "";
                        txtdinnoedit.Text = "";
                        txtmobnoedit.Text = "";
                        ViewState["editidgrid"] = null;
                        BindUpdatePopupCode();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record update successfully')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not update successfully')", true);
                    }
                }
            }
            else if (btnupdate.Text == "Submit")
            {
                Int32 ESaveID = Lo.InsertStatusEdit(Enc.DecryptData(Session["VendorRefNo"].ToString()), "EnterNameof", ddlenternameedit.SelectedItem.Text, txtnameedit.Text, txtdesignationedit.Text, txtdinnoedit.Text, txtmobnoedit.Text);
                if (ESaveID != 0)
                {
                    ddlenternameedit.SelectedIndex = -1;
                    txtnameedit.Text = "";
                    txtdesignationedit.Text = "";
                    txtdinnoedit.Text = "";
                    txtmobnoedit.Text = "";
                    BindUpdatePopupCode();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record save successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not save successfully')", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void gvgridNameof_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Add")
            {
                btnupdate.Text = "Submit";
                txtnameedit.Text = "";
                txtdesignationedit.Text = "";
                txtdinnoedit.Text = "";
                txtmobnoedit.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "changePass", "showPopup();", true);
            }
            else if (e.CommandName == "Upda")
            {
                btnupdate.Text = "Edit & Update";
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvgridNameof.Rows[rowIndex];
                HiddenField hfn = (HiddenField)gvgridNameof.Rows[rowIndex].FindControl("hfmid");
                ddlenternameedit.SelectedValue = row.Cells[0].Text;
                txtnameedit.Text = row.Cells[1].Text;
                txtdesignationedit.Text = row.Cells[2].Text;
                txtdinnoedit.Text = row.Cells[3].Text;
                txtmobnoedit.Text = row.Cells[4].Text;
                ViewState["editidgrid"] = hfn.Value;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "changePass", "showPopup();", true);
            }
            else if (e.CommandName == "Del")
            {
                Int32 Delid = Lo.DeleteEditGrid(Convert.ToInt32(e.CommandArgument.ToString()));
                if (Delid != 0)
                {
                    BindUpdatePopupCode();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record deleted successfull.')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not deleted.')", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
}