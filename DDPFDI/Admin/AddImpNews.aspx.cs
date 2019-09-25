using System;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Text;
using BusinessLayer;
using Encryption;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using System.Web.Helpers;
using System.Net.Http;

public partial class Admin_AddImpNews : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Cryptography Enc = new Cryptography();
    DataUtility Co = new DataUtility();
    private Int64 id = 0;
    private string _sysMsg = string.Empty;
    private string _msg = string.Empty;
    HybridDictionary HySave = new HybridDictionary();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] != null)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["id"] != null)
                    {
                        string strid = Request.QueryString["id"].ToString().Replace(" ", "+");
                        string strPageName = Enc.DecryptData(strid);
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
                        BindNews();
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();
                    string Page = Request.Url.AbsolutePath.ToString();
                    Response.Redirect("Error?techerror=" + HttpUtility.UrlEncode(Enc.EncryptData(error)) + "&page=" + HttpUtility.UrlEncode(Enc.EncryptData(Page)));
                }
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert",
                "ErrorMssgPopup('Session Expired,Please login again');window.location='Login'", true);
        }
    }
    protected void SaveComp()
    {
        try
        {
            HySave["NewsId"] = 0;
            HySave["News"] = txtnews.Text.Trim();
            DateTime Date = Convert.ToDateTime(txtdate.Text.Trim());
            string mdate = Date.ToString("MM/dd/yyyy hh:mm tt");
            HySave["Date"] = mdate.ToString();
            HySave["Pages"] = txtpages.Text.Trim();
            string StrSaveComp = Lo.SaveImpNews(HySave, out _sysMsg, out _msg);
            if (_sysMsg != "")
            {
                BindNews();

                txtnews.Text = "";
                txtdate.Text = "";
                txtpages.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "SuccessfullPop('Save successfully !')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Record not saved.')", true);
            }
        }
        catch (Exception ex)
        {
            string error = ex.ToString();
            string Page = Request.Url.AbsolutePath.ToString();
            Response.Redirect("Error?techerror=" + HttpUtility.UrlEncode(Enc.EncryptData(error)) + "&page=" + HttpUtility.UrlEncode(Enc.EncryptData(Page)));
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtdate.Text != "" && txtnews.Text != "" && txtpages.Text != "")
            {
                SaveComp();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Field fill mandatory.')", true);
            }
        }
        catch (Exception rx)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('" + rx.Message + "')", true);
        }
    }
    protected void gvnewsadd_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void BindNews()
    {
        DataTable DtCountry = Lo.RetriveCountry(0, "ImpNews");
        if (DtCountry.Rows.Count > 0)
        {
            gvnewsadd.DataSource = DtCountry;
            gvnewsadd.DataBind();
            gvnewsadd.Visible = true;
        }
        else
        {
            gvnewsadd.Visible = false;
        }
    }
}