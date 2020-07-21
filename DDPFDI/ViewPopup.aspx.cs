using System;
using System.Data;
using System.Web.UI;
using BusinessLayer;

public partial class ViewPopup : System.Web.UI.Page
{
    Logic Lo = new Logic();
    protected void Page_Load(object sender, EventArgs e)
    {
        #region ViewOneProd
        try
        {
            string Role = "";
            string PRefNo = "";
            DataTable DtView = Lo.RetriveProductCode("", "PRO11382", "ProductMasterID", "Company");
            if (DtView.Rows.Count > 0)
            {
                lblrefnoview.Text = "PRO11382";
                lblcompname.Text = DtView.Rows[0]["CompanyName"].ToString();
                if (DtView.Rows[0]["FactoryName"].ToString() != "")
                {
                    lbldiviname.Text = DtView.Rows[0]["FactoryName"].ToString();
                    one.Visible = true;
                }
                else
                {
                    one.Visible = false;
                }
                if (DtView.Rows[0]["UnitName"].ToString() != "")
                {
                    lblunitnamepro.Text = DtView.Rows[0]["UnitName"].ToString();
                    two.Visible = true;
                }
                else
                {
                    two.Visible = false;
                }
                lblnsngroup.Text = DtView.Rows[0]["ProdLevel1Name"].ToString();
                lblnsngroupclass.Text = DtView.Rows[0]["ProdLevel2Name"].ToString();
                lblclassitem.Text = DtView.Rows[0]["ProdLevel3Name"].ToString();
                lblsearchkeywords.Text = DtView.Rows[0]["Searchkeyword"].ToString();
                if (DtView.Rows[0]["ProductDescription"].ToString() != "")
                {
                    itemname2.Text = DtView.Rows[0]["ProductDescription"].ToString();
                    lblitemname1.Text = DtView.Rows[0]["ProductDescription"].ToString();
                    eleven.Visible = true;
                    Tr23.Visible = true;

                }
                else
                {
                    Tr23.Visible = false;
                    eleven.Visible = false;
                }
                if (DtView.Rows[0]["DPSUPartNumber"].ToString() != "")
                {
                    lbldpsupartno.Text = DtView.Rows[0]["DPSUPartNumber"].ToString();
                    three.Visible = true;
                }
                else
                {
                    three.Visible = false;
                }
                if (DtView.Rows[0]["HsnCode8digit"].ToString() != "")
                {
                    lblhsncode8digit.Text = DtView.Rows[0]["HsnCode8digit"].ToString();
                    four.Visible = true;
                }
                else
                {
                    four.Visible = false;
                }
                prodIndustryDomain.Text = DtView.Rows[0]["TechLevel1Name"].ToString();
                ProdIndusSubDomain.Text = DtView.Rows[0]["Techlevel2Name"].ToString();
                if (DtView.Rows[0]["IsProductImported"].ToString() != "")
                {
                    five.Visible = true;
                }
                else
                {
                    five.Visible = false;
                }
                if (DtView.Rows[0]["NSCCode"].ToString() != "")
                {
                    lblnsccode4digit.Text = DtView.Rows[0]["NSCCode"].ToString();
                    six.Visible = true;
                }
                else
                { six.Visible = false; }
                if (DtView.Rows[0]["CountryName"].ToString() != "")
                {
                    lbloemcountry.Text = DtView.Rows[0]["CountryName"].ToString();
                    nine.Visible = true;
                }
                else
                { nine.Visible = false; }
                if (DtView.Rows[0]["OEMName"].ToString() != "")
                {
                    lbloemname.Text = DtView.Rows[0]["OEMName"].ToString();
                    seven.Visible = true;
                }
                else
                { seven.Visible = false; }
                if (DtView.Rows[0]["OEMPartNumber"].ToString() != "")
                {
                    lbloempartno.Text = DtView.Rows[0]["OEMPartNumber"].ToString();
                    eight.Visible = true;
                }
                else
                { eight.Visible = false; }
                if (DtView.Rows[0]["OEMAddress"].ToString() != "")
                {
                    lbloemaddress.Text = DtView.Rows[0]["OEMAddress"].ToString();
                    twentyfive.Visible = true;
                }
                else
                { twentyfive.Visible = false; }
                DataTable DtGridEstimate1 = new DataTable();
                DtGridEstimate1 = Lo.RetriveSaveEstimateGrid("Select", 0, "PRO11382", 0, "", "", "", "", "O");
                if (DtGridEstimate1.Rows.Count > 0)
                {
                    decimal tot = 0;
                    for (int i = 0; DtGridEstimate1.Rows.Count > i; i++)
                    {
                        tot = tot + Convert.ToDecimal(DtGridEstimate1.Rows[i]["EstimatedPrice"]);
                    }
                    gvestimatequanold.DataSource = DtGridEstimate1;
                    gvestimatequanold.DataBind();
                    gvestimatequanold.Visible = true;
                    decimal msumobject = tot; //* qtyimp / 100000;
                    lblvalueimport.Text = msumobject.ToString("F2");
                    ten.Visible = true;
                }
                else
                {
                    gvestimatequanold.Visible = false;
                    lblvalueimport.Text = "0.00";
                    ten.Visible = false;
                }
                DataTable dtPdfBind = Lo.RetriveProductCode("", "PRO11382", "ProductImage", "PDF");
                if (dtPdfBind.Rows.Count > 0)
                {
                    gvpdf.DataSource = dtPdfBind;
                    gvpdf.DataBind();
                    gvpdf.Visible = true;
                    twele.Visible = true;
                }
                else
                {
                    gvpdf.Visible = false;
                    twele.Visible = false;
                }
                DataTable dtImageBindfinal = Lo.RetriveProductCode("", "PRO11382", "ProductImage", "Image");
                if (dtImageBindfinal.Rows.Count > 0)
                {
                    dlimage.DataSource = dtImageBindfinal;
                    dlimage.DataBind();
                    dlimage.Visible = true;
                    thirteen.Visible = true;
                }
                else
                {
                    dlimage.Visible = false;
                    thirteen.Visible = false;
                }
                if (DtView.Rows[0]["FeatursandDetail"].ToString() != "")
                {
                    lblfeaturesanddetail.Text = DtView.Rows[0]["FeatursandDetail"].ToString();
                    fourteen.Visible = true;
                }
                else
                {
                    fourteen.Visible = false;
                }
                DataTable dtProdInfo = Lo.RetriveProductCode("", "PRO11382", "RetriveProdInfo", "");
                if (dtProdInfo.Rows.Count > 0)
                {
                    gvProdInfo.DataSource = dtProdInfo;
                    gvProdInfo.DataBind();
                    gvProdInfo.Visible = true;
                }
                else
                {
                    gvProdInfo.Visible = false;
                }
                DataTable dtestimatequanorprice = Lo.RetriveSaveEstimateGrid("2Select", 0, "PRO11382", 0, "", "", "", "", "F");
                if (dtestimatequanorprice.Rows.Count > 0)
                {
                    gvestimatequanorprice.DataSource = dtestimatequanorprice;
                    gvestimatequanorprice.DataBind();
                    gvestimatequanorprice.Visible = true;
                    fifteen.Visible = true;
                }
                else
                {
                    gvestimatequanorprice.Visible = false;
                    fifteen.Visible = false;
                }
                lblindicate.Text = "";
                if (DtView.Rows[0]["PurposeofProcurement"].ToString() != "")
                {
                    DataTable DTporCat = Lo.RetriveProductCode("", "PRO11382", "ProductPOP", "Company");
                    if (DTporCat.Rows.Count > 0)
                    {
                        lblindicate.Text = "";
                        for (int i = 0; DTporCat.Rows.Count > i; i++)
                        {
                            lblindicate.Text = lblindicate.Text + DTporCat.Rows[i]["SCategoryName"].ToString() + ", ";
                        }
                        lblindicate.Text = lblindicate.Text.Substring(0, lblindicate.Text.Length - 2);
                        sixteen.Visible = true;
                    }
                    else
                    { sixteen.Visible = false; }
                }
                else
                {
                    sixteen.Visible = false;
                }
                lblprocremarks.Text = DtView.Rows[0]["ProcurmentCategoryRemark"].ToString();
                if (DtView.Rows[0]["EOIStatus"].ToString() != "")
                {
                    lbleoirep.Text = DtView.Rows[0]["EOIStatus"].ToString();
                    seventeen.Visible = true;
                }
                else
                { seventeen.Visible = false; }
                if (DtView.Rows[0]["EOIURL"].ToString() != "")
                {
                    lbleoilink.Text = DtView.Rows[0]["EOIURL"].ToString();
                    eighteen.Visible = true;
                }
                else
                { eighteen.Visible = false; }
                if (DtView.Rows[0]["TenderStatus"].ToString() == "Live")
                {
                    lbltendor.Text = "Live";
                }
                else if (DtView.Rows[0]["TenderStatus"].ToString() == "Archive" || DtView.Rows[0]["TenderStatus"].ToString() == "Not Floated")
                {
                    lbltendor.Text = DtView.Rows[0]["TenderStatus"].ToString();
                }
                else
                {
                    lbltendor.Text = "No";
                }
                string Nodel1Id = DtView.Rows[0]["NodelDetail"].ToString();
                if (Nodel1Id.ToString() != "")
                {
                    DataTable dtNodal = Lo.RetriveProductCode(Nodel1Id.ToString(), "", "ProdNodal", "");
                    if (dtNodal.Rows.Count > 0)
                    {
                        lblempname.Text = dtNodal.Rows[0]["NodalOficerName"].ToString();
                        lbldesignation.Text = dtNodal.Rows[0]["Designation"].ToString();
                        lblemailidpro.Text = dtNodal.Rows[0]["NodalOfficerEmail"].ToString();
                        lblmobilenumber.Text = dtNodal.Rows[0]["NodalOfficerMobile"].ToString();
                        lblphonenumber.Text = dtNodal.Rows[0]["NodalOfficerTelephone"].ToString();
                        lblfaxpro.Text = dtNodal.Rows[0]["NodalOfficerFax"].ToString();
                    }
                    else
                    {
                        nineteen.Visible = false;
                    }
                }
                else
                {
                    nineteen.Visible = false;
                }
                if (DtView.Rows[0]["EndUser"].ToString() != "")
                {
                    DataTable DTporCat = Lo.RetriveProductCode("", "PRO11382", "EndUser", "Company");
                    if (DTporCat.Rows.Count > 0)
                    {
                        lblenduser.Text = "";
                        for (int i = 0; DTporCat.Rows.Count > i; i++)
                        {
                            lblenduser.Text = lblenduser.Text + DTporCat.Rows[i]["EndUser"].ToString() + ", ";
                        }
                        lblenduser.Text = lblenduser.Text.Substring(0, lblenduser.Text.Length - 2);
                        twenty.Visible = true;
                    }
                    else
                    { twenty.Visible = false; }
                }
                else
                {
                    twenty.Visible = false;
                }
                if (DtView.Rows[0]["PlatName"].ToString() != "")
                {
                    lbldefenceplatform.Text = DtView.Rows[0]["PlatName"].ToString();
                    twentyone.Visible = true;
                }
                else
                {
                    twentyone.Visible = false;
                }
                if (DtView.Rows[0]["Nomenclature"].ToString() != "")
                {
                    lblnameofdefplat.Text = DtView.Rows[0]["Nomenclature"].ToString();
                    twentytwo.Visible = true;
                }
                else
                {
                    twentytwo.Visible = false;
                }
                if (DtView.Rows[0]["IsShowGeneral"].ToString() != "")
                {
                    if (DtView.Rows[0]["IsShowGeneral"].ToString() == "Y")
                        lblisshowgeneral.Text = "Yes";
                    else
                        lblisshowgeneral.Text = "No";
                    twentyfour.Visible = true;
                }
                else
                {
                    twentyfour.Visible = false;
                }
                if (DtView.Rows[0]["TermConditionImage"].ToString() != "")
                {
                    twentythree.Visible = true;
                }
                else
                {
                    twentythree.Visible = false;
                }
                if (DtView.Rows[0]["QAAgency"].ToString() != "")
                {
                    DataTable DTporCat = Lo.RetriveProductCode("", "PRO11382", "ProductQAAgency", "Company");
                    if (DTporCat.Rows.Count > 0)
                    {
                        lbqa.Text = "";
                        for (int i = 0; DTporCat.Rows.Count > i; i++)
                        {
                            lbqa.Text = lbqa.Text + DTporCat.Rows[i]["SCategoryName"].ToString() + ", ";
                        }
                        lbqa.Text = lbqa.Text.Substring(0, lbqa.Text.Length - 2);
                        twentysix.Visible = true;
                    }
                    else
                    { twentysix.Visible = false; }
                }
                else
                {
                    twentysix.Visible = false;
                }
                if (DtView.Rows[0]["NIINCode"].ToString() != "")
                {
                    Tr8.Visible = true;
                    lblnincode.Text = DtView.Rows[0]["NIINCode"].ToString();
                }
                else
                {
                    Tr8.Visible = false;
                }
                if (DtView.Rows[0]["IsIndeginized"].ToString() != "")
                {

                    if (DtView.Rows[0]["IsIndeginized"].ToString() == "Y")
                    {
                        Tr19.Visible = true;
                        lblisindigenised.Text = "Yes";
                        if (DtView.Rows[0]["ManufactureName"].ToString() != "")
                        {
                            lblmanuname.Text = DtView.Rows[0]["ManufactureName"].ToString();
                            Tr20.Visible = true;
                        }
                        else
                        {
                            Tr20.Visible = false;
                        }
                        if (DtView.Rows[0]["ManufactureAddress"].ToString() != "")
                        {
                            lblmanuaddress.Text = DtView.Rows[0]["ManufactureAddress"].ToString();
                            Tr21.Visible = true;
                        }
                        else
                        {
                            Tr21.Visible = false;
                        }
                        if (DtView.Rows[0]["YearofIndiginization"].ToString() != "")
                        {
                            lblyearofindi.Text = DtView.Rows[0]["FY"].ToString();
                            Tr22.Visible = true;
                        }
                        else
                        {
                            Tr22.Visible = false;
                        }
                    }
                    else
                    {
                        lblisindigenised.Text = "No";
                        Tr20.Visible = false;
                        Tr21.Visible = false;
                        Tr22.Visible = false;
                    }
                }
                else
                {
                    Tr19.Visible = false;
                    Tr20.Visible = false;
                    Tr21.Visible = false;
                    Tr22.Visible = false;
                }
                if (DtView.Rows[0]["IndTargetYear"].ToString() != "")
                {
                    lblindtrgyr.Text = DtView.Rows[0]["IndTargetYear"].ToString().Substring(0, DtView.Rows[0]["IndTargetYear"].ToString().Length - 1);
                    if (lblindtrgyr.Text == "NIL")
                    { Tr25.Visible = false; }
                    else
                    {
                        Tr25.Visible = true;
                    }
                }
                else
                {
                    Tr25.Visible = false;
                }
                if (DtView.Rows[0]["IndProcess"].ToString() != "")
                {
                    lblprocstart.Text = DtView.Rows[0]["IndProcess"].ToString();
                    Tr24.Visible = true;
                }
                else
                {
                    Tr24.Visible = false;
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "ProductCompany", "showPopup();", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
        #endregion
    }
}