using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using Encryption;
using System.Text;

public partial class Admin_HeadDropdown : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Cryptography Enc = new Cryptography();
    DataTable DtCompanyDDL = new DataTable();
    DataUtility Co = new DataUtility();
    private string currentPage = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                string strid = Request.QueryString["id"].ToString().Replace(" ", "+");
                string strPageName = Enc.DecryptData(strid);
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
                if (Request.QueryString["mEdit"] != null)
                {
                    BindCompanyDropdownLoad(Enc.DecryptData(Request.QueryString["mlogrole"].ToString()),Enc.DecryptData(Request.QueryString["mrefno"].ToString()));
                }
                else
                {
                    BindCompanyDropdownLoad(Enc.DecryptData(Session["Type"].ToString()), Session["CompanyRefNo"].ToString());
                }
            }
        }
    }
    protected void BindCompanyDropdownLoad(string mType, string mRefNo)
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
            divlblselectdivison.Visible = false;
            divlblselectunit.Visible = false;
        }
        if (mType == "Company")
        {
            if (Request.QueryString["mEdit"] != null)
            {
                DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company", 0, "", "", "CompanyName");
                if (DtCompanyDDL.Rows.Count > 0)
                {
                    Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                    ddlcompany.Enabled = false;
                    divlblselectdivison.Visible = false;
                    divlblselectunit.Visible = false;
                }
            }
            else
            {
                DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company", 0, "", "", "CompanyName");
                if (DtCompanyDDL.Rows.Count > 0)
                {
                    Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                    ddlcompany.Enabled = false;
                }
                DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
                if (DtCompanyDDL.Rows.Count > 0)
                {
                    Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                    ddldivision.Items.Insert(0, "Select");
                    divlblselectdivison.Visible = true;
                    divlblselectunit.Visible = false;
                }
                else
                {
                    divlblselectdivison.Visible = false;
                }
            }
        }
        if (mType == "Division" || mType == "Factory")
        {
            if (Request.QueryString["mEdit"] != null)
            {
                DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company1", 0, "", "", "CompanyName");
                if (DtCompanyDDL.Rows.Count > 0)
                {
                    Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                    ddlcompany.Enabled = false;
                }
                DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Factory2", 0, "", "", "CompanyName");
                if (DtCompanyDDL.Rows.Count > 0)
                {
                    Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                    divlblselectdivison.Visible = true;
                    ddldivision.Enabled = false;
                    divlblselectunit.Visible = false;
                }
            }
            else
            {
                DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company1", 0, "", "", "CompanyName");
                if (DtCompanyDDL.Rows.Count > 0)
                {
                    Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                    ddlcompany.Enabled = false;
                }

                DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "",
                    "CompanyName");
                if (DtCompanyDDL.Rows.Count > 0)
                {
                    Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                    divlblselectdivison.Visible = true;
                    ddldivision.Enabled = false;
                }
                DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "Unit1", 0, "", "", "CompanyName");
                if (DtCompanyDDL.Rows.Count > 0)
                {
                    Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
                    ddlunit.Items.Insert(0, "Select");
                    divlblselectunit.Visible = true;
                }
                else
                {
                    divlblselectunit.Visible = false;
                }
            }
        }
        if (mType == "Unit")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company2", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                divlblselectdivison.Visible = true;
                ddldivision.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Unit2", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
                divlblselectunit.Visible = true;
                ddlunit.Enabled = false;
            }
        }
    }
    protected void ddlcompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcompany.SelectedItem.Text != "Select")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                ddldivision.Items.Insert(0, "Select");
                divlblselectdivison.Visible = true;
                divlblselectunit.Visible = false;
            }
            else
            {
                divlblselectdivison.Visible = false;
            }
        }
        else
        {
            divlblselectunit.Visible = false;
            divlblselectdivison.Visible = false;
        }
    }
    protected void ddldivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldivision.SelectedItem.Text != "Select")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "Unit1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
                ddlunit.Items.Insert(0, "Select");
                divlblselectunit.Visible = true;
            }
            else
            {
                divlblselectunit.Visible = false;
            }
        }
        else
        {
            divlblselectunit.Visible = false;
        }
    }
    protected void ddlunit_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlunit.SelectedItem.Text != "Select")
        {
            //DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "Unit1", 0, "", "", "CompanyName");
            //if (DtCompanyDDL.Rows.Count > 0)
            //{
            //    Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
            //    divlblselectunit.Visible = true;
            //    if (Enc.DecryptData(Session["Type"].ToString()) == "SuperAdmin" || Enc.DecryptData(Session["Type"].ToString()) == "Admin" || Enc.DecryptData(Session["Type"].ToString()) == "Division")
            //    { }
            //    else
            //    {
            //        ddlunit.Enabled = false;  
            //    }
            //}
        }
        else
        {

        }
    }
}