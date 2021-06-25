using BusinessLayer;
using Encryption;
using System;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;

public partial class Grievance_GMaster : System.Web.UI.MasterPage
{
    Logic Lo = new Logic();
    Cryptography ObjEnc = new Cryptography();
    string strInterestedArea = "";
    string strMasterAlloted = "";
    string sType = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["GUser"] != null)
        {
            if (Session["SFToken"] != null && Request.Cookies["SFToken"] != null)
            {
                if (!Session["SFToken"].ToString().Equals(Request.Cookies["SFToken"].Value))
                {
                    FuncLogout();
                    Response.RedirectToRoute("ProductList");
                }
                else
                {
                    MenuLogin();
                }
            }
            else
            {
                FuncLogout();
                Response.RedirectToRoute("ProductList");
            }
        }
        else
        {
            Response.RedirectToRoute("ProductList");
        }
        Response.Cache.SetExpires(DateTime.Now.AddMinutes(30));
    }
    protected void FuncLogout()
    {
        Session.Abandon();
        Session.Remove("GType");
        Session.Remove("GUser");
        Session.Remove("Gid");
        Session.Remove("SFToken");
        Session.RemoveAll();
        Session.Contents.RemoveAll();
        Session.Clear();
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cookies["GOfficialLogin"].Expires = DateTime.Now;
        Response.Buffer = true;
        Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
        Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
        Response.AppendHeader("Expires", "0"); // Proxies.
        if (Request.Cookies["GUser"] != null)
        {
            Response.Cookies["GUser"].Value = string.Empty;
            Response.Cookies["GUser"].Expires = DateTime.Now.AddMonths(-20);
        }
        if (Request.Cookies["SFToken"] != null)
        {
            Response.Cookies["SFToken"].Value = string.Empty;
            Response.Cookies["SFToken"].Expires = DateTime.Now.AddMonths(-20);
        }
    }
    protected void lbllogout_Click(object sender, EventArgs e)
    {
        FuncLogout();
        Response.RedirectToRoute("GOfficialLogin");
    }
    #region Menu Wise Login
    protected void MenuLogin()
    {
        lbltypelogin.Text = ObjEnc.DecryptData(Session["GType"].ToString());
        lblusername.Text = ObjEnc.DecryptData(Session["GUser"].ToString());
        if (Session["Gid"] != null)
        {
            sType = Session["Gid"].ToString();
        }
        if (lbltypelogin.Text == "Admin")
        {
            a3.Visible = true;
            a4.Visible = true;           
            a5.Visible = false;
            a7.Visible = true;
        }
        else
        {
            if (lbltypelogin.Text == "Helpdesk")
            {
                a3.Visible = false;
                a4.Visible = true;
                a5.Visible = true;
                a7.Visible = true;
            }
            else
            {
                a3.Visible = false;
                a4.Visible = false;
                a5.Visible = true;
                a7.Visible = false;
            }
        }
    }
    #endregion
    ////Crose site antiforgiryt key
    private const string AntiXsrfTokenKey = "__AntiXsrfToken";
    private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    private string _antiXsrfTokenValue;
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void master_Page_PreLoad(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
}
