using System;
using System.Data;
using System.Web.UI;
using BusinessLayer;
using Encryption;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Collections.Generic;

public partial class Admin_Dashboard : System.Web.UI.Page
{
    Cryptography objCrypto = new Cryptography();
    Logic Lo = new Logic();
    DataTable DtGrid = new DataTable();
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
                lnkbtnIndigenizedProduct.Text = dt.Rows[0]["IsIndiginised"].ToString();
                if (objCrypto.DecryptData(Session["Type"].ToString()) == "Admin" || objCrypto.DecryptData(Session["Type"].ToString()) == "SuperAdmin")
                {
                    FillProduct();
                    GetChartData();
                }
            }
        }
        else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert",
                "alert('Session Expired,Please login again');window.location='Login'", true);
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object[] GetChartData()
    {
        Logic Lo = new Logic();

        DataTable data = Lo.RetriveProductIndig();
        var chartData = new object[data.Rows.Count + 1];
        chartData[0] = new object[]{
                "Company Name",
                "P",
                "I"
            };
        int j = 0;
        for (int i = 0; data.Rows.Count > i; i++)
        {
            j++;
            chartData[j] = new object[] { data.Rows[i]["CompName"], data.Rows[i]["TotalProd"], data.Rows[i]["IsIndiginised"] };
        }

        return chartData;
    }

    public void FillProduct()
    {
        DataTable dtProductDetail = Lo.RetriveProductIndig();
        if (dtProductDetail.Rows.Count > 0)
        {
            gvPrdoct.DataSource = dtProductDetail;
            gvPrdoct.DataBind();
        }
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
    protected void lnkbtnIndigenizedProduct_Click(object sender, EventArgs e)
    {
        Response.Redirect("Dashboard-View?id=" + HttpUtility.UrlEncode(objCrypto.EncryptData("PI")));
    }
    protected void lblComp_Click(object sender, EventArgs e)
    {
        DtGrid = Lo.GetDashboardData("Company", "");
        DataView dv = new DataView(DtGrid);
        if (objCrypto.DecryptData(Session["Type"].ToString()) != "Admin" && objCrypto.DecryptData(Session["Type"].ToString()) != "SuperAdmin")
        {
            if (DtGrid.Rows.Count > 0)
            {
                //code to filter role wise 
                DataTable dtParentNode = Lo.RetriveParentNode(objCrypto.DecryptData(Session["Type"].ToString()).ToUpper(), Session["CompanyRefNo"].ToString());
                if (objCrypto.DecryptData(Session["Type"].ToString()).ToUpper() == "COMPANY")
                    dv.RowFilter = "CompanyRefNo='" + Session["CompanyRefNo"].ToString() + "'";
                else if (objCrypto.DecryptData(Session["Type"].ToString()).ToUpper() == "DIVISION")
                    dv.RowFilter = "CompanyRefNo='" + dtParentNode.Rows[0]["CompanyRefNo"].ToString() + "'";
                else
                    dv.RowFilter = "CompanyRefNo='" + dtParentNode.Rows[0]["CompanyRefNo"].ToString() + "'";
            }
        }
        try
        {
            int[] iColumns = { 1, 2, 6, 5, 7 };
            RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
            objExport.ExportDetails(dv.ToTable(), iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "CompanyList.xls");
        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkDiv_Click(object sender, EventArgs e)
    {
        DtGrid = Lo.GetDashboardData("Division", "");
        DataView dv = new DataView(DtGrid);
        if (objCrypto.DecryptData(Session["Type"].ToString()) != "Admin" && objCrypto.DecryptData(Session["Type"].ToString()) != "SuperAdmin")
        {
            if (DtGrid.Rows.Count > 0)
            {
                //code to filter role wise 
                DataTable dtParentNode = Lo.RetriveParentNode(objCrypto.DecryptData(Session["Type"].ToString()).ToUpper(), Session["CompanyRefNo"].ToString());
                if (objCrypto.DecryptData(Session["Type"].ToString()).ToUpper() == "COMPANY")
                    dv.RowFilter = "CompanyRefNo='" + Session["CompanyRefNo"].ToString() + "'";
                else if (objCrypto.DecryptData(Session["Type"].ToString()).ToUpper() == "DIVISION")
                    dv.RowFilter = "FactoryRefNo='" + dtParentNode.Rows[0]["FactoryRefNo"].ToString() + "'";
                else
                    dv.RowFilter = "UnitRefNo='" + dtParentNode.Rows[0]["UnitRefNo"].ToString() + "'";
            }
        }
        //renaming colm for user
        dv.Table.Columns["FactoryRefNo"].ColumnName = "DivisionRefNo";
        dv.Table.Columns["FactoryName"].ColumnName = "DivisionName";

        try
        {
            int[] iColumns = { 3, 6, 5, 8, 10 };
            RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
            objExport.ExportDetails(dv.ToTable(), iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "Division.xls");
        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkUnit_Click(object sender, EventArgs e)
    {
        DtGrid = Lo.GetDashboardData("Unit", "");
        DataView dv = new DataView(DtGrid);
        if (objCrypto.DecryptData(Session["Type"].ToString()) != "Admin" && objCrypto.DecryptData(Session["Type"].ToString()) != "SuperAdmin")
        {
            if (DtGrid.Rows.Count > 0)
            {
                //code to filter role wise 
                DataTable dtParentNode = Lo.RetriveParentNode(objCrypto.DecryptData(Session["Type"].ToString()).ToUpper(), Session["CompanyRefNo"].ToString());
                if (objCrypto.DecryptData(Session["Type"].ToString()).ToUpper() == "COMPANY")
                    dv.RowFilter = "CompanyRefNo='" + Session["CompanyRefNo"].ToString() + "'";
                else if (objCrypto.DecryptData(Session["Type"].ToString()).ToUpper() == "DIVISION")
                    dv.RowFilter = "FactoryRefNo='" + dtParentNode.Rows[0]["FactoryRefNo"].ToString() + "'";
                else
                    dv.RowFilter = "UnitRefNo='" + dtParentNode.Rows[0]["UnitRefNo"].ToString() + "'";
            }
        }
        //renaming colm for user
        dv.Table.Columns["FactoryName"].ColumnName = "DivisionName";
        try
        {
            int[] iColumns = { 3, 5, 8, 7, 10, 11 };
            RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
            objExport.ExportDetails(dv.ToTable(), iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "Unit.xls");
        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkEmp_Click(object sender, EventArgs e)
    {
        DtGrid = Lo.GetDashboardData("Employee", "");
        DataView dv = new DataView(DtGrid);
        if (objCrypto.DecryptData(Session["Type"].ToString()) != "Admin" && objCrypto.DecryptData(Session["Type"].ToString()) != "SuperAdmin")
        {
            if (DtGrid.Rows.Count > 0)
            {
                this.UpdateDtGridValue();
                // code to filter row role wise
                if (objCrypto.DecryptData(Session["Type"].ToString()).ToUpper() == "COMPANY")
                    dv.RowFilter = "CompanyRefNo='" + Session["CompanyRefNo"].ToString() + "'";
                else if (objCrypto.DecryptData(Session["Type"].ToString()).ToUpper() == "DIVISION")
                    dv.RowFilter = "FactoryRefNo='" + Session["CompanyRefNo"].ToString() + "'";
                else
                    dv.RowFilter = "UnitRefNo='" + Session["CompanyRefNo"].ToString() + "'";
            }
        }
        dv.Sort = "CompanyName asc,FactoryName asc";

        //renaming colm for user
        dv.Table.Columns["Type"].ColumnName = "Role";
        dv.Table.Columns["FactoryName"].ColumnName = "DivisionName";

        try
        {
            int[] iColumns = { 1, 2, 3, 4, 8, 11, 14, 19 };
            RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
            objExport.ExportDetails(dv.ToTable(), iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "Employee.xls");
        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkProduct_Click(object sender, EventArgs e)
    {
        DtGrid = Lo.GetDashboardData("Product", "");
        DataView dv = new DataView(DtGrid);
        if (objCrypto.DecryptData(Session["Type"].ToString()) != "Admin" && objCrypto.DecryptData(Session["Type"].ToString()) != "SuperAdmin")
        {
            if (DtGrid.Rows.Count > 0)
            {
                this.UpdateDtGridValue();
                // code to filter row role wise
                if (objCrypto.DecryptData(Session["Type"].ToString()).ToUpper() == "COMPANY")
                    dv.RowFilter = "CompanyRefNo='" + Session["CompanyRefNo"].ToString() + "'";
                else if (objCrypto.DecryptData(Session["Type"].ToString()).ToUpper() == "DIVISION")
                    dv.RowFilter = "FactoryRefNo='" + Session["CompanyRefNo"].ToString() + "'";
                else
                    dv.RowFilter = "UnitRefNo='" + Session["CompanyRefNo"].ToString() + "'";
            }
        }
        dv.Sort = "CompanyName asc,FactoryName asc";
        //renaming colm for user                
        dv.Table.Columns["FactoryName"].ColumnName = "DivisionName";
        try
        {
            int[] iColumns = { 10, 9, 4, 14, 18, 19, 20, 21, 22, 24, 25 };
            RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
            objExport.ExportDetails(dv.ToTable(), iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "Product.xls");
        }
        catch (Exception ex)
        {

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
    protected void gvPrdoct_RowCreated(object sender, GridViewRowEventArgs e)
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
}