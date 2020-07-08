using BusinessLayer;
using Encryption;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Web;
using System.Web.Helpers;
using System.Text.RegularExpressions;

public partial class User_U_Cart : System.Web.UI.Page
{
    #region Pagevariable
    private Logic Lo = new Logic();
    private DataTable DtGrid = new DataTable();
    private string _msg = string.Empty;
    private string _sysMsg = string.Empty;
    DataUtility Co = new DataUtility();
    private Cryptography Encrypt = new Cryptography();
    private PagedDataSource pgsource = new PagedDataSource();
    DataTable dtCart = new DataTable();
    DataRow dr;
    string mval = "";
    #endregion
    protected void UpdateDtGridValue(DataTable DtGrid)
    {
        for (int a = 0; a < DtGrid.Rows.Count; a++)
        {
            if (DtGrid.Rows[a]["UCompany"].ToString() != "")
            {
                DtGrid.Rows[a]["CompanyName"] = DtGrid.Rows[a]["UCompany"];
                DtGrid.Rows[a]["FactoryName"] = DtGrid.Rows[a]["UFactory"];
                DtGrid.Rows[a]["CompanyRefNo"] = DtGrid.Rows[a]["UCompRefNo"];
                DtGrid.Rows[a]["FactoryRefNo"] = DtGrid.Rows[a]["UFactoryRefNo"];
            }
            else if (DtGrid.Rows[a]["FCompany"].ToString() != "")
            {
                DtGrid.Rows[a]["CompanyName"] = DtGrid.Rows[a]["FCompany"];
                DtGrid.Rows[a]["CompanyRefNo"] = DtGrid.Rows[a]["FCompRefNo"];
            }
        }
    }
    protected void PageLoad()
    {
        if (Session["DCart"] != null)
        {
            dtCart = (DataTable)Session["DCart"];
            for (int i = 0; dtCart.Rows.Count > i; i++)
            {
                mval = mval + "," + "'" + dtCart.Rows[i]["ProductRefNo"].ToString() + "'";
            }
            string smval = mval.Substring(1);
            ViewState["RefN"] = smval;
            dtCart = Lo.RetriveCart(smval.ToString());
            if (dtCart.Rows.Count > 0)
            {
                UpdateDtGridValue(DtGrid);
                dtCart.Columns.Add("TopImages", typeof(string));
                for (int i = 0; dtCart.Rows.Count > i; i++)
                {
                    string mProdRefTime = dtCart.Rows[i]["ProductRefNo"].ToString();
                    DataTable dtImageBind4 = Lo.RetriveProductCode("", mProdRefTime, "RetImageTop", "");
                    if (dtImageBind4.Rows.Count > 0)
                    {
                        dtCart.Rows[i]["TopImages"] = dtImageBind4.Rows[0]["ImageName"].ToString();
                    }
                    else
                    {
                        dtCart.Rows[i]["TopImages"] = "/assets/images/Noimage.png";
                    }
                }
                dlCartProd.DataSource = dtCart;
                dlCartProd.DataBind();
                lbltotalprodincart.Text = "You have " + dtCart.Rows.Count + " products in your cart";
                totalno.InnerText = dtCart.Rows.Count.ToString();
            }
            else
            {

            }
        }
        else
        {
            Response.Redirect("UProductList");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PageLoad();
        }
    }
    protected void lbclaercart_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("Uproductlist");
    }
    protected void btnsendmail_Click(object sender, EventArgs e)
    {
        if (txtname.Text != "" && txtemail.Text != "" && txtcompname.Text != "" && txtofficeaddress.Text != "" && txtphone.Text != "")
        {
            if (VerifyEmailID(txtemail.Text) != false)
            {
                SendEmailCode(txtemail.Text, ViewState["RefN"].ToString());
                SendEmailCodeNodalOfficer(ViewState["RefN"].ToString());
                cleartext();
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Please enter your name or email')", true);
        }
    }
    protected void cleartext()
    {
        txtname.Text = "";
        txtemail.Text = "";
        txtcompname.Text = "";
        txtofficeaddress.Text = "";
        txtphone.Text = "";
    }
    public static bool VerifyEmailID(string email)
    {
        string expresion;
        expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        if (Regex.IsMatch(email, expresion))
        {
            if (Regex.Replace(email, expresion, string.Empty).Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    public void SendEmailCode(string empid, string id)
    {
        try
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/CartDetail.html")))
            {
                body = reader.ReadToEnd();
            }
            DataTable dtMail = Lo.RetriveMailCart(id);
            StringBuilder html = new StringBuilder();
            html.Append("<table border = '1'>");
            html.Append("<tr>");
            foreach (DataColumn column in dtMail.Columns)
            {
                html.Append("<th>");
                html.Append(column.ColumnName);
                html.Append("</th>");
            }
            html.Append("</tr>");
            {
                for (int m = 0; dtMail.Rows.Count > m; m++)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dtMail.Columns)
                    {
                        html.Append("<td>");
                        html.Append(dtMail.Rows[m][column.ColumnName]);
                        html.Append("</td>");
                    }
                    html.Append("</tr>");
                }
            }
            html.Append("</table>");
            body = body.Replace("{dt}", html.ToString());
            SendMail s;
            s = new SendMail();
            s.CreateMail("noreply-srijandefence@gov.in", empid, "Defence Imports Cart Product Info", body);
            s.sendMail();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Thankyou for your intrest in these product.We will revert you soon')", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('" + ex.Message + "')", true);
        }

    }
    public void SendEmailCodeNodalOfficer(string id)
    {
        try
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/NodalInfoDetailCart.html")))
            {
                body = reader.ReadToEnd();
            }
            DataTable dtMail = Lo.RetriveMailCart(id);
            StringBuilder html = new StringBuilder();
            html.Append("<table border = '1'>");
            html.Append("<tr>");
            foreach (DataColumn column in dtMail.Columns)
            {
                html.Append("<th>");
                html.Append(column.ColumnName);
                html.Append("</th>");
            }
            html.Append("</tr>");
            {
                for (int m = 0; dtMail.Rows.Count > m; m++)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dtMail.Columns)
                    {
                        html.Append("<td>");
                        html.Append(dtMail.Rows[m][column.ColumnName]);
                        html.Append("</td>");
                    }
                    html.Append("</tr>");
                }
            }
            html.Append("</table>");
            body = body.Replace("{dt}", html.ToString());
            body = body.Replace("{name}", txtname.Text.Trim());
            body = body.Replace("{compname}", txtcompname.Text.Trim());
            body = body.Replace("{address}", txtofficeaddress.Text.Trim());
            body = body.Replace("{email}", txtemail.Text.Trim());
            body = body.Replace("{phone}", txtphone.Text.Trim());
            SendMail s;
            s = new SendMail();
            for (int n = 0; dtMail.Rows.Count > n; n++)
            {
                if (dtMail.Rows[n]["NodalOfficerEmail"].ToString() != "")
                {
                    s.CreateMail("noreply-srijandefence@gov.in", dtMail.Rows[n]["NodalOfficerEmail"].ToString(), "Defence Imports Product Info", body);
                }
                else
                {
                    s.CreateMail("noreply-srijandefence@gov.in", "shrishkumar.ofb@ofb.gov.in", "Defence Imports Product Info", body);
                }
            }
            s.sendMail();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('" + ex.Message + "')", true);
        }
    }
    protected void dlCartProd_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "removecart")
        {
            DataTable dtremove = (DataTable)Session["DCart"];
            for (int i = 0; dtremove.Rows.Count > i; i++)
            {
                DataRow dr = dtremove.Rows[i];
                if (dr["ProductRefNo"].ToString() == e.CommandArgument.ToString())
                    dtremove.Rows.Remove(dr);
            }
            dtremove.AcceptChanges();
            if (dtremove.Rows.Count > 0)
            {
                Session["DCart"] = dtremove;
                PageLoad();
            }
            else
            {
                Session["DCart"] = null;
                Session.Abandon();
                Session.Clear();
                Response.Redirect("UProductList");
            }

        }
        else if (e.CommandName == "moredetail")
        {
            #region ViewOneProd
            try
            {
                DataListItem item = (DataListItem)(((Control)(e.CommandSource)).NamingContainer);
                string Role = ((HiddenField)item.FindControl("hfrole")).Value;
                DataTable DtView = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductMasterID", Role.ToString());
                if (DtView.Rows.Count > 0)
                {
                    lblrefnoview.Text = e.CommandArgument.ToString();
                    lblcompname.Text = DtView.Rows[0]["CompanyName"].ToString();
                    lbldiviname.Text = DtView.Rows[0]["FactoryName"].ToString();
                    lblunitnamepro.Text = DtView.Rows[0]["UnitName"].ToString();
                    lblnsngroup.Text = DtView.Rows[0]["ProdLevel1Name"].ToString();
                    lblnsngroupclass.Text = DtView.Rows[0]["ProdLevel2Name"].ToString();
                    lblclassitem.Text = DtView.Rows[0]["ProdLevel3Name"].ToString();
                    lblsearchkeywords.Text = DtView.Rows[0]["Searchkeyword"].ToString();
                    lblproductdescription.Text = DtView.Rows[0]["ProductDescription"].ToString();
                    lbldpsupartno.Text = DtView.Rows[0]["DPSUPartNumber"].ToString();
                    lblhsncode8digit.Text = DtView.Rows[0]["HsnCode8digit"].ToString();
                    prodIndustryDomain.Text = DtView.Rows[0]["TechLevel1Name"].ToString();
                    ProdIndusSubDomain.Text = DtView.Rows[0]["Techlevel2Name"].ToString();
                    lblisproductimported.Text = DtView.Rows[0]["IsProductImported"].ToString();
                    lblnsccode4digit.Text = DtView.Rows[0]["NSCCode"].ToString();
                    if (lblisproductimported.Text == "Y")
                    {
                        lblisproductimported.Text = "Yes";
                    }
                    else { lblisproductimported.Text = "No"; }
                    DataTable DtGridEstimate1 = new DataTable();
                    DtGridEstimate1 = Lo.RetriveSaveEstimateGrid("Select", 0, e.CommandArgument.ToString(), 0, "", "", "", "", "O");
                    if (DtGridEstimate1.Rows.Count > 0)
                    {
                        decimal tot = 0;
                        // int qtyimp = 0;
                        for (int i = 0; DtGridEstimate1.Rows.Count > i; i++)
                        {
                            // if (DtGridEstimate1.Rows[i]["FYear"].ToString() == "2019-20")
                            //  { 
                            tot = tot + Convert.ToDecimal(DtGridEstimate1.Rows[i]["EstimatedPrice"]);
                            // qtyimp = qtyimp + Convert.ToInt16(DtGridEstimate1.Rows[i]["EstimatedQty"]);
                            //  }
                        }
                        gvestimatequanold.DataSource = DtGridEstimate1;
                        gvestimatequanold.DataBind();
                        gvestimatequanold.Visible = true;
                        decimal msumobject = tot; //* qtyimp / 100000;
                        lblvalueimport.Text = msumobject.ToString("F2");
                    }
                    else
                    {
                        gvestimatequanold.Visible = false;
                        lblvalueimport.Text = "0.00";
                    }
                    DataTable dtPdfBind = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductImage", "PDF");
                    if (dtPdfBind.Rows.Count > 0)
                    {
                        gvpdf.DataSource = dtPdfBind;
                        gvpdf.DataBind();
                        gvpdf.Visible = true;
                    }
                    else
                    {
                        gvpdf.Visible = false;
                    }
                    DataTable dtImageBindfinal = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductImage", "Image");
                    if (dtImageBindfinal.Rows.Count > 0)
                    {
                        dlimage.DataSource = dtImageBindfinal;
                        dlimage.DataBind();
                        dlimage.Visible = true;
                    }
                    else
                    {
                        dlimage.Visible = false;
                    }
                    lblfeaturesanddetail.Text = DtView.Rows[0]["FeatursandDetail"].ToString();
                    lblitemspecification.Text = DtView.Rows[0]["ItemSpecification"].ToString();
                    DataTable dtProdInfo = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "RetriveProdInfo", "");
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
                    lbladditionalinfo.Text = DtView.Rows[0]["AdditionalDetail"].ToString();
                    DataTable dtestimatequanorprice = Lo.RetriveSaveEstimateGrid("2Select", 0, e.CommandArgument.ToString(), 0, "", "", "", "", "F");
                    if (dtestimatequanorprice.Rows.Count > 0)
                    {
                        gvestimatequanorprice.DataSource = dtestimatequanorprice;
                        gvestimatequanorprice.DataBind();
                        gvestimatequanorprice.Visible = true;
                    }
                    else
                    {
                        gvestimatequanorprice.Visible = false;
                    }
                    lblindicate.Text = "";
                    if (DtView.Rows[0]["PurposeofProcurement"].ToString() != "")
                    {
                        DataTable DTporCat = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductPOP", "Company");
                        if (DTporCat.Rows.Count > 0)
                        {
                            for (int i = 0; DTporCat.Rows.Count > i; i++)
                            {
                                lblindicate.Text = lblindicate.Text + DTporCat.Rows[i]["SCategoryName"].ToString();
                            }
                        }
                    }
                    lblprocremarks.Text = DtView.Rows[0]["ProcurmentCategoryRemark"].ToString();
                    lbleoirep.Text = DtView.Rows[0]["EOIStatus"].ToString();
                    lbleoilink.Text = DtView.Rows[0]["EOIURL"].ToString();
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
}