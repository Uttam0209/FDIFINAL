using System;
using System.Data;
using BusinessLayer;
using Encryption;
using System.Collections.Specialized;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using System.Net;

public partial class Admin_CompanyDetail : System.Web.UI.Page
{
    Logic Lo = new Logic();
    HybridDictionary HySave = new HybridDictionary();
    Cryptography objCrypto = new Cryptography();
    Int64 Mid = 0;
    DataUtility Co = new DataUtility();
    string _msg = string.Empty;
    string _sysMsg = string.Empty;
    DataTable DtView = new DataTable();
    string currentPage = "";
    string lbltypelogin = "";
    private string mType = "";
    private string mRefNo = "";
    DataTable DtCompanyDDL = new DataTable();
    DataTable dtViewDefault = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            try
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

                BindCompany();

                EditCOde(dtViewDefault);

                BindMasterCategory();
            }
            catch (Exception ex)
            {
                Response.RedirectToRoute("Login");
            }
        }
    }

    protected void BindMasterCategory()
    {
        DataTable DtMasterCategroy = Lo.RetriveMasterCategoryDate(0, "", "", "Select");
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
        BindMasterSubCategory();
    }

    protected void BindMasterSubCategory()
    {
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(Convert.ToInt16(ddlmastercategory.SelectedItem.Value), "", "", "SubSelectID");
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillCheckBox(chkSubCategory, DtMasterCategroy, "SCategoryName", "SCategoryId");
            
        }
        else
        {
           
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
                lblCName.Text = "Company/Organization Name";
                tcompanyname.ReadOnly = true;
                taddress.Text = DtView.Rows[0]["Address"].ToString();

                selstate.SelectedItem.Value = DtView.Rows[0]["State"].ToString();

                txtceoname.Text = DtView.Rows[0]["CEOName"].ToString();
                tpincode.Text = DtView.Rows[0]["Pincode"].ToString();
                tpersonname.Text = DtView.Rows[0]["ContactPersonName"].ToString();
                temailid.Text = DtView.Rows[0]["ContactPersonEmailID"].ToString();
                temailid.ReadOnly = true;
                tcontactno.Text = DtView.Rows[0]["ContactPersonContactNo"].ToString();
                tgstno.Text = DtView.Rows[0]["GSTNo"].ToString();
                tcinno.Text = DtView.Rows[0]["CINNo"].ToString();
                tpanno.Text = DtView.Rows[0]["PANNo"].ToString();
                thssnono.Text = DtView.Rows[0]["HSNO"].ToString();
                txtCEOEmailId.Text = DtView.Rows[0]["CEOEmail"].ToString();
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
                DtView = Lo.RetriveGridViewCompany(Session["CompanyRefNo"].ToString(), "", "", "CompanyMainGridView");
            }

            if (DtView.Rows.Count > 0)
            {
                hfid.Value = DtView.Rows[0]["FactoryID"].ToString();
                seljvventure.Visible = false;
                tcompanyname.Text = DtView.Rows[0]["FactoryName"].ToString();
                divmcName.Visible = false;
                divmName.Visible = true;
                lblMCName.Text = "Divison/Plant Name";
                txtMCName.Text = DtView.Rows[0]["FactoryName"].ToString();
                lblCName.Text = "Divison/Plant Name";
                tcompanyname.ReadOnly = true;
                taddress.Text = DtView.Rows[0]["FactoryAddress"].ToString();

                selstate.SelectedItem.Value = DtView.Rows[0]["FactoryStateID"].ToString();

                DivCEOName.Visible = true;
                tpincode.Text = DtView.Rows[0]["FactoryPincode"].ToString();
                tpersonname.Text = DtView.Rows[0]["FactoryNodalPerson"].ToString();
                temailid.Text = DtView.Rows[0]["FactoryEmailId"].ToString();
                temailid.ReadOnly = true;
                tcontactno.Text = DtView.Rows[0]["FactoryContactNo"].ToString();
                tgstno.Text = DtView.Rows[0]["FactoryGSTNo"].ToString();
                tcinno.Text = DtView.Rows[0]["FactoryCINNo"].ToString();
                tpanno.Text = DtView.Rows[0]["FactoryPANNo"].ToString();
                thssnono.Text = DtView.Rows[0]["FactoryHSNO"].ToString();
                DivCEOEmail.Visible = true;
                lbltypelogin = DtView.Rows[0]["Role"].ToString();
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
                    DivPAN.Visible = true;
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
                DtView = Lo.RetriveGridViewCompany(Session["CompanyRefNo"].ToString(), "", "", "CompanyMainGridView");
            }

            if (DtView.Rows.Count > 0)
            {
                hfid.Value = DtView.Rows[0]["UnitID"].ToString();
                seljvventure.Visible = false;
                divmName.Visible = true;
                divmcName.Visible = true;
                tcompanyname.Text = DtView.Rows[0]["UnitName"].ToString();
                lblCName.Text = "Unit Name";
                tcompanyname.ReadOnly = true;
                taddress.Text = DtView.Rows[0]["UnitAddress"].ToString();

                selstate.SelectedItem.Value = DtView.Rows[0]["UnitStateID"].ToString();

                DivCEOName.Visible = true;
                tpincode.Text = DtView.Rows[0]["UnitPincode"].ToString();
                tpersonname.Text = DtView.Rows[0]["UnitNodalPerson"].ToString();
                temailid.Text = DtView.Rows[0]["UnitEmailId"].ToString();
                temailid.ReadOnly = true;
                tcontactno.Text = DtView.Rows[0]["UnitContactNo"].ToString();
                DivCEOEmail.Visible = true;
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
        HySave["ContactPersonName"] = Co.RSQandSQLInjection(tpersonname.Text.Trim(), "soft");
        HySave["ContactPersonEmailID"] = Co.RSQandSQLInjection(temailid.Text.Trim(), "soft");
        if (tcontactno.Text == "")
        {
            HySave["ContactPersonContactNo"] = null;
        }
        else
        {
            HySave["ContactPersonContactNo"] = Co.RSQandSQLInjection(tcontactno.Text.Trim(), "soft");
        }
        HySave["GSTNo"] = Co.RSQandSQLInjection(tgstno.Text.Trim(), "soft");
        HySave["CINNo"] = Co.RSQandSQLInjection(tcinno.Text.Trim(), "soft");
        HySave["PANNo"] = Co.RSQandSQLInjection(tpanno.Text.Trim(), "soft");
        HySave["HSNo"] = Co.RSQandSQLInjection(thssnono.Text.Trim(), "soft");
        HySave["IsDefenceActivity"] = Co.RSQandSQLInjection(companyengaged.SelectedItem.Value, "soft");
        HySave["CEOName"] = Co.RSQandSQLInjection(txtceoname.Text, "soft");
        HySave["CEOEmail"] = Co.RSQandSQLInjection(txtCEOEmailId.Text, "soft");
        HySave["InterestedArea"] = "";
        HySave["MasterAllowed"] = "";
        HySave["Role"] = "";
    }
    public string ValidatePreview()
    {
        string mCon = "";
        if (tcompanyname.Text == "")
        {
            mCon = "Company name is mandatory";
        }
        if (temailid.Text == "")
        {
            mCon = "Email is mandatory";
        }
        if (IsValidEmailId(temailid.Text) == true)
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
        if (temailid.Text != "" && hfid.Value == "")
        {
            DataTable Dt = Lo.RetriveMasterData(0, temailid.Text, "", 0, "", "", "CheckEmailNodel");
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
            return true;
        else
            return false;
    }
    protected void btndemofirst_Click(object sender, EventArgs e)
    {
        //dd();

        if (tcompanyname.Text != "" && temailid.Text != "")
        {
            string msg = this.ValidatePreview();
            if (msg != "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + msg + "');", true);
            }
            else
            {
                SaveFDI();
                string StrSaveFDIComp = Lo.SaveMasterCompany(HySave, out _msg, out _sysMsg);
                if (StrSaveFDIComp != "0" && StrSaveFDIComp != "-1")
                {
                    if (hfid.Value != "")
                    {
                        cleartext();
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Record updated successfully');window.location='Add-Company';", true);
                    }
                    else
                    {
                        cleartext();
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Record save successfully');window.location='Add-Company';", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not save successfully.')", true);
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Please fill all mandatory field.');", true);
        }
    }
    protected void BindCompany()
    {
        if (mType == "SuperAdmin")
        {
            if (Request.QueryString["mcurrentcompRefNo"] != null)
            {
                lblSelectCompany.Text = "Company/Organization Name";
                ddlcompany.Enabled = false;
                DtCompanyDDL = Lo.RetriveMasterData(0, objCrypto.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "Company", 0, "", "", "CompanyName");
            }
            else
            {
                DtCompanyDDL = Lo.RetriveMasterData(0, "", mType, 0, "", "", "Select");
            }
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = true;
                lblSelectCompany.Text = "Company/Organization Name";
            }
            else
            {
                lblSelectCompany.Text = "Company/Organization Name";
                ddlcompany.Enabled = false;
            }

            ddldivision.Visible = false;
            ddlunit.Visible = false;
        }
        else if (mType == "Admin")
        {

        }
        else if (mType == "Company")
        {
            if (Request.QueryString["mcurrentcompRefNo"] != null)
            {
                lblSelectCompany.Text = "Company/Organization Name";
                ddlcompany.Enabled = false;
                DtCompanyDDL = Lo.RetriveMasterData(0, objCrypto.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "Company", 0, "", "", "CompanyName");
            }
            else
            {
                DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company", 0, "", "", "CompanyName");
            }
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                lblSelectCompany.Text = "Company Name";
                ddlcompany.Enabled = false;
            }
            else
            {
                lblSelectCompany.Text = "Company Name";
                ddlcompany.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                ddldivision.Items.Insert(0, "All");
                if (mType == "Company")
                {
                    lblselectdivison.Visible = true;
                    ddldivision.Enabled = true;
                    ddlunit.Visible = false;
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
                lblSelectCompany.Text = "Company Name";
            }
            else
            {
                ddlcompany.Enabled = false;
                lblSelectCompany.Text = "Company Name";
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
                ddlunit.Items.Insert(0, "All");
                ddlunit.Enabled = true;
                lblselectunit.Visible = true;
            }
            else
            {
                ddlunit.Enabled = false;
            }
        }
        else if (mType == "Unit")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company2", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = false;
                lblSelectCompany.Text = "Company Name";
            }
            else
            {
                ddlcompany.Enabled = false;
                lblSelectCompany.Text = "Company Name";
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
                ddlunit.Enabled = false;
                lblselectunit.Visible = true;
                DataTable DtGrid = Lo.RetriveGridViewCompany("", ddldivision.SelectedItem.Value, ddlunit.SelectedItem.Value, "InnerGVUnitID");
                if (DtGrid.Rows.Count > 0)
                {
                    EditUnit(DtGrid);
                }
            }
            else
            {
                ddlunit.Enabled = false;
            }
        }
    }
    protected void ddlcompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcompany.SelectedItem.Text != "All")
        {
            DataTable DtGrid = Lo.RetriveGridViewCompany(ddlcompany.SelectedItem.Value, "", "", "CompanyMainGridView");
            if (DtGrid.Rows.Count > 0)
            {
                EditCOde(DtGrid);
                DivActivities.Visible = true;
                DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "", 0, "", "", "FactoryCompanyID");
                if (DtCompanyDDL.Rows.Count > 0)
                {
                    Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                    ddldivision.Items.Insert(0, "All");
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

        if (ddldivision.SelectedItem.Text != "All")
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
                ddlunit.Items.Insert(0, "All");
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

        if (ddlunit.SelectedItem.Text != "All")
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
        tpersonname.Text = "";
        temailid.Text = "";
        tcontactno.Text = "";
        tcinno.Text = "";
        tpanno.Text = "";
        tgstno.Text = "";
        thssnono.Text = "";
        companyengaged.SelectedIndex = 0;
        hfid.Value = "";
        txtCEOEmailId.Text = "";
        txtceoname.Text = "";
    }

}