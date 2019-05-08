using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Encryption;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;



public partial class Admin_ViewNodalOfficer : System.Web.UI.Page
{
    Logic Lo = new Logic();
    DataUtility Co = new DataUtility();
    Cryptography objEnc = new Cryptography();
    DataTable DtGrid = new DataTable();
    DataTable DtCompanyDDL = new DataTable();
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
                mType = objEnc.DecryptData(Session["Type"].ToString());
                mRefNo = Session["CompanyRefNo"].ToString();
                BindCompany();
                BindGridView();
            }
        }
    }

    protected void BindGridView(string sortExpression = null)
    {
        try
        {
            if (mType == "SuperAdmin")
            {
                DataTable DtGrid = Lo.RetriveMasterData(0, "0", "", 0, "", "", "ViewNodal");
                if (DtGrid.Rows.Count > 0)
                {
                    if (sortExpression != null)
                    {
                        DataView dv = DtGrid.AsDataView();
                        this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";

                        dv.Sort = sortExpression + " " + this.SortDirection;
                        gvViewNodalOfficer.DataSource = dv;
                    }
                    else
                    {
                        gvViewNodalOfficer.DataSource = DtGrid;
                    }
                    gvViewNodalOfficer.DataBind();
                    lbltotal.Text = DtGrid.Rows.Count.ToString();
                }
            }
            else if (mType == "Company" && mRefNo != "")
            {
                DataTable DtGrid = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "", 0, "", "", "ViewNodal");
                if (DtGrid.Rows.Count > 0)
                {
                    gvViewNodalOfficer.DataSource = DtGrid;
                    gvViewNodalOfficer.DataBind();
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
    protected void BindCompany()
    {
        if (mType == "SuperAdmin")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, "", "", 0, "", "", "Select");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Items.Insert(0, "All");
                ddlcompany.Enabled = true;
            }

        }
        else if (mType == "Admin")
        {
        }
        else if (mType == "Company")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company", 0, "", "", "CompanyName");
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
            Response.Redirect("Add-Nodal?mrcreaterole=" + objEnc.EncryptData(e.CommandArgument.ToString()) + "&mcurrentcompRefNo=" + (objEnc.EncryptData(e.CommandArgument.ToString())) + "&id=" + mstrid);
        }

        else if (e.CommandName == "DeleteComp")
        {
            try
            {
                string DeleteRec = Lo.DeleteRecord(e.CommandArgument.ToString(), "InActiveCompany");
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

    #region DropDownList Code
    protected void ddlcompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcompany.SelectedItem.Text != "All")
        {
            DataTable DtGrid = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "", 0, "", "", "ViewNodal");
            if (DtGrid.Rows.Count > 0)
            {
                gvViewNodalOfficer.DataSource = DtGrid;
                gvViewNodalOfficer.DataBind();

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
                gvViewNodalOfficer.DataSource = DtGrid;
                gvViewNodalOfficer.DataBind();
                lbltotal.Text = DtGrid.Rows.Count.ToString();
            }
        }
        else
        {
            BindGridView();
        }
    }
    #endregion
}