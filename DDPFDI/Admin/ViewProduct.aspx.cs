using BusinessLayer;
using Encryption;
using System;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ViewProduct : System.Web.UI.Page
{
    private Logic Lo = new Logic();
    private DataUtility Co = new DataUtility();
    private Cryptography objEnc = new Cryptography();
    private DataTable DtGrid = new DataTable();
    private DataTable DtCompanyDDL = new DataTable();
    private string currentPage = "";
    private string mRefNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["Type"].ToString() != null)
                {
                    if (Request.QueryString["id"] != null)
                    {
                        string strid = Request.QueryString["id"].ToString().Replace(" ", "+");
                        string strPageName = objEnc.DecryptData(strid);
                        StringBuilder strheadPage = new StringBuilder();
                        strheadPage.Append("<ul class='breadcrumb'>");
                        string[] MCateg = strPageName.Split(new string[] { ">>" }, StringSplitOptions.RemoveEmptyEntries);
                        string MmCval = "";
                        for (int x = 0; x < MCateg.Length; x++)
                        {
                            MmCval = MCateg[x];
                            strheadPage.Append("<li class=''><span>" + MmCval + "</span></li>");
                        }

                        divHeadPage.InnerHtml = strheadPage.ToString();
                        strheadPage.Append("</ul");
                        currentPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
                        hidType.Value = objEnc.DecryptData(Session["Type"].ToString());
                        mRefNo = Session["CompanyRefNo"].ToString();
                        BindCompany();
                        BindGridView();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string error = ex.ToString();
            string Page = Request.Url.AbsolutePath.ToString();
            Response.Redirect("Error?techerror=" + objEnc.EncryptData(error) + "&page=" + objEnc.EncryptData(Page));
        }
    }
    protected void btnAddProduct_Click(object sender, EventArgs e)
    {
        string stridNew = Request.QueryString["id"].ToString().Replace(" ", "+");
        string mstrid = objEnc.EncryptData((objEnc.DecryptData(stridNew)));
        Response.Redirect("AddProduct?mu=" + objEnc.EncryptData("Panel") + "&id=" + mstrid);

    }
    #region Load
    protected void BindGridView(string sortExpression = null)
    {
        try
        {
            if (hidType.Value == "SuperAdmin" || hidType.Value == "Admin")
            {
                if (Request.QueryString["mu"] != null)
                {
                    if (objEnc.DecryptData(Request.QueryString["mu"].ToString().Replace(" ", "+")) == "View")
                    {
                        DtGrid = Lo.RetriveProductCode("", "", "ProductMaster", "All");
                        if (DtGrid.Rows.Count > 0)
                        {
                            gvproduct.DataSource = DtGrid;
                            gvproduct.DataBind();
                            gvproduct.Visible = true;
                        }
                    }
                    else
                    {
                        if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.Visible == false)
                        {
                            DtGrid = Lo.RetriveProductCode(ddlcompany.SelectedItem.Value, "", "CompanyProduct", "Company");
                        }
                        else if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.SelectedItem.Text != "Select")
                        {
                            DtGrid = Lo.RetriveProductCode(ddldivision.SelectedItem.Value, "", "CompanyProduct", "Division");
                        }
                        else if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.SelectedItem.Text != "Select" && ddlunit.SelectedItem.Text != "Select")
                        {
                            DtGrid = Lo.RetriveProductCode(ddlunit.SelectedItem.Value, "", "CompanyProduct", "Unit");
                        }
                        if (DtGrid.Rows.Count > 0)
                        {
                            gvproduct.DataSource = DtGrid;
                            gvproduct.DataBind();
                            gvproduct.Visible = true;
                        }
                        else
                        {
                            gvproduct.Visible = false;
                        }
                    }
                }
            }
            else if (hidType.Value == "Company")
            {
                if (ddlcompany.SelectedItem.Text != "Select")
                {
                    DtGrid = Lo.RetriveProductCode(ddlcompany.SelectedItem.Value, "", "CompanyProduct", hidType.Value);
                }
                if (DtGrid.Rows.Count > 0)
                {
                    gvproduct.DataSource = DtGrid;
                    gvproduct.DataBind();
                    gvproduct.Visible = true;
                }
                else
                {
                    gvproduct.Visible = false;
                }
            }
            else if (hidType.Value == "Factory" || hidType.Value == "Division")
            {
                if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.SelectedItem.Value != "Select")
                {
                    DtGrid = Lo.RetriveProductCode(ddldivision.SelectedItem.Value, "", "CompanyProduct", hidType.Value);
                }
                if (DtGrid.Rows.Count > 0)
                {
                    gvproduct.DataSource = DtGrid;
                    gvproduct.DataBind();
                    gvproduct.Visible = true;
                    ddlcompany.Enabled = false;
                }
                else
                {
                    ddldivision.Enabled = true;
                    gvproduct.Visible = false;
                }
            }
            else if (hidType.Value == "Unit")
            {
                if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.SelectedItem.Value != "Select" && ddlunit.SelectedItem.Text != "Select")
                {
                    DtGrid = Lo.RetriveProductCode(ddlunit.SelectedItem.Value, "", "CompanyProduct", hidType.Value);
                }
                if (DtGrid.Rows.Count > 0)
                {
                    gvproduct.DataSource = DtGrid;
                    gvproduct.DataBind();
                    gvproduct.Visible = true;
                }
                else
                {
                    gvproduct.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            string error = ex.ToString();
            string Page = Request.Url.AbsolutePath.ToString().Substring(1);
            Response.Redirect("Error?techerror=" + objEnc.EncryptData(error) + "&page=" + objEnc.EncryptData(Page));
        }
    }
    private string SortDirection
    {
        get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
        set { ViewState["SortDirection"] = value; }
    }
    #endregion
    #region PageIndex or Sorting
    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvproduct.PageIndex = e.NewPageIndex;
        BindGridView();
    }
    protected void OnSorting(object sender, GridViewSortEventArgs e)
    {
        BindGridView(e.SortExpression);
    }
    #endregion
    #region RowCommand
    private string stpsdq;
    private string Financial;
    private string Testing;
    private string Certification;
    protected void gvproduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditComp")
        {
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gvr.RowIndex;
            string Role = (gvproduct.Rows[rowIndex].FindControl("hfrole") as HiddenField).Value;
            string CRefNo = (gvproduct.Rows[rowIndex].FindControl("hfcomprefno") as HiddenField).Value;
            string stridNew = Request.QueryString["id"].ToString().Replace(" ", "+");
            string mstrid = objEnc.EncryptData((objEnc.DecryptData(stridNew) + " >> Edit Product"));
            Response.Redirect("AddProduct?mrcreaterole=" + HttpUtility.UrlEncode(objEnc.EncryptData(Role.Trim())) + "&mcurrentcompRefNo= " + HttpUtility.UrlEncode(objEnc.EncryptData(CRefNo.Trim())) + "&MProductRefNo=" + HttpUtility.UrlEncode((objEnc.EncryptData(e.CommandArgument.ToString().Trim()))) + "&id=" + mstrid.Trim());
        }
        else if (e.CommandName == "ViewComp")
        {
            if (ddlcompany.SelectedItem.Text == "Select")
            { hidType.Value = "All"; }
            else if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.Visible == false)
            { hidType.Value = "Company"; }
            else if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.SelectedItem.Text != "Select")
            { hidType.Value = "Division"; }
            else if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.SelectedItem.Text != "Select" && ddlunit.SelectedItem.Text != "Select")
            { hidType.Value = "Unit"; }
            DataTable DtView = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductMasterID", hidType.Value);
            if (DtView.Rows.Count > 0)
            {
                lblcomprefno.Text = DtView.Rows[0]["CompanyRefNo"].ToString();
                lblcompname.Text = DtView.Rows[0]["CompanyName"].ToString();
                lblprodrefno.Text = DtView.Rows[0]["ProductRefNo"].ToString();
                lblprodlevel1.Text = DtView.Rows[0]["ProdLevel1Name"].ToString();
                productlevel2.Text = DtView.Rows[0]["ProdLevel2Name"].ToString();
                lblprodlevel3.Text = DtView.Rows[0]["ProdLevel3Name"].ToString();
                lblnsccode.Text = DtView.Rows[0]["NSCCode"].ToString();
                lblniincode.Text = DtView.Rows[0]["NIINCode"].ToString();
                lblproductdescription.Text = DtView.Rows[0]["ProductDescription"].ToString();
                lbloempartnumber.Text = DtView.Rows[0]["OEMPartNumber"].ToString();
                lbloemname.Text = DtView.Rows[0]["OEMName"].ToString();
                lbloemcountry.Text = DtView.Rows[0]["CountryName"].ToString();
                lbldpsupartno.Text = DtView.Rows[0]["DPSUPartNumber"].ToString();
                lblenduserpartno.Text = DtView.Rows[0]["EndUserPartNumber"].ToString();
                lblhsncode.Text = DtView.Rows[0]["HSNCode"].ToString();
                //  lblnatocode.Text = DtView.Rows[0]["NatoCode"].ToString();
                // lblerprefno.Text = DtView.Rows[0]["ERPRefNo"].ToString();
                lbltechlevel1.Text = DtView.Rows[0]["TechLevel1Name"].ToString();
                lbltechlevel2.Text = DtView.Rows[0]["Techlevel2Name"].ToString();
                lbltechlevel3.Text = DtView.Rows[0]["Techlevel3Name"].ToString();
                lblplatform.Text = DtView.Rows[0]["PlatName"].ToString();
                lblnomenclatureofmainsystem.Text = DtView.Rows[0]["Nomenclature"].ToString();
                lblenduser.Text = DtView.Rows[0]["EUserName"].ToString();
                lblpurposeofprocurement.Text = DtView.Rows[0]["PRrocurement"].ToString();
                // lblprodtimeframe.Text = DtView.Rows[0]["PRequirement"].ToString();
                lblsearchkeyword.Text = DtView.Rows[0]["SearchKeyword"].ToString();
                lblprodalredyindeginized.Text = DtView.Rows[0]["IsIndeginized"].ToString();
                if (lblprodalredyindeginized.Text == "Y")
                {
                    lblprodalredyindeginized.Text = "Yes";
                    tablemanufacturename.Visible = true;
                    tablemanufacturename1.Visible = true;
                    tablemanufactureAddress.Visible = true;
                    tablemanufactureYear.Visible = true;
                    tablemanufacturename1.Visible = true;
                    tablemanufacturename2.Visible = true;
                    tablemanufacturename3.Visible = true;
                    lblmanufacturename.Text = DtView.Rows[0]["ManufactureName"].ToString();
                    lblmanaddress.Text = DtView.Rows[0]["ManufactureAddress"].ToString();
                    lblyearofindiginization.Text = DtView.Rows[0]["YearofIndiginization"].ToString();
                }
                else
                {
                    lblprodalredyindeginized.Text = "No";
                    tablemanufacturename1.Visible = false;
                    tablemanufacturename.Visible = false;
                    tablemanufactureAddress.Visible = false;
                    tablemanufactureYear.Visible = false;
                    tablemanufacturename1.Visible = false;
                    tablemanufacturename2.Visible = false;
                    tablemanufacturename3.Visible = false;
                }
                DataTable dtImageBind = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductImage", hidType.Value);
                if (dtImageBind.Rows.Count > 0)
                {
                    dlimage.DataSource = dtImageBind;
                    dlimage.DataBind();
                    dlimage.Visible = true;
                }
                else
                {
                    dlimage.Visible = false;
                }
                DataTable dtpsdq = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductPSDQ", hidType.Value);
                if (dtpsdq.Rows.Count > 0)
                {
                    for (int i = 0; dtpsdq.Rows.Count > i; i++)
                    {
                        stpsdq = stpsdq + "," + dtpsdq.Rows[i]["SCategoryName"].ToString();
                    }
                }
                if (stpsdq != null)
                {
                    lblsupportprovidedbydpsu.Text = stpsdq.Substring(1).ToString();
                }
                lblremarks.Text = DtView.Rows[0]["Remarks"].ToString();
                DataTable dtFinn = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductFinancial", hidType.Value);
                if (dtFinn.Rows.Count > 0)
                {
                    for (int i = 0; dtFinn.Rows.Count > i; i++)
                    {
                        Financial = Financial + "," + dtFinn.Rows[i]["SCategoryName"].ToString();
                    }
                }
                if (Financial != null)
                {
                    lblfinancial.Text = Financial.Substring(1).ToString();
                }
                lblfinancialRemark.Text = DtView.Rows[0]["FinancialRemark"].ToString();
                lblestimatedquantity.Text = DtView.Rows[0]["Estimatequantity"].ToString();
                lblestimatedprice.Text = DtView.Rows[0]["EstimatePriceLLP"].ToString();
                lbltenderstatus.Text = DtView.Rows[0]["TenderStatus"].ToString();
                string tensub = DtView.Rows[0]["TenderSubmition"].ToString();
                if (tensub.ToString() == "Y")
                {
                    lbltendersubmission.Text = "Yes";
                }
                else
                {
                    lbltendersubmission.Text = "No";
                }
                if (DtView.Rows[0]["TenderFillDate"].ToString() != "")
                {
                    DateTime tenderdate = Convert.ToDateTime(DtView.Rows[0]["TenderFillDate"].ToString());
                    string tDate = tenderdate.ToString("dd-MMM-yyyy");
                    lbltenderdate.Text = tDate.ToString();
                }
                lbltenderurl.Text = DtView.Rows[0]["TenderUrl"].ToString();
                DataTable dtNodal = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductNodal", hidType.Value);
                if (dtNodal.Rows.Count > 0)
                {
                    lblempcode.Text = dtNodal.Rows[0]["NodalEmpCode"].ToString();
                    lbldesignation.Text = dtNodal.Rows[0]["Designation"].ToString();
                    lblemailid.Text = dtNodal.Rows[0]["NodalOfficerEmail"].ToString();
                    lblmobilenumber.Text = dtNodal.Rows[0]["NodalOfficerMobile"].ToString();
                    lblphonenumber.Text = dtNodal.Rows[0]["NodalOfficerTelephone"].ToString();
                    lblfax.Text = dtNodal.Rows[0]["NodalOfficerFax"].ToString();

                    if (dtNodal.Rows.Count == 2)
                    {
                        tablenodal2.Visible = true;
                        lblempcode2.Text = dtNodal.Rows[1]["NodalEmpCode"].ToString();
                        lbldesignation2.Text = dtNodal.Rows[1]["Designation"].ToString();
                        lblemailid2.Text = dtNodal.Rows[1]["NodalOfficerEmail"].ToString();
                        lblmobileno2.Text = dtNodal.Rows[1]["NodalOfficerMobile"].ToString();
                        lblphoneno2.Text = dtNodal.Rows[1]["NodalOfficerTelephone"].ToString();
                        lblfax2.Text = dtNodal.Rows[1]["NodalOfficerFax"].ToString();
                    }
                    else
                    {
                        tablenodal2.Visible = false;
                    }
                }
                DataTable dttesting = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductTesting", hidType.Value);
                if (dttesting.Rows.Count > 0)
                {
                    for (int i = 0; dttesting.Rows.Count > i; i++)
                    {
                        Testing = Testing + "," + dttesting.Rows[i]["SCategoryName"].ToString();
                    }
                }
                if (Testing != null)
                {
                    lbltesting.Text = Testing.Substring(1).ToString();
                }
                lbltestingremarks.Text = DtView.Rows[0]["Remarks"].ToString();
                DataTable dtcertification = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductCertification", hidType.Value);
                if (dtcertification.Rows.Count > 0)
                {
                    for (int i = 0; dtcertification.Rows.Count > i; i++)
                    {
                        Certification = Certification + "," + dtcertification.Rows[i]["SCategoryName"].ToString();
                    }
                }
                if (Certification != null)
                {
                    lblcertification.Text = Certification.Substring(1).ToString();
                }
                lblcertificationremarks.Text = DtView.Rows[0]["Remarks"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "changePass", "showPopup();", true);
            }
        }
        else if (e.CommandName == "DeleteComp")
        {
            try
            {
                string DeleteRec = Lo.DeleteRecord(e.CommandArgument.ToString(), "InActiveProd");
                if (DeleteRec == "true")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('User details saved sucessfully');window.location ='View-Product';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not deleted.')", true);
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not deleted.')", true);
            }
        }
    }
    #endregion
    #region ReturnUrl Long"
    public string Resturl(int length)
    {
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        StringBuilder res = new StringBuilder();
        Random rnd = new Random();
        while (0 < length--)
        {
            res.Append(valid[rnd.Next(valid.Length)]);
        }
        return res.ToString();
    }
    #endregion
    #region DropDownList
    protected void BindCompany()
    {
        if (hidType.Value == "SuperAdmin" || hidType.Value == "Admin")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, "", "", 0, "", "", "Select");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Items.Insert(0, "Select");
                ddlcompany.Enabled = true;
            }
            else
            {
                ddlcompany.Enabled = false;
            }

            lblselectdivison.Visible = false;
            lblselectunit.Visible = false;
        }
        else if (hidType.Value == "Company")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = false;
            }
            else
            {
                ddlcompany.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                ddldivision.Items.Insert(0, "Select");
                if (hidType.Value == "Company")
                {
                    lblselectdivison.Visible = true;
                    ddldivision.Enabled = true;
                    ddlunit.Visible = false;
                    lblselectunit.Visible = false;
                }
                else
                {
                    ddldivision.Enabled = false;
                }
            }
            else
            {
                lblselectdivison.Visible = false;
                lblselectunit.Visible = false;
            }
        }
        else if (hidType.Value == "Factory" || hidType.Value == "Division")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = false;
            }
            else
            {
                ddlcompany.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                lblselectdivison.Visible = true;
                ddldivision.Enabled = false;
            }
            else
            {
                ddldivision.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "Unit1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
                ddlunit.Items.Insert(0, "Select");
                ddlunit.Enabled = true;
                lblselectunit.Visible = true;
            }
            else
            {
                lblselectunit.Visible = false;
            }
        }
        else if (hidType.Value == "Unit")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company2", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = false;
            }
            else
            {
                ddlcompany.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                lblselectdivison.Visible = true;
                ddldivision.Enabled = false;
            }
            else
            {
                ddldivision.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "Unit1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
                ddlunit.Enabled = false;
                lblselectunit.Visible = true;
            }
            else
            {
                ddlunit.Enabled = false;
            }
        }
    }
    protected void ddlcompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcompany.SelectedItem.Text != "Select")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "", 0, "", "", "FactoryCompanyID");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                ddldivision.Items.Insert(0, "Select");
                lblselectdivison.Visible = true;
                ddldivision.Visible = true;
                hidCompanyRefNo.Value = ddlcompany.SelectedItem.Value;
                hidType.Value = "Company";
                BindGridView();
            }
            else
            {
                ddldivision.Visible = false;
                lblselectdivison.Visible = false;
                gvproduct.Visible = false;
                BindGridView();
            }
        }
        else if (ddlcompany.SelectedItem.Text == "Select")
        {
            lblselectdivison.Visible = false;
            lblselectunit.Visible = false;
            BindGridView();
        }
        hfcomprefno.Value = "";
        hfcomprefno.Value = ddlcompany.SelectedItem.Value;

    }
    protected void ddldivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //if (hidType.Value == "SuperAdmin" || hidType.Value == "Admin")
        //{
        //    if (ddldivision.SelectedItem.Text != "Select")
        //    {
        //        DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "", 0, "", "", "UnitSelectID");
        //        if (DtCompanyDDL.Rows.Count > 0)
        //        {
        //            Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
        //            ddlunit.Items.Insert(0, "Select");
        //            ddlunit.Visible = true;
        //            lblselectunit.Visible = true;
        //            if (ddlunit.SelectedItem.Text == "Select")
        //            {
        //                ddldivision.Enabled = true;
        //            }
        //            else
        //            { ddldivision.Enabled = false; }
        //            hidCompanyRefNo.Value = ddldivision.SelectedItem.Value;
        //            hidType.Value = "Factory";
        //            BindGridView();
        //        }
        //        else
        //        {
        //            lblselectunit.Visible = false;
        //            ddlunit.Visible = false;
        //        }
        //        hfcomprefno.Value = "";
        //        hfcomprefno.Value = ddldivision.SelectedItem.Value;
        //    }
        //    else if (ddldivision.SelectedItem.Text == "Select")
        //    {
        //        ddlcompany.Enabled = true;
        //        lblselectunit.Visible = false;
        //        hidCompanyRefNo.Value = ddlcompany.SelectedItem.Value;
        //        hidType.Value = "Company";
        //        hfcomprefno.Value = "";
        //        hfcomprefno.Value = ddlcompany.SelectedItem.Value;
        //        BindGridView();
        //    }
        //}
        //else
        //{
        if (ddldivision.SelectedItem.Text != "Select")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "", 0, "", "", "UnitSelectID");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
                ddlunit.Items.Insert(0, "Select");
                ddlunit.Visible = true;
                lblselectunit.Visible = true;
                if (ddlunit.SelectedItem.Text == "Select")
                {
                    ddldivision.Enabled = true;
                }
                else
                { ddldivision.Enabled = false; }
                hidCompanyRefNo.Value = ddldivision.SelectedItem.Value;
                hidType.Value = "Division";
                BindGridView();
            }
            else
            {
                lblselectunit.Visible = false;
                ddlunit.Visible = false;
                BindGridView();
            }
            hfcomprefno.Value = "";
            hfcomprefno.Value = ddldivision.SelectedItem.Value;
        }
        else if (ddldivision.SelectedItem.Text == "Select")
        {
            ddlcompany.Enabled = false;
            lblselectunit.Visible = false;
            hidCompanyRefNo.Value = ddlcompany.SelectedItem.Value;
            hidType.Value = "Company";
            hfcomprefno.Value = "";
            hfcomprefno.Value = ddlcompany.SelectedItem.Value;
            BindGridView();
        }
    }
    protected void ddlunit_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlunit.SelectedItem.Text != "Select")
        {
            hidCompanyRefNo.Value = ddlunit.SelectedItem.Value;
            hidType.Value = "Unit";
            hfcomprefno.Value = "";
            hfcomprefno.Value = ddlunit.SelectedItem.Value;
            BindGridView();
        }
        else
        {
            hidCompanyRefNo.Value = ddldivision.SelectedItem.Value;
            hidType.Value = "Division";
            if (hidType.Value == "Unit")
            {
                ddldivision.Enabled = false;
            }
            else
            {
                ddldivision.Enabled = true;
            }
            hfcomprefno.Value = "";
            hfcomprefno.Value = ddldivision.SelectedItem.Value;
            BindGridView();
        }

    }
    #endregion
}