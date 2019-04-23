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
    string strInterestedArea = "";
    string strMasterAlloted = "";
    string sType = "";
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
                strMenu.Append("<li class='parent-nav'><a href='#' data-original-title='Dashboard'><i class='fas fa-tachometer-alt'></i><span class='hidden-minibar'>" + row["InterestArea"].ToString() + " </span><span style='position:absolute; right:40px;top:10px;'>M" + row["Id"].ToString() + "</span> <i class='fas fa-angle-down'></i></a>");
               

                string[] Categ1 = dtArea.Rows[0]["MenuId"].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                string mCval1 = "";
                strMenu.Append("<ul class='parent-nav-child'>");
                for (int j = 0; j < Categ1.Length; j++)
                {
                    mCval1 = Categ1[j];
                    DataTable dtMenu = Lo.RetriveMasterData(0, "", lbltypelogin.Text, Convert.ToInt16(mCval1), "", "", "MenuMain"); 


                    foreach (DataRow row2 in dtMenu.Rows)
                    {
                        strMenu.Append("<li class='parent-nav'><a href='" + row2["MenuUrl"].ToString() + "?mu=" + ObjEnc.EncryptData(row2["Spanclass"].ToString()) + "&id=" + ObjEnc.EncryptData(row2["MenuName"].ToString()) + "' data-original-title='Dashboard'><i class='fas fa-tachometer-alt'></i><span class='hidden-minibar'>" + row2["MenuName"].ToString() + "</span><span style='position:absolute; right:40px;top:10px;'>C" + row2["MenuId"].ToString() + "</span>");
                         strMenu.Append("<i class='fas fa-angle-down'></i></a>");
                           DataTable Submenu = Lo.RetriveMasterData(0, "", lbltypelogin.Text, Convert.ToInt16(row2["MenuID"].ToString()), "", "", "SubMenu");
                        if (Submenu.Rows.Count > 0)
                        {
                            strMenu.Append("<ul class='parent-nav-child'>");
                            foreach (DataRow row1 in Submenu.Rows)
                            {
                                strMenu.Append("<li><a href='" + row1["MenuUrl"].ToString() + "?mu=" + ObjEnc.EncryptData(row1["Spanclass"].ToString()) + "&id=" + ObjEnc.EncryptData(row1["MenuName"].ToString()) + "'><i class='far fa-building'></i>" + row1["MenuName"].ToString() + "<span style='position:absolute; right:20px;top:10px;'>SC" + row1["MenuId"].ToString() + "</span></a></li> ");
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
            strMenu.Append("</ul");
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
                strMasterMenu.Append("<li class='parent-nav'><a href='#' data-original-title='Dashboard'><i class='fas fa-tachometer-alt'></i><span class='hidden-minibar'>" + row["InterestArea"].ToString() + "</span><span style='position:absolute; right:40px;top:10px;'>M" + row["Id"].ToString() + "</span>");
                strMasterMenu.Append("<i class='fas fa-angle-down'></i></a>");
                string[] MCateg1 = dtMArea.Rows[0]["MenuId"].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                string MmCval1 = "";
                strMasterMenu.Append("<ul class='parent-nav-child'>");
                for (int j = 0; j < MCateg1.Length; j++)
                {
                    MmCval1 = MCateg1[j];

                    DataTable dtMMenu = Lo.RetriveMasterData(0, "", lbltypelogin.Text, Convert.ToInt16(MmCval1), "", "", "MenuMain"); 



                    foreach (DataRow row2 in dtMMenu.Rows)
                    {
                        strMasterMenu.Append("<li class='parent-nav'><a href='" + row2["MenuUrl"].ToString() + "?mu=" + ObjEnc.EncryptData(row2["Spanclass"].ToString()) + "&id=" + ObjEnc.EncryptData(row2["MenuName"].ToString()) + "' ><i class='fas fa-tachometer-alt'></i><span class='hidden-minibar'>" + row2["MenuName"].ToString() + "</span><span style='position:absolute; right:40px;top:10px;'>C" + row2["MenuId"].ToString() + "</span>");
                        strMasterMenu.Append("<i class='fas fa-angle-down'></i></a>");
                        DataTable SubMmenu = Lo.RetriveMasterData(0, "", lbltypelogin.Text, Convert.ToInt16(row2["MenuID"].ToString()), "", "", "SubMenu");

                        if (SubMmenu.Rows.Count > 0)
                        {
                           
                            strMasterMenu.Append("<ul class='parent-nav-child'>");
                            foreach (DataRow row1 in SubMmenu.Rows)
                            {
                                strMasterMenu.Append("<li><a href='" + row1["MenuUrl"].ToString() + "?mu=" + ObjEnc.EncryptData(row1["Spanclass"].ToString()) + "&id=" + ObjEnc.EncryptData(row1["MenuName"].ToString()) + "'><i class='far fa-building'></i>" + row1["MenuName"].ToString() + "</span><span style='position:absolute; right:20px;top:10px;'>SC" + row1["MenuId"].ToString() + "</span></a></li>");
                            }
                            strMasterMenu.Append("</ul>");
                            strMasterMenu.Append("</li>");
                        }
                        strMasterMenu.Append("</li>");
                    }
                }
                strMasterMenu.Append("</ul>");
            }
            MasterMenu.InnerHtml = strMasterMenu.ToString();
            strMasterMenu.Append("</ul");
        }
    }
    #region Menu Wise Login
    protected void MenuLogin()
    {
        lbltypelogin.Text = ObjEnc.DecryptData(Session["Type"].ToString());
        lblusername.Text = ObjEnc.DecryptData(Session["User"].ToString());
        if (Session["CompanyRefNo"] != null)
        {
            if (Session["CompanyRefNo"].ToString().Substring(0, 1) == "F")
            {
                DataTable dtFactory = Lo.RetriveMasterData(0, Session["CompanyRefNo"].ToString(), "", 0, "", "", "FactoryName");
                if (dtFactory.Rows.Count > 0)
                {

                    lblfactory.Text = "Division/Plant - " + dtFactory.Rows[0]["FactoryName"].ToString();

                    sType = dtFactory.Rows[0]["CompanyRefNo"].ToString();
                }
            }
            else if (Session["CompanyRefNo"].ToString().Substring(0, 1) == "U")
            {
                DataTable dtUnit = Lo.RetriveMasterData(0, Session["CompanyRefNo"].ToString(), "", 0, "", "", "UnitName");
                if (dtUnit.Rows.Count > 0)
                {
                    lblfactory.Text = "Division/Plant - " + dtUnit.Rows[0]["FactoryName"].ToString();
                    lblunit.Text = "UnitName - " + dtUnit.Rows[0]["UnitName"].ToString();
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

                lblmastercompany.Text = "Company - " + dtCompany.Rows[0]["CompanyName"].ToString();

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
}
