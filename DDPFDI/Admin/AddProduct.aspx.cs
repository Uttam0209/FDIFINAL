﻿using BusinessLayer;
using Encryption;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
    public DataTable dtSaveProdInfo = new DataTable();
    public DataTable dtSaveEstimateQuantity = new DataTable();
    private string DisplayPanel = "";
    public static Int64 CountryID = 0;
    private string _msg = string.Empty;
    private string _sysMsg = string.Empty;
    public string Services = "";
    public string Remarks = "";
    public string ProcurmentCat = "";
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
                            SetInitialRowProductInfo();
                            SetInitialRowGvEstimateQuanPrice();
                        }
                        if (hidType.Value.ToString() != "SuperAdmin" || hidType.Value.ToString() != "Admin")
                        {
                            BindCompany();
                            BindFinancialYear();
                            // BindCountry();
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
                                // BindMasterProductReqCategory();
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
                                // BindMasterProductReqCategory();
                                // BindMasterProductNoenCletureCategory();
                                BindEndUser();
                            }
                        }
                        else
                        {
                            BindCompany();
                            BindFinancialYear();
                            //  BindCountry();
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
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Session Expired,Please login again');window.location='Login'", true);
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
                DataTable dt = Lo.RetriveMasterData(0, hfcomprefno.Value, "Factory2", 0, "", "", "CompanyName");
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
                ddlunit.Visible = true;
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
                Co.FillDropdownlist(ddlNodalOfficerEmail2, DtCompanyDDL, "NodalOficerName", "NodalOfficerID");
                ddlNodalOfficerEmail2.Items.Insert(0, "Select");
            }
            else
            {
                contactpanel1.Visible = false;
                ddlNodalOfficerEmail.Items.Clear();
                ddlNodalOfficerEmail2.Items.Clear();
                ddlNodalOfficerEmail.Items.Insert(0, "Select");
                ddlNodalOfficerEmail2.Items.Insert(0, "Select");
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
                // BindMasterProductReqCategory();
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
                DtCompanyDDL = Lo.RetriveMasterData(Convert.ToInt16(ddlNodalOfficerEmail.SelectedItem.Value), DtGetNodel.Rows[0]["CompanyRefNo"].ToString(), "", 0, "", "", "AllNodelNotSelect");
                if (DtCompanyDDL.Rows.Count > 0)
                {
                    Co.FillDropdownlist(ddlNodalOfficerEmail2, DtCompanyDDL, "NodalOficerName", "NodalOfficerID");
                    ddlNodalOfficerEmail2.Items.Insert(0, "Select");
                    divnodal2.Visible = true;
                }
                else
                {
                    divnodal2.Visible = false;
                }
            }
        }
        else
        {
            contactpanel1.Visible = false;
            contactpanel2.Visible = false;
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
                DtCompanyDDL = Lo.RetriveMasterData(Convert.ToInt16(ddlNodalOfficerEmail2.SelectedItem.Value), DtGetNodel.Rows[0]["CompanyRefNo"].ToString(), "", 0, "", "", "AllNodelNotSelect");
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
            // Co.FillDropdownlist(ddlprocurmentcategory, DtPurposeProcuremnt, "SCategoryName", "SCategoryID");
            // ddlprocurmentcategory.Items.Insert(0, "Select");
            gvprocurmentcategory.DataSource = DtPurposeProcuremnt;
            gvprocurmentcategory.DataBind();
            // Co.FillCheckBox(chkprocurmentcategory, DtPurposeProcuremnt, "SCategoryName", "SCategoryID");
        }
        else
        {
            DtPurposeProcuremnt = Lo.RetriveMasterSubCategoryDate(0, "PROCURMENT CATEGORY", "", "SelectProductCat", "", "");
            // Co.FillDropdownlist(ddlprocurmentcategory, DtPurposeProcuremnt, "SCategoryName", "SCategoryID");
            // ddlprocurmentcategory.Items.Insert(0, "Select");
            gvprocurmentcategory.DataSource = DtPurposeProcuremnt;
            gvprocurmentcategory.DataBind();
            //  Co.FillCheckBox(chkprocurmentcategory, DtPurposeProcuremnt, "SCategoryName", "SCategoryID");
        }
    }
    #endregion
    #region For Procurement Time Frame
    //protected void BindMasterProductReqCategory()
    //{
    //    DataTable DtMasterCategroy = new DataTable();
    //    if (ddlcompany.SelectedItem.Text != "Select")
    //    {
    //        DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, lblprodrequir.Text, "", "SelectInnerMaster1", ddlcompany.SelectedItem.Value, "");
    //    }
    //    else
    //    {
    //        DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, lblprodrequir.Text, "", "SelectInnerMaster1", "", "");
    //    }
    //    if (DtMasterCategroy.Rows.Count > 0)
    //    {
    //        Co.FillDropdownlist(ddlproctimeframe, DtMasterCategroy, "SCategoryName", "SCategoryID");
    //        ddlproctimeframe.Items.Insert(0, "Select");
    //    }
    //    else
    //    {
    //        DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, lblprodrequir.Text, "", "SelectInnerMaster1", "", "");
    //        Co.FillDropdownlist(ddlproctimeframe, DtMasterCategroy, "SCategoryName", "SCategoryID");
    //        ddlproctimeframe.Items.Insert(0, "Select");
    //    }
    //}
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
    //protected void BindCountry()
    //{
    //    DataTable DtCountry = Lo.RetriveCountry(0,"Select");
    //    if (DtCountry.Rows.Count > 0)
    //    {
    //        Co.FillDropdownlist(ddlcountry, DtCountry, "CountryName", "CountryID");
    //        ddlcountry.Items.Insert(0, "Select");
    //    }
    //}
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
            else
            {
                if (lblfuitemdescriptionfile.Text != "")
                { HyPanel1["ItemDescriptionPDFFile"] = lblfuitemdescriptionfile.Text; }
                else
                {
                    HyPanel1["ItemDescriptionPDFFile"] = "";
                }
            }
        }
        HyPanel1["OEMPartNumber"] = Co.RSQandSQLInjection(txtoempartnumber.Text.Trim(), "soft");
        HyPanel1["OEMName"] = Co.RSQandSQLInjection(txtoemname.Text.Trim(), "soft");
        HyPanel1["OEMCountry"] = Convert.ToInt64(CountryID.ToString());
        HyPanel1["DPSUPartNumber"] = Co.RSQandSQLInjection(txtdpsupartnumber.Text.Trim(), "soft");
        HyPanel1["EndUserPartNumber"] = Co.RSQandSQLInjection(txtenduserpartnumber.Text.Trim(), "soft");
        HyPanel1["HSNCode"] = Co.RSQandSQLInjection(txthsncode.Text.Trim(), "soft");
        HyPanel1["NatoCode"] = "";//Co.RSQandSQLInjection(txtnatocode.Text, "soft");
        HyPanel1["ERPRefNo"] = "";// Co.RSQandSQLInjection(txterprefno.Text.Trim(), "soft");
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
        foreach (GridViewRow gvPofPROCURMENT in gvprocurmentcategory.Rows)
        {
            CheckBox chkBxProc = (CheckBox)gvPofPROCURMENT.FindControl("chkprocurmentcategory");
            HiddenField hfProcCatId = (HiddenField)gvPofPROCURMENT.FindControl("hfproccateid");
            if (chkBxProc != null && chkBxProc.Checked)
            {
                ProcurmentCat = ProcurmentCat + "," + hfProcCatId.Value;
            }
        }
        if (ProcurmentCat.ToString() != "")
        {
            HyPanel1["PurposeofProcurement"] = Co.RSQandSQLInjection(ProcurmentCat.Substring(1).ToString() + ",", "soft");
        }
        else
        {
            HyPanel1["PurposeofProcurement"] = "";
        }
        HyPanel1["ProcurmentCategoryRemark"] = Co.RSQandSQLInjection(txtremarksprocurmentCategory.Text.Trim(), "soft");
        HyPanel1["ProductRequirment"] = null;
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
        if (fuimages.HasFiles != false)
        {
            if (hfprodid.Value != "")
            {
                DataTable dtImageBind = Lo.RetriveProductCode("", hfprodrefno.Value, "RetriveImage", hidType.Value);
                if (dtImageBind.Rows.Count > 0)
                {
                    short CountImageTotal = Convert.ToInt16(fuimages.PostedFiles.Count);
                    short AlreadyUploadImage = Convert.ToInt16(dtImageBind.Rows.Count);
                    ImageMaxCount = (CountImageTotal + AlreadyUploadImage);
                    if (ImageMaxCount <= 4)
                    {
                        dtImage = imagedb();
                    }
                }
                else
                {
                    if (fuimages.HasFiles != false)
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
            foreach (ListItem chk in chklistimportyearfive.Items)
            {
                if (chk.Selected == true)
                {
                    IsImportedyesYear = IsImportedyesYear + "," + chk.Value;
                }
            }
            HyPanel1["YearofImport"] = Co.RSQandSQLInjection(IsImportedyesYear.Substring(1).ToString(), "soft");
            HyPanel1["YearofImportRemarks"] = Co.RSQandSQLInjection(txtremarksyearofimportyes.Text.Trim(), "soft");
        }
        foreach (GridViewRow rw in gvservices.Rows)
        {
            CheckBox chkBx = (CheckBox)rw.FindControl("chk");
            HiddenField hfservicesid = (HiddenField)rw.FindControl("hfservicesid");
            if (chkBx != null && chkBx.Checked)
            {
                Services = Services + "," + hfservicesid.Value;
            }
        }
        if (Services.ToString() != "")
        {
            HyPanel1["DPSUServices"] = Co.RSQandSQLInjection(Services.Substring(1).ToString() + ",", "soft");
        }
        else
        {
            HyPanel1["DPSUServices"] = "";
        }
        HyPanel1["Remarks"] = Co.RSQandSQLInjection(txtservisesremarks.Text.Trim() + ",", "soft");
        foreach (GridViewRow rw in gvfinancialsupp.Rows)
        {
            CheckBox chkfinanBx = (CheckBox)rw.FindControl("chkfinan");
            HiddenField hffinanservicesid = (HiddenField)rw.FindControl("hffinanciailserviceid");
            if (chkfinanBx != null && chkfinanBx.Checked)
            {
                FinancialServices = FinancialServices + "," + hffinanservicesid.Value;
            }
        }
        if (FinancialServices.ToString() != "")
        {
            HyPanel1["FinancialSupport"] = Co.RSQandSQLInjection(FinancialServices.Substring(1).ToString() + ",", "soft");
        }
        else
        {
            HyPanel1["FinancialSupport"] = "";
        }
        HyPanel1["FinancialRemark"] = Co.RSQandSQLInjection(txtfinancialsuppRemarks.Text.Trim() + ",", "soft");
        if (ViewState["CurrentTableEstimateQuan"] != null)
        {
            dtSaveEstimateQuantity = SaveCodeEstimateQuantity();
        }
        else
        {
            dtSaveEstimateQuantity = null;

        }
        if (ViewState["CurrentTableProdInfo"] != null)
        {
            dtSaveProdInfo = SaveCodeProdInfo();
        }
        else
        {
            dtSaveProdInfo = null;
        }
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
        if (ddlNodalOfficerEmail.Text == "" || ddlNodalOfficerEmail.SelectedItem.Text == "Select")
        {
            HyPanel1["NodelDetail"] = null;
        }
        else
        {
            HyPanel1["NodelDetail"] = Convert.ToInt16(ddlNodalOfficerEmail.SelectedItem.Value);
        }
        if (ddlNodalOfficerEmail2.Text == "" || ddlNodalOfficerEmail2.SelectedItem.Text == "Select")//ddlprocurmentcategory
        {
            HyPanel1["NodalDetail2"] = null;
        }
        else
        {
            HyPanel1["NodalDetail2"] = Convert.ToInt16(ddlNodalOfficerEmail2.SelectedItem.Value);
        }
        foreach (GridViewRow rw in gvtesting.Rows)
        {
            CheckBox chktest = (CheckBox)rw.FindControl("chktesting");
            HiddenField hftestingid = (HiddenField)rw.FindControl("hftestingid");
            if (chktest != null && chktest.Checked)
            {
                ServicesTesting = ServicesTesting + "," + hftestingid.Value;
            }
        }
        if (ServicesTesting.ToString() != "")
        {
            HyPanel1["Testing"] = Co.RSQandSQLInjection(ServicesTesting.Substring(1).ToString() + ",", "soft");
        }
        else
        {
            HyPanel1["Testing"] = "";
        }
        HyPanel1["TestingRemarks"] = Co.RSQandSQLInjection(txttestingremarks.Text.Trim() + ",", "soft");
        foreach (GridViewRow rw in gvCertification.Rows)
        {
            CheckBox chkcerti = (CheckBox)rw.FindControl("chkcertification");
            HiddenField hfcertiid = (HiddenField)rw.FindControl("hfcertification");
            if (chkcerti != null && chkcerti.Checked)
            {
                ServicesCertification = ServicesCertification + "," + hfcertiid.Value;
            }
        }
        if (ServicesCertification.ToString() != "")
        {
            HyPanel1["Certification"] = Co.RSQandSQLInjection(ServicesCertification.Substring(1).ToString() + ",", "soft");
        }
        else
        {
            HyPanel1["Certification"] = "";
        }
        HyPanel1["CertificationRemark"] = Co.RSQandSQLInjection(txtcertificationremarks.Text.Trim() + ",", "soft");
        HyPanel1["CreatedBy"] = ViewState["UserLoginEmail"].ToString();
        string StrProductDescription = Lo.SaveCodeProduct(HyPanel1, dtImage, dtSaveProdInfo, dtSaveEstimateQuantity, out _sysMsg, out _msg, "Product");
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
                if (txtcountry.Text != "" && txtdpsupartnumber.Text != "")
                {
                    if (fuitemdescriptionfile.HasFile != false)
                    {
                        int iFileSize = fuitemdescriptionfile.PostedFile.ContentLength;
                        if (iFileSize > 1048576) // 1MB
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Maximum 1 Mb pdf file can be uploaded')", true);
                        }
                        else
                        {
                            if (fuimages.HasFile != false)
                            {
                                int filecount = 0;
                                filecount = Convert.ToInt16(fuimages.PostedFiles.Count.ToString());
                                if (filecount <= 4)
                                {
                                    int iImageFileSize = fuimages.PostedFile.ContentLength;
                                    if (iImageFileSize > 4194304)
                                    {
                                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert",
                                            "alert('Maximum 1 Mb .jpg,.jpeg,.png,.tif images can be uploaded')", true);
                                    }
                                    else
                                    {
                                        SaveProductDescription();
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert",
                                        "alert('Maximum 4 files can be uploaded')", true);
                                }
                            }
                            else
                            {
                                SaveProductDescription();
                            }
                        }
                    }
                    else
                    {
                        if (fuimages.HasFile != false)
                        {
                            int filecount = 0;
                            filecount = Convert.ToInt16(fuimages.PostedFiles.Count.ToString());
                            if (filecount <= 4)
                            {
                                int iImageFileSize = fuimages.PostedFile.ContentLength;
                                if (iImageFileSize > 4194304)
                                {
                                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert",
                                        "alert('Maximum 1 Mb .jpg,.jpeg,.png,.tif images can be uploaded')", true);
                                }
                                else
                                {
                                    SaveProductDescription();
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert",
                                    "alert('Maximum 4 files can be uploaded')", true);
                            }
                        }
                        else
                        {
                            SaveProductDescription();
                        }
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
        txtcountry.Text = "";
        txtmanifacaddress.Text = "";
        ddlyearofindiginization.SelectedIndex = 0;
        txtremarksprocurmentCategory.Text = "";
        txtnsccode.Text = "";
        txtniincode.Text = "";
        txtdpsupartnumber.Text = "";
        txtenduserpartnumber.Text = "";
        txthsncode.Text = "";
        ddlnomnclature.SelectedIndex = 0;
        ddlmastercategory.SelectedIndex = 0;
        ddlsubcategory.SelectedIndex = 0;
        txtproductdescription.Text = "";
        ddltechnologycat.SelectedIndex = 0;
        ddlsubtech.SelectedIndex = 0;
        ddlenduser.SelectedIndex = 0;
        ddlplatform.SelectedIndex = 0;
        rbisindinised.SelectedIndex = 0;
        divisIndigenized.Visible = false;
        txtmanufacturename.Text = "";
        txtsearchkeyword.Text = "";
        // txtestimatequantity.Text = "";
        // txtestimateprice.Text = "";
        ddltendorstatus.SelectedIndex = 0;
        txttendordate.Text = "";
        txttendorurl.Text = "";
        ddlsubcategory.Items.Clear();
        ddllevel3product.Items.Clear();
        ddlsubtech.Items.Clear();
        ddltechlevel3.Items.Clear();
        //  ddlestimatequantityidle.SelectedIndex = 0;
        CountryID = 0;
        if (ddlNodalOfficerEmail.Text != "")
        {
            ddlNodalOfficerEmail.SelectedIndex = 0;
        }
        foreach (GridViewRow rw in gvservices.Rows)
        {
            CheckBox chkBx = (CheckBox)rw.FindControl("chk");
            HiddenField hfservicesid = (HiddenField)rw.FindControl("hfservicesid");
            if (chkBx != null && chkBx.Checked)
            {
                chkBx.Checked = false;
                hfservicesid.Value = "";
            }
        }
        foreach (GridViewRow rw in gvfinancialsupp.Rows)
        {
            CheckBox chkfinBx = (CheckBox)rw.FindControl("chkfinan");
            HiddenField hffinnid = (HiddenField)rw.FindControl("hffinanciailserviceid");
            if (chkfinBx != null && chkfinBx.Checked)
            {
                chkfinBx.Checked = false;
                hffinnid.Value = "";
            }
        }
        foreach (GridViewRow rw in gvtesting.Rows)
        {
            CheckBox chktesting = (CheckBox)rw.FindControl("chktesting");
            HiddenField hftestid = (HiddenField)rw.FindControl("hftestingid");
            if (chktesting != null && chktesting.Checked)
            {
                chktesting.Checked = false;
                hftestid.Value = "";
            }
        }
        foreach (GridViewRow rw in gvCertification.Rows)
        {
            CheckBox chkcertification = (CheckBox)rw.FindControl("chkcertification");
            HiddenField hfcertification = (HiddenField)rw.FindControl("hfcertification");
            if (chkcertification != null && chkcertification.Checked)
            {
                chkcertification.Checked = false;
                hfcertification.Value = "";
            }
        }
        txtremarksyearofimportyes.Text = "";
        txtyearofimportremarksno.Text = "";
        rbproductImported.SelectedIndex = 0;
        divyearofimportYes.Visible = false;
        divyearofimportNo.Visible = true;
    }
    #endregion
    #region Image Code
    private int ImageMaxCount;
    protected DataTable imagedb()
    {
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
                if (ImageMaxCount <= 4)
                {
                    foreach (HttpPostedFile postfiles in fuimages.PostedFiles)
                    {
                        string FileType = Path.GetExtension(postfiles.FileName);
                        int FileSize = postfiles.ContentLength;
                        if (Co.GetImageFilter(postfiles.FileName) == true) ;
                        {
                            string FilePathName = hfcomprefno.Value + "_" + DateTime.Now.ToString("hh_mm_ss") + postfiles.FileName;
                            postfiles.SaveAs(HttpContext.Current.Server.MapPath("/Upload") + "\\" + FilePathName);
                            dr = dt.NewRow();
                            dr["ImageID"] = "-1";
                            dr["ImageName"] = "Upload/" + FilePathName;
                            dr["ImageType"] = FileType.ToString();
                            dr["ImageActualSize"] = FileSize.ToString();
                            dr["CompanyRefNo"] = hfcomprefno.Value;
                            dr["Priority"] = ImageMaxCount++ + 1;
                            dt.Rows.Add(dr);
                        }
                    }
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
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetCountry(string prefix)
    {
        Cryptography objCrypto1 = new Cryptography();
        List<string> customers = new List<string>();
        Int64 FinalNicCode = 0;
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = objCrypto1.DecryptData(ConfigurationManager.ConnectionStrings["connectiondb"].ConnectionString);
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select Upper(CountryName) as CountryName,CountryId from tbl_mst_Country where CountryName like @SearchText + '%' and IsActive='Y'";
                cmd.Parameters.AddWithValue("@SearchText", prefix);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}", sdr["CountryName"], sdr["CountryID"]));
                        FinalNicCode = Convert.ToInt64(sdr["CountryId"].ToString());
                    }
                }
                conn.Close();
            }
        }
        CountryID = FinalNicCode;
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
                DataTable dtcount = Lo.RetriveCountry(Convert.ToInt64(DtView.Rows[0]["OEMCountry"].ToString()), "GetCountryByID");
                if (dtcount.Rows.Count > 0)
                { txtcountry.Text = dtcount.Rows[0]["CountryName"].ToString(); }
                CountryID = Convert.ToInt16(DtView.Rows[0]["OEMCountry"].ToString());
                //ddlcountry.SelectedValue = DtView.Rows[0]["OEMCountry"].ToString();
                txtdpsupartnumber.Text = DtView.Rows[0]["DPSUPartNumber"].ToString();
                txtenduserpartnumber.Text = DtView.Rows[0]["EndUserPartNumber"].ToString();
                txthsncode.Text = DtView.Rows[0]["HSNCode"].ToString();
                //   txtnatocode.Text = DtView.Rows[0]["NatoCode"].ToString();
                // txterprefno.Text = DtView.Rows[0]["ERPRefNo"].ToString();
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
                // ddlprocurmentcategory.SelectedValue = DtView.Rows[0]["PurposeofProcurement"].ToString();
                txtremarksprocurmentCategory.Text = DtView.Rows[0]["ProcurmentCategoryRemark"].ToString();
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
                if (DtView.Rows[0]["ItemDescriptionPDFFile"].ToString() != "")
                {
                    lblfuitemdescriptionfile.Text = DtView.Rows[0]["ItemDescriptionPDFFile"].ToString();
                    lblfuitemdescriptionfile.Visible = true;
                }
                else
                {
                    lblfuitemdescriptionfile.Text = "";
                    lblfuitemdescriptionfile.Visible = false;
                }
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
                    divyearofimportNo.Visible = true;
                    divyearofimportYes.Visible = false;
                }
                else
                {
                    foreach (ListItem chk in chklistimportyearfive.Items)
                    {
                        chk.Selected = true;
                    }
                    txtremarksyearofimportyes.Text = DtView.Rows[0]["YearofImportRemarks"].ToString();
                    divyearofimportNo.Visible = false;
                    divyearofimportYes.Visible = true;
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
                // txtestimatequantity.Text = DtView.Rows[0]["Estimatequantity"].ToString();
                // ddlestimatequantityidle.SelectedValue = DtView.Rows[0]["EstimatequantityIdle"].ToString();
                //  txtestimateprice.Text = DtView.Rows[0]["EstimatePriceLLP"].ToString();
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
    #region SetGridView Initial ProductInformaTION
    private void SetInitialRowProductInfo()
    {
        DataTable dtProductInfo = new DataTable();
        DataRow drProductInfo = null;
        dtProductInfo.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dtProductInfo.Columns.Add(new DataColumn("Length", typeof(string)));
        dtProductInfo.Columns.Add(new DataColumn("Value", typeof(decimal)));
        dtProductInfo.Columns.Add(new DataColumn("ProductUnit", typeof(string)));
        drProductInfo = dtProductInfo.NewRow();
        drProductInfo["RowNumber"] = 1;
        drProductInfo["Length"] = string.Empty;
        drProductInfo["Value"] = "0.0";
        drProductInfo["ProductUnit"] = string.Empty;
        dtProductInfo.Rows.Add(drProductInfo);
        ViewState["CurrentTableProdInfo"] = dtProductInfo;
        gvProductInformation.DataSource = dtProductInfo;
        gvProductInformation.DataBind();
    }
    private void AddNewRowToGrid()
    {
        if (ViewState["CurrentTableProdInfo"] != null)
        {
            DataTable dtCurrentTableProdInfo = (DataTable)ViewState["CurrentTableProdInfo"];
            DataRow drCurrentRowProdInfo = null;
            if (dtCurrentTableProdInfo.Rows.Count <= 4)
            {
                drCurrentRowProdInfo = dtCurrentTableProdInfo.NewRow();
                drCurrentRowProdInfo["RowNumber"] = dtCurrentTableProdInfo.Rows.Count + 1;
                //add new row to DataTable 
                dtCurrentTableProdInfo.Rows.Add(drCurrentRowProdInfo);
                ViewState["CurrentTable"] = dtCurrentTableProdInfo;
                for (int i = 0; i < dtCurrentTableProdInfo.Rows.Count - 1; i++)
                {
                    TextBox TbProdInfo1 = (TextBox)gvProductInformation.Rows[i].Cells[1].FindControl("txtlenth");
                    TextBox TbProdInfo2 = (TextBox)gvProductInformation.Rows[i].Cells[2].FindControl("txtvalue");
                    TextBox TbProdInfo3 = (TextBox)gvProductInformation.Rows[i].Cells[3].FindControl("txtProdInfoUnit");
                    dtCurrentTableProdInfo.Rows[i]["Length"] = TbProdInfo1.Text;
                    dtCurrentTableProdInfo.Rows[i]["Value"] = TbProdInfo2.Text;
                    dtCurrentTableProdInfo.Rows[i]["ProductUnit"] = TbProdInfo3.Text;
                }
                //Rebind the Grid with the current data to reflect changes 
                gvProductInformation.DataSource = dtCurrentTableProdInfo;
                gvProductInformation.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Maximum 5 row added.')", true);
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        //Set Previous Data on Postbacks 
        SetPreviousData();
    }
    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTableProdInfo"] != null)
        {
            DataTable dtProdInfo = (DataTable)ViewState["CurrentTableProdInfo"];
            if (dtProdInfo.Rows.Count > 0)
            {
                for (int i = 0; i < dtProdInfo.Rows.Count; i++)
                {
                    TextBox TbProdInfo1 = (TextBox)gvProductInformation.Rows[i].Cells[1].FindControl("txtlenth");
                    TextBox TbProdInfo2 = (TextBox)gvProductInformation.Rows[i].Cells[2].FindControl("txtvalue");
                    TextBox TbProdInfo3 = (TextBox)gvProductInformation.Rows[i].Cells[3].FindControl("txtProdInfoUnit");
                    if (i < dtProdInfo.Rows.Count - 1)
                    {   //Assign the value from DataTable to the TextBox 
                        TbProdInfo1.Text = dtProdInfo.Rows[i]["Length"].ToString();
                        TbProdInfo2.Text = dtProdInfo.Rows[i]["Value"].ToString();
                        TbProdInfo3.Text = dtProdInfo.Rows[i]["ProductUnit"].ToString();
                    }
                    rowIndex++;
                }
            }
        }
    }
    protected void btnaddmore_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }
    protected void gvProductInformation_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dtProdIn = (DataTable)ViewState["CurrentTableProdInfo"];
            LinkButton lbProdInfo = (LinkButton)e.Row.FindControl("lbRemove");
            if (lbProdInfo != null)
            {
                if (dtProdIn.Rows.Count > 1)
                {
                    if (e.Row.RowIndex == dtProdIn.Rows.Count - 1)
                    {
                        lbProdInfo.Visible = false;
                    }
                }
                else
                {
                    lbProdInfo.Visible = false;
                }
            }
        }
    }
    protected void lbRemove_Click(object sender, EventArgs e)
    {
        LinkButton lbProdInfo = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbProdInfo.NamingContainer;
        int rowID = gvRow.RowIndex;
        if (ViewState["CurrentTableProdInfo"] != null)
        {
            DataTable dtProdInfo = (DataTable)ViewState["CurrentTableProdInfo"];
            if (dtProdInfo.Rows.Count > 1)
            {
                if (gvRow.RowIndex < dtProdInfo.Rows.Count - 1)
                {
                    //Remove the Selected Row data and reset row number
                    dtProdInfo.Rows.Remove(dtProdInfo.Rows[rowID]);
                    ResetRowID(dtProdInfo);
                }
            }
            //Store the current data in ViewState for future reference
            ViewState["CurrentTableProdInfo"] = dtProdInfo;
            //Re bind the GridView for the updated data
            gvProductInformation.DataSource = dtProdInfo;
            gvProductInformation.DataBind();
        }
        //Set Previous Data on Postbacks
        SetPreviousData();
    }
    private void ResetRowID(DataTable dtProdInfo)
    {
        int rowNumber = 1;
        if (dtProdInfo.Rows.Count > 0)
        {
            foreach (DataRow row in dtProdInfo.Rows)
            {
                row[0] = rowNumber;
                rowNumber++;
            }
        }
    }
    protected DataTable SaveCodeProdInfo()
    {
        int rowIndex = 0;
        DataTable DtNProdInfo = new DataTable();
        DtNProdInfo.Columns.Add(new DataColumn("ProdInfoId", typeof(int)));
        DtNProdInfo.Columns.Add(new DataColumn("Length", typeof(string)));
        DtNProdInfo.Columns.Add(new DataColumn("Value", typeof(decimal)));
        DtNProdInfo.Columns.Add(new DataColumn("ProductUnit", typeof(string)));
        DataRow drProdInfoSave;
        {
            try
            {
                if (ViewState["CurrentTableProdInfo"] != null)
                {
                    for (int i = 1; i <= gvProductInformation.Rows.Count; i++)
                    {
                        TextBox txtlenth =
                            (TextBox)gvProductInformation.Rows[rowIndex].Cells[1].FindControl("txtlenth");
                        TextBox txtvalue =
                            (TextBox)gvProductInformation.Rows[rowIndex].Cells[2].FindControl("txtvalue");
                        TextBox txtProdInfoUnit = (TextBox)gvProductInformation.Rows[rowIndex].Cells[3]
                            .FindControl("txtProdInfoUnit");
                        drProdInfoSave = DtNProdInfo.NewRow();
                        drProdInfoSave["ProdInfoId"] = "-1";
                        drProdInfoSave["Length"] = txtlenth.Text.Trim();
                        drProdInfoSave["Value"] = Convert.ToDecimal(txtvalue.Text.Trim());
                        drProdInfoSave["ProductUnit"] = txtProdInfoUnit.Text.Trim();
                        DtNProdInfo.Rows.Add(drProdInfoSave);
                    }
                }
            }
            catch (Exception Ex)
            { }
            return DtNProdInfo;
        }
    }
    #endregion
    #region Gridview Initial GvEstimateQuanPrice
    private void SetInitialRowGvEstimateQuanPrice()
    {
        DataTable DtEstimateQuanPrice = new DataTable();
        DataRow drEstimateQuanPrice = null;
        DtEstimateQuanPrice.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        DtEstimateQuanPrice.Columns.Add(new DataColumn("Year", typeof(int)));//for DropDownList selected item 
        DtEstimateQuanPrice.Columns.Add(new DataColumn("EstimateQuantity", typeof(decimal)));//for TextBox value 
        DtEstimateQuanPrice.Columns.Add(new DataColumn("MeasuringUnit", typeof(string)));//for DropDownList selected item
        DtEstimateQuanPrice.Columns.Add(new DataColumn("EstimatePrice", typeof(decimal)));//for TextBox value
        drEstimateQuanPrice = DtEstimateQuanPrice.NewRow();
        drEstimateQuanPrice["RowNumber"] = 1;
        drEstimateQuanPrice["EstimateQuantity"] = "0.0";
        drEstimateQuanPrice["EstimatePrice"] = "0.0";
        drEstimateQuanPrice["Year"] = "-1";
        drEstimateQuanPrice["MeasuringUnit"] = "-1";
        DtEstimateQuanPrice.Rows.Add(drEstimateQuanPrice);
        ViewState["CurrentTableEstimateQuan"] = DtEstimateQuanPrice;
        GvEstimateQuanPrice.DataSource = DtEstimateQuanPrice;
        GvEstimateQuanPrice.DataBind();
    }
    private void AddNewRowToGridEstimate()
    {
        if (ViewState["CurrentTableEstimateQuan"] != null)
        {
            DataTable dtEstimateCurrentTable = (DataTable)ViewState["CurrentTableEstimateQuan"];
            DataRow drEstimateCurrentRow = null;
            if (dtEstimateCurrentTable.Rows.Count <= 4)
            {
                drEstimateCurrentRow = dtEstimateCurrentTable.NewRow();
                drEstimateCurrentRow["RowNumber"] = dtEstimateCurrentTable.Rows.Count + 1;
                //add new row to DataTable 
                dtEstimateCurrentTable.Rows.Add(drEstimateCurrentRow);
                //Store the current data to ViewState for future reference 
                ViewState["CurrentTable"] = dtEstimateCurrentTable;
                for (int i = 0; i < dtEstimateCurrentTable.Rows.Count - 1; i++)
                {
                    //extract the TextBox values
                    DropDownList ddlestimatequanYear = (DropDownList)GvEstimateQuanPrice.Rows[i].Cells[1].FindControl("ddlestimatequanYear");
                    TextBox txtEstimateQuantity = (TextBox)GvEstimateQuanPrice.Rows[i].Cells[2].FindControl("txtEstimateQuantity");
                    DropDownList ddlMeasurUnit = (DropDownList)GvEstimateQuanPrice.Rows[i].Cells[3].FindControl("ddlMeasurUnit");
                    TextBox txtestimPrice = (TextBox)GvEstimateQuanPrice.Rows[i].Cells[4].FindControl("txtestimPrice");
                    dtEstimateCurrentTable.Rows[i]["Year"] = ddlestimatequanYear.SelectedItem.Text;
                    dtEstimateCurrentTable.Rows[i]["EstimateQuantity"] = txtEstimateQuantity.Text;
                    dtEstimateCurrentTable.Rows[i]["MeasuringUnit"] = ddlMeasurUnit.SelectedItem.Text;
                    dtEstimateCurrentTable.Rows[i]["EstimatePrice"] = txtestimPrice.Text;
                }
                //Rebind the Grid with the current data to reflect changes 
                GvEstimateQuanPrice.DataSource = dtEstimateCurrentTable;
                GvEstimateQuanPrice.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Maximum 5 row added.')", true);
            }

        }
        else
        {
            Response.Write("ViewState is null");
        }
        //Set Previous Data on Postbacks 
        SetPreviousDataEstimateQuantity();
    }
    private void SetPreviousDataEstimateQuantity()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTableEstimateQuan"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTableEstimateQuan"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList ddlestimatequanYear = (DropDownList)GvEstimateQuanPrice.Rows[rowIndex].Cells[1].FindControl("ddlestimatequanYear");
                    TextBox txtEstimateQuantity = (TextBox)GvEstimateQuanPrice.Rows[i].Cells[2].FindControl("txtEstimateQuantity");
                    DropDownList ddlMeasurUnit = (DropDownList)GvEstimateQuanPrice.Rows[rowIndex].Cells[3].FindControl("ddlMeasurUnit");
                    TextBox txtestimPrice = (TextBox)GvEstimateQuanPrice.Rows[i].Cells[4].FindControl("txtestimPrice");
                    if (i < dt.Rows.Count - 1)
                    {
                        ddlestimatequanYear.Items.FindByText(dt.Rows[i]["Year"].ToString()).Selected = true;
                        txtEstimateQuantity.Text = dt.Rows[i]["EstimateQuantity"].ToString();
                        ddlMeasurUnit.Items.FindByText(dt.Rows[i]["MeasuringUnit"].ToString()).Selected = true;
                        txtestimPrice.Text = dt.Rows[i]["EstimatePrice"].ToString();
                    }
                    rowIndex++;
                }
            }
        }
    }
    protected void lbAddMoreRow_Click(object sender, EventArgs e)
    {
        AddNewRowToGridEstimate();
    }
    protected void GvEstimateQuanPrice_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dtEstimatequan = (DataTable)ViewState["CurrentTableEstimateQuan"];
            LinkButton lbestimatequan = (LinkButton)e.Row.FindControl("lbRemoveEstiQuan");
            if (lbestimatequan != null)
            {
                if (dtEstimatequan.Rows.Count > 1)
                {
                    if (e.Row.RowIndex == dtEstimatequan.Rows.Count - 1)
                    {
                        lbestimatequan.Visible = false;
                    }
                }
                else
                {
                    lbestimatequan.Visible = false;
                }
            }
        }
    }
    protected void lbRemoveEstiQuan_Click(object sender, EventArgs e)
    {
        LinkButton lbestimatequan = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lbestimatequan.NamingContainer;
        int rowID = gvRow.RowIndex;
        if (ViewState["CurrentTableEstimateQuan"] != null)
        {
            DataTable dtEstimatequan = (DataTable)ViewState["CurrentTableEstimateQuan"];
            if (dtEstimatequan.Rows.Count > 1)
            {
                if (gvRow.RowIndex < dtEstimatequan.Rows.Count - 1)
                {
                    //Remove the Selected Row data and reset row number
                    dtEstimatequan.Rows.Remove(dtEstimatequan.Rows[rowID]);
                    ResetRowIDEstimate(dtEstimatequan);
                }
            }
            //Store the current data in ViewState for future reference
            ViewState["CurrentTableEstimateQuan"] = dtEstimatequan;
            //Re bind the GridView for the updated data
            GvEstimateQuanPrice.DataSource = dtEstimatequan;
            GvEstimateQuanPrice.DataBind();
        }
        //Set Previous Data on Postbacks
        SetPreviousDataEstimateQuantity();
    }
    private void ResetRowIDEstimate(DataTable dtEstimatequan)
    {
        int rowNumber = 1;
        if (dtEstimatequan.Rows.Count > 0)
        {
            foreach (DataRow row in dtEstimatequan.Rows)
            {
                row[0] = rowNumber;
                rowNumber++;
            }
        }
    }
    protected DataTable SaveCodeEstimateQuantity()
    {
        DataTable DNSaveEstimateQuen = new DataTable();
        int rowIndex = 0;

        DNSaveEstimateQuen.Columns.Add(new DataColumn("ProdQtyPriceId", typeof(int)));
        DNSaveEstimateQuen.Columns.Add(new DataColumn("Year", typeof(int)));
        DNSaveEstimateQuen.Columns.Add(new DataColumn("FYear", typeof(decimal)));
        DNSaveEstimateQuen.Columns.Add(new DataColumn("Unit", typeof(string)));
        DNSaveEstimateQuen.Columns.Add(new DataColumn("EstimatedQty", typeof(decimal)));
        DNSaveEstimateQuen.Columns.Add(new DataColumn("EstimatedPrice", typeof(decimal)));
        DataRow drEstimateQuantitySave;
        {
            try
            {
                if (ViewState["CurrentTableEstimateQuan"] != null)
                {
                    for (int i = 1; i <= GvEstimateQuanPrice.Rows.Count; i++)
                    {
                        DropDownList ddlestimatequanYear = (DropDownList)GvEstimateQuanPrice.Rows[rowIndex]
                            .Cells[1].FindControl("ddlestimatequanYear");
                        TextBox txtEstimateQuantity = (TextBox)GvEstimateQuanPrice.Rows[rowIndex].Cells[2]
                            .FindControl("txtEstimateQuantity");
                        DropDownList ddlMeasurUnit = (DropDownList)GvEstimateQuanPrice.Rows[rowIndex].Cells[3]
                            .FindControl("ddlMeasurUnit");
                        TextBox txtestimPrice = (TextBox)GvEstimateQuanPrice.Rows[rowIndex].Cells[4]
                            .FindControl("txtestimPrice");
                        drEstimateQuantitySave = DNSaveEstimateQuen.NewRow();
                        drEstimateQuantitySave["ProdQtyPriceId"] = "-1";
                        drEstimateQuantitySave["Year"] = Convert.ToInt64(ddlestimatequanYear.SelectedItem.Value);
                        drEstimateQuantitySave["FYear"] = ddlestimatequanYear.SelectedItem.Text.Trim();
                        drEstimateQuantitySave["Unit"] = ddlMeasurUnit.SelectedItem.Text.Trim();
                        drEstimateQuantitySave["EstimatedQty"] = Convert.ToDecimal(txtEstimateQuantity.Text.Trim());
                        drEstimateQuantitySave["EstimatedPrice"] = Convert.ToDecimal(txtestimPrice.Text.Trim());
                        DNSaveEstimateQuen.Rows.Add(drEstimateQuantitySave);
                    }
                }
            }
            catch (Exception Ex)
            { }
            return DNSaveEstimateQuen;
        }

    }
    #endregion
}