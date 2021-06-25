using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Encryption;
using BusinessLayer;
using DataAccessLayer;
using System.Text;
using System.Collections.Specialized;
using System.IO;

public partial class Admin_CompaniesFeedback : System.Web.UI.Page
{
    Cryptography objCrypto = new Cryptography();

    Logic Lo = new Logic();
    //private Cryptography Encrypt = new Cryptography();
    HybridDictionary hySave = new HybridDictionary();
    private string _msg = string.Empty;
    private string _sysMsg = string.Empty;
    private DataTable DtGrid = new DataTable();
    DataUtility Co = new DataUtility();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
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
            if ((objCrypto.DecryptData(Session["Type"].ToString()) == "Admin") || (objCrypto.DecryptData(Session["Type"].ToString()) == "SuperAdmin") || (objCrypto.DecryptData(Session["Type"].ToString()) == "Company"))
            {
                BindCompaniesFeedback();
            }
            else
            {
                Response.RedirectToRoute("ProductList");
            }
        }
    }
    public void BindCompaniesFeedback()
    {
        DataTable dtfeedback = new DataTable();
        if (objCrypto.DecryptData(Session["Type"].ToString()) == "Company")
        {
            dtfeedback = Lo.RetriveFilterCode(Session["CompanyRefNo"].ToString(), "", "GetCompaniesFeedback");
            gvcompanyfeedback.Columns[7].Visible = false;
        }
        else
        {
            dtfeedback = Lo.RetriveFilterCode("", "", "GetCompaniesFeedback");
            gvcompanyfeedback.Columns[7].Visible = true;
        }
        if (dtfeedback.Rows.Count > 0)
        {
            gvcompanyfeedback.DataSource = dtfeedback;
            gvcompanyfeedback.DataBind();
            txtrefno.Text = dtfeedback.Rows[0]["FeedBackRefNo"].ToString();
            TxtBxFirstNm.Text = dtfeedback.Rows[0]["UserName"].ToString();
            txtusermsg.Text = dtfeedback.Rows[0]["Message"].ToString();
            Txtemail.Text = dtfeedback.Rows[0]["UserEmail"].ToString();
            txtcompname.Text = dtfeedback.Rows[0]["CompanyName"].ToString();

        }
    }
    public void SendEmailCode(string email)
    {
        try
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/FeedbackReply.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Name}", TxtBxFirstNm.Text);
            body = body.Replace("{CompanyName}", txtcompname.Text);
            body = body.Replace("{YourFeedback}", txtusermsg.Text);
            body = body.Replace("{CompanyReply}", TxtBxDesc.Text);

            SendMail s;
            s = new SendMail();
            s.CreateMail(Session["User"].ToString(), Txtemail.Text, "Feedback Revert", body);
            s.sendMail();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Save();


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }

    protected void Save()
    {
        try
        {
            //hySave["FeedBackRefNo"] = txtrefno.Text;
            hySave["R_UserName"] = txtcompname.Text;
            hySave["R_Email"] = txtcompemail.Text;
            hySave["R_Messg"] = TxtBxDesc.Text;
            hySave["S_Email"] = Txtemail.Text;
            hySave["S_Name"] = TxtBxFirstNm.Text;
            hySave["S_Question"] = txtusermsg.Text;

            string save = Lo.InsertFeedbackReply(hySave, out _sysMsg, out _msg);
            //SendEmailCode(Txtemail.Text);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Send Successfully!!!')", true);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);

        }
    }
    protected void gvcompanyfeedback_RowCreated(object sender, GridViewRowEventArgs e)
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
    protected void gvcompanyfeedback_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            txtrefno.Text = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "reply", "showPopup();", true);

        }
    }
}
