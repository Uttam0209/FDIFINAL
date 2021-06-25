using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using Encryption;
using System.Drawing;

public partial class User_MasterUpdateSuccessStory : System.Web.UI.Page
{
    Logic Lo = new Logic();
    DataTable Dt = new DataTable();
    DataTable Dt1 = new DataTable();
    Cryptography Enc = new Cryptography();
    DataUtility Co = new DataUtility();
    private PagedDataSource pgsource = new PagedDataSource();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["mpafefor"] != null)
            {
                BindSupplyOrder();
            }
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
            SeachResult();
        }
    }
    protected void gvSuccessStory_RowDataBound(object sender, GridViewRowEventArgs e)
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
            
            }
    }

    protected void gvSuccessStory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "VEOI")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "modelsupplyorder", "showPopup();", true);
        }
    }

    protected void ddlsort_SelectedIndexChanged(object sender, EventArgs e)
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
                        gvSuccessStory.DataSource = pgsource;
                        gvSuccessStory.DataBind();
                        gvSuccessStory.Visible = true;
                        lbltotalshowpageitem.Text = pgsource.FirstIndexInPage + 1 + " - " + (pgsource.FirstIndexInPage + pgsource.Count);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
                        gvSuccessStory.Visible = false;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
                }
            }
            else
            {
                gvSuccessStory.Visible = false;
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

    protected void lbsubmit_Click(object sender, EventArgs e)
    {

    }
}