using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;
using Encryption;

public partial class Grievance_G_Dashboard : System.Web.UI.Page
{
    Logic Lo = new Logic();
    DataTable DtDash = new DataTable();
    Cryptography enc = new Cryptography();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (enc.DecryptData(Session["GType"].ToString()) != "" && enc.DecryptData(Session["GUser"].ToString()) != "")
            {
                BindDashboard();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Session Expired.');window.location('GOfficalLogin')'", true);
            }
        }
    }
    protected void BindDashboard()
    {
        DtDash = Lo.RetriveHelpdesk(0, 0, 0, enc.DecryptData(Session["GType"].ToString()), enc.DecryptData(Session["GUser"].ToString()), "", "", "ADashboard");
        if (DtDash.Rows.Count > 0)
        {
            lnkTotalTeam.Text = DtDash.Rows[0]["TotalTeam"].ToString();
            lnkTotalCase.Text = DtDash.Rows[0]["TotalCase"].ToString();
            lnkTotalIssue.Text = DtDash.Rows[0]["TotalIssue"].ToString();
            lnkFeedback.Text = DtDash.Rows[0]["TotalFeedBack"].ToString();
            lnkResolvedIssue.Text = DtDash.Rows[0]["ResolvedIssue"].ToString();
            lnkIssueInProgress.Text = DtDash.Rows[0]["IssueInProgress"].ToString();
            lnkClose.Text = DtDash.Rows[0]["Issueclose"].ToString();
            //////////////////////First
            lblcaseonhelpdesk.Text = DtDash.Rows[0]["TotalTaskHelpdesk"].ToString();
            lbondeveloper.Text = DtDash.Rows[0]["TotalTaskDeveloper"].ToString();
            lbonmanager.Text = DtDash.Rows[0]["TotalTaskManager"].ToString();
            ////////////////////////////////Two
            lblopentoday.Text = "Total Open Issue/Feedback Today :- " + DtDash.Rows[0]["TotalOpenToday"].ToString();
            lblclosetoday.Text = "Total Close Issue/Feedback Today :- " + DtDash.Rows[0]["TotalCloseToday"].ToString();
            lblinprogresstoday.Text = "Total Pending Issue/Feedback Today :- " + DtDash.Rows[0]["TotalPendingToday"].ToString();
            BindTotalCase();
        }
    }
    protected void BindTotalCase()
    {
        DataTable DtCase = Lo.RetriveHelpdesk(0, 0, 0, enc.DecryptData(Session["GType"].ToString()), enc.DecryptData(Session["GUser"].ToString()), "", "", "Case");
        if (DtCase.Rows.Count > 0)
        {
            gvcase.DataSource = DtCase;
            gvcase.DataBind();
            lbMore.Visible = true;
        }
        else
        {
            lbMore.Visible = false;
        }
    }

    protected void BindTicketGenrate()
    {
        try
        {
            DataTable DtCase = Lo.RetriveHelpdesk(0, 0, 0, enc.DecryptData(Session["GType"].ToString()), enc.DecryptData(Session["GUser"].ToString()), "", "", "TicketGenrate");
            if (DtCase.Rows.Count > 0)
            {
                gvcase.DataSource = DtCase;
                gvcase.DataBind();
                lbMore.Visible = true;
            }
            else
            {
                lbMore.Visible = false;
            }
        }
        catch (Exception ex)
        { }
    }

    protected void BindTicketInProcess()
    {
        DataTable DtCase = Lo.RetriveHelpdesk(0, 0, 0, enc.DecryptData(Session["GType"].ToString()), enc.DecryptData(Session["GUser"].ToString()), "", "", "TicketInProcess");
        if (DtCase.Rows.Count > 0)
        {
            gvcase.DataSource = DtCase;
            gvcase.DataBind();
            lbMore.Visible = true;
        }
        else
        {
            lbMore.Visible = false;
        }
    }

    protected void BindTicketClose()
    {
        DataTable DtCase = Lo.RetriveHelpdesk(0, 0, 0, enc.DecryptData(Session["GType"].ToString()), enc.DecryptData(Session["GUser"].ToString()), "", "", "CloseTicket");
        if (DtCase.Rows.Count > 0)
        {
            gvcase.DataSource = DtCase;
            gvcase.DataBind();
            lbMore.Visible = true;
        }
        else
        {
            lbMore.Visible = false;
        }
    }

    protected void BindTotalTicket()
    {
        DataTable DtCase = Lo.RetriveHelpdesk(0, 0, 0, enc.DecryptData(Session["GType"].ToString()), enc.DecryptData(Session["GUser"].ToString()), "", "", "TotalTicket");
        if (DtCase.Rows.Count > 0)
        {
            gvcase.DataSource = DtCase;
            gvcase.DataBind();
            lbMore.Visible = true;
        }
        else
        {
            lbMore.Visible = false;
        }
    }
    protected void BindTotalIssue()
    {
        DataTable DtCase = Lo.RetriveHelpdesk(0, 0, 0, enc.DecryptData(Session["GType"].ToString()), enc.DecryptData(Session["GUser"].ToString()), "", "", "TotalIssue");
        if (DtCase.Rows.Count > 0)
        {
            gvcase.DataSource = DtCase;
            gvcase.DataBind();
            lbMore.Visible = true;
        }
        else
        {
            lbMore.Visible = false;
        }
    }

    
 protected void BindTotalFeedback()
    {
        DataTable DtCase = Lo.RetriveHelpdesk(0, 0, 0, enc.DecryptData(Session["GType"].ToString()), enc.DecryptData(Session["GUser"].ToString()), "", "", "TotalFeedback");
        if (DtCase.Rows.Count > 0)
        {
            gvcase.DataSource = DtCase;
            gvcase.DataBind();
            lbMore.Visible = true;
        }
        else
        {
            lbMore.Visible = false;
        }
    }
    protected void lbMore_Click(object sender, EventArgs e)
    {
        Response.Redirect("GViewRecords?msession=" + Session["GType"].ToString() + "&type=" + DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss:tt") + "&mu=" + Session["GUser"].ToString());
    }

    protected void lnkResolvedIssue_Click(object sender, EventArgs e)
    {
        BindTicketGenrate();
    }

    protected void lnkIssueInProgress_Click(object sender, EventArgs e)
    {
        BindTicketInProcess();
    }

    protected void lnkClose_Click(object sender, EventArgs e)
    {
        BindTicketClose();
    }

    protected void lblcaseonhelpdesk_Click(object sender, EventArgs e)
    {

    }

    protected void lbondeveloper_Click(object sender, EventArgs e)
    {

    }

    protected void lbonmanager_Click(object sender, EventArgs e)
    {

    }

    protected void lnkFeedback_Click(object sender, EventArgs e)
    {
        BindTotalFeedback();
    }

    protected void lnkTotalIssue_Click(object sender, EventArgs e)
    {
        BindTotalIssue();
    }

    protected void lnkTotalCase_Click(object sender, EventArgs e)
    {
        BindTotalTicket();


    }

    protected void lnkTotalTeam_Click(object sender, EventArgs e)
    {

    }
}