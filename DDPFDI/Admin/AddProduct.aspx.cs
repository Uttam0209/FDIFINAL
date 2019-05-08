using BusinessLayer;
using Encryption;
using System;
using System.Collections.Specialized;
using System.Data;
using System.ServiceModel.Description;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Web;
using System.IO;

public partial class Admin_AddProduct : System.Web.UI.Page
{
    Cryptography objEnc = new Cryptography();
    DataUtility Co = new DataUtility();
    Logic Lo = new Logic();
    private string DisplayPanel = "";
    string _msg = string.Empty;
    string _sysMsg = string.Empty;

    public string Services = "";
    public string Remarks = "";
    public string NodalDDL = "";
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
                    hfcomprefno.Value = Session["CompanyRefNo"].ToString();
                    BindMasterCategory();
                    BindMasterTechnologyCategory();
                    BindMasterPlatCategory();
                    BindMasterProductReqCategory();
                    BindMasterProductNoenCletureCategory();
                    BindNodelEmail();
                    BindServcies();
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
        DtCompanyDDL = Lo.RetriveMasterData(0, hfcomprefno.Value, "", 0, "", "", "AllNodel");
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
                // txtNName.Text = DtGetNodel.Rows[0]["NodalOficerName"].ToString();
                txtNEmailId.Text = DtGetNodel.Rows[0]["NodalOfficerEmail"].ToString();
                txtNTelephone.Text = DtGetNodel.Rows[0]["NodalOfficerTelephone"].ToString();
                txtNFaxNo.Text = DtGetNodel.Rows[0]["NodalOfficerFax"].ToString();
                //  lblcomapnyNodal.Text = DtGetNodel.Rows[0]["Type"].ToString();
                //===Bind Nodel officer expect Nodel one
                DtCompanyDDL = Lo.RetriveMasterData(Convert.ToInt16(ddlNodalOfficerEmail.SelectedItem.Value), hfcomprefno.Value, "", 0, "", "", "AllNodelNotSelect");
                if (DtCompanyDDL.Rows.Count > 0)
                {
                    Co.FillDropdownlist(ddlNodalOfficerEmail2, DtCompanyDDL, "NodalOficerName", "NodalOfficerID");
                    //  ddlNodalOfficerEmail2.Items.Insert(0, "Select Nodel Officer");
                }
                else
                {
                    divnodal2.Visible = false;
                }
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
                // txtNName2.Text = DtGetNodel.Rows[0]["NodalOficerName"].ToString();
                txtNEmailId2.Text = DtGetNodel.Rows[0]["NodalOfficerEmail"].ToString();
                txtNTelephone2.Text = DtGetNodel.Rows[0]["NodalOfficerTelephone"].ToString();
                txtNFaxNo2.Text = DtGetNodel.Rows[0]["NodalOfficerFax"].ToString();
                lblcompanynodal2.Text = DtGetNodel.Rows[0]["Type"].ToString();
                //===Bind Nodel officer expect Nodel Two
                DtCompanyDDL = Lo.RetriveMasterData(Convert.ToInt16(ddlNodalOfficerEmail2.SelectedItem.Value), hfcomprefno.Value, "", 0, "", "", "AllNodelNotSelect");
                if (DtCompanyDDL.Rows.Count > 0)
                {
                    Co.FillDropdownlist(ddlNodalOfficerEmail, DtCompanyDDL, "NodalOficerName", "NodalOfficerID");
                    //  ddlNodalOfficerEmail2.Items.Insert(0, "Select Nodel Officer");
                }
                else
                {
                    divnodal.Visible = false;
                }
            }
        }
        else
        {
        }
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
    }
    #region BindServices
    protected void BindServcies()
    {
        DataTable Dtservices = Lo.RetriveMasterSubCategoryDate(20, "Services", "", "SelectInnerMaster", hfcomprefno.Value);
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
        ddlsubcategory.Items.Insert(0, "Lavel 2");
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, ddlmastercategory.SelectedItem.Value, "", "SelectProductCat", hfcomprefno.Value);
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlmastercategory, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlmastercategory.Items.Insert(0, "Lavel 1");
        }
    }
    protected void BindMasterSubCategory()
    {
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(ddlmastercategory.SelectedItem.Value), "", "", "SubSelectID", "");
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlsubcategory, DtMasterCategroy, "SCategoryName", "SCategoryId");
            ddlsubcategory.Items.Insert(0, "Lavel 2");
        }
        else
        {
            ddlsubcategory.Items.Clear();
            ddlsubcategory.Items.Insert(0, "Lavel 2");
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
        ddlprodreqir.Items.Insert(0, "Product Requirement");
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, ddlprodreqir.SelectedItem.Value, "", "SelectProductCat", hfcomprefno.Value);
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
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "Nomenclature", "", "SelectProductCat", hfcomprefno.Value);
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlnomnclature, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlnomnclature.Items.Insert(0, "Select");
        }
        ddlnomnclature.Items.Insert(0, "Select");
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
    #region PanelSaveCode
    protected void SaveProductDescription()
    {
        if (hfprodid.Value != "")
        {
            HyPanel1["ProductID"] = Convert.ToInt16(hfprodid.Value);
        }
        else
        {
            HyPanel1["ProductID"] = 0;
        }
        HyPanel1["CompanyRefNo"] = Co.RSQandSQLInjection(hfcomprefno.Value, "soft");
        HyPanel1["OEMPartNumber"] = Co.RSQandSQLInjection(txtoempartnumber.Text, "soft");
        HyPanel1["DPSUPartNumber"] = Co.RSQandSQLInjection(txtdpsupartnumber.Text, "soft");
        HyPanel1["EndUserPartNumber"] = Co.RSQandSQLInjection(txtenduserpartnumber.Text, "soft");
        HyPanel1["HSNCode"] = Co.RSQandSQLInjection(txthsncode.Text, "soft");
        HyPanel1["NatoCode"] = Co.RSQandSQLInjection(txtnatocode.Text, "soft");
        HyPanel1["ERPRefNo"] = Co.RSQandSQLInjection(txterprefno.Text, "soft");
        HyPanel1["NomenclatureOfMainSystem"] = Co.RSQandSQLInjection(ddlnomnclature.SelectedItem.Value, "soft");
        HyPanel1["ProductLevel1"] = Co.RSQandSQLInjection(ddlmastercategory.SelectedItem.Value, "soft");
        HyPanel1["ProductLevel2"] = Co.RSQandSQLInjection(ddlsubcategory.SelectedItem.Value, "soft");
        HyPanel1["ProductDescription"] = Co.RSQandSQLInjection(txtproductdescription.Text, "soft");
        HyPanel1["TechnologyLevel1"] = Co.RSQandSQLInjection(ddltechnologycat.SelectedItem.Value, "soft");
        HyPanel1["TechnologyLevel2"] = Co.RSQandSQLInjection(ddlsubtech.SelectedItem.Value, "soft");
        HyPanel1["EndUser"] = Co.RSQandSQLInjection(ddlenduser.SelectedItem.Value, "soft");
        HyPanel1["Platform"] = Co.RSQandSQLInjection(ddlplatform.SelectedItem.Value, "soft");
        HyPanel1["PurposeofProcurement"] = Co.RSQandSQLInjection(ddlplatformsubcat.SelectedItem.Value, "soft");
        HyPanel1["ProductRequirment"] = Co.RSQandSQLInjection(ddlprodreqir.SelectedItem.Value, "soft");
        HyPanel1["IsIndeginized"] = Co.RSQandSQLInjection(rbisindinised.SelectedItem.Value, "soft");
        HyPanel1["ManufactureName"] = Co.RSQandSQLInjection(txtmanufacturename.Text, "soft");
        HyPanel1["SearchKeyword"] = Co.RSQandSQLInjection(txtsearchkeyword.Text, "soft");
        HyPanel1["DPSUServices"] = "";
        HyPanel1["Remarks"] = "";
        HyPanel1["Estimatequantity"] = "";
        HyPanel1["EstimatePriceLLP"] = "";
        HyPanel1["TenderStatus"] = "";
        HyPanel1["TenderFillDate"] = null;
        HyPanel1["TenderUrl"] = "";
        HyPanel1["NodelDetail"] = "";
        string StrProductDescription = Lo.SaveCodeProduct(HyPanel1, out _sysMsg, out _msg, "Product");
        if (StrProductDescription != "")
        {
            Cleartext();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record Saved.')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not saved.')", true);
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
        string StrImage = Lo.SaveCodeProduct(HyPanel2, out _sysMsg, out _msg, "Images");
        if (StrImage != "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record updated.')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not updated.')", true);
        }
    }
    protected void SaveSPDPSU()
    {
        if (hfprodid.Value != "")
        {
            HyPanel1["ProductID"] = Convert.ToInt16(hfprodid.Value);
        }
        else
        {
            HyPanel1["ProductID"] = 0;
        }
        HyPanel1["CompanyRefNo"] = Co.RSQandSQLInjection(hfcomprefno.Value, "soft");
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
        foreach (GridViewRow rw in gvservices.Rows)
        {
            CheckBox chkBx = (CheckBox)rw.FindControl("chk");
            HiddenField hfservicesid = (HiddenField)rw.FindControl("hfservicesid");
            TextBox txtRemarks = (TextBox)rw.FindControl("txtRemarks");
            if (chkBx != null && chkBx.Checked)
            {
                Services = Services + "," + hfservicesid.Value;
                Remarks = Remarks + "," + txtRemarks.Text;
            }
        }
        HyPanel1["DPSUServices"] = Co.RSQandSQLInjection(Services.Substring(1).ToString(), "soft");
        HyPanel1["Remarks"] = Co.RSQandSQLInjection(Remarks.Substring(1).ToString(), "soft");
        HyPanel1["Estimatequantity"] = "";
        HyPanel1["EstimatePriceLLP"] = "";
        HyPanel1["TenderStatus"] = "";
        HyPanel1["TenderFillDate"] = null;
        HyPanel1["TenderUrl"] = "";
        HyPanel1["NodelDetail"] = "";
        string StrSPDPSU = Lo.SaveCodeProduct(HyPanel1, out _sysMsg, out _msg, "DPSU");
        if (StrSPDPSU != "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record Saved.')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not saved.')", true);
        }
    }
    protected void SaveQuantityRequired()
    {

        if (hfprodid.Value != "")
        {
            HyPanel1["ProductID"] = Convert.ToInt16(hfprodid.Value);
        }
        else
        {
            HyPanel1["ProductID"] = 0;
        }

        HyPanel1["CompanyRefNo"] = Co.RSQandSQLInjection(hfcomprefno.Value, "soft");
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

        HyPanel1["DPSUServices"] = "";
        HyPanel1["Remarks"] = "";
        HyPanel1["Estimatequantity"] = Co.RSQandSQLInjection(txtestimatequantity.Text, "soft");
        HyPanel1["EstimatePriceLLP"] = Co.RSQandSQLInjection(txtestimateprice.Text, "soft");
        HyPanel1["TenderStatus"] = Co.RSQandSQLInjection(ddltendorstatus.SelectedItem.Value, "soft");
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
        HyPanel1["NodelDetail"] = "";
        string StrQuantity = Lo.SaveCodeProduct(HyPanel1, out _sysMsg, out _msg, "QuantityReq");
        if (StrQuantity != "")
        {
            Cleartext3();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record Saved.')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record Saved.')", true);
        }
    }
    protected void SaveNodalDetail()
    {

        if (hfprodid.Value != "")
        {
            HyPanel1["ProductID"] = Convert.ToInt16(hfprodid.Value);
        }
        else
        {
            HyPanel1["ProductID"] = 0;
        }

        HyPanel1["CompanyRefNo"] = Co.RSQandSQLInjection(hfcomprefno.Value, "soft");
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

        HyPanel1["DPSUServices"] = "";
        HyPanel1["Remarks"] = "";
        HyPanel1["Estimatequantity"] = "";
        HyPanel1["EstimatePriceLLP"] = "";
        HyPanel1["TenderStatus"] = "";
        HyPanel1["TenderUrl"] = "";
        HyPanel1["NodelDetail"] = Co.RSQandSQLInjection(ddlNodalOfficerEmail.SelectedItem.Value, "soft") + "," + Co.RSQandSQLInjection(ddlNodalOfficerEmail2.SelectedItem.Value, "soft");
        string StrNodal = Lo.SaveCodeProduct(HyPanel3, out _sysMsg, out _msg, "Nodal");
        if (StrNodal != "")
        {
            Cleartext4();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record Saved.')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not Saved.')", true);
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
    protected void btnprodimagesave_Click(object sender, EventArgs e)
    {
        if (files.HasFile != null)
        {
            submitad();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Please fill OEM Part Number')", true);
        }
    }
    protected void btnprodback_Click(object sender, EventArgs e)
    {
        Cleartext();
    }
    protected void btnsavepanel3_Click(object sender, EventArgs e)
    {
        if (txtestimatequantity.Text != "")
        {
            SaveSPDPSU();
        }
    }
    protected void btnbackpanel3_Click(object sender, EventArgs e)
    {
        Cleartext();
    }
    protected void btnsavepanel4_Click(object sender, EventArgs e)
    {
        if (ddlNodalOfficerEmail.SelectedItem.Value != "Select Nodal Officer" || ddlNodalOfficerEmail2.SelectedItem.Value != "Select Nodal Officer")
        {
            SaveQuantityRequired();
        }
    }
    protected void btnbackpanel4_Click(object sender, EventArgs e)
    {
        Cleartext3();
    }
    #endregion
    #region Claertext
    protected void Cleartext()
    {
        hfprodid.Value = "";
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
    }
    protected void Cleartext3()
    {
        txtestimatequantity.Text = "";
        txtestimateprice.Text = "";
        ddltendorstatus.SelectedIndex = 0;
        txttendordate.Text = "";
        txttendorurl.Text = "";
    }
    protected void Cleartext4()
    {
        ddlNodalOfficerEmail.SelectedIndex = 0;
        ddlNodalOfficerEmail2.SelectedIndex = 0;
    }
    #endregion
    #region Image Code
    protected void submitad()
    {
        try
        {
            if (files.HasFile != null)
            {
                DataTable dtImage = imagedb();
                string str = Lo.SaveImages(dtImage, out _sysMsg, out _msg, "ImageSave");
                if (str == "Save")
                {
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Please fill form correctly. fill all column')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message.ToString() + "')", true);
        }
    }
    protected DataTable imagedb()
    {
        HttpFileCollection uploadedFiles = Request.Files;
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("ImageID", typeof(Int64)));
        dt.Columns.Add(new DataColumn("ImageName", typeof(string)));
        dt.Columns.Add(new DataColumn("ImageType", typeof(string)));
        dt.Columns.Add(new DataColumn("ImageActualSize", typeof(Int64)));
        dt.Columns.Add(new DataColumn("ProductRefNo", typeof(string)));
        dt.Columns.Add(new DataColumn("Priority", typeof(int)));
        DataRow dr;
        {
            try
            {
               for (int i = 0; i < uploadedFiles.Count; i++)
                    {
                        uploadedFiles[i].SaveAs(Server.MapPath("~/Temp/" + Path.GetFileName(uploadedFiles[i].FileName)));
                        Bitmap bitmap = new Bitmap(Server.MapPath("~/Temp/" + Path.GetFileName(uploadedFiles[i].FileName)));
                        int iwidth = bitmap.Width;
                        int iheight = bitmap.Height;
                        iwidth = iwidth / 3; 
                        iheight = iheight / 3;
                        bitmap.Dispose();
                        System.Drawing.Image objOptImage = new System.Drawing.Bitmap(iwidth, iheight, System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
                        using (System.Drawing.Image objImg = System.Drawing.Image.FromFile(Server.MapPath("Temp/" + uploadedFiles[i].FileName)))
                        {
                            using (System.Drawing.Graphics oGraphic = System.Drawing.Graphics.FromImage(objOptImage))
                            {
                                var _1 = oGraphic;
                                System.Drawing.Rectangle oRectangle = new System.Drawing.Rectangle(0, 0, iwidth, iheight);
                                _1.DrawImage(objImg, oRectangle);
                            }
                            objOptImage.Save(Server.MapPath("Upload/" + uploadedFiles[i].FileName), System.Drawing.Imaging.ImageFormat.Png);
                            objImg.Dispose();
                        }
                        objOptImage.Dispose();
                        dr = dt.NewRow();
                        dr["ImageID"] = "-1";
                        dr["ImageName"] = "Upload/" + uploadedFiles[i].FileName;
                        dr["ImageType"] = files.PostedFile.ContentType;
                        dr["ImageActualSize"] = files.PostedFile.ContentLength;
                        dr["ProductRefNo"] = "";
                        dr["CompanyRefNo"] = hfcomprefno.Value;
                        dr["Priority"] = i + 1;
                        dt.Rows.Add(dr);
                    }
                
            }
            catch (Exception ex)
            {
            }
            return dt;

        }
    }
    #endregion
}