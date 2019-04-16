using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Encryption;
using System.Data;

public partial class Admin_DetailFDIRegistration : System.Web.UI.Page
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
            DataTable DtGrid = Lo.RetriveGridView("");
            if (DtGrid.Rows.Count > 0)
            {
                if (sortExpression != null)
                {
                    DataView dv = DtGrid.AsDataView();
                    this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                    dv.Sort = sortExpression + " " + this.SortDirection;
                    gvdetail.DataSource = dv;
                }
                else
                {
                    gvdetail.DataSource = DtGrid;
                }
                gvdetail.DataBind();

                double total = 0;
                for (int i = 0; i < DtGrid.Rows.Count; i++)
                {

                    total = total + Convert.ToDouble(DtGrid.Rows[i]["ExchangeTotalAmount"].ToString());
                }

                lbltotal.Text = Math.Round(total, 0).ToString("N0");

                
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
        gvdetail.PageIndex = e.NewPageIndex;
        this.BindGridView();
    }
    protected void OnSorting(object sender, GridViewSortEventArgs e)
    {
        this.BindGridView(e.SortExpression);
    }
    protected void gvdetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            Response.Redirect("FDIRegistration?mpath" + objEnc.EncryptData("NoOneCanTraceIT") + "&mcurrentID=" + (objEnc.EncryptData(e.CommandArgument.ToString())));
        }
        else if (e.CommandName == "View")
        {
            DataTable DtView = Lo.RetriveGridView(e.CommandArgument.ToString());
            if (DtView.Rows.Count > 0)
            {
                DateTime date = new DateTime();
                lbladdress.Text = DtView.Rows[0]["Address"].ToString();
                lbladdress1.Text = DtView.Rows[0]["Address"].ToString();
                if (DtView.Rows[0]["ApprovalDate"].ToString() != "")
                {
                    date = Convert.ToDateTime(DtView.Rows[0]["ApprovalDate"].ToString());
                }
                lblapprovaldate.Text = date.ToString("dd-MMM-yyyy");
                lblaprrovalno.Text = DtView.Rows[0]["ApprovalNo"].ToString();
                lblauthinfo.Text = DtView.Rows[0]["AuthencityofInformation"].ToString();
                lblcaseof.Text = DtView.Rows[0]["InCaseOf"].ToString();
                lblcinno.Text = DtView.Rows[0]["CINNo"].ToString();
                lblcodeofbuis.Text = DtView.Rows[0]["NicCode"].ToString();
                lblcompanyname.Text = DtView.Rows[0]["CompanyName"].ToString();
                lblcontactperemailid.Text = DtView.Rows[0]["ContactPersonEmailID"].ToString();
                lblcontactpersonmobno.Text = DtView.Rows[0]["ContactPersonContactNo"].ToString();
                lblcontactpersonname.Text = DtView.Rows[0]["ContactPersonName"].ToString();
                lblcountry.Text = DtView.Rows[0]["CountryName"].ToString();
                lblcureency.Text = DtView.Rows[0]["Currency"].ToString();
                lbldefactivity.Text = DtView.Rows[0]["IsDefenceActivity"].ToString();
                lblceoname.Text = DtView.Rows[0]["CEOName"].ToString();
                lbleqinr.Text = DtView.Rows[0]["EquINRExchangeRate"].ToString();
                lblfdivalue.Text = DtView.Rows[0]["FDIValueType"].ToString();
                lblfordefactivity.Text = DtView.Rows[0]["ForeignDefenceActivity"].ToString();
                lblforeigncompname.Text = DtView.Rows[0]["ForeignCompanyName"].ToString();
               // lblgstno.Text = DtView.Rows[0]["GSTNo"].ToString();
                lblceoemail.Text = DtView.Rows[0]["CEOEmail"].ToString();
                lbljointventure.Text = DtView.Rows[0]["IsJointVenture"].ToString();
                lblpanno.Text = DtView.Rows[0]["PANNo"].ToString();
                lblperiodofreprot.Text = DtView.Rows[0]["PeriodofReporting"].ToString();
                lblpincode.Text = DtView.Rows[0]["Pincode"].ToString();
                //lblrecinfo.Text = DtView.Rows[0]["RecInsTime"].ToString();
                lblremark.Text = DtView.Rows[0]["Remarks"].ToString();
                lblsourceinfo.Text = DtView.Rows[0]["SourceofInformation"].ToString();
                lblstate.Text = DtView.Rows[0]["StateName"].ToString();
                lbltotalfdiinflow.Text = DtView.Rows[0]["TotalFDIInFlow"].ToString();
                lbltotamount.Text = DtView.Rows[0]["ExchangeTotalAmount"].ToString();
                lblzipcode.Text = DtView.Rows[0]["ZipCode"].ToString();
                linkpath.HRef = "CompDocument/" + DtView.Rows[0]["DocumentAttach"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "changePass", "showPopup();", true);
            }
        }
        else if (e.CommandName == "Delete")
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
    }
    protected void Search_Click(object sender, EventArgs e)
    {
        if (txtserch.Text != "")
        {
            DataTable DtGrid = Lo.SearchResult(Co.RSQandSQLInjection("'%" + txtserch.Text + "%'", "hard"));
            if (DtGrid.Rows.Count > 0)
            {
                gvdetail.DataSource = DtGrid;
                gvdetail.DataBind();
                double total=0;
                for (int i = 0; i < DtGrid.Rows.Count; i++)
                {

                    total = total + Convert.ToDouble(DtGrid.Rows[i]["ExchangeTotalAmount"].ToString());
                }

                lbltotal.Text = Math.Round(total, 0).ToString("N0");
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No record found.')", true);
            }
        }
        else
        {
            BindGridView();
        }
    }
}