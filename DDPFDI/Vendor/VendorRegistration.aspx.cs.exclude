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

public partial class Vendor_VendorRegistration : System.Web.UI.Page
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
                #region PageLoad Grid Functions
                SetInitialRow();
                SetInitialRowProductDetails();
                SetInitialRowTechnologyDetails();
                SetRawmeterialDetails();
                SetInitialRowItemProductorSupplied();
                SetInitialRowItemProductorSupplied1();
                SetInitialRowGovt();
                SetInitialRowTurnOverLast3Years();
                SetInitialRowListofManufacturingFacilities();
                SetInitialRowArea();
                SetInitialRowPM();
                SetInitialRowEMPCI();
                SetInitialRowTestFacili();
                SetInitialRowAuthodel();
                SetInitialRowOF();
                SetInitialRowJVF();
                SetInitialRowOEMNameAddress();
                SetInitialRowEMPCI1();
                SetInitialRowTestFacili1();
                #endregion
                BindTypeOfBusiness();
                Bindbusinesssector();
                #region Registration Date Retrive
                DataTable DtGetRegisVendor = Lo.RetriveVendor(0, Enc.DecryptData(Session["VendorRefNo"].ToString()), "", "RetriveData");
                if (DtGetRegisVendor.Rows.Count > 0)
                {
                    txtbusinessname.Text = DtGetRegisVendor.Rows[0]["BusinessName"].ToString();
                    if (DtGetRegisVendor.Rows[0]["TypeOfBuisness"].ToString() != "")
                    {
                        ddltypeofbusiness.Items.FindByValue(DtGetRegisVendor.Rows[0]["TypeOfBuisness"].ToString()).Selected = true;
                    }
                    ddltypeofbusiness.SelectedItem.Enabled = false;
                    if (DtGetRegisVendor.Rows[0]["BusinessSector"].ToString() != "")
                    {
                        ddlbusinesssector.Items.FindByValue(DtGetRegisVendor.Rows[0]["BusinessSector"].ToString()).Selected = true;
                    }
                    txtstreetaddress.Text = DtGetRegisVendor.Rows[0]["StreetAddress"].ToString();
                    txtstreetaddressline2.Text = DtGetRegisVendor.Rows[0]["StreetAddressLine2"].ToString();
                    txtcity.Text = DtGetRegisVendor.Rows[0]["City"].ToString();
                    txtstateprovince.Text = DtGetRegisVendor.Rows[0]["State"].ToString();
                    txtpostalzipcode.Text = DtGetRegisVendor.Rows[0]["ZipCode"].ToString();
                    txtemail.Text = DtGetRegisVendor.Rows[0]["NodalOfficerEmail"].ToString();
                    txtstdcode.Text = DtGetRegisVendor.Rows[0]["ContactNo"].ToString().Substring(0, 3);
                    string a = DtGetRegisVendor.Rows[0]["ContactNo"].ToString();
                    var result = a.Substring(a.IndexOf('-') + 1);
                    txtphoneno.Text = result.ToString();
                    if (txtfaxstdcode.Text != "")
                    {
                        txtfaxstdcode.Text = DtGetRegisVendor.Rows[0]["FaxNo"].ToString().Substring(0, 3);
                    }
                    string b = DtGetRegisVendor.Rows[0]["FaxNo"].ToString();
                    var result1 = b.Substring(b.IndexOf('-') + 1);
                    if (txtfaxphoneno.Text != "")
                    {
                        txtfaxphoneno.Text = result1.ToString();
                    }
                    txtgstin.Text = DtGetRegisVendor.Rows[0]["GSTNo"].ToString();
                    txtfirstname.Text = DtGetRegisVendor.Rows[0]["NodalOfficerFirstName"].ToString();
                    txtmiddlename.Text = DtGetRegisVendor.Rows[0]["NodalOfficerMiddleName"].ToString();
                    txtlastname.Text = DtGetRegisVendor.Rows[0]["NodalOfficerLastName"].ToString();
                }
                #endregion
            }
        }
        else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert",
                   "alert('Session Expired,Please login again');window.location='VendorLogin'", true);
    }
    #endregion
    #region other
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
    protected void ddltypeofchk_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltypeofchk.SelectedItem.Text != "Select")
        { chklist.Visible = true; }
        else
        { chklist.Visible = false; }
    }
    protected void ddlenternameof_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlenternameof.SelectedItem.Text == "Other")
        //{ divdesignation.Visible = true; }
        //else { divdesignation.Visible = false; }
    }
    protected void rbfinnyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < rbfinnyear.Items.Count; i++)
        {
            if (rbfinnyear.Items[i].Value == "1" && rbfinnyear.Items[i].Selected == true)
            {
                div19.Visible = true;
            }
            else if (rbfinnyear.Items[i].Value == "2" && rbfinnyear.Items[i].Selected == true)
            {
                div18.Visible = true;
            }
            else if (rbfinnyear.Items[i].Value == "3" && rbfinnyear.Items[i].Selected == true)
            {
                div17.Visible = true;
            }
            else
            {
                if (rbfinnyear.Items[i].Value == "1" && rbfinnyear.Items[i].Selected == false)
                { div19.Visible = false; }
                else if (rbfinnyear.Items[i].Value == "2" && rbfinnyear.Items[i].Selected == false)
                { div18.Visible = false; }
                else if (rbfinnyear.Items[i].Value == "3" && rbfinnyear.Items[i].Selected == false)
                { div17.Visible = false; }
            }
        }
    }
    protected void btnprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Printing.....')", true);
    }
    protected void ddltypeofaccount_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltypeofaccount.SelectedItem.Text == "Select")
        { divbankdetail.Visible = false; }
        else
        { divbankdetail.Visible = true; }
    }
    protected void ddldetailofpantan_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldetailofpantan.SelectedItem.Text == "Select")
        { divpantan.Visible = false; }
        else if (ddldetailofpantan.SelectedItem.Text == "PAN")
        { lblpantan.Text = "PAN"; divpantan.Visible = true; }
        else if (ddldetailofpantan.SelectedItem.Text == "TAN")
        { lblpantan.Text = "TAN"; divpantan.Visible = true; }
    }
    protected void ddlDepartmentUndertakingPSU_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartmentUndertakingPSU.SelectedItem.Text == "YES")
        { divgovtundertaking.Visible = true; }
        else
        { divgovtundertaking.Visible = false; }
    }
    protected void ddlnabl_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlnabl.SelectedItem.Value == "1")
        { divcertificatevalid.Visible = true; }
        else
        { divcertificatevalid.Visible = false; }
    }
    protected void ddlregiscategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlregiscategory.SelectedItem.Text == "MANUFACTURER" || ddlregiscategory.SelectedItem.Text == "OEM")
        {
            testcompinfo.Visible = true;
            othercate.Visible = false;
        }
        else if (ddlregiscategory.SelectedItem.Text == "AUTHORISED AGENT" || ddlregiscategory.SelectedItem.Text == "TRADER" || ddlregiscategory.SelectedItem.Text == "Stockist" || ddlregiscategory.SelectedItem.Text == "Distributor")
        {
            othercate.Visible = true;
            testcompinfo.Visible = false;
        }
        else
        {
            testcompinfo.Visible = false;
            othercate.Visible = false;
        }
        ddltypeofchk.SelectedValue = ddlregiscategory.SelectedValue;
        if (ddltypeofchk.SelectedItem.Text != "Select")
        {
             chklist.Visible = true; 
        }
    }
    protected void chkcertificate_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < chkcertificate.Items.Count; i++)
        {
            if (chkcertificate.Items[i].Value == "1" && chkcertificate.Items[i].Selected == true)
            {
                div1certificate.Visible = true;
            }
            else if (chkcertificate.Items[i].Value == "2" && chkcertificate.Items[i].Selected == true)
            {
                div2certificate.Visible = true;
            }
            else if (chkcertificate.Items[i].Value == "3" && chkcertificate.Items[i].Selected == true)
            {
                div3certificate.Visible = true;
            }
            else if (chkcertificate.Items[i].Value == "4" && chkcertificate.Items[i].Selected == true)
            {
                div4certificate.Visible = true;
            }
            else if (chkcertificate.Items[i].Value == "5" && chkcertificate.Items[i].Selected == true)
            {
                div5certificate.Visible = true;
            }
            else
            {
                if (chkcertificate.Items[i].Value == "1" && chkcertificate.Items[i].Selected == false)
                { div1certificate.Visible = false; }
                else if (chkcertificate.Items[i].Value == "2" && chkcertificate.Items[i].Selected == false)
                { div2certificate.Visible = false; }
                else if (chkcertificate.Items[i].Value == "3" && chkcertificate.Items[i].Selected == false)
                { div3certificate.Visible = false; }
                else if (chkcertificate.Items[i].Value == "4" && chkcertificate.Items[i].Selected == false)
                { div4certificate.Visible = false; }
                else if (chkcertificate.Items[i].Value == "5" && chkcertificate.Items[i].Selected == false)
                { div5certificate.Visible = false; }
            }
        }
    }
    protected void chkqualitycertificate_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < chkqualitycertificate.Items.Count; i++)
        {
            if (chkqualitycertificate.Items[i].Value == "2" && chkqualitycertificate.Items[i].Selected == true)
            {
                divqualitycertificate2.Visible = true;
            }
            else if (chkqualitycertificate.Items[i].Value == "3" && chkqualitycertificate.Items[i].Selected == true)
            {
                divqualitycertificate3.Visible = true;
            }
            else if (chkqualitycertificate.Items[i].Value == "4" && chkqualitycertificate.Items[i].Selected == true)
            {
                divqualitycertificate4.Visible = true;
            }
            else
            {
                if (chkqualitycertificate.Items[i].Value == "1" && chkqualitycertificate.Items[i].Selected == false)
                { divqualitycertificate2.Visible = false; }
                else if (chkqualitycertificate.Items[i].Value == "2" && chkqualitycertificate.Items[i].Selected == false)
                { divqualitycertificate3.Visible = false; }
                else if (chkqualitycertificate.Items[i].Value == "3" && chkqualitycertificate.Items[i].Selected == false)
                { divqualitycertificate4.Visible = false; }

            }
        }
    }
    protected void ddloffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddloffice.SelectedItem.Value == "1")
        { detailofoffcie.Visible = true; }
        else
        { detailofoffcie.Visible = false; }
    }
    protected void ddltypeofbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltypeofbusiness.SelectedItem.Text == "MSME")
        { divmsmetypeofbuisness.Visible = true; }
        else if (ddltypeofbusiness.SelectedItem.Text == "Startup")
        {
            lblcin.Text = "Startup Registration No";
        }
        else if (ddltypeofbusiness.SelectedItem.Value == "10")
        {
            divothersdetails.Visible = true;
        }
        else
        {
            divothersdetails.Visible = false;
            divmsmetypeofbuisness.Visible = false;
        }

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
    protected void ddldistributoraddress_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldistributoraddress.SelectedItem.Text == "Yes")
        { gv3.Visible = true; }
        else
        { gv3.Visible = false; }
    }
    protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < CheckBoxList1.Items.Count; i++)
        {
            if (CheckBoxList1.Items[i].Value == "1" && CheckBoxList1.Items[i].Selected == true)
            {
                div2.Visible = true;
            }
            else if (CheckBoxList1.Items[i].Value == "2" && CheckBoxList1.Items[i].Selected == true)
            {
                div3.Visible = true;
            }
            else if (CheckBoxList1.Items[i].Value == "3" && CheckBoxList1.Items[i].Selected == true)
            {
                div4.Visible = true;
            }
            else if (CheckBoxList1.Items[i].Value == "4" && CheckBoxList1.Items[i].Selected == true)
            {
                div5.Visible = true;
            }
            else if (CheckBoxList1.Items[i].Value == "5" && CheckBoxList1.Items[i].Selected == true)
            {
                div6.Visible = true;
            }
            else
            {
                if (CheckBoxList1.Items[i].Value == "1" && CheckBoxList1.Items[i].Selected == false)
                { div2.Visible = false; }
                else if (CheckBoxList1.Items[i].Value == "2" && CheckBoxList1.Items[i].Selected == false)
                { div3.Visible = false; }
                else if (CheckBoxList1.Items[i].Value == "3" && CheckBoxList1.Items[i].Selected == false)
                { div4.Visible = false; }
                else if (CheckBoxList1.Items[i].Value == "4" && CheckBoxList1.Items[i].Selected == false)
                { div5.Visible = false; }
                else if (CheckBoxList1.Items[i].Value == "5" && CheckBoxList1.Items[i].Selected == false)
                { div6.Visible = false; }
            }
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedItem.Value == "1")
        { div7.Visible = true; }
        else
        { div7.Visible = false; }
    }
    protected void CheckBoxList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < CheckBoxList2.Items.Count; i++)
        {
            if (CheckBoxList2.Items[i].Value == "2" && CheckBoxList2.Items[i].Selected == true)
            {
                div8.Visible = true;
            }
            else if (CheckBoxList2.Items[i].Value == "3" && CheckBoxList2.Items[i].Selected == true)
            {
                div9.Visible = true;
            }
            else if (CheckBoxList2.Items[i].Value == "4" && CheckBoxList2.Items[i].Selected == true)
            {
                div10.Visible = true;
            }
            else
            {
                if (CheckBoxList2.Items[i].Value == "1" && CheckBoxList2.Items[i].Selected == false)
                { div8.Visible = false; }
                else if (CheckBoxList2.Items[i].Value == "2" && CheckBoxList2.Items[i].Selected == false)
                { div9.Visible = false; }
                else if (CheckBoxList2.Items[i].Value == "3" && CheckBoxList2.Items[i].Selected == false)
                { div10.Visible = false; }

            }
        }
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddloffice.SelectedItem.Value == "1")
        { Div11.Visible = true; }
        else
        { Div11.Visible = false; }
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
                div1.Visible = true;
            }
            else if (chkcontracts.Items[i].Value == "3" && chkcontracts.Items[i].Selected == true)
            {
                div12.Visible = true;
            }
            else if (chkcontracts.Items[i].Value == "4" && chkcontracts.Items[i].Selected == true)
            {
                div13.Visible = true;
            }
            else
            {
                if (chkcontracts.Items[i].Value == "1" && chkcontracts.Items[i].Selected == false)
                { divfin.Visible = false; }
                else if (chkcontracts.Items[i].Value == "2" && chkcontracts.Items[i].Selected == false)
                { div1.Visible = false; }
                else if (chkcontracts.Items[i].Value == "3" && chkcontracts.Items[i].Selected == false)
                { div12.Visible = false; }
                else if (chkcontracts.Items[i].Value == "4" && chkcontracts.Items[i].Selected == false)
                { div13.Visible = false; }
            }
        }
    }
    protected void ddldebarredgovtcont_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldebarredgovtcont.SelectedItem.Text == "Yes")
        { chkcontracts.Visible = true; }
        else
        { chkcontracts.Visible = false; }
    }
    protected void ddlscaleofbuisness_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlscaleofbuisness.SelectedItem.Text == "Small")
        { cermsme.Visible = true; }
        else
        { cermsme.Visible = false; }
    }
    #endregion
    #region BindAddMore Grid One by One All Grid Code
    //Add Grid of Please Enter Name of Code
    #region Grid of Please Enter Name of Code
    private void SetInitialRow()
    {
        //Create false table
        DataTable dtenternameof = new DataTable();
        DataRow drenternameof = null;
        dtenternameof.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dtenternameof.Columns.Add(new DataColumn("EnterName", typeof(string)));
        dtenternameof.Columns.Add(new DataColumn("Name", typeof(string)));
        dtenternameof.Columns.Add(new DataColumn("Designation", typeof(string)));
        dtenternameof.Columns.Add(new DataColumn("DinNo", typeof(string)));
        dtenternameof.Columns.Add(new DataColumn("MobileNo", typeof(string)));
        drenternameof = dtenternameof.NewRow();
        drenternameof["RowNumber"] = 1;
        drenternameof["EnterName"] = string.Empty;
        drenternameof["Name"] = string.Empty;
        drenternameof["Designation"] = string.Empty;
        drenternameof["DinNo"] = string.Empty;
        drenternameof["MobileNo"] = string.Empty;
        dtenternameof.Rows.Add(drenternameof);
        //Store the DataTable in ViewState or bind or show false grid
        ViewState["EnterNameof"] = dtenternameof;
        gridNameof.DataSource = dtenternameof;
        gridNameof.DataBind();
    }
    private void AddNewRowToGrid()
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
                    //extract the TextBox values
                    DropDownList ddlenternameof = (DropDownList)gridNameof.Rows[i].Cells[1].FindControl("ddlenternameof");
                    TextBox TextBox1 = (TextBox)gridNameof.Rows[i].Cells[2].FindControl("txtEnterNameof");
                    TextBox TextBox2 = (TextBox)gridNameof.Rows[i].Cells[3].FindControl("txtdesignation");
                    TextBox TextBox3 = (TextBox)gridNameof.Rows[i].Cells[4].FindControl("txtdinno");
                    TextBox TextBox4 = (TextBox)gridNameof.Rows[i].Cells[5].FindControl("txtmobno");

                    dtCurrentTableNameCode.Rows[i]["EnterName"] = ddlenternameof.Text;
                    dtCurrentTableNameCode.Rows[i]["Name"] = TextBox1.Text;
                    dtCurrentTableNameCode.Rows[i]["Designation"] = TextBox2.Text;
                    dtCurrentTableNameCode.Rows[i]["DinNo"] = TextBox3.Text;
                    dtCurrentTableNameCode.Rows[i]["MobileNo"] = TextBox4.Text;
                    // rowIndex++;
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
    private void SetPreviousData()
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
    protected void ButtonAddEnterNameof_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }
    protected void gridNameof_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void LinkButton1_Click(object sender, EventArgs e)
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
    private void ResetRowID(DataTable dtremovecount)
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
    //Add Grid of Products Details
    #region  Grid of Products Details
    private void SetInitialRowProductDetails()
    {
        //Create false table
        DataTable dtProdDetail = new DataTable();
        DataRow drProdDetail = null;
        dtProdDetail.Columns.Add(new DataColumn("RowNumberProd", typeof(string)));
        dtProdDetail.Columns.Add(new DataColumn("Nomenclature", typeof(string)));
        dtProdDetail.Columns.Add(new DataColumn("NatoGroup", typeof(string)));
        dtProdDetail.Columns.Add(new DataColumn("NatoClass", typeof(string)));
        dtProdDetail.Columns.Add(new DataColumn("ItemCode", typeof(string)));
        dtProdDetail.Columns.Add(new DataColumn("HSNCode", typeof(string)));
        drProdDetail = dtProdDetail.NewRow();
        drProdDetail["RowNumberProd"] = 1;
        drProdDetail["Nomenclature"] = string.Empty;
        drProdDetail["NatoGroup"] = string.Empty;
        drProdDetail["NatoClass"] = string.Empty;
        drProdDetail["ItemCode"] = string.Empty;
        drProdDetail["HSNCode"] = string.Empty;
        dtProdDetail.Rows.Add(drProdDetail);
        //Store the DataTable in ViewState or bind or show false grid
        ViewState["ProductsDetails"] = dtProdDetail;
        gvproddetail.DataSource = dtProdDetail;
        gvproddetail.DataBind();
        //After binding the gridview, we can then extract and fill the DropDownList with Data 
        DropDownList ddlnatogroup = (DropDownList)gvproddetail.Rows[0].Cells[2].FindControl("ddlnatogroup");
        BindNatoGroup(ddlnatogroup);
    }
    protected void BindNatoGroup(DropDownList DropDownValue)
    {
        DataTable DtDropDownNatoGroup = new DataTable();
        DtDropDownNatoGroup = Lo.RetriveMasterSubCategoryDate(0, "NSN GROUP", "", "SelectProductCat", "", "");
        if (DtDropDownNatoGroup.Rows.Count > 0)
        {
            Co.FillDropdownlist(DropDownValue, DtDropDownNatoGroup, "SCategoryName", "SCategoryID");
            DropDownValue.Items.Insert(0, "Select");
        }
    }
    protected void ddlnatogroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);
        int rowIndex = gvr.RowIndex;
        DropDownList DDLnatoPostBack = gvproddetail.Rows[rowIndex].FindControl("ddlnatogroup") as DropDownList;
        DropDownList ddlnatoclass = gvproddetail.Rows[rowIndex].FindControl("ddlnatoclass") as DropDownList;
        BindNatoClassSubCategoryofNatoGroup(DDLnatoPostBack, ddlnatoclass);
    }
    protected void BindNatoClassSubCategoryofNatoGroup(DropDownList DDLnatoPostBack, DropDownList ddlnatoclass)
    {
        if (DDLnatoPostBack.SelectedItem.Text != "Select")
        {
            DataTable DtNatoclass = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(DDLnatoPostBack.SelectedItem.Value), "", "", "SubSelectID", "", "");
            if (DtNatoclass.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlnatoclass, DtNatoclass, "SCategoryName", "SCategoryId");
                ddlnatoclass.Items.Insert(0, "Select");
            }
            else
            {
                ddlnatoclass.Items.Clear();
                ddlnatoclass.Items.Insert(0, "Select");
            }
        }
    }
    protected void ddlnatoclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);
        int rowIndex = gvr.RowIndex;
        DropDownList DDLNatoClassPostBack = gvproddetail.Rows[rowIndex].FindControl("ddlnatoclass") as DropDownList;
        DropDownList ddlItemCode = gvproddetail.Rows[rowIndex].FindControl("ddlitemcode") as DropDownList;
        BindItemClassSubCategoryofNatoClass(DDLNatoClassPostBack, ddlItemCode);
    }
    protected void BindItemClassSubCategoryofNatoClass(DropDownList DDLNatoClassPostBack, DropDownList ddlItemCode)
    {
        if (DDLNatoClassPostBack.Text != "" && DDLNatoClassPostBack.SelectedItem.Text != "Select")
        {
            DataTable dtItemCode = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(DDLNatoClassPostBack.SelectedItem.Value), "", "", "SubSelectID", "", "");
            if (dtItemCode.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlItemCode, dtItemCode, "SCategoryName", "SCategoryId");
                ddlItemCode.Items.Insert(0, "Select");
            }
            else
            {
                ddlItemCode.Items.Clear();
                ddlItemCode.Items.Insert(0, "Select");
                ddlItemCode.Items.Insert(1, "NA");
            }
        }
    }
    private void AddNewProductDetailGrid()
    {
        int rowIndexProd = 0;
        if (ViewState["ProductsDetails"] != null)
        {
            DataTable dtCurrentTableProd = (DataTable)ViewState["ProductsDetails"];
            DataRow drCurrentRowNameProd = null;
            if (dtCurrentTableProd.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTableProd.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox TextBox1 = (TextBox)gvproddetail.Rows[rowIndexProd].Cells[1].FindControl("txtproductnomen");
                    DropDownList ddl_1 = (DropDownList)gvproddetail.Rows[rowIndexProd].Cells[2].FindControl("ddlnatogroup");
                    DropDownList ddl_2 = (DropDownList)gvproddetail.Rows[rowIndexProd].Cells[3].FindControl("ddlnatoclass");
                    DropDownList ddl_3 = (DropDownList)gvproddetail.Rows[rowIndexProd].Cells[4].FindControl("ddlitemcode");
                    TextBox TextBox2 = (TextBox)gvproddetail.Rows[rowIndexProd].Cells[5].FindControl("txthsnno");
                    drCurrentRowNameProd = dtCurrentTableProd.NewRow();
                    drCurrentRowNameProd["RowNumberProd"] = i + 1;
                    dtCurrentTableProd.Rows[i - 1]["Nomenclature"] = TextBox1.Text;
                    dtCurrentTableProd.Rows[i - 1]["NatoGroup"] = ddl_1.Text;
                    dtCurrentTableProd.Rows[i - 1]["NatoClass"] = ddl_2.Text;
                    dtCurrentTableProd.Rows[i - 1]["ItemCode"] = ddl_3.Text;
                    dtCurrentTableProd.Rows[i - 1]["HSNCode"] = TextBox2.Text;
                    rowIndexProd++;
                }
                dtCurrentTableProd.Rows.Add(drCurrentRowNameProd);
                ViewState["ProductsDetails"] = dtCurrentTableProd;
                gvproddetail.DataSource = dtCurrentTableProd;
                gvproddetail.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousDataProductDetail();
    }
    private void SetPreviousDataProductDetail()
    {
        int rowIndexProd = 0;
        if (ViewState["ProductsDetails"] != null)
        {
            DataTable dtProdPrevious = (DataTable)ViewState["ProductsDetails"];
            if (dtProdPrevious.Rows.Count > 0)
            {
                for (int i = 0; i < dtProdPrevious.Rows.Count; i++)
                {
                    TextBox TB_1 = (TextBox)gvproddetail.Rows[rowIndexProd].Cells[1].FindControl("txtproductnomen");
                    DropDownList DDL_1 = (DropDownList)gvproddetail.Rows[rowIndexProd].Cells[2].FindControl("ddlnatogroup");
                    DropDownList DDL_2 = (DropDownList)gvproddetail.Rows[rowIndexProd].Cells[3].FindControl("ddlnatoclass");
                    DropDownList DDL_3 = (DropDownList)gvproddetail.Rows[rowIndexProd].Cells[4].FindControl("ddlitemcode");
                    TextBox TB_2 = (TextBox)gvproddetail.Rows[rowIndexProd].Cells[5].FindControl("txthsnno");
                    TB_1.Text = dtProdPrevious.Rows[i]["Nomenclature"].ToString();
                    TB_2.Text = dtProdPrevious.Rows[i]["HSNCode"].ToString();
                    BindNatoGroup(DDL_1);
                    if (i < dtProdPrevious.Rows.Count - 1)
                    {
                        DDL_1.ClearSelection();
                        DDL_1.Items.FindByValue(dtProdPrevious.Rows[i]["NatoGroup"].ToString()).Selected = true;
                        DDL_2.ClearSelection();
                        BindNatoClassSubCategoryofNatoGroup(DDL_1, DDL_2);
                        if (DDL_2.Text != "")
                        {
                            DDL_2.Items.FindByValue(dtProdPrevious.Rows[i]["NatoClass"].ToString()).Selected = true;
                        }
                        DDL_3.ClearSelection();
                        BindItemClassSubCategoryofNatoClass(DDL_2, DDL_3);
                        if (DDL_3.Text != "")
                        {
                            DDL_3.Items.FindByValue(dtProdPrevious.Rows[i]["ItemCode"].ToString()).Selected = true;
                        }
                    }
                    rowIndexProd++;
                }
            }
        }
    }
    protected void btnProductDetailAddMore_Click(object sender, EventArgs e)
    {
        AddNewProductDetailGrid();
    }
    protected void gvproddetail_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dtgridProdrowcreated = (DataTable)ViewState["ProductsDetails"];
            LinkButton lbProd = (LinkButton)e.Row.FindControl("lbProductDetail");
            if (lbProd != null)
            {
                if (dtgridProdrowcreated.Rows.Count > 1)
                {
                    if (e.Row.RowIndex == dtgridProdrowcreated.Rows.Count - 1)
                    {
                        lbProd.Visible = false;
                    }
                }
                else
                {
                    lbProd.Visible = false;
                }
            }
        }
    }
    protected void lbProductDetail_Click(object sender, EventArgs e)
    {
        LinkButton lbProd = (LinkButton)sender;
        GridViewRow gvRowProd = (GridViewRow)lbProd.NamingContainer;
        int rowID = gvRowProd.RowIndex;
        if (ViewState["ProductsDetails"] != null)
        {
            DataTable dtremovegridprod = (DataTable)ViewState["ProductsDetails"];
            if (dtremovegridprod.Rows.Count > 1)
            {
                if (gvRowProd.RowIndex < dtremovegridprod.Rows.Count - 1)
                {
                    dtremovegridprod.Rows.Remove(dtremovegridprod.Rows[rowID]);
                    ResetRowIDProductDetail(dtremovegridprod);
                }
            }
            ViewState["ProductsDetails"] = dtremovegridprod;
            gvproddetail.DataSource = dtremovegridprod;
            gvproddetail.DataBind();
        }
        SetPreviousDataProductDetail();
    }
    private void ResetRowIDProductDetail(DataTable dtRemoveProductdetail)
    {
        int rowNumberProd = 1;
        if (dtRemoveProductdetail.Rows.Count > 0)
        {
            foreach (DataRow rowProd in dtRemoveProductdetail.Rows)
            {
                rowProd[0] = rowNumberProd;
                rowNumberProd++;
            }
        }
    }
    DataTable DtSaveProdDetails = new DataTable();
    protected void SaveCodeProdDetails()
    {
        DtSaveProdDetails.Columns.Add(new DataColumn("RowNumberProd", typeof(string)));
        DtSaveProdDetails.Columns.Add(new DataColumn("Nomenclature", typeof(string)));
        DtSaveProdDetails.Columns.Add(new DataColumn("NatoGroup", typeof(string)));
        DtSaveProdDetails.Columns.Add(new DataColumn("NatoClass", typeof(string)));
        DtSaveProdDetails.Columns.Add(new DataColumn("ItemCode", typeof(string)));
        DtSaveProdDetails.Columns.Add(new DataColumn("HSNCode", typeof(string)));
        DataRow drCurrentRowSaveCode = null;
        for (int i = 0; gvproddetail.Rows.Count > i; i++)
        {
            TextBox TB_1 = (TextBox)gvproddetail.Rows[i].Cells[1].FindControl("txtproductnomen");
            DropDownList DDL_1 = (DropDownList)gvproddetail.Rows[i].Cells[2].FindControl("ddlnatogroup");
            DropDownList DDL_2 = (DropDownList)gvproddetail.Rows[i].Cells[3].FindControl("ddlnatoclass");
            DropDownList DDL_3 = (DropDownList)gvproddetail.Rows[i].Cells[4].FindControl("ddlitemcode");
            TextBox TB_2 = (TextBox)gvproddetail.Rows[i].Cells[5].FindControl("txthsnno");
            if (DDL_1.SelectedItem.Text != "Select" && TB_2.Text != "")
            {
                drCurrentRowSaveCode = DtSaveProdDetails.NewRow();
                drCurrentRowSaveCode["RowNumberProd"] = i + 1;
                drCurrentRowSaveCode["Nomenclature"] = TB_1.Text;
                drCurrentRowSaveCode["NatoGroup"] = DDL_1.SelectedItem.Value;
                drCurrentRowSaveCode["NatoClass"] = DDL_2.SelectedItem.Value;
                drCurrentRowSaveCode["ItemCode"] = DDL_3.SelectedItem.Value;
                drCurrentRowSaveCode["HSNCode"] = TB_2.Text;
                DtSaveProdDetails.Rows.Add(drCurrentRowSaveCode);
            }
        }
        ViewState["ProductsDetails"] = DtSaveProdDetails;
    }
    #endregion
    //Add Grid of Technalogy Details
    #region Grid of Technology Details
    private void SetInitialRowTechnologyDetails()
    {
        //Create false table
        DataTable dtTechDetail = new DataTable();
        DataRow drTechDetail = null;
        dtTechDetail.Columns.Add(new DataColumn("RowNumberTech", typeof(string)));
        dtTechDetail.Columns.Add(new DataColumn("TechNomenclature", typeof(string)));
        dtTechDetail.Columns.Add(new DataColumn("Technology1", typeof(string)));
        dtTechDetail.Columns.Add(new DataColumn("Technology2", typeof(string)));
        dtTechDetail.Columns.Add(new DataColumn("Technology3", typeof(string)));
        drTechDetail = dtTechDetail.NewRow();
        drTechDetail["RowNumberTech"] = 1;
        drTechDetail["TechNomenclature"] = string.Empty;
        drTechDetail["Technology1"] = string.Empty;
        drTechDetail["Technology2"] = string.Empty;
        drTechDetail["Technology3"] = string.Empty;
        dtTechDetail.Rows.Add(drTechDetail);
        //Store the DataTable in ViewState or bind or show false grid
        ViewState["TechnologyDetails"] = dtTechDetail;
        gvtechnology.DataSource = dtTechDetail;
        gvtechnology.DataBind();
        //After binding the gridview, we can then extract and fill the DropDownList with Data 
        DropDownList ddltech1 = (DropDownList)gvtechnology.Rows[0].Cells[2].FindControl("ddltech1");
        BindMasterTechnologyCategory(ddltech1);
    }
    protected void BindMasterTechnologyCategory(DropDownList ddltech1)
    {
        DataTable DtTechDrop = new DataTable();
        DtTechDrop = Lo.RetriveMasterSubCategoryDate(0, "PRODUCT (INDUSTRY DOMAIN)", "", "SelectProductCat", "", "");
        if (DtTechDrop.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddltech1, DtTechDrop, "SCategoryName", "SCategoryID");
            ddltech1.Items.Insert(0, "Select");
        }
    }
    protected void ddltech1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvrtech = (GridViewRow)(((Control)sender).NamingContainer);
        int rowIndextech = gvrtech.RowIndex;
        DropDownList ddltech1 = gvtechnology.Rows[rowIndextech].FindControl("ddltech1") as DropDownList;
        DropDownList ddltech2 = gvtechnology.Rows[rowIndextech].FindControl("ddltech2") as DropDownList;
        BindMasterTechnologySubCat(ddltech1, ddltech2);
    }
    protected void BindMasterTechnologySubCat(DropDownList ddltech1, DropDownList ddltech2)
    {
        if (ddltech1.SelectedItem.Text != "Select")
        {
            DataTable DtTech2 = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddltech1.SelectedItem.Value), "", "", "SubSelectID", "", "");
            if (DtTech2.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddltech2, DtTech2, "SCategoryName", "SCategoryId");
                ddltech2.Items.Insert(0, "Select");
            }
            else
            {
                ddltech2.Items.Clear();
                ddltech2.Items.Insert(0, "Select");
            }
        }
    }
    protected void txttech2_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);
        int rowIndex = gvr.RowIndex;
        DropDownList ddltech2 = gvtechnology.Rows[rowIndex].FindControl("ddltech2") as DropDownList;
        DropDownList ddltech3 = gvtechnology.Rows[rowIndex].FindControl("ddltech3") as DropDownList;
        BindMasterSubTech3(ddltech2, ddltech3);
    }
    protected void BindMasterSubTech3(DropDownList ddltech2, DropDownList ddltech3)
    {
        if (ddltech2.Text != "" && ddltech2.SelectedItem.Text != "Select")
        {
            DataTable DtMasterCatTech3 = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddltech2.SelectedItem.Value), "", "", "SubSelectID", "", "");
            if (DtMasterCatTech3.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddltech3, DtMasterCatTech3, "SCategoryName", "SCategoryId");
                ddltech3.Items.Insert(0, "Select");
            }
            else
            {
                ddltech3.Items.Clear();
                ddltech3.Items.Insert(0, "Select");
                ddltech3.Items.Insert(1, "NA");
            }
        }
    }
    private void AddNewTechnologyDetailGrid()
    {
        int rowIndexTech = 0;
        if (ViewState["TechnologyDetails"] != null)
        {
            DataTable dtCurrentTableTech = (DataTable)ViewState["TechnologyDetails"];
            DataRow drCurrentRowNameTech = null;
            if (dtCurrentTableTech.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTableTech.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox TextBox1 = (TextBox)gvtechnology.Rows[rowIndexTech].Cells[1].FindControl("txttechnomen");
                    DropDownList ddltech_1 = (DropDownList)gvtechnology.Rows[rowIndexTech].Cells[2].FindControl("ddltech1");
                    DropDownList ddlsubtech_2 = (DropDownList)gvtechnology.Rows[rowIndexTech].Cells[3].FindControl("ddltech2");
                    DropDownList ddlInnertech_3 = (DropDownList)gvtechnology.Rows[rowIndexTech].Cells[4].FindControl("ddltech3");
                    drCurrentRowNameTech = dtCurrentTableTech.NewRow();
                    drCurrentRowNameTech["RowNumberTech"] = i + 1;
                    dtCurrentTableTech.Rows[i - 1]["TechNomenclature"] = TextBox1.Text;
                    dtCurrentTableTech.Rows[i - 1]["Technology1"] = ddltech_1.Text;
                    dtCurrentTableTech.Rows[i - 1]["Technology2"] = ddlsubtech_2.Text;
                    dtCurrentTableTech.Rows[i - 1]["Technology3"] = ddlInnertech_3.Text;
                    rowIndexTech++;
                }
                dtCurrentTableTech.Rows.Add(drCurrentRowNameTech);
                ViewState["TechnologyDetails"] = dtCurrentTableTech;
                gvtechnology.DataSource = dtCurrentTableTech;
                gvtechnology.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousDataTechnologyDetail();
    }
    private void SetPreviousDataTechnologyDetail()
    {
        int rowIndexTech = 0;
        if (ViewState["TechnologyDetails"] != null)
        {
            DataTable dtTechPrevious = (DataTable)ViewState["TechnologyDetails"];
            if (dtTechPrevious.Rows.Count > 0)
            {
                for (int i = 0; i < dtTechPrevious.Rows.Count; i++)
                {
                    TextBox TBtech_1 = (TextBox)gvtechnology.Rows[rowIndexTech].Cells[1].FindControl("txttechnomen");
                    DropDownList DDLtech_1 = (DropDownList)gvtechnology.Rows[rowIndexTech].Cells[2].FindControl("ddltech1");
                    DropDownList DDLtech_2 = (DropDownList)gvtechnology.Rows[rowIndexTech].Cells[3].FindControl("ddltech1");
                    DropDownList DDLtech_3 = (DropDownList)gvtechnology.Rows[rowIndexTech].Cells[4].FindControl("ddltech1");
                    TBtech_1.Text = dtTechPrevious.Rows[i]["TechNomenclature"].ToString();
                    BindMasterTechnologyCategory(DDLtech_1);
                    if (i < dtTechPrevious.Rows.Count - 1)
                    {
                        DDLtech_1.ClearSelection();
                        DDLtech_1.Items.FindByValue(dtTechPrevious.Rows[i]["Technology1"].ToString()).Selected = true;
                        DDLtech_2.ClearSelection();
                        BindMasterTechnologySubCat(DDLtech_1, DDLtech_2);
                        if (DDLtech_2.Text != "Select" && DDLtech_2.Text != "")
                        {
                            DDLtech_2.Items.FindByValue(dtTechPrevious.Rows[i]["Technology2"].ToString()).Selected = true;
                        }
                        DDLtech_3.ClearSelection();
                        BindMasterSubTech3(DDLtech_2, DDLtech_3);
                        if (DDLtech_3.Text != "Select" && DDLtech_3.Text != "")
                        {
                            DDLtech_3.Items.FindByValue(dtTechPrevious.Rows[i]["Technology3"].ToString()).Selected = true;
                        }
                    }
                    rowIndexTech++;
                }
            }
        }
    }
    protected void btnAddTech_Click(object sender, EventArgs e)
    {
        AddNewTechnologyDetailGrid();
    }
    protected void gvtechnology_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dtgridTechrowcreated = (DataTable)ViewState["TechnologyDetails"];
            LinkButton lbTech = (LinkButton)e.Row.FindControl("lbtechremove");
            if (lbTech != null)
            {
                if (dtgridTechrowcreated.Rows.Count > 1)
                {
                    if (e.Row.RowIndex == dtgridTechrowcreated.Rows.Count - 1)
                    {
                        lbTech.Visible = false;
                    }
                }
                else
                {
                    lbTech.Visible = false;
                }
            }
        }
    }
    protected void lbtechremove_Click(object sender, EventArgs e)
    {
        LinkButton lbTech = (LinkButton)sender;
        GridViewRow gvRowTech = (GridViewRow)lbTech.NamingContainer;
        int rowID = gvRowTech.RowIndex;
        if (ViewState["TechnologyDetails"] != null)
        {
            DataTable dtremovegridTech = (DataTable)ViewState["TechnologyDetails"];
            if (dtremovegridTech.Rows.Count > 1)
            {
                if (gvRowTech.RowIndex < dtremovegridTech.Rows.Count - 1)
                {
                    dtremovegridTech.Rows.Remove(dtremovegridTech.Rows[rowID]);
                    ResetRowIDTechDetail(dtremovegridTech);
                }
            }
            ViewState["TechnologyDetails"] = dtremovegridTech;
            gvtechnology.DataSource = dtremovegridTech;
            gvtechnology.DataBind();
        }
        SetPreviousDataTechnologyDetail();
    }
    private void ResetRowIDTechDetail(DataTable dtRemoveTechdetail)
    {
        int rowNumberTech = 1;
        if (dtRemoveTechdetail.Rows.Count > 0)
        {
            foreach (DataRow rowTech in dtRemoveTechdetail.Rows)
            {
                rowTech[0] = rowNumberTech;
                rowNumberTech++;
            }
        }
    }
    DataTable DtSaveTech = new DataTable();
    protected void SaveCodeTechDetails()
    {
        DtSaveTech.Columns.Add(new DataColumn("RowNumberTech", typeof(string)));
        DtSaveTech.Columns.Add(new DataColumn("TechNomenclature", typeof(string)));
        DtSaveTech.Columns.Add(new DataColumn("Technology1", typeof(string)));
        DtSaveTech.Columns.Add(new DataColumn("Technology2", typeof(string)));
        DtSaveTech.Columns.Add(new DataColumn("Technology3", typeof(string)));
        DataRow drCurrentRowSaveCode = null;
        for (int i = 0; gvtechnology.Rows.Count > i; i++)
        {
            TextBox TBtech_1 = (TextBox)gvtechnology.Rows[i].Cells[1].FindControl("txttechnomen");
            DropDownList DDLtech_1 = (DropDownList)gvtechnology.Rows[i].Cells[2].FindControl("ddltech1");
            DropDownList DDLtech_2 = (DropDownList)gvtechnology.Rows[i].Cells[3].FindControl("ddltech1");
            DropDownList DDLtech_3 = (DropDownList)gvtechnology.Rows[i].Cells[4].FindControl("ddltech1");
            if (DDLtech_1.SelectedItem.Text != "Select" && TBtech_1.Text != "")
            {
                drCurrentRowSaveCode = DtSaveTech.NewRow();
                drCurrentRowSaveCode["RowNumberTech"] = i + 1;
                drCurrentRowSaveCode["TechNomenclature"] = TBtech_1.Text;
                drCurrentRowSaveCode["Technology1"] = DDLtech_1.SelectedItem.Value;
                drCurrentRowSaveCode["Technology2"] = DDLtech_2.SelectedItem.Value;
                drCurrentRowSaveCode["Technology3"] = DDLtech_3.SelectedItem.Value;
                DtSaveTech.Rows.Add(drCurrentRowSaveCode);
            }
        }
        ViewState["TechnologyDetails"] = DtSaveTech;
    }
    #endregion
    // Add  Grid of Source of Raw Material
    #region Grid of Source of Raw Material
    private void SetRawmeterialDetails()
    {
        DataTable dtRawmeterialDetail = new DataTable();
        DataRow drRawmeterialDetail = null;
        dtRawmeterialDetail.Columns.Add(new DataColumn("SrNoRawMeterail", typeof(string)));
        dtRawmeterialDetail.Columns.Add(new DataColumn("Items", typeof(string)));
        dtRawmeterialDetail.Columns.Add(new DataColumn("RawMeterial", typeof(string)));
        dtRawmeterialDetail.Columns.Add(new DataColumn("SourceMeterial", typeof(string)));
        dtRawmeterialDetail.Columns.Add(new DataColumn("MeterailSupplier", typeof(string)));
        drRawmeterialDetail = dtRawmeterialDetail.NewRow();
        drRawmeterialDetail["SrNoRawMeterail"] = 1;
        drRawmeterialDetail["Items"] = string.Empty;
        drRawmeterialDetail["RawMeterial"] = string.Empty;
        drRawmeterialDetail["SourceMeterial"] = string.Empty;
        drRawmeterialDetail["MeterailSupplier"] = string.Empty;
        dtRawmeterialDetail.Rows.Add(drRawmeterialDetail);
        ViewState["RawMeterialDetails"] = dtRawmeterialDetail;
        gvSourceofRawMaterial.DataSource = dtRawmeterialDetail;
        gvSourceofRawMaterial.DataBind();
    }
    private void AddSourceofRawMeterial()
    {
        int rowIndexRawmeterial = 0;
        if (ViewState["RawMeterialDetails"] != null)
        {
            DataTable dtCurrentRawMeterialDetails = (DataTable)ViewState["RawMeterialDetails"];
            DataRow drCurrentRawMeterialDetails = null;
            if (dtCurrentRawMeterialDetails.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentRawMeterialDetails.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox TextBox1_Raw = (TextBox)gvSourceofRawMaterial.Rows[rowIndexRawmeterial].Cells[1].FindControl("txtitems");
                    TextBox TextBox2_Raw = (TextBox)gvSourceofRawMaterial.Rows[rowIndexRawmeterial].Cells[2].FindControl("txtbasicrawmeterial");
                    DropDownList ddl3_Raw = (DropDownList)gvSourceofRawMaterial.Rows[rowIndexRawmeterial].Cells[3].FindControl("ddlsourceofmaterial");
                    TextBox TextBox4_Raw = (TextBox)gvSourceofRawMaterial.Rows[rowIndexRawmeterial].Cells[4].FindControl("txtmaterialsupplier");
                    drCurrentRawMeterialDetails = dtCurrentRawMeterialDetails.NewRow();
                    drCurrentRawMeterialDetails["SrNoRawMeterail"] = i + 1;
                    dtCurrentRawMeterialDetails.Rows[i - 1]["Items"] = TextBox1.Text;
                    dtCurrentRawMeterialDetails.Rows[i - 1]["RawMeterial"] = TextBox2.Text;
                    dtCurrentRawMeterialDetails.Rows[i - 1]["SourceMeterial"] = ddl3_Raw.Text;
                    dtCurrentRawMeterialDetails.Rows[i - 1]["MeterailSupplier"] = TextBox4.Text;
                    rowIndexRawmeterial++;
                }
                dtCurrentRawMeterialDetails.Rows.Add(drCurrentRawMeterialDetails);
                ViewState["RawMeterialDetails"] = dtCurrentRawMeterialDetails;
                gvSourceofRawMaterial.DataSource = dtCurrentRawMeterialDetails;
                gvSourceofRawMaterial.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousDataSourceofRawMeterial();
    }
    private void SetPreviousDataSourceofRawMeterial()
    {
        int rowIndexRawmeterialPre = 0;
        if (ViewState["RawMeterialDetails"] != null)
        {
            DataTable dtPreSourceRaw = (DataTable)ViewState["RawMeterialDetails"];
            if (dtPreSourceRaw.Rows.Count > 0)
            {
                for (int i = 0; i < dtPreSourceRaw.Rows.Count; i++)
                {
                    TextBox TextBox_1Raw = (TextBox)gvSourceofRawMaterial.Rows[rowIndexRawmeterialPre].Cells[1].FindControl("txtitems");
                    TextBox TextBox_2Raw = (TextBox)gvSourceofRawMaterial.Rows[rowIndexRawmeterialPre].Cells[2].FindControl("txtbasicrawmeterial");
                    DropDownList ddl_3Raw = (DropDownList)gvSourceofRawMaterial.Rows[rowIndexRawmeterialPre].Cells[3].FindControl("ddlsourceofmaterial");
                    TextBox TextBox_4Raw = (TextBox)gvSourceofRawMaterial.Rows[rowIndexRawmeterialPre].Cells[4].FindControl("txtmaterialsupplier");
                    TextBox_1Raw.Text = dtPreSourceRaw.Rows[i]["Items"].ToString();
                    TextBox_2Raw.Text = dtPreSourceRaw.Rows[i]["RawMeterial"].ToString();
                    if (i < dtPreSourceRaw.Rows.Count - 1)
                    {
                        ddl_3Raw.ClearSelection();
                        ddl_3Raw.Items.FindByValue(dtPreSourceRaw.Rows[i]["SourceMeterial"].ToString()).Selected = true;
                    }
                    TextBox_4Raw.Text = dtPreSourceRaw.Rows[i]["MeterailSupplier"].ToString();
                    rowIndexRawmeterialPre++;
                }
            }
        }
    }
    protected void btnAddRawMeterial_Click(object sender, EventArgs e)
    {
        AddSourceofRawMeterial();
    }
    protected void lbmeterailremove_Click(object sender, EventArgs e)
    {
        LinkButton lbRaw = (LinkButton)sender;
        GridViewRow gvRowMeterial = (GridViewRow)lbRaw.NamingContainer;
        int rowID = gvRowMeterial.RowIndex;
        if (ViewState["RawMeterialDetails"] != null)
        {
            DataTable dtremovegridRawmete = (DataTable)ViewState["RawMeterialDetails"];
            if (dtremovegridRawmete.Rows.Count > 1)
            {
                if (gvRowMeterial.RowIndex < dtremovegridRawmete.Rows.Count - 1)
                {
                    dtremovegridRawmete.Rows.Remove(dtremovegridRawmete.Rows[rowID]);
                    ResetRowIDRawmeterial(dtremovegridRawmete);
                }
            }
            ViewState["RawMeterialDetails"] = dtremovegridRawmete;
            gvSourceofRawMaterial.DataSource = dtremovegridRawmete;
            gvSourceofRawMaterial.DataBind();
        }
        SetPreviousDataSourceofRawMeterial();
    }
    private void ResetRowIDRawmeterial(DataTable dtremovecountrawmete)
    {
        int rowNumbermeterial = 1;
        if (dtremovecountrawmete.Rows.Count > 0)
        {
            foreach (DataRow row in dtremovecountrawmete.Rows)
            {
                row[0] = rowNumbermeterial;
                rowNumbermeterial++;
            }
        }
    }
    protected void gvSourceofRawMaterial_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dtgridrowmetecreated = (DataTable)ViewState["RawMeterialDetails"];
            LinkButton lbraw = (LinkButton)e.Row.FindControl("lbmeterailremove");
            if (lbraw != null)
            {
                if (dtgridrowmetecreated.Rows.Count > 1)
                {
                    if (e.Row.RowIndex == dtgridrowmetecreated.Rows.Count - 1)
                    {
                        lbraw.Visible = false;
                    }
                }
                else
                {
                    lbraw.Visible = false;
                }
            }
        }
    }
    DataTable DtSaveRawMeterial = new DataTable();
    private void SaveRawmeterial()
    {
        DtSaveRawMeterial.Columns.Add(new DataColumn("SrNoRawMeterail", typeof(string)));
        DtSaveRawMeterial.Columns.Add(new DataColumn("Items", typeof(string)));
        DtSaveRawMeterial.Columns.Add(new DataColumn("RawMeterial", typeof(string)));
        DtSaveRawMeterial.Columns.Add(new DataColumn("SourceMeterial", typeof(string)));
        DtSaveRawMeterial.Columns.Add(new DataColumn("MeterailSupplier", typeof(string)));
        DataRow drCurrentRowSaveCode = null;
        for (int i = 0; gvSourceofRawMaterial.Rows.Count > i; i++)
        {
            TextBox TextBox_1Raw = (TextBox)gvSourceofRawMaterial.Rows[i].Cells[1].FindControl("txtitems");
            TextBox TextBox_2Raw = (TextBox)gvSourceofRawMaterial.Rows[i].Cells[2].FindControl("txtbasicrawmeterial");
            DropDownList ddl_3Raw = (DropDownList)gvSourceofRawMaterial.Rows[i].Cells[3].FindControl("ddlsourceofmaterial");
            TextBox TextBox_4Raw = (TextBox)gvSourceofRawMaterial.Rows[i].Cells[4].FindControl("txtmaterialsupplier");
            if (ddl_3Raw.SelectedItem.Text != "Select" && TextBox_1Raw.Text != "")
            {
                drCurrentRowSaveCode = DtSaveRawMeterial.NewRow();
                drCurrentRowSaveCode["SrNoRawMeterail"] = i + 1;
                drCurrentRowSaveCode["Items"] = TextBox_1Raw.Text;
                drCurrentRowSaveCode["RawMeterial"] = TextBox_2Raw.Text;
                drCurrentRowSaveCode["SourceMeterial"] = ddl_3Raw.SelectedItem.Value;
                drCurrentRowSaveCode["MeterailSupplier"] = TextBox_4Raw.Text;
                DtSaveRawMeterial.Rows.Add(drCurrentRowSaveCode);
            }
        }
        ViewState["RawMeterialDetails"] = DtSaveRawMeterial;
    }
    #endregion
    //Add Grid of Item Produced and Supplied
    #region Item Product and supplied
    private void SetInitialRowItemProductorSupplied()
    {
        //Create false table
        DataTable dtProdSupp = new DataTable();
        DataRow drProdSupp = null;
        dtProdSupp.Columns.Add(new DataColumn("SrNoSpplied", typeof(string)));
        dtProdSupp.Columns.Add(new DataColumn("NameCust", typeof(string)));
        dtProdSupp.Columns.Add(new DataColumn("DesStoreSupp", typeof(string)));
        dtProdSupp.Columns.Add(new DataColumn("OderNoorDate", typeof(string)));
        dtProdSupp.Columns.Add(new DataColumn("OrderQty", typeof(string)));
        dtProdSupp.Columns.Add(new DataColumn("ValueQtySupp", typeof(string)));
        dtProdSupp.Columns.Add(new DataColumn("DateofLastSupp", typeof(string)));
        drProdSupp = dtProdSupp.NewRow();
        drProdSupp["SrNoSpplied"] = 1;
        drProdSupp["NameCust"] = string.Empty;
        drProdSupp["DesStoreSupp"] = string.Empty;
        drProdSupp["OderNoorDate"] = string.Empty;
        drProdSupp["OrderQty"] = string.Empty;
        drProdSupp["ValueQtySupp"] = string.Empty;
        drProdSupp["DateofLastSupp"] = string.Empty;
        dtProdSupp.Rows.Add(drProdSupp);
        //Store the DataTable in ViewState or bind or show false grid
        ViewState["ProdSupp"] = dtProdSupp;
        gvItemProducedandSupplied.DataSource = dtProdSupp;
        gvItemProducedandSupplied.DataBind();
    }
    private void AddNewRowItemProductorSupplied()
    {
        int rowIndexProd = 0;
        if (ViewState["ProdSupp"] != null)
        {
            DataTable dtCurrentTableProd = (DataTable)ViewState["ProdSupp"];
            DataRow drCurrentRowProd = null;
            if (dtCurrentTableProd.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTableProd.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox TextBox1_Prod = (TextBox)gvItemProducedandSupplied.Rows[rowIndexProd].Cells[1].FindControl("txtnameofrepcustomer");
                    TextBox TextBox2_Prod = (TextBox)gvItemProducedandSupplied.Rows[rowIndexProd].Cells[2].FindControl("txtdescofstoresupp");
                    TextBox TextBox3_Prod = (TextBox)gvItemProducedandSupplied.Rows[rowIndexProd].Cells[3].FindControl("txtsonoanddate");
                    TextBox TextBox4_Prod = (TextBox)gvItemProducedandSupplied.Rows[rowIndexProd].Cells[4].FindControl("txtorderqty");
                    TextBox TextBox5_Prod = (TextBox)gvItemProducedandSupplied.Rows[rowIndexProd].Cells[5].FindControl("txtvalueqtysupplied");
                    TextBox TextBox6_Prod = (TextBox)gvItemProducedandSupplied.Rows[rowIndexProd].Cells[6].FindControl("txtdateoflastsupplie");
                    drCurrentRowProd = dtCurrentTableProd.NewRow();
                    drCurrentRowProd["SrNoSpplied"] = i + 1;
                    dtCurrentTableProd.Rows[i - 1]["NameCust"] = TextBox1_Prod.Text;
                    dtCurrentTableProd.Rows[i - 1]["DesStoreSupp"] = TextBox2_Prod.Text;
                    dtCurrentTableProd.Rows[i - 1]["OderNoorDate"] = TextBox3_Prod.Text;
                    dtCurrentTableProd.Rows[i - 1]["OrderQty"] = TextBox4_Prod.Text;
                    dtCurrentTableProd.Rows[i - 1]["ValueQtySupp"] = TextBox5_Prod.Text;
                    dtCurrentTableProd.Rows[i - 1]["DateofLastSupp"] = TextBox6_Prod.Text;
                    rowIndexProd++;
                }
                dtCurrentTableProd.Rows.Add(drCurrentRowProd);
                ViewState["ProdSupp"] = dtCurrentTableProd;
                gvItemProducedandSupplied.DataSource = dtCurrentTableProd;
                gvItemProducedandSupplied.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousDataItemProductorSupplied();
    }
    private void SetPreviousDataItemProductorSupplied()
    {
        int rowIndexPro = 0;
        if (ViewState["ProdSupp"] != null)
        {
            DataTable dtProdPer = (DataTable)ViewState["ProdSupp"];
            if (dtProdPer.Rows.Count > 0)
            {
                for (int i = 0; i < dtProdPer.Rows.Count; i++)
                {
                    TextBox TextBox_1 = (TextBox)gvItemProducedandSupplied.Rows[rowIndexPro].Cells[1].FindControl("txtnameofrepcustomer");
                    TextBox TextBox_2 = (TextBox)gvItemProducedandSupplied.Rows[rowIndexPro].Cells[2].FindControl("txtdescofstoresupp");
                    TextBox TextBox_3 = (TextBox)gvItemProducedandSupplied.Rows[rowIndexPro].Cells[3].FindControl("txtsonoanddate");
                    TextBox TextBox_4 = (TextBox)gvItemProducedandSupplied.Rows[rowIndexPro].Cells[4].FindControl("txtorderqty");
                    TextBox TextBox_5 = (TextBox)gvItemProducedandSupplied.Rows[rowIndexPro].Cells[5].FindControl("txtvalueqtysupplied");
                    TextBox TextBox_6 = (TextBox)gvItemProducedandSupplied.Rows[rowIndexPro].Cells[6].FindControl("txtdateoflastsupplie");
                    TextBox_1.Text = dtProdPer.Rows[i]["NameCust"].ToString();
                    TextBox_2.Text = dtProdPer.Rows[i]["DesStoreSupp"].ToString();
                    TextBox_3.Text = dtProdPer.Rows[i]["OderNoorDate"].ToString();
                    TextBox_4.Text = dtProdPer.Rows[i]["OrderQty"].ToString();
                    TextBox_5.Text = dtProdPer.Rows[i]["ValueQtySupp"].ToString();
                    TextBox_6.Text = dtProdPer.Rows[i]["DateofLastSupp"].ToString();
                    rowIndexPro++;
                }
            }
        }
    }
    protected void btnAddSupplied_Click(object sender, EventArgs e)
    {
        AddNewRowItemProductorSupplied();
    }
    protected void gvItemProducedandSupplied_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dtgridProd = (DataTable)ViewState["ProdSupp"];
            LinkButton lbProd = (LinkButton)e.Row.FindControl("lbSuplliedremove");
            if (lbProd != null)
            {
                if (dtgridProd.Rows.Count > 1)
                {
                    if (e.Row.RowIndex == dtgridProd.Rows.Count - 1)
                    {
                        lbProd.Visible = false;
                    }
                }
                else
                {
                    lbProd.Visible = false;
                }
            }
        }
    }
    protected void lbSuplliedremove_Click(object sender, EventArgs e)
    {
        LinkButton lbProd = (LinkButton)sender;
        GridViewRow gvRowProd = (GridViewRow)lbProd.NamingContainer;
        int rowID = gvRowProd.RowIndex;
        if (ViewState["ProdSupp"] != null)
        {
            DataTable dtremovegridProd = (DataTable)ViewState["ProdSupp"];
            if (dtremovegridProd.Rows.Count > 1)
            {
                if (gvRowProd.RowIndex < dtremovegridProd.Rows.Count - 1)
                {
                    dtremovegridProd.Rows.Remove(dtremovegridProd.Rows[rowID]);
                    ResetRowIDProd(dtremovegridProd);
                }
            }
            ViewState["ProdSupp"] = dtremovegridProd;
            gvItemProducedandSupplied.DataSource = dtremovegridProd;
            gvItemProducedandSupplied.DataBind();
        }
        SetPreviousDataItemProductorSupplied();
    }
    private void ResetRowIDProd(DataTable dtcountProd)
    {
        int rowNumberProd = 1;
        if (dtcountProd.Rows.Count > 0)
        {
            foreach (DataRow row in dtcountProd.Rows)
            {
                row[0] = rowNumberProd;
                rowNumberProd++;
            }
        }
    }
    DataTable DtSavePAS = new DataTable();
    protected void SavePAS()
    {
        DtSavePAS.Columns.Add(new DataColumn("SrNoSpplied", typeof(string)));
        DtSavePAS.Columns.Add(new DataColumn("NameCust", typeof(string)));
        DtSavePAS.Columns.Add(new DataColumn("DesStoreSupp", typeof(string)));
        DtSavePAS.Columns.Add(new DataColumn("OderNoorDate", typeof(string)));
        DtSavePAS.Columns.Add(new DataColumn("OrderQty", typeof(string)));
        DtSavePAS.Columns.Add(new DataColumn("ValueQtySupp", typeof(string)));
        DtSavePAS.Columns.Add(new DataColumn("DateofLastSupp", typeof(string)));
        DataRow drCurrentRowSaveCode = null;
        for (int i = 0; gvItemProducedandSupplied.Rows.Count > i; i++)
        {
            TextBox TextBox_1 = (TextBox)gvItemProducedandSupplied.Rows[i].Cells[1].FindControl("txtnameofrepcustomer");
            TextBox TextBox_2 = (TextBox)gvItemProducedandSupplied.Rows[i].Cells[2].FindControl("txtdescofstoresupp");
            TextBox TextBox_3 = (TextBox)gvItemProducedandSupplied.Rows[i].Cells[3].FindControl("txtsonoanddate");
            TextBox TextBox_4 = (TextBox)gvItemProducedandSupplied.Rows[i].Cells[4].FindControl("txtorderqty");
            TextBox TextBox_5 = (TextBox)gvItemProducedandSupplied.Rows[i].Cells[5].FindControl("txtvalueqtysupplied");
            TextBox TextBox_6 = (TextBox)gvItemProducedandSupplied.Rows[i].Cells[6].FindControl("txtdateoflastsupplie");
            if (TextBox_1.Text != "" && TextBox_2.Text != "")
            {
                drCurrentRowSaveCode = DtSavePAS.NewRow();
                drCurrentRowSaveCode["SrNoSpplied"] = i + 1;
                drCurrentRowSaveCode["NameCust"] = TextBox_1.Text;
                drCurrentRowSaveCode["DesStoreSupp"] = TextBox_2.Text;
                drCurrentRowSaveCode["OderNoorDate"] = TextBox_3.Text;
                drCurrentRowSaveCode["OrderQty"] = TextBox_4.Text;
                drCurrentRowSaveCode["ValueQtySupp"] = TextBox_5.Text;
                drCurrentRowSaveCode["DateofLastSupp"] = TextBox_6.Text;
                DtSavePAS.Rows.Add(drCurrentRowSaveCode);
            }
        }
        ViewState["ProdSupp"] = DtSavePAS;
    }
    #endregion
    // Add grid of Item Supplied but not produced
    #region item supplied but not produced
    private void SetInitialRowItemProductorSupplied1()
    {
        //Create false table
        DataTable dtProdSupp1 = new DataTable();
        DataRow drProdSupp1 = null;
        dtProdSupp1.Columns.Add(new DataColumn("SrNoSpplied1", typeof(string)));
        dtProdSupp1.Columns.Add(new DataColumn("NameCust1", typeof(string)));
        dtProdSupp1.Columns.Add(new DataColumn("DesStoreSupp1", typeof(string)));
        dtProdSupp1.Columns.Add(new DataColumn("OderNoorDate1", typeof(string)));
        dtProdSupp1.Columns.Add(new DataColumn("OrderQty1", typeof(string)));
        dtProdSupp1.Columns.Add(new DataColumn("ValueQtySupp1", typeof(string)));
        dtProdSupp1.Columns.Add(new DataColumn("DateofLastSupp1", typeof(string)));
        drProdSupp1 = dtProdSupp1.NewRow();
        drProdSupp1["SrNoSpplied1"] = 1;
        drProdSupp1["NameCust1"] = string.Empty;
        drProdSupp1["DesStoreSupp1"] = string.Empty;
        drProdSupp1["OderNoorDate1"] = string.Empty;
        drProdSupp1["OrderQty1"] = string.Empty;
        drProdSupp1["ValueQtySupp1"] = string.Empty;
        drProdSupp1["DateofLastSupp1"] = string.Empty;
        dtProdSupp1.Rows.Add(drProdSupp1);
        //Store the DataTable in ViewState or bind or show false grid
        ViewState["ProdSupp1"] = dtProdSupp1;
        gvItemSuppliedbutnotproduced.DataSource = dtProdSupp1;
        gvItemSuppliedbutnotproduced.DataBind();
    }
    private void AddNewRowItemProductorSupplied1()
    {
        int rowIndexProd1 = 0;
        if (ViewState["ProdSupp1"] != null)
        {
            DataTable dtCurrentTableProd1 = (DataTable)ViewState["ProdSupp1"];
            DataRow drCurrentRowProd1 = null;
            if (dtCurrentTableProd1.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTableProd1.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox TextBox1_Prod = (TextBox)gvItemSuppliedbutnotproduced.Rows[rowIndexProd1].Cells[1].FindControl("txtnameofrepcustomer1");
                    TextBox TextBox2_Prod = (TextBox)gvItemSuppliedbutnotproduced.Rows[rowIndexProd1].Cells[2].FindControl("txtdescofstoresupp1");
                    TextBox TextBox3_Prod = (TextBox)gvItemSuppliedbutnotproduced.Rows[rowIndexProd1].Cells[3].FindControl("txtsonoanddate1");
                    TextBox TextBox4_Prod = (TextBox)gvItemSuppliedbutnotproduced.Rows[rowIndexProd1].Cells[4].FindControl("txtorderqty1");
                    TextBox TextBox5_Prod = (TextBox)gvItemSuppliedbutnotproduced.Rows[rowIndexProd1].Cells[5].FindControl("txtvalueqtysupplied1");
                    TextBox TextBox6_Prod = (TextBox)gvItemSuppliedbutnotproduced.Rows[rowIndexProd1].Cells[6].FindControl("txtdateoflastsupplie1");
                    drCurrentRowProd1 = dtCurrentTableProd1.NewRow();
                    drCurrentRowProd1["SrNoSpplied1"] = i + 1;
                    dtCurrentTableProd1.Rows[i - 1]["NameCust1"] = TextBox1_Prod.Text;
                    dtCurrentTableProd1.Rows[i - 1]["DesStoreSupp1"] = TextBox2_Prod.Text;
                    dtCurrentTableProd1.Rows[i - 1]["OderNoorDate1"] = TextBox3_Prod.Text;
                    dtCurrentTableProd1.Rows[i - 1]["OrderQty1"] = TextBox4_Prod.Text;
                    dtCurrentTableProd1.Rows[i - 1]["ValueQtySupp1"] = TextBox5_Prod.Text;
                    dtCurrentTableProd1.Rows[i - 1]["DateofLastSupp1"] = TextBox6_Prod.Text;
                    rowIndexProd1++;
                }
                dtCurrentTableProd1.Rows.Add(drCurrentRowProd1);
                ViewState["ProdSupp1"] = dtCurrentTableProd1;
                gvItemSuppliedbutnotproduced.DataSource = dtCurrentTableProd1;
                gvItemSuppliedbutnotproduced.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousDataItemProductorSupplied1();
    }
    private void SetPreviousDataItemProductorSupplied1()
    {
        int rowIndexPro1 = 0;
        if (ViewState["ProdSupp1"] != null)
        {
            DataTable dtProdPer1 = (DataTable)ViewState["ProdSupp1"];
            if (dtProdPer1.Rows.Count > 0)
            {
                for (int i = 0; i < dtProdPer1.Rows.Count; i++)
                {
                    TextBox TextBox_1 = (TextBox)gvItemSuppliedbutnotproduced.Rows[rowIndexPro1].Cells[1].FindControl("txtnameofrepcustomer1");
                    TextBox TextBox_2 = (TextBox)gvItemSuppliedbutnotproduced.Rows[rowIndexPro1].Cells[2].FindControl("txtdescofstoresupp1");
                    TextBox TextBox_3 = (TextBox)gvItemSuppliedbutnotproduced.Rows[rowIndexPro1].Cells[3].FindControl("txtsonoanddate1");
                    TextBox TextBox_4 = (TextBox)gvItemSuppliedbutnotproduced.Rows[rowIndexPro1].Cells[4].FindControl("txtorderqty1");
                    TextBox TextBox_5 = (TextBox)gvItemSuppliedbutnotproduced.Rows[rowIndexPro1].Cells[5].FindControl("txtvalueqtysupplied1");
                    TextBox TextBox_6 = (TextBox)gvItemSuppliedbutnotproduced.Rows[rowIndexPro1].Cells[6].FindControl("txtdateoflastsupplie1");
                    TextBox_1.Text = dtProdPer1.Rows[i]["NameCust1"].ToString();
                    TextBox_2.Text = dtProdPer1.Rows[i]["DesStoreSupp1"].ToString();
                    TextBox_3.Text = dtProdPer1.Rows[i]["OderNoorDate1"].ToString();
                    TextBox_4.Text = dtProdPer1.Rows[i]["OrderQty1"].ToString();
                    TextBox_5.Text = dtProdPer1.Rows[i]["ValueQtySupp1"].ToString();
                    TextBox_6.Text = dtProdPer1.Rows[i]["DateofLastSupp1"].ToString();
                    rowIndexPro1++;
                }
            }
        }
    }
    protected void btnAddSupplied1_Click(object sender, EventArgs e)
    {
        AddNewRowItemProductorSupplied1();
    }
    protected void gvItemSuppliedbutnotproduced_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dtgridProd1 = (DataTable)ViewState["ProdSupp1"];
            LinkButton lbProd1 = (LinkButton)e.Row.FindControl("lbSuplliedremove1");
            if (lbProd1 != null)
            {
                if (dtgridProd1.Rows.Count > 1)
                {
                    if (e.Row.RowIndex == dtgridProd1.Rows.Count - 1)
                    {
                        lbProd1.Visible = false;
                    }
                }
                else
                {
                    lbProd1.Visible = false;
                }
            }
        }
    }
    protected void lbSuplliedremove1_Click(object sender, EventArgs e)
    {
        LinkButton lbProd1 = (LinkButton)sender;
        GridViewRow gvRowProd1 = (GridViewRow)lbProd1.NamingContainer;
        int rowID = gvRowProd1.RowIndex;
        if (ViewState["ProdSupp1"] != null)
        {
            DataTable dtremovegridProd1 = (DataTable)ViewState["ProdSupp1"];
            if (dtremovegridProd1.Rows.Count > 1)
            {
                if (gvRowProd1.RowIndex < dtremovegridProd1.Rows.Count - 1)
                {
                    dtremovegridProd1.Rows.Remove(dtremovegridProd1.Rows[rowID]);
                    ResetRowIDProd1(dtremovegridProd1);
                }
            }
            ViewState["ProdSupp1"] = dtremovegridProd1;
            gvItemSuppliedbutnotproduced.DataSource = dtremovegridProd1;
            gvItemSuppliedbutnotproduced.DataBind();
        }
        SetPreviousDataItemProductorSupplied1();
    }
    private void ResetRowIDProd1(DataTable dtcountProd1)
    {
        int rowNumberProd1 = 1;
        if (dtcountProd1.Rows.Count > 0)
        {
            foreach (DataRow row in dtcountProd1.Rows)
            {
                row[0] = rowNumberProd1;
                rowNumberProd1++;
            }
        }
    }
    DataTable DtSaveProdSupp = new DataTable();
    protected void SaveProdSupp()
    {
        DtSaveProdSupp.Columns.Add(new DataColumn("SrNoSpplied1", typeof(string)));
        DtSaveProdSupp.Columns.Add(new DataColumn("NameCust1", typeof(string)));
        DtSaveProdSupp.Columns.Add(new DataColumn("DesStoreSupp1", typeof(string)));
        DtSaveProdSupp.Columns.Add(new DataColumn("OderNoorDate1", typeof(string)));
        DtSaveProdSupp.Columns.Add(new DataColumn("OrderQty1", typeof(string)));
        DtSaveProdSupp.Columns.Add(new DataColumn("ValueQtySupp1", typeof(string)));
        DtSaveProdSupp.Columns.Add(new DataColumn("DateofLastSupp1", typeof(string)));
        DataRow drCurrentRowSaveCode = null;
        for (int i = 0; gvItemSuppliedbutnotproduced.Rows.Count > i; i++)
        {
            TextBox TextBox_1 = (TextBox)gvItemSuppliedbutnotproduced.Rows[i].Cells[1].FindControl("txtnameofrepcustomer1");
            TextBox TextBox_2 = (TextBox)gvItemSuppliedbutnotproduced.Rows[i].Cells[2].FindControl("txtdescofstoresupp1");
            TextBox TextBox_3 = (TextBox)gvItemSuppliedbutnotproduced.Rows[i].Cells[3].FindControl("txtsonoanddate1");
            TextBox TextBox_4 = (TextBox)gvItemSuppliedbutnotproduced.Rows[i].Cells[4].FindControl("txtorderqty1");
            TextBox TextBox_5 = (TextBox)gvItemSuppliedbutnotproduced.Rows[i].Cells[5].FindControl("txtvalueqtysupplied1");
            TextBox TextBox_6 = (TextBox)gvItemSuppliedbutnotproduced.Rows[i].Cells[6].FindControl("txtdateoflastsupplie1");
            if (TextBox_1.Text != "" && TextBox_2.Text != "")
            {
                drCurrentRowSaveCode = DtSaveProdSupp.NewRow();
                drCurrentRowSaveCode["SrNoSpplied1"] = i + 1;
                drCurrentRowSaveCode["NameCust1"] = TextBox_1.Text;
                drCurrentRowSaveCode["DesStoreSupp1"] = TextBox_2.Text;
                drCurrentRowSaveCode["OderNoorDate1"] = TextBox_3.Text;
                drCurrentRowSaveCode["OrderQty1"] = TextBox_4.Text;
                drCurrentRowSaveCode["ValueQtySupp1"] = TextBox_5.Text;
                drCurrentRowSaveCode["DateofLastSupp1"] = TextBox_6.Text;
                DtSaveProdSupp.Rows.Add(drCurrentRowSaveCode);
            }
        }
        ViewState["ProdSupp1"] = DtSaveProdSupp;
    }
    #endregion
    // Add grid of GovtUnderPSU 
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
    #endregion
    //Add Grid of Turn Over During Last 3 years
    #region Turn Over during last three year
    private void SetInitialRowTurnOverLast3Years()
    {
        //Create false table
        DataTable dtTurnOver = new DataTable();
        DataRow drTurnOver = null;
        dtTurnOver.Columns.Add(new DataColumn("SNo", typeof(string)));
        dtTurnOver.Columns.Add(new DataColumn("FinancialYear", typeof(string)));
        dtTurnOver.Columns.Add(new DataColumn("CurrentAsset", typeof(string)));
        dtTurnOver.Columns.Add(new DataColumn("CurrentLiblities", typeof(string)));
        dtTurnOver.Columns.Add(new DataColumn("ProfitLoss", typeof(string)));
        dtTurnOver.Columns.Add(new DataColumn("BalanceSheet", typeof(string)));
        drTurnOver = dtTurnOver.NewRow();
        drTurnOver["SNo"] = 1;
        drTurnOver["FinancialYear"] = string.Empty;
        drTurnOver["CurrentAsset"] = string.Empty;
        drTurnOver["CurrentLiblities"] = string.Empty;
        drTurnOver["ProfitLoss"] = string.Empty;
        drTurnOver["BalanceSheet"] = string.Empty;
        dtTurnOver.Rows.Add(drTurnOver);
        //Store the DataTable in ViewState or bind or show false grid
        ViewState["TurnOver"] = dtTurnOver;
        gvTURNOVERDURINGLAST3YEARS.DataSource = dtTurnOver;
        gvTURNOVERDURINGLAST3YEARS.DataBind();
    }
    private void AddNewRowToGridTurnOver()
    {
        int TOrowIndex = 0;
        if (ViewState["TurnOver"] != null)
        {
            DataTable dtCurrentTableTuOver = (DataTable)ViewState["TurnOver"];
            DataRow drCurrentRowNameTuOver = null;
            if (dtCurrentTableTuOver.Rows.Count > 0 && dtCurrentTableTuOver.Rows.Count <= 2)
            {
                for (int i = 1; i <= dtCurrentTableTuOver.Rows.Count; i++)
                {
                    //extract the TextBox values

                    TextBox TextBox1TO = (TextBox)gvTURNOVERDURINGLAST3YEARS.Rows[TOrowIndex].Cells[1].FindControl("txtfinyear");
                    TextBox TextBox2TO = (TextBox)gvTURNOVERDURINGLAST3YEARS.Rows[TOrowIndex].Cells[2].FindControl("txtcurrentasset");
                    TextBox TextBox3TO = (TextBox)gvTURNOVERDURINGLAST3YEARS.Rows[TOrowIndex].Cells[3].FindControl("txtcurrentlibilites");
                    TextBox TextBox4TO = (TextBox)gvTURNOVERDURINGLAST3YEARS.Rows[TOrowIndex].Cells[4].FindControl("txtprofitloss");
                    FileUpload FuTO = (FileUpload)gvTURNOVERDURINGLAST3YEARS.Rows[TOrowIndex].Cells[5].FindControl("fufileprofitloss");
                    HiddenField hfTurner = (HiddenField)gvTURNOVERDURINGLAST3YEARS.Rows[TOrowIndex].Cells[5].FindControl("hfprofitloss");
                    hfTurner.Value = FuTO.FileName.ToString();
                    drCurrentRowNameTuOver = dtCurrentTableTuOver.NewRow();
                    drCurrentRowNameTuOver["SNo"] = i + 1;
                    dtCurrentTableTuOver.Rows[i - 1]["FinancialYear"] = TextBox1TO.Text;
                    dtCurrentTableTuOver.Rows[i - 1]["CurrentAsset"] = TextBox2TO.Text;
                    dtCurrentTableTuOver.Rows[i - 1]["CurrentLiblities"] = TextBox3TO.Text;
                    dtCurrentTableTuOver.Rows[i - 1]["ProfitLoss"] = TextBox4TO.Text;
                    dtCurrentTableTuOver.Rows[i - 1]["BalanceSheet"] = hfTurner.Value;
                    TOrowIndex++;
                }
                dtCurrentTableTuOver.Rows.Add(drCurrentRowNameTuOver);
                ViewState["TurnOver"] = dtCurrentTableTuOver;
                gvTURNOVERDURINGLAST3YEARS.DataSource = dtCurrentTableTuOver;
                gvTURNOVERDURINGLAST3YEARS.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Max three record add.')", true);
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousDataGovtTurnOver();
    }
    private void SetPreviousDataGovtTurnOver()
    {
        int rowIndexTover = 0;
        if (ViewState["TurnOver"] != null)
        {
            DataTable dtGovrTurnOver = (DataTable)ViewState["TurnOver"];
            if (dtGovrTurnOver.Rows.Count > 0)
            {
                for (int i = 0; i < dtGovrTurnOver.Rows.Count; i++)
                {
                    TextBox TextBox_1 = (TextBox)gvTURNOVERDURINGLAST3YEARS.Rows[rowIndexTover].Cells[1].FindControl("txtfinyear");
                    TextBox TextBox_2 = (TextBox)gvTURNOVERDURINGLAST3YEARS.Rows[rowIndexTover].Cells[2].FindControl("txtcurrentasset");
                    TextBox TextBox_3 = (TextBox)gvTURNOVERDURINGLAST3YEARS.Rows[rowIndexTover].Cells[3].FindControl("txtcurrentlibilites");
                    TextBox TextBox_4 = (TextBox)gvTURNOVERDURINGLAST3YEARS.Rows[rowIndexTover].Cells[4].FindControl("txtprofitloss");
                    HiddenField hfprofloss = (HiddenField)gvTURNOVERDURINGLAST3YEARS.Rows[rowIndexTover].Cells[5].FindControl("hfprofitloss");
                    TextBox_1.Text = dtGovrTurnOver.Rows[i]["FinancialYear"].ToString();
                    TextBox_2.Text = dtGovrTurnOver.Rows[i]["CurrentAsset"].ToString();
                    TextBox_3.Text = dtGovrTurnOver.Rows[i]["CurrentLiblities"].ToString();
                    TextBox_4.Text = dtGovrTurnOver.Rows[i]["ProfitLoss"].ToString();
                    hfprofloss.Value = dtGovrTurnOver.Rows[i]["BalanceSheet"].ToString();
                    rowIndexTover++;
                }
            }
        }
    }
    protected void btnturnover_Click(object sender, EventArgs e)
    {
        AddNewRowToGridTurnOver();
    }
    protected void gvTURNOVERDURINGLAST3YEARS_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dtgridTOverrowcreated = (DataTable)ViewState["TurnOver"];
            LinkButton lbTurnOver = (LinkButton)e.Row.FindControl("lbturnover");
            if (lbTurnOver != null)
            {
                if (dtgridTOverrowcreated.Rows.Count > 1)
                {
                    if (e.Row.RowIndex == dtgridTOverrowcreated.Rows.Count - 1)
                    {
                        lbTurnOver.Visible = false;
                    }
                }
                else
                {
                    lbTurnOver.Visible = false;
                }
            }
        }
    }
    protected void lbturnover_Click(object sender, EventArgs e)
    {
        LinkButton lbto = (LinkButton)sender;
        GridViewRow gvRowto = (GridViewRow)lbto.NamingContainer;
        int rowID = gvRowto.RowIndex;
        if (ViewState["TurnOver"] != null)
        {
            DataTable dtremovegridto = (DataTable)ViewState["TurnOver"];
            if (dtremovegridto.Rows.Count > 1)
            {
                if (gvRowto.RowIndex < dtremovegridto.Rows.Count - 1)
                {
                    dtremovegridto.Rows.Remove(dtremovegridto.Rows[rowID]);
                    ResetRowIDTurnOver(dtremovegridto);
                }
            }
            ViewState["TurnOver"] = dtremovegridto;
            gvTURNOVERDURINGLAST3YEARS.DataSource = dtremovegridto;
            gvTURNOVERDURINGLAST3YEARS.DataBind();
        }
        SetPreviousDataGovtTurnOver();
    }
    private void ResetRowIDTurnOver(DataTable dtTOver)
    {
        int rowNumberTO = 1;
        if (dtTOver.Rows.Count > 0)
        {
            foreach (DataRow row in dtTOver.Rows)
            {
                row[0] = rowNumberTO;
                rowNumberTO++;
            }
        }
    }
    #endregion
    // Add Grid of Manufacturing Facilities
    #region Add Grid of Manufacturing Facilities
    private void SetInitialRowListofManufacturingFacilities()
    {
        //Create false table
        DataTable dtMF = new DataTable();
        DataRow drMF = null;
        dtMF.Columns.Add(new DataColumn("SNo", typeof(string)));
        dtMF.Columns.Add(new DataColumn("FactoryName", typeof(string)));
        dtMF.Columns.Add(new DataColumn("CAddress", typeof(string)));
        dtMF.Columns.Add(new DataColumn("COfficialName", typeof(string)));
        dtMF.Columns.Add(new DataColumn("TeleNo", typeof(string)));
        dtMF.Columns.Add(new DataColumn("FaxNo", typeof(string)));
        dtMF.Columns.Add(new DataColumn("EmailId", typeof(string)));
        drMF = dtMF.NewRow();
        drMF["SNo"] = 1;
        drMF["FactoryName"] = string.Empty;
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
    private void AddNewRowToGridManufacFacilities()
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
                    TextBox TextBox2MF = (TextBox)gvmanufacility.Rows[MFrowIndex].Cells[2].FindControl("txtCAddrssMF");
                    TextBox TextBox3MF = (TextBox)gvmanufacility.Rows[MFrowIndex].Cells[3].FindControl("txtofficialNameMF");
                    TextBox TextBox4MF = (TextBox)gvmanufacility.Rows[MFrowIndex].Cells[4].FindControl("txttelephonenoMF");
                    TextBox TextBox5MF = (TextBox)gvmanufacility.Rows[MFrowIndex].Cells[5].FindControl("txtfaxnoMF");
                    TextBox TextBox6MF = (TextBox)gvmanufacility.Rows[MFrowIndex].Cells[6].FindControl("txtemailidMF");
                    drCurrentRowMF = dtCurrentTableMF.NewRow();
                    drCurrentRowMF["SNo"] = i + 1;
                    dtCurrentTableMF.Rows[i - 1]["FactoryName"] = TextBox1MF.Text;
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
    private void SetPreviousDataGovtManuFacilities()
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
                    TextBox TextBox_2 = (TextBox)gvmanufacility.Rows[rowIndexMF].Cells[2].FindControl("txtCAddrssMF");
                    TextBox TextBox_3 = (TextBox)gvmanufacility.Rows[rowIndexMF].Cells[3].FindControl("txtofficialNameMF");
                    TextBox TextBox_4 = (TextBox)gvmanufacility.Rows[rowIndexMF].Cells[4].FindControl("txttelephonenoMF");
                    TextBox TextBox_5 = (TextBox)gvmanufacility.Rows[rowIndexMF].Cells[5].FindControl("txtfaxnoMF");
                    TextBox TextBox_6 = (TextBox)gvmanufacility.Rows[rowIndexMF].Cells[6].FindControl("txtemailidMF");
                    TextBox_1.Text = dtMF.Rows[i]["FactoryName"].ToString();
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
    protected void btnAddManufac_Click(object sender, EventArgs e)
    {
        AddNewRowToGridManufacFacilities();
    }
    protected void gvmanufacility_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void lblremovemanufac_Click(object sender, EventArgs e)
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
    private void ResetRowIDMFacilities(DataTable dtMfaci)
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
    #endregion
    // Add Grid of Area Details
    #region Add Grid of Area Details
    private void SetInitialRowArea()
    {
        //Create false table
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
    private void AddNewRowToGridArea()
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
    private void SetPreviousDataArea()
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
    protected void gvareadetail_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void btnAddArea_Click1(object sender, EventArgs e)
    {
        AddNewRowToGridArea();
    }
    protected void lblRemoveArea_Click1(object sender, EventArgs e)
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
    private void ResetRowIDArea(DataTable dtmArea)
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
    #endregion
    // Add Grid of List of All Plant and Machines
    #region Add Grid of List of All Plant and Machines
    private void SetInitialRowPM()
    {
        //Create false table
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
    private void AddNewRowToGridPM()
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
    private void SetPreviousDataPM()
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
    protected void gvplantandmachines_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void btnAddPlantorMachine_Click(object sender, EventArgs e)
    {
        AddNewRowToGridPM();
    }
    protected void lblRemovePlantMachine_Click(object sender, EventArgs e)
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
    private void ResetRowIDPM(DataTable dtmPlantM)
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
    #endregion
    // Add Grid to Emp List
    #region Add Grid to Emp List
    private void SetInitialRowEMPCI()
    {
        //Create false table
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
    private void AddNewRowToGridEMPCI()
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
    private void SetPreviousDataEMPCI()
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
    protected void gvempCompInfo_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void btnEmpInfo_Click(object sender, EventArgs e)
    {
        AddNewRowToGridEMPCI();
    }
    protected void lbRemoveEmp_Click(object sender, EventArgs e)
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
    private void ResetRowIDEMPCI(DataTable dtEmpCI)
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
    #endregion
    // Add Grid of List of Test Facilities
    #region  Grid of List of Test Facilities
    private void SetInitialRowTestFacili()
    {
        //Create false table
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
    private void AddNewRowToGridzTestFici()
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
    private void SetPreviousDataTestFici()
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
    protected void gvtestfacilities_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void btntestfacilities_Click(object sender, EventArgs e)
    {
        AddNewRowToGridzTestFici();
    }
    protected void lbRemovetestfacili_Click(object sender, EventArgs e)
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
    private void ResetRowIDTestFaci(DataTable dtTestFaci)
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
    #endregion
    // Add Grid of List  Authorise dealer
    #region Grid of List  Authorise dealer
    private void SetInitialRowAuthodel()
    {
        //Create false table
        DataTable dtAu = new DataTable();
        DataRow drAu = null;
        dtAu.Columns.Add(new DataColumn("SNo", typeof(string)));
        dtAu.Columns.Add(new DataColumn("DAddress", typeof(string)));
        dtAu.Columns.Add(new DataColumn("DState", typeof(string)));
        dtAu.Columns.Add(new DataColumn("DPincode", typeof(string)));
        dtAu.Columns.Add(new DataColumn("DPhone", typeof(string)));
        dtAu.Columns.Add(new DataColumn("DFax", typeof(string)));
        dtAu.Columns.Add(new DataColumn("DEmail", typeof(string)));
        drAu = dtAu.NewRow();
        drAu["SNo"] = 1;
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
    private void AddNewRowToGridAuthdel()
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
                    TextBox TextBox1DA = (TextBox)gvauthdealaddress.Rows[Index].Cells[1].FindControl("txtDstreetaddress");
                    TextBox TextBox2DA = (TextBox)gvauthdealaddress.Rows[Index].Cells[2].FindControl("txtdState");
                    TextBox TextBox3DA = (TextBox)gvauthdealaddress.Rows[Index].Cells[3].FindControl("txtDPincode");
                    TextBox TextBox4DA = (TextBox)gvauthdealaddress.Rows[Index].Cells[4].FindControl("txtDPhone");
                    TextBox TextBox5DA = (TextBox)gvauthdealaddress.Rows[Index].Cells[5].FindControl("txtDFax");
                    TextBox TextBox6DA = (TextBox)gvauthdealaddress.Rows[Index].Cells[6].FindControl("txtDEmail");
                    drCurrentDAut = dtCurrentDAut.NewRow();
                    drCurrentDAut["SNo"] = i + 1;
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
    private void SetPreviousDataDAuth()
    {
        int rowIndexDA = 0;
        if (ViewState["DAuth"] != null)
        {
            DataTable dtDA = (DataTable)ViewState["DAuth"];
            if (dtDA.Rows.Count > 0)
            {
                for (int i = 0; i < dtDA.Rows.Count; i++)
                {
                    TextBox TextBox_1 = (TextBox)gvauthdealaddress.Rows[rowIndexDA].Cells[1].FindControl("txtDstreetaddress");
                    TextBox TextBox_2 = (TextBox)gvauthdealaddress.Rows[rowIndexDA].Cells[2].FindControl("txtdState");
                    TextBox TextBox_3 = (TextBox)gvauthdealaddress.Rows[rowIndexDA].Cells[3].FindControl("txtDPincode");
                    TextBox TextBox_4 = (TextBox)gvauthdealaddress.Rows[rowIndexDA].Cells[4].FindControl("txtDPhone");
                    TextBox TextBox_5 = (TextBox)gvauthdealaddress.Rows[rowIndexDA].Cells[5].FindControl("txtDFax");
                    TextBox TextBox_6 = (TextBox)gvauthdealaddress.Rows[rowIndexDA].Cells[6].FindControl("txtDEmail");
                    TextBox_1.Text = dtDA.Rows[i]["DAddress"].ToString();
                    TextBox_2.Text = dtDA.Rows[i]["DState"].ToString();
                    TextBox_3.Text = dtDA.Rows[i]["DPincode"].ToString();
                    TextBox_4.Text = dtDA.Rows[i]["DPhone"].ToString();
                    TextBox_5.Text = dtDA.Rows[i]["DFax"].ToString();
                    TextBox_6.Text = dtDA.Rows[i]["DEmail"].ToString();
                    rowIndexDA++;
                }
            }
        }
    }
    protected void gvauthdealaddress_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void btnautdeal_Click(object sender, EventArgs e)
    {
        AddNewRowToGridAuthdel();
    }
    protected void lbRemoveAuthdel_Click(object sender, EventArgs e)
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
    private void ResetRowIDDA(DataTable dtDA)
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
    #endregion
    // Add Grid of List  Details of Outsourcing Facilites
    #region Grid of List  Details of Outsourcing Facilites
    private void SetInitialRowOF()
    {
        //Create false table
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
    private void AddNewRowToGridOF()
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
    private void SetPreviousDataOF()
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
    protected void gvoutsourcefacility_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void btnoutsourcefac_Click(object sender, EventArgs e)
    {
        AddNewRowToGridOF();
    }
    protected void lbRemoveOutfaci_Click(object sender, EventArgs e)
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
    private void ResetRowIDOF(DataTable dtOF)
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
    #endregion
    // Add Grid of List of Joint-Venture Facility
    #region Grid of List of Joint-Venture Facility
    private void SetInitialRowJVF()
    {
        //Create false table
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
    private void AddNewRowToGridJVF()
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
    private void SetPreviousDataJVF()
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
    protected void gvjointventure_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void btnjointven_Click(object sender, EventArgs e)
    {
        AddNewRowToGridJVF();
    }
    protected void lbRemovejointven_Click(object sender, EventArgs e)
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
    private void ResetRowIDJVF(DataTable dtJVF)
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
    #endregion
    //  Add Grid of Name and Address of Product OEM
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
    #endregion
    // Add Grid to Emp List
    #region Add Grid to Emp List
    private void SetInitialRowEMPCI1()
    {
        //Create false table
        DataTable dtEmp11 = new DataTable();
        DataRow drEmp11 = null;
        dtEmp11.Columns.Add(new DataColumn("SNo", typeof(string)));
        dtEmp11.Columns.Add(new DataColumn("TEmp1", typeof(string)));
        dtEmp11.Columns.Add(new DataColumn("Admins1", typeof(string)));
        dtEmp11.Columns.Add(new DataColumn("Tech1", typeof(string)));
        dtEmp11.Columns.Add(new DataColumn("NonTech1", typeof(string)));
        dtEmp11.Columns.Add(new DataColumn("QCIns1", typeof(string)));
        dtEmp11.Columns.Add(new DataColumn("SkLab1", typeof(string)));
        dtEmp11.Columns.Add(new DataColumn("USKLab1", typeof(string)));
        drEmp11 = dtEmp11.NewRow();
        drEmp11["SNo"] = 1;
        drEmp11["TEmp1"] = string.Empty;
        drEmp11["Admins1"] = string.Empty;
        drEmp11["Tech1"] = string.Empty;
        drEmp11["NonTech1"] = string.Empty;
        drEmp11["QCIns1"] = string.Empty;
        drEmp11["SkLab1"] = string.Empty;
        drEmp11["USKLab1"] = string.Empty;
        dtEmp11.Rows.Add(drEmp11);
        //Store the DataTable in ViewState or bind or show false grid
        ViewState["EMP1"] = dtEmp11;
        gvemp1.DataSource = dtEmp11;
        gvemp1.DataBind();
    }
    private void AddNewRowToGridEMPCI1()
    {
        int EMProwIndex1 = 0;
        if (ViewState["EMP1"] != null)
        {
            DataTable dtCurrentEMPCI1 = (DataTable)ViewState["EMP1"];
            DataRow drCurrentEMPCI1 = null;
            if (dtCurrentEMPCI1.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentEMPCI1.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox TextBox1E = (TextBox)gvemp1.Rows[EMProwIndex1].Cells[1].FindControl("txttotalempCI1");
                    TextBox TextBox2E = (TextBox)gvemp1.Rows[EMProwIndex1].Cells[2].FindControl("txtadministrativeCI1");
                    TextBox TextBox3E = (TextBox)gvemp1.Rows[EMProwIndex1].Cells[3].FindControl("txttechCI1");
                    TextBox TextBox4E = (TextBox)gvemp1.Rows[EMProwIndex1].Cells[4].FindControl("txtNontechCI1");
                    TextBox TextBox5E = (TextBox)gvemp1.Rows[EMProwIndex1].Cells[5].FindControl("txtqcCI1");
                    TextBox TextBox6E = (TextBox)gvemp1.Rows[EMProwIndex1].Cells[6].FindControl("txtskCI1");
                    TextBox TextBox7E = (TextBox)gvemp1.Rows[EMProwIndex1].Cells[7].FindControl("txtuLCI1");
                    drCurrentEMPCI1 = dtCurrentEMPCI1.NewRow();
                    drCurrentEMPCI1["SNo"] = i + 1;
                    dtCurrentEMPCI1.Rows[i - 1]["TEmp1"] = TextBox1E.Text;
                    dtCurrentEMPCI1.Rows[i - 1]["Admins1"] = TextBox2E.Text;
                    dtCurrentEMPCI1.Rows[i - 1]["Tech1"] = TextBox3E.Text;
                    dtCurrentEMPCI1.Rows[i - 1]["NonTech1"] = TextBox4E.Text;
                    dtCurrentEMPCI1.Rows[i - 1]["QCIns1"] = TextBox5E.Text;
                    dtCurrentEMPCI1.Rows[i - 1]["SkLab1"] = TextBox6E.Text;
                    dtCurrentEMPCI1.Rows[i - 1]["USKLab1"] = TextBox7E.Text;
                    EMProwIndex1++;
                }
                dtCurrentEMPCI1.Rows.Add(drCurrentEMPCI1);
                ViewState["EMP1"] = dtCurrentEMPCI1;
                gvemp1.DataSource = dtCurrentEMPCI1;
                gvemp1.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousDataEMP1();
    }
    private void SetPreviousDataEMP1()
    {
        int rowIndexEMP1 = 0;
        if (ViewState["EMP1"] != null)
        {
            DataTable dtEMPCI1 = (DataTable)ViewState["EMP1"];
            if (dtEMPCI1.Rows.Count > 0)
            {
                for (int i = 0; i < dtEMPCI1.Rows.Count; i++)
                {
                    TextBox TextBox_1 = (TextBox)gvemp1.Rows[rowIndexEMP1].Cells[1].FindControl("txttotalempCI1");
                    TextBox TextBox_2 = (TextBox)gvemp1.Rows[rowIndexEMP1].Cells[2].FindControl("txtadministrativeCI1");
                    TextBox TextBox_3 = (TextBox)gvemp1.Rows[rowIndexEMP1].Cells[3].FindControl("txttechCI1");
                    TextBox TextBox_4 = (TextBox)gvemp1.Rows[rowIndexEMP1].Cells[4].FindControl("txtNontechCI1");
                    TextBox TextBox_5 = (TextBox)gvemp1.Rows[rowIndexEMP1].Cells[5].FindControl("txtqcCI1");
                    TextBox TextBox_6 = (TextBox)gvemp1.Rows[rowIndexEMP1].Cells[6].FindControl("txtskCI1");
                    TextBox TextBox_7 = (TextBox)gvemp1.Rows[rowIndexEMP1].Cells[7].FindControl("txtuLCI1");
                    TextBox_1.Text = dtEMPCI1.Rows[i]["TEmp1"].ToString();
                    TextBox_2.Text = dtEMPCI1.Rows[i]["Admins1"].ToString();
                    TextBox_3.Text = dtEMPCI1.Rows[i]["Tech1"].ToString();
                    TextBox_4.Text = dtEMPCI1.Rows[i]["NonTech1"].ToString();
                    TextBox_5.Text = dtEMPCI1.Rows[i]["QCIns1"].ToString();
                    TextBox_6.Text = dtEMPCI1.Rows[i]["SkLab1"].ToString();
                    TextBox_7.Text = dtEMPCI1.Rows[i]["USKLab1"].ToString();
                    rowIndexEMP1++;
                }
            }
        }
    }
    protected void gvemp1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dtgridEMPCI1 = (DataTable)ViewState["EMP1"];
            LinkButton lbEMPCI1 = (LinkButton)e.Row.FindControl("lbempremove");
            if (lbEMPCI1 != null)
            {
                if (dtgridEMPCI1.Rows.Count > 1)
                {
                    if (e.Row.RowIndex == dtgridEMPCI1.Rows.Count - 1)
                    {
                        lbEMPCI1.Visible = false;
                    }
                }
                else
                {
                    lbEMPCI1.Visible = false;
                }
            }
        }
    }
    protected void btnempdetail_Click(object sender, EventArgs e)
    {
        AddNewRowToGridEMPCI1();
    }
    protected void lbempremove_Click(object sender, EventArgs e)
    {
        LinkButton lbEmp1 = (LinkButton)sender;
        GridViewRow gvRowEmp1 = (GridViewRow)lbEmp1.NamingContainer;
        int rowID = gvRowEmp1.RowIndex;
        if (ViewState["EMP1"] != null)
        {
            DataTable dtremoveEMP1 = (DataTable)ViewState["EMP1"];
            if (dtremoveEMP1.Rows.Count > 1)
            {
                if (gvRowEmp1.RowIndex < dtremoveEMP1.Rows.Count - 1)
                {
                    dtremoveEMP1.Rows.Remove(dtremoveEMP1.Rows[rowID]);
                    ResetRowIDEMP1(dtremoveEMP1);
                }
            }
            ViewState["EMP1"] = dtremoveEMP1;
            gvemp1.DataSource = dtremoveEMP1;
            gvemp1.DataBind();
        }
        SetPreviousDataEMP1();
    }
    private void ResetRowIDEMP1(DataTable dtEmp1)
    {
        int rowNumberEmp1 = 1;
        if (dtEmp1.Rows.Count > 0)
        {
            foreach (DataRow row in dtEmp1.Rows)
            {
                row[0] = rowNumberEmp1;
                rowNumberEmp1++;
            }
        }
    }
    #endregion
    // Add Grid of List of Test Facilities
    #region  Grid of List of Test Facilities
    private void SetInitialRowTestFacili1()
    {
        //Create false table
        DataTable dtTFacili1 = new DataTable();
        DataRow drTFacili1 = null;
        dtTFacili1.Columns.Add(new DataColumn("SNo", typeof(string)));
        dtTFacili1.Columns.Add(new DataColumn("TestEqip1", typeof(string)));
        dtTFacili1.Columns.Add(new DataColumn("TestEqipMake1", typeof(string)));
        dtTFacili1.Columns.Add(new DataColumn("TestLeastCount1", typeof(string)));
        dtTFacili1.Columns.Add(new DataColumn("Rangeofmeasur1", typeof(string)));
        dtTFacili1.Columns.Add(new DataColumn("CertificationYear1", typeof(string)));
        dtTFacili1.Columns.Add(new DataColumn("YearofPurchase1", typeof(string)));
        drTFacili1 = dtTFacili1.NewRow();
        drTFacili1["SNo"] = 1;
        drTFacili1["TestEqip1"] = string.Empty;
        drTFacili1["TestEqipMake1"] = string.Empty;
        drTFacili1["TestLeastCount1"] = string.Empty;
        drTFacili1["Rangeofmeasur1"] = string.Empty;
        drTFacili1["CertificationYear1"] = string.Empty;
        drTFacili1["YearofPurchase1"] = string.Empty;
        dtTFacili1.Rows.Add(drTFacili1);
        //Store the DataTable in ViewState or bind or show false grid
        ViewState["TestFaci1"] = dtTFacili1;
        gvtestfac1.DataSource = dtTFacili1;
        gvtestfac1.DataBind();
    }
    private void AddNewRowToGridzTestFici1()
    {
        int TFrowIndex1 = 0;
        if (ViewState["TestFaci1"] != null)
        {
            DataTable dtCurrentTFI1 = (DataTable)ViewState["TestFaci1"];
            DataRow drCurrentTFI1 = null;
            if (dtCurrentTFI1.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTFI1.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox TextBox1TF1 = (TextBox)gvtestfac1.Rows[TFrowIndex1].Cells[1].FindControl("txtnametestfesi1");
                    TextBox TextBox2TF1 = (TextBox)gvtestfac1.Rows[TFrowIndex1].Cells[2].FindControl("txtmaketf1");
                    TextBox TextBox3TF1 = (TextBox)gvtestfac1.Rows[TFrowIndex1].Cells[3].FindControl("txtcounttf1");
                    TextBox TextBox4TF1 = (TextBox)gvtestfac1.Rows[TFrowIndex1].Cells[4].FindControl("txtrangetf1");
                    TextBox TextBox5TF1 = (TextBox)gvtestfac1.Rows[TFrowIndex1].Cells[5].FindControl("txtcertiyeartf1");
                    TextBox TextBox6TF1 = (TextBox)gvtestfac1.Rows[TFrowIndex1].Cells[6].FindControl("txtyearofpurtf1");
                    drCurrentTFI1 = dtCurrentTFI1.NewRow();
                    drCurrentTFI1["SNo"] = i + 1;
                    dtCurrentTFI1.Rows[i - 1]["TestEqip1"] = TextBox1TF1.Text;
                    dtCurrentTFI1.Rows[i - 1]["TestEqipMake1"] = TextBox2TF1.Text;
                    dtCurrentTFI1.Rows[i - 1]["TestLeastCount1"] = TextBox3TF1.Text;
                    dtCurrentTFI1.Rows[i - 1]["Rangeofmeasur1"] = TextBox4TF1.Text;
                    dtCurrentTFI1.Rows[i - 1]["CertificationYear1"] = TextBox5TF1.Text;
                    dtCurrentTFI1.Rows[i - 1]["YearofPurchase1"] = TextBox6TF1.Text;
                    TFrowIndex1++;
                }
                dtCurrentTFI1.Rows.Add(drCurrentTFI1);
                ViewState["TestFaci1"] = dtCurrentTFI1;
                gvtestfac1.DataSource = dtCurrentTFI1;
                gvtestfac1.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousDataTestFici1();
    }
    private void SetPreviousDataTestFici1()
    {
        int rowIndexTF1 = 0;
        if (ViewState["TestFaci1"] != null)
        {
            DataTable dtPTF1 = (DataTable)ViewState["TestFaci1"];
            if (dtPTF1.Rows.Count > 0)
            {
                for (int i = 0; i < dtPTF1.Rows.Count; i++)
                {
                    TextBox TextBox_1 = (TextBox)gvtestfac1.Rows[rowIndexTF1].Cells[1].FindControl("txtnametestfesi1");
                    TextBox TextBox_2 = (TextBox)gvtestfac1.Rows[rowIndexTF1].Cells[2].FindControl("txtmaketf1");
                    TextBox TextBox_3 = (TextBox)gvtestfac1.Rows[rowIndexTF1].Cells[3].FindControl("txtcounttf1");
                    TextBox TextBox_4 = (TextBox)gvtestfac1.Rows[rowIndexTF1].Cells[4].FindControl("txtrangetf1");
                    TextBox TextBox_5 = (TextBox)gvtestfac1.Rows[rowIndexTF1].Cells[5].FindControl("txtcertiyeartf1");
                    TextBox TextBox_6 = (TextBox)gvtestfac1.Rows[rowIndexTF1].Cells[6].FindControl("txtyearofpurtf1");
                    TextBox_1.Text = dtPTF1.Rows[i]["TestEqip1"].ToString();
                    TextBox_2.Text = dtPTF1.Rows[i]["TestEqipMake1"].ToString();
                    TextBox_3.Text = dtPTF1.Rows[i]["TestLeastCount1"].ToString();
                    TextBox_4.Text = dtPTF1.Rows[i]["Rangeofmeasur1"].ToString();
                    TextBox_5.Text = dtPTF1.Rows[i]["CertificationYear1"].ToString();
                    TextBox_6.Text = dtPTF1.Rows[i]["YearofPurchase1"].ToString();
                    rowIndexTF1++;
                }
            }
        }
    }
    protected void gvtestfac1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dtgridTeFi1 = (DataTable)ViewState["TestFaci1"];
            LinkButton lbTF1 = (LinkButton)e.Row.FindControl("lbremovetestfacili");
            if (lbTF1 != null)
            {
                if (dtgridTeFi1.Rows.Count > 1)
                {
                    if (e.Row.RowIndex == dtgridTeFi1.Rows.Count - 1)
                    {
                        lbTF1.Visible = false;
                    }
                }
                else
                {
                    lbTF1.Visible = false;
                }
            }
        }
    }
    protected void btntestfaci_Click(object sender, EventArgs e)
    {
        AddNewRowToGridzTestFici1();
    }
    protected void lbremovetestfacili_Click(object sender, EventArgs e)
    {
        LinkButton lbTF1 = (LinkButton)sender;
        GridViewRow gvRowTF1 = (GridViewRow)lbTF1.NamingContainer;
        int rowID = gvRowTF1.RowIndex;
        if (ViewState["TestFaci1"] != null)
        {
            DataTable dtremoveTestFaci1 = (DataTable)ViewState["TestFaci1"];
            if (dtremoveTestFaci1.Rows.Count > 1)
            {
                if (gvRowTF1.RowIndex < dtremoveTestFaci1.Rows.Count - 1)
                {
                    dtremoveTestFaci1.Rows.Remove(dtremoveTestFaci1.Rows[rowID]);
                    ResetRowIDTestFaci1(dtremoveTestFaci1);
                }
            }
            ViewState["TestFaci1"] = dtremoveTestFaci1;
            gvtestfac1.DataSource = dtremoveTestFaci1;
            gvtestfac1.DataBind();
        }
        SetPreviousDataTestFici1();
    }
    private void ResetRowIDTestFaci1(DataTable dtTestFaci1)
    {
        int rowNumberTestFCI1 = 1;
        if (dtTestFaci1.Rows.Count > 0)
        {
            foreach (DataRow row in dtTestFaci1.Rows)
            {
                row[0] = rowNumberTestFCI1;
                rowNumberTestFCI1++;
            }
        }
    }
    #endregion
    #endregion
    #region Save Button and Save Function Code
    protected void SaveRegistration()
    {
        if (Mid != 0)
        { HySaveVendorRegisdetail["VendorDetailID"] = Mid; }
        else
        {
            HySaveVendorRegisdetail["VendorDetailID"] = Mid;
        }
        HySaveVendorRegisdetail["VendorRefNo"] = Enc.DecryptData(Session["VendorRefNo"].ToString());
        HySaveVendorRegisdetail["RegistrationCategory"] = Co.RSQandSQLInjection(ddlregiscategory.SelectedItem.Text, "soft");
        HySaveVendorRegisdetail["FirmCompanyName"] = Co.RSQandSQLInjection(txtbusinessname.Text.Trim(), "soft");
        HySaveVendorRegisdetail["TypeOfOwnership"] = Co.RSQandSQLInjection(ddltypeofbusiness.SelectedItem.Value, "soft");
        if (ddltypeofbusiness.SelectedItem.Text == "")
        {
            HySaveVendorRegisdetail["ScaleofBuisness"] = Co.RSQandSQLInjection(ddlscaleofbuisness.SelectedItem.Text, "soft");
            HySaveVendorRegisdetail["Ownership"] = Co.RSQandSQLInjection(chkownership.SelectedItem.Value, "soft");
            HySaveVendorRegisdetail["PercentofOwnership"] = Co.RSQandSQLInjection(txtpercent1.Text + "_" + txtpercent2.Text, "soft");
            HySaveVendorRegisdetail["FileofOwnership"] = Co.RSQandSQLInjection(fun.FileName, "soft");
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
        HySaveVendorRegisdetail["Street_Address"] = Co.RSQandSQLInjection(txtstreetaddress.Text, "soft");
        HySaveVendorRegisdetail["Street_Address_Line_2"] = Co.RSQandSQLInjection(txtstreetaddressline2.Text, "soft");
        HySaveVendorRegisdetail["City"] = Co.RSQandSQLInjection(txtcity.Text, "soft");
        HySaveVendorRegisdetail["State"] = Co.RSQandSQLInjection(txtstateprovince.Text, "soft");
        HySaveVendorRegisdetail["PinCode"] = Co.RSQandSQLInjection(txtpostalzipcode.Text, "soft");
        HySaveVendorRegisdetail["FirstName"] = Co.RSQandSQLInjection(txtfirstname.Text, "soft");
        HySaveVendorRegisdetail["MiddleName"] = Co.RSQandSQLInjection(txtmiddlename.Text, "soft");
        HySaveVendorRegisdetail["LastName"] = Co.RSQandSQLInjection(txtlastname.Text, "soft");
        HySaveVendorRegisdetail["Email"] = Co.RSQandSQLInjection(txtemail.Text, "soft");
        HySaveVendorRegisdetail["ContactNo"] = Co.RSQandSQLInjection(txtstdcode.Text + "_" + txtphoneno.Text, "soft");
        HySaveVendorRegisdetail["FaxNo"] = Co.RSQandSQLInjection(txtfaxstdcode.Text + "_" + txtfaxphoneno.Text, "soft");
        HySaveVendorRegisdetail["Type"] = "Save_FP";
        SaveCodeForNameof();
        SaveCodeProdDetails();
        SaveCodeTechDetails();
        SaveRawmeterial();
        SavePAS();
        SaveProdSupp();
        DataTable DtFIrstGrid = (DataTable)ViewState["EnterNameof"];
        DataTable DtSecondGrid = (DataTable)ViewState["ProductsDetails"];
        DataTable DtThirdGrid = (DataTable)ViewState["TechnologyDetails"];
        DataTable DtFourthGrid = (DataTable)ViewState["RawMeterialDetails"];
        DataTable DtFiveGrid = (DataTable)ViewState["ProdSupp"];
        DataTable DtSixGrid = (DataTable)ViewState["ProdSupp1"];
        string str = Lo.SaveVendorRegistrationDetails(HySaveVendorRegisdetail, DtFIrstGrid, DtSecondGrid, DtThirdGrid, DtFourthGrid, DtFiveGrid, DtSixGrid, out _sysMsg, out _msg);
        if (str != "")
        {
            cleartext();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Successfully save general information')", true);
        }
        else
        { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not saved.')", true); }

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        SaveRegistration();
    }
    protected void cleartext()
    {
        ddlregiscategory.SelectedIndex = -1;
        txtbusinessname.Text = "";
        ddltypeofbusiness.SelectedIndex = -1;
        ddlscaleofbuisness.SelectedIndex = -1;
        chkownership.ClearSelection();
        txtpercent1.Text = "";
        txtpercent2.Text = "";
        fun.Attributes.Clear();
        ddlbusinesssector.SelectedIndex = -1;
        txtdateofincorofthecompany.Text = "";
        txtstreetaddress.Text = "";
        txtstreetaddressline2.Text = "";
        txtcity.Text = "";
        txtstateprovince.Text = "";
        txtpostalzipcode.Text = "";
        txtfirstname.Text = "";
        txtmiddlename.Text = "";
        txtlastname.Text = "";
        txtemail.Text = "";
        txtstdcode.Text = "";
        txtphoneno.Text = "";
        txtfaxstdcode.Text = "";
        txtfaxphoneno.Text = "";
        ViewState["EnterName"] = null;
        gridNameof.DataSource = null;
        gridNameof.DataBind();
        SetInitialRow();
    }
    #endregion
}
