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
                string mProdRefesti = "";
                if (dtnew.Rows.Count > 0)
                {
                    if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.Visible == false || ddldivision.SelectedItem.Text == "Select")
                    {
                        if (txtsearchbyrefid.Text != "")
                        {
                            dv.RowFilter = "ProductRefNo='" + txtsearchbyrefid.Text + "' and CompanyName='" + ddlcompany.SelectedItem.Text + "'";
                            mProdRefesti = ddlcompany.SelectedItem.Value;
                        }
                        else
                        {
                            dv.RowFilter = "CompanyName='" + ddlcompany.SelectedItem.Text + "'";
                            mProdRefesti = ddlcompany.SelectedItem.Value;
                        }
                    }
                    else if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.SelectedItem.Text != "Select" && ddlunit.Visible == false || ddlunit.SelectedItem.Text == "Select")
                    {
                        if (txtsearchbyrefid.Text != "")
                        {
                            dv.RowFilter = "ProductRefNo='" + txtsearchbyrefid.Text + "' and FactoryName='" + ddldivision.SelectedItem.Text + "'";
                            mProdRefesti = ddldivision.SelectedItem.Value;
                        }
                        else
                        {
                            dv.RowFilter = "FactoryName='" + ddldivision.SelectedItem.Text + "'";
                            mProdRefesti = ddldivision.SelectedItem.Value;
                        }
                    }
                    else if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.SelectedItem.Text != "Select" && ddlunit.SelectedItem.Text != "Select")
                    {
                        if (txtsearchbyrefid.Text != "")
                        {
                            dv.RowFilter = "ProductRefNo='" + txtsearchbyrefid.Text + "' and UnitName='" + ddlunit.SelectedItem.Text + "'";
                            mProdRefesti = ddlunit.SelectedItem.Value;
                        }
                        else
                        {
                            dv.RowFilter = "UnitName='" + ddlunit.SelectedItem.Text + "'";
                            mProdRefesti = ddlunit.SelectedItem.Value;
                        }
                    }
                    else
                    {
                        dv.Sort = "LastUpdated desc,CompanyName asc,FactoryName asc";
                    }
                    DataTable dtads = dv.ToTable();
                    lbltotfilter.Text = dtads.Rows.Count.ToString();
                    dtads.Columns.Add("1718", typeof(decimal));
                    dtads.Columns.Add("1819", typeof(decimal));
                    dtads.Columns.Add("1920", typeof(decimal));
                    dtads.Columns.Add("2021", typeof(decimal));
                    for (int i = 0; dtads.Rows.Count > i; i++)
                    {
                        DataTable dtEstimate1 = Lo.RetriveProductCode("", dtads.Rows[i]["ProductRefNo"].ToString(), "estimate", "");
                        if (dtEstimate1.Rows.Count > 0)
                        {
                            for (int es = 0; dtEstimate1.Rows.Count > es; es++)
                            {
                                if (dtEstimate1.Rows[es]["FYear"].ToString() == "2017-18" && dtEstimate1.Rows[es]["Type"].ToString() == "O")
                                {
                                    dtads.Rows[i]["1718"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                                }
                                if (dtEstimate1.Rows[es]["FYear"].ToString() == "2018-19" && dtEstimate1.Rows[es]["Type"].ToString() == "O")
                                {
                                    dtads.Rows[i]["1819"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                                }
                                if (dtEstimate1.Rows[es]["FYear"].ToString() == "2019-20" && dtEstimate1.Rows[es]["Type"].ToString() == "O")
                                {
                                    dtads.Rows[i]["1920"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                                }
                                if (dtEstimate1.Rows[es]["FYear"].ToString() == "2020-21" && dtEstimate1.Rows[es]["Type"].ToString() == "F")
                                {
                                    dtads.Rows[i]["2021"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                                }
                            }
                        }
                    }
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
                    lbltotalshowpageitem.Text = pgsource.FirstIndexInPage + 1 + " - " + (pgsource.FirstIndexInPage + pgsource.Count);
                    divpageindex.Visible = true;
                    divproductgridview.Visible = true;
                }
                else
                {
                    gvproductItem.Visible = false;
                    divpageindex.Visible = false;
                    lbltotalshowpageitem.Text = "";
                    divproductgridview.Visible = false;
                }
            }
            else
            {
                lbltotalshowpageitem.Text = "";
                gvproductItem.Visible = false;
                divpageindex.Visible = false;
                divproductgridview.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(Something went wrong please refresh page. " + ex.Message + ")", true);

        }
    }
    #endregion
    #region RowCommand
    private string mhiddenRefNo;
    private string EndUserValue;
    private string POProc;
    protected void gvproductItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region edit
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
        #endregion
        #region ViewOneProd
        if (e.CommandName == "ViewComp")
        {
            try
            {
                GridViewRow item = (GridViewRow)(((Control)(e.CommandSource)).NamingContainer);
                string Role = ((HiddenField)item.FindControl("hfrole")).Value;
                DataTable DtView = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductMasterID", Role.ToString());
                if (DtView.Rows.Count > 0)
                {
                    lblrefnoview.Text = e.CommandArgument.ToString();
                    lblcompname.Text = DtView.Rows[0]["CompanyName"].ToString();
                    if (DtView.Rows[0]["FactoryName"].ToString() != "")
                    {
                        lbldiviname.Text = DtView.Rows[0]["FactoryName"].ToString();
                        one.Visible = true;
                    }
                    else
                    {
                        one.Visible = false;
                    }
                    if (DtView.Rows[0]["UnitName"].ToString() != "")
                    {
                        lblunitnamepro.Text = DtView.Rows[0]["UnitName"].ToString();
                        two.Visible = true;
                    }
                    else
                    {
                        two.Visible = false;
                    }
                    lblnsngroup.Text = DtView.Rows[0]["ProdLevel1Name"].ToString();
                    lblnsngroupclass.Text = DtView.Rows[0]["ProdLevel2Name"].ToString();
                    lblclassitem.Text = DtView.Rows[0]["ProdLevel3Name"].ToString();
                    lblsearchkeywords.Text = DtView.Rows[0]["Searchkeyword"].ToString();
                    if (DtView.Rows[0]["ProductDescription"].ToString() != "")
                    {
                        itemname2.Text = DtView.Rows[0]["ProductDescription"].ToString();
                        lblitemname1.Text = DtView.Rows[0]["ProductDescription"].ToString();
                        eleven.Visible = true;
                        Tr23.Visible = true;

                    }
                    else
                    {
                        Tr23.Visible = false;
                        eleven.Visible = false;
                    }
                    if (DtView.Rows[0]["DPSUPartNumber"].ToString() != "")
                    {
                        lbldpsupartno.Text = DtView.Rows[0]["DPSUPartNumber"].ToString();
                        three.Visible = true;
                    }
                    else
                    {
                        three.Visible = false;
                    }
                    if (DtView.Rows[0]["HsnCode8digit"].ToString() != "")
                    {
                        lblhsncode8digit.Text = DtView.Rows[0]["HsnCode8digit"].ToString();
                        four.Visible = true;
                    }
                    else
                    {
                        four.Visible = false;
                    }
                    prodIndustryDomain.Text = DtView.Rows[0]["TechLevel1Name"].ToString();
                    ProdIndusSubDomain.Text = DtView.Rows[0]["Techlevel2Name"].ToString();
                    if (DtView.Rows[0]["IsProductImported"].ToString() != "")
                    {
                        five.Visible = true;
                    }
                    else
                    {
                        five.Visible = false;
                    }
                    if (DtView.Rows[0]["NSCCode"].ToString() != "")
                    {
                        lblnsccode4digit.Text = DtView.Rows[0]["NSCCode"].ToString();
                        six.Visible = true;
                    }
                    else
                    { six.Visible = false; }
                    if (DtView.Rows[0]["CountryName"].ToString() != "")
                    {
                        lbloemcountry.Text = DtView.Rows[0]["CountryName"].ToString();
                        nine.Visible = true;
                    }
                    else
                    { nine.Visible = false; }
                    if (DtView.Rows[0]["OEMName"].ToString() != "")
                    {
                        lbloemname.Text = DtView.Rows[0]["OEMName"].ToString();
                        seven.Visible = true;
                    }
                    else
                    { seven.Visible = false; }
                    if (DtView.Rows[0]["OEMPartNumber"].ToString() != "")
                    {
                        lbloempartno.Text = DtView.Rows[0]["OEMPartNumber"].ToString();
                        eight.Visible = true;
                    }
                    else
                    { eight.Visible = false; }
                    if (DtView.Rows[0]["OEMAddress"].ToString() != "")
                    {
                        lbloemaddress.Text = DtView.Rows[0]["OEMAddress"].ToString();
                        twentyfive.Visible = true;
                    }
                    else
                    { twentyfive.Visible = false; }
                    DataTable DtGridEstimate1 = new DataTable();
                    DtGridEstimate1 = Lo.RetriveSaveEstimateGrid("Select", 0, e.CommandArgument.ToString(), 0, "", "", "", "", "O");
                    if (DtGridEstimate1.Rows.Count > 0)
                    {
                        decimal tot = 0;
                        for (int i = 0; DtGridEstimate1.Rows.Count > i; i++)
                        {
                            tot = tot + Convert.ToDecimal(DtGridEstimate1.Rows[i]["EstimatedPrice"]);
                        }
                        gvestimatequanold.DataSource = DtGridEstimate1;
                        gvestimatequanold.DataBind();
                        gvestimatequanold.Visible = true;
                        decimal msumobject = tot; //* qtyimp / 100000;
                        lblvalueimport.Text = msumobject.ToString("F2");
                        ten.Visible = true;
                    }
                    else
                    {
                        gvestimatequanold.Visible = false;
                        lblvalueimport.Text = "0.00";
                        ten.Visible = false;
                    }
                    DataTable dtPdfBind = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductImage", "PDF");
                    if (dtPdfBind.Rows.Count > 0)
                    {
                        gvpdf.DataSource = dtPdfBind;
                        gvpdf.DataBind();
                        gvpdf.Visible = true;
                        twele.Visible = true;
                    }
                    else
                    {
                        gvpdf.Visible = false;
                        twele.Visible = false;
                    }
                    DataTable dtImageBindfinal = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductImage", "Image");
                    if (dtImageBindfinal.Rows.Count > 0)
                    {
                        dlimage.DataSource = dtImageBindfinal;
                        dlimage.DataBind();
                        dlimage.Visible = true;
                        thirteen.Visible = true;
                    }
                    else
                    {
                        dlimage.Visible = false;
                        thirteen.Visible = false;
                    }
                    if (DtView.Rows[0]["FeatursandDetail"].ToString() != "")
                    {
                        lblfeaturesanddetail.Text = DtView.Rows[0]["FeatursandDetail"].ToString();
                        fourteen.Visible = true;
                    }
                    else
                    {
                        fourteen.Visible = false;
                    }
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
                    DataTable dtestimatequanorprice = Lo.RetriveSaveEstimateGrid("2Select", 0, e.CommandArgument.ToString(), 0, "", "", "", "", "F");
                    if (dtestimatequanorprice.Rows.Count > 0)
                    {
                        gvestimatequanorprice.DataSource = dtestimatequanorprice;
                        gvestimatequanorprice.DataBind();
                        gvestimatequanorprice.Visible = true;
                        fifteen.Visible = true;
                    }
                    else
                    {
                        gvestimatequanorprice.Visible = false;
                        fifteen.Visible = false;
                    }
                    lblindicate.Text = "";
                    if (DtView.Rows[0]["PurposeofProcurement"].ToString() != "")
                    {
                        DataTable DTporCat = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductPOP", "Company");
                        if (DTporCat.Rows.Count > 0)
                        {
                            lblindicate.Text = "";
                            for (int i = 0; DTporCat.Rows.Count > i; i++)
                            {
                                lblindicate.Text = lblindicate.Text + DTporCat.Rows[i]["SCategoryName"].ToString() + ", ";
                            }
                            lblindicate.Text = lblindicate.Text.Substring(0, lblindicate.Text.Length - 2);
                            sixteen.Visible = true;
                        }
                        else
                        { sixteen.Visible = false; }
                    }
                    else
                    {
                        sixteen.Visible = false;
                    }
                    lblprocremarks.Text = DtView.Rows[0]["ProcurmentCategoryRemark"].ToString();
                    if (DtView.Rows[0]["EOIStatus"].ToString() != "")
                    {
                        lbleoirep.Text = DtView.Rows[0]["EOIStatus"].ToString();
                        seventeen.Visible = true;
                    }
                    else
                    { seventeen.Visible = false; }
                    if (DtView.Rows[0]["EOIURL"].ToString() != "")
                    {
                        lbleoilink.Text = DtView.Rows[0]["EOIURL"].ToString();
                        eighteen.Visible = true;
                    }
                    else
                    { eighteen.Visible = false; }
                    if (DtView.Rows[0]["TenderStatus"].ToString() == "Live")
                    {
                        lbltendor.Text = "Live";
                    }
                    else if (DtView.Rows[0]["TenderStatus"].ToString() == "Archive" || DtView.Rows[0]["TenderStatus"].ToString() == "Not Floated")
                    {
                        lbltendor.Text = DtView.Rows[0]["TenderStatus"].ToString();
                    }
                    else
                    {
                        lbltendor.Text = "No";
                    }
                    string Nodel1Id = DtView.Rows[0]["NodelDetail"].ToString();
                    if (Nodel1Id.ToString() != "")
                    {
                        DataTable dtNodal = Lo.RetriveProductCode(Nodel1Id.ToString(), "", "ProdNodal", "");
                        if (dtNodal.Rows.Count > 0)
                        {
                            lblempname.Text = dtNodal.Rows[0]["NodalOficerName"].ToString();
                            lbldesignation.Text = dtNodal.Rows[0]["Designation"].ToString();
                            lblemailidpro.Text = dtNodal.Rows[0]["NodalOfficerEmail"].ToString();
                            lblmobilenumber.Text = dtNodal.Rows[0]["NodalOfficerMobile"].ToString();
                            lblphonenumber.Text = dtNodal.Rows[0]["NodalOfficerTelephone"].ToString();
                            lblfaxpro.Text = dtNodal.Rows[0]["NodalOfficerFax"].ToString();
                        }
                        else
                        {
                            nineteen.Visible = false;
                        }
                    }
                    else
                    {
                        nineteen.Visible = false;
                    }
                    if (DtView.Rows[0]["EndUser"].ToString() != "")
                    {
                        DataTable DTporCat = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "EndUser", "Company");
                        if (DTporCat.Rows.Count > 0)
                        {
                            lblenduser.Text = "";
                            for (int i = 0; DTporCat.Rows.Count > i; i++)
                            {
                                lblenduser.Text = lblenduser.Text + DTporCat.Rows[i]["EndUser"].ToString() + ", ";
                            }
                            lblenduser.Text = lblenduser.Text.Substring(0, lblenduser.Text.Length - 2);
                            twenty.Visible = true;
                        }
                        else
                        { twenty.Visible = false; }
                    }
                    else
                    {
                        twenty.Visible = false;
                    }
                    if (DtView.Rows[0]["PlatName"].ToString() != "")
                    {
                        lbldefenceplatform.Text = DtView.Rows[0]["PlatName"].ToString();
                        twentyone.Visible = true;
                    }
                    else
                    {
                        twentyone.Visible = false;
                    }
                    if (DtView.Rows[0]["Nomenclature"].ToString() != "")
                    {
                        lblnameofdefplat.Text = DtView.Rows[0]["Nomenclature"].ToString();
                        twentytwo.Visible = true;
                    }
                    else
                    {
                        twentytwo.Visible = false;
                    }
                    if (DtView.Rows[0]["IsShowGeneral"].ToString() != "")
                    {
                        if (DtView.Rows[0]["IsShowGeneral"].ToString() == "Y")
                            lblisshowgeneral.Text = "Yes";
                        else
                            lblisshowgeneral.Text = "No";
                        twentyfour.Visible = false;
                    }
                    else
                    {
                        twentyfour.Visible = false;
                    }
                    if (DtView.Rows[0]["TermConditionImage"].ToString() != "")
                    {
                        twentythree.Visible = false;
                    }
                    else
                    {
                        twentythree.Visible = false;
                    }
                    if (DtView.Rows[0]["QAAgency"].ToString() != "")
                    {
                        DataTable DTporCat = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductQAAgency", "Company");
                        if (DTporCat.Rows.Count > 0)
                        {
                            lbqa.Text = "";
                            for (int i = 0; DTporCat.Rows.Count > i; i++)
                            {
                                lbqa.Text = lbqa.Text + DTporCat.Rows[i]["SCategoryName"].ToString() + ", ";
                            }
                            lbqa.Text = lbqa.Text.Substring(0, lbqa.Text.Length - 2);
                            twentysix.Visible = true;
                        }
                        else
                        { twentysix.Visible = false; }
                    }
                    else
                    {
                        twentysix.Visible = false;
                    }
                    if (DtView.Rows[0]["NIINCode"].ToString() != "")
                    {
                        Tr8.Visible = true;
                        lblnincode.Text = DtView.Rows[0]["NIINCode"].ToString();
                    }
                    else
                    {
                        Tr8.Visible = false;
                    }
                    if (DtView.Rows[0]["IsIndeginized"].ToString() != "")
                    {

                        if (DtView.Rows[0]["IsIndeginized"].ToString() == "Y")
                        {
                            Tr19.Visible = true;
                            lblisindigenised.Text = "Yes";
                            if (DtView.Rows[0]["ManufactureName"].ToString() != "")
                            {
                                lblmanuname.Text = DtView.Rows[0]["ManufactureName"].ToString();
                                Tr20.Visible = true;
                            }
                            else
                            {
                                Tr20.Visible = false;
                            }
                            if (DtView.Rows[0]["ManufactureAddress"].ToString() != "")
                            {
                                lblmanuaddress.Text = DtView.Rows[0]["ManufactureAddress"].ToString();
                                Tr21.Visible = true;
                            }
                            else
                            {
                                Tr21.Visible = false;
                            }
                            if (DtView.Rows[0]["YearofIndiginization"].ToString() != "")
                            {
                                lblyearofindi.Text = DtView.Rows[0]["FY"].ToString();
                                Tr22.Visible = true;
                            }
                            else
                            {
                                Tr22.Visible = false;
                            }
                        }
                        else
                        {
                            lblisindigenised.Text = "No";
                            Tr20.Visible = false;
                            Tr21.Visible = false;
                            Tr22.Visible = false;
                        }
                    }
                    else
                    {
                        Tr19.Visible = false;
                        Tr20.Visible = false;
                        Tr21.Visible = false;
                        Tr22.Visible = false;
                    }
                    if (DtView.Rows[0]["IndTargetYear"].ToString() != "")
                    {
                        lblindtrgyr.Text = DtView.Rows[0]["IndTargetYear"].ToString().Substring(0, DtView.Rows[0]["IndTargetYear"].ToString().Length - 1);
                        if (lblindtrgyr.Text == "NIL")
                        { Tr25.Visible = false; }
                        else
                        {
                            Tr25.Visible = true;
                        }
                    }
                    else
                    {
                        Tr25.Visible = false;
                    }
                    if (DtView.Rows[0]["IndProcess"].ToString() != "")
                    {
                        lblprocstart.Text = DtView.Rows[0]["IndProcess"].ToString();
                        Tr24.Visible = true;
                    }
                    else
                    {
                        Tr24.Visible = false;
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "changePass", "showPopup();", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
            }
        }
        #endregion
        #region dele
        else if (e.CommandName == "DeleteComp")
        {
            try
            {
                string DeleteRec = Lo.DeleteRecord(e.CommandArgument.ToString(), "InActiveProd");
                if (DeleteRec == "true")
                {
                    BindGridView();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "SuccessfullPop('Product deactive successfully.')", true);
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
        #endregion
    }
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
    protected void gvproductItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
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
        if (hidType.Value == "Company")
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
        else if (hidType.Value == "SuperAdmin" || hidType.Value == "Admin")
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
        if (hidType.Value == "SuperAdmin" || hidType.Value == "Admin")
        { }
        else
        {
            BindGridView();
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
    protected void txtsearchbyrefid_TextChanged(object sender, EventArgs e)
    {
        if (txtsearchbyrefid.Text != "")
        {
            BindGridView();
        }
    }
    #endregion
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
}