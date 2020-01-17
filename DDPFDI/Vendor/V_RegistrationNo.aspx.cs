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

public partial class Vendor_V_RegistrationNo : System.Web.UI.Page
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
                DataTable DtCheckSavedetails = Lo.RetriveVendor(0, Enc.DecryptData(Session["VendorRefNo"].ToString()), "", "CheckRegis");
                if (DtCheckSavedetails.Rows.Count > 0)
                {
                    btnsubmit.Text = "Update";
                    ViewState["Mid"] = Convert.ToInt64(DtCheckSavedetails.Rows[0]["VendorDetailID"].ToString());
                    DataTable dtcheckmultigriddata = Lo.RetriveVendor(0, Enc.DecryptData(Session["VendorRefNo"].ToString()), "", "RetriveMultigrid");
                    if (dtcheckmultigriddata.Rows.Count > 0)
                    {
                        DataView dv = new DataView(dtcheckmultigriddata);
                        dv.RowFilter = "Type='RGovt'";
                        if (dv.Count > 0)
                        {
                            gvgovtundertakingedit.DataSource = dv;
                            gvgovtundertakingedit.DataBind();
                            gvgovtundertaking.Visible = false;
                            gvgovtundertakingedit.Visible = true;
                        }
                        else
                        {
                            SetInitialRowGovt();
                            gvgovtundertaking.Visible = true;
                            gvgovtundertakingedit.Visible = false;
                        }
                    }
                    else
                    {

                    }
                }
                else
                { }
            }
        }
        else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert",
                   "alert('Session Expired,Please login again');window.location='VendorLogin'", true);

    }
    #endregion
    protected void ddldetailofpantan_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldetailofpantan.SelectedItem.Text == "Select")
        { divpantan.Visible = false; }
        else if (ddldetailofpantan.SelectedItem.Text == "NO")
        { lblpantan.Text = "TAN"; divpantan.Visible = false; }
        else if (ddldetailofpantan.SelectedItem.Text == "YES")
        { lblpantan.Text = "TAN"; divpantan.Visible = true; }
    }
    protected void ddlDepartmentUndertakingPSU_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartmentUndertakingPSU.SelectedItem.Text == "YES")
        { divgovtundertaking.Visible = true; }
        else
        { divgovtundertaking.Visible = false; }
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
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        SaveRegistration();
    }
    protected void SaveRegistration()
    {
        HySaveVendorRegisdetail["VendorRefNo"] = Enc.DecryptData(Session["VendorRefNo"].ToString());
        HySaveVendorRegisdetail["Is_PANTAN"] = Co.RSQandSQLInjection(ddldetailofpantan.SelectedItem.Value, "soft");
        HySaveVendorRegisdetail["PanTan_No"] = Co.RSQandSQLInjection(txtpantan.Text, "soft");
        HySaveVendorRegisdetail["GSTNo"] = Co.RSQandSQLInjection(txtgstin.Text, "soft");
        HySaveVendorRegisdetail["UAM"] = Co.RSQandSQLInjection(txtUAM.Text, "soft");
        HySaveVendorRegisdetail["CIN"] = Co.RSQandSQLInjection(txtCIN.Text, "soft");
        HySaveVendorRegisdetail["IsRegisterdwithgovt"] = Co.RSQandSQLInjection(ddlDepartmentUndertakingPSU.SelectedItem.Value, "soft");
        DataTable DtGOVTPSU = new DataTable();
        if (btnsubmit.Text == "Update")
        {
            if (gvgovtundertaking.Visible == true && gvgovtundertakingedit.Visible == false)
            {
                SaveCodeForRegisNo();
                DtGOVTPSU = (DataTable)ViewState["GovtPsu"];
            }
            else
            {
            }
        }
        else
        {
            SaveCodeForRegisNo();
            DtGOVTPSU = (DataTable)ViewState["GovtPsu"];
        }
        string str = Lo.SaveVendorRegisNoDetails(HySaveVendorRegisdetail,DtGOVTPSU, out _sysMsg, out _msg);
        if (str != "")
        {
            if (btnsubmit.Text == "Update")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Successfully update company information')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Successfully save company information')", true);
            }
        }
        else
        { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not saved.')", true); }
    }
}