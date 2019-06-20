using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using BusinessLayer;

public partial class Admin_Test : System.Web.UI.Page
{
    Logic Lo = new Logic();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.BindGrid();
        }
    }
    private void BindGrid()
    {
        DataTable DtGrid = Lo.TestGrid("Select", "PRO1", 0, "", 0, "");
        if (DtGrid.Rows.Count > 0)
        {
            GridView1.DataSource = DtGrid;
            GridView1.DataBind();
        }
    }
    protected void Insert(object sender, EventArgs e)
    {
        string name = txtName.Text;
        decimal country = Convert.ToDecimal(txtCountry.Text);
        string Unit = txtUnit.Text;
        txtName.Text = "";
        txtCountry.Text = "";
        Lo.TestGrid("Insert", "PRO1", 0, name, country, txtUnit.Text);
        this.BindGrid();
    }
    protected void OnRowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        this.BindGrid();
    }
    protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = GridView1.Rows[e.RowIndex];
        Int16 customerId = Convert.ToInt16(GridView1.DataKeys[e.RowIndex].Values[0]);
        string name = (row.FindControl("txtName") as TextBox).Text;
        decimal country = Convert.ToDecimal((row.FindControl("txtCountry") as TextBox).Text);
        string unit = (row.FindControl("txtCountry1") as TextBox).Text;
        Lo.TestGrid("Update", "PRO1", customerId, name, country, unit);
        GridView1.EditIndex = -1;
        this.BindGrid();
    }
    protected void OnRowCancelingEdit(object sender, EventArgs e)
    {
        GridView1.EditIndex = -1;
        this.BindGrid();
    }
    protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Int16 customerId = Convert.ToInt16(GridView1.DataKeys[e.RowIndex].Values[0]);
        Lo.TestGrid("Delete", "", customerId, "", 0, "");

        this.BindGrid();
    }
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != GridView1.EditIndex)
        {
            (e.Row.Cells[3].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";
        }
    }
    protected void OnPaging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        this.BindGrid();
    }
}