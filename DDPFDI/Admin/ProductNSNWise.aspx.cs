using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.UI.DataVisualization.Charting;
using BusinessLayer;
using System.Drawing;
using Encryption;

public partial class Admin_ProductNSNWise : System.Web.UI.Page
{
    private Logic Lo = new Logic();
    DataTable DtGrid = new DataTable();
    DataUtility Co = new DataUtility();
    private Cryptography objEnc = new Cryptography();
    private PagedDataSource pgsource = new PagedDataSource();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Type"] != null || Session["User"] != null)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    lblPageName.Text = "Product Industry wise domain";
                    hidType.Value = objEnc.DecryptData(Session["Type"].ToString());
                    mRefNo.Value = objEnc.DecryptData(Request.QueryString["strangone"].ToString());
                    BindGridView();
                }
                else
                { Response.RedirectToRoute("login"); }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "ErrorMssgPopup('Session Expired,Please login again');window.location='Login'", true);
        }
    }
    #region Grid
    protected void BindGridView()
    {
        try
        {
            DtGrid = Lo.GetGraphNSNGROUP(mRefNo.Value, "", "ViewGraph");
            if (DtGrid.Rows.Count > 0)
            {
                gvnsngroup.DataSource = DtGrid;
                gvnsngroup.DataBind();
                string[] x = new string[DtGrid.Rows.Count];
                int[] y = new int[DtGrid.Rows.Count];
                for (int i = 0; i < DtGrid.Rows.Count; i++)
                {
                    x[i] = DtGrid.Rows[i][0].ToString();
                    y[i] = Convert.ToInt32(DtGrid.Rows[i][1]);
                }
                crtCompGraph.Series[0].Points.DataBindXY(x, y);
                crtCompGraph.Series[0].ChartType = SeriesChartType.Column;
                crtCompGraph.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
                crtCompGraph.ChartAreas["ChartArea1"].AxisX.LabelStyle.Interval = 1;
                crtCompGraph.Legends[0].Enabled = true;
                lblmsg.Text = "Total Record Found:- " + DtGrid.Rows.Count.ToString();
                foreach (DataPoint dp in this.crtCompGraph.Series["Default"].Points)
                {
                    dp.PostBackValue = "#VALX,#VALY";
                }
                pan1.Visible = true;
                pan2.Visible = false;
            }
            else
            {
                pan1.Visible = false;
                pan2.Visible = false;
                lblmsg.Text = "No record found";
            }

        }
        catch (Exception ex)
        {
            pan1.Visible = false;
            pan2.Visible = false;
            lblmsg.Text = "No record found";
        }
    }
    protected void BindGridViewSubDomain(string a)
    {
        try
        {
            DataTable DtSubGraph = Lo.GetGraphNSNGROUP(mRefNo.Value, a, "ViewSubGraph");
            if (DtSubGraph.Rows.Count > 0)
            {
                gvnsngroupclass.DataSource = DtSubGraph;
                gvnsngroupclass.DataBind();
                string[] x = new string[DtSubGraph.Rows.Count];
                int[] y = new int[DtSubGraph.Rows.Count];
                for (int i = 0; i < DtSubGraph.Rows.Count; i++)
                {
                    x[i] = DtSubGraph.Rows[i][0].ToString();
                    y[i] = Convert.ToInt32(DtSubGraph.Rows[i][1]);
                }
                crtSubdomain.Series[0].Points.DataBindXY(x, y);
                crtSubdomain.Series[0].ChartType = SeriesChartType.Column;
                crtSubdomain.ChartAreas["crtsub"].Area3DStyle.Enable3D = false;
                crtSubdomain.ChartAreas["crtsub"].AxisX.LabelStyle.Interval = 1;
                crtSubdomain.Legends[0].Enabled = true;
                lblmsg.Text = "Total Record Found:- " + DtSubGraph.Rows.Count.ToString();
                foreach (DataPoint dp in this.crtSubdomain.Series["SubDomian"].Points)
                {
                    dp.PostBackValue = "#VALX,#VALY";
                }
                pan2.Visible = true;
                pan1.Visible = false;
            }
            else
            {
                lblmsg.Text = "No record found";
                pan2.Visible = false;
                pan1.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = "No record found";
            pan2.Visible = false;
            pan1.Visible = false;
        }
    }
    #endregion
    protected void crtCompGraph_Click(object sender, ImageMapEventArgs e)
    {
        string[] pointData = e.PostBackValue.Split(',');
        string a = pointData[0];
        ViewState["lblValue"] = pointData[1];
        BindGridViewSubDomain(a);
      
    }
    string subdomain = "";
    protected void crtSubdomain_Click(object sender, ImageMapEventArgs e)
    {
        string[] pointData = e.PostBackValue.Split(',');
        subdomain = pointData[0];
        mRefNo.Value = subdomain.ToString();
        DtGrid = Lo.GetDashboardData("ProdSearchNor", mRefNo.Value);
        if (DtGrid.Rows.Count > 0)
        {
            try
            {
                int[] iColumns = { 2, 4, 6, 7, 9, 11, 18, 19, 20, 21, 22, 24, 25, 57, 60, 58, 59, 62, 61 };
                RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
                objExport.ExportDetails(DtGrid, iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "ProductIndustryDomian.xls");
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void gvnsngroup_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void gvnsngroup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gvr.RowIndex;
            ViewState["lblValue"] = (gvnsngroup.Rows[rowIndex].FindControl("lblnsngroup") as Label).Text;
            string a = e.CommandArgument.ToString();
            string mString = ViewState["lblValue"].ToString();
            string[] splitString = mString.Split('(');
            ViewState["lblValue"] = splitString[1];
            BindGridViewSubDomain(a);
        }
    }
    protected void gvnsngroupclass_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "exview")
        {
            string RefNo = e.CommandArgument.ToString();
            DtGrid = Lo.GetDashboardData("ProdSearchNor", RefNo);
            if (DtGrid.Rows.Count > 0)
            {
                try
                {
                    int[] iColumns = { 2, 4, 6, 7, 9, 11, 18, 19, 20, 21, 22, 24, 25, 57, 60, 58, 59, 62, 61 };
                    RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
                    objExport.ExportDetails(DtGrid, iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "ProductNSNGROUPorClass.xls");
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
    protected void gvnsngroupclass_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void gvnsngroupclass_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label getactual = (e.Row.FindControl("lblnsngroupclass") as Label);
            string SmString = getactual.Text;
            string[] splitString = SmString.Split('(');
            string mFinalString = splitString[0] + "(" + ViewState["lblValue"].ToString().Remove(ViewState["lblValue"].ToString().Length - 1, 1) + splitString[1];
            e.Row.Cells[2].Text = mFinalString;
        }
    }
}