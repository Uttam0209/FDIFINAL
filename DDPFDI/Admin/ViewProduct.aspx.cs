using BusinessLayer;
using Encryption;
using System;
using System.Data;
using System.Drawing;
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
    private PagedDataSource pgsource = new PagedDataSource();
    private int firstindex, lastindex;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Type"] != null || Session["User"] != null)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString["id"] != null)
                {
                    try
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
                    catch (Exception ex)
                    {
                        string error = ex.ToString();
                        string Page = Request.Url.AbsolutePath.ToString();
                        Response.Redirect("Error?techerror=" + objEnc.EncryptData(error) + "&page=" + objEnc.EncryptData(Page));
                    }
                }
                else
                { Response.RedirectToRoute("login"); }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "ErrorMssgPopup('Session Expired,Please login again');window.location='Login'", true);
        }
    }
    protected void btnAddProduct_Click(object sender, EventArgs e)
    {
        string stridNew = Request.QueryString["id"].ToString().Replace(" ", "+");
        string mstrid = objEnc.EncryptData((objEnc.DecryptData(stridNew)));
        Response.Redirect("AddProduct?mu=" + objEnc.EncryptData("Panel") + "&id=" + mstrid);

    }
    #region Load
    protected void UpdateDtGridValue()
    {
        for (int a = 0; a < DtGrid.Rows.Count; a++)
        {
            if (DtGrid.Rows[a]["UCompany"].ToString() != "")
            {
                DtGrid.Rows[a]["CompanyName"] = DtGrid.Rows[a]["UCompany"];
                DtGrid.Rows[a]["FactoryName"] = DtGrid.Rows[a]["UFactory"];
                DtGrid.Rows[a]["CompanyRefNo"] = DtGrid.Rows[a]["UCompRefNo"];
                DtGrid.Rows[a]["FactoryRefNo"] = DtGrid.Rows[a]["UFactoryRefNo"];
            }
            else if (DtGrid.Rows[a]["FCompany"].ToString() != "")
            {
                DtGrid.Rows[a]["CompanyName"] = DtGrid.Rows[a]["FCompany"];
                DtGrid.Rows[a]["CompanyRefNo"] = DtGrid.Rows[a]["FCompRefNo"];
            }
        }
    }
    protected void BindGridView()
    {
        try
        {
            DtGrid = Lo.GetDashboardData("Product", "");
            if (DtGrid.Rows.Count > 0)
            {
                UpdateDtGridValue();
                DataView dv = new DataView(DtGrid)
                {
                    RowFilter = "CompanyRefNo='" + ddlcompany.SelectedItem.Value + "'"
                };
                DataTable dtnew = dv.ToTable();
                if (dtnew.Rows.Count > 0)
                {
                    if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.Visible == false || ddldivision.SelectedItem.Text == "Select")
                    {
                        dv.RowFilter = "CompanyName='" + ddlcompany.SelectedItem.Text + "'";
                    }
                    else if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.SelectedItem.Text != "Select" && ddlunit.Visible == false || ddlunit.SelectedItem.Text == "Select")
                    {
                        dv.RowFilter = "FactoryName='" + ddldivision.SelectedItem.Text + "'";
                    }
                    else if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.SelectedItem.Text != "Select" && ddlunit.SelectedItem.Text != "Select")
                    {
                        dv.RowFilter = "UnitName='" + ddlunit.SelectedItem.Text + "'";
                    }
                    else
                    {
                        dv.Sort = "LastUpdated desc,CompanyName asc,FactoryName asc";
                    }

                    DataTable dtads = dv.ToTable();
                    pgsource.DataSource = dtads.DefaultView;
                    pgsource.AllowPaging = true;
                    pgsource.PageSize = 100;
                    pgsource.CurrentPageIndex = pagingCurrentPage;
                    ViewState["totpage"] = pgsource.PageCount;
                    lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                    lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                    lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                    pgsource.DataSource = dtads.DefaultView;
                    gvproductItem.DataSource = pgsource;
                    gvproductItem.DataBind();
                    gvproductItem.Visible = true;
                    lbltot.Text = "Showing  " + gvproductItem.Rows.Count.ToString() + " result from page " + (pagingCurrentPage + 1) + " out of " + pgsource.PageCount + " pages";
                    divpageindex.Visible = true;
                }
                else
                {
                    gvproductItem.Visible = false;
                    divpageindex.Visible = false;
                    lbltot.Text = "";
                }
            }
            else
            {
                lbltot.Text = "";
                gvproductItem.Visible = false;
                divpageindex.Visible = false;
            }
        }
        catch (Exception ex)
        {
            string error = ex.ToString();
            string Page = Request.Url.AbsolutePath.ToString().Substring(1);
            Response.Redirect("Error?techerror=" + objEnc.EncryptData(error) + "&page=" + objEnc.EncryptData(Page));
        }
    }
    #endregion
    #region RowCommand
    private string mhiddenRefNo;
    private string EndUserValue;
    private string POProc;
    protected void gvproductItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditComp")
        {
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gvr.RowIndex;
            string Role = (gvproductItem.Rows[rowIndex].FindControl("hfrole") as HiddenField).Value;
            string CRefNo = (gvproductItem.Rows[rowIndex].FindControl("hfcomprefno") as HiddenField).Value;
            string FRefNo = (gvproductItem.Rows[rowIndex].FindControl("hfdivisionrefno") as HiddenField).Value;
            string URefNo = (gvproductItem.Rows[rowIndex].FindControl("hfunitrefno") as HiddenField).Value;
            if (URefNo != "")
            {
                mhiddenRefNo = URefNo.ToString();
            }
            else if (FRefNo != "")
            { mhiddenRefNo = FRefNo.ToString(); }
            else if (CRefNo != "" && FRefNo == "")
            { mhiddenRefNo = CRefNo.ToString(); }
            string stridNew = Request.QueryString["id"].ToString().Replace(" ", "+");
            string mstrid = objEnc.EncryptData((objEnc.DecryptData(stridNew) + " >> Edit Product"));
            Response.Redirect("AddProduct?mrcreaterole=" + HttpUtility.UrlEncode(objEnc.EncryptData(Role.Trim())) + "&mcurrentcompRefNo= " + HttpUtility.UrlEncode(objEnc.EncryptData(mhiddenRefNo.Trim())) + "&MProductRefNo=" + HttpUtility.UrlEncode((objEnc.EncryptData(e.CommandArgument.ToString().Trim()))) + "&id=" + mstrid.Trim());
        }
        else if (e.CommandName == "ViewComp")
        {
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gvr.RowIndex;
            string Role = (gvproductItem.Rows[rowIndex].FindControl("hfrole") as HiddenField).Value;
            DataTable DtView = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductMasterID", Role);
            if (DtView.Rows.Count > 0)
            {
                lblcomprefno.Text = DtView.Rows[0]["CompanyRefNo"].ToString();
                lblcompname.Text = DtView.Rows[0]["CompanyName"].ToString();
                lbldiviname.Text = DtView.Rows[0]["FactoryName"].ToString();
                lblunitname.Text = DtView.Rows[0]["UnitName"].ToString();
                lblprodrefno.Text = DtView.Rows[0]["ProductRefNo"].ToString();
                lblnsngroup.Text = DtView.Rows[0]["ProdLevel1Name"].ToString();
                lblnsngroupclass.Text = DtView.Rows[0]["ProdLevel2Name"].ToString();
                lblclassitem.Text = DtView.Rows[0]["ProdLevel3Name"].ToString();
                if (lblclassitem.Text != "")
                {
                    lblviewitemcode.Visible = true;
                }
                else
                {
                    lblviewitemcode.Visible = false;
                }
                lblnsccode.Text = DtView.Rows[0]["NSCCode"].ToString();
                lblniincode.Text = DtView.Rows[0]["NIINCode"].ToString();
                lblproductdescription.Text = DtView.Rows[0]["ProductDescription"].ToString();
                lbloempartnumber.Text = DtView.Rows[0]["OEMPartNumber"].ToString();
                lbloemname.Text = DtView.Rows[0]["OEMName"].ToString();
                lbloemcountry.Text = DtView.Rows[0]["CountryName"].ToString();
                lbldpsupartno.Text = DtView.Rows[0]["DPSUPartNumber"].ToString();
                lblhsncode8digit.Text = DtView.Rows[0]["HsnCode8digit"].ToString();
                DataTable dtenduser = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "EndUser", "");
                if (dtenduser.Rows.Count > 0)
                {
                    for (int i = 0; dtenduser.Rows.Count > i; i++)
                    {
                        EndUserValue = EndUserValue + ", " + dtenduser.Rows[i]["EndUser"].ToString();
                    }
                }
                if (EndUserValue != null)
                {
                    lblenduser.Text = EndUserValue.Substring(1).ToString();
                }
                //  lblenduser.Text = DtView.Rows[0]["EUserName"].ToString();
                lbldefenceplatform.Text = DtView.Rows[0]["PlatName"].ToString();
                lblnameofdefenceplatform.Text = DtView.Rows[0]["Nomenclature"].ToString();
                prodIndustryDomain.Text = DtView.Rows[0]["TechLevel1Name"].ToString();
                ProdIndusSubDomain.Text = DtView.Rows[0]["Techlevel2Name"].ToString();
                ProdIndus2SubDomain.Text = DtView.Rows[0]["Techlevel3Name"].ToString();
                lblsearchkeyword.Text = DtView.Rows[0]["SearchKeyword"].ToString();
                lblprodalredyindeginized.Text = DtView.Rows[0]["IsIndeginized"].ToString();
                if (lblprodalredyindeginized.Text == "Y")
                {
                    lblprodalredyindeginized.Text = "Yes";
                    tableIsIndiginized.Visible = true;
                    lblmanufacturename.Text = DtView.Rows[0]["ManufactureName"].ToString();
                    lblmanaddress.Text = DtView.Rows[0]["ManufactureAddress"].ToString();
                    lblyearofindiginization.Text = DtView.Rows[0]["FY"].ToString();
                }
                else
                {
                    lblprodalredyindeginized.Text = "No";
                    tableIsIndiginized.Visible = false;
                }
                lblisproductimported.Text = DtView.Rows[0]["IsProductImported"].ToString();
                if (lblisproductimported.Text == "Y")
                {
                    lblisproductimported.Text = "Yes";
                }
                else { lblisproductimported.Text = "No"; }
                lblyearofimport.Text = DtView.Rows[0]["YearofImport"].ToString();
                lblremarksproductimported.Text = DtView.Rows[0]["YearofImportRemarks"].ToString();
                if (DtView.Rows[0]["ItemDescriptionPDFFile"].ToString() == "")
                {
                    itemdocument.Visible = false;
                }
                else
                {
                    itemdocument.Visible = true;
                    a_downitem.HRef = "http://srijandefence.gov.in/Upload/" + DtView.Rows[0]["ItemDescriptionPDFFile"].ToString();
                }
                DataTable dtImageBind = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductImage", "");
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
                lblfeaturesanddetail.Text = DtView.Rows[0]["FeatursandDetail"].ToString();
                lblitemspecification.Text = DtView.Rows[0]["ItemSpecification"].ToString();
                DataTable dtProdInfo = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "RetriveProdInfo", "");
                if (dtProdInfo.Rows.Count > 0)
                {
                    gvProdInfo.DataSource = dtProdInfo;
                    gvProdInfo.DataBind();
                    gvProdInfo.Visible = true;
                }
                else
                {
                    gvProdInfo.Visible = false;
                }
                lbladditionalinfo.Text = DtView.Rows[0]["AdditionalDetail"].ToString();
                DataTable dtestimatequanorprice = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "EstimateQuanPrice", "");
                if (dtestimatequanorprice.Rows.Count > 0)
                {
                    gvestimatequanorprice.DataSource = dtestimatequanorprice;
                    gvestimatequanorprice.DataBind();
                    gvestimatequanorprice.Visible = true;
                }
                else
                {
                    gvestimatequanorprice.Visible = false;
                }
                DataTable dtPurposeofProcurement = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductPOP", "");
                if (dtPurposeofProcurement.Rows.Count > 0)
                {
                    for (int i = 0; dtPurposeofProcurement.Rows.Count > i; i++)
                    {
                        POProc = POProc + "," + dtPurposeofProcurement.Rows[i]["SCategoryName"].ToString();
                    }
                }
                if (POProc != null)
                {
                    lblpurposeofprocurement.Text = POProc.Substring(1).ToString();
                }
                lblprocremarks.Text = DtView.Rows[0]["ProcurmentCategoryRemark"].ToString();
                lbltendersubmission.Text = DtView.Rows[0]["TenderSubmition"].ToString();
                lbltenderstatus.Text = DtView.Rows[0]["TenderStatus"].ToString();
                if (lbltenderstatus.Text == "Live")
                {
                    if (DtView.Rows[0]["TenderFillDate"].ToString() != "")
                    {
                        DateTime tenderdate = Convert.ToDateTime(DtView.Rows[0]["TenderFillDate"].ToString());
                        string tDate = tenderdate.ToString("dd-MMM-yyyy");
                        lbltenderdate.Text = tDate.ToString();
                    }
                    lbltenderurl.Text = DtView.Rows[0]["TenderUrl"].ToString();
                    tenderstatus.Visible = true;
                }
                else
                {
                    tenderstatus.Visible = false;
                }
                string Nodel1Id = DtView.Rows[0]["NodelDetail"].ToString();
                if (Nodel1Id.ToString() != "")
                {
                    DataTable dtNodal = Lo.RetriveProductCode(Nodel1Id.ToString(), "", "ProdNodal", "");
                    if (dtNodal.Rows.Count > 0)
                    {
                        lblempcode.Text = dtNodal.Rows[0]["NodalOfficerRefNo"].ToString();
                        lblempname.Text = dtNodal.Rows[0]["NodalOficerName"].ToString();
                        lbldesignation.Text = dtNodal.Rows[0]["Designation"].ToString();
                        lblemailid.Text = dtNodal.Rows[0]["NodalOfficerEmail"].ToString();
                        lblmobilenumber.Text = dtNodal.Rows[0]["NodalOfficerMobile"].ToString();
                        lblphonenumber.Text = dtNodal.Rows[0]["NodalOfficerTelephone"].ToString();
                        lblfax.Text = dtNodal.Rows[0]["NodalOfficerFax"].ToString();
                    }
                }
                string Nodel2Id = DtView.Rows[0]["NodalDetail2"].ToString();
                if (Nodel2Id.ToString() != "")
                {
                    DataTable dtNodal2 = Lo.RetriveProductCode(Nodel2Id.ToString(), "", "ProdNodal", "");
                    if (dtNodal2.Rows.Count > 0)
                    {
                        lblempcode2.Text = dtNodal2.Rows[0]["NodalOfficerRefNo"].ToString();
                        lblempname2.Text = dtNodal2.Rows[0]["NodalOficerName"].ToString();
                        lbldesignation2.Text = dtNodal2.Rows[0]["Designation"].ToString();
                        lblemailid2.Text = dtNodal2.Rows[0]["NodalOfficerEmail"].ToString();
                        lblmobilenumber2.Text = dtNodal2.Rows[0]["NodalOfficerMobile"].ToString();
                        lblphonenumber2.Text = dtNodal2.Rows[0]["NodalOfficerTelephone"].ToString();
                        lblfax2.Text = dtNodal2.Rows[0]["NodalOfficerFax"].ToString();
                    }
                }
                Panel1.Visible = true;
                pan.Visible = false;
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
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "SuccessfullPop('Product deactive successfully.');window.location ='View-Product';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Record not deleted.')", true);
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Record not deleted.')", true);
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
                // code by gk to select indivisual division for the particular unit
                DataTable dt = Lo.RetriveMasterData(0, mRefNo, "Factory2", 0, "", "", "CompanyName");
                if (dt.Rows.Count > 0)
                {
                    ddldivision.SelectedValue = dt.Rows[0]["FactoryRefNo"].ToString();
                }
                //end code
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
                // code by gk to select indivisual division for the particular unit
                DataTable dt = Lo.RetriveMasterData(0, mRefNo, "Factory3", 0, "", "", "CompanyName");
                if (dt.Rows.Count > 0)
                {
                    ddldivision.SelectedValue = dt.Rows[0]["FactoryRefNo"].ToString();
                }
                //end code
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
                ddlunit.SelectedValue = mRefNo.ToString();
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
                lblselectunit.Visible = false;
                hidCompanyRefNo.Value = ddlcompany.SelectedItem.Value;
                hidType.Value = "Company";
                BindGridView();
            }
            else
            {
                ddldivision.Visible = false;
                lblselectdivison.Visible = false;
                gvproductItem.Visible = false;
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
                hidType.Value = "Division";
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
    protected void gvproductItem_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TableSection = TableRowSection.TableBody;
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.TableSection = TableRowSection.TableFooter;
        }
    }
    #region //------------------------pageindex code--------------//
    protected void lnkbtnPgPrevious_Click(object sender, EventArgs e)
    {
        pagingCurrentPage -= 1;
        BindGridView();
    }
    protected void lnkbtnPgNext_Click(object sender, EventArgs e)
    {
        pagingCurrentPage += 1;
        BindGridView();
    }
    protected void btngoto_Click(object sender, EventArgs e)
    {
        if (System.Text.RegularExpressions.Regex.IsMatch(txtpageno.Text, "[^0-9]"))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Please enter only number')", true);
        }
        else
        {
            int txtpage = Convert.ToInt32(txtpageno.Text) - 1;
            pagingCurrentPage = Convert.ToInt32(txtpage.ToString());
            BindGridView();
        }
    }
    private int pagingCurrentPage
    {
        get
        {
            if (ViewState["pagingCurrentPage"] == null)
            {
                return 0;
            }
            else
            {
                return ((int)ViewState["pagingCurrentPage"]);
            }
        }
        set
        {
            ViewState["pagingCurrentPage"] = value;
        }
    }
    //end page index---------------------------------------//
    #endregion
    protected void gvproductItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hfisapproved = (e.Row.FindControl("hfisaaproved") as HiddenField);
            if (hfisapproved.Value == "Y")
            {
                gvproductItem.BackColor = Color.Green;
            }
            else if (hfisapproved.Value == "N")
            {
                gvproductItem.BackColor = Color.Red;
            }
            if (objEnc.DecryptData(Session["Type"].ToString()) == "SuperAdmin")
            {
                LinkButton delpro = ((LinkButton)e.Row.FindControl("lbldel"));
                delpro.Visible = true;
            }
            else
            {
                LinkButton delpro = ((LinkButton)e.Row.FindControl("lbldel"));
                delpro.Visible = false;
            }
        }
    }
    protected void lblviewitemcode_Click(object sender, EventArgs e)
    {
        DataTable DtViewNewCode = Lo.RetrivenewcategortFIIG_No(lblclassitem.Text, "Found");
        if (DtViewNewCode.Rows.Count > 0)
        {
            gvproditemdetail.DataSource = DtViewNewCode;
            gvproditemdetail.DataBind();
            gvproditemdetail.Visible = true;
            Panel1.Visible = false;
            pan.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "divmodel2", "showPopup3();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('No Record Found')", true);
        }
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        pan.Visible = false;
        Panel1.Visible = true;
    }
}