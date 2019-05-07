using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Encryption;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;

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
        }
    }
    #region Load
    protected void BindGridView(string sortExpression = null)
    {
        try
        {
            if (mType == "SuperAdmin")
            {
                DataTable DtGrid = Lo.RetriveMasterCategoryDate(0, "", "", "", "Select");
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
                DataTable DtGrid = Lo.RetriveMasterSubCategoryDate(0, "", "", "SubCompCat", mRefNo);
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
                DataTable DtGrid = Lo.RetriveMasterSubCategoryDate(0, "", "", "SubCompCat", mRefNo);
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
                DataTable DtGrid = Lo.RetriveMasterSubCategoryDate(0, "", "", "SubCompCat", mRefNo);
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
    //protected void BindCompany()
    //{
    //    if (mType == "SuperAdmin")
    //    {
    //        DtCompanyDDL = Lo.RetriveMasterData(0, "", "", 0, "", "", "Select");
    //        if (DtCompanyDDL.Rows.Count > 0)
    //        {
    //            Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
    //            ddlcompany.Items.Insert(0, "All");
    //            ddlcompany.Enabled = true;
    //        }
    //        else
    //        {
    //            ddlcompany.Enabled = false;
    //        }

    //        ddldivision.Visible = false;
    //        ddlunit.Visible = false;
    //    }
    //    else if (mType == "Admin")
    //    {
    //    }
    //    else if (mType == "Company")
    //    {
    //        DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company", 0, "", "", "CompanyName");
    //        if (DtCompanyDDL.Rows.Count > 0)
    //        {
    //            Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
    //            ddlcompany.Enabled = false;
    //        }
    //        else
    //        {
    //            ddlcompany.Enabled = false;
    //        }
    //        DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
    //        if (DtCompanyDDL.Rows.Count > 0)
    //        {
    //            Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
    //            ddldivision.Items.Insert(0, "All");
    //            if (mType == "Company")
    //            {
    //                lblselectdivison.Visible = true;
    //                ddldivision.Enabled = true;
    //                ddlunit.Visible = false;
    //            }
    //            else
    //            {
    //                ddldivision.Enabled = false;
    //            }
    //        }
    //        else
    //        {
    //            ddldivision.Enabled = false;
    //        }
    //    }
    //    else if (mType == "Factory")
    //    {
    //        DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company1", 0, "", "", "CompanyName");
    //        if (DtCompanyDDL.Rows.Count > 0)
    //        {
    //            Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
    //            ddlcompany.Enabled = false;
    //            DataTable DtGrid =
    //                Lo.RetriveGridViewCompany(ddlcompany.SelectedItem.Value, "", "", "CompanyMainGridView");
    //            if (DtGrid.Rows.Count > 0)
    //            {
    //                //gvcompanydetail.DataSource = DtGrid;
    //                // gvcompanydetail.DataBind();
    //            }
    //        }
    //        else
    //        {
    //            ddlcompany.Enabled = false;
    //        }

    //        DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
    //        if (DtCompanyDDL.Rows.Count > 0)
    //        {
    //            Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
    //            lblselectdivison.Visible = true;
    //            ddldivision.Enabled = false;
    //            DataTable DtGrid = Lo.RetriveGridViewCompany(ddlcompany.SelectedItem.Value,
    //                ddldivision.SelectedItem.Value, "", "InnerGVFactoryID");
    //            if (DtGrid.Rows.Count > 0)
    //            {
    //                //foreach (GridViewRow row in gvcompanydetail.Rows)
    //                //{
    //                //    gvinnerfactory = ((GridView)row.FindControl("gvfactory"));
    //                //}
    //                //gvinnerfactory.DataSource = DtGrid;
    //                //gvinnerfactory.DataBind();
    //            }
    //        }
    //        else
    //        {
    //            ddldivision.Enabled = false;
    //        }

    //        DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "Unit1", 0, "", "", "CompanyName");
    //        if (DtCompanyDDL.Rows.Count > 0)
    //        {
    //            Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
    //            ddlunit.Items.Insert(0, "All");
    //            ddlunit.Enabled = true;
    //            lblselectunit.Visible = true;
    //            DataTable DtGrid = Lo.RetriveGridViewCompany("", ddldivision.SelectedItem.Value,
    //                ddlunit.SelectedItem.Value, "InnerGVUnitID");
    //            if (DtGrid.Rows.Count > 0)
    //            {
    //                //    foreach (GridViewRow row in gvcompanydetail.Rows)
    //                //    {
    //                //        gvinnerfactroyforunit = ((GridView)row.FindControl("gvfactory"));
    //                //    }
    //                //    foreach (GridViewRow row in gvinnerfactroyforunit.Rows)
    //                //    {
    //                //        gvinunit = ((GridView)row.FindControl("gvunit"));
    //                //    }
    //                //    gvinunit.DataSource = DtGrid;
    //                //    gvinunit.DataBind();
    //            }
    //        }
    //        else
    //        {
    //            ddlunit.Visible = false;
    //        }
    //    }
    //    else if (mType == "Unit")
    //    {
    //        DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company2", 0, "", "", "CompanyName");
    //        if (DtCompanyDDL.Rows.Count > 0)
    //        {
    //            Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
    //            ddlcompany.Enabled = false;
    //            DataTable DtGrid = Lo.RetriveGridViewCompany(ddlcompany.SelectedItem.Value, "", "", "CompanyMainGridView");
    //            if (DtGrid.Rows.Count > 0)
    //            {
    //                //gvcompanydetail.DataSource = DtGrid;
    //                //gvcompanydetail.DataBind();
    //            }
    //        }
    //        else
    //        {
    //            ddlcompany.Enabled = false;
    //        }
    //        DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
    //        if (DtCompanyDDL.Rows.Count > 0)
    //        {
    //            Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
    //            lblselectdivison.Visible = true;
    //            ddldivision.Enabled = false;
    //            DataTable DtGrid = Lo.RetriveGridViewCompany(ddlcompany.SelectedItem.Value, ddldivision.SelectedItem.Value, "", "InnerGVFactoryID");
    //            if (DtGrid.Rows.Count > 0)
    //            {
    //                //foreach (GridViewRow row in gvcompanydetail.Rows)
    //                //{
    //                //    gvinnerfactory = ((GridView)row.FindControl("gvfactory"));
    //                //}
    //                //gvinnerfactory.DataSource = DtGrid;
    //                //gvinnerfactory.DataBind();
    //            }
    //        }
    //        else
    //        {
    //            ddldivision.Enabled = false;
    //        }
    //        DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "Unit1", 0, "", "", "CompanyName");
    //        if (DtCompanyDDL.Rows.Count > 0)
    //        {
    //            Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
    //            ddlunit.Enabled = false;
    //            lblselectunit.Visible = true;
    //            DataTable DtGrid = Lo.RetriveGridViewCompany("", ddldivision.SelectedItem.Value, ddlunit.SelectedItem.Value, "InnerGVUnitID");
    //            if (DtGrid.Rows.Count > 0)
    //            {
    //                //foreach (GridViewRow row in gvcompanydetail.Rows)
    //                //{
    //                //    gvinnerfactroyforunit = ((GridView)row.FindControl("gvfactory"));
    //                //}
    //                //foreach (GridViewRow row in gvinnerfactroyforunit.Rows)
    //                //{
    //                //    gvinunit = ((GridView)row.FindControl("gvunit"));
    //                //}
    //                //gvinunit.DataSource = DtGrid;
    //                //gvinunit.DataBind();
    //            }
    //        }
    //        else
    //        {
    //            ddlunit.Enabled = false;
    //        }
    //    }
    //}
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
                DataTable DtGrid = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(lblCatId.Value), "", "", "SelectInnerMaster", "");
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
                DataTable DtGrid = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(lblCatId.Value), "", "", "MasterCatComp", "");
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
            DataTable DtGrid = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(lblfactroyrefno.Value), "", "", "SubSelectID", "");
            if (DtGrid.Rows.Count > 0)
            {
                gvsubcatlevel2.DataSource = DtGrid;
                gvsubcatlevel2.DataBind();
            }
            GridView gvsublevel2 = e.Row.FindControl("gvsubcatlevel2") as GridView;
            gvsublevel2.RowCommand += new GridViewCommandEventHandler(gvsublevel2_RowCommand);
        }
    }
    #endregion
    //#region DropDownList Code
    //protected void ddlcompany_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlcompany.SelectedItem.Text != "All")
    //    {
    //        DataTable DtGrid = Lo.RetriveMasterSubCategoryDate(0, "", "", "SubCompCat", ddlcompany.SelectedItem.Value);
    //        if (DtGrid.Rows.Count > 0)
    //        {
    //            gvCategory.DataSource = DtGrid;
    //            gvCategory.DataBind();
    //            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "", 0, "", "", "FactoryCompanyID");
    //            if (DtCompanyDDL.Rows.Count > 0)
    //            {
    //                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
    //                ddldivision.Items.Insert(0, "All");
    //                lblselectdivison.Visible = true;
    //                ddldivision.Visible = true;
    //            }
    //            else
    //            {
    //                ddldivision.Visible = false;
    //                lblselectdivison.Visible = false;
    //            }
    //        }
    //    }
    //    else if (ddlcompany.SelectedItem.Value == "All")
    //    {
    //        DataTable DtGrid = Lo.RetriveMasterCategoryDate(0, "", "", "Select");
    //        if (DtGrid.Rows.Count > 0)
    //        {
    //            gvCategory.DataSource = DtGrid;
    //            gvCategory.DataBind();
    //            ddldivision.Visible = false;
    //            ddlunit.Visible = false;
    //            lblselectunit.Visible = false;
    //            lblselectdivison.Visible = false;
    //        }
    //        else
    //        {
    //            lblselectunit.Visible = false;
    //            lblselectdivison.Visible = false;
    //            ddldivision.Visible = false;
    //            ddlunit.Visible = false;
    //        }
    //    }
    //    else
    //    {
    //        lblselectunit.Visible = false;
    //        ddldivision.Visible = false;
    //        ddlunit.Visible = false;
    //    }
    //}
    //GridView gvinnerfactory;
    //protected void ddldivision_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddldivision.SelectedItem.Text != "All")
    //    {
    //        DataTable DtGrid = Lo.RetriveGridViewCompany(ddlcompany.SelectedItem.Value, ddldivision.SelectedItem.Value, "", "InnerGVFactoryID");
    //        if (DtGrid.Rows.Count > 0)
    //        {
    //            foreach (GridViewRow row in gvCategory.Rows)
    //            {
    //                gvinnerfactory = ((GridView)row.FindControl("gvsubcatlevel1"));
    //            }
    //            gvinnerfactory.DataSource = DtGrid;
    //            gvinnerfactory.DataBind();
    //        }
    //        DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "", 0, "", "", "UnitSelectID");
    //        if (DtCompanyDDL.Rows.Count > 0)
    //        {
    //            Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
    //            ddlunit.Items.Insert(0, "All");
    //            ddlunit.Visible = true;
    //            lblselectunit.Visible = true;
    //        }
    //        else
    //        {
    //            lblselectunit.Visible = false;
    //            ddlunit.Visible = false;
    //        }
    //    }
    //    else if (ddldivision.SelectedItem.Text == "All")
    //    {
    //        DataTable DtGrid = Lo.RetriveGridViewCompany(ddlcompany.SelectedItem.Value, "", "", "InnerGridViewFactory");
    //        if (DtGrid.Rows.Count > 0)
    //        {
    //            foreach (GridViewRow row in gvCategory.Rows)
    //            {
    //                gvinnerfactory = ((GridView)row.FindControl("gvsubcatlevel1"));
    //            }
    //            gvinnerfactory.DataSource = DtGrid;
    //            gvinnerfactory.DataBind();
    //            ddlunit.Visible = false;
    //            lblselectunit.Visible = false;
    //        }
    //    }
    //}
    //GridView gvinunit;
    //private GridView gvinnerfactroyforunit;
    //protected void ddlunit_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlunit.SelectedItem.Text != "All")
    //    {
    //        DataTable DtGrid = Lo.RetriveGridViewCompany("", ddldivision.SelectedItem.Value, ddlunit.SelectedItem.Value, "InnerGVUnitID");
    //        if (DtGrid.Rows.Count > 0)
    //        {
    //            foreach (GridViewRow row in gvCategory.Rows)
    //            {
    //                gvinnerfactroyforunit = ((GridView)row.FindControl("gvsubcatlevel1"));
    //            }
    //            foreach (GridViewRow row in gvinnerfactroyforunit.Rows)
    //            {
    //                gvinunit = ((GridView)row.FindControl("gvsubcatlevel2"));
    //            }
    //            gvinunit.DataSource = DtGrid;
    //            gvinunit.DataBind();
    //        }
    //    }
    //    else if (ddlunit.SelectedItem.Text == "All")
    //    {
    //        DataTable DtGrid = Lo.RetriveGridViewCompany("", ddldivision.SelectedItem.Value, "", "InnerGridViewUnit");
    //        if (DtGrid.Rows.Count > 0)
    //        {
    //            foreach (GridViewRow row in gvCategory.Rows)
    //            {
    //                gvinnerfactroyforunit = ((GridView)row.FindControl("gvsubcatlevel1"));
    //            }
    //            foreach (GridViewRow row in gvinnerfactroyforunit.Rows)
    //            {
    //                gvinunit = ((GridView)row.FindControl("gvsubcatlevel2"));
    //            }
    //            gvinunit.DataSource = DtGrid;
    //            gvinunit.DataBind();
    //        }
    //    }
    //    else
    //    {
    //        lblselectunit.Visible = false;
    //        ddlunit.Visible = false;
    //    }
    //}
    //#endregion
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
    #endregion
}