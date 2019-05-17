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

            }
        }
    }

    protected void btnAddNodalOfficer_Click(object sender, EventArgs e)
    {
        string Role = "Company";
        string stridNew = Request.QueryString["id"].ToString().Replace(" ", "+");
        string mstrid = objEnc.EncryptData((objEnc.DecryptData(stridNew)));
        Response.Redirect("Add-Nodal?mrcreaterole=" + objEnc.EncryptData(Role) + "&id=" + mstrid);

    }

    public void GridViewNodalOfficerBind(string mRefNo, string mRole)
    {
        DataTable DtGrid = Lo.RetriveAllNodalOfficer(mRefNo, mRole);
        if (DtGrid.Rows.Count > 0)
        {
            gvViewNodalOfficer.DataSource = DtGrid;
            gvViewNodalOfficer.DataBind();
            gvViewNodalOfficer.Visible = true;
        }
        else
        {
            gvViewNodalOfficer.Visible = false;
        }
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
        else if (mType == "Factory")
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
                lblselectdivison.Visible = true;
                ddldivision.Enabled = false;
                GridViewNodalOfficerBind(ddldivision.SelectedItem.Value, "Division");
            }
            else
            {
                ddldivision.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "Unit1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
                ddlunit.Items.Insert(0, "Select");
                ddlunit.Enabled = true;
                lblselectunit.Visible = true;
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
            string mstrid = objEnc.EncryptData((objEnc.DecryptData(stridNew) + " >> Edit Nodal Office"));
            Response.Redirect("Add-Nodal?mrcreaterole=" + objEnc.EncryptData(e.CommandArgument.ToString()) + "&mcurrentcompRefNo=" + (objEnc.EncryptData(e.CommandArgument.ToString())) + "&id=" + mstrid);
        }
        else if (e.CommandName == "ViewComp")
        {
            DataTable DtView = new DataTable();
            if (ddldivision.SelectedValue != "Select" &&  ddldivision.SelectedValue!="")
            {
                DtView = Lo.RetriveAllNodalOfficer(e.CommandArgument.ToString(), "DivisionID");

                if (ddlunit.SelectedValue != "Select" && ddlunit.SelectedValue!="")
                {
                    DtView = Lo.RetriveAllNodalOfficer(e.CommandArgument.ToString(), "UnitID");
                }

            }
            else
            {
                DtView = Lo.RetriveAllNodalOfficer(e.CommandArgument.ToString(), "CompanyID");
            }
            if (DtView.Rows.Count > 0)
            {
                lblcompanyname.Text = DtView.Rows[0]["CompanyName"].ToString();
                lblDivision.Text = DtView.Rows[0]["FactoryName"].ToString();
                lblUnit.Text = DtView.Rows[0]["UnitName"].ToString();
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
                body = body.Replace("{refno}", objEnc.EncryptData(lblnodelrefno.Text));
                body = body.Replace("{curid}", Resturl(56));
                SendMail s;
                s = new SendMail();
                s.CreateMail("aeroindia-ddp@gov.in", lblnodelname.Text, "Create Password", body);
                s.sendMail();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Create password email send successfully.')", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Login detail not send. Some error occured.')", true);
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
}