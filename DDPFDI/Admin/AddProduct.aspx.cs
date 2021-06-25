using System;
using BusinessLayer;
using Encryption;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using context = System.Web.HttpContext;

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
                        }
                        if (hidType.Value.ToString() != "SuperAdmin" || hidType.Value.ToString() != "Admin")
                        {
                            BindCompany();
                            BindCountry();
                            BindFinancialYear();
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
                    ExceptionLogging.SendErrorToText(ex);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "ErrorMssgPopup('Oops some error occured Please refresh page or contact us." + ex.Message + "')", true);
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
                    // divnodal2.Visible = true;
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
    protected void ddltechnologycat_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMasterSubCategoryTech();
    }
    protected void rbisindinised_CheckedChanged(object sender, EventArgs e)
    {
        if (rbisindinised.SelectedItem.Value == "Y")
        {
            if (hfvaluelast3years.Value != "")
            {
                txtmaxvalue.Text = hfvaluelast3years.Value;
            }
            //if (hfprodrefno.Value == "")
            //{
                DateTime dtmnth = Convert.ToDateTime(DateTime.Now);
                string Printmnth = dtmnth.ToString("MMM");
                ddlmonth.SelectedValue = Printmnth.ToString();
                DateTime dtyear = Convert.ToDateTime(DateTime.Now);
                string Printyear = dtyear.ToString("yyyy");
                ddlyear.SelectedValue = Printyear.ToString();
                divisIndigenized.Visible = true;
                DivAtmnirbhar.Visible = true;
            //}
            //else
            //{
            //    divisIndigenized.Visible = true;
            //    DivAtmnirbhar.Visible = true;
            //}
        }
        else if (rbisindinised.SelectedItem.Value == "N")
        {
            divisIndigenized.Visible = false;
            if (rbSuuplyOrder.SelectedItem.Value == "Yes")
            {
                DivAtmnirbhar.Visible = true;
                //if (hfprodrefno.Value == "")
                //{
                    DateTime dtmnth = Convert.ToDateTime(DateTime.Now);
                    string Printmnth = dtmnth.ToString("MMM");
                    ddlmonth.SelectedValue = Printmnth.ToString();
                    DateTime dtyear = Convert.ToDateTime(DateTime.Now);
                    string Printyear = dtyear.ToString("yyyy");
                    ddlyear.SelectedValue = Printyear.ToString();
                //}
            }
            else
            {
                DivAtmnirbhar.Visible = false;
            }
        }
    }
    protected void rbSuuplyOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbSuuplyOrder.SelectedItem.Value == "Yes")
        {
            supplyorder.Visible = true;
            DivAtmnirbhar.Visible = true;
            //if (hfprodrefno.Value == "")
            //{
                DateTime dtmnth = Convert.ToDateTime(DateTime.Now);
                string Printmnth = dtmnth.ToString("MMM");
                ddlmonth.SelectedValue = Printmnth.ToString();
                DateTime dtyear = Convert.ToDateTime(DateTime.Now);
                string Printyear = dtyear.ToString("yyyy");
                ddlyear.SelectedValue = Printyear.ToString();
           // }
        }
        else
        {
            supplyorder.Visible = false;
            DivAtmnirbhar.Visible = false;
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
        if (rbeoimake2.SelectedValue != "No")
        {
            eoi.Visible = true;
            if (txteoistartdate.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Please enter EOI Start Date.');", true);
                txteoistartdate.Focus();

            }
            else if ((!Regex.Match(txteoistartdate.Text, @"^(0[1-9]|[12][0-9]|3[01])[ /](0[1-9]|1[012])[ /]([0-9]{4})$", RegexOptions.None).Success))
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "MyScript", "alert('Please enter EOI Start Date in DD/MM/YYYY.');", true);
                txteoistartdate.Focus();

            }
            if (txteoienddate.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), UniqueID, "alert('Please enter EOI End Date.');", true);
                txteoienddate.Focus();

            }
            else if ((!Regex.Match(txteoistartdate.Text, @"^(0[1-9]|[12][0-9]|3[01])[ /](0[1-9]|1[012])[ /]([0-9]{4})$", RegexOptions.None).Success))
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "MyScript", "alert('Please enter EOI End Date in DD/MM/YYYY.');", true);
                txteoienddate.Focus();

            }

        }
        else
        {
            eoi.Visible = false;
        }
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
        }
        else
        {
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
            Co.FillRadioBoxList(rbIgCategory, DtMasterCategroy, "SCategoryName", "SCategoryID");
        }
    }
    #endregion
    #region code change of desc
    protected void txtproductdescription_TextChanged(object sender, EventArgs e)
    {
        if (txtproductdescription.Text.Trim() != "")
        {
            try
            {
                DataTable DtGetProdDesc = Lo.NewRetriveFilterCode("DuplicateProd", Co.RSQandSQLInjection(txtproductdescription.Text.Trim(), "soft"), "", "", "", 0, 0, 0);
                if (DtGetProdDesc.Rows.Count > 0)
                {
                    dup.Value = "Error";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('duplicate record found with same item name.Item will not be saved,Please ensure before enter record')", true);
                }
            }
            catch (Exception ex)
            {
                dup.Value = "Error";
                ex.Message.ToString();
            }
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
                HyPanel1["ProductID"] = Convert.ToInt32(Server.HtmlEncode(hfprodid.Value.Trim()));
                HyPanel1["ProductRefNo"] = Co.RSQandSQLInjection(Server.HtmlEncode(hfprodrefno.Value.Trim()), "soft");
            }
            else
            {
                HyPanel1["ProductID"] = 0;
            }
            if (hfprodid.Value != "")
            {
                if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.Visible == false)
                {
                    HyPanel1["CompanyRefNo"] = Co.RSQandSQLInjection(Server.HtmlEncode(ddlcompany.SelectedItem.Value), "soft");
                    hfcomprefno.Value = Server.HtmlEncode(ddlcompany.SelectedItem.Value.Trim());
                    hidType.Value = "Company";
                    HyPanel1["Role"] = Server.HtmlEncode(hidType.Value.ToString());
                }
                else if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.SelectedItem.Text != "Select" && ddlunit.Visible == false)
                {
                    HyPanel1["CompanyRefNo"] = Co.RSQandSQLInjection(Server.HtmlEncode(ddldivision.SelectedItem.Value), "soft");
                    hfcomprefno.Value = Server.HtmlEncode(ddldivision.SelectedItem.Value.Trim());
                    hidType.Value = "Division";
                    HyPanel1["Role"] = Server.HtmlEncode(hidType.Value.ToString());
                }
                else if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.SelectedItem.Text != "Select" && ddlunit.SelectedItem.Text != "Select")
                {
                    HyPanel1["CompanyRefNo"] = Co.RSQandSQLInjection(Server.HtmlEncode(ddlunit.SelectedItem.Value), "soft");
                    hfcomprefno.Value = Server.HtmlEncode(ddlunit.SelectedItem.Value.Trim());
                    hidType.Value = "Unit";
                    HyPanel1["Role"] = Server.HtmlEncode(hidType.Value.ToString());
                }
            }
            else
            {
                if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.SelectedItem.Text == "Select" || ddldivision.Visible == false)
                {
                    HyPanel1["CompanyRefNo"] = Co.RSQandSQLInjection(Server.HtmlEncode(ddlcompany.SelectedItem.Value), "soft");
                    hidType.Value = "Company";
                    hfcomprefno.Value = Server.HtmlEncode(ddlcompany.SelectedItem.Value.Trim());
                    HyPanel1["Role"] = Server.HtmlEncode(hidType.Value.ToString());
                }
                else if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.SelectedItem.Text != "Select" && ddlunit.SelectedItem.Text == "Select" || ddlunit.SelectedItem.Text == "Select")
                {
                    HyPanel1["CompanyRefNo"] = Co.RSQandSQLInjection(Server.HtmlEncode(ddldivision.SelectedItem.Value), "soft");
                    hidType.Value = "Division";
                    hfcomprefno.Value = Server.HtmlEncode(ddldivision.SelectedItem.Value.Trim());
                    HyPanel1["Role"] = Server.HtmlEncode(hidType.Value.ToString());
                }
                else if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.SelectedItem.Text != "Select" && ddlunit.SelectedItem.Text != "Select")
                {
                    HyPanel1["CompanyRefNo"] = Co.RSQandSQLInjection(Server.HtmlEncode(ddlunit.SelectedItem.Value), "soft");
                    hidType.Value = "Unit";
                    hfcomprefno.Value = Server.HtmlEncode(ddlunit.SelectedItem.Value.Trim());
                    HyPanel1["Role"] = Server.HtmlEncode(hidType.Value.ToString());
                }
            }
            if (ddlmastercategory.SelectedItem.Value != "Select")
            {
                HyPanel1["ProductLevel1"] = Co.RSQandSQLInjection(Server.HtmlEncode(ddlmastercategory.SelectedItem.Value.Trim()), "soft");
            }
            else
            {
                HyPanel1["ProductLevel1"] = null;
            }
            if (ddlmastercategory.SelectedItem.Value == "Select")
            {
                HyPanel1["ProductLevel3"] = null;
                HyPanel1["ProductLevel2"] = null;
            }
            else
            {
                if (ddlsubcategory.SelectedItem.Value != "Select")
                {
                    HyPanel1["ProductLevel2"] = Co.RSQandSQLInjection(Server.HtmlEncode(ddlsubcategory.SelectedItem.Value.Trim()), "soft");
                }
                else
                {
                    HyPanel1["ProductLevel2"] = null;
                }
                if (ddllevel3product.SelectedItem.Value != "Select")
                {
                    HyPanel1["ProductLevel3"] = Co.RSQandSQLInjection(Server.HtmlEncode(ddllevel3product.SelectedItem.Value.Trim()), "soft");
                }
                else
                {
                    HyPanel1["ProductLevel3"] = null;
                }
            }
            HyPanel1["ProductDescription"] = Co.RSQandSQLInjection(Server.HtmlEncode(txtproductdescription.Text.Trim()), "soft");
            HyPanel1["NSCCode"] = Co.RSQandSQLInjection(Server.HtmlEncode(txtnsccode.Text.Trim()), "soft");
            HyPanel1["NIINCode"] = Co.RSQandSQLInjection(Server.HtmlEncode(txtniincode.Text.Trim()), "soft");
            HyPanel1["FeatursandDetail"] = Co.RSQandSQLInjection(Server.HtmlEncode(txtfeaturesanddetails.Text.Trim()), "soft");
            if (fuitemdescriptionfile.HasFiles != false)
            {
                if (hfprodid.Value != "")
                {
                    DataTable dtPdfBind = Lo.RetriveProductCode("", Server.HtmlEncode(hfprodrefno.Value), "RetrivePdf", Server.HtmlEncode(hidType.Value));
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
                    DataTable dtImageBind = Lo.RetriveProductCode("", Server.HtmlEncode(hfprodrefno.Value), "RetriveImage", Server.HtmlEncode(hidType.Value));
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
            HyPanel1["OEMPartNumber"] = Co.RSQandSQLInjection(Server.HtmlEncode(txtoempartnumber.Text.Trim()), "soft");
            HyPanel1["OEMName"] = Co.RSQandSQLInjection(Server.HtmlEncode(txtoemname.Text.Trim()), "soft");
            if (txtcountry.SelectedItem.Value != "Select")
            {
                HyPanel1["OEMCountry"] = Convert.ToInt64(Server.HtmlEncode(txtcountry.SelectedItem.Value.Trim()));
            }
            else
            {
                HyPanel1["OEMCountry"] = "0";
            }

            HyPanel1["OEMAddress"] = Co.RSQandSQLInjection(Server.HtmlEncode(txtoemaddress.Text.Trim()), "soft");
            HyPanel1["DPSUPartNumber"] = Co.RSQandSQLInjection(Server.HtmlEncode(txtdpsupartnumber.Text.Trim()), "soft");
            HyPanel1["HsnCode8digit"] = Server.HtmlEncode(txthsncodereadonly.Text.Trim());
            if (ddlenduser.SelectedIndex != -1)
            {
                for (int o = 0; o < ddlenduser.Items.Count; o++)
                {
                    if (ddlenduser.Items[o].Selected == true)
                    {
                        m = m + ddlenduser.Items[o].Value + ",";
                    }
                }
                HyPanel1["EndUser"] = Server.HtmlEncode(m.ToString().Trim());
            }
            else
            {
                HyPanel1["EndUser"] = null;
            }
            if (ddlplatform.SelectedItem.Value != "Select")
            {
                HyPanel1["Platform"] = Co.RSQandSQLInjection(Server.HtmlEncode(ddlplatform.SelectedItem.Value.Trim()), "soft");
            }
            else
            {
                HyPanel1["Platform"] = null;
            }
            if (ddlplatform.SelectedItem.Value == "Select")
            {
                HyPanel1["NomenclatureOfMainSystem"] = null;
            }
            else
            {
                if (ddlnomnclature.SelectedItem.Value != "Select")
                {
                    HyPanel1["NomenclatureOfMainSystem"] = Co.RSQandSQLInjection(Server.HtmlEncode(ddlnomnclature.SelectedItem.Value.Trim()), "soft");
                }
                else
                {
                    HyPanel1["NomenclatureOfMainSystem"] = null;
                }
            }
            if (ddltechnologycat.SelectedItem.Value.Trim() != "Select")
            {
                HyPanel1["TechnologyLevel1"] = Co.RSQandSQLInjection(Server.HtmlEncode(ddltechnologycat.SelectedItem.Value.Trim()), "soft");
            }
            else
            {
                HyPanel1["TechnologyLevel1"] = null;
            }
            if (ddltechnologycat.SelectedItem.Value == "Select")
            {
                HyPanel1["TechnologyLevel2"] = null;
            }
            else
            {
                if (ddlsubtech.SelectedItem.Value != "Select")
                {
                    HyPanel1["TechnologyLevel2"] = Co.RSQandSQLInjection(Server.HtmlEncode(ddlsubtech.SelectedItem.Value.Trim()), "soft");
                }
                else
                {
                    HyPanel1["TechnologyLevel2"] = null;
                }
            }

            if (rbproductImported.SelectedItem.Value == "N")
            {
                HyPanel1["IsProductImported"] = Co.RSQandSQLInjection(Server.HtmlEncode(rbproductImported.SelectedItem.Value.Trim()), "soft");
            }
            else
            {
                HyPanel1["IsProductImported"] = Co.RSQandSQLInjection(Server.HtmlEncode(rbproductImported.SelectedItem.Value.Trim()), "soft");
                dtSaveEstimateQuantity1 = Dvinsert();
            }
            dtSaveEstimateQuantity = Dvinsertfuture();
            //for (int i = 0; i < chkinditargetyear.Items.Count; i++)
            //{
            //    if (chkinditargetyear.Items[i].Selected == true)
            //    {
            //        ity = ity + chkinditargetyear.Items[i].Value.Trim() + ",";
            //    }
            //}
            if (chkinditargetyear.SelectedIndex != -1)  // if (ity.ToString() != "")
            {
                HyPanel1["IndTargetYear"] = Server.HtmlEncode(chkinditargetyear.SelectedItem.Value.Trim()); // ity.ToString().Trim();
            }
            else
            {
                HyPanel1["IndTargetYear"] = "";
            }
            HyPanel1["EOIStatus"] = Co.RSQandSQLInjection(Server.HtmlEncode(rbeoimake2.SelectedValue.Trim()), "soft");
            HyPanel1["EOIURL"] = Server.HtmlEncode(txteoilink.Text.Trim());
            if (txteoistartdate.Text != "")
            {
                DateTime EoiStar = Convert.ToDateTime(Server.HtmlEncode(txteoistartdate.Text));
                string mDateEoiStart = EoiStar.ToString("yyyy-MMM-dd");
                HyPanel1["EOIStartDate"] = mDateEoiStart.ToString().Trim();
            }
            else
            {
                HyPanel1["EOIStartDate"] = null;
            }
            if (txteoienddate.Text != "")
            {
                DateTime EoiEnd = Convert.ToDateTime(Server.HtmlEncode(txteoienddate.Text));
                string mDateEoiEnd = EoiEnd.ToString("yyyy-MMM-dd");
                HyPanel1["EOIEndDate"] = mDateEoiEnd.ToString().Trim();
            }
            else
            {
                HyPanel1["EOIEndDate"] = null;
            }
            HyPanel1["SupplyOrderStatus"] = Server.HtmlEncode(rbSuuplyOrder.SelectedItem.Value.Trim());
            HyPanel1["SupplyManfutureName"] = Server.HtmlEncode(txtsupplumanufacturename.Text.Trim());
            HyPanel1["SupplyManfutureAddress"] = Server.HtmlEncode(txtsupplymanufactureaddress.Text.Trim());
            if (txtsupplyorderrplkh.Text != "")
            {
                HyPanel1["SupplyOrderValue"] = Server.HtmlEncode(txtsupplyorderrplkh.Text.Trim());
            }
            else
            {
                HyPanel1["SupplyOrderValue"] = 0;
            }
            if (txtdeliverycompdate.Text != "")
            {
                DateTime dtsuppdelivery = Convert.ToDateTime(Server.HtmlEncode(txtdeliverycompdate.Text));
                string dtsuppdeliveryDate = dtsuppdelivery.ToString("dd-MMM-yyyy");
                HyPanel1["SupplyDeliveryDate"] = dtsuppdeliveryDate.ToString().Trim();
            }
            else
            {
                HyPanel1["SupplyDeliveryDate"] = "";
            }
            if (txtsodate.Text != "")
            {
                DateTime dtsodate = Convert.ToDateTime(Server.HtmlEncode(txtsodate.Text));
                string dtsoDatem = dtsodate.ToString("dd-MMM-yyyy");
                HyPanel1["SupplyOrderDate"] = dtsoDatem.ToString().Trim();
            }
            else
            {
                HyPanel1["SupplyOrderDate"] = "";
            }

            HyPanel1["SanctionOrderStatus"] = Server.HtmlEncode(rbSuuplyOrder.SelectedItem.Value.Trim());
            HyPanel1["SanctionReason"] = Server.HtmlEncode(rbSuuplyOrder.SelectedItem.Value.Trim());
            HyPanel1["SanctionManfutureName"] = Server.HtmlEncode(txtsupplumanufacturename.Text.Trim());
            HyPanel1["SanctionManfutureAddress"] = Server.HtmlEncode(txtsupplymanufactureaddress.Text.Trim());
            if (Txtorderdate.Text != "")
            {
                DateTime dtoodate = Convert.ToDateTime(Server.HtmlEncode(Txtorderdate.Text));
                string dtooDatem = dtoodate.ToString("dd-MMM-yyyy");
                HyPanel1["SanctionOrderDate"] = dtooDatem.ToString().Trim();
            }
            else
            {
                HyPanel1["SanctionOrderDate"] = "";
            }

            if (txtdeliverycompdate.Text != "")
            {
                DateTime dtsuppdelivery = Convert.ToDateTime(Server.HtmlEncode(txtdeliverycompdate.Text));
                string dtsuppdeliveryDate = dtsuppdelivery.ToString("dd-MMM-yyyy");
                HyPanel1["SupplyDeliveryDate"] = dtsuppdeliveryDate.ToString().Trim();
            }
            else
            {
                HyPanel1["SupplyDeliveryDate"] = "";
            }



            HyPanel1["IndProcess"] = Server.HtmlEncode(chkindiprocstart.SelectedItem.Value.Trim());
            HyPanel1["IsIndeginized"] = Co.RSQandSQLInjection(Server.HtmlEncode(rbisindinised.SelectedItem.Value.Trim()), "soft");
            HyPanel1["IndeginizedDate"] = Co.RSQandSQLInjection(Server.HtmlEncode(ddlmonth.SelectedValue.Trim() + "-" + ddlyear.SelectedValue.Trim()), "soft");
            //   HyPanel1["IndeginizedDate"] = Co.RSQandSQLInjection(Server.HtmlEncode(ddlyear.SelectedValue.Trim()), "soft");
            //if (ddlmonth.Text != "")
            //{
            //    //  DateTime dtindidate = Convert.ToDateTime(lblindiginizeddateorvalue.Text);
            //    //  string mdateindate = dtindidate.ToString("yyyy-MMM-dd"); 
            //    DateTime dtindidate = DateTime.Parse(lblindiginizeddateorvalue.Text);
            //    string mdateindate = dtindidate.ToString("yyyy-MMM-dd");
            //    HyPanel1["IndeginizedDate"] = mdateindate.ToString();
            //}
            //else
            //{
            //    HyPanel1["IndeginizedDate"] = null;
            //}
            HyPanel1["IndeginizedMaxValue"] = Co.RSQandSQLInjection(Server.HtmlEncode(txtmaxvalue.Text.Trim()), "soft");
            HyPanel1["ManufactureName"] = Co.RSQandSQLInjection(Server.HtmlEncode(txtmanufacturename.Text.Trim()), "soft");
            HyPanel1["ManufactureAddress"] = Co.RSQandSQLInjection(Server.HtmlEncode(txtmanifacaddress.Text.Trim()), "soft");
            if (ddlyearofindiginization.SelectedItem.Text == "Select")
            {
                HyPanel1["YearofIndiginization"] = null;
            }
            else
            {
                HyPanel1["YearofIndiginization"] = Co.RSQandSQLInjection(Server.HtmlEncode(ddlyearofindiginization.SelectedItem.Value.Trim()), "soft");
            }
            if (chkindiprocstart.SelectedItem.Value == "Yes")
            {
                //for (int j = 0; j < rbIgCategory.Items.Count; j++)
                //{
                //    if (rbIgCategory.Items[j].Selected == true)
                //    {
                //        a = a + rbIgCategory.Items[j].Value + ",";
                //    }
                //}
                if (rbIgCategory.SelectedIndex != -1)  // if (a.ToString() != "")
                {
                    HyPanel1["PurposeofProcurement"] = Server.HtmlEncode(rbIgCategory.SelectedItem.Value.Trim());//a.ToString();
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
            for (int i = 0; i < chkQAA.Items.Count; i++)
            {
                if (chkQAA.Items[i].Selected == true)
                {
                    qa = qa + chkQAA.Items[i].Value + ",";
                }
            }
            if (qa != null)
            {
                HyPanel1["QAAgency"] = Server.HtmlEncode(qa.ToString().Trim());
            }
            else
            {
                HyPanel1["QAAgency"] = "";
            }
            if (ddlNodalOfficerEmail.Text == "" || ddlNodalOfficerEmail.SelectedItem.Text == "Select")
            {
                HyPanel1["NodelDetail"] = null;
            }
            else
            {
                HyPanel1["NodelDetail"] = Convert.ToInt32(Server.HtmlEncode(ddlNodalOfficerEmail.SelectedItem.Value));
            }
            if (ddlNodalOfficerEmail2.Text == "" || ddlNodalOfficerEmail2.SelectedItem.Text == "Select")//ddlprocurmentcategory
            {
                HyPanel1["NodalDetail2"] = null;
            }
            else
            {
                HyPanel1["NodalDetail2"] = Convert.ToInt32(Server.HtmlEncode(ddlNodalOfficerEmail2.SelectedItem.Value.Trim()));
            }
            HyPanel1["ShowGeneralDec"] = Server.HtmlEncode(rbeligible.SelectedItem.Value.Trim());

            if (rbViewOnlyStatus.SelectedIndex != -1)
            {
                HyPanel1["ViewOnlyStatus"] = "Yes";
                HyPanel1["ViewOnlyReasone"] = Server.HtmlEncode(rbViewOnlyStatus.SelectedItem.Value);
            }
            else
            {
                HyPanel1["ViewOnlyStatus"] = "No";
            }
            HyPanel1["CreatedBy"] = Server.HtmlEncode(ViewState["UserLoginEmail"].ToString().Trim());
            if (dup.Value != "Error")
            {
                string StrProductDescription = Lo.SaveCodeProduct(HyPanel1, dtImage, dtPdf, dtSaveEstimateQuantity, dtSaveEstimateQuantity1, out _sysMsg, out _msg, "Product");
                if (StrProductDescription != "-1")
                {
                    if (btnsubmitpanel1.Text != "Update")
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Item Id (Portal) = " + StrProductDescription + " saved successfully.')", true);
                        Cleartext();
                    }
                    else
                    {
                        try
                        {
                            string dtrecentupdate = Lo.UpdateStatus(0, StrProductDescription.ToString(), "InstantUpdate");
                        }
                        catch (Exception ex)
                        { ex.Message.ToString(); }
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
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Record not saved.Duplicate product name found.')", true);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert(" + ex.Message + ")", true);
        }
    }
    #endregion
    #region PanelSaveButtonCode
    protected void btnsubmitpanel1_Click(object sender, EventArgs e)
    {
        try
        {
            HyPanel1["CompanyRefNo"] = Co.RSQandSQLInjection(ddlcompany.SelectedItem.Value, "soft");
            if (HyPanel1["CompanyRefNo"].ToString() == "C0023" || HyPanel1["CompanyRefNo"].ToString() == "C0024" || HyPanel1["CompanyRefNo"].ToString() == "C0025")
            {
                if (txtproductdescription.Text != "")
                {
                    if (txtestquan1.Text != "" && txtpriceestimate1.Text != "" || txtestquan2.Text != "" && txtpriceestimate2.Text != "" || txtestquan3.Text != "" && txtpriceestimate3.Text != "")
                    {
                        SaveProductDescription();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Please select any one year of import during last 3 years detail. and Item Name is mandatory')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Item Name is mandatory')", true);
                }
                try
                {
                    string id = Lo.UpdateStatus(0, "", "Updatecodeproduct");
                    if (id != "-1")
                    {
                        DataTable dt = Lo.RetriveFilterCode("", "", "tryGetUpdatecode");
                        if (dt.Rows.Count != 0)
                        {
                            //lbl.Text = "Total rows update :- " + dt.Rows[0]["Total"].ToString();
                            DataTable mdt = Lo.RetriveFilterCode(objEnc.DecryptData(Session["User"].ToString()).Trim(), dt.Rows[0]["Total"].ToString(), "updatestatus");
                            string id1 = Lo.UpdateStatus(0, "", "UpdateProgRep");
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Oops some error occurs')", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Oops some error occurs (Timeout error)')", true);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Oops some error occurs')", true);
                }
            }
            else
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
                                                string mIndigYearTarget = "";
                                                foreach (ListItem chkIty in chkinditargetyear.Items)
                                                {
                                                    if (chkIty.Selected == true)
                                                    {
                                                        mIndigYearTarget = mIndigYearTarget + chkIty.ToString().Trim();
                                                    }
                                                }
                                                if (mIndigYearTarget != "")
                                                {
                                                    if (txtestquan4.Text != "" && txtpriceestimate4.Text != "" || txtestquan2.Text != "" && txtpriceestimate2.Text != "" || txtestquan3.Text != "" && txtpriceestimate3.Text != "")
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
                                                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Please checked Indigenization Target Year.')", true);
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
        }
        catch (Exception ex)
        { ExceptionLogging.SendErrorToText(ex); }
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
        ddlsubcategory.Items.Clear();
        ddllevel3product.Items.Clear();
        ddlsubtech.Items.Clear();
        txtfeaturesanddetails.Text = "";
        rbproductImported.SelectedIndex = 0;
        contactpanel1.Visible = false;
        divnodal2.Visible = false;
        if (ddlNodalOfficerEmail.Text != "")
        {
            ddlNodalOfficerEmail.SelectedIndex = 0;
        }
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
        txteoienddate.Text = "";
        txteoistartdate.Text = "";
        txtsupplumanufacturename.Text = "";
        txtsupplymanufactureaddress.Text = "";
        supplyorder.Visible = false;
        eoi.Visible = false;
        dup.Value = "";
        rbViewOnlyStatus.SelectedIndex = -1;
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
        //if (txtfutQuantity1.Text != "" && txtfutvalue1.Text != "")
        //{
        //    dr = insertfuture.NewRow();
        //    if (EstimateQunFutureID.Value == "")
        //    {
        //        dr["ProdQtyPriceId"] = 0;
        //    }
        //    else
        //    {
        //        dr["ProdQtyPriceId"] = EstimateQunFutureID.Value;
        //    }
        //    dr["Year"] = ddlfutyear1.SelectedItem.Value;
        //    dr["FYear"] = ddlfutyear1.SelectedItem.Text;
        //    dr["EstimatedQty"] = txtfutQuantity1.Text;
        //    dr["Unit"] = ddlfutunit1.SelectedItem.Text;
        //    dr["EstimatedPrice"] = txtfutvalue1.Text;
        //    dr["Type"] = "F";
        //    insertfuture.Rows.Add(dr);
        //}
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
        if (txtfutQuantity6.Text != "" && txtfutvalue6.Text != "")
        {
            dr = insertfuture.NewRow();
            if (EstimateQunFutureID6.Value == "")
            {
                dr["ProdQtyPriceId"] = 0;
            }
            else
            {
                dr["ProdQtyPriceId"] = EstimateQunFutureID6.Value;
            }
            dr["Year"] = ddlfutyear6.SelectedItem.Value;
            dr["FYear"] = ddlfutyear6.SelectedItem.Text;
            dr["EstimatedQty"] = txtfutQuantity6.Text;
            dr["Unit"] = ddlfutunit6.SelectedItem.Text;
            dr["EstimatedPrice"] = txtfutvalue6.Text;
            dr["Type"] = "F";
            insertfuture.Rows.Add(dr);
        }
        if (txtfutQuantity7.Text != "" && txtfutvalue7.Text != "")
        {
            dr = insertfuture.NewRow();
            if (EstimateQunFutureID7.Value == "")
            {
                dr["ProdQtyPriceId"] = 0;
            }
            else
            {
                dr["ProdQtyPriceId"] = EstimateQunFutureID7.Value;
            }
            dr["Year"] = ddlfutyear7.SelectedItem.Value;
            dr["FYear"] = ddlfutyear7.SelectedItem.Text;
            dr["EstimatedQty"] = txtfutQuantity7.Text;
            dr["Unit"] = ddlfutunit7.SelectedItem.Text;
            dr["EstimatedPrice"] = txtfutvalue7.Text;
            dr["Type"] = "F";
            insertfuture.Rows.Add(dr);
        }
        return insertfuture;
    }
    private void BindGridEstimateQuantity()
    {
        DataTable DtGridEstimate = new DataTable();
        DtGridEstimate = Lo.RetriveSaveEstimateGrid("Selectp1", 0, hfprodrefno.Value, 0, "", "", "", "", "F");
        if (DtGridEstimate.Rows.Count > 0)
        {
            for (int ss = 0; DtGridEstimate.Rows.Count > ss; ss++)
            {
                //if (DtGridEstimate.Rows[ss]["FYear"].ToString() == "2020-21")
                //{
                //    EstimateQunFutureID.Value = DtGridEstimate.Rows[ss]["ProdQtyPriceId"].ToString();
                //    ddlfutyear1.SelectedValue = DtGridEstimate.Rows[ss]["Year"].ToString();
                //    txtfutQuantity1.Text = DtGridEstimate.Rows[ss]["EstimatedQty"].ToString();
                //    ddlfutunit1.SelectedValue = DtGridEstimate.Rows[ss]["Unit"].ToString();
                //    txtfutvalue1.Text = DtGridEstimate.Rows[ss]["EstimatedPrice"].ToString();
                //}
                if (DtGridEstimate.Rows[ss]["FYear"].ToString() == "2021-22")
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
                else if (DtGridEstimate.Rows[ss]["FYear"].ToString() == "Probable future import (with tentative value in rupees lakh)")
                {
                    EstimateQunFutureID6.Value = DtGridEstimate.Rows[ss]["ProdQtyPriceId"].ToString();
                    ddlfutyear6.SelectedValue = DtGridEstimate.Rows[ss]["Year"].ToString();
                    txtfutQuantity6.Text = DtGridEstimate.Rows[ss]["EstimatedQty"].ToString();
                    ddlfutunit6.SelectedValue = DtGridEstimate.Rows[ss]["Unit"].ToString();
                    txtfutvalue6.Text = DtGridEstimate.Rows[ss]["EstimatedPrice"].ToString();
                }
                else if (DtGridEstimate.Rows[ss]["FYear"].ToString() == "2025-26")
                {
                    EstimateQunFutureID7.Value = DtGridEstimate.Rows[ss]["ProdQtyPriceId"].ToString();
                    ddlfutyear7.SelectedValue = DtGridEstimate.Rows[ss]["Year"].ToString();
                    txtfutQuantity7.Text = DtGridEstimate.Rows[ss]["EstimatedQty"].ToString();
                    ddlfutunit7.SelectedValue = DtGridEstimate.Rows[ss]["Unit"].ToString();
                    txtfutvalue7.Text = DtGridEstimate.Rows[ss]["EstimatedPrice"].ToString();
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
        //if (txtestquan1.Text != "" && txtpriceestimate1.Text != "")
        //{
        //    dr = insert.NewRow();
        //    if (EstimateQunOldID.Value == "")
        //    {
        //        dr["ProdQtyPriceId"] = 0;
        //    }
        //    else
        //    {
        //        dr["ProdQtyPriceId"] = EstimateQunOldID.Value;
        //    }
        //    dr["Year"] = ddlyearestimate1.SelectedItem.Value;
        //    dr["FYear"] = ddlyearestimate1.SelectedItem.Text;
        //    dr["EstimatedQty"] = txtestquan1.Text;
        //    dr["Unit"] = ddlunit1.SelectedItem.Text;
        //    dr["EstimatedPrice"] = txtpriceestimate1.Text;
        //    dr["Type"] = "O";
        //    insert.Rows.Add(dr);
        //}
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
        if (txtestquan4.Text != "" && txtpriceestimate4.Text != "")
        {
            dr = insert.NewRow();
            if (EstimateQunOldID4.Value == "")
            {
                dr["ProdQtyPriceId"] = 0;
            }
            else
            {
                dr["ProdQtyPriceId"] = EstimateQunOldID4.Value;
            }
            dr["Year"] = ddlyearestimate4.SelectedItem.Value;
            dr["FYear"] = ddlyearestimate4.SelectedItem.Text;
            dr["EstimatedQty"] = txtestquan4.Text;
            dr["Unit"] = ddlunit4.SelectedItem.Text;
            dr["EstimatedPrice"] = txtpriceestimate4.Text;
            dr["Type"] = "O";
            insert.Rows.Add(dr);
        }
        try
        {
            if (insert.Rows.Count > 0)
            {
                decimal max = Convert.ToDecimal(insert.AsEnumerable().Max(row => row["EstimatedPrice"].ToString()));
                hfvaluelast3years.Value = max.ToString();
            }
            else
            {
                hfvaluelast3years.Value = "0.00";
            }
        }
        catch (Exception ex)
        { }
        return insert;
    }
    private void BindGridEstimateQuantity1()
    {
        DataTable DtGridEstimate1 = new DataTable();
        DtGridEstimate1 = Lo.RetriveSaveEstimateGrid("Selectp1", 0, hfprodrefno.Value, 0, "", "", "", "", "O");
        if (DtGridEstimate1.Rows.Count > 0)
        {
            for (int estimate = 0; DtGridEstimate1.Rows.Count > estimate; estimate++)
            {
                //if (DtGridEstimate1.Rows[estimate]["FYear"].ToString() == "2017-18")
                //{
                //    EstimateQunOldID.Value = DtGridEstimate1.Rows[estimate]["ProdQtyPriceId"].ToString();
                //    ddlyearestimate1.SelectedValue = DtGridEstimate1.Rows[estimate]["Year"].ToString();
                //    txtestquan1.Text = DtGridEstimate1.Rows[estimate]["EstimatedQty"].ToString();
                //    ddlunit1.SelectedValue = DtGridEstimate1.Rows[estimate]["Unit"].ToString();
                //    txtpriceestimate1.Text = DtGridEstimate1.Rows[estimate]["EstimatedPrice"].ToString();
                //}
                if (DtGridEstimate1.Rows[estimate]["FYear"].ToString() == "2018-19")
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
                else if (DtGridEstimate1.Rows[estimate]["FYear"].ToString() == "2020-21")
                {
                    EstimateQunOldID4.Value = DtGridEstimate1.Rows[estimate]["ProdQtyPriceId"].ToString();
                    ddlyearestimate4.SelectedValue = DtGridEstimate1.Rows[estimate]["Year"].ToString();
                    txtestquan4.Text = DtGridEstimate1.Rows[estimate]["EstimatedQty"].ToString();
                    ddlunit4.SelectedValue = DtGridEstimate1.Rows[estimate]["Unit"].ToString();
                    txtpriceestimate4.Text = DtGridEstimate1.Rows[estimate]["EstimatedPrice"].ToString();
                }
            }
        }
    }
    #endregion
    #region EditCodeForProduct
    protected void EditCode()
    {
        try
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
                    try
                    {
                        if (DtView.Rows[0]["Platform"].ToString() != "")
                        {
                            ddlplatform.Items.FindByValue(DtView.Rows[0]["Platform"].ToString()).Selected = true;
                            if (DtView.Rows[0]["NomenclatureOfMainSystem"].ToString() != "")
                            {
                                BindMasterProductNoenCletureCategory();
                                ddlnomnclature.SelectedValue = DtView.Rows[0]["NomenclatureOfMainSystem"].ToString();
                            }
                            else
                            {
                                ddlnomnclature.Items.Insert(0, "Select");
                            }
                        }
                    }
                    catch (Exception ex)
                    { ex.Message.ToString(); }
                    try
                    {
                        if (DtView.Rows[0]["TechnologyLevel1"].ToString() != "")
                        {
                            ddltechnologycat.Items.FindByValue(DtView.Rows[0]["TechnologyLevel1"].ToString()).Selected = true;
                            BindMasterSubCategoryTech();
                            if (DtView.Rows[0]["TechnologyLevel2"].ToString() != "")
                            {
                                ddlsubtech.Items.FindByValue(DtView.Rows[0]["TechnologyLevel2"].ToString()).Selected = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.Message.ToString();
                    }
                    rbproductImported.SelectedValue = DtView.Rows[0]["IsProductImported"].ToString().Trim();
                    BindGridEstimateQuantity1();
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
                    BindGridEstimateQuantity();
                    rbeoimake2.SelectedValue = DtView.Rows[0]["EOIStatus"].ToString().Trim();
                    if (rbeoimake2.SelectedValue.Trim() == "Yes")
                    {
                        txteoilink.Text = DtView.Rows[0]["EOIURL"].ToString();
                        if (DtView.Rows[0]["EOIStartDate"].ToString() != "")
                            txteoistartdate.Text = DtView.Rows[0]["EOIStartDate"].ToString();
                        if (DtView.Rows[0]["EOIEndDate"].ToString() != "")
                            txteoienddate.Text = DtView.Rows[0]["EOIEndDate"].ToString();
                        eoi.Visible = true;
                    }
                    if (rbeoimake2.SelectedValue.Trim() == "Archive")
                    {
                        txteoilink.Text = DtView.Rows[0]["EOIURL"].ToString();
                        if (DtView.Rows[0]["EOIStartDate"].ToString() != "")
                            txteoistartdate.Text = DtView.Rows[0]["EOIStartDate"].ToString();
                        if (DtView.Rows[0]["EOIEndDate"].ToString() != "")
                            txteoienddate.Text = DtView.Rows[0]["EOIEndDate"].ToString();
                        eoi.Visible = true;
                    }
                    if (rbeoimake2.SelectedValue.Trim() == "Active")
                    {
                        txteoilink.Text = DtView.Rows[0]["EOIURL"].ToString();
                        if (DtView.Rows[0]["EOIStartDate"].ToString() != "")
                            txteoistartdate.Text = DtView.Rows[0]["EOIStartDate"].ToString();
                        if (DtView.Rows[0]["EOIEndDate"].ToString() != "")
                            txteoienddate.Text = DtView.Rows[0]["EOIEndDate"].ToString();
                        eoi.Visible = true;
                    }
                    if (rbeoimake2.SelectedValue.Trim() == "No")
                    {
                        eoi.Visible = false;
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
                        chkinditargetyear.SelectedValue = DtView.Rows[0]["IndTargetYear"].ToString().Trim();
                    }
                    chkindiprocstart.SelectedValue = DtView.Rows[0]["IndProcess"].ToString().Trim().Trim();
                    if (chkindiprocstart.SelectedValue == "Yes")
                    {
                        if (DtView.Rows[0]["PurposeofProcurement"].ToString() != "")
                        {
                            DataTable DTporCat = Lo.RetriveProductCode("", hfprodrefno.Value, "ProductPOP", hidType.Value);
                            if (DTporCat.Rows.Count > 0)
                            {
                                rbIgCategory.SelectedValue = DTporCat.Rows[0]["SCategoryId"].ToString();
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
                                rbIgCategory.SelectedValue = DTporCat.Rows[0]["SCategoryId"].ToString();                              
                                chkindiprocstart.SelectedValue = "Yes";
                            }
                        }
                        else
                        {
                            indicatchk.Visible = false;
                        }
                    }
                    if (DtView.Rows[0]["ViewOnlyStatus"].ToString().Trim() == "Yes")
                    { rbViewOnlyStatus.SelectedValue = DtView.Rows[0]["ViewOnlyReasone"].ToString().Trim(); }
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
                    rbeligible.SelectedValue = DtView.Rows[0]["IsShowGeneral"].ToString().Trim();
                    rbisindinised.SelectedValue = DtView.Rows[0]["IsIndeginized"].ToString().Trim();
                    if (rbisindinised.SelectedItem.Value == "Y")
                    {
                        txtmanufacturename.Text = DtView.Rows[0]["ManufactureName"].ToString();
                        txtmanifacaddress.Text = DtView.Rows[0]["ManufactureAddress"].ToString();
                        ddlyearofindiginization.SelectedValue = DtView.Rows[0]["YearofIndiginization"].ToString();
                        if (DtView.Rows[0]["IndeginizedDate"].ToString() != "")
                        {
                            DateTime mdtedit = Convert.ToDateTime(DtView.Rows[0]["IndeginizedDate"].ToString());
                            string msmdtedit = mdtedit.ToString("MMM");
                            ddlmonth.SelectedValue = msmdtedit.ToString();
                        }
                        else
                        {
                            DateTime dateeditmonth = Convert.ToDateTime(DateTime.Now);
                            string datemonthatm = dateeditmonth.ToString("MMM");
                            ddlmonth.SelectedValue = datemonthatm.ToString();
                        }
                        if (DtView.Rows[0]["IndeginizedDate"].ToString() != "")
                        {
                            DateTime mdtedityr = Convert.ToDateTime(DtView.Rows[0]["IndeginizedDate"].ToString());
                            string msyredit = mdtedityr.ToString("yyyy");
                            ddlyear.SelectedValue = msyredit.ToString();
                        }
                        else
                        {
                            DateTime dateedityear = Convert.ToDateTime(DateTime.Now);
                            string dateyratm = dateedityear.ToString("yyyy");
                            ddlyear.SelectedValue = dateyratm.ToString();
                        }
                        DataTable dtMaxValue = Lo.RetriveFilterCode("", hfprodrefno.Value, "FindMax");
                        if (dtMaxValue.Rows.Count > 0)
                        {
                            txtmaxvalue.Text = dtMaxValue.Rows[0]["MaxValue"].ToString();
                        }
                        else
                        {
                            txtmaxvalue.Text = "0";
                        }
                        divisIndigenized.Visible = true;
                        DivAtmnirbhar.Visible = true;
                    }
                    else
                    {
                        divisIndigenized.Visible = false;
                        DivAtmnirbhar.Visible = false;
                    }
                    if (DtView.Rows[0]["SupplyOrderStatus"].ToString().Trim() == "Yes")
                    {
                        rbSuuplyOrder.SelectedValue = DtView.Rows[0]["SupplyOrderStatus"].ToString().Trim();
                        txtsupplyorderrplkh.Text = DtView.Rows[0]["SupplyOrderValue"].ToString();
                        txtdeliverycompdate.Text = DtView.Rows[0]["SupplyDeliveryDate"].ToString();
                        txtsodate.Text = DtView.Rows[0]["SupplyOrderDate"].ToString();
                        txtsupplumanufacturename.Text = DtView.Rows[0]["SupplyManfutureName"].ToString();
                        txtsupplymanufactureaddress.Text = DtView.Rows[0]["SupplyManfutureAddress"].ToString();
                        supplyorder.Visible = true;
                        DivAtmnirbhar.Visible = true;
                    }
                    else
                    {
                        rbSuuplyOrder.SelectedValue = DtView.Rows[0]["SupplyOrderStatus"].ToString();
                        supplyorder.Visible = false;
                    }
                    if (DtView.Rows[0]["SanctionOrderStatus"].ToString().Trim() == "Yes")
                    {
                        rbSuuplyOrder.SelectedValue = DtView.Rows[0]["SanctionOrderStatus"].ToString().Trim();
                        Rblsanction.SelectedValue = DtView.Rows[0]["SanctionReason"].ToString();
                        Txtorderdate.Text = DtView.Rows[0]["SanctionOrderDate"].ToString();
                        txtnamesanction.Text = DtView.Rows[0]["SanctionManfutureName"].ToString();
                        TxtAddresssanction.Text = DtView.Rows[0]["SanctionManfutureAddress"].ToString();
                        divsanction.Visible = true;
                    }
                    else
                    {
                        rbSuuplyOrder.SelectedValue = DtView.Rows[0]["SanctionOrderStatus"].ToString();
                        divsanction.Visible = false;
                    }
                    //New Code Imp 
                    if (DtView.Rows[0]["PurposeofProcurement"].ToString() == "58264" || DtView.Rows[0]["PurposeofProcurement"].ToString() == "58270")
                    {
                        if (rbisindinised.SelectedItem.Value == "Y")
                        {
                            if (ddlcompany.Visible == true && ddldivision.Visible == true && ddlunit.Visible == true)
                            { txtmanufacturename.Text = ddlunit.SelectedItem.Text; }
                            else if (ddlcompany.Visible == true && ddldivision.Visible == true)
                            { txtmanufacturename.Text = ddldivision.SelectedItem.Text; }
                            else if (ddlcompany.Visible == true)
                            { txtmanufacturename.Text = ddlcompany.SelectedItem.Text; }
                            txtmanifacaddress.Text = DtView.Rows[0]["ManufactureAddress"].ToString();
                            ddlyearofindiginization.SelectedValue = DtView.Rows[0]["YearofIndiginization"].ToString();
                        }
                    }
                    if (hfcomprefno.Value == "C0023" || hfcomprefno.Value == "C0024" || hfcomprefno.Value == "C0025")
                    {
                        foreach (ListItem chki in chklistdeclarationimage.Items)
                        {
                            chki.Selected = false;
                        }
                    }
                    else
                    {
                        foreach (ListItem chki in chklistdeclarationimage.Items)
                        {
                            chki.Selected = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        { ExceptionLogging.SendErrorToText(ex); }
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
    #region Exceptionlog
    public static class ExceptionLogging
    {
        private static String ErrorlineNo, Errormsg, extype, exurl, hostIp, ErrorLocation, HostAdd;
        public static void SendErrorToText(Exception ex)
        {
            var line = Environment.NewLine + Environment.NewLine;
            ErrorlineNo = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
            Errormsg = ex.GetType().Name.ToString();
            extype = ex.GetType().ToString();
            exurl = context.Current.Request.Url.ToString();
            ErrorLocation = ex.Message.ToString();
            try
            {
                string filepath = context.Current.Server.MapPath("/Logs/");  //Text File Path
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                filepath = filepath + DateTime.Today.ToString("dd-MM-yy") + ".txt";   //Text File Name
                if (!File.Exists(filepath))
                {
                    File.Create(filepath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    string error = "Log Written Date:" + " " + DateTime.Now.ToString() + line + "Error Line No :" + " " + ErrorlineNo + line + "Error Message:" + " " + Errormsg + line + "Exception Type:" + " " + extype + line + "Error Location :" + " " + ErrorLocation + line + " Error Page Url:" + " " + exurl + line + "User Host IP:" + " " + hostIp + line;
                    sw.WriteLine("-----------Exception Details on " + " " + DateTime.Now.ToString() + "-----------------");
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                    sw.WriteLine(line);
                    sw.WriteLine(error);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    sw.WriteLine(line);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception ex1)
            {
                ExceptionLogging.SendErrorToText(ex1);
            }
        }
    }
    #endregion
    protected void RBLProjectSanction_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RBLProjectSanction.SelectedValue.ToString() == "Yes")
        {
            divrblsanction.Visible = true;
            divsanction.Visible = true;
            DivAtmnirbhar.Visible = true;
        }
        else
        {
            divrblsanction.Visible = false;
            divsanction.Visible = false;
            DivAtmnirbhar.Visible = false;
        }
    }
}