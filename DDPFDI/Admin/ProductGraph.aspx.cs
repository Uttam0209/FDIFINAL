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

public partial class Admin_ProductGraph : System.Web.UI.Page
{
    private Logic Lo = new Logic();
    DataTable DtGrid = new DataTable();
    DataUtility Co = new DataUtility();
    private Cryptography objEnc = new Cryptography();
    private PagedDataSource pgsource = new PagedDataSource();
    string subdomain = "";
    string RefNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Type"] != null || Session["User"] != null)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    lblPageName.Text = "Item Industry Domain Graph";
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
    protected void BindGridView()
    {
        try
        {
            DtGrid = Lo.GetGraph(mRefNo.Value, "", "ViewGraph");
            if (DtGrid.Rows.Count > 0)
            {
                string[] x = new string[DtGrid.Rows.Count];
                int[] y = new int[DtGrid.Rows.Count];
                for (int i = 0; i < DtGrid.Rows.Count; i++)
                {
                    x[i] = DtGrid.Rows[i][0].ToString();
                    y[i] = Convert.ToInt32(DtGrid.Rows[i][1]);
                }
                crtCompGraph.Series[0].Points.DataBindXY(x, y);
                crtCompGraph.Series[0].ChartType = SeriesChartType.StackedColumn;
                crtCompGraph.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
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
    protected void crtCompGraph_Click(object sender, ImageMapEventArgs e)
    {
        string[] pointData = e.PostBackValue.Split(',');
        string a = pointData[0];
        BindGridViewSubDomain(a);
    }
    protected void BindGridViewSubDomain(string a)
    {
        try
        {
            DataTable DtSubGraph = Lo.GetGraph(mRefNo.Value, a, "ViewSubGraph");
            if (DtSubGraph.Rows.Count > 0)
            {
                string[] x = new string[DtSubGraph.Rows.Count];
                int[] y = new int[DtSubGraph.Rows.Count];
                for (int i = 0; i < DtSubGraph.Rows.Count; i++)
                {
                    x[i] = DtSubGraph.Rows[i][0].ToString();
                    y[i] = Convert.ToInt32(DtSubGraph.Rows[i][1]);
                }
                crtSubdomain.Series[0].Points.DataBindXY(x, y);
                crtSubdomain.Series[0].ChartType = SeriesChartType.StackedColumn;
                crtSubdomain.ChartAreas["crtsub"].Area3DStyle.Enable3D = true;
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
    protected void crtSubdomain_Click(object sender, ImageMapEventArgs e)
    {
        string[] pointData = e.PostBackValue.Split(',');
        subdomain = pointData[0];
        RefNo = subdomain.ToString();
        DtGrid = Lo.GetDashboardData("ProdSearchNor", RefNo);
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
}