using BusinessLayer;
using Encryption;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Configuration;

public partial class Admin_ViewProductFilter : System.Web.UI.Page
{
    private Logic Lo = new Logic();
    private DataTable DtGrid = new DataTable();
    private HybridDictionary hySaveProdInfo = new HybridDictionary();
    private string _msg = string.Empty;
    private string _sysMsg = string.Empty;
    DataUtility Co = new DataUtility();
    private Cryptography Encrypt = new Cryptography();
    private PagedDataSource pgsource = new PagedDataSource();
    HybridDictionary HyLoginStatus = new HybridDictionary();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Session["User"] != null)
            {
                if (Request.QueryString["id"] != null)
                {
                    if (Encrypt.DecryptData(Session["Type"].ToString()) == "Admin" || Encrypt.DecryptData(Session["Type"].ToString()) == "SuperAdmin")
                    {
                        if (Request.QueryString["strangone"] != null)
                        {
                            ControlGrid(Encrypt.DecryptData(Request.QueryString["id"].ToString()), Encrypt.DecryptData(Request.QueryString["strangone"].ToString()));
                        }
                        else
                        {
                            ControlGrid(Encrypt.DecryptData(Request.QueryString["id"].ToString()), "");
                        }
                    }
                    else
                    {
                        ControlGrid(Encrypt.DecryptData(Request.QueryString["id"].ToString()), Session["CompanyRefNo"].ToString());
                    }
                }
                BindComapnyCheckbox();
                BindEndUser();
                BindDefencePlatform();
                BindIndusrtyDomain();
                BindCountry();
                BindSearchKeyword();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "ErrorMssgPopup('Session Expired,Please login again');window.location='Login'", true);
            }
        }
    }
    protected void lbllogout_Click(object sender, EventArgs e)
    {
        Logoutstatus();
    }
    protected void Logoutstatus()
    {
        try
        {
            HyLoginStatus["LoginUser"] = Encrypt.DecryptData(Session["User"].ToString());
            HyLoginStatus["IsLogedIn"] = "N";
            DateTime Date = Convert.ToDateTime(DateTime.Now);
            string dateformat = Date.ToString("yyyy-MM-dd hh:mm:ss");
            HyLoginStatus["IsLogedOutTime"] = dateformat.ToString();
            string InsertLogOutStatus = Lo.SaveLogoutstatus(HyLoginStatus, out  _sysMsg, out  _msg);
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            Response.RedirectToRoute("Login");
        }
        catch (Exception ex)
        { Response.RedirectToRoute("Login"); }
    }
    protected void UpdateDtGridValue(DataTable DtGrid)
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
    private void ControlGrid(string mVal, string RefNo)
    {
        hfmtype.Value = mVal.ToString();
        hfmref.Value = RefNo.ToString();
        if (hfmtype.Value == "P")
        {
            gvproduct.Visible = true;
            BindProduct(hfmref.Value);
        }
        else if (hfmtype.Value == "PI")
        {
            gvproduct.Visible = true;
            BindProduct(hfmref.Value);
        }
        else if (hfmtype.Value == "M2")
        {
            gvproduct.Visible = true;
            BindProduct(hfmref.Value);
        }
        else if (hfmtype.Value == "PH")
        {
            gvproduct.Visible = true;
            BindProduct(hfmref.Value);
        }
        else if (hfmtype.Value == "WPH")
        {
            gvproduct.Visible = true;
            BindProduct(hfmref.Value);
        }
        else
        {
            BindProduct(hfmref.Value);
        }
    }
    int n1 = 1;
    int n2 = 300;
    protected void BindProduct(string RefNo)
    {
        DtGrid = Lo.GetDashboardData("Product", "");
        if (DtGrid.Rows.Count > 0)
        {
            divclearfilorexceldown.Visible = true;
            UpdateDtGridValue(DtGrid);
            if (hfmref.Value != "")
            {
                DataView dv = new DataView(DtGrid, "ROW_NUMBER >='" + n1 + "' And  ROW_NUMBER<='" + n2 + "'", "", DataViewRowState.CurrentRows);
                if (Request.QueryString["strangone"] != null)
                {
                    dv.RowFilter = "CompanyRefNo='" + hfmref.Value + "'";
                }
                else if (Encrypt.DecryptData(Session["Type"].ToString()).ToUpper() == "COMPANY" || ddlcomp.SelectedItem.Selected == true)
                {
                    dv.RowFilter = "CompanyRefNo='" + hfmref.Value + "'";
                }
                else if (Encrypt.DecryptData(Session["Type"].ToString()).ToUpper() == "DIVISION" || ddldivision.SelectedItem.Selected == true)
                {
                    dv.RowFilter = "FactoryRefNo='" + hfmref.Value + "'";
                }
                else if (Encrypt.DecryptData(Session["Type"].ToString()).ToUpper() == "UNIT" || ddlunit.SelectedItem.Selected == true)
                {
                    dv.RowFilter = "UnitRefNo='" + hfmref.Value + "'";
                }
                dv.Sort = "LastUpdated desc,CompanyName asc,FactoryName asc";
                DataTable dtads = dv.ToTable();
                dtads.Columns.Add("TopImages", typeof(string));
                for (int i = 0; dtads.Rows.Count > i; i++)
                {
                    string mProdRefTime = dtads.Rows[i]["ProductRefNo"].ToString();
                    DataTable dtImageBind = Lo.RetriveProductCode("", mProdRefTime, "RetImageTop", "");
                    if (dtImageBind.Rows.Count > 0)
                    {
                        dtads.Rows[i]["TopImages"] = dtImageBind.Rows[0]["ImageName"].ToString();
                    }
                    else
                    {
                        dtads.Rows[i]["TopImages"] = "assets/images/Noimage.png";
                    }
                }
                pgsource.DataSource = dtads.DefaultView;
                pgsource.AllowPaging = true;
                pgsource.PageSize = 300;
                pgsource.CurrentPageIndex = pagingCurrentPage;
                ViewState["totpage"] = pgsource.PageCount;
                lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                lbltotal.Text = "Total " + DtGrid.Rows.Count.ToString() + ", Showing  " + gvproduct.Rows.Count.ToString() + " result from page " + (pagingCurrentPage + 1) + " out of " + pgsource.PageCount + " pages";
                lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                pgsource.DataSource = dtads.DefaultView;
                gvproduct.DataSource = dtads.DefaultView;
                gvproduct.DataBind();
                gvproduct.Visible = true;
                divpageindex.Visible = true;
                divProductGrid.Visible = true;
            }
            else
            {
                if (Request.QueryString["id"] != null)
                {
                    if (Encrypt.DecryptData(Request.QueryString["id"].ToString()) == "PI")
                    {
                        DataView dv1 = new DataView(DtGrid, "ROW_NUMBER >='" + n1 + "' And  ROW_NUMBER<='" + n2 + "' or IsIndeginized='Y'", "LastUpdated desc,CompanyName asc,FactoryName asc", DataViewRowState.CurrentRows);
                        DataTable dtads = dv1.ToTable();
                        dtads.Columns.Add("TopImages", typeof(string));
                        for (int i = 0; dtads.Rows.Count > i; i++)
                        {
                            string mProdRefTime = dtads.Rows[i]["ProductRefNo"].ToString();
                            DataTable dtImageBind = Lo.RetriveProductCode("", mProdRefTime, "RetImageTop", "");
                            if (dtImageBind.Rows.Count > 0)
                            {
                                dtads.Rows[i]["TopImages"] = dtImageBind.Rows[0]["ImageName"].ToString();
                            }
                            else
                            {
                                dtads.Rows[i]["TopImages"] = "assets/images/Noimage.png";
                            }
                        }
                        pgsource.DataSource = dtads.DefaultView;
                        pgsource.AllowPaging = true;
                        pgsource.PageSize = 300;
                        pgsource.CurrentPageIndex = pagingCurrentPage;
                        ViewState["totpage"] = pgsource.PageCount;
                        lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                        lbltotal.Text = "Total " + DtGrid.Rows.Count.ToString() + ", Showing  " + pgsource.PageSize + " result from page " + (pagingCurrentPage + 1) + " out of " + pgsource.PageCount + " pages";
                        lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                        lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                        pgsource.DataSource = dtads.DefaultView;
                        gvproduct.DataSource = dtads.DefaultView;
                    }
                    else if (Encrypt.DecryptData(Request.QueryString["id"].ToString()) == "M2")
                    {
                        DataView dv2 = new DataView(DtGrid, "ROW_NUMBER >='" + n1 + "' And  ROW_NUMBER<='" + n2 + "' or PurposeofProcurement like '%25%'", "LastUpdated desc,CompanyName asc,FactoryName asc", DataViewRowState.CurrentRows);
                        DataTable dtads = dv2.ToTable();
                        dtads.Columns.Add("TopImages", typeof(string));
                        for (int i = 0; dtads.Rows.Count > i; i++)
                        {
                            string mProdRefTime = dtads.Rows[i]["ProductRefNo"].ToString();
                            DataTable dtImageBind = Lo.RetriveProductCode("", mProdRefTime, "RetImageTop", "");
                            if (dtImageBind.Rows.Count > 0)
                            {
                                dtads.Rows[i]["TopImages"] = dtImageBind.Rows[0]["ImageName"].ToString();
                            }
                            else
                            {
                                dtads.Rows[i]["TopImages"] = "assets/images/Noimage.png";
                            }
                        }
                        pgsource.DataSource = dtads.DefaultView;
                        pgsource.AllowPaging = true;
                        pgsource.PageSize = 300;
                        pgsource.CurrentPageIndex = pagingCurrentPage;
                        ViewState["totpage"] = pgsource.PageCount;
                        lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                        lbltotal.Text = "Total " + DtGrid.Rows.Count.ToString() + ", Showing  " + pgsource.PageSize + " result from page " + (pagingCurrentPage + 1) + " out of " + pgsource.PageCount + " pages";
                        lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                        lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                        pgsource.DataSource = dtads.DefaultView;
                        gvproduct.DataSource = dtads.DefaultView;
                    }
                    else
                    {
                        DataView dv3 = new DataView(DtGrid, "ROW_NUMBER >='" + n1 + "' And  ROW_NUMBER<='" + n2 + "'", "", DataViewRowState.CurrentRows);
                        DataTable dtads = dv3.ToTable();
                        dtads.Columns.Add("TopImages", typeof(string));
                        for (int i = 0; dtads.Rows.Count > i; i++)
                        {
                            string mProdRefTime = dtads.Rows[i]["ProductRefNo"].ToString();
                            DataTable dtImageBind = Lo.RetriveProductCode("", mProdRefTime, "RetImageTop", "");
                            if (dtImageBind.Rows.Count > 0)
                            {
                                dtads.Rows[i]["TopImages"] = dtImageBind.Rows[0]["ImageName"].ToString();
                            }
                            else
                            {
                                dtads.Rows[i]["TopImages"] = "assets/images/Noimage.png";
                            }
                        }
                        pgsource.DataSource = DtGrid.DefaultView;
                        pgsource.AllowPaging = true;
                        pgsource.PageSize = 300;
                        pgsource.CurrentPageIndex = pagingCurrentPage;
                        ViewState["totpage"] = pgsource.PageCount;
                        lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                        lbltotal.Text = "Total " + DtGrid.Rows.Count.ToString() + ", Showing  " + pgsource.PageSize + " result from page " + (pagingCurrentPage + 1) + " out of " + pgsource.PageCount + " pages";
                        lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                        lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                        pgsource.DataSource = dtads.DefaultView;
                        gvproduct.DataSource = dtads.DefaultView;
                    }
                    gvproduct.DataBind();
                    gvproduct.Visible = true;
                    divpageindex.Visible = true;
                    divProductGrid.Visible = true;
                }
            }
        }
        else
        {
            divclearfilorexceldown.Visible = false;
            divpageindex.Visible = false;
        }
    }
    private string POProc;
    private string EndUserValue;
    protected void gvproduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        int rowIndex = gvr.RowIndex;
        string Role = (gvproduct.Rows[rowIndex].FindControl("hfroleProd") as HiddenField).Value;
        DataTable DtView = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductMasterID", Role.ToString());
        if (DtView.Rows.Count > 0)
        {
            lblcomprefno.Text = DtView.Rows[0]["CompanyRefNo"].ToString();
            lblcompname.Text = DtView.Rows[0]["CompanyName"].ToString();
            lbldiviname.Text = DtView.Rows[0]["FactoryName"].ToString();
            lblunitnamepro.Text = DtView.Rows[0]["UnitName"].ToString();
            lblprodrefno.Text = DtView.Rows[0]["ProductRefNo"].ToString();
            lblnsngroup.Text = DtView.Rows[0]["ProdLevel1Name"].ToString();
            lblnsngroupclass.Text = DtView.Rows[0]["ProdLevel2Name"].ToString();
            lblclassitem.Text = DtView.Rows[0]["ProdLevel3Name"].ToString();
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
            DataTable dtImageBindfinal = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductImage", "");
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
                    lblemailidpro.Text = dtNodal.Rows[0]["NodalOfficerEmail"].ToString();
                    lblmobilenumber.Text = dtNodal.Rows[0]["NodalOfficerMobile"].ToString();
                    lblphonenumber.Text = dtNodal.Rows[0]["NodalOfficerTelephone"].ToString();
                    lblfaxpro.Text = dtNodal.Rows[0]["NodalOfficerFax"].ToString();
                }
            }
            string Nodel2Id = DtView.Rows[0]["NodalDetail2"].ToString();
            if (Nodel2Id.ToString() != "")
            {
                DataTable dtNodal2 = Lo.RetriveProductCode(Nodel2Id.ToString(), "", "ProdNodal", "");
                if (dtNodal2.Rows.Count == 2)
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
            ScriptManager.RegisterStartupScript(this, GetType(), "ProductCompany", "showPopup4();", true);
        }
    }
    protected void gvproduct_RowCreated(object sender, GridViewRowEventArgs e)
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
        txtpageno.Text = "";
        pagingCurrentPage -= 1;
        n2 = Convert.ToInt16(pagingCurrentPage) * Convert.ToInt16(300);
        n1 = Convert.ToInt16(n2 - 300);
        if (lbclearfilter.Visible == true)
        { SeachResult(); }
        else
        {
            if (hfmtype.Value == "P")
            { BindProduct(hfmref.Value); }
            else if (hfmtype.Value == "PI")
            { BindProduct(hfmref.Value); }
            else if (hfmtype.Value == "M2")
            { BindProduct(hfmref.Value); }
        }
    }
    protected void lnkbtnPgNext_Click(object sender, EventArgs e)
    {
        pagingCurrentPage += 1;
        int txtpage = Convert.ToInt32(pagingCurrentPage) + 1;
        txtpageno.Text = txtpage.ToString();
        n2 = Convert.ToInt16(txtpage) * Convert.ToInt16(300);
        n1 = Convert.ToInt16(n2 - 300);
        if (lbclearfilter.Visible == true)
        { SeachResult(); }
        else
        {
            if (hfmtype.Value == "P")
            { BindProduct(hfmref.Value); }
            else if (hfmtype.Value == "PI")
            { BindProduct(hfmref.Value); }
            else if (hfmtype.Value == "M2")
            { BindProduct(hfmref.Value); }
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
            n2 = Convert.ToInt16(pagingCurrentPage + 1) * Convert.ToInt16(300);
            n1 = Convert.ToInt16(n2 + 1 - 300);
            if (lbclearfilter.Visible == true)
            { SeachResult(); }
            else
            {
                if (hfmtype.Value == "P")
                { BindProduct(hfmref.Value); }
                else if (hfmtype.Value == "PI")
                { BindProduct(hfmref.Value); }
                else if (hfmtype.Value == "M2")
                { BindProduct(hfmref.Value); }
            }
        }
    }
    //end page index---------------------------------------//
    #endregion
    protected void lblback_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard?mu=" + Encrypt.EncryptData(Session["Type"].ToString()) + "&id=" + Encrypt.EncryptData(Session["CompanyRefNo"].ToString()));
    }
    protected void gvproduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hy = (e.Row.FindControl("lbpdffile") as HyperLink);
            HiddenField hfisapproved = (e.Row.FindControl("hfisaaproved") as HiddenField);
            if (hy.NavigateUrl.Trim() == "" || hy.NavigateUrl.Trim() == "~/Upload/" || hy.NavigateUrl.Trim() == null)
            {
                e.Row.Cells[2].Text = "NA";
            }
            else
            { }
            if (hfisapproved.Value == "Y")
            {
                gvproduct.BackColor = Color.Green;
            }
            else if (hfisapproved.Value == "N")
            {
                gvproduct.BackColor = Color.Red;
            }
        }
    }
    #region Filtor or CheckBoxCode
    protected void BindComapnyCheckbox()
    {

        DataTable DtCompany = new DataTable();
        if (Encrypt.DecryptData(Session["Type"].ToString()) == "Admin" || Encrypt.DecryptData(Session["Type"].ToString()) == "SuperAdmin")
        {
            DtCompany = Lo.GetDashboardData("CompOnProdtbl", "");
            if (DtCompany.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcomp, DtCompany, "CompanyName", "CompanyRefNo");
                ddlcomp.Items.Insert(0, "Select");
            }
        }
        else
        {
            DtCompany = Lo.GetDashboardData("CompByrefno", Session["CompanyRefNo"].ToString());
            if (DtCompany.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcomp, DtCompany, "CompanyName", "CompanyRefNo");
                ddlcomp.Items.Insert(0, "Select");
                BindComapnyDivisionCheckbox();
            }
        }

    }
    protected void BindComapnyDivisionCheckbox()
    {
        try
        {
            DataTable dtFactory = Lo.GetDashboardData("DivisionByname", ddlcomp.SelectedItem.Value);
            if (dtFactory.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, dtFactory, "FactoryName", "FactoryRefNo");
                ddldivision.Items.Insert(0, "Select");
                divfilterdivision.Visible = true;
            }
        }
        catch (Exception ex)
        { }
    }
    protected void BindComapnyDivisionUnitCheckbox()
    {
        try
        {
            DataTable dtUnit = Lo.GetDashboardData("UnitByname", ddldivision.SelectedItem.Value);
            if (dtUnit.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlunit, dtUnit, "UnitName", "UnitRefNo");
                ddlunit.Items.Insert(0, "Select");
                divfilterunit.Visible = true;
            }
        }
        catch (Exception ex)
        { }
    }
    protected void BindEndUser()
    {
        try
        {
            DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "End User", "", "SelectInnerMaster1", "", "");
            if (DtMasterCategroy.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlchkenduser, DtMasterCategroy, "SCategoryName", "SCategoryID");
                ddlchkenduser.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        { }
    }
    protected void BindDefencePlatform()
    {
        try
        {
            DataTable DtMasterCategroy = new DataTable();
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "", "", "DefPlatProd", "", "");
            if (DtMasterCategroy.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldefplatform, DtMasterCategroy, "SCategoryName", "SCategoryID");
                ddldefplatform.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        { }
    }
    protected void BindNameofDefencePlatform()
    {
        try
        {
            DataTable DtNAMEOFDEFENCEPLATFORM = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddldefplatform.SelectedItem.Value), "", "", "SubSelectID", "", "");
            if (DtNAMEOFDEFENCEPLATFORM.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlnameofdefplat, DtNAMEOFDEFENCEPLATFORM, "SCategoryName", "SCategoryId");
                ddlnameofdefplat.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        { }
    }
    protected void BindIndusrtyDomain()
    {
        try
        {
            DataTable DtMasterCategroy = new DataTable();
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "", "", "DefPlatIndus", "", "");
            if (DtMasterCategroy.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlprodindustrydomain, DtMasterCategroy, "SCategoryName", "SCategoryID");
                ddlprodindustrydomain.Items.Insert(0, "Select");
            }
            else
            {
                divfilterpisd.Visible = false;
            }
        }
        catch (Exception ex)
        { }
    }
    protected void BindIndustrySubDomain()
    {
        try
        {
            DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlprodindustrydomain.SelectedItem.Value), "", "", "DefPlatIndusSub", "", "");
            if (DtMasterCategroy.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlprodindussubdomain, DtMasterCategroy, "SCategoryName", "SCategoryId");
                ddlprodindussubdomain.Items.Insert(0, "Select");
                divfilterpisd.Visible = true;
            }
            else
            {
                divfilterpisd.Visible = false;
            }
        }
        catch (Exception ex)
        { }
    }
    protected void BindCountry()
    {
        DataTable DtCountry = Lo.RetriveCountry(0, "CntryByMProd");
        if (DtCountry.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlcountry, DtCountry, "CountryName", "CountryID");
            ddlcountry.Items.Insert(0, "Select");
        }
    }
    protected void BindSearchKeyword()
    {
        DataTable dtsearchkey = Lo.RetriveFilterCode(hfmref.Value, "", "SearchKeyword");
        if (dtsearchkey.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlsearchkeywordsfilter, dtsearchkey, "SearchKeyword", "SearchKeyword");
            ddlsearchkeywordsfilter.Items.Insert(0, "Select");
        }
    }
    #endregion
    #region Filter CheckBox Code
    protected void rbistender_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    protected void rbiscontact_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    protected void rbismake2_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    protected void rbisindezinized_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    protected void ddlsearchkeywordsfilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    protected void ddlcomp_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindComapnyDivisionCheckbox();
        SeachResult();
    }
    protected void ddldivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindComapnyDivisionUnitCheckbox();
        SeachResult();

    }
    protected void ddlunit_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    protected void ddlchkenduser_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    protected void ddldefplatform_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindNameofDefencePlatform();
        SeachResult();
    }
    protected void ddlnameofdefplat_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    protected void ddlprodindustrydomain_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindIndustrySubDomain();
        SeachResult();
    }
    protected void ddlprodindussubdomain_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    #endregion
    #region Search Code Filter Code
    protected DataTable insert()
    {
        DataTable insert = new DataTable();
        insert.Columns.Add(new DataColumn("Column", typeof(string)));
        insert.Columns.Add(new DataColumn("Value", typeof(string)));
        DataRow dr;
        if (ddlcomp.SelectedItem.Text != "Select")
        {
            dr = insert.NewRow();
            dr["Column"] = "C.CompanyRefNo" + "=";
            dr["Value"] = "'" + ddlcomp.SelectedItem.Value + "'";
            if (ddldivision.Visible == true && ddldivision.SelectedItem.Text != "Select")
            {
                dr = insert.NewRow();
                dr["Column"] = "F.FactoryRefNo" + " =";
                dr["Value"] = "'" + ddldivision.SelectedItem.Value + "' or FC.CompanyRefNo='" + ddlcomp.SelectedItem.Value + "'";
            }
            if (ddlunit.Visible == true && ddlunit.SelectedItem.Text != "Select")
            {
                dr = insert.NewRow();
                dr["Column"] = "U.UnitRefNo" + " =";
                dr["Value"] = "'" + ddlunit.SelectedItem.Value + "' or UF.FactoryRefNo='" + ddldivision.SelectedItem.Value + "' or UC.CompanyRefNo='" + ddlcomp.SelectedItem.Value + "'";
            }
            insert.Rows.Add(dr);
        }
        if (ddlchkenduser.SelectedItem.Text != "Select")
        {
            dr = insert.NewRow();
            dr["Column"] = "P.EndUser" + " like ";
            dr["Value"] = "'%" + ddlchkenduser.SelectedItem.Value + "%'";
            insert.Rows.Add(dr);
        }
        if (ddldefplatform.SelectedItem.Text != "Select")
        {
            dr = insert.NewRow();
            dr["Column"] = "P.Platform" + "=";
            dr["Value"] = "'" + ddldefplatform.SelectedItem.Value + "'";
            insert.Rows.Add(dr);
            if (ddlnameofdefplat.Visible = true && ddlnameofdefplat.SelectedItem.Text != "Select")
            {
                dr = insert.NewRow();
                dr["Column"] = "P.NomenclatureOfMainSystem" + "="; ;
                dr["Value"] = "'" + ddlnameofdefplat.SelectedItem.Value + "'";
                insert.Rows.Add(dr);
            }
        }

        if (ddlprodindustrydomain.SelectedItem.Text != "Select")
        {
            dr = insert.NewRow();
            dr["Column"] = "P.TechnologyLevel1" + "="; ;
            dr["Value"] = "'" + ddlprodindustrydomain.SelectedItem.Value + "'";
            insert.Rows.Add(dr);
            if (ddlprodindussubdomain.Visible = true && ddlprodindussubdomain.SelectedItem.Text != "Select")
            {
                dr = insert.NewRow();
                dr["Column"] = "P.TechnologyLevel2" + "="; ;
                dr["Value"] = "'" + ddlprodindussubdomain.SelectedItem.Value + "'";
                insert.Rows.Add(dr);
            }
        }

        if (ddlcountry.SelectedItem.Text != "Select")
        {
            dr = insert.NewRow();
            dr["Column"] = "P.OEMCountry" + "="; ;
            dr["Value"] = "'" + ddlcountry.SelectedItem.Value + "'";
            insert.Rows.Add(dr);
        }
        if (ddlsearchkeywordsfilter.SelectedItem.Text != "Select")
        {
            dr = insert.NewRow();
            dr["Column"] = "P.SearchKeyword" + "="; ;
            dr["Value"] = "'" + ddlsearchkeywordsfilter.SelectedItem.Text + "'";
            insert.Rows.Add(dr);
        }

        if (rbisindezinized.SelectedIndex != -1 && rbisindezinized.SelectedItem.Value != "C")
        {
            dr = insert.NewRow();
            dr["Column"] = "P.IsIndeginized" + "="; ;
            dr["Value"] = "'" + rbisindezinized.SelectedItem.Value + "'";
            insert.Rows.Add(dr);
        }
        if (rbismake2.SelectedIndex != -1 && rbismake2.SelectedItem.Value != "C")
        {
            dr = insert.NewRow();
            dr["Column"] = "P.PurposeofProcurement" + " like";
            dr["Value"] = "'%" + 25 + "%'";
            insert.Rows.Add(dr);
        }
        if (rbiscontact.SelectedIndex != -1 && rbiscontact.SelectedItem.Value != "C")
        {
            if (rbiscontact.SelectedItem.Value == "Y")
            {
                dr = insert.NewRow();
                dr["Column"] = "P.NodelDetail" + " is not";
                dr["Value"] = " NULL or P.NodelDetail!=''";
                insert.Rows.Add(dr);
            }
            else
            {
            }
        }
        if (rbistender.SelectedIndex != -1 && rbistender.SelectedItem.Value != "C")
        {
            if (rbistender.SelectedItem.Value == "Y")
            {
                dr = insert.NewRow();
                dr["Column"] = "P.TenderStatus" + " =";
                dr["Value"] = "'Live'";
                insert.Rows.Add(dr);
            }
            else
            {
            }
        }
        if (hfmtype.Value == "PI")
        {
            dr = insert.NewRow();
            dr["Column"] = "P.IsIndeginized" + "=";
            dr["Value"] = "'Y'";
            insert.Rows.Add(dr);
        }
        if (hfmtype.Value == "M2")
        {
            dr = insert.NewRow();
            dr["Column"] = "P.PurposeofProcurement" + " like";
            dr["Value"] = "'%" + 25 + "%'";
            insert.Rows.Add(dr);
        }
        if (hfmref.Value != "")
        {
            dr = insert.NewRow();
            dr["Column"] = "P.CompanyRefNo" + "=";
            dr["Value"] = "'" + hfmref.Value + "'";
            insert.Rows.Add(dr);
        }
        return insert;
    }
    protected DataTable BindInsert()
    {
        return Lo.GetProductFilterData(this.insert(), "ProductSearch", "", "");
    }
    protected void SeachResult()
    {
        try
        {
            DataTable DtGridFilter = new DataTable();
            DtGridFilter = this.BindInsert();
            if (DtGridFilter.Rows.Count > 0)
            {
                UpdateDtGridValue(DtGridFilter);
                DtGridFilter.Columns.Add("TopImages", typeof(string));
                for (int i = 0; DtGridFilter.Rows.Count > i; i++)
                {
                    string mProdRefTime = DtGridFilter.Rows[i]["ProductRefNo"].ToString();
                    DataTable dtImageBind = Lo.RetriveProductCode("", mProdRefTime, "RetImageTop", "");
                    if (dtImageBind.Rows.Count > 0)
                    {
                        DtGridFilter.Rows[i]["TopImages"] = dtImageBind.Rows[0]["ImageName"].ToString();
                    }
                    else
                    {
                        DtGridFilter.Rows[i]["TopImages"] = "assets/images/Noimage.png";
                    }
                }
                pgsource.DataSource = DtGridFilter.DefaultView;
                pgsource.AllowPaging = true;
                pgsource.PageSize = 300;
                pgsource.CurrentPageIndex = pagingCurrentPage;
                ViewState["totpage"] = pgsource.PageCount;
                lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                pgsource.DataSource = DtGridFilter.DefaultView;
                gvproduct.DataSource = pgsource;
                gvproduct.DataBind();
                lbltotal.Text = "Total " + DtGridFilter.Rows.Count.ToString() + ", Showing  " + gvproduct.Rows.Count.ToString() + " result from page " + (pagingCurrentPage + 1) + " out of " + pgsource.PageCount + " pages";
                divpageindex.Visible = true;
                divProductGrid.Visible = true;
                divpageindex.Visible = true;
                FilterSelectName();
            }
            else
            {
                FilterSelectName();
                divpageindex.Visible = false;
                gvproduct.Visible = false;
                lbltotal.Text = "No Record Found. Please select valid keyword";
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void lbclearfilter_Click(object sender, EventArgs e)
    {
        ddlcomp.SelectedValue = "Select";
        ddldivision.SelectedValue = "Select";
        ddlunit.SelectedValue = "Select";
        ddlchkenduser.SelectedValue = "Select";
        ddldefplatform.SelectedValue = "Select";
        ddlnameofdefplat.SelectedValue = "Select";
        ddlprodindustrydomain.SelectedValue = "Select";
        ddlprodindussubdomain.SelectedValue = "Select";
        ddlcountry.SelectedValue = "Select";
        ddlsearchkeywordsfilter.SelectedValue = "Select";
        rbiscontact.SelectedIndex = -1;
        rbisindezinized.SelectedIndex = -1;
        rbismake2.SelectedIndex = -1;
        rbistender.SelectedIndex = -1;
        lblfilter1.Visible = false; lblfilter2.Visible = false; lblfilter3.Visible = false; lblfilter4.Visible = false; lblfilter5.Visible = false; lblfilter6.Visible = false;
        lblfilter12.Visible = false; lblfilter11.Visible = false; lblfilter10.Visible = false; lblfilter9.Visible = false; lblfilter8.Visible = false; lblfilter7.Visible = false;
        lblfilter13.Visible = false; lblfilter14.Visible = false;
        lbclearfilter.Visible = false;
        BindProduct(hfmref.Value);
    }
    #endregion
    protected void lbldownloadexcel_Click(object sender, EventArgs e)
    {

        DtGrid = Lo.GetDashboardData("Product", "");
        DataView dv = new DataView(DtGrid);
        if (hfmtype.Value == "PI")
        {
            dv.RowFilter = "IsIndeginized='Y'";
        }
        else if (hfmtype.Value == "M2")
        {
            dv.RowFilter = "PurposeofProcurement like'%25%'";
        }
        if (ddlcomp.SelectedItem.Value != "")
        { dv.RowFilter = "CompanyRefNo='" + ddlcomp.SelectedItem.Value + "'"; }
        if (ddldivision.SelectedIndex != -1)
        { dv.RowFilter = "CompanyRefNo='" + ddldivision.SelectedItem.Value + "'"; }
        if (ddlunit.SelectedIndex != -1)
        { dv.RowFilter = "CompanyRefNo='" + ddldivision.SelectedItem.Value + "'"; }
        if (ddlchkenduser.SelectedItem.Value != "")
        { dv.RowFilter = "EndUser='" + ddlchkenduser.SelectedItem.Value + "'"; }
        if (ddldefplatform.SelectedItem.Value != "")
        { dv.RowFilter = "Platform='" + ddldefplatform.SelectedItem.Value + "'"; }
        if (ddlnameofdefplat.SelectedIndex != -1)
        { dv.RowFilter = "NomenclatureOfMainSystem='" + ddlnameofdefplat.SelectedItem.Value + "'"; }
        if (ddlprodindustrydomain.SelectedItem.Value != "")
        { dv.RowFilter = "TechnologyLevel1='" + ddlprodindustrydomain.SelectedItem.Value + "'"; }
        if (ddlprodindussubdomain.SelectedIndex != -1)
        { dv.RowFilter = "TechnologyLevel2='" + ddlprodindussubdomain.SelectedItem.Value + "'"; }
        if (ddlcountry.SelectedItem.Value != "")
        { dv.RowFilter = "OEMCountry='" + ddlcountry.SelectedItem.Value + "'"; }
        if (ddlsearchkeywordsfilter.SelectedItem.Value != "")
        { dv.RowFilter = "SearchKeyword='" + ddlsearchkeywordsfilter.SelectedItem.Value + "'"; }
        if (Encrypt.DecryptData(Session["Type"].ToString()) != "Admin" && Encrypt.DecryptData(Session["Type"].ToString()) != "SuperAdmin")
        {
            if (DtGrid.Rows.Count > 0)
            {
                this.UpdateDtGridValue(DtGrid);
                if (Encrypt.DecryptData(Session["Type"].ToString()).ToUpper() == "COMPANY")
                    dv.RowFilter = "CompanyRefNo='" + Session["CompanyRefNo"].ToString() + "'";
                else if (Encrypt.DecryptData(Session["Type"].ToString()).ToUpper() == "DIVISION")
                    dv.RowFilter = "FactoryRefNo='" + Session["CompanyRefNo"].ToString() + "'";
                else
                    dv.RowFilter = "UnitRefNo='" + Session["CompanyRefNo"].ToString() + "'";
            }
        }
        dv.Sort = "CompanyName asc,FactoryName asc";
        dv.Table.Columns["FactoryName"].ColumnName = "DivisionName";
        try
        {
            int[] iColumns = { 2, 4, 6, 7, 9, 11, 18, 19, 20, 21, 22, 24, 25, 57, 60, 58, 59, 62, 61 };
            RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
            objExport.ExportDetails(dv.ToTable(), iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "Product.xls");
        }
        catch (Exception ex)
        {

        }
    }
    #region Clear Filter Select Result
    protected void FilterSelectName()
    {
        if (ddlcomp.SelectedItem.Text != "Select")
        {
            lblfilter5.Text = ddlcomp.SelectedItem.Text;
            lblfilter5.Visible = true;
            lbclearfilter.Visible = true;
        }
        if (ddldivision.Visible == true && ddldivision.SelectedItem.Text != "Select")
        {
            lblfilter6.Text = ddldivision.SelectedItem.Text;
            lblfilter6.Visible = true; lbclearfilter.Visible = true;
        }
        if (ddlunit.Visible == true && ddlunit.SelectedItem.Text != "Select")
        {
            lblfilter7.Text = ddlunit.SelectedItem.Text;
            lblfilter7.Visible = true; lbclearfilter.Visible = true;
        }
        if (ddlchkenduser.SelectedItem.Text != "Select")
        {
            lblfilter8.Text = ddlchkenduser.SelectedItem.Text;
            lblfilter8.Visible = true; lbclearfilter.Visible = true;
        }
        if (ddldefplatform.SelectedItem.Text != "Select")
        {
            lblfilter9.Text = ddldefplatform.SelectedItem.Text;
            lblfilter9.Visible = true; lbclearfilter.Visible = true;
        }
        if (ddlnameofdefplat.Visible = true && ddlnameofdefplat.SelectedIndex != -1)
        {
            lblfilter10.Text = ddlnameofdefplat.SelectedItem.Text;
            lblfilter10.Visible = true; lbclearfilter.Visible = true;
        }
        if (ddlprodindustrydomain.SelectedItem.Text != "Select")
        {
            lblfilter11.Text = ddlprodindustrydomain.SelectedItem.Text;
            lblfilter11.Visible = true; lbclearfilter.Visible = true;
        }
        if (ddlprodindussubdomain.Visible = true && ddlprodindussubdomain.SelectedIndex != -1)
        {
            lblfilter12.Text = ddlprodindussubdomain.SelectedItem.Text;
            lblfilter12.Visible = true; lbclearfilter.Visible = true;
        }
        if (ddlcountry.SelectedItem.Text != "Select")
        {
            lblfilter13.Text = ddlcountry.SelectedItem.Text;
            lblfilter13.Visible = true; lbclearfilter.Visible = true;
        }
        if (ddlsearchkeywordsfilter.SelectedItem.Text != "Select")
        {
            lblfilter14.Text = ddlsearchkeywordsfilter.SelectedItem.Text;
            lblfilter14.Visible = true; lbclearfilter.Visible = true;
        }
        if (rbisindezinized.SelectedIndex != -1 && rbisindezinized.SelectedItem.Value != "C")
        {
            lblfilter1.Text = "Is Indigenized -" + rbisindezinized.SelectedItem.Text;
            lblfilter1.Visible = true; lbclearfilter.Visible = true;
        }
        if (rbismake2.SelectedIndex != -1 && rbismake2.SelectedItem.Value != "C")
        {
            lblfilter2.Text = "Is Make II -" + rbismake2.SelectedItem.Text;
            lblfilter2.Visible = true; lbclearfilter.Visible = true;
        }
        if (rbiscontact.SelectedIndex != -1 && rbiscontact.SelectedItem.Value != "C")
        {
            lblfilter3.Text = "Is Contact -" + rbiscontact.SelectedItem.Text;
            lblfilter3.Visible = true; lbclearfilter.Visible = true;
        }
        if (rbistender.SelectedIndex != -1 && rbistender.SelectedItem.Value != "C")
        {
            lblfilter4.Text = "Is Tender -" + rbistender.SelectedItem.Text;
            lblfilter4.Visible = true; lbclearfilter.Visible = true;
        }

    }
    protected void lblfilter1_Click(object sender, EventArgs e)
    {
        rbisindezinized.SelectedIndex = -1;
        SeachResult();
        lblfilter1.Visible = false;
    }
    protected void lblfilter2_Click(object sender, EventArgs e)
    {
        rbismake2.SelectedIndex = -1;
        SeachResult();
        lblfilter2.Visible = false;
    }
    protected void lblfilter3_Click(object sender, EventArgs e)
    {
        rbiscontact.SelectedIndex = -1;
        SeachResult();
        lblfilter3.Visible = false;
    }
    protected void lblfilter4_Click(object sender, EventArgs e)
    {
        rbistender.SelectedIndex = -1;
        SeachResult();
        lblfilter4.Visible = false;
    }
    protected void lblfilter5_Click(object sender, EventArgs e)
    {
        ddlcomp.SelectedIndex = -1;
        SeachResult();
        lblfilter5.Visible = false;
    }
    protected void lblfilter6_Click(object sender, EventArgs e)
    {
        ddldivision.SelectedIndex = -1;
        SeachResult();
        lblfilter6.Visible = false;
    }
    protected void lblfilter7_Click(object sender, EventArgs e)
    {
        ddlunit.SelectedIndex = -1;
        SeachResult();
        lblfilter7.Visible = false;
    }
    protected void lblfilter8_Click(object sender, EventArgs e)
    {
        ddlchkenduser.SelectedIndex = -1;
        SeachResult();
        lblfilter8.Visible = false;
    }
    protected void lblfilter9_Click(object sender, EventArgs e)
    {
        ddldefplatform.SelectedIndex = -1;
        SeachResult();
        lblfilter9.Visible = false;
    }
    protected void lblfilter10_Click(object sender, EventArgs e)
    {
        ddlnameofdefplat.SelectedIndex = -1;
        SeachResult(); lblfilter10.Visible = false;
    }
    protected void lblfilter11_Click(object sender, EventArgs e)
    {
        ddlprodindustrydomain.SelectedIndex = -1;
        SeachResult(); lblfilter11.Visible = false;
    }
    protected void lblfilter12_Click(object sender, EventArgs e)
    {
        ddlprodindussubdomain.SelectedIndex = -1;
        SeachResult(); lblfilter12.Visible = false;
    }
    protected void lblfilter13_Click(object sender, EventArgs e)
    {
        ddlcountry.SelectedIndex = -1;
        SeachResult(); lblfilter13.Visible = false;
    }
    protected void lblfilter14_Click(object sender, EventArgs e)
    {
        ddlsearchkeywordsfilter.SelectedIndex = -1;
        SeachResult(); lblfilter14.Visible = false;
    }
    #endregion
}