﻿using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Encryption;
using System.Data;
using System.Text;
using System.Web;

public partial class Admin_ViewDesignation : System.Web.UI.Page
{
    Logic Lo = new Logic();
    DataUtility Co = new DataUtility();
    Cryptography objEnc = new Cryptography();
    DataTable DtGrid = new DataTable();
    DataTable DtCompanyDDL = new DataTable();
    //  private string mType="";
    private string mRefNo;
    PagedDataSource pgsource = new PagedDataSource();
    int firstindex, lastindex;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Type"] != null)
        {
            if (!IsPostBack)
            {
                if (Session["User"] != null)
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
                        hfmtype.Value = objEnc.DecryptData(Session["Type"].ToString());
                        mRefNo = Session["CompanyRefNo"].ToString();
                        BindCompany();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert",
                        "alert('Session Expired,Please login again');window.location='Login'", true);
                }
            }
        }
    }
    protected void BindCompany()
    {
        if (hfmtype.Value == "SuperAdmin" || hfmtype.Value == "Admin")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, "", "", 0, "", "", "Select");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                if (!IsPostBack)
                {
                    Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                    ddlcompany.Items.Insert(0, "Select");
                    ddlcompany.Enabled = true;
                }
                if (ddlcompany.SelectedItem.Text != "Select")
                {
                    DataTable DtGrid = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "", 0, "", "", "ViewDesignation");
                    if (DtGrid.Rows.Count > 0)
                    {
                        BindGrid(DtGrid);
                    }
                    else
                    {
                        divpageindex.Visible = false;
                        gvViewDesignation.Visible = false;

                    }
                }
            }
        }
        else if (hfmtype.Value == "Company")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                if (!IsPostBack)
                {
                    Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                    ddlcompany.Enabled = false;
                }
                DataTable DtGrid = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "", 0, "", "", "ViewDesignation");
                if (DtGrid.Rows.Count > 0)
                {
                    BindGrid(DtGrid);
                }
                else
                {
                    divpageindex.Visible = false;
                    gvViewDesignation.Visible = false;

                }
            }
            else
            {
                ddlcompany.Enabled = false;
            }
        }
    }
    protected void BindGrid(DataTable DtGrid)
    {
        DataTable dtads = DtGrid;
        pgsource.DataSource = dtads.DefaultView;
        pgsource.AllowPaging = true;
        pgsource.PageSize = 100;
        pgsource.CurrentPageIndex = pagingCurrentPage;
        ViewState["totpage"] = pgsource.PageCount;
        lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
        lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
        lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
        lnkbtnPgFirst.Enabled = !pgsource.IsFirstPage;
        lnkbtnPgLast.Enabled = !pgsource.IsLastPage;
        pgsource.DataSource = dtads.DefaultView;
        gvViewDesignation.DataSource = pgsource;// DtGrid;
        gvViewDesignation.DataBind();
        DataListPagingMethod();
        gvViewDesignation.Visible = true;
        divpageindex.Visible = true;
    }
    #region RowCommand
    protected void gvViewDesignation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditComp")
        {
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gvr.RowIndex;
            string stridNew = Request.QueryString["id"].ToString().Replace(" ", "+");
            string mstrid = objEnc.EncryptData((objEnc.DecryptData(stridNew) + " >> Edit Designation"));
            Response.Redirect("Add-Designation?mrcreaterole=" + HttpUtility.UrlEncode(objEnc.EncryptData(e.CommandArgument.ToString())) + "&mcurrentcompRefNo=" + HttpUtility.UrlEncode(objEnc.EncryptData(e.CommandArgument.ToString())) + "&id=" + mstrid);
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

    }
    #endregion
    protected void btnAddDesignation_Click(object sender, EventArgs e)
    {
        string Role = "Company";
        string stridNew = Request.QueryString["id"].ToString().Replace(" ", "+");
        string mstrid = objEnc.EncryptData((objEnc.DecryptData(stridNew)));
        Response.Redirect("Add-Designation?mrcreaterole=" + objEnc.EncryptData(Role) + "&id=" + mstrid);

    }
    #region DropDownList Code
    protected void ddlcompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcompany.SelectedItem.Text != "Select")
        {
            DataTable DtGrid = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "", 0, "", "", "ViewDesignation");
            if (DtGrid.Rows.Count > 0)
            {
                BindGrid(DtGrid);
            }
            else
            {
                divpageindex.Visible = false;
                gvViewDesignation.Visible = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not found !')", true);
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
                gvViewDesignation.DataSource = DtGrid;
                gvViewDesignation.DataBind();
                lbltotal.Text = DtGrid.Rows.Count.ToString();
            }
            else
            {
                divpageindex.Visible = false;
                gvViewDesignation.Visible = false;
            }
        }
        else
        {
        }
    }
    #endregion
    protected void gvViewDesignation_RowCreated(object sender, GridViewRowEventArgs e)
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
        BindCompany();
    }
    protected void lnkbtnPgPrevious_Click(object sender, EventArgs e)
    {
        pagingCurrentPage -= 1;
        BindCompany();
    }
    protected void lnkbtnPgNext_Click(object sender, EventArgs e)
    {
        pagingCurrentPage += 1;
        BindCompany();
    }
    protected void lnkbtnPgLast_Click(object sender, EventArgs e)
    {
        pagingCurrentPage = (Convert.ToInt32(ViewState["totpage"]) - 1);
        BindCompany();
    }
    protected void DataListPaging_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName.Equals("Newpage"))
        {
            pagingCurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            BindCompany();
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