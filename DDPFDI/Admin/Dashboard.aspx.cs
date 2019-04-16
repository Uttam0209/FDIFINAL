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

public partial class Admin_Dashboard : System.Web.UI.Page
{
    Logic Lo = new Logic();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] != null)
        {
            if (!IsPostBack)
            {
                DataTable dt = Lo.RetriveAggregateValue("Count", "Company");
                lnkbtnTotComp.Text = dt.Rows[0][0].ToString();

                dt = Lo.RetriveAggregateValue("", "FDI");

                dt.Columns.Add(new DataColumn("Total", typeof(int)));
                Double total = Convert.ToDouble(dt.Compute("Sum(ExchangeTotalAmount)", "").ToString());

                lnkbtnFDI.Text = Math.Round((total / 10000000), 2).ToString() + " Crore";

                total = Convert.ToDouble(dt.Compute("Sum(ExchangeTotalAmount)", "FYID=17").ToString());
                lnkbtnLYFDI.Text = Math.Round((total / 10000000), 2).ToString() + " Crore";
            }
        }
        else
            Response.RedirectToRoute("Login");
    }
}