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
            currentPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
            mType = objEnc.DecryptData(Session["Type"].ToString());
            mRefNo = Session["CompanyRefNo"].ToString();
            BindCompany();
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
                DataTable DtGrid = Lo.RetriveGridViewCompany("0", "", "", "CompanyMainGridView");
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
            else if (mType == "Company" && mRefNo != "")
            {
                DataTable DtGrid = Lo.RetriveGridViewCompany(mRefNo, "", "", "CompanyMainGridView");
                if (DtGrid.Rows.Count > 0)
                {
                    gvcompanydetail.DataSource = DtGrid;
                    gvcompanydetail.DataBind();
                }
            }
            else if (mType != "Factroy" && mRefNo != "")
            {
                DataTable DtGrid = Lo.RetriveGridViewCompany(mRefNo, "", "", "InnerGridViewFactory");
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
            else if (mType != "Unit" && mRefNo != "")
            {
                DataTable DtGrid = Lo.RetriveGridViewCompany(mRefNo, "", "", "InnerGridViewUnit");
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
            else
            {
                ddlcompany.Enabled = false;
            }

            ddldivision.Visible = false;
            ddlunit.Visible = false;
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
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                ddldivision.Items.Insert(0, "All");
                if (mType == "Company")
                {
                    lblselectdivison.Visible = true;
                    ddldivision.Enabled = true;
                    ddlunit.Visible = false;
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
        else if (mType == "Factory")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = false;
                DataTable DtGrid = Lo.RetriveGridViewCompany(ddlcompany.SelectedItem.Value, "", "", "CompanyMainGridView");
                if (DtGrid.Rows.Count > 0)
                {
                    gvcompanydetail.DataSource = DtGrid;
                    gvcompanydetail.DataBind();
                }
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
                DataTable DtGrid = Lo.RetriveGridViewCompany(ddlcompany.SelectedItem.Value, ddldivision.SelectedItem.Value, "", "InnerGVFactoryID");
                if (DtGrid.Rows.Count > 0)
                {
                    foreach (GridViewRow row in gvcompanydetail.Rows)
                    {
                        gvinnerfactory = ((GridView)row.FindControl("gvfactory"));
                    }
                    gvinnerfactory.DataSource = DtGrid;
                    gvinnerfactory.DataBind();
                }
            }
            else
            {
                ddldivision.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "Unit1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
                ddlunit.Items.Insert(0, "All");
                ddlunit.Enabled = true;
                lblselectunit.Visible = true;
                DataTable DtGrid = Lo.RetriveGridViewCompany("", ddldivision.SelectedItem.Value, ddlunit.SelectedItem.Value, "InnerGVUnitID");
                if (DtGrid.Rows.Count > 0)
                {
                    foreach (GridViewRow row in gvcompanydetail.Rows)
                    {
                        gvinnerfactroyforunit = ((GridView)row.FindControl("gvfactory"));
                    }
                    foreach (GridViewRow row in gvinnerfactroyforunit.Rows)
                    {
                        gvinunit = ((GridView)row.FindControl("gvunit"));
                    }
                    gvinunit.DataSource = DtGrid;
                    gvinunit.DataBind();
                }
            }
            else
            {
                ddlunit.Visible = false;
            }
        }
        else if (mType == "Unit")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company2", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = false;
                DataTable DtGrid = Lo.RetriveGridViewCompany(ddlcompany.SelectedItem.Value, "", "", "CompanyMainGridView");
                if (DtGrid.Rows.Count > 0)
                {
                    gvcompanydetail.DataSource = DtGrid;
                    gvcompanydetail.DataBind();
                }
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
                DataTable DtGrid = Lo.RetriveGridViewCompany(ddlcompany.SelectedItem.Value, ddldivision.SelectedItem.Value, "", "InnerGVFactoryID");
                if (DtGrid.Rows.Count > 0)
                {
                    foreach (GridViewRow row in gvcompanydetail.Rows)
                    {
                        gvinnerfactory = ((GridView)row.FindControl("gvfactory"));
                    }
                    gvinnerfactory.DataSource = DtGrid;
                    gvinnerfactory.DataBind();
                }
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
                DataTable DtGrid = Lo.RetriveGridViewCompany("", ddldivision.SelectedItem.Value, ddlunit.SelectedItem.Value, "InnerGVUnitID");
                if (DtGrid.Rows.Count > 0)
                {
                    foreach (GridViewRow row in gvcompanydetail.Rows)
                    {
                        gvinnerfactroyforunit = ((GridView)row.FindControl("gvfactory"));
                    }
                    foreach (GridViewRow row in gvinnerfactroyforunit.Rows)
                    {
                        gvinunit = ((GridView)row.FindControl("gvunit"));
                    }
                    gvinunit.DataSource = DtGrid;
                    gvinunit.DataBind();
                }
            }
            else
            {
                ddlunit.Enabled = false;
            }
        }
    }
    #endregion
    #region PageIndex or Sorting
    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvcompanydetail.PageIndex = e.NewPageIndex;
        this.BindGridView();
    }
    protected void OnSorting(object sender, GridViewSortEventArgs e)
    {
        this.BindGridView(e.SortExpression);
    }
    #endregion
    #region RowCommand
    protected void gvcompanydetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditComp")
        {
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gvr.RowIndex;
            string Role = (gvcompanydetail.Rows[rowIndex].FindControl("lblcompanyrole") as Label).Text;
            Response.Redirect("Add-Company?mrcreaterole=" + objEnc.EncryptData(Role) + "&mcurrentcompRefNo=" + (objEnc.EncryptData(e.CommandArgument.ToString())));
        }
        else if (e.CommandName == "ViewComp")
        {
            DataTable DtView = Lo.RetriveGridViewCompany(e.CommandArgument.ToString(), "", "", "CompanyMainGridView");
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Mail not sent error occured.')", true);
            }
        }
    }
    protected void gvfactory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditfactoryComp")
        {
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gvr.RowIndex;
           // string Role = (gvcompanydetail.Rows[rowIndex].FindControl("lblfactoryrole") as Label).Text;
            string Role = "ff";
            Response.Redirect("Add-Company?mrcreaterole=" + objEnc.EncryptData(Role) + "&mcurrentFactroyRefNo=" + (objEnc.EncryptData(e.CommandArgument.ToString())));
        }
        else if (e.CommandName == "ViewfactoryComp")
        {
            DataTable DtView = Lo.RetriveGridViewCompany("", e.CommandArgument.ToString(), "", "DisplayFactory");
            if (DtView.Rows.Count > 0)
            {
                lblfacrefno.Text = DtView.Rows[0]["FactoryRefNo"].ToString();
                lblfacaddress.Text = DtView.Rows[0]["FactoryAddress"].ToString();
                lblfaccin.Text = DtView.Rows[0]["FactoryCINNo"].ToString();
                lblfacname.Text = DtView.Rows[0]["FactoryName"].ToString();
                lblfacemail.Text = DtView.Rows[0]["FactoryEmailId"].ToString();
                lblfacmobno.Text = DtView.Rows[0]["FactoryContactNo"].ToString();
                lblfacnodalname.Text = DtView.Rows[0]["FactoryNodalPerson"].ToString();
                lblfacpan.Text = DtView.Rows[0]["FactoryPANNo"].ToString();
                lblfacpincode.Text = DtView.Rows[0]["FactoryPincode"].ToString();
                lblfacstate.Text = DtView.Rows[0]["StateName"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "divfactoryshow", "showPopup1();", true);
            }
        }
        else if (e.CommandName == "DeletefactoryComp")
        {
            try
            {
                string DeleteRec = Lo.DeleteRecord(e.CommandArgument.ToString(), "InActiveFactory");
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
        else if (e.CommandName == "factorySendLogin")
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
    protected void gvunit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "unitEditComp")
        {
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gvr.RowIndex;
            string Role = (gvcompanydetail.Rows[rowIndex].FindControl("lblunitrole") as Label).Text;
            Response.Redirect("Add-Company?mrcreaterole=" + objEnc.EncryptData(Role) + "&mcurrentUnitRefNo=" + (objEnc.EncryptData(e.CommandArgument.ToString())));
        }
        else if (e.CommandName == "unitViewComp")
        {
            DataTable DtView = Lo.RetriveGridViewCompany("", "", e.CommandArgument.ToString(), "GVUnitID");
            if (DtView.Rows.Count > 0)
            {
                lblurefno.Text = DtView.Rows[0]["UnitRefNo"].ToString();
                lbluaddress.Text = DtView.Rows[0]["UnitAddress"].ToString();
                lbluname.Text = DtView.Rows[0]["UnitName"].ToString();
                lbluemail.Text = DtView.Rows[0]["UnitEmailId"].ToString();
                lblnofficialcontact.Text = DtView.Rows[0]["UnitContactNo"].ToString();
                lblnodalname.Text = DtView.Rows[0]["UnitNodalPerson"].ToString();
                lblupin.Text = DtView.Rows[0]["UnitPincode"].ToString();
                lblustate.Text = DtView.Rows[0]["StateName"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "divunitshow", "showPopup2();", true);
            }
        }
        else if (e.CommandName == "unitdel")
        {
            try
            {
                string DeleteRec = Lo.DeleteRecord(e.CommandArgument.ToString(), "InActiveUnit");
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
        else if (e.CommandName == "unitSendLogin")
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
    #endregion
    #region RowDatabound
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblrefno = e.Row.FindControl("lblrefno") as Label;
            GridView gvfactory = e.Row.FindControl("gvfactory") as GridView;
            DataTable DtGrid = Lo.RetriveGridViewCompany(lblrefno.Text, "", "", "InnerGridViewFactory");

            if (DtGrid.Rows.Count > 0)
            {
                gvfactory.DataSource = DtGrid;
                gvfactory.DataBind();
            }

            DataTable dtCompany = Lo.RetriveMasterData(0, "", "", 0, currentPage, "", "btn");
            if (dtCompany.Rows.Count > 0)
            {
                if (dtCompany.Rows[0]["Company"].ToString() == "1")
                {
                    LinkButton lbledit = e.Row.FindControl("lbledit") as LinkButton;
                    LinkButton lblview = (LinkButton)e.Row.FindControl("lblview") as LinkButton;
                    LinkButton lbldel = (LinkButton)e.Row.FindControl("lbldel") as LinkButton;
                    lbledit.Visible = false;
                    lblview.Visible = true;
                    lbldel.Visible = false;
                    //LinkButton lbleditfactory = (LinkButton)e.Row.FindControl("lbleditfactory");
                    //LinkButton lblviewfactory = (LinkButton)e.Row.FindControl("lblviewfactory");
                    //LinkButton lbldelfactory = (LinkButton)e.Row.FindControl("lbldelfactory");
                    //if (lbleditfactory != null)
                    //{
                    //lbleditfactory.Visible = false;
                    //lblviewfactory.Visible = true;
                    //lbldelfactory.Visible = false;
                    // }




                }
                else if (dtCompany.Rows[0]["Company"].ToString() == "2")
                {
                    LinkButton lbledit = (LinkButton)e.Row.FindControl("lbledit") as LinkButton;
                    LinkButton lblview = (LinkButton)e.Row.FindControl("lblview") as LinkButton;
                    LinkButton lbldel = (LinkButton)e.Row.FindControl("lbldel") as LinkButton;
                    lbledit.Visible = true;
                    lblview.Visible = true;
                    lbldel.Visible = false;
                    LinkButton lbleditfactory = (LinkButton)e.Row.FindControl("lbleditfactory") as LinkButton;
                    LinkButton lblviewfactory = (LinkButton)e.Row.FindControl("lblviewfactory") as LinkButton;
                    LinkButton lbldelfactory = (LinkButton)e.Row.FindControl("lbldelfactory") as LinkButton;
                    // if (lbleditfactory != null)
                    // {
                    //lbleditfactory.Visible = true;
                    //lblviewfactory.Visible = true;
                    //lbldelfactory.Visible = false;
                    // }
                }
                else if (dtCompany.Rows[0]["Company"].ToString() == "3")
                {
                    LinkButton lbledit = (LinkButton)e.Row.FindControl("lbledit") as LinkButton;
                    LinkButton lblview = (LinkButton)e.Row.FindControl("lblview") as LinkButton;
                    LinkButton lbldel = (LinkButton)e.Row.FindControl("lbldel") as LinkButton;
                    lbledit.Visible = true;
                    lblview.Visible = true;
                    lbldel.Visible = true;
                    //LinkButton lbleditfactory = (LinkButton)e.Row.FindControl("lbleditfactory");
                    //LinkButton lblviewfactory = (LinkButton)e.Row.FindControl("lblviewfactory");
                    //LinkButton lbldelfactory = (LinkButton)e.Row.FindControl("lbldelfactory");
                    //  if (lbleditfactory != null)
                    //  {
                    //lbleditfactory.Visible = true;
                    //lblviewfactory.Visible = true;
                    //lbldelfactory.Visible = true;
                    // }

                }
                else if (dtCompany.Rows[0]["Factory"].ToString() == "2")
                {
                    // btndemofirst.Visible = true;
                    //  btnDelete.Visible = false;
                }
                else if (dtCompany.Rows[0]["Factory"].ToString() == "3")
                {
                    //   btndemofirst.Visible = true;
                    //   btnDelete.Visible = true;
                }
                else if (dtCompany.Rows[0]["Unit"].ToString() == "2")
                {
                    //     btndemofirst.Visible = true;
                    //     btnDelete.Visible = false;
                }
                else if (dtCompany.Rows[0]["Unit"].ToString() == "3")
                {
                    //     btndemofirst.Visible = true;
                    //    btnDelete.Visible = true;
                }
                else if (dtCompany.Rows[0]["SuperAdmin"].ToString() == "1")
                {
                    LinkButton lbledit = (LinkButton)e.Row.FindControl("lbledit") as LinkButton;
                    LinkButton lblview = (LinkButton)e.Row.FindControl("lblview") as LinkButton;
                    LinkButton lbldel = (LinkButton)e.Row.FindControl("lbldel") as LinkButton;
                    lbledit.Visible = true;
                    lblview.Visible = true;
                    lbldel.Visible = true;
                    //  LinkButton lbleditfactory = (LinkButton)e.Row.FindControl("lbleditfactory");
                    //  LinkButton lblviewfactory = (LinkButton)e.Row.FindControl("lblviewfactory");
                    //  LinkButton lbldelfactory = (LinkButton)e.Row.FindControl("lbldelfactory");
                    ////  if (lbleditfactory != null)
                    ////  {
                    //      lbleditfactory.Visible = true;
                    //      lblviewfactory.Visible = true;
                    //      lbldelfactory.Visible = true;
                    ////  }
                }
            }
            GridView gvFactoryRow = e.Row.FindControl("gvfactory") as GridView;
            gvFactoryRow.RowCommand += new GridViewCommandEventHandler(gvfactory_RowCommand);
        }
    }
    protected void gvfactory_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblfactroyrefno = e.Row.FindControl("lblfactoryrefno") as Label;
            GridView gvunit = e.Row.FindControl("gvunit") as GridView;

            DataTable DtGrid = Lo.RetriveGridViewCompany("", lblfactroyrefno.Text, "", "InnerGridViewUnit");
            if (DtGrid.Rows.Count > 0)
            {
                gvunit.DataSource = DtGrid;
                gvunit.DataBind();
            }
            GridView gvUnitRow = e.Row.FindControl("gvunit") as GridView;
            gvUnitRow.RowCommand += new GridViewCommandEventHandler(gvunit_RowCommand);
        }
    }
    #endregion
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
    #region DropDownList Code
    protected void ddlcompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcompany.SelectedItem.Text != "All")
        {
            DataTable DtGrid = Lo.RetriveGridViewCompany(ddlcompany.SelectedItem.Value, "", "", "CompanyMainGridView");
            if (DtGrid.Rows.Count > 0)
            {
                gvcompanydetail.DataSource = DtGrid;
                gvcompanydetail.DataBind();
                DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "", 0, "", "", "FactoryCompanyID");
                if (DtCompanyDDL.Rows.Count > 0)
                {
                    Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                    ddldivision.Items.Insert(0, "All");
                    lblselectdivison.Visible = true;
                    ddldivision.Visible = true;
                }
                else
                {
                    ddldivision.Visible = false;
                    lblselectdivison.Visible = false;
                }
            }
        }
        else if (ddlcompany.SelectedItem.Value == "All")
        {
            DataTable DtGrid = Lo.RetriveGridViewCompany("0", "", "", "CompanyMainGridView");
            if (DtGrid.Rows.Count > 0)
            {
                gvcompanydetail.DataSource = DtGrid;
                gvcompanydetail.DataBind();
                ddldivision.Visible = false;
                ddlunit.Visible = false;
                lblselectunit.Visible = false;
                lblselectdivison.Visible = false;
            }
            else
            {
                lblselectunit.Visible = false;
                lblselectdivison.Visible = false;
                ddldivision.Visible = false;
                ddlunit.Visible = false;
            }
        }
        else
        {
            lblselectunit.Visible = false;
            ddldivision.Visible = false;
            ddlunit.Visible = false;
        }
    }
    GridView gvinnerfactory;
    protected void ddldivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldivision.SelectedItem.Text != "All")
        {
            DataTable DtGrid = Lo.RetriveGridViewCompany(ddlcompany.SelectedItem.Value, ddldivision.SelectedItem.Value, "", "InnerGVFactoryID");
            if (DtGrid.Rows.Count > 0)
            {
                foreach (GridViewRow row in gvcompanydetail.Rows)
                {
                    gvinnerfactory = ((GridView)row.FindControl("gvfactory"));
                }
                gvinnerfactory.DataSource = DtGrid;
                gvinnerfactory.DataBind();
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "", 0, "", "", "UnitSelectID");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
                ddlunit.Items.Insert(0, "All");
                ddlunit.Visible = true;
                lblselectunit.Visible = true;
            }
            else
            {
                lblselectunit.Visible = false;
                ddlunit.Visible = false;
            }
        }
        else if (ddldivision.SelectedItem.Text == "All")
        {
            DataTable DtGrid = Lo.RetriveGridViewCompany(ddlcompany.SelectedItem.Value, "", "", "InnerGridViewFactory");
            if (DtGrid.Rows.Count > 0)
            {
                foreach (GridViewRow row in gvcompanydetail.Rows)
                {
                    gvinnerfactory = ((GridView)row.FindControl("gvfactory"));
                }
                gvinnerfactory.DataSource = DtGrid;
                gvinnerfactory.DataBind();
                ddlunit.Visible = false;
                lblselectunit.Visible = false;
            }
        }
    }
    GridView gvinunit;
    private GridView gvinnerfactroyforunit;
    protected void ddlunit_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlunit.SelectedItem.Text != "All")
        {
            DataTable DtGrid = Lo.RetriveGridViewCompany("", ddldivision.SelectedItem.Value, ddlunit.SelectedItem.Value, "InnerGVUnitID");
            if (DtGrid.Rows.Count > 0)
            {
                foreach (GridViewRow row in gvcompanydetail.Rows)
                {
                    gvinnerfactroyforunit = ((GridView)row.FindControl("gvfactory"));
                }
                foreach (GridViewRow row in gvinnerfactroyforunit.Rows)
                {
                    gvinunit = ((GridView)row.FindControl("gvunit"));
                }
                gvinunit.DataSource = DtGrid;
                gvinunit.DataBind();
            }
        }
        else if (ddlunit.SelectedItem.Text == "All")
        {
            DataTable DtGrid = Lo.RetriveGridViewCompany("", ddldivision.SelectedItem.Value, "", "InnerGridViewUnit");
            if (DtGrid.Rows.Count > 0)
            {
                foreach (GridViewRow row in gvcompanydetail.Rows)
                {
                    gvinnerfactroyforunit = ((GridView)row.FindControl("gvfactory"));
                }
                foreach (GridViewRow row in gvinnerfactroyforunit.Rows)
                {
                    gvinunit = ((GridView)row.FindControl("gvunit"));
                }
                gvinunit.DataSource = DtGrid;
                gvinunit.DataBind();
            }
        }
        else
        {
            lblselectunit.Visible = false;
            ddlunit.Visible = false;
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
    #endregion
}