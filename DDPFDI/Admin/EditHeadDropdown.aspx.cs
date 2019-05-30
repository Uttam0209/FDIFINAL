using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Encryption;
using System.Data;
using System.Text;

public partial class Admin_EditHeadDropdown : System.Web.UI.Page
{
    Cryptography objEnc = new Cryptography();
    Logic Lo = new Logic();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
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
            bindnodal();
        }
    }

    protected void bindnodal()
    {
        DataTable DtGrid = Lo.GetDashboardData("Employee");
        if (DtGrid.Rows.Count > 0)
        {
            gvpass.DataSource = DtGrid;
            gvpass.DataBind();
        }
    }

    protected void btncomp_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", "SuccessfullPop('test')", true);
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "SuccessfullPop();", true);
        //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Message Display')", true);
        //string Type = "Company";
        //string RefNo = "C0008";
        //string Edit = "CEdit";
        //string stridNew = Request.QueryString["id"].ToString().Replace(" ", "+");
        //string mstrid = objEnc.EncryptData((objEnc.DecryptData(stridNew) + " >> Edit Test"));
        //Response.Redirect("Test?mlogrole=" + HttpUtility.UrlEncode(objEnc.EncryptData(Type.Trim())) + "&mrefno=" + HttpUtility.UrlEncode(objEnc.EncryptData(RefNo.Trim())) +
        //                  "&mEdit=" + HttpUtility.UrlEncode(objEnc.EncryptData(Edit)) + "&id=" + mstrid.ToString());

    }
    protected void btndivision_Click(object sender, EventArgs e)
    {
        string Type = "Division";
        string RefNo = "D0002";
        string Edit = "CEdit";
        string stridNew = Request.QueryString["id"].ToString().Replace(" ", "+");
        string mstrid = objEnc.EncryptData((objEnc.DecryptData(stridNew) + " >> Edit Test"));
        Response.Redirect("Test?mlogrole=" + HttpUtility.UrlEncode(objEnc.EncryptData(Type.Trim())) + "&mrefno=" + HttpUtility.UrlEncode(objEnc.EncryptData(RefNo.Trim())) +
                          "&mEdit=" + HttpUtility.UrlEncode(objEnc.EncryptData(Edit)) + "&id=" + mstrid.ToString());

    }
    protected void btnunit_Click(object sender, EventArgs e)
    {
        string Type = "Unit";
        string RefNo = "U0001";
        string Edit = "CEdit";
        string stridNew = Request.QueryString["id"].ToString().Replace(" ", "+");
        string mstrid = objEnc.EncryptData((objEnc.DecryptData(stridNew) + " >> Edit Test"));
        Response.Redirect("Test?mlogrole=" + HttpUtility.UrlEncode(objEnc.EncryptData(Type.Trim())) + "&mrefno=" + HttpUtility.UrlEncode(objEnc.EncryptData(RefNo.Trim())) +
                          "&mEdit=" + HttpUtility.UrlEncode(objEnc.EncryptData(Edit)) + "&id=" + mstrid.ToString());
    }

    protected void gvpass_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[3].Text != "")
            {
                if (e.Row.Cells[3].Text == "&nbsp;")
                {

                }
                else
                {
                    string str = objEnc.DecryptData(e.Row.Cells[3].Text.Replace(" ", "+"));
                    e.Row.Cells[3].Text = str;
                }
            }
        }
    }
}