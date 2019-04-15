using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Encryption;
using BusinessLayer;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Data;
using System.Data.Sql;

public partial class Admin_MasterPage : System.Web.UI.MasterPage
{
    Logic Lo = new Logic();
    Cryptography ObjEnc = new Cryptography();
    string type = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] != null)
        {
            MenuLogin();
        }
        else
        {
            Response.RedirectToRoute("Login");
        }
    }
    protected void lbllogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.RedirectToRoute("Login");
    }
    private void bindMenu()
    {
        StringBuilder strMenu = new StringBuilder();
        DataTable dtMenu = Lo.RetriveCompany("MenuMain", 0, type);
        if (dtMenu.Rows.Count > 0)
        {
            strMenu.Append("<ul class='nav  nav-list'>");
            strMenu.Append(" <li class='parent-nav'><a href='javascript:void(0)'><i class='fas fa-user-tie'></i><span class='hidden-minibar'>" + type + "</span><i class='fas fa-angle-down'></i></a>");
            strMenu.Append("<ul class='parent-nav-child'>");
            foreach (DataRow row in dtMenu.Rows)
            {
                strMenu.Append("<li class='parent-nav'><a href='" + row["MenuUrl"].ToString() + "' data-original-title='Dashboard'><i class='fas fa-tachometer-alt'></i><span class='hidden-minibar'>" + row["MenuName"].ToString() + "</span>");
                DataTable Submenu = Lo.RetriveCompany1("SubMenu", row["MenuRefNo"].ToString(), type);
                if (Submenu.Rows.Count > 0)
                {
                    //strMenu.Append("<li class='parent-nav'>");
                    //strMenu.Append(" <a href='#'><i class='fas fa-user-tie'></i><span class='hidden-minibar'>" + row["MenuName"].ToString() + " </span><i class='fas fa-angle-down'></i></a>");
                    //strMenu.Append("<i class='fas fa-angle-down'></i></a>");
                    strMenu.Append("<i class='fas fa-angle-down'></i></a>");
                    strMenu.Append("<ul class='parent-nav'>");
                    foreach (DataRow row1 in Submenu.Rows)
                    {

                        strMenu.Append("<li><a href='" + row1["MenuUrl"].ToString() + "'><i class='far fa-building'></i>" + row1["MenuName"].ToString() + "</a></li>");

                    }
                    strMenu.Append("</ul>");
                    
                   
                }
                
                strMenu.Append("</li>");
                me.InnerHtml = strMenu.ToString();
            }
            
            strMenu.Append("</ul>");
            strMenu.Append(" </li> ");
            strMenu.Append("</ul");
        }
    }
    #region Menu Wise Login
    protected void MenuLogin()
    {
        type = ObjEnc.DecryptData(Session["Type"].ToString());
        lblusername.Text = ObjEnc.DecryptData(Session["User"].ToString());
        bindMenu();
    }
    #endregion
}
