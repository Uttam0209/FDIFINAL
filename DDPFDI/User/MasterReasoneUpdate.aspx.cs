using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using Encryption;

public partial class User_MasterReasoneUpdate : System.Web.UI.Page
{

    Cryptography Enc = new Cryptography();
    DataUtility Co = new DataUtility();
    Logic Lo = new Logic();
    DataTable Dt1 = new DataTable();
    private PagedDataSource pgsource = new PagedDataSource();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { BindData(); }
    }
    protected void BindData()
    {
        DataTable DtRec = new DataTable();
        if (Request.QueryString["below0"] != null)
        {
            DtRec = Lo.ProductWizard(0, 0, 0, 0, 0, Enc.DecryptData(Session["Type"].ToString()), Session["CompanyRefNo"].ToString(), "", "", "", "", "", "", "", "Below0");
            infotitle.InnerText = "Product Below 0.005";
        }
        else if (Request.QueryString["value0"] != null)
        {
            DtRec = Lo.ProductWizard(0, 0, 0, 0, 0, Enc.DecryptData(Session["Type"].ToString()), Session["CompanyRefNo"].ToString(), "", "", "", "", "", "", "", "value0");
            infotitle.InnerText = "Product Value has 0";
        }
        else if (Request.QueryString["lb17"] != null)
        {
            DtRec = Lo.ProductWizard(0, 0, 0, 0, 0, Enc.DecryptData(Session["Type"].ToString()), Session["CompanyRefNo"].ToString(), "", "", "", "", "", "", "", "lb17");
            infotitle.InnerText = "Product has Year below 2017-18";
        }
        else if (Request.QueryString["yrnot"] != null)
        {
            DtRec = Lo.ProductWizard(0, 0, 0, 0, 0, Enc.DecryptData(Session["Type"].ToString()), Session["CompanyRefNo"].ToString(), "", "", "", "", "", "", "", "yrnot");
            infotitle.InnerText = "Product that has year not fill";
        }
        else if (Request.QueryString["eligibe"] != null)
        {
            DtRec = Lo.ProductWizard(0, 0, 0, 0, 0, Enc.DecryptData(Session["Type"].ToString()), Session["CompanyRefNo"].ToString(), "", "", "", "", "", "", "", "eligibe");
            infotitle.InnerText = "Product not eligible to display on public portal";
        }
        else if (Request.QueryString["isindigi"] != null)
        {
            DtRec = Lo.ProductWizard(0, 0, 0, 0, 0, Enc.DecryptData(Session["Type"].ToString()), Session["CompanyRefNo"].ToString(), "", "", "", "", "", "", "", "isindigi");
            infotitle.InnerText = "Product Indegenized No";
        }
        else if (Request.QueryString["viewonly"] != null)
        {
            DtRec = Lo.ProductWizard(0, 0, 0, 0, 0, Enc.DecryptData(Session["Type"].ToString()), Session["CompanyRefNo"].ToString(), "", "", "", "", "", "", "", "viewonly");
            infotitle.InnerText = "Product display but View only Category IGA,In-Hosuse or Indegenized Yes";
        }
        else
        { Response.Redirect("ProductWizard"); }
        if (DtRec.Rows.Count > 0)
        {
            ViewState["PDatatTable"] = DtRec;
            SeachResult();
        }
        else
        { }
    }
    protected void ddlsort_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
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
    protected void txtsearch_TextChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    protected void gvEoi_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvEoi_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "")
        {
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gvr.RowIndex;
            CheckBox chk = (gvEoi.Rows[rowIndex].FindControl("chkRow") as CheckBox);
            if (chk.Checked == true)
            {
                if (Request.QueryString["below0"] != null)
                {ScriptManager.RegisterStartupScript(this, GetType(), "modeleoi", "showPopup();", true);}
                else if (Request.QueryString["value0"] != null)
                { ScriptManager.RegisterStartupScript(this, GetType(), "modeleoi", "showPopup();", true); }
                else if (Request.QueryString["lb17"] != null)
                { ScriptManager.RegisterStartupScript(this, GetType(), "modeleoi", "showPopup();", true); }
                else if (Request.QueryString["yrnot"] != null)
                { ScriptManager.RegisterStartupScript(this, GetType(), "modeleoi", "showPopup();", true); }
                else if (Request.QueryString["eligibe"] != null)
                { ScriptManager.RegisterStartupScript(this, GetType(), "modeleoi", "showPopup();", true); }
                else if (Request.QueryString["isindigi"] != null)
                { ScriptManager.RegisterStartupScript(this, GetType(), "modeleoi", "showPopup();", true); }
                else if (Request.QueryString["viewonly"] != null)
                { ScriptManager.RegisterStartupScript(this, GetType(), "modeleoi", "showPopup();", true); }                
            }
            else
            { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Please select checkbox to update.');", true); }

        }
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
}