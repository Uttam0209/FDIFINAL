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
using System.Text.RegularExpressions;

public partial class User_MasterUpdateEOI : System.Web.UI.Page
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
                BindEoi();
            }
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
    protected void lbsubmit_Click(object sender, EventArgs e)
    {
        if (txturl.Text != "" && txtstartdate.Text != "" && txtenddate.Text != "" && ddleoistatus.SelectedItem.Text != "Select" && ddleoitype.SelectedItem.Text != "Select")
        {
            DateTime DtStart = Convert.ToDateTime(txtstartdate.Text.Trim());
            string DateStart = DtStart.ToString("yyyy-MMM-dd");
            DateTime DtEnd = Convert.ToDateTime(txtenddate.Text.Trim());
            string DateEnd = DtStart.ToString("yyyy-MMM-dd");
            string valuetype = "";
            if (ddleoistatus.SelectedItem.Text == "No")
            { valuetype = "No"; }
            else
            {
                if (ddleoitype.SelectedItem.Text == "Archive")
                { valuetype = "Archive"; }
                else
                {
                    valuetype = "Yes";
                }
            }
            if (DtStart <= DtEnd)
            {
                if (IsValidURL(txturl.Text.Trim()) == true)
                {
                    foreach (GridViewRow row in gvEoi.Rows)
                    {
                        try
                        {
                            if ((row.FindControl("chkRow") as CheckBox).Checked)
                            {
                                DataTable DtUpdateEoi = Lo.ProductWizard(0, 0, 0, 0, 0, row.Cells[1].Text.Trim(), valuetype.ToString().Trim(), DateStart.ToString().Trim(), DateEnd.ToString().Trim(), txturl.Text.Trim(), "", "", "", "", "UpdateEOISave");
                            }
                            BindEoi();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('EOI Status item no " + row.Cells[1].Text.Trim() + " ,Item name " + row.Cells[2].Text.Trim() + " has been update successfully.')", true);
                        }
                        catch (Exception ex)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Invalid Url format.')", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('EOI start date always less then EOI end date.')", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Fill all field mandatory')", true);

        }
    }
    public static bool IsValidURL(string InputEmail)
    {
        string pattern = "((http|https)://)(www.)?[a-zA-Z0-9@:%._\\+~#?&//=]{2,256}\\.[a-z]{2,6}\\b([-a-zA-Z0-9@:%._\\+~#?&//=]*)";
        Match match = Regex.Match(InputEmail.Trim(), pattern, RegexOptions.IgnoreCase);
        if (match.Success)
            return true;
        else
            return false;
    }
    protected void ddleoistatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddleoistatus.SelectedItem.Text == "Select")
        { diveoishow.Visible = false; lbsubmit.Visible = false; }
        else
        { diveoishow.Visible = true; lbsubmit.Visible = true; }
    }

    protected void txtenddate_TextChanged(object sender, EventArgs e)
    {
        if (txtenddate.Text != "")
        {
            if (Convert.ToDateTime(txtenddate.Text) <= DateTime.Now.AddDays(-1))
            {
                ddleoitype.SelectedValue = "Archive";
            }
            if (Convert.ToDateTime(txtenddate.Text) < Convert.ToDateTime(txtstartdate.Text))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('EOI start date could not be greater then eoi end date')", true);
            }
        }
    }

    
}