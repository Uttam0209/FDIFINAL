using BusinessLayer;
using Encryption;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_S_Story2 : System.Web.UI.Page
{
    #region Pagevariable
    private Logic Lo = new Logic();
    private DataTable DtGrid = new DataTable();
    private string _msg = string.Empty;
    private string _sysMsg = string.Empty;
    DataUtility Co = new DataUtility();
    private Cryptography Encrypt = new Cryptography();
    DataTable DtFilterView = new DataTable();
    private PagedDataSource pgsource = new PagedDataSource();
    string isindegnizeds = "";
    private Cryptography objEnc = new Cryptography();

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Type"] != null || Session["User"] != null)
        {
            if (!IsPostBack)
            {
                if (Session["User"] != null)
                {
                    try
                    {

                        hidType.Value = objEnc.DecryptData(Session["Type"].ToString());
                        mRefNo.Value = Session["CompanyRefNo"].ToString();
                        BindTopBoxRecords();
                        BindData();
                        BindCompany();
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
                    Response.RedirectToRoute("login");
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "ErrorMssgPopup('Session Expired,Please login again');window.location='Login'", true);
        }
    }
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
    protected void BindTopBoxRecords()
    {
        DataTable dtprdetails = new DataTable();
        dtprdetails = Lo.RetriveFilterCode(Session["CompanyRefNo"].ToString(), Encrypt.DecryptData(Session["Type"].ToString()), "GetCount");
        lblTarget.Text = dtprdetails.Rows[0]["TargetYear"].ToString();
        lblIndiG.Text = dtprdetails.Rows[0]["IndiginizationYear"].ToString();
        lblcat.Text = dtprdetails.Rows[0]["MakeInIndiaCategory"].ToString();
        lblNM.Text = dtprdetails.Rows[0]["ManufactureName"].ToString();
        lbladd.Text = dtprdetails.Rows[0]["ManufactureAddress"].ToString();
    }
    protected void BindData()
    {
        DtGrid = Lo.RetriveFilterCodeupdate("", "", "", "", "SStoryData");
        if (DtGrid.Rows.Count > 0)
        {
            UpdateDtGridValue();
            Session["PDatatTable"] = DtGrid;
            SeachResult();
        }
        else
        {
            divcontentproduct.Visible = false;
            gveoi.DataBind();
        }
    }
    protected void gveoi_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void lblmis_Click(object sender, EventArgs e)
    {

    }
    protected void linklogin_Click(object sender, EventArgs e)
    {
        Response.RedirectToRoute("Login");
    }
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
    protected void gveoi_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)

        {
            Label lbdivision = (Label)e.Row.FindControl("lbldivsion");
            Label lbunit = (Label)e.Row.FindControl("lblunit");
            Label lbindiacategory = (Label)e.Row.FindControl("lblindiacategory");
            Label indegyr = (Label)e.Row.FindControl("lblindegyr");
            Label manufname = (Label)e.Row.FindControl("lblmanufname");
            Label manufaddress = (Label)e.Row.FindControl("lblmanufaddress");
            Label targtyr = (Label)e.Row.FindControl("lbltargtyr");
            string lbcomp = e.Row.Cells[1].Text;
            if (lbindiacategory.Text.Trim() == "-1" || lbindiacategory.Text.Trim() == "")
            {
                lbindiacategory.Text = "NA";
            }
            if (lbunit.Text != "")
            {
                lbdivision.Visible = false;
                lbunit.Visible = true;
            }
            else
            {
                lbdivision.Visible = true;
                lbunit.Visible = true;
                if (lbdivision.Text == "")
                {
                    lbdivision.Text = lbcomp.ToString();
                }
            }
        }
    }
    protected void gveoi_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ed")
        {
            try
            {
                GridViewRow item = (GridViewRow)(((Control)(e.CommandSource)).NamingContainer);
                HiddenField isindegnized = (HiddenField)item.FindControl("hfisindegized");
                HiddenField hfindproc = (HiddenField)item.FindControl("hfindproc");
                Label itemcode = (Label)item.FindControl("lblitemcode");
                HiddenField IndeginizedMaxValue = (HiddenField)item.FindControl("IndeginizedMaxValue");
                
                HiddenField IndeginizedDate = (HiddenField)item.FindControl("IndeginizedDate");
                
                lblprorefcode.Text = itemcode.Text.Trim();
                if (isindegnized.Value == "Y")
                {
                    rdblisindig.SelectedValue = isindegnized.Value.Trim();

                }
                else
                {
                    rdblisindig.SelectedValue = isindegnized.Value.Trim();

                }

                Label targetyr = (Label)item.FindControl("lbltargtyr");
                if (targetyr.Text != "")
                {
                    chkinditargetyear.SelectedValue = targetyr.Text.Trim();
                }
                BindFinancialYear();
                Label indigyear = (Label)item.FindControl("lblindegyr");
                if (indigyear.Text.ToUpper().Trim() != "NA" && indigyear.Text != "0")
                {
                    ddlyearofindiginization.SelectedIndex = ddlyearofindiginization.Items.IndexOf(ddlyearofindiginization.Items.FindByText(indigyear.Text));
                }
                PROCURMENTCATEGORYIndigenization();
                Label indiacategory = (Label)item.FindControl("lblindiacategory");
                HiddenField hfidproc = (HiddenField)item.FindControl("hfproc");
                try
                {
                    if (indiacategory.Text != "" && indiacategory.Text.ToUpper().Trim() != "NA")
                    {
                        rbIgCategory.SelectedValue = hfidproc.Value;
                    }
                }
                catch (Exception ex)
                { }

                if (indiacategory.Text != "" && indiacategory.Text.ToUpper().Trim() != "NA")
                    try
                    {
                        rbIgCategory.SelectedValue = hfidproc.Value;
                    }
                    catch (Exception ex)
                    { }
                txtmanufacturngname.Text = "";
                txtmanufacturngadress.Text = "";
                Label manufactrngname = (Label)item.FindControl("lblmanufname");
                txtmanufacturngname.Text = manufactrngname.Text.Trim();
                Label manufactringaddress = (Label)item.FindControl("lblmanufaddress");

                txtmanufacturngadress.Text = manufactringaddress.Text.Trim();
                if (IndeginizedDate.Value != "")
                {
                    txtinddate.Text = IndeginizedDate.Value;
                }
                else
                {
                    txtinddate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                }
                Label lblestimate = (Label)item.FindControl("lblestimate");
                if (lblestimate.Text != "")
                {
                    Txtnegated.Text = lblestimate.Text; 
                }
                else
                {
                    if (lblestimate.Text != "")
                    {
                        Txtnegated.Text = lblestimate.Text;
                    }
                    else
                    { Txtnegated.Text = "0"; }
                }

                Label lblapprox = (Label)item.FindControl("lblapprox");
                if (lblapprox.Text != "")
                {
                    TxtApproxvalue.Text = lblapprox.Text;
                }
                else
                {
                    if (lblapprox.Text != "")
                    {
                        TxtApproxvalue.Text = lblapprox.Text;
                    }
                    else
                    {
                        TxtApproxvalue.Text = "0";
                    }
                }

                //TxtApproxvalue.Text = Approxvalue.Text.Trim();


                ScriptManager.RegisterStartupScript(this, GetType(), "divupdate", "showPopup2();", true);

            }
            catch (Exception ex)
            { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(" + ex.Message + ")", true); }
        }
    }
    protected void BindFinancialYear()
    {
        DataTable MasterFinancialYear = Lo.RetriveMasterSubCategoryDate(0, "", "", "AllFinancialYear", "", "");
        if (MasterFinancialYear.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlyearofindiginization, MasterFinancialYear, "FY", "FYID");
            ddlyearofindiginization.Items.Insert(0, "Select");
        }
    }
    Int32 mddlyr = 0;
    string makecat = "";
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (rdblindegprocess.SelectedIndex != -1 && chkinditargetyear.SelectedIndex != -1 && rdblisindig.SelectedIndex != -1)
            {
                if (ddlyearofindiginization.SelectedItem.Text == "Select")
                {
                    mddlyr = 0;
                }
                else
                {
                    mddlyr = Convert.ToInt32(ddlyearofindiginization.SelectedItem.Value);
                }
                if (rbIgCategory.SelectedIndex != -1)
                {
                    makecat = rbIgCategory.SelectedValue;
                }
                else
                { makecat = ""; }
                try
                {
                    string manufname = txtmanufacturngname.Text.Trim();
                    string manufadres = txtmanufacturngadress.Text.Trim();
                    DateTime date = Convert.ToDateTime(txtinddate.Text);
                    string Approxvalue = TxtApproxvalue.Text.Trim();
                    string mdate = date.ToString("dd-MMM-yyyy");
                    Lo.updateSucessStory(lblprorefcode.Text, rdblindegprocess.SelectedItem.Value, mddlyr, rdblisindig.SelectedItem.Value,
                        chkinditargetyear.SelectedValue, makecat.ToString(), manufname.Replace(",", ""), manufadres.Replace(",", ""),
                        Convert.ToDecimal(TxtApproxvalue.Text), Convert.ToDateTime(mdate));
                    SuccessStoryLog();
                }
                catch (Exception ex)
                {

                }
                success.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully!!!');window.location ='SuccessStoryupdate';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select indegenization process started value!!!');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(" + ex.Message + ")", true);
        }
    }
    #region Success Story Update Log
    protected void SuccessStoryLog()
    {
        try
        {
            DateTime Date = Convert.ToDateTime(DateTime.Now.ToString());
            string mDate = Date.ToString("dd-MM-yyyy");
            DateTime Time = Convert.ToDateTime(DateTime.Now.ToString());
            string mTime = Time.ToString("hh:mm:ss");
            Lo.saveSuccessStoryLog(lblprorefcode.Text.Trim(), Session["CompanyRefNo"].ToString(), mDate, mTime);
        }
        catch (Exception ex)
        { }
    }
    #endregion
    protected void rdblisindig_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (rdblisindig.SelectedItem.Value == "Y")
        {
            txtmanufacturngname.Enabled = true;
            txtmanufacturngadress.Enabled = true;
            ddlyearofindiginization.Enabled = true;
        }
        else
        {
            txtmanufacturngname.Enabled = false;
            txtmanufacturngadress.Enabled = false;
            ddlyearofindiginization.Enabled = false;
        }
    }
    protected void PROCURMENTCATEGORYIndigenization()
    {
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "PROCURMENT CATEGORY", "", "SelectProductCat", "", "");
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillRadioBoxList(rbIgCategory, DtMasterCategroy, "SCategoryName", "SCategoryID");
        }
    }
    /// Minakshi
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
    string insert1 = "";
    protected string Dvinsert(string sortExpression = null)
    {

        DataTable insert = new DataTable();
        insert.Columns.Add(new DataColumn("Column", typeof(string)));
        insert.Columns.Add(new DataColumn("Value", typeof(string)));
        DataRow dr;
        if (hidType.Value != "" && hidType.Value != "Admin" || hidType.Value != "SuperAdmin")
        {
            dr = insert.NewRow();
            if (hidType.Value.Contains("C"))
            {
                dr["Column"] = "CompanyRefNo" + "=";
                dr["Value"] = "'" + mRefNo.Value + "'";
                insert.Rows.Add(dr);
            }
            else if (hidType.Value.Contains("D"))
            {
                dr["Column"] = "FactoryRefno" + "=";
                dr["Value"] = "'" + mRefNo.Value + "'";
                insert.Rows.Add(dr);
            }
            else if (hidType.Value.Contains("U"))
            {
                dr["Column"] = "UnitRefno" + "=";
                dr["Value"] = "'" + mRefNo.Value + "'";
                insert.Rows.Add(dr);
            }
        }
        if (RdoYear.SelectedValue == "2020-21")
        {
            dr = insert.NewRow();
            dr["Column"] = "IndegenizationYear" + "=";
            dr["Value"] = "'" + RdoYear.SelectedItem.Value + "'";
            insert.Rows.Add(dr);
        }
        if (RdoYear.SelectedValue == "2021-22")
        {
            dr = insert.NewRow();
            dr["Column"] = "IndegenizationYear" + "=";
            dr["Value"] = "'" + RdoYear.SelectedItem.Value + "'";
            insert.Rows.Add(dr);
        }

        if (RdoYear.SelectedValue == "1")
        {
            dr = insert.NewRow();
            dr["Column"] = "IndegenizationYear" + "<";
            dr["Value"] = "'2020-21'";
            insert.Rows.Add(dr);
        }
        if (DDLMonth.SelectedItem.Text != "Select")
        {
            dr = insert.NewRow();
            dr["Column"] = "MonthSearch" + "=";
            dr["Value"] = "'" + DDLMonth.SelectedItem.Value + "'";
            insert.Rows.Add(dr);
        }
        if (txtsearch.Text.Trim() != "")
        {
            dr = insert.NewRow();
            dr["Column"] = "((ProductRefNo like";
            dr["Value"] = "'%" + txtsearch.Text.Trim() + "%') or (CompanyName like '%" + txtsearch.Text.Trim() + "%') or (FactoryName like '%" + txtsearch.Text.Trim() + "%') or (UnitName like '%" + txtsearch.Text.Trim() + "%') or (ManufactureName like '%" + txtsearch.Text.Trim() + "%') or (Itemname like '%" + txtsearch.Text.Trim() + "%') or (NSCCode like '%" + txtsearch.Text.Trim() + "%') or (IndTargetYear like '%" + txtsearch.Text.Trim() + "%') or (MakeInIndiaCategory like '%" + txtsearch.Text.Trim() + "%'))";
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
            DtFilterView = (DataTable)Session["PDatatTable"];
            if (DtFilterView.Rows.Count > 0)
            {
                DataView dv = new DataView(DtFilterView);
                DataTable dtnew = dv.ToTable();
                if (dtnew.Rows.Count > 0)
                {
                    dv.RowFilter = BindInsertfilter();
                    DataTable dtinner = dv.ToTable();
                    lbltotfilter.Text = dtinner.Rows.Count.ToString();
                    Session["ExcelDT"] = dtinner;
                    DataTable dtads = dv.ToTable();
                    if (dtads.Rows.Count > 0)
                    {
                        DataView _dv = new DataView(dtads);
                        if (dtads.Columns.Contains("row_no"))
                        {
                            int i = 1; foreach (DataRow r in dtads.Rows) r["row_no"] = i++;                            
                        }
                        else
                        {
                            dtads.Columns.Add("row_no");
                            int i = 1; foreach (DataRow r in dtads.Rows) r["row_no"] = i++;                           
                        }
                        pgsource.DataSource = dtinner.DefaultView;
                        pgsource.AllowPaging = true;
                        pgsource.PageSize = Convert.ToInt32(ddlPageSize.SelectedItem.Text);
                        pgsource.CurrentPageIndex = pagingCurrentPage;
                        lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                        lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                        lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                        pgsource.DataSource = dtads.DefaultView;
                        gveoi.DataSource = pgsource;
                        gveoi.DataBind();
                        gveoi.Visible = true;
                        lbltotalshowpageitem.Text = pgsource.FirstIndexInPage + 1 + " - " + (pgsource.FirstIndexInPage + pgsource.Count);
                        divcontentproduct.Visible = true;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
                        divcontentproduct.Visible = false;
                        gveoi.Visible = false;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
                }
            }
            else
            {
                gveoi.Visible = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        SeachResult();
    }
    protected void lbladd_Click(object sender, EventArgs e)
    {
        chktargetclick.Value = "";
        chkindigclick.Value = "";
        chkcategclick.Value = "";
        chknameclick.Value = "";
        chkaddressclick.Value = "Yes";
        pagingCurrentPage = 0;
        SeachResult();
    }
    protected void lblcat_Click(object sender, EventArgs e)
    {
        chktargetclick.Value = "";
        chkindigclick.Value = "";
        chkcategclick.Value = "Yes";
        chknameclick.Value = "";
        chkaddressclick.Value = "";
        pagingCurrentPage = 0;
        SeachResult();
    }
    protected void lblTarget_Click(object sender, EventArgs e)
    {
        chktargetclick.Value = "Yes";
        chkindigclick.Value = "";
        chkcategclick.Value = "";
        chknameclick.Value = "";
        chkaddressclick.Value = "";
        pagingCurrentPage = 0;
        SeachResult();
    }
    protected void lblNM_Click(object sender, EventArgs e)
    {
        chktargetclick.Value = "";
        chkindigclick.Value = "";
        chkcategclick.Value = "";
        chknameclick.Value = "Yes";
        chkaddressclick.Value = "";
        pagingCurrentPage = 0;
        SeachResult();
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable searchdt = (DataTable)Session["ExcelDT"];
            int[] iColumns = { 0, 3, 6, 18, 12, 28, 26, 20, 22, 14, 16 };
            RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
            objExport.ExportDetails(searchdt, iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "SuccessStoryList.xls");
        }
        catch (Exception ex)
        {
        }
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
                cmd.CommandText = "select distinct ProductRefNo from Vw_MasteRecord  where  IsActive='Y' AND IsIndeginized='Y' and ProductRefNo like @SearchText + '%' ";
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
                cmd.CommandText = "select distinct CompanyName from Vw_MasteRecord where IsActive='Y' AND IsIndeginized='Y' and CompanyName like @SearchText + '%' ";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["CompanyName"]));
                    }
                }
                cmd.CommandText = "select distinct FactoryName from Vw_MasteRecord where IsActive='Y' AND IsIndeginized='Y' and FactoryName like @SearchText + '%' ";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["FactoryName"]));
                    }
                }
                cmd.CommandText = "select distinct UnitName from Vw_MasteRecord where IsActive='Y' AND IsIndeginized='Y' and UnitName like @SearchText + '%' ";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["UnitName"]));
                    }
                }
                cmd.CommandText = "select distinct NSCCode from Vw_MasteRecord where IsActive='Y' AND IsIndeginized='Y' and NSCCode like @SearchText + '%' ";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["NSCCode"]));
                    }
                }
                cmd.CommandText = "select distinct IndTargetYear from Vw_MasteRecord  where IsActive='Y' AND IsIndeginized='Y' and IndTargetYear like @SearchText + '%' ";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["IndTargetYear"]));
                    }
                }
                cmd.CommandText = "select distinct ManufactureName from Vw_MasteRecord where IsActive='Y' AND IsIndeginized='Y' and ManufactureName like @SearchText + '%' ";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["ManufactureName"]));
                    }
                }
                cmd.CommandText = "select distinct Itemname from Vw_MasteRecord where IsActive='Y' AND IsIndeginized='Y' and Itemname like @SearchText + '%' ";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["Itemname"]));
                    }
                }
                cmd.CommandText = "SELECT P.PurposeofProcurement,S.SCategoryName as MakeInIndiaCategory FROM tbl_mst_MainProduct AS P LEFT OUTER JOIN tbl_mst_SubCategory AS S ON P.PurposeofProcurement = S.SCategoryId where S.SCategoryName like @SearchText + '%' group by P.PurposeofProcurement ,S.SCategoryName ";
                cmd.CommandText = "select P.PurposeofProcurement,tbl_mst_SubCategory.SCategoryName as MakeInIndiaCategory FROM tbl_mst_MainProduct AS P LEFT OUTER JOIN tbl_mst_SubCategory ON P.PurposeofProcurement = tbl_mst_SubCategory.SCategoryId where tbl_mst_SubCategory.ScategoryName like @SearchText + '%' and PurposeofProcurement!='' group by PurposeofProcurement ,tbl_mst_SubCategory.SCategoryName ";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["MakeInIndiaCategory"]));
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
        User_S_Story2 u = new User_S_Story2();
        u.SeachResult(prefix);
        return "search";
    }
    #endregion
    protected void lblIndiG_Click(object sender, EventArgs e)
    {
        chktargetclick.Value = "";
        chkindigclick.Value = "Yes";
        chkcategclick.Value = "";
        chknameclick.Value = "";
        chkaddressclick.Value = "";
        pagingCurrentPage = 0;
        SeachResult();
    }
    protected void rdblindegprocess_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdblindegprocess.SelectedItem.Text == "No")
        {
            rbIgCategory.Enabled = false;
        }
        else
        {
            rbIgCategory.Enabled = true;
        }
    }  
    protected void BindCompany()
    {
        DataTable DtCompanyDDL = new DataTable();
        try
        {
            if (hidType.Value == "Admin" || hidType.Value == "SuperAdmin")
            {
                DtCompanyDDL = DtGrid.DefaultView.ToTable(true, "CompanyName", "CompanyRefNo");
            }
            else
            {
                DataView dvcompfilter = new DataView(DtGrid);
                if (hidType.Value.Contains("C"))
                {
                    dvcompfilter.RowFilter = "CompanyRefNo='" + Session["CompanyRefNo"].ToString() + "'";
                }
                else if (hidType.Value.Contains("D"))
                {
                    dvcompfilter.RowFilter = "FactoryRefno='" + Session["CompanyRefNo"].ToString() + "'";
                }
                else if (hidType.Value.Contains("U"))
                {
                    dvcompfilter.RowFilter = "UnitRefno='" + Session["CompanyRefNo"].ToString() + "'";
                }
                DtGrid = dvcompfilter.ToTable();
                DtCompanyDDL = DtGrid.DefaultView.ToTable(true, "CompanyName", "CompanyRefNo");
            }
            DtCompanyDDL.DefaultView.Sort = "CompanyName asc";
            if (DtCompanyDDL.Rows.Count > 0)
            {
                for (int i = DtCompanyDDL.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = DtCompanyDDL.Rows[i];
                    if (dr["CompanyName"].ToString() == "Testing Company")
                        dr.Delete();
                }
                DtCompanyDDL.AcceptChanges();
                if (hidType.Value == "Admin" || hidType.Value == "SuperAdmin")
                {
                    ddlcompany.DataSource = DtCompanyDDL;
                    ddlcompany.DataBind();
                    ddlcompany.Visible = true;
                }
                else
                { ddlcompany.Visible = false; }
            }
        }
        catch (Exception ex)
        { }
    }
    protected void ddlcompany_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "comp")
        {
            if (hidType.Value == "Admin" || hidType.Value == "SuperAdmin")
            {
                hidType.Value = "Company";
            }
            mRefNo.Value = e.CommandArgument.ToString();
            SeachResult();
        }
    }
    protected void DDLMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }  
    protected void RdoYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        SeachResult();
    }
  
}