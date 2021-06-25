using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Encryption;
using System.Drawing;
using System.Text.RegularExpressions;

public partial class User_UpdateWizard : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Cryptography Enc = new Cryptography();
    DataTable Dt = new DataTable();
    DataUtility Co = new DataUtility();
    private PagedDataSource pgsource = new PagedDataSource();
    DataTable Dt1 = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Type"] != null && Session["User"] != null && Session["CompanyRefNo"] != null) { BindTotalCount(); }
            else
            {
                Response.RedirectToRoute("ProductList");

            }
            divupdates.Visible = false;
        }
    }
    protected void BindTotalCount()
    {
        DataTable dtCount = Lo.ProductWizard(0, 0, 0, 0, 0, Enc.DecryptData(Session["Type"].ToString()), Session["CompanyRefNo"].ToString(), "", "", "", "", "", "", "", "CountAll");
        if (dtCount.Rows.Count > 0)
        {
            //lbltotalcmsprod.Text = dtCount.Rows[0]["TotalProduct"].ToString();
            lbltotalproduct.Text = dtCount.Rows[0]["TotalPublicPortal"].ToString();
            lbltotalinterest.Text = dtCount.Rows[0]["TotalInterestProduct"].ToString();
            lbtotaleoi.Text = dtCount.Rows[0]["EoiTotoal"].ToString();
            lbtotaleoiinterest.Text = dtCount.Rows[0]["EoiInterestTotal"].ToString();
            lbtotalsupplyorder.Text = dtCount.Rows[0]["SupplyTotoal"].ToString();
            lbtotalintsupplyorder.Text = dtCount.Rows[0]["SupplyInterestTotal"].ToString();
            lblnotdisplayinmarketplace.Text = dtCount.Rows[0]["NotDisplayInPortal"].ToString();
            lbtotalsuccessstory.Text = dtCount.Rows[0]["SuccessstoryInterestTotal"].ToString();

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
    string insert1 = "";
    protected string Dvinsert(string sortExpression = null)
    {
        DataTable insert = new DataTable();
        insert.Columns.Add(new DataColumn("Column", typeof(string)));
        insert.Columns.Add(new DataColumn("Value", typeof(string)));
        DataRow dr;
        if (txtsearch.Text.Trim() != "")
        {
            dr = insert.NewRow();
            dr["Column"] = "((ProductRefNo like";
            dr["Value"] = "'%" + txtsearch.Text.Trim() + "%') or (CompanyName like '%" + txtsearch.Text.Trim() + "%') or (FactoryName like '%" + txtsearch.Text.Trim() + "%') or (UnitName like '%" + txtsearch.Text.Trim() + "%'))";
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
    public void SeachResult(string sortExpression = null)
    {
        try
        {
            Dt1 = (DataTable)ViewState["PDatatTable"];
            if (Dt1.Rows.Count > 0)
            {
                DataView dv = new DataView(Dt1);
                DataTable dtnew = dv.ToTable();
                if (dtnew.Rows.Count > 0)
                {
                    dv.RowFilter = BindInsertfilter();
                    DataTable dtinner = dv.ToTable();
                    lbltotfilter.Text = dtinner.Rows.Count.ToString();
                    ViewState["ExcelDT"] = dtinner;
                    DataTable dtads = dv.ToTable();
                    if (dtads.Rows.Count > 0)
                    {
                        DataView _dv = new DataView(dtads);
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
                        pgsource.PageSize = Convert.ToInt32(ddlsort.SelectedItem.Text);
                        pgsource.CurrentPageIndex = pagingCurrentPage;
                        lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                        lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                        lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                        pgsource.DataSource = dtads.DefaultView;
                        gvEoi.DataSource = pgsource;
                        gvEoi.DataBind();
                        gvEoi.Visible = true;
                        lbltotalshowpageitem.Text = pgsource.FirstIndexInPage + 1 + " - " + (pgsource.FirstIndexInPage + pgsource.Count);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
                        gvEoi.Visible = false;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
                }
            }
            else
            {
                gvEoi.Visible = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void BindEoi()
    {
        if (Request.QueryString["mnnn"] == "eoiall")
        {
            Dt = Lo.ProductWizard(0, 0, 0, 0, 0, Enc.DecryptData(Session["Type"].ToString()), Session["CompanyRefNo"].ToString(), "", "", "", "", "", "", "", "EOI");
        }
        else
        {
            Dt = Lo.ProductWizard(0, 0, 0, 0, 0, Enc.DecryptData(Session["Type"].ToString()), Session["CompanyRefNo"].ToString(), "", "", "", "", "", "", "", "EOIInterest");
        }
        if (Dt.Rows.Count > 0)
        {
            ViewState["PDatatTable"] = Dt;
            SeachResult();
        }
       divupdates.Visible = true;
        gvEoi.Visible = true;
    }
    protected void lbaddproduct_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddProduct?mu=MRtCwN+7N6dMmohOhVozbQ==&id=+N4siQw3MQs=");
    }
    protected void lbwizarddashboard_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProductWizard");
    }
    protected void lbviewproduct_Click(object sender, EventArgs e)
    {
        Response.Redirect("View-Product?mu=MRtCwN+7N6dMmohOhVozbQ==&id=68L+ArI+MgA=");
    }
    protected void lbltotalproduct_Click(object sender, EventArgs e)
    {
        Response.Redirect("View-Product?mu=MRtCwN+7N6dMmohOhVozbQ==&id=68L+ArI+MgA=&mtype=giunULx7nTjzohX9iBWmCQ==");
    }
    protected void lbltotalinterest_Click(object sender, EventArgs e)
    {
        Response.Redirect("View-Product?mu=MRtCwN+7N6dMmohOhVozbQ==&id=68L+ArI+MgA=&mtype=giunULx7nTiw8JOwUDXsg0EXUnr/Bk+E");
    }
    protected void lbtotaleoi_Click(object sender, EventArgs e)
    {

        BindEoi();
        divupdates.Visible = true;
        //Response.Redirect("EOIU?mpafefor=dgdsg" + Enc.EncryptData(lbtotaleoi.Text) + "&mnnn=eoiall");
    }
    protected void lbtotaleoiinterest_Click(object sender, EventArgs e)
    {
        divwizard.Visible = true;
        BindEoi();
        divupdates.Visible = true;
        //Response.Redirect("EOIU?mpafefor=dgdsg" + Enc.EncryptData(lbtotaleoi.Text) + "&mnnn=eoiallinter");
    }
    protected void lblnotdisplayinmarketplace_Click(object sender, EventArgs e)
    {
        Response.Redirect("NotDisplayPortalProduct?mqueryfornoproduct=" + Enc.EncryptData("Show"));
    }
    protected void lbtotalsupplyorder_Click(object sender, EventArgs e)
    {
        Response.Redirect("SupplyOrderU?mpafefor=dgdsg" + Enc.EncryptData(lbtotalsupplyorder.Text) + "&mnnn=supplyorderall");
    }
    protected void lbtotalintsupplyorder_Click(object sender, EventArgs e)
    {
        divwizard.Visible = true;
        BindSupplyOrder();
        //Response.Redirect("SupplyOrderU?mpafefor=dgdsg" + Enc.EncryptData(lbtotalintsupplyorder.Text) + "&mnnn=supplyorderallinter");
    }


    protected void lbtotalsuccessstory_Click(object sender, EventArgs e)
    {
        Response.Redirect("SuccessStoryU?mpafefor=dgdsg" + Enc.EncryptData(lbtotalintsupplyorder.Text) + "&mnnn=supplyorderallinter");
    }

    protected void lnksetback_Click(object sender, EventArgs e)
    {

    }

    protected void gvEoi_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].ToolTip = "Select checkbox to update details and click on update button to update records. You can select multiple record at a time";
            e.Row.Cells[1].ToolTip = "Product Item Number";
            e.Row.Cells[2].ToolTip = "Product Item Name";
            e.Row.Cells[3].ToolTip = "EOI Status of Product. (you will change it to Yes,Archive)";
            e.Row.Cells[4].ToolTip = "EOI Start Date (It could not be greater then start date.)";
            e.Row.Cells[5].ToolTip = "EOI End Date (It could not be less then start date.)";
            e.Row.Cells[6].ToolTip = "EOI URL (Please not if url will disable at your end please update here to.)";
            e.Row.Cells[7].ToolTip = "Click on update button to update status of EOI.";
            if (e.Row.Cells[3].Text == "#" || e.Row.Cells[3].Text == null || e.Row.Cells[3].Text == "No")
            {
                e.Row.Cells[3].BackColor = Color.DarkRed;
            }
            if (e.Row.Cells[4].Text == "#" || e.Row.Cells[4].Text == "01-Jan-1900" || e.Row.Cells[4].Text == null)
            {
                e.Row.Cells[4].BackColor = Color.DarkCyan;
            }
            if (e.Row.Cells[5].Text == "#" || e.Row.Cells[5].Text == "01-Jan-1900" || e.Row.Cells[5].Text == null)
            {
                e.Row.Cells[5].BackColor = Color.DarkMagenta;
            }
            if (e.Row.Cells[6].Text == "#" || e.Row.Cells[6].Text == null)
            {
                e.Row.Cells[6].BackColor = Color.DarkViolet;
            }
        }
    }

    protected void gvEoi_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "VEOI")
        {
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gvr.RowIndex;
            CheckBox chk = (gvEoi.Rows[rowIndex].FindControl("chkRow") as CheckBox);
            if (chk.Checked == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "modeleoi", "showPopup();", true);
            }
            else
            { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Please select checkbox to update eoi status.');", true); }
        }
    }

    protected void lnkbtnPgPrevious_Click(object sender, EventArgs e)
    {

    }

    protected void lnkbtnPgNext_Click(object sender, EventArgs e)
    {

    }

    protected void ddleoistatus_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void txtenddate_TextChanged(object sender, EventArgs e)
    {

    }

    protected void lbsubmit_Click(object sender, EventArgs e)
    {

    }

    protected void txtsearch_TextChanged(object sender, EventArgs e)
    {
        SeachResult();
    }

    protected void ddlsort_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private int pagingCurrentPageS
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
    string insertS1 = "";
    protected string DvinsertS(string sortExpression = null)
    {
        DataTable inserts = new DataTable();
        inserts.Columns.Add(new DataColumn("Column", typeof(string)));
        inserts.Columns.Add(new DataColumn("Value", typeof(string)));
        DataRow dr;
        if (txtsearch.Text.Trim() != "")
        {
            dr = inserts.NewRow();
            dr["Column"] = "((ProductRefNo like";
            dr["Value"] = "'%" + txtsearch.Text.Trim() + "%') or (CompanyName like '%" + txtsearch.Text.Trim() + "%') or (FactoryName like '%" + txtsearch.Text.Trim() + "%') or (UnitName like '%" + txtsearch.Text.Trim() + "%'))";
            inserts.Rows.Add(dr);
        }
        for (int i = 0; inserts.Rows.Count > i; i++)
        {
            insertS1 = insertS1 + inserts.Rows[i]["Column"].ToString() + " " + inserts.Rows[i]["Value"].ToString() + " " + " and ";
        }
        if (insertS1.ToString() != "")
        {
            insertS1 = insertS1.Substring(0, insertS1.Length - 5);
        }
        return insertS1;
    }
    protected string BindInsertfilterS()
    {
        return DvinsertS();
    }
    public void SeachResultS(string sortExpression = null)
    {
        try
        {
            Dt1 = (DataTable)ViewState["PDatatTable"];
            if (Dt1.Rows.Count > 0)
            {
                DataView dv = new DataView(Dt1);
                DataTable dtnew = dv.ToTable();
                if (dtnew.Rows.Count > 0)
                {
                    dv.RowFilter = BindInsertfilter();
                    DataTable dtinner = dv.ToTable();
                    lbltotfilter.Text = dtinner.Rows.Count.ToString();
                    ViewState["ExcelDT"] = dtinner;
                    DataTable dtads = dv.ToTable();
                    if (dtads.Rows.Count > 0)
                    {
                        DataView _dv = new DataView(dtads);
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
                        pgsource.PageSize = Convert.ToInt32(ddlsort.SelectedItem.Text);
                        pgsource.CurrentPageIndex = pagingCurrentPageS;
                        lblpaging.Text = "Page " + (pagingCurrentPageS + 1) + " of " + pgsource.PageCount;
                        lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                        lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                        pgsource.DataSource = dtads.DefaultView;
                        gvSupplyOrder.DataSource = pgsource;
                        gvSupplyOrder.DataBind();
                        gvSupplyOrder.Visible = true;
                        lbltotalshowpageitem.Text = pgsource.FirstIndexInPage + 1 + " - " + (pgsource.FirstIndexInPage + pgsource.Count);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
                        gvSupplyOrder.Visible = false;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
                }
            }
            else
            {
                gvSupplyOrder.Visible = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void BindSupplyOrder()
    {
        if (Request.QueryString["mnnn"] == "supplyorderall")
        {
            Dt = Lo.ProductWizard(0, 0, 0, 0, 0, Enc.DecryptData(Session["Type"].ToString()), Session["CompanyRefNo"].ToString(), "", "", "", "", "", "", "", "SupplyOrder");
        }
        else
        {
            Dt = Lo.ProductWizard(0, 0, 0, 0, 0, Enc.DecryptData(Session["Type"].ToString()), Session["CompanyRefNo"].ToString(), "", "", "", "", "", "", "", "SupplyorderInterest");
        }
        if (Dt.Rows.Count > 0)
        {
            ViewState["PDatatTable"] = Dt;
            SeachResultS();
        }
    }
    protected void ddlsupplyorder_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResultS();
    }

    protected void Txtsupply_TextChanged(object sender, EventArgs e)
    {
        SeachResultS();
    }

    protected void gvSupplyOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[3].Text == "#" || e.Row.Cells[3].Text == null || e.Row.Cells[3].Text == "No")
            {
                e.Row.Cells[3].BackColor = Color.DarkRed;
            }
            if (e.Row.Cells[4].Text == "#" || e.Row.Cells[4].Text == "01-01-1900")
            {
                e.Row.Cells[4].BackColor = Color.DarkCyan;
            }
            if (e.Row.Cells[5].Text == "#" || e.Row.Cells[5].Text == "01-01-1900")
            {
                e.Row.Cells[5].BackColor = Color.DarkMagenta;
            }
            if (e.Row.Cells[6].Text == "#" || e.Row.Cells[6].Text == null)
            {
                e.Row.Cells[6].BackColor = Color.DarkViolet;
            }
            if (e.Row.Cells[7].Text == "01-jan-1990" || e.Row.Cells[7].Text == null || e.Row.Cells[7].Text == "#")
            {
                e.Row.Cells[7].BackColor = Color.DarkViolet;
            }
            if (e.Row.Cells[8].Text == "01-jan-1990" || e.Row.Cells[8].Text == null || e.Row.Cells[8].Text == "#")
            {
                e.Row.Cells[8].BackColor = Color.DarkViolet;
            }
        }
    }

    protected void gvSupplyOrder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "VEOI")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "modelsupplyorder", "showPopup();", true);
        }
    }

    protected void Lnksupply_Click(object sender, EventArgs e)
    {
        pagingCurrentPage -= 1;
        SeachResultS();
    }

    protected void Lnksupplynext_Click(object sender, EventArgs e)
    {
        pagingCurrentPage += 1;
        SeachResultS();
    }
}