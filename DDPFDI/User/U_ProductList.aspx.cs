using BusinessLayer;
using Encryption;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Windows.Input;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

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
    int n2 = 50;
    #endregion
    #region Pageload
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
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
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Technical Error:- " + ex.Message + "');", true);
            }
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
        lblyearvalue.Text = rbsort.SelectedItem.Text;
        //if (rbsort.SelectedIndex != -1)
        //{
        //    DtGrid = Lo.RetriveProductUser(rbsort.SelectedItem.Value);
        //}
        //else
        //{
        DtGrid = Lo.RetriveProductUser("");
        // }
        if (DtGrid.Rows.Count > 0)
        {
            DtGrid.Columns.Add("RCount", typeof(Int64));
            for (int n = 0; DtGrid.Rows.Count > n; n++)
            {
                DtGrid.Rows[n]["RCount"] = n + 1;
            }
            object sumObject = DtGrid.Compute("Sum(EstimatePrice)", string.Empty);
            if (sumObject.ToString() == "")
            { lblestimateprice.Text = "0"; }
            else
            {
                lblestimateprice.Text = sumObject.ToString();
            }
            object sumObject1 = DtGrid.Compute("Sum(EstimatePricefuture)", string.Empty);
            if (sumObject1.ToString() == "")
            { lblfuturepurchase.Text = "0"; }
            else
            { lblfuturepurchase.Text = sumObject1.ToString(); }
            Session["TempData"] = DtGrid;
            ViewState["ResetData"] = DtGrid;
            DtFilterView = (DataTable)Session["TempData"];
            UpdateDtGridValue(DtGrid);
            DataView dv4 = new DataView(DtGrid, "RCount >='" + n1 + "' And  RCount<='" + n2 + "'", "", DataViewRowState.CurrentRows);
            DataTable dtads4 = dv4.ToTable();
            pgsource.DataSource = DtGrid.DefaultView;
            pgsource.AllowPaging = true;
            pgsource.PageSize = 50;
            pgsource.CurrentPageIndex = pagingCurrentPage;
            ViewState["totpage"] = pgsource.PageCount;
            lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
            lbltotal.Text = "Filter/Search Results " + DtGrid.Rows.Count.ToString() + " items";
            lbltotalleft.Text = "Total items uploaded :-  " + DtGrid.Rows.Count.ToString();
            lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
            lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
            dlproduct.DataSource = dtads4.DefaultView;
            divcontentproduct.Visible = true;
            dlproduct.DataBind();
            SeachResult();
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
        pagingCurrentPage -= 1;
        n2 = Convert.ToInt16(pagingCurrentPage) * Convert.ToInt16(24);
        if (ddlcomp.SelectedItem.Text != "Select" || ddlnsg.SelectedItem.Text != "Select" || ddlprodindustrydomain.SelectedItem.Text != "Select" || ddlprocurmentcatgory.SelectedItem.Text != "Select" || txtsearch.Text != "")
        {
            n1 = 1;
        }
        else
        {
            n1 = Convert.ToInt16(n2 - 50);
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
        n2 = Convert.ToInt16(mcount) * Convert.ToInt16(50);
        if (ddlcomp.SelectedItem.Text != "Select" || ddlnsg.SelectedItem.Text != "Select" || ddlprodindustrydomain.SelectedItem.Text != "Select" || ddlprocurmentcatgory.SelectedItem.Text != "Select" || txtsearch.Text != "")
        {
            n1 = 1;
        }
        else
        {
            n1 = Convert.ToInt16(n2 - 49);
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
            n2 = Convert.ToInt16(pagingCurrentPage + 1) * Convert.ToInt16(50);
            if (ddlcomp.SelectedItem.Text != "Select" || ddlnsg.SelectedItem.Text != "Select" || ddlprodindustrydomain.SelectedItem.Text != "Select" || ddlprocurmentcatgory.SelectedItem.Text != "Select" || txtsearch.Text != "")
            {
                n1 = 1;
            }
            else
            {
                n1 = Convert.ToInt16(n2 + 1 - 50);
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
                // ddldivision.Items.Insert(0, "Select");
                divfilterdivision.Visible = false;
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
            else
            {
                divfilterunit.Visible = false;
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
        for (int i = 0; i < ddldeclaration.Items.Count; i++)
        {
            ddldeclaration.Items[i].Attributes.Add("onclick", "MutExChkList(this)");
        }
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
    protected void rbsort_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rberffpurchase.SelectedIndex != -1 || chklast5year.SelectedIndex != -1 || chktendor.SelectedIndex != -1 || ddlcomp.SelectedIndex != 0 || ddlnsg.SelectedIndex != 0 || ddlprodindustrydomain.SelectedIndex != 0 || ddlprocurmentcatgory.SelectedIndex != 0 || ddlisindezinized.SelectedIndex != -1 || ddldeclaration.SelectedIndex != -1)
        { BindProduct(); }
        else
        {
            SeachResult();
        }
    }
    protected void chkimportvalue_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    #endregion
    #region Search Code Filter Code
    string insert1 = "";
    string chkproofcat = "";
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
            dr = insert.NewRow();
            foreach (ListItem item in chktendor.Items)
            {
                if (item.Selected == true)
                {
                    chkproofcat = chkproofcat + item.Value + ",";
                }
            }
            dr["Column"] = "PurposeofProcurement" + " like";
            dr["Value"] = "'%" + chkproofcat.Substring(0, chkproofcat.Length - 1) + "%'";
            insert.Rows.Add(dr);
            //else
            //{
            //    dr = insert.NewRow();
            //    dr["Column"] = "(TenderStatus " + "=";
            //    dr["Value"] = "'Archive' and EOIStatus ='Archive') or (TenderStatus='Not Floated' and  EOIStatus='Not Floated') or (TenderStatus='To be Floated shortly' and  EOIStatus='To be Floated shortly')";
            //    insert.Rows.Add(dr);
            //}
        }
        if (rberffpurchase.SelectedIndex != -1)
        {
            if (rberffpurchase.SelectedItem.Value != "0")
            {
                dr = insert.NewRow();
                dr["Column"] = "EstiPriMultiF" + ">";
                dr["Value"] = "'0'";
                insert.Rows.Add(dr);
            }
            else
            {
                dr = insert.NewRow();
                dr["Column"] = "EstiPriMultiF" + "=";
                dr["Value"] = "'0'";
                insert.Rows.Add(dr);
            }
        }
        if (chkimportvalue.SelectedIndex != -1)
        {
            if (chkimportvalue.SelectedItem.Value == "2")
            {
                dr = insert.NewRow();
                dr["Column"] = "(" + rbsort.SelectedValue + " >=";
                dr["Value"] = " '10'  and " + rbsort.SelectedValue + " < '50' )";
                insert.Rows.Add(dr);

            }
            else if (chkimportvalue.SelectedItem.Value == "1")
            {
                dr = insert.NewRow();
                dr["Column"] = "(" + rbsort.SelectedValue + " >=";
                dr["Value"] = " '5'  and " + rbsort.SelectedValue + " < '10')";
                insert.Rows.Add(dr);

            }
            else if (chkimportvalue.SelectedItem.Value == "3")
            {
                dr = insert.NewRow();
                dr["Column"] = "(" + rbsort.SelectedValue + "  >";
                dr["Value"] = "'50')";
                insert.Rows.Add(dr);
            }
            else if (chkimportvalue.SelectedItem.Value == "4")
            {
                dr = insert.NewRow();
                dr["Column"] = "(" + rbsort.SelectedValue + " >=";
                dr["Value"] = " '1'  and " + rbsort.SelectedValue + " < '5')";
                insert.Rows.Add(dr);
            }
        }
        //if (ddlprocurmentcatgory.SelectedItem.Text != "Select")
        //{
        //    dr = insert.NewRow();
        //    dr["Column"] = "PurposeofProcurement" + " like";
        //    dr["Value"] = "'%" + ddlprocurmentcatgory.SelectedItem.Value + "%'";
        //    insert.Rows.Add(dr);
        //}
        //if (ddldeclaration.SelectedIndex != -1)
        //{
        //    dr = insert.NewRow();
        //    dr["Column"] = "IsShowGeneral" + "="; ;
        //    dr["Value"] = "'" + ddldeclaration.SelectedItem.Value + "'";
        //    insert.Rows.Add(dr);
        //}
        if (rbsort.SelectedIndex != -1)
        {
            if (rbsort.SelectedValue == "EstimatePrice")
            {
                dr = insert.NewRow();
                //  dr["Column"] = "EstimatePrice" + " >"; ;
                //  dr["Value"] = "'50' and ImportYear = '2019-20'";
                dr["Column"] = "ImportYear " + "= ";
                dr["Value"] = "'2019-20'";
                insert.Rows.Add(dr);
            }
            else
            {
                dr = insert.NewRow();
                //   dr["Column"] = "EstimatePricefuture" + " > "; ;
                //   dr["Value"] = "'50' and ImportFutureYear = '2020-21'";
                dr["Column"] = "ImportFutureYear" + " = "; ;
                dr["Value"] = "'2020-21'";

                insert.Rows.Add(dr);
            }
        }
        if (txtsearch.Text.Trim() != "" && txtsearch.Text.Trim().Length >= 3)
        {
            dr = insert.NewRow();
            dr["Column"] = "((ProductRefNo like";
            dr["Value"] = "'" + txtsearch.Text.Trim() + "%') or (CompanyName like '" + txtsearch.Text.Trim() + "%') or (UnitName like '" + txtsearch.Text.Trim() + "%') or (FactoryName like '" + txtsearch.Text.Trim() + "%') or (NSCCode like '" + txtsearch.Text.Trim() + "%') or (ProductDescription like '" + txtsearch.Text.Trim() + "%') or (NSNGroup like '" + txtsearch.Text.Trim() + "%') or (DefencePlatform  like '" + txtsearch.Text.Trim() + "%') or (ProdIndustryDoamin like '" + txtsearch.Text.Trim() + "%')  or (NSNGroupClass like '" + txtsearch.Text.Trim() + "%')  or  (ItemCode  like '" + txtsearch.Text.Trim() + "%')  or (ProdIndustrySubDomain like '" + txtsearch.Text.Trim() + "%') or (TopPdf like '" + txtsearch.Text.Trim() + "%')  or (TopImages like '" + txtsearch.Text.Trim() + "%')  or (HSNCode like '" + txtsearch.Text.Trim() + "%') or (Convert(EstimateQu, 'System.String') LIKE '" + txtsearch.Text + "%') or (Convert(EstimatePrice, 'System.String') LIKE '" + txtsearch.Text + "%'))";
            insert.Rows.Add(dr);
        }
        //if (ddlisindezinized.SelectedIndex != -1)
        //{
        //    dr = insert.NewRow();
        //    dr["Column"] = "IsIndeginized" + "="; ;
        //    dr["Value"] = "'" + ddlisindezinized.SelectedItem.Value + "'";
        //    insert.Rows.Add(dr);
        //}
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
                    DataTable dtinner = dv.ToTable();
                    if (dtinner.Rows.Count > 0)
                    {
                        if (rbsort.SelectedIndex != -1)
                        {
                            dv.Sort = rbsort.SelectedItem.Value + " " + "desc";
                        }
                        else
                        {
                            dv.Sort = "EstiPriMultiF desc";
                        }
                        if (rbsort.SelectedItem.Text.Trim() == "2019-20")
                        {
                            object sumObject = dtinner.Compute("Sum(EstimatePrice)", string.Empty);
                            if (sumObject.ToString() == "")
                            {
                                lblyearvalue.Text = rbsort.SelectedItem.Text;
                                lblestimateprice.Text = "0";
                            }
                            else
                            {
                                lblyearvalue.Text = rbsort.SelectedItem.Text;
                                // double Sumvalue = Convert.ToDouble(sumObject.ToString());
                                lblestimateprice.Text = sumObject.ToString();
                            }
                        }
                        else
                        {
                            object sumObject1 = dtinner.Compute("Sum(EstimatePricefuture)", string.Empty);
                            if (sumObject1.ToString() == "")
                            {
                                lblyearvalue.Text = rbsort.SelectedItem.Text;
                                // lblfuturepurchase.Text = "0";
                                lblestimateprice.Text = "0";
                            }
                            else
                            {
                                lblyearvalue.Text = rbsort.SelectedItem.Text;
                                // lblfuturepurchase.Text = sumObject1.ToString();
                                // double Sumvalue1 = Convert.ToDouble(sumObject1.ToString());
                                lblestimateprice.Text = sumObject1.ToString();
                            }
                        }
                    }
                    else
                    {
                        lblfuturepurchase.Text = "0"; lblestimateprice.Text = "0";
                    }
                    lbltotal.Text = "Filter/Search Results " + dtinner.Rows.Count.ToString() + " items";
                    DataTable dtads = dv.ToTable();
                    dtads.Columns.Add("FCount", typeof(Int64));
                    for (int n = 0; dtads.Rows.Count > n; n++)
                    {
                        dtads.Rows[n]["FCount"] = n + 1;
                    }
                    DataView dv4 = new DataView(dtads, "FCount >='" + n1 + "' And  FCount<='" + n2 + "'", "", DataViewRowState.CurrentRows);
                    dtads = dv4.ToTable();
                    if (dtads.Rows.Count > 0)
                    {
                        pgsource.DataSource = dtinner.DefaultView;
                        pgsource.AllowPaging = true;
                        pgsource.PageSize = 50;
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
            try
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
                    lblclassitem.Text = DtView.Rows[0]["ProdLevel3Name"].ToString();
                    lblsearchkeywords.Text = DtView.Rows[0]["Searchkeyword"].ToString();
                    lblproductdescription.Text = DtView.Rows[0]["ProductDescription"].ToString();
                    lbldpsupartno.Text = DtView.Rows[0]["DPSUPartNumber"].ToString();
                    lblhsncode8digit.Text = DtView.Rows[0]["HsnCode8digit"].ToString();
                    prodIndustryDomain.Text = DtView.Rows[0]["TechLevel1Name"].ToString();
                    ProdIndusSubDomain.Text = DtView.Rows[0]["Techlevel2Name"].ToString();
                    lblisproductimported.Text = DtView.Rows[0]["IsProductImported"].ToString();
                    lblnsccode4digit.Text = DtView.Rows[0]["NSCCode"].ToString();
                    if (lblisproductimported.Text == "Y")
                    {
                        lblisproductimported.Text = "Yes";
                    }
                    else { lblisproductimported.Text = "No"; }
                    DataTable DtGridEstimate1 = new DataTable();
                    DtGridEstimate1 = Lo.RetriveSaveEstimateGrid("Select", 0, e.CommandArgument.ToString(), 0, "", "", "", "", "O");
                    if (DtGridEstimate1.Rows.Count > 0)
                    {
                        decimal tot = 0;
                        // int qtyimp = 0;
                        for (int i = 0; DtGridEstimate1.Rows.Count > i; i++)
                        {
                            // if (DtGridEstimate1.Rows[i]["FYear"].ToString() == "2019-20")
                            //  { 
                            tot = tot + Convert.ToDecimal(DtGridEstimate1.Rows[i]["EstimatedPrice"]);
                            // qtyimp = qtyimp + Convert.ToInt16(DtGridEstimate1.Rows[i]["EstimatedQty"]);
                            //  }
                        }
                        gvestimatequanold.DataSource = DtGridEstimate1;
                        gvestimatequanold.DataBind();
                        gvestimatequanold.Visible = true;
                        decimal msumobject = tot; //* qtyimp / 100000;
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
                    DataTable dtestimatequanorprice = Lo.RetriveSaveEstimateGrid("2Select", 0, e.CommandArgument.ToString(), 0, "", "", "", "", "F");
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
                    lblindicate.Text = "";
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
                            lblempname.Text = dtNodal.Rows[0]["NodalOficerName"].ToString();
                            lbldesignation.Text = dtNodal.Rows[0]["Designation"].ToString();
                            lblemailidpro.Text = dtNodal.Rows[0]["NodalOfficerEmail"].ToString();
                            lblmobilenumber.Text = dtNodal.Rows[0]["NodalOfficerMobile"].ToString();
                            lblphonenumber.Text = dtNodal.Rows[0]["NodalOfficerTelephone"].ToString();
                            lblfaxpro.Text = dtNodal.Rows[0]["NodalOfficerFax"].ToString();
                        }
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "ProductCompany", "showPopup();", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
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
            Label lblepold = (Label)e.Item.FindControl("lblepold");
            Label lblepfu = (Label)e.Item.FindControl("lblepfu");
            Label lbleq = (Label)e.Item.FindControl("Label2");
            Label lbleqf = (Label)e.Item.FindControl("Label3");
            Label lbleuold = (Label)e.Item.FindControl("lblestunitold");
            Label lbleufutu = (Label)e.Item.FindControl("lblestunitfut");

            if (rbsort.SelectedItem.Text.Trim() == "2019-20")
            {
                lblepold.Visible = true;
                lbleq.Visible = true;
                //  lbleuold.Visible = true;
                lblepfu.Visible = false;
                lbleqf.Visible = false;
                //  lbleufutu.Visible = false;

            }
            else if (rbsort.SelectedItem.Text.Trim() == "2020-21")
            {
                lblepold.Visible = false;
                lbleq.Visible = false;
                //  lbleuold.Visible = false;
                lblepfu.Visible = true;
                lbleqf.Visible = true;
                // lbleufutu.Visible = true;
            }
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
        cleartext();
        Div1.Visible = false;
        // divhiddensearch.Visible = false;
        SeachResult();
    }
    #region AutoComplete Serach Box
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetSearchKeyword(string prefix)
    {
        Cryptography objCrypto1 = new Cryptography();
        List<string> customers = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = objCrypto1.DecryptData(ConfigurationManager.ConnectionStrings["connectiondb"].ConnectionString);
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select top 100000 ProductRefNo from tbl_mst_MainProduct  where ProductRefNo like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefix);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}", sdr["ProductRefNo"], sdr["ProductRefNo"]));
                    }
                }
                cmd.CommandText = "select distinct CompanyName from tbl_mst_Company where CompanyName like @SearchText + '%'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}", sdr["CompanyName"], sdr["CompanyName"]));
                    }
                }
                cmd.CommandText = "select distinct FactoryName from tbl_mst_Factory where FactoryName like @SearchText + '%'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}", sdr["FactoryName"], sdr["FactoryName"]));
                    }
                }

                cmd.CommandText = "select distinct UnitName from tbl_mst_Unit where UnitName like @SearchText + '%'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}", sdr["UnitName"], sdr["UnitName"]));
                    }
                }
                cmd.CommandText = "select distinct NSCCode from tbl_mst_MainProduct where NSCCode like @SearchText + '%'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}", sdr["NSCCode"], sdr["NSCCode"]));
                    }
                }
                cmd.CommandText = "select distinct ProductDescription from tbl_Mst_MainProduct where ProductDescription like @SearchText + '%'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}", sdr["ProductDescription"], sdr["ProductDescription"]));
                    }
                }
                cmd.CommandText = "select distinct NSNGroup from Vw_MasteRecord where NSNGroup like @SearchText + '%'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}", sdr["NSNGroup"], sdr["NSNGroup"]));
                    }
                }
                cmd.CommandText = "select distinct DefencePlatform from Vw_MasteRecord where DefencePlatform like @SearchText + '%'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}", sdr["DefencePlatform"], sdr["DefencePlatform"]));
                    }
                }
                cmd.CommandText = "select distinct ProdIndustryDoamin from Vw_MasteRecord where ProdIndustryDoamin like @SearchText + '%'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}", sdr["ProdIndustryDoamin"], sdr["ProdIndustryDoamin"]));
                    }
                }
                cmd.CommandText = "select distinct NSNGroupClass from Vw_MasteRecord where NSNGroupClass like @SearchText + '%'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}", sdr["NSNGroupClass"], sdr["NSNGroupClass"]));
                    }
                }
                cmd.CommandText = "select distinct ItemCode from Vw_MasteRecord where ItemCode like @SearchText + '%'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}", sdr["ItemCode"], sdr["ItemCode"]));
                    }
                }
                cmd.CommandText = "select distinct ProdIndustrySubDomain from Vw_MasteRecord where ProdIndustrySubDomain like @SearchText + '%'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}", sdr["ProdIndustrySubDomain"], sdr["ProdIndustrySubDomain"]));
                    }
                }
                cmd.CommandText = "select distinct ImageName from tbl_trn_Image where ImageName like @SearchText + '%' and FType='Image'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}", sdr["ImageName"], sdr["ImageName"]));
                    }
                }
                cmd.CommandText = "select  distinct ImageName from tbl_trn_Image where ImageName like @SearchText + '%' and FType='PDF'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}", sdr["ImageName"], sdr["ImageName"]));
                    }
                }
                cmd.CommandText = "select distinct HSNCode from tbl_mst_MainProduct where HSNCode like @SearchText + '%'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}", sdr["HSNCode"], sdr["HSNCode"]));
                    }
                }
                cmd.CommandText = "select distinct EstimatedQty from tbl_trn_ProdQtyPrice where EstimatedQty like @SearchText + '%'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}", sdr["EstimatedQty"], sdr["EstimatedQty"]));
                    }
                }
                cmd.CommandText = "select distinct EstimatedPrice from tbl_trn_ProdQtyPrice where EstimatedPrice like @SearchText + '%'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}", sdr["EstimatedPrice"], sdr["EstimatedPrice"]));
                    }
                }
                conn.Close();
            }
        }
        return customers.ToArray();
    }
    #endregion
    protected void lbadvancesearch_Click(object sender, EventArgs e)
    {
        // divhiddensearch.Visible = true;
    }
    protected void cleartext()
    {
        txtsearch.Text = "";
        ddlcomp.SelectedIndex = -1;
        if (ddldivision.Visible == true)
        {
            divfilterdivision.Visible = false;
        }
        if (ddlunit.Visible == true)
        {
            divfilterunit.Visible = false;
        }
        if (ddlnsc.Visible == true)
        {
            divnsc.Visible = false;
        }
        if (ddlic.Visible == true)
        {
            divic.Visible = false;
        }
        if (ddlindustrysubdoamin.Visible == true)
        {
            divisd.Visible = false;
        }
        chktendor.SelectedIndex = -1;
        ddlnsg.SelectedIndex = -1;
        ddlprodindustrydomain.SelectedIndex = -1;
        chkimportvalue.SelectedValue = "1";
        rbsort.SelectedValue = "EstimatePrice";
    }
}