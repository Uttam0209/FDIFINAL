using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Encryption;
using System.Text;

public partial class Admin_GeneralInformation : System.Web.UI.Page
{
    Cryptography objEnc = new Cryptography();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            string strid = Request.QueryString["id"].ToString().Replace(" ", "+");
            string strPageName = objEnc.DecryptData(strid);
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
    }
}