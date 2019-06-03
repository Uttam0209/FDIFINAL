
using System;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Text;
using BusinessLayer;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Data.SqlClient;
using Encryption;
using System.Web;

public partial class Admin_Dashboard : System.Web.UI.Page
{
    Cryptography objCrypto = new Cryptography();
    Logic Lo = new Logic();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] != null)
        {
            if (!IsPostBack)
            {
                DataTable dt = Lo.RetriveAggregateValue("Count", objCrypto.DecryptData(Session["Type"].ToString()), Session["CompanyRefNo"].ToString());
                lnkbtnTotComp.Text = dt.Rows[0]["TotComp"].ToString();
                lnkbtnTotDiv.Text = dt.Rows[0]["TotDiv"].ToString();
                lnkbtnTotUnit.Text = dt.Rows[0]["TotUnit"].ToString();
                lnkbtnTotEmp.Text = dt.Rows[0]["TotEmployee"].ToString();
                lnkbtnProduct.Text = dt.Rows[0]["TotProduct"].ToString();
            }
        }
        else
            Response.RedirectToRoute("Login");
    }
    protected void lnkbtnTotComp_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard-View?id=" + HttpUtility.UrlEncode(objCrypto.EncryptData("C")));

    }
    protected void lnkbtnTotDiv_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard-View?id=" + HttpUtility.UrlEncode(objCrypto.EncryptData("D")));
    }
    protected void lnkbtnTotUnit_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard-View?id=" + HttpUtility.UrlEncode(objCrypto.EncryptData("U")));
    }

    protected void lnkbtnTotEmp_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard-View?id=" + HttpUtility.UrlEncode(objCrypto.EncryptData("E")));
    }
    protected void lnkbtnProduct_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard-View?id=" + HttpUtility.UrlEncode(objCrypto.EncryptData("P")));
    }

    protected void lblComp_Click(object sender, EventArgs e)
    {
        DataTable dt = Lo.GetDashboardData("Company", "");
        try
        {
            int[] iColumns = { 1, 2, 3, 5, 7 };
            RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
            objExport.ExportDetails(dt, iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "Company.xls");
        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkDiv_Click(object sender, EventArgs e)
    {
        DataTable dt = Lo.GetDashboardData("Division", "");
        try
        {
            int[] iColumns = { 3, 7, 8, 5, 6 };
            RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
            objExport.ExportDetails(dt, iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "Division.xls");
        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkUnit_Click(object sender, EventArgs e)
    {
        DataTable dt = Lo.GetDashboardData("Unit", "");
        try
        {
            int[] iColumns = { 3, 7, 9, 10, 5, 6 };
            RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
            objExport.ExportDetails(dt, iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "Unit.xls");
        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkEmp_Click(object sender, EventArgs e)
    {
        DataTable dt = Lo.GetDashboardData("Employee", "");
        try
        {
            int[] iColumns = { 4, 7, 2, 8, 10 };
            RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
            objExport.ExportDetails(dt, iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "Employee.xls");
        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkProduct_Click(object sender, EventArgs e)
    {
        DataTable dt = Lo.GetDashboardData("Product", "");
        try
        {
            int[] iColumns = { 2, 4, 5, 6, 13, 14, 16, 3 };
            RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
            objExport.ExportDetails(dt, iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "Product.xls");
        }
        catch (Exception ex)
        {

        }
    }
}