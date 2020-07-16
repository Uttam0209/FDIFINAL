using BusinessLayer;
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
                        }
                        if (hidType.Value.ToString() != "SuperAdmin" || hidType.Value.ToString() != "Admin")
                        {
                            BindCompany();
                            BindCountry();
                            BindFinancialYear();
                            // IsProductImported();
                            if (Request.QueryString["mcurrentcompRefNo"] != null)
                            {
                                BindMasterCategory();
                                BindMasterTechnologyCategory();
                                BindMasterPlatCategory();
                                BindEndUser();
                                BindNodelEmail();
                                BindQualityAssurance();
                                PROCURMENTCATEGORYIndigenization();
                            }
                            else
                            {
                                BindMasterCategory();
                                BindMasterTechnologyCategory();
                                BindMasterPlatCategory();
                                BindEndUser();
                                BindQualityAssurance();
                                PROCURMENTCATEGORYIndigenization();
                            }
                        }
                        else
                        {
                            BindCompany();
                            BindCountry();
                            BindFinancialYear();
                            BindQualityAssurance();
                            PROCURMENTCATEGORYIndigenization();
                            // IsProductImported();
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
        //if (rbproductImported.SelectedItem.Value == "Y")
        //{
        // divyearofimportYes.Visible = true;
        //}
        //else if (rbproductImported.SelectedItem.Value == "N")
        //{
        //divyearofimportYes.Visible = false;
        //}
    }
    protected void IsProductImported()
    {
        //if (rbproductImported.SelectedItem.Value == "Y")
        //{
        //    divyearofimportYes.Visible = true;
        //}
        //else if (rbproductImported.SelectedItem.Value == "N")
        //{
        //    divyearofimportYes.Visible = false;
        //}
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
    protected void rbeoimake2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbeoimake2.SelectedValue == "Yes")
        { eoi.Visible = true; }
        else
        { eoi.Visible = false; }
    }
    protected void chkindiprocstart_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (chkindiprocstart.SelectedValue == "Yes")
        {
            indicatchk.Visible = true;
        }
        else
        {
            indicatchk.Visible = false;
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
            Co.FillCheckBox(ddlenduser, DtMasterCategroy, "SCategoryName", "SCategoryID");
        }
        else
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, lblenduser.Text, "", "SelectInnerMaster1", "", "");
            Co.FillCheckBox(ddlenduser, DtMasterCategroy, "SCategoryName", "SCategoryID");
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
    #region For Quality Assurance
    protected void BindQualityAssurance()
    {
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "QA AGENCY", "", "SelectProductCat", "", "");
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillCheckBox(chkQAA, DtMasterCategroy, "SCategoryName", "SCategoryID");
        }
    }
    #endregion
    #region For Indinization category proc cat
    protected void PROCURMENTCATEGORYIndigenization()
    {
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "PROCURMENT CATEGORY", "", "SelectProductCat", "", "");
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillCheckBox(rbIgCategory, DtMasterCategroy, "SCategoryName", "SCategoryID");
        }
    }
    #endregion
    #region PanelSaveCode
    private string m;
    string a, b, qa, ity = string.Empty;
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
            HyPanel1["OEMAddress"] = Co.RSQandSQLInjection(txtoemaddress.Text.Trim(), "soft");
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
                for (int o = 0; o < ddlenduser.Items.Count; o++)
                {
                    if (ddlenduser.Items[o].Selected == true)
                    {
                        m = m + ddlenduser.Items[o].Value + ",";
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
            dtSaveProdInfo = SaveCodeProdInfo();
            HyPanel1["AdditionalDetail"] = Co.RSQandSQLInjection(txtadditionalinfo.Text.Trim(), "soft");
            dtSaveEstimateQuantity = Dvinsertfuture();
            HyPanel1["ProcurmentCategoryRemark"] = Co.RSQandSQLInjection(txtremarksprocurmentCategory.Text.Trim(), "soft");
            HyPanel1["TenderSubmition"] = "";
            HyPanel1["TenderStatus"] = "";
            HyPanel1["TenderFillDate"] = null;
            HyPanel1["TenderUrl"] = "";
            HyPanel1["EOIStatus"] = Co.RSQandSQLInjection(rbeoimake2.SelectedValue, "soft");
            HyPanel1["EOISubmition"] = "";
            HyPanel1["EOIFillDate"] = null;
            HyPanel1["EOIURL"] = txteoilink.Text;
            for (int i = 0; i < chkQAA.Items.Count; i++)
            {
                if (chkQAA.Items[i].Selected == true)
                {
                    qa = qa + chkQAA.Items[i].Value + ",";
                }
            }
            if (qa.ToString() != "")
            {
                HyPanel1["QAAgency"] = qa.ToString();
            }
            else
            {
                HyPanel1["QAAgency"] = "";
            }
            HyPanel1["QAReamrks"] = "";
            HyPanel1["Testing"] = "";
            HyPanel1["TestingRemarks"] = "";
            HyPanel1["Certification"] = "";
            HyPanel1["CertificationRemark"] = "";
            HyPanel1["DPSUServices"] = "";
            HyPanel1["Remarks"] = "";
            HyPanel1["FinancialSupport"] = "";
            HyPanel1["FinancialRemark"] = "";
            for (int i = 0; i < chkinditargetyear.Items.Count; i++)
            {
                if (chkinditargetyear.Items[i].Selected == true)
                {
                    ity = ity + chkinditargetyear.Items[i].Value + ",";
                }
            }
            if (ity.ToString() != "")
            {
                HyPanel1["IndTargetYear"] = ity.ToString();
            }
            else
            {
                HyPanel1["IndTargetYear"] = "";
            }
            HyPanel1["IndProcess"] = chkindiprocstart.SelectedItem.Value;
            if (chkindiprocstart.SelectedItem.Value == "Yes")
            {
                for (int j = 0; j < rbIgCategory.Items.Count; j++)
                {
                    if (rbIgCategory.Items[j].Selected == true)
                    {
                        a = a + rbIgCategory.Items[j].Value + ",";
                    }
                }
                if (a.ToString() != "")
                {
                    HyPanel1["PurposeofProcurement"] = a.ToString();
                }
                else
                {
                    HyPanel1["PurposeofProcurement"] = "";
                }
            }
            else
            {
                HyPanel1["PurposeofProcurement"] = "";
            }
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
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Item Id (Portal) = " + StrProductDescription + " saved successfully.')", true);
                }
                else
                {
                    Cleartext();
                    btnsubmitpanel1.Text = "Save";
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Item Id (Portal) = " + StrProductDescription + " updated successfully.')", true);
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
            if (ddlcompany.SelectedItem.Text != "Select" && ddlmastercategory.SelectedItem.Text != "Select" && ddlsubcategory.SelectedItem.Text != "Select" && ddllevel3product.SelectedItem.Text != "Select" && ddltechnologycat.SelectedItem.Text != "Select" && ddlcompany.SelectedItem.Text != "Select")
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
                                    checklistdeclaration = checklistdeclaration + chkd.ToString().Substring(0, 1);
                                }
                                else
                                {
                                    checklistdeclaration = "";
                                    break;
                                }
                            }
                            if (checklistdeclaration != "")
                            {
                                //string mIgCat = "";
                                //foreach (ListItem chkIg in rbIgCategory.Items)
                                //{
                                //    if (chkIg.Selected == true)
                                //    {
                                //        mIgCat = mIgCat + chkIg;
                                //    }
                                //}
                                //if (mIgCat != "")
                                //{
                                if (rbeligible.SelectedIndex != -1)
                                {
                                    string mchkqua = "";
                                    foreach (ListItem chkQa in chkQAA.Items)
                                    {
                                        if (chkQa.Selected == true)
                                        {
                                            mchkqua = mchkqua + chkQa;
                                        }
                                    }
                                    if (mchkqua != "")
                                    {
                                        if (txtestquan1.Text != "" && txtpriceestimate1.Text != "" || txtestquan2.Text != "" && txtpriceestimate2.Text != "" || txtestquan3.Text != "" && txtpriceestimate3.Text != "")
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
                                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Please select any one year of import during last 3 years detail.')", true);
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Please select Quality Assurance Agency category tab and select checkbox before submit.')", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Please select EoI/RFP before submit.')", true);
                                }
                                //}
                                //else
                                //{ ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Please select indigenization category tab and select checkbox before submit.')", true); }
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
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Please fill (*) mandatory fields.')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Please fill (*) mandatory fields.')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Please fill (*) mandatory fields.')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Please check all (*) mandatory field are filled or check properly.' )", true);
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
        ddlenduser.SelectedIndex = -1;
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
        rbeoimake2.SelectedIndex = -1;
        eoi.Visible = false;
        chkQAA.SelectedIndex = -1;
        chkindiprocstart.SelectedValue = "Yes";
        chkinditargetyear.SelectedIndex = -1;
        rbIgCategory.SelectedIndex = -1;
        txtestquan1.Text = "";
        txtpriceestimate1.Text = "";
        txtestquan2.Text = "";
        txtpriceestimate2.Text = "";
        txtestquan3.Text = "";
        txtpriceestimate3.Text = "";
        txtfutQuantity1.Text = "";
        txtfutvalue1.Text = "";
        txtfutQuantity2.Text = "";
        txtfutvalue2.Text = "";
        txtfutQuantity3.Text = "";
        txtfutvalue3.Text = "";
        txtfutQuantity4.Text = "";
        txtfutvalue4.Text = "";
        txtfutQuantity5.Text = "";
        txtfutvalue5.Text = "";
        lblrefnoforinfo.Text = "";
        txteoilink.Text = "";
        portalid.Visible = false;
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
    protected DataTable Dvinsertfuture()
    {
        DataTable insertfuture = new DataTable();
        insertfuture.Columns.Add(new DataColumn("Year", typeof(string)));
        insertfuture.Columns.Add(new DataColumn("FYear", typeof(string)));
        insertfuture.Columns.Add(new DataColumn("EstimatedQty", typeof(string)));
        insertfuture.Columns.Add(new DataColumn("Unit", typeof(string)));
        insertfuture.Columns.Add(new DataColumn("EstimatedPrice", typeof(string)));
        insertfuture.Columns.Add(new DataColumn("Type", typeof(string)));
        insertfuture.Columns.Add(new DataColumn("ProdQtyPriceId", typeof(Int64)));
        DataRow dr;
        if (txtfutQuantity1.Text != "" && txtfutvalue1.Text != "")
        {
            dr = insertfuture.NewRow();
            if (EstimateQunFutureID.Value == "")
            {
                dr["ProdQtyPriceId"] = 0;
            }
            else
            {
                dr["ProdQtyPriceId"] = EstimateQunFutureID.Value;
            }
            dr["Year"] = ddlfutyear1.SelectedItem.Value;
            dr["FYear"] = ddlfutyear1.SelectedItem.Text;
            dr["EstimatedQty"] = txtfutQuantity1.Text;
            dr["Unit"] = ddlfutunit1.SelectedItem.Text;
            dr["EstimatedPrice"] = txtfutvalue1.Text;
            dr["Type"] = "F";
            insertfuture.Rows.Add(dr);
        }
        if (txtfutQuantity2.Text != "" && txtfutvalue2.Text != "")
        {
            dr = insertfuture.NewRow();
            if (EstimateQunFutureID2.Value == "")
            {
                dr["ProdQtyPriceId"] = 0;
            }
            else
            {
                dr["ProdQtyPriceId"] = EstimateQunFutureID2.Value;
            }
            dr["Year"] = ddlfutyear2.SelectedItem.Value;
            dr["FYear"] = ddlfutyear2.SelectedItem.Text;
            dr["EstimatedQty"] = txtfutQuantity2.Text;
            dr["Unit"] = ddlfutunit2.SelectedItem.Text;
            dr["EstimatedPrice"] = txtfutvalue2.Text;
            dr["Type"] = "F";
            insertfuture.Rows.Add(dr);
        }
        if (txtfutQuantity3.Text != "" && txtfutvalue3.Text != "")
        {
            dr = insertfuture.NewRow();
            if (EstimateQunFutureID3.Value == "")
            {
                dr["ProdQtyPriceId"] = 0;
            }
            else
            {
                dr["ProdQtyPriceId"] = EstimateQunFutureID3.Value;
            }
            dr["Year"] = ddlfutyear3.SelectedItem.Value;
            dr["FYear"] = ddlfutyear3.SelectedItem.Text;
            dr["EstimatedQty"] = txtfutQuantity3.Text;
            dr["Unit"] = ddlfutunit3.SelectedItem.Text;
            dr["EstimatedPrice"] = txtfutvalue3.Text;
            dr["Type"] = "F";
            insertfuture.Rows.Add(dr);
        }
        if (txtfutQuantity4.Text != "" && txtfutvalue4.Text != "")
        {
            dr = insertfuture.NewRow();
            if (EstimateQunFutureID4.Value == "")
            {
                dr["ProdQtyPriceId"] = 0;
            }
            else
            {
                dr["ProdQtyPriceId"] = EstimateQunFutureID4.Value;
            }
            dr["Year"] = ddlfutyear4.SelectedItem.Value;
            dr["FYear"] = ddlfutyear4.SelectedItem.Text;
            dr["EstimatedQty"] = txtfutQuantity4.Text;
            dr["Unit"] = ddlfutunit4.SelectedItem.Text;
            dr["EstimatedPrice"] = txtfutvalue4.Text;
            dr["Type"] = "F";
            insertfuture.Rows.Add(dr);
        }
        if (txtfutQuantity5.Text != "" && txtfutvalue5.Text != "")
        {
            dr = insertfuture.NewRow();
            if (EstimateQunFutureID5.Value == "")
            {
                dr["ProdQtyPriceId"] = 0;
            }
            else
            {
                dr["ProdQtyPriceId"] = EstimateQunFutureID5.Value;
            }
            dr["Year"] = ddlfutyear5.SelectedItem.Value;
            dr["FYear"] = ddlfutyear5.SelectedItem.Text;
            dr["EstimatedQty"] = txtfutQuantity5.Text;
            dr["Unit"] = ddlfutunit5.SelectedItem.Text;
            dr["EstimatedPrice"] = txtfutvalue5.Text;
            dr["Type"] = "F";
            insertfuture.Rows.Add(dr);
        }
        return insertfuture;
    }
    private void BindGridEstimateQuantity()
    {
        DataTable DtGridEstimate = new DataTable();
        DtGridEstimate = Lo.RetriveSaveEstimateGrid("Select", 0, hfprodrefno.Value, 0, "", "", "", "", "F");
        if (DtGridEstimate.Rows.Count > 0)
        {
            for (int ss = 0; DtGridEstimate.Rows.Count > ss; ss++)
            {
                if (DtGridEstimate.Rows[ss]["FYear"].ToString() == "2020-21")
                {
                    EstimateQunFutureID.Value = DtGridEstimate.Rows[ss]["ProdQtyPriceId"].ToString();
                    ddlfutyear1.SelectedValue = DtGridEstimate.Rows[ss]["Year"].ToString();
                    txtfutQuantity1.Text = DtGridEstimate.Rows[ss]["EstimatedQty"].ToString();
                    ddlfutunit1.SelectedValue = DtGridEstimate.Rows[ss]["Unit"].ToString();
                    txtfutvalue1.Text = DtGridEstimate.Rows[ss]["EstimatedPrice"].ToString();
                }
                else if (DtGridEstimate.Rows[ss]["FYear"].ToString() == "2021-22")
                {

                    EstimateQunFutureID2.Value = DtGridEstimate.Rows[ss]["ProdQtyPriceId"].ToString();
                    ddlfutyear2.SelectedValue = DtGridEstimate.Rows[ss]["Year"].ToString();
                    txtfutQuantity2.Text = DtGridEstimate.Rows[ss]["EstimatedQty"].ToString();
                    ddlfutunit2.SelectedValue = DtGridEstimate.Rows[ss]["Unit"].ToString();
                    txtfutvalue2.Text = DtGridEstimate.Rows[ss]["EstimatedPrice"].ToString();
                }
                else if (DtGridEstimate.Rows[ss]["FYear"].ToString() == "2022-23")
                {

                    EstimateQunFutureID3.Value = DtGridEstimate.Rows[ss]["ProdQtyPriceId"].ToString();
                    ddlfutyear3.SelectedValue = DtGridEstimate.Rows[ss]["Year"].ToString();
                    txtfutQuantity3.Text = DtGridEstimate.Rows[ss]["EstimatedQty"].ToString();
                    ddlfutunit3.SelectedValue = DtGridEstimate.Rows[ss]["Unit"].ToString();
                    txtfutvalue3.Text = DtGridEstimate.Rows[ss]["EstimatedPrice"].ToString();
                }
                else if (DtGridEstimate.Rows[ss]["FYear"].ToString() == "2023-24")
                {

                    EstimateQunFutureID4.Value = DtGridEstimate.Rows[ss]["ProdQtyPriceId"].ToString();
                    ddlfutyear4.SelectedValue = DtGridEstimate.Rows[ss]["Year"].ToString();
                    txtfutQuantity4.Text = DtGridEstimate.Rows[ss]["EstimatedQty"].ToString();
                    ddlfutunit4.SelectedValue = DtGridEstimate.Rows[ss]["Unit"].ToString();
                    txtfutvalue4.Text = DtGridEstimate.Rows[ss]["EstimatedPrice"].ToString();
                }
                else if (DtGridEstimate.Rows[ss]["FYear"].ToString() == "2024-25")
                {
                    EstimateQunFutureID5.Value = DtGridEstimate.Rows[ss]["ProdQtyPriceId"].ToString();
                    ddlfutyear5.SelectedValue = DtGridEstimate.Rows[ss]["Year"].ToString();
                    txtfutQuantity5.Text = DtGridEstimate.Rows[ss]["EstimatedQty"].ToString();
                    ddlfutunit5.SelectedValue = DtGridEstimate.Rows[ss]["Unit"].ToString();
                    txtfutvalue5.Text = DtGridEstimate.Rows[ss]["EstimatedPrice"].ToString();
                }
            }
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
        insert.Columns.Add(new DataColumn("ProdQtyPriceId", typeof(Int64)));
        DataRow dr;
        if (txtestquan1.Text != "" && txtpriceestimate1.Text != "")
        {
            dr = insert.NewRow();
            if (EstimateQunOldID.Value == "")
            {
                dr["ProdQtyPriceId"] = 0;
            }
            else
            {
                dr["ProdQtyPriceId"] = EstimateQunOldID.Value;
            }
            dr["Year"] = ddlyearestimate1.SelectedItem.Value;
            dr["FYear"] = ddlyearestimate1.SelectedItem.Text;
            dr["EstimatedQty"] = txtestquan1.Text;
            dr["Unit"] = ddlunit1.SelectedItem.Text;
            dr["EstimatedPrice"] = txtpriceestimate1.Text;
            dr["Type"] = "O";
            insert.Rows.Add(dr);
        }
        if (txtestquan2.Text != "" && txtpriceestimate2.Text != "")
        {
            dr = insert.NewRow();
            if (EstimateQunOldID2.Value == "")
            {
                dr["ProdQtyPriceId"] = 0;
            }
            else
            {
                dr["ProdQtyPriceId"] = EstimateQunOldID2.Value;
            }
            dr["Year"] = ddlyearestimate2.SelectedItem.Value;
            dr["FYear"] = ddlyearestimate2.SelectedItem.Text;
            dr["EstimatedQty"] = txtestquan2.Text;
            dr["Unit"] = ddlunit2.SelectedItem.Text;
            dr["EstimatedPrice"] = txtpriceestimate2.Text;
            dr["Type"] = "O";
            insert.Rows.Add(dr);
        }
        if (txtestquan3.Text != "" && txtpriceestimate3.Text != "")
        {
            dr = insert.NewRow();
            if (EstimateQunOldID3.Value == "")
            {
                dr["ProdQtyPriceId"] = 0;
            }
            else
            {
                dr["ProdQtyPriceId"] = EstimateQunOldID3.Value;
            }
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
            for (int estimate = 0; DtGridEstimate1.Rows.Count > estimate; estimate++)
            {
                if (DtGridEstimate1.Rows[estimate]["FYear"].ToString() == "2017-18")
                {
                    EstimateQunOldID.Value = DtGridEstimate1.Rows[estimate]["ProdQtyPriceId"].ToString();
                    ddlyearestimate1.SelectedValue = DtGridEstimate1.Rows[estimate]["Year"].ToString();
                    txtestquan1.Text = DtGridEstimate1.Rows[estimate]["EstimatedQty"].ToString();
                    ddlunit1.SelectedValue = DtGridEstimate1.Rows[estimate]["Unit"].ToString();
                    txtpriceestimate1.Text = DtGridEstimate1.Rows[estimate]["EstimatedPrice"].ToString();
                }
                else if (DtGridEstimate1.Rows[estimate]["FYear"].ToString() == "2018-19")
                {
                    EstimateQunOldID2.Value = DtGridEstimate1.Rows[estimate]["ProdQtyPriceId"].ToString();
                    ddlyearestimate2.SelectedValue = DtGridEstimate1.Rows[estimate]["Year"].ToString();
                    txtestquan2.Text = DtGridEstimate1.Rows[estimate]["EstimatedQty"].ToString();
                    ddlunit2.SelectedValue = DtGridEstimate1.Rows[estimate]["Unit"].ToString();
                    txtpriceestimate2.Text = DtGridEstimate1.Rows[estimate]["EstimatedPrice"].ToString();
                }
                else if (DtGridEstimate1.Rows[estimate]["FYear"].ToString() == "2019-20")
                {
                    EstimateQunOldID3.Value = DtGridEstimate1.Rows[estimate]["ProdQtyPriceId"].ToString();
                    ddlyearestimate3.SelectedValue = DtGridEstimate1.Rows[estimate]["Year"].ToString();
                    txtestquan3.Text = DtGridEstimate1.Rows[estimate]["EstimatedQty"].ToString();
                    ddlunit3.SelectedValue = DtGridEstimate1.Rows[estimate]["Unit"].ToString();
                    txtpriceestimate3.Text = DtGridEstimate1.Rows[estimate]["EstimatedPrice"].ToString();
                }
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
                lblrefnoforinfo.Text = hfprodrefno.Value;
                portalid.Visible = true;
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
                txtoemaddress.Text = DtView.Rows[0]["OEMAddress"].ToString();
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
                }
                else
                {
                    BindGridEstimateQuantity1();
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

                txtremarksprocurmentCategory.Text = DtView.Rows[0]["ProcurmentCategoryRemark"].ToString();
                rbeoimake2.SelectedValue = DtView.Rows[0]["EOIStatus"].ToString();
                if (rbeoimake2.SelectedValue == "Yes")
                {
                    txteoilink.Text = DtView.Rows[0]["EOIURL"].ToString();
                    eoi.Visible = true;
                }
                if (DtView.Rows[0]["QAAgency"].ToString() != "")
                {
                    DataTable dtQat = Lo.RetriveProductCode("", hfprodrefno.Value, "ProductQAAgency", hidType.Value);
                    if (dtQat.Rows.Count > 0)
                    {
                        for (int i = 0; dtQat.Rows.Count > i; i++)
                        {
                            foreach (ListItem liqa in chkQAA.Items)
                            {
                                if (liqa.Value == dtQat.Rows[i]["SCategoryId"].ToString())
                                {
                                    liqa.Selected = true;
                                }
                            }
                        }

                    }
                }
                if (DtView.Rows[0]["IndTargetYear"].ToString() != "")
                {
                    string m = DtView.Rows[0]["IndTargetYear"].ToString().Substring(0, DtView.Rows[0]["IndTargetYear"].ToString().Length - 1);
                    char[] spearator = { ',', ' ' };
                    Int32 count = 6;
                    String[] strlist = m.Split(spearator, count, StringSplitOptions.None);
                    foreach (String s in strlist)
                    {
                        for (int i = 0; chkinditargetyear.Items.Count > i; i++)
                        {
                            if (s == chkinditargetyear.Items[i].Value)
                            {
                                chkinditargetyear.Items[i].Selected = true;
                            }
                        }
                    }
                }
                chkindiprocstart.SelectedValue = DtView.Rows[0]["IndProcess"].ToString();
                if (chkindiprocstart.SelectedValue == "Yes")
                {
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
                                        rw.Selected = true;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
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
                                        rw.Selected = true;
                                    }
                                }
                            }
                            chkindiprocstart.SelectedValue = "Yes";
                        }
                    }
                    else
                    {
                        indicatchk.Visible = false;
                    }
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