using BusinessLayer;
using Encryption;
using System;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;


public partial class Vendor_VendorMaster : System.Web.UI.MasterPage
{
    Logic Lo = new Logic();
    Cryptography ObjEnc = new Cryptography();
    string strInterestedArea = "";
    string strMasterAlloted = "";
    string sType = "";
    DataTable MenuDT = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["VUser"] != null && Session["VUserEmail"] != null)
        {
            if (Session["SFToken"] != null && Request.Cookies["SFToken"] != null)
            {
                if (!Session["SFToken"].ToString().Equals(Request.Cookies["SFToken"].Value))
                {
                    FuncLogout();
                    Response.Redirect("VendorLogin");

                }
                else
                {
                    MenuLogin();
                }
            }
            else
            {
                FuncLogout();
                Response.Redirect("VendorLogin");
            }
        }
        else
        {
            Response.RedirectToRoute("VendorLogin");
        }
        Response.Cache.SetExpires(DateTime.Now.AddMinutes(30));

    }
    protected void FuncLogout()
    {
        Session.Abandon();
        Session.Remove("VType");
        Session.Remove("VUser");
        Session.Remove("VendorRefNo");
        Session.Remove("VCompName");
        Session.Remove("SFToken");
        Session.RemoveAll();
        Session.Contents.RemoveAll();
        Session.Clear();
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cookies["VendorLogin"].Expires = DateTime.Now;
        Response.Buffer = true;
        Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
        Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
        Response.AppendHeader("Expires", "0"); // Proxies.
        if (Request.Cookies["VUser"] != null)
        {
            Response.Cookies["VUser"].Value = string.Empty;
            Response.Cookies["VUser"].Expires = DateTime.Now.AddMonths(-20);
        }
        if (Request.Cookies["SFToken"] != null)
        {
            Response.Cookies["SFToken"].Value = string.Empty;
            Response.Cookies["SFToken"].Expires = DateTime.Now.AddMonths(-20);
        }
    }
    protected void lblogout_Click(object sender, EventArgs e)
    {
        FuncLogout();
        Response.RedirectToRoute("VendorLogin");
    }
    private const string AntiXsrfTokenKey = "__AntiXsrfToken";
    private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    private string _antiXsrfTokenValue;
    protected void Page_Init(object sender, EventArgs e)
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
    protected void master_Page_PreLoad(object sender, EventArgs e)
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
    private void bindMasterMenu(string sType)
    {
        StringBuilder strMasterMenu = new StringBuilder();
        strMasterMenu.Append("<ul class='side-nav mt-2'>");
        string[] MCateg = strMasterAlloted.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        string MmCval = "";
        for (int x = 0; x < MCateg.Length; x++)
        {
            MmCval = MCateg[x];
            DataTable dtMArea = Lo.RetriveMasterData(Convert.ToInt64(MmCval), sType, "", 0, "", "", "VendorIntAreaMenuId");
            foreach (DataRow row in dtMArea.Rows)
            {
                strMasterMenu.Append("<li class='side-nav-item'>" +
                    "<a data-bs-toggle='collapse' href='#" + row["InterestArea"].ToString().Substring(1, 2) + row["Id"].ToString() + "' aria-expanded='false' aria-controls='" + row["InterestArea"].ToString().Substring(1, 2) + row["Id"].ToString() + "'" +
                    " class='side-nav-link collapsed'  title='" + row["Tooltip"].ToString() + "'>" +
                    "<i class='fas fa-tachometer-alt mr-1'></i>" +
                    "<span>" + row["InterestArea"].ToString() + " </span>" +
                    "<span class='menu-arrow'>" +
                    "<i class='fas fa-angle-down'></i>" +
                    "</span>" +
                    "</a>");
                string[] MCateg1 = dtMArea.Rows[0]["MenuId"].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                string MmCval1 = "";
                strMasterMenu.Append("<div class='collapse' id='" + row["InterestArea"].ToString().Substring(1, 2) + row["Id"].ToString() + "'>");
                strMasterMenu.Append("<ul class='side-nav-second-level'>");
                for (int j = 0; j < MCateg1.Length; j++)
                {
                    MmCval1 = MCateg1[j];
                    DataTable dtMMenu = Lo.RetriveMasterData(0, "", ObjEnc.DecryptData(Session["VType"].ToString()),
                        Convert.ToInt16(MmCval1), "", "", "MenuMainVendor");
                    foreach (DataRow row2 in dtMMenu.Rows)
                    {
                        strMasterMenu.Append("<li class='side-nav-item'>" +
                            "<a data-bs-toggle='collapse'"
                             + "aria-expanded='false' aria-controls='" + row2["MenuUrl"].ToString().Substring(1) + "' class='collapsed'"
                             + "href='" + row2["MenuUrl"].ToString() + "'"
                             + "title='" + row2["Tooltip"].ToString() + "'>"
                             + "<span >" + row2["MenuName"].ToString() + "</span>"
                           + "<span class='menu-arrow'><i class='fas fa-angle-down'></i></span>"
                             + "</a>");
                        DataTable SubMmenu = Lo.RetriveMasterData(0, "", ObjEnc.DecryptData(Session["VType"].ToString()),
                            Convert.ToInt16(row2["MenuID"].ToString()), "", "", "SubMenuVendor");
                        if (SubMmenu.Rows.Count > 0)
                        {
                            strMasterMenu.Append("<div class='collapse' id='" + row2["MenuUrl"].ToString().Substring(1) + "'>");
                            strMasterMenu.Append("<ul class='side-nav-third-level'>");
                            foreach (DataRow row1 in SubMmenu.Rows)
                            {
                                strMasterMenu.Append("<li><a href='" + row1["MenuUrl"].ToString() + "?mu="
                                    + ObjEnc.EncryptData(row1["Spanclass"].ToString()) + "&id="
                                    + ObjEnc.EncryptData(row["InterestArea"].ToString() + " >>" +
                                    " " + row2["MenuName"].ToString() + " >> " +
                                    "" + row1["MenuName"].ToString()) + "" +
                                    "' title='" + row1["Tooltip"].ToString() + "'>"
                                    + row1["MenuName"].ToString() +
                                    "</a>" +
                                    "</li> ");
                            }
                            strMasterMenu.Append("</ul>");
                            strMasterMenu.Append("</div>");
                        }
                        strMasterMenu.Append("</li>");
                    }
                }
                strMasterMenu.Append("</ul>");
                strMasterMenu.Append("</div>");
                strMasterMenu.Append("</li>");
            }
        }
        strMasterMenu.Append("</ul>");
        MasterMenu.InnerHtml = strMasterMenu.ToString();

    }
    #region Menu Wise Login
    protected void MenuLogin()
    {
        if (ObjEnc.DecryptData(Session["VType"].ToString()) == "Vendor")
        {
            ViewState["mVtype"] = "Vendor";
        }
        else
        {
            ViewState["mVtype"] = ObjEnc.DecryptData(Session["VType"].ToString());
        }
        lblusername.Text = ObjEnc.DecryptData(Session["VUser"].ToString());
        if (Session["VendorRefNo"] != null)
        {
            sType = Session["VendorRefNo"].ToString();
            DataTable dtCompany = Lo.RetriveMasterData(0, ObjEnc.DecryptData(sType), "", 0, "", "", "InterestedAreaVendor");
            if (dtCompany.Rows.Count > 0)
            {
                //  lblcomp.Text = "Company - " + dtCompany.Rows[0]["V_CompName"].ToString() + " , ";
                strMasterAlloted = dtCompany.Rows[0]["MasterAllowed"].ToString();
            }
        }
        if (strMasterAlloted != "")
        {
            bindMasterMenu(sType);
        }
    }
    #endregion
    protected void lbchngpassword_Click(object sender, EventArgs e)
    {
        Response.Redirect("https://srijandefence.gov.in/CreateVenPass?mcref=" + Session["VendorRefNo"].ToString() + "&curid=" + Resturl(25));
    }
    #region ReturnUrl Long"
    public string Resturl(int length)
    {
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        StringBuilder res = new StringBuilder();
        Random rnd = new Random();
        while (0 < length--)
        {
            res.Append(valid[rnd.Next(valid.Length)]);
        }
        return res.ToString();
    }
    #endregion
}
