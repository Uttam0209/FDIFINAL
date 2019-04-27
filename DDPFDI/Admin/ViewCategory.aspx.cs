using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Encryption;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;

public partial class Admin_ViewCategory : System.Web.UI.Page
{
    Logic Lo = new Logic();
    DataUtility Co = new DataUtility();
    Cryptography objEnc = new Cryptography();
    DataTable DtGrid = new DataTable();
    DataTable DtCompanyDDL = new DataTable();
    private string mType = "";
    private string currentPage = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            currentPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
            mType = objEnc.DecryptData(Session["Type"].ToString());
            BindGridView();
        }
    }
    #region Load
    protected void BindGridView(string sortExpression = null)
    {
        try
        {
            DataTable DtGrid = Lo.RetriveMasterCategoryDate(0, "", "", "Select");
            if (DtGrid.Rows.Count > 0)
            {
                if (sortExpression != null)
                {
                    DataView dv = DtGrid.AsDataView();
                    this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";
                    dv.Sort = sortExpression + " " + this.SortDirection;
                    gvCategory.DataSource = dv;
                }
                else
                {
                    gvCategory.DataSource = DtGrid;
                }
                gvCategory.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }
    private string SortDirection
    {
        get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
        set { ViewState["SortDirection"] = value; }
    }
    #endregion
    #region PageIndex or Sorting
    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCategory.PageIndex = e.NewPageIndex;
        this.BindGridView();
    }
    protected void OnSorting(object sender, GridViewSortEventArgs e)
    {
        this.BindGridView(e.SortExpression);
    }
    #endregion
    #region RowDatabound
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField lblrefno = e.Row.FindControl("hfcat") as HiddenField;
            GridView gvSubcat = e.Row.FindControl("gvsubcat") as GridView;
            DataTable DtGrid = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(lblrefno.Value), "", "", "SubSelectID");
            if (DtGrid.Rows.Count > 0)
            {
                gvSubcat.DataSource = DtGrid;
                gvSubcat.DataBind();
            }
            //GridView gvFactoryRow = e.Row.FindControl("gvfactory") as GridView;
            // gvFactoryRow.RowCommand += new GridViewCommandEventHandler(gvfactory_RowCommand);
        }
    }
    #endregion
}