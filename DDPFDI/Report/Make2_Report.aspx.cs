using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using Encryption;
using BusinessLayer;
using System.Data.SqlClient;
using System.Configuration;
using context = System.Web.HttpContext;
using System.Text;

public partial class Report_Make2_Report : System.Web.UI.Page
{
    #region Pagevariable
    private Logic Lo = new Logic();
    private DataTable DtGrid = new DataTable();
    DataUtility Co = new DataUtility();
    DataTable DtFilterView = new DataTable();
    private PagedDataSource pgsource = new PagedDataSource();
    private DataTable DtCompanyDDL = new DataTable();
    private string mRefNo = "";
    private Cryptography objEnc = new Cryptography();

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Type"] != null || Session["User"] != null)
        {
            if (!IsPostBack)
            {
                if (Session["User"] != null)
                {
                    try
                    {

                        hidType.Value = objEnc.DecryptData(Session["Type"].ToString());
                        mRefNo = Session["CompanyRefNo"].ToString();
                        hidCompanyRefNo.Value = mRefNo.ToString();
                        BindData();
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
                {
                    Response.RedirectToRoute("login");
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "ErrorMssgPopup('Session Expired,Please login again');window.location='Login'", true);
        }
    }
    protected void BindCompany()
    {
        try
        {
            if (hidType.Value == "Admin" || hidType.Value == "SuperAdmin")
            {
                DtCompanyDDL = DtGrid.DefaultView.ToTable(true, "CompanyName", "CompanyRefNo");
            }
            else
            {
                DataView dvcompfilter = new DataView(DtGrid);
                if (hidType.Value.Contains("C"))
                {
                    dvcompfilter.RowFilter = "CompanyRefNo='" + Session["CompanyRefNo"].ToString() + "'";
                }
                else if (hidType.Value.Contains("D"))
                {
                    dvcompfilter.RowFilter = "FactoryRefno='" + Session["CompanyRefNo"].ToString() + "'";
                }
                else if (hidType.Value.Contains("U"))
                {
                    dvcompfilter.RowFilter = "UnitRefno='" + Session["CompanyRefNo"].ToString() + "'";
                }
                DtGrid = dvcompfilter.ToTable();
                DtCompanyDDL = DtGrid.DefaultView.ToTable(true, "CompanyName", "CompanyRefNo");
            }
            DtCompanyDDL.DefaultView.Sort = "CompanyName asc";
            if (DtCompanyDDL.Rows.Count > 0)
            {
                for (int i = DtCompanyDDL.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = DtCompanyDDL.Rows[i];
                    if (dr["CompanyName"].ToString() == "Testing Company")
                        dr.Delete();
                }
                DtCompanyDDL.AcceptChanges();
                ddlcompany.DataSource = DtCompanyDDL;
                ddlcompany.DataBind();
                if(hidType.Value == "Admin" || hidType.Value == "SuperAdmin")
                { ddlcompany.Visible = true; }
                else
                { ddlcompany.Visible = false; }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(" + ex.Message + ")", true);
        }

    }
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
    protected void BindData()
    {
        //ddlcompany.Items.Insert(0, "Select");
        DtGrid = Lo.MaketwoReport(ddlcompany.ToString(), "", "", "", "Make2Report");
        if (DtGrid.Rows.Count > 0)
        {
            UpdateDtGridValue();
            Session["PDatatTable"] = DtGrid;
            SeachResult();
        }
        else
        {
            divcontentproduct.Visible = false;
            gveoi.DataBind();
        }
    }
    protected string Dvinsert(string sortExpression = null)
    {
        DataTable insert = new DataTable();
        insert.Columns.Add(new DataColumn("Column", typeof(string)));
        insert.Columns.Add(new DataColumn("Value", typeof(string)));
        DataRow dr;

        if (hidType.Value != "" && hidType.Value != "Admin" || hidType.Value != "SuperAdmin")
        {
            dr = insert.NewRow();
            if (hidType.Value.Contains("C"))
            {
                dr["Column"] = "CompanyRefNo" + "=";
                dr["Value"] = "'" + hidCompanyRefNo.Value.ToString() + "'";
            }
            else if (hidType.Value.Contains("D"))
            {
                dr["Column"] = "FactoryRefno" + "=";
                dr["Value"] = "'" + hidCompanyRefNo.Value.ToString() + "'";
            }
            else if (hidType.Value.Contains("U"))
            {
                dr["Column"] = "UnitRefno" + "=";
                dr["Value"] = "'" + hidCompanyRefNo.Value.ToString() + "'";
            }
            insert.Rows.Add(dr);
        }
        if (txtsearch.Text.Trim() != "")
        {
            dr = insert.NewRow();
            dr["Column"] = "((ProductRefNo like";
            dr["Value"] = "'%" + txtsearch.Text.Trim() + "%') or (CompanyName like '%" + txtsearch.Text.Trim() + "%') or (FactoryName like '%" + txtsearch.Text.Trim() + "%') or (UnitName like '%" + txtsearch.Text.Trim() + "%') or (ManufactureName like '%" + txtsearch.Text.Trim() + "%') or (ProductDescription like '%" + txtsearch.Text.Trim() + "%') or (IndTargetYear like '%" + txtsearch.Text.Trim() + "%'))";
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
    }
    protected string BindInsertfilter()
    {
        return Dvinsert();
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
    string insert1 = "";
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
                    lbltotfilter.Text = dtinner.Rows.Count.ToString();
                    Session["ExcelDT"] = dtinner;
                    DataTable dtads = dv.ToTable();
                    if (dtads.Rows.Count > 0)
                    {
                        if (dtads.Columns.Contains("row_no"))
                        {
                            int i = 1; foreach (DataRow r in dtads.Rows) r["row_no"] = i++;
                        }
                        else
                        {
                            dtads.Columns.Add("row_no");
                            int i = 1; foreach (DataRow r in dtads.Rows) r["row_no"] = i++;
                        }
                        pgsource.DataSource = dtinner.DefaultView;
                        pgsource.AllowPaging = true;
                        pgsource.PageSize = 25;
                        pgsource.CurrentPageIndex = pagingCurrentPage;
                        lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                        lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                        lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                        pgsource.DataSource = dtads.DefaultView;
                        gveoi.DataSource = pgsource;
                        gveoi.DataBind();
                        gveoi.Visible = true;
                        lbltotalshowpageitem.Text = pgsource.FirstIndexInPage + 1 + " - " + (pgsource.FirstIndexInPage + pgsource.Count);
                        divcontentproduct.Visible = true;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
                        divcontentproduct.Visible = false;
                        gveoi.Visible = false;
                    }
                }
                else
                {
                    gveoi.Visible = false;
                    divcontentproduct.Visible = false;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
                }
            }
            else
            {
                gveoi.Visible = false;
                divcontentproduct.Visible = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable searchdt = (DataTable)Session["ExcelDT"];
            int[] iColumns = { 0, 3, 6, 12, 13, 15, 16, 23, 25, 26, 27 };
            RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
            objExport.ExportDetails(searchdt, iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "Make-II.xls");
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        SeachResult();
    }
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
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard");
    }
    protected void ddlcompany_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "comp")
        {
            if (hidType.Value == "Admin" || hidType.Value == "SuperAdmin")
            {
                hidType.Value = "Company";
            }
            hidCompanyRefNo.Value = e.CommandArgument.ToString();
            SeachResult();
        }

    }
    protected void gveoi_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[5].Text == "" || e.Row.Cells[5].Text == "")
            {
                e.Row.Cells[5].Text = "No";
            }
            if (e.Row.Cells[7].Text == "" || e.Row.Cells[7].Text == "")
                e.Row.Cells[7].Text = "0.00";
        }
    }
}
