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

public partial class Vendor_V_CompInfo2 : System.Web.UI.Page
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
                        dv.RowFilter = "Type='OEM'";
                        if (dv.Count > 0)
                        {
                            gveditoemnameadd.DataSource = dv;
                            gveditoemnameadd.DataBind();
                            gvOEMNameadd.Visible = false;
                            gveditoemnameadd.Visible = true;
                        }
                        else
                        {
                            SetInitialRowOEMNameAddress();
                            gvOEMNameadd.Visible = true;
                            gveditoemnameadd.Visible = false;
                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                    SetInitialRowOEMNameAddress();
                }
            }
        }
        else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert",
                   "alert('Session Expired,Please login again');window.location='VendorLogin'", true);
    }
    #endregion
    #region Name and Address of Product OEM
    private void SetInitialRowOEMNameAddress()
    {
        //Create false table
        DataTable dtOMF = new DataTable();
        DataRow drOMF = null;
        dtOMF.Columns.Add(new DataColumn("SNo", typeof(string)));
        dtOMF.Columns.Add(new DataColumn("FactoryName1", typeof(string)));
        dtOMF.Columns.Add(new DataColumn("OEM1", typeof(string)));
        dtOMF.Columns.Add(new DataColumn("CAddress1", typeof(string)));
        dtOMF.Columns.Add(new DataColumn("COfficialName1", typeof(string)));
        dtOMF.Columns.Add(new DataColumn("TeleNo1", typeof(string)));
        dtOMF.Columns.Add(new DataColumn("FaxNo1", typeof(string)));
        dtOMF.Columns.Add(new DataColumn("EmailId1", typeof(string)));
        dtOMF.Columns.Add(new DataColumn("AUTHRIZATION1", typeof(string)));
        drOMF = dtOMF.NewRow();
        drOMF["SNo"] = 1;
        drOMF["FactoryName1"] = string.Empty;
        drOMF["OEM1"] = string.Empty;
        drOMF["CAddress1"] = string.Empty;
        drOMF["COfficialName1"] = string.Empty;
        drOMF["TeleNo1"] = string.Empty;
        drOMF["FaxNo1"] = string.Empty;
        drOMF["EmailId1"] = string.Empty;
        drOMF["AUTHRIZATION1"] = string.Empty;
        dtOMF.Rows.Add(drOMF);
        //Store the DataTable in ViewState or bind or show false grid
        ViewState["OMF"] = dtOMF;
        gvOEMNameadd.DataSource = dtOMF;
        gvOEMNameadd.DataBind();
    }
    private void AddNewRowToGridOEMNameAddress()
    {
        int MFrowIndex1 = 0;
        if (ViewState["OMF"] != null)
        {
            DataTable dtCurrentTableMF1 = (DataTable)ViewState["OMF"];
            DataRow drCurrentRowMF1 = null;
            if (dtCurrentTableMF1.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTableMF1.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox TextBox1oMF = (TextBox)gvOEMNameadd.Rows[MFrowIndex1].Cells[1].FindControl("txtmanofficename1");
                    DropDownList ddl2oem = (DropDownList)gvOEMNameadd.Rows[MFrowIndex1].Cells[2].FindControl("ddlOEM1");
                    TextBox TextBox2oMF = (TextBox)gvOEMNameadd.Rows[MFrowIndex1].Cells[3].FindControl("txtCAddrssMF1");
                    TextBox TextBox3oMF = (TextBox)gvOEMNameadd.Rows[MFrowIndex1].Cells[4].FindControl("txtofficialNameMF1");
                    TextBox TextBox4oMF = (TextBox)gvOEMNameadd.Rows[MFrowIndex1].Cells[5].FindControl("txttelephonenoMF1");
                    TextBox TextBox5oMF = (TextBox)gvOEMNameadd.Rows[MFrowIndex1].Cells[6].FindControl("txtfaxnoMF1");
                    TextBox TextBox6oMF = (TextBox)gvOEMNameadd.Rows[MFrowIndex1].Cells[7].FindControl("txtemailidMF1");
                    FileUpload fuoem1 = (FileUpload)gvOEMNameadd.Rows[MFrowIndex1].Cells[8].FindControl("fuAUTHRIZATION1");
                    HiddenField hfuoem1 = (HiddenField)gvOEMNameadd.Rows[MFrowIndex1].Cells[8].FindControl("hfauth1");
                    hfuoem1.Value = fuoem1.FileName;
                    drCurrentRowMF1 = dtCurrentTableMF1.NewRow();
                    drCurrentRowMF1["SNo"] = i + 1;
                    dtCurrentTableMF1.Rows[i - 1]["FactoryName1"] = TextBox1oMF.Text;
                    dtCurrentTableMF1.Rows[i - 1]["OEM1"] = ddl2oem.Text;
                    dtCurrentTableMF1.Rows[i - 1]["CAddress1"] = TextBox2oMF.Text;
                    dtCurrentTableMF1.Rows[i - 1]["COfficialName1"] = TextBox3oMF.Text;
                    dtCurrentTableMF1.Rows[i - 1]["TeleNo1"] = TextBox4oMF.Text;
                    dtCurrentTableMF1.Rows[i - 1]["FaxNo1"] = TextBox5oMF.Text;
                    dtCurrentTableMF1.Rows[i - 1]["EmailId1"] = TextBox6oMF.Text;
                    dtCurrentTableMF1.Rows[i - 1]["AUTHRIZATION1"] = hfuoem1.Value;
                    MFrowIndex1++;
                }
                dtCurrentTableMF1.Rows.Add(drCurrentRowMF1);
                ViewState["OMF"] = dtCurrentTableMF1;
                gvOEMNameadd.DataSource = dtCurrentTableMF1;
                gvOEMNameadd.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousDataOEM();
    }
    private void SetPreviousDataOEM()
    {
        int rowIndexMF1 = 0;
        if (ViewState["OMF"] != null)
        {
            DataTable dtMF1 = (DataTable)ViewState["OMF"];
            if (dtMF1.Rows.Count > 0)
            {
                for (int i = 0; i < dtMF1.Rows.Count; i++)
                {
                    TextBox TextBox_1 = (TextBox)gvOEMNameadd.Rows[rowIndexMF1].Cells[1].FindControl("txtmanofficename1");
                    DropDownList TextBox_2 = (DropDownList)gvOEMNameadd.Rows[rowIndexMF1].Cells[2].FindControl("ddlOEM1");
                    TextBox TextBox_3 = (TextBox)gvOEMNameadd.Rows[rowIndexMF1].Cells[3].FindControl("txtCAddrssMF1");
                    TextBox TextBox_4 = (TextBox)gvOEMNameadd.Rows[rowIndexMF1].Cells[4].FindControl("txtofficialNameMF1");
                    TextBox TextBox_5 = (TextBox)gvOEMNameadd.Rows[rowIndexMF1].Cells[5].FindControl("txttelephonenoMF1");
                    TextBox TextBox_6 = (TextBox)gvOEMNameadd.Rows[rowIndexMF1].Cells[6].FindControl("txtfaxnoMF1");
                    TextBox TextBox_7 = (TextBox)gvOEMNameadd.Rows[rowIndexMF1].Cells[7].FindControl("txtemailidMF1");
                    FileUpload fu1 = (FileUpload)gvOEMNameadd.Rows[rowIndexMF1].Cells[8].FindControl("fuAUTHRIZATION1");
                    HiddenField hfauth1 = (HiddenField)gvOEMNameadd.Rows[rowIndexMF1].Cells[8].FindControl("hfauth1");
                    hfauth1.Value = fu1.FileName;
                    TextBox_1.Text = dtMF1.Rows[i]["FactoryName1"].ToString();
                    TextBox_2.Text = dtMF1.Rows[i]["OEM1"].ToString();
                    TextBox_3.Text = dtMF1.Rows[i]["CAddress1"].ToString();
                    TextBox_4.Text = dtMF1.Rows[i]["COfficialName1"].ToString();
                    TextBox_5.Text = dtMF1.Rows[i]["TeleNo1"].ToString();
                    TextBox_6.Text = dtMF1.Rows[i]["FaxNo1"].ToString();
                    TextBox_7.Text = dtMF1.Rows[i]["EmailId1"].ToString();
                    hfauth1.Value = dtMF1.Rows[i]["AUTHRIZATION1"].ToString();
                    rowIndexMF1++;
                }
            }
        }
    }
    protected void btnoem_Click(object sender, EventArgs e)
    {
        AddNewRowToGridOEMNameAddress();
    }
    protected void gvOEMNameadd_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dtgridMFacili1 = (DataTable)ViewState["OMF"];
            LinkButton lbRMF1 = (LinkButton)e.Row.FindControl("lbremoveoem");
            if (lbRMF1 != null)
            {
                if (dtgridMFacili1.Rows.Count > 1)
                {
                    if (e.Row.RowIndex == dtgridMFacili1.Rows.Count - 1)
                    {
                        lbRMF1.Visible = false;
                    }
                }
                else
                {
                    lbRMF1.Visible = false;
                }
            }
        }
    }
    protected void lbremoveoem_Click(object sender, EventArgs e)
    {
        LinkButton lbMF1 = (LinkButton)sender;
        GridViewRow gvRowMF1 = (GridViewRow)lbMF1.NamingContainer;
        int rowID = gvRowMF1.RowIndex;
        if (ViewState["OMF"] != null)
        {
            DataTable dtremovegridMF1 = (DataTable)ViewState["OMF"];
            if (dtremovegridMF1.Rows.Count > 1)
            {
                if (gvRowMF1.RowIndex < dtremovegridMF1.Rows.Count - 1)
                {
                    dtremovegridMF1.Rows.Remove(dtremovegridMF1.Rows[rowID]);
                    ResetRowIDOEMFac(dtremovegridMF1);
                }
            }
            ViewState["OMF"] = dtremovegridMF1;
            gvOEMNameadd.DataSource = dtremovegridMF1;
            gvOEMNameadd.DataBind();
        }
        SetPreviousDataOEM();
    }
    private void ResetRowIDOEMFac(DataTable dtMfaci1)
    {
        int rowNumberMfaci1 = 1;
        if (dtMfaci1.Rows.Count > 0)
        {
            foreach (DataRow row in dtMfaci1.Rows)
            {
                row[0] = rowNumberMfaci1;
                rowNumberMfaci1++;
            }
        }
    }
    protected void SaveCodeForOEMProduct()
    {
        int rowIndex = 0;
        DataTable dtOEMS = new DataTable();
        dtOEMS.Columns.Add(new DataColumn("SNo", typeof(string)));
        dtOEMS.Columns.Add(new DataColumn("FactoryName1", typeof(string)));
        dtOEMS.Columns.Add(new DataColumn("OEM1", typeof(string)));
        dtOEMS.Columns.Add(new DataColumn("CAddress1", typeof(string)));
        dtOEMS.Columns.Add(new DataColumn("COfficialName1", typeof(string)));
        dtOEMS.Columns.Add(new DataColumn("TeleNo1", typeof(string)));
        dtOEMS.Columns.Add(new DataColumn("FaxNo1", typeof(string)));
        dtOEMS.Columns.Add(new DataColumn("EmailId1", typeof(string)));
        dtOEMS.Columns.Add(new DataColumn("AUTHRIZATION1", typeof(string)));
        DataRow drOEMS = null;
        for (int i = 0; gvOEMNameadd.Rows.Count > i; i++)
        {
            TextBox TextBox_1 = (TextBox)gvOEMNameadd.Rows[i].Cells[1].FindControl("txtmanofficename1");
            DropDownList TextBox_2 = (DropDownList)gvOEMNameadd.Rows[i].Cells[2].FindControl("ddlOEM1");
            TextBox TextBox_3 = (TextBox)gvOEMNameadd.Rows[i].Cells[3].FindControl("txtCAddrssMF1");
            TextBox TextBox_4 = (TextBox)gvOEMNameadd.Rows[i].Cells[4].FindControl("txtofficialNameMF1");
            TextBox TextBox_5 = (TextBox)gvOEMNameadd.Rows[i].Cells[5].FindControl("txttelephonenoMF1");
            TextBox TextBox_6 = (TextBox)gvOEMNameadd.Rows[i].Cells[6].FindControl("txtfaxnoMF1");
            TextBox TextBox_7 = (TextBox)gvOEMNameadd.Rows[i].Cells[7].FindControl("txtemailidMF1");
            FileUpload fu1 = (FileUpload)gvOEMNameadd.Rows[i].Cells[8].FindControl("fuAUTHRIZATION1");          
            if (TextBox_1.Text != "" && TextBox_3.Text != "")
            {
                drOEMS = dtOEMS.NewRow();
                drOEMS["FactoryName1"] = TextBox_1.Text;
                drOEMS["OEM1"] = TextBox_2.Text;
                drOEMS["CAddress1"] = TextBox_3.Text;
                drOEMS["COfficialName1"] = TextBox_4.Text;
                drOEMS["TeleNo1"] = TextBox_5.Text;
                drOEMS["FaxNo1"] = TextBox_6.Text;
                drOEMS["EmailId1"] = TextBox_7.Text;
                drOEMS["AUTHRIZATION1"] = fu1.PostedFile.FileName;
                dtOEMS.Rows.Add(drOEMS);
            }
        }
        ViewState["OMF"] = dtOEMS;
    }
    #endregion
    protected void SaveRegistration()
    {  
        DataTable DtOEM = new DataTable();
        if (btnsubmit.Text == "Update")
        {
            if (gvOEMNameadd.Visible == true && gveditoemnameadd.Visible == false)
            {
                SaveCodeForOEMProduct();
                DtOEM = (DataTable)ViewState["OMF"];
            }
            else
            {
            }
        }
        else
        {
            SaveCodeForOEMProduct();
            DtOEM = (DataTable)ViewState["OMF"];
        }
        string str = Lo.SaveVendorCompanyInfo2(DtOEM, Enc.DecryptData(Session["VendorRefNo"].ToString()), out _sysMsg, out _msg);
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
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        SaveRegistration();
    }    
}