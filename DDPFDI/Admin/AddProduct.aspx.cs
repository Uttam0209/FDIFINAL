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
    public DataTable dtSaveProdInfo = new DataTable();
    public DataTable dtSaveEstimateQuantity = new DataTable();
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
                            BindGridEstimateQuantity();
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
                                BindMasterTechnologyCategory();
                                BindMasterPlatCategory();
                                BindPurposeProcuremnt();
                                BindEndUser();
                                BindNodelEmail();
                                HSNCodeLevel();
                            }
                            else
                            {
                                BindMasterCategory();
                                BindMasterTechnologyCategory();
                                BindMasterPlatCategory();
                                BindPurposeProcuremnt();
                                BindEndUser();
                                HSNCodeLevel();
                            }
                        }
                        else
                        {
                            BindCompany();
                            BindCountry();
                            BindFinancialYear();
                            IsProductImported();
                            HSNCodeLevel();
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
    private void HsHeadingNo(string NSNGroupddl)
    {
        try
        {
            string a = NSNGroupddl.Substring((NSNGroupddl.IndexOf("(") + 1), NSNGroupddl.IndexOf(")") - (NSNGroupddl.IndexOf("(") + 1));
            txthscodereadonly.Text = a;
        }
        catch (Exception)
        {
            txthscodereadonly.Text = "";
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
    protected void ddlhschapter_SelectedIndexChanged(object sender, EventArgs e)
    {
        HSNCodeLevel1();
    }
    protected void ddlhsncodelev1_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMasterHSNLevel2();
        HsHeadingNo(ddlhsncodelev1.SelectedItem.Text);
    }
    protected void ddlhsncodelevel2_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMasterHSNLevel3();
    }
    protected void ddlhsncodelevel3_SelectedIndexChanged(object sender, EventArgs e)
    {
        Nsncode8digit(ddlhsncodelevel3.SelectedItem.Text);
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
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlmastercategory.SelectedItem.Value), "", "", "SubSelectID", "", "");
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
            DataTable DtMasterCategroyLevel3 = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlsubcategory.SelectedItem.Value), "", "", "SubSelectID", "", "");
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
        }
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
            gvprocurmentcategory.DataSource = DtPurposeProcuremnt;
            gvprocurmentcategory.DataBind();
        }
        else
        {
            DtPurposeProcuremnt = Lo.RetriveMasterSubCategoryDate(0, "PROCURMENT CATEGORY", "", "SelectProductCat", "", "");
            gvprocurmentcategory.DataSource = DtPurposeProcuremnt;
            gvprocurmentcategory.DataBind();
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
    #region for HS Code
    public void HSCode()
    {
        DataTable DtMasterHS = Lo.RetriveMasterSubCategoryDate(0, "HS Code", "", "SelectInnerHSMaster", "", "");

        if (DtMasterHS.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlHSNCode, DtMasterHS, "SCategoryName", "SCategoryID");
            ddlHSNCode.Items.Insert(0, "Select");
        }
    }
    #endregion
    #region for HSN Code
    public void HSNCodeLevel()
    {
        DataTable DtMasterHSN = Lo.RetriveMasterSubCategoryDate(0, "HSN CODE", "", "SelectInnerHSMaster", "", "");
        if (DtMasterHSN.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlhschapter, DtMasterHSN, "SCategoryName", "SCategoryID");
            ddlhschapter.Items.Insert(0, "Select");
        }
    }
    public void HSNCodeLevel1()
    {
        DataTable DtMasterHSN = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlhschapter.SelectedItem.Value), "", "", "SubSelectID", "", "");
        if (DtMasterHSN.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlhsncodelev1, DtMasterHSN, "SCategoryName", "SCategoryID");
            ddlhsncodelev1.Items.Insert(0, "Select");
        }
    }
    protected void BindMasterHSNLevel2()
    {
        DataTable DtMasterHSNLev2 = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlhsncodelev1.SelectedItem.Value), "", "", "SubSelectID", "", "");
        if (DtMasterHSNLev2.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlhsncodelevel2, DtMasterHSNLev2, "SCategoryName", "SCategoryId");
            ddlhsncodelevel2.Items.Insert(0, "Select");
        }
        else
        {
            ddlhsncodelevel2.Items.Clear();
            ddlhsncodelevel2.Items.Insert(0, "Select");
            txthscodereadonly.Text = "";
        }
    }
    protected void BindMasterHSNLevel3()
    {
        if (ddlhsncodelevel2.SelectedItem.Value != null || ddlhsncodelevel2.SelectedItem.Text != "Select")
        {
            DataTable DtMasterHSNLevel3 = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlhsncodelevel2.SelectedItem.Value), "", "", "SubSelectID", "", "");
            if (DtMasterHSNLevel3.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlhsncodelevel3, DtMasterHSNLevel3, "SCategoryName", "SCategoryId");
                ddlhsncodelevel3.Items.Insert(0, "Select");
            }
            else
            {
                ddlhsncodelevel3.Items.Clear();
                ddlhsncodelevel3.Items.Insert(0, "Select");
                ddlhsncodelevel3.Items.Insert(1, "NA");
            }
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
            if (ddlHSNCode.Text != "")
            {
                HyPanel1["HSCode"] = Co.RSQandSQLInjection(ddlHSNCode.SelectedItem.Value, "soft");
            }
            else
            {
                HyPanel1["HSCode"] = null;
            }
            HyPanel1["HSNCode"] = Co.RSQandSQLInjection(txthsncode.Text.Trim(), "soft");
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
            if (ddltechlevel3.SelectedItem.Value != "Select")
            {
                HyPanel1["TechnologyLevel3"] = Co.RSQandSQLInjection(ddltechlevel3.SelectedItem.Value, "soft");
            }
            else
            {
                HyPanel1["TechnologyLevel3"] = null;
            }
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
                HyPanel1["YearofImport"] = Co.RSQandSQLInjection("All (Expect Last Five Year)", "soft");
                HyPanel1["YearofImportRemarks"] = Co.RSQandSQLInjection(txtremarksyearofimportyes.Text.Trim(), "soft");
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
            HyPanel1["FeatursandDetail"] = Co.RSQandSQLInjection(txtfeaturesanddetails.Text.Trim(), "soft");
            HyPanel1["ItemSpecification"] = Co.RSQandSQLInjection(rbitemspecification.SelectedValue, "soft");
            if (gvProductInformation.Rows.Count != 0)
            {
                dtSaveProdInfo = SaveCodeProdInfo();
            }
            HyPanel1["AdditionalDetail"] = Co.RSQandSQLInjection(txtadditionalinfo.Text.Trim(), "soft");
            if (GvEstimateQuanPrice.Rows.Count != 0)
            {
                dtSaveEstimateQuantity = SaveCodeEstimateQuantity();
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
            if (ProcurmentCat.ToString() != "")
            {
                HyPanel1["TenderSubmition"] = Co.RSQandSQLInjection(ddlteneoi.SelectedItem.Text, "soft");
                HyPanel1["TenderStatus"] = Co.RSQandSQLInjection(ddlstatus.SelectedItem.Value, "soft");
                if (txtdate.Text != "")
                {
                    DateTime Datetendor = Convert.ToDateTime(txtdate.Text);
                    string FinalDate = Datetendor.ToString("dd-MMM-yyyy");
                    HyPanel1["TenderFillDate"] = Co.RSQandSQLInjection(FinalDate.ToString(), "soft");
                }
                else
                {
                    HyPanel1["TenderFillDate"] = null;
                }
                HyPanel1["TenderUrl"] = Co.RSQandSQLInjection(txtendorurl.Text, "soft");

            }
            else
            {
                HyPanel1["TenderSubmition"] = "";
                HyPanel1["TenderStatus"] = "";
                HyPanel1["TenderFillDate"] = null;
                HyPanel1["TenderUrl"] = "";
            }
            HyPanel1["EOIStatus"] = "";
            HyPanel1["EOISubmition"] = "";
            HyPanel1["EOIFillDate"] = null;
            HyPanel1["EOIURL"] = "";
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
            HyPanel1["CreatedBy"] = ViewState["UserLoginEmail"].ToString();
            string StrProductDescription = Lo.SaveCodeProduct(HyPanel1, dtImage, dtSaveProdInfo, dtSaveEstimateQuantity, DtNSFIIG, out _sysMsg, out _msg, "Product");
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
            if (txtproductdescription.Text != "" && ddlmastercategory.SelectedItem.Text != "Select" && ddlsubcategory.SelectedItem.Text != "Select" && ddltechnologycat.SelectedItem.Text != "Select" && txtsearchkeyword.Text != "" && ddlcompany.SelectedItem.Text != "Select")
            {
                if (ddlsubtech.SelectedItem.Text != "Select" && ddlnomnclature.SelectedItem.Text != "Select" && ddlenduser.SelectedItem.Text != "Select" && ddlplatform.SelectedItem.Text != "Select")
                {
                    if (txtcountry.SelectedItem.Text != "Select")
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
        txtenduserpartnumber.Text = "";
        txthsncode.Text = "";
        lblfuitemdescriptionfile.Text = "";
        dlimage.DataSource = null;
        dlimage.DataBind();
        divimgdel.Visible = false;
        ddlnomnclature.SelectedIndex = 0;
        txthscodereadonly.Text = "";
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
        txtestimatePriceLLp.Text = "";
        txtestimateQuantity.Text = "";
        ddlMeasuringUnit.SelectedIndex = 0;
        ddlYearEstimate.SelectedIndex = 0;
        contactpanel1.Visible = false;
        divnodal2.Visible = false;
        if (ddlNodalOfficerEmail.Text != "")
        {
            ddlNodalOfficerEmail.SelectedIndex = 0;
        }
        foreach (ListItem chk in chklistimportyearfive.Items)
        {
            if (chk.Selected == true)
            {
                chk.Selected = false;
            }
        }
        foreach (GridViewRow gvPofPROCURMENT in gvprocurmentcategory.Rows)
        {
            CheckBox chkBxProc = (CheckBox)gvPofPROCURMENT.FindControl("chkprocurmentcategory");
            HiddenField hfProcCatId = (HiddenField)gvPofPROCURMENT.FindControl("hfproccateid");
            if (chkBxProc != null && chkBxProc.Checked)
            {
                chkBxProc.Checked = false;
                hfProcCatId.Value = "";
            }
        }
        txtfiigno.Text = "";
        txtfiigtype.Text = "";
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Invalid file format only pdf allowd.')", true);
            }
        }
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
    #region Gridview Initial GvEstimateQuanPrice
    protected DataTable SaveCodeEstimateQuantity()
    {
        DataTable DNSaveEstimateQuen = new DataTable();
        DNSaveEstimateQuen.Columns.Add(new DataColumn("ProdQtyPriceId", typeof(int)));
        DNSaveEstimateQuen.Columns.Add(new DataColumn("Year", typeof(int)));
        DNSaveEstimateQuen.Columns.Add(new DataColumn("FYear", typeof(string)));
        DNSaveEstimateQuen.Columns.Add(new DataColumn("EstimatedQty", typeof(string)));
        DNSaveEstimateQuen.Columns.Add(new DataColumn("Unit", typeof(string)));
        DNSaveEstimateQuen.Columns.Add(new DataColumn("EstimatedPrice", typeof(string)));
        DataRow drEstimateQuantitySave;
        {
            try
            {
                for (int i = 0; i <= GvEstimateQuanPrice.Rows.Count; i++)
                {
                    Label ddlestimatequanYear = (Label)GvEstimateQuanPrice.Rows[i].Cells[1].FindControl("lblYear");
                    Label txtEstimateQuantity = (Label)GvEstimateQuanPrice.Rows[i].Cells[2].FindControl("lblestimateQuantityGrid");
                    Label ddlMeasurUnit = (Label)GvEstimateQuanPrice.Rows[i].Cells[3].FindControl("lblMeasuringUnitGrid");
                    Label txtestimPrice = (Label)GvEstimateQuanPrice.Rows[i].Cells[4].FindControl("lblestimatePriceLLpGrid");
                    drEstimateQuantitySave = DNSaveEstimateQuen.NewRow();
                    drEstimateQuantitySave["ProdQtyPriceId"] = GvEstimateQuanPrice.DataKeys[i].Value.ToString();
                    if (ddlestimatequanYear.Text == "2019-20")
                    { drEstimateQuantitySave["Year"] = "1"; }
                    if (ddlestimatequanYear.Text == "2019-21")
                    { drEstimateQuantitySave["Year"] = "2"; }
                    if (ddlestimatequanYear.Text == "2019-22")
                    { drEstimateQuantitySave["Year"] = "3"; }
                    if (ddlestimatequanYear.Text == "2019-23")
                    { drEstimateQuantitySave["Year"] = "4"; }
                    if (ddlestimatequanYear.Text == "2019-24")
                    { drEstimateQuantitySave["Year"] = "5"; }
                    drEstimateQuantitySave["FYear"] = ddlestimatequanYear.Text.Trim();
                    drEstimateQuantitySave["EstimatedQty"] = txtEstimateQuantity.Text.Trim();
                    drEstimateQuantitySave["Unit"] = ddlMeasurUnit.Text.Trim();
                    drEstimateQuantitySave["EstimatedPrice"] = txtestimPrice.Text.Trim();
                    DNSaveEstimateQuen.Rows.Add(drEstimateQuantitySave);
                }
            }
            catch (Exception)
            { }
            return DNSaveEstimateQuen;
        }
    }
    private void BindGridEstimateQuantity()
    {
        DataTable DtGridEstimate = new DataTable();
        DtGridEstimate = Lo.RetriveSaveEstimateGrid("Select", 0, hfprodrefno.Value, 0, "", "", "", "");
        if (DtGridEstimate.Rows.Count > 0)
        {
            GvEstimateQuanPrice.DataSource = DtGridEstimate;
            GvEstimateQuanPrice.DataBind();
            GvEstimateQuanPrice.Visible = true;
        }
        else
        {
            GvEstimateQuanPrice.Visible = false;
        }
    }
    protected void btnAddEstimate_Click(object sender, EventArgs e)
    {
        string EstimateYear = ddlYearEstimate.SelectedItem.Text;
        int EsitmateYearId = Convert.ToInt32(ddlYearEstimate.SelectedItem.Value);
        string EstimateQuantity = txtestimateQuantity.Text;
        string EstimateMeasuring = ddlMeasuringUnit.SelectedItem.Text;
        string UnitProd = "0";
        Lo.RetriveSaveEstimateGrid("Insert", 0, hfprodrefno.Value, EsitmateYearId, EstimateYear, EstimateQuantity, EstimateMeasuring, UnitProd);
        ddlYearEstimate.SelectedIndex = 0;
        txtestimateQuantity.Text = "";
        ddlMeasuringUnit.SelectedIndex = 0;
        txtestimatePriceLLp.Text = "";
        BindGridEstimateQuantity();
    }
    protected void GvEstimateQuanPrice_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GvEstimateQuanPrice.EditIndex = e.NewEditIndex;
        BindGridEstimateQuantity();
    }
    protected void GvEstimateQuanPrice_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = GvEstimateQuanPrice.Rows[e.RowIndex];
        int ProdSpeciId = Convert.ToInt32(GvEstimateQuanPrice.DataKeys[e.RowIndex].Values[0]);
        string EditEstimateYear = (row.FindControl("ddlYearEstimateGrid") as DropDownList).SelectedItem.Text;
        int EditEstimateYearId = Convert.ToInt32((row.FindControl("ddlYearEstimateGrid") as DropDownList).SelectedItem.Value);
        string EditEstimateQuan = (row.FindControl("txtEstimateQuantityGrid") as TextBox).Text;
        string EditEstimateUnit = (row.FindControl("ddlEstimateUnit") as DropDownList).SelectedItem.Text;
        string EditEstimatePrice = "0";//Convert.ToDecimal((row.FindControl("txtEstimatePriceLLpGrid") as TextBox).Text);
        Lo.RetriveSaveEstimateGrid("Update", ProdSpeciId, hfprodrefno.Value, EditEstimateYearId, EditEstimateYear, EditEstimateQuan, EditEstimateUnit, EditEstimatePrice);
        GvEstimateQuanPrice.EditIndex = -1;
        BindGridEstimateQuantity();
    }
    protected void GvEstimateQuanPrice_RowCancelingEdit(object sender, EventArgs e)
    {
        GvEstimateQuanPrice.EditIndex = -1;
        BindGridEstimateQuantity();
    }
    protected void GvEstimateQuanPrice_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int EstimateIdDel = Convert.ToInt32(GvEstimateQuanPrice.DataKeys[e.RowIndex].Values[0]);
        Lo.RetriveSaveEstimateGrid("Delete", EstimateIdDel, "", 0, "", "", "", "");
        BindGridEstimateQuantity();
    }
    protected void GvEstimateQuanPrice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != GvEstimateQuanPrice.EditIndex)
        {
            (e.Row.Cells[5].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";
        }
    }
    protected void GvEstimateQuanPrice_Paging(object sender, GridViewPageEventArgs e)
    {
        GvEstimateQuanPrice.PageIndex = e.NewPageIndex;
        BindGridEstimateQuantity();
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
                if (DtView.Rows[0]["ProductLevel1"].ToString() != "")
                {
                    ddlmastercategory.Items.FindByValue(DtView.Rows[0]["ProductLevel1"].ToString()).Selected = true;
                    BindMasterSubCategory();
                    ddlsubcategory.Items.FindByValue(DtView.Rows[0]["ProductLevel2"].ToString()).Selected = true;
                    BindMaster3levelSubCategory();
                    if (DtView.Rows[0]["ProductLevel3"].ToString() != "")
                    {
                        ddllevel3product.SelectedValue = DtView.Rows[0]["ProductLevel3"].ToString();
                        lblviewitemcode.Visible = true;
                    }
                }
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
                if (DtView.Rows[0]["HSCode"].ToString() != "")
                {
                    ddlHSNCode.SelectedValue = DtView.Rows[0]["HSCode"].ToString();
                }
                txthsncode.Text = DtView.Rows[0]["HSNCode"].ToString();
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
                    if (DtView.Rows[0]["TechnologyLevel2"].ToString() != "")
                    {
                        BindMasterSubCategoryTechLevel3();
                        if (DtView.Rows[0]["TechnologyLevel3"].ToString() != "")
                        {
                            ddltechlevel3.SelectedValue = DtView.Rows[0]["TechnologyLevel3"].ToString();
                        }
                    }
                    else
                    {
                        ddltechlevel3.Items.Clear();
                        ddltechlevel3.Items.Insert(0, "Select");
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
                    foreach (ListItem chk in chklistimportyearfive.Items)
                    {
                        chk.Selected = true;
                    }
                    txtremarksyearofimportyes.Text = DtView.Rows[0]["YearofImportRemarks"].ToString();
                    divyearofimportYes.Visible = true;
                }
                HyPanel1["ItemDescriptionPDFFile"] = DtView.Rows[0]["ItemDescriptionPDFFile"].ToString();

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
                DataTable DTporCat = Lo.RetriveProductCode("", hfprodrefno.Value, "ProductPOP", hidType.Value);
                if (DTporCat.Rows.Count > 0)
                {
                    for (int i = 0; DTporCat.Rows.Count > i; i++)
                    {
                        foreach (GridViewRow rw in gvprocurmentcategory.Rows)
                        {
                            CheckBox chkBxPOP = (CheckBox)rw.FindControl("chkprocurmentcategory");
                            HiddenField hfPOPid = (HiddenField)rw.FindControl("hfproccateid");
                            if (hfPOPid.Value == DTporCat.Rows[i]["SCategoryId"].ToString())
                            {
                                chkBxPOP.Checked = true;
                                if (chkBxPOP.Checked == true)
                                {
                                    divmake2status.Style["display"] = "";
                                    divmake2status.Visible = true;
                                    ddlteneoi.SelectedValue = DtView.Rows[0]["TenderSubmition"].ToString();
                                }
                            }
                        }
                    }
                }
                txtremarksprocurmentCategory.Text = DtView.Rows[0]["ProcurmentCategoryRemark"].ToString();
                ddlstatus.SelectedValue = DtView.Rows[0]["TenderStatus"].ToString();
                if (ddlstatus.SelectedValue == "Live")
                {
                    extimedatevisible.Style["display"] = "";
                    divtimedateurl.Style["display"] = "";
                    extimedatevisible.Visible = true;
                    if (DtView.Rows[0]["TenderFillDate"].ToString() != "")
                    {
                        DateTime Date = Convert.ToDateTime(DtView.Rows[0]["TenderFillDate"].ToString());
                        string nDate = Date.ToString("yy-MMM-dd");
                        txtdate.Text = nDate.ToString();
                    }
                    txtendorurl.Text = DtView.Rows[0]["TenderUrl"].ToString();
                }
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
}