using Encryption;
using System;
using System.Web;
using System.Web.UI;

public partial class User_MasterPage : System.Web.UI.MasterPage
{
    private Cryptography Encrypt = new Cryptography();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MenuLink();
        }
    }
    protected void lbtotalcart_Click(object sender, EventArgs e)
    {
        if (ViewState["buyitems"] != null)
        {
            Session["DCart"] = ViewState["buyitems"];
            Response.Redirect("U_Cart");
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No product in cart')", true);
        }
    }
    protected void lbcart_Click(object sender, EventArgs e)
    {
        if (ViewState["buyitems"] != null)
        {
            Session["DCart"] = ViewState["buyitems"];
            Response.Redirect("U_Cart");
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No product in cart')", true);
        }
    }
    protected void lbllogout_Click(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cookies["DefaultDpsu"].Expires = DateTime.Now;
        Response.Buffer = true;
        Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
        Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
        Response.AppendHeader("Expires", "0"); // Proxies.
        Response.Cookies.Remove("DefaultDpsu");
        Response.Cookies.Remove("Dashboard");
        Response.Cookies.Remove("ProductList");
        if (Request.Cookies["User"] != null)
        {
            Response.Cookies["User"].Value = string.Empty;
            Response.Cookies["User"].Expires = DateTime.Now.AddMonths(-20);
        }
        if (Request.Cookies["SFToken"] != null)
        {
            Response.Cookies["SFToken"].Value = string.Empty;
            Response.Cookies["SFToken"].Expires = DateTime.Now.AddMonths(-20);
        }
        Session.Remove("Type");
        Session.Remove("User");
        Session.Remove("CompanyRefNo");
        Session.Remove("SFToken");
        Session.RemoveAll();
        Session.Clear();
        Session.Contents.RemoveAll();
        Session.Abandon();
        Response.RedirectToRoute("Productlist");
    }
    protected void MenuLink()
    {
        if (Session["User"] != null)
        {
            linkusername.Text = Encrypt.DecryptData(Session["User"].ToString());
            linkusername.Visible = true;
            linkusername.Text = "Welcome: " + linkusername.Text;
            lnkfeedback.Visible = true;
            linklogin.Visible = false;
            lblmis.Visible = true;
            lbllogout.Visible = true;
            lbSuccesstory.Visible = true;
            reportdiv.Visible = true;
            mhwparti.Visible = false;
        }
        else
        {
            lbSuccesstory.Visible = false;
            linklogin.Visible = true;
            linkusername.Visible = false;
            lblmis.Visible = false;
            lbllogout.Visible = false;
            PR.Visible = false; A3.Visible = false;
            reportdiv.Visible = false;
            mhwparti.Visible = true;
        }
    }
}
