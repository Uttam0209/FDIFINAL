using BusinessLayer;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;

public partial class Grievance_G_ViewTicketsSingleClick : System.Web.UI.Page
{
    #region Pagevariable
    private Logic Lo = new Logic();
    DataTable dtnew = new DataTable();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { }
    }
    protected void lbstatus_Click(object sender, EventArgs e)
    {
        DataTable DtStatus = Lo.RetriveHelpdesk(0, 0, 0, txtrefeno.Text, "", "", "", "RTrack");
        if (DtStatus.Rows.Count > 0)
        {
            try
            {
                dlissue.DataSource = DtStatus;
                dlissue.DataBind();
                divtrack.Visible = true;
                if (DtStatus.Rows[0]["Status"].ToString() == "1")
                {
                    step1.Attributes.Add("Class", "step active"); step2.Attributes.Add("Class", "step"); step3.Attributes.Add("Class", "step"); step4.Attributes.Add("Class", "step");
                }
                if (DtStatus.Rows[0]["Status"].ToString() == "2")
                {
                    step1.Attributes.Add("Class", "step active"); step2.Attributes.Add("Class", "step active"); step3.Attributes.Add("Class", "step"); step4.Attributes.Add("Class", "step");
                }
                if (DtStatus.Rows[0]["Status"].ToString() == "3")
                {
                    step1.Attributes.Add("Class", "step active"); step2.Attributes.Add("Class", "step active"); step3.Attributes.Add("Class", "step active"); step4.Attributes.Add("Class", "step");
                }
                if (DtStatus.Rows[0]["Status"].ToString() == "4")
                {
                    step1.Attributes.Add("Class", "step active"); step2.Attributes.Add("Class", "step active"); step3.Attributes.Add("Class", "step active"); step4.Attributes.Add("Class", "step active");
                }
            }
            catch (Exception ex)
            { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Technical error:- " + ex.Message + " User Error:- No Record Found.')", true); }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found.')", true);
        }
    }
}