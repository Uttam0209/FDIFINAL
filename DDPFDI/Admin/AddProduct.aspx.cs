using BusinessLayer;
using Encryption;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web;
using System.IO;

public partial class Admin_AddProduct : System.Web.UI.Page
{
    Cryptography objEnc = new Cryptography();
    DataUtility Co = new DataUtility();
    Logic Lo = new Logic();
    DataTable dtImage = new DataTable();
    private string DisplayPanel = "";
    string _msg = string.Empty;
    string _sysMsg = string.Empty;
    public string Services = "";
    public string Remarks = "";
    public string NodalDDL = "";
    string UserName;
    string RefNo;
    string UserEmail;
    string currentPage = "";
    private string mType = "";
    private string mRefNo = "";
    private Int16 Mid = 0;
    DataTable DtCompanyDDL = new DataTable();
    HybridDictionary HyPanel1 = new HybridDictionary();
    protected void Page_Load(object sender, EventArgs e)
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
                    divHeadPage.InnerHtml = strheadPage.ToString();
                    strheadPage.Append("</ul");
                    mType = objEnc.DecryptData(Session["Type"].ToString());
                    mRefNo = Session["CompanyRefNo"].ToString();
                    hfcomprefno.Value = Session["CompanyRefNo"].ToString();
                    BindCompany();
                    BindMasterCategory();
                    BindMasterTechnologyCategory();
                    BindMasterPlatCategory();
                    BindMasterProductReqCategory();
                    BindMasterProductNoenCletureCategory();
                    // BindNodelEmail();
                    BindServcies();
                    BindEndUser();
                    tendorstatus();
                }
                EditCode();
            }
            catch (Exception ex)
            {
                Response.RedirectToRoute("Login");
            }
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
                ddlcompany.Enabled = false;
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
                if (mType == "Company")
                {
                    lblselectdivison.Visible = true;
                    ddldivision.Enabled = true;
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
                BindNodelEmail();
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
                ddlunit.Items.Insert(0, "Select");
                ddlunit.Enabled = true;
                lblselectunit.Visible = true;
            }
            else
            {
                lblselectunit.Visible = false;
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
                BindNodelEmail();
                ddlunit.Enabled = false;
                lblselectunit.Visible = true;
            }
            else
            {
                ddlunit.Enabled = false;
            }
        }
    }
    protected void BindNodelEmail()
    {
        if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.SelectedItem.Text == "Select")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "", 0, "", "", "CompanyNodelDetail");
        }
        else if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.SelectedItem.Text != "Select" && ddlunit.SelectedItem.Text == "Select")
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
            ddlNodalOfficerEmail.Items.Insert(0, "Select");
            ddlNodalOfficerEmail2.Items.Insert(0, "Select");
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
                lblselectdivison.Visible = true;
                ddldivision.Visible = true;
                hidCompanyRefNo.Value = ddlcompany.SelectedItem.Value;
                hidType.Value = "Company";
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
                lblselectunit.Visible = true;
                hidCompanyRefNo.Value = ddldivision.SelectedItem.Value;
                hidType.Value = "Divison/Plant";
            }
            else
            {
                lblselectunit.Visible = false;
                ddlunit.Visible = false;
            }
            hfcomprefno.Value = "";
            hfcomprefno.Value = ddldivision.SelectedItem.Value;
            BindNodelEmail();
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
            }
            hfcomprefno.Value = "";
            hfcomprefno.Value = ddlcompany.SelectedItem.Value;
            BindNodelEmail();
        }

    }
    protected void ddlunit_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        hidCompanyRefNo.Value = ddlunit.SelectedItem.Value;
        hidType.Value = "Unit";
        hfcomprefno.Value = "";
        hfcomprefno.Value = ddlunit.SelectedItem.Value;
        BindNodelEmail();
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
                DtCompanyDDL = Lo.RetriveMasterData(0, hfcomprefno.Value, ddlNodalOfficerEmail.SelectedItem.Value, 0, "", "", "AllNodelNotSelect");
                if (DtCompanyDDL.Rows.Count > 0)
                {
                    Co.FillDropdownlist(ddlNodalOfficerEmail2, DtCompanyDDL, "NodalOficerName", "NodalOfficerID");
                    ddlNodalOfficerEmail2.Items.Insert(0, "Select");
                }
                else
                {
                    divnodal2.Visible = true;
                }
            }
        }
        else
        {
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
                DtCompanyDDL = Lo.RetriveMasterData(0, hfcomprefno.Value, ddlNodalOfficerEmail.SelectedItem.Value, 0, "", "", "AllNodelNotSelect");
                if (DtCompanyDDL.Rows.Count > 0)
                {
                    Co.FillDropdownlist(ddlNodalOfficerEmail, DtCompanyDDL, "NodalOficerName", "NodalOfficerID");
                    ddlNodalOfficerEmail2.Items.Insert(0, "Select");
                }
                else
                {
                    divnodal.Visible = true;
                }
            }
        }
        else
        {
        }
    }
    protected void ddlmastercategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMasterSubCategory();
    }
    protected void ddlsubcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMaster3levelSubCategory();
    }
    protected void ddltechnologycat_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMasterSubCategoryTech();
    }
    protected void ddlsubtech_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMasterSubCategoryTechLevel3();
    }
    protected void ddlplatform_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMasterSubCategoryPlat();
    }
    protected void rbisindinised_CheckedChanged(object sender, EventArgs e)
    {
        if (rbisindinised.SelectedItem.Value == "Y")
        {
            txtmanufacturename.Visible = true;
        }
        else if (rbisindinised.SelectedItem.Value == "N")
        {
            txtmanufacturename.Visible = false;
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
    protected void tendorstatus()
    {
        if (ddltendorstatus.SelectedItem.Value == "Live" && rbtendordateyesno.SelectedItem.Value == "Y")
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
    #endregion
    #region BindServices
    protected void BindServcies()
    {
        DataTable Dtservices = Lo.RetriveMasterSubCategoryDate(0, "Support Provided by DPSU", "", "SelectInnerMaster1", hfcomprefno.Value);
        if (Dtservices.Rows.Count > 0)
        {
            gvservices.DataSource = Dtservices;
            gvservices.DataBind();
        }
    }
    #endregion
    #region For ProductCode
    protected void BindMasterCategory()
    {
        ddlmastercategory.Items.Insert(0, "Product Category");
        ddlsubcategory.Items.Insert(0, "Select");
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, ddlmastercategory.SelectedItem.Value, "", "SelectProductCat", hfcomprefno.Value);
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlmastercategory, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlmastercategory.Items.Insert(0, "Select");
        }
    }
    protected void BindMasterSubCategory()
    {
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(ddlmastercategory.SelectedItem.Value), "", "", "SubSelectID", "");
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
        DataTable DtMasterCategroyLevel3 = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(ddlsubcategory.SelectedItem.Value), "", "", "SubSelectID", "");
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
    #endregion
    #region For Technology
    protected void BindMasterTechnologyCategory()
    {
        ddltechnologycat.Items.Insert(0, "Technology Category");
        ddlsubtech.Items.Insert(0, "Select");
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, ddltechnologycat.SelectedItem.Value, "", "SelectProductCat", hfcomprefno.Value);
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddltechnologycat, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddltechnologycat.Items.Insert(0, "Select");
        }
    }
    protected void BindMasterSubCategoryTech()
    {
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(ddltechnologycat.SelectedItem.Value), "", "", "SubSelectID", "");
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
        DataTable DtMasterCategroyLevel3 = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(ddlsubtech.SelectedItem.Value), "", "", "SubSelectID", "");
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
    #endregion
    #region For PlatForm
    protected void BindMasterPlatCategory()
    {
        ddlplatform.Items.Insert(0, "Platform Category");
        ddlplatformsubcat.Items.Insert(0, "Select");
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, ddlplatform.SelectedItem.Value, "", "SelectProductCat", hfcomprefno.Value);
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlplatform, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlplatform.Items.Insert(0, "Select");
        }
    }
    protected void BindMasterSubCategoryPlat()
    {
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(ddlplatform.SelectedItem.Value), "", "", "SubSelectID", "");
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlplatformsubcat, DtMasterCategroy, "SCategoryName", "SCategoryId");
            ddlplatformsubcat.Items.Insert(0, "Select");
        }
        else
        {
            ddlplatformsubcat.Items.Clear();
            ddlplatformsubcat.Items.Insert(0, "Select");
        }
    }
    #endregion  
    #region For ProductRequirment
    protected void BindMasterProductReqCategory()
    {
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, lblprodrequir.Text, "", "SelectInnerMaster1", hfcomprefno.Value);
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlprodreqir, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlprodreqir.Items.Insert(0, "Select");
        }
    }
    #endregion
    #region For NomenClature
    protected void BindMasterProductNoenCletureCategory()
    {
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, lblNomenclature.Text, "", "SelectInnerMaster1", hfcomprefno.Value);
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlnomnclature, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlnomnclature.Items.Insert(0, "Select");
        }
        else
        {
            ddlnomnclature.Items.Insert(0, "Select");
        }

    }
    #endregion
    #region For EndUser
    protected void BindEndUser()
    {
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, lblenduser.Text, "", "SelectInnerMaster1", hfcomprefno.Value);
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlenduser, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlenduser.Items.Insert(0, "Select");
        }
        else
        {
            ddlenduser.Items.Insert(0, "Select");
        }
    }
    #endregion
    #region PanelSaveCode
    protected void SaveProductDescription()
    {
        if (hfprodid.Value != "")
        {
            HyPanel1["ProductID"] = Convert.ToInt16(hfprodid.Value);
            HyPanel1["ProductRefNo"] = Co.RSQandSQLInjection(hfprodrefno.Value, "soft");
        }
        else
        {
            HyPanel1["ProductID"] = 0;
        }
        if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.SelectedItem.Text == "Select")
        {
            HyPanel1["CompanyRefNo"] = Co.RSQandSQLInjection(ddlcompany.SelectedItem.Value, "soft");
            mType = "Company";
            HyPanel1["Role"] = mType.ToString();
        }
        else if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.SelectedItem.Text != "Select" && ddlunit.SelectedItem.Text == "Select")
        {
            HyPanel1["CompanyRefNo"] = Co.RSQandSQLInjection(ddldivision.SelectedItem.Value, "soft");
            mType = "Factory";
            HyPanel1["Role"] = mType.ToString();
        }
        else if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.SelectedItem.Text != "Select" && ddlunit.SelectedItem.Text != "Select")
        {
            HyPanel1["CompanyRefNo"] = Co.RSQandSQLInjection(ddlunit.SelectedItem.Value, "soft");
            mType = "Unit";
            HyPanel1["Role"] = mType.ToString();
        }
        else
        {
            HyPanel1["CompanyRefNo"] = Co.RSQandSQLInjection(hfcomprefno.Value, "soft");
        }
        HyPanel1["OEMPartNumber"] = Co.RSQandSQLInjection(txtoempartnumber.Text, "soft");
        HyPanel1["DPSUPartNumber"] = Co.RSQandSQLInjection(txtdpsupartnumber.Text, "soft");
        HyPanel1["EndUserPartNumber"] = Co.RSQandSQLInjection(txtenduserpartnumber.Text, "soft");
        HyPanel1["HSNCode"] = Co.RSQandSQLInjection(txthsncode.Text, "soft");
        HyPanel1["NatoCode"] = Co.RSQandSQLInjection(txtnatocode.Text, "soft");
        HyPanel1["ERPRefNo"] = Co.RSQandSQLInjection(txterprefno.Text, "soft");
        if (ddlnomnclature.SelectedItem.Value != "Select")
        {
            HyPanel1["NomenclatureOfMainSystem"] = Co.RSQandSQLInjection(ddlnomnclature.SelectedItem.Value, "soft");
        }
        else
        {
            HyPanel1["NomenclatureOfMainSystem"] = 0;
        }
        if (ddlmastercategory.SelectedItem.Value != "Select")
        {
            HyPanel1["ProductLevel1"] = Co.RSQandSQLInjection(ddlmastercategory.SelectedItem.Value, "soft");
        }
        else
        {
            HyPanel1["ProductLevel1"] = 0;
        }
        if (ddlsubcategory.SelectedItem.Value != "Select")
        {
            HyPanel1["ProductLevel2"] = Co.RSQandSQLInjection(ddlsubcategory.SelectedItem.Value, "soft");
        }
        else
        {
            HyPanel1["ProductLevel2"] = 0;
        }
        if (ddllevel3product.SelectedItem.Value != "Select")
        {
            HyPanel1["ProductLevel3"] = Co.RSQandSQLInjection(ddllevel3product.SelectedItem.Value, "soft");
        }
        else
        {
            HyPanel1["ProductLevel3"] = 0;
        }
        HyPanel1["ProductDescription"] = Co.RSQandSQLInjection(txtproductdescription.Text, "soft");
        if (ddltechnologycat.SelectedItem.Value != "Select")
        {
            HyPanel1["TechnologyLevel1"] = Co.RSQandSQLInjection(ddltechnologycat.SelectedItem.Value, "soft");
        }
        else
        {
            HyPanel1["TechnologyLevel1"] = 0;
        }
        if (ddlsubtech.SelectedItem.Value != "Select")
        {
            HyPanel1["TechnologyLevel2"] = Co.RSQandSQLInjection(ddlsubtech.SelectedItem.Value, "soft");
        }
        else
        {
            HyPanel1["TechnologyLevel2"] = 0;
        }
        if (ddltechlevel3.SelectedItem.Value != "Select")
        {
            HyPanel1["TechnologyLevel3"] = Co.RSQandSQLInjection(ddltechlevel3.SelectedItem.Value, "soft");
        }
        else
        {
            HyPanel1["TechnologyLevel3"] = 0;
        }
        if (ddlenduser.SelectedItem.Value != "Select")
        {
            HyPanel1["EndUser"] = Co.RSQandSQLInjection(ddlenduser.SelectedItem.Value, "soft");
        }
        else
        {
            HyPanel1["EndUser"] = 0;
        }
        if (ddlplatform.SelectedItem.Value != "Select")
        {
            HyPanel1["Platform"] = Co.RSQandSQLInjection(ddlplatform.SelectedItem.Value, "soft");
        }
        else
        {
            HyPanel1["Platform"] = 0;
        }
        if (ddlplatformsubcat.SelectedItem.Value != "Select")
        {
            HyPanel1["PurposeofProcurement"] = Co.RSQandSQLInjection(ddlplatformsubcat.SelectedItem.Value, "soft");
        }
        else
        {
            HyPanel1["PurposeofProcurement"] = 0;
        }
        if (ddlnomnclature.SelectedItem.Value != "Select")
        {
            HyPanel1["ProductRequirment"] = Co.RSQandSQLInjection(ddlprodreqir.SelectedItem.Value, "soft");
        }
        else
        {
            HyPanel1["ProductRequirment"] = 0;
        }
        HyPanel1["IsIndeginized"] = Co.RSQandSQLInjection(rbisindinised.SelectedItem.Value, "soft");
        HyPanel1["ManufactureName"] = Co.RSQandSQLInjection(txtmanufacturename.Text, "soft");
        HyPanel1["SearchKeyword"] = Co.RSQandSQLInjection(txtsearchkeyword.Text, "soft");
        if (files.HasFiles != false)
        {
            if (hfprodid.Value != "")
            {
                DataTable dtImageBind = Lo.RetriveProductCode("", hfprodrefno.Value, "RetriveImage");
                if (dtImageBind.Rows.Count > 0)
                {
                    Int16 CountImageTotal = Convert.ToInt16(files.PostedFiles.Count);
                    Int16 AlreadyUploadImage = Convert.ToInt16(dtImageBind.Rows.Count);
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
        foreach (GridViewRow rw in gvservices.Rows)
        {
            CheckBox chkBx = (CheckBox)rw.FindControl("chk");
            HiddenField hfservicesid = (HiddenField)rw.FindControl("hfservicesid");
            TextBox txtRemarks = (TextBox)rw.FindControl("txtRemarks");
            if (chkBx != null && chkBx.Checked)
            {
                Services = Services + "," + hfservicesid.Value + ",";
                Remarks = Remarks + "," + txtRemarks.Text + ",";
            }
        }
        if (Services != "")
        {
            HyPanel1["DPSUServices"] = Co.RSQandSQLInjection(Services.Substring(1).ToString(), "soft");
            HyPanel1["Remarks"] = Co.RSQandSQLInjection(Remarks.Substring(1).ToString(), "soft");
        }
        else
        {
            HyPanel1["DPSUServices"] = "";
            HyPanel1["Remarks"] = "";
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
        if (ddlNodalOfficerEmail.Text == "" && ddlNodalOfficerEmail2.Text == "")
        {
            HyPanel1["NodelDetail"] = null;
        }
        else if (ddlNodalOfficerEmail.SelectedItem.Text != "Select" && ddlNodalOfficerEmail2.SelectedItem.Text == "Select")
        {
            HyPanel1["NodelDetail"] = Co.RSQandSQLInjection(ddlNodalOfficerEmail.SelectedItem.Value, "soft") + ",";
        }
        else if (ddlNodalOfficerEmail.SelectedItem.Text == "Select" && ddlNodalOfficerEmail2.SelectedItem.Text != "Select")
        {
            HyPanel1["NodelDetail"] = Co.RSQandSQLInjection(ddlNodalOfficerEmail2.SelectedItem.Value, "soft") + ",";
        }
        else if (ddlNodalOfficerEmail.SelectedItem.Text != "Select" && ddlNodalOfficerEmail2.SelectedItem.Text != "Select")
        {
            HyPanel1["NodelDetail"] = Co.RSQandSQLInjection(ddlNodalOfficerEmail.SelectedItem.Value, "soft") + "," +
                                      Co.RSQandSQLInjection(ddlNodalOfficerEmail2.SelectedItem.Value, "soft") + ",";
        }

        string StrProductDescription = Lo.SaveCodeProduct(HyPanel1, dtImage, out _sysMsg, out _msg, "Product");
        if (StrProductDescription != "-1")
        {
            if (btnsubmitpanel1.Text != "Update")
            {
                Cleartext();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record Saved.')", true);
            }
            else
            {
                Cleartext();
                btnsubmitpanel1.Text = "Save";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record updated successfully.')", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not saved.')", true);
        }
    }
    #endregion
    #region PanelSaveButtonCode
    protected void btnsubmitpanel1_Click(object sender, EventArgs e)
    {
        if (txtoempartnumber.Text != "")
        {
            SaveProductDescription();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Please fill OEM Part Number')", true);
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
        txtdpsupartnumber.Text = "";
        txtenduserpartnumber.Text = "";
        txthsncode.Text = "";
        txtnatocode.Text = "";
        txterprefno.Text = "";
        ddlnomnclature.SelectedIndex = 0;
        ddlmastercategory.SelectedIndex = 0;
        ddlsubcategory.SelectedIndex = 0;
        txtproductdescription.Text = "";
        ddltechnologycat.SelectedIndex = 0;
        ddlsubtech.SelectedIndex = 0;
        ddlenduser.SelectedIndex = 0;
        ddlplatform.SelectedIndex = 0;
        ddlplatformsubcat.SelectedIndex = 0;
        ddlprodreqir.SelectedIndex = 0;
        rbisindinised.SelectedIndex = 0;
        txtmanufacturename.Text = "";
        txtsearchkeyword.Text = "";
        txtestimatequantity.Text = "";
        txtestimateprice.Text = "";
        ddltendorstatus.SelectedIndex = 0;
        txttendordate.Text = "";
        txttendorurl.Text = "";
        if (ddlNodalOfficerEmail.Text != "")
        {
            ddlNodalOfficerEmail.SelectedIndex = 0;
            ddlNodalOfficerEmail2.SelectedIndex = 0;
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
    }
    #endregion
    #region Image Code
    private int ImageMaxCount;
    protected DataTable imagedb()
    {
        HttpFileCollection uploadedFiles = Request.Files;
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("ImageID", typeof(Int64)));
        dt.Columns.Add(new DataColumn("ImageName", typeof(string)));
        dt.Columns.Add(new DataColumn("ImageType", typeof(string)));
        dt.Columns.Add(new DataColumn("ImageActualSize", typeof(Int64)));
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
                    if (FileSize < 1024)
                    {
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
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return dt;
        }
    }
    #endregion
    #region EditCodeForProduct
    protected void EditCode()
    {
        if (Request.QueryString["mcurrentcompRefNo"] != null)
        {
            hfprodrefno.Value = objEnc.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString());
            DataTable DtView = Lo.RetriveProductCode("", hfprodrefno.Value, "ProductMasterID");
            if (DtView.Rows.Count > 0)
            {
                btnsubmitpanel1.Text = "Update";
                hfprodid.Value = DtView.Rows[0]["ProductID"].ToString();
                hfcomprefno.Value = DtView.Rows[0]["CompanyRefNo"].ToString();
                txtoempartnumber.Text = DtView.Rows[0]["OEMPartNumber"].ToString();
                txtdpsupartnumber.Text = DtView.Rows[0]["DPSUPartNumber"].ToString();
                txtenduserpartnumber.Text = DtView.Rows[0]["EndUserPartNumber"].ToString();
                txthsncode.Text = DtView.Rows[0]["HSNCode"].ToString();
                txtnatocode.Text = DtView.Rows[0]["NatoCode"].ToString();
                txterprefno.Text = DtView.Rows[0]["ERPRefNo"].ToString();
                ddlnomnclature.SelectedValue = DtView.Rows[0]["NomenclatureOfMainSystem"].ToString();
                ddlmastercategory.SelectedValue = DtView.Rows[0]["ProductLevel1"].ToString();
                BindMasterSubCategory();
                ddlsubcategory.SelectedValue = DtView.Rows[0]["ProductLevel2"].ToString();
                BindMasterSubCategoryTechLevel3();
                ddllevel3product.SelectedValue = DtView.Rows[0]["ProductLevel3"].ToString();
                txtproductdescription.Text = DtView.Rows[0]["ProductDescription"].ToString();
                ddltechnologycat.SelectedValue = DtView.Rows[0]["TechnologyLevel1"].ToString();
                BindMasterSubCategoryTech();
                ddlsubtech.SelectedValue = DtView.Rows[0]["TechnologyLevel2"].ToString();
                BindMasterSubCategoryTechLevel3();
                ddltechlevel3.SelectedValue = DtView.Rows[0]["TechnologyLevel3"].ToString();
                ddlenduser.SelectedValue = DtView.Rows[0]["EndUser"].ToString();
                ddlplatform.SelectedValue = DtView.Rows[0]["Platform"].ToString();
                BindMasterSubCategoryPlat();
                ddlplatformsubcat.SelectedValue = DtView.Rows[0]["PurposeofProcurement"].ToString();
                ddlprodreqir.SelectedValue = DtView.Rows[0]["ProductRequirment"].ToString();
                rbisindinised.SelectedValue = DtView.Rows[0]["IsIndeginized"].ToString();
                if (rbisindinised.SelectedItem.Value == "Y")
                {
                    txtmanufacturename.Visible = true;
                    txtmanufacturename.Text = DtView.Rows[0]["ManufactureName"].ToString();
                }
                txtsearchkeyword.Text = DtView.Rows[0]["SearchKeyword"].ToString();
                DataTable dtImageBind = Lo.RetriveProductCode("", hfprodrefno.Value, "ProductImage");
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
                DataTable dtpsdq = Lo.RetriveProductCode("", hfprodrefno.Value, "ProductPSDQ");
                if (dtpsdq.Rows.Count > 0)
                {
                    for (int i = 0; dtpsdq.Rows.Count > i; i++)
                    {
                        foreach (GridViewRow rw in gvservices.Rows)
                        {
                            CheckBox chkBx = (CheckBox)rw.FindControl("chk");
                            HiddenField hfservicesid = (HiddenField)rw.FindControl("hfservicesid");
                            //TextBox txtRemarks = (TextBox)rw.FindControl("txtRemarks");
                            if (hfservicesid.Value == dtpsdq.Rows[i]["SCategoryId"].ToString())
                            {
                                chkBx.Checked = true;
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
                    txttendordate.Text = DtView.Rows[0]["TenderFillDate"].ToString();
                    txttendorurl.Text = DtView.Rows[0]["TenderUrl"].ToString();
                }
                else
                {
                    divtdate.Visible = false;
                }
                BindNodelEmail();
                DataTable dtNodal = Lo.RetriveProductCode("", hfprodrefno.Value, "ProductNodal");
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
                    DataTable dtImageBind = Lo.RetriveProductCode("", hfprodrefno.Value, "ProductImage");
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
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not deleted.')", true);
            }
        }
    }
    #endregion
}