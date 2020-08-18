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
using System.Web;
using System.Linq;
using System.Web.Services;
using Microsoft.Ajax.Utilities;
using Microsoft.SqlServer.Server;

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
        ddlcomp.Items.Insert(0, "Select");
        ddlnsg.Items.Insert(0, "Select");
        ddlprodindustrydomain.Items.Insert(0, "Select");
        BindProduct();
        BindComapnyCheckbox();
        BindNSG();
        BindIndusrtyDomain();
        BindIndiCategory();
    }
    protected void BindProduct()
    {
        DtGrid = Lo.RetriveProductUser();
        if (DtGrid.Rows.Count > 0)
        {
            Session["TempData"] = DtGrid;
            SeachResult();
        }
        else
        {
            divcontentproduct.Visible = false;
            dlproduct.DataBind();
        }
    }
    #endregion
    #region  DDL Fill
    DataTable DtCompanyDDL = new DataTable();
    protected void BindComapnyCheckbox()
    {
        if (DtGrid.Rows.Count > 0)
        {
            DataTable mDtGrid = DtGrid.DefaultView.ToTable(true, "CompanyName", "CompanyRefNo");
            mDtGrid.DefaultView.Sort = "CompanyName asc";
            Co.FillDropdownlist(ddlcomp, mDtGrid, "CompanyName", "CompanyRefNo");
            ddlcomp.Items.Insert(0, "Select");
            ddlcomp.Enabled = true;
        }
        else
        {
            ddlcomp.Enabled = false;
        }
    }
    protected void BindComapnyDivisionCheckbox()
    {
        try
        {
            if (Session["TempData"] != null)
            {
                DtGrid = (DataTable)Session["TempData"];
                DataTable mDtGrid = DtGrid.DefaultView.ToTable(true, "FactoryName", "FactoryRefNo", "CompanyRefNo");
                mDtGrid.DefaultView.Sort = "FactoryName asc";
                DataView dvm = new DataView(mDtGrid);
                dvm.RowFilter = "CompanyRefNo='" + ddlcomp.SelectedItem.Value + "'";
                Co.FillDropdownlist(ddldivision, dvm.ToTable(), "FactoryName", "FactoryRefNo");
                ddldivision.Items.Insert(0, "Select");
                divfilterdivision.Visible = true;
            }
            else
            {
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
            if (Session["TempData"] != null)
            {
                DtGrid = (DataTable)Session["TempData"];
                DataTable mDtGrid = DtGrid.DefaultView.ToTable(true, "UnitName", "UnitRefNo", "FactoryRefNo");
                mDtGrid.DefaultView.Sort = "UnitName asc";
                DataView dvm = new DataView(mDtGrid);
                dvm.RowFilter = "FactoryRefNo='" + ddldivision.SelectedItem.Value + "'";
                Co.FillDropdownlist(ddlunit, dvm.ToTable(), "UnitName", "UnitRefNo");
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

            if (ddlcomp.SelectedItem.Text != "Select")
            {
                if (Session["TempData"] != null)
                {
                    DtGrid = (DataTable)Session["TempData"];
                    DataTable mDtGrid = DtGrid.DefaultView.ToTable(true, "ProdIndustryDoamin", "TechnologyLevel1", "CompanyRefNo");
                    mDtGrid.DefaultView.Sort = "ProdIndustryDoamin asc";
                    DataView dvm = new DataView(mDtGrid);
                    dvm.RowFilter = "CompanyRefNo='" + ddlcomp.SelectedItem.Value + "'";
                    Co.FillDropdownlist(ddlprodindustrydomain, dvm.ToTable(), "ProdIndustryDoamin", "TechnologyLevel1");
                    ddlprodindustrydomain.Items.Insert(0, "Select");
                    Div7.Visible = true;
                }
                else
                {
                    Div7.Visible = false;
                    divisd.Visible = false;
                }
            }
            else
            {
                if (DtGrid.Rows.Count > 0)
                {
                    DataTable mDtGrid = DtGrid.DefaultView.ToTable(true, "ProdIndustryDoamin", "TechnologyLevel1");
                    mDtGrid.DefaultView.Sort = "ProdIndustryDoamin asc";
                    Co.FillDropdownlist(ddlprodindustrydomain, mDtGrid, "ProdIndustryDoamin", "TechnologyLevel1");
                    ddlprodindustrydomain.Items.Insert(0, "Select");
                    Div7.Visible = true;
                }
                else
                {
                    Div7.Visible = false;
                    divisd.Visible = false;
                }
            }
        }
        catch (Exception ex)
        { }
    }
    protected void BindIndustrySubDomain()
    {
        if (ddlprodindustrydomain.SelectedItem.Text != "Select")
        {
            if (Session["TempData"] != null)
            {
                DtGrid = (DataTable)Session["TempData"];
                DataTable mDtGrid = DtGrid.DefaultView.ToTable(true, "ProdIndustrySubDomain", "TechnologyLevel2", "TechnologyLevel1");
                mDtGrid.DefaultView.Sort = "ProdIndustrySubDomain asc";
                DataView dvm = new DataView(mDtGrid);
                dvm.RowFilter = "TechnologyLevel1='" + ddlprodindustrydomain.SelectedItem.Value + "'";
                Co.FillDropdownlist(ddlindustrysubdoamin, dvm.ToTable(), "ProdIndustrySubDomain", "TechnologyLevel2");
                ddlindustrysubdoamin.Items.Insert(0, "Select");
                divisd.Visible = true;
            }
            else
            {
                divisd.Visible = false;
            }
        }
        else
        {
            divisd.Visible = false;
        }
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
        if (ddlcomp.SelectedItem.Text != "Select")
        {
            if (Session["TempData"] != null)
            {
                DtGrid = (DataTable)Session["TempData"];
                DataTable mDtGrid = DtGrid.DefaultView.ToTable(true, "NSNGROUP", "ProductLevel1", "CompanyRefNo");
                mDtGrid.DefaultView.Sort = "NSNGROUP asc";
                DataView dvm = new DataView(mDtGrid);
                dvm.RowFilter = "CompanyRefNo='" + ddlcomp.SelectedItem.Value + "'";
                Co.FillDropdownlist(ddlnsg, dvm.ToTable(), "NSNGROUP", "ProductLevel1");
                ddlnsg.Items.Insert(0, "Select");
            }
            else
            {
                ddlnsg.Visible = false;
                divnsc.Visible = false;
                divic.Visible = false;
            }
        }
        else
        {
            if (DtGrid.Rows.Count > 0)
            {
                DataTable mDtGrid = DtGrid.DefaultView.ToTable(true, "NSNGROUP", "ProductLevel1");
                mDtGrid.DefaultView.Sort = "NSNGROUP asc";
                Co.FillDropdownlist(ddlnsg, mDtGrid, "NSNGROUP", "ProductLevel1");
                ddlnsg.Items.Insert(0, "Select");
            }
            else
            {
                ddlnsg.Visible = false;
                divnsc.Visible = false;
                divic.Visible = false;
            }
        }
    }
    protected void BindNSC()
    {
        if (ddlnsg.SelectedIndex != -1 || ddlnsg.SelectedItem.Text != "Select")
        {
            if (Session["TempData"] != null)
            {
                DtGrid = (DataTable)Session["TempData"];
                DataTable mDtGrid = DtGrid.DefaultView.ToTable(true, "NSNGroupClass", "ProductLevel2", "ProductLevel1");
                mDtGrid.DefaultView.Sort = "NSNGROUPClass asc";
                DataView dvm = new DataView(mDtGrid);
                dvm.RowFilter = "ProductLevel1='" + ddlnsg.SelectedItem.Value + "'";
                Co.FillDropdownlist(ddlnsc, dvm.ToTable(), "NSNGroupClass", "ProductLevel2");
                ddlnsc.Items.Insert(0, "Select");
                divnsc.Visible = true;
            }
            else
            {
                divic.Visible = false;
                divnsc.Visible = false;
            }
        }
    }
    protected void BindIC()
    {
        if (ddlnsc.SelectedIndex != -1 || ddlnsc.SelectedItem.Text != "Select")
        {
            if (Session["TempData"] != null)
            {
                DtGrid = (DataTable)Session["TempData"];
                DataTable mDtGrid = DtGrid.DefaultView.ToTable(true, "ItemCode", "ProductLevel3", "ProductLevel2");
                mDtGrid.DefaultView.Sort = "ItemCode asc";
                DataView dvm = new DataView(mDtGrid);
                dvm.RowFilter = "ProductLevel2='" + ddlnsc.SelectedItem.Value + "'";
                Co.FillDropdownlist(ddlic, dvm.ToTable(), "ItemCode", "ProductLevel3");
                ddlic.Items.Insert(0, "Select");
                divic.Visible = true;
            }
            else
            {
                ddlic.Items.Insert(0, "Select");
            }
        }
    }
    protected void BindIndiCategory()
    {
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "PROCURMENT CATEGORY", "", "SelectProductCat", "", "");
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillRadioBoxList(chktendor, DtMasterCategroy, "SCategoryName", "SCategoryID");
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
        if (ddlcomp.SelectedItem.Text != "Select")
        {
            BindComapnyDivisionCheckbox();
            SeachResult();
        }
        else
        {
            divfilterdivision.Visible = false;
            divfilterunit.Visible = false;
            SeachResult();

        }
    }
    protected void ddldivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldivision.SelectedItem.Text != "Select")
        {
            BindComapnyDivisionUnitCheckbox();
            SeachResult();
        }
        else
        {
            SeachResult();
            divfilterunit.Visible = false;
        }
    }
    protected void ddlunit_SelectedIndexChanged(object sender, EventArgs e)
    {
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
        if (ddlnsg.SelectedItem.Text != "Select")
        {
            BindNSC();
            SeachResult();
        }
        else
        {
            divnsc.Visible = false;
            divic.Visible = false;
            SeachResult();

        }
    }
    protected void ddlnsc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlnsc.SelectedItem.Text != "Select")
        {
            BindIC();
            SeachResult();
        }
        else
        {
            SeachResult();
            divic.Visible = false;
        }
    }
    protected void ddlic_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
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
        //if (rberffpurchase.SelectedIndex != -1 || chklast5year.SelectedIndex != -1 || chktendor.SelectedIndex != -1 || ddlcomp.SelectedIndex != 0 || ddlnsg.SelectedIndex != 0 || ddlprodindustrydomain.SelectedIndex != 0 || ddlprocurmentcatgory.SelectedIndex != 0 || ddlisindezinized.SelectedIndex != -1 || ddldeclaration.SelectedIndex != -1)
        //{ BindProduct(); }
        //else
        //{
        SeachResult();
        //}
    }
    protected void chkimportvalue_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    #endregion
    #region Search Code Filter Code
    string insert1 = "";
    string chkproofcat = "";
    protected string Dvinsert(string sortExpression = null)
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
                dr["Value"] = " '0.5'  and " + rbsort.SelectedValue + " < '5')";
                insert.Rows.Add(dr);
            }
        }
        if (rbsort.SelectedValue == "EstimatePrice")
        {
            dr = insert.NewRow();
            dr["Column"] = "ImportYear " + "= ";
            if (chkimportvalue.SelectedIndex == -1)
            {
                dr["Value"] = "'2019-20' and EstimatePrice >= 0.5 ";
            }
            else
            {
                dr["Value"] = "'2019-20'";
            }
            insert.Rows.Add(dr);
        }
        else if (rbsort.SelectedValue == "EstimatePrice18")
        {
            dr = insert.NewRow();
            dr["Column"] = "ImportYear18 " + "= ";
            if (chkimportvalue.SelectedIndex == -1)
            {
                dr["Value"] = "'2018-19' and EstimatePrice18 >= 0.5 ";
            }
            else
            {
                dr["Value"] = "'2018-19'";
            }
            insert.Rows.Add(dr);
        }
        else if (rbsort.SelectedValue == "EstimatePrice17")
        {
            dr = insert.NewRow();
            dr["Column"] = "ImportYear17 " + "= ";
            if (chkimportvalue.SelectedIndex == -1)
            {
                dr["Value"] = "'2017-18' and EstimatePrice17 >= 0.5 ";
            }
            else
            {
                dr["Value"] = "'2017-18'";
            }
            insert.Rows.Add(dr);
        }
        else if (rbsort.SelectedValue == "EstimatePricefuture")
        {
            dr = insert.NewRow();
            dr["Column"] = "ImportFutureYear" + " = ";
            if (chkimportvalue.SelectedIndex == -1)
            {
                dr["Value"] = "'2020-21' and EstimatePricefuture >= 0.5 ";
            }
            else
            {
                dr["Value"] = "'2020-21'";
            }
            insert.Rows.Add(dr);
        }
        if (ddldeclaration.SelectedIndex != -1)
        {
            dr = insert.NewRow();
            dr["Column"] = "IsShowGeneral =";
            dr["Value"] = "'" + ddldeclaration.SelectedItem.Value + "'";
            insert.Rows.Add(dr);
        }
        if (chkeoistatus.SelectedIndex != -1)
        {
            dr = insert.NewRow();
            dr["Column"] = "EOIStatus =";
            dr["Value"] = "'" + chkeoistatus.SelectedItem.Value + "'";
            insert.Rows.Add(dr);
        }
        if (rbindigtarget.SelectedIndex != -1)
        {
            dr = insert.NewRow();
            dr["Column"] = "IndTargetYear  like ";
            dr["Value"] = "'%" + rbindigtarget.SelectedItem.Value.Trim() + "%'";
            insert.Rows.Add(dr);
        }
        if (txtsearch.Text.Trim() != "")
        {
            dr = insert.NewRow();
            dr["Column"] = "((ProductRefNo like";
            dr["Value"] = "'" + txtsearch.Text.Trim() + "%') or (CompanyName like '" + txtsearch.Text.Trim() + "%') or (UnitName like '" + txtsearch.Text.Trim() + "%') or (FactoryName like '" + txtsearch.Text.Trim() + "%') or (NSCCode like '" + txtsearch.Text.Trim() + "%') or (ProductDescription like '" + txtsearch.Text.Trim() + "%') or (NSNGroup like '" + txtsearch.Text.Trim() + "%') or (DefencePlatform  like '" + txtsearch.Text.Trim() + "%') or (ProdIndustryDoamin like '" + txtsearch.Text.Trim() + "%')  or (NSNGroupClass like '" + txtsearch.Text.Trim() + "%')  or  (ItemCode  like '" + txtsearch.Text.Trim() + "%')  or (ProdIndustrySubDomain like '" + txtsearch.Text.Trim() + "%') or (TopPdf like '" + txtsearch.Text.Trim() + "%')  or (TopImages like '" + txtsearch.Text.Trim() + "%')  or (HSNCode like '" + txtsearch.Text.Trim() + "%') or (Convert(EstimateQu, 'System.String') LIKE '" + txtsearch.Text + "%') or (Convert(EstimatePrice, 'System.String') LIKE '" + txtsearch.Text + "%') or (DPSUPartNumber like '" + txtsearch.Text.Trim() + "%') or (OEMName like '" + txtsearch.Text.Trim() + "%') or (OEMCountry like '" + txtsearch.Text.Trim() + "%'))";
            insert.Rows.Add(dr);
        }
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
                        if (rbsort.SelectedIndex != -1)
                        {
                            dv.Sort = rbsort.SelectedItem.Value + " " + "desc";
                        }
                        else
                        {
                            dv.Sort = "EstiPriMultiF desc";
                        }
                        if (rbsort.SelectedIndex != -1 && rbsort.SelectedItem.Text.Trim() == "2019-20")
                        {
                            object sumObject = dtinner.Compute("Sum(EstimatePrice)", string.Empty);
                            if (sumObject.ToString() == "")
                            {
                                lblyearvalue.Text = "Import Value during " + rbsort.SelectedItem.Text;
                                lblestimateprice.Text = "0";
                            }
                            else
                            {
                                lblyearvalue.Text = "Import Value during " + rbsort.SelectedItem.Text;
                                decimal d = Convert.ToDecimal(sumObject.ToString());
                                decimal m = Math.Round(d, 0);
                                lblestimateprice.Text = m.ToString();
                            }
                        }
                        else if (rbsort.SelectedIndex != -1 && rbsort.SelectedItem.Text.Trim() == "2018-19")
                        {
                            object sumObject = dtinner.Compute("Sum(EstimatePrice18)", string.Empty);
                            if (sumObject.ToString() == "")
                            {
                                lblyearvalue.Text = "Import Value during " + rbsort.SelectedItem.Text;
                                lblestimateprice.Text = "0";
                            }
                            else
                            {
                                lblyearvalue.Text = "Import Value during " + rbsort.SelectedItem.Text;
                                decimal d = Convert.ToDecimal(sumObject.ToString());
                                decimal m = Math.Round(d, 0);
                                lblestimateprice.Text = m.ToString();

                            }
                        }
                        else if (rbsort.SelectedIndex != -1 && rbsort.SelectedItem.Text.Trim() == "2017-18")
                        {
                            object sumObject = dtinner.Compute("Sum(EstimatePrice17)", string.Empty);
                            if (sumObject.ToString() == "")
                            {
                                lblyearvalue.Text = "Import Value during " + rbsort.SelectedItem.Text;
                                lblestimateprice.Text = "0";
                            }
                            else
                            {
                                lblyearvalue.Text = "Import Value during " + rbsort.SelectedItem.Text;
                                decimal d = Convert.ToDecimal(sumObject.ToString());
                                decimal m = Math.Round(d, 0);
                                lblestimateprice.Text = m.ToString();
                            }
                        }
                        else
                        {
                            if (rbsort.SelectedIndex != -1)
                            {
                                object sumObject1 = dtinner.Compute("Sum(EstimatePricefuture)", string.Empty);
                                if (sumObject1.ToString() == "")
                                {
                                    lblyearvalue.Text = "Estimated Import Value " + rbsort.SelectedItem.Text;
                                    lblestimateprice.Text = "0";
                                }
                                else
                                {
                                    lblyearvalue.Text = "Estimated Import Value " + rbsort.SelectedItem.Text;
                                    decimal d = Convert.ToDecimal(sumObject1.ToString());
                                    decimal m = Math.Round(d, 0);
                                    lblestimateprice.Text = m.ToString();
                                }
                            }
                        }
                    }
                    else
                    {
                        lblfuturepurchase.Text = "0"; lblestimateprice.Text = "0";
                    }
                    lbltotal.Text = "Filter/Search Results " + dtinner.Rows.Count.ToString() + " items";
                    lbltotfilter.Text = dtinner.Rows.Count.ToString();
                    DataTable dtads = dv.ToTable();
                    if (dtads.Rows.Count > 0)
                    {
                        pgsource.DataSource = dtinner.DefaultView;
                        pgsource.AllowPaging = true;
                        pgsource.PageSize = 48;
                        pgsource.CurrentPageIndex = pagingCurrentPage;
                        lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                        lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                        LinkButton1.Enabled = !pgsource.IsFirstPage;
                        lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                        LinkButton2.Enabled = !pgsource.IsLastPage;
                        pgsource.DataSource = dtads.DefaultView;
                        dlproduct.DataSource = pgsource;
                        dlproduct.DataBind();
                        lbltotalleft.Text = "Total Imported Items :-  " + DtFilterView.Rows.Count.ToString();
                        lbltotalshowpageitem.Text = pgsource.FirstIndexInPage + 1 + " - " + (pgsource.FirstIndexInPage + pgsource.Count);
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
                    //if (DtView.Rows[0]["IsShowGeneral"].ToString() != "")
                    //{
                    //    if (DtView.Rows[0]["IsShowGeneral"].ToString() == "Y")
                    //        lblisshowgeneral.Text = "Yes";
                    //    else
                    //        lblisshowgeneral.Text = "No";
                    //    twentyfour.Visible = true;
                    //}
                    //else
                    //{
                    //    twentyfour.Visible = false;
                    //}
                    //if (DtView.Rows[0]["TermConditionImage"].ToString() != "")
                    //{
                    //    twentythree.Visible = true;
                    //}
                    //else
                    //{
                    //    twentythree.Visible = false;
                    //}
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
                    //if (DtView.Rows[0]["IndProcess"].ToString() != "")
                    //{
                    //    lblprocstart.Text = DtView.Rows[0]["IndProcess"].ToString();
                    //    Tr24.Visible = true;
                    //}
                    //else
                    //{
                    //    Tr24.Visible = false;
                    //}
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
    #region oterscode
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
            Label lblepold17 = (Label)e.Item.FindControl("lblepold17");
            Label lblepold18 = (Label)e.Item.FindControl("lblepold18");
            Label lblepfu = (Label)e.Item.FindControl("lblepfu");
            Label lbleq = (Label)e.Item.FindControl("Label2");
            Label lbleq17 = (Label)e.Item.FindControl("Label7");
            Label lbleq18 = (Label)e.Item.FindControl("Label8");
            Label lbleqf = (Label)e.Item.FindControl("Label3");
            Label lbleuold = (Label)e.Item.FindControl("lblestunitold");
            Label lbleufutu = (Label)e.Item.FindControl("lblestunitfut");

            if (rbsort.SelectedIndex != -1 && rbsort.SelectedItem.Text.Trim() == "2019-20")
            {
                lblepold.Visible = true;
                lbleq.Visible = true;
                lblepfu.Visible = false;
                lbleqf.Visible = false;
                lblepold17.Visible = false;
                lblepold18.Visible = false;
                lbleq17.Visible = false;
                lbleq18.Visible = false;

            }
            else if (rbsort.SelectedIndex != -1 && rbsort.SelectedItem.Text.Trim() == "2018-19")
            {
                lblepold.Visible = false;
                lbleq.Visible = false;
                lblepfu.Visible = false;
                lbleqf.Visible = false;
                lblepold17.Visible = false;
                lblepold18.Visible = true;
                lbleq17.Visible = false;
                lbleq18.Visible = true;

            }
            else if (rbsort.SelectedIndex != -1 && rbsort.SelectedItem.Text.Trim() == "2017-18")
            {
                lblepold.Visible = false;
                lbleq.Visible = false;
                lblepfu.Visible = false;
                lbleqf.Visible = false;
                lblepold17.Visible = true;
                lblepold18.Visible = false;
                lbleq17.Visible = true;
                lbleq18.Visible = false;

            }
            else if (rbsort.SelectedIndex != -1 && rbsort.SelectedItem.Text.Trim() == "2020-21")
            {
                lblepold.Visible = false;
                lbleq.Visible = false;
                lblepfu.Visible = true;
                lbleqf.Visible = true;
                lblepold17.Visible = false;
                lblepold18.Visible = false;
            }
        }
    }
    protected void totoalmore_Click(object sender, EventArgs e)
    {
        FillProduct();
    }
    public void FillProduct()
    {
        DataTable dt = Lo.RetriveFilterCode("", "", "Updatetimeget");
        if (dt.Rows.Count != 0)
        {
            DateTime mdt = Convert.ToDateTime(dt.Rows[0]["UpdateDate"].ToString());
            atime.Text = mdt.ToString("dd-MMM-yyyy , hh:mm tt");
        }
        #region First Table
        DataTable dtProductDetail = Lo.RetriveProductIndig1("", "", "Gettotalprovalue20");
        if (dtProductDetail.Rows.Count > 0)
        {
            gvPrdoct.DataSource = dtProductDetail;
            gvPrdoct.DataBind();
            //0
            gvPrdoct.FooterRow.Cells[1].Text = "Total";
            gvPrdoct.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            object sumObjectn = dtProductDetail.Compute("Sum(TotalProd)", string.Empty);
            gvPrdoct.FooterRow.Cells[2].Text = sumObjectn.ToString();
            //1
            object sumObjectn1 = dtProductDetail.Compute("Sum(TotalPrice1920)", string.Empty);
            gvPrdoct.FooterRow.Cells[3].Text = sumObjectn1.ToString();
            //2
            object sumObjectn2 = dtProductDetail.Compute("Sum(ProdLess5)", string.Empty);
            gvPrdoct.FooterRow.Cells[4].Text = sumObjectn2.ToString();
            //3
            object sumObjectn3 = dtProductDetail.Compute("Sum(PriceLess5)", string.Empty);
            gvPrdoct.FooterRow.Cells[5].Text = sumObjectn3.ToString();
            //4
            object sumObjectn4 = dtProductDetail.Compute("Sum(ProdLess10)", string.Empty);
            gvPrdoct.FooterRow.Cells[6].Text = sumObjectn4.ToString();
            //5
            object sumObjectn5 = dtProductDetail.Compute("Sum(PriceLess10)", string.Empty);
            gvPrdoct.FooterRow.Cells[7].Text = sumObjectn5.ToString();
            //6
            object sumObjectn6 = dtProductDetail.Compute("Sum(ProdLess50)", string.Empty);
            gvPrdoct.FooterRow.Cells[8].Text = sumObjectn6.ToString();
            //7
            object sumObjectn7 = dtProductDetail.Compute("Sum(PriceLess50)", string.Empty);
            gvPrdoct.FooterRow.Cells[9].Text = sumObjectn7.ToString();
            //8
            object sumObjectn8 = dtProductDetail.Compute("Sum(ProdAbove50)", string.Empty);
            gvPrdoct.FooterRow.Cells[10].Text = sumObjectn8.ToString();
            //9
            object sumObjectn9 = dtProductDetail.Compute("Sum(PriceAbove50)", string.Empty);
            gvPrdoct.FooterRow.Cells[11].Text = sumObjectn9.ToString();
            //10
            object sumObjectn10 = dtProductDetail.Compute("Sum(TargetIndig2020)", string.Empty);
            gvPrdoct.FooterRow.Cells[12].Text = sumObjectn10.ToString();
        }
        else
        { }
        #endregion
        #region Second Table
        DataTable dtProductDetail1 = Lo.RetriveProductIndig1("", "", "Gettotalprovalue21");
        if (dtProductDetail.Rows.Count > 0)
        {
            DataList1.DataSource = dtProductDetail1;
            DataList1.DataBind();
            //0
            DataList1.FooterRow.Cells[1].Text = "Total";
            DataList1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            object sumObjectn = dtProductDetail1.Compute("Sum(TotalProd)", string.Empty);
            DataList1.FooterRow.Cells[2].Text = sumObjectn.ToString();
            //1
            object sumObjectn1 = dtProductDetail1.Compute("Sum(TotalPrice2021)", string.Empty);
            DataList1.FooterRow.Cells[3].Text = sumObjectn1.ToString();
            //2
            object sumObjectn2 = dtProductDetail1.Compute("Sum(ProdLess5)", string.Empty);
            DataList1.FooterRow.Cells[4].Text = sumObjectn2.ToString();
            //3
            object sumObjectn3 = dtProductDetail1.Compute("Sum(PriceLess5)", string.Empty);
            DataList1.FooterRow.Cells[5].Text = sumObjectn3.ToString();
            //4
            object sumObjectn4 = dtProductDetail1.Compute("Sum(ProdLess10)", string.Empty);
            DataList1.FooterRow.Cells[6].Text = sumObjectn4.ToString();
            //5
            object sumObjectn5 = dtProductDetail1.Compute("Sum(PriceLess10)", string.Empty);
            DataList1.FooterRow.Cells[7].Text = sumObjectn5.ToString();
            //6
            object sumObjectn6 = dtProductDetail1.Compute("Sum(ProdLess50)", string.Empty);
            DataList1.FooterRow.Cells[8].Text = sumObjectn6.ToString();
            //7
            object sumObjectn7 = dtProductDetail1.Compute("Sum(PriceLess50)", string.Empty);
            DataList1.FooterRow.Cells[9].Text = sumObjectn7.ToString();
            //8
            object sumObjectn8 = dtProductDetail1.Compute("Sum(ProdAbove50)", string.Empty);
            DataList1.FooterRow.Cells[10].Text = sumObjectn8.ToString();
            //9
            object sumObjectn9 = dtProductDetail1.Compute("Sum(PriceAbove50)", string.Empty);
            DataList1.FooterRow.Cells[11].Text = sumObjectn9.ToString();
            //10
            object sumObjectn10 = dtProductDetail1.Compute("Sum(TargetIndig2021)", string.Empty);
            DataList1.FooterRow.Cells[12].Text = sumObjectn10.ToString();
        }
        else
        { }
        #endregion
        #region Third Table
        DataTable dtProductDetail2 = Lo.RetriveProductIndig1("", "", "GettoalMake2019");
        if (dtProductDetail2.Rows.Count > 0)
        {
            GridView1.DataSource = dtProductDetail2;
            GridView1.DataBind();
            //0
            GridView1.FooterRow.Cells[1].Text = "Total";
            GridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            object sumObjectn = dtProductDetail2.Compute("Sum(TotalProd)", string.Empty);
            GridView1.FooterRow.Cells[2].Text = sumObjectn.ToString();
            //1
            object sumObjectn1 = dtProductDetail2.Compute("Sum(TotalPrice1920)", string.Empty);
            GridView1.FooterRow.Cells[3].Text = sumObjectn1.ToString();
            //2
            object sumObjectn2 = dtProductDetail2.Compute("Sum(MAKE2Total)", string.Empty);
            GridView1.FooterRow.Cells[4].Text = sumObjectn2.ToString();
            //3
            object sumObjectn3 = dtProductDetail2.Compute("Sum(MAKE2Price)", string.Empty);
            GridView1.FooterRow.Cells[5].Text = sumObjectn3.ToString();
            //4
            object sumObjectn4 = dtProductDetail2.Compute("Sum(IDExTotal)", string.Empty);
            GridView1.FooterRow.Cells[6].Text = sumObjectn4.ToString();
            //5
            object sumObjectn5 = dtProductDetail2.Compute("Sum(IDExPriceTotal)", string.Empty);
            GridView1.FooterRow.Cells[7].Text = sumObjectn5.ToString();
            //6
            object sumObjectn6 = dtProductDetail2.Compute("Sum(IGATotal)", string.Empty);
            GridView1.FooterRow.Cells[8].Text = sumObjectn6.ToString();
            //7
            object sumObjectn7 = dtProductDetail2.Compute("Sum(IGAPrice)", string.Empty);
            GridView1.FooterRow.Cells[9].Text = sumObjectn7.ToString();
            //8
            object sumObjectn8 = dtProductDetail2.Compute("Sum(OTHERTHANMAKE2Total)", string.Empty);
            GridView1.FooterRow.Cells[10].Text = sumObjectn8.ToString();
            //9
            object sumObjectn9 = dtProductDetail2.Compute("Sum(OTHERTHANMAKE2Price)", string.Empty);
            GridView1.FooterRow.Cells[11].Text = sumObjectn9.ToString();
            //10
            object sumObjectn10 = dtProductDetail2.Compute("Sum(HOUSETotal)", string.Empty);
            GridView1.FooterRow.Cells[12].Text = sumObjectn10.ToString();
            //11
            object sumObjectn11 = dtProductDetail2.Compute("Sum(HOUSEPrice)", string.Empty);
            GridView1.FooterRow.Cells[13].Text = sumObjectn11.ToString();
        }
        else
        { }
        #endregion
        #region FOurth Table
        DataTable dtProductDetail3 = Lo.RetriveProductIndig1("", "", "GettoalMake2021");
        if (dtProductDetail3.Rows.Count > 0)
        {
            GridView2.DataSource = dtProductDetail3;
            GridView2.DataBind();
            //0
            GridView2.FooterRow.Cells[1].Text = "Total";
            GridView2.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            object sumObjectn = dtProductDetail3.Compute("Sum(TotalProd)", string.Empty);
            GridView2.FooterRow.Cells[2].Text = sumObjectn.ToString();
            //1
            object sumObjectn1 = dtProductDetail3.Compute("Sum(TotalPrice2021)", string.Empty);
            GridView2.FooterRow.Cells[3].Text = sumObjectn1.ToString();
            //2
            object sumObjectn2 = dtProductDetail3.Compute("Sum(MAKE2Total)", string.Empty);
            GridView2.FooterRow.Cells[4].Text = sumObjectn2.ToString();
            //3
            object sumObjectn3 = dtProductDetail3.Compute("Sum(MAKE2Price)", string.Empty);
            GridView2.FooterRow.Cells[5].Text = sumObjectn3.ToString();
            //4
            object sumObjectn4 = dtProductDetail3.Compute("Sum(IDExTotal)", string.Empty);
            GridView2.FooterRow.Cells[6].Text = sumObjectn4.ToString();
            //5
            object sumObjectn5 = dtProductDetail3.Compute("Sum(IDExPriceTotal)", string.Empty);
            GridView2.FooterRow.Cells[7].Text = sumObjectn5.ToString();
            //6
            object sumObjectn6 = dtProductDetail3.Compute("Sum(IGATotal)", string.Empty);
            GridView2.FooterRow.Cells[8].Text = sumObjectn6.ToString();
            //7
            object sumObjectn7 = dtProductDetail3.Compute("Sum(IGAPrice)", string.Empty);
            GridView2.FooterRow.Cells[9].Text = sumObjectn7.ToString();
            //8
            object sumObjectn8 = dtProductDetail3.Compute("Sum(OTHERTHANMAKE2Total)", string.Empty);
            GridView2.FooterRow.Cells[10].Text = sumObjectn8.ToString();
            //9
            object sumObjectn9 = dtProductDetail3.Compute("Sum(OTHERTHANMAKE2Price)", string.Empty);
            GridView2.FooterRow.Cells[11].Text = sumObjectn9.ToString();
            //10
            object sumObjectn10 = dtProductDetail3.Compute("Sum(HOUSETotal)", string.Empty);
            GridView2.FooterRow.Cells[12].Text = sumObjectn10.ToString();
            //11
            object sumObjectn11 = dtProductDetail3.Compute("Sum(HOUSEPrice)", string.Empty);
            GridView2.FooterRow.Cells[13].Text = sumObjectn11.ToString();
        }
        else
        { }
        #endregion
        #region Fifth Table
        DataTable dtProductDetail4 = Lo.RetriveProductIndig1("", "", "GetIndigTarAll");
        if (dtProductDetail4.Rows.Count > 0)
        {
            GridView3.DataSource = dtProductDetail4;
            GridView3.DataBind();
            //0
            GridView3.FooterRow.Cells[1].Text = "Total";
            GridView3.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            object sumObjectn = dtProductDetail4.Compute("Sum(TargetIndig2021)", string.Empty);
            GridView3.FooterRow.Cells[2].Text = sumObjectn.ToString();
            //1
            object sumObjectn1 = dtProductDetail4.Compute("Sum(TargetIndig2022)", string.Empty);
            GridView3.FooterRow.Cells[3].Text = sumObjectn1.ToString();
            //2
            object sumObjectn2 = dtProductDetail4.Compute("Sum(TargetIndig2023)", string.Empty);
            GridView3.FooterRow.Cells[4].Text = sumObjectn2.ToString();
            //3
            object sumObjectn3 = dtProductDetail4.Compute("Sum(TargetIndig2024)", string.Empty);
            GridView3.FooterRow.Cells[5].Text = sumObjectn3.ToString();
            //4
            object sumObjectn4 = dtProductDetail4.Compute("Sum(TargetIndig2025)", string.Empty);
            GridView3.FooterRow.Cells[6].Text = sumObjectn4.ToString();
            //5
            object sumObjectn5 = dtProductDetail4.Compute("Sum(TargetIndigNILL)", string.Empty);
            GridView3.FooterRow.Cells[7].Text = sumObjectn5.ToString();
        }
        else
        { }
        #endregion
        #region sixth Table
        DataTable dtProductDetail5 = Lo.RetriveProductIndig1("", "", "gettotprovalue18");
        if (dtProductDetail5.Rows.Count > 0)
        {
            GridView4.DataSource = dtProductDetail5;
            GridView4.DataBind();
            //0
            GridView4.FooterRow.Cells[1].Text = "Total";
            GridView4.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            object sumObjectn = dtProductDetail5.Compute("Sum(TotalProd)", string.Empty);
            GridView4.FooterRow.Cells[2].Text = sumObjectn.ToString();
            //1
            object sumObjectn1 = dtProductDetail5.Compute("Sum(TotalPrice1819)", string.Empty);
            GridView4.FooterRow.Cells[3].Text = sumObjectn1.ToString();
            //2
            object sumObjectn2 = dtProductDetail5.Compute("Sum(ProdLess5)", string.Empty);
            GridView4.FooterRow.Cells[4].Text = sumObjectn2.ToString();
            //3
            object sumObjectn3 = dtProductDetail5.Compute("Sum(ProdLess10)", string.Empty);
            GridView4.FooterRow.Cells[5].Text = sumObjectn3.ToString();
            //4
            object sumObjectn4 = dtProductDetail5.Compute("Sum(ProdLess50)", string.Empty);
            GridView4.FooterRow.Cells[6].Text = sumObjectn4.ToString();
            //5
            object sumObjectn5 = dtProductDetail5.Compute("Sum(ProdAbove50)", string.Empty);
            GridView4.FooterRow.Cells[7].Text = sumObjectn5.ToString();

        }
        else
        { }
        #endregion
        ScriptManager.RegisterStartupScript(this, GetType(), "divCompany", "showPopup1();", true);
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("ProductList");
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        SeachResult();
    }
    #region AutoComplete Serach Box
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetSearchKeyword(string prefix)
    {
        Cryptography objCrypto1 = new Cryptography();
        List<string> customers = new List<string>();
        List<string> Finalcustomers = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = objCrypto1.DecryptData(ConfigurationManager.ConnectionStrings["connectiondb"].ConnectionString);
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct ProductRefNo from tbl_trn_ProductFilterSearchTemp  where ProductRefNo like @SearchText + '%'";
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
                cmd.CommandText = "select distinct CompanyName from tbl_trn_ProductFilterSearchTemp where CompanyName like @SearchText + '%'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["CompanyName"]));
                    }
                }
                cmd.CommandText = "select distinct FactoryName from tbl_trn_ProductFilterSearchTemp where FactoryName like @SearchText + '%'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["FactoryName"]));
                    }
                }

                cmd.CommandText = "select distinct UnitName from tbl_trn_ProductFilterSearchTemp where UnitName like @SearchText + '%'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["UnitName"]));
                    }
                }
                cmd.CommandText = "select distinct NSCCode from tbl_mst_MainProduct where NSCCode like @SearchText + '%'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["NSCCode"]));
                    }
                }
                cmd.CommandText = "select distinct ProductDescription from tbl_Mst_MainProduct where ProductDescription like @SearchText + '%' and (ProductDescription is not null or ProductDescription !='')";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["ProductDescription"]));
                    }
                }
                cmd.CommandText = "select distinct NSNGroup from tbl_trn_ProductFilterSearchTemp where NSNGroup like @SearchText + '%' and (NSNGroup is not null or NSNGroup !='')";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["NSNGroup"]));
                    }
                }
                cmd.CommandText = "select distinct DefencePlatform from tbl_trn_ProductFilterSearchTemp where DefencePlatform like @SearchText + '%' and (DefencePlatform is not null or DefencePlatform !='')";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["DefencePlatform"]));
                    }
                }
                cmd.CommandText = "select distinct ProdIndustryDoamin from tbl_trn_ProductFilterSearchTemp where ProdIndustryDoamin like @SearchText + '%' and (ProdIndustryDoamin is not null or ProdIndustryDoamin !='')";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["ProdIndustryDoamin"]));
                    }
                }
                cmd.CommandText = "select distinct NSNGroupClass from tbl_trn_ProductFilterSearchTemp where NSNGroupClass like @SearchText + '%' and (NSNGroupClass is not null or NSNGroupClass !='')";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["NSNGroupClass"]));
                    }
                }
                cmd.CommandText = "select distinct ItemCode from tbl_trn_ProductFilterSearchTemp where ItemCode like @SearchText + '%' and (ItemCode is not null or ItemCode !='')";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["ItemCode"]));
                    }
                }
                cmd.CommandText = "select distinct ProdIndustrySubDomain from tbl_trn_ProductFilterSearchTemp where ProdIndustrySubDomain like @SearchText + '%' and (ProdIndustrySubDomain is not null or ProdIndustrySubDomain !='')";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["ProdIndustrySubDomain"]));
                    }
                }
                cmd.CommandText = "select distinct TopImages from tbl_trn_ProductFilterSearchTemp where TopImages like @SearchText + '%' and (TopImages is not null or TopImages !='')";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["TopImages"]));
                    }
                }
                cmd.CommandText = "select  distinct TopPdf from tbl_trn_ProductFilterSearchTemp where TopPdf like @SearchText + '%' and (TopPdf is not null or TopPdf !='') ";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["TopPdf"]));
                    }
                }
                cmd.CommandText = "select distinct HSNCode from tbl_trn_ProductFilterSearchTemp where HSNCode like @SearchText + '%' and (HSNCode is not null or HSNCode !='')";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["HSNCode"]));
                    }
                }
                cmd.CommandText = "select distinct DPSUPartNumber from tbl_trn_ProductFilterSearchTemp where DPSUPartNumber like @SearchText + '%' and (DPSUPartNumber is not null or DPSUPartNumber !='')";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["DPSUPartNumber"]));
                    }
                }
                cmd.CommandText = "select distinct OEMName from tbl_trn_ProductFilterSearchTemp where OEMName like @SearchText + '%' and (OEMName is not null or OEMName !='')";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["OEMName"]));
                    }
                }
                cmd.CommandText = "select distinct OEMCountry from tbl_trn_ProductFilterSearchTemp where OEMCountry like @SearchText + '%' and (OEMCountry is not null or OEMCountry !='')";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["OEMCountry"]));
                    }
                }
                cmd.CommandText = "select distinct EstimatedQty from tbl_trn_ProdQtyPrice where EstimatedQty like @SearchText + '%'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["EstimatedQty"]));
                    }
                }
                cmd.CommandText = "select distinct EstimatedPrice from tbl_trn_ProdQtyPrice where EstimatedPrice like @SearchText + '%'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["EstimatedPrice"]));
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
        User_U_ProductList u = new User_U_ProductList();
        u.SeachResult(prefix);
        return "search";
    }
    #endregion
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
        chkimportvalue.SelectedIndex = -1;
        rbsort.SelectedIndex = -1;
        ddldeclaration.SelectedIndex = -1;
        chkeoistatus.SelectedIndex = -1;
    }
    #endregion
}