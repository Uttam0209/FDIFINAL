using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

public partial class Admin_UpdateCatstatus : System.Web.UI.Page
{
    Logic Lo = new Logic();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { Bind(); }
    }
    protected void rbtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind();
    }
    protected void Bind()
    {
        DataTable dtCat = new DataTable();
        DataTable dtSubCat = new DataTable();
        if (rbtype.SelectedItem.Value == "1")
        {
            dtCat = Lo.RetriveCountry(0, "Cat");
            if (dtCat.Rows.Count > 0)
            {
                gvmastercategoryupdate.DataSource = dtCat;
                gvmastercategoryupdate.DataBind();
                gvmastercategoryupdate.Visible = true;
                gvmastersubcategory.Visible = false;
            }
            else
            {
                gvmastercategoryupdate.Visible = false;
                gvmastersubcategory.Visible = false;
            }
        }
        else if (rbtype.SelectedItem.Value == "2")
        {
            dtSubCat = Lo.RetriveCountry(0, "SubCat1");
            if (dtSubCat.Rows.Count > 0)
            {
                gvmastersubcategory.DataSource = dtSubCat;
                gvmastersubcategory.DataBind();
                gvmastersubcategory.Visible = true;
                gvmastercategoryupdate.Visible = false;
            }
            else
            {
                gvmastersubcategory.Visible = false;
                gvmastercategoryupdate.Visible = false;
            }
        }
        else if (rbtype.SelectedItem.Value == "3")
        {
            dtSubCat = Lo.RetriveCountry(0, "SubCat");
            if (dtSubCat.Rows.Count > 0)
            {
                gvmastersubcategory.DataSource = dtSubCat;
                gvmastersubcategory.DataBind();
                gvmastersubcategory.Visible = true;
                gvmastercategoryupdate.Visible = false;
            }
            else
            {
                gvmastersubcategory.Visible = false;
                gvmastercategoryupdate.Visible = false;
            }
        }
    }
    protected void gvmastercategoryupdate_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void gvmastersubcategory_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void gvmastercategoryupdate_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "active")
        {
            string UpdateCatActive = Lo.UpdateStatus(Convert.ToInt64(e.CommandArgument.ToString()), "", "upyCat");
            if (UpdateCatActive != "-1")
            {
                Bind();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Successfully Updated')", true);
            }
            else
            { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Oops some error occured.')", true); }
        }
        else if (e.CommandName == "deactive")
        {
            string UpdateCatActive = Lo.UpdateStatus(Convert.ToInt64(e.CommandArgument.ToString()), "", "upnCat");
            if (UpdateCatActive != "-1")
            {
                Bind();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Successfully Updated')", true);
            }
            else
            { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Oops some error occured.')", true); }
        }
    }
    protected void gvmastersubcategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "subactive")
        {
            string UpdateCatActive = Lo.UpdateStatus(Convert.ToInt64(e.CommandArgument.ToString()), "", "upysubCat");
            if (UpdateCatActive != "-1")
            {
                Bind();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Successfully Updated')", true);
            }
            else
            { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Oops some error occured.')", true); }
        }
        else if (e.CommandName == "subdeactive")
        {
            string UpdateCatActive = Lo.UpdateStatus(Convert.ToInt64(e.CommandArgument.ToString()), "", "upnsubCat");
            if (UpdateCatActive != "-1")
            {
                Bind();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Successfully Updated')", true);
            }
            else
            { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Oops some error occured.')", true); }
        }
    }
    protected void gvmastercategoryupdate_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[2].Text == "Y")
            {
                LinkButton lblactive = (e.Row.FindControl("lbactive") as LinkButton);
                lblactive.Visible = false;
                LinkButton lblunactive = (e.Row.FindControl("lbunactive") as LinkButton);
                lblunactive.Visible = true;
            }
            else
            {
                LinkButton lblactive = (e.Row.FindControl("lbactive") as LinkButton);
                lblactive.Visible = true;
                LinkButton lblunactive = (e.Row.FindControl("lbunactive") as LinkButton);
                lblunactive.Visible = false;
            }
        }
    }
    protected void gvmastersubcategory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[3].Text == "Y")
            {
                LinkButton lblsubactive = (e.Row.FindControl("lbsubactive") as LinkButton);
                lblsubactive.Visible = false;
                LinkButton lblsubunactive = (e.Row.FindControl("lbsubunactive") as LinkButton);
                lblsubunactive.Visible = true;
            }
            else
            {
                LinkButton lblsubactive = (e.Row.FindControl("lbsubactive") as LinkButton);
                lblsubactive.Visible = true;
                LinkButton lblsubunactive = (e.Row.FindControl("lbsubunactive") as LinkButton);
                lblsubunactive.Visible = false;
            }
        }
    }
}