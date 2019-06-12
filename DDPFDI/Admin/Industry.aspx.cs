using BusinessLayer;
using Encryption;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Industry : System.Web.UI.Page
{
    private Logic Lo = new Logic();
    private Cryptography objCrypto = new Cryptography();
    private string currentPage = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Type"] != null)
        {
            try
            {
                if (Request.QueryString["id"] != null)
                {
                    string strid = Request.QueryString["id"].ToString().Replace(" ", "+");
                    string strPageName = objCrypto.DecryptData(strid);
                    StringBuilder strheadPage = new StringBuilder();
                    strheadPage.Append("<ul class='breadcrumb'>");
                    string[] MCateg = strPageName.Split(new string[] { ">>" }, StringSplitOptions.RemoveEmptyEntries);
                    string MmCval = "";
                    for (int x = 0; x < MCateg.Length; x++)
                    {
                        MmCval = MCateg[x];
                        strheadPage.Append("<li class=''><span>" + MmCval + "</span></li>");
                    }
                    divHeadPage.InnerHtml = strheadPage.ToString();
                    strheadPage.Append("</ul");
                }
                currentPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                string Page = Request.Url.AbsolutePath.ToString();
                Response.Redirect("Error?techerror=" + HttpUtility.UrlEncode(objCrypto.EncryptData(error)) + "&page=" + HttpUtility.UrlEncode(objCrypto.EncryptData(Page)));
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert",
                "alert('Session Expired,Please login again');window.location='Login'", true);
        }
    }
}