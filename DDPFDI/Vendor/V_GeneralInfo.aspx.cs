using BusinessLayer;
using Encryption;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Vendor_V_GeneralInfo : System.Web.UI.Page
{
    #region Variable
    Logic Lo = new Logic();
    Cryptography Enc = new Cryptography();
    DataUtility Co = new DataUtility();
    HybridDictionary HySaveVendorRegisdetail = new HybridDictionary();
    private string _msg = string.Empty;
    private string _sysMsg = string.Empty;
    string LogoPath = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["VUser"] != null)
        {
            if (!IsPostBack)
            {
                Image1.Visible = false;
                lbcomp.Text = Session["VCompName"].ToString();
                SetInitialRow();
                SetInitialRowGovt();
                LoadCheckStatus();
            }
        }
        else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert",
                   "alert('Session Expired,Please login again');window.location='VendorLogin'", true);
    }
    #region PageLoad   
    protected void LoadCheckStatus()
    {
        #region Registration Date Retrive
        try
        {
            DataTable DtGetRegisVendor1 = Lo.RetriveVendor(0, Enc.DecryptData(Session["VendorRefNo"].ToString()), "", "RVendGInfo");
            if (DtGetRegisVendor1.Rows.Count > 0)
            {
                if (DtGetRegisVendor1.Rows[0]["LogoPath"].ToString() != "")
                {
                    Image1.Visible = true;
                    Image1.ImageUrl = DtGetRegisVendor1.Rows[0]["LogoPath"].ToString();
                    string Path = DtGetRegisVendor1.Rows[0]["LogoPath"].ToString();
                    Session["LogoPath"] = Path;
                }
                else
                {
                    Image1.Visible = false;
                }
                ddlregiscategory.Text = DtGetRegisVendor1.Rows[0]["RegistrationCategory"].ToString();
                ddltypeofbusiness.Text = DtGetRegisVendor1.Rows[0]["TypeOfOwnership"].ToString();
                ddlbusinesssector.Text = DtGetRegisVendor1.Rows[0]["BuisnessSector"].ToString();
                if (DtGetRegisVendor1.Rows[0]["Date_Incorportaion_Company"].ToString() != "")
                {
                    DateTime CdateT = Convert.ToDateTime(DtGetRegisVendor1.Rows[0]["Date_Incorportaion_Company"].ToString());
                    string Cdate = Convert.ToString(CdateT.ToString("dd/MM/yyyy"));
                    if (Cdate == "01/01/1900")
                    {
                        txtdateofincorofthecompany.Text = "";
                    }
                    else
                    {
                        DateTime DtInc = Convert.ToDateTime(DtGetRegisVendor1.Rows[0]["Date_Incorportaion_Company"].ToString());
                        string DTIncString = DtInc.ToString("dd/MM/yyyy");
                        txtdateofincorofthecompany.Text = DTIncString.ToString();
                    }
                }
                else
                {
                    txtdateofincorofthecompany.Text = "";
                }
                TxtCompUrl.Text = DtGetRegisVendor1.Rows[0]["CompanyURL"].ToString();
                TxtAddress1.Text = DtGetRegisVendor1.Rows[0]["Street_Address"].ToString();
                TxtAddress2.Text = DtGetRegisVendor1.Rows[0]["Street_Address_Line_2"].ToString();
                txtmobile.Text = DtGetRegisVendor1.Rows[0]["MobileNo"].ToString();
                txtphoneno.Text = DtGetRegisVendor1.Rows[0]["ContactNo"].ToString();
                txtfaxphoneno.Text = DtGetRegisVendor1.Rows[0]["FaxNo"].ToString();
                txtState.Text = DtGetRegisVendor1.Rows[0]["State"].ToString();
                txtCity.Text = DtGetRegisVendor1.Rows[0]["City"].ToString();
                txtPinCode.Text = DtGetRegisVendor1.Rows[0]["PinCode"].ToString();
                txTanNo.Text = DtGetRegisVendor1.Rows[0]["TANNo"].ToString();
                txtuamno.Text = DtGetRegisVendor1.Rows[0]["UAMNo"].ToString();
                txtcinno.Text = DtGetRegisVendor1.Rows[0]["CINNo"].ToString();
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
                //DataTable DtMultigrid2 = Lo.RetriveVendor(0, Enc.DecryptData(Session["VendorRefNo"].ToString()), "", "MulGrid1");
                //if (DtMultigrid2.Rows.Count > 0)
                //{
                //    ViewState["EnterNameof"] = DtMultigrid2;
                //    gvgovtundertaking.DataSource = DtMultigrid2;
                //    gvgovtundertaking.DataBind();
                //    gvgovtundertaking.Visible = true;
                //    gvgovtundertakingedit.Visible = false;
                //}
                //else
                //{
                //    gvgovtundertaking.Visible = false;
                //    gvgovtundertakingedit.Visible = true;
                //}
                hfGenInfoID.Value = DtGetRegisVendor1.Rows[0]["VendorRefNo"].ToString();
                btnsubmit.Text = "Update";
                btncancel.Visible = false;
            }
        }
        catch (Exception ex)
        { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message.ToString() + "')", true); }
        #endregion
    }
    #endregion 
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
            drenternameof["MobileNo"] = 91;
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
                if (lb.Text != null || lb.Text != "")
                {
                    if (dtgridNameofrowcreated != null)
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
        dtSaveNameof.Columns.Add(new DataColumn("mProcess", typeof(string)));
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
                drCurrentRowNameCode["mProcess"] = "Insert";
                dtSaveNameof.Rows.Add(drCurrentRowNameCode);
            }
        }
        if (gridNameof.Visible == false) { }
        else
            ViewState["EnterNameof"] = dtSaveNameof;
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "changePass", "showPopup1();", true);
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "changePass", "showPopup1();", true);
            }
            else if (e.CommandName == "Del")
            {
                Int32 Delid = Lo.DeleteEditGrid(Convert.ToInt32(e.CommandArgument.ToString()));
                if (Delid != 0)
                {
                    BindUpdatePopupCode();
                }
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
        }
    }
    #endregion
    #region button code  
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlbusinesssector.Text != "")
            {
                SaveRegistration();
            }
            else
            {
                lblmsg.Text = "Fill all details mandatory.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        cleartext();
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
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#changePass').modal('hide')", true);
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#changePass').modal('hide')", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#changePass').modal('hide')", true);
            lblmsg.Text = ex.Message;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
        }
    }
    protected void btnUploadLogo_Click(object sender, EventArgs e)
    {
        if (FileUpload1.PostedFile != null)
        {
            if (FileUpload1.PostedFile.ContentLength < 1000000)
            {
                string strLogoName = DateTime.Now.ToString("hh_mm_ss_t") + FileUpload1.FileName;
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Upload/" + strLogoName));
                LogoPath = "~/Upload/" + strLogoName.ToString();
                Session["LogoPath"] = LogoPath;
                Image1.Visible = true;
                Image1.ImageUrl = "~/Upload/" + strLogoName;
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Image size must be less than 1 MB')", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Please Upload Logo !!')", true);
            //["LogoPath"] = LogoPath;
        }

    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Response.Redirect("https://srijandefence.gov.in/CompanyInformation_I?mu=KQ5FIC8PdXE=&id=YUM6Wog/7cKd56S2dApVEg==");
    }
    #endregion
    #region masterbind()
    protected void SaveRegistration()
    {
        HySaveVendorRegisdetail["VendorRefNo"] = Enc.DecryptData(Session["VendorRefNo"].ToString());
        if (Session["LogoPath"] != null)
        {
            HySaveVendorRegisdetail["LogoPath"] = Co.RSQandSQLInjection(Server.HtmlEncode(Session["LogoPath"].ToString()), "soft");
        }
        else
        {
            HySaveVendorRegisdetail["LogoPath"] = "";
        }
        HySaveVendorRegisdetail["RegistrationCategory"] = Co.RSQandSQLInjection(ddlregiscategory.Text, "soft");
        HySaveVendorRegisdetail["TypeOfOwnership"] = Co.RSQandSQLInjection(ddltypeofbusiness.Text, "soft");
        HySaveVendorRegisdetail["BuisnessSector"] = Co.RSQandSQLInjection(ddlbusinesssector.Text, "soft");
        if (txtdateofincorofthecompany.Text != "")
        {
            DateTime DtIcor_Comp = Convert.ToDateTime(txtdateofincorofthecompany.Text);
            string Date_Icor_Comp = DtIcor_Comp.ToString("yyyy-MMM-dd");
            HySaveVendorRegisdetail["Date_Incorportaion_Company"] = Co.RSQandSQLInjection(Date_Icor_Comp.ToString(), "soft");
        }
        else
        { HySaveVendorRegisdetail["Date_Incorportaion_Company"] = null; }
        HySaveVendorRegisdetail["CompanyURL"] = Co.RSQandSQLInjection(Server.HtmlEncode(TxtCompUrl.Text), "soft");
        HySaveVendorRegisdetail["Street_Address"] = Co.RSQandSQLInjection(Server.HtmlEncode(TxtAddress1.Text), "soft");
        HySaveVendorRegisdetail["Street_Address_Line_2"] = Co.RSQandSQLInjection(Server.HtmlEncode(TxtAddress2.Text), "soft");
        HySaveVendorRegisdetail["MobileNo"] = Co.RSQandSQLInjection(Server.HtmlEncode(txtmobile.Text), "soft");
        HySaveVendorRegisdetail["ContactNo"] = Co.RSQandSQLInjection(Server.HtmlEncode(txtphoneno.Text), "soft");
        HySaveVendorRegisdetail["FaxNo"] = Co.RSQandSQLInjection(Server.HtmlEncode(txtfaxphoneno.Text), "soft");
        HySaveVendorRegisdetail["State"] = Co.RSQandSQLInjection(Server.HtmlEncode(txtState.Text), "soft");
        HySaveVendorRegisdetail["City"] = Co.RSQandSQLInjection(Server.HtmlEncode(txtCity.Text), "soft");
        HySaveVendorRegisdetail["PinCode"] = Co.RSQandSQLInjection(Server.HtmlEncode(txtPinCode.Text), "soft");
        HySaveVendorRegisdetail["TANNo"] = Co.RSQandSQLInjection(Server.HtmlEncode(txTanNo.Text), "soft");
        HySaveVendorRegisdetail["UAMNo"] = Co.RSQandSQLInjection(Server.HtmlEncode(txtuamno.Text), "soft");
        HySaveVendorRegisdetail["CINNo"] = Co.RSQandSQLInjection(Server.HtmlEncode(txtcinno.Text), "soft");
        HySaveVendorRegisdetail["Type"] = "Save_FP";
        SaveCodeForNameof();
        DataTable DtFIrstGrid = (DataTable)ViewState["EnterNameof"];
        string str = Lo.SaveVendorGeneralInfo(HySaveVendorRegisdetail, DtFIrstGrid, out _sysMsg, out _msg);
        if (str != "" && _msg.ToString() == "Save")
        {
            cleartext();
            LoadCheckStatus();
            ViewState["EnterNameof"] = null;
            lblmsg.Text = "Successfully save general information";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
        }
        else
        {
            lblmsg.Text = "User Error:- Record not saved. Technical Error:- " + _msg + "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
        }
    }
    protected void cleartext()
    {
        txtdateofincorofthecompany.Text = "";
        txtphoneno.Text = "";
        txtfaxphoneno.Text = "";
        TxtAddress1.Text = "";
        TxtAddress2.Text = "";
        txtmobile.Text = "";
        txtPinCode.Text = "";
        txtCity.Text = "";
        txtState.Text = "";
        TxtCompUrl.Text = "";
        ViewState["EnterName"] = null;
        Session["LogoPath"] = "";
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
    protected void Upload(object sender, EventArgs e)
    {
        string fileName = System.IO.Path.GetFileName(FileUpload1.FileName);
    }
    #endregion
    #region StateCityPinCode
    [WebMethod]
    public static string[] GetStates(string prefix, int parentId)
    {
        SqlCommand cmd = new SqlCommand();
        string query = "SELECT distinct StateId,StateName FROM tbl_mst_StateMaster WHERE StateName LIKE @Prefix + '%'";
        cmd.Parameters.AddWithValue("@Prefix", prefix);
        cmd.CommandText = query;
        return PopulateAutoComplete(cmd);
    }
    [WebMethod]
    public static string[] GetCities(string prefix, int parentId)
    {
        SqlCommand cmd = new SqlCommand();
        string query = "SELECT CityId,CityName FROM tbl_mst_CityMaster WHERE CityName LIKE @Prefix + '%' AND StateId=@StateId";
        cmd.Parameters.AddWithValue("@Prefix", prefix);
        cmd.Parameters.AddWithValue("@StateId", parentId);
        cmd.CommandText = query;
        return PopulateAutoComplete(cmd);
    }
    [WebMethod]
    public static string[] GetPinCodes(string prefix, int parentId)
    {
        SqlCommand cmd = new SqlCommand();
        string query = "SELECT distinct PinCode,PinCode FROM tbl_mst_PinMaster WHERE PinCode LIKE @Prefix + '%' AND CityId=@CityId";
        cmd.Parameters.AddWithValue("@Prefix", prefix);
        cmd.Parameters.AddWithValue("@CityId", parentId);
        cmd.CommandText = query;
        return PopulateAutoComplete(cmd);
    }
    private static string[] PopulateAutoComplete(SqlCommand cmd)
    {
        Cryptography Enc = new Cryptography();
        List<string> autocompleteItems = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = Enc.DecryptData(ConfigurationManager.ConnectionStrings["connectiondb"].ConnectionString);
            cmd.Connection = conn;
            conn.Open();
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                while (sdr.Read())
                {
                    autocompleteItems.Add(string.Format("{0}-{1}", sdr[1], sdr[0]));
                }
            }
            conn.Close();
        }
        return autocompleteItems.ToArray();
    }
    #endregion
    #region govt undertaking
    protected void gvgovtundertakingedit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "newsave")
        {
            btnsubmit.Text = "Submit";
            txtname.Text = "";
            txtregno.Text = "";
            txtdatevalid.Text = "";
            hffile.Value = "";
            ViewState["editGovtPsu"] = null;
            LoadCheckStatus();          
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divgovt", "showPopup();", true);
        }
        else if (e.CommandName == "newedit")
        {
            btnsubmit.Text = "Edit & Update";
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvgovtundertakingedit.Rows[rowIndex];
            HiddenField hfn = (HiddenField)gvgovtundertakingedit.Rows[rowIndex].FindControl("hfeditgovt");
            txtname.Text = row.Cells[1].Text;
            txtname.Text = row.Cells[2].Text;
            txtregno.Text = row.Cells[3].Text;
            txtdatevalid.Text = row.Cells[4].Text;
            hffile.Value = row.Cells[5].Text;
            ViewState["editGovtPsu"] = hfn.Value;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divgovt", "showPopup();", true);
        }
        else if (e.CommandName == "newdel")
        {
            Int32 Delid = Lo.DeleteEditGrid(Convert.ToInt32(e.CommandArgument.ToString()));
            if (Delid != 0)
            {
                LoadCheckStatus();
                lblmsg.Text = "Record deleted successfully";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#divgovt').modal('hide')", true);
            }
            else
            {
                lblmsg.Text = "Record not deleted successfully";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#divgovt').modal('hide')", true);
            }
        }
    }
    protected void lbsub_Click(object sender, EventArgs e)
    {
        try
        {
            if (lbsub.Text == "Edit & Update")
            {
                if (txtname.Text != "")
                {
                    Int32 ESaveID = Lo.UpdateGovt(Convert.ToInt64(ViewState["editGovtPsu"]), Enc.DecryptData(Session["VendorRefNo"].ToString()), txtname.Text, txtregno.Text, txtdatevalid.Text, hffile.Value);
                    if (ESaveID != 0)
                    {
                        txtname.Text = "";
                        txtregno.Text = "";
                        txtdatevalid.Text = "";
                        hffile.Value = "";
                        ViewState["editGovtPsu"] = null;
                        LoadCheckStatus();
                        lblmsg.Text = "Record update successfully";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#divgovt').modal('hide')", true);
                    }
                    else
                    {
                        lblmsg.Text = "Record not update successfully";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#divgovt').modal('hide')", true);
                    }
                }
            }
            else if (lbsub.Text == "Submit")
            {
                Int32 ESaveID = Lo.InsertGovt(Enc.DecryptData(Session["VendorRefNo"].ToString()), txtname.Text, txtregno.Text, txtdatevalid.Text, hffile.Value);
                if (ESaveID != 0)
                {
                    txtname.Text = "";
                    txtregno.Text = "";
                    txtdatevalid.Text = "";
                    hffile.Value = "";
                    ViewState["editGovtPsu"] = null;
                    LoadCheckStatus();
                    lblmsg.Text = "Record save successfully";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#divgovt').modal('hide')", true);
                }
                else
                {
                    lblmsg.Text = "Record not save successfully";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#divgovt').modal('hide')", true);
                }
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup();", true);
        }
    }
    #region Grid of Please Govt Under PSU
    private void SetInitialRowGovt()
    {
        //Create false table
        DataTable dtGovtPSU = new DataTable();
        DataRow drdtGovtPSU = null;
        dtGovtPSU.Columns.Add(new DataColumn("SrNoGovt", typeof(string)));
        dtGovtPSU.Columns.Add(new DataColumn("GName", typeof(string)));
        dtGovtPSU.Columns.Add(new DataColumn("GRegNo", typeof(string)));
        dtGovtPSU.Columns.Add(new DataColumn("GcertifiValid", typeof(string)));
        dtGovtPSU.Columns.Add(new DataColumn("UCertificate", typeof(string)));
        drdtGovtPSU = dtGovtPSU.NewRow();
        drdtGovtPSU["SrNoGovt"] = 1;
        drdtGovtPSU["GName"] = string.Empty;
        drdtGovtPSU["GRegNo"] = string.Empty;
        drdtGovtPSU["GcertifiValid"] = string.Empty;
        drdtGovtPSU["UCertificate"] = string.Empty;
        dtGovtPSU.Rows.Add(drdtGovtPSU);
        //Store the DataTable in ViewState or bind or show false grid
        ViewState["GovtPsu"] = dtGovtPSU;
        gvgovtundertaking.DataSource = dtGovtPSU;
        gvgovtundertaking.DataBind();
    }
    private void AddNewRowToGridGPsu()
    {
        int GrowIndex = 0;
        if (ViewState["GovtPsu"] != null)
        {
            DataTable dtCurrentTableNameGPsu = (DataTable)ViewState["GovtPsu"];
            DataRow drCurrentRowNameGPsu = null;
            if (dtCurrentTableNameGPsu.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTableNameGPsu.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox TextBox1Gp = (TextBox)gvgovtundertaking.Rows[GrowIndex].Cells[1].FindControl("txtnameundertaking");
                    TextBox TextBox2Gp = (TextBox)gvgovtundertaking.Rows[GrowIndex].Cells[2].FindControl("txtregisnogovtpsu");
                    TextBox TextBox3Gp = (TextBox)gvgovtundertaking.Rows[GrowIndex].Cells[3].FindControl("txtcertificatevalidupto");
                    FileUpload FuGp = (FileUpload)gvgovtundertaking.Rows[GrowIndex].Cells[4].FindControl("furegiscerti");
                    HiddenField hfFuGp = (HiddenField)gvgovtundertaking.Rows[GrowIndex].Cells[4].FindControl("hffuregiscerti");
                    hfFuGp.Value = FuGp.FileName.ToString();
                    drCurrentRowNameGPsu = dtCurrentTableNameGPsu.NewRow();
                    drCurrentRowNameGPsu["SrNoGovt"] = i + 1;
                    dtCurrentTableNameGPsu.Rows[i - 1]["GName"] = TextBox1Gp.Text;
                    dtCurrentTableNameGPsu.Rows[i - 1]["GRegNo"] = TextBox2Gp.Text;
                    dtCurrentTableNameGPsu.Rows[i - 1]["GcertifiValid"] = TextBox3Gp.Text;
                    dtCurrentTableNameGPsu.Rows[i - 1]["UCertificate"] = hfFuGp.Value;
                    GrowIndex++;
                }
                dtCurrentTableNameGPsu.Rows.Add(drCurrentRowNameGPsu);
                ViewState["GovtPsu"] = dtCurrentTableNameGPsu;
                gvgovtundertaking.DataSource = dtCurrentTableNameGPsu;
                gvgovtundertaking.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousDataGovtPsu();
    }
    private void SetPreviousDataGovtPsu()
    {
        int rowIndexGpsu = 0;
        if (ViewState["GovtPsu"] != null)
        {
            DataTable dtGovrPs = (DataTable)ViewState["GovtPsu"];
            if (dtGovrPs.Rows.Count > 0)
            {
                for (int i = 0; i < dtGovrPs.Rows.Count; i++)
                {
                    TextBox TextBox_1 = (TextBox)gvgovtundertaking.Rows[rowIndexGpsu].Cells[1].FindControl("txtnameundertaking");
                    TextBox TextBox_2 = (TextBox)gvgovtundertaking.Rows[rowIndexGpsu].Cells[2].FindControl("txtregisnogovtpsu");
                    TextBox TextBox_3 = (TextBox)gvgovtundertaking.Rows[rowIndexGpsu].Cells[3].FindControl("txtcertificatevalidupto");
                    HiddenField hfuGPsu = (HiddenField)gvgovtundertaking.Rows[rowIndexGpsu].Cells[4].FindControl("hffuregiscerti");
                    TextBox_1.Text = dtGovrPs.Rows[i]["GName"].ToString();
                    TextBox_2.Text = dtGovrPs.Rows[i]["GRegNo"].ToString();
                    TextBox_3.Text = dtGovrPs.Rows[i]["GcertifiValid"].ToString();
                    hfuGPsu.Value = dtGovrPs.Rows[i]["UCertificate"].ToString();
                    rowIndexGpsu++;
                }
            }
        }
    }
    protected void btnAddmoreGovtpsu_Click(object sender, EventArgs e)
    {
        AddNewRowToGridGPsu();
    }
    protected void gvgovtundertaking_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dtgridGovtPsurowcreated = (DataTable)ViewState["GovtPsu"];
            LinkButton lbPsu = (LinkButton)e.Row.FindControl("lbremoveGOvtPSU");
            if (lbPsu != null)
            {
                if (dtgridGovtPsurowcreated.Rows.Count > 1)
                {
                    if (e.Row.RowIndex == dtgridGovtPsurowcreated.Rows.Count - 1)
                    {
                        lbPsu.Visible = false;
                    }
                }
                else
                {
                    lbPsu.Visible = false;
                }
            }
        }
    }
    protected void lbremoveGOvtPSU_Click(object sender, EventArgs e)
    {
        LinkButton lbGP = (LinkButton)sender;
        GridViewRow gvRowGP = (GridViewRow)lbGP.NamingContainer;
        int rowID = gvRowGP.RowIndex;
        if (ViewState["GovtPsu"] != null)
        {
            DataTable dtremovegridGps = (DataTable)ViewState["GovtPsu"];
            if (dtremovegridGps.Rows.Count > 1)
            {
                if (gvRowGP.RowIndex < dtremovegridGps.Rows.Count - 1)
                {
                    dtremovegridGps.Rows.Remove(dtremovegridGps.Rows[rowID]);
                    ResetRowIDGovtPSU(dtremovegridGps);
                }
            }
            ViewState["GovtPsu"] = dtremovegridGps;
            gvgovtundertaking.DataSource = dtremovegridGps;
            gvgovtundertaking.DataBind();
        }
        SetPreviousDataGovtPsu();
    }
    private void ResetRowIDGovtPSU(DataTable dtremovecountGP)
    {
        int rowNumberGP = 1;
        if (dtremovecountGP.Rows.Count > 0)
        {
            foreach (DataRow row in dtremovecountGP.Rows)
            {
                row[0] = rowNumberGP;
                rowNumberGP++;
            }
        }
    }
    protected void SaveCodeForRegisNo()
    {
        int rowIndex = 0;
        DataTable dtRGEM = new DataTable();
        dtRGEM.Columns.Add(new DataColumn("SrNoGovt", typeof(string)));
        dtRGEM.Columns.Add(new DataColumn("GName", typeof(string)));
        dtRGEM.Columns.Add(new DataColumn("GRegNo", typeof(string)));
        dtRGEM.Columns.Add(new DataColumn("GcertifiValid", typeof(string)));
        dtRGEM.Columns.Add(new DataColumn("UCertificate", typeof(string)));
        DataRow drRGEM = null;
        for (int i = 0; gvgovtundertaking.Rows.Count > i; i++)
        {
            TextBox TextBox1Gp = (TextBox)gvgovtundertaking.Rows[i].Cells[1].FindControl("txtnameundertaking");
            TextBox TextBox2Gp = (TextBox)gvgovtundertaking.Rows[i].Cells[2].FindControl("txtregisnogovtpsu");
            TextBox TextBox3Gp = (TextBox)gvgovtundertaking.Rows[i].Cells[3].FindControl("txtcertificatevalidupto");
            FileUpload FuGp = (FileUpload)gvgovtundertaking.Rows[i].Cells[4].FindControl("furegiscerti");
            if (TextBox1Gp.Text != "" && TextBox2Gp.Text != "")
            {
                drRGEM = dtRGEM.NewRow();
                drRGEM["GName"] = TextBox1Gp.Text;
                drRGEM["GRegNo"] = TextBox2Gp.Text;
                drRGEM["GcertifiValid"] = TextBox3Gp.Text;
                drRGEM["UCertificate"] = FuGp.PostedFile.FileName;
                dtRGEM.Rows.Add(drRGEM);
            }
        }
        ViewState["GovtPsu"] = dtRGEM;
    }
    #endregion
    #endregion
}