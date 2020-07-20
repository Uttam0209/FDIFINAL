using BusinessLayer;
using Encryption;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Configuration;

public partial class Admin_ViewProductFilter : System.Web.UI.Page
{
    private Logic Lo = new Logic();
    private DataTable DtGrid = new DataTable();
    private HybridDictionary hySaveProdInfo = new HybridDictionary();
    private string _msg = string.Empty;
    private string _sysMsg = string.Empty;
    DataUtility Co = new DataUtility();
    private Cryptography Encrypt = new Cryptography();
    private PagedDataSource pgsource = new PagedDataSource();
    HybridDictionary HyLoginStatus = new HybridDictionary();
    DataTable DtCompany = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        PageL();
    }
    #region pageload
    protected void PageL()
    {
        if (!IsPostBack)
        {
            if (Session["User"] != null)
            {
                if (Request.QueryString["id"] != null)
                {
                    if (Encrypt.DecryptData(Session["Type"].ToString()) == "Admin" || Encrypt.DecryptData(Session["Type"].ToString()) == "SuperAdmin")
                    {
                        if (Request.QueryString["strangone"] != null)
                        {
                            ControlGrid(Encrypt.DecryptData(Request.QueryString["id"].ToString()), Encrypt.DecryptData(Request.QueryString["strangone"].ToString()));
                        }
                        else
                        {
                            ControlGrid(Encrypt.DecryptData(Request.QueryString["id"].ToString()), "");
                        }
                    }
                    else
                    {
                        ControlGrid(Encrypt.DecryptData(Request.QueryString["id"].ToString()), Session["CompanyRefNo"].ToString());
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "ErrorMssgPopup('Session Expired,Please login again');window.location='Login'", true);
            }
        }
    }
    #endregion
    protected void lbllogout_Click(object sender, EventArgs e)
    {
        Logoutstatus();
    }
    protected void Logoutstatus()
    {
        try
        {
            HyLoginStatus["LoginUser"] = Encrypt.DecryptData(Session["User"].ToString());
            HyLoginStatus["IsLogedIn"] = "N";
            DateTime Date = Convert.ToDateTime(DateTime.Now);
            string dateformat = Date.ToString("yyyy-MM-dd hh:mm:ss");
            HyLoginStatus["IsLogedOutTime"] = dateformat.ToString();
            string InsertLogOutStatus = Lo.SaveLogoutstatus(HyLoginStatus, out  _sysMsg, out  _msg);
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            Response.RedirectToRoute("Login");
        }
        catch (Exception ex)
        { Response.RedirectToRoute("Login"); }
    }
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
    private void ControlGrid(string mVal, string RefNo)
    {
        hfmtype.Value = mVal.ToString();
        hfmref.Value = RefNo.ToString();
        BindComapnyCheckbox();
        BindNSG();
        BindIndusrtyDomain();
        BindSearchKeyword();
        BindPurposeProcuremnt();
        if (hfmtype.Value == "P")
        {
            gvproduct.Visible = true;
            BindProduct(hfmref.Value);
        }
        else if (hfmtype.Value == "PI")
        {
            gvproduct.Visible = true;
            BindProduct(hfmref.Value);
        }
        else if (hfmtype.Value == "M2")
        {
            gvproduct.Visible = true;
            BindProduct(hfmref.Value);
        }
        else if (hfmtype.Value == "PH")
        {
            gvproduct.Visible = true;
            BindProduct(hfmref.Value);
        }
        else if (hfmtype.Value == "WPH")
        {
            gvproduct.Visible = true;
            BindProduct(hfmref.Value);
        }
        else
        {
            BindProduct(hfmref.Value);
        }
    }
    int n1 = 1;
    int n2 = 100;
    DataTable DtFilterView = new DataTable();
    protected void BindProduct(string RefNo, string sortExpression = null)
    {
        DtGrid = Lo.GetDashboardData("Product", "");
        if (DtGrid.Rows.Count > 0)
        {
            Session["TempData"] = DtGrid;
            DtFilterView = (DataTable)Session["TempData"];
            divclearfilorexceldown.Visible = true;
            UpdateDtGridValue(DtGrid);
            if (hfmref.Value != "")
            {
                DataView dv1 = new DataView(DtGrid, "ROW_NUMBER >='" + n1 + "' And  ROW_NUMBER <='" + n2 + "'", "", DataViewRowState.CurrentRows);
                if (Request.QueryString["strangone"] != null)
                {
                    dv1.RowFilter = "CompanyRefNo='" + hfmref.Value + "'";
                }
                else if (Encrypt.DecryptData(Session["Type"].ToString()).ToUpper() == "COMPANY")
                {
                    dv1.RowFilter = "CompanyRefNo='" + hfmref.Value + "'";
                }
                else if (Encrypt.DecryptData(Session["Type"].ToString()).ToUpper() == "DIVISION")
                {
                    dv1.RowFilter = "FactoryRefNo='" + hfmref.Value + "'";
                }
                else if (Encrypt.DecryptData(Session["Type"].ToString()).ToUpper() == "UNIT")
                {
                    dv1.RowFilter = "UnitRefNo='" + hfmref.Value + "'";
                }
                dv1.Sort = "LastUpdated desc";
                DataTable dtads1 = dv1.ToTable();
                dtads1.Columns.Add("TopPdf", typeof(string));
                dtads1.Columns.Add("TopImages", typeof(string));
                dtads1.Columns.Add("1718", typeof(decimal));
                dtads1.Columns.Add("1819", typeof(decimal));
                dtads1.Columns.Add("1920", typeof(decimal));
                dtads1.Columns.Add("2021", typeof(decimal));
                for (int i = 0; dtads1.Rows.Count > i; i++)
                {
                    string mProdRefTime = dtads1.Rows[i]["ProductRefNo"].ToString();
                    DataTable dtImageBind1 = Lo.RetriveProductCode("", mProdRefTime, "RetImageTop", "");
                    if (dtImageBind1.Rows.Count > 0)
                    {
                        dtads1.Rows[i]["TopImages"] = dtImageBind1.Rows[0]["ImageName"].ToString();
                    }
                    else
                    {
                        // dtads.Rows[i]["TopImages"] = "assets/images/Noimage.png";
                    }
                    DataTable dtImageBind2 = Lo.RetriveProductCode("", mProdRefTime, "RetPdfTop", "");
                    if (dtImageBind2.Rows.Count > 0)
                    {
                        dtads1.Rows[i]["TopPdf"] = dtImageBind2.Rows[0]["ImageName"].ToString();
                    }
                    else
                    {
                        // dtads.Rows[i]["TopImages"] = "assets/images/Noimage.png";
                    }
                    string mProdRefesti = dtads1.Rows[i]["ProductRefNo"].ToString();
                    DataTable dtEstimate1 = Lo.RetriveProductCode("", mProdRefesti, "estimate", "");
                    for (int es = 0; dtEstimate1.Rows.Count > es; es++)
                    {
                        if (dtEstimate1.Rows[es]["FYear"].ToString() == "2017-18" && dtEstimate1.Rows[es]["Type"].ToString() == "O")
                        {
                            dtads1.Rows[i]["1718"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                        }
                        if (dtEstimate1.Rows[es]["FYear"].ToString() == "2018-19" && dtEstimate1.Rows[es]["Type"].ToString() == "O")
                        {
                            dtads1.Rows[i]["1819"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                        }
                        if (dtEstimate1.Rows[es]["FYear"].ToString() == "2019-20" && dtEstimate1.Rows[es]["Type"].ToString() == "O")
                        {
                            dtads1.Rows[i]["1920"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                        }
                        if (dtEstimate1.Rows[es]["FYear"].ToString() == "2020-21" && dtEstimate1.Rows[es]["Type"].ToString() == "F")
                        {
                            dtads1.Rows[i]["2021"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                        }
                    }
                }
                pgsource.DataSource = dtads1.DefaultView;
                pgsource.AllowPaging = true;
                pgsource.PageSize = 100;
                pgsource.CurrentPageIndex = pagingCurrentPage;
                ViewState["totpage"] = pgsource.PageCount;
                lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                lblcountpgindex.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                if (hfmref.Value != "")
                {
                    lbltotal.Text = "Total Result " + dtads1.Rows.Count.ToString();

                }
                else
                {
                    lbltotal.Text = "Total Result " + DtGrid.Rows.Count.ToString();
                }
                lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                lnkbtnPgPre.Enabled = !pgsource.IsFirstPage;
                lnkbtnne.Enabled = !pgsource.IsLastPage;
                if (sortExpression != null)
                {
                    DataView _DvSort = dtads1.DefaultView;
                    if (_DvSort != null && _DvSort.Count > 0)
                    {
                        this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";
                        _DvSort.Sort = sortExpression + " " + this.SortDirection;
                        pgsource.DataSource = _DvSort;
                    }
                    else
                    {
                        pgsource.DataSource = dtads1.DefaultView;
                    }
                }
                else
                {
                    pgsource.DataSource = dtads1.DefaultView;
                }
                gvproduct.DataSource = dtads1.DefaultView;
                gvproduct.DataBind();
                gvproduct.Visible = true;
                divpageindex.Visible = true;
                divProductGrid.Visible = true;
                divpageindex1.Visible = true;
            }
            else
            {
                if (Request.QueryString["id"] != null)
                {
                    if (Encrypt.DecryptData(Request.QueryString["id"].ToString()) == "PI")
                    {
                        DataView dv2 = new DataView(DtGrid);
                        dv2.RowFilter = "IsIndeginized='Y'";
                        dv2.Sort = "LastUpdated desc";
                        DataTable dtads2 = dv2.ToTable();
                        dtads2.Columns.Add("TopPdf", typeof(string));
                        dtads2.Columns.Add("TopImages", typeof(string));
                        dtads2.Columns.Add("1718", typeof(decimal));
                        dtads2.Columns.Add("1819", typeof(decimal));
                        dtads2.Columns.Add("1920", typeof(decimal));
                        dtads2.Columns.Add("2021", typeof(decimal));
                        for (int i = 0; dtads2.Rows.Count > i; i++)
                        {
                            string mProdRefTime = dtads2.Rows[i]["ProductRefNo"].ToString();
                            DataTable dtImageBind2 = Lo.RetriveProductCode("", mProdRefTime, "RetImageTop", "");
                            if (dtImageBind2.Rows.Count > 0)
                            {
                                dtads2.Rows[i]["TopImages"] = dtImageBind2.Rows[0]["ImageName"].ToString();
                            }
                            else
                            {
                                //   dtads.Rows[i]["TopImages"] = "assets/images/Noimage.png";
                            }
                            DataTable dtImageBind3 = Lo.RetriveProductCode("", mProdRefTime, "RetPdfTop", "");
                            if (dtImageBind3.Rows.Count > 0)
                            {
                                dtads2.Rows[i]["TopPdf"] = dtImageBind3.Rows[0]["ImageName"].ToString();
                            }
                            else
                            {
                                // dtads.Rows[i]["TopImages"] = "assets/images/Noimage.png";
                            }
                            string mProdRefesti = dtads2.Rows[i]["ProductRefNo"].ToString();
                            DataTable dtEstimate1 = Lo.RetriveProductCode("", mProdRefesti, "estimate", "");
                            for (int es = 0; dtEstimate1.Rows.Count > es; es++)
                            {
                                if (dtEstimate1.Rows[es]["FYear"].ToString() == "2017-18" && dtEstimate1.Rows[es]["Type"].ToString() == "O")
                                {
                                    dtads2.Rows[i]["1718"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                                }
                                if (dtEstimate1.Rows[es]["FYear"].ToString() == "2018-19" && dtEstimate1.Rows[es]["Type"].ToString() == "O")
                                {
                                    dtads2.Rows[i]["1819"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                                }
                                if (dtEstimate1.Rows[es]["FYear"].ToString() == "2019-20" && dtEstimate1.Rows[es]["Type"].ToString() == "O")
                                {
                                    dtads2.Rows[i]["1920"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                                }
                                if (dtEstimate1.Rows[es]["FYear"].ToString() == "2020-21" && dtEstimate1.Rows[es]["Type"].ToString() == "F")
                                {
                                    dtads2.Rows[i]["2021"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                                }
                            }
                        }
                        pgsource.DataSource = dtads2.DefaultView;
                        pgsource.AllowPaging = true;
                        pgsource.PageSize = 100;
                        pgsource.CurrentPageIndex = pagingCurrentPage;
                        ViewState["totpage"] = pgsource.PageCount;
                        lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                        lblcountpgindex.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                        if (hfmref.Value != "")
                        {
                            lbltotal.Text = "Total Result " + dtads2.Rows.Count.ToString();
                        }
                        else
                        {
                            lbltotal.Text = "Total Result " + DtGrid.Rows.Count.ToString();
                        }
                        lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                        lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                        lnkbtnPgPre.Enabled = !pgsource.IsFirstPage;
                        lnkbtnne.Enabled = !pgsource.IsLastPage;
                        if (sortExpression != null)
                        {
                            DataView _DvSort2 = dtads2.DefaultView;
                            if (_DvSort2 != null && _DvSort2.Count > 0)
                            {
                                this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";
                                _DvSort2.Sort = sortExpression + " " + this.SortDirection;
                                pgsource.DataSource = _DvSort2;
                            }
                            else
                            {
                                pgsource.DataSource = dtads2.DefaultView;
                            }
                        }
                        else
                        {
                            pgsource.DataSource = dtads2.DefaultView;
                        }
                        gvproduct.DataSource = dtads2.DefaultView;
                    }
                    else if (Encrypt.DecryptData(Request.QueryString["id"].ToString()) == "M2")
                    {
                        DataView dv3 = new DataView(DtGrid);
                        dv3.RowFilter = "PurposeofProcurement like '%25%'";
                        dv3.Sort = "LastUpdated desc";
                        DataTable dtads3 = dv3.ToTable();
                        dtads3.Columns.Add("TopPdf", typeof(string));
                        dtads3.Columns.Add("TopImages", typeof(string));
                        dtads3.Columns.Add("1718", typeof(decimal));
                        dtads3.Columns.Add("1819", typeof(decimal));
                        dtads3.Columns.Add("1920", typeof(decimal));
                        dtads3.Columns.Add("2021", typeof(decimal));
                        for (int i = 0; dtads3.Rows.Count > i; i++)
                        {
                            string mProdRefTime = dtads3.Rows[i]["ProductRefNo"].ToString();
                            DataTable dtImageBind3 = Lo.RetriveProductCode("", mProdRefTime, "RetImageTop", "");
                            if (dtImageBind3.Rows.Count > 0)
                            {
                                dtads3.Rows[i]["TopImages"] = dtImageBind3.Rows[0]["ImageName"].ToString();
                            }
                            else
                            {
                                //  dtads.Rows[i]["TopImages"] = "assets/images/Noimage.png";
                            }
                            DataTable dtImageBind4 = Lo.RetriveProductCode("", mProdRefTime, "RetPdfTop", "");
                            if (dtImageBind4.Rows.Count > 0)
                            {
                                dtads3.Rows[i]["TopPdf"] = dtImageBind4.Rows[0]["ImageName"].ToString();
                            }
                            else
                            {
                                // dtads.Rows[i]["TopImages"] = "assets/images/Noimage.png";
                            }
                            string mProdRefesti = dtads3.Rows[i]["ProductRefNo"].ToString();
                            DataTable dtEstimate1 = Lo.RetriveProductCode("", mProdRefesti, "estimate", "");
                            for (int es = 0; dtEstimate1.Rows.Count > es; es++)
                            {
                                if (dtEstimate1.Rows[es]["FYear"].ToString() == "2017-18" && dtEstimate1.Rows[es]["Type"].ToString() == "O")
                                {
                                    dtads3.Rows[i]["1718"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                                }
                                if (dtEstimate1.Rows[es]["FYear"].ToString() == "2018-19" && dtEstimate1.Rows[es]["Type"].ToString() == "O")
                                {
                                    dtads3.Rows[i]["1819"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                                }
                                if (dtEstimate1.Rows[es]["FYear"].ToString() == "2019-20" && dtEstimate1.Rows[es]["Type"].ToString() == "O")
                                {
                                    dtads3.Rows[i]["1920"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                                }
                                if (dtEstimate1.Rows[es]["FYear"].ToString() == "2020-21" && dtEstimate1.Rows[es]["Type"].ToString() == "F")
                                {
                                    dtads3.Rows[i]["2021"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                                }
                            }
                        }
                        pgsource.DataSource = dtads3.DefaultView;
                        pgsource.AllowPaging = true;
                        pgsource.PageSize = 100;
                        pgsource.CurrentPageIndex = pagingCurrentPage;
                        ViewState["totpage"] = pgsource.PageCount;
                        lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                        lblcountpgindex.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                        if (hfmref.Value != "")
                        {
                            lbltotal.Text = "Total Result " + dtads3.Rows.Count.ToString();
                        }
                        else
                        {
                            lbltotal.Text = "Total Result " + DtGrid.Rows.Count.ToString();
                        }
                        lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                        lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                        lnkbtnPgPre.Enabled = !pgsource.IsFirstPage;
                        lnkbtnne.Enabled = !pgsource.IsLastPage;
                        if (sortExpression != null)
                        {
                            DataView _DvSort3 = dtads3.DefaultView;
                            if (_DvSort3 != null && _DvSort3.Count > 0)
                            {
                                this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";
                                _DvSort3.Sort = sortExpression + " " + this.SortDirection;
                                pgsource.DataSource = _DvSort3;
                            }
                            else
                            {
                                pgsource.DataSource = dtads3.DefaultView;
                            }
                        }
                        else
                        {
                            pgsource.DataSource = dtads3.DefaultView;
                        }
                        gvproduct.DataSource = dtads3.DefaultView; ;
                    }
                    else if (Encrypt.DecryptData(Request.QueryString["id"].ToString()) == "ID")
                    {
                        DataView dv5 = new DataView(DtGrid);
                        dv5.RowFilter = "IsApproved='N'";
                        dv5.Sort = "LastUpdated desc";
                        DataTable dtads5 = dv5.ToTable();
                        dtads5.Columns.Add("TopPdf", typeof(string));
                        dtads5.Columns.Add("TopImages", typeof(string));
                        dtads5.Columns.Add("1718", typeof(decimal));
                        dtads5.Columns.Add("1819", typeof(decimal));
                        dtads5.Columns.Add("1920", typeof(decimal));
                        dtads5.Columns.Add("2021", typeof(decimal));
                        for (int i = 0; dtads5.Rows.Count > i; i++)
                        {
                            string mProdRefTime = dtads5.Rows[i]["ProductRefNo"].ToString();
                            DataTable dtImageBind5 = Lo.RetriveProductCode("", mProdRefTime, "RetImageTop", "");
                            if (dtImageBind5.Rows.Count > 0)
                            {
                                dtads5.Rows[i]["TopImages"] = dtImageBind5.Rows[0]["ImageName"].ToString();
                            }
                            else
                            {
                                //   dtads.Rows[i]["TopImages"] = "assets/images/Noimage.png";
                            }
                            DataTable dtImageBind6 = Lo.RetriveProductCode("", mProdRefTime, "RetPdfTop", "");
                            if (dtImageBind6.Rows.Count > 0)
                            {
                                dtads5.Rows[i]["TopPdf"] = dtImageBind6.Rows[0]["ImageName"].ToString();
                            }
                            else
                            {
                                // dtads.Rows[i]["TopImages"] = "assets/images/Noimage.png";
                            }
                            string mProdRefesti = dtads5.Rows[i]["ProductRefNo"].ToString();
                            DataTable dtEstimate1 = Lo.RetriveProductCode("", mProdRefesti, "estimate", "");
                            for (int es = 0; dtEstimate1.Rows.Count > es; es++)
                            {
                                if (dtEstimate1.Rows[es]["FYear"].ToString() == "2017-18" && dtEstimate1.Rows[es]["Type"].ToString() == "O")
                                {
                                    dtads5.Rows[i]["1718"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                                }
                                if (dtEstimate1.Rows[es]["FYear"].ToString() == "2018-19" && dtEstimate1.Rows[es]["Type"].ToString() == "O")
                                {
                                    dtads5.Rows[i]["1819"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                                }
                                if (dtEstimate1.Rows[es]["FYear"].ToString() == "2019-20" && dtEstimate1.Rows[es]["Type"].ToString() == "O")
                                {
                                    dtads5.Rows[i]["1920"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                                }
                                if (dtEstimate1.Rows[es]["FYear"].ToString() == "2020-21" && dtEstimate1.Rows[es]["Type"].ToString() == "F")
                                {
                                    dtads5.Rows[i]["2021"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                                }
                            }
                        }
                        pgsource.DataSource = dtads5.DefaultView;
                        pgsource.AllowPaging = true;
                        pgsource.PageSize = 100;
                        pgsource.CurrentPageIndex = pagingCurrentPage;
                        ViewState["totpage"] = pgsource.PageCount;
                        lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                        lblcountpgindex.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                        if (hfmref.Value != "")
                        {
                            lbltotal.Text = "Total Result " + dtads5.Rows.Count.ToString();
                        }
                        else
                        {
                            lbltotal.Text = "Total Result " + dtads5.Rows.Count.ToString();
                        }
                        lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                        lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                        lnkbtnPgPre.Enabled = !pgsource.IsFirstPage;
                        lnkbtnne.Enabled = !pgsource.IsLastPage;
                        if (sortExpression != null)
                        {
                            DataView _DvSort2 = dtads5.DefaultView;
                            if (_DvSort2 != null && _DvSort2.Count > 0)
                            {
                                this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";
                                _DvSort2.Sort = sortExpression + " " + this.SortDirection;
                                pgsource.DataSource = _DvSort2;
                            }
                            else
                            {
                                pgsource.DataSource = dtads5.DefaultView;
                            }
                        }
                        else
                        {
                            pgsource.DataSource = dtads5.DefaultView;
                        }
                        gvproduct.DataSource = dtads5.DefaultView;
                    }
                    else if (Encrypt.DecryptData(Request.QueryString["id"].ToString()) == "IA")
                    {
                        DataView dv2 = new DataView(DtGrid);
                        dv2.RowFilter = "IsApproved='Y'";
                        dv2.Sort = "LastUpdated desc";
                        DataTable dtads2 = dv2.ToTable();
                        dtads2.Columns.Add("TopPdf", typeof(string));
                        dtads2.Columns.Add("TopImages", typeof(string));
                        dtads2.Columns.Add("1718", typeof(decimal));
                        dtads2.Columns.Add("1819", typeof(decimal));
                        dtads2.Columns.Add("1920", typeof(decimal));
                        dtads2.Columns.Add("2021", typeof(decimal));
                        for (int i = 0; dtads2.Rows.Count > i; i++)
                        {
                            string mProdRefTime = dtads2.Rows[i]["ProductRefNo"].ToString();
                            DataTable dtImageBind2 = Lo.RetriveProductCode("", mProdRefTime, "RetImageTop", "");
                            if (dtImageBind2.Rows.Count > 0)
                            {
                                dtads2.Rows[i]["TopImages"] = dtImageBind2.Rows[0]["ImageName"].ToString();
                            }
                            else
                            {
                                //   dtads.Rows[i]["TopImages"] = "assets/images/Noimage.png";
                            }
                            DataTable dtImageBind6 = Lo.RetriveProductCode("", mProdRefTime, "RetPdfTop", "");
                            if (dtImageBind6.Rows.Count > 0)
                            {
                                dtads2.Rows[i]["TopPdf"] = dtImageBind6.Rows[0]["ImageName"].ToString();
                            }
                            else
                            {
                                // dtads.Rows[i]["TopImages"] = "assets/images/Noimage.png";
                            }
                            string mProdRefesti = dtads2.Rows[i]["ProductRefNo"].ToString();
                            DataTable dtEstimate1 = Lo.RetriveProductCode("", mProdRefesti, "estimate", "");
                            for (int es = 0; dtEstimate1.Rows.Count > es; es++)
                            {
                                if (dtEstimate1.Rows[es]["FYear"].ToString() == "2017-18" && dtEstimate1.Rows[es]["Type"].ToString() == "O")
                                {
                                    dtads2.Rows[i]["1718"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                                }
                                if (dtEstimate1.Rows[es]["FYear"].ToString() == "2018-19" && dtEstimate1.Rows[es]["Type"].ToString() == "O")
                                {
                                    dtads2.Rows[i]["1819"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                                }
                                if (dtEstimate1.Rows[es]["FYear"].ToString() == "2019-20" && dtEstimate1.Rows[es]["Type"].ToString() == "O")
                                {
                                    dtads2.Rows[i]["1920"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                                }
                                if (dtEstimate1.Rows[es]["FYear"].ToString() == "2020-21" && dtEstimate1.Rows[es]["Type"].ToString() == "F")
                                {
                                    dtads2.Rows[i]["2021"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                                }
                            }
                        }
                        pgsource.DataSource = dtads2.DefaultView;
                        pgsource.AllowPaging = true;
                        pgsource.PageSize = 100;
                        pgsource.CurrentPageIndex = pagingCurrentPage;
                        ViewState["totpage"] = pgsource.PageCount;
                        lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                        lblcountpgindex.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                        if (hfmref.Value != "")
                        {
                            lbltotal.Text = "Total Result " + dtads2.Rows.Count.ToString();
                        }
                        else
                        {
                            lbltotal.Text = "Total Result " + dtads2.Rows.Count.ToString();
                        }
                        lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                        lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                        lnkbtnPgPre.Enabled = !pgsource.IsFirstPage;
                        lnkbtnne.Enabled = !pgsource.IsLastPage;
                        if (sortExpression != null)
                        {
                            DataView _DvSort2 = dtads2.DefaultView;
                            if (_DvSort2 != null && _DvSort2.Count > 0)
                            {
                                this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";
                                _DvSort2.Sort = sortExpression + " " + this.SortDirection;
                                pgsource.DataSource = _DvSort2;
                            }
                            else
                            {
                                pgsource.DataSource = dtads2.DefaultView;
                            }
                        }
                        else
                        {
                            pgsource.DataSource = dtads2.DefaultView;
                        }
                        gvproduct.DataSource = dtads2.DefaultView;
                    }
                    else
                    {
                        DataView dv4 = new DataView(DtGrid, "ROW_NUMBER >='" + n1 + "' And  ROW_NUMBER<='" + n2 + "'", "", DataViewRowState.CurrentRows);
                        dv4.Sort = "LastUpdated desc";
                        DataTable dtads4 = dv4.ToTable();
                        dtads4.Columns.Add("TopPdf", typeof(string));
                        dtads4.Columns.Add("TopImages", typeof(string));
                        dtads4.Columns.Add("1718", typeof(decimal));
                        dtads4.Columns.Add("1819", typeof(decimal));
                        dtads4.Columns.Add("1920", typeof(decimal));
                        dtads4.Columns.Add("2021", typeof(decimal));
                        for (int i = 0; dtads4.Rows.Count > i; i++)
                        {
                            string mProdRefTime = dtads4.Rows[i]["ProductRefNo"].ToString();
                            DataTable dtImageBind4 = Lo.RetriveProductCode("", mProdRefTime, "RetImageTop", "");
                            if (dtImageBind4.Rows.Count > 0)
                            {
                                dtads4.Rows[i]["TopImages"] = dtImageBind4.Rows[0]["ImageName"].ToString();
                            }
                            else
                            {
                                //dtads.Rows[i]["TopImages"] = "assets/images/Noimage.png";
                            }
                            DataTable dtImageBind9 = Lo.RetriveProductCode("", mProdRefTime, "RetPdfTop", "");
                            if (dtImageBind9.Rows.Count > 0)
                            {
                                dtads4.Rows[i]["TopPdf"] = dtImageBind9.Rows[0]["ImageName"].ToString();
                            }
                            else
                            {
                                // dtads.Rows[i]["TopImages"] = "assets/images/Noimage.png";
                            }
                            string mProdRefesti = dtads4.Rows[i]["ProductRefNo"].ToString();
                            DataTable dtEstimate1 = Lo.RetriveProductCode("", mProdRefesti, "estimate", "");
                            for (int es = 0; dtEstimate1.Rows.Count > es; es++)
                            {
                                if (dtEstimate1.Rows[es]["FYear"].ToString() == "2017-18" && dtEstimate1.Rows[es]["Type"].ToString() == "O")
                                {
                                    dtads4.Rows[i]["1718"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                                }
                                if (dtEstimate1.Rows[es]["FYear"].ToString() == "2018-19" && dtEstimate1.Rows[es]["Type"].ToString() == "O")
                                {
                                    dtads4.Rows[i]["1819"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                                }
                                if (dtEstimate1.Rows[es]["FYear"].ToString() == "2019-20" && dtEstimate1.Rows[es]["Type"].ToString() == "O")
                                {
                                    dtads4.Rows[i]["1920"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                                }
                                if (dtEstimate1.Rows[es]["FYear"].ToString() == "2020-21" && dtEstimate1.Rows[es]["Type"].ToString() == "F")
                                {
                                    dtads4.Rows[i]["2021"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                                }
                            }
                        }
                        pgsource.DataSource = DtGrid.DefaultView;
                        pgsource.AllowPaging = true;
                        pgsource.PageSize = 100;
                        pgsource.CurrentPageIndex = pagingCurrentPage;
                        ViewState["totpage"] = pgsource.PageCount;
                        lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                        lblcountpgindex.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                        if (hfmref.Value != "")
                        {
                            lbltotal.Text = "Total Result " + dtads4.Rows.Count.ToString();
                        }
                        else
                        {
                            lbltotal.Text = "Total Result " + DtGrid.Rows.Count.ToString();
                        }
                        lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                        lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                        lnkbtnPgPre.Enabled = !pgsource.IsFirstPage;
                        lnkbtnne.Enabled = !pgsource.IsLastPage;
                        if (sortExpression != null)
                        {
                            DataView _DvSort4 = dtads4.DefaultView;
                            if (_DvSort4 != null && _DvSort4.Count > 0)
                            {
                                this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";
                                _DvSort4.Sort = sortExpression + " " + this.SortDirection;
                                pgsource.DataSource = _DvSort4;
                            }
                            else
                            {
                                pgsource.DataSource = dtads4.DefaultView;
                            }
                        }
                        else
                        {
                            pgsource.DataSource = dtads4.DefaultView;
                        }
                        gvproduct.DataSource = dtads4.DefaultView;
                    }
                    gvproduct.DataBind();
                    gvproduct.Visible = true;
                    divpageindex.Visible = true;
                    divProductGrid.Visible = true;
                    divpageindex1.Visible = true;
                }
            }
        }
        else
        {
            divclearfilorexceldown.Visible = false;
            divpageindex.Visible = false;
            divpageindex1.Visible = false;
        }
    }
    protected void gvproduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region ViewOneProd
        if (e.CommandName == "ViewComp")
        {
            try
            {
                GridViewRow item = (GridViewRow)(((Control)(e.CommandSource)).NamingContainer);
                string Role = ((HiddenField)item.FindControl("hfroleProd")).Value;
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
                    DtGridEstimate1 = Lo.RetriveSaveEstimateGrid("Select", 0, e.CommandArgument.ToString(), 0, "", "", "", "", "O");
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
                    if (DtView.Rows[0]["FeatursandDetail"].ToString() != "")
                    {
                        lblfeaturesanddetail.Text = DtView.Rows[0]["FeatursandDetail"].ToString();
                        fourteen.Visible = true;
                    }
                    else
                    {
                        fourteen.Visible = false;
                    }
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
                    if (DtView.Rows[0]["IsShowGeneral"].ToString() != "")
                    {
                        if (DtView.Rows[0]["IsShowGeneral"].ToString() == "Y")
                            lblisshowgeneral.Text = "Yes";
                        else
                            lblisshowgeneral.Text = "No";
                        twentyfour.Visible = false;
                    }
                    else
                    {
                        twentyfour.Visible = false;
                    }
                    if (DtView.Rows[0]["TermConditionImage"].ToString() != "")
                    {
                        twentythree.Visible = false;
                    }
                    else
                    {
                        twentythree.Visible = false;
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
                    ScriptManager.RegisterStartupScript(this, GetType(), "ProductCompany", "showPopup4();", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
            }
        }
        #endregion
    }
    protected void gvproduct_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TableSection = TableRowSection.TableBody;
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.TableSection = TableRowSection.TableFooter;
        }
    }
    #region //------------------------pageindex code--------------//
    protected void lnkbtnPgPrevious_Click(object sender, EventArgs e)
    {
        txtpageno.Text = "";
        txtsea.Text = "";
        pagingCurrentPage -= 1;
        n2 = Convert.ToInt16(pagingCurrentPage) * Convert.ToInt16(100);
        n1 = Convert.ToInt16(n2 - 100);
        if (lbclearfilter.Visible == true)
        { SeachResult(); }
        else
        {
            if (hfmtype.Value == "P")
            { BindProduct(hfmref.Value); }
            else if (hfmtype.Value == "PI")
            { BindProduct(hfmref.Value); }
            else if (hfmtype.Value == "M2")
            { BindProduct(hfmref.Value); }
        }
    }
    protected void lnkbtnPgNext_Click(object sender, EventArgs e)
    {
        pagingCurrentPage += 1;
        int txtpage = Convert.ToInt32(pagingCurrentPage) + 1;
        string mcount = "";
        mcount = txtpage.ToString();
        if (txtpageno.Text != "")
        {
            txtpageno.Text = txtpage.ToString();
            mcount = txtpageno.Text;
            txtsea.Text = txtpageno.Text;
        }
        else if (txtsea.Text != "")
        {
            txtsea.Text = txtpage.ToString();
            mcount = txtsea.Text;
            txtpageno.Text = txtsea.Text;
        }
        n2 = Convert.ToInt16(mcount) * Convert.ToInt16(100);
        n1 = Convert.ToInt16(n2 - 100);
        if (lbclearfilter.Visible == true)
        { SeachResult(); }
        else
        {
            if (hfmtype.Value == "P")
            { BindProduct(hfmref.Value); }
            else if (hfmtype.Value == "PI")
            { BindProduct(hfmref.Value); }
            else if (hfmtype.Value == "M2")
            { BindProduct(hfmref.Value); }
        }
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
    protected void btngoto_Click(object sender, EventArgs e)
    {
        if (System.Text.RegularExpressions.Regex.IsMatch(txtpageno.Text, "[^0-9]"))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Please enter only number')", true);
        }
        else
        {
            if (txtpageno.Text != "")
            {
                int txtpage = Convert.ToInt32(txtpageno.Text) - 1;
                pagingCurrentPage = Convert.ToInt32(txtpage.ToString());
                txtsea.Text = txtpageno.Text;
            }
            else if (txtsea.Text != "")
            {
                int txtpage = Convert.ToInt32(txtsea.Text) - 1;
                pagingCurrentPage = Convert.ToInt32(txtpage.ToString());
                txtpageno.Text = txtsea.Text;
            }
            n2 = Convert.ToInt16(pagingCurrentPage + 1) * Convert.ToInt16(100);
            n1 = Convert.ToInt16(n2 + 1 - 100);
            if (lbclearfilter.Visible == true)
            { SeachResult(); }
            else
            {
                if (hfmtype.Value == "P")
                { BindProduct(hfmref.Value); }
                else if (hfmtype.Value == "PI")
                { BindProduct(hfmref.Value); }
                else if (hfmtype.Value == "M2")
                { BindProduct(hfmref.Value); }
            }
        }
    }
    //end page index---------------------------------------//
    #endregion
    protected void lblback_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard?mu=" + Encrypt.EncryptData(Session["Type"].ToString()) + "&id=" + Encrypt.EncryptData(Session["CompanyRefNo"].ToString()));
    }
    protected void gvproduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hy = (e.Row.FindControl("lbpdffile") as HyperLink);
            HiddenField hfisapproved = (e.Row.FindControl("hfisaaproved") as HiddenField);
            if (hy.NavigateUrl.Trim() == "" || hy.NavigateUrl.Trim() == "~/Upload/" || hy.NavigateUrl.Trim() == null)
            {
                e.Row.Cells[3].Text = "NA";
            }
            HiddenField hfimagehide = (e.Row.FindControl("hfimagehide") as HiddenField);
            System.Web.UI.WebControls.Image img = (e.Row.FindControl("imgtop") as System.Web.UI.WebControls.Image);
            Label lblimagena = (e.Row.FindControl("lblimagena") as Label);
            if (hfimagehide.Value == "")
            {
                img.Visible = false;
                lblimagena.Visible = true;
            }
            else
            {
                img.Visible = true;
                lblimagena.Visible = false;
            }
            if (hfisapproved.Value == "Y")
            {
                //    gvproduct.BackColor = Color.Green;
            }
            else if (hfisapproved.Value == "N")
            {
                //  gvproduct.BackColor = Color.Red;
            }
        }
    }
    #region Filtor or CheckBoxCode
    DataTable DtCompanyDDL = new DataTable();
    protected void BindComapnyCheckbox()
    {
        if (Encrypt.DecryptData(Session["Type"].ToString()) == "Admin" || Encrypt.DecryptData(Session["Type"].ToString()) == "SuperAdmin")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, "", "", 0, "", "", "Select");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcomp, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcomp.Items.Insert(0, "Select");
                ddlcomp.Enabled = true;
            }
            else
            {
                ddlcomp.Enabled = false;
            }
        }
        else if (Encrypt.DecryptData(Session["Type"].ToString()) == "Company")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, hfmref.Value, "Company", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcomp, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcomp.Enabled = false;
            }
            else
            {
                ddlcomp.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcomp.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                ddldivision.Items.Insert(0, "Select");
                divfilterdivision.Visible = true;
            }
            else
            {
                ddldivision.Items.Insert(0, "Select");
                ddlunit.Items.Insert(0, "Select");
            }
        }
        else if (Encrypt.DecryptData(Session["Type"].ToString()) == "Factory" || Encrypt.DecryptData(Session["Type"].ToString()) == "Division")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, hfmref.Value, "Company1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcomp, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcomp.Enabled = false;
            }
            else
            {
                ddlcomp.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcomp.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                // code by gk to select indivisual division for the particular unit
                DataTable dt = Lo.RetriveMasterData(0, hfmref.Value, "Factory2", 0, "", "", "CompanyName");
                if (dt.Rows.Count > 0)
                    ddldivision.SelectedValue = dt.Rows[0]["FactoryRefNo"].ToString();
                //end code
                ddldivision.Enabled = false;
                divfilterdivision.Visible = true;
            }
            else
            {
                divfilterdivision.Visible = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "Unit1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
                ddlunit.Items.Insert(0, "Select");
                ddlunit.Enabled = true;
                divfilterunit.Visible = true;
            }
            else
            {
                divfilterunit.Visible = false;
            }
        }
        else if (Encrypt.DecryptData(Session["Type"].ToString()) == "Unit")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, hfmref.Value, "Company2", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcomp, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcomp.Enabled = false;
            }
            else
            {
                ddlcomp.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcomp.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                // code by gk to select indivisual division for the particular unit
                DataTable dt = Lo.RetriveMasterData(0, hfmref.Value, "Factory3", 0, "", "", "CompanyName");
                if (dt.Rows.Count > 0)
                    ddldivision.SelectedValue = dt.Rows[0]["FactoryRefNo"].ToString();
                //end code
                ddldivision.Enabled = false;
                divfilterdivision.Visible = true;
            }
            else
            {
                ddldivision.Enabled = false;
                divfilterdivision.Visible = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "Unit1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
                // code by gk to select indivisual unit for the particular unit     
                ddlunit.SelectedValue = hfmref.Value.ToString();
                //end code
                ddlunit.Enabled = false;
                divfilterunit.Visible = true;
            }
            else
            {
                ddlunit.Enabled = false;
                divfilterunit.Visible = false;
            }
        }
    }
    protected void BindComapnyCompanyCheckbox()
    {
        try
        {
            DataTable dtFactory = Lo.GetDashboardData("CompanyByname", ddlcomp.SelectedItem.Value);
            if (dtFactory.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, dtFactory, "FactoryName", "FactoryRefNo");
                ddldivision.Items.Insert(0, "Select");
                divfilterdivision.Visible = true;
            }
        }
        catch (Exception ex)
        { }
    }
    protected void BindComapnyDivisionCheckbox()
    {
        try
        {
            DataTable dtFactory = Lo.GetDashboardData("DivisionByname", ddlcomp.SelectedItem.Value);
            if (dtFactory.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, dtFactory, "FactoryName", "FactoryRefNo");
                ddldivision.Items.Insert(0, "Select");
                divfilterdivision.Visible = true;
            }
            else
            {
                ddldivision.Items.Insert(0, "Select");
                divfilterdivision.Visible = true;
            }
        }
        catch (Exception ex)
        { }
    }
    protected void BindComapnyDivisionUnitCheckbox()
    {
        try
        {
            DataTable dtUnit = Lo.GetDashboardData("UnitByname", ddldivision.SelectedItem.Value);
            if (dtUnit.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlunit, dtUnit, "UnitName", "UnitRefNo");
                ddlunit.Items.Insert(0, "Select");
                divfilterunit.Visible = true;
            }
        }
        catch (Exception ex)
        { }
    }
    protected void BindIndusrtyDomain()
    {
        try
        {
            DataTable DtMasterCategroy = new DataTable();
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "", "", "DefPlatIndus", "", "");
            if (DtMasterCategroy.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlprodindustrydomain, DtMasterCategroy, "SCategoryName", "SCategoryID");
                ddlprodindustrydomain.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        { }
    }
    protected void BindSearchKeyword()
    {
        DataTable dtsearchkey = Lo.RetriveFilterCode(hfmref.Value, "", "SearchKeyword");
        if (dtsearchkey.Rows.Count > 0)
        {
            //DataTable insert = new DataTable();
            //insert.Columns.Add(new DataColumn("Column1", typeof(string)));
            //DataRow dr;
            //for (int i = 0; dtsearchkey.Rows.Count > i; i++)
            //{
            //    if (dtsearchkey.Rows[i]["SearchKeyword"].ToString().Contains(","))
            //    {
            //        string mystring = dtsearchkey.Rows[i]["SearchKeyword"].ToString();
            //        string[] finalstring = mystring.Split(',');
            //        dr = insert.NewRow();
            //        dr["Column1"] = "" + finalstring[0].Trim() + "";
            //        insert.Rows.Add(dr);
            //        dr = insert.NewRow();
            //        dr["Column1"] = "" + finalstring[1].Trim() + "";
            //        insert.Rows.Add(dr);
            //        dtsearchkey.Rows[i].Delete();
            //        dtsearchkey.AcceptChanges();
            //    }
            //}
            //dtsearchkey.Merge(insert);
            ////  Co.FillDropdownlist(ddlsearchkeywordsfilter, dtsearchkey, "SearchKeyword", "SearchKeyword");
            //// ddlsearchkeywordsfilter.Items.Insert(0, "Select");
        }
    }
    protected void BindPurposeProcuremnt()
    {
        DataTable DtPurposeProcuremnt = new DataTable();
        if (ddlcomp.SelectedItem.Text != "Select")
        {
            DtPurposeProcuremnt = Lo.RetriveMasterSubCategoryDate(0, "PROCURMENT CATEGORY", "", "SelectProductCat", ddlcomp.SelectedItem.Value, "");
        }
        else
        {
            DtPurposeProcuremnt = Lo.RetriveMasterSubCategoryDate(0, "PROCURMENT CATEGORY", "", "SelectProductCat", "", "");
        }
        if (DtPurposeProcuremnt.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlprocurmentcatgory, DtPurposeProcuremnt, "ScategoryName", "SCategoryId");
            ddlprocurmentcatgory.Items.Insert(0, "Select");
        }
        else
        {
            DtPurposeProcuremnt = Lo.RetriveMasterSubCategoryDate(0, "PROCURMENT CATEGORY", "", "SelectProductCat", "", "");
            Co.FillDropdownlist(ddlprocurmentcatgory, DtPurposeProcuremnt, "ScategoryName", "SCategoryId");
            ddlprocurmentcatgory.Items.Insert(0, "Select");
        }
    }
    protected void BindNSG()
    {
        DataTable DtMasterCategroy = new DataTable();
        if (ddlcomp.SelectedItem.Text != "Select")
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "NSN GROUP", "", "SelectProductCat", ddlcomp.SelectedItem.Value, "");
        }
        else
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "NSN GROUP", "", "SelectProductCat", "", "");
        }
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlnsg, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlnsg.Items.Insert(0, "Select");
        }
        else
        {
            ddlnsg.Visible = false;
            divnsc.Visible = false;
            divic.Visible = false;
        }
    }
    protected void BindNSC()
    {
        DataTable DtMasterCategroy = new DataTable();
        if (ddlnsg.SelectedItem.Text != "Select")
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlnsg.SelectedItem.Value), "", "", "SubSelectID", "", "");
        }
        else
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "", "", "SubSelectSec", "", "");
        }
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlnsc, DtMasterCategroy, "SCategoryName", "SCategoryId");
            ddlnsc.Items.Insert(0, "Select");
            divnsc.Visible = true;
        }
        else
        {
            divic.Visible = false;
            divnsc.Visible = false;
        }
    }
    protected void BindIC()
    {
        DataTable DtMasterCategroyLevel3 = new DataTable();
        if (ddlnsc.SelectedIndex != -1)
        { DtMasterCategroyLevel3 = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlnsc.SelectedItem.Value), "", "", "SubSelectID", "", ""); }
        else
        { DtMasterCategroyLevel3 = Lo.RetriveMasterSubCategoryDate(0, "", "", "SubSelectthr", "", ""); }
        if (DtMasterCategroyLevel3.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlic, DtMasterCategroyLevel3, "SCategoryName", "SCategoryId");
            ddlic.Items.Insert(0, "Select");
            divic.Visible = true;
        }
        else
        {
            ddlic.Items.Insert(0, "Select");
            ddlic.Items.Insert(1, "NA");
        }
    }
    #endregion
    #region Filter CheckBox Code
    protected void ddlisindezinized_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    protected void ddlsearchkeywordsfilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    protected void ddlcomp_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindComapnyDivisionCheckbox();
        SeachResult();
        DataTable dtcompindustry = (DataTable)Session["TempData"];
        if (dtcompindustry.Rows.Count > 0)
        {
            //Bind Industry Domain
            DataView dvcompind = new DataView(dtcompindustry);
            dvcompind.RowFilter = "CompanyRefNo='" + ddlcomp.SelectedItem.Value + "' and ProdIndustryDoamin is not null";
            DataTable dtnew1 = dvcompind.ToTable();
            string[] strColnames = new string[2];
            strColnames[0] = "ProdIndustryDoamin";
            strColnames[1] = "TechnologyLevel1";
            dtnew1 = dvcompind.ToTable(true, strColnames);
            ddlprodindustrydomain.DataTextField = "ProdIndustryDoamin";
            ddlprodindustrydomain.DataValueField = "TechnologyLevel1";
            ddlprodindustrydomain.DataSource = dtnew1;
            ddlprodindustrydomain.DataBind();
            ddlprodindustrydomain.Items.Insert(0, "Select");
            //Bind SearchKeyword
            DataView dvcompind1 = new DataView(dtcompindustry);
            dvcompind1.RowFilter = "CompanyRefNo='" + ddlcomp.SelectedItem.Value + "' and SearchKeyword is not null";
            DataTable dtnew2 = dvcompind1.ToTable();
            string[] strColnames1 = new string[2];
            strColnames1[0] = "SearchKeyword";
            strColnames1[1] = "CompanyRefNo";
            dtnew2 = dvcompind1.ToTable(true, strColnames1);
            //ddlsearchkeywordsfilter.DataTextField = "SearchKeyword";
            //ddlsearchkeywordsfilter.DataValueField = "CompanyRefNo";
            //ddlsearchkeywordsfilter.DataSource = dtnew2;
            //ddlsearchkeywordsfilter.DataBind();
            //ddlsearchkeywordsfilter.Items.Insert(0, "Select");
        }
    }
    protected void ddldivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindComapnyDivisionUnitCheckbox();
        SeachResult();
        DataTable dtcompindustry = (DataTable)Session["TempData"];
        if (dtcompindustry.Rows.Count > 0)
        {
            //Bind Industry Domain
            DataView dvcompind = new DataView(dtcompindustry);
            dvcompind.RowFilter = "FactoryRefNo='" + ddldivision.SelectedItem.Value + "' and ProdIndustryDoamin is not null";
            DataTable dtnew1 = dvcompind.ToTable();
            string[] strColnames = new string[2];
            strColnames[0] = "ProdIndustryDoamin";
            strColnames[1] = "TechnologyLevel1";
            dtnew1 = dvcompind.ToTable(true, strColnames);
            ddlprodindustrydomain.DataTextField = "ProdIndustryDoamin";
            ddlprodindustrydomain.DataValueField = "TechnologyLevel1";
            ddlprodindustrydomain.DataSource = dtnew1;
            ddlprodindustrydomain.DataBind();
            ddlprodindustrydomain.Items.Insert(0, "Select");
            //Bind SearchKeyword
            DataView dvcompind1 = new DataView(dtcompindustry);
            dvcompind1.RowFilter = "FactoryRefNo='" + ddldivision.SelectedItem.Value + "' and SearchKeyword is not null";
            DataTable dtnew2 = dvcompind1.ToTable();
            string[] strColnames1 = new string[2];
            strColnames1[0] = "SearchKeyword";
            strColnames1[1] = "FactoryRefNo";
            dtnew2 = dvcompind1.ToTable(true, strColnames1);
            //ddlsearchkeywordsfilter.DataTextField = "SearchKeyword";
            //ddlsearchkeywordsfilter.DataValueField = "FactoryRefNo";
            //ddlsearchkeywordsfilter.DataSource = dtnew2;
            //ddlsearchkeywordsfilter.DataBind();
            //ddlsearchkeywordsfilter.Items.Insert(0, "Select");
        }
    }
    protected void ddlunit_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
        DataTable dtcompindustry = (DataTable)Session["TempData"];
        if (dtcompindustry.Rows.Count > 0)
        {
            //Bind Industry Domain
            DataView dvcompind = new DataView(dtcompindustry);
            dvcompind.RowFilter = "UnitRefNo='" + ddlunit.SelectedItem.Value + "' and ProdIndustryDoamin is not null";
            DataTable dtnew1 = dvcompind.ToTable();
            string[] strColnames = new string[2];
            strColnames[0] = "ProdIndustryDoamin";
            strColnames[1] = "TechnologyLevel1";
            dtnew1 = dvcompind.ToTable(true, strColnames);
            ddlprodindustrydomain.DataTextField = "ProdIndustryDoamin";
            ddlprodindustrydomain.DataValueField = "TechnologyLevel1";
            ddlprodindustrydomain.DataSource = dtnew1;
            ddlprodindustrydomain.DataBind();
            ddlprodindustrydomain.Items.Insert(0, "Select");
            //Bind SearchKeyword
            DataView dvcompind1 = new DataView(dtcompindustry);
            dvcompind1.RowFilter = "UnitRefNo='" + ddlunit.SelectedItem.Value + "' and SearchKeyword is not null";
            DataTable dtnew2 = dvcompind1.ToTable();
            string[] strColnames1 = new string[2];
            strColnames1[0] = "SearchKeyword";
            strColnames1[1] = "UnitRefNo";
            dtnew2 = dvcompind1.ToTable(true, strColnames1);
            //ddlsearchkeywordsfilter.DataTextField = "SearchKeyword";
            //ddlsearchkeywordsfilter.DataValueField = "UnitRefNo";
            //ddlsearchkeywordsfilter.DataSource = dtnew2;
            //ddlsearchkeywordsfilter.DataBind();
            //ddlsearchkeywordsfilter.Items.Insert(0, "Select");
        }
    }
    protected void ddlnameofdefplat_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    protected void ddlprodindustrydomain_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    protected void ddlprodindussubdomain_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    #endregion
    #region Search Code Filter Code
    protected void UpdateDtGridValue()
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
    string insert1 = "";
    protected string Dvinsert()
    {
        DataTable insert = new DataTable();
        insert.Columns.Add(new DataColumn("Column", typeof(string)));
        insert.Columns.Add(new DataColumn("Value", typeof(string)));
        DataRow dr;
        if (ddlcomp.SelectedItem.Text != "Select")
        {
            dr = insert.NewRow();
            dr["Column"] = "CompanyRefNo" + "=";
            dr["Value"] = "'" + ddlcomp.SelectedItem.Value + "'";
            insert.Rows.Add(dr);
        }
        if (ddlnsg.SelectedItem.Text != "Select")
        {
            dr = insert.NewRow();
            dr["Column"] = "ProductLevel1" + "=";
            dr["Value"] = "'" + ddlnsg.SelectedItem.Value + "'";
            insert.Rows.Add(dr);
            if (divnsc.Visible != false)
            {
                if (ddlnsc.SelectedItem.Text != "Select")
                {
                    dr = insert.NewRow();
                    dr["Column"] = "ProductLevel2" + "=";
                    dr["Value"] = "'" + ddlnsc.SelectedItem.Value + "'";
                    insert.Rows.Add(dr);
                }
            }
            if (divic.Visible != false)
            {
                if (ddlic.SelectedItem.Text != "Select")
                {
                    dr = insert.NewRow();
                    dr["Column"] = "ProductLevel3" + "=";
                    dr["Value"] = "'" + ddlic.SelectedItem.Value + "'";
                    insert.Rows.Add(dr);
                }
            }
        }
        if (ddlprodindustrydomain.SelectedItem.Text != "Select")
        {
            dr = insert.NewRow();
            dr["Column"] = "TechnologyLevel1" + "="; ;
            dr["Value"] = "'" + ddlprodindustrydomain.SelectedItem.Value + "'";
            insert.Rows.Add(dr);
        }
        if (ddlsearchkeywordsfilter.Text != "Select" && ddlsearchkeywordsfilter.Text.Length >= 3)
        {
            dr = insert.NewRow();
            dr["Column"] = "SearchKeyword" + " like";
            dr["Value"] = "'%" + ddlsearchkeywordsfilter.Text + "%'";
            insert.Rows.Add(dr);
        }
        if (ddlimported.SelectedItem.Text != "Select")
        {
            dr = insert.NewRow();
            dr["Column"] = "IsProductImported" + "="; ;
            dr["Value"] = "'" + ddlimported.SelectedItem.Value + "'";
            insert.Rows.Add(dr);
        }
        if (ddlprocurmentcatgory.SelectedItem.Text != "Select")
        {
            dr = insert.NewRow();
            dr["Column"] = "PurposeofProcurement" + " like";
            dr["Value"] = "'%" + ddlprocurmentcatgory.SelectedItem.Value + "%'";
            insert.Rows.Add(dr);
        }
        if (ddlisindezinized.SelectedItem.Text != "Select")
        {
            dr = insert.NewRow();
            dr["Column"] = "IsIndeginized" + "="; ;
            dr["Value"] = "'" + ddlisindezinized.SelectedItem.Value + "'";
            insert.Rows.Add(dr);
        }
        if (ddldeclaration.SelectedItem.Text != "Select")
        {
            dr = insert.NewRow();
            dr["Column"] = "IsShowGeneral" + "="; ;
            dr["Value"] = "'" + ddldeclaration.SelectedItem.Value + "'";
            insert.Rows.Add(dr);
        }
        if (hfmtype.Value == "PI")
        {
            dr = insert.NewRow();
            dr["Column"] = "IsIndeginized" + "=";
            dr["Value"] = "'Y'";
            insert.Rows.Add(dr);
        }
        if (hfmtype.Value == "M2")
        {
            dr = insert.NewRow();
            dr["Column"] = "PurposeofProcurement" + " like";
            dr["Value"] = "'%" + 25 + "%'";
            insert.Rows.Add(dr);
        }
        if (hfmtype.Value == "ID")
        {
            dr = insert.NewRow();
            dr["Column"] = "IsApproved" + "=";
            dr["Value"] = "'N'";
            insert.Rows.Add(dr);
        }
        if (hfmtype.Value == "IA")
        {
            dr = insert.NewRow();
            dr["Column"] = "IsApproved" + " =";
            dr["Value"] = "'Y'";
            insert.Rows.Add(dr);
        }
        if (hfmref.Value != "")
        {
            dr = insert.NewRow();
            dr["Column"] = "CompanyRefNo" + "=";
            dr["Value"] = "'" + hfmref.Value + "'";
            insert.Rows.Add(dr);
        }
        if (txtitemportalid.Text != "")
        {
            dr = insert.NewRow();
            dr["Column"] = "ProductRefNo" + "=";
            dr["Value"] = "'" + txtitemportalid.Text + "'";
            insert.Rows.Add(dr);
        }
        for (int i = 0; insert.Rows.Count > i; i++)
        {
            insert1 = insert1 + insert.Rows[i]["Column"].ToString() + " " + insert.Rows[i]["Value"].ToString() + " " + " and ";
        }
        insert1 = insert1.Substring(0, insert1.Length - 5);
        return insert1;
    }
    protected string BindInsertfilter()
    {
        return Dvinsert();
    }
    protected void SeachResult(string sortExpression = null)
    {
        try
        {
            DtFilterView = (DataTable)Session["TempData"];
            if (DtFilterView.Rows.Count > 0)
            {
                UpdateDtGridValue();
                DataView dv = new DataView(DtFilterView);
                DataTable dtnew = dv.ToTable();
                if (dtnew.Rows.Count > 0)
                {
                    dv.RowFilter = BindInsertfilter();
                    dv.Sort = "LastUpdated desc";
                    DataTable dtads = dv.ToTable();
                    if (dtads.Rows.Count > 0)
                    {
                        dtads.Columns.Add("TopPdf", typeof(string));
                        dtads.Columns.Add("TopImages", typeof(string));
                        dtads.Columns.Add("1718", typeof(decimal));
                        dtads.Columns.Add("1819", typeof(decimal));
                        dtads.Columns.Add("1920", typeof(decimal));
                        dtads.Columns.Add("2021", typeof(decimal));
                        for (int i = 0; dtads.Rows.Count > i; i++)
                        {
                            try
                            {
                                string mProdRefTime = dtads.Rows[i]["ProductRefNo"].ToString();
                                DataTable dtImageBind = Lo.RetriveProductCode("", mProdRefTime, "RetImageTop", "");
                                if (dtImageBind.Rows.Count > 0)
                                {
                                    dtads.Rows[i]["TopImages"] = dtImageBind.Rows[0]["ImageName"].ToString();
                                }
                                else
                                {
                                    // dtads.Rows[i]["TopImages"] = "assets/images/Noimage.png";
                                }
                                DataTable dtImageBind0 = Lo.RetriveProductCode("", mProdRefTime, "RetPdfTop", "");
                                if (dtImageBind0.Rows.Count > 0)
                                {
                                    dtads.Rows[i]["TopPdf"] = dtImageBind0.Rows[0]["ImageName"].ToString();
                                }
                                else
                                {
                                    // dtads.Rows[i]["TopImages"] = "assets/images/Noimage.png";
                                }

                                string mProdRefesti = dtads.Rows[i]["ProductRefNo"].ToString();
                                DataTable dtEstimate1 = Lo.RetriveProductCode("", mProdRefesti, "estimate", "");
                                for (int es = 0; dtEstimate1.Rows.Count > es; es++)
                                {
                                    if (dtEstimate1.Rows[es]["FYear"].ToString() == "2017-18" && dtEstimate1.Rows[es]["Type"].ToString() == "O")
                                    {
                                        dtads.Rows[i]["1718"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                                    }
                                    if (dtEstimate1.Rows[es]["FYear"].ToString() == "2018-19" && dtEstimate1.Rows[es]["Type"].ToString() == "O")
                                    {
                                        dtads.Rows[i]["1819"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                                    }
                                    if (dtEstimate1.Rows[es]["FYear"].ToString() == "2019-20" && dtEstimate1.Rows[es]["Type"].ToString() == "O")
                                    {
                                        dtads.Rows[i]["1920"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                                    }
                                    if (dtEstimate1.Rows[es]["FYear"].ToString() == "2020-21" && dtEstimate1.Rows[es]["Type"].ToString() == "F")
                                    {
                                        dtads.Rows[i]["2021"] = dtEstimate1.Rows[es]["EstimatedPrice"].ToString();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                        pgsource.DataSource = dtads.DefaultView;
                        pgsource.AllowPaging = true;
                        pgsource.PageSize = 100;
                        pgsource.CurrentPageIndex = pagingCurrentPage;
                        ViewState["totpage"] = pgsource.PageCount;
                        lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                        lblcountpgindex.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                        lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                        lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                        lnkbtnPgPre.Enabled = !pgsource.IsFirstPage;
                        lnkbtnne.Enabled = !pgsource.IsLastPage;
                        if (sortExpression != null)
                        {
                            DataView _DvSort = dtads.DefaultView;
                            if (_DvSort != null && _DvSort.Count > 0)
                            {
                                this.SortDirection = this.SortDirection == "ASC" ? "DESC" : "ASC";
                                _DvSort.Sort = sortExpression + " " + this.SortDirection;
                                pgsource.DataSource = _DvSort;
                            }
                            else
                            {
                                pgsource.DataSource = dtads.DefaultView;
                            }
                        }
                        else
                        {
                            pgsource.DataSource = dtads.DefaultView;
                        }
                        gvproduct.DataSource = pgsource;
                        gvproduct.DataBind();
                        lbltotal.Text = "Total Result " + dtads.Rows.Count.ToString();
                        gvproduct.Visible = true;
                        divpageindex.Visible = true;
                        divProductGrid.Visible = true;
                        divpageindex1.Visible = true;
                        FilterSelectName();
                        dtads = null;
                    }
                    else
                    {
                        FilterSelectName();
                        divpageindex.Visible = false;
                        divpageindex1.Visible = false;
                        gvproduct.Visible = false;
                        lbldownloadexcel.Visible = false;
                        lbltotal.Text = "No Record Found";
                    }
                }
                else
                {
                    FilterSelectName();
                    divpageindex.Visible = false;
                    divpageindex1.Visible = false;
                    gvproduct.Visible = false;
                    lbldownloadexcel.Visible = false;
                    lbltotal.Text = "No Record Found";
                }
            }
            else
            {
                FilterSelectName();
                gvproduct.Visible = false;
                divpageindex.Visible = false;
                divpageindex1.Visible = false;
                lbldownloadexcel.Visible = false;
                lbltotal.Text = "No Record Found";
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void lbclearfilter_Click(object sender, EventArgs e)
    {
        ddlcomp.SelectedValue = "Select";
        ddldivision.SelectedValue = "Select";
        ddlunit.SelectedValue = "Select";
        ddlprodindustrydomain.SelectedValue = "Select";
        // ddlsearchkeywordsfilter.SelectedValue = "Select";
        ddlsearchkeywordsfilter.Text = "";
        ddlimported.SelectedIndex = -1;
        ddlprocurmentcatgory.SelectedValue = "Select";
        ddlisindezinized.SelectedIndex = -1;
        ddldeclaration.SelectedIndex = -1;
        divfilterdivision.Visible = false;
        divfilterunit.Visible = false;
        lbclearfilter.Visible = false;
        FilterSelectName();
        BindProduct(hfmref.Value);
    }
    #endregion
    protected void lbldownloadexcel_Click(object sender, EventArgs e)
    {
        DtFilterView = (DataTable)Session["TempData"];
        if (DtFilterView.Rows.Count > 0)
        {
            UpdateDtGridValue();
            DataView dv = new DataView(DtFilterView);
            DataTable dtnew = dv.ToTable();
            if (dtnew.Rows.Count > 0)
            {
                try
                {
                    dv.RowFilter = BindInsertfilter();
                    dv.Sort = "LastUpdated desc";
                    DtFilterView = dv.ToTable();
                    if (DtFilterView.Rows.Count > 0)
                    {
                        DtFilterView.Columns.Add("EstimateQu", typeof(int));
                        DtFilterView.Columns.Add("EstimatePrice", typeof(int));
                        for (int i = 0; DtFilterView.Rows.Count > i; i++)
                        {
                            try
                            {
                                string mProdRefesti = DtFilterView.Rows[i]["ProductRefNo"].ToString();
                                DataTable dtEstimate = Lo.RetriveProductCode("", mProdRefesti, "EstimateQuanTotal", "");
                                if (dtEstimate.Rows[0]["EstQe"].ToString() == "" || dtEstimate.Rows[0]["EstQe"].ToString() == null)
                                {
                                    DtFilterView.Rows[i]["EstimateQu"] = 0;
                                }
                                else
                                {
                                    DtFilterView.Rows[i]["EstimateQu"] = dtEstimate.Rows[0]["EstQe"].ToString();
                                }
                                if (dtEstimate.Rows[0]["estpri"].ToString() == "")
                                {
                                    DtFilterView.Rows[i]["EstimatePrice"] = 0;
                                }
                                else
                                {
                                    DtFilterView.Rows[i]["EstimatePrice"] = dtEstimate.Rows[0]["estpri"].ToString();
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
        }
        try
        {
            //int[] iColumns = { 2, 4, 6, 7, 9, 11, 18, 19, 20, 21, 22, 24, 25, 57, 60, 58, 59, 62, 61 };
            int[] iColumns = { 2, 4, 7, 18, 20, 24, 25, 60, 58, 61, 64, 66, 67 };
            RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
            objExport.ExportDetails(DtFilterView, iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "Product.xls");
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
    #region Clear Filter Select Result
    protected void FilterSelectName()
    {
        lbclearfilter.Visible = true;
        lbldownloadexcel.Visible = true;
        divclearfilorexceldown.Visible = true;
        hidemainclearfilter();
    }
    protected void hidemainclearfilter()
    {
        if (ddlcomp.SelectedItem.Text == "Select" && ddlisindezinized.SelectedItem.Text == "Select" && ddlprocurmentcatgory.SelectedItem.Text == "Select" && ddlprodindustrydomain.SelectedItem.Text == "Select" && ddlimported.SelectedItem.Text == "Select")
        {
            lbclearfilter.Visible = false;
        }
    }
    #endregion
    protected void gvproditemdetail_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TableSection = TableRowSection.TableBody;
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.TableSection = TableRowSection.TableFooter;
        }
    }
    protected void ddlprocurmentcatgory_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    protected void ddldeclaration_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    protected void ddlimported_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    private string SortDirection
    {
        get { return ViewState["SortDirection"] != null ? ViewState["SortDirection"].ToString() : "ASC"; }
        set { ViewState["SortDirection"] = value; }
    }
    protected void OnSorting(object sender, GridViewSortEventArgs e)
    {
        if (ddlcomp.SelectedItem.Text == "Select" && ddlsearchkeywordsfilter.Text == "Select" && ddldeclaration.SelectedItem.Text == "Select" && ddlisindezinized.SelectedItem.Text == "Select" && ddlprocurmentcatgory.SelectedItem.Text == "Select" && ddlprodindustrydomain.SelectedItem.Text == "Select")
        {
            this.BindProduct(hfmref.Value, e.SortExpression);
        }
        else
        {
            SeachResult(e.SortExpression);
        }
    }
    protected void ddlsearchkeywordsfilter_TextChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    protected void ddlnsg_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlnsg.SelectedItem.Text == "Select")
        {
            divnsc.Visible = false;
            ddlnsc.Items.Insert(0, "Select");
            divic.Visible = false;
            ddlic.Items.Insert(0, "Select");
        }
        else
        {
            BindNSC();
        }
        if (ddlnsg.SelectedItem.Text != "Select")
        {
            SeachResult();
        }
    }
    protected void ddlnsc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlnsc.SelectedItem.Text == "Select")
        {
            divic.Visible = false;
            ddlic.Items.Insert(0, "Select");
        }
        else
        {
            BindIC();
        }
        if (ddlnsc.SelectedItem.Text != "Select")
        {
            SeachResult();
        }
    }
    protected void ddlic_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlic.SelectedItem.Text == "Select")
        { }
        else
        {
            SeachResult();
        }
    }
    protected void txtitemportalid_TextChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
}