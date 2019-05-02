using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_Map : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dtCoordinates = new DataTable();
        DataColumn dc = new DataColumn();
        dc.ColumnName = "SCAddress1";
        dc.DefaultValue = "Delhi";
        dtCoordinates.Columns.Add(dc);

        DataColumn dc1 = new DataColumn();
        dc1.ColumnName = "Latitude";
        dc1.DefaultValue = "28.537361";
        dtCoordinates.Columns.Add(dc1);

        DataColumn dc2 = new DataColumn();
        dc2.ColumnName = "Longitude";
        dc2.DefaultValue = "77.238808";
        dtCoordinates.Columns.Add(dc2);

        DataColumn dc3 = new DataColumn();
        dc3.ColumnName = "Description";
        dc3.DefaultValue = "Test";
        dtCoordinates.Columns.Add(dc3);
        DataRow dr = dtCoordinates.NewRow();
        dtCoordinates.Rows.Add(dr);
        rptMarkers.DataSource = dtCoordinates;
        rptMarkers.DataBind();
    }
}