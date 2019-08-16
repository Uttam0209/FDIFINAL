using System;
using System.Web;
using System.Web.UI;
using Encryption;
using BusinessLayer;
using System.Text;
using System.Data;
using System.Web.Security;
using System.Web.Helpers;

public partial class Admin_MasterPage : System.Web.UI.MasterPage
{
    Logic Lo = new Logic();
    Cryptography ObjEnc = new Cryptography();
    string strInterestedArea = "";
    string strMasterAlloted = "";
    string sType = "";
    private const string AntiXsrfTokenKey = "__AntiXsrfToken";
    private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    private string _antiXsrfTokenValue;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] != null)
        {
            if (!IsPostBack)
            {
                try
                {
                    MenuLogin();
                }
                catch (Exception exception)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Session Expired,Please login again');window.location='Login'", true);
                }
            }
        }
        else
        {
            Session.Clear();
            Session.Abandon();
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Session Expired,Please login again');window.location='Login'", true);
        }
    }
    protected void lbllogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
        Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
        Response.AppendHeader("Expires", "0"); // Proxies.
        Response.RedirectToRoute("Login");
    }
    private void bindMenu(string sType)
    {
        StringBuilder strMenu = new StringBuilder();
        strMenu.Append("<ul class='nav  nav-list'>");
        string[] Categ = strInterestedArea.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        string mCval = "";
        for (int x = 0; x < Categ.Length; x++)
        {
            mCval = Categ[x];
            DataTable dtArea = Lo.RetriveMasterData(Convert.ToInt64(mCval), sType, "", 0, "", "", "InterestedAreaMenuId");
            foreach (DataRow row in dtArea.Rows)
            {
                strMenu.Append("<li class='parent-nav'><a href='#'  title='" + row["Tooltip"].ToString() + "'><i class='fas fa-tachometer-alt'></i><span class='hidden-minibar'>" + row["InterestArea"].ToString() + " </span><span class='menuNo' style='position:absolute; right:40px;top:13px;'>M" + row["Id"].ToString() + "</span> <i class='fas fa-angle-down'></i></a>");
                string[] Categ1 = dtArea.Rows[0]["MenuId"].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                string mCval1 = "";
                strMenu.Append("<ul class='parent-nav-child'>");
                for (int j = 0; j < Categ1.Length; j++)
                {
                    mCval1 = Categ1[j];
                    DataTable dtMenu = Lo.RetriveMasterData(0, "", ObjEnc.DecryptData(Session["Type"].ToString()), Convert.ToInt16(mCval1), "", "", "MenuMain");
                    foreach (DataRow row2 in dtMenu.Rows)
                    {
                        strMenu.Append("<li class='parent-nav'><a href='" + row2["MenuUrl"].ToString() + "?mu=" + HttpUtility.UrlEncode(ObjEnc.EncryptData(row2["Spanclass"].ToString())) + "&id=" + HttpUtility.UrlEncode(ObjEnc.EncryptData(row["InterestArea"].ToString() + " >> " + row2["MenuName"].ToString())) + "' title='" + row2["Tooltip"].ToString() + "'><i class='fas fa-tachometer-alt'></i><span class='hidden-minibar'>" + row2["MenuName"].ToString() + "</span><span class='menuNo' style='position:absolute; right:40px;top:13px;'>C" + row2["MenuId"].ToString() + "</span>");
                        strMenu.Append("<i class='fas fa-angle-down'></i></a>");
                        DataTable Submenu = Lo.RetriveMasterData(0, "", ObjEnc.DecryptData(Session["Type"].ToString()), Convert.ToInt16(row2["MenuID"].ToString()), "", "", "SubMenu");
                        if (Submenu.Rows.Count > 0)
                        {
                            strMenu.Append("<ul class='parent-nav-child'>");
                            foreach (DataRow row1 in Submenu.Rows)
                            {
                                strMenu.Append("<li><a href='" + row1["MenuUrl"].ToString() + "?mu=" + HttpUtility.UrlEncode(ObjEnc.EncryptData(row1["Spanclass"].ToString())) + "&id=" + HttpUtility.UrlEncode(ObjEnc.EncryptData(row["InterestArea"].ToString() + " >> " + row2["MenuName"].ToString() + " >> " + row1["MenuName"].ToString())) + "' title='" + row1["Tooltip"].ToString() + "'><i class='far fa-building'></i><span class='hidden-minibar'>" + row1["MenuName"].ToString() + "</span><span class='menuNo' style='position:absolute; right:20px;top:13px;'>L" + row1["MenuId"].ToString() + "</span></a></li> ");
                            }
                            strMenu.Append("</ul>");

                        }
                        strMenu.Append("</li>");
                    }
                }
                strMenu.Append("</ul>");
                strMenu.Append("</li>");
            }
            menu.InnerHtml = strMenu.ToString();
            strMenu.Append("</ul>");
        }
    }
    private void bindMasterMenu(string sType)
    {
        StringBuilder strMasterMenu = new StringBuilder();
        strMasterMenu.Append("<ul class='nav  nav-list'>");
        string[] MCateg = strMasterAlloted.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        string MmCval = "";
        for (int x = 0; x < MCateg.Length; x++)
        {
            MmCval = MCateg[x];
            DataTable dtMArea = Lo.RetriveMasterData(Convert.ToInt64(MmCval), sType, "", 0, "", "", "InterestedAreaMenuId");
            foreach (DataRow row in dtMArea.Rows)
            {
                strMasterMenu.Append("<li class='parent-nav'><a href='#'  title='" + row["Tooltip"].ToString() + "'><i class='fas fa-tachometer-alt'></i><span class='hidden-minibar'>" + row["InterestArea"].ToString() + " </span><span class='menuNo' style='position:absolute; right:40px;top:13px;'>M" + row["Id"].ToString() + "</span> <i class='fas fa-angle-down'></i></a>");
                string[] MCateg1 = dtMArea.Rows[0]["MenuId"].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                string MmCval1 = "";
                strMasterMenu.Append("<ul class='parent-nav-child'>");
                for (int j = 0; j < MCateg1.Length; j++)
                {
                    MmCval1 = MCateg1[j];
                    DataTable dtMMenu = Lo.RetriveMasterData(0, "", ObjEnc.DecryptData(Session["Type"].ToString()), Convert.ToInt16(MmCval1), "", "", "MenuMain");
                    foreach (DataRow row2 in dtMMenu.Rows)
                    {
                        strMasterMenu.Append("<li class='parent-nav'><a href='" + row2["MenuUrl"].ToString() + "?mu=" + ObjEnc.EncryptData(row2["Spanclass"].ToString()) + "&id=" + ObjEnc.EncryptData(row["InterestArea"].ToString() + " >> " + row2["MenuName"].ToString()) + "' title='" + row2["Tooltip"].ToString() + "'><i class='fas fa-tachometer-alt'></i><span class='hidden-minibar'>" + row2["MenuName"].ToString() + "</span><span class='menuNo' style='position:absolute; right:40px;top:13px;'>C" + row2["MenuId"].ToString() + "</span>");
                        strMasterMenu.Append("<i class='fas fa-angle-down'></i></a>");
                        DataTable SubMmenu = Lo.RetriveMasterData(0, "", ObjEnc.DecryptData(Session["Type"].ToString()), Convert.ToInt16(row2["MenuID"].ToString()), "", "", "SubMenu");
                        if (SubMmenu.Rows.Count > 0)
                        {
                            strMasterMenu.Append("<ul class='parent-nav-child'>");
                            foreach (DataRow row1 in SubMmenu.Rows)
                            {
                                strMasterMenu.Append("<li><a href='" + row1["MenuUrl"].ToString() + "?mu=" + ObjEnc.EncryptData(row1["Spanclass"].ToString()) + "&id=" + ObjEnc.EncryptData(row["InterestArea"].ToString() + " >> " + row2["MenuName"].ToString() + " >> " + row1["MenuName"].ToString()) + "' title='" + row1["Tooltip"].ToString() + "'><i class='far fa-building'></i><span class='hidden-minibar'>" + row1["MenuName"].ToString() + "</span><span class='menuNo' style='position:absolute; right:20px;top:13px;'>L" + row1["MenuId"].ToString() + "</span></a></li> ");
                            }
                            strMasterMenu.Append("</ul>");
                        }
                        strMasterMenu.Append("</li>");
                    }
                }
                strMasterMenu.Append("</ul>");
                strMasterMenu.Append("</li>");
            }
        }
        MasterMenu.InnerHtml = strMasterMenu.ToString();
        strMasterMenu.Append("</ul>");
    }
    #region Menu Wise Login
    protected void MenuLogin()
    {
        if (ObjEnc.DecryptData(Session["Type"].ToString()) == "Factory")
        {
            lbltypelogin.Text = "Division";
        }
        else
        {
            lbltypelogin.Text = ObjEnc.DecryptData(Session["Type"].ToString());
        }
        lblusername.Text = ObjEnc.DecryptData(Session["User"].ToString());
        if (Session["CompanyRefNo"] != null)
        {
            if (lbltypelogin.Text == "Division")
            {
                DataTable dtFactory = Lo.RetriveMasterData(0, Session["CompanyRefNo"].ToString(), "", 0, "", "", "FactoryJoin");
                if (dtFactory.Rows.Count > 0)
                {
                    DivCompanyName.Visible = false;
                    lblfactory.Text = "Division/Plant - " + dtFactory.Rows[0]["FactoryName"].ToString() + " ,";

                    sType = dtFactory.Rows[0]["CompanyRefNo"].ToString();
                }
            }
            else if (lbltypelogin.Text == "Unit")
            {
                DataTable dtUnit = Lo.RetriveMasterData(0, Session["CompanyRefNo"].ToString(), "", 0, "", "", "UnitJoin");
                if (dtUnit.Rows.Count > 0)
                {
                    DivCompanyName.Visible = false;
                    lblfactory.Text = "Division/Plant - " + dtUnit.Rows[0]["FactoryName"].ToString() + ", ";
                    lblunit.Text = "Unit - " + dtUnit.Rows[0]["UnitName"].ToString() + " , ";
                    sType = dtUnit.Rows[0]["CompanyRefNo"].ToString();
                }
            }
            else
            {
                sType = Session["CompanyRefNo"].ToString();
            }
            DataTable dtCompany = Lo.RetriveMasterData(0, sType, "", 0, "", "", "InterestedArea");
            if (dtCompany.Rows.Count > 0)
            {
                if (lbltypelogin.Text == "SuperAdmin")
                {
                    DivCompanyName.Visible = false;
                }
                else if (lbltypelogin.Text == "Admin")
                {
                    DivCompanyName.Visible = false;
                }
                else
                {

                    DivCompanyName.Visible = true;
                    lblmastercompany.Text = "Company - " + dtCompany.Rows[0]["CompanyName"].ToString() + " , ";
                }
                strInterestedArea = dtCompany.Rows[0]["InterestedArea"].ToString();
                strMasterAlloted = dtCompany.Rows[0]["MasterAllowed"].ToString();
            }
        }
        if (strInterestedArea != "")
        {
            bindMenu(sType);
        }
        if (strMasterAlloted != "")
        {
            bindMasterMenu(sType);
        }
    }
    #endregion
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
    void master_Page_PreLoad(object sender, EventArgs e)
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
}
