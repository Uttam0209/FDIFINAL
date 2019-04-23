using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Encryption;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;

public partial class Admin_DetailofMasterCompany : System.Web.UI.Page
{
    Logic Lo = new Logic();
    DataUtility Co = new DataUtility();
    Cryptography objEnc = new Cryptography();
    string UserName;
    string RefNo;
    string UserEmail;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                string id = Request.QueryString["id"].ToString().Replace(" ", "+");
                lblPageName.Text = objEnc.DecryptData(id);
            }
            BindGridView();
        }
    }
    protected void BindGridView(string sortExpression = null)
    {
        try
        {

            DataTable DtGrid = Lo.RetriveGridViewCompany("0", "CompanyMainGridView");
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
                lbltotal.Text = DtGrid.Rows.Count.ToString();
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

            DataTable DtView = Lo.RetriveGridViewCompany(e.CommandArgument.ToString(), "CompanyMainGridView");
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
                //  lblgstno.Text = DtView.Rows[0]["GSTNo"].ToString();
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
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                int rowIndex = gvr.RowIndex;
                RefNo = (gvcompanydetail.Rows[rowIndex].FindControl("lblrefno") as Label).Text;
                UserName = (gvcompanydetail.Rows[rowIndex].FindControl("lblnodelname") as Label).Text;
                UserEmail = (gvcompanydetail.Rows[rowIndex].FindControl("hfemail") as HiddenField).Value;
                SendEmailCode();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Mail not send error occured.')", true);
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

                lbltotal.Text = DtGrid.Rows.Count.ToString();
            }
        }
        else
        {
            BindGridView();
        }
    }
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblrefno = e.Row.FindControl("lblrefno") as Label;
            GridView gvfactory = e.Row.FindControl("gvfactory") as GridView;
            DataTable DtGrid = Lo.RetriveGridViewCompany(lblrefno.Text, "InnerGridViewFactory");
            if (DtGrid.Rows.Count > 0)
            {
                gvfactory.DataSource = DtGrid;
                gvfactory.DataBind();
            }
        }
    }
    protected void gvfactory_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    Label lblfactroyrefno = e.Row.FindControl("lblfactoryrefno") as Label;
        //    GridView gvunit = e.Row.FindControl("gvunit") as GridView;
        //    DataTable DtGrid = Lo.RetriveGridViewCompany(lblfactroyrefno.Text, "InnerGridViewUnit");
        //    if (DtGrid.Rows.Count > 0)
        //    {
        //        gvunit.DataSource = DtGrid;
        //        gvunit.DataBind();
        //    }
        //}
    }
    #region Send Mail
    public void SendEmailCode()
    {
        try
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/GeneratePassword.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", UserName);
            body = body.Replace("{refno}", objEnc.EncryptData(RefNo));
            body = body.Replace("{mcurid}", Resturl(56));
            SendMail s;
            s = new SendMail();
            s.CreateMail("aeroindia-ddp@gov.in", UserEmail, "Create Password", body);
            s.sendMail();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Password change mail send successfully.')", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }

    }
    #endregion
    #region ReturnUrl Long"
    public string Resturl(int length)
    {
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        StringBuilder res = new StringBuilder();
        Random rnd = new Random();
        while (0 < length--)
        {
            res.Append(valid[rnd.Next(valid.Length)]);
        }
        return res.ToString();
    }
    #endregion
}