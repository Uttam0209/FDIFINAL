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

public partial class Vendor_V_CompInfo : System.Web.UI.Page
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
                LoadCode();
            }
        }
        else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert",
                   "alert('Session Expired,Please login again');window.location='VendorLogin'", true);
    }
    protected void LoadCode()
    {
        #region PageLoad code For Check Regis User or Show Edit Option
        DataTable DtCheckSavedetails = Lo.RetriveVendor(0, Enc.DecryptData(Session["VendorRefNo"].ToString()), "", "CheckRegis");
        if (DtCheckSavedetails.Rows.Count > 0)
        {
            btnsubmit.Text = "Update";
            ViewState["Mid"] = Convert.ToInt64(DtCheckSavedetails.Rows[0]["VendorDetailID"].ToString());
             DataTable DtCheckSavedetails1 = Lo.RetriveVendor(0, Enc.DecryptData(Session["VendorRefNo"].ToString()), "", "CheckRegis1");
             if (DtCheckSavedetails1.Rows.Count > 0)
             {
                 if (DtCheckSavedetails1.Rows[0]["Is_Lab_accredited_by_NABL"].ToString().Trim() == "1")
                 {
                     ddlnabl.Text = DtCheckSavedetails1.Rows[0]["Is_Lab_accredited_by_NABL"].ToString().Trim();
                     divcertificatevalid.Visible = true;
                 }
                 else
                 {
                     divcertificatevalid.Visible = false;
                 }
                 txtdate.Text = DtCheckSavedetails1.Rows[0]["CertifictionValid"].ToString();
                 txtmss.Text = DtCheckSavedetails1.Rows[0]["Details_of_R_D_Facilities"].ToString();
                 if (DtCheckSavedetails1.Rows[0]["IsSalesOrMarketOffice"].ToString().Trim() == "1")
                 {
                     ddloffice.Text = DtCheckSavedetails1.Rows[0]["IsSalesOrMarketOffice"].ToString().Trim();
                     detailofoffcie.Visible = true;
                 }
                 else
                 {
                     detailofoffcie.Visible = false;
                 }
                 txtlname.Text = DtCheckSavedetails1.Rows[0]["NodelName"].ToString();
                 txtstreetaddress.Text = DtCheckSavedetails1.Rows[0]["MarketingOfficeAddress"].ToString();
                 txtstreetaddressline2.Text = DtCheckSavedetails1.Rows[0]["Line2"].ToString();
                 txtcity.Text = DtCheckSavedetails1.Rows[0]["OfficerCity"].ToString();
                 txtstate.Text = DtCheckSavedetails1.Rows[0]["OfficeState"].ToString();
                 txtpincode.Text = DtCheckSavedetails1.Rows[0]["OfficePincode"].ToString();
                 txtcontactno.Text = DtCheckSavedetails1.Rows[0]["PhoneNo"].ToString();
                 txtfaxno.Text = DtCheckSavedetails1.Rows[0]["OfficeFaxNo"].ToString();
                 txtemail.Text = DtCheckSavedetails1.Rows[0]["OfficeEmail"].ToString();
                 if (DtCheckSavedetails1.Rows[0]["IsAuthorisedDealer"].ToString().Trim() == "1")
                 {
                     ddldistributoraddress.Text = DtCheckSavedetails1.Rows[0]["IsAuthorisedDealer"].ToString().Trim();
                     gv3.Visible = true;
                 }
                 else
                 {
                     gv3.Visible = false;
                 }
                 txtfuture.Text = DtCheckSavedetails1.Rows[0]["FuturePlan"].ToString();
             }             
            DataTable dtcheckmultigriddata = Lo.RetriveVendor(0, Enc.DecryptData(Session["VendorRefNo"].ToString()), "", "RetriveMultigrid");
            if (dtcheckmultigriddata.Rows.Count > 0)
            {
                DataView dv = new DataView(dtcheckmultigriddata);
                dv.RowFilter = "Type='CompInfo'";
                if (dv.Count > 0)
                {
                    gvmanufacilityedit.DataSource = dv;
                    gvmanufacilityedit.DataBind();
                    gvmanufacility.Visible = false;
                    gvmanufacilityedit.Visible = true;
                }
                else
                {
                    SetInitialRowListofManufacturingFacilities();
                    gvmanufacility.Visible = true;
                    gvmanufacilityedit.Visible = false;
                }
                dv.RowFilter = "Type='AreaDetail'";
                if (dv.Count > 0)
                {
                    gvareadetailedit.DataSource = dv;
                    gvareadetailedit.DataBind();
                    gvareadetail.Visible = false;
                    gvareadetailedit.Visible = true;
                }
                else
                {
                    SetInitialRowArea();
                    gvareadetail.Visible = true;
                    gvareadetailedit.Visible = false;
                }
                dv.RowFilter = "Type='AllPlantOrMachine'";
                if (dv.Count > 0)
                {
                    gvplantandmachinesedit.DataSource = dv;
                    gvplantandmachinesedit.DataBind();
                    gvplantandmachines.Visible = false;
                    gvplantandmachinesedit.Visible = true;
                }
                else
                {
                    SetInitialRowPM();
                    gvplantandmachines.Visible = true;
                    gvplantandmachinesedit.Visible = false;
                }
                dv.RowFilter = "Type='Employeedetail'";
                if (dv.Count > 0)
                {
                    gvempCompInfoedit.DataSource = dv;
                    gvempCompInfoedit.DataBind();
                    gvempCompInfo.Visible = false;
                    gvempCompInfoedit.Visible = true;
                }
                else
                {
                    SetInitialRowEMPCI();
                    gvempCompInfo.Visible = true;
                    gvempCompInfoedit.Visible = false;
                }
                dv.RowFilter = "Type='TestEquipment'";
                if (dv.Count > 0)
                {
                    gvtestfacilitiesedit.DataSource = dv;
                    gvtestfacilitiesedit.DataBind();
                    gvtestfacilities.Visible = false;
                    gvtestfacilitiesedit.Visible = true;
                }
                else
                {
                    SetInitialRowTestFacili();
                    gvtestfacilities.Visible = true;
                    gvtestfacilitiesedit.Visible = false;
                }
                dv.RowFilter = "Type='Distributer'";
                if (dv.Count > 0)
                {
                    gvauthdealaddressedit.DataSource = dv;
                    gvauthdealaddressedit.DataBind();
                    gvauthdealaddress.Visible = false;
                    gvauthdealaddressedit.Visible = true;
                }
                else
                {
                    SetInitialRowAuthodel();
                    gvauthdealaddress.Visible = true;
                    gvauthdealaddressedit.Visible = false;
                }
                dv.RowFilter = "Type='JointVentureFacility'";
                if (dv.Count > 0)
                {
                    gvjointventureedit.DataSource = dv;
                    gvjointventureedit.DataBind();
                    gvjointventure.Visible = false;
                    gvjointventureedit.Visible = true;
                }
                else
                {
                    SetInitialRowJVF();
                    gvjointventure.Visible = true;
                    gvjointventureedit.Visible = false;
                }
                dv.RowFilter = "Type='Outsourcing'";
                if (dv.Count > 0)
                {
                    gvoutsourcefacilityedit.DataSource = dv;
                    gvoutsourcefacilityedit.DataBind();
                    gvoutsourcefacility.Visible = false;
                    gvoutsourcefacilityedit.Visible = true;
                }
                else
                {
                    SetInitialRowOF();
                    gvoutsourcefacility.Visible = true;
                    gvoutsourcefacilityedit.Visible = false;
                }
            }
            DataTable dtcheckmultiimage = Lo.RetriveVendor(0, Enc.DecryptData(Session["VendorRefNo"].ToString()), "", "RetriveMultigridImage");
            if (dtcheckmultiimage.Rows.Count > 0)
            {
                DataView dv1 = new DataView(dtcheckmultiimage);
                dv1.RowFilter = "Type='FCertificate'";
                if (dv1.Count > 0)
                {
                    gvcertificateedit.DataSource = dv1;
                    gvcertificateedit.DataBind();
                    gvcertificate.Visible = false;
                    gvcertificateedit.Visible = true;
                }
                else
                {
                    SetInitialRowCertificate();
                    gvcertificate.Visible = true;
                    gvcertificateedit.Visible = false;
                }
                dv1.RowFilter = "Type='QCertificate'";
                if (dv1.Count > 0)
                {
                    gvchkqualitycertificateedit.DataSource = dv1;
                    gvchkqualitycertificateedit.DataBind();
                    gvchkqualitycertificate.Visible = false;
                    gvchkqualitycertificateedit.Visible = true;
                }
                else
                {
                    SetInitialRowqualitycertificate();
                    gvchkqualitycertificate.Visible = true;
                    gvchkqualitycertificateedit.Visible = false;
                }
            }
            else
            {
                SetInitialRowCertificate();
                SetInitialRowqualitycertificate();
            }
        }
        else
        {
            btnsubmit.Text = "Save";
            #region PageLoad Grid Functions
            SetInitialRowListofManufacturingFacilities();
            SetInitialRowArea();
            SetInitialRowPM();
            SetInitialRowEMPCI();
            SetInitialRowTestFacili();
            SetInitialRowAuthodel();
            SetInitialRowOF();
            SetInitialRowJVF();
            SetInitialRowCertificate();
            SetInitialRowqualitycertificate();
            #endregion
        }
        #endregion
    }
    #endregion
    #region Add Grid of Manufacturing Facilities
    private void SetInitialRowListofManufacturingFacilities()
    {
        try
        {
            DataTable dtMF = new DataTable();
            DataRow drMF = null;
            dtMF.Columns.Add(new DataColumn("SNo", typeof(string)));
            dtMF.Columns.Add(new DataColumn("FactoryName", typeof(string)));
            dtMF.Columns.Add(new DataColumn("FACGSTNO", typeof(string)));
            dtMF.Columns.Add(new DataColumn("CAddress", typeof(string)));
            dtMF.Columns.Add(new DataColumn("COfficialName", typeof(string)));
            dtMF.Columns.Add(new DataColumn("TeleNo", typeof(string)));
            dtMF.Columns.Add(new DataColumn("FaxNo", typeof(string)));
            dtMF.Columns.Add(new DataColumn("EmailId", typeof(string)));
            drMF = dtMF.NewRow();
            drMF["SNo"] = 1;
            drMF["FactoryName"] = string.Empty;
            drMF["FACGSTNO"] = string.Empty;
            drMF["CAddress"] = string.Empty;
            drMF["COfficialName"] = string.Empty;
            drMF["TeleNo"] = string.Empty;
            drMF["FaxNo"] = string.Empty;
            drMF["EmailId"] = string.Empty;
            dtMF.Rows.Add(drMF);
            //Store the DataTable in ViewState or bind or show false grid
            ViewState["MF"] = dtMF;
            gvmanufacility.DataSource = dtMF;
            gvmanufacility.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void AddNewRowToGridManufacFacilities()
    {
        try
        {
            int MFrowIndex = 0;
            if (ViewState["MF"] != null)
            {
                DataTable dtCurrentTableMF = (DataTable)ViewState["MF"];
                DataRow drCurrentRowMF = null;
                if (dtCurrentTableMF.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTableMF.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        TextBox TextBox1MF = (TextBox)gvmanufacility.Rows[MFrowIndex].Cells[1].FindControl("txtmanofficename");
                        TextBox TextBox7MF = (TextBox)gvmanufacility.Rows[MFrowIndex].Cells[2].FindControl("TXTFACGSTNO");
                        TextBox TextBox2MF = (TextBox)gvmanufacility.Rows[MFrowIndex].Cells[3].FindControl("txtCAddrssMF");
                        TextBox TextBox3MF = (TextBox)gvmanufacility.Rows[MFrowIndex].Cells[4].FindControl("txtofficialNameMF");
                        TextBox TextBox4MF = (TextBox)gvmanufacility.Rows[MFrowIndex].Cells[5].FindControl("txttelephonenoMF");
                        TextBox TextBox5MF = (TextBox)gvmanufacility.Rows[MFrowIndex].Cells[6].FindControl("txtfaxnoMF");
                        TextBox TextBox6MF = (TextBox)gvmanufacility.Rows[MFrowIndex].Cells[7].FindControl("txtemailidMF");
                        drCurrentRowMF = dtCurrentTableMF.NewRow();
                        drCurrentRowMF["SNo"] = i + 1;
                        dtCurrentTableMF.Rows[i - 1]["FactoryName"] = TextBox1MF.Text;
                        dtCurrentTableMF.Rows[i - 1]["FACGSTNO"] = TextBox7MF.Text;
                        dtCurrentTableMF.Rows[i - 1]["CAddress"] = TextBox2MF.Text;
                        dtCurrentTableMF.Rows[i - 1]["COfficialName"] = TextBox3MF.Text;
                        dtCurrentTableMF.Rows[i - 1]["TeleNo"] = TextBox4MF.Text;
                        dtCurrentTableMF.Rows[i - 1]["FaxNo"] = TextBox5MF.Text;
                        dtCurrentTableMF.Rows[i - 1]["EmailId"] = TextBox6MF.Text;
                        MFrowIndex++;
                    }
                    dtCurrentTableMF.Rows.Add(drCurrentRowMF);
                    ViewState["MF"] = dtCurrentTableMF;
                    gvmanufacility.DataSource = dtCurrentTableMF;
                    gvmanufacility.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousDataGovtManuFacilities();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void SetPreviousDataGovtManuFacilities()
    {
        try
        {
            int rowIndexMF = 0;
            if (ViewState["MF"] != null)
            {
                DataTable dtMF = (DataTable)ViewState["MF"];
                if (dtMF.Rows.Count > 0)
                {
                    for (int i = 0; i < dtMF.Rows.Count; i++)
                    {
                        TextBox TextBox_1 = (TextBox)gvmanufacility.Rows[rowIndexMF].Cells[1].FindControl("txtmanofficename");
                        TextBox TextBox_7 = (TextBox)gvmanufacility.Rows[rowIndexMF].Cells[2].FindControl("TXTFACGSTNO");
                        TextBox TextBox_2 = (TextBox)gvmanufacility.Rows[rowIndexMF].Cells[3].FindControl("txtCAddrssMF");
                        TextBox TextBox_3 = (TextBox)gvmanufacility.Rows[rowIndexMF].Cells[4].FindControl("txtofficialNameMF");
                        TextBox TextBox_4 = (TextBox)gvmanufacility.Rows[rowIndexMF].Cells[5].FindControl("txttelephonenoMF");
                        TextBox TextBox_5 = (TextBox)gvmanufacility.Rows[rowIndexMF].Cells[6].FindControl("txtfaxnoMF");
                        TextBox TextBox_6 = (TextBox)gvmanufacility.Rows[rowIndexMF].Cells[7].FindControl("txtemailidMF");
                        TextBox_1.Text = dtMF.Rows[i]["FactoryName"].ToString();
                        TextBox_7.Text = dtMF.Rows[i]["FACGSTNO"].ToString();
                        TextBox_2.Text = dtMF.Rows[i]["CAddress"].ToString();
                        TextBox_3.Text = dtMF.Rows[i]["COfficialName"].ToString();
                        TextBox_4.Text = dtMF.Rows[i]["TeleNo"].ToString();
                        TextBox_5.Text = dtMF.Rows[i]["FaxNo"].ToString();
                        TextBox_6.Text = dtMF.Rows[i]["EmailId"].ToString();
                        rowIndexMF++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void btnAddManufac_Click(object sender, EventArgs e)
    {
        AddNewRowToGridManufacFacilities();
    }
    protected void gvmanufacility_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dtgridMFacili = (DataTable)ViewState["MF"];
                LinkButton lbRMF = (LinkButton)e.Row.FindControl("lblremovemanufac");
                if (lbRMF != null)
                {
                    if (dtgridMFacili.Rows.Count > 1)
                    {
                        if (e.Row.RowIndex == dtgridMFacili.Rows.Count - 1)
                        {
                            lbRMF.Visible = false;
                        }
                    }
                    else
                    {
                        lbRMF.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void lblremovemanufac_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lbMF = (LinkButton)sender;
            GridViewRow gvRowMF = (GridViewRow)lbMF.NamingContainer;
            int rowID = gvRowMF.RowIndex;
            if (ViewState["MF"] != null)
            {
                DataTable dtremovegridMF = (DataTable)ViewState["MF"];
                if (dtremovegridMF.Rows.Count > 1)
                {
                    if (gvRowMF.RowIndex < dtremovegridMF.Rows.Count - 1)
                    {
                        dtremovegridMF.Rows.Remove(dtremovegridMF.Rows[rowID]);
                        ResetRowIDMFacilities(dtremovegridMF);
                    }
                }
                ViewState["MF"] = dtremovegridMF;
                gvmanufacility.DataSource = dtremovegridMF;
                gvmanufacility.DataBind();
            }
            SetPreviousDataGovtManuFacilities();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void ResetRowIDMFacilities(DataTable dtMfaci)
    {
        try
        {
            int rowNumberMfaci = 1;
            if (dtMfaci.Rows.Count > 0)
            {
                foreach (DataRow row in dtMfaci.Rows)
                {
                    row[0] = rowNumberMfaci;
                    rowNumberMfaci++;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    DataTable dtManufacturingFacilities = new DataTable();
    protected void SaveCodeForManufacturingFacilities()
    {
        try
        {
            int rowIndex = 0;
            DataTable dtManufacturingFacilities = new DataTable();
            dtManufacturingFacilities.Columns.Add(new DataColumn("SNo", typeof(string)));
            dtManufacturingFacilities.Columns.Add(new DataColumn("FactoryName", typeof(string)));
            dtManufacturingFacilities.Columns.Add(new DataColumn("FACGSTNO", typeof(string)));
            dtManufacturingFacilities.Columns.Add(new DataColumn("CAddress", typeof(string)));
            dtManufacturingFacilities.Columns.Add(new DataColumn("COfficialName", typeof(string)));
            dtManufacturingFacilities.Columns.Add(new DataColumn("TeleNo", typeof(string)));
            dtManufacturingFacilities.Columns.Add(new DataColumn("FaxNo", typeof(string)));
            dtManufacturingFacilities.Columns.Add(new DataColumn("EmailId", typeof(string)));
            DataRow drManufacturingFacilities = null;
            for (int i = 0; gvmanufacility.Rows.Count > i; i++)
            {
                TextBox TextBox1MF = (TextBox)gvmanufacility.Rows[i].Cells[1].FindControl("txtmanofficename");
                TextBox TextBox7MF = (TextBox)gvmanufacility.Rows[i].Cells[1].FindControl("TXTFACGSTNO");
                TextBox TextBox2MF = (TextBox)gvmanufacility.Rows[i].Cells[2].FindControl("txtCAddrssMF");
                TextBox TextBox3MF = (TextBox)gvmanufacility.Rows[i].Cells[3].FindControl("txtofficialNameMF");
                TextBox TextBox4MF = (TextBox)gvmanufacility.Rows[i].Cells[4].FindControl("txttelephonenoMF");
                TextBox TextBox5MF = (TextBox)gvmanufacility.Rows[i].Cells[5].FindControl("txtfaxnoMF");
                TextBox TextBox6MF = (TextBox)gvmanufacility.Rows[i].Cells[6].FindControl("txtemailidMF");
                if (TextBox7MF.Text != "" && TextBox1MF.Text != "")
                {
                    drManufacturingFacilities = dtManufacturingFacilities.NewRow();
                    drManufacturingFacilities["FactoryName"] = TextBox1MF.Text;
                    drManufacturingFacilities["FACGSTNO"] = TextBox7MF.Text;
                    drManufacturingFacilities["CAddress"] = TextBox2MF.Text;
                    drManufacturingFacilities["COfficialName"] = TextBox3MF.Text;
                    drManufacturingFacilities["TeleNo"] = TextBox4MF.Text;
                    drManufacturingFacilities["FaxNo"] = TextBox5MF.Text;
                    drManufacturingFacilities["EmailId"] = TextBox6MF.Text;
                    dtManufacturingFacilities.Rows.Add(drManufacturingFacilities);
                }
            }
            ViewState["MF"] = dtManufacturingFacilities;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    #endregion
    #region Add Grid of Area Details
    private void SetInitialRowArea()
    {
        try
        {
            DataTable dtArea = new DataTable();
            DataRow drArea = null;
            dtArea.Columns.Add(new DataColumn("SNo", typeof(string)));
            dtArea.Columns.Add(new DataColumn("AreaFactoryName", typeof(string)));
            dtArea.Columns.Add(new DataColumn("PArea", typeof(string)));
            dtArea.Columns.Add(new DataColumn("InsArea", typeof(string)));
            dtArea.Columns.Add(new DataColumn("CoverArea", typeof(string)));
            dtArea.Columns.Add(new DataColumn("TotalArea", typeof(string)));
            drArea = dtArea.NewRow();
            drArea["SNo"] = 1;
            drArea["AreaFactoryName"] = string.Empty;
            drArea["PArea"] = string.Empty;
            drArea["InsArea"] = string.Empty;
            drArea["CoverArea"] = string.Empty;
            drArea["TotalArea"] = string.Empty;
            dtArea.Rows.Add(drArea);
            //Store the DataTable in ViewState or bind or show false grid
            ViewState["Area"] = dtArea;
            gvareadetail.DataSource = dtArea;
            gvareadetail.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void AddNewRowToGridArea()
    {
        try
        {
            int ArowIndex = 0;
            if (ViewState["Area"] != null)
            {
                DataTable dtCurrentTableArea = (DataTable)ViewState["Area"];
                DataRow drCurrentRowArea = null;
                if (dtCurrentTableArea.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTableArea.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        TextBox TextBox1A = (TextBox)gvareadetail.Rows[ArowIndex].Cells[1].FindControl("txtAreaFactoryName");
                        TextBox TextBox2A = (TextBox)gvareadetail.Rows[ArowIndex].Cells[2].FindControl("txtprodarea");
                        TextBox TextBox3A = (TextBox)gvareadetail.Rows[ArowIndex].Cells[3].FindControl("txtinsarea");
                        TextBox TextBox4A = (TextBox)gvareadetail.Rows[ArowIndex].Cells[4].FindControl("txttotalcoverdarea");
                        TextBox TextBox5A = (TextBox)gvareadetail.Rows[ArowIndex].Cells[5].FindControl("txttotalarea");
                        drCurrentRowArea = dtCurrentTableArea.NewRow();
                        drCurrentRowArea["SNo"] = i + 1;
                        dtCurrentTableArea.Rows[i - 1]["AreaFactoryName"] = TextBox1A.Text;
                        dtCurrentTableArea.Rows[i - 1]["PArea"] = TextBox2A.Text;
                        dtCurrentTableArea.Rows[i - 1]["InsArea"] = TextBox3A.Text;
                        dtCurrentTableArea.Rows[i - 1]["CoverArea"] = TextBox4A.Text;
                        dtCurrentTableArea.Rows[i - 1]["TotalArea"] = TextBox5A.Text;
                        ArowIndex++;
                    }
                    dtCurrentTableArea.Rows.Add(drCurrentRowArea);
                    ViewState["Area"] = dtCurrentTableArea;
                    gvareadetail.DataSource = dtCurrentTableArea;
                    gvareadetail.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousDataArea();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void SetPreviousDataArea()
    {
        try
        {
            int rowIndexArea = 0;
            if (ViewState["Area"] != null)
            {
                DataTable dtArea = (DataTable)ViewState["Area"];
                if (dtArea.Rows.Count > 0)
                {
                    for (int i = 0; i < dtArea.Rows.Count; i++)
                    {
                        TextBox TextBox_1 = (TextBox)gvareadetail.Rows[rowIndexArea].Cells[1].FindControl("txtAreaFactoryName");
                        TextBox TextBox_2 = (TextBox)gvareadetail.Rows[rowIndexArea].Cells[2].FindControl("txtprodarea");
                        TextBox TextBox_3 = (TextBox)gvareadetail.Rows[rowIndexArea].Cells[3].FindControl("txtinsarea");
                        TextBox TextBox_4 = (TextBox)gvareadetail.Rows[rowIndexArea].Cells[4].FindControl("txttotalcoverdarea");
                        TextBox TextBox_5 = (TextBox)gvareadetail.Rows[rowIndexArea].Cells[5].FindControl("txttotalarea");
                        TextBox_1.Text = dtArea.Rows[i]["AreaFactoryName"].ToString();
                        TextBox_2.Text = dtArea.Rows[i]["PArea"].ToString();
                        TextBox_3.Text = dtArea.Rows[i]["InsArea"].ToString();
                        TextBox_4.Text = dtArea.Rows[i]["CoverArea"].ToString();
                        TextBox_5.Text = dtArea.Rows[i]["TotalArea"].ToString();
                        rowIndexArea++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void gvareadetail_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dtgridArea = (DataTable)ViewState["Area"];
                LinkButton lbArea = (LinkButton)e.Row.FindControl("lblRemoveArea");
                if (lbArea != null)
                {
                    if (dtgridArea.Rows.Count > 1)
                    {
                        if (e.Row.RowIndex == dtgridArea.Rows.Count - 1)
                        {
                            lbArea.Visible = false;
                        }
                    }
                    else
                    {
                        lbArea.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void btnAddArea_Click1(object sender, EventArgs e)
    {
        AddNewRowToGridArea();
    }
    protected void lblRemoveArea_Click1(object sender, EventArgs e)
    {
        try
        {
            LinkButton lbArea = (LinkButton)sender;
            GridViewRow gvRowArea = (GridViewRow)lbArea.NamingContainer;
            int rowID = gvRowArea.RowIndex;
            if (ViewState["Area"] != null)
            {
                DataTable dtremovegridArea = (DataTable)ViewState["Area"];
                if (dtremovegridArea.Rows.Count > 1)
                {
                    if (gvRowArea.RowIndex < dtremovegridArea.Rows.Count - 1)
                    {
                        dtremovegridArea.Rows.Remove(dtremovegridArea.Rows[rowID]);
                        ResetRowIDArea(dtremovegridArea);
                    }
                }
                ViewState["Area"] = dtremovegridArea;
                gvareadetail.DataSource = dtremovegridArea;
                gvareadetail.DataBind();
            }
            SetPreviousDataArea();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void ResetRowIDArea(DataTable dtmArea)
    {
        try
        {
            int rowNumberAreaDet = 1;
            if (dtmArea.Rows.Count > 0)
            {
                foreach (DataRow row in dtmArea.Rows)
                {
                    row[0] = rowNumberAreaDet;
                    rowNumberAreaDet++;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    DataTable dtareasave = new DataTable();
    protected void SaveCodeForPArea()
    {
        try
        {
            int rowIndex = 0;
            dtareasave.Columns.Add(new DataColumn("SNo", typeof(string)));
            dtareasave.Columns.Add(new DataColumn("AreaFactoryName", typeof(string)));
            dtareasave.Columns.Add(new DataColumn("PArea", typeof(string)));
            dtareasave.Columns.Add(new DataColumn("InsArea", typeof(string)));
            dtareasave.Columns.Add(new DataColumn("CoverArea", typeof(string)));
            dtareasave.Columns.Add(new DataColumn("TotalArea", typeof(string)));
            DataRow drAreaSave = null;
            for (int i = 0; gvareadetail.Rows.Count > i; i++)
            {
                TextBox TextBox1A = (TextBox)gvareadetail.Rows[i].Cells[1].FindControl("txtAreaFactoryName");
                TextBox TextBox2A = (TextBox)gvareadetail.Rows[i].Cells[2].FindControl("txtprodarea");
                TextBox TextBox3A = (TextBox)gvareadetail.Rows[i].Cells[3].FindControl("txtinsarea");
                TextBox TextBox4A = (TextBox)gvareadetail.Rows[i].Cells[4].FindControl("txttotalcoverdarea");
                TextBox TextBox5A = (TextBox)gvareadetail.Rows[i].Cells[5].FindControl("txttotalarea");
                if (TextBox1A.Text != "" && TextBox2A.Text != "")
                {
                    drAreaSave = dtareasave.NewRow();
                    drAreaSave["SNo"] = i + 1;
                    drAreaSave["AreaFactoryName"] = TextBox1A.Text;
                    drAreaSave["PArea"] = TextBox2A.Text;
                    drAreaSave["InsArea"] = TextBox3A.Text;
                    drAreaSave["CoverArea"] = TextBox4A.Text;
                    drAreaSave["TotalArea"] = TextBox5A.Text;
                    dtareasave.Rows.Add(drAreaSave);
                }
            }
            ViewState["Area"] = dtareasave;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    #endregion
    #region Add Grid of List of All Plant and Machines
    private void SetInitialRowPM()
    {
        try
        {
            DataTable dtPlantM = new DataTable();
            DataRow drPalntM = null;
            dtPlantM.Columns.Add(new DataColumn("SNo", typeof(string)));
            dtPlantM.Columns.Add(new DataColumn("MachineModelSpec", typeof(string)));
            dtPlantM.Columns.Add(new DataColumn("MakePlant", typeof(string)));
            dtPlantM.Columns.Add(new DataColumn("QuanPlant", typeof(string)));
            dtPlantM.Columns.Add(new DataColumn("DOPPlant", typeof(string)));
            dtPlantM.Columns.Add(new DataColumn("UsagePlant", typeof(string)));
            drPalntM = dtPlantM.NewRow();
            drPalntM["SNo"] = 1;
            drPalntM["MachineModelSpec"] = string.Empty;
            drPalntM["MakePlant"] = string.Empty;
            drPalntM["QuanPlant"] = string.Empty;
            drPalntM["DOPPlant"] = string.Empty;
            drPalntM["UsagePlant"] = string.Empty;
            dtPlantM.Rows.Add(drPalntM);
            //Store the DataTable in ViewState or bind or show false grid
            ViewState["PlantM"] = dtPlantM;
            gvplantandmachines.DataSource = dtPlantM;
            gvplantandmachines.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void AddNewRowToGridPM()
    {
        try
        {
            int ProwIndex = 0;
            if (ViewState["PlantM"] != null)
            {
                DataTable dtCurrentTablePlantMan = (DataTable)ViewState["PlantM"];
                DataRow drCurrentRowPalntMan = null;
                if (dtCurrentTablePlantMan.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTablePlantMan.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        TextBox TextBox1P = (TextBox)gvplantandmachines.Rows[ProwIndex].Cells[1].FindControl("txtPlantandMachineName");
                        TextBox TextBox2P = (TextBox)gvplantandmachines.Rows[ProwIndex].Cells[2].FindControl("txtPlantMachine");
                        TextBox TextBox3P = (TextBox)gvplantandmachines.Rows[ProwIndex].Cells[3].FindControl("txtQuanProdManu");
                        TextBox TextBox4P = (TextBox)gvplantandmachines.Rows[ProwIndex].Cells[4].FindControl("txtplantmachiPurchase");
                        TextBox TextBox5P = (TextBox)gvplantandmachines.Rows[ProwIndex].Cells[5].FindControl("txtPlantMachiUsage");
                        drCurrentRowPalntMan = dtCurrentTablePlantMan.NewRow();
                        drCurrentRowPalntMan["SNo"] = i + 1;
                        dtCurrentTablePlantMan.Rows[i - 1]["MachineModelSpec"] = TextBox1P.Text;
                        dtCurrentTablePlantMan.Rows[i - 1]["MakePlant"] = TextBox2P.Text;
                        dtCurrentTablePlantMan.Rows[i - 1]["QuanPlant"] = TextBox3P.Text;
                        dtCurrentTablePlantMan.Rows[i - 1]["DOPPlant"] = TextBox4P.Text;
                        dtCurrentTablePlantMan.Rows[i - 1]["UsagePlant"] = TextBox5P.Text;
                        ProwIndex++;
                    }
                    dtCurrentTablePlantMan.Rows.Add(drCurrentRowPalntMan);
                    ViewState["PlantM"] = dtCurrentTablePlantMan;
                    gvplantandmachines.DataSource = dtCurrentTablePlantMan;
                    gvplantandmachines.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousDataPM();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void SetPreviousDataPM()
    {
        try
        {
            int rowIndexPM = 0;
            if (ViewState["PlantM"] != null)
            {
                DataTable dtPlantM = (DataTable)ViewState["PlantM"];
                if (dtPlantM.Rows.Count > 0)
                {
                    for (int i = 0; i < dtPlantM.Rows.Count; i++)
                    {
                        TextBox TextBox_1 = (TextBox)gvplantandmachines.Rows[rowIndexPM].Cells[1].FindControl("txtPlantandMachineName");
                        TextBox TextBox_2 = (TextBox)gvplantandmachines.Rows[rowIndexPM].Cells[2].FindControl("txtPlantMachine");
                        TextBox TextBox_3 = (TextBox)gvplantandmachines.Rows[rowIndexPM].Cells[3].FindControl("txtQuanProdManu");
                        TextBox TextBox_4 = (TextBox)gvplantandmachines.Rows[rowIndexPM].Cells[4].FindControl("txtplantmachiPurchase");
                        TextBox TextBox_5 = (TextBox)gvplantandmachines.Rows[rowIndexPM].Cells[5].FindControl("txtPlantMachiUsage");
                        TextBox_1.Text = dtPlantM.Rows[i]["MachineModelSpec"].ToString();
                        TextBox_2.Text = dtPlantM.Rows[i]["MakePlant"].ToString();
                        TextBox_3.Text = dtPlantM.Rows[i]["QuanPlant"].ToString();
                        TextBox_4.Text = dtPlantM.Rows[i]["DOPPlant"].ToString();
                        TextBox_5.Text = dtPlantM.Rows[i]["UsagePlant"].ToString();
                        rowIndexPM++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void gvplantandmachines_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dtgridPlantManu = (DataTable)ViewState["PlantM"];
                LinkButton lbPManu = (LinkButton)e.Row.FindControl("lblRemovePlantMachine");
                if (lbPManu != null)
                {
                    if (dtgridPlantManu.Rows.Count > 1)
                    {
                        if (e.Row.RowIndex == dtgridPlantManu.Rows.Count - 1)
                        {
                            lbPManu.Visible = false;
                        }
                    }
                    else
                    {
                        lbPManu.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void btnAddPlantorMachine_Click(object sender, EventArgs e)
    {
        AddNewRowToGridPM();
    }
    protected void lblRemovePlantMachine_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lbPM = (LinkButton)sender;
            GridViewRow gvRowPManuf = (GridViewRow)lbPM.NamingContainer;
            int rowID = gvRowPManuf.RowIndex;
            if (ViewState["PlantM"] != null)
            {
                DataTable dtremovegridPlantManufac = (DataTable)ViewState["PlantM"];
                if (dtremovegridPlantManufac.Rows.Count > 1)
                {
                    if (gvRowPManuf.RowIndex < dtremovegridPlantManufac.Rows.Count - 1)
                    {
                        dtremovegridPlantManufac.Rows.Remove(dtremovegridPlantManufac.Rows[rowID]);
                        ResetRowIDPM(dtremovegridPlantManufac);
                    }
                }
                ViewState["PlantM"] = dtremovegridPlantManufac;
                gvplantandmachines.DataSource = dtremovegridPlantManufac;
                gvplantandmachines.DataBind();
            }
            SetPreviousDataPM();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void ResetRowIDPM(DataTable dtmPlantM)
    {
        try
        {
            int rowNumberPlantManufac = 1;
            if (dtmPlantM.Rows.Count > 0)
            {
                foreach (DataRow row in dtmPlantM.Rows)
                {
                    row[0] = rowNumberPlantManufac;
                    rowNumberPlantManufac++;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    DataTable dtPlantMSave = new DataTable();
    protected void SaveCodeForPlantM()
    {
        try
        {
            int rowIndex = 0;
            dtPlantMSave.Columns.Add(new DataColumn("SNo", typeof(string)));
            dtPlantMSave.Columns.Add(new DataColumn("MachineModelSpec", typeof(string)));
            dtPlantMSave.Columns.Add(new DataColumn("MakePlant", typeof(string)));
            dtPlantMSave.Columns.Add(new DataColumn("QuanPlant", typeof(string)));
            dtPlantMSave.Columns.Add(new DataColumn("DOPPlant", typeof(string)));
            dtPlantMSave.Columns.Add(new DataColumn("UsagePlant", typeof(string)));
            DataRow drPlantSave = null;
            for (int i = 0; gvplantandmachines.Rows.Count > i; i++)
            {
                TextBox TextBox1P = (TextBox)gvplantandmachines.Rows[i].Cells[1].FindControl("txtPlantandMachineName");
                TextBox TextBox2P = (TextBox)gvplantandmachines.Rows[i].Cells[2].FindControl("txtPlantMachine");
                TextBox TextBox3P = (TextBox)gvplantandmachines.Rows[i].Cells[3].FindControl("txtQuanProdManu");
                TextBox TextBox4P = (TextBox)gvplantandmachines.Rows[i].Cells[4].FindControl("txtplantmachiPurchase");
                TextBox TextBox5P = (TextBox)gvplantandmachines.Rows[i].Cells[5].FindControl("txtPlantMachiUsage");
                if (TextBox1P.Text != "" && TextBox2P.Text != "")
                {
                    drPlantSave = dtPlantMSave.NewRow();
                    drPlantSave["SNo"] = i + 1;
                    drPlantSave["MachineModelSpec"] = TextBox1P.Text;
                    drPlantSave["MakePlant"] = TextBox2P.Text;
                    drPlantSave["QuanPlant"] = TextBox3P.Text;
                    drPlantSave["DOPPlant"] = TextBox4P.Text;
                    drPlantSave["UsagePlant"] = TextBox5P.Text;
                    dtPlantMSave.Rows.Add(drPlantSave);
                }
            }
            ViewState["PlantM"] = dtPlantMSave;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    #endregion
    #region Add Grid to Emp List
    private void SetInitialRowEMPCI()
    {
        try
        {
            DataTable dtEmp1 = new DataTable();
            DataRow drEmp1 = null;
            dtEmp1.Columns.Add(new DataColumn("SNo", typeof(string)));
            dtEmp1.Columns.Add(new DataColumn("TEmp", typeof(string)));
            dtEmp1.Columns.Add(new DataColumn("Admins", typeof(string)));
            dtEmp1.Columns.Add(new DataColumn("Tech", typeof(string)));
            dtEmp1.Columns.Add(new DataColumn("NonTech", typeof(string)));
            dtEmp1.Columns.Add(new DataColumn("QCIns", typeof(string)));
            dtEmp1.Columns.Add(new DataColumn("SkLab", typeof(string)));
            dtEmp1.Columns.Add(new DataColumn("USKLab", typeof(string)));
            drEmp1 = dtEmp1.NewRow();
            drEmp1["SNo"] = 1;
            drEmp1["TEmp"] = string.Empty;
            drEmp1["Admins"] = string.Empty;
            drEmp1["Tech"] = string.Empty;
            drEmp1["NonTech"] = string.Empty;
            drEmp1["QCIns"] = string.Empty;
            drEmp1["SkLab"] = string.Empty;
            drEmp1["USKLab"] = string.Empty;
            dtEmp1.Rows.Add(drEmp1);
            //Store the DataTable in ViewState or bind or show false grid
            ViewState["EMP"] = dtEmp1;
            gvempCompInfo.DataSource = dtEmp1;
            gvempCompInfo.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void AddNewRowToGridEMPCI()
    {
        try
        {
            int EMProwIndex = 0;
            if (ViewState["EMP"] != null)
            {
                DataTable dtCurrentEMPCI = (DataTable)ViewState["EMP"];
                DataRow drCurrentEMPCI = null;
                if (dtCurrentEMPCI.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentEMPCI.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        TextBox TextBox1E = (TextBox)gvempCompInfo.Rows[EMProwIndex].Cells[1].FindControl("txttotalempCI");
                        TextBox TextBox2E = (TextBox)gvempCompInfo.Rows[EMProwIndex].Cells[2].FindControl("txtadministrativeCI");
                        TextBox TextBox3E = (TextBox)gvempCompInfo.Rows[EMProwIndex].Cells[3].FindControl("txttechCI");
                        TextBox TextBox4E = (TextBox)gvempCompInfo.Rows[EMProwIndex].Cells[4].FindControl("txtNontechCI");
                        TextBox TextBox5E = (TextBox)gvempCompInfo.Rows[EMProwIndex].Cells[5].FindControl("txtqcCI");
                        TextBox TextBox6E = (TextBox)gvempCompInfo.Rows[EMProwIndex].Cells[6].FindControl("txtskCI");
                        TextBox TextBox7E = (TextBox)gvempCompInfo.Rows[EMProwIndex].Cells[7].FindControl("txtuLCI");
                        drCurrentEMPCI = dtCurrentEMPCI.NewRow();
                        drCurrentEMPCI["SNo"] = i + 1;
                        dtCurrentEMPCI.Rows[i - 1]["TEmp"] = TextBox1E.Text;
                        dtCurrentEMPCI.Rows[i - 1]["Admins"] = TextBox2E.Text;
                        dtCurrentEMPCI.Rows[i - 1]["Tech"] = TextBox3E.Text;
                        dtCurrentEMPCI.Rows[i - 1]["NonTech"] = TextBox4E.Text;
                        dtCurrentEMPCI.Rows[i - 1]["QCIns"] = TextBox5E.Text;
                        dtCurrentEMPCI.Rows[i - 1]["SkLab"] = TextBox6E.Text;
                        dtCurrentEMPCI.Rows[i - 1]["USKLab"] = TextBox7E.Text;
                        EMProwIndex++;
                    }
                    dtCurrentEMPCI.Rows.Add(drCurrentEMPCI);
                    ViewState["EMP"] = dtCurrentEMPCI;
                    gvempCompInfo.DataSource = dtCurrentEMPCI;
                    gvempCompInfo.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousDataPM();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void SetPreviousDataEMPCI()
    {
        try
        {
            int rowIndexEMP = 0;
            if (ViewState["EMP"] != null)
            {
                DataTable dtEMPCI = (DataTable)ViewState["EMP"];
                if (dtEMPCI.Rows.Count > 0)
                {
                    for (int i = 0; i < dtEMPCI.Rows.Count; i++)
                    {
                        TextBox TextBox_1 = (TextBox)gvempCompInfo.Rows[rowIndexEMP].Cells[1].FindControl("txttotalempCI");
                        TextBox TextBox_2 = (TextBox)gvempCompInfo.Rows[rowIndexEMP].Cells[2].FindControl("txtadministrativeCI");
                        TextBox TextBox_3 = (TextBox)gvempCompInfo.Rows[rowIndexEMP].Cells[3].FindControl("txttechCI");
                        TextBox TextBox_4 = (TextBox)gvempCompInfo.Rows[rowIndexEMP].Cells[4].FindControl("txtNontechCI");
                        TextBox TextBox_5 = (TextBox)gvempCompInfo.Rows[rowIndexEMP].Cells[5].FindControl("txtqcCI");
                        TextBox TextBox_6 = (TextBox)gvempCompInfo.Rows[rowIndexEMP].Cells[6].FindControl("txtskCI");
                        TextBox TextBox_7 = (TextBox)gvempCompInfo.Rows[rowIndexEMP].Cells[7].FindControl("txtuLCI");
                        TextBox_1.Text = dtEMPCI.Rows[i]["TEmp"].ToString();
                        TextBox_2.Text = dtEMPCI.Rows[i]["Admins"].ToString();
                        TextBox_3.Text = dtEMPCI.Rows[i]["Tech"].ToString();
                        TextBox_4.Text = dtEMPCI.Rows[i]["NonTech"].ToString();
                        TextBox_5.Text = dtEMPCI.Rows[i]["QCIns"].ToString();
                        TextBox_6.Text = dtEMPCI.Rows[i]["SkLab"].ToString();
                        TextBox_7.Text = dtEMPCI.Rows[i]["USKLab"].ToString();
                        rowIndexEMP++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void gvempCompInfo_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dtgridEMPCI = (DataTable)ViewState["EMP"];
                LinkButton lbEMPCI = (LinkButton)e.Row.FindControl("lbRemoveEmp");
                if (lbEMPCI != null)
                {
                    if (dtgridEMPCI.Rows.Count > 1)
                    {
                        if (e.Row.RowIndex == dtgridEMPCI.Rows.Count - 1)
                        {
                            lbEMPCI.Visible = false;
                        }
                    }
                    else
                    {
                        lbEMPCI.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void btnEmpInfo_Click(object sender, EventArgs e)
    {
        AddNewRowToGridEMPCI();
    }
    protected void lbRemoveEmp_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lbEmpCI = (LinkButton)sender;
            GridViewRow gvRowEmp = (GridViewRow)lbEmpCI.NamingContainer;
            int rowID = gvRowEmp.RowIndex;
            if (ViewState["EMP"] != null)
            {
                DataTable dtremoveEMP = (DataTable)ViewState["EMP"];
                if (dtremoveEMP.Rows.Count > 1)
                {
                    if (gvRowEmp.RowIndex < dtremoveEMP.Rows.Count - 1)
                    {
                        dtremoveEMP.Rows.Remove(dtremoveEMP.Rows[rowID]);
                        ResetRowIDEMPCI(dtremoveEMP);
                    }
                }
                ViewState["EMP"] = dtremoveEMP;
                gvempCompInfo.DataSource = dtremoveEMP;
                gvempCompInfo.DataBind();
            }
            SetPreviousDataEMPCI();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void ResetRowIDEMPCI(DataTable dtEmpCI)
    {
        try
        {
            int rowNumberEmpCI = 1;
            if (dtEmpCI.Rows.Count > 0)
            {
                foreach (DataRow row in dtEmpCI.Rows)
                {
                    row[0] = rowNumberEmpCI;
                    rowNumberEmpCI++;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    DataTable dtEMPCISave = new DataTable();
    protected void SaveCodeForEMPCISave()
    {
        try
        {
            int rowIndex = 0;
            dtEMPCISave.Columns.Add(new DataColumn("SNo", typeof(string)));
            dtEMPCISave.Columns.Add(new DataColumn("TEmp", typeof(string)));
            dtEMPCISave.Columns.Add(new DataColumn("Admins", typeof(string)));
            dtEMPCISave.Columns.Add(new DataColumn("Tech", typeof(string)));
            dtEMPCISave.Columns.Add(new DataColumn("NonTech", typeof(string)));
            dtEMPCISave.Columns.Add(new DataColumn("QCIns", typeof(string)));
            dtEMPCISave.Columns.Add(new DataColumn("SkLab", typeof(string)));
            dtEMPCISave.Columns.Add(new DataColumn("USKLab", typeof(string)));
            DataRow drEMPCISave = null;
            for (int i = 0; gvempCompInfo.Rows.Count > i; i++)
            {
                TextBox TextBox1E = (TextBox)gvempCompInfo.Rows[i].Cells[1].FindControl("txttotalempCI");
                TextBox TextBox2E = (TextBox)gvempCompInfo.Rows[i].Cells[2].FindControl("txtadministrativeCI");
                TextBox TextBox3E = (TextBox)gvempCompInfo.Rows[i].Cells[3].FindControl("txttechCI");
                TextBox TextBox4E = (TextBox)gvempCompInfo.Rows[i].Cells[4].FindControl("txtNontechCI");
                TextBox TextBox5E = (TextBox)gvempCompInfo.Rows[i].Cells[5].FindControl("txtqcCI");
                TextBox TextBox6E = (TextBox)gvempCompInfo.Rows[i].Cells[6].FindControl("txtskCI");
                TextBox TextBox7E = (TextBox)gvempCompInfo.Rows[i].Cells[7].FindControl("txtuLCI");
                if (TextBox1E.Text != "" && TextBox2E.Text != "")
                {
                    drEMPCISave = dtEMPCISave.NewRow();
                    drEMPCISave["SNo"] = i + 1;
                    drEMPCISave["TEmp"] = TextBox1E.Text;
                    drEMPCISave["Admins"] = TextBox2E.Text;
                    drEMPCISave["Tech"] = TextBox3E.Text;
                    drEMPCISave["NonTech"] = TextBox4E.Text;
                    drEMPCISave["QCIns"] = TextBox5E.Text;
                    drEMPCISave["SkLab"] = TextBox6E.Text;
                    drEMPCISave["USKLab"] = TextBox7E.Text;
                    dtEMPCISave.Rows.Add(drEMPCISave);
                }
            }
            ViewState["EMP"] = dtEMPCISave;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    #endregion
    #region  Grid of List of Test Facilities
    private void SetInitialRowTestFacili()
    {
        try
        {
            DataTable dtTFacili = new DataTable();
            DataRow drTFacili = null;
            dtTFacili.Columns.Add(new DataColumn("SNo", typeof(string)));
            dtTFacili.Columns.Add(new DataColumn("TestEqip", typeof(string)));
            dtTFacili.Columns.Add(new DataColumn("TestEqipMake", typeof(string)));
            dtTFacili.Columns.Add(new DataColumn("TestLeastCount", typeof(string)));
            dtTFacili.Columns.Add(new DataColumn("Rangeofmeasur", typeof(string)));
            dtTFacili.Columns.Add(new DataColumn("CertificationYear", typeof(string)));
            dtTFacili.Columns.Add(new DataColumn("YearofPurchase", typeof(string)));
            drTFacili = dtTFacili.NewRow();
            drTFacili["SNo"] = 1;
            drTFacili["TestEqip"] = string.Empty;
            drTFacili["TestEqipMake"] = string.Empty;
            drTFacili["TestLeastCount"] = string.Empty;
            drTFacili["Rangeofmeasur"] = string.Empty;
            drTFacili["CertificationYear"] = string.Empty;
            drTFacili["YearofPurchase"] = string.Empty;
            dtTFacili.Rows.Add(drTFacili);
            //Store the DataTable in ViewState or bind or show false grid
            ViewState["TestFaci"] = dtTFacili;
            gvtestfacilities.DataSource = dtTFacili;
            gvtestfacilities.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void AddNewRowToGridzTestFici()
    {
        try
        {
            int TFrowIndex = 0;
            if (ViewState["TestFaci"] != null)
            {
                DataTable dtCurrentTFI = (DataTable)ViewState["TestFaci"];
                DataRow drCurrentTFI = null;
                if (dtCurrentTFI.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTFI.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        TextBox TextBox1TF = (TextBox)gvtestfacilities.Rows[TFrowIndex].Cells[1].FindControl("txtnametestfesi");
                        TextBox TextBox2TF = (TextBox)gvtestfacilities.Rows[TFrowIndex].Cells[2].FindControl("txtmaketf");
                        TextBox TextBox3TF = (TextBox)gvtestfacilities.Rows[TFrowIndex].Cells[3].FindControl("txtcounttf");
                        TextBox TextBox4TF = (TextBox)gvtestfacilities.Rows[TFrowIndex].Cells[4].FindControl("txtrangetf");
                        TextBox TextBox5TF = (TextBox)gvtestfacilities.Rows[TFrowIndex].Cells[5].FindControl("txtcertiyeartf");
                        TextBox TextBox6TF = (TextBox)gvtestfacilities.Rows[TFrowIndex].Cells[6].FindControl("txtyearofpurtf");
                        drCurrentTFI = dtCurrentTFI.NewRow();
                        drCurrentTFI["SNo"] = i + 1;
                        dtCurrentTFI.Rows[i - 1]["TestEqip"] = TextBox1TF.Text;
                        dtCurrentTFI.Rows[i - 1]["TestEqipMake"] = TextBox2TF.Text;
                        dtCurrentTFI.Rows[i - 1]["TestLeastCount"] = TextBox3TF.Text;
                        dtCurrentTFI.Rows[i - 1]["Rangeofmeasur"] = TextBox4TF.Text;
                        dtCurrentTFI.Rows[i - 1]["CertificationYear"] = TextBox5TF.Text;
                        dtCurrentTFI.Rows[i - 1]["YearofPurchase"] = TextBox6TF.Text;
                        TFrowIndex++;
                    }
                    dtCurrentTFI.Rows.Add(drCurrentTFI);
                    ViewState["TestFaci"] = dtCurrentTFI;
                    gvtestfacilities.DataSource = dtCurrentTFI;
                    gvtestfacilities.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousDataTestFici();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void SetPreviousDataTestFici()
    {
        try
        {
            int rowIndexTF = 0;
            if (ViewState["TestFaci"] != null)
            {
                DataTable dtPTF = (DataTable)ViewState["TestFaci"];
                if (dtPTF.Rows.Count > 0)
                {
                    for (int i = 0; i < dtPTF.Rows.Count; i++)
                    {
                        TextBox TextBox_1 = (TextBox)gvtestfacilities.Rows[rowIndexTF].Cells[1].FindControl("txtnametestfesi");
                        TextBox TextBox_2 = (TextBox)gvtestfacilities.Rows[rowIndexTF].Cells[2].FindControl("txtmaketf");
                        TextBox TextBox_3 = (TextBox)gvtestfacilities.Rows[rowIndexTF].Cells[3].FindControl("txtcounttf");
                        TextBox TextBox_4 = (TextBox)gvtestfacilities.Rows[rowIndexTF].Cells[4].FindControl("txtrangetf");
                        TextBox TextBox_5 = (TextBox)gvtestfacilities.Rows[rowIndexTF].Cells[5].FindControl("txtcertiyeartf");
                        TextBox TextBox_6 = (TextBox)gvtestfacilities.Rows[rowIndexTF].Cells[6].FindControl("txtyearofpurtf");
                        TextBox_1.Text = dtPTF.Rows[i]["TestEqip"].ToString();
                        TextBox_2.Text = dtPTF.Rows[i]["TestEqipMake"].ToString();
                        TextBox_3.Text = dtPTF.Rows[i]["TestLeastCount"].ToString();
                        TextBox_4.Text = dtPTF.Rows[i]["Rangeofmeasur"].ToString();
                        TextBox_5.Text = dtPTF.Rows[i]["CertificationYear"].ToString();
                        TextBox_6.Text = dtPTF.Rows[i]["YearofPurchase"].ToString();
                        rowIndexTF++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void gvtestfacilities_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dtgridTeFi = (DataTable)ViewState["TestFaci"];
                LinkButton lbTF = (LinkButton)e.Row.FindControl("lbRemovetestfacili");
                if (lbTF != null)
                {
                    if (dtgridTeFi.Rows.Count > 1)
                    {
                        if (e.Row.RowIndex == dtgridTeFi.Rows.Count - 1)
                        {
                            lbTF.Visible = false;
                        }
                    }
                    else
                    {
                        lbTF.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void btntestfacilities_Click(object sender, EventArgs e)
    {
        AddNewRowToGridzTestFici();
    }
    protected void lbRemovetestfacili_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lbTF = (LinkButton)sender;
            GridViewRow gvRowTF = (GridViewRow)lbTF.NamingContainer;
            int rowID = gvRowTF.RowIndex;
            if (ViewState["TestFaci"] != null)
            {
                DataTable dtremoveTestFaci = (DataTable)ViewState["TestFaci"];
                if (dtremoveTestFaci.Rows.Count > 1)
                {
                    if (gvRowTF.RowIndex < dtremoveTestFaci.Rows.Count - 1)
                    {
                        dtremoveTestFaci.Rows.Remove(dtremoveTestFaci.Rows[rowID]);
                        ResetRowIDTestFaci(dtremoveTestFaci);
                    }
                }
                ViewState["TestFaci"] = dtremoveTestFaci;
                gvtestfacilities.DataSource = dtremoveTestFaci;
                gvtestfacilities.DataBind();
            }
            SetPreviousDataTestFici();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void ResetRowIDTestFaci(DataTable dtTestFaci)
    {
        try
        {
            int rowNumberTestFCI = 1;
            if (dtTestFaci.Rows.Count > 0)
            {
                foreach (DataRow row in dtTestFaci.Rows)
                {
                    row[0] = rowNumberTestFCI;
                    rowNumberTestFCI++;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    DataTable dtTestFaciliSave = new DataTable();
    protected void SaveCodeForTestFaciliSave()
    {
        try
        {
            int rowIndex = 0;
            dtTestFaciliSave.Columns.Add(new DataColumn("SNo", typeof(string)));
            dtTestFaciliSave.Columns.Add(new DataColumn("TestEqip", typeof(string)));
            dtTestFaciliSave.Columns.Add(new DataColumn("TestEqipMake", typeof(string)));
            dtTestFaciliSave.Columns.Add(new DataColumn("TestLeastCount", typeof(string)));
            dtTestFaciliSave.Columns.Add(new DataColumn("Rangeofmeasur", typeof(string)));
            dtTestFaciliSave.Columns.Add(new DataColumn("CertificationYear", typeof(string)));
            dtTestFaciliSave.Columns.Add(new DataColumn("YearofPurchase", typeof(string)));
            DataRow drTestFaciliSave = null;
            for (int i = 0; gvempCompInfo.Rows.Count > i; i++)
            {
                TextBox TextBox1TF = (TextBox)gvtestfacilities.Rows[i].Cells[1].FindControl("txtnametestfesi");
                TextBox TextBox2TF = (TextBox)gvtestfacilities.Rows[i].Cells[2].FindControl("txtmaketf");
                TextBox TextBox3TF = (TextBox)gvtestfacilities.Rows[i].Cells[3].FindControl("txtcounttf");
                TextBox TextBox4TF = (TextBox)gvtestfacilities.Rows[i].Cells[4].FindControl("txtrangetf");
                TextBox TextBox5TF = (TextBox)gvtestfacilities.Rows[i].Cells[5].FindControl("txtcertiyeartf");
                TextBox TextBox6TF = (TextBox)gvtestfacilities.Rows[i].Cells[6].FindControl("txtyearofpurtf");
                if (TextBox1TF.Text != "" && TextBox2TF.Text != "")
                {
                    drTestFaciliSave = dtTestFaciliSave.NewRow();
                    drTestFaciliSave["SNo"] = i + 1;
                    drTestFaciliSave["TestEqip"] = TextBox1TF.Text;
                    drTestFaciliSave["TestEqipMake"] = TextBox2TF.Text;
                    drTestFaciliSave["TestLeastCount"] = TextBox3TF.Text;
                    drTestFaciliSave["Rangeofmeasur"] = TextBox4TF.Text;
                    drTestFaciliSave["CertificationYear"] = TextBox5TF.Text;
                    drTestFaciliSave["YearofPurchase"] = TextBox6TF.Text;
                    dtTestFaciliSave.Rows.Add(drTestFaciliSave);
                }
            }
            ViewState["TestFaci"] = dtTestFaciliSave;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    #endregion
    #region Grid of List  Authorise dealer
    private void SetInitialRowAuthodel()
    {
        try
        {
            DataTable dtAu = new DataTable();
            DataRow drAu = null;
            dtAu.Columns.Add(new DataColumn("SNo", typeof(string)));
            dtAu.Columns.Add(new DataColumn("DName", typeof(string)));
            dtAu.Columns.Add(new DataColumn("DAddress", typeof(string)));
            dtAu.Columns.Add(new DataColumn("DState", typeof(string)));
            dtAu.Columns.Add(new DataColumn("DPincode", typeof(string)));
            dtAu.Columns.Add(new DataColumn("DPhone", typeof(string)));
            dtAu.Columns.Add(new DataColumn("DFax", typeof(string)));
            dtAu.Columns.Add(new DataColumn("DEmail", typeof(string)));
            drAu = dtAu.NewRow();
            drAu["SNo"] = 1;
            drAu["DName"] = string.Empty;
            drAu["DAddress"] = string.Empty;
            drAu["DState"] = string.Empty;
            drAu["DPincode"] = string.Empty;
            drAu["DPhone"] = string.Empty;
            drAu["DFax"] = string.Empty;
            drAu["DEmail"] = string.Empty;
            dtAu.Rows.Add(drAu);
            //Store the DataTable in ViewState or bind or show false grid
            ViewState["DAuth"] = dtAu;
            gvauthdealaddress.DataSource = dtAu;
            gvauthdealaddress.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void AddNewRowToGridAuthdel()
    {
        try
        {
            int Index = 0;
            if (ViewState["DAuth"] != null)
            {
                DataTable dtCurrentDAut = (DataTable)ViewState["DAuth"];
                DataRow drCurrentDAut = null;
                if (dtCurrentDAut.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentDAut.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        TextBox TextBox7DA = (TextBox)gvauthdealaddress.Rows[Index].Cells[1].FindControl("txtDName");
                        TextBox TextBox1DA = (TextBox)gvauthdealaddress.Rows[Index].Cells[2].FindControl("txtDstreetaddress");
                        TextBox TextBox2DA = (TextBox)gvauthdealaddress.Rows[Index].Cells[3].FindControl("txtdState");
                        TextBox TextBox3DA = (TextBox)gvauthdealaddress.Rows[Index].Cells[4].FindControl("txtDPincode");
                        TextBox TextBox4DA = (TextBox)gvauthdealaddress.Rows[Index].Cells[5].FindControl("txtDPhone");
                        TextBox TextBox5DA = (TextBox)gvauthdealaddress.Rows[Index].Cells[6].FindControl("txtDFax");
                        TextBox TextBox6DA = (TextBox)gvauthdealaddress.Rows[Index].Cells[7].FindControl("txtDEmail");
                        drCurrentDAut = dtCurrentDAut.NewRow();
                        drCurrentDAut["SNo"] = i + 1;
                        dtCurrentDAut.Rows[i - 1]["DName"] = TextBox7DA.Text;
                        dtCurrentDAut.Rows[i - 1]["DAddress"] = TextBox1DA.Text;
                        dtCurrentDAut.Rows[i - 1]["DState"] = TextBox2DA.Text;
                        dtCurrentDAut.Rows[i - 1]["DPincode"] = TextBox3DA.Text;
                        dtCurrentDAut.Rows[i - 1]["DPhone"] = TextBox4DA.Text;
                        dtCurrentDAut.Rows[i - 1]["DFax"] = TextBox5DA.Text;
                        dtCurrentDAut.Rows[i - 1]["DEmail"] = TextBox6DA.Text;
                        Index++;
                    }
                    dtCurrentDAut.Rows.Add(drCurrentDAut);
                    ViewState["DAuth"] = dtCurrentDAut;
                    gvauthdealaddress.DataSource = dtCurrentDAut;
                    gvauthdealaddress.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousDataDAuth();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void SetPreviousDataDAuth()
    {
        try
        {
            int rowIndexDA = 0;
            if (ViewState["DAuth"] != null)
            {
                DataTable dtDA = (DataTable)ViewState["DAuth"];
                if (dtDA.Rows.Count > 0)
                {
                    for (int i = 0; i < dtDA.Rows.Count; i++)
                    {
                        TextBox TextBox_7 = (TextBox)gvauthdealaddress.Rows[rowIndexDA].Cells[1].FindControl("txtDName");
                        TextBox TextBox_1 = (TextBox)gvauthdealaddress.Rows[rowIndexDA].Cells[2].FindControl("txtDstreetaddress");
                        TextBox TextBox_2 = (TextBox)gvauthdealaddress.Rows[rowIndexDA].Cells[3].FindControl("txtdState");
                        TextBox TextBox_3 = (TextBox)gvauthdealaddress.Rows[rowIndexDA].Cells[4].FindControl("txtDPincode");
                        TextBox TextBox_4 = (TextBox)gvauthdealaddress.Rows[rowIndexDA].Cells[5].FindControl("txtDPhone");
                        TextBox TextBox_5 = (TextBox)gvauthdealaddress.Rows[rowIndexDA].Cells[6].FindControl("txtDFax");
                        TextBox TextBox_6 = (TextBox)gvauthdealaddress.Rows[rowIndexDA].Cells[7].FindControl("txtDEmail");
                        TextBox_1.Text = dtDA.Rows[i]["DAddress"].ToString();
                        TextBox_2.Text = dtDA.Rows[i]["DState"].ToString();
                        TextBox_3.Text = dtDA.Rows[i]["DPincode"].ToString();
                        TextBox_4.Text = dtDA.Rows[i]["DPhone"].ToString();
                        TextBox_5.Text = dtDA.Rows[i]["DFax"].ToString();
                        TextBox_6.Text = dtDA.Rows[i]["DEmail"].ToString();
                        TextBox_7.Text = dtDA.Rows[i]["DName"].ToString();
                        rowIndexDA++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void gvauthdealaddress_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dtgridDA = (DataTable)ViewState["DAuth"];
                LinkButton lbDA = (LinkButton)e.Row.FindControl("lbRemoveAuthdel");
                if (lbDA != null)
                {
                    if (dtgridDA.Rows.Count > 1)
                    {
                        if (e.Row.RowIndex == dtgridDA.Rows.Count - 1)
                        {
                            lbDA.Visible = false;
                        }
                    }
                    else
                    {
                        lbDA.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void btnautdeal_Click(object sender, EventArgs e)
    {
        try
        {
            AddNewRowToGridAuthdel();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void lbRemoveAuthdel_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lbDA = (LinkButton)sender;
            GridViewRow gvRowDA = (GridViewRow)lbDA.NamingContainer;
            int rowID = gvRowDA.RowIndex;
            if (ViewState["DAuth"] != null)
            {
                DataTable dtremoveDA = (DataTable)ViewState["DAuth"];
                if (dtremoveDA.Rows.Count > 1)
                {
                    if (gvRowDA.RowIndex < dtremoveDA.Rows.Count - 1)
                    {
                        dtremoveDA.Rows.Remove(dtremoveDA.Rows[rowID]);
                        ResetRowIDDA(dtremoveDA);
                    }
                }
                ViewState["DAuth"] = dtremoveDA;
                gvauthdealaddress.DataSource = dtremoveDA;
                gvauthdealaddress.DataBind();
            }
            SetPreviousDataDAuth();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void ResetRowIDDA(DataTable dtDA)
    {
        try
        {
            int rowNumberDA = 1;
            if (dtDA.Rows.Count > 0)
            {
                foreach (DataRow row in dtDA.Rows)
                {
                    row[0] = rowNumberDA;
                    rowNumberDA++;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    DataTable dtAuSave = new DataTable();
    protected void SaveCodeForAUSave()
    {
        try
        {
            int rowIndex = 0;
            dtAuSave.Columns.Add(new DataColumn("SNo", typeof(string)));
            dtAuSave.Columns.Add(new DataColumn("DAddress", typeof(string)));
            dtAuSave.Columns.Add(new DataColumn("DState", typeof(string)));
            dtAuSave.Columns.Add(new DataColumn("DPincode", typeof(string)));
            dtAuSave.Columns.Add(new DataColumn("DPhone", typeof(string)));
            dtAuSave.Columns.Add(new DataColumn("DFax", typeof(string)));
            dtAuSave.Columns.Add(new DataColumn("DEmail", typeof(string)));
            DataRow drAUSave = null;
            for (int i = 0; gvauthdealaddress.Rows.Count > i; i++)
            {
                TextBox TextBox1DA = (TextBox)gvauthdealaddress.Rows[i].Cells[1].FindControl("txtDstreetaddress");
                TextBox TextBox2DA = (TextBox)gvauthdealaddress.Rows[i].Cells[2].FindControl("txtdState");
                TextBox TextBox3DA = (TextBox)gvauthdealaddress.Rows[i].Cells[3].FindControl("txtDPincode");
                TextBox TextBox4DA = (TextBox)gvauthdealaddress.Rows[i].Cells[4].FindControl("txtDPhone");
                TextBox TextBox5DA = (TextBox)gvauthdealaddress.Rows[i].Cells[5].FindControl("txtDFax");
                TextBox TextBox6DA = (TextBox)gvauthdealaddress.Rows[i].Cells[6].FindControl("txtDEmail");
                if (TextBox1DA.Text != "" && TextBox2DA.Text != "")
                {
                    drAUSave = dtAuSave.NewRow();
                    drAUSave["SNo"] = i + 1;
                    drAUSave["DAddress"] = TextBox1DA.Text;
                    drAUSave["DPincode"] = TextBox2DA.Text;
                    drAUSave["DPhone"] = TextBox3DA.Text;
                    drAUSave["DFax"] = TextBox4DA.Text;
                    drAUSave["DEmail"] = TextBox5DA.Text;
                    dtAuSave.Rows.Add(drAUSave);
                }
            }
            ViewState["DAuth"] = dtAuSave;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    #endregion
    #region Grid of List of Joint-Venture Facility
    private void SetInitialRowJVF()
    {
        try
        {
            DataTable dtJVF1 = new DataTable();
            DataRow drJVF1 = null;
            dtJVF1.Columns.Add(new DataColumn("SNo", typeof(string)));
            dtJVF1.Columns.Add(new DataColumn("JVFName", typeof(string)));
            dtJVF1.Columns.Add(new DataColumn("JVFIs", typeof(string)));
            dtJVF1.Columns.Add(new DataColumn("JVFAddress", typeof(string)));
            dtJVF1.Columns.Add(new DataColumn("JVFOffName", typeof(string)));
            dtJVF1.Columns.Add(new DataColumn("JVFTel", typeof(string)));
            dtJVF1.Columns.Add(new DataColumn("JVFFAX", typeof(string)));
            dtJVF1.Columns.Add(new DataColumn("JVFEmail", typeof(string)));
            drJVF1 = dtJVF1.NewRow();
            drJVF1["SNo"] = 1;
            drJVF1["JVFName"] = string.Empty;
            drJVF1["JVFIs"] = string.Empty;
            drJVF1["JVFAddress"] = string.Empty;
            drJVF1["JVFOffName"] = string.Empty;
            drJVF1["JVFTel"] = string.Empty;
            drJVF1["JVFFAX"] = string.Empty;
            drJVF1["JVFEmail"] = string.Empty;
            dtJVF1.Rows.Add(drJVF1);
            //Store the DataTable in ViewState or bind or show false grid
            ViewState["JVF"] = dtJVF1;
            gvjointventure.DataSource = dtJVF1;
            gvjointventure.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void AddNewRowToGridJVF()
    {
        try
        {
            int JVFIndex = 0;
            if (ViewState["JVF"] != null)
            {
                DataTable dtCurrentJVF = (DataTable)ViewState["JVF"];
                DataRow drCurrentJVF = null;
                if (dtCurrentJVF.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentJVF.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        TextBox TextBox1JVF = (TextBox)gvjointventure.Rows[JVFIndex].Cells[1].FindControl("txtjvfname");
                        DropDownList ddl2jvf = (DropDownList)gvjointventure.Rows[JVFIndex].Cells[2].FindControl("ddljvfis");
                        TextBox TextBox3JVF = (TextBox)gvjointventure.Rows[JVFIndex].Cells[3].FindControl("txtjvfaddress");
                        TextBox TextBox4JVF = (TextBox)gvjointventure.Rows[JVFIndex].Cells[4].FindControl("txtjvfoffname");
                        TextBox TextBox5JVF = (TextBox)gvjointventure.Rows[JVFIndex].Cells[5].FindControl("txtjvftele");
                        TextBox TextBox6JVF = (TextBox)gvjointventure.Rows[JVFIndex].Cells[6].FindControl("txtjvffax");
                        TextBox TextBox7JVF = (TextBox)gvjointventure.Rows[JVFIndex].Cells[7].FindControl("txtjvfemail");
                        drCurrentJVF = dtCurrentJVF.NewRow();
                        drCurrentJVF["SNo"] = i + 1;
                        dtCurrentJVF.Rows[i - 1]["JVFName"] = TextBox1JVF.Text;
                        dtCurrentJVF.Rows[i - 1]["JVFIs"] = ddl2jvf.Text;
                        dtCurrentJVF.Rows[i - 1]["JVFAddress"] = TextBox3JVF.Text;
                        dtCurrentJVF.Rows[i - 1]["JVFOffName"] = TextBox4JVF.Text;
                        dtCurrentJVF.Rows[i - 1]["JVFTel"] = TextBox5JVF.Text;
                        dtCurrentJVF.Rows[i - 1]["JVFFAX"] = TextBox6JVF.Text;
                        dtCurrentJVF.Rows[i - 1]["JVFEmail"] = TextBox7JVF.Text;
                        JVFIndex++;
                    }
                    dtCurrentJVF.Rows.Add(drCurrentJVF);
                    ViewState["JVF"] = dtCurrentJVF;
                    gvjointventure.DataSource = dtCurrentJVF;
                    gvjointventure.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousDataJVF();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void SetPreviousDataJVF()
    {
        try
        {
            int rowIndexJVF = 0;
            if (ViewState["JVF"] != null)
            {
                DataTable dtJVF = (DataTable)ViewState["JVF"];
                if (dtJVF.Rows.Count > 0)
                {
                    for (int i = 0; i < dtJVF.Rows.Count; i++)
                    {
                        TextBox TextBox_1 = (TextBox)gvjointventure.Rows[rowIndexJVF].Cells[1].FindControl("txtjvfname");
                        DropDownList ddl_2 = (DropDownList)gvjointventure.Rows[rowIndexJVF].Cells[2].FindControl("ddljvfis");
                        TextBox TextBox_3 = (TextBox)gvjointventure.Rows[rowIndexJVF].Cells[3].FindControl("txtjvfaddress");
                        TextBox TextBox_4 = (TextBox)gvjointventure.Rows[rowIndexJVF].Cells[4].FindControl("txtjvfoffname");
                        TextBox TextBox_5 = (TextBox)gvjointventure.Rows[rowIndexJVF].Cells[5].FindControl("txtjvftele");
                        TextBox TextBox_6 = (TextBox)gvjointventure.Rows[rowIndexJVF].Cells[6].FindControl("txtjvffax");
                        TextBox TextBox_7 = (TextBox)gvjointventure.Rows[rowIndexJVF].Cells[7].FindControl("txtjvfemail");
                        TextBox_1.Text = dtJVF.Rows[i]["JVFName"].ToString();
                        ddl_2.Text = dtJVF.Rows[i]["JVFIs"].ToString();
                        TextBox_3.Text = dtJVF.Rows[i]["JVFAddress"].ToString();
                        TextBox_4.Text = dtJVF.Rows[i]["JVFOffName"].ToString();
                        TextBox_5.Text = dtJVF.Rows[i]["JVFTel"].ToString();
                        TextBox_6.Text = dtJVF.Rows[i]["JVFFAX"].ToString();
                        TextBox_7.Text = dtJVF.Rows[i]["JVFEmail"].ToString();
                        rowIndexJVF++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void gvjointventure_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dtgridJVF1 = (DataTable)ViewState["JVF"];
                LinkButton lbJVF = (LinkButton)e.Row.FindControl("lbRemovejointven");
                if (lbJVF != null)
                {
                    if (dtgridJVF1.Rows.Count > 1)
                    {
                        if (e.Row.RowIndex == dtgridJVF1.Rows.Count - 1)
                        {
                            lbJVF.Visible = false;
                        }
                    }
                    else
                    {
                        lbJVF.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void btnjointven_Click(object sender, EventArgs e)
    {
        AddNewRowToGridJVF();
    }
    protected void lbRemovejointven_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lbJVF = (LinkButton)sender;
            GridViewRow gvRowJVF = (GridViewRow)lbJVF.NamingContainer;
            int rowID = gvRowJVF.RowIndex;
            if (ViewState["JVF"] != null)
            {
                DataTable dtremoveJVF = (DataTable)ViewState["JVF"];
                if (dtremoveJVF.Rows.Count > 1)
                {
                    if (gvRowJVF.RowIndex < dtremoveJVF.Rows.Count - 1)
                    {
                        dtremoveJVF.Rows.Remove(dtremoveJVF.Rows[rowID]);
                        ResetRowIDJVF(dtremoveJVF);
                    }
                }
                ViewState["JVF"] = dtremoveJVF;
                gvjointventure.DataSource = dtremoveJVF;
                gvjointventure.DataBind();
            }
            SetPreviousDataJVF();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void ResetRowIDJVF(DataTable dtJVF)
    {
        try
        {
            int rowNumberJVF = 1;
            if (dtJVF.Rows.Count > 0)
            {
                foreach (DataRow row in dtJVF.Rows)
                {
                    row[0] = rowNumberJVF;
                    rowNumberJVF++;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    DataTable dtJVSave = new DataTable();
    protected void SaveCodeForJVSave()
    {
        try
        {
            int rowIndex = 0;
            dtJVSave.Columns.Add(new DataColumn("SNo", typeof(string)));
            dtJVSave.Columns.Add(new DataColumn("JVFName", typeof(string)));
            dtJVSave.Columns.Add(new DataColumn("JVFIs", typeof(string)));
            dtJVSave.Columns.Add(new DataColumn("JVFAddress", typeof(string)));
            dtJVSave.Columns.Add(new DataColumn("JVFOffName", typeof(string)));
            dtJVSave.Columns.Add(new DataColumn("JVFTel", typeof(string)));
            dtJVSave.Columns.Add(new DataColumn("JVFFAX", typeof(string)));
            dtJVSave.Columns.Add(new DataColumn("JVFEmail", typeof(string)));
            DataRow drJVSave = null;
            for (int i = 0; gvjointventure.Rows.Count > i; i++)
            {
                TextBox TextBox1JVF = (TextBox)gvjointventure.Rows[i].Cells[1].FindControl("txtjvfname");
                DropDownList ddl2jvf = (DropDownList)gvjointventure.Rows[i].Cells[2].FindControl("ddljvfis");
                TextBox TextBox3JVF = (TextBox)gvjointventure.Rows[i].Cells[3].FindControl("txtjvfaddress");
                TextBox TextBox4JVF = (TextBox)gvjointventure.Rows[i].Cells[4].FindControl("txtjvfoffname");
                TextBox TextBox5JVF = (TextBox)gvjointventure.Rows[i].Cells[5].FindControl("txtjvftele");
                TextBox TextBox6JVF = (TextBox)gvjointventure.Rows[i].Cells[6].FindControl("txtjvffax");
                TextBox TextBox7JVF = (TextBox)gvjointventure.Rows[i].Cells[7].FindControl("txtjvfemail");
                if (TextBox1JVF.Text != "" && TextBox3JVF.Text != "")
                {
                    drJVSave = dtJVSave.NewRow();
                    drJVSave["SNo"] = i + 1;
                    drJVSave["JVFName"] = TextBox1JVF.Text;
                    drJVSave["JVFIs"] = ddl2jvf.SelectedItem.Text;
                    drJVSave["JVFAddress"] = TextBox3JVF.Text;
                    drJVSave["JVFOffName"] = TextBox4JVF.Text;
                    drJVSave["JVFTel"] = TextBox5JVF.Text;
                    drJVSave["JVFFAX"] = TextBox6JVF.Text;
                    drJVSave["JVFEmail"] = TextBox7JVF.Text;
                    dtJVSave.Rows.Add(drJVSave);
                }
            }
            ViewState["JVF"] = dtJVSave;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    #endregion
    #region Grid of List  Details of Outsourcing Facilites
    private void SetInitialRowOF()
    {
        try
        {
            DataTable dtof1 = new DataTable();
            DataRow drof1 = null;
            dtof1.Columns.Add(new DataColumn("SNo", typeof(string)));
            dtof1.Columns.Add(new DataColumn("MEquipment", typeof(string)));
            dtof1.Columns.Add(new DataColumn("TEquipment", typeof(string)));
            dtof1.Columns.Add(new DataColumn("PFacility", typeof(string)));
            dtof1.Columns.Add(new DataColumn("NASub", typeof(string)));
            drof1 = dtof1.NewRow();
            drof1["SNo"] = 1;
            drof1["MEquipment"] = string.Empty;
            drof1["TEquipment"] = string.Empty;
            drof1["PFacility"] = string.Empty;
            drof1["NASub"] = string.Empty;
            dtof1.Rows.Add(drof1);
            //Store the DataTable in ViewState or bind or show false grid
            ViewState["OF"] = dtof1;
            gvoutsourcefacility.DataSource = dtof1;
            gvoutsourcefacility.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void AddNewRowToGridOF()
    {
        try
        {
            int OFIndex = 0;
            if (ViewState["OF"] != null)
            {
                DataTable dtCurrentOF = (DataTable)ViewState["OF"];
                DataRow drCurrentOF = null;
                if (dtCurrentOF.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentOF.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        TextBox TextBox1oF = (TextBox)gvoutsourcefacility.Rows[OFIndex].Cells[1].FindControl("txtnameofsource");
                        TextBox TextBox2oF = (TextBox)gvoutsourcefacility.Rows[OFIndex].Cells[2].FindControl("txttestequipof");
                        TextBox TextBox3oF = (TextBox)gvoutsourcefacility.Rows[OFIndex].Cells[3].FindControl("txtprofaciof");
                        TextBox TextBox4oF = (TextBox)gvoutsourcefacility.Rows[OFIndex].Cells[4].FindControl("txtnameaddof");
                        drCurrentOF = dtCurrentOF.NewRow();
                        drCurrentOF["SNo"] = i + 1;
                        dtCurrentOF.Rows[i - 1]["MEquipment"] = TextBox1oF.Text;
                        dtCurrentOF.Rows[i - 1]["TEquipment"] = TextBox2oF.Text;
                        dtCurrentOF.Rows[i - 1]["PFacility"] = TextBox3oF.Text;
                        dtCurrentOF.Rows[i - 1]["NASub"] = TextBox4oF.Text;
                        OFIndex++;
                    }
                    dtCurrentOF.Rows.Add(drCurrentOF);
                    ViewState["OF"] = dtCurrentOF;
                    gvoutsourcefacility.DataSource = dtCurrentOF;
                    gvoutsourcefacility.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousDataOF();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void SetPreviousDataOF()
    {
        try
        {
            int rowIndexOF = 0;
            if (ViewState["OF"] != null)
            {
                DataTable dtPOF = (DataTable)ViewState["OF"];
                if (dtPOF.Rows.Count > 0)
                {
                    for (int i = 0; i < dtPOF.Rows.Count; i++)
                    {
                        TextBox TextBox_1 = (TextBox)gvoutsourcefacility.Rows[rowIndexOF].Cells[1].FindControl("txtnameofsource");
                        TextBox TextBox_2 = (TextBox)gvoutsourcefacility.Rows[rowIndexOF].Cells[2].FindControl("txttestequipof");
                        TextBox TextBox_3 = (TextBox)gvoutsourcefacility.Rows[rowIndexOF].Cells[3].FindControl("txtprofaciof");
                        TextBox TextBox_4 = (TextBox)gvoutsourcefacility.Rows[rowIndexOF].Cells[4].FindControl("txtnameaddof");
                        TextBox_1.Text = dtPOF.Rows[i]["MEquipment"].ToString();
                        TextBox_2.Text = dtPOF.Rows[i]["TEquipment"].ToString();
                        TextBox_3.Text = dtPOF.Rows[i]["PFacility"].ToString();
                        TextBox_4.Text = dtPOF.Rows[i]["NASub"].ToString();
                        rowIndexOF++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void gvoutsourcefacility_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dtgridOF = (DataTable)ViewState["OF"];
                LinkButton lbOF = (LinkButton)e.Row.FindControl("lbRemoveOutfaci");
                if (lbOF != null)
                {
                    if (dtgridOF.Rows.Count > 1)
                    {
                        if (e.Row.RowIndex == dtgridOF.Rows.Count - 1)
                        {
                            lbOF.Visible = false;
                        }
                    }
                    else
                    {
                        lbOF.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void btnoutsourcefac_Click(object sender, EventArgs e)
    {
        AddNewRowToGridOF();
    }
    protected void lbRemoveOutfaci_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lbOF = (LinkButton)sender;
            GridViewRow gvRowOF = (GridViewRow)lbOF.NamingContainer;
            int rowID = gvRowOF.RowIndex;
            if (ViewState["OF"] != null)
            {
                DataTable dtremoveOF = (DataTable)ViewState["OF"];
                if (dtremoveOF.Rows.Count > 1)
                {
                    if (gvRowOF.RowIndex < dtremoveOF.Rows.Count - 1)
                    {
                        dtremoveOF.Rows.Remove(dtremoveOF.Rows[rowID]);
                        ResetRowIDOF(dtremoveOF);
                    }
                }
                ViewState["OF"] = dtremoveOF;
                gvoutsourcefacility.DataSource = dtremoveOF;
                gvoutsourcefacility.DataBind();
            }
            SetPreviousDataOF();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void ResetRowIDOF(DataTable dtOF)
    {
        try
        {
            int rowNumberOF = 1;
            if (dtOF.Rows.Count > 0)
            {
                foreach (DataRow row in dtOF.Rows)
                {
                    row[0] = rowNumberOF;
                    rowNumberOF++;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    DataTable dtof1Save = new DataTable();
    protected void SaveCodeForof1Save()
    {
        try
        {
            int rowIndex = 0;
            dtof1Save.Columns.Add(new DataColumn("SNo", typeof(string)));
            dtof1Save.Columns.Add(new DataColumn("MEquipment", typeof(string)));
            dtof1Save.Columns.Add(new DataColumn("TEquipment", typeof(string)));
            dtof1Save.Columns.Add(new DataColumn("PFacility", typeof(string)));
            dtof1Save.Columns.Add(new DataColumn("NASub", typeof(string)));
            DataRow drof1Save = null;
            for (int i = 0; gvoutsourcefacility.Rows.Count > i; i++)
            {
                TextBox TextBox1oF = (TextBox)gvoutsourcefacility.Rows[i].Cells[1].FindControl("txtnameofsource");
                TextBox TextBox2oF = (TextBox)gvoutsourcefacility.Rows[i].Cells[2].FindControl("txttestequipof");
                TextBox TextBox3oF = (TextBox)gvoutsourcefacility.Rows[i].Cells[3].FindControl("txtprofaciof");
                TextBox TextBox4oF = (TextBox)gvoutsourcefacility.Rows[i].Cells[4].FindControl("txtnameaddof");
                if (TextBox1oF.Text != "" && TextBox2oF.Text != "")
                {
                    drof1Save = dtof1Save.NewRow();
                    drof1Save["SNo"] = i + 1;
                    drof1Save["MEquipment"] = TextBox1oF.Text;
                    drof1Save["TEquipment"] = TextBox2oF.Text;
                    drof1Save["PFacility"] = TextBox3oF.Text;
                    drof1Save["NASub"] = TextBox4oF.Text;
                    dtof1Save.Rows.Add(drof1Save);
                }
            }
            ViewState["OF"] = dtof1Save;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    #endregion
    #region Grid of Certificate
    private void SetInitialRowCertificate()
    {
        try
        {
            DataTable dtCertificate = new DataTable();
            DataRow drdtCertificate = null;
            dtCertificate.Columns.Add(new DataColumn("SNo", typeof(string)));
            dtCertificate.Columns.Add(new DataColumn("CheckBox", typeof(string)));
            dtCertificate.Columns.Add(new DataColumn("CertificateName", typeof(string)));
            dtCertificate.Columns.Add(new DataColumn("CertificateImage", typeof(string)));
            drdtCertificate = dtCertificate.NewRow();
            drdtCertificate["SNo"] = 1;
            drdtCertificate["CheckBox"] = "1";
            drdtCertificate["CertificateName"] = "Factory Licence /Municipal Shop's & Establishment";
            drdtCertificate["CertificateImage"] = string.Empty;
            dtCertificate.Rows.Add(drdtCertificate);
            drdtCertificate = dtCertificate.NewRow();
            drdtCertificate["SNo"] = 2;
            drdtCertificate["CheckBox"] = "2";
            drdtCertificate["CertificateName"] = "Registration Certificate from Labor Commissioner";
            drdtCertificate["CertificateImage"] = string.Empty;
            dtCertificate.Rows.Add(drdtCertificate);
            drdtCertificate = dtCertificate.NewRow();
            drdtCertificate["SNo"] = 3;
            drdtCertificate["CheckBox"] = "3";
            drdtCertificate["CertificateName"] = "VAT Registration Certificate";
            drdtCertificate["CertificateImage"] = string.Empty;
            dtCertificate.Rows.Add(drdtCertificate);
            drdtCertificate = dtCertificate.NewRow();
            drdtCertificate["SNo"] = 4;
            drdtCertificate["CheckBox"] = "4";
            drdtCertificate["CertificateName"] = "Excise Registration Certificate";
            drdtCertificate["CertificateImage"] = string.Empty;
            dtCertificate.Rows.Add(drdtCertificate);
            drdtCertificate = dtCertificate.NewRow();
            drdtCertificate["SNo"] = 5;
            drdtCertificate["CheckBox"] = "5";
            drdtCertificate["CertificateName"] = "Any other Certificate";
            drdtCertificate["CertificateImage"] = string.Empty;
            dtCertificate.Rows.Add(drdtCertificate);
            gvcertificate.DataSource = dtCertificate;
            gvcertificate.DataBind();
            ViewState["Certificate"] = dtCertificate;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void SetInitialRowqualitycertificate()
    {
        try
        {
            DataTable dtQCertificate = new DataTable();
            DataRow drQdtCertificate = null;
            dtQCertificate.Columns.Add(new DataColumn("SNo", typeof(string)));
            dtQCertificate.Columns.Add(new DataColumn("QCheckBox", typeof(string)));
            dtQCertificate.Columns.Add(new DataColumn("QCertificateName", typeof(string)));
            dtQCertificate.Columns.Add(new DataColumn("QCertificateImage", typeof(string)));
            drQdtCertificate = dtQCertificate.NewRow();
            drQdtCertificate["SNo"] = 1;
            drQdtCertificate["QCheckBox"] = "1";
            drQdtCertificate["QCertificateName"] = "IMS";
            drQdtCertificate["QCertificateImage"] = string.Empty;
            dtQCertificate.Rows.Add(drQdtCertificate);
            drQdtCertificate = dtQCertificate.NewRow();
            drQdtCertificate["SNo"] = 2;
            drQdtCertificate["QCheckBox"] = "2";
            drQdtCertificate["QCertificateName"] = "EnMS";
            drQdtCertificate["QCertificateImage"] = string.Empty;
            dtQCertificate.Rows.Add(drQdtCertificate);
            drQdtCertificate = dtQCertificate.NewRow();
            drQdtCertificate["SNo"] = 3;
            drQdtCertificate["QCheckBox"] = "3";
            drQdtCertificate["QCertificateName"] = "QMS";
            drQdtCertificate["QCertificateImage"] = string.Empty;
            dtQCertificate.Rows.Add(drQdtCertificate);
            drQdtCertificate = dtQCertificate.NewRow();
            drQdtCertificate["SNo"] = 4;
            drQdtCertificate["QCheckBox"] = "4";
            drQdtCertificate["QCertificateName"] = "Any other Certificate";
            drQdtCertificate["QCertificateImage"] = string.Empty;
            dtQCertificate.Rows.Add(drQdtCertificate);
            gvchkqualitycertificate.DataSource = dtQCertificate;
            gvchkqualitycertificate.DataBind();
            ViewState["QCertificate"] = dtQCertificate;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void SaveCodeCertificate()
    {
        try
        {
            DataTable DtSaveCertificate1st = new DataTable();
            DtSaveCertificate1st.Columns.Add(new DataColumn("ImageID", typeof(string)));
            DtSaveCertificate1st.Columns.Add(new DataColumn("CertificateName", typeof(string)));
            DtSaveCertificate1st.Columns.Add(new DataColumn("CertificateImage", typeof(string)));
            DataRow drSaveCertificate1st = null;
            for (int i = 0; gvcertificate.Rows.Count > i; i++)
            {
                CheckBox ChkCheck = (CheckBox)gvcertificate.Rows[i].Cells[1].FindControl("chkcertificate");
                Label lblname = (Label)gvcertificate.Rows[i].Cells[2].FindControl("lblnamecertificate");
                FileUpload fucerti = (FileUpload)gvcertificate.Rows[i].Cells[3].FindControl("fuuploadcertificate");
                if (ChkCheck.Checked == true && fucerti.HasFile != false)
                {
                    drSaveCertificate1st = DtSaveCertificate1st.NewRow();
                    drSaveCertificate1st["ImageID"] = i + 1;
                    drSaveCertificate1st["CertificateName"] = lblname.Text;
                    string FilePathName = Enc.DecryptData(Session["VendorRefNo"].ToString()) + "_" + DateTime.Now.ToString("hh_mm_ss") + fucerti.FileName;
                    fucerti.SaveAs(HttpContext.Current.Server.MapPath("/Upload/VendorImage") + "\\" + FilePathName);
                    drSaveCertificate1st["CertificateImage"] = FilePathName;
                    DtSaveCertificate1st.Rows.Add(drSaveCertificate1st);
                }
            }
            ViewState["SCertificate"] = DtSaveCertificate1st;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void SaveCodeCertificate2()
    {
        try
        {
            DataTable DtSaveCertificate2st = new DataTable();
            DtSaveCertificate2st.Columns.Add(new DataColumn("ImageID", typeof(string)));
            DtSaveCertificate2st.Columns.Add(new DataColumn("CertificateName", typeof(string)));
            DtSaveCertificate2st.Columns.Add(new DataColumn("CertificateImage", typeof(string)));
            DataRow drSaveCertificate2st = null;
            for (int i = 0; gvchkqualitycertificate.Rows.Count > i; i++)
            {
                CheckBox ChkCheckQ = (CheckBox)gvchkqualitycertificate.Rows[i].Cells[1].FindControl("Qchkcertificate");
                Label lblnameQ = (Label)gvchkqualitycertificate.Rows[i].Cells[2].FindControl("lblnameQ");
                FileUpload fucertiQ = (FileUpload)gvchkqualitycertificate.Rows[i].Cells[3].FindControl("fuQuploadcertificate");
                if (ChkCheckQ.Checked == true && fucertiQ.HasFile != false)
                {
                    drSaveCertificate2st = DtSaveCertificate2st.NewRow();
                    drSaveCertificate2st["ImageID"] = i + 1;
                    drSaveCertificate2st["CertificateName"] = lblnameQ.Text;
                    string FilePathName = Enc.DecryptData(Session["VendorRefNo"].ToString()) + "_" + DateTime.Now.ToString("hh_mm_ss") + fucertiQ.FileName;
                    fucertiQ.SaveAs(HttpContext.Current.Server.MapPath("/Upload/VendorImage") + "\\" + FilePathName);
                    drSaveCertificate2st["CertificateImage"] = FilePathName;
                    DtSaveCertificate2st.Rows.Add(drSaveCertificate2st);
                }
            }
            ViewState["SQCertificate"] = DtSaveCertificate2st;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    #endregion
    protected void ddlnabl_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlnabl.SelectedItem.Value == "1")
        { divcertificatevalid.Visible = true; }
        else
        { divcertificatevalid.Visible = false; }
    }
    protected void ddloffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddloffice.SelectedItem.Value == "1")
        { detailofoffcie.Visible = true; }
        else
        { detailofoffcie.Visible = false; }
    }
    protected void ddldistributoraddress_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldistributoraddress.SelectedItem.Text == "Yes")
        { gv3.Visible = true; }
        else
        { gv3.Visible = false; }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        SaveRegistration();
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {

    }
    protected void SaveRegistration()
    {
        if (ViewState["Mid"] != null)
        { HySaveVendorRegisdetail["VendorDetailID"] = ViewState["Mid"]; }
        else
        {
            HySaveVendorRegisdetail["VendorDetailID"] = 0;
        }
        HySaveVendorRegisdetail["VendorRefNo"] = Enc.DecryptData(Session["VendorRefNo"].ToString());
        HySaveVendorRegisdetail["Is_Lab_accredited_by_NABL"] = Co.RSQandSQLInjection(ddlnabl.SelectedItem.Value, "soft");
        if (txtdate.Text != "")
        {
            DateTime datetime = Convert.ToDateTime(txtdate.Text);
            string mdatetime = datetime.ToString("dd/MMM/yyyy");
            HySaveVendorRegisdetail["CertifictionValid"] = Co.RSQandSQLInjection(mdatetime.ToString(), "soft");
        }
        else
        {
            HySaveVendorRegisdetail["CertifictionValid"] = null;
        }
        HySaveVendorRegisdetail["Details_of_R_D_Facilities"] = Co.RSQandSQLInjection(txtmss.Text, "soft");
        HySaveVendorRegisdetail["IsSalesOrMarketOffice"] = Co.RSQandSQLInjection(ddloffice.SelectedItem.Value, "soft");
        if (ddloffice.SelectedValue == "1")
        {
            HySaveVendorRegisdetail["NodelName"] = Co.RSQandSQLInjection(txtlname.Text, "soft");
            HySaveVendorRegisdetail["MarketingOfficeAddress"] = Co.RSQandSQLInjection(txtstreetaddress.Text, "soft");
            HySaveVendorRegisdetail["Line2"] = Co.RSQandSQLInjection(txtstreetaddressline2.Text, "soft");
            HySaveVendorRegisdetail["OfficerCity"] = Co.RSQandSQLInjection(txtcity.Text, "soft");
            HySaveVendorRegisdetail["OfficeState"] = Co.RSQandSQLInjection(txtstate.Text, "soft");
            HySaveVendorRegisdetail["OfficePincode"] = Co.RSQandSQLInjection(txtpincode.Text, "soft");
            HySaveVendorRegisdetail["PhoneNo"] = Co.RSQandSQLInjection(txtcontactno.Text, "soft");
            HySaveVendorRegisdetail["OfficeFaxNo"] = Co.RSQandSQLInjection(txtfaxno.Text, "soft");
            HySaveVendorRegisdetail["OfficeEmail"] = Co.RSQandSQLInjection(txtemail.Text, "soft");
        }
        else
        {
            HySaveVendorRegisdetail["NodelName"] = "";
            HySaveVendorRegisdetail["MarketingOfficeAddress"] = "";
            HySaveVendorRegisdetail["Line2"] = "";
            HySaveVendorRegisdetail["OfficerCity"] = "";
            HySaveVendorRegisdetail["OfficeState"] = "";
            HySaveVendorRegisdetail["OfficePincode"] = "";
            HySaveVendorRegisdetail["PhoneNo"] = "";
            HySaveVendorRegisdetail["OfficeFaxNo"] = "";
            HySaveVendorRegisdetail["OfficeEmail"] = "";
        }
        HySaveVendorRegisdetail["IsAuthorisedDealer"] = Co.RSQandSQLInjection(ddldistributoraddress.SelectedItem.Value, "soft");
        HySaveVendorRegisdetail["FuturePlan"] = Co.RSQandSQLInjection(txtfuture.Text, "soft");
        DataTable DtCer1 = new DataTable();
        DataTable DtCer2 = new DataTable();
        DataTable dtJointVentureFacility = new DataTable();
        DataTable dtOutsourcingFacilites = new DataTable();
        DataTable DtTestFacility = new DataTable();
        DataTable DtDistrubutedealer = new DataTable();
        DataTable DtEmpDetails = new DataTable();
        DataTable DtPlantorMachine = new DataTable();
        DataTable DtAreDetails = new DataTable();
        DataTable DtManufacFacility = new DataTable();
        if (btnsubmit.Text == "Update")
        {
            if (gvmanufacility.Visible == true && gvareadetail.Visible == true && gvmanufacilityedit.Visible == false)
            {
                SaveCodeForManufacturingFacilities();
                DtManufacFacility = (DataTable)ViewState["MF"];
                SaveCodeForPArea();
                DtAreDetails = (DataTable)ViewState["Area"];
                SaveCodeForPlantM();
                DtPlantorMachine = (DataTable)ViewState["PlantM"];
                SaveCodeForEMPCISave();
                DtEmpDetails = (DataTable)ViewState["EMP"];
                SaveCodeForAUSave();
                DtDistrubutedealer = (DataTable)ViewState["DAuth"];
                SaveCodeForTestFaciliSave();
                DtTestFacility = (DataTable)ViewState["TestFaci"];
                SaveCodeForJVSave();
                dtOutsourcingFacilites = (DataTable)ViewState["JVF"];
                SaveCodeForof1Save();
                dtJointVentureFacility = (DataTable)ViewState["OF"];
                SaveCodeCertificate();
                DtCer1 = (DataTable)ViewState["SCertificate"];
                SaveCodeCertificate2();
                DtCer2 = (DataTable)ViewState["SQCertificate"];
            }
            else
            {
            }
        }
        else
        {
            SaveCodeForManufacturingFacilities();
            DtManufacFacility = (DataTable)ViewState["MF"];
            SaveCodeForPArea();
            DtAreDetails = (DataTable)ViewState["Area"];
            SaveCodeForPlantM();
            DtPlantorMachine = (DataTable)ViewState["PlantM"];
            SaveCodeForEMPCISave();
            DtEmpDetails = (DataTable)ViewState["EMP"];
            SaveCodeForAUSave();
            DtDistrubutedealer = (DataTable)ViewState["DAuth"];
            SaveCodeForTestFaciliSave();
            DtTestFacility = (DataTable)ViewState["TestFaci"];
            SaveCodeForJVSave();
            dtOutsourcingFacilites = (DataTable)ViewState["JVF"];
            SaveCodeForof1Save();
            dtJointVentureFacility = (DataTable)ViewState["OF"];
            SaveCodeCertificate();
            DtCer1 = (DataTable)ViewState["SCertificate"];
            SaveCodeCertificate2();
            DtCer2 = (DataTable)ViewState["SQCertificate"];
        }
        string str = Lo.SaveVendorCompanyInfo(HySaveVendorRegisdetail, DtManufacFacility, DtAreDetails, DtPlantorMachine, DtEmpDetails, DtTestFacility, DtDistrubutedealer, dtOutsourcingFacilites, dtJointVentureFacility, DtCer1, DtCer2, out _sysMsg, out _msg);
        if (str != "")
        {
            if (btnsubmit.Text == "Update")
            {
                cleartext();
                LoadCode();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Successfully update company information')", true);
            }
            else
            {
                cleartext();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Successfully save company information')", true);
            }
        }
        else
        { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not saved.')", true); }
    }
    protected void cleartext()
    {
        ViewState["EnterNameof"] = null;
        ViewState["ProductsDetails"] = null;
        ViewState["TechnologyDetails"] = null;
        ViewState["RawMeterialDetails"] = null;
        ViewState["ProdSupp"] = null;
        ViewState["ProdSupp1"] = null;
    }
    protected void gvmanufacilityedit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "addnewmfe")
        {
            btnupdate.Text = "Submit";
            txtnameoffactorypopup.Text = "";
            txtfactorygstnopopup.Text = "";
            txtcompletepostaladdresspopup.Text = "";
            txtcontactofficialnamepopup.Text = "";
            txttelephonepopup.Text = "";
            txtfaxnopopup.Text = "";
            txtemailidpopup.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "changePass", "showPopup0();", true);
        }
        else if (e.CommandName == "updatenewmfe")
        {
            btnupdate.Text = "Edit & Update";
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvmanufacilityedit.Rows[rowIndex];
            HiddenField hfn = (HiddenField)gvmanufacilityedit.Rows[rowIndex].FindControl("hfmanuid");
            txtnameoffactorypopup.Text = row.Cells[1].Text;
            txtfactorygstnopopup.Text = row.Cells[2].Text;
            txtcompletepostaladdresspopup.Text = row.Cells[3].Text;
            txtcontactofficialnamepopup.Text = row.Cells[4].Text;
            txttelephonepopup.Text = row.Cells[5].Text;
            txtfaxnopopup.Text = row.Cells[6].Text;
            txtemailidpopup.Text = row.Cells[7].Text;
            ViewState["editmanu"] = hfn.Value;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "changePass", "showPopup0();", true);
        }
        else if (e.CommandName == "deletenewmfe")
        {
            Int32 Delid = Lo.DeleteEditGrid(Convert.ToInt32(e.CommandArgument.ToString()));
            if (Delid != 0)
            {
                // BindUpdatePopupCode();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record deleted successfull.')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not deleted.')", true);
            }
        }
    }
    protected void gvareadetailedit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "addnewad")
        {
            btnareadetails.Text = "Submit";
            txtnameoffactoryareadetailpopup.Text = "";
            txtproductionareapopup.Text = "";
            txtinspectionarea.Text = "";
            txttotalcoverdareapopup.Text = "";
            txttotalarea.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divareadetail", "showPopup1();", true);
        }
        else if (e.CommandName == "updatenewad")
        {
            btnareadetails.Text = "Edit & Update";
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvareadetailedit.Rows[rowIndex];
            HiddenField hfad = (HiddenField)gvareadetailedit.Rows[rowIndex].FindControl("hfareadetailedit");
            txtnameoffactoryareadetailpopup.Text = row.Cells[1].Text;
            txtproductionareapopup.Text = row.Cells[2].Text;
            txtinspectionarea.Text = row.Cells[3].Text;
            txttotalcoverdareapopup.Text = row.Cells[4].Text;
            txttotalarea.Text = row.Cells[5].Text;
            ViewState["editareadetail"] = hfad.Value;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divareadetail", "showPopup1();", true);
        }
        else if (e.CommandName == "deletenewad")
        {
            Int32 Delid = Lo.DeleteEditGrid(Convert.ToInt32(e.CommandArgument.ToString()));
            if (Delid != 0)
            {
                // BindUpdatePopupCode();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record deleted successfull.')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not deleted.')", true);
            }
        }

    }
    protected void gvplantandmachinesedit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "addnewplantandmachinesedit")
        {
            lballpalntormachine.Text = "Submit";
            txtmachineormodalpopup.Text = "";
            txtMakepopup.Text = "";
            txtQuantitypopup.Text = "";
            txtdateofpurchasepopup.Text = "";
            txtUsagepopup.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "allplantormachine", "showPopup2();", true);
        }
        else if (e.CommandName == "updatenewplantandmachinesedit")
        {
            lballpalntormachine.Text = "Edit & Update";
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvplantandmachinesedit.Rows[rowIndex];
            HiddenField hfpm = (HiddenField)gvplantandmachinesedit.Rows[rowIndex].FindControl("hfplantmachineedit");
            txtmachineormodalpopup.Text = row.Cells[1].Text;
            txtMakepopup.Text = row.Cells[2].Text;
            txtQuantitypopup.Text = row.Cells[3].Text;
            txtdateofpurchasepopup.Text = row.Cells[4].Text;
            txtUsagepopup.Text = row.Cells[5].Text;
            ViewState["editpm"] = hfpm.Value;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "allplantormachine", "showPopup2();", true);
        }
        else if (e.CommandName == "deletenewplantandmachinesedit")
        {
            Int32 Delid = Lo.DeleteEditGrid(Convert.ToInt32(e.CommandArgument.ToString()));
            if (Delid != 0)
            {
                // BindUpdatePopupCode();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record deleted successfull.')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not deleted.')", true);
            }
        }

    }
    protected void gvempCompInfoedit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "addnewempCompInfoedit")
        {
            lbemployee.Text = "Submit";
            txttotalemployeepopup.Text = "";
            txtADMINISTRATIVEpopup.Text = "";
            txtTECHNICALpopup.Text = "";
            txtnontechpopup.Text = "";
            Textxtqcinspopup.Text = "";
            txtskilledlabour.Text = "";
            txtUNSKILLEDLABOURpopup.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divemployee", "showPopup3();", true);
        }
        else if (e.CommandName == "updatenewempCompInfoedit")
        {
            lbemployee.Text = "Edit & Update";
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvempCompInfoedit.Rows[rowIndex];
            HiddenField hfci = (HiddenField)gvempCompInfoedit.Rows[rowIndex].FindControl("hfcompinfo");
            txttotalemployeepopup.Text = row.Cells[1].Text;
            txtADMINISTRATIVEpopup.Text = row.Cells[2].Text;
            txtTECHNICALpopup.Text = row.Cells[3].Text;
            txtnontechpopup.Text = row.Cells[4].Text;
            Textxtqcinspopup.Text = row.Cells[5].Text;
            txtskilledlabour.Text = row.Cells[6].Text;
            txtUNSKILLEDLABOURpopup.Text = row.Cells[7].Text;
            ViewState["editcompinfo"] = hfci.Value;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divemployee", "showPopup3();", true);
        }
        else if (e.CommandName == "deletenewempCompInfoedit")
        {
            Int32 Delid = Lo.DeleteEditGrid(Convert.ToInt32(e.CommandArgument.ToString()));
            if (Delid != 0)
            {
                // BindUpdatePopupCode();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record deleted successfull.')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not deleted.')", true);
            }
        }

    }
    protected void gvcertificateedit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "addnewecertificateedit")
        {
            lbcertificateadd.Text = "Submit";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divCertificate1", "showPopup8();", true);
        }
        else if (e.CommandName == "updatenewcertificateedit")
        {
            lbcertificateadd.Text = "Edit & Update";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divCertificate1", "showPopup8();", true);
        }
        else if (e.CommandName == "deletenewcertificateedit")
        {
            Int32 Delid = Lo.DeleteEditGrid1(Convert.ToInt32(e.CommandArgument.ToString()));
            if (Delid != 0)
            {
                // BindUpdatePopupCode();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record deleted successfull.')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not deleted.')", true);
            }
        }

    }
    protected void gvtestfacilitiesedit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "addnewtestfacilitiesedit")
        {
            btnupdate.Text = "Submit";
            txttestequippopup.Text = "";
            txtmakefacpopup.Text = "";
            txtlcountpopup.Text = "";
            txtrngmeasurpopup.Text = "";
            txtcertiyearpopup.Text = "";
            txtyrofpurpopup.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "changePass", "showPopup4();", true);
        }
        else if (e.CommandName == "updatenewtestfacilitiesedit")
        {
            btnupdate.Text = "Edit & Update";
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvempCompInfoedit.Rows[rowIndex];
            HiddenField hfcer = (HiddenField)gvempCompInfoedit.Rows[rowIndex].FindControl("hftestfacilities");
            txttestequippopup.Text = row.Cells[1].Text;
            txtmakefacpopup.Text = row.Cells[2].Text;
            txtlcountpopup.Text = row.Cells[3].Text;
            txtrngmeasurpopup.Text = row.Cells[4].Text;
            txtcertiyearpopup.Text = row.Cells[5].Text;
            txtyrofpurpopup.Text = row.Cells[6].Text;
            ViewState["editcerti"] = hfcer.Value;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "changePass", "showPopup4();", true);
        }
        else if (e.CommandName == "deletenewtestfacilitiesedit")
        {
            Int32 Delid = Lo.DeleteEditGrid(Convert.ToInt32(e.CommandArgument.ToString()));
            if (Delid != 0)
            {
                // BindUpdatePopupCode();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record deleted successfull.')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not deleted.')", true);
            }
        }

    }
    protected void gvchkqualitycertificateedit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "addnewechkqualitycertificateedit")
        {
            lbcertificate1.Text = "Submit";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divCertificate2", "showPopup9();", true);
        }
        else if (e.CommandName == "updatenewchkqualitycertificateedit")
        {
            lbcertificate1.Text = "Edit & Update";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divCertificate2", "showPopup9();", true);
        }
        else if (e.CommandName == "deletenewchkqualitycertificateedit")
        {
            Int32 Delid = Lo.DeleteEditGrid1(Convert.ToInt32(e.CommandArgument.ToString()));
            if (Delid != 0)
            {
                // BindUpdatePopupCode();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record deleted successfull.')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not deleted.')", true);
            }
        }

    }
    protected void gvauthdealaddressedit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "addnewauthdealaddressedit")
        {
            lbldistri.Text = "Submit";
            txtdisname.Text = "";
            txtstreetaddpopup.Text = "";
            txtstatepopup.Text = "";
            txtPinCodepopup.Text = "";
            txtphonepopup.Text = "";
            txtFaxpopup.Text = "";
            txtemailpopup.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divautdisdeal", "showPopup5();", true);
        }
        else if (e.CommandName == "updatenewauthdealaddressedit")
        {
            lbldistri.Text = "Edit & Update";
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvauthdealaddressedit.Rows[rowIndex];
            HiddenField hfcer = (HiddenField)gvauthdealaddressedit.Rows[rowIndex].FindControl("hfauth");
            txtdisname.Text = row.Cells[1].Text;
            txtstreetaddpopup.Text = row.Cells[2].Text;
            txtstatepopup.Text = row.Cells[3].Text;
            txtPinCodepopup.Text = row.Cells[4].Text;
            txtphonepopup.Text = row.Cells[5].Text;
            txtFaxpopup.Text = row.Cells[6].Text;
            txtemailpopup.Text = row.Cells[7].Text;
            ViewState["editauthdeal"] = hfcer.Value;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divautdisdeal", "showPopup5();", true);
        }
        else if (e.CommandName == "deletenewauthdealaddressedit")
        {
            Int32 Delid = Lo.DeleteEditGrid(Convert.ToInt32(e.CommandArgument.ToString()));
            if (Delid != 0)
            {
                // BindUpdatePopupCode();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record deleted successfull.')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not deleted.')", true);
            }
        }

    }
    protected void gvoutsourcefacilityedit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "addnewoutsourcefacilityedit")
        {
            lboutfac.Text = "Submit";
            txtmainequippopup.Text = "";
            txttestpopup.Text = "";
            txtprocfacli.Text = "";
            txtnameaddpopup.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divof", "showPopup6();", true);
        }
        else if (e.CommandName == "updatenewoutsourcefacilityedit")
        {
            lboutfac.Text = "Edit & Update";
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvoutsourcefacilityedit.Rows[rowIndex];
            HiddenField hfcer = (HiddenField)gvoutsourcefacilityedit.Rows[rowIndex].FindControl("hfoutscr");
            txtmainequippopup.Text = row.Cells[1].Text;
            txttestpopup.Text = row.Cells[2].Text;
            txtprocfacli.Text = row.Cells[3].Text;
            txtnameaddpopup.Text = row.Cells[4].Text;
            ViewState["editoutscr"] = hfcer.Value;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divof", "showPopup6();", true);
        }
        else if (e.CommandName == "deletenewoutsourcefacilityedit")
        {
            Int32 Delid = Lo.DeleteEditGrid(Convert.ToInt32(e.CommandArgument.ToString()));
            if (Delid != 0)
            {
                // BindUpdatePopupCode();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record deleted successfull.')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not deleted.')", true);
            }
        }

    }
    protected void gvjointventureedit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "addnewjointventureedit")
        {
            lbkjoinven.Text = "Submit";
            txtnamejvpop.Text = "";
            ddljointvennature.SelectedIndex = -1;
            txtcomaddpopup.Text = "";
            txtconoffnamepopup.Text = "";
            txttelenopopup.Text = "";
            txtfaxjvpopup.Text = "";
            txtemailpopupjv.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divjvfaci", "showPopup7();", true);
        }
        else if (e.CommandName == "updatenewjointventureedit")
        {
            lbkjoinven.Text = "Edit & Update";
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvjointventureedit.Rows[rowIndex];
            HiddenField hfcer = (HiddenField)gvjointventureedit.Rows[rowIndex].FindControl("hfjoint");
            txtmainequippopup.Text = row.Cells[1].Text;
            txttestpopup.Text = row.Cells[2].Text;
            txtprocfacli.Text = row.Cells[3].Text;
            txtnameaddpopup.Text = row.Cells[4].Text;
            txtnameaddpopup.Text = row.Cells[5].Text;
            txtnameaddpopup.Text = row.Cells[6].Text;
            ViewState["editjoint"] = hfcer.Value;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divjvfaci", "showPopup7();", true);
        }
        else if (e.CommandName == "deletenewjointventureedit")
        {
            Int32 Delid = Lo.DeleteEditGrid(Convert.ToInt32(e.CommandArgument.ToString()));
            if (Delid != 0)
            {
                // BindUpdatePopupCode();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record deleted successfull.')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not deleted.')", true);
            }
        }

    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnupdate.Text == "Edit & Update")
            {
                if (txtnameoffactorypopup.Text != "")
                {
                    Int32 ESaveID = Lo.UpdateMaufacfacility(Convert.ToInt64(ViewState["editmanu"]), Enc.DecryptData(Session["VendorRefNo"].ToString()), txtnameoffactorypopup.Text, txtfactorygstnopopup.Text, txtcompletepostaladdresspopup.Text, txtcontactofficialnamepopup.Text, txttelephonepopup.Text, txtfaxnopopup.Text, txtemailidpopup.Text);
                    if (ESaveID != 0)
                    {
                        txtnameoffactorypopup.Text = "";
                        txtfactorygstnopopup.Text = "";
                        txtcompletepostaladdresspopup.Text = "";
                        txtcontactofficialnamepopup.Text = "";
                        txttelephonepopup.Text = "";
                        txtfaxnopopup.Text = "";
                        txtemailidpopup.Text = "";
                        ViewState["editmanu"] = null;
                        LoadCode();
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
                Int32 ESaveID = Lo.InsertMaufacfacility(Enc.DecryptData(Session["VendorRefNo"].ToString()), "CompInfo", txtnameoffactorypopup.Text, txtfactorygstnopopup.Text, txtcompletepostaladdresspopup.Text, txtcontactofficialnamepopup.Text, txttelephonepopup.Text, txtfaxnopopup.Text, txtemailidpopup.Text);
                if (ESaveID != 0)
                {
                    txtnameoffactorypopup.Text = "";
                    txtfactorygstnopopup.Text = "";
                    txtcompletepostaladdresspopup.Text = "";
                    txtcontactofficialnamepopup.Text = "";
                    txttelephonepopup.Text = "";
                    txtfaxnopopup.Text = "";
                    txtemailidpopup.Text = "";
                    LoadCode();           
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
    protected void btnareadetails_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnareadetails.Text == "Edit & Update")
            {
                if (txtnameoffactoryareadetailpopup.Text != "")
                {
                    Int32 ESaveID = Lo.UpdateArea(Convert.ToInt64(ViewState["editareadetail"]), Enc.DecryptData(Session["VendorRefNo"].ToString()), txtnameoffactoryareadetailpopup.Text, txtproductionareapopup.Text, txtinspectionarea.Text, txttotalcoverdareapopup.Text, txttotalarea.Text);
                    if (ESaveID != 0)
                    {
                        txtnameoffactoryareadetailpopup.Text = "";
                        txtproductionareapopup.Text = "";
                        txtinspectionarea.Text = "";
                        txttotalcoverdareapopup.Text = "";
                        txttotalarea.Text = "";
                        ViewState["editareadetail"] = null;
                        LoadCode();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record update successfully')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not update successfully')", true);
                    }
                }
            }
            else if (btnareadetails.Text == "Submit")
            {
                Int32 ESaveID = Lo.InsertArea(Enc.DecryptData(Session["VendorRefNo"].ToString()), "AreaDetail", txtnameoffactoryareadetailpopup.Text, txtproductionareapopup.Text, txtinspectionarea.Text, txttotalcoverdareapopup.Text, txttotalarea.Text);
                if (ESaveID != 0)
                {
                    txtnameoffactoryareadetailpopup.Text = "";
                    txtproductionareapopup.Text = "";
                    txtinspectionarea.Text = "";
                    txttotalcoverdareapopup.Text = "";
                    txttotalarea.Text = "";
                    LoadCode();            
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
    protected void lballpalntormachine_Click(object sender, EventArgs e)
    {
        try
        {
            if (lballpalntormachine.Text == "Edit & Update")
            {
                if (txtmachineormodalpopup.Text != "")
                {
                    Int32 ESaveID = Lo.UpdatePlantMachine(Convert.ToInt64(ViewState["editpm"]), Enc.DecryptData(Session["VendorRefNo"].ToString()), txtmachineormodalpopup.Text, txtMakepopup.Text, txtQuantitypopup.Text, txtdateofpurchasepopup.Text, txtUsagepopup.Text);
                    if (ESaveID != 0)
                    {
                        txtnameoffactorypopup.Text = "";
                        txtfactorygstnopopup.Text = "";
                        txtcompletepostaladdresspopup.Text = "";
                        txtcontactofficialnamepopup.Text = "";
                        txttelephonepopup.Text = "";
                        txtfaxnopopup.Text = "";
                        txtemailidpopup.Text = "";
                        ViewState["editpm"] = null;
                        LoadCode();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record update successfully')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not update successfully')", true);
                    }
                }
            }
            else if (lballpalntormachine.Text == "Submit")
            {
                Int32 ESaveID = Lo.InsertPlantMachine(Enc.DecryptData(Session["VendorRefNo"].ToString()), "AllPlantOrMachine", txtmachineormodalpopup.Text, txtMakepopup.Text, txtQuantitypopup.Text, txtdateofpurchasepopup.Text, txtUsagepopup.Text);
                if (ESaveID != 0)
                {
                    txtnameoffactorypopup.Text = "";
                    txtfactorygstnopopup.Text = "";
                    txtcompletepostaladdresspopup.Text = "";
                    txtcontactofficialnamepopup.Text = "";
                    txttelephonepopup.Text = "";
                    txtfaxnopopup.Text = "";
                    txtemailidpopup.Text = "";
                    LoadCode();              
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
    protected void lbemployee_Click(object sender, EventArgs e)
    {
        try
        {
            if (lbemployee.Text == "Edit & Update")
            {
                if (txttotalemployeepopup.Text != "")
                {
                    Int32 ESaveID = Lo.UpdateCust(Convert.ToInt64(ViewState["editcompinfo"]), Enc.DecryptData(Session["VendorRefNo"].ToString()), txttotalemployeepopup.Text, txtADMINISTRATIVEpopup.Text, txtTECHNICALpopup.Text, txtnontechpopup.Text, Textxtqcinspopup.Text, txtskilledlabour.Text, txtUNSKILLEDLABOURpopup.Text);
                    if (ESaveID != 0)
                    {
                        txtnameoffactorypopup.Text = "";
                        txtfactorygstnopopup.Text = "";
                        txtcompletepostaladdresspopup.Text = "";
                        txtcontactofficialnamepopup.Text = "";
                        txttelephonepopup.Text = "";
                        txtfaxnopopup.Text = "";
                        txtemailidpopup.Text = "";
                        ViewState["editcompinfo"] = null;
                        LoadCode();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record update successfully')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not update successfully')", true);
                    }
                }
            }
            else if (lbemployee.Text == "Submit")
            {
                Int32 ESaveID = Lo.InsertCust(Enc.DecryptData(Session["VendorRefNo"].ToString()), "Employeedetail", txttotalemployeepopup.Text, txtADMINISTRATIVEpopup.Text, txtTECHNICALpopup.Text, txtnontechpopup.Text, Textxtqcinspopup.Text, txtskilledlabour.Text, txtUNSKILLEDLABOURpopup.Text);
                if (ESaveID != 0)
                {
                    txtnameoffactorypopup.Text = "";
                    txtfactorygstnopopup.Text = "";
                    txtcompletepostaladdresspopup.Text = "";
                    txtcontactofficialnamepopup.Text = "";
                    txttelephonepopup.Text = "";
                    txtfaxnopopup.Text = "";
                    txtemailidpopup.Text = "";
                    LoadCode();             
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
    protected void lbQCF_Click(object sender, EventArgs e)
    {
        try
        {
            if (lbQCF.Text == "Edit & Update")
            {
                if (txttestequippopup.Text != "")
                {
                    Int32 ESaveID = Lo.Updatetestfacilities(Convert.ToInt64(ViewState["editcerti"]), Enc.DecryptData(Session["VendorRefNo"].ToString()), txttestequippopup.Text, txtmakefacpopup.Text, txtlcountpopup.Text, txtrngmeasurpopup.Text, txtcertiyearpopup.Text, txtyrofpurpopup.Text);
                    if (ESaveID != 0)
                    {
                        txtnameoffactorypopup.Text = "";
                        txtfactorygstnopopup.Text = "";
                        txtcompletepostaladdresspopup.Text = "";
                        txtcontactofficialnamepopup.Text = "";
                        txttelephonepopup.Text = "";
                        txtfaxnopopup.Text = "";
                        txtemailidpopup.Text = "";
                        ViewState["editcerti"] = null;
                        LoadCode();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record update successfully')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not update successfully')", true);
                    }
                }
            }
            else if (lbQCF.Text == "Submit")
            {
                Int32 ESaveID = Lo.Inserttestfacilities(Enc.DecryptData(Session["VendorRefNo"].ToString()), "TestEquipment", txttestequippopup.Text, txtmakefacpopup.Text, txtlcountpopup.Text, txtrngmeasurpopup.Text, txtcertiyearpopup.Text, txtyrofpurpopup.Text);
                if (ESaveID != 0)
                {
                    txtnameoffactorypopup.Text = "";
                    txtfactorygstnopopup.Text = "";
                    txtcompletepostaladdresspopup.Text = "";
                    txtcontactofficialnamepopup.Text = "";
                    txttelephonepopup.Text = "";
                    txtfaxnopopup.Text = "";
                    txtemailidpopup.Text = "";
                    LoadCode();         
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
    protected void lbldistri_Click(object sender, EventArgs e)
    {
        try
        {
            if (lbldistri.Text == "Edit & Update")
            {
                if (txtstreetaddpopup.Text != "")
                {
                    Int32 ESaveID = Lo.Updateauthdealaddress(Convert.ToInt64(ViewState["editauthdeal"]), Enc.DecryptData(Session["VendorRefNo"].ToString()), txtstreetaddpopup.Text, txtstatepopup.Text, txtPinCodepopup.Text, txtphonepopup.Text, txtFaxpopup.Text, txtemailpopup.Text,txtdisname.Text);
                    if (ESaveID != 0)
                    {
                        txtnameoffactorypopup.Text = "";
                        txtfactorygstnopopup.Text = "";
                        txtcompletepostaladdresspopup.Text = "";
                        txtcontactofficialnamepopup.Text = "";
                        txttelephonepopup.Text = "";
                        txtfaxnopopup.Text = "";
                        txtemailidpopup.Text = "";
                        ViewState["editauthdeal"] = null;
                        LoadCode();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record update successfully')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not update successfully')", true);
                    }
                }
            }
            else if (lbldistri.Text == "Submit")
            {
                Int32 ESaveID = Lo.Insertauthdealaddress(Enc.DecryptData(Session["VendorRefNo"].ToString()), "Distributer", txtstreetaddpopup.Text, txtstatepopup.Text, txtPinCodepopup.Text, txtphonepopup.Text, txtFaxpopup.Text, txtemailpopup.Text, txtdisname.Text);
                if (ESaveID != 0)
                {
                    txtnameoffactorypopup.Text = "";
                    txtfactorygstnopopup.Text = "";
                    txtcompletepostaladdresspopup.Text = "";
                    txtcontactofficialnamepopup.Text = "";
                    txttelephonepopup.Text = "";
                    txtfaxnopopup.Text = "";
                    txtemailidpopup.Text = "";
                    LoadCode();
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
    protected void lboutfac_Click(object sender, EventArgs e)
    {
        try
        {
            if (lboutfac.Text == "Edit & Update")
            {
                if (txtmainequippopup.Text != "")
                {
                    Int32 ESaveID = Lo.Updateoutsourcefacility(Convert.ToInt64(ViewState["editoutscr"]), Enc.DecryptData(Session["VendorRefNo"].ToString()), txtmainequippopup.Text, txttestpopup.Text, txtprocfacli.Text, txtnameaddpopup.Text);
                    if (ESaveID != 0)
                    {
                        txtnameoffactorypopup.Text = "";
                        txtfactorygstnopopup.Text = "";
                        txtcompletepostaladdresspopup.Text = "";
                        txtcontactofficialnamepopup.Text = "";
                        txttelephonepopup.Text = "";
                        txtfaxnopopup.Text = "";
                        txtemailidpopup.Text = "";
                        ViewState["editoutscr"] = null;
                        LoadCode();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record update successfully')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not update successfully')", true);
                    }
                }
            }
            else if (lboutfac.Text == "Submit")
            {
                Int32 ESaveID = Lo.Insertoutsourcefacility(Enc.DecryptData(Session["VendorRefNo"].ToString()), "JointVentureFacility", txtmainequippopup.Text, txttestpopup.Text, txtprocfacli.Text, txtnameaddpopup.Text);
                if (ESaveID != 0)
                {
                    txtnameoffactorypopup.Text = "";
                    txtfactorygstnopopup.Text = "";
                    txtcompletepostaladdresspopup.Text = "";
                    txtcontactofficialnamepopup.Text = "";
                    txttelephonepopup.Text = "";
                    txtfaxnopopup.Text = "";
                    txtemailidpopup.Text = "";
                    LoadCode();            
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
    protected void lbkjoinven_Click(object sender, EventArgs e)
    {
        try
        {
            if (lbkjoinven.Text == "Edit & Update")
            {
                if (txtnamejvpop.Text != "")
                {
                    Int32 ESaveID = Lo.Updatejointventure(Convert.ToInt64(ViewState["editjoint"]), Enc.DecryptData(Session["VendorRefNo"].ToString()), txtnamejvpop.Text, ddljointvennature.SelectedItem.Text, txtcomaddpopup.Text, txtconoffnamepopup.Text, txttelenopopup.Text, txtfaxjvpopup.Text, txtemailpopupjv.Text);
                    if (ESaveID != 0)
                    {
                        txtnameoffactorypopup.Text = "";
                        txtfactorygstnopopup.Text = "";
                        txtcompletepostaladdresspopup.Text = "";
                        txtcontactofficialnamepopup.Text = "";
                        txttelephonepopup.Text = "";
                        txtfaxnopopup.Text = "";
                        txtemailidpopup.Text = "";
                        ViewState["editjoint"] = null;
                        LoadCode();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record update successfully')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not update successfully')", true);
                    }
                }
            }
            else if (lbkjoinven.Text == "Submit")
            {
                Int32 ESaveID = Lo.Insertjointventure(Enc.DecryptData(Session["VendorRefNo"].ToString()), "Outsourcing", txtnamejvpop.Text, ddljointvennature.SelectedItem.Text, txtcomaddpopup.Text, txtconoffnamepopup.Text, txttelenopopup.Text, txtfaxjvpopup.Text, txtemailpopupjv.Text);
                if (ESaveID != 0)
                {
                    txtnameoffactorypopup.Text = "";
                    txtfactorygstnopopup.Text = "";
                    txtcompletepostaladdresspopup.Text = "";
                    txtcontactofficialnamepopup.Text = "";
                    txttelephonepopup.Text = "";
                    txtfaxnopopup.Text = "";
                    txtemailidpopup.Text = "";
                    LoadCode();  
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
    protected void lbcertificateadd_Click(object sender, EventArgs e)
    {
        if (lbkjoinven.Text == "Submit")
        {
            string FilePathName = Enc.DecryptData(Session["VendorRefNo"].ToString()) + "_" + DateTime.Now.ToString("hh_mm_ss") + fucer.FileName;
            fucer.SaveAs(HttpContext.Current.Server.MapPath("Upload/VendorImage") + "\\" + FilePathName);
            Int32 ESaveID = Lo.Insertcerti1(Enc.DecryptData(Session["VendorRefNo"].ToString()), "FCertificate", rbcer.SelectedItem.Text, FilePathName.ToString());
            if (ESaveID != 0)
            {
                LoadCode();   
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Certificate save successfully')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Certificate not save successfully')", true);
            }
        }
    }
    protected void lbcertificate1_Click(object sender, EventArgs e)
    {
        if (lbkjoinven.Text == "Submit")
        {
            string FilePathName = Enc.DecryptData(Session["VendorRefNo"].ToString()) + "_" + DateTime.Now.ToString("hh_mm_ss") + fucertificate1.FileName;
            fucertificate1.SaveAs(HttpContext.Current.Server.MapPath("Upload/VendorImage") + "\\" + FilePathName);
            Int32 ESaveID = Lo.Insertcerti2(Enc.DecryptData(Session["VendorRefNo"].ToString()), "QCertificate", rb1.SelectedItem.Text, FilePathName.ToString());
            if (ESaveID != 0)
            {
                LoadCode();          
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Certificate save successfully')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Certificate not save successfully')", true);
            }
        }
    }
}