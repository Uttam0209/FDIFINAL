using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;
using Encryption;
using System.IO;

public partial class Grievance_GAssignJob : System.Web.UI.Page
{
    Logic Lo = new Logic();
    DataTable Dt = new DataTable();
    Cryptography Enc = new Cryptography();
    DataUtility Co = new DataUtility();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["Gid"] != null && Session["GType"] != null)
                { bindAssign(); }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Ticket Found.');", true);
                    Response.Redirect("ADashboard");
                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void bindAssign()
    {
        Dt = Lo.RetriveHelpdesk(0, 0, 0, Enc.DecryptData(Session["Gid"].ToString()), "", "", "", "AssignToQuery");
        if (Dt.Rows.Count > 0)
        {
            dlassignjob.DataSource = Dt;
            dlassignjob.DataBind();
            dlassignjob.Visible = true;
            lbltotalcase.Text = "Total Assign job :- " + Dt.Rows.Count.ToString();
        }
        else
        {
            lbltotalcase.Text = "Total Assign job :- " + Dt.Rows.Count.ToString();
        }
    }
    protected void BindUser()
    {
        DataTable Dtuser = Lo.RetriveHelpdesk(0, 0, 0, Enc.DecryptData(Session["Gid"].ToString()), "", "", "", "UserName1");
        if (Dtuser.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlAssginto, Dtuser, "Name", "Gid");
            ddlAssginto.Items.Insert(0, "Select");
        }
    }
    protected void btnassignto_Click(object sender, EventArgs e)
    {
        if (ddlAssginto.SelectedIndex != -1 && txtcomment.Text != "")
        {
            try
            {
                DataTable UForword = Lo.RetriveHelpdesk(0, 0, 0, ddlAssginto.SelectedItem.Value, txtcomment.Text, hfissuerefno.Value, Enc.DecryptData(Session["Gid"].ToString()), "Updateforword");
                cleartext();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Successfully case forword to " + ddlAssginto.SelectedItem.Text + "');", true);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
    protected void dlassignjob_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "Forword")
        {
            hfissuerefno.Value = e.CommandArgument.ToString();
            BindUser();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modelforword", "showPopup();", true);
        }
        else if (e.CommandName == "Reply")
        {
            hfissuerefno.Value = e.CommandArgument.ToString();
            DataListItem gvr = (DataListItem)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gvr.ItemIndex;
            string email = (dlassignjob.Items[rowIndex].FindControl("hfemail") as HiddenField).Value;
            string sub = (dlassignjob.Items[rowIndex].FindControl("lbsub") as Label).Text;
            string query = (dlassignjob.Items[rowIndex].FindControl("lbissue") as Label).Text;
            string qufpor = (dlassignjob.Items[rowIndex].FindControl("lblqueryfor") as Label).Text;
            Response.Redirect("GUpdateReply?iismref=" + Enc.EncryptData(hfissuerefno.Value) + "&me=" + Enc.EncryptData(email.ToString()) + "&msb=" + Enc.EncryptData(sub.ToString()) + "&mqu=" + Enc.EncryptData(query.ToString()) + "&mque=" + Enc.EncryptData(qufpor.ToString()));
        }
    }
    protected void dlassignjob_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            GridView gvstatus = e.Item.FindControl("gvassignjob") as GridView;
            Label ticketno = e.Item.FindControl("ticketno") as Label;
            DataTable dtassigngrid = Lo.RetriveHelpdesk(0, 0, 0, ticketno.Text, "", "", "", "AssignToQueryGrid");
            gvstatus.DataSource = dtassigngrid;
            gvstatus.DataBind();
            gvstatus.Visible = true;
        }
    }
    protected void cleartext()
    {
        hfissuerefno.Value = ""; txtcomment.Text = ""; ddlAssginto.SelectedIndex = -1;
    }
}