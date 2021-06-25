using BusinessLayer;
using Encryption;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using context = System.Web.HttpContext;
using System.Activities.Statements;
using System.Globalization;

public partial class User_ProgressReport2 : System.Web.UI.Page
{
    #region Pagevariable

    Logic Lo = new Logic();
    HybridDictionary hysaveip = new HybridDictionary();
    private DataTable DtGrid = new DataTable();
    private string _msg = string.Empty;
    private string _sysMsg = string.Empty;
    DataUtility Co = new DataUtility();
    private Cryptography Encrypt = new Cryptography();
    Cryptography objEnc = new Cryptography();
    private PagedDataSource pgsource = new PagedDataSource();
    DataTable DtCompany = new DataTable();
    DataTable DtFilterView = new DataTable();
    HybridDictionary hyLogin = new HybridDictionary();
    DataTable dtCart = new DataTable();
    string Defaultpage = string.Empty;
    DataRow dr;
    #endregion
    #region Pageload
    protected void Page_Load(object sender, EventArgs e)

    {

        if (Session["User"] != null)
        {
            if (!this.IsPostBack)
            {
                try
                {
                    //string type = Lo.VerifyEmployee(hyLogin, out _msg, out Defaultpage);

                    ControlGrid();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Technical Error:- " + ex.Message + "');", true);
                }
            }
        }
    }
    //protected void BindProductCounts()
    //{
    //    DataTable dtcount = new DataTable();
    //    dtcount = Lo.RetriveFilterCode("", "", "GetProgressCount");
    //    lbltotother.Text = "Target for Indegenisation (2021):" + " " + dtcount.Rows[0]["TargetIndig"].ToString();
    //    lblNoOfProducts.Text = "No. Of interest Recieved:" + " " + dtcount.Rows[0]["InterestReceive"].ToString();
    //    lbltotalintrestshowprod.Text = "Action pending on interest shown product:" + " " + dtcount.Rows[0]["PendIntShown"].ToString();
    //    lbleoissue.Text = "Action pending for EOI Issue:" + " " + dtcount.Rows[0]["PendEOIissue"].ToString();
    //    lblsuppyissue.Text = "Action pending for supply order issue:" + " " + dtcount.Rows[0]["PendSuppOrder"].ToString();
    //    lblindig.Text = "Action pending for Indegenisation:" + " " + dtcount.Rows[0]["PendIndig"].ToString();
    //    tabcolrchng.Style["Background-Color"] = "rgb(0, 102, 153)";
    //    divInfo.Visible = true;
    //    th1.Visible = true;
    //    th2.Visible = false;
    //    th3.Visible = true;
    //    th4.Visible = true;
    //    th5.Visible = true;
    //}
    private void ControlGrid()
    {
        try
        {
            BindTopBoxRecords();
            BindProduct();

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BindNilCount()
    {
        DataTable dtnil = new DataTable();
        if (Session["CompanyRefNo"].ToString() != "" && txtsearch.Text != "")
        {
            dtnil = Lo.NewRetriveFilterCode("GetNilcount", Encrypt.DecryptData(Session["Type"].ToString()), Session["CompanyRefNo"].ToString(), txtsearch.Text, "", 0, 0, 0);
            if (dtnil.Rows.Count > 0)
            {
                lnknil.Text = dtnil.Rows[0]["nilaction"].ToString();
            }
            else { }
        }
        else
        {
            dtnil = Lo.NewRetriveFilterCode("GetNilcount", Encrypt.DecryptData(Session["Type"].ToString()), Session["CompanyRefNo"].ToString(), "", "", 0, 0, 0);
            lnknil.Text = dtnil.Rows[0]["nilaction"].ToString();
        }
    }
    protected void BindInterestCounts()
    {
        DataTable dtint = new DataTable();
        if (Session["CompanyRefNo"].ToString() != "" && txtsearch.Text != "")
        {
            dtint = Lo.NewRetriveFilterCode("GetInterestCount", Encrypt.DecryptData(Session["Type"].ToString()), Session["CompanyRefNo"].ToString(), txtsearch.Text, "", 0, 0, 0);
            if (dtint.Rows.Count > 0)
            {
                lnkunbconvend.Text = dtint.Rows[0][0].ToString();
                lnkresawait.Text = dtint.Rows[0][1].ToString();
                lnkrespeval.Text = dtint.Rows[0][2].ToString();
                lnkvendnotfit.Text = dtint.Rows[0][3].ToString();
                lnkvendfit.Text = dtint.Rows[0][4].ToString();
                lnkeoidone.Text = dtint.Rows[0][5].ToString();
                lnkitemnotreq.Text = dtint.Rows[0][6].ToString();
                lnkindUproc.Text = dtint.Rows[0][7].ToString();

            }
        }
        else
        {
            dtint = Lo.NewRetriveFilterCode("GetInterestCount", Encrypt.DecryptData(Session["Type"].ToString()), Session["CompanyRefNo"].ToString(), "", "", 0, 0, 0);
            if (dtint.Rows.Count > 0)
            {
                lnkunbconvend.Text = dtint.Rows[0][0].ToString();
                lnkresawait.Text = dtint.Rows[0][1].ToString();
                lnkrespeval.Text = dtint.Rows[0][2].ToString();
                lnkvendnotfit.Text = dtint.Rows[0][3].ToString();
                lnkvendfit.Text = dtint.Rows[0][4].ToString();
                lnkeoidone.Text = dtint.Rows[0][5].ToString();
                lnkitemnotreq.Text = dtint.Rows[0][6].ToString();
                lnkindUproc.Text = dtint.Rows[0][7].ToString();

            }
        }
        if (lnkitemnotreq.Text == String.Empty)
        {
            lnkitemnotreq.Text = "0";
        }
        if (lnkunbconvend.Text == String.Empty)
        {
            lnkunbconvend.Text = "0";
        }
        if (lnkresawait.Text == String.Empty)
        {
            lnkresawait.Text = "0";
        }
        if (lnkrespeval.Text == String.Empty)
        {
            lnkrespeval.Text = "0";
        }
        if (lnkvendnotfit.Text == String.Empty)
        {
            lnkvendnotfit.Text = "0";
        }
        if (lnkvendfit.Text == String.Empty)
        {
            lnkvendfit.Text = "0";
        }
        if (lnkitemnotreq.Text == String.Empty)
        {
            lnkitemnotreq.Text = "0";
        }
        if (lnkindUproc.Text == String.Empty)
        {
            lnkindUproc.Text = "0";
        }
        if (lnkeoidone.Text == String.Empty)
        {
            lnkeoidone.Text = "0";
        }
    }
    protected void BindProduct()
    {
        DtGrid = Lo.RetriveFilterCode(Session["CompanyRefNo"].ToString(), Encrypt.DecryptData(Session["Type"].ToString()), "GetProgressData");
        if (DtGrid.Rows.Count > 0)
        {
            Session["PDatatTable"] = DtGrid;
            SeachResult();
        }
        else
        {
            divcontentproduct.Visible = false;
            gvProgress.DataBind();
        }
    }

    //protected void BindFinancialYear()
    //{
    //    DataTable MasterFinancialYear = Lo.RetriveMasterSubCategoryDate(0, "", "", "AllFinancialYear", "", "");
    //    if (MasterFinancialYear.Rows.Count > 0)
    //    {
    //        Co.FillDropdownlist(ddlyearofindiginization, MasterFinancialYear, "FY", "FYID");
    //        ddlyearofindiginization.Items.Insert(0, "Select");
    //    }
    //}
    #endregion
    #region Filter Dropdown Code
    protected void txtsearch_TextChanged(object sender, EventArgs e)
    {

        if (txtsearch.Text == string.Empty)
        {
            searchResultLabel.Visible = false;
        }

    }
    #endregion
    #region Search Code Filter Code
    string insert1 = "";
    string chkproofcat = "";
    protected string Dvinsert(string sortExpression = null)
    {
        DataTable insert = new DataTable();

        string prodint = "";
        insert.Columns.Add(new DataColumn("Column", typeof(string)));
        insert.Columns.Add(new DataColumn("Value", typeof(string)));
        DataRow dr;
        if (CheckIntrestClick.Value == "Yes")
        {
            dr = insert.NewRow();
            dr["Column"] = "VendorName <>";
            dr["Value"] = "''";
            insert.Rows.Add(dr);
        }
        else if (CheckEOIClick.Value == "Yes")
        {
            dr = insert.NewRow();
            dr["Column"] = "EOIStatus =";
            dr["Value"] = "'Yes' and Total <> 0";
            insert.Rows.Add(dr);
        }
        else if (CheckInHouse.Value == "Yes")
        {
            dr = insert.NewRow();
            dr["Column"] = "(PurposeofProcurement like";
            dr["Value"] = "'%58270%')";
            insert.Rows.Add(dr);
        }
        else if (CheckTarget.Value == "Yes")
        {
            dr = insert.NewRow();
            dr["Column"] = "(IndTargetYear =";
            dr["Value"] = "'2020-21')";
            insert.Rows.Add(dr);
        }
        else if (CheckNoOfint.Value == "Yes")
        {
            DataTable dtcount1 = new DataTable();
            dtcount1 = Lo.RetriveFilterCode("", "", "FILTERCODENOOFINT");
            if (dtcount1.Rows.Count > 0)
            {
                for (int i = 0; dtcount1.Rows.Count > i; i++)
                {
                    prodint = prodint + "'" + dtcount1.Rows[i]["ProductRefno"].ToString() + "',";
                }
            }
            dr = insert.NewRow();
            dr["Column"] = "(IndTargetYear =";
            dr["Value"] = "'2020-21') and productrefno in (" + prodint.Substring(0, prodint.Length - 1) + ")";
            insert.Rows.Add(dr);

        }
        else if (CheckpendInt.Value == "Yes")
        {
            DataTable dtcount2 = new DataTable();
            dtcount2 = Lo.RetriveFilterCode("", "", "FILTERCODEPENDSHOWNINT");
            if (dtcount2.Rows.Count > 0)
            {
                for (int i = 0; dtcount2.Rows.Count > i; i++)
                {
                    prodint = prodint + "'" + dtcount2.Rows[i]["ProductRefno"].ToString() + "',";
                }
            }
            dr = insert.NewRow();
            dr["Column"] = "(IndTargetYear =";
            dr["Value"] = "'2020-21') and (IntShownStatus = '' or IntShownStatus is null) and productrefno in (" + prodint.Substring(0, prodint.Length - 1) + ")";
            insert.Rows.Add(dr);

        }
        else if (Checkpendeoi.Value == "Yes")
        {
            DataTable dtcount3 = new DataTable();
            dtcount3 = Lo.RetriveFilterCode("", "", "FILTERCODEPENDEOI");
            if (dtcount3.Rows.Count > 0)
            {
                for (int i = 0; dtcount3.Rows.Count > i; i++)
                {
                    prodint = prodint + "'" + dtcount3.Rows[i]["ProductRefno"].ToString() + "',";
                }
            }
            dr = insert.NewRow();
            dr["Column"] = "(IndTargetYear =";
            dr["Value"] = "'2020-21') and (IntShownStatus = '' or IntShownStatus is null) and (EOIStatus = 'No' or EOIStatus is null) and productrefno in (" + prodint.Substring(0, prodint.Length - 1) + ")";
            insert.Rows.Add(dr);

        }
        else if (CheckPendSupp.Value == "Yes")
        {
            DataTable dtcount4 = new DataTable();
            dtcount4 = Lo.RetriveFilterCode("", "", "FILTERCODEPENDSUPP");
            if (dtcount4.Rows.Count > 0)
            {
                for (int i = 0; dtcount4.Rows.Count > i; i++)
                {
                    prodint = prodint + "'" + dtcount4.Rows[i]["ProductRefno"].ToString() + "',";
                }
            }
            dr = insert.NewRow();
            dr["Column"] = "(IndTargetYear =";
            dr["Value"] = "'2020-21') and (IntShownStatus = '' or IntShownStatus is null) and (EOIStatus = 'No' or EOIStatus is null) and (SupplyOrderStatus = 'No' or SupplyOrderStatus is null) and productrefno in (" + prodint.Substring(0, prodint.Length - 1) + ")";
            insert.Rows.Add(dr);
        }
        else if (CheckPenIndig.Value == "Yes")
        {
            DataTable dtcount5 = new DataTable();
            dtcount5 = Lo.RetriveFilterCode("", "", "FILTERCODEPENDINDIG");
            if (dtcount5.Rows.Count > 0)
            {
                for (int i = 0; dtcount5.Rows.Count > i; i++)
                {
                    prodint = prodint + "'" + dtcount5.Rows[i]["ProductRefno"].ToString() + "',";
                }
            }
            dr = insert.NewRow();
            dr["Column"] = "(IndTargetYear =";
            dr["Value"] = "'2020-21') and (IntShownStatus = '' or IntShownStatus is null) and (EOIStatus = 'No' or EOIStatus is null) and (SupplyOrderStatus = 'No' or SupplyOrderStatus is null) and (IsIndeginized = 'N') and productrefno in (" + prodint.Substring(0, prodint.Length - 1) + ")";
            insert.Rows.Add(dr);
        }
        if (txtsearch.Text.Trim() != "")
        {
            dr = insert.NewRow();
            dr["Column"] = "((ProductRefNo like";
            dr["Value"] = "'" + txtsearch.Text.Trim() + "%') or (CompanyName like '" + txtsearch.Text.Trim() + "%') or (UnitName like '" + txtsearch.Text.Trim() + "%') or (FactoryName like '" + txtsearch.Text.Trim() + "%') or (NSCCode like '" + txtsearch.Text.Trim() + "%') or (ProductDescription like '" + txtsearch.Text.Trim() + "%') or (EOIStatus like '" + txtsearch.Text.Trim() + "%'))";
            insert.Rows.Add(dr);
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
        //lbltotother.Text=
    }
    protected string BindInsertfilter()
    {
        return Dvinsert();
    }
    public void SeachResult(string sortExpression = null)
    {
        try
        {
            DtFilterView = (DataTable)Session["PDatatTable"];
            if (DtFilterView.Rows.Count > 0)
            {
                DataView dv = new DataView(DtFilterView);
                DataTable dtnew = dv.ToTable();
                if (dtnew.Rows.Count > 0)
                {
                    dv.RowFilter = BindInsertfilter();
                    DataTable dtinner = dv.ToTable();
                    if (CheckIntrestClick.Value == "Yes")
                    {
                        DataTable chkcountofven = dv.ToTable(true, "VendorName");
                        lbltotalintrestshowprod.Text = "By" + " " + chkcountofven.Rows.Count + " " + "Vendors";
                        //lbltotother.Text = "Total Product : " + lbltotalshowpageitem.Text;
                    }

                    lbltotfilter.Text = dtinner.Rows.Count.ToString();
                    lbltotother.Text = "Total Product : " + lbltotfilter.Text;
                    // lbltotother.Text = "Total Product : " + dtinner.Rows.Count.ToString();
                    DataTable dtads = dv.ToTable();
                    if (dtads.Rows.Count > 0)
                    {
                        if (dtads.Columns.Contains("row_no"))
                        {
                            int i = 1;
                            foreach (DataRow r in dtads.Rows)
                                r["row_no"] = i++;
                        }
                        else
                        {
                            dtads.Columns.Add("row_no");
                            int i = 1;
                            foreach (DataRow r in dtads.Rows)
                                r["row_no"] = i++;
                        }
                        Session["ExcelDT"] = dtads;
                        pgsource.DataSource = dtinner.DefaultView;
                        pgsource.AllowPaging = true;
                        pgsource.PageSize = 25;
                        pgsource.CurrentPageIndex = pagingCurrentPage;
                        lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                        lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                        LinkButton1.Enabled = !pgsource.IsFirstPage;
                        lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                        LinkButton2.Enabled = !pgsource.IsLastPage;
                        pgsource.DataSource = dtads.DefaultView;
                        if (txtsearch.Text != "" && txtgosearch.Text != "")
                        {
                            if (txtsearch.Text.ToUpper().Contains("PRO"))
                            {
                                gvProgress.DataSource = dtads;
                            }
                            else
                            { gvProgress.DataSource = pgsource; }
                        }
                        else
                        { gvProgress.DataSource = pgsource; }
                        gvProgress.DataBind();
                        lbltotalshowpageitem.Text = pgsource.FirstIndexInPage + 1 + " - " + (pgsource.FirstIndexInPage + pgsource.Count);
                        //Label3.Text = "Total Product : " + lbltotalshowpageitem.Text;
                        Label3.Text = dtinner.Rows.Count.ToString();
                        divcontentproduct.Visible = true;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
                        divcontentproduct.Visible = false;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
                }
                if (CheckEOIClick.Value == "Yes")
                {
                    lbltotother.Text = "Total Product : " + lblProducts.Text;
                    lblNoOfProducts.Text = "No. of interested shown : " + lblInterest.Text;
                    lbltotalintrestshowprod.Text = "EOI Issued : interest : product " + lblEOIRFP.Text;
                    tabcolrchng.Style["Background-Color"] = "#00CC66";
                    th2.Visible = true;
                    divInfo.Visible = true;
                    th1.Visible = true;
                    th3.Visible = false;
                    th4.Visible = false;
                    th5.Visible = false;
                }
                if (CheckIntrestClick.Value == "Yes")
                {

                    string query = txtsearch.Text;
                    //lblNoOfProducts.Text = "No. of interest shown : " + lbltotfilter.Text;
                    lblNoOfProducts.Text = "No. of interest shown : " + lblInterest.Text;
                    if (!String.IsNullOrEmpty(query))
                    {
                        searchResultLabel.Text = "Search Result for" + " " + query;
                        th15.Visible = true;
                    }
                    else
                    {
                        th15.Visible = false;
                    }
                    if (txtsearch.Text == "")
                    {
                        lbltotother.Text = "Total Product : " + lblProducts.Text;
                    }
                    if ((txtsearch.Text == "BEL") || (txtsearch.Text == "OFB") || (txtsearch.Text == "BDL") || (txtsearch.Text == "BEML") || (txtsearch.Text == "GRSE") ||
                        (txtsearch.Text == "GSL") || (txtsearch.Text == "HAL") || (txtsearch.Text == "HSL") || (txtsearch.Text == "MDL") || (txtsearch.Text == "MIDHANI") ||
                        (txtsearch.Text == "OFB") || (txtsearch.Text == "SHQ (AIR FORCE)") || (txtsearch.Text == "SHQ (ARMY)") || (txtsearch.Text == "SHQ (NAVY)"))
                    {
                        DataTable dt = Lo.RetriveFilterCode("", txtsearch.Text, "GetInterestTotal");
                        // lbltotother.Text = "Total Product : " + lbltotfilter.Text;
                        lbltotother.Text = "Total Product : " + dt.Rows[0]["TotalProduct"].ToString();
                        lblNoOfProducts.Text = "No. of interest shown : " + dt.Rows[0]["Interest"].ToString();
                    }
                    tabcolrchng.Style["Background-Color"] = "#999966";
                    divInfo.Visible = true;
                    th1.Visible = true;
                    th2.Visible = false;
                    th3.Visible = false;
                    th4.Visible = false;
                    th5.Visible = false;
                }
            }
            else
            {
                gvProgress.Visible = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void BindTopBoxRecords()
    {
        DataTable dtprdetails = Lo.RetrieveProductDetails("Count", Encrypt.DecryptData(Session["Type"].ToString()), Session["CompanyRefNo"].ToString());
        lblProducts.Text = dtprdetails.Rows[0]["Product"].ToString();
        lblInterest.Text = dtprdetails.Rows[0]["TotalIntrestDis"].ToString();
        decimal num = (Convert.ToDecimal(lblInterest.Text) / Convert.ToDecimal(lblProducts.Text) * 100);
        lblIntrProdPer.Text = num.ToString("F2");
        lblEOIRFP.Text = dtprdetails.Rows[0]["TotalEOIDis"].ToString();
        lblother1.Text = "EOI issued : Total Product " + dtprdetails.Rows[0]["EOIRFP"].ToString();
        decimal num1 = (Convert.ToDecimal(lblEOIRFP.Text) / Convert.ToDecimal(lblProducts.Text) * 100);
        lblEoiPerc.Text = num1.ToString("F2");
        lblsupply.Text = dtprdetails.Rows[0]["SupplyOrder"].ToString();
        decimal num2 = (Convert.ToDecimal(lblsupply.Text) / Convert.ToDecimal(lblProducts.Text) * 100);
        lbSupplyPer.Text = num2.ToString("F2");
        lblindiginized.Text = dtprdetails.Rows[0]["IndiginizedIOS"].ToString();
        decimal num3 = (Convert.ToDecimal(lblindiginized.Text) / Convert.ToDecimal(lblProducts.Text) * 100);
        lblIndiPerc.Text = num3.ToString("F2");
        //lblInhouse.Text = dtprdetails.Rows[0]["IndiginizedInhouse"].ToString();
        //lblIoS.Text = dtprdetails.Rows[0]["IndiginizedIOS"].ToString();
        //lblTotalIndi.Text = dtprdetails.Rows[0]["TotalIndiginized"].ToString();
        lblNoOfProducts.Text = "Total products : " + lblProducts.Text;
        //      lbltotalintrestshowprod.Text = "Interest's Shown by Vendor(s) : " + lblInterest.Text;
    }
    #endregion
    #region pageindex code
    protected void lnkbtnPgPrevious_Click(object sender, EventArgs e)
    {
        pagingCurrentPage -= 1;
        SeachResult();
    }
    protected void lnkbtnPgNext_Click(object sender, EventArgs e)
    {
        pagingCurrentPage += 1;
        SeachResult();
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
    #region CartCode
    protected void gvProgress_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region
        if (e.CommandName == "Product")
        {
            try
            {
                GridViewRow item = (GridViewRow)(((Control)(e.CommandSource)).NamingContainer);
                DataTable DtView = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductMasterID", "Company");
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
                            lblindicate.Text = DTporCat.Rows[0]["SCategoryName"].ToString();

                            sixteen.Visible = true;
                        }
                        else
                        {
                            sixteen.Visible = false;
                        }
                    }
                    else
                    {
                        sixteen.Visible = false;
                    }
                    if (DtView.Rows[0]["IsIndeginized"].ToString() != "")
                    {
                        Tr1.Visible = true;
                        lblindstart.Text = DtView.Rows[0]["IsIndeginized"].ToString();
                        if (lblindstart.Text == "N")
                            lblindstart.Text = "No";
                        else
                            lblindstart.Text = "Yes";
                    }
                    else
                    { Tr1.Visible = false; }


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
                    {
                        eighteen.Visible = false;
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
                            lblphonenumber.Text = dtNodal.Rows[0]["NodalOfficerTelephone"].ToString();
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
                    if (DtView.Rows[0]["IndTargetYear"].ToString() != "")
                    {
                        lblindtrgyr.Text = DtView.Rows[0]["IndTargetYear"].ToString();
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
                    DataTable DtBindIntrestUser = Lo.RetriveFilterCode(e.CommandArgument.ToString(), "", "SHomedetails");
                    ScriptManager.RegisterStartupScript(this, GetType(), "ProductCompany", "showPopup();", true);


                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
            }

        }
        else if (e.CommandName == "EOI")
        {
            lblrefnoview.Text = e.CommandArgument.ToString();
            DataTable dt = Lo.RetriveFilterCode(e.CommandArgument.ToString(), "", "SHomedetailsforpopup");
            GridViewRow item = (GridViewRow)(((Control)(e.CommandSource)).NamingContainer);
            HiddenField hfeoi = (HiddenField)item.FindControl("hfeoiurl");
            HyperLink hflink = (HyperLink)item.FindControl("mhylink");
            LinkButton leoi = (LinkButton)item.FindControl("EOI");
            if (hfeoi.Value != "")
            {
                if (dt.Rows.Count.ToString() != "")
                {
                    rbeoimake2.SelectedValue = Co.RSQandSQLInjection(leoi.Text.Trim(), "soft");
                    mhylink.Text = Co.RSQandSQLInjection(Convert.ToString(dt.Rows[0]["EOIURL"].ToString()).Trim(), "soft");

                    if (!string.IsNullOrEmpty(dt.Rows[0]["EOIStartDate"].ToString()))
                    {
                        lblstartdate.Text = (Convert.ToDateTime(dt.Rows[0]["EOIStartDate"]).ToString("dd/MM/yyyy"));

                        //DateTime date = DateTime.ParseExact(lblstartdate.Text, "d/M/yyyy", CultureInfo.InvariantCulture);
                        //// for both "1/1/2000" or "25/1/2000" formats
                        //string newString = date.ToString("MM/dd/yyyy");
                    }

                    else
                    {
                        lblstartdate.Text = "";
                    }
                    //lblstartdate.Text = Convert.ToDateTime(dtable.Rows[0]["EOIStartDate"]).ToString("yyyy/MM/dd");

                    if (!string.IsNullOrEmpty(dt.Rows[0]["EOIEndDate"].ToString()))
                    {
                        lblenddate.Text = Co.RSQandSQLInjection(Convert.ToDateTime(dt.Rows[0]["EOIEndDate"]).ToString("dd/MM/yyyy").Trim(), "soft");
                    }

                    else
                    {
                        lblenddate.Text = "";
                    }


                }
                else
                {
                    mhylink.Text = "NO Url Found";
                    lblstartdate.Text = "";
                    lblenddate.Text = "";
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "divstatus", "showPopup1();", true);
            }
            else
            {
                mhylink.Text = "NO Url Found";
                lblstartdate.Text = "";
                lblenddate.Text = "";
                ScriptManager.RegisterStartupScript(this, GetType(), "divstatus", "showPopup1();", true);
            }
        }
        else if (e.CommandName == "SupplyOrd")
        {
            lblrefnoview.Text = e.CommandArgument.ToString();
            DataTable dtable = Lo.RetriveFilterCode(e.CommandArgument.ToString(), "", "SHomedetailsforpopup");
            GridViewRow item = (GridViewRow)(((Control)(e.CommandSource)).NamingContainer);
            LinkButton sostatus = (LinkButton)item.FindControl("mSupplyorder");

            //rdblsoplaced.SelectedValue = sostatus.Text;

            if (dtable.Rows[0]["SupplyOrderStatus"].ToString() != "")
            {
                rdblsoplaced.SelectedValue = sostatus.Text;
                txtsupplyname.Text = Co.RSQandSQLInjection(dtable.Rows[0]["SupplyManfutureName"].ToString().Trim(), "soft");
                txtsupplyaddress.Text = Co.RSQandSQLInjection(dtable.Rows[0]["SupplyManfutureAddress"].ToString().Trim(), "soft");
                txtsupplyvalue.Text = Co.RSQandSQLInjection(dtable.Rows[0]["SupplyOrderValue"].ToString().Trim(), "soft");
                if (!string.IsNullOrEmpty(dtable.Rows[0]["SupplyOrderDate"].ToString()))
                {
                    txtsupplyorderdate.Text = Co.RSQandSQLInjection(Convert.ToDateTime(dtable.Rows[0]["SupplyOrderDate"]).ToString("dd/MM/yyyy").Trim(), "soft");
                }

                else
                {
                    txtsupplyorderdate.Text = "";
                }
                if (!string.IsNullOrEmpty(dtable.Rows[0]["SupplyDeliveryDate"].ToString()))
                {
                    txtsupplydelivrydate.Text = Co.RSQandSQLInjection(Convert.ToDateTime(dtable.Rows[0]["SupplyDeliveryDate"]).ToString("dd/MM/yyyy").Trim(), "soft");
                }
                else
                {
                    txtsupplydelivrydate.Text = "";
                }
                if (sostatus.Text != "")
                {
                    rdblsoplaced.SelectedValue = Co.RSQandSQLInjection(sostatus.Text.Trim(), "soft");
                }
                // ScriptManager.RegisterStartupScript(this, GetType(), "Supplyorder", "showPopup3();", true);
            }
            else
            {
                txtsuppman.Text = "";
                txtsuppadrr.Text = "";
                txtsupplyvalue.Text = "";
                txtsupplyname.Text = "";
                txtsupplyaddress.Text = "";

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "Supplyorder", "showPopup3();", true);
        }
        else if (e.CommandName == "Success_story")
        {

            //BindFinancialYear();
            lblrefnoview.Text = e.CommandArgument.ToString();
            DataTable dtable = Lo.RetriveFilterCode(e.CommandArgument.ToString(), "", "SHomedetailsforpopup");
            GridViewRow item = (GridViewRow)(((Control)(e.CommandSource)).NamingContainer);
            hfisindegized.Value = Co.RSQandSQLInjection(dtable.Rows[0]["IsIndeginized"].ToString().Trim(), "soft");
            txtsuppman.Text = Co.RSQandSQLInjection(dtable.Rows[0]["SupplyManfutureName"].ToString().Trim(), "soft");
            txtsuppadrr.Text = Co.RSQandSQLInjection(dtable.Rows[0]["SupplyManfutureAddress"].ToString().Trim(), "soft");
            //ddlyearofindiginization.SelectedItem.Value = Co.RSQandSQLInjection(dtable.Rows[0]["YearofIndiginization"].ToString().Trim(), "soft");
            txtyear.Text = Co.RSQandSQLInjection(dtable.Rows[0]["YearofIndiginization"].ToString().Trim(), "soft");

            ScriptManager.RegisterStartupScript(this, GetType(), "successtory", "showPopup5();", true);
        }
        else if (e.CommandName == "Status")
        {
            try
            {
                lblrefnoview.Text = e.CommandArgument.ToString();
                DataTable dtable = Lo.RetriveFilterCode(e.CommandArgument.ToString(), "", "SHomedetails");
                if (dtable.Rows.Count > 0)
                {
                    try
                    {
                        grdintshown.DataSource = dtable;
                        grdintshown.DataBind();
                    }
                    catch (Exception ex)
                    { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(" + ex.Message + ")", true); }
                    grdintshown.Columns[11].Visible = false;
                }
                else
                {
                    grdintshown.Visible = false;
                }
                if (dtable.Rows.Count > 0)
                {
                    if (dtable.Rows[0]["IntShownReason"].ToString() != "")
                    {
                        grdintshown.Columns[11].Visible = true;
                        ScriptManager.RegisterStartupScript(this, GetType(), "InterestShownStatus", "showPopup4();", true);
                    }
                }
            }
            catch (Exception ex)
            { }
        }
        else if (e.CommandName == "Status1")
        {
            lblrefnoview.Text = e.CommandArgument.ToString();
            DataTable dtable1 = Lo.RetriveFilterCode(e.CommandArgument.ToString(), "", "SHomedetails1");
            if (dtable1.Rows.Count > 0)
            {
                try
                {
                    GridView1.DataSource = dtable1;
                    GridView1.DataBind();
                    GridView1.Columns[11].Visible = false;
                }
                catch (Exception ex)
                { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(" + ex.Message + ")", true); }
            }
            else
            {
                GridView1.Visible = false;
            }
            if (dtable1.Rows.Count > 0)
            {
                if (dtable1.Rows[0]["IntShownReason"].ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "InterestShownStatus1", "showPopup8();", true);
                }
            }
        }
        else if (e.CommandName == "totalintstatus")
        {
            lblrefnoview.Text = e.CommandArgument.ToString();
            DataTable dtable = Lo.RetriveFilterCode(e.CommandArgument.ToString(), "", "showuniquerows");
            Session["dtable"] = dtable;
            if (dtable.Rows.Count > 0)
            {
                grdinttotal.DataSource = dtable;
                grdinttotal.DataBind();
                P8.InnerText = "Total record found :- " + dtable.Rows.Count.ToString();
            }
            else
            {
                grdinttotal.Visible = false;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "inttotalshown", "showPopup7();", true);
        }
        #endregion
    }
    #endregion
    #region oterscode
    protected void gvProgress_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbeoi = (LinkButton)e.Row.FindControl("EOI");
            LinkButton lbSupplyorder = (LinkButton)e.Row.FindControl("mSupplyorder");
            LinkButton lbsuccessstory = (LinkButton)e.Row.FindControl("successstory");
            LinkButton lblstatusyes = (LinkButton)e.Row.FindControl("lblintstatus");
            LinkButton lblstatusno = (LinkButton)e.Row.FindControl("lblintstatus1");
            Label lbdivision = (Label)e.Row.FindControl("lbldivsion");
            Label lbunit = (Label)e.Row.FindControl("lblunit");
            Label lbcomp = (Label)e.Row.FindControl("lblcompshow");
            Label lbdate = (Label)e.Row.FindControl("lbldate");
            if (Convert.ToString(lbdate.Text.Trim()) == "01-Jan-1900")
            {
                lbdate.Text = "NA";
            }
            LinkButton lblint1 = (LinkButton)e.Row.FindControl("lblinterest");
            if (lblstatusyes.Text == "0")
            {
                lblstatusyes.Enabled = false;
            }
            else
            {
                lblstatusyes.Enabled = true;
            }
            if (lblstatusno.Text == "0")
            {
                lblstatusno.Enabled = false;
            }
            else
            {
                lblstatusno.Enabled = true;
            }
            if (lbeoi.Text == "")
            {
                lbeoi.Text = "No";
            }
            if (lbSupplyorder.Text == "")
            {
                lbSupplyorder.Text = "No";
            }
            if (lbsuccessstory.Text == "")
            {
                lbsuccessstory.Text = "No";
            }
            if (lbunit.Text != "")
            {
                lbdivision.Visible = false;
                lbunit.Visible = true;
            }
            else
            {
                lbdivision.Visible = true;
                lbunit.Visible = false;
                if (lbdivision.Text == "")
                {
                    lbdivision.Text = lbcomp.Text;
                }
            }
            if (lbdate.Text == "01-Jan-1900")
            { lbdate.Text = ""; }
        }
    }
    protected void gvProgress_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        SeachResult();
        BindInterestCounts();
        BindNilCount();
    }
    #region AutoComplete Serach Box
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetSearchKeyword(string prefix)
    {
        Cryptography objCrypto1 = new Cryptography();
        List<string> customers = new List<string>();
        //  List<string> Finalcustomers = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = objCrypto1.DecryptData(ConfigurationManager.ConnectionStrings["connectiondb"].ConnectionString);
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct ProductRefNo from tbl_mst_ProgressReport where ProductRefNo like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefix);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["ProductRefNo"]));
                    }
                }
                cmd.CommandText = "select distinct CompanyName from tbl_mst_ProgressReport where CompanyName like @SearchText + '%'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["CompanyName"]));
                    }
                }
                cmd.CommandText = "select distinct FactoryName from tbl_mst_ProgressReport where FactoryName like @SearchText + '%'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["FactoryName"]));
                    }
                }

                cmd.CommandText = "select distinct UnitName from tbl_mst_ProgressReport where UnitName like @SearchText + '%'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["UnitName"]));
                    }
                }
                cmd.CommandText = "select distinct NSCCode from tbl_mst_ProgressReport where NSCCode like @SearchText + '%'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["NSCCode"]));
                    }
                }
                cmd.CommandText = "select distinct ProductDescription from tbl_mst_ProgressReport where ProductDescription like @SearchText + '%' and (ProductDescription is not null or ProductDescription !='')";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["ProductDescription"]));
                    }
                }
                cmd.CommandText = "select distinct VendorName from tbl_mst_ProgressReport where VendorName like @SearchText + '%' and (VendorName is not null or NSNGroup !='')";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["VendorName"]));
                    }
                }
                cmd.CommandText = "select distinct VendorDate from tbl_mst_ProgressReport where VendorDate like @SearchText + '%' and (VendorDate is not null or VendorDate !='')";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["VendorDate"]));
                    }
                }
                conn.Close();
            }
        }

        return customers.Distinct().ToArray();
    }
    #endregion
    #region AutoComplete Serach Box
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string GetSearchKeywordemo(string prefix)
    {
        User_ProgressReport2 u = new User_ProgressReport2();
        u.SeachResult(prefix);
        return "search";
    }
    #endregion
    #endregion
    #region SHALINI Code
    protected void linklogin_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("Login");
    }
    protected void linkusername_Click(object sender, EventArgs e)
    {
        if (Session["User"] != null)
        {
            Response.RedirectToRoute("Dashboard");
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "ErrorMssgPopup('Please login again');window.location='Login'", true);
        }
    }
    #endregion
    #region LogoutCode
    protected void lbllogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Remove("Type");
        Session.Remove("User");
        Session.Remove("CompanyRefNo");
        Session.Remove("SFToken");
        Session.RemoveAll();
        Session.Contents.RemoveAll();
        Session.Clear();
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cookies["DefaultDpsu"].Expires = DateTime.Now;
        Response.Buffer = true;
        Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
        Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
        Response.AppendHeader("Expires", "0"); // Proxies.
        if (Request.Cookies["User"] != null)
        {
            Response.Cookies["User"].Value = string.Empty;
            Response.Cookies["User"].Expires = DateTime.Now.AddMonths(-20);
        }
        if (Request.Cookies["SFToken"] != null)
        {
            Response.Cookies["SFToken"].Value = string.Empty;
            Response.Cookies["SFToken"].Expires = DateTime.Now.AddMonths(-20);
        }
        Response.RedirectToRoute("Productlist");
    }
    #endregion
    #region TryCatchLog
    public static class ExceptionLogging
    {
        private static String ErrorlineNo, Errormsg, extype, exurl, hostIp, ErrorLocation, HostAdd;
        public static void SendErrorToText(Exception ex)
        {
            var line = Environment.NewLine + Environment.NewLine;
            ErrorlineNo = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
            Errormsg = ex.GetType().Name.ToString();
            extype = ex.GetType().ToString();
            exurl = context.Current.Request.Url.ToString();
            ErrorLocation = ex.Message.ToString();
            try
            {
                string filepath = context.Current.Server.MapPath("/Logs/");  //Text File Path
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                filepath = filepath + DateTime.Today.ToString("dd-MM-yy") + ".txt";   //Text File Name
                if (!File.Exists(filepath))
                {
                    File.Create(filepath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    string error = "Log Written Date:" + " " + DateTime.Now.ToString() + line + "Error Line No :" + " " + ErrorlineNo + line + "Error Message:" + " " + Errormsg + line + "Exception Type:" + " " + extype + line + "Error Location :" + " " + ErrorLocation + line + " Error Page Url:" + " " + exurl + line + "User Host IP:" + " " + hostIp + line;
                    sw.WriteLine("-----------Exception Details on " + " " + DateTime.Now.ToString() + "-----------------");
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                    sw.WriteLine(line);
                    sw.WriteLine(error);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    sw.WriteLine(line);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception exm)
            {
                exm.Message.ToString();
            }
        }
    }
    #endregion
    protected void lblProducts_Click(object sender, EventArgs e)
    {
        CheckEOIClick.Value = "";
        CheckIntrestClick.Value = "";
        CheckInHouse.Value = "";
        CheckProductsClick.Value = "Yes";
        divbox.Visible = false;
        divInfo.Visible = false;
        txtsearch.Text = "";
        pagingCurrentPage = 0;
        SeachResult();
        Response.Redirect("PReport2");
    }
    int countprod;
    protected void lblInterest_Click(object sender, EventArgs e)
    {
        CheckIntrestClick.Value = "Yes";
        CheckEOIClick.Value = "";
        CheckInHouse.Value = "";
        CheckProductsClick.Value = "";
        lblNoOfProducts.Enabled = true;
        lbltotother.Text = "Total Product : " + Label3.Text;
        //lbltotother.Text = "Total Product : " + lblProducts.Text;
        txtsearch.Text = "";
        pagingCurrentPage = 0;
        SeachResult();
    }
    protected void lblEOIRFP_Click(object sender, EventArgs e)
    {
        CheckEOIClick.Value = "Yes";
        CheckIntrestClick.Value = "";
        CheckInHouse.Value = "";
        CheckProductsClick.Value = "";
        divbox.Visible = false;
        lblNoOfProducts.Enabled = false;
        searchResultLabel.Visible = false;
        txtsearch.Text = "";
        pagingCurrentPage = 0;
        SeachResult();
    }
    protected void bindhover()
    {
        DataTable dtHover = Lo.RetriveFilterCode("", "", "GetProgHover");
        if (dtHover.Rows.Count > 0)
        {
            gvhover.DataSource = dtHover;
            gvhover.DataBind();
            //0
            gvhover.FooterRow.Cells[0].Text = "Total";
            gvhover.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            object sumObjectn = dtHover.Compute("Sum(TotalProduct)", string.Empty);
            gvhover.FooterRow.Cells[1].Text = sumObjectn.ToString();
            //1
            object sumObjectn1 = dtHover.Compute("Sum(Interest)", string.Empty);
            decimal num = (Convert.ToDecimal(sumObjectn1.ToString()) / Convert.ToDecimal(lblProducts.Text) * 100);
            gvhover.FooterRow.Cells[2].Text = sumObjectn1 + " " + "(" + num.ToString("F2") + "%)";
            //2
            object sumObjectn2 = dtHover.Compute("Sum(EoiProductIntrest)", string.Empty);
            decimal num1 = (Convert.ToDecimal(sumObjectn2.ToString()) / Convert.ToDecimal(lblProducts.Text) * 100);
            gvhover.FooterRow.Cells[3].Text = sumObjectn2 + " " + "(" + num1.ToString("F2") + "%)";
            //3
            object sumObjectn3 = dtHover.Compute("Sum(eoistatus)", string.Empty);
            decimal num2 = (Convert.ToDecimal(sumObjectn3.ToString()) / Convert.ToDecimal(lblProducts.Text) * 100);
            gvhover.FooterRow.Cells[4].Text = sumObjectn3 + " " + "(" + num2.ToString("F2") + "%)";
            //4
            object sumObjectn4 = dtHover.Compute("Sum(supplyorder)", string.Empty);
            decimal num3 = (Convert.ToDecimal(sumObjectn4.ToString()) / Convert.ToDecimal(lblProducts.Text) * 100);
            gvhover.FooterRow.Cells[5].Text = sumObjectn4 + " " + "(" + num3.ToString("F2") + "%)";
            //5
            object sumObjectn5 = dtHover.Compute("Sum(Indiginized)", string.Empty);
            decimal num4 = (Convert.ToDecimal(sumObjectn5.ToString()) / Convert.ToDecimal(lblProducts.Text) * 100);
            gvhover.FooterRow.Cells[6].Text = sumObjectn5 + " " + "(" + num4.ToString("F2") + "%)";
            //6
            object sumObjectn6 = dtHover.Compute("Sum(IndigTarget)", string.Empty);
            decimal num5 = (Convert.ToDecimal(sumObjectn6.ToString()) / Convert.ToDecimal(lblProducts.Text) * 100);
            gvhover.FooterRow.Cells[7].Text = sumObjectn6 + " " + "(" + num5.ToString("F2") + "%)";
            //7
            object sumObjectn7 = dtHover.Compute("Sum(Category)", string.Empty);
            decimal num6 = (Convert.ToDecimal(sumObjectn7.ToString()) / Convert.ToDecimal(lblProducts.Text) * 100);
            gvhover.FooterRow.Cells[8].Text = sumObjectn7 + " " + "(" + num6.ToString("F2") + "%)";


            if (Encrypt.DecryptData(Session["User"].ToString()) == "shrishkumar.ofb@ofb.gov.in")
            {
                gvhover.Columns[7].Visible = true;
                gvhover.Columns[8].Visible = true;
            }
            else
            {
                gvhover.Columns[7].Visible = false;
                gvhover.Columns[8].Visible = false;
            }
        }
    }
    protected void lbproddetail_Click(object sender, EventArgs e)
    {
        bindhover();
        ScriptManager.RegisterStartupScript(this, GetType(), "ProductCompany1", "showPopup2();", true);
    }
    protected void lblInhouse_Click(object sender, EventArgs e)
    {
        CheckEOIClick.Value = "";
        CheckIntrestClick.Value = "";
        CheckInHouse.Value = "Yes";
        CheckProductsClick.Value = "";
        SeachResult();
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            //DataTable mIntrest = (DataTable)Session["PDatatTable"];
            //if (mIntrest.Rows.Count > 0)
            //{
            //    DataView dv = new DataView(mIntrest);
            //    DataTable dtnew = dv.ToTable();
            //    if (dtnew.Rows.Count > 0)
            //    {
            //        dv.RowFilter = BindInsertfilter();
            //        DataTable dtinner = dv.ToTable();
            //        if (CheckIntrestClick.Value == "Yes")
            //        {
            //            DataTable chkcountofven = dv.ToTable(true, "VendorName");
            //            lbltotalintrestshowprod.Text = "By" + " " + chkcountofven.Rows.Count + " " + "Vendors";
            //            //lbltotother.Text = "Total Product : " + lbltotalshowpageitem.Text;
            //        }
            //        lbltotfilter.Text = dtinner.Rows.Count.ToString();
            //        lbltotother.Text = "Total Product : " + lbltotfilter.Text;
            //        // lbltotother.Text = "Total Product : " + dtinner.Rows.Count.ToString();
            //        DataTable dtads = dv.ToTable();
            //        if (dtads.Rows.Count > 0)
            //        {
            //            if (dtads.Columns.Contains("row_no"))
            //            {
            //                int i = 1;
            //                foreach (DataRow r in dtads.Rows)
            //                    r["row_no"] = i++;
            //            }
            //            else
            //            {
            //                dtads.Columns.Add("row_no");
            //                int i = 1;
            //                foreach (DataRow r in dtads.Rows)
            //                    r["row_no"] = i++;
            //            }
            //            pgsource.DataSource = dtinner.DefaultView;
            //            pgsource.AllowPaging = true;
            //            pgsource.PageSize = 25;
            //            pgsource.CurrentPageIndex = pagingCurrentPage;
            //            lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
            //            lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
            //            LinkButton1.Enabled = !pgsource.IsFirstPage;
            //            lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
            //            LinkButton2.Enabled = !pgsource.IsLastPage;
            //            pgsource.DataSource = dtads.DefaultView;
            //            gvProgress.DataSource = pgsource;
            //            gvProgress.DataBind();
            //            lbltotalshowpageitem.Text = pgsource.FirstIndexInPage + 1 + " - " + (pgsource.FirstIndexInPage + pgsource.Count);
            //            Label3.Text = dtinner.Rows.Count.ToString();
            //            divcontentproduct.Visible = true;
            DataTable searchdt = (DataTable)Session["ExcelDT"];
            int[] iColumns = { 2, 4, 5, 0, 7, 8, 9, 11, 12, 14, 16, 17, 18, 19, 25, 26, 21, 23, 24, 27, 28 };
            RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
            objExport.ExportDetails(searchdt, iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "ProgressReportList.xls");
            //  }
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
            //    divcontentproduct.Visible = false;
            //}
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
            //}
            //  }
        }
        catch (Exception ex)
        {
        }
    }
    #region Shaliniin update code status
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (lblstartdate.Text != "" && lblenddate.Text != "" && mhylink.Text != "")
        {
            hfprorefno.Value = lblrefnoview.Text;
            DateTime dteoistart = Convert.ToDateTime(lblstartdate.Text.Trim(','));
            string eoistart = dteoistart.ToString("yyyy-MMM-dd");
            DateTime dteoiend = Convert.ToDateTime(lblenddate.Text.Trim(','));
            string eoiend = dteoiend.ToString("yyyy-MMM-dd");
            Lo.updateeoi(hfprorefno.Value.TrimEnd(','), rbeoimake2.SelectedValue.TrimEnd(','), eoistart.ToString().TrimEnd(','), eoiend.ToString().TrimEnd(','), mhylink.Text.TrimEnd(','));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully!!!');window.location ='PReport2';", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully!!!');window.location ='divstatus';", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "divstatus", "showPopup1();", true);
            ControlGrid();
            lblstartdate.ReadOnly = true;
            lblenddate.ReadOnly = true;
            mhylink.ReadOnly = true;
            Response.Redirect("PReport2");
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Fill mandatory')", true);
        }
    }
    protected void lnkupate_Click(object sender, EventArgs e)
    {
        if (txtsupplyorderdate.Text != "" && txtsupplydelivrydate.Text != "" && txtsupplyname.Text != "")
        {
            hfprorefno.Value = lblrefnoview.Text;
            if (txtsupplydelivrydate.Text != "")
            {
                DateTime dtsuppdelivery = Convert.ToDateTime(txtsupplydelivrydate.Text);
                string dtsuppdeliveryDate = dtsuppdelivery.ToString("dd-MM-yyyy");
                txtsupplydelivrydate.Text = dtsuppdelivery.ToString("dd-MM-yyyy");

                try
                {
                    txtsupplydelivrydate.Text = dtsuppdelivery.ToString();
                }
                catch (Exception ex)
                { txtsupplydelivrydate.Text = ""; }

            }
            else
            {
                txtsupplydelivrydate.Text = "";
            }
            if (txtsupplyorderdate.Text != "")
            {
                DateTime dtsodate = Convert.ToDateTime(txtsupplyorderdate.Text);
                string dtsoDatem = dtsodate.ToString("dd-MM-yyyy");
                txtsupplyorderdate.Text = dtsodate.ToString("dd-MM-yyyy");

                try
                {
                    txtsupplyorderdate.Text = dtsodate.ToString();
                }
                catch (Exception ex)
                { txtsupplydelivrydate.Text = ""; }

            }
            else
            {
                txtsupplyorderdate.Text = "";
            }
            Lo.updatesupplyorder(hfprorefno.Value.TrimEnd(','), rdblsoplaced.SelectedValue.TrimEnd(','),
                Convert.ToDecimal(txtsupplyvalue.Text.TrimEnd(',')), txtsupplydelivrydate.Text.TrimEnd(','), txtsupplyorderdate.Text.TrimEnd(','), txtsupplyname.Text.TrimEnd(','), txtsupplyaddress.Text.TrimEnd(','));

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully!!!');window.location ='PReport2';", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Fill mandatory')", true);
        }

    }
    Int32 mddlyr = 0;
    protected void lnkupatesuccess_Click(object sender, EventArgs e)
    {
        hfprorefno2.Value = "";
        hfprorefno2.Value = lblrefnoview.Text;
        string str = Lo.updateprogSuccessStory2(hfprorefno2.Value, txtyear.Text.TrimEnd(','), txtsuppman.Text.TrimEnd(','), txtsuppadrr.Text.TrimEnd(','));
        ControlGrid();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully!!!');window.location ='PReport2';", true);

    }
    #endregion
    #region Minakkshi update code interest shown status
    protected void gvinterestshown_RowCreated(object sender, GridViewRowEventArgs e)
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
    int mcount = 0;
    protected void lnkupdate_Click1(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in grdintshown.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk1 = (CheckBox)row.FindControl("SelectCheckBox");
                    Label hpref1 = (Label)row.FindControl("pref");
                    DropDownList ddlvalue1 = (DropDownList)row.FindControl("ddlreason");
                    DropDownList ddlstatus1 = (DropDownList)row.FindControl("ddlstatus1");
                    Label lrequestid = (Label)row.FindControl("lblrequestid");
                    TextBox txtreas = (TextBox)row.FindControl("TxtBxDesc");
                    if ((chk1.Checked && ddlvalue1.SelectedItem.Text != "Select"))
                    {
                        try
                        {
                            if (ddlvalue1.SelectedItem.Text == "Any other reason")
                            { Lo.updateintshownstatus(hpref1.Text, lrequestid.Text, ddlvalue1.SelectedValue.ToString(), txtreas.Text, ddlstatus1.SelectedValue.ToString()); }
                            else
                            {
                                Lo.updateintshownstatus(hpref1.Text, lrequestid.Text, ddlvalue1.SelectedValue.ToString(), txtreas.Text, "");
                            }
                            //DataTable dtretriveIntStatus = Lo.RetriveFilterCode(hpref1.Text, "", "getstatusintshow");
                            //if (dtretriveIntStatus.Rows.Count > 0)
                            //{
                            //    mcount = mcount + 1;
                            //    DataTable dtupdate = Lo.RetriveFilterCode(hpref1.Text, mcount.ToString(), "getintupdateno");
                            //}
                            success.Visible = true;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully!!!');window.location ='PReport2';", true);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select Checkbox and any one dropdown value');", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "InterestShownStatus", "showPopup4();", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            success.Visible = false;
        }
    }
    #endregion
    protected void grdintshown_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkum = (CheckBox)e.Row.FindControl("SelectCheckBox");
            DropDownList mddlreason = (DropDownList)e.Row.FindControl("ddlreason");
            DropDownList mddlstatus = (DropDownList)e.Row.FindControl("ddlstatus1");
            Label lblrem = (Label)e.Row.FindControl("txtremark");
            if (lblrem.Text != "")
            {
                //mddlreason.SelectedItem.Text = lblrem.Text;
                chkum.Checked = true;
            }
            else if (lblrem.Text == "")
            {
                lblrem.Text = "Action Pending";
            }
        }
    }
    protected void lbltotother_Click(object sender, EventArgs e)
    {
        CheckTarget.Value = "Yes";
        CheckNoOfint.Value = "";
        CheckpendInt.Value = "";
        Checkpendeoi.Value = "";
        CheckPendSupp.Value = "";
        CheckPenIndig.Value = "";
        SeachResult();
    }
    protected void lblNoOfProducts_Click(object sender, EventArgs e)
    {
        CheckTarget.Value = "";
        CheckNoOfint.Value = "Yes";
        CheckpendInt.Value = "";
        Checkpendeoi.Value = "";
        CheckPendSupp.Value = "";
        CheckPenIndig.Value = "";
        SeachResult();
        divbox.Visible = true;
        BindInterestCounts();
        BindNilCount();
    }
    protected void lbltotalintrestshowprod_Click(object sender, EventArgs e)
    {
        CheckTarget.Value = "";
        CheckNoOfint.Value = "";
        CheckpendInt.Value = "Yes";
        Checkpendeoi.Value = "";
        CheckPendSupp.Value = "";
        CheckPenIndig.Value = "";
        SeachResult();
    }
    protected void lbleoissue_Click(object sender, EventArgs e)
    {
        CheckTarget.Value = "";
        CheckNoOfint.Value = "";
        CheckpendInt.Value = "";
        Checkpendeoi.Value = "Yes";
        CheckPendSupp.Value = "";
        CheckPenIndig.Value = "";
        SeachResult();
    }
    protected void lblsuppyissue_Click(object sender, EventArgs e)
    {
        CheckTarget.Value = "";
        CheckNoOfint.Value = "";
        CheckpendInt.Value = "";
        Checkpendeoi.Value = "";
        CheckPendSupp.Value = "Yes";
        CheckPenIndig.Value = "";
        SeachResult();
    }
    protected void lblindig_Click(object sender, EventArgs e)
    {
        CheckTarget.Value = "";
        CheckNoOfint.Value = "";
        CheckpendInt.Value = "";
        Checkpendeoi.Value = "";
        CheckPendSupp.Value = "";
        CheckPenIndig.Value = "Yes";
        SeachResult();
    }
    #region Minakkshi code for searching page number
    protected void btngosearch_Click(object sender, EventArgs e)
    {
        if (System.Text.RegularExpressions.Regex.IsMatch(txtgosearch.Text, "[^0-9]"))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Please enter only number')", true);
        }
        else
        {
            int txtpage = Convert.ToInt32(txtgosearch.Text) - 1;
            pagingCurrentPage = Convert.ToInt32(txtpage.ToString());
            BindProduct();
        }
    }
    #endregion
    protected void lnkupdreason_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk1 = (CheckBox)row.FindControl("SelectCheckBox");
                    Label hpref1 = (Label)row.FindControl("pref");
                    DropDownList ddlvalue1 = (DropDownList)row.FindControl("ddlreason");
                    DropDownList ddlstatus = (DropDownList)row.FindControl("ddlstatus");
                    Label lrequestid = (Label)row.FindControl("lblrequestid");
                    TextBox txtreas = (TextBox)row.FindControl("TxtBxDesc");
                    if ((chk1.Checked && ddlvalue1.SelectedItem.Text != "Select"))
                    {
                        try
                        {
                            if (ddlvalue1.SelectedItem.Text == "Any other reason")
                            { Lo.updateintshownstatus(hpref1.Text, lrequestid.Text, ddlvalue1.SelectedValue.ToString(), txtreas.Text, ddlstatus.SelectedItem.Value); }
                            else
                            {
                                Lo.updateintshownstatus(hpref1.Text, lrequestid.Text, ddlvalue1.SelectedValue.ToString(), txtreas.Text, "");
                            }
                            DataTable dtretriveIntStatus = Lo.RetriveFilterCode(hpref1.Text, "", "getstatusintshow");
                            if (dtretriveIntStatus.Rows.Count > 0)
                            {
                                mcount = mcount + 1;
                                DataTable dtupdate = Lo.RetriveFilterCode(hpref1.Text, mcount.ToString(), "getintupdateno");
                            }
                            success.Visible = true;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully!!!');window.location ='PReport2';", true);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select Checkbox and any one dropdown value');", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "InterestShownStatus1", "showPopup8();", true);
                    }

                }
            }
        }
        catch (Exception ex)
        {
            //success.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(" + ex.Message + ")", true);
        }
    }
    protected void lnkresawait_Click(object sender, EventArgs e)
    {
        try
        {
            string CompRef = Session["CompanyRefNo"].ToString();
            string type = objEnc.DecryptData(Session["Type"].ToString());
            DataTable dt = Lo.RetriveFilterCode(CompRef.ToString(), type.ToString().Trim() + "_" + txtsearch.Text, "RespAwaited");
            if (dt.Rows.Count > 0)
            {
                GridView2.DataSource = dt;
                GridView2.DataBind();
                GridView2.Visible = true;
                lblcountgridtotal.InnerText = "Total record found :- " + dt.Rows.Count.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "reqnotneeded", "showPopup9();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record not found');", true);
            }

        }
        catch (Exception ex)
        { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true); }
    }
    protected void lnkrespeval_Click(object sender, EventArgs e)
    {
        try
        {
            string CompRef = Session["CompanyRefNo"].ToString();
            //if(txtsearch.Text==)
            string type = objEnc.DecryptData(Session["Type"].ToString());
            DataTable dt = Lo.RetriveFilterCode(CompRef.ToString(), type.ToString().Trim() + "_" + txtsearch.Text, "RespUEval");
            if (dt.Rows.Count > 0)
            {
                GridView3.DataSource = dt;
                GridView3.DataBind();
                GridView3.Visible = true; P1.InnerText = "Total record found :- " + dt.Rows.Count.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "underprocess", "showPopup10();", true);
            }


            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record not found');", true);
            }
        }
        catch (Exception ex)
        {
            //success.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(" + ex.Message + ")", true);
        }
    }
    protected void lnkvendnotfit_Click(object sender, EventArgs e)
    {
        try
        {
            string CompRef = Session["CompanyRefNo"].ToString();
            string type = objEnc.DecryptData(Session["Type"].ToString());
            DataTable dt = Lo.RetriveFilterCode(CompRef.ToString(), type.ToString().Trim() + "_" + txtsearch.Text, "Vendornotfit");
            if (dt.Rows.Count > 0)
            {
                GridView4.DataSource = dt;
                GridView4.DataBind();
                GridView4.Visible = true; P2.InnerText = "Total record found :- " + dt.Rows.Count.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "vendorsuit", "showPopup11();", true);
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record not found');", true);
            }
        }
        catch (Exception ex)
        {
            //success.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(" + ex.Message + ")", true);
        }
    }
    protected void lnkvendfit_Click(object sender, EventArgs e)
    {
        try
        {
            string CompRef = Session["CompanyRefNo"].ToString();
            string type = objEnc.DecryptData(Session["Type"].ToString());
            DataTable dt = Lo.RetriveFilterCode(CompRef.ToString(), type.ToString().Trim() + "_" + txtsearch.Text, "Vendorfit");
            if (dt.Rows.Count > 0)
            {
                GridView5.DataSource = dt;
                GridView5.DataBind();
                GridView5.Visible = true; P3.InnerText = "Total record found :- " + dt.Rows.Count.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "eoi", "showPopup12();", true);
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record not found');", true);
            }
        }
        catch (Exception ex)
        {
            //success.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(" + ex.Message + ")", true);
        }
    }
    protected void lnkitemnotreq_Click(object sender, EventArgs e)
    {
        try
        {
            string CompRef = Session["CompanyRefNo"].ToString();
            string type = objEnc.DecryptData(Session["Type"].ToString());
            DataTable dt = Lo.RetriveFilterCode(CompRef.ToString(), type.ToString().Trim() + "_" + txtsearch.Text, "Itemnotreq");
            if (dt.Rows.Count > 0)
            {
                GridView6.DataSource = dt;
                GridView6.DataBind();
                GridView6.Visible = true; P4.InnerText = "Total record found :- " + dt.Rows.Count.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "other_action", "showPopup13();", true);
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record not found');", true);
            }
        }
        catch (Exception ex)
        {
            //success.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(" + ex.Message + ")", true);
        }
    }
    protected void lnkindUproc_Click(object sender, EventArgs e)
    {
        try
        {
            string CompRef = Session["CompanyRefNo"].ToString();
            string type = objEnc.DecryptData(Session["Type"].ToString());
            DataTable dt = Lo.RetriveFilterCode(CompRef.ToString(), type.ToString().Trim() + "_" + txtsearch.Text, "IndigUProc");
            if (dt.Rows.Count > 0)
            {
                GridView7.DataSource = dt;
                GridView7.DataBind();
                GridView7.Visible = true; P5.InnerText = "Total record found :- " + dt.Rows.Count.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "reqdoesnotexist", "showPopup14();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record not found');", true);
            }

        }
        catch (Exception ex)
        {
            //success.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(" + ex.Message + ")", true);
        }
    }
    protected void lnkeoidone_Click(object sender, EventArgs e)
    {
        try
        {
            string CompRef = Session["CompanyRefNo"].ToString();
            string type = objEnc.DecryptData(Session["Type"].ToString());
            DataTable dt = Lo.RetriveFilterCode(CompRef.ToString(), type.ToString().Trim() + "_" + txtsearch.Text, "EoidoneSOplac");
            if (dt.Rows.Count > 0)
            {
                GridView8.DataSource = dt;
                GridView8.DataBind();
                GridView8.Visible = true; P6.InnerText = "Total record found :- " + dt.Rows.Count.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "soplaced", "showPopup15();", true);
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record not found');", true);
            }
        }
        catch (Exception ex)
        {
            //success.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(" + ex.Message + ")", true);
        }
    }
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {

                DropDownList ddlstatus1 = (DropDownList)row.FindControl("ddlstatus");
                DropDownList ddlreason1 = (DropDownList)row.FindControl("ddlreason");
                if (ddlstatus1.SelectedItem.Value == "1")
                {
                    ddlreason1.Enabled = true;
                    ddlreason1.Visible = true;
                    GridView1.Columns[11].Visible = true;
                    ddlreason1.Items.Add("Details given are not correct");
                    ddlreason1.Items.Add("Reply mail sent, response not received");
                    //ddlreason1.Items.Add("Any other reason");
                    ddlreason1.Items.Remove("Reply mail sent, response/details of vendor awaited");
                    ddlreason1.Items.Remove("Technical detail shared, response awaited");
                    ddlreason1.Items.Remove("Response received, Under evaluation");
                    ddlreason1.Items.Remove("Response received, Vendor has withdrawn interest/misunderstood");
                    ddlreason1.Items.Remove("Response received, Vendor does not meet the technical requirement");
                    ddlreason1.Items.Remove("Response received, Vendor does not meet Financial requirement");
                    ddlreason1.Items.Remove("Response received, Vendor declined to proceed further");
                    ddlreason1.Items.Remove("Response of vendor promising");
                    ddlreason1.Items.Remove("Awaiting NDA from vendor");
                    ddlreason1.Items.Remove("Sanction Order under approval");
                    ddlreason1.Items.Remove("Item obselete");
                    ddlreason1.Items.Remove("Requirement does not exist at present");
                    ddlreason1.Items.Remove("Vendor interest noted for future");
                    ddlreason1.Items.Remove("Indigenization U/Process with other vendor");
                    //   ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("EOI Published");
                    ddlreason1.Items.Remove("SO Placed");
                    ddlreason1.Items.Remove("Indigenized");

                }
                else if (ddlstatus1.SelectedItem.Value == "2")
                {
                    ddlreason1.Enabled = true;
                    ddlreason1.Visible = true;
                    GridView1.Columns[11].Visible = true;
                    ddlreason1.Items.Remove("Details given are not correct");
                    ddlreason1.Items.Remove("Reply mail sent, response not received");
                    ddlreason1.Items.Add("Reply mail sent, response/details of vendor awaited");
                    ddlreason1.Items.Add("Technical detail shared, response awaited");
                    // ddlreason1.Items.Add("Any other reason");
                    ddlreason1.Items.Remove("Response received, Under evaluation");
                    //  ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Response received, Vendor has withdrawn interest/misunderstood");
                    ddlreason1.Items.Remove("Response received, Vendor does not meet the technical requirement");
                    ddlreason1.Items.Remove("Response received, Vendor does not meet Financial requirement");
                    ddlreason1.Items.Remove("Response received, Vendor declined to proceed further");
                    ddlreason1.Items.Remove("Response of vendor promising");
                    ddlreason1.Items.Remove("Awaiting NDA from vendor");
                    ddlreason1.Items.Remove("Sanction Order under approval");
                    ddlreason1.Items.Remove("Item obselete");
                    ddlreason1.Items.Remove("Requirement does not exist at present");
                    ddlreason1.Items.Remove("Vendor interest noted for future");
                    ddlreason1.Items.Remove("Indigenization U/Process with other vendor");
                    ddlreason1.Items.Remove("EOI Published");
                    ddlreason1.Items.Remove("SO Placed");
                    ddlreason1.Items.Remove("Indigenized");

                }
                else if (ddlstatus1.SelectedItem.Value == "3")
                {
                    ddlreason1.Enabled = true;
                    ddlreason1.Visible = true;
                    GridView1.Columns[11].Visible = true;
                    ddlreason1.Items.Remove("Details given are not correct");
                    ddlreason1.Items.Remove("Reply mail sent, response not received");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Reply mail sent, response/details of vendor awaited");
                    ddlreason1.Items.Remove("Technical detail shared, response awaited");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Add("Response received, Under evaluation");
                    // ddlreason1.Items.Add("Any other reason");
                    ddlreason1.Items.Remove("Response received, Vendor has withdrawn interest/misunderstood");
                    ddlreason1.Items.Remove("Response received, Vendor does not meet the technical requirement");
                    ddlreason1.Items.Remove("Response received, Vendor does not meet Financial requirement");
                    ddlreason1.Items.Remove("Response received, Vendor declined to proceed further");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Response of vendor promising");
                    ddlreason1.Items.Remove("Awaiting NDA from vendor");
                    ddlreason1.Items.Remove("Sanction Order under approval");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Item obselete");
                    ddlreason1.Items.Remove("Requirement does not exist at present");
                    ddlreason1.Items.Remove("Vendor interest noted for future");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Indigenization U/Process with other vendor");
                    //  ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("EOI Published");
                    ddlreason1.Items.Remove("SO Placed");
                    ddlreason1.Items.Remove("Indigenized");
                    // ddlreason1.Items.Remove("Any other reason");
                }
                else if (ddlstatus1.SelectedItem.Value == "4")
                {
                    ddlreason1.Enabled = true;
                    ddlreason1.Visible = true;
                    GridView1.Columns[11].Visible = true;
                    ddlreason1.Items.Remove("Details given are not correct");
                    ddlreason1.Items.Remove("Reply mail sent, response not received");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Reply mail sent, response/details of vendor awaited");
                    ddlreason1.Items.Remove("Technical detail shared, response awaited");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Response received, Under evaluation");
                    //  ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Add("Response received, Vendor has withdrawn interest/misunderstood");
                    ddlreason1.Items.Add("Response received, Vendor does not meet the technical requirement");
                    ddlreason1.Items.Add("Response received, Vendor does not meet Financial requirement");
                    ddlreason1.Items.Add("Response received, Vendor declined to proceed further");
                    //  ddlreason1.Items.Add("Any other reason");
                    ddlreason1.Items.Remove("Response of vendor promising");
                    ddlreason1.Items.Remove("Awaiting NDA from vendor");
                    ddlreason1.Items.Remove("Sanction Order under approval");
                    //   ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Item obselete");
                    ddlreason1.Items.Remove("Requirement does not exist at present");
                    ddlreason1.Items.Remove("Vendor interest noted for future");
                    //  ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Indigenization U/Process with other vendor");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("EOI Published");
                    ddlreason1.Items.Remove("SO Placed");
                    ddlreason1.Items.Remove("Indigenized");
                    // ddlreason1.Items.Remove("Any other reason");
                }
                else if (ddlstatus1.SelectedItem.Value == "5")
                {
                    ddlreason1.Enabled = true;
                    ddlreason1.Visible = true;
                    GridView1.Columns[11].Visible = true;
                    ddlreason1.Items.Remove("Details given are not correct");
                    ddlreason1.Items.Remove("Reply mail sent, response not received");
                    //  ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Reply mail sent, response/details of vendor awaited");
                    ddlreason1.Items.Remove("Technical detail shared, response awaited");
                    //  ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Response received, Under evaluation");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Response received, Vendor has withdrawn interest/misunderstood");
                    ddlreason1.Items.Remove("Response received, Vendor does not meet the technical requirement");
                    ddlreason1.Items.Remove("Response received, Vendor does not meet Financial requirement");
                    ddlreason1.Items.Remove("Response received, Vendor declined to proceed further");
                    //   ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Add("Response of vendor promising");
                    ddlreason1.Items.Add("Awaiting NDA from vendor");
                    ddlreason1.Items.Add("Sanction Order under approval");
                    //  ddlreason1.Items.Add("Any other reason");
                    ddlreason1.Items.Remove("Item obselete");
                    ddlreason1.Items.Remove("Requirement does not exist at present");
                    ddlreason1.Items.Remove("Vendor interest noted for future");
                    //  ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Indigenization U/Process with other vendor");
                    //  ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("EOI Published");
                    ddlreason1.Items.Remove("SO Placed");
                    ddlreason1.Items.Remove("Indigenized");
                    //  ddlreason1.Items.Remove("Any other reason");
                }

                else if (ddlstatus1.SelectedItem.Value == "6")
                {
                    ddlreason1.Enabled = true;
                    ddlreason1.Visible = true;
                    GridView1.Columns[11].Visible = true;
                    ddlreason1.Items.Remove("Details given are not correct");
                    ddlreason1.Items.Remove("Reply mail sent, response not received");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Reply mail sent, response/details of vendor awaited");
                    ddlreason1.Items.Remove("Technical detail shared, response awaited");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Response received, Under evaluation");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Response received, Vendor has withdrawn interest/misunderstood");
                    ddlreason1.Items.Remove("Response received, Vendor does not meet the technical requirement");
                    ddlreason1.Items.Remove("Response received, Vendor does not meet Financial requirement");
                    ddlreason1.Items.Remove("Response received, Vendor declined to proceed further");
                    //  ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Response of vendor promising");
                    ddlreason1.Items.Remove("Awaiting NDA from vendor");
                    ddlreason1.Items.Remove("Sanction Order under approval");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Add("Item obselete");
                    ddlreason1.Items.Add("Requirement does not exist at present");
                    ddlreason1.Items.Add("Vendor interest noted for future");
                    // ddlreason1.Items.Remove("Any other reason"); 
                    ddlreason1.Items.Remove("Indigenization U/Process with other vendor");
                    // ddlreason1.Items.Add("Any other reason");
                    ddlreason1.Items.Remove("EOI Published");
                    ddlreason1.Items.Remove("SO Placed");
                    ddlreason1.Items.Remove("Indigenized");
                    //ddlreason1.Items.Remove("Any other reason");
                }
                else if (ddlstatus1.SelectedItem.Value == "7")
                {
                    ddlreason1.Enabled = true;
                    ddlreason1.Visible = true;
                    GridView1.Columns[11].Visible = true;
                    ddlreason1.Items.Remove("Details given are not correct");
                    ddlreason1.Items.Remove("Reply mail sent, response not received");
                    //  ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Reply mail sent, response/details of vendor awaited");
                    ddlreason1.Items.Remove("Technical detail shared, response awaited");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Response received, Under evaluation");
                    //  ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Response received, Vendor has withdrawn interest/misunderstood");
                    ddlreason1.Items.Remove("Response received, Vendor does not meet the technical requirement");
                    ddlreason1.Items.Remove("Response received, Vendor does not meet Financial requirement");
                    ddlreason1.Items.Remove("Response received, Vendor declined to proceed further");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Response of vendor promising");
                    ddlreason1.Items.Remove("Awaiting NDA from vendor");
                    ddlreason1.Items.Remove("Sanction Order under approval");
                    //  ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Item obselete");
                    ddlreason1.Items.Remove("Requirement does not exist at present");
                    ddlreason1.Items.Remove("Vendor interest noted for future");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Add("Indigenization U/Process with other vendor");
                    //  ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("EOI Published");
                    ddlreason1.Items.Remove("SO Placed");
                    ddlreason1.Items.Remove("Indigenized");
                    //ddlreason1.Items.Add("Any other reason");
                }
                else if (ddlstatus1.SelectedItem.Value == "8")
                {
                    ddlreason1.Enabled = true;
                    ddlreason1.Visible = true;
                    GridView1.Columns[11].Visible = true;
                    ddlreason1.Items.Remove("Details given are not correct");
                    ddlreason1.Items.Remove("Reply mail sent, response not received");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Reply mail sent, response/details of vendor awaited");
                    ddlreason1.Items.Remove("Technical detail shared, response awaited");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Response received, Under evaluation");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Response received, Vendor has withdrawn interest/misunderstood");
                    ddlreason1.Items.Remove("Response received, Vendor does not meet the technical requirement");
                    ddlreason1.Items.Remove("Response received, Vendor does not meet Financial requirement");
                    ddlreason1.Items.Remove("Response received, Vendor declined to proceed further");
                    //  ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Response of vendor promising");
                    ddlreason1.Items.Remove("Awaiting NDA from vendor");
                    ddlreason1.Items.Remove("Sanction Order under approval");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Item obselete");
                    ddlreason1.Items.Remove("Requirement does not exist at present");
                    ddlreason1.Items.Remove("Vendor interest noted for future");
                    // ddlreason1.Items.Add("Any other reason");
                    ddlreason1.Items.Remove("Indigenization U/Process with other vendor");
                    //ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Add("EoI published");
                    ddlreason1.Items.Add("SO placed");
                    ddlreason1.Items.Add("Indegenized");
                    // ddlreason1.Items.Remove("Any other reason");
                }
                else if (ddlstatus1.SelectedItem.Value == "Select")
                {
                    ddlreason1.Enabled = false;
                    ddlreason1.Visible = false;
                    GridView1.Columns[11].Visible = false;
                }
            }
        }
    }
    protected void ddlstatus1_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grdintshown.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {

                DropDownList ddlstatus = (DropDownList)row.FindControl("ddlstatus1");
                DropDownList ddlreason1 = (DropDownList)row.FindControl("ddlreason");
                if (ddlstatus.SelectedItem.Value == "1")
                {
                    ddlreason1.Enabled = true;
                    ddlreason1.Visible = true;
                    GridView1.Columns[11].Visible = true;
                    ddlreason1.Items.Add("Details given are not correct");
                    ddlreason1.Items.Add("Reply mail sent, response not received");
                    //ddlreason1.Items.Add("Any other reason");
                    ddlreason1.Items.Remove("Reply mail sent, response/details of vendor awaited");
                    ddlreason1.Items.Remove("Technical detail shared, response awaited");
                    ddlreason1.Items.Remove("Response received, Under evaluation");
                    ddlreason1.Items.Remove("Response received, Vendor has withdrawn interest/misunderstood");
                    ddlreason1.Items.Remove("Response received, Vendor does not meet the technical requirement");
                    ddlreason1.Items.Remove("Response received, Vendor does not meet Financial requirement");
                    ddlreason1.Items.Remove("Response received, Vendor declined to proceed further");
                    ddlreason1.Items.Remove("Response of vendor promising");
                    ddlreason1.Items.Remove("Awaiting NDA from vendor");
                    ddlreason1.Items.Remove("Sanction Order under approval");
                    ddlreason1.Items.Remove("Item obselete");
                    ddlreason1.Items.Remove("Requirement does not exist at present");
                    ddlreason1.Items.Remove("Vendor interest noted for future");
                    ddlreason1.Items.Remove("Indigenization U/Process with other vendor");
                    //   ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("EOI Published");
                    ddlreason1.Items.Remove("SO Placed");
                    ddlreason1.Items.Remove("Indigenized");

                }
                else if (ddlstatus.SelectedItem.Value == "2")
                {
                    ddlreason1.Enabled = true;
                    ddlreason1.Visible = true;
                    GridView1.Columns[11].Visible = true;
                    ddlreason1.Items.Remove("Details given are not correct");
                    ddlreason1.Items.Remove("Reply mail sent, response not received");
                    ddlreason1.Items.Add("Reply mail sent, response/details of vendor awaited");
                    ddlreason1.Items.Add("Technical detail shared, response awaited");
                    // ddlreason1.Items.Add("Any other reason");
                    ddlreason1.Items.Remove("Response received, Under evaluation");
                    //  ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Response received, Vendor has withdrawn interest/misunderstood");
                    ddlreason1.Items.Remove("Response received, Vendor does not meet the technical requirement");
                    ddlreason1.Items.Remove("Response received, Vendor does not meet Financial requirement");
                    ddlreason1.Items.Remove("Response received, Vendor declined to proceed further");
                    ddlreason1.Items.Remove("Response of vendor promising");
                    ddlreason1.Items.Remove("Awaiting NDA from vendor");
                    ddlreason1.Items.Remove("Sanction Order under approval");
                    ddlreason1.Items.Remove("Item obselete");
                    ddlreason1.Items.Remove("Requirement does not exist at present");
                    ddlreason1.Items.Remove("Vendor interest noted for future");
                    ddlreason1.Items.Remove("Indigenization U/Process with other vendor");
                    ddlreason1.Items.Remove("EOI Published");
                    ddlreason1.Items.Remove("SO Placed");
                    ddlreason1.Items.Remove("Indigenized");

                }
                else if (ddlstatus.SelectedItem.Value == "3")
                {
                    ddlreason1.Enabled = true;
                    ddlreason1.Visible = true;
                    GridView1.Columns[11].Visible = true;
                    ddlreason1.Items.Remove("Details given are not correct");
                    ddlreason1.Items.Remove("Reply mail sent, response not received");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Reply mail sent, response/details of vendor awaited");
                    ddlreason1.Items.Remove("Technical detail shared, response awaited");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Add("Response received, Under evaluation");
                    // ddlreason1.Items.Add("Any other reason");
                    ddlreason1.Items.Remove("Response received, Vendor has withdrawn interest/misunderstood");
                    ddlreason1.Items.Remove("Response received, Vendor does not meet the technical requirement");
                    ddlreason1.Items.Remove("Response received, Vendor does not meet Financial requirement");
                    ddlreason1.Items.Remove("Response received, Vendor declined to proceed further");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Response of vendor promising");
                    ddlreason1.Items.Remove("Awaiting NDA from vendor");
                    ddlreason1.Items.Remove("Sanction Order under approval");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Item obselete");
                    ddlreason1.Items.Remove("Requirement does not exist at present");
                    ddlreason1.Items.Remove("Vendor interest noted for future");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Indigenization U/Process with other vendor");
                    //  ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("EOI Published");
                    ddlreason1.Items.Remove("SO Placed");
                    ddlreason1.Items.Remove("Indigenized");
                    // ddlreason1.Items.Remove("Any other reason");
                }
                else if (ddlstatus.SelectedItem.Value == "4")
                {
                    ddlreason1.Enabled = true;
                    ddlreason1.Visible = true;
                    GridView1.Columns[11].Visible = true;
                    ddlreason1.Items.Remove("Details given are not correct");
                    ddlreason1.Items.Remove("Reply mail sent, response not received");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Reply mail sent, response/details of vendor awaited");
                    ddlreason1.Items.Remove("Technical detail shared, response awaited");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Response received, Under evaluation");
                    //  ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Add("Response received, Vendor has withdrawn interest/misunderstood");
                    ddlreason1.Items.Add("Response received, Vendor does not meet the technical requirement");
                    ddlreason1.Items.Add("Response received, Vendor does not meet Financial requirement");
                    ddlreason1.Items.Add("Response received, Vendor declined to proceed further");
                    //  ddlreason1.Items.Add("Any other reason");
                    ddlreason1.Items.Remove("Response of vendor promising");
                    ddlreason1.Items.Remove("Awaiting NDA from vendor");
                    ddlreason1.Items.Remove("Sanction Order under approval");
                    //   ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Item obselete");
                    ddlreason1.Items.Remove("Requirement does not exist at present");
                    ddlreason1.Items.Remove("Vendor interest noted for future");
                    //  ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Indigenization U/Process with other vendor");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("EOI Published");
                    ddlreason1.Items.Remove("SO Placed");
                    ddlreason1.Items.Remove("Indigenized");
                    // ddlreason1.Items.Remove("Any other reason");
                }
                else if (ddlstatus.SelectedItem.Value == "5")
                {
                    ddlreason1.Enabled = true;
                    ddlreason1.Visible = true;
                    GridView1.Columns[11].Visible = true;
                    ddlreason1.Items.Remove("Details given are not correct");
                    ddlreason1.Items.Remove("Reply mail sent, response not received");
                    //  ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Reply mail sent, response/details of vendor awaited");
                    ddlreason1.Items.Remove("Technical detail shared, response awaited");
                    //  ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Response received, Under evaluation");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Response received, Vendor has withdrawn interest/misunderstood");
                    ddlreason1.Items.Remove("Response received, Vendor does not meet the technical requirement");
                    ddlreason1.Items.Remove("Response received, Vendor does not meet Financial requirement");
                    ddlreason1.Items.Remove("Response received, Vendor declined to proceed further");
                    //   ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Add("Response of vendor promising");
                    ddlreason1.Items.Add("Awaiting NDA from vendor");
                    ddlreason1.Items.Add("Sanction Order under approval");
                    //  ddlreason1.Items.Add("Any other reason");
                    ddlreason1.Items.Remove("Item obselete");
                    ddlreason1.Items.Remove("Requirement does not exist at present");
                    ddlreason1.Items.Remove("Vendor interest noted for future");
                    //  ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Indigenization U/Process with other vendor");
                    //  ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("EOI Published");
                    ddlreason1.Items.Remove("SO Placed");
                    ddlreason1.Items.Remove("Indigenized");
                    //  ddlreason1.Items.Remove("Any other reason");
                }
                else if (ddlstatus.SelectedItem.Value == "6")
                {
                    ddlreason1.Enabled = true;
                    ddlreason1.Visible = true;
                    GridView1.Columns[11].Visible = true;
                    ddlreason1.Items.Remove("Details given are not correct");
                    ddlreason1.Items.Remove("Reply mail sent, response not received");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Reply mail sent, response/details of vendor awaited");
                    ddlreason1.Items.Remove("Technical detail shared, response awaited");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Response received, Under evaluation");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Response received, Vendor has withdrawn interest/misunderstood");
                    ddlreason1.Items.Remove("Response received, Vendor does not meet the technical requirement");
                    ddlreason1.Items.Remove("Response received, Vendor does not meet Financial requirement");
                    ddlreason1.Items.Remove("Response received, Vendor declined to proceed further");
                    //  ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Response of vendor promising");
                    ddlreason1.Items.Remove("Awaiting NDA from vendor");
                    ddlreason1.Items.Remove("Sanction Order under approval");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Add("Item obselete");
                    ddlreason1.Items.Add("Requirement does not exist at present");
                    ddlreason1.Items.Add("Vendor interest noted for future");
                    // ddlreason1.Items.Remove("Any other reason"); 
                    ddlreason1.Items.Remove("Indigenization U/Process with other vendor");
                    // ddlreason1.Items.Add("Any other reason");
                    ddlreason1.Items.Remove("EOI Published");
                    ddlreason1.Items.Remove("SO Placed");
                    ddlreason1.Items.Remove("Indigenized");
                    //ddlreason1.Items.Remove("Any other reason");
                }
                else if (ddlstatus.SelectedItem.Value == "7")
                {
                    ddlreason1.Enabled = true;
                    ddlreason1.Visible = true;
                    GridView1.Columns[11].Visible = true;
                    ddlreason1.Items.Remove("Details given are not correct");
                    ddlreason1.Items.Remove("Reply mail sent, response not received");
                    //  ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Reply mail sent, response/details of vendor awaited");
                    ddlreason1.Items.Remove("Technical detail shared, response awaited");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Response received, Under evaluation");
                    //  ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Response received, Vendor has withdrawn interest/misunderstood");
                    ddlreason1.Items.Remove("Response received, Vendor does not meet the technical requirement");
                    ddlreason1.Items.Remove("Response received, Vendor does not meet Financial requirement");
                    ddlreason1.Items.Remove("Response received, Vendor declined to proceed further");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Response of vendor promising");
                    ddlreason1.Items.Remove("Awaiting NDA from vendor");
                    ddlreason1.Items.Remove("Sanction Order under approval");
                    //  ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Item obselete");
                    ddlreason1.Items.Remove("Requirement does not exist at present");
                    ddlreason1.Items.Remove("Vendor interest noted for future");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Add("Indigenization U/Process with other vendor");
                    //  ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("EOI Published");
                    ddlreason1.Items.Remove("SO Placed");
                    ddlreason1.Items.Remove("Indigenized");
                    //ddlreason1.Items.Add("Any other reason");
                }
                else if (ddlstatus.SelectedItem.Value == "8")
                {
                    ddlreason1.Enabled = true;
                    ddlreason1.Visible = true;
                    GridView1.Columns[11].Visible = true;
                    ddlreason1.Items.Remove("Details given are not correct");
                    ddlreason1.Items.Remove("Reply mail sent, response not received");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Reply mail sent, response/details of vendor awaited");
                    ddlreason1.Items.Remove("Technical detail shared, response awaited");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Response received, Under evaluation");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Response received, Vendor has withdrawn interest/misunderstood");
                    ddlreason1.Items.Remove("Response received, Vendor does not meet the technical requirement");
                    ddlreason1.Items.Remove("Response received, Vendor does not meet Financial requirement");
                    ddlreason1.Items.Remove("Response received, Vendor declined to proceed further");
                    //  ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Response of vendor promising");
                    ddlreason1.Items.Remove("Awaiting NDA from vendor");
                    ddlreason1.Items.Remove("Sanction Order under approval");
                    // ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Remove("Item obselete");
                    ddlreason1.Items.Remove("Requirement does not exist at present");
                    ddlreason1.Items.Remove("Vendor interest noted for future");
                    // ddlreason1.Items.Add("Any other reason");
                    ddlreason1.Items.Remove("Indigenization U/Process with other vendor");
                    //ddlreason1.Items.Remove("Any other reason");
                    ddlreason1.Items.Add("EoI published");
                    ddlreason1.Items.Add("SO placed");
                    ddlreason1.Items.Add("Indegenized");
                    // ddlreason1.Items.Remove("Any other reason");
                }
                else if (ddlstatus.SelectedItem.Value == "")
                {
                    ddlreason1.Enabled = true;
                    ddlreason1.Visible = true;
                    grdintshown.Columns[11].Visible = true;
                }
            }
        }
    }
    protected void lnkunbconvend_Click(object sender, EventArgs e)
    {
        try
        {
            string CompRef = Session["CompanyRefNo"].ToString();
            string type = objEnc.DecryptData(Session["Type"].ToString());
            DataTable dt = Lo.RetriveFilterCode(CompRef.ToString(), type.ToString().Trim() + "_" + txtsearch.Text, "Unabcontvend");
            if (dt.Rows.Count > 0)
            {
                GridView9.DataSource = dt;
                GridView9.DataBind();
                P7.InnerText = "Total record found :- " + dt.Rows.Count.ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "unabcontvendor", "showPopup16();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not found')", true);
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void ddlreason_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grdintshown.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlreason1 = (DropDownList)row.FindControl("ddlreason");
                TextBox txtreas = (TextBox)row.FindControl("TxtBxDesc");
                if (ddlreason1.SelectedItem.Value == "Any other reason")
                {
                    txtreas.Visible = true;
                }
                else
                {
                    txtreas.Visible = false;
                }

            }
        }
    }
    protected void ddlreason_SelectedIndexChanged1(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlreason1 = (DropDownList)row.FindControl("ddlreason");
                TextBox txtreas = (TextBox)row.FindControl("TxtBxDesc");
                if (ddlreason1.SelectedItem.Value == "Any other reason")
                {
                    txtreas.Visible = true;
                }
                else
                {
                    txtreas.Visible = false;
                }

            }
        }
    }
    decimal sumFooterValue = 0;
    protected void gvhover_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label eoiso = (Label)e.Row.FindControl("lbleoisoplaced");
            Label eoirfp = (Label)e.Row.FindControl("lbleoirfp");
            Label totaleoi = (Label)e.Row.FindControl("lbltotaleoi");
            Label suppord = (Label)e.Row.FindControl("lblsuppord");
            Label ttlindig = (Label)e.Row.FindControl("lblttlindig");
            decimal sum1 = 0, sum2 = 0, sum3 = 0, sum4 = 0;
            //for (int i = -1; i < gvhover.Rows.Count; ++i)
            //{
            //    sum1 = Convert.ToInt32(eoirfp.Text);
            //    sum2 = Convert.ToInt32(totaleoi.Text);
            //    sum3 = Convert.ToInt32(suppord.Text);
            //    sum4 = Convert.ToInt32(ttlindig.Text);
            //}
            //eoiso.Text = (sum1 + sum2 + sum3 + sum4).ToString();
            //sumFooterValue += (sum1 + sum2 + sum3 + sum4);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[9].Text = sumFooterValue.ToString();
        }
    }
    protected void LinkButton17_Click(object sender, EventArgs e)
    {
        DataTable dt = Lo.RetriveFilterCode("", "", "Unabcontvend");
        if (dt.Rows.Count > 0)
        {
            GridView9.DataSource = dt;
            GridView9.DataBind();
            pgsource.DataSource = dt.DefaultView;
            pgsource.AllowPaging = true;
            pgsource.PageSize = 25;
            pgsource.CurrentPageIndex = pagingCurrentPage;
            lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
            lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
            LinkButton1.Enabled = !pgsource.IsFirstPage;
            lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
            LinkButton2.Enabled = !pgsource.IsLastPage;
            pgsource.DataSource = dt.DefaultView;
            GridView9.DataSource = pgsource;
            GridView9.DataBind();
            lbltotalshowpageitem.Text = pgsource.FirstIndexInPage + 1 + " - " + (pgsource.FirstIndexInPage + pgsource.Count);
            Label3.Text = "Total Product : " + lbltotalshowpageitem.Text;
            Label3.Text = dt.Rows.Count.ToString();
            divcontentproduct.Visible = true;
            int[] iColumns = { 0, 1, 2, 3, 4 };
            RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
            objExport.ExportDetails(dt, iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "ProgressReportList.xls");
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
            divcontentproduct.Visible = false;
        }
    }
    protected void LinkButton16_Click(object sender, EventArgs e)
    {
        DataTable dt = Lo.RetriveFilterCode("", "", "RespAwaited");
        if (dt.Rows.Count > 0)
        {
            GridView2.DataSource = dt;
            GridView2.DataBind();
            pgsource.DataSource = dt.DefaultView;
            pgsource.AllowPaging = true;
            pgsource.PageSize = 25;
            pgsource.CurrentPageIndex = pagingCurrentPage;
            lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
            lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
            LinkButton1.Enabled = !pgsource.IsFirstPage;
            lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
            LinkButton2.Enabled = !pgsource.IsLastPage;
            pgsource.DataSource = dt.DefaultView;
            GridView2.DataSource = pgsource;
            GridView2.DataBind();
            lbltotalshowpageitem.Text = pgsource.FirstIndexInPage + 1 + " - " + (pgsource.FirstIndexInPage + pgsource.Count);
            Label3.Text = "Total Product : " + lbltotalshowpageitem.Text;
            Label3.Text = dt.Rows.Count.ToString();
            divcontentproduct.Visible = true;
            int[] iColumns = { 0, 1, 2, 3, 4 };
            RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
            objExport.ExportDetails(dt, iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "ProgressReportList.xls");
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
            divcontentproduct.Visible = false;
        }
    }
    protected void LinkButton15_Click(object sender, EventArgs e)
    {
        DataTable dt = Lo.RetriveFilterCode("", "", "RespUEval");
        if (dt.Rows.Count > 0)
        {
            GridView3.DataSource = dt;
            GridView3.DataBind();
            pgsource.DataSource = dt.DefaultView;
            pgsource.AllowPaging = true;
            pgsource.PageSize = 25;
            pgsource.CurrentPageIndex = pagingCurrentPage;
            lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
            lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
            LinkButton1.Enabled = !pgsource.IsFirstPage;
            lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
            LinkButton2.Enabled = !pgsource.IsLastPage;
            pgsource.DataSource = dt.DefaultView;
            GridView3.DataSource = pgsource;
            GridView3.DataBind();
            lbltotalshowpageitem.Text = pgsource.FirstIndexInPage + 1 + " - " + (pgsource.FirstIndexInPage + pgsource.Count);
            Label3.Text = "Total Product : " + lbltotalshowpageitem.Text;
            Label3.Text = dt.Rows.Count.ToString();
            divcontentproduct.Visible = true;
            int[] iColumns = { 0, 1, 2, 3, 4 };
            RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
            objExport.ExportDetails(dt, iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "ProgressReportList.xls");
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
            divcontentproduct.Visible = false;
        }
    }
    protected void LinkButton14_Click(object sender, EventArgs e)
    {
        DataTable dt = Lo.RetriveFilterCode("", "", "Vendornotfit");
        if (dt.Rows.Count > 0)
        {
            GridView4.DataSource = dt;
            GridView4.DataBind();
            pgsource.DataSource = dt.DefaultView;
            pgsource.AllowPaging = true;
            pgsource.PageSize = 25;
            pgsource.CurrentPageIndex = pagingCurrentPage;
            lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
            lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
            LinkButton1.Enabled = !pgsource.IsFirstPage;
            lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
            LinkButton2.Enabled = !pgsource.IsLastPage;
            pgsource.DataSource = dt.DefaultView;
            GridView4.DataSource = pgsource;
            GridView4.DataBind();
            lbltotalshowpageitem.Text = pgsource.FirstIndexInPage + 1 + " - " + (pgsource.FirstIndexInPage + pgsource.Count);
            Label3.Text = "Total Product : " + lbltotalshowpageitem.Text;
            Label3.Text = dt.Rows.Count.ToString();
            divcontentproduct.Visible = true;
            int[] iColumns = { 0, 1, 2, 3, 4 };
            RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
            objExport.ExportDetails(dt, iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "ProgressReportList.xls");
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
            divcontentproduct.Visible = false;
        }
    }
    protected void LinkButton13_Click(object sender, EventArgs e)
    {
        DataTable dt = Lo.RetriveFilterCode("", "", "Vendorfit");
        if (dt.Rows.Count > 0)
        {
            GridView5.DataSource = dt;
            GridView5.DataBind();
            pgsource.DataSource = dt.DefaultView;
            pgsource.AllowPaging = true;
            pgsource.PageSize = 25;
            pgsource.CurrentPageIndex = pagingCurrentPage;
            lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
            lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
            LinkButton1.Enabled = !pgsource.IsFirstPage;
            lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
            LinkButton2.Enabled = !pgsource.IsLastPage;
            pgsource.DataSource = dt.DefaultView;
            GridView5.DataSource = pgsource;
            GridView5.DataBind();
            lbltotalshowpageitem.Text = pgsource.FirstIndexInPage + 1 + " - " + (pgsource.FirstIndexInPage + pgsource.Count);
            Label3.Text = "Total Product : " + lbltotalshowpageitem.Text;
            Label3.Text = dt.Rows.Count.ToString();
            divcontentproduct.Visible = true;
            int[] iColumns = { 0, 1, 2, 3, 4 };
            RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
            objExport.ExportDetails(dt, iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "ProgressReportList.xls");
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
            divcontentproduct.Visible = false;
        }
    }
    protected void LinkButton12_Click(object sender, EventArgs e)
    {
        DataTable dt = Lo.RetriveFilterCode("", "", "Itemnotreq");
        if (dt.Rows.Count > 0)
        {
            GridView6.DataSource = dt;
            GridView6.DataBind();
            pgsource.DataSource = dt.DefaultView;
            pgsource.AllowPaging = true;
            pgsource.PageSize = 25;
            pgsource.CurrentPageIndex = pagingCurrentPage;
            lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
            lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
            LinkButton1.Enabled = !pgsource.IsFirstPage;
            lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
            LinkButton2.Enabled = !pgsource.IsLastPage;
            pgsource.DataSource = dt.DefaultView;
            GridView6.DataSource = pgsource;
            GridView6.DataBind();
            lbltotalshowpageitem.Text = pgsource.FirstIndexInPage + 1 + " - " + (pgsource.FirstIndexInPage + pgsource.Count);
            Label3.Text = "Total Product : " + lbltotalshowpageitem.Text;
            Label3.Text = dt.Rows.Count.ToString();
            divcontentproduct.Visible = true;
            int[] iColumns = { 0, 1, 2, 3, 4 };
            RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
            objExport.ExportDetails(dt, iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "ProgressReportList.xls");
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
            divcontentproduct.Visible = false;
        }
    }
    protected void LinkButton10_Click(object sender, EventArgs e)
    {
        DataTable dt = Lo.RetriveFilterCode("", "", "IndigUProc");
        if (dt.Rows.Count > 0)
        {
            GridView7.DataSource = dt;
            GridView7.DataBind();
            pgsource.DataSource = dt.DefaultView;
            pgsource.AllowPaging = true;
            pgsource.PageSize = 25;
            pgsource.CurrentPageIndex = pagingCurrentPage;
            lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
            lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
            LinkButton1.Enabled = !pgsource.IsFirstPage;
            lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
            LinkButton2.Enabled = !pgsource.IsLastPage;
            pgsource.DataSource = dt.DefaultView;
            GridView7.DataSource = pgsource;
            GridView7.DataBind();
            lbltotalshowpageitem.Text = pgsource.FirstIndexInPage + 1 + " - " + (pgsource.FirstIndexInPage + pgsource.Count);
            Label3.Text = "Total Product : " + lbltotalshowpageitem.Text;
            Label3.Text = dt.Rows.Count.ToString();
            divcontentproduct.Visible = true;
            int[] iColumns = { 0, 1, 2, 3, 4 };
            RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
            objExport.ExportDetails(dt, iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "ProgressReportList.xls");
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
            divcontentproduct.Visible = false;
        }
    }
    protected void LinkButton11_Click(object sender, EventArgs e)
    {
        DataTable dt = Lo.RetriveFilterCode("", "", "EoidoneSOplac");
        if (dt.Rows.Count > 0)
        {
            GridView8.DataSource = dt;
            GridView8.DataBind();
            pgsource.DataSource = dt.DefaultView;
            pgsource.AllowPaging = true;
            pgsource.PageSize = 25;
            pgsource.CurrentPageIndex = pagingCurrentPage;
            lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
            lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
            LinkButton1.Enabled = !pgsource.IsFirstPage;
            lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
            LinkButton2.Enabled = !pgsource.IsLastPage;
            pgsource.DataSource = dt.DefaultView;
            GridView8.DataSource = pgsource;
            GridView8.DataBind();
            lbltotalshowpageitem.Text = pgsource.FirstIndexInPage + 1 + " - " + (pgsource.FirstIndexInPage + pgsource.Count);
            Label3.Text = "Total Product : " + lbltotalshowpageitem.Text;
            Label3.Text = dt.Rows.Count.ToString();
            divcontentproduct.Visible = true;
            int[] iColumns = { 0, 1, 2, 3, 4 };
            RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
            objExport.ExportDetails(dt, iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "ProgressReportList.xls");
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
            divcontentproduct.Visible = false;
        }
    }
    protected void LinkButton18_Click(object sender, EventArgs e)
    {

    }
    protected void lnknil_Click(object sender, EventArgs e)
    {
        try
        {
            string CompRef = Session["CompanyRefNo"].ToString();
            string type = objEnc.DecryptData(Session["Type"].ToString());
            DataTable dtnill = Lo.RetriveFilterCode(CompRef.ToString(), type.ToString().Trim() + "_" + txtsearch.Text, "nilaction");
            if (dtnill.Rows.Count > 0)
            {
                GridView10.DataSource = dtnill;
                GridView10.DataBind();
                GridView10.Visible = true; P9.InnerText = "Total record found :- " + dtnill.Rows.Count.ToString();

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "soplaced", "showPopup19();", true);
        }
        catch (Exception ex)
        {
            //success.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(" + ex.Message + ")", true);
        }
    }
    protected void LinkButton18_Click1(object sender, EventArgs e)
    {
        string CompRef = Session["CompanyRefNo"].ToString();
        string type = objEnc.DecryptData(Session["Type"].ToString());
        DataTable dtnill = Lo.RetriveFilterCode(CompRef.ToString(), type.ToString(), "nilaction");
        if (dtnill.Rows.Count > 0)
        {
            GridView10.DataSource = dtnill;
            GridView10.DataBind();
            pgsource.DataSource = dtnill.DefaultView;
            pgsource.AllowPaging = true;
            pgsource.PageSize = 25;
            pgsource.CurrentPageIndex = pagingCurrentPage;
            lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
            lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
            LinkButton1.Enabled = !pgsource.IsFirstPage;
            lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
            LinkButton2.Enabled = !pgsource.IsLastPage;
            pgsource.DataSource = dtnill.DefaultView;
            GridView10.DataSource = pgsource;
            GridView10.DataBind();
            lbltotalshowpageitem.Text = pgsource.FirstIndexInPage + 1 + " - " + (pgsource.FirstIndexInPage + pgsource.Count);
            Label3.Text = "Total Product : " + lbltotalshowpageitem.Text;
            Label3.Text = dtnill.Rows.Count.ToString();
            divcontentproduct.Visible = true;
            int[] iColumns = { 0, 1, 2 };
            RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
            objExport.ExportDetails(dtnill, iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "ProgressReportList.xls");
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
            divcontentproduct.Visible = false;
        }
    }
}