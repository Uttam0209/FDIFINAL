using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Encryption;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using System.Web;

public partial class Admin_ViewNodalOfficer : System.Web.UI.Page
{
    Logic Lo = new Logic();
    DataUtility Co = new DataUtility();
    Cryptography objEnc = new Cryptography();
    DataTable DtGrid = new DataTable();
    DataTable DtCompanyDDL = new DataTable();
    private string mType = "";
    private string mRefNo = "";
    PagedDataSource pgsource = new PagedDataSource();
    int firstindex, lastindex;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] != null)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    try
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
                        mType = objEnc.DecryptData(Session["Type"].ToString());
                        mRefNo = Session["CompanyRefNo"].ToString();
                        BindCompany();
                    }
                    catch (Exception ex)
                    {
                        string error = ex.ToString();
                        string Page = Request.Url.AbsolutePath.ToString();
                        Response.Redirect("Error?techerror=" + objEnc.EncryptData(error) + "&page=" + objEnc.EncryptData(Page));
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert",
                        "alert('Session Expired,Please login again');window.location='Login'", true);
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert",
                "alert('Session Expired,Please login again');window.location='Login'", true);
        }
    }
    protected void btnAddNodalOfficer_Click(object sender, EventArgs e)
    {
        string Role = "Company";
        string stridNew = Request.QueryString["id"].ToString().Replace(" ", "+");
        string mstrid = objEnc.EncryptData((objEnc.DecryptData(stridNew)));
        Response.Redirect("Add-Nodal?mrcreaterole=" + objEnc.EncryptData(Role) + "&id=" + mstrid);

    }
    protected void BindEmployee(string mRoleEmployee)
    {
        DataTable DtGrid = Lo.GetDashboardData("Employee", "");
        if (DtGrid.Rows.Count > 0)
        {
            for (int a = 0; a < DtGrid.Rows.Count; a++)
            {
                if (DtGrid.Rows[a]["UCompany"].ToString() != "")
                {
                    DtGrid.Rows[a]["CompanyName"] = DtGrid.Rows[a]["UCompany"];
                    DtGrid.Rows[a]["FactoryName"] = DtGrid.Rows[a]["UFactory"];
                }
                else if (DtGrid.Rows[a]["FCompany"].ToString() != "")
                {
                    DtGrid.Rows[a]["CompanyName"] = DtGrid.Rows[a]["FCompany"];
                }
            }
            DataView dv = new DataView(DtGrid);
            if (mRoleEmployee == "Company")
            {
                dv.RowFilter = "CompanyName='" + ddlcompany.SelectedItem.Text + "'";
            }
            else if (mRoleEmployee == "Division")
            {
                dv.RowFilter = "FactoryName='" + ddldivision.SelectedItem.Text + "'";
            }
            else if (mRoleEmployee == "Unit")
            {
                dv.RowFilter = "UnitName='" + ddlunit.SelectedItem.Text + "'";
            }
            dv.Sort = "CompanyName asc,FactoryName asc";
            DataTable DtAdd = dv.ToTable();
            pgsource.DataSource = DtAdd.DefaultView;
            pgsource.AllowPaging = true;
            pgsource.PageSize = 100;
            pgsource.CurrentPageIndex = pagingCurrentPage;
            ViewState["totpage"] = pgsource.PageCount;
            lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
            lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
            lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
            lnkbtnPgFirst.Enabled = !pgsource.IsFirstPage;
            lnkbtnPgLast.Enabled = !pgsource.IsLastPage;
            pgsource.DataSource = DtAdd.DefaultView;
            gvViewNodalOfficer.DataSource = pgsource;
            gvViewNodalOfficer.DataBind();
            DataListPagingMethod();
            gvViewNodalOfficer.Visible = true;
            divpageindex.Visible = true;
            lbltotal.Text = "Total Records:- " + DtAdd.Rows.Count.ToString();
            divTotalNumber.Visible = true;
        }
        else
            divTotalNumber.Visible = false;
    }
    public void GridViewNodalOfficerBind(string mRefNo, string mRole)
    {
        hfmrole.Value = mRole.ToString();
        BindEmployee(hfmrole.Value);
    }
    protected void BindCompany()
    {
        if (mType == "SuperAdmin" || mType == "Admin")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, "", "", 0, "", "", "Select");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Items.Insert(0, "Select");
                ddlcompany.Enabled = true;
            }
            else
            {
                ddlcompany.Enabled = false;
            }
            lblselectdivison.Visible = false;
            lblselectunit.Visible = false;
        }
        else if (mType == "Company")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                GridViewNodalOfficerBind(mRefNo, "Company");
                ddlcompany.Enabled = false;
            }
            else
            {
                ddlcompany.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                ddldivision.Items.Insert(0, "Select");
                if (mType == "Company")
                {
                    lblselectdivison.Visible = true;
                    ddldivision.Enabled = true;
                    ddlunit.Visible = false;
                    lblselectunit.Visible = false;
                }
                else
                {
                    ddldivision.Enabled = false;
                }
            }
            else
            {
                ddldivision.Enabled = false;
            }
        }
        else if (mType == "Factory" || mType == "Division")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = false;
            }
            else
            {
                ddlcompany.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                // code by gk to select indivisual division for the particular unit
                DataTable dt = Lo.RetriveMasterData(0, mRefNo, "Factory2", 0, "", "", "CompanyName");
                if (dt.Rows.Count > 0)
                    ddldivision.SelectedValue = dt.Rows[0]["FactoryRefNo"].ToString();
                //end code
                lblselectdivison.Visible = true;
                ddldivision.Enabled = false;
                GridViewNodalOfficerBind(ddldivision.SelectedItem.Value, "Division");
            }
            else
            {
                lblselectdivison.Visible = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "Unit1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
                ddlunit.Items.Insert(0, "Select");
                lblselectunit.Visible = true;
                ddlunit.Enabled = true;
            }
            else
            {
                lblselectunit.Visible = false;
            }
        }
        else if (mType == "Unit")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company2", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = false;
            }
            else
            {
                ddlcompany.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                // code by gk to select indivisual division for the particular unit
                DataTable dt = Lo.RetriveMasterData(0, mRefNo, "Factory3", 0, "", "", "CompanyName");
                if (dt.Rows.Count > 0)
                    ddldivision.SelectedValue = dt.Rows[0]["FactoryRefNo"].ToString();
                //end code
                lblselectdivison.Visible = true;
                ddldivision.Enabled = false;
            }
            else
            {
                ddldivision.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "Unit1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
                ddlunit.SelectedValue = mRefNo.ToString();
                lblselectunit.Visible = true;
                ddlunit.Enabled = false;
                GridViewNodalOfficerBind(ddlunit.SelectedItem.Value, "Unit");
            }
            else
            {
                ddlunit.Enabled = false;
            }
        }
    }
    #region RowCommand
    protected void gvViewDesignation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditComp")
        {
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gvr.RowIndex;
            string stridNew = Request.QueryString["id"].ToString().Replace(" ", "+");
            string mstrid = objEnc.EncryptData((objEnc.DecryptData(stridNew)));
            string UrlEdit = "Add-Nodal?mrcreaterole=" + HttpUtility.UrlEncode(objEnc.EncryptData(e.CommandArgument.ToString())) + "&mcurrentcompRefNo=" + HttpUtility.UrlEncode(objEnc.EncryptData(e.CommandArgument.ToString())) + "&id=" + mstrid;
            Response.Redirect(UrlEdit);
        }
        else if (e.CommandName == "ViewComp")
        {
            //DataTable DtView = new DataTable();
            //if (ddldivision.SelectedValue != "Select" && ddldivision.SelectedValue != "")
            //{
            //    DtView = Lo.RetriveAllNodalOfficer(e.CommandArgument.ToString(), "DivisionID");

            //    if (ddlunit.SelectedValue != "Select" && ddlunit.SelectedValue != "")
            //    {
            //        DtView = Lo.RetriveAllNodalOfficer(e.CommandArgument.ToString(), "UnitID");
            //    }
            //}
            //else
            //{
            //    DtView = Lo.RetriveAllNodalOfficer(e.CommandArgument.ToString(), "CompanyID");
            //}
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gvr.RowIndex;
            string Role = (gvViewNodalOfficer.Rows[rowIndex].FindControl("hfnodalrole") as HiddenField).Value;
            if (Role == "Unit")
            {
                Role = "UnitID";
            }
            else if (Role == "Division" || Role == "Factory")
            {
                Role = "DivisionID";
            }
            else if (Role == "Company")
            {
                Role = "CompanyID";
            }
            DataTable DtView = new DataTable();
            DtView = Lo.RetriveAllNodalOfficer(e.CommandArgument.ToString(), Role);
            if (DtView.Rows.Count > 0)
            {
                lblcompanyname.Text = DtView.Rows[0]["CompanyName"].ToString();

                if (Role == "CompanyID")
                {
                    lblDivision.Text = "";
                }
                else
                {
                    lblDivision.Text = DtView.Rows[0]["FactoryName"].ToString();
                }
                if (Role == "DivisionID")
                {
                    lblUnit.Text = "";
                }
                else
                {
                    lblUnit.Text = DtView.Rows[0]["UnitName"].ToString();
                }
                lblNodalOfficerRefNo.Text = DtView.Rows[0]["NodalOfficerRefNo"].ToString();
                lblNodalOficerName.Text = DtView.Rows[0]["NodalOficerName"].ToString();
                //lblDesignation.Text = DtView.Rows[0]["Designation"].ToString();
                lblNodalEmpCode.Text = DtView.Rows[0]["NodalEmpCode"].ToString();
                lblEmail.Text = DtView.Rows[0]["NodalOfficerEmail"].ToString();
                lblMobile.Text = DtView.Rows[0]["NodalOfficerMobile"].ToString();
                lblTelephone.Text = DtView.Rows[0]["NodalOfficerTelephone"].ToString();
                lblFax.Text = DtView.Rows[0]["NodalOfficerFax"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ViewNodalDetail", "showPopup();", true);
            }
        }

        else if (e.CommandName == "DeleteComp")
        {
            try
            {
                string DeleteRec = Lo.DeleteRecord(e.CommandArgument.ToString(), "InActiveCompany");
                if (DeleteRec == "true")
                {
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
                GridViewRow gvrow = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                Label lblnodelrefno = (Label)gvrow.FindControl("lblnodelrefno");
                Label nodelemail = (Label)gvrow.FindControl("nodelemail");
                Label lblnodelname = (Label)gvrow.FindControl("lblnodelname");
                string body;
                using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/GeneratePassword.html")))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{UserName}", lblnodelname.Text);
                body = body.Replace("{refno}", HttpUtility.UrlEncode(objEnc.EncryptData(lblnodelrefno.Text)));
                body = body.Replace("{curid}", Resturl(56));
                SendMail s;
                s = new SendMail();
                s.CreateMail("aeroindia-ddp@gov.in", nodelemail.Text, "Create Password Email", body);
                s.sendMail();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Create password email send successfully.')", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Login detail not send. Some error occured.')", true);
            }
        }

    }
    protected void gvViewNodalOfficer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label s_lblnodalofficer = (Label)e.Row.FindControl("lblnodalofficer");
            Label s_lblnodallogactive = (Label)e.Row.FindControl("lblnodallogactive");
            LinkButton s_lbllogindetail = (LinkButton)e.Row.FindControl("lbllogindetail");
            if (s_lblnodalofficer.Text == "Y")
            {
                e.Row.Attributes.Add("Class", "bg-purple");
                s_lblnodalofficer.Text = "Nodal Officer";
                s_lblnodalofficer.Visible = true;
            }
            else if (s_lblnodallogactive.Text == "Y")
            {
                s_lblnodallogactive.Text = "User";
                s_lblnodallogactive.Visible = true;
            }
            else if (s_lblnodallogactive.Text == "N" && s_lblnodalofficer.Text == "N")
            {
                s_lblnodalofficer.Text = "Employee";
                s_lblnodalofficer.Visible = true;
            }
        }
    }
    #endregion
    #region DropDownList Code
    protected void ddlcompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcompany.SelectedItem.Text != "Select")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "", 0, "", "", "FactoryCompanyID");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                ddldivision.Items.Insert(0, "Select");
                lblselectdivison.Visible = true;
                ddldivision.Visible = true;
                GridViewNodalOfficerBind(ddlcompany.SelectedItem.Value, "Company");
            }
            else
            {
                ddldivision.Visible = false;
                lblselectdivison.Visible = false;
                GridViewNodalOfficerBind(ddlcompany.SelectedItem.Value, "Company");
            }
        }
        else if (ddlcompany.SelectedItem.Text == "Select")
        {
            lblselectdivison.Visible = false;
            lblselectunit.Visible = false;
        }
    }
    protected void ddldivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldivision.SelectedItem.Text != "Select")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "", 0, "", "", "UnitSelectID");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
                ddlunit.Items.Insert(0, "Select");
                ddlunit.Visible = true;
                lblselectunit.Visible = true;
                GridViewNodalOfficerBind(ddldivision.SelectedItem.Value, "Division");
            }
            else
            {
                lblselectunit.Visible = false;
                ddlunit.Visible = false;
                GridViewNodalOfficerBind(ddldivision.SelectedItem.Value, "Division");
            }
        }
        else if (ddldivision.SelectedItem.Text == "Select")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "", 0, "", "", "FactoryCompanyID");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                ddldivision.Items.Insert(0, "Select");
                lblselectdivison.Visible = true;
                ddldivision.Visible = true;
                lblselectdivison.Visible = false;
                GridViewNodalOfficerBind(ddlcompany.SelectedItem.Value, "Division");
            }
        }
    }
    protected void ddlunit_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldivision.SelectedItem.Text == "Select")
        {
            ddldivision_OnSelectedIndexChanged(sender, e);
        }
        else
        {

            GridViewNodalOfficerBind(ddlunit.SelectedItem.Value, "Unit");
        }
    }
    #endregion
    protected void Search_Click(object sender, EventArgs e)
    {
        if (txtserch.Text != "")
        {
            DataTable DtGrid = Lo.SearchResultCompany(Co.RSQandSQLInjection("'%" + txtserch.Text + "%'", "hard"));
            if (DtGrid.Rows.Count > 0)
            {
                gvViewNodalOfficer.DataSource = DtGrid;
                gvViewNodalOfficer.DataBind();
                lbltotal.Text = DtGrid.Rows.Count.ToString();
            }
        }
        else
        {

        }
    }
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
    protected void gvViewNodalOfficer_RowCreated(object sender, GridViewRowEventArgs e)
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
    //------------------------pageindex code--------------//
    protected void lnkbtnPgFirst_Click(object sender, EventArgs e)
    {
        pagingCurrentPage = 0;
        BindEmployee(hfmrole.Value);
    }
    protected void lnkbtnPgPrevious_Click(object sender, EventArgs e)
    {
        pagingCurrentPage -= 1;
        BindEmployee(hfmrole.Value);
    }
    protected void lnkbtnPgNext_Click(object sender, EventArgs e)
    {
        pagingCurrentPage += 1;
        BindEmployee(hfmrole.Value);
    }
    protected void lnkbtnPgLast_Click(object sender, EventArgs e)
    {
        pagingCurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
        BindEmployee(hfmrole.Value);
    }
    protected void DataListPaging_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName.Equals("Newpage"))
        {
            pagingCurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            BindEmployee(hfmrole.Value);
        }
    }
    protected void DataListPaging_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        LinkButton lnkPage = (LinkButton)e.Item.FindControl("Pagingbtn");
        if (lnkPage.CommandArgument.ToString() == pagingCurrentPage.ToString())
        {
            lnkPage.Enabled = false;
        }
    }
    private int pagingCurrentPage
    {
        get
        {
            if (ViewState["pagingCurrentPage"] == null)
            {
                return 0;
            }
            else
            {
                return ((int)ViewState["pagingCurrentPage"]);
            }
        }
        set
        {
            ViewState["pagingCurrentPage"] = value;
        }
    }
    private void DataListPagingMethod()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PageIndex");
        dt.Columns.Add("PageText");
        firstindex = pagingCurrentPage - 100;
        if (pagingCurrentPage > 100)
        {
            lastindex = pagingCurrentPage + 100;
        }
        else
        {
            lastindex = 24;
        }
        if (lastindex > Convert.ToInt32(ViewState["totpage"]))
        {
            lastindex = Convert.ToInt32(ViewState["totpage"]);
            firstindex = lastindex - 24;
        }
        if (firstindex < 0)
        {
            firstindex = 0;
        }
        for (int i = firstindex; i < lastindex; i++)
        {
            DataRow dr = dt.NewRow();
            dr[0] = i;
            dr[1] = i + 1;
            dt.Rows.Add(dr);
        }

        DataListPaging.DataSource = dt;
        DataListPaging.DataBind();
    }
    //end page index---------------------------------------//
}