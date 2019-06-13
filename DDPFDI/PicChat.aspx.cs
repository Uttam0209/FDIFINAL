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

public partial class PicChat : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetChartData();
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static object[] GetChartData()
    {
        Logic Lo = new Logic();

        DataTable data = Lo.RetriveProductIndig();
        var chartData = new object[data.Rows.Count + 1];
        chartData[0] = new object[]{
                "CompName",
                "TotalProd",
                "IsIndiginised"
            };
        int j = 0;
        for (int i = 0; data.Rows.Count > i; i++)
        {
            j++;
            chartData[j] = new object[] { data.Rows[i]["CompName"], data.Rows[i]["TotalProd"], data.Rows[i]["IsIndiginised"] };
        }

        return chartData;
    }
}