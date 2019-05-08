using BusinessLayer;
using Encryption;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web;
using System.IO;

public partial class Admin_ViewProduct : System.Web.UI.Page
{
    Logic Lo = new Logic();
    DataUtility Co = new DataUtility();
    Cryptography objEnc = new Cryptography();
    DataTable DtGrid = new DataTable();
    DataTable DtCompanyDDL = new DataTable();
    string UserName;
    string RefNo;
    string UserEmail;
    string currentPage = "";
    private string mType = "";
    private string mRefNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                string strid = Request.QueryString["id"].ToString().Replace(" ", "+");
                string strPageName = objEnc.DecryptData(strid);
                StringBuilder strheadPage = new StringBuilder();
                strheadPage.Append("<ul class='breadcrumb'>");
                string[] MCateg = strPageName.Split(new string[] { ">>" }, StringSplitOptions.RemoveEmptyEntries);
                string MmCval = "";
                for (int x = 0; x < MCateg.Length; x++)
                {
                    MmCval = MCateg[x];
                    strheadPage.Append("<li class=''><span>" + MmCval + "</span></li>");
                }
                divHeadPage.InnerHtml = strheadPage.ToString();
                strheadPage.Append("</ul");
                currentPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
                mType = objEnc.DecryptData(Session["Type"].ToString());
                mRefNo = Session["CompanyRefNo"].ToString();
                BindGridView();
            }
        }
    }
    #region Load
    protected void BindGridView(string sortExpression = null)
    {
        try
        {
            if (mType == "SuperAdmin")
            {
                DataTable DtGrid = Lo.RetriveProductCode("", "", "ProductMaster");
                if (DtGrid.Rows.Count > 0)
                {
                    if (sortExpression != null)
                    {
                        DataView dv = DtGrid.AsDataView();
                        this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";
                        dv.Sort = sortExpression + " " + this.SortDirection;
                        gvproduct.DataSource = dv;
                    }
                    else
                    {
                        gvproduct.DataSource = DtGrid;
                    }
                    gvproduct.DataBind();

                }
            }
            else if (mType == "Company" && mRefNo != "")
            {
                DataTable DtGrid = Lo.RetriveProductCode("", mRefNo, "ProductMasterID");
                if (DtGrid.Rows.Count > 0)
                {
                    gvproduct.DataSource = DtGrid;
                    gvproduct.DataBind();
                }
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
    #endregion
    #region PageIndex or Sorting
    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvproduct.PageIndex = e.NewPageIndex;
        this.BindGridView();
    }
    protected void OnSorting(object sender, GridViewSortEventArgs e)
    {
        this.BindGridView(e.SortExpression);
    }
    #endregion
    #region RowCommand
    protected void gvproduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditComp")
        {
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gvr.RowIndex;
            string Role = (gvproduct.Rows[rowIndex].FindControl("lblcompanyrole") as Label).Text;
            string stridNew = Request.QueryString["id"].ToString().Replace(" ", "+");
            string mstrid = objEnc.EncryptData((objEnc.DecryptData(stridNew) + " >> Edit Company"));
            Response.Redirect("AddProduct?mrcreaterole=" + objEnc.EncryptData(Role) + "&mcurrentcompRefNo=" + (objEnc.EncryptData(e.CommandArgument.ToString())) + "&id=" + mstrid);
        }
        else if (e.CommandName == "ViewComp")
        {
            DataTable DtView = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "CompanyMainGridView");
            if (DtView.Rows.Count > 0)
            {
                lblrefno.Text = DtView.Rows[0]["CompanyRefNo"].ToString();
                lblcompanyname.Text = DtView.Rows[0]["CompanyName"].ToString();
                lbladdress.Text = DtView.Rows[0]["Address"].ToString();
                lblstate.Text = DtView.Rows[0]["StateName"].ToString();
                lblpanno.Text = DtView.Rows[0]["PANNo"].ToString();
                lblpincode.Text = DtView.Rows[0]["Pincode"].ToString();
                lblcinno.Text = DtView.Rows[0]["CINNo"].ToString();
                lblceoname.Text = DtView.Rows[0]["CEOName"].ToString();
                lblceoemail.Text = DtView.Rows[0]["CEOEmail"].ToString();
                lblTelephoneNo.Text = DtView.Rows[0]["TelephoneNo"].ToString();
                lblFaxNo.Text = DtView.Rows[0]["FaxNo"].ToString();
                lblEmailID.Text = DtView.Rows[0]["EmailID"].ToString();
                lblWebsite.Text = DtView.Rows[0]["Website"].ToString();
                lblGSTNo.Text = DtView.Rows[0]["GSTNo"].ToString();
                lblNodalEmail.Text = DtView.Rows[0]["ContactPersonEmailID"].ToString();
                lblNodalOfficerName.Text = DtView.Rows[0]["NodalOficerName"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "changePass", "showPopup();", true);
            }
        }
        else if (e.CommandName == "DeleteComp")
        {
            try
            {
                string DeleteRec = Lo.DeleteRecord(e.CommandArgument.ToString(), "InActiveProd");
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