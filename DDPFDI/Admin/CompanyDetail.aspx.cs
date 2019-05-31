using BusinessLayer;
using Encryption;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_CompanyDetail : System.Web.UI.Page
{
    private Logic Lo = new Logic();
    private HybridDictionary HySave = new HybridDictionary();
    private Cryptography objCrypto = new Cryptography();
    private long Mid = 0;
    private DataUtility Co = new DataUtility();
    private string _msg = string.Empty;
    private string _sysMsg = string.Empty;
    private DataTable DtView = new DataTable();
    private string currentPage = "";
    private string lbltypelogin = "";
    private string mType = "";
    private string mRefNo = "";
    private string Categoryintrestedare = "";
    private DataTable DtCompanyDDL = new DataTable();
    private DataTable dtViewDefault = new DataTable();
    private string strCRole, strDRole, strURole;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Session["Type"] != null)
                {
                    if (Request.QueryString["id"] != null)
                    {
                        string strid = Request.QueryString["id"].ToString().Replace(" ", "+");
                        string strPageName = objCrypto.DecryptData(strid);
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
                    }
                    currentPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
                    mType = objCrypto.DecryptData(Session["Type"].ToString());
                    mRefNo = Session["CompanyRefNo"].ToString();
                    BindState();
                    ddldivision.Visible = false;
                    ddlunit.Visible = false;
                    BindCompany();
                    BindMasterCategory();
                    //BindNodelEmail();
                    if (objCrypto.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Company")
                    {
                        EditCOde(dtViewDefault);
                        ddlcompany.Enabled = false;
                        lblselectdivison.Visible = false;
                        licc.Visible = false;
                        lblMCompany.Text = "Company";
                    }
                    else if (objCrypto.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Factory")
                    {
                        EditDivison(dtViewDefault);
                        licc.Visible = false;
                        lisr.Visible = false;
                        lisc.Visible = false;
                        lblMCompany.Text = "Division";
                    }
                    else if (objCrypto.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Unit")
                    {
                        EditUnit(dtViewDefault);
                        licc.Visible = false;
                        lisr.Visible = false;
                        licc.Visible = false;
                        lisc.Visible = false;
                        lblMCompany.Text = "Unit";
                    }
                }
                else
                { Response.RedirectToRoute("Login"); }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                string Page = Request.Url.AbsolutePath.ToString();
                Response.Redirect("Error?techerror=" + HttpUtility.UrlEncode(objCrypto.EncryptData(error)) + "&page=" + HttpUtility.UrlEncode(objCrypto.EncryptData(Page)));
            }
        }
    }
    protected void BindMasterCategory()
    {
        DataTable DtMasterCategroy = Lo.RetriveMasterCategoryDate(0, "", "", "", "", "Select", "");
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlmastercategory, DtMasterCategroy, "MCategoryName", "MCategoryID");
            ddlmastercategory.Items.Insert(0, "Select Category");
        }
        else
        {
            ddlmastercategory.Items.Insert(0, "Select Category");
        }
    }
    protected void ddlmastercategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMasterInnerSubCategory();
    }
    protected void BindMasterInnerSubCategory()
    {
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(ddlmastercategory.SelectedItem.Value), "", "", "SelectInnerMaster", "", "");
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillCheckBox(chkSubCategory, DtMasterCategroy, "SCategoryName", "SCategoryId");
        }
    }
    protected void EditCOde(DataTable DtView)
    {
        if (Session["CompanyRefNo"] != null)
        {

            if (DtView.Rows.Count > 0)
            {

            }
            else
            {
                if (Request.QueryString["mcurrentcompRefNo"] != null)
                {
                    DtView = Lo.RetriveGridViewCompany(objCrypto.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "", "", "CompanyMainGridView");
                }
                else
                {
                    DtView = Lo.RetriveGridViewCompany(Session["CompanyRefNo"].ToString(), "", "", "CompanyMainGridView");
                }
            }

            if (DtView.Rows.Count > 0)
            {
                hfid.Value = DtView.Rows[0]["CompanyID"].ToString();
                seljvventure.SelectedItem.Value = DtView.Rows[0]["IsJointVenture"].ToString();
                tcompanyname.Text = DtView.Rows[0]["CompanyName"].ToString();
                txtMName.Text = DtView.Rows[0]["CompanyName"].ToString();
                divmcName.Visible = false;
                divmName.Visible = false;
                lblMName.Text = "Company";
                lblCName.Text = "Company/Organization";
                tcompanyname.ReadOnly = true;
                ddlcompany.Enabled = false;
                taddress.Text = DtView.Rows[0]["Address"].ToString();

                selstate.SelectedValue = DtView.Rows[0]["State"].ToString();

                txtceoname.Text = DtView.Rows[0]["CEOName"].ToString();
                txtCEOEmailId.Text = DtView.Rows[0]["CEOEmail"].ToString();
                txtTelephone.Text = DtView.Rows[0]["TelephoneNo"].ToString();
                txtFaxNo.Text = DtView.Rows[0]["FaxNo"].ToString();
                txtEmailID.Text = DtView.Rows[0]["EmailID"].ToString();
                txtWebsite.Text = DtView.Rows[0]["Website"].ToString();
                tpincode.Text = DtView.Rows[0]["Pincode"].ToString();
                txtNEmailId.Text = DtView.Rows[0]["ContactPersonEmailID"].ToString();
                txtNEmailId.ReadOnly = true;
                tgstno.Text = DtView.Rows[0]["GSTNo"].ToString();
                tcinno.Text = DtView.Rows[0]["CINNo"].ToString();
                tpanno.Text = DtView.Rows[0]["PANNo"].ToString();
                txtIECode.Text = DtView.Rows[0]["IECode"].ToString();

                txtFacebook.Text = DtView.Rows[0]["Facebook"].ToString();
                txtTwitter.Text = DtView.Rows[0]["Twitter"].ToString();
                txtLinkedin.Text = DtView.Rows[0]["Linkedin"].ToString();
                txtInstagram.Text = DtView.Rows[0]["Instagram"].ToString();

                txtFacebook.Text = DtView.Rows[0]["Facebook"].ToString();
                txtTwitter.Text = DtView.Rows[0]["Twitter"].ToString();
                txtLinkedin.Text = DtView.Rows[0]["Linkedin"].ToString();
                txtInstagram.Text = DtView.Rows[0]["Instagram"].ToString();

                DataTable DtGetNodel = Lo.RetriveAllNodalOfficer(objCrypto.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "Company");
                if (DtGetNodel.Rows.Count > 0)
                {
                    txtNName.Text = DtGetNodel.Rows[0]["NodalOficerName"].ToString();
                    txtNEmailId.Text = DtGetNodel.Rows[0]["NodalOfficerEmail"].ToString();
                    txtNMobile.Text = DtGetNodel.Rows[0]["NodalOfficerMobile"].ToString();
                    txtNTelephone.Text = DtGetNodel.Rows[0]["NodalOfficerTelephone"].ToString();
                    txtNFaxNo.Text = DtGetNodel.Rows[0]["NodalOfficerFax"].ToString();
                    divNodalOfficer.Visible = true;
                }
                txtlatitude.Text = DtView.Rows[0]["latitude"].ToString();
                txtlongitude.Text = DtView.Rows[0]["longitude"].ToString();

                if (DtView.Rows[0]["Startup"].ToString() == "Y")
                {
                    rdoNo.Checked = false;
                    rdoYes.Checked = true;
                    divStartup.Visible = true;
                }
                else
                {
                    rdoYes.Checked = false;
                    rdoNo.Checked = true;
                    divStartup.Visible = false;
                }
                txtDIPP.Text = DtView.Rows[0]["DIPPNumber"].ToString();
                txtDIPPMobile.Text = DtView.Rows[0]["DIPPMobile"].ToString();


                if (DtView.Rows[0]["MSME"].ToString() == "Y")
                {
                    rdoMNo.Checked = false;
                    rdoMYes.Checked = true;
                    divMSMe.Visible = true;
                }
                else
                {
                    rdoMYes.Checked = false;
                    rdoMNo.Checked = true;
                    divMSMe.Visible = false;
                }
                txtVAM.Text = DtView.Rows[0]["VAM"].ToString();
                txtAad_Mob.Text = DtView.Rows[0]["Aadhar_Mobile"].ToString();



                companyengaged.SelectedItem.Value = DtView.Rows[0]["IsDefenceActivity"].ToString();
                lbltypelogin = DtView.Rows[0]["Role"].ToString();
                if (lbltypelogin == "SuperAdmin" || lbltypelogin == "Admin")
                {
                    DivJointVenture.Visible = false;
                    DivGST.Visible = false;
                    DivHSNO.Visible = false;
                    DivPAN.Visible = false;
                    DivCIN.Visible = false;
                    DivActivities.Visible = false;

                }
                else
                {
                    DivJointVenture.Visible = false;
                    DivGST.Visible = true;
                    DivHSNO.Visible = true;
                    DivPAN.Visible = true;
                    DivCIN.Visible = true;
                    DivActivities.Visible = true;
                }
                DataTable dtCompany = Lo.RetriveMasterData(0, "", "", 0, currentPage, "", "btn");
                if (dtCompany.Rows.Count > 0)
                {
                    if (dtCompany.Rows[0]["Admin"].ToString() == "2")
                    {
                        btndemofirst.Visible = true;
                        btnDelete.Visible = false;
                    }
                    else if (dtCompany.Rows[0]["Admin"].ToString() == "3")
                    {
                        btndemofirst.Visible = true;
                        btnDelete.Visible = false;
                    }
                    else if (dtCompany.Rows[0]["Company"].ToString() == "2")
                    {
                        btndemofirst.Visible = true;
                        btnDelete.Visible = false;
                    }
                    else if (dtCompany.Rows[0]["Company"].ToString() == "3")
                    {
                        btndemofirst.Visible = true;
                        btnDelete.Visible = false;
                    }
                    else if (dtCompany.Rows[0]["Factory"].ToString() == "2")
                    {
                        btndemofirst.Visible = true;
                        btnDelete.Visible = false;
                    }
                    else if (dtCompany.Rows[0]["Factory"].ToString() == "3")
                    {
                        btndemofirst.Visible = true;
                        btnDelete.Visible = false;
                    }
                    else if (dtCompany.Rows[0]["Unit"].ToString() == "2")
                    {
                        btndemofirst.Visible = true;
                        btnDelete.Visible = false;
                    }
                    else if (dtCompany.Rows[0]["Unit"].ToString() == "3")
                    {
                        btndemofirst.Visible = true;
                        btnDelete.Visible = false;
                    }
                    else if (dtCompany.Rows[0]["SuperAdmin"].ToString() == "1")
                    {
                        btndemofirst.Visible = true;
                        btnDelete.Visible = false;
                    }
                    else
                    {
                        btndemofirst.Visible = false;
                        btnDelete.Visible = false;
                    }

                }
            }
            btndemofirst.Text = "Save";
        }
    }
    protected void btnShowMap_Click(object sender, EventArgs e)
    {

    }
    protected void EditDivison(DataTable DtView)
    {
        if (Session["CompanyRefNo"] != null)
        {

            if (DtView.Rows.Count > 0)
            {

            }
            else
            {
                if (Request.QueryString["mcurrentcompRefNo"] != null)
                {
                    DtView = Lo.RetriveGridViewCompany("", objCrypto.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "", "DisplayFactory");
                }
                else
                {
                    DtView = Lo.RetriveGridViewCompany(Session["CompanyRefNo"].ToString(), "", "", "CompanyMainGridView");
                }
            }

            if (DtView.Rows.Count > 0)
            {
                hfid.Value = DtView.Rows[0]["FactoryID"].ToString();
                seljvventure.Visible = false;
                tcompanyname.Text = DtView.Rows[0]["FactoryName"].ToString();
                divmcName.Visible = false;
                divmName.Visible = true;
                lblMName.Text = "Company";
                txtMName.Text = DtView.Rows[0]["CompanyName"].ToString();
                lblMCName.Text = "Division";
                txtMCName.Text = DtView.Rows[0]["FactoryName"].ToString();
                lblCName.Text = "Division";
                lblceo.InnerText = "Division Head Name";
                lblceoemail.InnerText = "Division Head Email ID";
                tcompanyname.ReadOnly = true;

                DataTable DtGetNodel = Lo.RetriveAllNodalOfficer(objCrypto.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "Company");
                if (DtGetNodel.Rows.Count > 0)
                {
                    txtNName.Text = DtGetNodel.Rows[0]["NodalOficerName"].ToString();
                    txtNEmailId.Text = DtGetNodel.Rows[0]["NodalOfficerEmail"].ToString();
                    txtNMobile.Text = DtGetNodel.Rows[0]["NodalOfficerMobile"].ToString();
                    txtNTelephone.Text = DtGetNodel.Rows[0]["NodalOfficerTelephone"].ToString();
                    txtNFaxNo.Text = DtGetNodel.Rows[0]["NodalOfficerFax"].ToString();
                    divNodalOfficer.Visible = true;
                }

                taddress.Text = DtView.Rows[0]["FactoryAddress"].ToString();
                selstate.SelectedValue = DtView.Rows[0]["FactoryStateID"].ToString();
                DivCEOName.Visible = true;
                tpincode.Text = DtView.Rows[0]["FactoryPincode"].ToString();
                txtEmailID.Text = DtView.Rows[0]["FactoryEmailID"].ToString();
                txtWebsite.Text = DtView.Rows[0]["FactoryWebsite"].ToString();
                txtceoname.Text = DtView.Rows[0]["FactoryCEOName"].ToString();
                txtCEOEmailId.Text = DtView.Rows[0]["FactoryCEOEmail"].ToString();
                txtFaxNo.Text = DtView.Rows[0]["FactoryFaxNo"].ToString();
                txtTelephone.Text = DtView.Rows[0]["FactoryTelephoneNo"].ToString();
                txtFacebook.Text = DtView.Rows[0]["FactoryFacebook"].ToString();
                txtTwitter.Text = DtView.Rows[0]["FactoryTwitter"].ToString();
                txtLinkedin.Text = DtView.Rows[0]["FactoryLinkedin"].ToString();
                txtInstagram.Text = DtView.Rows[0]["FactoryInstagram"].ToString();
                txtlatitude.Text = DtView.Rows[0]["Factorylatitude"].ToString();
                txtlongitude.Text = DtView.Rows[0]["Factorylongitude"].ToString();
                DivCEOEmail.Visible = true;
                divwebsite.Visible = false;
                DivPAN.Visible = false;
                if (lbltypelogin == "SuperAdmin" || lbltypelogin == "Admin")
                {
                    DivJointVenture.Visible = false;
                    DivGST.Visible = false;
                    DivHSNO.Visible = false;
                    DivPAN.Visible = false;
                    DivCIN.Visible = false;

                }
                else
                {
                    DivJointVenture.Visible = false;
                    DivGST.Visible = true;
                    DivHSNO.Visible = true;
                    //  DivPAN.Visible = true;
                    DivCIN.Visible = true;
                    DivActivities.Visible = false;
                }
                DataTable dtCompany = Lo.RetriveMasterData(0, "", "", 0, currentPage, "", "btn");
                if (dtCompany.Rows.Count > 0)
                {
                    if (dtCompany.Rows[0]["Admin"].ToString() == "2")
                    {
                        btndemofirst.Visible = true;
                        btnDelete.Visible = false;
                    }
                    else if (dtCompany.Rows[0]["Admin"].ToString() == "3")
                    {
                        btndemofirst.Visible = true;
                        btnDelete.Visible = true;
                    }
                    else if (dtCompany.Rows[0]["Company"].ToString() == "2")
                    {
                        btndemofirst.Visible = true;
                        btnDelete.Visible = false;
                    }
                    else if (dtCompany.Rows[0]["Company"].ToString() == "3")
                    {
                        btndemofirst.Visible = true;
                        btnDelete.Visible = true;
                    }
                    else if (dtCompany.Rows[0]["Factory"].ToString() == "2")
                    {
                        btndemofirst.Visible = true;
                        btnDelete.Visible = false;
                    }
                    else if (dtCompany.Rows[0]["Factory"].ToString() == "3")
                    {
                        btndemofirst.Visible = true;
                        btnDelete.Visible = true;
                    }
                    else if (dtCompany.Rows[0]["Unit"].ToString() == "2")
                    {
                        btndemofirst.Visible = true;
                        btnDelete.Visible = false;
                    }
                    else if (dtCompany.Rows[0]["Unit"].ToString() == "3")
                    {
                        btndemofirst.Visible = true;
                        btnDelete.Visible = true;
                    }
                    else if (dtCompany.Rows[0]["SuperAdmin"].ToString() == "1")
                    {
                        btndemofirst.Visible = true;
                        btnDelete.Visible = true;
                    }
                    else
                    {
                        btndemofirst.Visible = false;
                        btnDelete.Visible = false;
                    }

                }
            }
            btndemofirst.Text = "Save Division";
        }
    }
    protected void EditUnit(DataTable DtView)
    {
        if (Session["CompanyRefNo"] != null)
        {

            if (DtView.Rows.Count > 0)
            {

            }
            else
            {
                if (Request.QueryString["mcurrentcompRefNo"] != null)
                {
                    DtView = Lo.RetriveGridViewCompany("", objCrypto.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "", "DisplayUnit");
                }
                else
                {
                    DtView = Lo.RetriveGridViewCompany(Session["CompanyRefNo"].ToString(), "", "", "CompanyMainGridView");
                }
            }

            if (DtView.Rows.Count > 0)
            {
                hfid.Value = DtView.Rows[0]["UnitID"].ToString();
                seljvventure.Visible = false;
                divmName.Visible = true;
                divmcName.Visible = true;
                lblMName.Text = "Company";
                lblMCName.Text = "Division";
                txtMCName.Text = DtView.Rows[0]["FactoryName"].ToString();
                txtMName.Text = DtView.Rows[0]["CompanyName"].ToString();
                tcompanyname.Text = DtView.Rows[0]["UnitName"].ToString();
                lblCName.Text = "Unit";
                tcompanyname.ReadOnly = true;
                taddress.Text = DtView.Rows[0]["UnitAddress"].ToString();
                DivPAN.Visible = false;
                divwebsite.Visible = false;
                lblceo.InnerText = "Unit Head Name";
                lblceoemail.InnerText = "Unit Head Email ID";
                selstate.SelectedValue = DtView.Rows[0]["UnitStateID"].ToString();
                ddlNodalOfficerEmail.SelectedValue = DtView.Rows[0]["NodalOfficeRefNo"].ToString();
                DataTable DtGetNodel = Lo.RetriveAllNodalOfficer(objCrypto.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "AllNodalDetail");
                if (DtGetNodel.Rows.Count > 0)
                {
                    txtNName.Text = DtGetNodel.Rows[0]["NodalOficerName"].ToString();
                    txtNEmailId.Text = DtGetNodel.Rows[0]["NodalOfficerEmail"].ToString();
                    txtNMobile.Text = DtGetNodel.Rows[0]["NodalOfficerMobile"].ToString();
                    txtNTelephone.Text = DtGetNodel.Rows[0]["NodalOfficerTelephone"].ToString();
                    txtNFaxNo.Text = DtGetNodel.Rows[0]["NodalOfficerFax"].ToString();
                    divNodalOfficer.Visible = true;
                }

                DivCEOName.Visible = true;
                tpincode.Text = DtView.Rows[0]["UnitPincode"].ToString();
                txtEmailID.Text = DtView.Rows[0]["UnitEmailId"].ToString();
                txtWebsite.Text = DtView.Rows[0]["UnitWebsite"].ToString();
                txtFaxNo.Text = DtView.Rows[0]["UnitFaxNo"].ToString();
                txtceoname.Text = DtView.Rows[0]["UnitCEOName"].ToString();
                txtCEOEmailId.Text = DtView.Rows[0]["UnitCEOEmail"].ToString();
                txtTelephone.Text = DtView.Rows[0]["UnitTelephoneNo"].ToString();
                txtFacebook.Text = DtView.Rows[0]["UnitFacebook"].ToString();
                txtTwitter.Text = DtView.Rows[0]["UnitTwitter"].ToString();
                txtLinkedin.Text = DtView.Rows[0]["UnitLinkedin"].ToString();
                txtInstagram.Text = DtView.Rows[0]["UnitInstagram"].ToString();
                txtlatitude.Text = DtView.Rows[0]["Unitlatitude"].ToString();
                txtlongitude.Text = DtView.Rows[0]["Unitlongitude"].ToString();
                txtNEmailId.Enabled = false;
                DivCEOEmail.Visible = true;
                if (lbltypelogin == "SuperAdmin" || lbltypelogin == "Admin")
                {
                    DivJointVenture.Visible = false;
                    DivGST.Visible = false;
                    DivHSNO.Visible = false;
                    DivPAN.Visible = false;
                    DivCIN.Visible = false;
                    DivActivities.Visible = false;

                }
                else
                {
                    DivJointVenture.Visible = false;
                    DivGST.Visible = false;
                    DivHSNO.Visible = false;
                    DivPAN.Visible = false;
                    DivCIN.Visible = false;
                    DivActivities.Visible = false;
                }
                DataTable dtCompany = Lo.RetriveMasterData(0, "", "", 0, currentPage, "", "btn");
                if (dtCompany.Rows.Count > 0)
                {
                    if (dtCompany.Rows[0]["Admin"].ToString() == "2")
                    {
                        btndemofirst.Visible = true;
                        btnDelete.Visible = false;
                    }
                    else if (dtCompany.Rows[0]["Admin"].ToString() == "3")
                    {
                        btndemofirst.Visible = true;
                        btnDelete.Visible = true;
                    }
                    else if (dtCompany.Rows[0]["Company"].ToString() == "2")
                    {
                        btndemofirst.Visible = true;
                        btnDelete.Visible = false;
                    }
                    else if (dtCompany.Rows[0]["Company"].ToString() == "3")
                    {
                        btndemofirst.Visible = true;
                        btnDelete.Visible = true;
                    }
                    else if (dtCompany.Rows[0]["Factory"].ToString() == "2")
                    {
                        btndemofirst.Visible = true;
                        btnDelete.Visible = false;
                    }
                    else if (dtCompany.Rows[0]["Factory"].ToString() == "3")
                    {
                        btndemofirst.Visible = true;
                        btnDelete.Visible = true;
                    }
                    else if (dtCompany.Rows[0]["Unit"].ToString() == "2")
                    {
                        btndemofirst.Visible = true;
                        btnDelete.Visible = false;
                    }
                    else if (dtCompany.Rows[0]["Unit"].ToString() == "3")
                    {
                        btndemofirst.Visible = true;
                        btnDelete.Visible = true;
                    }
                    else if (dtCompany.Rows[0]["SuperAdmin"].ToString() == "1")
                    {
                        btndemofirst.Visible = true;
                        btnDelete.Visible = true;
                    }
                    else
                    {
                        btndemofirst.Visible = false;
                        btnDelete.Visible = false;
                    }

                }
            }
            btndemofirst.Text = "Save Unit";
        }
    }
    protected void BindState()
    {
        DataTable Dt = Lo.RetriveState("Select");
        if (Dt.Rows.Count > 0 && Dt != null)
        {
            Co.FillDropdownlist(selstate, Dt, "StateName", "StateID");
            selstate.Items.Insert(0, "Select State");
        }
        else
        {
            selstate.Items.Insert(0, "Select State");
        }
    }
    protected void SaveFDI()
    {
        if (hfid.Value != "")
        {
            HySave["CompanyID"] = hfid.Value;
        }
        else
        {
            HySave["CompanyID"] = Mid;
        }
        HySave["IsJointVenture"] = Co.RSQandSQLInjection(seljvventure.SelectedItem.Value, "soft");
        HySave["CompanyName"] = Co.RSQandSQLInjection(tcompanyname.Text.Trim(), "soft");
        HySave["Address"] = Co.RSQandSQLInjection(taddress.Text.Trim(), "soft");
        if (selstate.SelectedItem.Text == "Select State")
        {
            HySave["State"] = null;
        }
        else
        {
            HySave["State"] = Co.RSQandSQLInjection(selstate.SelectedItem.Value, "soft");
        }
        HySave["District"] = Co.RSQandSQLInjection(seldistrict.Text.Trim(), "soft");
        HySave["Pincode"] = Co.RSQandSQLInjection(tpincode.Text.Trim(), "soft");
        //if (ddlNodalOfficerEmail.SelectedItem.Value == "0")
        //{
        //    HySave["NodalOfficeRefNo"] = "0";
        //}
        //else
        //{
        //    HySave["NodalOfficeRefNo"] = ddlNodalOfficerEmail.SelectedItem.Value;
        //}
        HySave["NodalOfficeRefNo"] = "";
        HySave["ContactPersonEmailID"] = Co.RSQandSQLInjection(txtNEmailId.Text.Trim(), "soft");
        HySave["GSTNo"] = Co.RSQandSQLInjection(tgstno.Text.Trim(), "soft");
        HySave["CINNo"] = Co.RSQandSQLInjection(tcinno.Text.Trim(), "soft");
        HySave["PANNo"] = Co.RSQandSQLInjection(tpanno.Text.Trim(), "soft");
        HySave["IECode"] = Co.RSQandSQLInjection(txtIECode.Text.Trim(), "soft");
        HySave["CEOName"] = Co.RSQandSQLInjection(txtceoname.Text.Trim(), "soft");
        HySave["CEOEmail"] = Co.RSQandSQLInjection(txtCEOEmailId.Text.Trim(), "soft");
        HySave["TelephoneNo"] = Co.RSQandSQLInjection(txtTelephone.Text.Trim(), "soft");
        HySave["FaxNo"] = Co.RSQandSQLInjection(txtFaxNo.Text.Trim(), "soft");
        HySave["EmailID"] = Co.RSQandSQLInjection(txtEmailID.Text.Trim(), "soft");
        HySave["Website"] = Co.RSQandSQLInjection(txtWebsite.Text.Trim(), "soft");
        if (rdoYes.Checked == true)
        {
            HySave["Startup"] = "Y";
        }
        else
        {
            HySave["Startup"] = "N";
        }
        HySave["DIPPNumber"] = Co.RSQandSQLInjection(txtDIPP.Text.Trim(), "soft");
        HySave["DIPPMobile"] = Co.RSQandSQLInjection(txtDIPPMobile.Text.Trim(), "soft");
        if (rdoMYes.Checked == true)
        {
            HySave["MSME"] = "Y";
        }
        else
        {
            HySave["MSME"] = "N";
        }
        HySave["VAM"] = Co.RSQandSQLInjection(txtVAM.Text.Trim(), "soft");
        HySave["Aadhar_Mobile"] = Co.RSQandSQLInjection(txtAad_Mob.Text.Trim(), "soft");
        HySave["Facebook"] = Co.RSQandSQLInjection(txtFacebook.Text.Trim(), "soft");
        HySave["Twitter"] = Co.RSQandSQLInjection(txtTwitter.Text.Trim(), "soft");
        HySave["Linkedin"] = Co.RSQandSQLInjection(txtLinkedin.Text.Trim(), "soft");
        HySave["Instagram"] = Co.RSQandSQLInjection(txtInstagram.Text.Trim(), "soft");
        HySave["latitude"] = Co.RSQandSQLInjection(txtlatitude.Text.Trim(), "soft");
        HySave["longitude"] = Co.RSQandSQLInjection(txtlongitude.Text.Trim(), "soft");
        HySave["InterestedArea"] = "";
        HySave["MasterAllowed"] = "";
        HySave["Role"] = "Company";
    }
    protected void SaveFactoryComp()
    {
        if (hfid.Value != "")
        {
            HySave["CompanyID"] = hfid.Value;
        }
        else
        {
            HySave["CompanyID"] = Mid;
        }
        HySave["CompanyRefNo"] = Co.RSQandSQLInjection(Session["CompanyRefNo"].ToString(), "soft");
        HySave["FactoryAddress"] = Co.RSQandSQLInjection(taddress.Text.Trim(), "soft");
        if (selstate.SelectedItem.Text == "Select State")
        {
            HySave["FactoryStateID"] = null;
        }
        else
        {
            HySave["FactoryStateID"] = Co.RSQandSQLInjection(selstate.SelectedItem.Value, "soft");
        }

        HySave["FactoryPincode"] = Co.RSQandSQLInjection(tpincode.Text.Trim(), "soft");
        HySave["FactoryCEOName"] = Co.RSQandSQLInjection(txtceoname.Text.Trim(), "soft");
        HySave["FactoryCEOEmail"] = Co.RSQandSQLInjection(txtCEOEmailId.Text.Trim(), "soft");
        HySave["FactoryTelephoneNo"] = Co.RSQandSQLInjection(txtTelephone.Text.Trim(), "soft");
        HySave["FactoryFaxNo"] = Co.RSQandSQLInjection(txtFaxNo.Text.Trim(), "soft");
        HySave["FactoryEmailID"] = Co.RSQandSQLInjection(txtEmailID.Text.Trim(), "soft");
        HySave["FactoryWebsite"] = Co.RSQandSQLInjection(txtWebsite.Text.Trim(), "soft");
        //if (ddlNodalOfficerEmail.SelectedItem.Value == "0")
        //{
        //    HySave["NodalOfficeRefNo"] = "0";
        //}
        //else
        //{
        //    HySave["NodalOfficeRefNo"] = ddlNodalOfficerEmail.SelectedItem.Value;
        //}
        HySave["NodalOfficeRefNo"] = "";
        HySave["FactoryNodalOfficerEmailId"] = Co.RSQandSQLInjection(txtNEmailId.Text.Trim(), "soft");
        if (selstate.SelectedItem.Text == "Select State")
        {
            HySave["FactoryStateID"] = null;
        }
        else
        {
            HySave["FactoryStateID"] = Co.RSQandSQLInjection(selstate.SelectedItem.Value, "soft");
        }

        HySave["FactoryFacebook"] = Co.RSQandSQLInjection(txtFacebook.Text.Trim(), "soft");
        HySave["FactoryTwitter"] = Co.RSQandSQLInjection(txtTwitter.Text.Trim(), "soft");
        HySave["FactoryLinkedin"] = Co.RSQandSQLInjection(txtLinkedin.Text.Trim(), "soft");
        HySave["FactoryInstagram"] = Co.RSQandSQLInjection(txtInstagram.Text.Trim(), "soft");
        HySave["Factorylatitude"] = Co.RSQandSQLInjection(txtlatitude.Text.Trim(), "soft");
        HySave["Factorylongitude"] = Co.RSQandSQLInjection(txtlongitude.Text.Trim(), "soft");
        HySave["InterestedArea"] = "";
        HySave["MasterAllowed"] = "";
        HySave["Role"] = "Division";
    }
    protected void SaveUnitComp()
    {
        if (hfid.Value != "")
        {
            HySave["CompanyID"] = hfid.Value;
        }
        else
        {
            HySave["CompanyID"] = Mid;
        }
        if (selstate.SelectedItem.Text == "Select State")
        {
            HySave["UnitStateID"] = null;
        }
        else
        {
            HySave["UnitStateID"] = Co.RSQandSQLInjection(selstate.SelectedItem.Value, "soft");
        }
        HySave["CompanyRefNo"] = Co.RSQandSQLInjection(Session["CompanyRefNo"].ToString(), "soft");
        HySave["UnitAddress"] = Co.RSQandSQLInjection(taddress.Text.Trim(), "soft");
        //if (ddlNodalOfficerEmail.SelectedItem.Value == "0")
        //{
        //    HySave["NodalOfficeRefNo"] = "0";
        //}
        //else
        //{
        //    HySave["NodalOfficeRefNo"] = ddlNodalOfficerEmail.SelectedItem.Value;
        //}
        HySave["NodalOfficeRefNo"] = "";
        HySave["UnitNodalOfficerEmailId"] = Co.RSQandSQLInjection(txtNEmailId.Text.Trim(), "soft");
        HySave["UnitPincode"] = Co.RSQandSQLInjection(tpincode.Text.Trim(), "soft");
        HySave["UnitCEOName"] = Co.RSQandSQLInjection(txtceoname.Text.Trim(), "soft");
        HySave["UnitCEOEmail"] = Co.RSQandSQLInjection(txtCEOEmailId.Text.Trim(), "soft");
        HySave["UnitTelephoneNo"] = Co.RSQandSQLInjection(txtTelephone.Text.Trim(), "soft");
        HySave["UnitFaxNo"] = Co.RSQandSQLInjection(txtFaxNo.Text.Trim(), "soft");
        HySave["UnitEmailID"] = Co.RSQandSQLInjection(txtEmailID.Text.Trim(), "soft");
        HySave["UnitWebsite"] = Co.RSQandSQLInjection(txtWebsite.Text.Trim(), "soft");
        HySave["UnitFacebook"] = Co.RSQandSQLInjection(txtFacebook.Text.Trim(), "soft");
        HySave["UnitTwitter"] = Co.RSQandSQLInjection(txtTwitter.Text.Trim(), "soft");
        HySave["UnitLinkedin"] = Co.RSQandSQLInjection(txtLinkedin.Text.Trim(), "soft");
        HySave["UnitInstagram"] = Co.RSQandSQLInjection(txtInstagram.Text.Trim(), "soft");
        HySave["Unitlatitude"] = Co.RSQandSQLInjection(txtlatitude.Text.Trim(), "soft");
        HySave["Unitlongitude"] = Co.RSQandSQLInjection(txtlongitude.Text.Trim(), "soft");
        HySave["InterestedArea"] = "";
        HySave["MasterAllowed"] = "";
        HySave["Role"] = "Unit";
    }
    protected void SaveCompanyMenu()
    {
        HybridDictionary hyMasterCategory = new HybridDictionary();
        hyMasterCategory["CompCatRelationId"] = 0;
        hyMasterCategory["CompanyRefNo"] = Co.RSQandSQLInjection(Session["CompanyRefNo"].ToString(), "soft");
        hyMasterCategory["MCategoryId"] = Co.RSQandSQLInjection(ddlmastercategory.SelectedItem.Value, "soft");
        foreach (ListItem li in chkSubCategory.Items)
        {
            if (li.Selected == true)
            {
                Categoryintrestedare = Categoryintrestedare + "," + li.Value;
            }
        }
        hyMasterCategory["SCategoryId"] = Co.RSQandSQLInjection(Categoryintrestedare.Substring(1).ToString(), "soft");
        string mStrCategory = Lo.SaveMasterCategroyMenu(hyMasterCategory, out _sysMsg, out _msg);
        if (mStrCategory == "Save")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Master Category saved successfully.')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not saved')", true);
        }
    }
    public string ValidatePreview()
    {
        string mCon = "";
        if (tcompanyname.Text == "")
        {
            mCon = "Company name is mandatory";
        }
        if (txtNEmailId.Text == "")
        {
            mCon = "Email is mandatory";
        }
        if (IsValidEmailId(txtNEmailId.Text) == true)
        {
        }
        else
        {
            mCon = "Email format is invalid";
        }
        if (txtCEOEmailId.Text != "")
        {
            if (IsValidEmailId(txtCEOEmailId.Text) == true)
            {
            }
            else
            {
                mCon = "Email format is invalid";
            }
        }
        if (tcinno.Text != "" && hfid.Value == "")
        {
            DataTable Dt = Lo.RetriveMasterData(0, tcinno.Text, "", 0, "", "", "CheckPanMo");
            if (Dt.Rows.Count > 0 && Dt != null)
            {
                mCon = "CIN No already registerd.";
            }
        }
        if (txtNEmailId.Text != "" && hfid.Value == "")
        {
            DataTable Dt = Lo.RetriveMasterData(0, txtNEmailId.Text, "", 0, "", "", "CheckEmailNodel");
            if (Dt.Rows.Count > 0 && Dt != null)
            {
                mCon = "Nodel person email-id already registerd";
            }
        }
        return mCon;
    }
    public static bool IsValidEmailId(string InputEmail)
    {
        string pattern = @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}$";
        Match match = Regex.Match(InputEmail.Trim(), pattern, RegexOptions.IgnoreCase);
        if (match.Success)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    protected void btndemofirst_Click(object sender, EventArgs e)
    {
        if (tcompanyname.Text != "")
        {
            //string msg = this.ValidatePreview();
            //if (msg != "")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + msg + "');", true);
            //}
            //else
            //{
            if (btndemofirst.Text == "Save")
            {
                SaveFDI();
                string StrSaveFDIComp = Lo.SaveMasterCompany(HySave, out _msg, out _sysMsg);
                if (StrSaveFDIComp != "0" && StrSaveFDIComp != "-1")
                {
                    //SaveCompanyMenu();
                    if (hfid.Value != "")
                    {
                        //cleartext();
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Record updated successfully')", true);
                    }
                    else
                    {
                        //cleartext();
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Record save successfully')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not save successfully.')", true);
                }
            }
            else if (btndemofirst.Text == "Save Division")
            {
                SaveFactoryComp();
                string StrSaveFact = Lo.SaveFactoryComp(HySave, out _msg, out _sysMsg);
                if (StrSaveFact != "0" && StrSaveFact != "-1")
                {
                    //SaveFactoryMenu();
                    if (hfid.Value != "")
                    {
                        //cleartext();
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Record updated successfully')", true);
                    }
                    else
                    {
                        //cleartext();
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Record save successfully')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not save successfully.')", true);
                }
            }
            else if (btndemofirst.Text == "Save Unit")
            {
                SaveUnitComp();
                string StrSaveUnit = Lo.SaveUnitComp(HySave, out _msg, out _sysMsg);
                if (StrSaveUnit != "0" && StrSaveUnit != "-1")
                {
                    //SaveFactoryMenu();
                    if (hfid.Value != "")
                    {
                        //cleartext();
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Record updated successfully')", true);
                    }
                    else
                    {
                        //cleartext();
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Record save successfully')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not save successfully.')", true);
                }

            }
            //    }
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Please fill all mandatory field.');", true);
            //}
        }
    }
    protected void BindCompany()
    {
        if (mType == "SuperAdmin" || mType == "Admin")
        {
            if (Request.QueryString["mcurrentcompRefNo"] != null)
            {
                lblSelectCompany.Text = "Company/Organization";
                ddlcompany.Enabled = false;
                if (objCrypto.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Company")
                {
                    DtCompanyDDL = Lo.RetriveMasterData(0, objCrypto.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "Company", 0, "", "", "CompanyName");
                    ddlcompany.Enabled = false;
                    if (DtCompanyDDL.Rows.Count > 0)
                    {
                        Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                        lblSelectCompany.Text = "Company";
                    }
                }
                else if (objCrypto.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Factory")
                {
                    DtCompanyDDL = Lo.RetriveMasterData(0, objCrypto.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "Company1", 0, "", "", "CompanyName");
                    ddlcompany.Enabled = false;
                    if (DtCompanyDDL.Rows.Count > 0)
                    {
                        Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                        lblSelectCompany.Text = "Company";
                    }
                    // DtCompanyDDL = Lo.RetriveMasterData(0, objCrypto.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "Factory", 0, "", "", "CompanyName");
                    DataTable DtDivisionDDL = Lo.RetriveMasterData(0, objCrypto.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "Factory2", 0, "", "", "CompanyName");
                    if (DtDivisionDDL.Rows.Count > 0)
                    {
                        Co.FillDropdownlist(ddldivision, DtDivisionDDL, "FactoryName", "FactoryRefNo");
                        lblselectdivison.Visible = true;
                        ddldivision.Enabled = false;
                        ddlcompany.Enabled = false;
                        ddldivision.Visible = true;
                    }
                    else
                    {
                        ddldivision.Enabled = false;
                    }
                }
                else if (objCrypto.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Unit")
                {
                    DtCompanyDDL = Lo.RetriveMasterData(0, objCrypto.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "Company2", 0, "", "", "CompanyName");
                    ddlcompany.Enabled = false;
                    if (DtCompanyDDL.Rows.Count > 0)
                    {
                        Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                        lblSelectCompany.Text = "Company";
                    }
                    // DtCompanyDDL = Lo.RetriveMasterData(0, objCrypto.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "Unit", 0, "", "", "CompanyName");
                    DataTable DtDivisionDDL = Lo.RetriveMasterData(0, objCrypto.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "Factory3", 0, "", "", "CompanyName");
                    if (DtDivisionDDL.Rows.Count > 0)
                    {
                        Co.FillDropdownlist(ddldivision, DtDivisionDDL, "FactoryName", "FactoryRefNo");
                        lblselectdivison.Visible = true;
                        ddldivision.Enabled = false;
                        ddlcompany.Enabled = false;
                        ddldivision.Visible = true;
                        DataTable DtUnitDDL = Lo.RetriveMasterData(0, objCrypto.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "Unit2", 0, "", "", "CompanyName");
                        if (DtUnitDDL.Rows.Count > 0)
                        {
                            Co.FillDropdownlist(ddlunit, DtUnitDDL, "UnitName", "UnitRefNo");
                            ddlunit.Enabled = true;
                            lblselectunit.Visible = true;
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
                DtCompanyDDL = Lo.RetriveMasterData(0, "", mType, 0, "", "", "Select");
                if (DtCompanyDDL.Rows.Count > 0)
                {
                    Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                    ddlcompany.Enabled = true;
                    lblSelectCompany.Text = "Company/Organization";
                }
                else
                {
                    lblSelectCompany.Text = "Company/Organization";
                    ddlcompany.Enabled = false;
                }
            }
        }
        else if (mType == "Company")
        {
            if (Request.QueryString["mrcreaterole"] != null)
            {
                if (objCrypto.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Company")
                {
                    strCRole = "Company";
                }
                else if (objCrypto.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Factory")
                {
                    strCRole = "Company1";
                    strDRole = "Factory2";
                }
                else if (objCrypto.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Unit")
                {
                    strCRole = "Company2";
                    strDRole = "Factory3";
                    strURole = "Unit2";
                }
            }
            else
            {
            }
            if (Request.QueryString["mcurrentcompRefNo"] != null)
            {
                lblSelectCompany.Text = "Company";
                ddlcompany.Enabled = false;
                DtCompanyDDL = Lo.RetriveMasterData(0, objCrypto.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), strCRole, 0, "", "", "CompanyName");
            }
            else
            {
                DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company1", 0, "", "", "CompanyName");
            }
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                lblSelectCompany.Text = "Company";
                ddlcompany.Enabled = false;
            }
            else
            {
                lblSelectCompany.Text = "Company";
                ddlcompany.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, objCrypto.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), strDRole, 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                if (mType == "Company")
                {
                    lblselectdivison.Visible = true;
                    ddldivision.Enabled = false;
                    ddlunit.Visible = false;
                    ddldivision.Visible = true;
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

            DataTable DtUnitDDL = Lo.RetriveMasterData(0, objCrypto.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), strURole, 0, "", "", "CompanyName");
            if (DtUnitDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlunit, DtUnitDDL, "UnitName", "UnitRefNo");
                ddlunit.Enabled = true;
                lblselectunit.Visible = true;
                ddlunit.Visible = true;
                ddlunit.Enabled = false;
            }
            else
            {
                ddlunit.Enabled = false;
            }
        }
        else if (mType == "Factory" || mType == "Division")
        {
            if (Request.QueryString["mrcreaterole"] != null)
            {

                if (objCrypto.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Company")
                {
                    strCRole = "Company";
                }
                else if (objCrypto.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Factory")
                {
                    strCRole = "Company1";
                    strDRole = "Factory2";
                }
                else if (objCrypto.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Unit")
                {
                    strCRole = "Company2";
                    strDRole = "Factory3";
                    strURole = "Unit2";
                }
            }
            else
            {
            }
            if (Request.QueryString["mcurrentcompRefNo"] != null)
            {
                lblSelectCompany.Text = "Company";
                ddlcompany.Enabled = false;
                DtCompanyDDL = Lo.RetriveMasterData(0, objCrypto.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), strCRole, 0, "", "", "CompanyName");
            }
            else
            {
                DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company1", 0, "", "", "CompanyName");
            }
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                lblSelectCompany.Text = "Company";
                ddlcompany.Enabled = false;
            }
            else
            {
                lblSelectCompany.Text = "Company";
                ddlcompany.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, objCrypto.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), strDRole, 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                lblselectdivison.Visible = true;
                ddldivision.Enabled = false;
                ddlunit.Visible = false;
                ddldivision.Visible = true;

            }
            else
            {
                ddldivision.Enabled = false;
            }

            DataTable DtUnitDDL = Lo.RetriveMasterData(0, objCrypto.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), strURole, 0, "", "", "CompanyName");
            if (DtUnitDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlunit, DtUnitDDL, "UnitName", "UnitRefNo");
                ddlunit.Enabled = true;
                lblselectunit.Visible = true;
                ddlunit.Visible = true;
                ddlunit.Enabled = false;
            }
            else
            {
                ddlunit.Enabled = false;
            }
        }
        else if (mType == "Unit")
        {

            if (Request.QueryString["mrcreaterole"] != null)
            {

                if (objCrypto.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Company")
                {
                    strCRole = "Company";
                }
                else if (objCrypto.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Factory")
                {
                    strCRole = "Company1";
                    strDRole = "Factory2";
                }
                else if (objCrypto.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Unit")
                {
                    strCRole = "Company2";
                    strDRole = "Factory3";
                    strURole = "Unit2";
                }
            }
            else
            {

            }
            if (Request.QueryString["mcurrentcompRefNo"] != null)
            {
                lblSelectCompany.Text = "Company";
                ddlcompany.Enabled = false;
                DtCompanyDDL = Lo.RetriveMasterData(0, objCrypto.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), strCRole, 0, "", "", "CompanyName");
            }
            else
            {
                DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company1", 0, "", "", "CompanyName");
            }
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                lblSelectCompany.Text = "Company";
                ddlcompany.Enabled = false;
            }
            else
            {
                lblSelectCompany.Text = "Company";
                ddlcompany.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, objCrypto.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), strDRole, 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                lblselectdivison.Visible = true;
                ddldivision.Enabled = false;
                ddlunit.Visible = false;
                ddldivision.Visible = true;

            }
            else
            {
                ddldivision.Enabled = false;
            }

            DataTable DtUnitDDL = Lo.RetriveMasterData(0, objCrypto.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), strURole, 0, "", "", "CompanyName");
            if (DtUnitDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlunit, DtUnitDDL, "UnitName", "UnitRefNo");
                ddlunit.Enabled = true;
                lblselectunit.Visible = true;
                ddlunit.Visible = true;
                ddlunit.Enabled = false;
            }
            else
            {
                ddlunit.Enabled = false;
            }

        }
    }
    protected void BindNodelEmail()
    {
        if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.Visible == false)
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "", 0, "", "", "CompanyNodelDetail");
        }
        else if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.SelectedItem.Text != "Select" && ddlunit.Visible == false)
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "", 0, "", "", "CompanyNodelDetail");
        }
        else if (ddlcompany.SelectedItem.Text != "Select" && ddldivision.SelectedItem.Text != "Select" && ddlunit.SelectedItem.Text != "Select")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlunit.SelectedItem.Value, "", 0, "", "", "CompanyNodelDetail");
        }
        if (DtCompanyDDL.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlNodalOfficerEmail, DtCompanyDDL, "NodalOficerName", "NodalOfficerRefNo");
            ddlNodalOfficerEmail.Items.Insert(0, "Select");
        }
    }
    protected void ddlNodalOfficerEmail_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlNodalOfficerEmail.SelectedItem.Text != "Select Nodel Officer")
        {
            DataTable DtGetNodel = Lo.RetriveMasterData(0, ddlNodalOfficerEmail.SelectedItem.Value, "", 0, "", "", "CompleteNodelDetail");
            if (DtGetNodel.Rows.Count > 0)
            {
                txtNName.Text = DtGetNodel.Rows[0]["NodalOficerName"].ToString();
                txtNEmailId.Text = DtGetNodel.Rows[0]["NodalOfficerEmail"].ToString();
                txtNMobile.Text = DtGetNodel.Rows[0]["NodalOfficerMobile"].ToString();
                txtNTelephone.Text = DtGetNodel.Rows[0]["NodalOfficerTelephone"].ToString();
                txtNFaxNo.Text = DtGetNodel.Rows[0]["NodalOfficerFax"].ToString();
                divNodalOfficer.Visible = true;
            }
        }
        else
        {
            divNodalOfficer.Visible = false;
        }
    }
    protected void ddlcompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcompany.SelectedItem.Text != "Select")
        {
            DataTable DtGrid = Lo.RetriveGridViewCompany(ddlcompany.SelectedItem.Value, "", "", "CompanyMainGridView");
            if (DtGrid.Rows.Count > 0)
            {

                EditCOde(DtGrid);
                ddlcompany.Enabled = false;
                DivActivities.Visible = true;
                DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "", 0, "", "", "FactoryCompanyID");
                if (DtCompanyDDL.Rows.Count > 0)
                {
                    Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                    ddldivision.Items.Insert(0, "Select");
                    lblselectdivison.Visible = true;
                    ddldivision.Visible = true;
                }
                else
                {
                    ddldivision.Visible = false;
                    lblselectdivison.Visible = false;
                }

            }
        }
        else
        {
            lblselectunit.Visible = false;
            ddldivision.Visible = false;
            ddlunit.Visible = false;
        }
    }
    protected void ddldivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddldivision.SelectedItem.Text != "Select")
        {
            DataTable DtGrid = Lo.RetriveGridViewCompany(ddlcompany.SelectedItem.Value, ddldivision.SelectedItem.Value, "", "InnerGVFactoryID");
            if (DtGrid.Rows.Count > 0)
            {
                EditDivison(DtGrid);
            }

            DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "", 0, "", "", "UnitSelectID");
            if (DtCompanyDDL.Rows.Count > 0)
            {

                Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
                ddlunit.Items.Insert(0, "Select");
                lblselectunit.Visible = true;
                ddlunit.Visible = true;
            }
            else
            {
                lblselectunit.Visible = false;
                ddlunit.Visible = false;
            }
        }
        else
        {
            lblselectunit.Visible = false;
            ddlunit.Visible = false;
            ddldivision.Visible = true;
            ddlcompany_OnSelectedIndexChanged(sender, e);
        }


    }
    protected void ddlunit_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlunit.SelectedItem.Text != "Select")
        {
            DataTable DtGrid = Lo.RetriveGridViewCompany("", ddldivision.SelectedItem.Value, ddlunit.SelectedItem.Value, "InnerGVUnitID");
            if (DtGrid.Rows.Count > 0)
            {
                EditUnit(DtGrid);
            }
        }
        else
        {
            ddlunit.Visible = true;
            ddldivision_OnSelectedIndexChanged(sender, e);
        }
    }
    protected void cleartext()
    {
        seljvventure.SelectedIndex = 0;
        tcompanyname.Text = "";
        taddress.Text = "";
        selstate.SelectedIndex = 0;
        seldistrict.Text = "";
        tpincode.Text = "";

        tcinno.Text = "";
        tpanno.Text = "";
        tgstno.Text = "";

        companyengaged.SelectedIndex = 0;
        hfid.Value = "";
        txtCEOEmailId.Text = "";
        txtceoname.Text = "";
    }
    protected void rdoMNo_CheckedChanged(object sender, EventArgs e)
    {
        if (rdoMNo.Checked == true)
        {
            divMSMe.Visible = false;
        }
        else
        {
            divMSMe.Visible = true;
        }
    }
    protected void rdoMYes_CheckedChanged(object sender, EventArgs e)
    {
        if (rdoMNo.Checked == true)
        {
            divMSMe.Visible = false;
        }
        else
        {
            divMSMe.Visible = true;
        }
    }
    protected void rdoNo_CheckedChanged(object sender, EventArgs e)
    {
        if (rdoNo.Checked == true)
        {
            divStartup.Visible = false;
        }
        else
        {
            divStartup.Visible = true;
        }

    }
    protected void rdoYes_CheckedChanged(object sender, EventArgs e)
    {
        if (rdoNo.Checked == true)
        {
            divStartup.Visible = false;
        }
        else
        {
            divStartup.Visible = true;
        }
    }
}