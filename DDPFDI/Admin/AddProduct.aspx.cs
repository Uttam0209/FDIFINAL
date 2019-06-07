using BusinessLayer;
using Encryption;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddProduct : System.Web.UI.Page
{
    private Cryptography objEnc = new Cryptography();
    private DataUtility Co = new DataUtility();
    private Logic Lo = new Logic();
    private DataTable dtImage = new DataTable();
    private string DisplayPanel = "";
    private string _msg = string.Empty;
    private string _sysMsg = string.Empty;
    public string Services = "";
    public string Remarks = "";
    public string FinancialServices = "";
    public string FinancialRemarks = "";
    public string ServicesTesting = "";
    public string RemarksTesting = "";
    public string ServicesCertification = "";
    public string RemarksCertification = "";
    public string NodalDDL = "";
    private string UserName;
    private string RefNo;
    private string UserEmail;
    private string currentPage = "";
    public string IsImportedyesYear = "";
    private short Mid = 0;
    private DataTable DtCompanyDDL = new DataTable();
    private HybridDictionary HyPanel1 = new HybridDictionary();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["User"] != null)
            {
                try
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
                            if (MmCval == " View ")
                            {
                                MmCval = "Add";
                            }

                            strheadPage.Append("<li class=''><span>" + MmCval + "</span></li>");
                        }
                        divHeadPage.InnerHtml = strheadPage.ToString().Trim();
                        strheadPage.Append("</ul");
                        ViewState["UserLoginEmail"] = objEnc.DecryptData(Session["User"].ToString()).Trim();
                        if (Request.QueryString["mcurrentcompRefNo"] != null)
                        {
                            hidType.Value = objEnc.DecryptData(Request.QueryString["mrcreaterole"].ToString().Trim());
                            hfcomprefno.Value = objEnc.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString().Trim());
                        }
                        else
                        {
                            hidType.Value = objEnc.DecryptData(Session["Type"].ToString().Trim());
                            hfcomprefno.Value = Session["CompanyRefNo"].ToString().Trim();
                        }
                        if (hidType.Value.ToString() != "SuperAdmin" || hidType.Value.ToString() != "Admin")
                        {
                            BindCompany();
                            BindFinancialYear();
                            BindCountry();
                            tendorstatus();
                            IsProductImported();
                            BindServcies();
                            BindFinancialSupport();
                            BindTesting();
                            BindCertification();
                            if (Request.QueryString["mcurrentcompRefNo"] != null)
                            {
                                BindMasterCategory();
                                BindMasterTechnologyCategory();
                                BindMasterPlatCategory();
                                BindPurposeProcuremnt();
                                BindMasterProductReqCategory();
                                //BindMasterProductNoenCletureCategory();
                                BindEndUser();
                                BindNodelEmail();
                                EditCode();
                            }
                            else
                            {
                                BindMasterCategory();
                                BindMasterTechnologyCategory();
                                BindMasterPlatCategory();
                                BindPurposeProcuremnt();
                                BindMasterProductReqCategory();
                                // BindMasterProductNoenCletureCategory();
                                BindEndUser();
                            }
                        }
                        else
                        {
                            BindCompany();
                            BindFinancialYear();
                            BindCountry();
                            tendorstatus();
                            IsProductImported();
                            BindServcies();
                            BindFinancialSupport();
                            BindTesting();
                            BindCertification();
                        }
                    }
                    if (hidType.Value == "Company")
                    {
                        ddlcompany.Visible = true;
                        divlblselectdivison.Visible = false;
                        divlblselectunit.Visible = false;
                        if (Request.QueryString["mcurrentcompRefNo"] != null)
                        {
                            EditCode();
                        }
                    }
                    else if (hidType.Value == "Division" || hidType.Value == "Factory")
                    {
                        ddlcompany.Visible = true;
                        divlblselectdivison.Visible = true;
                        divlblselectunit.Visible = false;
                        if (Request.QueryString["mcurrentcompRefNo"] != null)
                        {
                            EditCode();
                        }
                    }
                    else if (hidType.Value == "Unit")
                    {
                        ddlcompany.Visible = true;
                        divlblselectdivison.Visible = true;
                        divlblselectunit.Visible = true;
                        if (Request.QueryString["mcurrentcompRefNo"] != null)
                        {
                            EditCode();
                        }
                    }
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
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Session Expire,Please login again');window.location='Login'", true);
            }
        }
    }
    protected void BindCompany()
    {
        if (hidType.Value == "SuperAdmin" || hidType.Value == "Admin")
        {
            if (Request.QueryString["mcurrentcompRefNo"] != null)
            {
                ddlcompany.Enabled = false;
                if (objEnc.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Company")
                {
                    DtCompanyDDL = Lo.RetriveMasterData(0, HttpUtility.UrlEncode(objEnc.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString())), "Company", 0, "", "", "CompanyName");
                    // DtCompanyDDL = Lo.RetriveMasterData(0, objEnc.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "Company", 0, "", "", "CompanyName");
                    ddlcompany.Enabled = false;
                    if (DtCompanyDDL.Rows.Count > 0)
                    {
                        Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                        divlblselectunit.Visible = false;
                        divlblselectdivison.Visible = false;
                        //  EditCode();
                    }
                }
                else if (objEnc.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Factory" || objEnc.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Division")
                {
                    DtCompanyDDL = Lo.RetriveMasterData(0, objEnc.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "Company1", 0, "", "", "CompanyName");
                    ddlcompany.Enabled = false;
                    if (DtCompanyDDL.Rows.Count > 0)
                    {
                        Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                    }
                    DtCompanyDDL = Lo.RetriveMasterData(0, objEnc.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "Factory", 0, "", "", "CompanyName");
                    DataTable DtDivisionDDL = Lo.RetriveMasterData(0, DtCompanyDDL.Rows[0]["CompanyRefNo"].ToString(), "Factory1", 0, "", "", "CompanyName");
                    if (DtDivisionDDL.Rows.Count > 0)
                    {
                        Co.FillDropdownlist(ddldivision, DtDivisionDDL, "FactoryName", "FactoryRefNo");
                        ddldivision.Enabled = false;
                        ddlcompany.Enabled = false;
                        ddldivision.Visible = true;
                        divlblselectunit.Visible = false;
                        // EditCode();
                    }
                    else
                    {
                        ddldivision.Enabled = false;
                    }
                }
                else if (objEnc.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Unit")
                {
                    // DtCompanyDDL = Lo.RetriveMasterData(0, objEnc.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "Company2", 0, "", "", "CompanyName");
                    DtCompanyDDL = Lo.RetriveMasterData(0, objEnc.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "Company2", 0, "", "", "CompanyName");
                    ddlcompany.Enabled = false;
                    if (DtCompanyDDL.Rows.Count > 0)
                    {
                        Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                    }
                    DtCompanyDDL = Lo.RetriveMasterData(0, objEnc.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "Unit", 0, "", "", "CompanyName");
                    DataTable DtDivisionDDL = Lo.RetriveMasterData(0, DtCompanyDDL.Rows[0]["CompanyRefNo"].ToString(), "Factory1", 0, "", "", "CompanyName");
                    if (DtDivisionDDL.Rows.Count > 0)
                    {
                        Co.FillDropdownlist(ddldivision, DtDivisionDDL, "FactoryName", "FactoryRefNo");
                        divlblselectdivison.Visible = true;
                        ddldivision.Enabled = false;
                        ddlcompany.Enabled = false;
                        ddldivision.Visible = true;
                        DataTable DtUnitDDL = Lo.RetriveMasterData(0, DtDivisionDDL.Rows[0]["FactoryRefNo"].ToString(), "Unit1", 0, "", "", "CompanyName");
                        if (DtUnitDDL.Rows.Count > 0)
                        {
                            Co.FillDropdownlist(ddlunit, DtUnitDDL, "UnitName", "UnitRefNo");
                            ddlunit.Enabled = true;
                            divlblselectunit.Visible = true;
                            ddlunit.Visible = true;
                            ddlunit.Enabled = false;
                            // EditCode();
                        }
                        else
                        {
                            ddlunit.Enabled = false;
                        }
                    }
                    else
                    {
                        ddldivision.Enabled = false;
                    }
                }
            }
            else
            {
                DtCompanyDDL = Lo.RetriveMasterData(0, "", hidType.Value, 0, "", "", "Select");
                if (DtCompanyDDL.Rows.Count > 0)
                {
                    Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                    ddlcompany.Items.Insert(0, "Select");
                    ddlcompany.Enabled = true;
                    divlblselectdivison.Visible = false;
                    divlblselectunit.Visible = false;
                }
                else
                {
                    ddlcompany.Enabled = false;
                }
            }
        }
        else if (hidType.Value == "Company")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, hfcomprefno.Value, "Company", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = false;
                ddldivision.Items.Insert(0, "Select");
                divlblselectdivison.Visible = false;
                divlblselectunit.Visible = false;
                BindNodelEmail();
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
                if (hidType.Value == "Company")
                {
                    divlblselectdivison.Visible = true;
                    ddldivision.Enabled = true;
                    divlblselectunit.Visible = false;
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
        else if (hidType.Value == "Factory" || hidType.Value == "Division")
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
                // code by gk to select indivisual division for the particular unit
                DataTable dt = Lo.RetriveMasterData(0, hfcomprefno.Value, "Factory3", 0, "", "", "CompanyName");
                if (dt.Rows.Count > 0)
                    ddldivision.SelectedValue = dt.Rows[0]["FactoryRefNo"].ToString();
                //end code
                ddlunit.Items.Insert(0, "Select");
                BindNodelEmail();
                divlblselectunit.Visible = false;
                divlblselectdivison.Visible = true;
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
                ddlunit.Items.Insert(0, "Select");
                ddlunit.Enabled = true;
                divlblselectunit.Visible = true;
            }
            else
            {
                ddlunit.Items.Insert(0, "Select");
                divlblselectunit.Visible = false;
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
                // code by gk to select indivisual division for the particular unit
                DataTable dt = Lo.RetriveMasterData(0, hfcomprefno.Value, "Factory3", 0, "", "", "CompanyName");
                if (dt.Rows.Count > 0)
                    ddldivision.SelectedValue = dt.Rows[0]["FactoryRefNo"].ToString();
                //end code
                divlblselectdivison.Visible = true;
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
                // code by gk to select indivisual unit for the particular unit             
                ddlunit.SelectedValue = hfcomprefno.Value;
                //end code
                BindNodelEmail();
                ddlunit.Enabled = false;
                divlblselectunit.Visible = true;
            }
            else
            {
                ddlunit.Enabled = false;
            }
        }
    }
    protected void BindNodelEmail()
    {
        if (ddlcompany.SelectedItem.Text != "Select")
        {
            if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.Visible == false || ddldivision.SelectedItem.Text == "Select")
            {
                DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "", 0, "", "", "CompanyNodelDetail");
            }
            else if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.SelectedItem.Text != "Select" && ddlunit.Visible == false || ddlunit.SelectedItem.Text == "Select")
            {
                DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "", 0, "", "", "CompanyNodelDetail");
            }
            else if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.SelectedItem.Text != "Select" && ddlunit.SelectedItem.Text != "Select")
            {
                DtCompanyDDL = Lo.RetriveMasterData(0, ddlunit.SelectedItem.Value, "", 0, "", "", "CompanyNodelDetail");
            }
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlNodalOfficerEmail, DtCompanyDDL, "NodalOficerName", "NodalOfficerID");
                ddlNodalOfficerEmail.Items.Insert(0, "Select");
                //Co.FillDropdownlist(ddlNodalOfficerEmail2, DtCompanyDDL, "NodalOficerName", "NodalOfficerID");
                //ddlNodalOfficerEmail2.Items.Insert(0, "Select");
            }
            else
            {
                contactpanel1.Visible = false;
                ddlNodalOfficerEmail.Items.Clear();
                //  ddlNodalOfficerEmail.Items.Insert(0, "Select");
                //ddlNodalOfficerEmail2.Items.Insert(0, "Select");
            }
        }
        else
        {
            contactpanel1.Visible = false;
            ddlNodalOfficerEmail.Items.Clear();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('select Company for displayed nodal offcier')", true);
        }
    }
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
                divlblselectdivison.Visible = true;
                ddldivision.Visible = true;
                hidCompanyRefNo.Value = ddlcompany.SelectedItem.Value;
                hidType.Value = "Company";
                BindMasterCategory();
                BindMasterTechnologyCategory();
                //  BindMasterPlatCategory();
                BindMasterProductReqCategory();
                // BindMasterProductNoenCletureCategory();
                BindEndUser();
            }
            else
            {
                ddldivision.Items.Insert(0, "Select");
                ddldivision.Visible = false;
                divlblselectdivison.Visible = false;
            }
        }
        else if (ddlcompany.SelectedItem.Text == "Select")
        {
            divlblselectdivison.Visible = false;
            divlblselectunit.Visible = false;
        }
        hfcomprefno.Value = "";
        hfcomprefno.Value = ddlcompany.SelectedItem.Value;
        BindNodelEmail();
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
                divlblselectunit.Visible = true;
                if (ddlunit.SelectedItem.Text == "Select")
                {
                    ddldivision.Enabled = true;
                }
                else
                { ddldivision.Enabled = false; }
                hidCompanyRefNo.Value = ddldivision.SelectedItem.Value;
                hidType.Value = "Divison";
            }
            else
            {
                ddlunit.Items.Insert(0, "Select");
                divlblselectunit.Visible = false;
                ddlunit.Visible = false;
            }
            hfcomprefno.Value = "";
            hfcomprefno.Value = ddldivision.SelectedItem.Value;
            BindNodelEmail();
        }
        else if (ddldivision.SelectedItem.Text == "Select")
        {
            //ddlcompany.Enabled = false;
            divlblselectunit.Visible = false;
            hfcomprefno.Value = ddlcompany.SelectedItem.Value;
            hidType.Value = "Company";
            BindNodelEmail();
        }
    }
    protected void ddlunit_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlunit.SelectedItem.Text != "Select")
        {
            hidCompanyRefNo.Value = ddlunit.SelectedItem.Value;
            hidType.Value = "Unit";
            hfcomprefno.Value = "";
            hfcomprefno.Value = ddlunit.SelectedItem.Value;
            // ddlunit.Items.Insert(0, "Select");
            BindNodelEmail();
        }
        else
        {
            hidCompanyRefNo.Value = ddldivision.SelectedItem.Value;
            hidType.Value = "Division";
            // ddldivision.Enabled = false;
            hfcomprefno.Value = "";
            hfcomprefno.Value = ddldivision.SelectedItem.Value;
            BindNodelEmail();
        }
    }
    protected void ddlNodalOfficerEmail_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindNodelEmail1();
    }
    protected void BindNodelEmail1()
    {
        if (ddlNodalOfficerEmail.SelectedItem.Text != "Select")
        {
            DataTable DtGetNodel = Lo.RetriveMasterData(Convert.ToInt16(ddlNodalOfficerEmail.SelectedItem.Value), "", "", 0, "", "", "SearchNodalOfficerID");
            if (DtGetNodel.Rows.Count > 0)
            {
                contactpanel1.Visible = true;
                // txtNName.Text = DtGetNodel.Rows[0]["NodalOficerName"].ToString();
                txtNEmailId.Text = DtGetNodel.Rows[0]["NodalOfficerEmail"].ToString();
                txtNTelephone.Text = DtGetNodel.Rows[0]["NodalOfficerTelephone"].ToString();
                txtNFaxNo.Text = DtGetNodel.Rows[0]["NodalOfficerFax"].ToString();
                txtDesignation.Text = DtGetNodel.Rows[0]["Designation"].ToString();
                txtempcode.Text = DtGetNodel.Rows[0]["NodalEmpCode"].ToString();
                txtmobnodal.Text = DtGetNodel.Rows[0]["NodalOfficerMobile"].ToString();
                //===Bind Nodel officer except Nodel one
                //DtCompanyDDL = Lo.RetriveMasterData(Convert.ToInt16(ddlNodalOfficerEmail.SelectedItem.Value), hfcomprefno.Value, "", 0, "", "", "AllNodelNotSelect");
                //if (DtCompanyDDL.Rows.Count > 0)
                //{
                //    Co.FillDropdownlist(ddlNodalOfficerEmail2, DtCompanyDDL, "NodalOficerName", "NodalOfficerID");
                //    ddlNodalOfficerEmail2.Items.Insert(0, "Select");
                //}
                //else
                //{
                //    divnodal2.Visible = false;
                //}
            }
        }
        else
        {
            contactpanel1.Visible = false;
        }
    }
    protected void ddlNodalOfficerEmail2_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindNodal2();
    }
    protected void BindNodal2()
    {
        if (ddlNodalOfficerEmail2.SelectedItem.Text != "Select")
        {
            DataTable DtGetNodel = Lo.RetriveMasterData(Convert.ToInt16(ddlNodalOfficerEmail2.SelectedItem.Value), "", "", 0, "", "", "SearchNodalOfficerID");
            if (DtGetNodel.Rows.Count > 0)
            {
                contactpanel2.Visible = true;
                // txtNName2.Text = DtGetNodel.Rows[0]["NodalOficerName"].ToString();
                txtNEmailId2.Text = DtGetNodel.Rows[0]["NodalOfficerEmail"].ToString();
                txtNTelephone2.Text = DtGetNodel.Rows[0]["NodalOfficerTelephone"].ToString();
                txtNFaxNo2.Text = DtGetNodel.Rows[0]["NodalOfficerFax"].ToString();
                txtdesignationnodal2.Text = DtGetNodel.Rows[0]["Designation"].ToString();
                txtempcode2.Text = DtGetNodel.Rows[0]["NodalEmpCode"].ToString();
                txtmobnodal2.Text = DtGetNodel.Rows[0]["NodalOfficerMobile"].ToString();
                //===Bind Nodel officer expect Nodel Two                
                DtCompanyDDL = Lo.RetriveMasterData(Convert.ToInt16(ddlNodalOfficerEmail2.SelectedItem.Value), hfcomprefno.Value, "", 0, "", "", "AllNodelNotSelect");
                if (DtCompanyDDL.Rows.Count > 0)
                {
                    Co.FillDropdownlist(ddlNodalOfficerEmail, DtCompanyDDL, "NodalOficerName", "NodalOfficerID");
                    if (ddlNodalOfficerEmail.SelectedItem.Value != "Select")
                    {
                    }
                    else
                    {
                        ddlNodalOfficerEmail.Items.Insert(0, "Select");
                    }
                }
                else
                {
                    divnodal.Visible = false;
                }
            }
        }
        else
        {
            contactpanel2.Visible = false;
        }
    }
    protected void ddlmastercategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMasterSubCategory();
    }
    protected void ddlsubcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMaster3levelSubCategory();
        NSCCode(ddlmastercategory.SelectedItem.Text, ddlsubcategory.SelectedItem.Text);
    }
    protected void ddllevel3product_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindItemDescription();
    }
    protected void ddltechnologycat_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMasterSubCategoryTech();
    }
    protected void ddlsubtech_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMasterSubCategoryTechLevel3();
    }
    protected void rbisindinised_CheckedChanged(object sender, EventArgs e)
    {
        if (rbisindinised.SelectedItem.Value == "Y")
        {
            divisIndigenized.Visible = true;
        }
        else if (rbisindinised.SelectedItem.Value == "N")
        {
            divisIndigenized.Visible = false;
        }
    }
    protected void rbtendordateyesno_CheckedChanged(object sender, EventArgs e)
    {
        if (rbtendordateyesno.SelectedItem.Value == "Y")
        {
            divtdate.Visible = true;
        }
        else if (rbtendordateyesno.SelectedItem.Value == "N")
        {
            divtdate.Visible = false;
        }
    }
    protected void ddltendorstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        tendorstatus();
    }
    protected void ddlplatform_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlplatform.SelectedItem.Text != "Select")
        {
            BindMasterProductNoenCletureCategory();
        }
        else
        {
            ddlnomnclature.Items.Clear();
        }
    }
    protected void tendorstatus()
    {
        if (ddltendorstatus.SelectedItem.Value == "Live" && rbtendordateyesno.SelectedItem.Value == "Y")
        {
            divtendordate.Visible = true;
            divtdate.Visible = true;
        }
        else
        {
            if (rbtendordateyesno.SelectedItem.Value == "N" && ddltendorstatus.SelectedItem.Value == "Live")
            {
                divtendordate.Visible = true;
                divtdate.Visible = false;
            }
            else if (rbtendordateyesno.SelectedItem.Value == "Y")
            {
                divtendordate.Visible = true;
                divtdate.Visible = true;
            }
            else
            {
                divtendordate.Visible = false;
                divtdate.Visible = false;
            }
        }
    }
    protected void rbproductImported_CheckedChanged(object sender, EventArgs e)
    {
        if (rbproductImported.SelectedItem.Value == "Y")
        {
            divyearofimportNo.Visible = false;
            divyearofimportYes.Visible = true;
        }
        else if (rbproductImported.SelectedItem.Value == "N")
        {
            divyearofimportNo.Visible = true;
            divyearofimportYes.Visible = false;
        }
    }
    protected void IsProductImported()
    {
        if (rbproductImported.SelectedItem.Value == "Y")
        {
            divyearofimportNo.Visible = false;
            divyearofimportYes.Visible = true;
        }
        else if (rbproductImported.SelectedItem.Value == "N")
        {
            divyearofimportNo.Visible = true;
            divyearofimportYes.Visible = false;
        }
    }
    #endregion
    #region BindServices Testing Certification
    protected void BindServcies()
    {
        DataTable Dtservices = Lo.RetriveMasterSubCategoryDate(0, "TECHNICAL SUPPORT", "", "SelectInnerMaster1", hfcomprefno.Value, "");
        if (Dtservices.Rows.Count > 0)
        {
            gvservices.DataSource = Dtservices;
            gvservices.DataBind();
        }
    }
    protected void BindFinancialSupport()
    {
        DataTable DtFinanicialSupp = Lo.RetriveMasterSubCategoryDate(0, "Financial Support", "", "SelectInnerMaster1", hfcomprefno.Value, "");
        if (DtFinanicialSupp.Rows.Count > 0)
        {
            gvfinancialsupp.DataSource = DtFinanicialSupp;
            gvfinancialsupp.DataBind();
        }
    }
    protected void BindTesting()
    {
        DataTable DtTesting = Lo.RetriveMasterSubCategoryDate(0, "Testing", "", "SelectInnerMaster1", hfcomprefno.Value, "");
        if (DtTesting.Rows.Count > 0)
        {
            gvtesting.DataSource = DtTesting;
            gvtesting.DataBind();
        }
    }
    protected void BindCertification()
    {
        DataTable DtCertification = Lo.RetriveMasterSubCategoryDate(0, "Certification", "", "SelectInnerMaster1", hfcomprefno.Value, "");
        if (DtCertification.Rows.Count > 0)
        {
            gvCertification.DataSource = DtCertification;
            gvCertification.DataBind();
        }
    }
    #endregion
    #region For ProductCode
    protected void BindMasterCategory()
    {
        DataTable DtMasterCategroy = new DataTable();
        if (ddlcompany.SelectedItem.Text != "Select")
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "NSN GROUP", "", "SelectProductCat", ddlcompany.SelectedItem.Value, "");
        }
        else
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "NSN GROUP", "", "SelectProductCat", "", "");
        }
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlmastercategory, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlmastercategory.Items.Insert(0, "Select");
        }
        else
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "NSN GROUP", "", "SelectProductCat", "", "");
            Co.FillDropdownlist(ddlmastercategory, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlmastercategory.Items.Insert(0, "Select");
        }
    }
    protected void BindMasterSubCategory()
    {
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(ddlmastercategory.SelectedItem.Value), "", "", "SubSelectID", "", "");
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlsubcategory, DtMasterCategroy, "SCategoryName", "SCategoryId");
            ddlsubcategory.Items.Insert(0, "Select");
        }
        else
        {
            ddlsubcategory.Items.Clear();
            ddlsubcategory.Items.Insert(0, "Select");
        }
    }
    protected void BindMaster3levelSubCategory()
    {
        if (ddlsubcategory.SelectedItem.Value != null || ddlsubcategory.SelectedItem.Text != "Select")
        {
            DataTable DtMasterCategroyLevel3 = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(ddlsubcategory.SelectedItem.Value), "", "", "SubSelectID", "", "");
            if (DtMasterCategroyLevel3.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddllevel3product, DtMasterCategroyLevel3, "SCategoryName", "SCategoryId");
                ddllevel3product.Items.Insert(0, "Select");
            }
            else
            {
                ddllevel3product.Items.Clear();
                ddllevel3product.Items.Insert(0, "Select");
            }
        }
    }
    protected void BindItemDescription()
    {
        if (ddllevel3product.SelectedItem.Value != null || ddllevel3product.SelectedItem.Text != "Select")
        {
            DataTable DtItemDescription = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(ddllevel3product.SelectedItem.Value), "", "", "Level3ID", "", "");
            if (DtItemDescription.Rows.Count > 0)
            {
                txtproductdescription.Text = DtItemDescription.Rows[0]["Description"].ToString();
            }
            else
            {
                txtproductdescription.Text = "";
            }
        }
    }
    #endregion
    #region For Technology
    protected void BindMasterTechnologyCategory()
    {
        DataTable DtMasterCategroy = new DataTable();
        if (ddlcompany.SelectedItem.Text != "Select")
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "PRODUCT (INDUSTRY DOMAIN)", "", "SelectProductCat", ddlcompany.SelectedItem.Value, "");
        }
        else
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "PRODUCT (INDUSTRY DOMAIN)", "", "SelectProductCat", "", "");
        }
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddltechnologycat, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddltechnologycat.Items.Insert(0, "Select");
        }
        else
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "PRODUCT (INDUSTRY DOMAIN)", "", "SelectProductCat", "", "");
            Co.FillDropdownlist(ddltechnologycat, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddltechnologycat.Items.Insert(0, "Select");
        }
    }
    protected void BindMasterSubCategoryTech()
    {
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(ddltechnologycat.SelectedItem.Value), "", "", "SubSelectID", "", "");
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlsubtech, DtMasterCategroy, "SCategoryName", "SCategoryId");
            ddlsubtech.Items.Insert(0, "Select");
        }
        else
        {
            ddlsubtech.Items.Clear();
            ddlsubtech.Items.Insert(0, "Select");
        }
    }
    protected void BindMasterSubCategoryTechLevel3()
    {
        if (ddlsubtech.SelectedItem.Value != null || ddlsubtech.SelectedItem.Text != "Select")
        {
            DataTable DtMasterCategroyLevel3 = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(ddlsubtech.SelectedItem.Value), "", "", "SubSelectID", "", "");
            if (DtMasterCategroyLevel3.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddltechlevel3, DtMasterCategroyLevel3, "SCategoryName", "SCategoryId");
                ddltechlevel3.Items.Insert(0, "Select");
            }
            else
            {
                ddltechlevel3.Items.Clear();
                ddltechlevel3.Items.Insert(0, "Select");
            }
        }
    }
    #endregion
    #region For PlatForm
    protected void BindMasterPlatCategory()
    {
        DataTable DtMasterCategroy = new DataTable();
        if (ddlcompany.SelectedItem.Text != "Select")
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "DEFENCE PLATFORM", "", "SelectProductCat", ddlcompany.SelectedItem.Value, "");
        }
        else
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "DEFENCE PLATFORM", "", "SelectProductCat", "", "");
        }
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlplatform, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlplatform.Items.Insert(0, "Select");
        }
        else
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "DEFENCE PLATFORM", "", "SelectProductCat", "", "");
            Co.FillDropdownlist(ddlplatform, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlplatform.Items.Insert(0, "Select");
        }
    }
    #endregion
    #region For PROCURMENT CATEGORY
    protected void BindPurposeProcuremnt()
    {
        DataTable DtPurposeProcuremnt = new DataTable();
        if (ddlcompany.SelectedItem.Text != "Select")
        {
            DtPurposeProcuremnt = Lo.RetriveMasterSubCategoryDate(0, "PROCURMENT CATEGORY", "", "SelectProductCat", ddlcompany.SelectedItem.Value, "");
        }
        else
        {
            DtPurposeProcuremnt = Lo.RetriveMasterSubCategoryDate(0, "PROCURMENT CATEGORY", "", "SelectProductCat", "", "");
        }
        if (DtPurposeProcuremnt.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlprocurmentcategory, DtPurposeProcuremnt, "SCategoryName", "SCategoryID");
            ddlprocurmentcategory.Items.Insert(0, "Select");
        }
        else
        {
            DtPurposeProcuremnt = Lo.RetriveMasterSubCategoryDate(0, "PROCURMENT CATEGORY", "", "SelectProductCat", "", "");
            Co.FillDropdownlist(ddlprocurmentcategory, DtPurposeProcuremnt, "SCategoryName", "SCategoryID");
            ddlprocurmentcategory.Items.Insert(0, "Select");
        }
    }
    #endregion
    #region For Procurement Time Frame
    protected void BindMasterProductReqCategory()
    {
        DataTable DtMasterCategroy = new DataTable();
        if (ddlcompany.SelectedItem.Text != "Select")
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, lblprodrequir.Text, "", "SelectInnerMaster1", ddlcompany.SelectedItem.Value, "");
        }
        else
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, lblprodrequir.Text, "", "SelectInnerMaster1", "", "");
        }
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlproctimeframe, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlproctimeframe.Items.Insert(0, "Select");
        }
        else
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, lblprodrequir.Text, "", "SelectInnerMaster1", "", "");
            Co.FillDropdownlist(ddlproctimeframe, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlproctimeframe.Items.Insert(0, "Select");
        }
    }
    #endregion
    #region For NomenClature
    protected void BindMasterProductNoenCletureCategory()
    {
        //DataTable DtMasterCategroy = new DataTable();
        //if (ddlcompany.SelectedItem.Text != "Select")
        //{
        //    DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "NAME OF DEFENCE PLATFORM", "", "SelectInnerMaster1", ddlcompany.SelectedItem.Value, "");
        //}
        //else
        //{
        //    DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "NAME OF DEFENCE PLATFORM", "", "SelectInnerMaster1", "", "");
        //}
        //if (DtMasterCategroy.Rows.Count > 0)
        //{
        //    Co.FillDropdownlist(ddlnomnclature, DtMasterCategroy, "SCategoryName", "SCategoryID");
        //    ddlnomnclature.Items.Insert(0, "Select");
        //}
        //else
        //{
        //    DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "NAME OF DEFENCE PLATFORM", "", "SelectInnerMaster1", "", "");
        //    Co.FillDropdownlist(ddlnomnclature, DtMasterCategroy, "SCategoryName", "SCategoryID");
        //    ddlnomnclature.Items.Insert(0, "Select");
        //}
        DataTable DtNAMEOFDEFENCEPLATFORM = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(ddlplatform.SelectedItem.Value), "", "", "SubSelectID", "", "");
        if (DtNAMEOFDEFENCEPLATFORM.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlnomnclature, DtNAMEOFDEFENCEPLATFORM, "SCategoryName", "SCategoryId");
            ddlnomnclature.Items.Insert(0, "Select");
        }
        else
        {
            ddlnomnclature.Items.Clear();
            ddlnomnclature.Items.Insert(0, "Select");
        }
    }
    #endregion
    #region For EndUser
    protected void BindEndUser()
    {
        DataTable DtMasterCategroy = new DataTable();
        if (ddlcompany.SelectedItem.Text != "Select")
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, lblenduser.Text, "", "SelectInnerMaster1", ddlcompany.SelectedItem.Value, "");
        }
        else
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, lblenduser.Text, "", "SelectInnerMaster1", "", "");
        }
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlenduser, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlenduser.Items.Insert(0, "Select");
        }
        else
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, lblenduser.Text, "", "SelectInnerMaster1", "", "");
            Co.FillDropdownlist(ddlenduser, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlenduser.Items.Insert(0, "Select");
        }
    }
    #endregion
    #region For FinancialYear
    protected void BindFinancialYear()
    {
        DataTable MasterFinancialYear = Lo.RetriveMasterSubCategoryDate(0, "", "", "AllFinancialYear", "", "");
        if (MasterFinancialYear.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlyearofindiginization, MasterFinancialYear, "FY", "FYID");
            ddlyearofindiginization.Items.Insert(0, "Select");
        }
    }
    #endregion
    #region For Country
    protected void BindCountry()
    {
        DataTable DtCountry = Lo.RetriveCountry("Select");
        if (DtCountry.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlcountry, DtCountry, "CountryName", "CountryID");
            ddlcountry.Items.Insert(0, "Select");
        }
    }
    #endregion
    #region PanelSaveCode
    protected void SaveProductDescription()
    {
        if (hfprodid.Value != "")
        {
            HyPanel1["ProductID"] = Convert.ToInt16(hfprodid.Value);
            HyPanel1["ProductRefNo"] = Co.RSQandSQLInjection(hfprodrefno.Value.Trim(), "soft");
        }
        else
        {
            HyPanel1["ProductID"] = 0;
        }
        if (hfprodid.Value != "")
        {
            if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.Visible == false)
            {
                HyPanel1["CompanyRefNo"] = Co.RSQandSQLInjection(ddlcompany.SelectedItem.Value, "soft");
                hfcomprefno.Value = ddlcompany.SelectedItem.Value;
                hidType.Value = "Company";
                HyPanel1["Role"] = hidType.Value.ToString();
            }
            else if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.SelectedItem.Text != "Select" && ddlunit.Visible == false)
            {
                HyPanel1["CompanyRefNo"] = Co.RSQandSQLInjection(ddldivision.SelectedItem.Value, "soft");
                hfcomprefno.Value = ddldivision.SelectedItem.Value;
                hidType.Value = "Division";
                HyPanel1["Role"] = hidType.Value.ToString();
            }
            else if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.SelectedItem.Text != "Select" && ddlunit.SelectedItem.Text != "Select")
            {
                HyPanel1["CompanyRefNo"] = Co.RSQandSQLInjection(ddlunit.SelectedItem.Value, "soft");
                hfcomprefno.Value = ddlunit.SelectedItem.Value;
                hidType.Value = "Unit";
                HyPanel1["Role"] = hidType.Value.ToString();
            }
        }
        else
        {
            if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.SelectedItem.Text == "Select" || ddldivision.Visible == false)
            {
                HyPanel1["CompanyRefNo"] = Co.RSQandSQLInjection(ddlcompany.SelectedItem.Value, "soft");
                hidType.Value = "Company";
                hfcomprefno.Value = ddlcompany.SelectedItem.Value;
                HyPanel1["Role"] = hidType.Value.ToString();
            }
            else if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.SelectedItem.Text != "Select" && ddlunit.SelectedItem.Text == "Select" || ddlunit.SelectedItem.Text == "Select")
            {
                HyPanel1["CompanyRefNo"] = Co.RSQandSQLInjection(ddldivision.SelectedItem.Value, "soft");
                hidType.Value = "Division";
                hfcomprefno.Value = ddldivision.SelectedItem.Value;
                HyPanel1["Role"] = hidType.Value.ToString();
            }
            else if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.SelectedItem.Text != "Select" && ddlunit.SelectedItem.Text != "Select")
            {
                HyPanel1["CompanyRefNo"] = Co.RSQandSQLInjection(ddlunit.SelectedItem.Value, "soft");
                hidType.Value = "Unit";
                hfcomprefno.Value = ddlunit.SelectedItem.Value;
                HyPanel1["Role"] = hidType.Value.ToString();
            }
        }
        if (ddlmastercategory.SelectedItem.Value != "Select")
        {
            HyPanel1["ProductLevel1"] = Co.RSQandSQLInjection(ddlmastercategory.SelectedItem.Value, "soft");
        }
        else
        {
            HyPanel1["ProductLevel1"] = null;
        }
        if (ddlsubcategory.SelectedItem.Value != "Select")
        {
            HyPanel1["ProductLevel2"] = Co.RSQandSQLInjection(ddlsubcategory.SelectedItem.Value, "soft");
        }
        else
        {
            HyPanel1["ProductLevel2"] = null;
        }
        if (ddllevel3product.SelectedItem.Value != "Select")
        {
            HyPanel1["ProductLevel3"] = Co.RSQandSQLInjection(ddllevel3product.SelectedItem.Value, "soft");
        }
        else
        {
            HyPanel1["ProductLevel3"] = null;
        }
        HyPanel1["ProductDescription"] = Co.RSQandSQLInjection(txtproductdescription.Text.Trim(), "soft");
        HyPanel1["NSCCode"] = Co.RSQandSQLInjection(txtnsccode.Text.Trim(), "soft");
        HyPanel1["NIINCode"] = Co.RSQandSQLInjection(txtniincode.Text.Trim(), "soft");
        if (fuitemdescriptionfile.HasFile != false)
        {
            PDFFileItemDescription();
        }
        else
        {
            if (hfprodid.Value == "")
            {
                HyPanel1["ItemDescriptionPDFFile"] = "";
            }
        }
        HyPanel1["OEMPartNumber"] = Co.RSQandSQLInjection(txtoempartnumber.Text.Trim(), "soft");
        HyPanel1["OEMName"] = Co.RSQandSQLInjection(txtoemname.Text.Trim(), "soft");
        if (ddlcountry.SelectedItem.Text == "Select")
        {
            HyPanel1["OEMCountry"] = null;
        }
        else
        {
            HyPanel1["OEMCountry"] = Convert.ToInt64(ddlcountry.SelectedItem.Value.ToString());
        }
        HyPanel1["DPSUPartNumber"] = Co.RSQandSQLInjection(txtdpsupartnumber.Text.Trim(), "soft");
        HyPanel1["EndUserPartNumber"] = Co.RSQandSQLInjection(txtenduserpartnumber.Text.Trim(), "soft");
        HyPanel1["HSNCode"] = Co.RSQandSQLInjection(txthsncode.Text.Trim(), "soft");
        HyPanel1["NatoCode"] = "";//Co.RSQandSQLInjection(txtnatocode.Text, "soft");
        HyPanel1["ERPRefNo"] = Co.RSQandSQLInjection(txterprefno.Text.Trim(), "soft");
        if (ddltechnologycat.SelectedItem.Value != "Select")
        {
            HyPanel1["TechnologyLevel1"] = Co.RSQandSQLInjection(ddltechnologycat.SelectedItem.Value, "soft");
        }
        else
        {
            HyPanel1["TechnologyLevel1"] = null;
        }
        if (ddlsubtech.SelectedItem.Value != "Select")
        {
            HyPanel1["TechnologyLevel2"] = Co.RSQandSQLInjection(ddlsubtech.SelectedItem.Value, "soft");
        }
        else
        {
            HyPanel1["TechnologyLevel2"] = null;
        }
        if (ddltechlevel3.SelectedItem.Value != "Select")
        {
            HyPanel1["TechnologyLevel3"] = Co.RSQandSQLInjection(ddltechlevel3.SelectedItem.Value, "soft");
        }
        else
        {
            HyPanel1["TechnologyLevel3"] = null;
        }
        if (ddlplatform.SelectedItem.Value != "Select")
        {
            HyPanel1["Platform"] = Co.RSQandSQLInjection(ddlplatform.SelectedItem.Value, "soft");
        }
        else
        {
            HyPanel1["Platform"] = null;
        }
        if (ddlnomnclature.SelectedItem.Value != "Select")
        {
            HyPanel1["NomenclatureOfMainSystem"] = Co.RSQandSQLInjection(ddlnomnclature.SelectedItem.Value, "soft");
        }
        else
        {
            HyPanel1["NomenclatureOfMainSystem"] = null;
        }
        if (ddlenduser.SelectedItem.Value != "Select")
        {
            HyPanel1["EndUser"] = Co.RSQandSQLInjection(ddlenduser.SelectedItem.Value, "soft");
        }
        else
        {
            HyPanel1["EndUser"] = null;
        }
        if (ddlprocurmentcategory.SelectedItem.Value != "Select")
        {
            HyPanel1["PurposeofProcurement"] = Co.RSQandSQLInjection(ddlprocurmentcategory.SelectedItem.Value, "soft");
        }
        else
        {
            HyPanel1["PurposeofProcurement"] = null;
        }
        HyPanel1["ProcurmentCategoryRemark"] = Co.RSQandSQLInjection(txtremarkspro.Text.Trim(), "soft");
        //if (ddlproctimeframe.SelectedItem.Value != "Select")
        //{
        //    HyPanel1["ProductRequirment"] = Co.RSQandSQLInjection(ddlproctimeframe.SelectedItem.Value, "soft");
        //}
        //else
        //{
        HyPanel1["ProductRequirment"] = null;
        //}
        HyPanel1["IsIndeginized"] = Co.RSQandSQLInjection(rbisindinised.SelectedItem.Value, "soft");
        HyPanel1["ManufactureName"] = Co.RSQandSQLInjection(txtmanufacturename.Text.Trim(), "soft");
        HyPanel1["ManufactureAddress"] = Co.RSQandSQLInjection(txtmanifacaddress.Text.Trim(), "soft");
        if (ddlyearofindiginization.SelectedItem.Text == "Select")
        {
            HyPanel1["YearofIndiginization"] = null;
        }
        else
        {
            HyPanel1["YearofIndiginization"] = Co.RSQandSQLInjection(ddlyearofindiginization.SelectedItem.Value, "soft");
        }
        HyPanel1["SearchKeyword"] = Co.RSQandSQLInjection(txtsearchkeyword.Text.Trim(), "soft");
        if (files.HasFiles != false)
        {
            if (hfprodid.Value != "")
            {
                DataTable dtImageBind = Lo.RetriveProductCode("", hfprodrefno.Value, "RetriveImage", hidType.Value);
                if (dtImageBind.Rows.Count > 0)
                {
                    short CountImageTotal = Convert.ToInt16(files.PostedFiles.Count);
                    short AlreadyUploadImage = Convert.ToInt16(dtImageBind.Rows.Count);
                    ImageMaxCount = (CountImageTotal + AlreadyUploadImage);
                    if (ImageMaxCount <= 4)
                    {
                        dtImage = imagedb();
                    }
                }
                else
                {
                    if (files.HasFiles != false)
                    {
                        ImageMaxCount = 4;
                        dtImage = imagedb();
                    }
                }

            }
            else
            {
                ImageMaxCount = 4;
                dtImage = imagedb();
            }
        }

        if (rbproductImported.SelectedItem.Value == "N")
        {
            HyPanel1["IsProductImported"] = Co.RSQandSQLInjection(rbproductImported.SelectedItem.Value.Trim(), "soft");
            HyPanel1["YearofImport"] = Co.RSQandSQLInjection(chkyearofimportall.SelectedItem.Value, "soft");
            HyPanel1["YearofImportRemarks"] = Co.RSQandSQLInjection(txtyearofimportremarksno.Text.Trim(), "soft");
        }
        else
        {
            HyPanel1["IsProductImported"] = Co.RSQandSQLInjection(rbproductImported.SelectedItem.Value.Trim(), "soft");
            foreach (CheckBoxList chk in chklistimportyearfive.Items)
            {
                if (chk.SelectedItem.Selected == true)
                {
                    IsImportedyesYear = IsImportedyesYear + "," + chk.SelectedItem.Value;
                }
            }
            HyPanel1["YearofImport"] = Co.RSQandSQLInjection(IsImportedyesYear.ToString(), "soft");
            HyPanel1["YearofImportRemarks"] = Co.RSQandSQLInjection(txtremarksyearofimportyes.Text.Trim(), "soft");
        }
        foreach (GridViewRow rw in gvservices.Rows)
        {
            CheckBox chkBx = (CheckBox)rw.FindControl("chk");
            HiddenField hfservicesid = (HiddenField)rw.FindControl("hfservicesid");
            TextBox txtRemarks = (TextBox)rw.FindControl("txtRemarks");
            if (chkBx != null && chkBx.Checked)
            {
                Services = Services + "," + hfservicesid.Value;
                if (txtRemarks.Text != "")
                {
                    Remarks = Remarks + "," + txtRemarks.Text;
                }
                else
                { }
            }
        }
        if (Services != "")
        {
            HyPanel1["DPSUServices"] = Co.RSQandSQLInjection(Services.Substring(1).ToString() + ",", "soft");
            if (Remarks != "")
            {
                HyPanel1["Remarks"] = Co.RSQandSQLInjection(Remarks.Substring(1).ToString() + ",", "soft");
            }
            else
            { HyPanel1["Remarks"] = ""; }
        }
        else
        {
            HyPanel1["DPSUServices"] = "";
            HyPanel1["Remarks"] = "";
        }
        foreach (GridViewRow rw in gvfinancialsupp.Rows)
        {
            CheckBox chkfinanBx = (CheckBox)rw.FindControl("chkfinan");
            HiddenField hffinanservicesid = (HiddenField)rw.FindControl("hffinanciailserviceid");
            TextBox txtFinancialRemarks = (TextBox)rw.FindControl("txtfinancialRemarks");
            if (chkfinanBx != null && chkfinanBx.Checked)
            {
                FinancialServices = FinancialServices + "," + hffinanservicesid.Value;
                if (txtFinancialRemarks.Text != "")
                {
                    FinancialRemarks = FinancialRemarks + "," + txtFinancialRemarks.Text;
                }
            }
        }
        if (FinancialServices != "")
        {
            HyPanel1["FinancialSupport"] = Co.RSQandSQLInjection(FinancialServices.Substring(1).ToString() + ",", "soft");
            if (FinancialRemarks != "")
            {
                HyPanel1["FinancialRemark"] =
                    Co.RSQandSQLInjection(FinancialRemarks.Substring(1).ToString() + ",", "soft");
            }
            else
            { HyPanel1["FinancialRemark"] = ""; }
        }
        else
        {
            HyPanel1["FinancialSupport"] = "";
            HyPanel1["FinancialRemark"] = "";
        }
        HyPanel1["Estimatequantity"] = Co.RSQandSQLInjection(txtestimatequantity.Text, "soft");
        HyPanel1["EstimatePriceLLP"] = Co.RSQandSQLInjection(txtestimateprice.Text, "soft");
        HyPanel1["TenderStatus"] = Co.RSQandSQLInjection(ddltendorstatus.SelectedItem.Value, "soft");
        HyPanel1["TenderSubmition"] = Co.RSQandSQLInjection(rbtendordateyesno.SelectedItem.Value, "soft");
        if (txttendordate.Text != "")
        {
            DateTime Datetendor = Convert.ToDateTime(txttendordate.Text);
            string FinalDate = Datetendor.ToString("dd-MMM-yyyy");
            HyPanel1["TenderFillDate"] = Co.RSQandSQLInjection(FinalDate.ToString(), "soft");
        }
        else
        {
            HyPanel1["TenderFillDate"] = null;
        }
        HyPanel1["TenderUrl"] = Co.RSQandSQLInjection(txttendorurl.Text, "soft");
        if (ddlNodalOfficerEmail.Text == "")// && ddlNodalOfficerEmail2.Text == "")
        {
            HyPanel1["NodelDetail"] = null;
        }
        else if (ddlNodalOfficerEmail.SelectedItem.Text != "Select") //&& ddlNodalOfficerEmail2.SelectedItem.Text == "Select")
        {
            HyPanel1["NodelDetail"] = Co.RSQandSQLInjection(ddlNodalOfficerEmail.SelectedItem.Value, "soft") + ",";
        }
        //else if (ddlNodalOfficerEmail.SelectedItem.Text == "Select" && ddlNodalOfficerEmail2.SelectedItem.Text != "Select")
        //{
        //    HyPanel1["NodelDetail"] = Co.RSQandSQLInjection(ddlNodalOfficerEmail2.SelectedItem.Value, "soft") + ",";
        //}
        //else if (ddlNodalOfficerEmail.SelectedItem.Text != "Select" && ddlNodalOfficerEmail2.SelectedItem.Text != "Select")
        //{
        //    HyPanel1["NodelDetail"] = Co.RSQandSQLInjection(ddlNodalOfficerEmail.SelectedItem.Value, "soft") + "," +
        //                              Co.RSQandSQLInjection(ddlNodalOfficerEmail2.SelectedItem.Value, "soft") + ",";
        //}
        foreach (GridViewRow rw in gvtesting.Rows)
        {
            CheckBox chktest = (CheckBox)rw.FindControl("chktesting");
            HiddenField hftestingid = (HiddenField)rw.FindControl("hftestingid");
            TextBox txtRemarksTest = (TextBox)rw.FindControl("txttestingRemarks");
            if (chktest != null && chktest.Checked)
            {
                ServicesTesting = ServicesTesting + "," + hftestingid.Value;
                if (txtRemarksTest.Text != "")
                {
                    RemarksTesting = RemarksTesting + "," + txtRemarksTest.Text;
                }
            }
        }
        if (ServicesTesting != "")
        {
            HyPanel1["Testing"] = Co.RSQandSQLInjection(ServicesTesting.Substring(1).ToString() + ",", "soft");
            if (RemarksTesting != "")
            {
                HyPanel1["TestingRemarks"] =
                    Co.RSQandSQLInjection(RemarksTesting.Substring(1).ToString() + ",", "soft");
            }
            else
            { HyPanel1["TestingRemarks"] = ""; }
        }
        else
        {
            HyPanel1["Testing"] = "";
            HyPanel1["TestingRemarks"] = "";
        }
        foreach (GridViewRow rw in gvCertification.Rows)
        {
            CheckBox chkcerti = (CheckBox)rw.FindControl("chkcertification");
            HiddenField hfcertiid = (HiddenField)rw.FindControl("hfcertification");
            TextBox txtremarkscerti = (TextBox)rw.FindControl("txtCertificationRemarks");
            if (chkcerti != null && chkcerti.Checked)
            {
                ServicesCertification = ServicesCertification + "," + hfcertiid.Value;
                if (txtremarkscerti.Text != "")
                {
                    RemarksCertification = RemarksCertification + "," + txtremarkscerti.Text;
                }
            }
        }
        if (ServicesCertification != "")
        {
            HyPanel1["Certification"] = Co.RSQandSQLInjection(ServicesCertification.Substring(1).ToString() + ",", "soft");
            if (RemarksCertification != "")
            {
                HyPanel1["CertificationRemark"] = Co.RSQandSQLInjection(RemarksCertification.Substring(1).ToString() + ",", "soft");
            }
            else
            { HyPanel1["CertificationRemark"] = ""; }
        }
        else
        {
            HyPanel1["Certification"] = "";
            HyPanel1["CertificationRemark"] = "";
        }
        HyPanel1["CreatedBy"] = ViewState["UserLoginEmail"].ToString();
        string StrProductDescription = Lo.SaveCodeProduct(HyPanel1, dtImage, out _sysMsg, out _msg, "Product");
        if (StrProductDescription != "-1")
        {
            if (btnsubmitpanel1.Text != "Update")
            {
                Cleartext();
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Record Saved.')", true);
            }
            else
            {
                Cleartext();
                btnsubmitpanel1.Text = "Save";
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Record updated successfully.')", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Record not saved.')", true);
        }
    }
    #endregion
    #region PanelSaveButtonCode
    protected void btnsubmitpanel1_Click(object sender, EventArgs e)
    {
        if (txtproductdescription.Text != "" && ddlmastercategory.SelectedItem.Text != "Select" && ddlsubcategory.SelectedItem.Text != "Select" && ddltechnologycat.SelectedItem.Text != "Select")
        {
            if (ddlsubtech.SelectedItem.Text != "Select" && ddlnomnclature.SelectedItem.Text != "Select" && ddlenduser.SelectedItem.Text != "Select" && ddlplatform.SelectedItem.Text != "Select")
            {
                if (ddlprocurmentcategory.SelectedItem.Text != "Select" && ddlcountry.SelectedItem.Text != "Select")
                {
                    SaveProductDescription();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Please fill mandatory fields.')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Please fill mandatory fields.')", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Please fill mandatory fields.')", true);
        }
    }
    protected void btncancelpanel1_Click(object sender, EventArgs e)
    {
        Cleartext();
    }
    #endregion
    #region Claertext
    protected void Cleartext()
    {
        hfprodid.Value = "";
        hfprodrefno.Value = "";
        txtoempartnumber.Text = "";
        txtoemname.Text = "";
        ddlcountry.SelectedIndex = 0;
        txtmanifacaddress.Text = "";
        ddlyearofindiginization.SelectedIndex = 0;
        txtremarkspro.Text = "";
        txtnsccode.Text = "";
        txtniincode.Text = "";
        txtdpsupartnumber.Text = "";
        txtenduserpartnumber.Text = "";
        txthsncode.Text = "";
        // txtnatocode.Text = "";
        txterprefno.Text = "";
        ddlnomnclature.SelectedIndex = 0;
        ddlmastercategory.SelectedIndex = 0;
        ddlsubcategory.SelectedIndex = 0;
        txtproductdescription.Text = "";
        ddltechnologycat.SelectedIndex = 0;
        ddlsubtech.SelectedIndex = 0;
        ddlenduser.SelectedIndex = 0;
        ddlplatform.SelectedIndex = 0;
        ddlprocurmentcategory.SelectedIndex = 0;
        ddlproctimeframe.SelectedIndex = 0;
        rbisindinised.SelectedIndex = 0;
        divisIndigenized.Visible = false;
        txtmanufacturename.Text = "";
        txtsearchkeyword.Text = "";
        txtestimatequantity.Text = "";
        txtestimateprice.Text = "";
        ddltendorstatus.SelectedIndex = 0;
        txttendordate.Text = "";
        txttendorurl.Text = "";
        ddlsubcategory.Items.Clear();
        ddllevel3product.Items.Clear();
        ddlsubtech.Items.Clear();
        ddltechlevel3.Items.Clear();
        if (ddlNodalOfficerEmail.Text != "")
        {
            ddlNodalOfficerEmail.SelectedIndex = 0;
            // ddlNodalOfficerEmail2.SelectedIndex = 0;
        }
        foreach (GridViewRow rw in gvservices.Rows)
        {
            CheckBox chkBx = (CheckBox)rw.FindControl("chk");
            HiddenField hfservicesid = (HiddenField)rw.FindControl("hfservicesid");
            TextBox txtRemarks = (TextBox)rw.FindControl("txtRemarks");
            if (chkBx != null && chkBx.Checked)
            {
                chkBx.Checked = false;
                txtRemarks.Text = "";
                hfservicesid.Value = "";
            }
        }
        foreach (GridViewRow rw in gvfinancialsupp.Rows)
        {
            CheckBox chkfinBx = (CheckBox)rw.FindControl("chkfinan");
            HiddenField hffinnid = (HiddenField)rw.FindControl("hffinanciailserviceid");
            TextBox txtfinnRemarks = (TextBox)rw.FindControl("txtfinancialRemarks");
            if (chkfinBx != null && chkfinBx.Checked)
            {
                chkfinBx.Checked = false;
                txtfinnRemarks.Text = "";
                hffinnid.Value = "";
            }
        }
        foreach (GridViewRow rw in gvtesting.Rows)
        {
            CheckBox chktesting = (CheckBox)rw.FindControl("chktesting");
            HiddenField hftestid = (HiddenField)rw.FindControl("hftestingid");
            TextBox txttestRemarks = (TextBox)rw.FindControl("txttestingRemarks");
            if (chktesting != null && chktesting.Checked)
            {
                chktesting.Checked = false;
                txttestRemarks.Text = "";
                hftestid.Value = "";
            }
        }
        foreach (GridViewRow rw in gvCertification.Rows)
        {
            CheckBox chkcertification = (CheckBox)rw.FindControl("chkcertification");
            HiddenField hfcertification = (HiddenField)rw.FindControl("hfcertification");
            TextBox txtCertificationRemarks = (TextBox)rw.FindControl("txtCertificationRemarks");
            if (chkcertification != null && chkcertification.Checked)
            {
                chkcertification.Checked = false;
                txtCertificationRemarks.Text = "";
                hfcertification.Value = "";
            }
        }
        txtremarksyearofimportyes.Text = "";
        txtyearofimportremarksno.Text = "";
        rbtendordateyesno.SelectedIndex = 0;
        divyearofimportYes.Visible = false;
        divyearofimportNo.Visible = true;
    }
    #endregion
    #region Image Code
    private int ImageMaxCount;
    protected DataTable imagedb()
    {
        HttpFileCollection uploadedFiles = Request.Files;
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("ImageID", typeof(long)));
        dt.Columns.Add(new DataColumn("ImageName", typeof(string)));
        dt.Columns.Add(new DataColumn("ImageType", typeof(string)));
        dt.Columns.Add(new DataColumn("ImageActualSize", typeof(long)));
        dt.Columns.Add(new DataColumn("CompanyRefNo", typeof(string)));
        dt.Columns.Add(new DataColumn("Priority", typeof(int)));
        DataRow dr;
        {
            try
            {
                for (int i = 0; i < ImageMaxCount; i++)
                {
                    HttpPostedFile hpf = uploadedFiles[i];
                    string FileType = hpf.ContentType;
                    int FileSize = hpf.ContentLength;
                    // if (FileSize < 102400)
                    //{
                    if (Co.GetImageFilter(hpf.FileName) == true)
                    {
                        string FilePathName = hfcomprefno.Value + "_" + DateTime.Now.ToString("hh_mm_ss") + hpf.FileName;
                        hpf.SaveAs(HttpContext.Current.Server.MapPath("/Upload") + "\\" + FilePathName);
                        dr = dt.NewRow();
                        dr["ImageID"] = "-1";
                        dr["ImageName"] = "Upload/" + FilePathName;
                        dr["ImageType"] = FileType.ToString();
                        dr["ImageActualSize"] = FileSize.ToString();
                        dr["CompanyRefNo"] = hfcomprefno.Value;
                        dr["Priority"] = i + 1;
                        dt.Rows.Add(dr);
                    }
                    // }
                }
            }
            catch (Exception)
            {
            }
            return dt;
        }
    }
    #endregion
    #region AutoComplete OEM Name
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetOEMName(string prefix)
    {
        Cryptography objCrypto1 = new Cryptography();
        List<string> customers = new List<string>();
        // Int64 FinalNicCode = 0;
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = objCrypto1.DecryptData(ConfigurationManager.ConnectionStrings["connectiondb"].ConnectionString);
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select OEMName,ProductID from tbl_mst_MainProduct where OEMName like @SearchText + '%' and IsActive='Y'";
                cmd.Parameters.AddWithValue("@SearchText", prefix);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}", sdr["OEMName"], sdr["ProductID"]));
                        // FinalNicCode = Convert.ToInt64(sdr["ProductID"].ToString());
                    }
                }
                conn.Close();
            }
        }
        // NicCode = FinalNicCode;
        return customers.ToArray();
    }
    #endregion
    #region EditCodeForProduct
    protected void EditCode()
    {
        if (Request.QueryString["MProductRefNo"] != null && Request.QueryString["mcurrentcompRefNo"] != null && Request.QueryString["mrcreaterole"] != null)
        {
            hfprodrefno.Value = objEnc.DecryptData(Request.QueryString["MProductRefNo"].ToString());
            hidType.Value = objEnc.DecryptData(Request.QueryString["mrcreaterole"].ToString());
            DataTable DtView = Lo.RetriveProductCode("", hfprodrefno.Value, "ProductMasterID", hidType.Value);
            if (DtView.Rows.Count > 0)
            {
                btnsubmitpanel1.Text = "Update";
                hfprodid.Value = DtView.Rows[0]["ProductID"].ToString();
                hfcomprefno.Value = DtView.Rows[0]["CompanyRefNo"].ToString();
                ddlmastercategory.Items.FindByValue(DtView.Rows[0]["ProductLevel1"].ToString()).Selected = true;
                BindMasterSubCategory();
                ddlsubcategory.Items.FindByValue(DtView.Rows[0]["ProductLevel2"].ToString()).Selected = true;
                BindMaster3levelSubCategory();
                if (DtView.Rows[0]["ProductLevel3"].ToString() != "")
                {
                    ddllevel3product.SelectedValue = DtView.Rows[0]["ProductLevel3"].ToString();
                }
                txtnsccode.Text = DtView.Rows[0]["NSCCode"].ToString();
                txtniincode.Text = DtView.Rows[0]["NIINCode"].ToString();
                txtproductdescription.Text = DtView.Rows[0]["ProductDescription"].ToString();
                HyPanel1["ItemDescriptionPDFFile"] = DtView.Rows[0]["ItemDescriptionPDFFile"].ToString();
                txtoempartnumber.Text = DtView.Rows[0]["OEMPartNumber"].ToString();
                txtoemname.Text = DtView.Rows[0]["OEMName"].ToString();
                ddlcountry.SelectedValue = DtView.Rows[0]["OEMCountry"].ToString();
                txtdpsupartnumber.Text = DtView.Rows[0]["DPSUPartNumber"].ToString();
                txtenduserpartnumber.Text = DtView.Rows[0]["EndUserPartNumber"].ToString();
                txthsncode.Text = DtView.Rows[0]["HSNCode"].ToString();
                //   txtnatocode.Text = DtView.Rows[0]["NatoCode"].ToString();
                txterprefno.Text = DtView.Rows[0]["ERPRefNo"].ToString();
                ddltechnologycat.Items.FindByValue(DtView.Rows[0]["TechnologyLevel1"].ToString()).Selected = true;
                BindMasterSubCategoryTech();
                ddlsubtech.Items.FindByValue(DtView.Rows[0]["TechnologyLevel2"].ToString()).Selected = true;
                BindMasterSubCategoryTechLevel3();
                if (DtView.Rows[0]["TechnologyLevel3"].ToString() != "")
                {
                    ddltechlevel3.SelectedValue = DtView.Rows[0]["TechnologyLevel3"].ToString();
                }
                ddlplatform.Items.FindByValue(DtView.Rows[0]["Platform"].ToString()).Selected = true;
                BindMasterProductNoenCletureCategory();
                ddlnomnclature.SelectedValue = DtView.Rows[0]["NomenclatureOfMainSystem"].ToString();
                ddlenduser.SelectedValue = DtView.Rows[0]["EndUser"].ToString();
                ddlprocurmentcategory.SelectedValue = DtView.Rows[0]["PurposeofProcurement"].ToString();
                txtremarkspro.Text = DtView.Rows[0]["ProcurmentCategoryRemark"].ToString();
                //ddlprodreqir.SelectedValue = DtView.Rows[0]["ProductRequirment"].ToString();
                rbisindinised.SelectedValue = DtView.Rows[0]["IsIndeginized"].ToString();
                if (rbisindinised.SelectedItem.Value == "Y")
                {

                    txtmanufacturename.Text = DtView.Rows[0]["ManufactureName"].ToString();
                    txtmanifacaddress.Text = DtView.Rows[0]["ManufactureAddress"].ToString();
                    ddlyearofindiginization.SelectedValue = DtView.Rows[0]["YearofIndiginization"].ToString();
                    divisIndigenized.Visible = true;
                }
                else
                {
                    divisIndigenized.Visible = false;
                }
                txtsearchkeyword.Text = DtView.Rows[0]["SearchKeyword"].ToString();
                DataTable dtImageBind = Lo.RetriveProductCode("", hfprodrefno.Value, "ProductImage", hidType.Value);
                if (dtImageBind.Rows.Count > 0)
                {
                    dlimage.DataSource = dtImageBind;
                    dlimage.DataBind();
                    divimgdel.Visible = true;
                }
                else
                {
                    divimgdel.Visible = false;
                }
                rbproductImported.SelectedValue = DtView.Rows[0]["IsProductImported"].ToString();
                if (rbproductImported.SelectedItem.Value == "N")
                {
                    foreach (ListItem chkno in chkyearofimportall.Items)
                    {
                        if (DtView.Rows[0]["YearofImport"].ToString() != "")
                        {
                            chkno.Selected = true;
                        }
                    }
                    txtyearofimportremarksno.Text = DtView.Rows[0]["YearofImportRemarks"].ToString();
                }
                else
                {
                    foreach (ListItem chk in chklistimportyearfive.Items)
                    {
                        chk.Selected = true;
                    }
                    txtremarksyearofimportyes.Text = DtView.Rows[0]["YearofImportRemarks"].ToString();
                }
                DataTable dtpsdq = Lo.RetriveProductCode("", hfprodrefno.Value, "ProductPSDQ", hidType.Value);
                if (dtpsdq.Rows.Count > 0)
                {
                    for (int i = 0; dtpsdq.Rows.Count > i; i++)
                    {
                        foreach (GridViewRow rw in gvservices.Rows)
                        {
                            CheckBox chkBx = (CheckBox)rw.FindControl("chk");
                            HiddenField hfservicesid = (HiddenField)rw.FindControl("hfservicesid");
                            if (hfservicesid.Value == dtpsdq.Rows[i]["SCategoryId"].ToString())
                            {
                                chkBx.Checked = true;
                            }
                        }
                    }
                }
                DataTable dtTechnicalSupp = Lo.RetriveProductCode("", hfprodrefno.Value, "ProductFinancial", hidType.Value);
                if (dtTechnicalSupp.Rows.Count > 0)
                {
                    for (int i = 0; dtTechnicalSupp.Rows.Count > i; i++)
                    {
                        foreach (GridViewRow rw in gvfinancialsupp.Rows)
                        {
                            CheckBox chkfinbox = (CheckBox)rw.FindControl("chkfinan");
                            HiddenField hffinnid = (HiddenField)rw.FindControl("hffinanciailserviceid");
                            if (hffinnid.Value == dtpsdq.Rows[i]["SCategoryId"].ToString())
                            {
                                chkfinbox.Checked = true;
                            }
                        }
                    }
                }
                txtestimatequantity.Text = DtView.Rows[0]["Estimatequantity"].ToString();
                txtestimateprice.Text = DtView.Rows[0]["EstimatePriceLLP"].ToString();
                ddltendorstatus.SelectedValue = DtView.Rows[0]["TenderStatus"].ToString();
                rbtendordateyesno.SelectedValue = DtView.Rows[0]["TenderSubmition"].ToString();
                if (ddltendorstatus.SelectedValue == "Live" && rbtendordateyesno.SelectedValue == "Y")
                {
                    divtdate.Visible = true;
                    if (DtView.Rows[0]["TenderFillDate"].ToString() != "")
                    {
                        DateTime Date = Convert.ToDateTime(DtView.Rows[0]["TenderFillDate"].ToString());
                        string nDate = Date.ToString("yy-MMM-dd");
                        txttendordate.Text = nDate.ToString();
                    }
                    txttendorurl.Text = DtView.Rows[0]["TenderUrl"].ToString();
                }
                else
                {
                    divtdate.Visible = false;
                }
                //BindNodelEmail();
                DataTable dtNodal = Lo.RetriveProductCode("", hfprodrefno.Value, "ProductNodal", hidType.Value);
                if (dtNodal.Rows.Count > 0)
                {
                    ddlNodalOfficerEmail.SelectedValue = dtNodal.Rows[0]["NodalOfficerID"].ToString();
                    if (ddlNodalOfficerEmail.SelectedValue != "")
                    {
                        BindNodelEmail1();
                    }
                    if (dtNodal.Rows.Count == 2)
                    {
                        ddlNodalOfficerEmail2.SelectedValue = dtNodal.Rows[1]["NodalOfficerID"].ToString();
                        if (ddlNodalOfficerEmail2.SelectedValue != "")
                        {
                            BindNodal2();
                        }
                    }
                }
                DataTable dttesting = Lo.RetriveProductCode("", hfprodrefno.Value, "ProductTesting", hidType.Value);
                if (dttesting.Rows.Count > 0)
                {
                    for (int i = 0; dttesting.Rows.Count > i; i++)
                    {
                        foreach (GridViewRow rw in gvtesting.Rows)
                        {
                            CheckBox chkBxtesting = (CheckBox)rw.FindControl("chktesting");
                            HiddenField hfservcetesting = (HiddenField)rw.FindControl("hftestingid");
                            if (hfservcetesting.Value == dttesting.Rows[i]["SCategoryId"].ToString())
                            {
                                chkBxtesting.Checked = true;
                            }
                        }
                    }
                }
                DataTable dtcertifica = Lo.RetriveProductCode("", hfprodrefno.Value, "ProductCertification", hidType.Value);
                if (dtcertifica.Rows.Count > 0)
                {
                    for (int i = 0; dtcertifica.Rows.Count > i; i++)
                    {
                        foreach (GridViewRow rw in gvCertification.Rows)
                        {
                            CheckBox chkBxCer = (CheckBox)rw.FindControl("chkcertification");
                            HiddenField hfcer = (HiddenField)rw.FindControl("hfcertification");
                            if (hfcer.Value == dtcertifica.Rows[i]["SCategoryId"].ToString())
                            {
                                chkBxCer.Checked = true;
                            }
                        }
                    }
                }
            }
        }
    }
    protected void dlimage_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "removeimg")
        {
            try
            {
                string DeleteRec = Lo.DeleteRecord(e.CommandArgument.ToString(), "InActiveImage");
                if (DeleteRec == "true")
                {
                    DataTable dtImageBind = Lo.RetriveProductCode("", hfprodrefno.Value, "ProductImage", hidType.Value);
                    if (dtImageBind.Rows.Count > 0)
                    {
                        dlimage.DataSource = dtImageBind;
                        dlimage.DataBind();
                        divimgdel.Visible = true;
                    }
                    else
                    {
                        divimgdel.Visible = false;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not deleted.')", true);
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not deleted.')", true);
            }
        }
    }
    #endregion
    private void NSCCode(string NSNGroupddl, string NSNClassddl)
    {
        try
        {
            string a = NSNGroupddl.Substring((NSNGroupddl.IndexOf("(") + 1), NSNGroupddl.IndexOf(")") - (NSNGroupddl.IndexOf("(") + 1));
            string b = NSNClassddl.Substring((NSNClassddl.IndexOf("(") + 1), NSNClassddl.IndexOf(")") - (NSNClassddl.IndexOf("(") + 1));
            txtnsccode.Text = a + b;
        }
        catch (Exception ex)
        {
            txtnsccode.Text = "";
        }
    }
    #region PDF File itemDescription
    protected void PDFFileItemDescription()
    {
        if (fuitemdescriptionfile.HasFile != false)
        {
            if (DataUtility.Instance.GetFileFilter(fuitemdescriptionfile.PostedFile.FileName) != false)
            {
                string File = fuitemdescriptionfile.FileName.Trim();
                string FileName = hfcomprefno.Value + "_" + DateTime.Now.ToString("hh_mm_ss") + "_" + txtnsccode.Text + "_" + File.ToString();
                fuitemdescriptionfile.SaveAs(HttpContext.Current.Server.MapPath("/Upload") + "\\" + FileName);
                HyPanel1["ItemDescriptionPDFFile"] = FileName.Trim().ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Invalid file format only pdf allowd.')", true);
            }
        }
    }
    #endregion
}