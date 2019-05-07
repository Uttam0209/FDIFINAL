using BusinessLayer;
using Encryption;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web.UI;

public partial class Admin_AddProduct : System.Web.UI.Page
{
    Cryptography objEnc = new Cryptography();
    DataUtility Co = new DataUtility();
    Logic Lo = new Logic();
    private string DisplayPanel = "";
    private string SessionID = "";
    string _msg = string.Empty;
    string _sysMsg = string.Empty;
    DataTable DtCompanyDDL = new DataTable();
    HybridDictionary HyPanel1 = new HybridDictionary();
    HybridDictionary HyPanel2 = new HybridDictionary();
    HybridDictionary HyPanel3 = new HybridDictionary();
    HybridDictionary HyPanel4 = new HybridDictionary();
    HybridDictionary HyPanel5 = new HybridDictionary();
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
                        strheadPage.Append("<li class=''><span>" + MmCval + "</span></li>");
                    }

                    divHeadPage.InnerHtml = strheadPage.ToString();
                    strheadPage.Append("</ul");
                    SessionID = Session["CompanyRefNo"].ToString();
                    BindMasterCategory();
                    BindMasterTechnologyCategory();
                    BindMasterPlatCategory();
                    BindMasterProductReqCategory();
                    BindMasterProductNoenCletureCategory();
                    BindNodelEmail();
                }
            }
            catch (Exception ex)
            {
                Response.RedirectToRoute("Login");
            }
        }
    }
    protected void BindNodelEmail()
    {
        DtCompanyDDL = Lo.RetriveMasterData(0, Session["CompanyRefNo"].ToString(), "", 0, "", "", "AllNodel");
        if (DtCompanyDDL.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlNodalOfficerEmail, DtCompanyDDL, "NodalOficerName", "NodalOfficerID");
            ddlNodalOfficerEmail.Items.Insert(0, "Select Nodel Officer");
            Co.FillDropdownlist(ddlNodalOfficerEmail2, DtCompanyDDL, "NodalOficerName", "NodalOfficerID");
            ddlNodalOfficerEmail2.Items.Insert(0, "Select Nodel Officer");
        }
    }
    protected void ddlNodalOfficerEmail_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlNodalOfficerEmail.SelectedItem.Text != "Select Nodel Officer")
        {
            DataTable DtGetNodel = Lo.RetriveMasterData(Convert.ToInt16(ddlNodalOfficerEmail.SelectedItem.Value), "", "", 0, "", "", "CompleteNodelDetail");
            if (DtGetNodel.Rows.Count > 0)
            {
                txtNName.Text = DtGetNodel.Rows[0]["NodalOficerName"].ToString();
                txtNEmailId.Text = DtGetNodel.Rows[0]["NodalOfficerEmail"].ToString();
                txtNTelephone.Text = DtGetNodel.Rows[0]["NodalOfficerTelephone"].ToString();
                txtNFaxNo.Text = DtGetNodel.Rows[0]["NodalOfficerFax"].ToString();
            }
        }
        else
        {
        }
    }
    protected void ddlNodalOfficerEmail2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlNodalOfficerEmail2.SelectedItem.Text != "Select Nodel Officer")
        {
            DataTable DtGetNodel = Lo.RetriveMasterData(Convert.ToInt16(ddlNodalOfficerEmail2.SelectedItem.Value), "", "", 0, "", "", "CompleteNodelDetail");
            if (DtGetNodel.Rows.Count > 0)
            {
                txtNName2.Text = DtGetNodel.Rows[0]["NodalOficerName"].ToString();
                txtNEmailId2.Text = DtGetNodel.Rows[0]["NodalOfficerEmail"].ToString();
                txtNTelephone2.Text = DtGetNodel.Rows[0]["NodalOfficerTelephone"].ToString();
                txtNFaxNo2.Text = DtGetNodel.Rows[0]["NodalOfficerFax"].ToString();
            }
        }
        else
        {
        }
    }
    protected void Cleartext()
    {

    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
    }
    #region For ProductCode
    protected void BindMasterCategory()
    {
        ddlmastercategory.Items.Insert(0, "Product Category");
        ddlsubcategory.Items.Insert(0, "Lavel 1 Values");
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, ddlmastercategory.SelectedItem.Value, "", "SelectProductCat", SessionID.ToString());
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlmastercategory, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlmastercategory.Items.Insert(0, "Lavel 1 Values");
        }
    }
    protected void BindMasterSubCategory()
    {
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(ddlmastercategory.SelectedItem.Value), "", "", "SubSelectID", "");
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlsubcategory, DtMasterCategroy, "SCategoryName", "SCategoryId");
            ddlsubcategory.Items.Insert(0, "Lavel 2 Values");
        }
        else
        {
            ddlsubcategory.Items.Clear();
            ddlsubcategory.Items.Insert(0, "Lavel 2 Values");
        }
    }
    #endregion
    #region For Technology
    protected void BindMasterTechnologyCategory()
    {
        ddltechnologycat.Items.Insert(0, "Technology Category");
        ddlsubtech.Items.Insert(0, "Select SubCategory");
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, ddltechnologycat.SelectedItem.Value, "", "SelectProductCat", SessionID.ToString());
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddltechnologycat, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddltechnologycat.Items.Insert(0, "Technology Category");
        }
    }
    protected void BindMasterSubCategoryTech()
    {
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(ddltechnologycat.SelectedItem.Value), "", "", "SubSelectID", "");
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlsubtech, DtMasterCategroy, "SCategoryName", "SCategoryId");
            ddlsubtech.Items.Insert(0, "Select SubCategory");
        }
        else
        {
            ddlsubtech.Items.Clear();
            ddlsubtech.Items.Insert(0, "Select SubCategory");
        }
    }
    #endregion
    #region For PlatForm
    protected void BindMasterPlatCategory()
    {
        ddlplatform.Items.Insert(0, "Platform Category");
        ddlplatformsubcat.Items.Insert(0, "Select SubCategory");
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, ddlplatform.SelectedItem.Value, "", "SelectProductCat", SessionID.ToString());
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlplatform, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlplatform.Items.Insert(0, "Platform Category");
        }
    }
    protected void BindMasterSubCategoryPlat()
    {
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(ddlplatform.SelectedItem.Value), "", "", "SubSelectID", "");
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlplatformsubcat, DtMasterCategroy, "SCategoryName", "SCategoryId");
            ddlplatformsubcat.Items.Insert(0, "Select SubCategory");
        }
        else
        {
            ddlplatformsubcat.Items.Clear();
            ddlplatformsubcat.Items.Insert(0, "Select SubCategory");
        }
    }
    #endregion
    #region For ProductRequirment
    protected void BindMasterProductReqCategory()
    {
        ddlprodreqir.Items.Insert(0, "Product Requirement");
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, ddlprodreqir.SelectedItem.Value, "", "SelectProductCat", SessionID.ToString());
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlprodreqir, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlprodreqir.Items.Insert(0, "Product Requirement");
        }
    }
    #endregion
    #region For NomenClature
    protected void BindMasterProductNoenCletureCategory()
    {
        ddlnomnclature.Items.Insert(0, "Nomenclature");
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, ddlnomnclature.SelectedItem.Value, "", "SelectProductCat", SessionID.ToString());
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlnomnclature, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlnomnclature.Items.Insert(0, "Nomenclature");
        }
    }
    #endregion
    protected void ddlmastercategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMasterSubCategory();
    }
    protected void ddltechnologycat_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMasterSubCategoryTech();
    }
    protected void ddlplatform_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMasterSubCategoryPlat();
    }
    #region PanelSaveCode
    protected void SaveProductDescription()
    {
        HyPanel1["ProductID"] = "";
        HyPanel1["CompanyRefNo"] = "";
        HyPanel1["OEMPartNumber"] = "";
        HyPanel1["DPSUPartNumber"] = "";
        HyPanel1["EndUserPartNumber"] = "";
        HyPanel1["HSNCode"] = "";
        HyPanel1["NatoCode"] = "";
        HyPanel1["ERPRefNo"] = "";
        HyPanel1["NomenclatureOfMainSystem"] = "";
        HyPanel1["ProductLevel1"] = "";
        HyPanel1["ProductLevel2"] = "";
        HyPanel1["ProductDescription"] = "";
        HyPanel1["TechnologyLevel1"] = "";
        HyPanel1["TechnologyLevel2"] = "";
        HyPanel1["EndUser"] = "";
        HyPanel1["Platform"] = "";
        HyPanel1["PurposeofProcurement"] = "";
        HyPanel1["ProductRequirment"] = "";
        HyPanel1["IsIndeginized"] = "";
        HyPanel1["ManufactureName"] = "";
        HyPanel1["SearchKeyword"] = "";
        string StrProductDescription = Lo.SaveMasterCompany(HyPanel1, out _sysMsg, out _msg);
        if (StrProductDescription != "")
        {
        }
        else
        {
        }
    }
    protected void SaveProductImage()
    {
        HyPanel2["ImageID"] = "";
        HyPanel2["ImageName"] = "";
        HyPanel2["ImageType"] = "";
        HyPanel2["ImageActualSize"] = "";
        HyPanel2["ImageLesserSize"] = "";
        HyPanel2["ProductRefNo"] = "";
        string StrImage = Lo.SaveMasterCompany(HyPanel2, out _sysMsg, out _msg);
        if (StrImage != "")
        {
        }
        else
        {           
        }
    }
    protected void SaveSPDPSU()
    {
        HyPanel3["DPSUServices"] = "";
        HyPanel3["Remarks"] = "";
        string StrSPDPSU = Lo.SaveMasterCompany(HyPanel3, out _sysMsg, out _msg);
        if (StrSPDPSU != "")
        {
        }
        else
        {
        }
    }
    protected void SaveQuantityRequired()
    {
        HyPanel4["Estimatequantity"] = "";
        HyPanel4["EstimatePriceLLP"] = "";
        HyPanel4["TenderStatus"] = "";
        HyPanel4["TenderStatus"] = "";
        HyPanel4["TenderFillDate"] = "";
        HyPanel4["TenderUrl"] = "";
        string StrQuantity = Lo.SaveMasterCompany(HyPanel3, out _sysMsg, out _msg);
        if (StrQuantity != "")
        {
        }
        else
        {
        }
    }
    protected void SaveNodalDetail()
    {
        HyPanel5[""] = "";
        HyPanel5[""] = "";
        string StrNodal = Lo.SaveMasterCompany(HyPanel3, out _sysMsg, out _msg);
        if (StrNodal != "")
        {
        }
        else
        {
        }
    }
    #endregion
}