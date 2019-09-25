using Encryption;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Web;
using System.Web.Helpers;
using BusinessLayer;

public partial class Admin_ActiveLogin : System.Web.UI.Page
{
    Cryptography objEnc = new Cryptography();
    DataUtility Co = new DataUtility();
    Logic Lo = new Logic();
    private string _sysMsg = string.Empty;
    private string _msg = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Type"] != null)
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
                        if (MmCval == " View ")
                        {
                            MmCval = "Add";
                        }
                        strheadPage.Append("<li class=''><span>" + MmCval + "</span></li>");
                    }
                    divHeadPage.InnerHtml = strheadPage.ToString();
                    strheadPage.Append("</ul");
                    ViewState["UserLoginEmail"] = objEnc.DecryptData(Session["User"].ToString());
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "ErrorMssgPopup('Session Expired,Please login again');window.location='Login'", true);
        }
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        if (txtsearch.Text != "")
        {
            DataTable DtGretUser = Lo.RetriveCountry(0, txtsearch.Text, "logmanup");
            if (DtGretUser.Rows.Count > 0)
            {
                gvViewNodalOfficerAdd.DataSource = DtGretUser;
                gvViewNodalOfficerAdd.DataBind();
                gvViewNodalOfficerAdd.Visible = true;
            }
            else
            {
                gvViewNodalOfficerAdd.Visible = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('No record found')", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Please enter user email')", true);
        }
    }
    protected void gvViewNodalOfficerAdd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "active")
        {
            string UpdateStatus = Lo.UpdateStatus(Convert.ToInt64(e.CommandArgument.ToString()), "", "uplogmanup");
            if (UpdateStatus == "true")
            {
                gvViewNodalOfficerAdd.Visible = false;
                txtsearch.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Succssfully updated.')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Error occure try again')", true);
            }
        }
    } 
    protected void gvViewNodalOfficerAdd_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TableSection = TableRowSection.TableBody;
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.TableSection = TableRowSection.TableFooter;
        }
    }
}
