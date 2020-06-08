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
    #region Load Variable
    private Cryptography objEnc = new Cryptography();
    private DataUtility Co = new DataUtility();
    private Logic Lo = new Logic();
    private DataTable dtImage = new DataTable();
    private DataTable dtPdf = new DataTable();
    public DataTable dtSaveProdInfo = new DataTable();
    public DataTable dtSaveEstimateQuantity = new DataTable();
    public DataTable dtSaveEstimateQuantity1 = new DataTable();
    DataTable DtNSFIIG = new DataTable();
    private string _msg = string.Empty;
    private string _sysMsg = string.Empty;
    public string Services = "";
    public string Remarks = "";
    public string ProcurmentCat = "";
    public string NodalDDL = "";
    public string IsImportedyesYear = "";
    private DataTable DtCompanyDDL = new DataTable();
    private HybridDictionary HyPanel1 = new HybridDictionary();
    #endregion
    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] != null)
        {
            if (!IsPostBack)
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
                            BindGrid();
                            SetInitialRowEstimateQuanPrice();
                        }
                        if (hidType.Value.ToString() != "SuperAdmin" || hidType.Value.ToString() != "Admin")
                        {
                            BindCompany();
                            BindCountry();
                            BindFinancialYear();
                            IsProductImported();
                            if (Request.QueryString["mcurrentcompRefNo"] != null)
                            {
                                BindMasterCategory();
                                //  BindMasterSubCategory();
                                // BindMaster3levelSubCategory();
                                BindMasterTechnologyCategory();
                                BindMasterPlatCategory();
                                BindEndUser();
                                BindNodelEmail();
                            }
                            else
                            {
                                BindMasterCategory();
                                //  BindMasterSubCategory();
                                //   BindMaster3levelSubCategory();
                                BindMasterTechnologyCategory();
                                BindMasterPlatCategory();
                                BindEndUser();
                            }
                        }
                        else
                        {
                            BindCompany();
                            BindCountry();
                            BindFinancialYear();
                            IsProductImported();
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
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "ErrorMssgPopup('Session Expired,Please login again');window.location='Login'", true);
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
                    ddlcompany.Enabled = false;
                    if (DtCompanyDDL.Rows.Count > 0)
                    {
                        Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                        divlblselectunit.Visible = false;
                        divlblselectdivison.Visible = false;
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
                    }
                    else
                    {
                        ddldivision.Enabled = false;
                    }
                }
                else if (objEnc.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Unit")
                {
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
                {
                    ddldivision.SelectedValue = dt.Rows[0]["FactoryRefNo"].ToString();
                }
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
                {
                    ddldivision.SelectedValue = dt.Rows[0]["FactoryRefNo"].ToString();
                }
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
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('select Company for displayed nodal offcier')", true);
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
                divlblselectdivison.Visible = true;
                ddldivision.Visible = true;
                hidCompanyRefNo.Value = ddlcompany.SelectedItem.Value;
                hidType.Value = "Company";
                BindMasterCategory();
                BindMasterTechnologyCategory();
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
            BindNodelEmail();
        }
        else
        {
            hidCompanyRefNo.Value = ddldivision.SelectedItem.Value;
            hidType.Value = "Division";
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
            DataTable DtGetNodel = Lo.RetriveMasterData(Convert.ToInt32(ddlNodalOfficerEmail.SelectedItem.Value), "", "", 0, "", "", "SearchNodalOfficerID");
            if (DtGetNodel.Rows.Count > 0)
            {
                contactpanel1.Visible = true;
                txtNEmailId.Text = DtGetNodel.Rows[0]["NodalOfficerEmail"].ToString();
                txtNTelephone.Text = DtGetNodel.Rows[0]["NodalOfficerTelephone"].ToString();
                txtNFaxNo.Text = DtGetNodel.Rows[0]["NodalOfficerFax"].ToString();
                txtDesignation.Text = DtGetNodel.Rows[0]["Designation"].ToString();
                txtempcode.Text = DtGetNodel.Rows[0]["NodalEmpCode"].ToString();
                txtmobnodal.Text = DtGetNodel.Rows[0]["NodalOfficerMobile"].ToString();
                //===Bind Nodel officer except Nodel one
                DtCompanyDDL = Lo.RetriveMasterData(Convert.ToInt32(ddlNodalOfficerEmail.SelectedItem.Value), DtGetNodel.Rows[0]["CompanyRefNo"].ToString(), "", 0, "", "", "AllNodelNotSelect");
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
            DataTable DtGetNodel = Lo.RetriveMasterData(Convert.ToInt32(ddlNodalOfficerEmail2.SelectedItem.Value), "", "", 0, "", "", "SearchNodalOfficerID");
            if (DtGetNodel.Rows.Count > 0)
            {
                contactpanel2.Visible = true;
                txtNEmailId2.Text = DtGetNodel.Rows[0]["NodalOfficerEmail"].ToString();
                txtNTelephone2.Text = DtGetNodel.Rows[0]["NodalOfficerTelephone"].ToString();
                txtNFaxNo2.Text = DtGetNodel.Rows[0]["NodalOfficerFax"].ToString();
                txtdesignationnodal2.Text = DtGetNodel.Rows[0]["Designation"].ToString();
                txtempcode2.Text = DtGetNodel.Rows[0]["NodalEmpCode"].ToString();
                txtmobnodal2.Text = DtGetNodel.Rows[0]["NodalOfficerMobile"].ToString();
                //===Bind Nodel officer expect Nodel Two                
                DtCompanyDDL = Lo.RetriveMasterData(Convert.ToInt32(ddlNodalOfficerEmail2.SelectedItem.Value), DtGetNodel.Rows[0]["CompanyRefNo"].ToString(), "", 0, "", "", "AllNodelNotSelect");
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
        string m = ddlsubcategory.SelectedItem.Value;
        DataTable dt2 = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlsubcategory.SelectedItem.Value), "", "", "2to2", "", "");
        if (dt2.Rows.Count > 0)
        {
            DataTable dt1sr = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlsubcategory.SelectedItem.Value), "", "", "3to21", "", "");
            ddlmastercategory.SelectedValue = dt1sr.Rows[0]["SCategoryId"].ToString();
            Co.FillDropdownlist(ddlsubcategory, dt2, "SCategoryName", "SCategoryId");
            ddlsubcategory.Items.Insert(0, "Select");
            ddlsubcategory.SelectedValue = m;
            DataTable dtbindvalue = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlsubcategory.SelectedItem.Value), "", "", "SubSelectID", "", "");
            if (dtbindvalue.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddllevel3product, dtbindvalue, "SCategoryName", "SCategoryId");
                ddllevel3product.Items.Insert(0, "Select");
            }
            else
            {
                ddllevel3product.Items.Clear();
                ddllevel3product.Items.Insert(0, "Select");
                ddllevel3product.Items.Insert(1, "NA");
            }
        }
        NSCCode(ddlmastercategory.SelectedItem.Text, ddlsubcategory.SelectedItem.Text);
    }
    protected void ddllevel3product_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddllevel3product.SelectedItem.Text != "Select" && ddllevel3product.SelectedItem.Text != "NA")
        {
            lblviewitemcode.Visible = true;
            //DataTable dt1 = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddllevel3product.SelectedItem.Value), "", "", "3to2", "", "");
            //if (dt1.Rows.Count > 0)
            //{
            //    Co.FillDropdownlist(ddlsubcategory, dt1, "SCategoryName", "SCategoryId");
            //    ddlsubcategory.Items.Insert(0, "Select");
            //    DataTable dtbindvalue = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddllevel3product.SelectedItem.Value), "", "", "3to21", "", "");
            //    ddlsubcategory.SelectedValue = dtbindvalue.Rows[0]["SCategoryId"].ToString();
            //    DataTable dt1sr = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlsubcategory.SelectedItem.Value), "", "", "3to21", "", "");
            //    ddlmastercategory.SelectedValue = dt1sr.Rows[0]["SCategoryId"].ToString();
            //}
            //else
            //{
            //    ddllevel3product.SelectedValue = "Select";
            //}
            //NSCCode(ddlmastercategory.SelectedItem.Text, ddlsubcategory.SelectedItem.Text);
        }
        else
        {
            lblviewitemcode.Visible = false;
        }
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
    protected void rbproductImported_CheckedChanged(object sender, EventArgs e)
    {
        if (rbproductImported.SelectedItem.Value == "Y")
        {
            divyearofimportYes.Visible = true;
        }
        else if (rbproductImported.SelectedItem.Value == "N")
        {
            divyearofimportYes.Visible = false;
        }
    }
    protected void IsProductImported()
    {
        if (rbproductImported.SelectedItem.Value == "Y")
        {
            divyearofimportYes.Visible = true;
        }
        else if (rbproductImported.SelectedItem.Value == "N")
        {
            divyearofimportYes.Visible = false;
        }
    }
    private void NSCCode(string NSNGroupddl, string NSNClassddl)
    {
        try
        {
            string a = NSNGroupddl.Substring((NSNGroupddl.IndexOf("(") + 1), NSNGroupddl.IndexOf(")") - (NSNGroupddl.IndexOf("(") + 1));
            string b = NSNClassddl.Substring((NSNClassddl.IndexOf("(") + 1), NSNClassddl.IndexOf(")") - (NSNClassddl.IndexOf("(") + 1));
            txtnsccode.Text = a + b;
        }
        catch (Exception)
        {
            txtnsccode.Text = "";
        }
    }
    private void Nsncode8digit(string NSNGroupddl)
    {
        try
        {
            string a = NSNGroupddl.Substring((NSNGroupddl.IndexOf("(") + 1), NSNGroupddl.IndexOf(")") - (NSNGroupddl.IndexOf("(") + 1));
            txthsncodereadonly.Text = a;
        }
        catch (Exception)
        {
            txthsncodereadonly.Text = "";
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
            ddlsubcategory.SelectedIndex = -1;
            ddllevel3product.SelectedIndex = -1;
        }
        else
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "NSN GROUP", "", "SelectProductCat", "", "");
            Co.FillDropdownlist(ddlmastercategory, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlmastercategory.Items.Insert(0, "Select");
            ddlsubcategory.SelectedIndex = -1;
            ddllevel3product.SelectedIndex = -1;
        }
    }
    protected void BindMasterSubCategory()
    {
        DataTable DtMasterCategroy = new DataTable();
        if (ddlmastercategory.SelectedItem.Text != "Select")
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlmastercategory.SelectedItem.Value), "", "", "SubSelectID", "", "");
        }
        else
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "", "", "SubSelectSec", "", "");
        }
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlsubcategory, DtMasterCategroy, "SCategoryName", "SCategoryId");
            ddlsubcategory.Items.Insert(0, "Select");
            ddllevel3product.SelectedIndex = -1;
        }
        else
        {
            ddlsubcategory.Items.Clear();
            ddlsubcategory.Items.Insert(0, "Select");
        }
    }
    protected void BindMaster3levelSubCategory()
    {
        DataTable DtMasterCategroyLevel3 = new DataTable();
        //if (ddlsubcategory.SelectedItem.Value != null || ddlsubcategory.SelectedItem.Text != "Select")
        //{
        if (ddlsubcategory.SelectedIndex != -1)
        { DtMasterCategroyLevel3 = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlsubcategory.SelectedItem.Value), "", "", "SubSelectID", "", ""); }
        else
        { DtMasterCategroyLevel3 = Lo.RetriveMasterSubCategoryDate(0, "", "", "SubSelectthr", "", ""); }
        if (DtMasterCategroyLevel3.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddllevel3product, DtMasterCategroyLevel3, "SCategoryName", "SCategoryId");
            ddllevel3product.Items.Insert(0, "Select");
            lblviewitemcode.Visible = true;
        }
        else
        {
            lblviewitemcode.Visible = false;
            ddllevel3product.Items.Clear();
            ddllevel3product.Items.Insert(0, "Select");
            ddllevel3product.Items.Insert(1, "NA");
        }
        //}
    }
    protected void BindItemDescription()
    {
        if (ddllevel3product.SelectedItem.Value != null || ddllevel3product.SelectedItem.Text != "Select")
        {
            if (ddllevel3product.SelectedItem.Value != "NA")
            {
                DataTable DtItemDescription = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddllevel3product.SelectedItem.Value), "", "", "Level3ID", "", "");
                if (DtItemDescription.Rows.Count > 0)
                {
                    txtproductdescription.Text = DtItemDescription.Rows[0]["Description"].ToString();
                }
                else
                {
                    txtproductdescription.Text = "";
                }
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
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddltechnologycat.SelectedItem.Value), "", "", "SubSelectID", "", "");
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
            DataTable DtMasterCategroyLevel3 = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlsubtech.SelectedItem.Value), "", "", "SubSelectID", "", "");
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
    #region For NomenClature
    protected void BindMasterProductNoenCletureCategory()
    {
        DataTable DtNAMEOFDEFENCEPLATFORM = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlplatform.SelectedItem.Value), "", "", "SubSelectID", "", "");
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
            Co.FillListBoxlist(ddlenduser, DtMasterCategroy, "SCategoryName", "SCategoryID");
        }
        else
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, lblenduser.Text, "", "SelectInnerMaster1", "", "");
            Co.FillListBoxlist(ddlenduser, DtMasterCategroy, "SCategoryName", "SCategoryID");
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
        DataTable DtCountry = Lo.RetriveCountry(0, "Select");
        if (DtCountry.Rows.Count > 0)
        {
            Co.FillDropdownlist(txtcountry, DtCountry, "CountryName", "CountryID");
            txtcountry.Items.Insert(0, "Select");
        }
    }
    #endregion
    #region PanelSaveCode
    private string m;
    string a, b = string.Empty;
    protected void SaveProductDescription()
    {
        try
        {
            if (hfprodid.Value != "")
            {
                HyPanel1["ProductID"] = Convert.ToInt32(hfprodid.Value);
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
                DtNSFIIG = SaveFiigNo();
            }
            else
            {
                HyPanel1["ProductLevel3"] = null;
            }
            HyPanel1["ProductDescription"] = Co.RSQandSQLInjection(txtproductdescription.Text.Trim(), "soft");
            HyPanel1["NSCCode"] = Co.RSQandSQLInjection(txtnsccode.Text.Trim(), "soft");
            HyPanel1["NIINCode"] = Co.RSQandSQLInjection(txtniincode.Text.Trim(), "soft");
            HyPanel1["ItemDescriptionPDFFile"] = null;
            if (fuitemdescriptionfile.HasFiles != false)
            {
                if (hfprodid.Value != "")
                {
                    DataTable dtPdfBind = Lo.RetriveProductCode("", hfprodrefno.Value, "RetrivePdf", hidType.Value);
                    if (dtPdfBind.Rows.Count > 0)
                    {
                        short CountPdfTotal = Convert.ToInt16(fuitemdescriptionfile.PostedFiles.Count);
                        short AlreadyUploadpdf = Convert.ToInt16(dtPdfBind.Rows.Count);
                        PdfMaxCount = (CountPdfTotal + AlreadyUploadpdf);
                        if (PdfMaxCount <= 4)
                        {
                            dtPdf = Pdfdb();
                        }
                    }
                    else
                    {
                        if (fuitemdescriptionfile.HasFiles != false)
                        {
                            PdfMaxCount = 4;
                            dtPdf = Pdfdb();
                        }
                    }
                }
                else
                {
                    PdfMaxCount = 4;
                    dtPdf = Pdfdb();
                }
            }
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
            HyPanel1["OEMPartNumber"] = Co.RSQandSQLInjection(txtoempartnumber.Text.Trim(), "soft");
            HyPanel1["OEMName"] = Co.RSQandSQLInjection(txtoemname.Text.Trim(), "soft");
            HyPanel1["OEMCountry"] = Convert.ToInt64(txtcountry.SelectedItem.Value);
            HyPanel1["DPSUPartNumber"] = Co.RSQandSQLInjection(txtdpsupartnumber.Text.Trim(), "soft");
            HyPanel1["HSCode"] = null;
            HyPanel1["HSNCode"] = null;
            HyPanel1["HSChapter"] = null;
            HyPanel1["HSNCodeLevel1"] = null;
            HyPanel1["HSNCodeLevel2"] = null;
            HyPanel1["HSNCodeLevel3"] = null;
            // }
            HyPanel1["HsCode4digit"] = "";
            HyPanel1["HsnCode8digit"] = txthsncodereadonly.Text.Trim();
            HyPanel1["EndUserPartNumber"] = "";
            if (ddlenduser.SelectedItem.Value != "Select")
            {
                foreach (ListItem li in ddlenduser.Items)
                {
                    if (li.Selected)
                    {
                        m = m + li.Value + ",";
                    }
                }
                HyPanel1["EndUser"] = m;
            }
            else
            {
                HyPanel1["EndUser"] = null;
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
            HyPanel1["TechnologyLevel3"] = null;
            HyPanel1["SearchKeyword"] = Co.RSQandSQLInjection(txtsearchkeyword.Text.Trim(), "soft");
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
            if (rbproductImported.SelectedItem.Value == "N")
            {
                HyPanel1["IsProductImported"] = Co.RSQandSQLInjection(rbproductImported.SelectedItem.Value.Trim(), "soft");
                HyPanel1["YearofImport"] = null;
                HyPanel1["YearofImportRemarks"] = Co.RSQandSQLInjection(txtremarksyearofimportyes.Text.Trim(), "soft");
            }
            else
            {
                HyPanel1["IsProductImported"] = Co.RSQandSQLInjection(rbproductImported.SelectedItem.Value.Trim(), "soft");
                dtSaveEstimateQuantity1 = Dvinsert();
                HyPanel1["YearofImport"] = null;
                HyPanel1["NEstimateQuantity"] = null;
                HyPanel1["NEstimateUnit"] = null;
                HyPanel1["NEstimatePrice"] = null;
                HyPanel1["YearofImportRemarks"] = Co.RSQandSQLInjection(txtremarksyearofimportyes.Text.Trim(), "soft");
            }
            HyPanel1["FeatursandDetail"] = Co.RSQandSQLInjection(txtfeaturesanddetails.Text.Trim(), "soft");
            HyPanel1["ItemSpecification"] = Co.RSQandSQLInjection(rbitemspecification.SelectedValue, "soft");
            if (gvProductInformation.Rows.Count != 0)
            {
                dtSaveProdInfo = SaveCodeProdInfo();
            }
            HyPanel1["AdditionalDetail"] = Co.RSQandSQLInjection(txtadditionalinfo.Text.Trim(), "soft");
            if (ViewState["MF"] != null)
            {
                SaveCodeEstimateQuantity();
                dtSaveEstimateQuantity = (DataTable)ViewState["MF"];
            }
            foreach (ListItem li in rbIgCategory.Items)
            {
                if (li.Selected == true)
                {
                    a = a + rbIgCategory.SelectedItem.Value + ",";
                }
            }
            HyPanel1["PurposeofProcurement"] = a.ToString();
            HyPanel1["ProcurmentCategoryRemark"] = Co.RSQandSQLInjection(txtremarksprocurmentCategory.Text.Trim(), "soft");
            HyPanel1["TenderSubmition"] = "";
            HyPanel1["TenderStatus"] = "";
            HyPanel1["TenderFillDate"] = null;
            HyPanel1["TenderUrl"] = "";
            HyPanel1["EOIStatus"] = Co.RSQandSQLInjection(rbeoimake2.SelectedValue, "soft");
            HyPanel1["EOISubmition"] = "";
            HyPanel1["EOIFillDate"] = null;
            HyPanel1["EOIURL"] = txteoilink.Text;
            HyPanel1["QAAgency"] = "";
            HyPanel1["QAReamrks"] = "";
            HyPanel1["Testing"] = "";
            HyPanel1["TestingRemarks"] = "";
            HyPanel1["Certification"] = "";
            HyPanel1["CertificationRemark"] = "";
            HyPanel1["DPSUServices"] = "";
            HyPanel1["Remarks"] = "";
            HyPanel1["FinancialSupport"] = "";
            HyPanel1["FinancialRemark"] = "";
            if (ddlNodalOfficerEmail.Text == "" || ddlNodalOfficerEmail.SelectedItem.Text == "Select")
            {
                HyPanel1["NodelDetail"] = null;
            }
            else
            {
                HyPanel1["NodelDetail"] = Convert.ToInt32(ddlNodalOfficerEmail.SelectedItem.Value);
            }
            if (ddlNodalOfficerEmail2.Text == "" || ddlNodalOfficerEmail2.SelectedItem.Text == "Select")//ddlprocurmentcategory
            {
                HyPanel1["NodalDetail2"] = null;
            }
            else
            {
                HyPanel1["NodalDetail2"] = Convert.ToInt32(ddlNodalOfficerEmail2.SelectedItem.Value);
            }
            HyPanel1["ShowGeneralDec"] = rbeligible.SelectedItem.Value;
            HyPanel1["CreatedBy"] = ViewState["UserLoginEmail"].ToString();
            string StrProductDescription = Lo.SaveCodeProduct(HyPanel1, dtImage, dtPdf, dtSaveProdInfo, dtSaveEstimateQuantity, dtSaveEstimateQuantity1, DtNSFIIG, out _sysMsg, out _msg, "Product");
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
        catch (Exception)
        {
        }
    }
    #endregion
    #region PanelSaveButtonCode
    protected void btnsubmitpanel1_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlcompany.SelectedItem.Text != "Select" && txtproductdescription.Text != "" && ddlmastercategory.SelectedItem.Text != "Select" && ddlsubcategory.SelectedItem.Text != "Select" && ddllevel3product.SelectedItem.Text != "Select" && ddltechnologycat.SelectedItem.Text != "Select" && ddlcompany.SelectedItem.Text != "Select")
            {
                if (ddlsubtech.SelectedItem.Text != "Select" && ddlnomnclature.SelectedItem.Text != "Select" && ddlenduser.SelectedItem.Text != "Select" && ddlplatform.SelectedItem.Text != "Select")
                {
                    if (txtcountry.SelectedItem.Text != "Select")
                    {
                        if (rbeligible.SelectedIndex != -1)
                        {
                            string checklistdeclaration = "";
                            foreach (ListItem chkd in chklistdeclarationimage.Items)
                            {
                                if (chkd.Selected == true)
                                {
                                    checklistdeclaration = checklistdeclaration + chkd;
                                }
                            }
                            if (checklistdeclaration != "")
                            {
                                if (fuitemdescriptionfile.HasFile != false)
                                {
                                    int iFileSize = fuitemdescriptionfile.PostedFile.ContentLength;
                                    if (iFileSize > 5242880) // 5MB
                                    {
                                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Maximum 5 Mb pdf file can be uploaded')", true);
                                    }
                                    else
                                    {
                                        if (fuimages.HasFile != false)
                                        {
                                            int filecount = 0;
                                            filecount = Convert.ToInt32(fuimages.PostedFiles.Count.ToString());
                                            if (filecount <= 4)
                                            {
                                                int iImageFileSize = fuimages.PostedFile.ContentLength;
                                                if (iImageFileSize > 20971520)
                                                {
                                                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert",
                                                        "alert('Maximum 5 Mb each .jpg,.jpeg,.png,.tif images can be uploaded')", true);
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
                                        filecount = Convert.ToInt32(fuimages.PostedFiles.Count.ToString());
                                        if (filecount <= 4)
                                        {
                                            int iImageFileSize = fuimages.PostedFile.ContentLength;
                                            if (iImageFileSize > 20971520)
                                            {
                                                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert",
                                                    "alert('Maximum 5 Mb each .jpg,.jpeg,.png,.tif images can be uploaded')", true);
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
                                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Please select declaration tab and select all checkbox before submit.')", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Please select and check declaration section at last tab.')", true);
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
        catch (Exception ex)
        { ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "ErrorMssgPopup('" + ex.Message + "')", true); }
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
        txtcountry.SelectedIndex = 0;
        txtmanifacaddress.Text = "";
        ddlyearofindiginization.SelectedIndex = 0;
        txtremarksprocurmentCategory.Text = "";
        txtnsccode.Text = "";
        txtniincode.Text = "";
        txtdpsupartnumber.Text = "";
        dlimage.DataSource = null;
        dlimage.DataBind();
        divimgdel.Visible = false;
        ddlnomnclature.SelectedIndex = 0;
        txthsncodereadonly.Text = "";
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
        ddlsubcategory.Items.Clear();
        ddllevel3product.Items.Clear();
        ddlsubtech.Items.Clear();
        ddltechlevel3.Items.Clear();
        txtfeaturesanddetails.Text = "";
        txtadditionalinfo.Text = "";
        txtremarksyearofimportyes.Text = "";
        rbproductImported.SelectedIndex = 0;
        divyearofimportYes.Visible = false;
        txtNameOfSpecificationAdd.Text = "";
        TxtValueProdAdd.Text = "";
        txtUnitProdAdd.Text = "";
        contactpanel1.Visible = false;
        divnodal2.Visible = false;
        if (ddlNodalOfficerEmail.Text != "")
        {
            ddlNodalOfficerEmail.SelectedIndex = 0;
        }
        txtfiigno.Text = "";
        txtfiigtype.Text = "";
        ViewState["MF"] = null;
        ViewState["MF1"] = null;
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
                        if (DataUtility.Instance.GetImageFilter(postfiles.FileName) != false)
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
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Invalid file format " + postfiles.FileName + "')", true);
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
    #region PDF File Upload
    private int PdfMaxCount;
    protected DataTable Pdfdb()
    {
        DataTable dtPdf = new DataTable();
        dtPdf.Columns.Add(new DataColumn("PdfID", typeof(long)));
        dtPdf.Columns.Add(new DataColumn("PdfName", typeof(string)));
        dtPdf.Columns.Add(new DataColumn("PdfType", typeof(string)));
        dtPdf.Columns.Add(new DataColumn("PdfActualSize", typeof(long)));
        dtPdf.Columns.Add(new DataColumn("PCompanyRefNo", typeof(string)));
        dtPdf.Columns.Add(new DataColumn("Priority", typeof(int)));
        DataRow drPdf;
        {
            try
            {
                if (PdfMaxCount <= 4)
                {
                    foreach (HttpPostedFile postfilespdf in fuitemdescriptionfile.PostedFiles)
                    {
                        string FileTypePdf = Path.GetExtension(postfilespdf.FileName);
                        int FileSizePdf = postfilespdf.ContentLength;
                        if (DataUtility.Instance.GetFileFilter(postfilespdf.FileName) != false)
                        {
                            string FilePathNamePdf = hfcomprefno.Value + "_" + DateTime.Now.ToString("hh_mm_ss") + postfilespdf.FileName;
                            postfilespdf.SaveAs(HttpContext.Current.Server.MapPath("/Upload") + "\\" + FilePathNamePdf);
                            drPdf = dtPdf.NewRow();
                            drPdf["PdfID"] = "-1";
                            drPdf["PdfName"] = "Upload/" + FilePathNamePdf;
                            drPdf["PdfType"] = FileTypePdf.ToString();
                            drPdf["PdfActualSize"] = FileSizePdf.ToString();
                            drPdf["PCompanyRefNo"] = hfcomprefno.Value;
                            drPdf["Priority"] = PdfMaxCount++ + 1;
                            dtPdf.Rows.Add(drPdf);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Invalid file format " + postfilespdf.FileName + "')", true);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return dtPdf;
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
                    }
                }
                conn.Close();
            }
        }
        return customers.ToArray();
    }
    #endregion
    #region SetGridView Initial ProductInformaTION
    protected DataTable SaveCodeProdInfo()
    {
        DataTable DtNProdInfo = new DataTable();
        DtNProdInfo.Columns.Add(new DataColumn("ProdInfoId", typeof(int)));
        DtNProdInfo.Columns.Add(new DataColumn("Length", typeof(string)));
        DtNProdInfo.Columns.Add(new DataColumn("Value", typeof(string)));
        DtNProdInfo.Columns.Add(new DataColumn("ProductUnit", typeof(string)));
        DataRow drProdInfoSave;
        {
            try
            {
                for (int i = 0; i <= gvProductInformation.Rows.Count; i++)
                {
                    int txtProdInfoId = Convert.ToInt32(gvProductInformation.DataKeys[i].Value.ToString());
                    Label txtlenth = (Label)gvProductInformation.Rows[i].Cells[1].FindControl("lblNameofspec");
                    Label txtvalue = (Label)gvProductInformation.Rows[i].Cells[2].FindControl("lblvalueProd");
                    Label txtProdInfoUnit = (Label)gvProductInformation.Rows[i].Cells[3].FindControl("lblUnitProd");
                    drProdInfoSave = DtNProdInfo.NewRow();
                    drProdInfoSave["ProdInfoId"] = txtProdInfoId.ToString();
                    drProdInfoSave["Length"] = txtlenth.Text.Trim();
                    drProdInfoSave["Value"] = txtvalue.Text.Trim();
                    drProdInfoSave["ProductUnit"] = txtProdInfoUnit.Text.Trim();
                    DtNProdInfo.Rows.Add(drProdInfoSave);
                }
            }
            catch (Exception)
            { }
            return DtNProdInfo;
        }
    }
    private void BindGrid()
    {
        DataTable DtGrid = new DataTable();
        DtGrid = Lo.TestGrid("Select", hfprodrefno.Value, 0, "", "", "");
        if (DtGrid.Rows.Count > 0)
        {
            gvProductInformation.DataSource = DtGrid;
            gvProductInformation.DataBind();
            gvProductInformation.Visible = true;
        }
        else
        {
            gvProductInformation.Visible = false;
        }
    }
    protected void Insert(object sender, EventArgs e)
    {
        string nameofspeci = txtNameOfSpecificationAdd.Text;
        string ValueProd = TxtValueProdAdd.Text;
        string UnitProd = txtUnitProdAdd.Text;
        Lo.TestGrid("Insert", hfprodrefno.Value, 0, nameofspeci, ValueProd, UnitProd);
        txtNameOfSpecificationAdd.Text = "";
        TxtValueProdAdd.Text = "";
        txtUnitProdAdd.Text = "";
        BindGrid();
    }
    protected void OnRowEditing(object sender, GridViewEditEventArgs e)
    {
        gvProductInformation.EditIndex = e.NewEditIndex;
        BindGrid();
    }
    protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gvProductInformation.Rows[e.RowIndex];
        int ProdSpeciId = Convert.ToInt32(gvProductInformation.DataKeys[e.RowIndex].Values[0]);
        string namePro = (row.FindControl("txtNameofspeci") as TextBox).Text;
        string ValuePro = (row.FindControl("txtValueProd") as TextBox).Text;
        string unitPro = (row.FindControl("txtUnitProd") as TextBox).Text;
        Lo.TestGrid("Update", hfprodrefno.Value, ProdSpeciId, namePro, ValuePro, unitPro);
        gvProductInformation.EditIndex = -1;
        BindGrid();
    }
    protected void OnRowCancelingEdit(object sender, EventArgs e)
    {
        gvProductInformation.EditIndex = -1;
        BindGrid();
    }
    protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ProdIdDel = Convert.ToInt32(gvProductInformation.DataKeys[e.RowIndex].Values[0]);
        Lo.TestGrid("Delete", "", ProdIdDel, "", "", "");
        BindGrid();
    }
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gvProductInformation.EditIndex)
        {
            (e.Row.Cells[4].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";
        }
    }
    protected void OnPaging(object sender, GridViewPageEventArgs e)
    {
        gvProductInformation.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    #endregion
    #region Add Grid of EstimateQuanPrice
    private void SetInitialRowEstimateQuanPrice()
    {
        try
        {
            DataTable dtMF = new DataTable();
            DataRow drMF = null;
            dtMF.Columns.Add(new DataColumn("SNo", typeof(string)));
            dtMF.Columns.Add(new DataColumn("FYear", typeof(string)));
            dtMF.Columns.Add(new DataColumn("EstimatedQty", typeof(string)));
            dtMF.Columns.Add(new DataColumn("Unit", typeof(string)));
            dtMF.Columns.Add(new DataColumn("EstimatedPrice", typeof(string)));
            drMF = dtMF.NewRow();
            drMF["SNo"] = 1;
            drMF["FYear"] = string.Empty;
            drMF["EstimatedQty"] = string.Empty;
            drMF["Unit"] = string.Empty;
            drMF["EstimatedPrice"] = string.Empty;
            dtMF.Rows.Add(drMF);
            //Store the DataTable in ViewState or bind or show false grid
            ViewState["MF"] = dtMF;
            GvEstiateQuanPrice.DataSource = dtMF;
            GvEstiateQuanPrice.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void AddNewRowToGridEstimateQuanPrice()
    {
        try
        {
            int MFrowIndex = 0;
            if (ViewState["MF"] != null)
            {
                DataTable dtCurrentTableMF = (DataTable)ViewState["MF"];
                DataRow drCurrentRowMF = null;
                if (dtCurrentTableMF.Rows.Count > 0 && dtCurrentTableMF.Rows.Count < 5)
                {
                    for (int i = 1; i <= dtCurrentTableMF.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        DropDownList ddl1 = (DropDownList)GvEstiateQuanPrice.Rows[MFrowIndex].Cells[1].FindControl("ddlYearEstimate");
                        TextBox TextBox1 = (TextBox)GvEstiateQuanPrice.Rows[MFrowIndex].Cells[2].FindControl("txtestimateQuantity");
                        DropDownList ddl2 = (DropDownList)GvEstiateQuanPrice.Rows[MFrowIndex].Cells[3].FindControl("ddlMeasuringUnit");
                        TextBox TextBox2 = (TextBox)GvEstiateQuanPrice.Rows[MFrowIndex].Cells[4].FindControl("txtestimatePriceLLp");
                        drCurrentRowMF = dtCurrentTableMF.NewRow();
                        drCurrentRowMF["SNo"] = i + 1;
                        dtCurrentTableMF.Rows[i - 1]["FYear"] = ddl1.Text;
                        dtCurrentTableMF.Rows[i - 1]["EstimatedQty"] = TextBox1.Text;
                        dtCurrentTableMF.Rows[i - 1]["Unit"] = ddl2.Text;
                        dtCurrentTableMF.Rows[i - 1]["EstimatedPrice"] = TextBox2.Text;
                        MFrowIndex++;
                    }
                    dtCurrentTableMF.Rows.Add(drCurrentRowMF);
                    ViewState["MF"] = dtCurrentTableMF;
                    GvEstiateQuanPrice.DataSource = dtCurrentTableMF;
                    GvEstiateQuanPrice.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Max five row add')", true);
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousDataGovtEstimateQuanPrice();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void SetPreviousDataGovtEstimateQuanPrice()
    {
        try
        {
            int rowIndexMF = 0;
            if (ViewState["MF"] != null)
            {
                DataTable dtMF = (DataTable)ViewState["MF"];
                if (dtMF.Rows.Count > 0)
                {
                    for (int i = 0; i < dtMF.Rows.Count; i++)
                    {
                        DropDownList DropDwon_1 = (DropDownList)GvEstiateQuanPrice.Rows[rowIndexMF].Cells[1].FindControl("ddlYearEstimate");
                        TextBox TextBox_1 = (TextBox)GvEstiateQuanPrice.Rows[rowIndexMF].Cells[2].FindControl("txtestimateQuantity");
                        DropDownList DropDwon_2 = (DropDownList)GvEstiateQuanPrice.Rows[rowIndexMF].Cells[3].FindControl("ddlMeasuringUnit");
                        TextBox TextBox_2 = (TextBox)GvEstiateQuanPrice.Rows[rowIndexMF].Cells[4].FindControl("txtestimatePriceLLp");
                        DropDwon_1.Text = dtMF.Rows[i]["FYear"].ToString();
                        TextBox_1.Text = dtMF.Rows[i]["EstimatedQty"].ToString();
                        DropDwon_2.Text = dtMF.Rows[i]["Unit"].ToString();
                        TextBox_2.Text = dtMF.Rows[i]["EstimatedPrice"].ToString();
                        rowIndexMF++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void btnAddEstimate_Click(object sender, EventArgs e)
    {
        AddNewRowToGridEstimateQuanPrice();
    }
    protected void GvEstiateQuanPrice_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dtgridMFacili = (DataTable)ViewState["MF"];
                LinkButton lbRMF = (LinkButton)e.Row.FindControl("lbRemoveestimate");
                if (lbRMF != null)
                {
                    if (dtgridMFacili.Rows.Count > 1)
                    {
                        if (e.Row.RowIndex == dtgridMFacili.Rows.Count - 1)
                        {
                            lbRMF.Visible = false;
                        }
                    }
                    else
                    {
                        lbRMF.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void lbRemoveestimate_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lbMF = (LinkButton)sender;
            GridViewRow gvRowMF = (GridViewRow)lbMF.NamingContainer;
            int rowID = gvRowMF.RowIndex;
            if (ViewState["MF"] != null)
            {
                DataTable dtremovegridMF = (DataTable)ViewState["MF"];
                if (dtremovegridMF.Rows.Count > 1)
                {
                    if (gvRowMF.RowIndex < dtremovegridMF.Rows.Count - 1)
                    {
                        dtremovegridMF.Rows.Remove(dtremovegridMF.Rows[rowID]);
                        ResetRowIDMFacilities(dtremovegridMF);
                    }
                }
                ViewState["MF"] = dtremovegridMF;
                GvEstiateQuanPrice.DataSource = dtremovegridMF;
                GvEstiateQuanPrice.DataBind();
            }
            SetPreviousDataGovtEstimateQuanPrice();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void ResetRowIDMFacilities(DataTable dtMfaci)
    {
        try
        {
            int rowNumberMfaci = 1;
            if (dtMfaci.Rows.Count > 0)
            {
                foreach (DataRow row in dtMfaci.Rows)
                {
                    row[0] = rowNumberMfaci;
                    rowNumberMfaci++;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    DataTable dtManufacturingFacilities = new DataTable();
    protected void SaveCodeEstimateQuantity()
    {
        try
        {
            int rowIndex = 0;
            DataTable dtManufacturingFacilities = new DataTable();
            dtManufacturingFacilities.Columns.Add(new DataColumn("SNo", typeof(string)));
            dtManufacturingFacilities.Columns.Add(new DataColumn("Year", typeof(string)));
            dtManufacturingFacilities.Columns.Add(new DataColumn("FYear", typeof(string)));
            dtManufacturingFacilities.Columns.Add(new DataColumn("EstimatedQty", typeof(string)));
            dtManufacturingFacilities.Columns.Add(new DataColumn("Unit", typeof(string)));
            dtManufacturingFacilities.Columns.Add(new DataColumn("EstimatedPrice", typeof(string)));
            dtManufacturingFacilities.Columns.Add(new DataColumn("Type", typeof(string)));
            DataRow drManufacturingFacilities = null;
            for (int i = 0; GvEstiateQuanPrice.Rows.Count > i; i++)
            {
                DropDownList TextBox1MF = (DropDownList)GvEstiateQuanPrice.Rows[i].Cells[1].FindControl("ddlYearEstimate");
                TextBox TextBox7MF = (TextBox)GvEstiateQuanPrice.Rows[i].Cells[2].FindControl("txtestimateQuantity");
                DropDownList TextBox2MF = (DropDownList)GvEstiateQuanPrice.Rows[i].Cells[3].FindControl("ddlMeasuringUnit");
                TextBox TextBox3MF = (TextBox)GvEstiateQuanPrice.Rows[i].Cells[4].FindControl("txtestimatePriceLLp");
                if (TextBox7MF.Text != "" && TextBox1MF.Text != "" && TextBox1MF.Text != "Select" && TextBox2MF.Text != "Select")
                {
                    drManufacturingFacilities = dtManufacturingFacilities.NewRow();
                    drManufacturingFacilities["Year"] = TextBox1MF.SelectedItem.Value;
                    drManufacturingFacilities["FYear"] = TextBox1MF.SelectedItem.Text;
                    drManufacturingFacilities["EstimatedQty"] = TextBox7MF.Text;
                    drManufacturingFacilities["Unit"] = TextBox2MF.Text;
                    drManufacturingFacilities["EstimatedPrice"] = TextBox3MF.Text;
                    drManufacturingFacilities["Type"] = "F";
                    dtManufacturingFacilities.Rows.Add(drManufacturingFacilities);
                }
            }
            ViewState["MF"] = dtManufacturingFacilities;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    private void BindGridEstimateQuantity()
    {
        DataTable DtGridEstimate = new DataTable();
        DtGridEstimate = Lo.RetriveSaveEstimateGrid("Select", 0, hfprodrefno.Value, 0, "", "", "", "", "F");
        if (DtGridEstimate.Rows.Count > 0)
        {
            GvEstimateQuanPriceEdit.DataSource = DtGridEstimate;
            GvEstimateQuanPriceEdit.DataBind();
            GvEstimateQuanPriceEdit.Visible = true;
            GvEstiateQuanPrice.Visible = false;
        }
        else
        {
            GvEstimateQuanPriceEdit.Visible = false;
            if (btnsubmitpanel1.Text == "Update")
            {
                GvEstiateQuanPrice.Visible = true;
                SetInitialRowEstimateQuanPrice();
            }
        }
    }
    protected void GvEstimateQuanPriceEdit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "addnewmfe")
        {
            lblsub2.Text = "Submit";
            txtestimateyearu.SelectedIndex = -1;
            txtestimatequanu.Text = "";
            ddlestimateunitu.SelectedIndex = -1;
            txtestimatepriceu.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divbank", "showPopup1();", true);
        }
        else if (e.CommandName == "updatenewmfe")
        {
            lblsub2.Text = "Edit & Update";
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GvEstimateQuanPriceEdit.Rows[rowIndex];
            HiddenField hfn = (HiddenField)GvEstimateQuanPriceEdit.Rows[rowIndex].FindControl("HiddenField1");
            txtestimateyearu.SelectedValue = row.Cells[1].Text;
            txtestimatequanu.Text = row.Cells[2].Text;
            ddlestimateunitu.SelectedValue = row.Cells[3].Text;
            txtestimatepriceu.Text = row.Cells[4].Text;
            ViewState["editaccount"] = hfn.Value;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divbank", "showPopup1();", true);
        }
        else if (e.CommandName == "deletenewmfe")
        {
            int EstimateIdDel = Convert.ToInt32(e.CommandArgument.ToString());
            Lo.RetriveSaveEstimateGrid("Delete", EstimateIdDel, "", 0, "", "", "", "", "");
            if (EstimateIdDel != 0)
            {
                BindGridEstimateQuantity();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record deleted successfull.')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not deleted.')", true);
            }
        }
    }
    protected void lblsub2_Click(object sender, EventArgs e)
    {
        try
        {
            if (lblsub2.Text == "Edit & Update")
            {
                if (txtestimateyearu.SelectedValue != "-1" && ddlestimateunitu.SelectedItem.Text != "Select")
                {
                    Lo.RetriveSaveEstimateGrid("Update", Convert.ToInt32(ViewState["editaccount"]), hfprodrefno.Value, Convert.ToInt32(txtestimateyearu.SelectedItem.Value), txtestimateyearu.SelectedItem.Text, txtestimatequanu.Text, ddlestimateunitu.SelectedItem.Text, txtestimatepriceu.Text, "F");
                    txtestimateyearu.SelectedValue = "Select";
                    txtestimatepriceu.Text = "";
                    ddlestimateunitu.SelectedValue = "Select";
                    txtestimatequanu.Text = "";
                    ViewState["editaccount"] = null;
                    BindGridEstimateQuantity();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record update successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not update successfully')", true);
                }
            }
            else if (lblsub2.Text == "Submit")
            {
                string EstimateYear = txtestimateyearu.SelectedItem.Text;
                string EsitmateYearId = txtestimateyearu.SelectedItem.Value;
                string EstimateQuantity = txtestimatequanu.Text;
                string EstimateMeasuring = ddlestimateunitu.SelectedItem.Text;
                string UnitProd = txtestimatepriceu.Text;
                if (EstimateYear.ToString() != "Select" && EstimateMeasuring.ToString() != "Select")
                {
                    Lo.RetriveSaveEstimateGrid("Insert", 0, hfprodrefno.Value, Convert.ToInt16(EsitmateYearId), EstimateYear, EstimateQuantity, EstimateMeasuring, UnitProd, "F");
                    txtestimateyearu.SelectedIndex = 0;
                    txtestimatequanu.Text = "";
                    ddlestimateunitu.SelectedIndex = 0;
                    txtestimatepriceu.Text = "";
                    BindGridEstimateQuantity();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record save successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not save successfully')", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not save successfully')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    #endregion
    #region Add Grid of EstimateQuanPriceOld
    protected DataTable Dvinsert()
    {
        DataTable insert = new DataTable();
        insert.Columns.Add(new DataColumn("Year", typeof(string)));
        insert.Columns.Add(new DataColumn("FYear", typeof(string)));
        insert.Columns.Add(new DataColumn("EstimatedQty", typeof(string)));
        insert.Columns.Add(new DataColumn("Unit", typeof(string)));
        insert.Columns.Add(new DataColumn("EstimatedPrice", typeof(string)));
        insert.Columns.Add(new DataColumn("Type", typeof(string)));
        DataRow dr;
        if (ddlyearestimate1.SelectedItem.Text != "Select")
        {
            dr = insert.NewRow();
            dr["Year"] = ddlyearestimate1.SelectedItem.Value;
            dr["FYear"] = ddlyearestimate1.SelectedItem.Text;
            dr["EstimatedQty"] = txtestquan1.Text;
            dr["Unit"] = ddlunit1.SelectedItem.Text;
            dr["EstimatedPrice"] = txtpriceestimate1.Text;
            dr["Type"] = "O";
            insert.Rows.Add(dr);
        }
        if (ddlyearestimate2.SelectedItem.Text != "Select")
        {
            dr = insert.NewRow();
            dr["Year"] = ddlyearestimate2.SelectedItem.Value;
            dr["FYear"] = ddlyearestimate2.SelectedItem.Text;
            dr["EstimatedQty"] = txtestquan2.Text;
            dr["Unit"] = ddlunit2.SelectedItem.Text;
            dr["EstimatedPrice"] = txtpriceestimate2.Text;
            dr["Type"] = "O";
            insert.Rows.Add(dr);
        }
        if (ddlyearestimate3.Text != "Select")
        {
            dr = insert.NewRow();
            dr["Year"] = ddlyearestimate3.SelectedItem.Value;
            dr["FYear"] = ddlyearestimate3.SelectedItem.Text;
            dr["EstimatedQty"] = txtestquan3.Text;
            dr["Unit"] = ddlunit3.SelectedItem.Text;
            dr["EstimatedPrice"] = txtpriceestimate3.Text;
            dr["Type"] = "O";
            insert.Rows.Add(dr);
        }
        return insert;
    }
    private void BindGridEstimateQuantity1()
    {
        DataTable DtGridEstimate1 = new DataTable();
        DtGridEstimate1 = Lo.RetriveSaveEstimateGrid("Select", 0, hfprodrefno.Value, 0, "", "", "", "", "O");
        if (DtGridEstimate1.Rows.Count > 0)
        {
            GridView3.DataSource = DtGridEstimate1;
            GridView3.DataBind();
            GridView3.Visible = true;
            xxesti.Visible = false;
        }
        else
        {
            GridView3.Visible = false;
            xxesti.Visible = true;
        }
    }
    protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "addnewmfe1")
        {
            lblsub3.Text = "Submit";
            DropDownList3.SelectedIndex = -1;
            TextBox3.Text = "";
            DropDownList4.SelectedIndex = -1;
            TextBox4.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divbank2", "showPopup2();", true);
        }
        else if (e.CommandName == "updatenewmfe1")
        {
            lblsub3.Text = "Edit & Update";
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GridView3.Rows[rowIndex];
            HiddenField hfn = (HiddenField)GridView3.Rows[rowIndex].FindControl("HiddenField3");
            txtestimateyearu.SelectedValue = row.Cells[1].Text;
            txtestimatequanu.Text = row.Cells[2].Text;
            ddlestimateunitu.SelectedValue = row.Cells[3].Text;
            txtestimatepriceu.Text = row.Cells[4].Text;
            ViewState["editaccount1"] = hfn.Value;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divbank2", "showPopup2();", true);
        }
        else if (e.CommandName == "deletenewmfe1")
        {
            int EstimateIdDel = Convert.ToInt32(e.CommandArgument.ToString());
            Lo.RetriveSaveEstimateGrid("Delete", EstimateIdDel, "", 0, "", "", "", "", "");
            if (EstimateIdDel != 0)
            {
                BindGridEstimateQuantity1();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record deleted successfull.')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not deleted.')", true);
            }
        }
    }
    protected void lblsub3_Click(object sender, EventArgs e)
    {
        try
        {
            if (lblsub3.Text == "Edit & Update")
            {
                if (DropDownList3.SelectedItem.Text != "Select" && DropDownList4.SelectedItem.Text != "Select")
                {
                    Lo.RetriveSaveEstimateGrid("Update", Convert.ToInt32(ViewState["editaccount1"]), hfprodrefno.Value, Convert.ToInt32(DropDownList3.SelectedItem.Value), DropDownList3.SelectedItem.Text, TextBox3.Text, DropDownList4.SelectedItem.Text, TextBox4.Text, "O");
                    DropDownList3.SelectedValue = "Select";
                    TextBox3.Text = "";
                    DropDownList4.SelectedValue = "Select";
                    TextBox4.Text = "";
                    ViewState["editaccount1"] = null;
                    BindGridEstimateQuantity1();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record update successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not update successfully,All field fill mandatory')", true);
                }
            }
            else if (lblsub3.Text == "Submit")
            {
                string EstimateYear = DropDownList3.SelectedItem.Text;
                string EsitmateYearId = DropDownList3.SelectedItem.Value;
                string EstimateQuantity = TextBox3.Text;
                string EstimateMeasuring = DropDownList4.SelectedItem.Text;
                string UnitProd = TextBox4.Text;
                if (EstimateYear.ToString() != "Select" && EstimateMeasuring.ToString() != "Select")
                {
                    Lo.RetriveSaveEstimateGrid("Insert", 0, hfprodrefno.Value, Convert.ToInt16(EsitmateYearId), EstimateYear, EstimateQuantity, EstimateMeasuring, UnitProd, "O");
                    DropDownList3.SelectedIndex = 0;
                    TextBox3.Text = "";
                    DropDownList4.SelectedIndex = 0;
                    TextBox4.Text = "";
                    BindGridEstimateQuantity1();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record save successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not save,All field fill mandatory')", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not save successfully')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    #endregion
    #region Show NSNGroup GRID TO Add
    protected void lblviewitemcode_Click(object sender, EventArgs e)
    {
        if (btnsubmitpanel1.Text == "Update")
        {
            DataTable DtViewNewCodeup = Lo.RetrivenewcategortFIIG_No(hfprodrefno.Value, "UFound");
            if (DtViewNewCodeup.Rows.Count > 0)
            {
                DataView Dv = new DataView(DtViewNewCodeup);
                Dv.RowFilter = "Remarks3='Y'";
                DataTable DtVY = Dv.ToTable();
                gvproditemdetail.DataSource = DtVY.DefaultView;
                gvproditemdetail.DataBind();
                gvproditemdetail.Visible = true;
                for (int i = 0; DtVY.Rows.Count > i; i++)
                {
                    txtfiigtype.Text = DtVY.Rows[i]["Remarks2"].ToString();
                    TextBox box = (TextBox)gvproditemdetail.Rows[i].Cells[2].FindControl("txtinfonsnfig");
                    box.Text = DtVY.Rows[i]["Remarks"].ToString();
                }
                pan.Visible = true;
                Panel2.Visible = true;
                Dv.RowFilter = "Remarks3='N'";
                DataTable DTVN = Dv.ToTable();
                GridView1.DataSource = DTVN.DefaultView;
                GridView1.DataBind();
                for (int i = 0; DTVN.Rows.Count > i; i++)
                {
                    txtfiigno.Text = DTVN.Rows[i]["Remarks2"].ToString();
                    TextBox box1 = (TextBox)GridView1.Rows[i].Cells[2].FindControl("txtremNoFiig");
                    box1.Text = DTVN.Rows[i]["Remarks"].ToString();
                }
                GridView1.Visible = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "changePass", "showPopup();", true);
            }
            else
            {
                DataTable DtViewNewCode = Lo.RetrivenewcategortFIIG_No(ddllevel3product.SelectedItem.Text, "Found");
                if (DtViewNewCode.Rows.Count > 0)
                {
                    DataView Dv = new DataView(DtViewNewCode);
                    Dv.RowFilter = "Type='Y'";
                    DataTable DtVY = Dv.ToTable();
                    gvproditemdetail.DataSource = DtVY.DefaultView;
                    gvproditemdetail.DataBind();
                    gvproditemdetail.Visible = true;
                    pan.Visible = true;
                    Panel2.Visible = true;
                    Dv.RowFilter = "Type='N'";
                    DataTable DTVN = Dv.ToTable();
                    GridView1.DataSource = DTVN.DefaultView;
                    GridView1.DataBind();
                    GridView1.Visible = true;
                    ScriptManager.RegisterStartupScript(this, GetType(), "changePass", "showPopup();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('No Record Found')", true);
                }
            }
        }
        else
        {
            DataTable DtViewNewCode = Lo.RetrivenewcategortFIIG_No(ddllevel3product.SelectedItem.Text, "Found");
            if (DtViewNewCode.Rows.Count > 0)
            {
                DataView Dv = new DataView(DtViewNewCode);
                Dv.RowFilter = "Type='Y'";
                DataTable DtVY = Dv.ToTable();
                gvproditemdetail.DataSource = DtVY.DefaultView;
                gvproditemdetail.DataBind();
                gvproditemdetail.Visible = true;
                pan.Visible = true;
                Panel2.Visible = true;
                Dv.RowFilter = "Type='N'";
                DataTable DTVN = Dv.ToTable();
                GridView1.DataSource = DTVN.DefaultView;
                GridView1.DataBind();
                GridView1.Visible = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "changePass", "showPopup();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('No Record Found')", true);
            }
        }
    }
    protected void gvproductItem_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected DataTable SaveFiigNo()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("FiigID", typeof(long)));
        dt.Columns.Add(new DataColumn("ProductRefNo", typeof(string)));
        dt.Columns.Add(new DataColumn("SCategoryName", typeof(string)));
        dt.Columns.Add(new DataColumn("Remarks", typeof(string)));
        dt.Columns.Add(new DataColumn("Remarks2", typeof(string)));
        dt.Columns.Add(new DataColumn("Remarks3", typeof(string)));
        DataRow dr;
        {
            try
            {
                foreach (GridViewRow row in gvproditemdetail.Rows)
                {
                    string scatfig = row.Cells[1].Text;
                    TextBox tb = (TextBox)row.FindControl("txtinfonsnfig");
                    dr = dt.NewRow();
                    dr["FiigID"] = "-1";
                    dr["ProductRefNo"] = "";
                    dr["SCategoryName"] = scatfig.ToString();
                    dr["Remarks"] = tb.Text;
                    dr["Remarks2"] = txtfiigtype.Text;
                    dr["Remarks3"] = "Y";
                    dt.Rows.Add(dr);
                }
                foreach (GridViewRow row1 in GridView1.Rows)
                {
                    string scatfig1 = row1.Cells[1].Text;
                    TextBox tb1 = (TextBox)row1.FindControl("txtremNoFiig");
                    dr = dt.NewRow();
                    dr["FiigID"] = "-1";
                    dr["ProductRefNo"] = "";
                    dr["SCategoryName"] = scatfig1.ToString();
                    dr["Remarks"] = tb1.Text;
                    dr["Remarks2"] = txtfiigno.Text; ;
                    dr["Remarks3"] = "N";
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dt;
        }
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
                ddlmastercategory.SelectedValue = DtView.Rows[0]["ProductLevel1"].ToString();
                BindMasterSubCategory();
                ddlsubcategory.SelectedValue = DtView.Rows[0]["ProductLevel2"].ToString();
                if (DtView.Rows[0]["ProductLevel3"].ToString() != "")
                {
                    BindMaster3levelSubCategory();
                    ddllevel3product.SelectedValue = DtView.Rows[0]["ProductLevel3"].ToString();
                }
                lblviewitemcode.Visible = true;
                txtnsccode.Text = DtView.Rows[0]["NSCCode"].ToString();
                txtniincode.Text = DtView.Rows[0]["NIINCode"].ToString();
                txtproductdescription.Text = DtView.Rows[0]["ProductDescription"].ToString();
                txtoempartnumber.Text = DtView.Rows[0]["OEMPartNumber"].ToString();
                txtoemname.Text = DtView.Rows[0]["OEMName"].ToString();
                if (DtView.Rows[0]["OEMCountry"].ToString() != "")
                {
                    txtcountry.SelectedValue = DtView.Rows[0]["OEMCountry"].ToString();
                }
                txtdpsupartnumber.Text = DtView.Rows[0]["DPSUPartNumber"].ToString();
                txthsncodereadonly.Text = DtView.Rows[0]["HsnCode8digit"].ToString();

                if (DtView.Rows[0]["EndUser"].ToString() != "")
                {
                    DataTable dtEndUseredit = Lo.RetriveProductCode("", hfprodrefno.Value, "EndUser", hidType.Value);
                    if (dtEndUseredit.Rows.Count > 0)
                    {
                        for (int i = 0; dtEndUseredit.Rows.Count > i; i++)
                        {
                            foreach (ListItem lie in ddlenduser.Items)
                            {
                                if (lie.Value == dtEndUseredit.Rows[i]["SCategoryId"].ToString())
                                {
                                    lie.Selected = true;
                                }
                            }
                        }

                    }
                }
                if (DtView.Rows[0]["Platform"].ToString() != "")
                {
                    ddlplatform.Items.FindByValue(DtView.Rows[0]["Platform"].ToString()).Selected = true;
                    if (DtView.Rows[0]["NomenclatureOfMainSystem"].ToString() != "")
                    {
                        BindMasterProductNoenCletureCategory();
                        ddlnomnclature.SelectedValue = DtView.Rows[0]["NomenclatureOfMainSystem"].ToString();
                    }
                }
                if (DtView.Rows[0]["TechnologyLevel1"].ToString() != "")
                {
                    ddltechnologycat.Items.FindByValue(DtView.Rows[0]["TechnologyLevel1"].ToString()).Selected = true;
                    BindMasterSubCategoryTech();
                    if (DtView.Rows[0]["TechnologyLevel2"].ToString() != "")
                    {
                        ddlsubtech.Items.FindByValue(DtView.Rows[0]["TechnologyLevel2"].ToString()).Selected = true;
                    }
                }
                txtsearchkeyword.Text = DtView.Rows[0]["SearchKeyword"].ToString();
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
                rbproductImported.SelectedValue = DtView.Rows[0]["IsProductImported"].ToString();
                if (rbproductImported.SelectedItem.Value == "N")
                {
                    txtremarksyearofimportyes.Text = DtView.Rows[0]["YearofImportRemarks"].ToString();
                    divyearofimportYes.Visible = false;
                }
                else
                {
                    BindGridEstimateQuantity1();
                    divyearofimportYes.Visible = true;
                }
                DataTable dtPdfBind = Lo.RetriveProductCode("", hfprodrefno.Value, "ProductImage", "PDF");
                if (dtPdfBind.Rows.Count > 0)
                {
                    dlpdf.DataSource = dtPdfBind;
                    dlpdf.DataBind();
                    divPdf.Visible = true;
                }
                else
                {
                    divPdf.Visible = false;
                }
                DataTable dtImageBind = Lo.RetriveProductCode("", hfprodrefno.Value, "ProductImage", "Image");
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
                txtfeaturesanddetails.Text = DtView.Rows[0]["FeatursandDetail"].ToString();
                rbitemspecification.SelectedValue = DtView.Rows[0]["ItemSpecification"].ToString();
                BindGrid();
                DataTable DtPInfoEditBind = Lo.RetriveProductCode("", hfprodrefno.Value, "ProdInfoBindEdit", "");
                if (DtPInfoEditBind.Rows.Count > 0)
                {
                    gvProductInformation.DataSource = DtPInfoEditBind;
                    gvProductInformation.DataBind();
                }
                txtadditionalinfo.Text = DtView.Rows[0]["AdditionalDetail"].ToString();
                BindGridEstimateQuantity();
                if (DtView.Rows[0]["PurposeofProcurement"].ToString() != "")
                {
                    DataTable DTporCat = Lo.RetriveProductCode("", hfprodrefno.Value, "ProductPOP", hidType.Value);
                    if (DTporCat.Rows.Count > 0)
                    {
                        for (int i = 0; DTporCat.Rows.Count > i; i++)
                        {
                            foreach (ListItem rw in rbIgCategory.Items)
                            {
                                if (rw.Value == DTporCat.Rows[i]["SCategoryId"].ToString())
                                {
                                    rbIgCategory.SelectedValue = DTporCat.Rows[i]["SCategoryId"].ToString();
                                }
                            }
                        }
                    }
                }
                txtremarksprocurmentCategory.Text = DtView.Rows[0]["ProcurmentCategoryRemark"].ToString();
                rbeoimake2.SelectedValue = DtView.Rows[0]["EOIStatus"].ToString();
                txteoilink.Text = DtView.Rows[0]["EOIURL"].ToString();
                ddlNodalOfficerEmail.SelectedValue = DtView.Rows[0]["NodelDetail"].ToString();
                if (ddlNodalOfficerEmail.SelectedValue != null)
                { BindNodelEmail1(); }
                else
                { contactpanel1.Visible = false; }
                ddlNodalOfficerEmail2.SelectedValue = DtView.Rows[0]["NodalDetail2"].ToString();
                if (ddlNodalOfficerEmail2.SelectedValue != null)
                { BindNodal2(); }
                else
                { contactpanel2.Visible = false; }
                rbeligible.SelectedValue = DtView.Rows[0]["IsShowGeneral"].ToString();
                foreach (ListItem chki in chklistdeclarationimage.Items)
                {
                    chki.Selected = true;
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
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Record not deleted.')", true);
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Record not deleted.')", true);
            }
        }
    }
    protected void dlpdf_OnRowCommand(object source, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "removepdf")
        {
            try
            {
                string DeleteRec = Lo.DeleteRecord(e.CommandArgument.ToString(), "InActivePdf");
                if (DeleteRec == "true")
                {
                    DataTable dtImageBind = Lo.RetriveProductCode("", hfprodrefno.Value, "ProductImage", hidType.Value);
                    if (dtImageBind.Rows.Count > 0)
                    {
                        dlpdf.DataSource = dtImageBind;
                        dlpdf.DataBind();
                        divPdf.Visible = true;
                    }
                    else
                    {
                        divPdf.Visible = false;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Record not deleted.')", true);
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Record not deleted.')", true);
            }
        }
    }
    #endregion
}