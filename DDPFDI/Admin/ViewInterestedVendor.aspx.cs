using BusinessLayer;
using Encryption;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using context = System.Web.HttpContext;


public partial class Admin_ViewInterestedVendor : System.Web.UI.Page
{
    private Logic Lo = new Logic();
    private DataUtility Co = new DataUtility();
    private Cryptography objCrypto= new Cryptography();
    private Cryptography objEnc = new Cryptography();
    private DataTable DtGrid = new DataTable();
    DataTable DtFilterView = new DataTable();
    private DataTable DtCompanyDDL = new DataTable();
    private string currentPage = "";
    private string mRefNo = "";
    private PagedDataSource pgsource = new PagedDataSource();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Type"] != null || Session["User"] != null)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    try
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
                        hidType.Value = objEnc.DecryptData(Session["Type"].ToString());
                        mRefNo = Session["CompanyRefNo"].ToString();
                        ddlcompany.Visible = true;
                        ddldivision.Visible = true;
                        ddlunit.Visible = true;
                        BindCompany();
                        ControlGrid();
                    }
                    catch (Exception ex)
                    {
                        string error = ex.ToString();
                        string Page = Request.Url.AbsolutePath.ToString();
                        Response.Redirect("Error?techerror=" + objEnc.EncryptData(error) + "&page=" + objEnc.EncryptData(Page));
                    }
                }
                else
                {
                    Response.RedirectToRoute("login"); }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "ErrorMssgPopup('Session Expired,Please login again');window.location='Login'", true);
        }
    }
    #region pageload

    #region Load
    protected void ControlGrid()
    {
        DataTable DtGrid = Lo.GetInteresteddata("CompanyRefNo", "", "");
        if(DtGrid.Rows.Count>0)
        {
            Session["PDatatTable"] = DtGrid;
            SeachResult();
        }
        else
        { }
    }    
    protected void BindCompany()
    {
        if (hidType.Value == "SuperAdmin" || hidType.Value == "Admin" )
        {
            if (Request.QueryString["mcurrentcompRefNo"] != null)
            {
                ddlcompany.Enabled = false;
                if (objCrypto.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Company")
                {
                    DtCompanyDDL = Lo.RetriveMasterData(0,Session["CompanyRefNo"].ToString(), "Company", 0, "", "", "CompanyName");
                    ddlcompany.Enabled = false;
                    if (DtCompanyDDL.Rows.Count > 0)
                    {
                        Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                        lblselectdivison.Visible = false;
                        lblselectunit.Visible = false;
                    }
                }
                else if (objCrypto.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Factory" || objCrypto.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Division")
                {
                    DtCompanyDDL = Lo.RetriveMasterData(0, Session["CompanyRefNo"].ToString(), "Company1", 0, "", "", "CompanyName");
                    ddlcompany.Enabled = false;
                    if (DtCompanyDDL.Rows.Count > 0)
                    {
                        Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                    }
                    DtCompanyDDL = Lo.RetriveMasterData(0, objCrypto.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "Factory", 0, "", "", "CompanyName");
                    DataTable DtDivisionDDL = Lo.RetriveMasterData(0, DtCompanyDDL.Rows[0]["CompanyRefNo"].ToString(), "Factory1", 0, "", "", "CompanyName");
                    if (DtDivisionDDL.Rows.Count > 0)
                    {
                        Co.FillDropdownlist(ddldivision, DtDivisionDDL, "FactoryName", "FactoryRefNo");
                        ddldivision.Enabled = false;
                        ddlcompany.Enabled = false;
                        ddldivision.Visible = true;
                        lblselectunit.Visible = false;
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
                    }
                    DtCompanyDDL = Lo.RetriveMasterData(0, objCrypto.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "Unit", 0, "", "", "CompanyName");
                    DataTable DtDivisionDDL = Lo.RetriveMasterData(0, DtCompanyDDL.Rows[0]["CompanyRefNo"].ToString(), "Factory1", 0, "", "", "CompanyName");
                    if (DtDivisionDDL.Rows.Count > 0)
                    {
                        Co.FillDropdownlist(ddldivision, DtDivisionDDL, "FactoryName", "FactoryRefNo");
                        lblselectdivison.Visible = true;
                        ddldivision.Enabled = false;
                        ddlcompany.Enabled = false;
                        ddldivision.Visible = true;
                        DataTable DtUnitDDL = Lo.RetriveMasterData(0, DtDivisionDDL.Rows[0]["FactoryRefNo"].ToString(), "Unit1", 0, "", "", "CompanyName");
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
                DtCompanyDDL = Lo.RetriveMasterData(0, "", hidType.Value, 0, "", "", "Select");
                if (DtCompanyDDL.Rows.Count > 0)
                {
                    Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                    ddlcompany.Items.Insert(0, "Select");
                    ddlcompany.Enabled = true;
                    lblselectdivison.Visible = false;
                    lblselectunit.Visible = false;
                }
                else
                {
                    ddlcompany.Enabled = false;
                }
            }
        }
        else if (hidType.Value == "Company")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, mRefNo, "Company", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = false;
                ddldivision.Items.Insert(0, "Select");
                lblselectdivison.Visible = false;
                lblselectunit.Visible = false;

            }
            else
            {
                ddlcompany.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                ddldivision.Items.Insert(0, "Select");
                if (hidType.Value == "Company")
                {
                    lblselectdivison.Visible = true;
                    ddldivision.Enabled = true;
                    lblselectunit.Visible = false;
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
        else if (hidType.Value == "Factory" || hidType.Value == "Division")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, hfcomprefno.Value, "Company1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = false;
            }
            else
            {
                ddlcompany.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                DataTable dt = Lo.RetriveMasterData(0, hfcomprefno.Value, "Factory2", 0, "", "", "CompanyName");
                if (dt.Rows.Count > 0)
                {
                    ddldivision.SelectedValue = dt.Rows[0]["FactoryRefNo"].ToString();
                }
                ddlunit.Items.Insert(0, "Select");
                lblselectunit.Visible = false;
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
                ddlunit.Items.Insert(0, "Select");
                ddlunit.Enabled = true;
                ddlunit.Visible = true;
                lblselectunit.Visible = true;
            }
            else
            {
                ddlunit.Items.Insert(0, "Select");
                lblselectunit.Visible = false;
            }
        }
        else if (hidType.Value == "Unit")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, hfcomprefno.Value, "Company2", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = false;
            }
            else
            {
                ddlcompany.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                DataTable dt = Lo.RetriveMasterData(0, hfcomprefno.Value, "Factory3", 0, "", "", "CompanyName");
                if (dt.Rows.Count > 0)
                {
                    ddldivision.SelectedValue = dt.Rows[0]["FactoryRefNo"].ToString();
                }
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
                ddlunit.SelectedValue = hfcomprefno.Value;
                ddlunit.Enabled = false;
                lblselectunit.Visible = true;
            }
            else
            {
                ddlunit.Enabled = false;
            }
        }       
    }  
    string insert1 = "";
    protected string Dvinsert(string sortExpression = null)
    {
        DataTable insert = new DataTable();
        insert.Columns.Add(new DataColumn("Column", typeof(string)));
        insert.Columns.Add(new DataColumn("Value", typeof(string)));
        DataRow dr;
        if (ddlunit.Visible == true && ddlunit.SelectedItem.Text != "Select")
        {
            dr = insert.NewRow();
            dr["Column"] = "UnitName =";
            dr["Value"] = "'" + ddlunit.SelectedItem.Text + "'";
            insert.Rows.Add(dr);
        }
        else if (ddldivision.Visible == true && ddldivision.SelectedItem.Text != "Select")
        {
            dr = insert.NewRow();
            dr["Column"] = "FactoryName =";
            dr["Value"] = "'" + ddldivision.SelectedItem.Text + "'";
            insert.Rows.Add(dr);
        }
        else if (ddlcompany.Visible == true && ddlcompany.SelectedItem.Text != "Select")
        {
            dr = insert.NewRow();
            dr["Column"] = "(CompanyName =";
            dr["Value"] = "'" + ddlcompany.SelectedItem.Text + "')";
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
    protected void SeachResult()
    {
        try
        {
            DtFilterView = (DataTable)Session["PDatatTable"];
            if (DtFilterView.Rows.Count > 0)
            {
                DataView dv = new DataView(DtFilterView);
                DataTable dtnew = dv.ToTable();
                
                if (dtnew.Rows.Count > 0)
                {
                    dv.RowFilter = BindInsertfilter();
                    DataTable dtinner = dv.ToTable();
                    DataTable dtads = dv.ToTable();
                    Session["TempData"] = dtads;
                    if (dtads.Rows.Count > 0)
                    {
                        lbltotfilter.Text = dtads.Rows.Count.ToString();
                        pgsource.DataSource = dtinner.DefaultView;
                        pgsource.AllowPaging = true;
                        pgsource.PageSize = 25;
                        pgsource.CurrentPageIndex = pagingCurrentPage;
                        lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                        lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                        lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                        pgsource.DataSource = dtads.DefaultView;
                        gvproductItem.DataSource = pgsource;
                        gvproductItem.DataBind();
                        gvproductItem.Visible = true;
                        divproductgridview.Visible = true;
                        divpageindex.Visible = true;
                        lbltotalshowpageitem.Text = pgsource.FirstIndexInPage + 1 + " - " + (pgsource.FirstIndexInPage + pgsource.Count);
                    }
                    else
                    {
                        gvproductItem.Visible = false;
                        divproductgridview.Visible = false;
                        divpageindex.Visible = false;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
                    }
                }
            }
        }
        catch (Exception ex)
        { }
    }
    protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (ddlcompany.SelectedItem.Text != "Select")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "", 0, "", "", "FactoryCompanyID");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                ddldivision.Items.Insert(0, "Select");
                lblselectdivison.Visible = true;
                ddldivision.Visible = true;
                lblselectunit.Visible = false;
                hidCompanyRefNo.Value = ddlcompany.SelectedItem.Value;
                hidType.Value = "Company";
            }
            else
            {
                ddldivision.Visible = false;
                lblselectdivison.Visible = false;
                gvproductItem.Visible = false;
            }
        }
        else if (ddlcompany.SelectedItem.Text == "Select")
        {
            lblselectdivison.Visible = false;
            lblselectunit.Visible = false;
          
        }
        hfcomprefno.Value = "";
        hfcomprefno.Value = ddlcompany.SelectedItem.Value;
        SeachResult();
    }
    protected void ddldivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldivision.SelectedItem.Text != "Select")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "", 0, "", "", "UnitSelectID");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
                ddlunit.Items.Insert(0, "Select");
                ddlunit.Visible = true;
                lblselectunit.Visible = true;
                if (ddlunit.SelectedItem.Text == "Select")
                {
                    ddldivision.Enabled = true;
                }
                else
                { ddldivision.Enabled = false; }
                hidCompanyRefNo.Value = ddldivision.SelectedItem.Value;
                hidType.Value = "Division";
            }
            else
            {
                lblselectunit.Visible = false;
                ddlunit.Visible = false;
                hidType.Value = "Division";
            }
            hfcomprefno.Value = "";
            hfcomprefno.Value = ddldivision.SelectedItem.Value;
        }
        else if (ddldivision.SelectedItem.Text == "Select")
        {
            ddlcompany.Enabled = false;
            lblselectunit.Visible = false;
            hidCompanyRefNo.Value = ddlcompany.SelectedItem.Value;
            hidType.Value = "Company";
            hfcomprefno.Value = "";
            hfcomprefno.Value = ddlcompany.SelectedItem.Value;
        }
        SeachResult();
    }
    protected void ddlunit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlunit.SelectedItem.Text != "Select")
        {
            hidCompanyRefNo.Value = ddlunit.SelectedItem.Value;
            hidType.Value = "Unit";
            hfcomprefno.Value = "";
            hfcomprefno.Value = ddlunit.SelectedItem.Value;
        }
        else
        {
            hidCompanyRefNo.Value = ddldivision.SelectedItem.Value;
            hidType.Value = "Division";
            if (hidType.Value == "Unit")
            {
                ddldivision.Enabled = false;
            }
            else
            {
                ddldivision.Enabled = true;
            }
            hfcomprefno.Value = "";
            hfcomprefno.Value = ddldivision.SelectedItem.Value;
        }
        SeachResult();
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
    protected void gvproductItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
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

    protected void btnexcel_Click(object sender, EventArgs e)
    {
        try
        {
           
           
           DataTable searchdt = (DataTable)Session["TempData"];
            //DataTable dtnew == (DataTable)Session["PDatatTable"];
            int[] iColumns = { 4,5,6,15,19,20,1,21,22 };
            RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
            objExport.ExportDetails(searchdt, iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "VendorList.xls");
        }
        catch (Exception ex)
        {
        }
    }
}
