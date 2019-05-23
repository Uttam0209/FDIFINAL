using System;
using System.Data;
using BusinessLayer;
using Encryption;
using System.Collections.Specialized;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using System.Net;
using System.Web.UI.WebControls;

public partial class Admin_ViewCompanyCategory : System.Web.UI.Page
{
    Logic Lo = new Logic();
    HybridDictionary HySave = new HybridDictionary();
    Cryptography objEnc = new Cryptography();
    Int64 Mid = 0;
    DataUtility Co = new DataUtility();
    string _msg = string.Empty;
    string _sysMsg = string.Empty;
    DataTable DtView = new DataTable();
    string currentPage = "";
    string lbltypelogin = "";
    private string Categoryintrestedare = "";
    DataTable DtCompanyDDL = new DataTable();
    DataTable dtViewDefault = new DataTable();
    string strCRole, strDRole, strURole;
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
            hidType.Value = objEnc.DecryptData(Session["Type"].ToString());
            hfcomprefno.Value = Session["CompanyRefNo"].ToString();
            BindCompany();
            BindGridView();
        }
    }
    #region Load
    protected void BindGridView()
    {
        try
        {
            if (hidType.Value == "SuperAdmin")
            {
                DataTable DtGrid = Lo.RetriveMasterCategoryDate(0, "", "", "", "", "Select","");
                if (DtGrid.Rows.Count > 0)
                {

                    gvCategory.DataSource = DtGrid;
                    gvCategory.DataBind();
                }
            }
            else if (hidType.Value == "Company" && hfcomprefno.Value != "")
            {
                DataTable DtGrid = Lo.RetriveMasterSubCategoryDate(0, "", "", "SubCompCat", hfcomprefno.Value,"");
                if (DtGrid.Rows.Count > 0)
                {
                    gvCategory.DataSource = DtGrid;
                    gvCategory.DataBind();
                }
            }
            else if (hidType.Value == "Factory" && hfcomprefno.Value != "" || hidType.Value == "Division" && hfcomprefno.Value != "")
            {
                DataTable DtGrid = Lo.RetriveMasterSubCategoryDate(0, "", "", "SubCompCat", hfcomprefno.Value,"");
                if (DtGrid.Rows.Count > 0)
                {
                    gvCategory.DataSource = DtGrid;
                    gvCategory.DataBind();
                }
            }
            else if (hidType.Value == "Unit" && hfcomprefno.Value != "")
            {
                DataTable DtGrid = Lo.RetriveMasterSubCategoryDate(0, "", "", "SubCompCat", hfcomprefno.Value,"");
                if (DtGrid.Rows.Count > 0)
                {
                    gvCategory.DataSource = DtGrid;
                    gvCategory.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    #endregion
    #region RowDatabound
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (hidType.Value == "SuperAdmin")
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
            else if (hidType.Value != "Company")
            {
                HiddenField lblCatId = e.Row.FindControl("hfcat") as HiddenField;
                GridView gvSubcat = e.Row.FindControl("gvsubcatlevel1") as GridView;
                DataTable DtGrid = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(lblCatId.Value), "", "", "MasterCatComp2", hfcomprefno.Value,"");
                if (DtGrid.Rows.Count > 0)
                {
                    gvSubcat.DataSource = DtGrid;
                    gvSubcat.DataBind();
                }
            }
            else if (hidType.Value != "Factory")
            {
                HiddenField lblCatId = e.Row.FindControl("hfcat") as HiddenField;
                GridView gvSubcat = e.Row.FindControl("gvsubcatlevel1") as GridView;
                DataTable DtGrid = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(lblCatId.Value), "", "", "MasterCatComp2", hfcomprefno.Value,"");
                if (DtGrid.Rows.Count > 0)
                {
                    gvSubcat.DataSource = DtGrid;
                    gvSubcat.DataBind();
                }
            }
            else if (hidType.Value != "Unit")
            {
                HiddenField lblCatId = e.Row.FindControl("hfcat") as HiddenField;
                GridView gvSubcat = e.Row.FindControl("gvsubcatlevel1") as GridView;
                DataTable DtGrid = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(lblCatId.Value), "", "", "MasterCatComp2", hfcomprefno.Value,"");
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
    #region DropDownList
    protected void BindCompany()
    {
        if (hidType.Value == "SuperAdmin")
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
        else if (hidType.Value == "Admin")
        {
        }
        else if (hidType.Value == "Company")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, hfcomprefno.Value, "Company", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = false;
            }
            else
            {
                ddlcompany.Enabled = false;
            }
        }
        else if (hidType.Value == "Factory")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, hfcomprefno.Value, "Company1", 0, "", "", "CompanyName");
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
                lblselectdivison.Visible = true;
                ddldivision.Enabled = false;
            }
            else
            {
                ddldivision.Enabled = false;
            }
        }
        else if (hidType.Value == "Unit")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, hfcomprefno.Value, "Company2", 0, "", "", "CompanyName");
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
                ddlunit.Enabled = false;
                lblselectunit.Visible = true;
            }
            else
            {
                lblselectunit.Visible = false;
            }
        }
    }
    #endregion
}