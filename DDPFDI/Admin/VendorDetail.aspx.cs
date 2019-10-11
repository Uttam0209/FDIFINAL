using Encryption;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Web;
using System.Web.Helpers;
using BusinessLayer;

public partial class Admin_VendorDetail : System.Web.UI.Page
{
    Cryptography Encrypt = new Cryptography();
    DataUtility Co = new DataUtility();
    Logic Lo = new Logic();
    DataTable DtGrid = new DataTable();
    private string _sysMsg = string.Empty;
    private string _msg = string.Empty;
    private PagedDataSource pgsource = new PagedDataSource();
    private int firstindex, lastindex;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            if (Session["User"] != null)
            {
                if (Request.QueryString["id"] != null)
                {
                    if (Encrypt.DecryptData(Session["Type"].ToString()) == "Admin" || Encrypt.DecryptData(Session["Type"].ToString()) == "SuperAdmin")
                    {
                        ControlGrid(Encrypt.DecryptData(Request.QueryString["id"].ToString()), "");
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "ErrorMssgPopup('Session Expired,Please login again');window.location='Login'", true);
            }
        }
    }
    private void ControlGrid(string mVal, string RefNo)
    {
        hfmtype.Value = mVal.ToString();
        if (hfmtype.Value == "V")
        {
            gvvendor.Visible = true;
            BindVendorDetail();
        }
    }
    protected void BindVendorDetail()
    {
        DtGrid = Lo.GetDashboardData("VendorDetail", "");
        if (DtGrid.Rows.Count > 0)
        {
            DataTable dtads = DtGrid;
            pgsource.DataSource = dtads.DefaultView;
            pgsource.AllowPaging = true;
            pgsource.PageSize = 100;
            pgsource.CurrentPageIndex = pagingCurrentPage;
            ViewState["totpage"] = pgsource.PageCount;
            lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
            lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
            lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
            pgsource.DataSource = dtads.DefaultView;
            gvvendor.DataSource = pgsource;
            gvvendor.DataBind();
            divpageindex.Visible = true;
            lbltotal.Text = "Showing  " + gvvendor.Rows.Count.ToString() + " result from page " + (pagingCurrentPage + 1) + " out of " + pgsource.PageCount + " pages";
        }
    }
    protected void gvvendor_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable DtView = Lo.RetriveGridViewCompany(e.CommandArgument.ToString(), "", "", "RetriveVendorById");
        if (DtView.Rows.Count > 0)
        {
            lblcomprefno.Text = DtView.Rows[0]["VendorRefNo"].ToString();
            lblcompname.Text = DtView.Rows[0]["V_CompName"].ToString();
            lbladdress.Text = DtView.Rows[0]["StreetAddress"].ToString();
            DataTable BindDPSU = Lo.RetriveGridViewCompany(e.CommandArgument.ToString(), "", "", "RetriveVendorDPSU");
            if (BindDPSU.Rows.Count > 0)
            {
                gvdpsu.DataSource = BindDPSU;
                gvdpsu.DataBind();
            }
            lblgst.Text = DtView.Rows[0]["GSTNo"].ToString();
            lblcontact.Text = DtView.Rows[0]["ContactNo"].ToString();
            lblcity.Text = DtView.Rows[0]["City"].ToString();
            lblzipcode.Text = DtView.Rows[0]["ZipCode"].ToString();
            lblnodalemail.Text = DtView.Rows[0]["NodalOfficerEmail"].ToString();
            lblmsme.Text = DtView.Rows[0]["MSME"].ToString();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "showPopup();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Error while fetching record.')", true);
        }
    }
    protected void gvvendor_RowCreated(object sender, GridViewRowEventArgs e)
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
    #region //------------------------pageindex code--------------//
    protected void lnkbtnPgPrevious_Click(object sender, EventArgs e)
    {
        txtpageno.Text = "";
        pagingCurrentPage -= 1;
        if (hfmtype.Value == "V")
        {
            BindVendorDetail();
        }
    }
    protected void lnkbtnPgNext_Click(object sender, EventArgs e)
    {
        pagingCurrentPage += 1;
        int txtpage = Convert.ToInt32(pagingCurrentPage) + 1;
        txtpageno.Text = txtpage.ToString();
        if (hfmtype.Value == "C")
        {
            BindVendorDetail();
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
            int txtpage = Convert.ToInt32(txtpageno.Text) - 1;
            pagingCurrentPage = Convert.ToInt32(txtpage.ToString());
            if (hfmtype.Value == "V")
            {
                BindVendorDetail();
            }
        }
    }
    //end page index---------------------------------------//
    #endregion
}