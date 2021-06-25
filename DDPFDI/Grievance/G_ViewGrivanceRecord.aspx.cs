using BusinessLayer;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using context = System.Web.HttpContext;
using Encryption;
public partial class Grievance_G_ViewGrivanceRecord : System.Web.UI.Page
{
    #region Variable    
    Logic Lo = new Logic();
    DataUtility Co = new DataUtility();
    DataTable DtFilterView = new DataTable();
    Cryptography Enc = new Cryptography();
    private PagedDataSource pgsource = new PagedDataSource();
    string foruse = "";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["mu"] != null && Request.QueryString["msession"] != null)
            {
                BindYear(); BindMonth(); BindType(); bindportal();
                BindRecord();
            }
            else
            {
                Response.Redirect("GADashboard");
            }
        }
    }
    #region MasterData
    protected void BindYear()
    {
        DataTable DtYear = Lo.RetriveHelpdesk(0, 0, 0, "", "", "", "", "Year");
        if (DtYear.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlyear, DtYear, "FY", "FYID");
            ddlyear.Items.Insert(0, "Select");
        }
    }
    protected void BindMonth()
    {
        DataTable DtYear = Lo.RetriveHelpdesk(0, 0, 0, "", "", "", "", "Month");
        if (DtYear.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlmonth, DtYear, "MonthName", "MonthName");
            ddlmonth.Items.Insert(0, "Select");
        }
    }
    protected void BindType()
    {
        DataTable DtYear = Lo.RetriveHelpdesk(0, 0, 0, "", "", "", "", "Type");
        if (DtYear.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddltype, DtYear, "Type", "Type");
            ddltype.Items.Insert(0, "Select");
        }
    }
    protected void bindportal()
    {
        DataTable DtPortal = Lo.RetriveHelpdesk(0, 0, 0, "", "", "", "", "Portal");
        if (DtPortal.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlportal, DtPortal, "HFrom", "HFrom");
            ddlportal.Items.Insert(0, "Select");
        }
    }
    protected void BindRecord()
    {
        DataTable DtMaster = Lo.RetriveHelpdesk(0, 0, 0, Enc.DecryptData(Request.QueryString["msession"].ToString()), Enc.DecryptData(Request.QueryString["mu"].ToString()), "", "", "MasterData");
        if (DtMaster.Rows.Count > 0)
        {
            Session["TempData"] = DtMaster;
            SeachResult();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found.')", true);
        }
    }
    #endregion
    #region SearchCode
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        SeachResult();
    }
    string insert1 = "";
    protected string Dvinsert(string sortExpression = null)
    {
        DataTable insert = new DataTable();
        insert.Columns.Add(new DataColumn("Column", typeof(string)));
        insert.Columns.Add(new DataColumn("Value", typeof(string)));
        DataRow dr;

        if (ddlmonth.SelectedItem.Value != "Select")
        {
            dr = insert.NewRow();
            dr["Column"] = "Month  = ";
            dr["Value"] = "'" + ddlmonth.SelectedItem.Value + "'";
            insert.Rows.Add(dr);
        }
        if (ddlyear.SelectedItem.Value != "Select")
        {
            dr = insert.NewRow();
            dr["Column"] = "Year = ";
            dr["Value"] = "'" + ddlyear.SelectedItem.Value.Trim() + "'";
            insert.Rows.Add(dr);
        }
        if (ddltype.SelectedItem.Value != "Select")
        {
            dr = insert.NewRow();
            dr["Column"] = "Type  = ";
            dr["Value"] = "'" + ddltype.SelectedItem.Value.Trim() + "'";
            insert.Rows.Add(dr);
        }
        if (ddlstatus.SelectedItem.Value != "Select")
        {
            dr = insert.NewRow();
            dr["Column"] = "IsOpen = ";
            dr["Value"] = "'" + ddlstatus.SelectedItem.Value.Trim() + "'";
            insert.Rows.Add(dr);
        }
        if (ddlportal.SelectedItem.Value != "Select")
        {
            dr = insert.NewRow();
            dr["Column"] = "HFrom =";
            dr["Value"] = "'" + ddlportal.SelectedItem.Value + "'";
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
                    if (dtinner.Rows.Count > 0)
                    {
                        lbltotal.Text = "Filter/Search Results " + dtinner.Rows.Count.ToString();
                        lbltotfilter.Text = dtinner.Rows.Count.ToString();
                        DataTable dtads = dv.ToTable();
                        if (dtads.Rows.Count > 0)
                        {
                            if (foruse == "Excle")
                            {
                                int[] iColumns = { 0, 1, 2, 3, 4, 5 };
                                RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
                                objExport.ExportDetails(dtads, iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "IssueFeedBack.xls");
                            }
                            else
                            {
                                pgsource.DataSource = dtinner.DefaultView;
                                pgsource.AllowPaging = true;
                                pgsource.PageSize = Convert.ToInt32(ddlpaging.SelectedItem.Value);
                                pgsource.CurrentPageIndex = pagingCurrentPage;
                                lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                                lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                                LinkButton1.Enabled = !pgsource.IsFirstPage;
                                lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                                LinkButton2.Enabled = !pgsource.IsLastPage;
                                pgsource.DataSource = dtads.DefaultView;
                                if (ddlsort.SelectedItem.Value != "Sort by")
                                {
                                    if (ddlsort.SelectedItem.Text == "DESC")
                                    { dtads.DefaultView.Sort = "desc"; }
                                    else if (ddlsort.SelectedItem.Text == "ASC")
                                    { dtads.DefaultView.Sort = "asc"; }
                                }
                                else
                                {
                                    dv.Sort = "RiseDate desc";
                                }
                                gvcase.DataSource = pgsource;
                                gvcase.DataBind();
                                lbltotalleft.Text = "Total :-  " + DtFilterView.Rows.Count.ToString();
                                lbltotalshowpageitem.Text = pgsource.FirstIndexInPage + 1 + " - " + (pgsource.FirstIndexInPage + pgsource.Count);
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
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
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
    #region clickcodes
    protected void ddlsort_SelectedIndexChanged(object sender, EventArgs e)
    {
        foruse = "";
        SeachResult();
    }
    protected void ddlpaging_SelectedIndexChanged(object sender, EventArgs e)
    {
        foruse = "";
        SeachResult();
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        foruse = "Excle";
        SeachResult();
    }
    #endregion
}