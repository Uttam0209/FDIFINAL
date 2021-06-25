using BusinessLayer;
using Encryption;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using context = System.Web.HttpContext;
using System.Web.UI.HtmlControls;

public partial class User_SimilerProduct : System.Web.UI.Page
{
    #region Pagevariable
    private Logic Lo = new Logic();
    HybridDictionary hysaveip = new HybridDictionary();
    private DataTable DtGrid = new DataTable();
    private string _msg = string.Empty;
    private string _sysMsg = string.Empty;
    DataUtility Co = new DataUtility();
    private Cryptography Encrypt = new Cryptography();
    private PagedDataSource pgsource = new PagedDataSource();
    DataTable DtCompany = new DataTable();
    DataTable DtFilterView = new DataTable();
    DataTable dtCart = new DataTable();
    DataRow dr;
    #endregion
    #region Pageload
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Session["DCart"] != null)
                {
                    ViewState["buyitems"] = Session["DCart"];
                    dtCart = (DataTable)ViewState["buyitems"];
                    totalno.InnerText = dtCart.Rows.Count.ToString();
                }
                else
                {
                    totalno.InnerText = dtCart.Rows.Count.ToString();
                }
                MenuLink();
                ControlGrid();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Technical Error:- " + ex.Message + "');", true);
            }
        }
    }
    protected void MenuLink()
    {
        if (Session["User"] != null)
        {
            linkusername.Text = Encrypt.DecryptData(Session["User"].ToString());
            linkusername.Visible = true;
            linkusername.Text = "Welcome: " + linkusername.Text;
            lnkfeedback.Visible = true;
            linklogin.Visible = false;
            lblmis.Visible = true;
            lbllogout.Visible = true;
            lbSuccesstory.Visible = true;
            reportdiv.Visible = true;
            mhwparti.Visible = false;
        }
        else
        {
            lbSuccesstory.Visible = false;
            linklogin.Visible = true;
            linkusername.Visible = false;
            lblmis.Visible = false;
            lbllogout.Visible = false;
            PR.Visible = false; mhwparti.Visible = true;
            reportdiv.Visible = false;
        }
    }
    private void StoreUserIP(string Prodref)
    {
        string ipAddress;

        DateTime Date = Convert.ToDateTime(DateTime.Now.ToString());
        string mDate = Date.ToString("dd-MM-yyyy");
        hysaveip["VisitedDate"] = mDate.ToString();
        DateTime Time = Convert.ToDateTime(DateTime.Now.ToString());
        string mTime = Time.ToString("hh:mm:ss");
        hysaveip["VisitedTime"] = mDate.ToString();
        ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (ipAddress == "" || ipAddress == null)
            ipAddress = Request.ServerVariables["REMOTE_ADDR"];
        hysaveip["IPAddress"] = ipAddress.ToString();
        hysaveip["ProductRefNo"] = Prodref.ToString();
        string strsaveip = Lo.SaveUserIP(hysaveip, out _sysMsg, out _msg);

    }
    private void ControlGrid()
    {
        try
        {
            BindProduct();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
    }
    protected void BindProduct()
    {
        if (Request.QueryString["sProd"] != null)
        {
            string mMinute = DateTime.Now.ToString("mm");
            DtGrid = Lo.RetriveFilterCode(Encrypt.DecryptData(Request.QueryString["msort"].ToString()), Encrypt.DecryptData(Request.QueryString["sProd"].ToString()), "BindSimiMainProd");
            if (DtGrid.Rows.Count > 0)
            {
                Session["TempData"] = DtGrid;
                SeachResult();
            }
            else
            {
                divcontentproduct.Visible = false;
                dlproduct.DataBind();
            }
        }
    }
    #endregion
    #region Search Code Filter Code
    string insert1 = "";
    string chkproofcat = "";
    protected string Dvinsert(string sortExpression = null)
    {
        DataTable insert = new DataTable();
        insert.Columns.Add(new DataColumn("Column", typeof(string)));
        insert.Columns.Add(new DataColumn("Value", typeof(string)));
        DataRow dr;
        if (txtsearch.Text.Trim() != "")
        {
            dr = insert.NewRow();
            dr["Column"] = "((ProductRefNo like";
            dr["Value"] = "'" + txtsearch.Text.Trim() + "%') or (CompanyName like '" + txtsearch.Text.Trim() + "%') or (UnitName like '" + txtsearch.Text.Trim() + "%') or (FactoryName like '" + txtsearch.Text.Trim() + "%') or (NSCCode like '" + txtsearch.Text.Trim() + "%') or (ProductDescription like '" + txtsearch.Text.Trim() + "%') or (NSNGroup like '" + txtsearch.Text.Trim() + "%') or (DefencePlatform  like '" + txtsearch.Text.Trim() + "%') or (ProdIndustryDoamin like '" + txtsearch.Text.Trim() + "%')  or (NSNGroupClass like '" + txtsearch.Text.Trim() + "%')  or  (ItemCode  like '" + txtsearch.Text.Trim() + "%')  or (ProdIndustrySubDomain like '" + txtsearch.Text.Trim() + "%') or (TopPdf like '" + txtsearch.Text.Trim() + "%')  or (TopImages like '" + txtsearch.Text.Trim() + "%')  or (HSNCode like '" + txtsearch.Text.Trim() + "%') or (Convert(EstimateQu, 'System.String') LIKE '" + txtsearch.Text + "%') or (Convert(EstimatePrice, 'System.String') LIKE '" + txtsearch.Text + "%') or (DPSUPartNumber like '" + txtsearch.Text.Trim() + "%') or (OEMName like '" + txtsearch.Text.Trim() + "%') or (OEMCountry like '" + txtsearch.Text.Trim() + "%'))";
            insert.Rows.Add(dr);
        }
        for (int i = 0; insert.Rows.Count > i; i++)
        {
            insert1 = insert1 + insert.Rows[i]["Column"].ToString() + " " + insert.Rows[i]["Value"].ToString() + " " + " and ";
        }
        if (insert1.ToString() != "")
        {
            insert1 = insert1.Substring(0, insert1.Length - 5);
        }
        return insert1;
    }
    protected string BindInsertfilter()
    {
        return Dvinsert();
    }
    public void SeachResult(string sortExpression = null)
    {
        try
        {
            DtFilterView = (DataTable)Session["TempData"];
            if (DtFilterView.Rows.Count > 0)
            {
                DataView dv = new DataView(DtFilterView);
                DataTable dtnew = dv.ToTable();
                if (dtnew.Rows.Count > 0)
                {
                    dv.RowFilter = BindInsertfilter();
                    DataTable dtinner = dv.ToTable();
                    lbltotal.Text = "Filter/Search Results " + dtinner.Rows.Count.ToString() + " items";
                    lbltotfilter.Text = dtinner.Rows.Count.ToString();
                    DataTable dtads = dv.ToTable();
                    if (dtads.Rows.Count > 0)
                    {
                        pgsource.DataSource = dtinner.DefaultView;
                        pgsource.AllowPaging = true;
                        pgsource.PageSize = 24;
                        pgsource.CurrentPageIndex = pagingCurrentPage;
                        lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                        lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                        LinkButton1.Enabled = !pgsource.IsFirstPage;
                        lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                        LinkButton2.Enabled = !pgsource.IsLastPage;
                        pgsource.DataSource = dtads.DefaultView;
                        dtads.DefaultView.Sort = "EstiPriMultiF,SortUsing desc";
                        dlproduct.DataSource = pgsource;
                        dlproduct.DataBind();
                        lbltotalleft.Text = "Total Imported Items :-  " + DtFilterView.Rows.Count.ToString();
                        lbltotalshowpageitem.Text = pgsource.FirstIndexInPage + 1 + " - " + (pgsource.FirstIndexInPage + pgsource.Count);
                        divcontentproduct.Visible = true;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
                        divcontentproduct.Visible = false;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    #endregion
    #region pageindex code
    protected void lnkbtnPgPrevious_Click(object sender, EventArgs e)
    {
        pagingCurrentPage -= 1;
        SeachResult();
    }
    protected void lnkbtnPgNext_Click(object sender, EventArgs e)
    {
        pagingCurrentPage += 1;
        SeachResult();
    }
    private int pagingCurrentPage
    {
        get
        {
            if (ViewState["pagingCurrentPage"] == null)
            {
                return 0;
            }
            else
            {
                return ((int)ViewState["pagingCurrentPage"]);
            }
        }
        set
        {
            ViewState["pagingCurrentPage"] = value;
        }
    }
    #endregion
    #region CartCode
    protected void dlproduct_ItemCommand(object source, DataListCommandEventArgs e)
    {
        #region ViewOneProd
        if (e.CommandName == "View")
        {
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
                        // .Substring(0, DtView.Rows[0]["IndTargetYear"].ToString().Length - 1
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
                    ScriptManager.RegisterStartupScript(this, GetType(), "aboutus", "showPopup();", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
            }
        }
        #endregion
        #region AddCart
        else if (e.CommandName == "addcart")
        {
            this.StoreUserIP(e.CommandArgument.ToString());
            LinkButton lnkId = (LinkButton)e.Item.FindControl("lbaddcart");
            dtCart.Columns.Add(new DataColumn("ProductRefNo", typeof(string)));
            if (lnkId.Text != "View Only")
            {
                if (ViewState["buyitems"] != null)
                {

                    dtCart = (DataTable)ViewState["buyitems"];
                    if (dtCart.Rows.Count > 0)
                    {
                        string InCart = "";
                        for (int i = 0; dtCart.Rows.Count > i; i++)
                        {
                            if (e.CommandArgument.ToString() == dtCart.Rows[i]["ProductRefNo"].ToString())
                            {
                                InCart = "AlreadyInCart";
                                lnkId.Text = "Successfully Added";
                                lnkId.Attributes.Remove("Class");
                                lnkId.Attributes.Add("Class", "btn btn-success btn-sm btn-block");
                                break;
                            }
                        }
                        if (InCart != "AlreadyInCart")
                        {
                            dr = dtCart.NewRow();
                            dr["ProductRefNo"] = e.CommandArgument.ToString();
                            dtCart.Rows.Add(dr);
                            ViewState["buyitems"] = dtCart;
                            lnkId.Text = "Successfully Added";
                            lnkId.Attributes.Remove("Class");
                            lnkId.Attributes.Add("Class", "btn btn-success btn-sm btn-block");

                        }
                    }
                }
                else
                {
                    dr = dtCart.NewRow();
                    dr["ProductRefNo"] = e.CommandArgument.ToString();
                    dtCart.Rows.Add(dr);
                    ViewState["buyitems"] = dtCart;
                    lnkId.Text = "Successfully Added";
                    lnkId.Attributes.Remove("Class");
                    lnkId.Attributes.Add("Class", "btn btn-success btn-sm btn-block");
                }
                if (dtCart != null)
                {
                    totalno.InnerText = dtCart.Rows.Count.ToString();
                }
                else
                {
                    totalno.InnerText = "0";
                }
            }
        }
        #endregion
    }
    protected void lbtotalcart_Click(object sender, EventArgs e)
    {
        if (ViewState["buyitems"] != null)
        {
            Session["DCart"] = ViewState["buyitems"];
            Response.Redirect("U_Cart");
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No product in cart')", true);
        }
    }
    protected void lbcart_Click(object sender, EventArgs e)
    {
        if (ViewState["buyitems"] != null)
        {
            Session["DCart"] = ViewState["buyitems"];
            Response.Redirect("U_Cart");
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No product in cart')", true);
        }
    }
    #endregion
    #region oterscode
    protected void dlproduct_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton lbcart = e.Item.FindControl("lbaddcart") as LinkButton;
            HtmlAnchor lbindevisi = (HtmlAnchor)e.Item.FindControl("indevisi");
            Label ISINDI = (Label)e.Item.FindControl("ISINDI");
            Label LBPURPOSEOFPROC = (Label)e.Item.FindControl("LBPURPOSEOFPROC");
            if (dtCart.Rows.Count > 0)
            {
                HiddenField hf = (HiddenField)e.Item.FindControl("hfr");
                for (int i = 0; dtCart.Rows.Count > i; i++)
                {
                    if (dtCart.Rows[i]["ProductRefNo"].ToString() == hf.Value)
                        lbcart.Text = "Successfully Added";
                    lbcart.Attributes.Remove("Class");
                    lbcart.Attributes.Add("Class", "btn btn-success btn-sm btn-block");
                }
            }
            Label lblepold = (Label)e.Item.FindControl("lblepold");
            Label lblepold17 = (Label)e.Item.FindControl("lblepold17");
            Label lblepold18 = (Label)e.Item.FindControl("lblepold18");
            Label lblepfu = (Label)e.Item.FindControl("lblepfu");


            if (Encrypt.DecryptData(Request.QueryString["msort"].ToString()) == "2019-20")
            {
                lblepold.Visible = true;
                lblepfu.Visible = false;
                lblepold17.Visible = false;
                lblepold18.Visible = false;
            }
            else if (Encrypt.DecryptData(Request.QueryString["msort"].ToString()) == "2018-19")
            {
                lblepold.Visible = false;
                lblepfu.Visible = false;
                lblepold17.Visible = false;
                lblepold18.Visible = true;

            }
            else if (Encrypt.DecryptData(Request.QueryString["msort"].ToString()) == "2017-18")
            {
                lblepold.Visible = false;
                lblepfu.Visible = false;
                lblepold17.Visible = true;
                lblepold18.Visible = false;

            }
            else if (Encrypt.DecryptData(Request.QueryString["msort"].ToString()) == "2020-21")
            {
                lblepold.Visible = false;
                lblepfu.Visible = true;
                lblepold17.Visible = false;
                lblepold18.Visible = false;
            }
            if (LBPURPOSEOFPROC.Text == "58264" || LBPURPOSEOFPROC.Text == "58270")
            {
                lbcart.Enabled = false;
                lbcart.Text = "View Only";
            }
            else
            {
                lbcart.Enabled = true;
            }
            if (ISINDI.Text == "Y")
            {
                lbindevisi.Visible = true;
                lbcart.Enabled = false;
                lbcart.Text = "View Only";
            }
            else
            {
                lbindevisi.Visible = false;
                lbcart.Enabled = true;
            }
            lbcart.CssClass = "btn btn-sm btn-block text-white";
        }
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("ProductList");
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        SeachResult();
    }
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
                cmd.CommandText = "select distinct ProductRefNo from tbl_trn_ProductFilterSearchTemp  where ProductRefNo like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefix);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["ProductRefNo"]));
                    }
                }
                cmd.CommandText = "select distinct CompanyName from tbl_trn_ProductFilterSearchTemp where CompanyName like @SearchText + '%'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["CompanyName"]));
                    }
                }
                cmd.CommandText = "select distinct FactoryName from tbl_trn_ProductFilterSearchTemp where FactoryName like @SearchText + '%'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["FactoryName"]));
                    }
                }

                cmd.CommandText = "select distinct UnitName from tbl_trn_ProductFilterSearchTemp where UnitName like @SearchText + '%'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["UnitName"]));
                    }
                }
                cmd.CommandText = "select distinct NSCCode from tbl_mst_MainProduct where NSCCode like @SearchText + '%'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["NSCCode"]));
                    }
                }
                cmd.CommandText = "select distinct ProductDescription from tbl_Mst_MainProduct where ProductDescription like @SearchText + '%' and (ProductDescription is not null or ProductDescription !='')";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["ProductDescription"]));
                    }
                }
                cmd.CommandText = "select distinct NSNGroup from tbl_trn_ProductFilterSearchTemp where NSNGroup like @SearchText + '%' and (NSNGroup is not null or NSNGroup !='')";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["NSNGroup"]));
                    }
                }
                cmd.CommandText = "select distinct DefencePlatform from tbl_trn_ProductFilterSearchTemp where DefencePlatform like @SearchText + '%' and (DefencePlatform is not null or DefencePlatform !='')";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["DefencePlatform"]));
                    }
                }
                cmd.CommandText = "select distinct ProdIndustryDoamin from tbl_trn_ProductFilterSearchTemp where ProdIndustryDoamin like @SearchText + '%' and (ProdIndustryDoamin is not null or ProdIndustryDoamin !='')";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["ProdIndustryDoamin"]));
                    }
                }
                cmd.CommandText = "select distinct NSNGroupClass from tbl_trn_ProductFilterSearchTemp where NSNGroupClass like @SearchText + '%' and (NSNGroupClass is not null or NSNGroupClass !='')";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["NSNGroupClass"]));
                    }
                }
                cmd.CommandText = "select distinct ItemCode from tbl_trn_ProductFilterSearchTemp where ItemCode like @SearchText + '%' and (ItemCode is not null or ItemCode !='')";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["ItemCode"]));
                    }
                }
                cmd.CommandText = "select distinct ProdIndustrySubDomain from tbl_trn_ProductFilterSearchTemp where ProdIndustrySubDomain like @SearchText + '%' and (ProdIndustrySubDomain is not null or ProdIndustrySubDomain !='')";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["ProdIndustrySubDomain"]));
                    }
                }
                cmd.CommandText = "select distinct TopImages from tbl_trn_ProductFilterSearchTemp where TopImages like @SearchText + '%' and (TopImages is not null or TopImages !='')";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["TopImages"]));
                    }
                }
                cmd.CommandText = "select  distinct TopPdf from tbl_trn_ProductFilterSearchTemp where TopPdf like @SearchText + '%' and (TopPdf is not null or TopPdf !='') ";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["TopPdf"]));
                    }
                }
                cmd.CommandText = "select distinct HSNCode from tbl_trn_ProductFilterSearchTemp where HSNCode like @SearchText + '%' and (HSNCode is not null or HSNCode !='')";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["HSNCode"]));
                    }
                }
                cmd.CommandText = "select distinct DPSUPartNumber from tbl_trn_ProductFilterSearchTemp where DPSUPartNumber like @SearchText + '%' and (DPSUPartNumber is not null or DPSUPartNumber !='')";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["DPSUPartNumber"]));
                    }
                }
                cmd.CommandText = "select distinct OEMName from tbl_trn_ProductFilterSearchTemp where OEMName like @SearchText + '%' and (OEMName is not null or OEMName !='')";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["OEMName"]));
                    }
                }
                cmd.CommandText = "select distinct OEMCountry from tbl_trn_ProductFilterSearchTemp where OEMCountry like @SearchText + '%' and (OEMCountry is not null or OEMCountry !='')";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["OEMCountry"]));
                    }
                }
                cmd.CommandText = "select distinct EstimatedQty from tbl_trn_ProdQtyPrice where EstimatedQty like @SearchText + '%'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["EstimatedQty"]));
                    }
                }
                cmd.CommandText = "select distinct EstimatedPrice from tbl_trn_ProdQtyPrice where EstimatedPrice like @SearchText + '%'";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["EstimatedPrice"]));
                    }
                }
                conn.Close();
            }
        }

        return customers.Distinct().ToArray();
    }
    #endregion
    #region AutoComplete Serach Box
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string GetSearchKeywordemo(string prefix)
    {
        User_SimilerProduct u = new User_SimilerProduct();
        u.SeachResult(prefix);
        return "search";
    }
    #endregion
    protected void cleartext()
    {
        txtsearch.Text = "";
    }
    #endregion
    #region SHALINI Code
    protected void linklogin_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("Login");
    }
    protected void linkusername_Click(object sender, EventArgs e)
    {
        if (Session["User"] != null)
        {
            Response.RedirectToRoute("Dashboard");
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "ErrorMssgPopup('Please login again');window.location='Login'", true);
        }
    }
    #endregion
    #region LogoutCode
    protected void lbllogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Remove("Type");
        Session.Remove("User");
        Session.Remove("CompanyRefNo");
        Session.Remove("SFToken");
        Session.RemoveAll();
        Session.Contents.RemoveAll();
        Session.Clear();
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cookies["DefaultDpsu"].Expires = DateTime.Now;
        Response.Buffer = true;
        Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
        Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
        Response.AppendHeader("Expires", "0"); // Proxies.
        if (Request.Cookies["User"] != null)
        {
            Response.Cookies["User"].Value = string.Empty;
            Response.Cookies["User"].Expires = DateTime.Now.AddMonths(-20);
        }
        if (Request.Cookies["SFToken"] != null)
        {
            Response.Cookies["SFToken"].Value = string.Empty;
            Response.Cookies["SFToken"].Expires = DateTime.Now.AddMonths(-20);
        }
        Response.RedirectToRoute("Productlist");
    }
    #endregion
    #region TryCatchLog
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
            catch (Exception exm)
            {
                exm.Message.ToString();
            }
        }
    }
    #endregion

    protected void btnback_Click(object sender, EventArgs e)
    {
        if (ViewState["buyitems"] != null)
        {
            Session["DCart"] = ViewState["buyitems"];
            string totalcartvalue = Session["DCart"].ToString();
            Response.Redirect("ProductList");
        }
        else
        {
            totalno.InnerText = dtCart.Rows.Count.ToString();
            Response.Redirect("ProductList");
        }
    }
    protected void lnktooltip_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "searchtooltip", "showPopup4();", true);
    }
}