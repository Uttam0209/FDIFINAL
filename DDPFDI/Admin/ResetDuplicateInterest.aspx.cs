using System;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Web.UI;

public partial class Admin_ResetDuplicateInterest : System.Web.UI.Page
{
    Logic Lo = new Logic();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { bindrecored(); }
    }
    protected void bindrecored()
    {
        dt = Lo.NewRetriveFilterCode("getdupint", "", "", "", "", 0, 0, 0);
        if (dt.Rows.Count > 0)
        {
            gvViewDesignationSave.DataSource = dt;
            gvViewDesignationSave.DataBind();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Duplicate Record Found.')", true);
        }
    }

    protected void gvViewDesignationSave_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "PRef")
        {
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvViewDesignationSave.Rows[rowIndex];
                dt = Lo.NewRetriveFilterCode("deldupint", row.Cells[1].Text, e.CommandArgument.ToString(), "", "", 0, 0, 0);
                if (dt.Rows.Count == 0)
                {
                    bindrecored();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Successfully updated')", true);
                }
                else
                { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Oops some error')", true); }
            }
            catch (Exception ex)
            { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true); }
        }
    }
}