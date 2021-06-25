using BusinessLayer;
using Encryption;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using context = System.Web.HttpContext;

public partial class User_StatusOnDate : System.Web.UI.Page
{
    #region Pagevariable
    private Logic Lo = new Logic();
    private DataTable DtGrid = new DataTable();
    private string _msg = string.Empty;
    private string _sysMsg = string.Empty;
    DataUtility Co = new DataUtility();
    private Cryptography Encrypt = new Cryptography();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridStatus();
            BindGrid();
        }
    }
    protected void BindGrid()
    {
        DtGrid = Lo.NewRetriveFilterCode("ProdCountYerWise", "", "", "", "", 0, 0, 0);
        if (DtGrid.Rows.Count > 0)
        {
            gvyrstatus.DataSource = DtGrid;
            gvyrstatus.DataBind();
        }
    }
    protected void BindGridStatus()
    {
        DtGrid = Lo.NewRetriveFilterCode("Status", "", "", "", "", 0, 0, 0);
        if (DtGrid.Rows.Count > 0)
        {
            gvstatus.DataSource = DtGrid;
            gvstatus.DataBind();
        }
    }

    protected void gvstatus_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "action")
        { }
    }
}