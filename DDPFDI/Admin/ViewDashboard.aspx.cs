using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Encryption;
using System.Data;

public partial class Admin_ViewDashboard : System.Web.UI.Page
{
    Logic Lo = new Logic();
    DataTable DtGrid = new DataTable();
    Cryptography Encrypt = new Cryptography();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                this.ControlGrid(Encrypt.DecryptData(Request.QueryString["id"].ToString()));
            }
        }
    }

    private void ControlGrid(string mVal)
    {

        if (mVal == "C")
        {
            gvcompanydetail.Visible = true;
            gvfactory.Visible = false;
            gvunit.Visible = false;
            gvViewNodalOfficer.Visible = false;
            gvproduct.Visible = false;
            BindCompany();
        }
        else if (mVal == "D")
        {
            gvcompanydetail.Visible = false;
            gvfactory.Visible = true;
            gvunit.Visible = false;
            gvViewNodalOfficer.Visible = false;
            gvproduct.Visible = false;
            BindDivision();
        }
        else if (mVal == "U")
        {
            gvcompanydetail.Visible = false;
            gvfactory.Visible = false;
            gvunit.Visible = true;
            gvViewNodalOfficer.Visible = false;
            gvproduct.Visible = false;
            BindUnit();
        }
        else if (mVal == "E")
        {
            gvcompanydetail.Visible = false;
            gvfactory.Visible = false;
            gvunit.Visible = false;
            gvViewNodalOfficer.Visible = true;
            gvproduct.Visible = false;
            BindEmployee();
        }
        else if (mVal == "P")
        {
            gvcompanydetail.Visible = false;
            gvfactory.Visible = false;
            gvunit.Visible = false;
            gvViewNodalOfficer.Visible = false;
            gvproduct.Visible = true;
            BindProduct();
        }
        else
        {
            BindCompany();
            BindDivision();
            BindUnit();
            BindEmployee();
            BindProduct();
        }

    }
    protected void BindCompany()
    {
        DataTable DtGrid = Lo.GetDashboardData("Company");
        if (DtGrid.Rows.Count > 0)
        {
            gvcompanydetail.DataSource = DtGrid;
            gvcompanydetail.DataBind();
            lbltotal.Text = "Total Records:- " + gvcompanydetail.Rows.Count.ToString();
            divcompanyGrid.Visible = true;
        }
        else
            divcompanyGrid.Visible = false;
    }
    protected void BindDivision()
    {
        DataTable DtGrid = Lo.GetDashboardData("Division");
        if (DtGrid.Rows.Count > 0)
        {
            gvfactory.DataSource = DtGrid;
            gvfactory.DataBind();
            lbltotal.Text = "Total Records:- " + gvfactory.Rows.Count.ToString();
            divfactorygrid.Visible = true;
        }
        else
            divfactorygrid.Visible = true;
    }
    protected void BindUnit()
    {
        DataTable DtGrid = Lo.GetDashboardData("Unit");
        if (DtGrid.Rows.Count > 0)
        {
            gvunit.DataSource = DtGrid;
            gvunit.DataBind();
            lbltotal.Text = "Total Records:- " + gvunit.Rows.Count.ToString();
            divunitGrid.Visible = true;
        }
        else
            divunitGrid.Visible = true;
    }
    protected void BindEmployee()
    {
        DataTable DtGrid = Lo.GetDashboardData("Employee");
        if (DtGrid.Rows.Count > 0)
        {
            gvViewNodalOfficer.DataSource = DtGrid;
            gvViewNodalOfficer.DataBind();
            lbltotal.Text = "Total Records:- " + gvViewNodalOfficer.Rows.Count.ToString();
            divEmployeeNodalGrid.Visible = true;
        }
        else
            divEmployeeNodalGrid.Visible = false;
    }
    protected void BindProduct()
    {
        DtGrid = Lo.GetDashboardData("Product");
        if (DtGrid.Rows.Count > 0)
        {
            gvproduct.DataSource = DtGrid;
            gvproduct.DataBind();
            lbltotal.Text = "Total Records:- " + gvproduct.Rows.Count.ToString();
            divProductGrid.Visible = true;
        }
    }
    #region RowCommand
    protected void gvcompanydetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable DtView = Lo.RetriveGridViewCompany(e.CommandArgument.ToString(), "", "", "CompanyMainGridView");
        if (DtView.Rows.Count > 0)
        {
            lblrefno.Text = DtView.Rows[0]["CompanyRefNo"].ToString();
            lblcompanyname.Text = DtView.Rows[0]["CompanyName"].ToString();
            lbladdress.Text = DtView.Rows[0]["Address"].ToString();
            lblstate.Text = DtView.Rows[0]["StateName"].ToString();
            lblpanno.Text = DtView.Rows[0]["PANNo"].ToString();
            lblpincode.Text = DtView.Rows[0]["Pincode"].ToString();
            lblcinno.Text = DtView.Rows[0]["CINNo"].ToString();
            lblceoname.Text = DtView.Rows[0]["CEOName"].ToString();
            lblceoemail.Text = DtView.Rows[0]["CEOEmail"].ToString();
            lblTelephoneNo.Text = DtView.Rows[0]["TelephoneNo"].ToString();
            lblFaxNo.Text = DtView.Rows[0]["FaxNo"].ToString();
            lblEmailID.Text = DtView.Rows[0]["EmailID"].ToString();
            lblWebsite.Text = DtView.Rows[0]["Website"].ToString();
            lblGSTNo.Text = DtView.Rows[0]["GSTNo"].ToString();
            lblNodalEmail.Text = DtView.Rows[0]["ContactPersonEmailID"].ToString();
            lblNodalOfficerName.Text = DtView.Rows[0]["NodalOficerName"].ToString();
            lblAad_Mobile.Text = DtView.Rows[0]["latitude"].ToString();
            lblLongitude.Text = DtView.Rows[0]["longitude"].ToString();
            lblFacebook.Text = DtView.Rows[0]["Facebook"].ToString();
            lblInstagram.Text = DtView.Rows[0]["Instagram"].ToString();
            lblTwitter.Text = DtView.Rows[0]["Twitter"].ToString();
            lblLinkedin.Text = DtView.Rows[0]["Linkedin"].ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divCompany", "showPopup();", true);
        }
    }
    protected void gvfactory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable DtView = Lo.RetriveGridViewCompany("", e.CommandArgument.ToString(), "", "DisplayFactory");
        if (DtView.Rows.Count > 0)
        {
            lblfacrefno.Text = DtView.Rows[0]["FactoryRefNo"].ToString();
            lblDivAddress.Text = DtView.Rows[0]["FactoryAddress"].ToString();
            lblDivName.Text = DtView.Rows[0]["FactoryName"].ToString();
            lblDivEmail.Text = DtView.Rows[0]["FactoryEmailID"].ToString();
            lblDivWebsite.Text = DtView.Rows[0]["FactoryWebsite"].ToString();
            lblDivPincode.Text = DtView.Rows[0]["FactoryPincode"].ToString();
            lblDivCeoName.Text = DtView.Rows[0]["FactoryCEOName"].ToString();
            lblDivCeoEmail.Text = DtView.Rows[0]["FactoryCEOEmail"].ToString();
            lblDivFax.Text = DtView.Rows[0]["FactoryFaxNo"].ToString();
            lblDivState.Text = DtView.Rows[0]["StateName"].ToString();
            lblDivConNo.Text = DtView.Rows[0]["FactoryTelephoneNo"].ToString();
            lblDivlatitude.Text = DtView.Rows[0]["Factorylatitude"].ToString();
            lblDivLongitude.Text = DtView.Rows[0]["Factorylongitude"].ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divfactoryshow", "showPopup1();", true);
        }

    }
    protected void gvunit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable DtView = Lo.RetriveGridViewCompany("", "", e.CommandArgument.ToString(), "GVUnitID");
        if (DtView.Rows.Count > 0)
        {
            lblurefno.Text = DtView.Rows[0]["UnitRefNo"].ToString();
            lblUnitAddress.Text = DtView.Rows[0]["UnitAddress"].ToString();
            lblUnitName.Text = DtView.Rows[0]["UnitName"].ToString();
            lblUnitEmail.Text = DtView.Rows[0]["UnitEmailId"].ToString();
            lblUnitPin.Text = DtView.Rows[0]["UnitPincode"].ToString();
            lblUnitWebsite.Text = DtView.Rows[0]["UnitWebsite"].ToString();
            lblUnitState.Text = DtView.Rows[0]["StateName"].ToString();
            lblUnitCeoName.Text = DtView.Rows[0]["UnitCEOName"].ToString();
            lblUnitCeoEmail.Text = DtView.Rows[0]["UnitCEOEmail"].ToString();
            lblUnitFacebook.Text = DtView.Rows[0]["UnitFacebook"].ToString();
            lblUnitInsta.Text = DtView.Rows[0]["UnitInstagram"].ToString();
            lblUnitLink.Text = DtView.Rows[0]["UnitLinkedin"].ToString();
            lblUnitTwitter.Text = DtView.Rows[0]["UnitTwitter"].ToString();
            lblUnitNodalEmail.Text = DtView.Rows[0]["UnitNodalOfficerEmailId"].ToString();
            lblUnitLatitude.Text = DtView.Rows[0]["Unitlatitude"].ToString();
            lblUnitLongitude.Text = DtView.Rows[0]["Unitlongitude"].ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divunitshow", "showPopup2();", true);
        }
    }
    private string stpsdq;
    private string Financial;
    private string Testing;
    private string Certification;
    protected void gvproduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        int rowIndex = gvr.RowIndex;
        string Role = (gvproduct.Rows[rowIndex].FindControl("hfrole") as HiddenField).Value;
        DataTable DtView = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductMasterID", Role);
        if (DtView.Rows.Count > 0)
        {
            lblcomprefno.Text = DtView.Rows[0]["CompanyRefNo"].ToString();
            lblcompname.Text = DtView.Rows[0]["CompanyName"].ToString();
            lbldiviname.Text = DtView.Rows[0]["DivisionName"].ToString();
            lblunitname.Text = DtView.Rows[0]["UnitName"].ToString();
            lblprodrefno.Text = DtView.Rows[0]["ProductRefNo"].ToString();
            lblprodlevel1.Text = DtView.Rows[0]["ProdLevel1Name"].ToString();
            productlevel2.Text = DtView.Rows[0]["ProdLevel2Name"].ToString();
            lblprodlevel3.Text = DtView.Rows[0]["ProdLevel3Name"].ToString();
            lblnsccode.Text = DtView.Rows[0]["NSCCode"].ToString();
            lblniincode.Text = DtView.Rows[0]["NIINCode"].ToString();
            lblproductdescription.Text = DtView.Rows[0]["ProductDescription"].ToString();
            lbloempartnumber.Text = DtView.Rows[0]["OEMPartNumber"].ToString();
            lbloemname.Text = DtView.Rows[0]["OEMName"].ToString();
            lbloemcountry.Text = DtView.Rows[0]["CountryName"].ToString();
            lbldpsupartno.Text = DtView.Rows[0]["DPSUPartNumber"].ToString();
            lblenduserpartno.Text = DtView.Rows[0]["EndUserPartNumber"].ToString();
            lblhsncode.Text = DtView.Rows[0]["HSNCode"].ToString();
            lbltechlevel1.Text = DtView.Rows[0]["TechLevel1Name"].ToString();
            lbltechlevel2.Text = DtView.Rows[0]["Techlevel2Name"].ToString();
            lbltechlevel3.Text = DtView.Rows[0]["Techlevel3Name"].ToString();
            lblplatform.Text = DtView.Rows[0]["PlatName"].ToString();
            lblnomenclatureofmainsystem.Text = DtView.Rows[0]["Nomenclature"].ToString();
            lblenduser.Text = DtView.Rows[0]["EUserName"].ToString();
            lblpurposeofprocurement.Text = DtView.Rows[0]["PRrocurement"].ToString();
            lblsearchkeyword.Text = DtView.Rows[0]["SearchKeyword"].ToString();
            lblprodalredyindeginized.Text = DtView.Rows[0]["IsIndeginized"].ToString();
            if (lblprodalredyindeginized.Text == "Y")
            {
                lblprodalredyindeginized.Text = "Yes";
                tablemanufacturename.Visible = true;
                tablemanufacturename1.Visible = true;
                tablemanufactureAddress.Visible = true;
                tablemanufactureYear.Visible = true;
                tablemanufacturename1.Visible = true;
                tablemanufacturename2.Visible = true;
                tablemanufacturename3.Visible = true;
                lblmanufacturename.Text = DtView.Rows[0]["ManufactureName"].ToString();
                lblmanaddress.Text = DtView.Rows[0]["ManufactureAddress"].ToString();
                lblyearofindiginization.Text = DtView.Rows[0]["YearofIndiginization"].ToString();
            }
            else
            {
                lblprodalredyindeginized.Text = "No";
                tablemanufacturename1.Visible = false;
                tablemanufacturename.Visible = false;
                tablemanufactureAddress.Visible = false;
                tablemanufactureYear.Visible = false;
                tablemanufacturename1.Visible = false;
                tablemanufacturename2.Visible = false;
                tablemanufacturename3.Visible = false;
            }
            DataTable dtImageBind = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductImage", "");
            if (dtImageBind.Rows.Count > 0)
            {
                dlimage.DataSource = dtImageBind;
                dlimage.DataBind();
                dlimage.Visible = true;
            }
            else
            {
                dlimage.Visible = false;
            }
            DataTable dtpsdq = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductPSDQ", "");
            if (dtpsdq.Rows.Count > 0)
            {
                for (int i = 0; dtpsdq.Rows.Count > i; i++)
                {
                    stpsdq = stpsdq + "," + dtpsdq.Rows[i]["SCategoryName"].ToString();
                }
            }
            if (stpsdq != null)
            {
                lblsupportprovidedbydpsu.Text = stpsdq.Substring(1).ToString();
            }
            lblremarks.Text = DtView.Rows[0]["Remarks"].ToString();
            DataTable dtFinn = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductFinancial", "");
            if (dtFinn.Rows.Count > 0)
            {
                for (int i = 0; dtFinn.Rows.Count > i; i++)
                {
                    Financial = Financial + "," + dtFinn.Rows[i]["SCategoryName"].ToString();
                }
            }
            if (Financial != null)
            {
                lblfinancial.Text = Financial.Substring(1).ToString();
            }
            lblfinancialRemark.Text = DtView.Rows[0]["FinancialRemark"].ToString();
            lblestimatedquantity.Text = DtView.Rows[0]["Estimatequantity"].ToString();
            lblestimatedprice.Text = DtView.Rows[0]["EstimatePriceLLP"].ToString();
            lbltenderstatus.Text = DtView.Rows[0]["TenderStatus"].ToString();
            string tensub = DtView.Rows[0]["TenderSubmition"].ToString();
            if (tensub.ToString() == "Y")
            {
                lbltendersubmission.Text = "Yes";
            }
            else
            {
                lbltendersubmission.Text = "No";
            }
            if (DtView.Rows[0]["TenderFillDate"].ToString() != "")
            {
                DateTime tenderdate = Convert.ToDateTime(DtView.Rows[0]["TenderFillDate"].ToString());
                string tDate = tenderdate.ToString("dd-MMM-yyyy");
                lbltenderdate.Text = tDate.ToString();
            }
            lbltenderurl.Text = DtView.Rows[0]["TenderUrl"].ToString();
            DataTable dtNodal = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductNodal", "");
            if (dtNodal.Rows.Count > 0)
            {
                lblempcode.Text = dtNodal.Rows[0]["NodalEmpCode"].ToString();
                lbldesignation.Text = dtNodal.Rows[0]["Designation"].ToString();
                lblemailprod.Text = dtNodal.Rows[0]["NodalOfficerEmail"].ToString();
                lblmobilenumber.Text = dtNodal.Rows[0]["NodalOfficerMobile"].ToString();
                lblphonenumber.Text = dtNodal.Rows[0]["NodalOfficerTelephone"].ToString();
                lblfaxprod.Text = dtNodal.Rows[0]["NodalOfficerFax"].ToString();

                if (dtNodal.Rows.Count == 2)
                {
                    tablenodal2.Visible = true;
                    lblempcode2.Text = dtNodal.Rows[1]["NodalEmpCode"].ToString();
                    lbldesignation2.Text = dtNodal.Rows[1]["Designation"].ToString();
                    lblemailid2.Text = dtNodal.Rows[1]["NodalOfficerEmail"].ToString();
                    lblmobileno2.Text = dtNodal.Rows[1]["NodalOfficerMobile"].ToString();
                    lblphoneno2.Text = dtNodal.Rows[1]["NodalOfficerTelephone"].ToString();
                    lblfax2.Text = dtNodal.Rows[1]["NodalOfficerFax"].ToString();
                }
                else
                {
                    tablenodal2.Visible = false;
                }
            }
            DataTable dttesting = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductTesting", "");
            if (dttesting.Rows.Count > 0)
            {
                for (int i = 0; dttesting.Rows.Count > i; i++)
                {
                    Testing = Testing + "," + dttesting.Rows[i]["SCategoryName"].ToString();
                }
            }
            if (Testing != null)
            {
                lbltesting.Text = Testing.Substring(1).ToString();
            }
            lbltestingremarks.Text = DtView.Rows[0]["Remarks"].ToString();
            DataTable dtcertification = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductCertification", "");
            if (dtcertification.Rows.Count > 0)
            {
                for (int i = 0; dtcertification.Rows.Count > i; i++)
                {
                    Certification = Certification + "," + dtcertification.Rows[i]["SCategoryName"].ToString();
                }
            }
            if (Certification != null)
            {
                lblcertification.Text = Certification.Substring(1).ToString();
            }
            lblcertificationremarks.Text = DtView.Rows[0]["Remarks"].ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "ProductCompany", "showPopup4();", true);
        }
    }
    protected void gvViewNodalOfficer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        int rowIndex = gvr.RowIndex;
        string Role = (gvViewNodalOfficer.Rows[rowIndex].FindControl("hfnodalrole") as HiddenField).Value;
        if (Role == "Unit")
        {
            Role = "UnitID";
        }
        else if (Role == "Division" || Role == "Factory")
        {
            Role = "DivisionID";
        }
        else if (Role == "Company")
        {
            Role = "CompanyID";
        }
        DataTable DtView = new DataTable();
        DtView = Lo.RetriveAllNodalOfficer(e.CommandArgument.ToString(), Role);
        if (DtView.Rows.Count > 0)
        {
            lblNodalComp.Text = DtView.Rows[0]["CompanyName"].ToString();
            lblDivision.Text = DtView.Rows[0]["FactoryName"].ToString();
            if (Role == "DivisionID")
            {
                lblUnit.Text = "";
            }
            else
            { lblUnit.Text = DtView.Rows[0]["UnitName"].ToString(); }
            lblNodalOfficerRefNo.Text = DtView.Rows[0]["NodalOfficerRefNo"].ToString();
            lblNodalOficerName.Text = DtView.Rows[0]["NodalOficerName"].ToString();
            lblNodalEmpCode.Text = DtView.Rows[0]["NodalEmpCode"].ToString();
            lblEmail.Text = DtView.Rows[0]["NodalOfficerEmail"].ToString();
            lblMobile.Text = DtView.Rows[0]["NodalOfficerMobile"].ToString();
            lblTelephone.Text = DtView.Rows[0]["NodalOfficerTelephone"].ToString();
            lblFax.Text = DtView.Rows[0]["NodalOfficerFax"].ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ViewNodalDetail", "showPopup3();", true);
        }
    }
    #endregion
}