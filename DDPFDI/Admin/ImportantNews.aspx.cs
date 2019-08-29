using System;
using BusinessLayer;
using System.Data;

public partial class Admin_ImportantNews : System.Web.UI.Page
{
    Logic Lo = new Logic();
    protected void Page_Load(object sender, EventArgs e)
    {
        BindNews();
    }
    protected void BindNews()
    {
        DataTable DtCountry = Lo.RetriveCountry(0, "ImpNews");
        if (DtCountry.Rows.Count > 0)
        {
            gv.DataSource = DtCountry;
            gv.DataBind();
        }
    }
}