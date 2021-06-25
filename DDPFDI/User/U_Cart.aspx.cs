using BusinessLayer;
using Encryption;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using context = System.Web.HttpContext;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;

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
    HybridDictionary hySave = new HybridDictionary();
    #endregion
    string temp = "";
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
            dtCart = Lo.RetriveCartNew(smval.ToString());
            if (dtCart.Rows.Count > 0)
            {
                dlCartProd.DataSource = dtCart;
                dlCartProd.DataBind();
                lbltotalprodincart.Text = "You have " + dtCart.Rows.Count + " products in your cart";
            }
        }
        else
        {
            Response.Redirect("ProductList");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                PageLoad();
            }
            catch (Exception ex)
            { ExceptionLogging.SendErrorToText(ex); }
        }
    }
    protected void lbclaercart_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("Productlist");
    }
    string msgotp;
    string mlblmProdRef;
    protected void btnsendmail_Click(object sender, EventArgs e)
    {
        try
        {
            if (Co.RSQandSQLInjection(txtname.Text, "soft") != "" && Co.RSQandSQLInjection(txtemail.Text, "soft") != "" && Co.RSQandSQLInjection(txtcompname.Text, "soft") != ""
                && Co.RSQandSQLInjection(txtofficeaddress.Text, "soft") != "" && Co.RSQandSQLInjection(txtphone.Text, "soft") != "" && Co.RSQandSQLInjection(txtphone.Text, "soft") != "")
            {
                if (VerifyEmailID(txtemail.Text) != false)
                {
                    foreach (DataListItem item in dlCartProd.Items)
                    {
                        string prodgetcart = ((LinkButton)(item.FindControl("ddlremovecart"))).CommandArgument.ToString();
                        mlblmProdRef = mlblmProdRef + ",'" + prodgetcart + "'";
                    }
                    DataTable dtChckProd = Lo.NewRetriveFilterCode12("RetReqProd", mlblmProdRef.Substring(1), txtemail.Text.Trim(), txtphone.Text.Trim(), "", 0, 0, 0);
                    if (dtChckProd.Rows.Count > 0)
                    {
                        for (int k = 0; dtChckProd.Rows.Count > k; k++)
                        {
                            if (dtChckProd.Rows[k]["RequestMobileNo"].ToString() == txtphone.Text.Trim()
                                && dtChckProd.Rows[k]["RequestEmail"].ToString() == txtemail.Text.Trim())
                            {
                                foreach (DataListItem item in dlCartProd.Items)
                                {
                                    string prodgetcart = ((LinkButton)(item.FindControl("ddlremovecart"))).CommandArgument.ToString();
                                    if (dtChckProd.Rows[k]["ProductRefNo"].ToString() == prodgetcart.ToString())
                                    {
                                        msgotp = "fail";
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert",
                             "alert('You are already shown intrest in item = " + dtChckProd.Rows[k]["ProductRefNo"].ToString() + ".Please remove first from the cart and then click on get otp')", true);
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        if (msgotp != "fail")
                        {
                            GenerateOTP();
                            sendMailOTP();
                            ScriptManager.RegisterStartupScript(this, GetType(), "modelotp", "showPopup1();", true);
                        }
                    }
                    else
                    {
                        GenerateOTP();
                        sendMailOTP();
                        ScriptManager.RegisterStartupScript(this, GetType(), "modelotp", "showPopup1();", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('All field fill mandatory.')", true);
            }
        }
        catch (Exception ex)
        { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message.ToString() + "')", true); }
    }
    protected void lbsubmit_Click(object sender, EventArgs e)
    {
        if (txtotp.Text != "" && txtotp.Text == hfotp.Value)
        {
            try
            {
                saveInfo();
                cleartext();
                Session.Clear();
                Session.Abandon();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Your interest sent to item releted dpsu's We will contact you soon.'); window.location.href='Productlist';", true);
            }
            catch (Exception ex)
            { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Error occured in send mail please contact admin person.')", true); }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Invalid otp.'); window.location.href='U_Cart';", true);
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
            s.CreateMail("noreply-srijandefence@gov.in", empid, "Make in India opportunities Information", body);
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
                    s.CreateMail("noreply-srijandefence@gov.in", dtMail.Rows[n]["NodalOfficerEmail"].ToString(), "Make in India opportunities Information", body);
                }
                else
                {
                    DataTable DtGetCompIdbyNodal = Lo.RetriveGridViewCompany(dtMail.Rows[n]["ProductRefNo"].ToString(), "", "", "GetIdComporNodal");
                    if (DtGetCompIdbyNodal.Rows.Count > 0)
                    {
                        s.CreateMail("noreply-srijandefence@gov.in", DtGetCompIdbyNodal.Rows[0]["NodalOfficerEmail"].ToString(), "Make in India opportunities Information", body);
                    }
                    else
                    {
                        s.CreateMail("noreply-srijandefence@gov.in", "shrishkumar.ofb@ofb.gov.in", "Make in India opportunities Information", body);
                    }
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
                Response.Redirect("ProductList");
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
                    DtGridEstimate1 = Lo.RetriveSaveEstimateGrid("Select", 0, e.CommandArgument.ToString(), 0, "", "", "", "", "O");
                    if (DtGridEstimate1.Rows.Count > 0)
                    {
                        gvestimatequanold.DataSource = DtGridEstimate1;
                        gvestimatequanold.DataBind();
                        gvestimatequanold.Visible = true;
                        decimal tot = 0;
                        for (int i = 0; DtGridEstimate1.Rows.Count > i; i++)
                        {
                            tot = tot + Convert.ToDecimal(DtGridEstimate1.Rows[i]["EstimatedPrice"]);
                        }
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
                    DataTable dtPdfBind = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductImage", "PDF");
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
                    DataTable dtImageBindfinal = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductImage", "Image");
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
                    DataTable dtestimatequanorprice = Lo.RetriveSaveEstimateGrid("2Select", 0, e.CommandArgument.ToString(), 0, "", "", "", "", "F");
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
                        DataTable DTporCat = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductPOP", "Company");
                        if (DTporCat.Rows.Count > 0)
                        {
                            lblindicate.Text = "";
                            lblindicate.Text = DTporCat.Rows[0]["SCategoryName"].ToString();
                            //for (int i = 0; DTporCat.Rows.Count > i; i++)
                            //{
                            //    lblindicate.Text = lblindicate.Text + DTporCat.Rows[i]["SCategoryName"].ToString() + ", ";
                            //}
                            //lblindicate.Text = lblindicate.Text.Substring(0, lblindicate.Text.Length - 2);
                            sixteen.Visible = true;
                        }
                        else
                        { sixteen.Visible = false; }
                    }
                    else
                    {
                        sixteen.Visible = false;
                    }
                    if (DtView.Rows[0]["IsIndeginized"].ToString() != "")
                    {
                        Tr1.Visible = true;
                        lblindstart.Text = DtView.Rows[0]["IsIndeginized"].ToString();
                        if (lblindstart.Text == "N")
                            lblindstart.Text = "No";
                        else
                            lblindstart.Text = "Yes";
                    }
                    else
                    { Tr1.Visible = false; }


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
                    string Nodel1Id = DtView.Rows[0]["NodelDetail"].ToString();
                    if (Nodel1Id.ToString() != "")
                    {
                        DataTable dtNodal = Lo.RetriveProductCode(Nodel1Id.ToString(), "", "ProdNodal", "");
                        if (dtNodal.Rows.Count > 0)
                        {
                            lblempname.Text = dtNodal.Rows[0]["NodalOficerName"].ToString();
                            lbldesignation.Text = dtNodal.Rows[0]["Designation"].ToString();
                            lblemailidpro.Text = dtNodal.Rows[0]["NodalOfficerEmail"].ToString();
                            lblphonenumber.Text = dtNodal.Rows[0]["NodalOfficerTelephone"].ToString();
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
                        DataTable DTporCat = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "EndUser", "Company");
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
                    if (DtView.Rows[0]["QAAgency"].ToString() != "")
                    {
                        DataTable DTporCat = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductQAAgency", "Company");
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
                    if (DtView.Rows[0]["IndTargetYear"].ToString() != "")
                    {
                        lblindtrgyr.Text = DtView.Rows[0]["IndTargetYear"].ToString();
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
    #region OTPCode
    protected void GenerateOTP()
    {
        try
        {
            string numbers = "1234567890";
            string characters = numbers;
            int length = int.Parse("6");
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            hfotp.Value = otp;
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    private void sendMailOTP()
    {
        try
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/OTP.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{OTP}", hfotp.Value);
            SendMail s;
            s = new SendMail();
            s.CreateMail("noreply-srijandefence@gov.in", txtemail.Text, "OTP Verification Cart.", body);
            s.sendMail();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void lbresendotp_Click(object sender, EventArgs e)
    {
        GenerateOTP();
        sendMailOTP();
    }

    #endregion
    protected void lblhome_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProductList");
    }
    string lblmProdRef;
    string lblmComp;
    string lblmRemarks;
    string lblprodname;
    string hfNamenodel;
    string hfNodalPhone;
    protected void saveInfo()
    {
        try
        {
            DataTable dtreqprod = new DataTable();
            dtreqprod.Columns.Add(new DataColumn("ProductRefNo", typeof(string)));
            dtreqprod.Columns.Add(new DataColumn("CompanyName", typeof(string)));
            dtreqprod.Columns.Add(new DataColumn("Remark", typeof(string)));
            dtreqprod.Columns.Add(new DataColumn("ProdName", typeof(string)));
            dtreqprod.Columns.Add(new DataColumn("NodalName", typeof(string)));
            dtreqprod.Columns.Add(new DataColumn("NodalPhone", typeof(string)));
            DataRow drreqprod;
            foreach (DataListItem item in dlCartProd.Items)
            {
                lblmProdRef = ((LinkButton)(item.FindControl("ddlremovecart"))).CommandArgument.ToString();
                lblmComp = ((Label)(item.FindControl("mComp"))).Text;
                lblmRemarks = ((TextBox)(item.FindControl("txtremark"))).Text;
                lblprodname = ((Label)(item.FindControl("lblprodname"))).Text;
                hfNamenodel = ((HiddenField)(item.FindControl("hfnodelname"))).Value;
                hfNodalPhone = ((HiddenField)(item.FindControl("hfnodalphone"))).Value;
                drreqprod = dtreqprod.NewRow();
                drreqprod["ProductRefNo"] = lblmProdRef.ToString();
                drreqprod["CompanyName"] = lblmComp.ToString();
                drreqprod["Remark"] = lblmRemarks.ToString();
                drreqprod["ProdName"] = lblprodname.ToString();
                drreqprod["NodalName"] = hfNamenodel.ToString();
                drreqprod["NodalPhone"] = hfNodalPhone.ToString();
                dtreqprod.Rows.Add(drreqprod);
            }
            DataTable dtgetprod = Lo.RetriveGridViewCompany(DateTime.Now.ToString("yyyy-MM-dd"), txtemail.Text.Trim(), txtphone.Text, "ret");
            if (dtgetprod.Rows.Count > 0)
            {

                hySave["RequestID"] = dtgetprod.Rows[0]["RequestID"].ToString();
            }
            else
            {
                hySave["RequestID"] = 0;
            }
            hySave["RequestBy"] = Co.RSQandSQLInjection(txtname.Text.Trim(), "soft");
            hySave["RequestCompName"] = Co.RSQandSQLInjection(txtcompname.Text.Trim(), "soft");
            hySave["RequestMobileNo"] = Co.RSQandSQLInjection(txtphone.Text.Trim(), "soft");
            hySave["RequestAddress"] = Co.RSQandSQLInjection(txtofficeaddress.Text.Trim(), "soft");
            hySave["RequestEmail"] = Co.RSQandSQLInjection(txtemail.Text.Trim(), "soft");
            hySave["RequestProduct"] = null;
            hySave["RequestMCompName"] = null;
            hySave["IsMailSend"] = Co.RSQandSQLInjection("Y", "soft");
            hySave["RequestDate"] = Co.RSQandSQLInjection(DateTime.Now.ToString("dd/MMM/yyyy"), "soft");
            string str = Lo.SaveRequestInfo(hySave, dtreqprod, out _sysMsg, out _msg);
            try
            {
                SendEmailCode(txtemail.Text, ViewState["RefN"].ToString());
                SendEmailCodeNodalOfficer(ViewState["RefN"].ToString());
                if (dtreqprod.Rows.Count > 0)
                {
                    for (int i = 0; dtreqprod.Rows.Count > i; i++)
                    {
                        Co.sendSMSMsg(dtreqprod.Rows[i]["NodalPhone"].ToString(), "", "Dear " + dtreqprod.Rows[i]["NodalName"].ToString() + ", Vendor " + txtname.Text + " showing interest in your item " + dtreqprod.Rows[i]["ProdName"].ToString() + " ,please do needfull process regards this item.\n Thanks and Regards\nTeam Srijandefence");
                    }
                }
                Co.sendSMSMsg(txtphone.Text, "", "Dear " + txtname.Text + " thanks for showing interest in item , we will contact you soon.\n Thanks and Regards\nTeam Srijandefence");
            }
            catch (Exception ex)

            { ExceptionLogging.SendErrorToText(ex); }
        }
        catch (Exception ex)
        { ExceptionLogging.SendErrorToText(ex); }
    }
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
    #region AutoComplete Serach Box
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetSearchKeyword(string prefix)
    {
        Cryptography objCrypto1 = new Cryptography();
        List<string> customers = new List<string>();
        List<string> Finalcustomers = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = objCrypto1.DecryptData(ConfigurationManager.ConnectionStrings["connectiondb"].ConnectionString);
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct RequestCompName from tbl_mst_RequestInfo  where RequestCompName like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefix);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["RequestCompName"]));
                    }
                }

                conn.Close();
            }
        }
        return customers.Distinct().ToArray();
    }
    #endregion
    protected void txtemail_TextChanged(object sender, EventArgs e)
    {
        DataTable dtCheckEmasil = Lo.NewRetriveFilterCode("CheckEmailUCart", txtemail.Text.Trim(), "", "", "", 0, 0, 0);
        if (dtCheckEmasil.Rows.Count > 0)
        { btnsendmail.Enabled = false; ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Email id already registerd.')", true); }
        else
        { btnsendmail.Enabled = true; }

    }
    protected void txtphone_TextChanged(object sender, EventArgs e)
    {
        DataTable dtcheckMobile = Lo.NewRetriveFilterCode("CheckPhoneUCart", txtphone.Text.Trim(), "", "", "", 0, 0, 0);
        if (dtcheckMobile.Rows.Count > 0)
        {
            btnsendmail.Enabled = false; ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Phone no already registerd.')", true);
        }
        else
        { btnsendmail.Enabled = true; }

    }
}