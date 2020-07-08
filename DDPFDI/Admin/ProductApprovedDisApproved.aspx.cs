using BusinessLayer;
using Encryption;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Drawing;


public partial class Admin_ProductApprovedDisApproved : System.Web.UI.Page
{
    private Logic Lo = new Logic();
    private DataUtility Co = new DataUtility();
    private Cryptography objEnc = new Cryptography();
    private DataTable DtGrid = new DataTable();
    private DataTable DtCompanyDDL = new DataTable();
    HybridDictionary HyUpdateProd = new HybridDictionary();
    private string currentPage = "";
    private string mRefNo = "";
    string _msg = string.Empty;
    string _sysMsg = string.Empty;
    PagedDataSource pgsource = new PagedDataSource();
    HybridDictionary hySaveProdInfo = new HybridDictionary();
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
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Session Expired,Please login again');window.location='Login'", true);
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
            if (rbliststatus.SelectedValue == "")
            { }
            else
            {
                DtGrid = Lo.GetDashboardDataApproveDisapproveItem("Product", "", rbliststatus.SelectedItem.Value);
                if (DtGrid.Rows.Count > 0)
                {
                    this.UpdateDtGridValue();
                    DataView dv = new DataView(DtGrid);
                    dv.RowFilter = "CompanyRefNo='" + ddlcompany.SelectedItem.Value + "'";
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
                        if (txtserachitemidprotal.Text != "")
                        {
                            dv.RowFilter = "ProductRefNo='" + txtserachitemidprotal.Text + "'";
                        }
                        else
                            dv.Sort = "LastUpdated desc,CompanyName asc,FactoryName asc";
                        DataTable dtads = dv.ToTable();
                        if (dtads.Rows.Count > 0)
                        {
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
                            divproductgridview.Visible = true;
                            lbltot.Text = "Showing  " + gvproductItem.Rows.Count.ToString() + " result from page " + (pagingCurrentPage + 1) + " out of " + pgsource.PageCount + " pages";
                            divpageindex.Visible = true;
                            divproductgridview.Visible = true;
                        }
                        else
                        {
                            gvproductItem.Visible = false;
                            divpageindex.Visible = false;
                            lbltot.Text = "";
                            divproductgridview.Visible = false;
                        }
                    }
                    else
                    {
                        gvproductItem.Visible = false;
                        divpageindex.Visible = false;
                        lbltot.Text = "";
                        divproductgridview.Visible = false;

                    }
                }
                else
                {
                    lbltot.Text = "";
                    divpageindex.Visible = false;
                    gvproductItem.Visible = false;
                    divproductgridview.Visible = false;
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
    #endregion
    #region RowCommand
    public string mhiddenProdRefNo;
    public string POProc;
    public string EndUserValue;
    string crfno;
    protected void gvproductItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditComp")
        {
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gvr.RowIndex;
            lblrefnoview.Text = e.CommandArgument.ToString();
            string CRefNo = (gvproductItem.Rows[rowIndex].FindControl("hfcomprefno") as HiddenField).Value;
            string FRefNo = (gvproductItem.Rows[rowIndex].FindControl("hfdivisionrefno") as HiddenField).Value;
            string URefNo = (gvproductItem.Rows[rowIndex].FindControl("hfunitrefno") as HiddenField).Value;
            if (URefNo != "")
            {
                hfcomprefno.Value = URefNo.ToString();
            }
            else if (FRefNo != "")
            { hfcomprefno.Value = FRefNo.ToString(); }
            else if (CRefNo != "" && FRefNo == "")
            { hfcomprefno.Value = CRefNo.ToString(); }
            BindMasterCategory();
            ScriptManager.RegisterStartupScript(this, GetType(), "updateitem", "showPopup1();", true);
        }
        else if (e.CommandName == "ViewComp")
        {
            try
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                int rowIndex = gvr.RowIndex;
                string Role = (gvproductItem.Rows[rowIndex].FindControl("hfrole") as HiddenField).Value;
                DataTable DtView = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductMasterID", Role);
                if (DtView.Rows.Count > 0)
                {
                    if (objEnc.DecryptData(Session["User"].ToString()) == "oicncbops.defstand@gov.in" || objEnc.DecryptData(Session["User"].ToString()) == "rgera@nic.in")
                    { pancheck.Visible = true; }
                    else
                    { pancheck.Visible = false; }
                    string crfno = DtView.Rows[0]["CompanyRefNo"].ToString();
                    lblrefnoview.Text = e.CommandArgument.ToString();
                    lblcompname.Text = DtView.Rows[0]["CompanyName"].ToString();
                    lbldiviname.Text = DtView.Rows[0]["FactoryName"].ToString();
                    lblunitnamepro.Text = DtView.Rows[0]["UnitName"].ToString();
                    lblnsngroup.Text = DtView.Rows[0]["ProdLevel1Name"].ToString();
                    lblnsngroupclass.Text = DtView.Rows[0]["ProdLevel2Name"].ToString();
                    lblclassitem.Text = DtView.Rows[0]["ProdLevel3Name"].ToString();
                    lblsearchkeywords.Text = DtView.Rows[0]["Searchkeyword"].ToString();
                    lblproductdescription.Text = DtView.Rows[0]["ProductDescription"].ToString();
                    lbldpsupartno.Text = DtView.Rows[0]["DPSUPartNumber"].ToString();
                    lblhsncode8digit.Text = DtView.Rows[0]["HsnCode8digit"].ToString();
                    prodIndustryDomain.Text = DtView.Rows[0]["TechLevel1Name"].ToString();
                    ProdIndusSubDomain.Text = DtView.Rows[0]["Techlevel2Name"].ToString();
                    lblisproductimported.Text = DtView.Rows[0]["IsProductImported"].ToString();
                    if (lblisproductimported.Text == "Y")
                    {
                        lblisproductimported.Text = "Yes";
                    }
                    else { lblisproductimported.Text = "No"; }
                    DataTable DtGridEstimate1 = new DataTable();
                    DtGridEstimate1 = Lo.RetriveSaveEstimateGrid("Select", 0, e.CommandArgument.ToString(), 0, "", "", "", "", "O");
                    if (DtGridEstimate1.Rows.Count > 0)
                    {
                        int tot = 0;
                        for (int i = 0; DtGridEstimate1.Rows.Count > i; i++)
                        {
                            tot = tot + Convert.ToInt32(DtGridEstimate1.Rows[i]["EstimatedPrice"]);
                        }
                        gvestimatequanold.DataSource = DtGridEstimate1;
                        gvestimatequanold.DataBind();
                        gvestimatequanold.Visible = true;
                        decimal msumobject = Convert.ToDecimal(tot); /// Convert.ToDecimal(100000);
                        lblvalueimport.Text = msumobject.ToString("F2");
                    }
                    else
                    {
                        gvestimatequanold.Visible = false;
                        lblvalueimport.Text = "0.00";
                    }
                    DataTable dtPdfBind = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductImage", "PDF");
                    if (dtPdfBind.Rows.Count > 0)
                    {
                        gvpdf.DataSource = dtPdfBind;
                        gvpdf.DataBind();
                        gvpdf.Visible = true;
                    }
                    else
                    {
                        gvpdf.Visible = false;
                    }
                    DataTable dtImageBindfinal = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductImage", "Image");
                    if (dtImageBindfinal.Rows.Count > 0)
                    {
                        dlimage.DataSource = dtImageBindfinal;
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
                    DataTable dtestimatequanorprice = Lo.RetriveSaveEstimateGrid("Select", 0, e.CommandArgument.ToString(), 0, "", "", "", "", "F");
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
                    if (DtView.Rows[0]["PurposeofProcurement"].ToString() != "")
                    {
                        DataTable DTporCat = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductPOP", "Company");
                        if (DTporCat.Rows.Count > 0)
                        {
                            for (int i = 0; DTporCat.Rows.Count > i; i++)
                            {
                                lblindicate.Text = lblindicate.Text + DTporCat.Rows[i]["SCategoryName"].ToString();
                            }
                        }
                    }
                    lbleoirep.Text = DtView.Rows[0]["EOIStatus"].ToString();
                    lbleoilink.Text = DtView.Rows[0]["EOIURL"].ToString();
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
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "changePass", "showPopup();", true);
                }
            }
            catch (Exception ex)
            { }
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
                    ddldivision.SelectedValue = dt.Rows[0]["FactoryRefNo"].ToString();
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
                    ddldivision.SelectedValue = dt.Rows[0]["FactoryRefNo"].ToString();
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
    #region Product Approved Disaaprocdd code
    protected void btnapprove_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtappdisappmssg.Text != "")
            {
                string DeleteRec = Lo.DeleteRecord(lblrefnoview.Text, "ActiveProd");
                if (DeleteRec == "true")
                {
                    hySaveProdInfo["Status"] = "Y";
                    SendMailApproved();
                    SaveUpdateInfoProduct();
                    txtappdisappmssg.Text = "";
                    BindGridView();
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "SuccessfullPop('Product approved successfully.Please click on close button to close popup or refresh page to see changes')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "ErrorMssgPopup('product not approved.')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "ErrorMssgPopup('please give reasone of approval.')", true);
            }
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "ErrorMssgPopup('product not approved.')", true);
        }
    }
    protected void btndisapproved_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtappdisappmssg.Text != "")
            {
                string DeleteRec = Lo.DeleteRecord(lblrefnoview.Text, "InActiveProd");
                if (DeleteRec == "true")
                {
                    hySaveProdInfo["Status"] = "N";
                    SendMailDisApproved();
                    SaveUpdateInfoProduct();
                    txtappdisappmssg.Text = "";
                    BindGridView();
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "SuccessfullPop('Product deactive successfully. Please click on close button to close popup or refresh page to see changes')", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "ErrorMssgPopup('Record not deleted.')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "ErrorMssgPopup('Please enter detail of disapproval.')", true);
            }
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "ErrorMssgPopup('Record not deleted.')", true);
        }
    }
    protected void SendMailApproved()
    {
        try
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/ProductAppDis.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{MESSAGE}", txtappdisappmssg.Text.Trim());
            body = body.Replace("{refno}", lblrefnoview.Text);
            SendMail s;
            s = new SendMail();
            if (lblemailidpro.Text != "")
            {
                s.CreateMail("noreply-srijandefence@gov.in", lblemailidpro.Text, "Amendment in product", body);
                s.sendMail();
                hySaveProdInfo["Mailsend"] = "Y";
            }
            else
            {
                DataTable DtNodalEmail = Lo.RetriveAllNodalOfficer(lblemailidpro.Text, "NodalForEmail");
                if (DtNodalEmail.Rows.Count > 0)
                {
                    s.CreateMail("noreply-srijandefence@gov.in", DtNodalEmail.Rows[0]["NodalOfficerEmail"].ToString(), "Approved amendment in product", body);
                    s.sendMail();
                    hySaveProdInfo["Mailsend"] = "Y";
                }
                else
                { hySaveProdInfo["Mailsend"] = "N"; }
            }

        }
        catch (Exception ex)
        {
            hySaveProdInfo["Mailsend"] = "N";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('" + ex.Message + "')", true);
        }
    }
    protected void SendMailDisApproved()
    {
        try
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/ProductAppDis.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{MESSAGE}", txtappdisappmssg.Text.Trim());
            body = body.Replace("{refno}", lblrefnoview.Text);
            SendMail s;
            s = new SendMail();
            if (lblemailidpro.Text != "")
            {
                s.CreateMail("noreply-srijandefence@gov.in", lblemailidpro.Text, "Amendment in product", body);
                s.sendMail();
                hySaveProdInfo["Mailsend"] = "Y";
            }
            else
            {
                DataTable DtNodalEmail = Lo.RetriveAllNodalOfficer(crfno.ToString(), "NodalForEmail");
                if (DtNodalEmail.Rows.Count > 0)
                {
                    s.CreateMail("noreply-srijandefence@gov.in", DtNodalEmail.Rows[0]["NodalOfficerEmail"].ToString(), "Disapproved amendment in product", body);
                    s.sendMail();
                    hySaveProdInfo["Mailsend"] = "Y";
                }
                else
                { hySaveProdInfo["Mailsend"] = "N"; }
            }
        }
        catch (Exception ex)
        {
            hySaveProdInfo["Mailsend"] = "N";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('" + ex.Message + "')", true);
        }
    }
    protected void SendMailProdChanges()
    {
        try
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/ProductAppDis.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{MESSAGE}", "We just changed your NSC Class,Nsc Group Class and Item Code.Please review and revert back if any crediential are wrong.");
            body = body.Replace("{refno}", lblrefnoview.Text);
            SendMail s;
            s = new SendMail();
            DataTable DtNodalEmail = Lo.RetriveAllNodalOfficer(hfcomprefno.Value, "NodalForEmail");
            if (DtNodalEmail.Rows.Count > 0)
            {
                s.CreateMail("noreply-srijandefence@gov.in", DtNodalEmail.Rows[0]["NodalOfficerEmail"].ToString(), "Amendment in product", body);
                s.sendMail();
                hySaveProdInfo["Mailsend"] = "Y";
            }
            else
            { hySaveProdInfo["Mailsend"] = "N"; }
        }
        catch (Exception ex)
        {
            hySaveProdInfo["Mailsend"] = "N";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('" + ex.Message + "')", true);
        }
    }
    public void SaveUpdateInfoProduct()
    {
        hySaveProdInfo["ProdRefNo"] = lblrefnoview.Text;
        hySaveProdInfo["ProductChanges"] = txtappdisappmssg.Text;
        hySaveProdInfo["ChangesBy"] = objEnc.DecryptData(Session["User"].ToString());
        string InsertLogProdSave = Lo.InsertLogProd(hySaveProdInfo, out  _sysMsg, out  _msg);
    }
    protected void gvproductItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //HiddenField hfisapproved = (e.Row.FindControl("hfisaaproved") as HiddenField);
            Label lblisapproved = e.Row.FindControl("lblisapproved") as Label;
            if (lblisapproved.Text == "Y")
            {
                lblisapproved.Text = "Yes";
                // e.Row.Attributes.Add("Class", "bg-purple");
            }
            else if (lblisapproved.Text == "N")
            {
                lblisapproved.Text = "No";
                // e.Row.Attributes.Add("Class", "bg-red");
            }
            else if (lblisapproved.Text == "A")
            {
                lblisapproved.Text = "Available";
                // e.Row.Attributes.Add("Class", "bg-red");
            }
        }
    }
    protected void rbliststatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridView();
    }
    #endregion
    #region Product Edit Code NSN Group Only
    protected void ddlnsn_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMasterSubCategory();
    }
    protected void ddlnsnclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMaster3levelSubCategory();
        NSCCode(ddlnsn.SelectedItem.Text, ddlnsnclass.SelectedItem.Text);
    }
    protected void ddlitemcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindItemDescription();
    }
    private void NSCCode(string NSNGroupddl, string NSNClassddl)
    {
        try
        {
            string a = NSNGroupddl.Substring((NSNGroupddl.IndexOf("(") + 1), NSNGroupddl.IndexOf(")") - (NSNGroupddl.IndexOf("(") + 1));
            string b = NSNClassddl.Substring((NSNClassddl.IndexOf("(") + 1), NSNClassddl.IndexOf(")") - (NSNClassddl.IndexOf("(") + 1));
            txtnsccode.Text = a + b;
        }
        catch (Exception)
        {
            txtnsccode.Text = "";
        }
    }
    #region For ProductCode
    protected void BindMasterCategory()
    {
        DataTable DtMasterCategroy = new DataTable();
        if (ddlcompany.SelectedItem.Text != "Select")
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "NSN GROUP", "", "SelectProductCat", ddlcompany.SelectedItem.Value, "");
        }
        else
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "NSN GROUP", "", "SelectProductCat", "", "");
        }
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlnsn, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlnsn.Items.Insert(0, "Select");
        }
        else
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "NSN GROUP", "", "SelectProductCat", "", "");
            Co.FillDropdownlist(ddlnsn, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlnsn.Items.Insert(0, "Select");
        }
    }
    protected void BindMasterSubCategory()
    {
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlnsn.SelectedItem.Value), "", "", "SubSelectID", "", "");
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlnsnclass, DtMasterCategroy, "SCategoryName", "SCategoryId");
            ddlnsnclass.Items.Insert(0, "Select");
        }
        else
        {
            ddlnsnclass.Items.Clear();
            ddlnsnclass.Items.Insert(0, "Select");
        }
    }
    protected void BindMaster3levelSubCategory()
    {
        if (ddlnsnclass.SelectedItem.Value != null || ddlnsnclass.SelectedItem.Text != "Select")
        {
            DataTable DtMasterCategroyLevel3 = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlnsnclass.SelectedItem.Value), "", "", "SubSelectID", "", "");
            if (DtMasterCategroyLevel3.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlitemcode, DtMasterCategroyLevel3, "SCategoryName", "SCategoryId");
                ddlitemcode.Items.Insert(0, "Select");
            }
            else
            {
                ddlitemcode.Items.Clear();
                ddlitemcode.Items.Insert(0, "Select");
                ddlitemcode.Items.Insert(1, "NA");
            }
        }
    }
    protected void BindItemDescription()
    {
        if (ddlitemcode.SelectedItem.Value != null || ddlitemcode.SelectedItem.Text != "Select")
        {
            if (ddlitemcode.SelectedItem.Value != "NA")
            {
                DataTable DtItemDescription = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlitemcode.SelectedItem.Value), "", "", "Level3ID", "", "");
                if (DtItemDescription.Rows.Count > 0)
                {
                    txtproductdescription.Text = DtItemDescription.Rows[0]["Description"].ToString();
                }
                else
                {
                    txtproductdescription.Text = "";
                }
            }
            else
            {
                txtproductdescription.Text = "";
            }
        }
    }
    #endregion
    #endregion
    protected void lblupdate_Click(object sender, EventArgs e)
    {
        HyUpdateProd["ProductRefNo"] = lblrefnoview.Text;
        if (ddlnsn.SelectedItem.Value != "Select")
        {
            HyUpdateProd["ProductLevel1"] = Co.RSQandSQLInjection(ddlnsn.SelectedItem.Value, "soft");
        }
        else
        {
            HyUpdateProd["ProductLevel1"] = null;
        }
        if (ddlnsnclass.SelectedItem.Value != "Select")
        {
            HyUpdateProd["ProductLevel2"] = Co.RSQandSQLInjection(ddlnsnclass.SelectedItem.Value, "soft");
        }
        else
        {
            HyUpdateProd["ProductLevel2"] = null;
        }
        if (ddlitemcode.SelectedItem.Value != "Select")
        {
            HyUpdateProd["ProductLevel3"] = Co.RSQandSQLInjection(ddlitemcode.SelectedItem.Value, "soft");
        }
        else
        {
            HyUpdateProd["ProductLevel3"] = null;
        }
        HyUpdateProd["ProductDescription"] = Co.RSQandSQLInjection(txtproductdescription.Text, "soft");
        HyUpdateProd["NIINCode"] = Co.RSQandSQLInjection(txtniincode.Text, "soft");
        HyUpdateProd["NSCCode"] = Co.RSQandSQLInjection(txtnsccode.Text, "soft");
        string updateprod = Lo.UpdateCodeProduct(HyUpdateProd, out _sysMsg, out _sysMsg);
        if (updateprod == "Update" && _sysMsg != "")
        {
            ddlnsn.SelectedIndex = 0;
            ddlnsnclass.SelectedIndex = 0;
            ddlitemcode.SelectedIndex = 0;
            txtniincode.Text = "";
            txtnsccode.Text = "";
            txtproductdescription.Text = "";
            SendMailProdChanges();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Item successfully update and mail send to nodal officer')", true);
        }
        else
        {
            ddlnsn.SelectedIndex = 0;
            ddlnsnclass.SelectedIndex = 0;
            ddlitemcode.SelectedIndex = 0;
            txtniincode.Text = "";
            txtnsccode.Text = "";
            txtproductdescription.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Some error occur, Item not updated')", true);
        }
    }
    protected void txtserachitemidprotal_TextChanged(object sender, EventArgs e)
    {
        if (txtserachitemidprotal.Text != "")
        {
            BindGridView();
        }
    }
}