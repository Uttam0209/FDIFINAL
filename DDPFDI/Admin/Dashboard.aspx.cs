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
    Cryptography objCrypto = new Cryptography();
    Logic Lo = new Logic();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["User"] != null)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    string id = Request.QueryString["id"].ToString().Replace(" ", "+");
                    lblPageName.Text = objCrypto.DecryptData(id);
                }
                DataTable dt = Lo.RetriveAggregateValue("Count", "Company");
                lnkbtnTotComp.Text = dt.Rows[0][0].ToString();
                lnkbtnFDI.Text = dt.Rows[0][1].ToString();               
                lnkbtnLYFDI.Text = dt.Rows[0][2].ToString();
            }
        }
        else
            Response.RedirectToRoute("Login");
    }
}