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

public partial class Admin_ViewProduct : System.Web.UI.Page
{
    Logic Lo = new Logic();
    DataUtility Co = new DataUtility();
    Cryptography objEnc = new Cryptography();
    DataTable DtGrid = new DataTable();
    DataTable DtCompanyDDL = new DataTable();
    string UserName;
    string RefNo;
    string UserEmail;
    string currentPage = "";
    private string mType = "";
    private string mRefNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
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
                currentPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
                mType = objEnc.DecryptData(Session["Type"].ToString());
                mRefNo = Session["CompanyRefNo"].ToString();
                BindGridView();
            }
        }
    }
    #region Load
    protected void BindGridView(string sortExpression = null)
    {
        try
        {
            if (mType == "SuperAdmin")
            {
                DataTable DtGrid = Lo.RetriveProductCode("", "", "ProductMaster");
                if (DtGrid.Rows.Count > 0)
                {
                    if (sortExpression != null)
                    {
                        DataView dv = DtGrid.AsDataView();
                        this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";
                        dv.Sort = sortExpression + " " + this.SortDirection;
                        gvproduct.DataSource = dv;
                    }
                    else
                    {
                        gvproduct.DataSource = DtGrid;
                    }
                    gvproduct.DataBind();

                }
            }
            else if (mType == "Company" && mRefNo != "")
            {
                DataTable DtGrid = Lo.RetriveProductCode("", mRefNo, "ProductMasterID");
                if (DtGrid.Rows.Count > 0)
                {
                    gvproduct.DataSource = DtGrid;
                    gvproduct.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    private string SortDirection
    {
        get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
        set { ViewState["SortDirection"] = value; }
    }
    #endregion
    #region PageIndex or Sorting
    protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvproduct.PageIndex = e.NewPageIndex;
        this.BindGridView();
    }
    protected void OnSorting(object sender, GridViewSortEventArgs e)
    {
        this.BindGridView(e.SortExpression);
    }
    #endregion
    #region RowCommand
    string stpsdq;
    protected void gvproduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditComp")
        {
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gvr.RowIndex;
            string Role = (gvproduct.Rows[rowIndex].FindControl("lblcompanyrole") as Label).Text;
            string stridNew = Request.QueryString["id"].ToString().Replace(" ", "+");
            string mstrid = objEnc.EncryptData((objEnc.DecryptData(stridNew) + " >> Edit Company"));
            Response.Redirect("AddProduct?mrcreaterole=" + objEnc.EncryptData(Role) + "&mcurrentcompRefNo=" + (objEnc.EncryptData(e.CommandArgument.ToString())) + "&id=" + mstrid);
        }
        else if (e.CommandName == "ViewComp")
        {
            DataTable DtView = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductMasterID");
            if (DtView.Rows.Count > 0)
            {
                lblcomprefno.Text = DtView.Rows[0]["CompanyRefNo"].ToString();
                lblcompname.Text = DtView.Rows[0]["CompanyName"].ToString();
                lblprodrefno.Text = DtView.Rows[0]["ProductRefNo"].ToString();
                lbloempartnumber.Text = DtView.Rows[0]["OEMPartNumber"].ToString();
                lbldpsupartno.Text = DtView.Rows[0]["DPSUPartNumber"].ToString();
                lblenduserpartno.Text = DtView.Rows[0]["EndUserPartNumber"].ToString();
                lblhsncode.Text = DtView.Rows[0]["HSNCode"].ToString();
                lblnatocode.Text = DtView.Rows[0]["NatoCode"].ToString();
                lblerprefno.Text = DtView.Rows[0]["ERPRefNo"].ToString();
                lblnomenclatureofmainsystem.Text = DtView.Rows[0]["Nomenclature"].ToString();
                lblprodlevel1.Text = DtView.Rows[0]["ProdLevel1Name"].ToString();
                productlevel2.Text = DtView.Rows[0]["ProdLevel2Name"].ToString();
                lblproductdescription.Text = DtView.Rows[0]["ProductDescription"].ToString();
                lbltechlevel1.Text = DtView.Rows[0]["TechLevel1Name"].ToString();
                lbltechlevel2.Text = DtView.Rows[0]["Techlevel2Name"].ToString();
                lblenduser.Text = DtView.Rows[0]["EUserName"].ToString();
                lblplatform.Text = DtView.Rows[0]["PlatName"].ToString();
                lblpurposeofprocurement.Text = DtView.Rows[0]["PRrocurement"].ToString();
                lblprodtimeframe.Text = DtView.Rows[0]["PRequirement"].ToString();
                lblsearchkeyword.Text = DtView.Rows[0]["SearchKeyword"].ToString();
                lblprodalredyindeginized.Text = DtView.Rows[0]["IsIndeginized"].ToString();
                if (lblprodalredyindeginized.Text == "Y")
                {
                    tablemanufacturename.Visible = true;
                    lblmanufacturename.Text = DtView.Rows[0]["ManufactureName"].ToString();
                }
                else
                {
                    tablemanufacturename.Visible = false;
                }
                DataTable dtImageBind = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductImage");
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
                DataTable dtpsdq = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductPSDQ");
                if (dtpsdq.Rows.Count > 0)
                {
                    for (int i = 0; dtpsdq.Rows.Count > i; i++)
                    {
                        stpsdq = stpsdq + "," + dtpsdq.Rows[i]["SCategoryName"].ToString();
                    }
                }
                lblsupportprovidedbydpsu.Text = stpsdq.Substring(1).ToString();
                lblremarks.Text = DtView.Rows[0]["Remarks"].ToString();
                lblestimatedquantity.Text = DtView.Rows[0]["Estimatequantity"].ToString();
                lblestimatedprice.Text = DtView.Rows[0]["EstimatePriceLLP"].ToString();
                lbltenderstatus.Text = DtView.Rows[0]["TenderStatus"].ToString();
                lbltendersubmission.Text = DtView.Rows[0]["TenderStatus"].ToString();
                lbltenderdate.Text = DtView.Rows[0]["TenderFillDate"].ToString();
                lbltenderurl.Text = DtView.Rows[0]["TenderUrl"].ToString();
                DataTable dtNodal = Lo.RetriveProductCode("", e.CommandArgument.ToString(), "ProductNodal");
                if (dtNodal.Rows.Count > 0)
                {
                    lblempcode.Text = dtNodal.Rows[0]["NodalEmpCode"].ToString();
                    lbldesignation.Text = dtNodal.Rows[0]["Designation"].ToString();
                    lblemailid.Text = dtNodal.Rows[0]["NodalOfficerEmail"].ToString();
                    lblmobilenumber.Text = dtNodal.Rows[0]["NodalOfficerMobile"].ToString();
                    lblphonenumber.Text = dtNodal.Rows[0]["NodalOfficerTelephone"].ToString();
                    lblfax.Text = dtNodal.Rows[0]["NodalOfficerFax"].ToString();

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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "changePass", "showPopup();", true);
            }
        }
        else if (e.CommandName == "DeleteComp")
        {
            try
            {
                string DeleteRec = Lo.DeleteRecord(e.CommandArgument.ToString(), "InActiveProd");
                if (DeleteRec == "true")
                {
                    BindGridView();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record delete succssfully.')", true);
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
    #region ReturnUrl Long"
    public string Resturl(int length)
    {
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        StringBuilder res = new StringBuilder();
        Random rnd = new Random();
        while (0 < length--)
        {
            res.Append(valid[rnd.Next(valid.Length)]);
        }
        return res.ToString();
    }
    #endregion
}