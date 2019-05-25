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
                //if (Request.QueryString["id"] != null)
                //{
                //    string id = Request.QueryString["id"].ToString().Replace(" ", "+");
                //    lblPageName.Text = objCrypto.DecryptData(id);
                //}
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
        Response.Redirect("Detail-Company?mu=" + objCrypto.EncryptData("View") + "&id=" + objCrypto.EncryptData("View-Company"));

    }
    protected void lnkbtnTotDiv_Click(object sender, EventArgs e)
    {
        Response.Redirect("Detail-Company?mu=" + objCrypto.EncryptData("View") + "&id=" + objCrypto.EncryptData("View-Division"));
    }
    protected void lnkbtnTotUnit_Click(object sender, EventArgs e)
    {
        Response.Redirect("Detail-Company?mu=" + objCrypto.EncryptData("View") + "&id=" + objCrypto.EncryptData("View-Unit"));
    }

    protected void lnkbtnTotEmp_Click(object sender, EventArgs e)
    {
        Response.Redirect("View-NodalOfficer?mu=" + objCrypto.EncryptData("View") + "&id=" + objCrypto.EncryptData("View-Nodal Officer"));
    }
    protected void lnkbtnProduct_Click(object sender, EventArgs e)
    {
        Response.Redirect("View-Product?mu=" + objCrypto.EncryptData("View") + "&id=" + objCrypto.EncryptData("View-Product"));
    }
}