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
    DataTable dtMenu = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] != null)
        {
            lblAcessType.Text = ObjEnc.DecryptData(Session["Type"].ToString());
            lblusername.Text = ObjEnc.DecryptData(Session["User"].ToString());
            bindMenu();
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
    protected void bindMenu()
    {

        dtMenu = Lo.RetriveCompany("MenuMain", 0, "");
        if (dtMenu.Rows.Count > 0)
        {
            DataView view = new DataView(dtMenu);
            view.RowFilter = "ParentMenuRefNo='M00'";
            this.rptCategories.DataSource = view;
            this.rptCategories.DataBind();
        }
    }
    protected void rptMenu_OnItemBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (dtMenu != null)
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                string ID = drv["MenuRefNo"].ToString();
                string Title = drv["MenuName"].ToString();
                string Class = drv["Class"].ToString();
                string Fontawsomeclass = drv["Fontawsomeclass"].ToString();
                string Spanclass = drv["Spanclass"].ToString();
                DataRow[] rows = dtMenu.Select("ParentMenuRefNo='" + ID + "'");
                if (rows.Length > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<li class='parent-nav'>");
                    sb.Append("<a href='#'><i class=" + Fontawsomeclass + "></i><span class='hidden-minibar'>" + Title + " </span><i class='fas fa-angle-down'></i></a>");
                    sb.Append("<ul id='" + Title + "' class='parent-nav-child'>");
                    foreach (var item in rows)
                    {
                        sb.Append("<li><a href='" + item["MenuUrl"] + "'>");
                        sb.Append("<i class='" + Fontawsomeclass + "' >");
                        sb.Append("</i>");
                        sb.Append(item["MenuName"]);
                        sb.Append("</a>");
                        sb.Append("</li>");
                    }
                    sb.Append("</ul>");
                    sb.Append("</li>");
                    (e.Item.FindControl("ltrlSubMenu") as Literal).Text = sb.ToString();
                }
            }
        }
    }
}
