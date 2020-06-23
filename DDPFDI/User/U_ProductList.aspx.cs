using BusinessLayer;
using Encryption;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class User_U_ProductList : System.Web.UI.Page
{
    #region Pagevariable
    private Logic Lo = new Logic();
    private DataTable DtGrid = new DataTable();
    private string _msg = string.Empty;
    private string _sysMsg = string.Empty;
    DataUtility Co = new DataUtility();
    private Cryptography Encrypt = new Cryptography();
    private PagedDataSource pgsource = new PagedDataSource();
    DataTable DtCompany = new DataTable();
    DataTable DtFilterView = new DataTable();
    DataTable dtCart = new DataTable();
    DataRow dr;
    int n1 = 1;
    int n2 = 24;
    #endregion
    #region Pageload
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["DCart"] != null)
            {
                ViewState["buyitems"] = Session["DCart"];
                dtCart = (DataTable)ViewState["buyitems"];
                totalno.InnerText = dtCart.Rows.Count.ToString();
            }
            else
            {
                totalno.InnerText = dtCart.Rows.Count.ToString();
            }
            ControlGrid();
        }
    }
    private void ControlGrid()
    {
        BindComapnyCheckbox();
        BindIndusrtyDomain();
        BindPurposeProcuremnt();
        BindNSG();
        BindProduct();
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
    protected void BindProduct()
    {
        DtGrid = Lo.RetriveProductUser();
        if (DtGrid.Rows.Count > 0)
        {
            object sumObject = DtGrid.Compute("Sum(EstimatePrice)", string.Empty);
            decimal mvalue = Convert.ToDecimal(sumObject) / Convert.ToDecimal(100000);
            if (mvalue.ToString() == "")
            { lblestimateprice.Text = "0"; }
            else
            { lblestimateprice.Text = mvalue.ToString("N5"); }
            object sumObject1 = DtGrid.Compute("Sum(EstimatePricefuture)", string.Empty);
            decimal mvalue1 = Math.Round(Convert.ToDecimal(sumObject1) / Convert.ToDecimal(100000));
            if (mvalue1.ToString() == "")
            { lblfuturepurchase.Text = "0"; }
            else
            { lblfuturepurchase.Text = mvalue1.ToString("F2"); }
            Session["TempData"] = DtGrid;
            DtFilterView = (DataTable)Session["TempData"];
            UpdateDtGridValue(DtGrid);
            DataView dv4 = new DataView(DtGrid, "ROW_NUMBER >='" + n1 + "' And  ROW_NUMBER<='" + n2 + "'", "", DataViewRowState.CurrentRows);
            DataTable dtads4 = dv4.ToTable();
            pgsource.DataSource = DtGrid.DefaultView;
            pgsource.AllowPaging = true;
            pgsource.PageSize = 24;
            pgsource.CurrentPageIndex = pagingCurrentPage;
            ViewState["totpage"] = pgsource.PageCount;
            lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
            lbltotal.Text = " Search Results " + DtGrid.Rows.Count.ToString() + " items";
            lbltotalleft.Text = "Total items uploaded :-  " + DtGrid.Rows.Count.ToString();
            lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
            lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
            dlproduct.DataSource = dtads4.DefaultView;
            divcontentproduct.Visible = true;
            dlproduct.DataBind();
        }
        else
        {
            divcontentproduct.Visible = false;
            dlproduct.DataBind();
        }

    }
    #endregion
    #region pageindex code
    protected void lnkbtnPgPrevious_Click(object sender, EventArgs e)
    {
        txtpageno.Text = "";
        pagingCurrentPage -= 1;
        n2 = Convert.ToInt16(pagingCurrentPage) * Convert.ToInt16(24);

        if (ddlcomp.SelectedItem.Text != "Select" || ddlnsg.SelectedItem.Text != "Select" || ddlprodindustrydomain.SelectedItem.Text != "Select" || ddlprocurmentcatgory.SelectedItem.Text != "Select" || txtsearch.Text != "")
        {
            n1 = 1;
        }
        else
        {
            n1 = Convert.ToInt16(n2 - 24);
        }
        if (ddlcomp.SelectedItem.Text != "Select" || ddlnsg.SelectedItem.Text != "Select" || ddlprodindustrydomain.SelectedItem.Text != "Select" || ddlprocurmentcatgory.SelectedItem.Text != "Select" || txtsearch.Text != "")
        { SeachResult(); }
        else
        {
            BindProduct();
        }
    }
    protected void lnkbtnPgNext_Click(object sender, EventArgs e)
    {
        pagingCurrentPage += 1;
        int txtpage = Convert.ToInt32(pagingCurrentPage) + 1;
        string mcount = "";
        mcount = txtpage.ToString();
        if (txtpageno.Text != "")
        {
            txtpageno.Text = txtpage.ToString();
            mcount = txtpageno.Text;
        }

        n2 = Convert.ToInt16(mcount) * Convert.ToInt16(24);
        // n1 = Convert.ToInt16(n2 - 23);
        if (ddlcomp.SelectedItem.Text != "Select" || ddlnsg.SelectedItem.Text != "Select" || ddlprodindustrydomain.SelectedItem.Text != "Select" || ddlprocurmentcatgory.SelectedItem.Text != "Select" || txtsearch.Text != "")
        {
            n1 = 1;
        }
        else
        {
            n1 = Convert.ToInt16(n2 - 23);
        }
        if (ddlcomp.SelectedItem.Text != "Select" || ddlnsg.SelectedItem.Text != "Select" || ddlprodindustrydomain.SelectedItem.Text != "Select" || ddlprocurmentcatgory.SelectedItem.Text != "Select" || txtsearch.Text != "")
        { SeachResult(); }
        else
        {
            BindProduct();
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
            if (txtpageno.Text != "")
            {
                int txtpage = Convert.ToInt32(txtpageno.Text) - 1;
                pagingCurrentPage = Convert.ToInt32(txtpage.ToString());
            }
            n2 = Convert.ToInt16(pagingCurrentPage + 1) * Convert.ToInt16(24);
            if (ddlcomp.SelectedItem.Text != "Select" || ddlnsg.SelectedItem.Text != "Select" || ddlprodindustrydomain.SelectedItem.Text != "Select" || ddlprocurmentcatgory.SelectedItem.Text != "Select" || txtsearch.Text != "")
            {
                n1 = 1;
            }
            else
            {
                n1 = Convert.ToInt16(n2 + 1 - 24);
            }
            if (ddlcomp.SelectedItem.Text != "Select" || ddlnsg.SelectedItem.Text != "Select" || ddlprodindustrydomain.SelectedItem.Text != "Select" || ddlprocurmentcatgory.SelectedItem.Text != "Select" || txtsearch.Text != "")
            { SeachResult(); }
            else
            {
                { BindProduct(); }
            }
        }
    }
    #endregion
    #region  DDL Fill
    DataTable DtCompanyDDL = new DataTable();
    protected void BindComapnyCheckbox()
    {
        DtCompanyDDL = Lo.RetriveMasterData(0, "", "", 0, "", "", "Select");
        if (DtCompanyDDL.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlcomp, DtCompanyDDL, "CompanyName", "CompanyRefNo");
            ddlcomp.Items.Insert(0, "Select");
            ddlcomp.Enabled = true;
        }
        else
        {
            ddlcomp.Enabled = false;
        }
    }
    protected void BindComapnyCompanyCheckbox()
    {
        try
        {
            DataTable dtFactory = Lo.GetDashboardData("CompanyByname", ddlcomp.SelectedItem.Value);
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
            else
            {
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
        }
        catch (Exception ex)
        { }
    }
    protected void BindPurposeProcuremnt()
    {
        DataTable DtPurposeProcuremnt = new DataTable();
        if (ddlcomp.SelectedItem.Text != "Select")
        {
            DtPurposeProcuremnt = Lo.RetriveMasterSubCategoryDate(0, "PROCURMENT CATEGORY", "", "SelectProductCat", ddlcomp.SelectedItem.Value, "");
        }
        else
        {
            DtPurposeProcuremnt = Lo.RetriveMasterSubCategoryDate(0, "PROCURMENT CATEGORY", "", "SelectProductCat", "", "");
        }
        if (DtPurposeProcuremnt.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlprocurmentcatgory, DtPurposeProcuremnt, "ScategoryName", "SCategoryId");
            ddlprocurmentcatgory.Items.Insert(0, "Select");
        }
        else
        {
            DtPurposeProcuremnt = Lo.RetriveMasterSubCategoryDate(0, "PROCURMENT CATEGORY", "", "SelectProductCat", "", "");
            Co.FillDropdownlist(ddlprocurmentcatgory, DtPurposeProcuremnt, "ScategoryName", "SCategoryId");
            ddlprocurmentcatgory.Items.Insert(0, "Select");
        }
    }
    protected void BindNSG()
    {
        DataTable DtMasterCategroy = new DataTable();
        if (ddlcomp.SelectedItem.Text != "Select")
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "NSN GROUP", "", "SelectProductCat", ddlcomp.SelectedItem.Value, "");
        }
        else
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "NSN GROUP", "", "SelectProductCat", "", "");
        }
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlnsg, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlnsg.Items.Insert(0, "Select");
        }
        else
        {
            ddlnsg.Visible = false;
            divnsc.Visible = false;
            divic.Visible = false;
        }
    }
    protected void BindNSC()
    {
        DataTable DtMasterCategroy = new DataTable();
        if (ddlnsg.SelectedItem.Text != "Select")
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlnsg.SelectedItem.Value), "", "", "SubSelectID", "", "");
        }
        else
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "", "", "SubSelectSec", "", "");
        }
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlnsc, DtMasterCategroy, "SCategoryName", "SCategoryId");
            ddlnsc.Items.Insert(0, "Select");
            divnsc.Visible = true;
        }
        else
        {
            divic.Visible = false;
            divnsc.Visible = false;
        }
    }
    protected void BindIC()
    {
        DataTable DtMasterCategroyLevel3 = new DataTable();
        if (ddlnsc.SelectedIndex != -1)
        { DtMasterCategroyLevel3 = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlnsc.SelectedItem.Value), "", "", "SubSelectID", "", ""); }
        else
        { DtMasterCategroyLevel3 = Lo.RetriveMasterSubCategoryDate(0, "", "", "SubSelectthr", "", ""); }
        if (DtMasterCategroyLevel3.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlic, DtMasterCategroyLevel3, "SCategoryName", "SCategoryId");
            ddlic.Items.Insert(0, "Select");
            divic.Visible = true;
        }
        else
        {
            ddlic.Items.Insert(0, "Select");
            ddlic.Items.Insert(1, "NA");
        }
    }
    #endregion
    #region Filter Dropdown Code
    protected void ddlisindezinized_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    protected void ddlcomp_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindComapnyDivisionCheckbox();
        SeachResult();
        DataTable dtcompindustry = (DataTable)Session["TempData"];
        if (dtcompindustry.Rows.Count > 0)
        {
            //Bind Industry Domain
            DataView dvcompind = new DataView(dtcompindustry);
            dvcompind.RowFilter = "CompanyRefNo='" + ddlcomp.SelectedItem.Value + "' and ProdIndustryDoamin is not null";
            DataTable dtnew1 = dvcompind.ToTable();
            string[] strColnames = new string[2];
            strColnames[0] = "ProdIndustryDoamin";
            strColnames[1] = "TechnologyLevel1";
            dtnew1 = dvcompind.ToTable(true, strColnames);
            ddlprodindustrydomain.DataTextField = "ProdIndustryDoamin";
            ddlprodindustrydomain.DataValueField = "TechnologyLevel1";
            ddlprodindustrydomain.DataSource = dtnew1;
            ddlprodindustrydomain.DataBind();
            ddlprodindustrydomain.Items.Insert(0, "Select");
            if (ddlprodindustrydomain.SelectedItem.Text == "Select")
            {
                ddlindustrysubdoamin.SelectedValue = "Select";
                divisd.Visible = false;
            }
            //Bind SearchKeyword
            DataView dvcompind1 = new DataView(dtcompindustry);
            dvcompind1.RowFilter = "CompanyRefNo='" + ddlcomp.SelectedItem.Value + "' and SearchKeyword is not null";
            DataTable dtnew2 = dvcompind1.ToTable();
            string[] strColnames1 = new string[2];
            strColnames1[0] = "SearchKeyword";
            strColnames1[1] = "CompanyRefNo";
            dtnew2 = dvcompind1.ToTable(true, strColnames1);
            //ddlsearchkeywordsfilter.DataTextField = "SearchKeyword";
            //ddlsearchkeywordsfilter.DataValueField = "CompanyRefNo";
            //ddlsearchkeywordsfilter.DataSource = dtnew2;
            //ddlsearchkeywordsfilter.DataBind();
            //ddlsearchkeywordsfilter.Items.Insert(0, "Select");
        }
    }
    protected void ddldivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindComapnyDivisionUnitCheckbox();
        SeachResult();
        DataTable dtcompindustry = (DataTable)Session["TempData"];
        if (dtcompindustry.Rows.Count > 0)
        {
            //Bind Industry Domain
            DataView dvcompind = new DataView(dtcompindustry);
            dvcompind.RowFilter = "FactoryRefNo='" + ddldivision.SelectedItem.Value + "' and ProdIndustryDoamin is not null";
            DataTable dtnew1 = dvcompind.ToTable();
            string[] strColnames = new string[2];
            strColnames[0] = "ProdIndustryDoamin";
            strColnames[1] = "TechnologyLevel1";
            dtnew1 = dvcompind.ToTable(true, strColnames);
            ddlprodindustrydomain.DataTextField = "ProdIndustryDoamin";
            ddlprodindustrydomain.DataValueField = "TechnologyLevel1";
            ddlprodindustrydomain.DataSource = dtnew1;
            ddlprodindustrydomain.DataBind();
            ddlprodindustrydomain.Items.Insert(0, "Select");
            //Bind SearchKeyword
            DataView dvcompind1 = new DataView(dtcompindustry);
            dvcompind1.RowFilter = "FactoryRefNo='" + ddldivision.SelectedItem.Value + "' and SearchKeyword is not null";
            DataTable dtnew2 = dvcompind1.ToTable();
            string[] strColnames1 = new string[2];
            strColnames1[0] = "SearchKeyword";
            strColnames1[1] = "FactoryRefNo";
            dtnew2 = dvcompind1.ToTable(true, strColnames1);
            //ddlsearchkeywordsfilter.DataTextField = "SearchKeyword";
            //ddlsearchkeywordsfilter.DataValueField = "FactoryRefNo";
            //ddlsearchkeywordsfilter.DataSource = dtnew2;
            //ddlsearchkeywordsfilter.DataBind();
            //ddlsearchkeywordsfilter.Items.Insert(0, "Select");
        }
    }
    protected void ddlunit_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
        DataTable dtcompindustry = (DataTable)Session["TempData"];
        if (dtcompindustry.Rows.Count > 0)
        {
            //Bind Industry Domain
            DataView dvcompind = new DataView(dtcompindustry);
            dvcompind.RowFilter = "UnitRefNo='" + ddlunit.SelectedItem.Value + "' and ProdIndustryDoamin is not null";
            DataTable dtnew1 = dvcompind.ToTable();
            string[] strColnames = new string[2];
            strColnames[0] = "ProdIndustryDoamin";
            strColnames[1] = "TechnologyLevel1";
            dtnew1 = dvcompind.ToTable(true, strColnames);
            ddlprodindustrydomain.DataTextField = "ProdIndustryDoamin";
            ddlprodindustrydomain.DataValueField = "TechnologyLevel1";
            ddlprodindustrydomain.DataSource = dtnew1;
            ddlprodindustrydomain.DataBind();
            ddlprodindustrydomain.Items.Insert(0, "Select");
            //Bind SearchKeyword
            DataView dvcompind1 = new DataView(dtcompindustry);
            dvcompind1.RowFilter = "UnitRefNo='" + ddlunit.SelectedItem.Value + "' and SearchKeyword is not null";
            DataTable dtnew2 = dvcompind1.ToTable();
            string[] strColnames1 = new string[2];
            strColnames1[0] = "SearchKeyword";
            strColnames1[1] = "UnitRefNo";
            dtnew2 = dvcompind1.ToTable(true, strColnames1);
            //ddlsearchkeywordsfilter.DataTextField = "SearchKeyword";
            //ddlsearchkeywordsfilter.DataValueField = "UnitRefNo";
            //ddlsearchkeywordsfilter.DataSource = dtnew2;
            //ddlsearchkeywordsfilter.DataBind();
            //ddlsearchkeywordsfilter.Items.Insert(0, "Select");
        }
    }
    protected void ddlnameofdefplat_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    protected void ddlprodindustrydomain_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtisd = new DataTable();
        if (ddlprodindustrydomain.SelectedItem.Text != "Select")
        {
            dtisd = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlprodindustrydomain.SelectedItem.Value), "", "", "SubSelectID", "", "");
            if (dtisd.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlindustrysubdoamin, dtisd, "ScategoryName", "SCategoryId");
                ddlindustrysubdoamin.Items.Insert(0, "Select");
                divisd.Visible = true;
            }
            else
            {
                ddlprocurmentcatgory.Items.Insert(0, "Select");
                divisd.Visible = false;
            }
        }
        SeachResult();
    }
    protected void ddlprocurmentcatgory_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    protected void ddldeclaration_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    protected void ddlimported_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    protected void ddlsearchkeywordsfilter_TextChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    protected void ddlnsg_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlnsg.SelectedItem.Text == "Select")
        {
            divnsc.Visible = false;
            ddlnsc.Items.Insert(0, "Select");
            divic.Visible = false;
            ddlic.Items.Insert(0, "Select");
        }
        else
        {
            BindNSC();
        }
        if (ddlnsg.SelectedItem.Text != "Select")
        {
            SeachResult();
        }
    }
    protected void ddlnsc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlnsc.SelectedItem.Text == "Select")
        {
            divic.Visible = false;
            ddlic.Items.Insert(0, "Select");
        }
        else
        {
            BindIC();
        }
        if (ddlnsc.SelectedItem.Text != "Select")
        {
            SeachResult();
        }
    }
    protected void ddlic_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlic.SelectedItem.Text == "Select")
        { }
        else
        {
            SeachResult();
        }
    }
    protected void ddlindustrysubdoamin_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    #endregion
    #region Search Code Filter Code
    string insert1 = "";
    protected string Dvinsert()
    {
        DataTable insert = new DataTable();
        insert.Columns.Add(new DataColumn("Column", typeof(string)));
        insert.Columns.Add(new DataColumn("Value", typeof(string)));
        DataRow dr;
        if (ddlcomp.SelectedItem.Text != "Select")
        {
            dr = insert.NewRow();
            dr["Column"] = "CompanyRefNo" + "=";
            dr["Value"] = "'" + ddlcomp.SelectedItem.Value + "'";
            insert.Rows.Add(dr);
            if (ddldivision.Visible == true && ddldivision.SelectedItem.Text != "Select")
            {
                dr = insert.NewRow();
                dr["Column"] = "FactoryRefNo" + "=";
                dr["Value"] = "'" + ddldivision.SelectedItem.Value + "'";
                insert.Rows.Add(dr);
                if (ddlunit.Visible == true && ddlunit.SelectedItem.Text != "Select")
                {
                    dr = insert.NewRow();
                    dr["Column"] = "UnitRefNo" + "=";
                    dr["Value"] = "'" + ddlunit.SelectedItem.Value + "'";
                    insert.Rows.Add(dr);
                }
            }
        }
        if (ddlnsg.SelectedItem.Text != "Select")
        {
            dr = insert.NewRow();
            dr["Column"] = "ProductLevel1" + "=";
            dr["Value"] = "'" + ddlnsg.SelectedItem.Value + "'";
            insert.Rows.Add(dr);
            if (divnsc.Visible != false)
            {
                if (ddlnsc.SelectedItem.Text != "Select")
                {
                    dr = insert.NewRow();
                    dr["Column"] = "ProductLevel2" + "=";
                    dr["Value"] = "'" + ddlnsc.SelectedItem.Value + "'";
                    insert.Rows.Add(dr);
                }
            }
            if (divic.Visible != false)
            {
                if (ddlic.SelectedItem.Text != "Select")
                {
                    dr = insert.NewRow();
                    dr["Column"] = "ProductLevel3" + "=";
                    dr["Value"] = "'" + ddlic.SelectedItem.Value + "'";
                    insert.Rows.Add(dr);
                }
            }
        }
        if (ddlprodindustrydomain.SelectedItem.Text != "Select")
        {
            dr = insert.NewRow();
            dr["Column"] = "TechnologyLevel1" + "=";
            dr["Value"] = "'" + ddlprodindustrydomain.SelectedItem.Value + "'";
            insert.Rows.Add(dr);
            if (divisd.Visible != false)
            {
                if (ddlindustrysubdoamin.SelectedItem.Text != "Select")
                {
                    dr = insert.NewRow();
                    dr["Column"] = "TechnologyLevel2" + "=";
                    dr["Value"] = "'" + ddlindustrysubdoamin.SelectedItem.Value + "'";
                    insert.Rows.Add(dr);
                }
            }
        }
        if (chklast5year.SelectedIndex != -1)
        {
            if (chklast5year.SelectedItem.Value == "Y")
            {
                dr = insert.NewRow();
                dr["Column"] = "EstimatePrice" + "> ";
                dr["Value"] = "0";
                insert.Rows.Add(dr);
            }
            else
            {
                dr = insert.NewRow();
                dr["Column"] = "EstimatePrice" + "=";
                dr["Value"] = "'0'";
                insert.Rows.Add(dr);
            }
        }
        if (chktendor.SelectedIndex != -1)
        {
            if (chktendor.SelectedItem.Value == "Live")
            {
                dr = insert.NewRow();
                dr["Column"] = "TenderStatus" + "=";
                dr["Value"] = "'" + chktendor.SelectedItem.Value + "' and EOIStatus='" + chktendor.SelectedItem.Value + "'";
                insert.Rows.Add(dr);
            }
            else
            {
                dr = insert.NewRow();
                dr["Column"] = "(TenderStatus " + "=";
                dr["Value"] = "'Archive' and EOIStatus ='Archive') or (TenderStatus='Not Floated' and  EOIStatus='Not Floated') or (TenderStatus='To be Floated shortly' and  EOIStatus='To be Floated shortly')";
                insert.Rows.Add(dr);
            }
        }
        if (rberffpurchase.SelectedIndex != -1)
        {
            if (rberffpurchase.SelectedItem.Value != "0")
            {
                dr = insert.NewRow();
                dr["Column"] = "EstimatePricefuture" + ">";
                dr["Value"] = "'0'";
                insert.Rows.Add(dr);
            }
            else
            {
                dr = insert.NewRow();
                dr["Column"] = "EstimatePricefuture" + "=";
                dr["Value"] = "'0'";
                insert.Rows.Add(dr);
            }
        }
        if (ddlprocurmentcatgory.SelectedItem.Text != "Select")
        {
            dr = insert.NewRow();
            dr["Column"] = "PurposeofProcurement" + " like";
            dr["Value"] = "'%" + ddlprocurmentcatgory.SelectedItem.Value + "%'";
            insert.Rows.Add(dr);
        }
        if (ddldeclaration.SelectedItem.Text != "Select")
        {
            dr = insert.NewRow();
            dr["Column"] = "IsShowGeneral" + "="; ;
            dr["Value"] = "'" + ddldeclaration.SelectedItem.Value + "'";
            insert.Rows.Add(dr);
        }
        if (txtsearch.Text != "" && txtsearch.Text.Length >= 3)
        {
            dr = insert.NewRow();
            dr["Column"] = "(CompanyName like";
            dr["Value"] = "'%" + txtsearch.Text + "%' or (FactoryName like '%" + txtsearch.Text + "%') or (UnitName like '%" + txtsearch.Text + "%') or (NSNGroup like '%" + txtsearch.Text + "%') or (DefencePlatform like '%" + txtsearch.Text + "%') or (ProdIndustryDoamin  like '%" + txtsearch.Text + "%') or (NSNGroupClass like '%" + txtsearch.Text + "%')  or (ItemCode like '%" + txtsearch.Text + "%')  or (ProdIndustrySubDomain  like '%" + txtsearch.Text + "%') or (ProductDescription  like '%" + txtsearch.Text + "%') or (NSCCode like '%" + txtsearch.Text + "%') or SearchKeyword like '%" + txtsearch.Text + "%' or IsIndeginized like '%" + txtsearch.Text + "%')";
            insert.Rows.Add(dr);
        }
        if (ddlisindezinized.SelectedItem.Text != "Select")
        {
            dr = insert.NewRow();
            dr["Column"] = "IsIndeginized" + "="; ;
            dr["Value"] = "'" + ddlisindezinized.SelectedItem.Value + "'";
            insert.Rows.Add(dr);
        }
        //if (ddlsearchkeywordsfilter.Text != "Select" && ddlsearchkeywordsfilter.Text.Length >= 3)
        //{
        //    dr = insert.NewRow();
        //    dr["Column"] = "ProductDescription" + " like";
        //    dr["Value"] = "'%" + ddlsearchkeywordsfilter.Text + "%'";
        //    insert.Rows.Add(dr);
        //}
        for (int i = 0; insert.Rows.Count > i; i++)
        {
            insert1 = insert1 + insert.Rows[i]["Column"].ToString() + " " + insert.Rows[i]["Value"].ToString() + " " + " and ";
            Div1.Visible = true;
        }
        insert1 = insert1.Substring(0, insert1.Length - 5);
        return insert1;
    }
    protected string BindInsertfilter()
    {
        return Dvinsert();
    }
    protected void SeachResult(string sortExpression = null)
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
                    dv.Sort = "EstimatePrice,EstimatePricefuture desc";
                    DataTable dtinner = dv.ToTable();
                    object sumObject = dtinner.Compute("Sum(EstimatePrice)", string.Empty);
                    decimal mvalue = Convert.ToDecimal(sumObject) / Convert.ToDecimal(100000);
                    if (mvalue.ToString() == "")
                    { lblestimateprice.Text = "0"; }
                    else
                    { lblestimateprice.Text = mvalue.ToString("N5"); }
                    object sumObject1 = dtinner.Compute("Sum(EstimatePricefuture)", string.Empty);
                    decimal mvalue1 = Math.Round(Convert.ToDecimal(sumObject1) / Convert.ToDecimal(100000));
                    if (mvalue1.ToString() == "")
                    { lblfuturepurchase.Text = "0"; }
                    else
                    { lblfuturepurchase.Text = mvalue1.ToString("F2"); }
                    lbltotal.Text = " Search Results " + dtinner.Rows.Count.ToString() + " items";
                    DataTable dtads = dv.ToTable();
                    dtads.Columns.Add("RCount", typeof(Int64));
                    for (int n = 0; dtads.Rows.Count > n; n++)
                    {
                        dtads.Rows[n]["RCount"] = n + 1;
                    }
                    DataView dv4 = new DataView(dtads, "RCount >='" + n1 + "' And  RCount<='" + n2 + "'", "", DataViewRowState.CurrentRows);
                    dtads = dv4.ToTable();
                    if (dtads.Rows.Count > 0)
                    {
                        pgsource.DataSource = dtinner.DefaultView;
                        pgsource.AllowPaging = true;
                        pgsource.PageSize = 24;
                        pgsource.CurrentPageIndex = pagingCurrentPage;
                        ViewState["totpage"] = pgsource.PageCount;
                        lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                        lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                        lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                        pgsource.DataSource = dtads.DefaultView;
                        dlproduct.DataSource = pgsource;
                        dlproduct.DataBind();
                        lbltotalleft.Text = "Total items uploaded :-  " + DtFilterView.Rows.Count.ToString();
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
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    #endregion
    #region CartCode
    protected void dlproduct_ItemCommand(object source, DataListCommandEventArgs e)
    {
        #region ViewOneProd
        if (e.CommandName == "View")
        {
            DataListItem item = (DataListItem)(((Control)(e.CommandSource)).NamingContainer);
            string Role = ((HiddenField)item.FindControl("hfrole")).Value;
            DataTable DtView = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductMasterID", Role.ToString());
            if (DtView.Rows.Count > 0)
            {
                lblrefnoview.Text = e.CommandArgument.ToString();
                lblcompname.Text = DtView.Rows[0]["CompanyName"].ToString();
                lbldiviname.Text = DtView.Rows[0]["FactoryName"].ToString();
                lblunitnamepro.Text = DtView.Rows[0]["UnitName"].ToString();
                lblnsngroup.Text = DtView.Rows[0]["ProdLevel1Name"].ToString();
                lblnsngroupclass.Text = DtView.Rows[0]["ProdLevel2Name"].ToString();
                // lblnsccode.Text = DtView.Rows[0]["NSCCode"].ToString();
                // lblniincode.Text = DtView.Rows[0]["NIINCode"].ToString();
                lblsearchkeywords.Text = DtView.Rows[0]["Searchkeyword"].ToString();
                lblproductdescription.Text = DtView.Rows[0]["ProductDescription"].ToString();
                lbldpsupartno.Text = DtView.Rows[0]["DPSUPartNumber"].ToString();
                lblhsncode8digit.Text = DtView.Rows[0]["HsnCode8digit"].ToString();
                prodIndustryDomain.Text = DtView.Rows[0]["TechLevel1Name"].ToString();
                ProdIndusSubDomain.Text = DtView.Rows[0]["Techlevel2Name"].ToString();
                //lblprodalredyindeginized.Text = DtView.Rows[0]["IsIndeginized"].ToString();
                //if (lblprodalredyindeginized.Text == "Y")
                //{
                //    lblprodalredyindeginized.Text = "Yes";
                //    tableIsIndiginized.Visible = false;
                //    lblmanufacturename.Text = DtView.Rows[0]["ManufactureName"].ToString();
                //    lblmanaddress.Text = DtView.Rows[0]["ManufactureAddress"].ToString();
                //    lblyearofindiginization.Text = DtView.Rows[0]["FY"].ToString();
                //}
                //else
                //{
                //    lblprodalredyindeginized.Text = "No";
                //    tableIsIndiginized.Visible = false;
                //}
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
                    decimal msumobject = Convert.ToDecimal(tot) / Convert.ToDecimal(100000);
                    lblvalueimport.Text = msumobject.ToString("N5");
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
                lblprocremarks.Text = DtView.Rows[0]["ProcurmentCategoryRemark"].ToString();
                lbleoirep.Text = DtView.Rows[0]["EOIStatus"].ToString();
                lbleoilink.Text = DtView.Rows[0]["EOIURL"].ToString();
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
                        // lblempcode.Text = dtNodal.Rows[0]["NodalOfficerRefNo"].ToString();
                        lblempname.Text = dtNodal.Rows[0]["NodalOficerName"].ToString();
                        lbldesignation.Text = dtNodal.Rows[0]["Designation"].ToString();
                        lblemailidpro.Text = dtNodal.Rows[0]["NodalOfficerEmail"].ToString();
                        lblmobilenumber.Text = dtNodal.Rows[0]["NodalOfficerMobile"].ToString();
                        lblphonenumber.Text = dtNodal.Rows[0]["NodalOfficerTelephone"].ToString();
                        lblfaxpro.Text = dtNodal.Rows[0]["NodalOfficerFax"].ToString();
                    }
                }
                //string Nodel2Id = DtView.Rows[0]["NodalDetail2"].ToString();
                //if (Nodel2Id.ToString() != "")
                //{
                //    DataTable dtNodal2 = Lo.RetriveProductCode(Nodel2Id.ToString(), "", "ProdNodal", "");
                //    if (dtNodal2.Rows.Count == 2)
                //    {
                //        lblempcode2.Text = dtNodal2.Rows[0]["NodalOfficerRefNo"].ToString();
                //        lblempname2.Text = dtNodal2.Rows[0]["NodalOficerName"].ToString();
                //        lbldesignation2.Text = dtNodal2.Rows[0]["Designation"].ToString();
                //        lblemailid2.Text = dtNodal2.Rows[0]["NodalOfficerEmail"].ToString();
                //        lblmobilenumber2.Text = dtNodal2.Rows[0]["NodalOfficerMobile"].ToString();
                //        lblphonenumber2.Text = dtNodal2.Rows[0]["NodalOfficerTelephone"].ToString();
                //        lblfax2.Text = dtNodal2.Rows[0]["NodalOfficerFax"].ToString();
                //    }
                //}
                ScriptManager.RegisterStartupScript(this, GetType(), "ProductCompany", "showPopup();", true);
            }
        }

        #endregion
        #region AddCart
        else if (e.CommandName == "addcart")
        {
            LinkButton lnkId = (LinkButton)e.Item.FindControl("lbaddcart");
            dtCart.Columns.Add(new DataColumn("ProductRefNo", typeof(string)));
            if (ViewState["buyitems"] != null)
            {
                dtCart = (DataTable)ViewState["buyitems"];
                if (dtCart.Rows.Count > 0)
                {
                    string InCart = "";
                    for (int i = 0; dtCart.Rows.Count > i; i++)
                    {
                        if (e.CommandArgument.ToString() == dtCart.Rows[i]["ProductRefNo"].ToString())
                        {
                            InCart = "AlreadyInCart";
                            lnkId.Text = "Successfully Added";
                            lnkId.Attributes.Remove("Class");
                            lnkId.Attributes.Add("Class", "btn btn-success btn-sm btn-block");
                            break;
                        }
                    }
                    if (InCart != "AlreadyInCart")
                    {
                        dr = dtCart.NewRow();
                        dr["ProductRefNo"] = e.CommandArgument.ToString();
                        dtCart.Rows.Add(dr);
                        ViewState["buyitems"] = dtCart;
                        lnkId.Text = "Successfully Added";
                        lnkId.Attributes.Remove("Class");
                        lnkId.Attributes.Add("Class", "btn btn-success btn-sm btn-block");

                    }
                }
            }
            else
            {
                dr = dtCart.NewRow();
                dr["ProductRefNo"] = e.CommandArgument.ToString();
                dtCart.Rows.Add(dr);
                ViewState["buyitems"] = dtCart;
                lnkId.Text = "Successfully Added";
                lnkId.Attributes.Remove("Class");
                lnkId.Attributes.Add("Class", "btn btn-success btn-sm btn-block");
            }
            if (dtCart != null)
            {
                totalno.InnerText = dtCart.Rows.Count.ToString();
            }
            else
            {
                totalno.InnerText = "0";
            }
        }
        #endregion
    }
    protected void lbtotalcart_Click(object sender, EventArgs e)
    {
        if (ViewState["buyitems"] != null)
        {
            Session["DCart"] = ViewState["buyitems"];
            Response.Redirect("U_Cart");
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No product in cart')", true);
        }
    }
    protected void lbcart_Click(object sender, EventArgs e)
    {
        if (ViewState["buyitems"] != null)
        {
            Session["DCart"] = ViewState["buyitems"];
            Response.Redirect("U_Cart");
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No product in cart')", true);
        }
    }
    #endregion
    protected void dlproduct_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (dtCart.Rows.Count > 0)
            {
                LinkButton lnkId = (LinkButton)e.Item.FindControl("lbaddcart");
                HiddenField hf = (HiddenField)e.Item.FindControl("hfr");
                for (int i = 0; dtCart.Rows.Count > i; i++)
                {
                    if (dtCart.Rows[i]["ProductRefNo"].ToString() == hf.Value)
                        lnkId.Text = "Successfully Added";
                    lnkId.Attributes.Remove("Class");
                    lnkId.Attributes.Add("Class", "btn btn-success btn-sm btn-block");
                }
            }
            Label lblep = (Label)e.Item.FindControl("lblep");
            decimal mlblep = Math.Round(Convert.ToDecimal(lblep.Text) / Convert.ToDecimal(100000));
            lblep.Text = mlblep.ToString("F2");
        }
    }
    protected void totoalmore_Click(object sender, EventArgs e)
    {
        FillProduct();
    }
    public void FillProduct()
    {

        DataTable dtProductDetail = Lo.RetriveProductIndig();
        if (dtProductDetail.Rows.Count > 0)
        {
            gvPrdoct.DataSource = dtProductDetail;
            gvPrdoct.DataBind();
            ScriptManager.RegisterStartupScript(this, GetType(), "divCompany", "showPopup1();", true);
        }
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Div1.Visible = false;
        Response.RedirectToRoute("uproductlist");
    }
    protected void chklast5year_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    protected void chktendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    protected void txtsearch_TextChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    protected void rberffpurchase_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
}