using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using Encryption;


public partial class User_NotDisplayProductOnPortal : System.Web.UI.Page
{
    Logic Lo = new Logic();
    DataTable Dt = new DataTable();
    Cryptography Enc = new Cryptography();
    DataUtility Co = new DataUtility();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["mqueryfornoproduct"] != null)
            {
                BindPagecode();
            }
            else
            {
                Response.Redirect("ProductWizard");
            }
        }
    }
    protected void BindPagecode()
    {
        DataTable Dt1 = Lo.ProductWizard(0, 0, 0, 0, 0, Enc.DecryptData(Session["Type"].ToString()), Session["CompanyRefNo"].ToString(), "", "", "", "", "", "", "", "CheckValueBelow0");
        if (Dt1.Rows.Count > 0)
        {
          //  lbbelowa.Text = Dt1.Rows.Count.ToString();

        }
        DataTable Dt2 = Lo.ProductWizard(0, 0, 0, 0, 0, Enc.DecryptData(Session["Type"].ToString()), Session["CompanyRefNo"].ToString(), "", "", "", "", "", "", "", "CheckValue0");
        if (Dt2.Rows.Count > 0)
        {
          //  lbvaluea.Text = Dt2.Rows.Count.ToString();
        }
        DataTable Dt3 = Lo.ProductWizard(0, 0, 0, 0, 0, Enc.DecryptData(Session["Type"].ToString()), Session["CompanyRefNo"].ToString(), "", "", "", "", "", "", "", "ProdBelowYr1718");
        if (Dt3.Rows.Count > 0)
        {
           // lb17.Text = Dt3.Rows.Count.ToString();
        }
        DataTable Dt = Lo.ProductWizard(0, 0, 0, 0, 0, Enc.DecryptData(Session["Type"].ToString()), Session["CompanyRefNo"].ToString(), "", "", "", "", "", "", "", "noprodqty");
        if (Dt.Rows.Count > 0)
        {
            //gvNotDisplayPortal.DataSource = Dt;
            //gvNotDisplayPortal.DataBind();
            //lbyrnotavailable.Text = Dt.Rows[0]["noprodqtyavailable"].ToString();
            //lbeligible.Text = Dt.Rows[0]["IsShowNo"].ToString();
            //lbisindiginized.Text = Dt.Rows[0]["ISIndiginizedNo"].ToString();
            //lbigaviewonly.Text = Dt.Rows[0]["viewonlyalready"].ToString();
        }
    }

    protected void lbbelow0_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasterReasoneUpdate?below0=" + Enc.EncryptData("ProductBelow"));
    }

    protected void lbvalue0_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasterReasoneUpdate?value0=" + Enc.EncryptData("Value0"));
    }

    protected void lb17_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasterReasoneUpdate?lb17=" + Enc.EncryptData("Yrbelow17"));
    }

    protected void lbyrnotavailable_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasterReasoneUpdate?yrnot=" + Enc.EncryptData("YrNotAvailable"));

    }

    protected void lbeligible_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasterReasoneUpdate?eligibe=" + Enc.EncryptData("Eligible"));
    }

    protected void lbisindiginized_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasterReasoneUpdate?isindigi=" + Enc.EncryptData("IsIndiginized"));
    }

    protected void lbigaviewonly_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasterReasoneUpdate?viewonly=" + Enc.EncryptData("viewonly"));
    }

    protected void gvNotDisplayPortal_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gvNotDisplayPortal_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void lbvaluea_Click(object sender, EventArgs e)
    {

    }
}