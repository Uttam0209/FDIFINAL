using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Encryption;
using System.Data;

public partial class DetailofMasterCompany : System.Web.UI.Page
{
    Logic Lo = new Logic();
    DataUtility Co = new DataUtility();
    Cryptography objEnc = new Cryptography();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridView();
        }
    }
    protected void BindGridView(string sortExpression = null)
    {
        try
        {
            DataTable DtGrid = Lo.RetriveGridViewCompany(0);
            if (DtGrid.Rows.Count > 0)
            {
                if (sortExpression != null)
                {
                    DataView dv = DtGrid.AsDataView();
                    this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                    dv.Sort = sortExpression + " " + this.SortDirection;
                    gvcompanydetail.DataSource = dv;
                }
                else
                {
                    gvcompanydetail.DataSource = DtGrid;
                }
                gvcompanydetail.DataBind();
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
    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvcompanydetail.PageIndex = e.NewPageIndex;
        this.BindGridView();
    }
    protected void OnSorting(object sender, GridViewSortEventArgs e)
    {
        this.BindGridView(e.SortExpression);
    }
    protected void gvcompanydetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditComp")
        {
            Response.Redirect("Add-Company?mpath" + objEnc.EncryptData("CompanyNoOneCanTraceITCauseItValidatate") + "&mcurrentID=" + (objEnc.EncryptData(e.CommandArgument.ToString())));
        }
        else if (e.CommandName == "ViewComp")
        {
            DataTable DtView = Lo.RetriveGridViewCompany(Convert.ToInt64(e.CommandArgument.ToString()));
            if (DtView.Rows.Count > 0)
            {
                lblrefno.Text = DtView.Rows[0]["CompanyRefNo"].ToString();
                lbladdress.Text = DtView.Rows[0]["Address"].ToString();
                lblcinno.Text = DtView.Rows[0]["CINNo"].ToString();
                lblceoname.Text = DtView.Rows[0]["CEOName"].ToString();
                lblcompanyname.Text = DtView.Rows[0]["CompanyName"].ToString();
                lblcontactperemailid.Text = DtView.Rows[0]["ContactPersonEmailID"].ToString();
                lblcontactpersonmobno.Text = DtView.Rows[0]["ContactPersonContactNo"].ToString();
                lblcontactpersonname.Text = DtView.Rows[0]["ContactPersonName"].ToString();
                lblceoemail.Text = DtView.Rows[0]["CEOEmail"].ToString();
                lbldefactivity.Text = DtView.Rows[0]["IsDefenceActivity"].ToString();
                lblgstno.Text = DtView.Rows[0]["GSTNo"].ToString();
                lbljointventure.Text = DtView.Rows[0]["IsJointVenture"].ToString();
                lblpanno.Text = DtView.Rows[0]["PANNo"].ToString();
                lblpincode.Text = DtView.Rows[0]["Pincode"].ToString();
                lblstate.Text = DtView.Rows[0]["StateName"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "changePass", "showPopup();", true);
            }
        }
        else if (e.CommandName == "DeleteComp")
        {
            try
            {
                string DeleteRec = Lo.DeleteRecord(Convert.ToInt64(e.CommandArgument.ToString()));
                if (DeleteRec == "true")
                {
                    BindGridView();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record delete succssfully.')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not deleted.')", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not deleted.')", true);
            }
        }
        else if (e.CommandName == "SendLogin")
        {
            try
            {
                //string DeleteRec = Lo.DeleteRecord(Convert.ToInt64(e.CommandArgument.ToString()));
                //if (DeleteRec == "true")
                //{
                //    BindGridView();
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record delete succssfully.')", true);
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not deleted.')", true);
                //}
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not deleted.')", true);
            }
        }
    }
    protected void Search_Click(object sender, EventArgs e)
    {
        if (txtserch.Text != "")
        {
            DataTable DtGrid = Lo.SearchResultCompany(Co.RSQandSQLInjection("'%" + txtserch.Text + "%'", "hard"));
            if (DtGrid.Rows.Count > 0)
            {
                gvcompanydetail.DataSource = DtGrid;
                gvcompanydetail.DataBind();
            }
        }
        else
        {
            BindGridView();
        }
    }
}