using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Encryption;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using System.Threading;

public partial class Admin_ViewCategory : System.Web.UI.Page
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
            if (Session["Type"] != null)
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
                }
                currentPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
                currentPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
                mType = objEnc.DecryptData(Session["Type"].ToString());
                mRefNo = Session["CompanyRefNo"].ToString();
                //  BindCompany();
                BindGridView();
                BindMasterCategory();
            }
        }
        else
        {

        }
    }
    #region Load
    protected void BindGridView(string sortExpression = null)
    {
        try
        {
            if (mType == "SuperAdmin" || mType == "Admin")
            {
                DataTable DtGrid = Lo.RetriveMasterCategoryDate(0, "", "", "", "", "SelectAll","");
                if (DtGrid.Rows.Count > 0)
                {
                    if (sortExpression != null)
                    {
                        DataView dv = DtGrid.AsDataView();
                        this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";
                        dv.Sort = sortExpression + " " + this.SortDirection;
                        gvCategory.DataSource = dv;
                    }
                    else
                    {
                        gvCategory.DataSource = DtGrid;
                    }
                    gvCategory.DataBind();
                }
            }
            else if (mType == "Company" && mRefNo != "")
            {
                DataTable DtGrid = Lo.RetriveMasterSubCategoryDate(0, "", "", "SubCompCat", mRefNo,"");
                if (DtGrid.Rows.Count > 0)
                {
                    if (sortExpression != null)
                    {
                        DataView dv = DtGrid.AsDataView();
                        this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";
                        dv.Sort = sortExpression + " " + this.SortDirection;
                        gvCategory.DataSource = dv;
                    }
                    else
                    {
                        gvCategory.DataSource = DtGrid;
                    }
                    gvCategory.DataBind();
                }
            }
            else if (mType == "Factroy" && mRefNo != "")
            {
                DataTable DtGrid = Lo.RetriveMasterSubCategoryDate(0, "", "", "SubCompCat", mRefNo,"");
                if (DtGrid.Rows.Count > 0)
                {
                    if (sortExpression != null)
                    {
                        DataView dv = DtGrid.AsDataView();
                        this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";
                        dv.Sort = sortExpression + " " + this.SortDirection;
                        gvCategory.DataSource = dv;
                    }
                    else
                    {
                        gvCategory.DataSource = DtGrid;
                    }
                    gvCategory.DataBind();
                }
            }
            else if (mType == "Unit" && mRefNo != "")
            {
                DataTable DtGrid = Lo.RetriveMasterSubCategoryDate(0, "", "", "SubCompCat", mRefNo,"");
                if (DtGrid.Rows.Count > 0)
                {
                    if (sortExpression != null)
                    {
                        DataView dv = DtGrid.AsDataView();
                        this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";
                        dv.Sort = sortExpression + " " + this.SortDirection;
                        gvCategory.DataSource = dv;
                    }
                    else
                    {
                        gvCategory.DataSource = DtGrid;
                    }
                    gvCategory.DataBind();
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
        gvCategory.PageIndex = e.NewPageIndex;
        this.BindGridView();
    }
    protected void OnSorting(object sender, GridViewSortEventArgs e)
    {
        this.BindGridView(e.SortExpression);
    }
    #endregion
    #region RowDatabound
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (mType == "SuperAdmin")
            {
                HiddenField lblCatId = e.Row.FindControl("hfcat") as HiddenField;
                GridView gvSubcat = e.Row.FindControl("gvsubcatlevel1") as GridView;
                DataTable DtGrid = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(lblCatId.Value), "", "", "SelectInnerMaster", "","");
                if (DtGrid.Rows.Count > 0)
                {
                    gvSubcat.DataSource = DtGrid;
                    gvSubcat.DataBind();
                }
            }
            else if (mType != "SuperAdmin")
            {
                HiddenField lblCatId = e.Row.FindControl("hfcat") as HiddenField;
                GridView gvSubcat = e.Row.FindControl("gvsubcatlevel1") as GridView;
                DataTable DtGrid = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(lblCatId.Value), "", "", "MasterCatComp1", "","");
                if (DtGrid.Rows.Count > 0)
                {
                    gvSubcat.DataSource = DtGrid;
                    gvSubcat.DataBind();
                }
            }
            GridView gvsubcatlevel1 = e.Row.FindControl("gvsubcatlevel1") as GridView;
            gvsubcatlevel1.RowCommand += new GridViewCommandEventHandler(gvsubcatlevel1_RowCommand);
        }
    }
    protected void gvsubcatlevel1_OnRowCommand(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField lblfactroyrefno = e.Row.FindControl("hfcatlevel1id") as HiddenField;
            GridView gvsubcatlevel2 = e.Row.FindControl("gvsubcatlevel2") as GridView;
            DataTable DtGrid = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(lblfactroyrefno.Value), "", "", "SubSelectID", "","");
            if (DtGrid.Rows.Count > 0)
            {
                gvsubcatlevel2.DataSource = DtGrid;
                gvsubcatlevel2.DataBind();
            }
            GridView gvsublevel2 = e.Row.FindControl("gvsubcatlevel2") as GridView;
            gvsublevel2.RowCommand += new GridViewCommandEventHandler(gvsublevel2_RowCommand);
        }
    }
    protected void gvsubcatlevel2_OnRowCommand(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField lbllevel3 = e.Row.FindControl("hfcatlevel2") as HiddenField;
            GridView grdlevel3 = e.Row.FindControl("gvlevel3") as GridView;
            DataTable DtGrid = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(lbllevel3.Value), "", "", "SubSelectID", "","");
            if (DtGrid.Rows.Count > 0)
            {
                grdlevel3.DataSource = DtGrid;
                grdlevel3.DataBind();
            }
            GridView gvlevel3 = e.Row.FindControl("gvlevel3") as GridView;
            gvlevel3.RowCommand += new GridViewCommandEventHandler(gvlevel3_RowCommand);
        }
    }
    #endregion
    #region RowCommand
    protected void gvCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "labeldel")
        {
            try
            {
                string DeleteRec = Lo.DeleteRecord(e.CommandArgument.ToString(), "InActiveLabel");
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
        else if (e.CommandName == "labelactive")
        {
            try
            {
                string DeleteRec = Lo.DeleteRecord(e.CommandArgument.ToString(), "ActiveLabel");
                if (DeleteRec == "true")
                {
                    BindGridView();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('label active succssfully.')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('label not active.')", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('label not active.')", true);
            }
        }
    }
    protected void gvsubcatlevel1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "level1edit")
        {
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gvr.RowIndex;
            string Role = "ff";
            Response.Redirect("Master-Category?mrcreaterole=" + objEnc.EncryptData(Role) + "&mcurrentFactroyRefNo=" + (objEnc.EncryptData(e.CommandArgument.ToString())) + "&id=" + Request.QueryString["id"].ToString());
        }
        else if (e.CommandName == "level2del")
        {
            try
            {
                string DeleteRec = Lo.DeleteRecord(e.CommandArgument.ToString(), "InActiveLabel1");
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
    protected void gvsublevel2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "level2edit")
        {
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gvr.RowIndex;
            string Role = (gvCategory.Rows[rowIndex].FindControl("lblunitrole") as Label).Text;
            Response.Redirect("Master-Category?mrcreaterole=" + objEnc.EncryptData(Role) + "&mcurrentUnitRefNo=" + (objEnc.EncryptData(e.CommandArgument.ToString())) + "&id=" + Request.QueryString["id"].ToString());
        }
        else if (e.CommandName == "level2delete")
        {
            try
            {
                string DeleteRec = Lo.DeleteRecord(e.CommandArgument.ToString(), "InActiveLabel2");
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
    protected void gvlevel3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "level2edit")
        //{
        //    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        //    int rowIndex = gvr.RowIndex;
        //    string Role = (gvCategory.Rows[rowIndex].FindControl("lblunitrole") as Label).Text;
        //    Response.Redirect("Master-Category?mrcreaterole=" + objEnc.EncryptData(Role) + "&mcurrentUnitRefNo=" + (objEnc.EncryptData(e.CommandArgument.ToString())) + "&id=" + Request.QueryString["id"].ToString());
        //}
        //else 
        if (e.CommandName == "level3delete")
        {
            try
            {
                string DeleteRec = Lo.DeleteRecord(e.CommandArgument.ToString(), "InActiveLabel2");
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
    #region SeachCode
    DataTable DtMasterCategroy = new DataTable();
    protected void BindMasterCategory()
    {

        if (mType == "SuperAdmin" || mType == "Admin")
        {
            DtMasterCategroy = Lo.RetriveMasterCategoryDate(0, "", "", "", "", mType,"");
        }
        else
        {
            DtMasterCategroy = Lo.RetriveMasterCategoryDate(0, "", "", "", "", "Select","");
        }
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlsearch, DtMasterCategroy, "MCategoryName", "MCategoryID");
            ddlsearch.Items.Insert(0, "All");
        }
        else
        {
            ddlsearch.Items.Insert(0, "All");
        }
    }
    protected void ddlsearch_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (objEnc.DecryptData(Session["Type"].ToString()) == "SuperAdmin" || objEnc.DecryptData(Session["Type"].ToString()) == "Admin")
            {
                DtGrid = Lo.RetriveMasterCategoryDate(Convert.ToInt16(ddlsearch.SelectedItem.Value), "", "", "",
                   "", "S" + objEnc.DecryptData(Session["Type"].ToString()),"");
            }
            else
            {
                DtGrid = Lo.RetriveMasterCategoryDate(Convert.ToInt16(ddlsearch.SelectedItem.Value), "", "", "",
                   "", "SelectByLavel","");
            }

            if (DtGrid.Rows.Count > 0)
            {
                gvCategory.DataSource = DtGrid;
                gvCategory.DataBind();
            }
        }
        catch (Exception exception)
        {
        }
    }
    #endregion
}