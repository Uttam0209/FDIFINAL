using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;
using Encryption;
public partial class Admin_ViewRequestInfo : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Cryptography Enc = new Cryptography();
    DataUtility Co = new DataUtility();
    private PagedDataSource pgsource = new PagedDataSource();
    DataTable DtReq = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindRequest();
        }
    }
    protected void BindRequest()
    {
        if (Session["User"] != null)
        {
            hfType.Value = Enc.DecryptData(Session["Type"].ToString().Trim());
            hfCompRefNo.Value = Session["CompanyRefNo"].ToString().Trim();
            //DtReq = Lo.RetriveGridViewCompany("", hfCompRefNo.Value, hfType.Value, "retdata");
            DtReq = Lo.RetriveGridViewCompany("", hfCompRefNo.Value, hfType.Value, "RetBindData");
            if (DtReq.Rows.Count > 0)
            {
                Session["TempData"] = DtReq;
                pgsource.DataSource = DtReq.DefaultView;
                pgsource.AllowPaging = true;
                pgsource.PageSize = 52;
                pgsource.CurrentPageIndex = pagingCurrentPage;
                lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                // LinkButton1.Enabled = !pgsource.IsFirstPage;
                lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                // LinkButton2.Enabled = !pgsource.IsLastPage;
                dlrequest.DataSource = pgsource;
                dlrequest.DataBind();
                lbltotalshowpageitem.Text = pgsource.FirstIndexInPage + 1 + " - " + (pgsource.FirstIndexInPage + pgsource.Count);
                lbltotfilter.Text = DtReq.Rows.Count.ToString();
                BindCompany();
                //Code of Count Product
                DataTable mtable = new DataTable();
                DataColumn ProdRef = mtable.Columns.Add("MProduct", typeof(string));
                for (int i = 0; DtReq.Rows.Count > i; i++)
                {
                    string source = DtReq.Rows[i]["RequestProduct"].ToString();
                    string[] lines = source.Split(',');
                    foreach (var line in lines)
                    {
                        string[] split = line.Split(',');
                        DataRow row = mtable.NewRow();
                        row.SetField(ProdRef, split[0]);
                        mtable.Rows.Add(row);
                    }
                }
                DataTable uniqueCols = mtable.DefaultView.ToTable(true, "MProduct");
                lbltotprodreq.Text =  uniqueCols.Rows.Count.ToString();
            }
            else
            {
                dlrequest.Visible = false;
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "ErrorMssgPopup('No Record Found')", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "ErrorMssgPopup('Session Expired,Please login again');window.location='Login'", true);
        }
    }
    protected void gvRequestInfo_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void dlrequest_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            try
            {
                DataListItem item = (DataListItem)(((Control)(e.CommandSource)).NamingContainer);
                string email = ((Label)item.FindControl("lblemail")).Text;
                string mdate = e.CommandArgument.ToString();
                DataTable dtgetProd = new DataTable();
                if (hfType.Value == "SuperAdmin" || hfType.Value == "Admin")
                {
                    dtgetProd = Lo.RetriveGridViewCompany(mdate.ToString(), email, hfType.Value, "RetReqProd");
                }
                else
                {
                    dtgetProd = Lo.RetriveGridViewCompany(mdate.ToString(), email, hfCompRefNo.Value, "RetReqProd");
                }
                if (dtgetProd.Rows.Count > 0)
                {
                    gvViewNodalOfficerAdd.DataSource = dtgetProd;
                    gvViewNodalOfficerAdd.DataBind();
                    pan1.Visible = true;
                    panview.Visible = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "aboutus", "showPopup();", true);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "ErrorMssgPopup('No Record Found')", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "ErrorMssgPopup('Error:- " + ex.Message.ToString() + ")", true);
            }

        }
    }
    protected void gvViewNodalOfficerAdd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region ViewOneProd
        if (e.CommandName == "View")
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
                        gvestimatequanold.DataSource = DtGridEstimate1;
                        gvestimatequanold.DataBind();
                        gvestimatequanold.Visible = true;
                        decimal tot = 0;
                        for (int i = 0; DtGridEstimate1.Rows.Count > i; i++)
                        {
                            tot = tot + Convert.ToDecimal(DtGridEstimate1.Rows[i]["EstimatedPrice"]);
                        }
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
                        fourteen.Visible = false;
                    }
                    else
                    {
                        fourteen.Visible = false;
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
                    pan1.Visible = false;
                    panview.Visible = true;
                    ScriptManager.RegisterStartupScript(this, GetType(), "aboutus", "showPopup();", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
            }
        }
        #endregion
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        panview.Visible = false;
        pan1.Visible = true;
    }
    #region pageindex code
    protected void lnkbtnPgPrevious_Click(object sender, EventArgs e)
    {
        pagingCurrentPage -= 1;
        BindRequest();
    }
    protected void lnkbtnPgNext_Click(object sender, EventArgs e)
    {
        pagingCurrentPage += 1;
        BindRequest();
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
    #endregion
    protected void btnexcelexport_Click(object sender, EventArgs e)
    {
        if (txtsearch.Text != "" || txtenddate.Text != "" || txtstartdate.Text != "")
        {
            SeachResult();
            if (ViewState["mExcel"] != null)
            {
                DataTable msdt = (DataTable)ViewState["mExcel"];
                int[] iColumns = { 0, 1, 2, 3, 4 };
                RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
                objExport.ExportDetails(msdt, iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "Intrested.xls");
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
            }
        }
        else
        {
            DataTable dtm = new DataTable();
            dtm = (DataTable)Session["TempData"];
            int[] iColumns = { 0, 1, 2, 3, 4 };
            RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
            objExport.ExportDetails(dtm, iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "Intrested.xls");
        }
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        if (txtsearch.Text != "" || txtenddate.Text != "" || txtstartdate.Text != "")
        {
            if (txtsearch.Text != "" && txtenddate.Text != "" && txtstartdate.Text != "")
            {
                DateTime old = Convert.ToDateTime(txtstartdate.Text);
                DateTime mnew = Convert.ToDateTime(txtenddate.Text);
                if (old > mnew)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Start date could not be greater then End date')", true);
                }
                else
                {
                    SeachResult();
                }
            }
            else if (txtsearch.Text != "")
            { SeachResult(); }
            else if (txtenddate.Text != "" && txtstartdate.Text != "")
            {
                DateTime old = Convert.ToDateTime(txtstartdate.Text);
                DateTime mnew = Convert.ToDateTime(txtenddate.Text);
                if (old > mnew)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Start date could not be greater then End date')", true);
                }
                else
                {
                    SeachResult();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Please select valid date.')", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Please select or enter search criteria.')", true);
        }
    }
    #region searchcode
    string insert1 = "";
    protected string Dvinsert(string sortExpression = null)
    {
        DataTable insert = new DataTable();
        insert.Columns.Add(new DataColumn("Column", typeof(string)));
        insert.Columns.Add(new DataColumn("Value", typeof(string)));
        DataRow dr;
        if (txtstartdate.Text != "" && txtenddate.Text != "")
        {
            DateTime sdate = Convert.ToDateTime(txtstartdate.Text);
            DateTime edate = Convert.ToDateTime(txtenddate.Text);
            dr = insert.NewRow();
            dr["Column"] = "RequestDate >= ";
            dr["Value"] = "'" + sdate + "' and RequestDate <= '" + edate + "'";
            insert.Rows.Add(dr);
        }
        if (txtsearch.Text.Trim() != "")
        {
            dr = insert.NewRow();
            dr["Column"] = "((RequestBy like ";
            dr["Value"] = "'%" + txtsearch.Text.Trim() + "%') or (RequestCompName like '%" + txtsearch.Text.Trim() + "%') or (RequestEmail like '%" + txtsearch.Text.Trim() + "%') or RequestProduct like '%" + txtsearch.Text.Trim() + "%%')";
            insert.Rows.Add(dr);
        }
        if (ddlcompany.SelectedItem.Text != "Select")
        {
            dr = insert.NewRow();
            dr["Column"] = "RequestCompName" + " like";
            dr["Value"] = "'%" + ddlcompany.SelectedItem.Text + "%'";
            insert.Rows.Add(dr);
            //if (ddldivision.Visible == true && ddldivision.SelectedItem.Text != "Select")
            //{
            //    dr = insert.NewRow();
            //    dr["Column"] = "RequestCompName" + " like";
            //    dr["Value"] = "'%" + ddldivision.SelectedItem.Text + "%'";
            //    insert.Rows.Add(dr);
            //    if (ddlunit.Visible == true && ddlunit.SelectedItem.Text != "Select")
            //    {
            //        dr = insert.NewRow();
            //        dr["Column"] = "RequestCompName" + " like";
            //        dr["Value"] = "'%" + ddlunit.SelectedItem.Text + "%'";
            //        insert.Rows.Add(dr);
            //    }
            //}
        }
        for (int i = 0; insert.Rows.Count > i; i++)
        {
            insert1 = insert1 + insert.Rows[i]["Column"].ToString() + " " + insert.Rows[i]["Value"].ToString() + " " + " and ";
        }
        if (insert1.ToString() != "")
        {
            insert1 = insert1.Substring(0, insert1.Length - 5);
        }
        return insert1;
    }
    protected string BindInsertfilter()
    {
        return Dvinsert();
    }
    DataTable DtFilterView = new DataTable();
    public void SeachResult(string sortExpression = null)
    {
        try
        {
            DtFilterView = (DataTable)Session["TempData"];
            if (DtFilterView.Rows.Count > 0)
            {
                DataView dv = new DataView(DtFilterView);
                DataTable dtnew = dv.ToTable();
                if (dtnew.Rows.Count > 0)
                {
                    dv.RowFilter = BindInsertfilter();
                    DataTable dtinner = dv.ToTable();
                    if (dtinner.Rows.Count > 0)
                    {
                        lbltotfilter.Text = dtinner.Rows.Count.ToString();
                        DataTable dtads = dv.ToTable();
                        if (dtads.Rows.Count > 0)
                        {
                            ViewState["mExcel"] = dtads;
                            pgsource.DataSource = dtinner.DefaultView;
                            pgsource.AllowPaging = true;
                            pgsource.PageSize = 52;
                            pgsource.CurrentPageIndex = pagingCurrentPage;
                            lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                            lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                            //  LinkButton1.Enabled = !pgsource.IsFirstPage;
                            lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                            //  LinkButton2.Enabled = !pgsource.IsLastPage;
                            pgsource.DataSource = dtads.DefaultView;
                            dlrequest.DataSource = pgsource;
                            dlrequest.DataBind();
                            lbltotalshowpageitem.Text = pgsource.FirstIndexInPage + 1 + " - " + (pgsource.FirstIndexInPage + pgsource.Count);

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    #endregion
    #region MasterEntryofCompUnitDivi
    DataTable DtCompanyDDL = new DataTable();
    protected void BindCompany()
    {
        if (hfType.Value == "SuperAdmin" || hfType.Value == "Admin")
        {
            if (Request.QueryString["mcurrentcompRefNo"] != null)
            {
                ddlcompany.Enabled = false;
                if (Enc.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Company")
                {
                    DtCompanyDDL = Lo.RetriveMasterData(0, HttpUtility.UrlEncode(Enc.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString())), "Company", 0, "", "", "CompanyName");
                    ddlcompany.Enabled = false;
                    if (DtCompanyDDL.Rows.Count > 0)
                    {
                        Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                        //   divlblselectunit.Visible = false;
                        //   divlblselectdivison.Visible = false;
                    }
                }
                else if (Enc.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Factory" || Enc.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Division")
                {
                    //  DtCompanyDDL = Lo.RetriveMasterData(0, Enc.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "Company1", 0, "", "", "CompanyName");
                    //  ddlcompany.Enabled = false;
                    //  if (DtCompanyDDL.Rows.Count > 0)
                    //  {
                    //      Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                    //  }
                    //  DtCompanyDDL = Lo.RetriveMasterData(0, Enc.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "Factory", 0, "", "", "CompanyName");
                    //  DataTable DtDivisionDDL = Lo.RetriveMasterData(0, DtCompanyDDL.Rows[0]["CompanyRefNo"].ToString(), "Factory1", 0, "", "", "CompanyName");
                    //    if (DtDivisionDDL.Rows.Count > 0)
                    //    {
                    //  Co.FillDropdownlist(ddldivision, DtDivisionDDL, "FactoryName", "FactoryRefNo");
                    //  ddldivision.Enabled = false;
                    //      ddlcompany.Enabled = false;
                    //  ddldivision.Visible = true;
                    // divlblselectunit.Visible = false;
                    //   }
                    //   else
                    //   {
                    //  ddldivision.Enabled = false;
                    //   }
                }
                else if (Enc.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Unit")
                {
                    // DtCompanyDDL = Lo.RetriveMasterData(0, Enc.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "Company2", 0, "", "", "CompanyName");
                    //  ddlcompany.Enabled = false;
                    //  if (DtCompanyDDL.Rows.Count > 0)
                    //   {
                    //       Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                    //   }
                    // DtCompanyDDL = Lo.RetriveMasterData(0, Enc.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "Unit", 0, "", "", "CompanyName");
                    //  DataTable DtDivisionDDL = Lo.RetriveMasterData(0, DtCompanyDDL.Rows[0]["CompanyRefNo"].ToString(), "Factory1", 0, "", "", "CompanyName");
                    //  if (DtDivisionDDL.Rows.Count > 0)
                    //  {
                    //    Co.FillDropdownlist(ddldivision, DtDivisionDDL, "FactoryName", "FactoryRefNo");
                    //   divlblselectdivison.Visible = true;
                    //   ddldivision.Enabled = false;
                    //   ddlcompany.Enabled = false;
                    //     ddldivision.Visible = true;
                    //  DataTable DtUnitDDL = Lo.RetriveMasterData(0, DtDivisionDDL.Rows[0]["FactoryRefNo"].ToString(), "Unit1", 0, "", "", "CompanyName");
                    //   if (DtUnitDDL.Rows.Count > 0)
                    //    {
                    //       Co.FillDropdownlist(ddlunit, DtUnitDDL, "UnitName", "UnitRefNo");
                    //    ddlunit.Enabled = true;
                    //    divlblselectunit.Visible = true;
                    //    ddlunit.Visible = true;
                    //     ddlunit.Enabled = false;
                    //  }
                    //  else
                    //   {
                    //       ddlunit.Enabled = false;
                    //   }
                    //  }
                    //  else
                    //  {
                    //     ddldivision.Enabled = false;
                    //}
                }
            }
            else
            {
                DtCompanyDDL = Lo.RetriveMasterData(0, "", hfType.Value, 0, "", "", "Select");
                if (DtCompanyDDL.Rows.Count > 0)
                {
                    Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                    ddlcompany.Items.Insert(0, "Select");
                    ddlcompany.Enabled = true;
                    //   divlblselectdivison.Visible = false;
                    // divlblselectunit.Visible = false;
                }
                else
                {
                    ddlcompany.Enabled = false;
                }
            }
        }
        else if (hfType.Value == "Company")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, hfCompRefNo.Value, "Company", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = false;
                //  ddldivision.Items.Insert(0, "Select");
                //   divlblselectdivison.Visible = false;
                //   divlblselectunit.Visible = false;
            }
            else
            {
                ddlcompany.Enabled = false;
            }
            //DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
            //if (DtCompanyDDL.Rows.Count > 0)
            //{
            //    Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
            //    ddldivision.Items.Insert(0, "Select");
            //    if (hfType.Value == "Company")
            //    {
            //        divlblselectdivison.Visible = true;
            //        ddldivision.Enabled = true;
            //        divlblselectunit.Visible = false;
            //    }
            //    else
            //    {
            //        ddldivision.Enabled = false;
            //    }
            //}
            //else
            //{
            //    ddldivision.Enabled = false;
            //}
        }
        else if (hfType.Value == "Factory" || hfType.Value == "Division")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, hfCompRefNo.Value, "Company1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = false;
            }
            else
            {
                ddlcompany.Enabled = false;
            }
            //    DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
            //    if (DtCompanyDDL.Rows.Count > 0)
            //    {
            //        Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
            //        // code by gk to select indivisual division for the particular unit
            //        DataTable dt = Lo.RetriveMasterData(0, hfCompRefNo.Value, "Factory2", 0, "", "", "CompanyName");
            //        if (dt.Rows.Count > 0)
            //        {
            //            ddldivision.SelectedValue = dt.Rows[0]["FactoryRefNo"].ToString();
            //        }
            //        //end code
            //        ddlunit.Items.Insert(0, "Select");
            //        divlblselectunit.Visible = false;
            //        divlblselectdivison.Visible = true;
            //        ddldivision.Enabled = false;
            //    }
            //    else
            //    {
            //        ddldivision.Enabled = false;
            //    }
            //    DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "Unit1", 0, "", "", "CompanyName");
            //    if (DtCompanyDDL.Rows.Count > 0)
            //    {
            //        Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
            //        ddlunit.Items.Insert(0, "Select");
            //        ddlunit.Enabled = true;
            //        ddlunit.Visible = true;
            //        divlblselectunit.Visible = true;
            //    }
            //    else
            //    {
            //        ddlunit.Items.Insert(0, "Select");
            //        divlblselectunit.Visible = false;
            //    }
        }
        else if (hfType.Value == "Unit")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, hfCompRefNo.Value, "Company2", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = false;
            }
            else
            {
                ddlcompany.Enabled = false;
            }
            //    DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
            //    if (DtCompanyDDL.Rows.Count > 0)
            //    {
            //        Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
            //        // code by gk to select indivisual division for the particular unit
            //        DataTable dt = Lo.RetriveMasterData(0, hfCompRefNo.Value, "Factory3", 0, "", "", "CompanyName");
            //        if (dt.Rows.Count > 0)
            //        {
            //            ddldivision.SelectedValue = dt.Rows[0]["FactoryRefNo"].ToString();
            //        }
            //        //end code
            //        divlblselectdivison.Visible = true;
            //        ddldivision.Enabled = false;
            //    }
            //    else
            //    {
            //        ddldivision.Enabled = false;
            //    }
            //    DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "Unit1", 0, "", "", "CompanyName");
            //    if (DtCompanyDDL.Rows.Count > 0)
            //    {
            //        Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
            //        // code by gk to select indivisual unit for the particular unit             
            //        ddlunit.SelectedValue = hfCompRefNo.Value;
            //        //end code
            //        ddlunit.Enabled = false;
            //        divlblselectunit.Visible = true;
            //    }
            //    else
            //    {
            //        ddlunit.Enabled = false;
            //    }
        }
    }
    protected void ddlcompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlcompany.SelectedItem.Text != "Select")
        //{
        //    DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "", 0, "", "", "FactoryCompanyID");
        //    if (DtCompanyDDL.Rows.Count > 0)
        //    {
        //        Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
        //        ddldivision.Items.Insert(0, "Select");
        //        divlblselectdivison.Visible = true;
        //        ddldivision.Visible = true;
        //        hfCompRefNo.Value = ddlcompany.SelectedItem.Value;
        //        hfType.Value = "Company";
        //    }
        //    else
        //    {
        //        ddldivision.Items.Insert(0, "Select");
        //        ddldivision.Visible = false;
        //        divlblselectdivison.Visible = false;
        //    }
        //}
        //else if (ddlcompany.SelectedItem.Text == "Select")
        //{
        //    divlblselectdivison.Visible = false;
        //    divlblselectunit.Visible = false;
        //}
        //  hfCompRefNo.Value = "";
        //  hfCompRefNo.Value = ddlcompany.SelectedItem.Value;
    }
    //protected void ddldivision_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddldivision.SelectedItem.Text != "Select")
    //    {
    //        DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "", 0, "", "", "UnitSelectID");
    //        if (DtCompanyDDL.Rows.Count > 0)
    //        {
    //            Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
    //            ddlunit.Items.Insert(0, "Select");
    //            ddlunit.Visible = true;
    //            divlblselectunit.Visible = true;
    //            if (ddlunit.SelectedItem.Text == "Select")
    //            {
    //                ddldivision.Enabled = true;
    //            }
    //            else
    //            { ddldivision.Enabled = false; }
    //            hfCompRefNo.Value = ddldivision.SelectedItem.Value;
    //            hfType.Value = "Divison";
    //        }
    //        else
    //        {
    //            ddlunit.Items.Insert(0, "Select");
    //            divlblselectunit.Visible = false;
    //            ddlunit.Visible = false;
    //        }
    //        hfCompRefNo.Value = "";
    //        hfCompRefNo.Value = ddldivision.SelectedItem.Value;          
    //    }
    //    else if (ddldivision.SelectedItem.Text == "Select")
    //    {
    //        divlblselectunit.Visible = false;
    //        hfCompRefNo.Value = ddlcompany.SelectedItem.Value;
    //        hfType.Value = "Company";

    //    }
    //}
    //protected void ddlunit_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlunit.SelectedItem.Text != "Select")
    //    {
    //        hfCompRefNo.Value = ddlunit.SelectedItem.Value;
    //        hfType.Value = "Unit";
    //        hfType.Value = "";
    //        hfType.Value = ddlunit.SelectedItem.Value;
    //    }
    //    else
    //    {
    //        hfCompRefNo.Value = ddldivision.SelectedItem.Value;
    //        hfType.Value = "Division";
    //        hfCompRefNo.Value = "";
    //        hfCompRefNo.Value = ddldivision.SelectedItem.Value;
    //    }
    //}
    #endregion
}