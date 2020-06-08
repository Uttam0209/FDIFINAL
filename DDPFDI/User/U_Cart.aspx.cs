using BusinessLayer;
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
using System.Text.RegularExpressions;

public partial class User_U_Cart : System.Web.UI.Page
{
    #region Pagevariable
    private Logic Lo = new Logic();
    private DataTable DtGrid = new DataTable();
    private string _msg = string.Empty;
    private string _sysMsg = string.Empty;
    DataUtility Co = new DataUtility();
    private Cryptography Encrypt = new Cryptography();
    private PagedDataSource pgsource = new PagedDataSource();
    DataTable dtCart = new DataTable();
    DataRow dr;
    string mval = "";
    #endregion
    protected void UpdateDtGridValue(DataTable DtGrid)
    {
        for (int a = 0; a < DtGrid.Rows.Count; a++)
        {
            if (DtGrid.Rows[a]["UCompany"].ToString() != "")
            {
                DtGrid.Rows[a]["CompanyName"] = DtGrid.Rows[a]["UCompany"];
                DtGrid.Rows[a]["FactoryName"] = DtGrid.Rows[a]["UFactory"];
                DtGrid.Rows[a]["CompanyRefNo"] = DtGrid.Rows[a]["UCompRefNo"];
                DtGrid.Rows[a]["FactoryRefNo"] = DtGrid.Rows[a]["UFactoryRefNo"];
            }
            else if (DtGrid.Rows[a]["FCompany"].ToString() != "")
            {
                DtGrid.Rows[a]["CompanyName"] = DtGrid.Rows[a]["FCompany"];
                DtGrid.Rows[a]["CompanyRefNo"] = DtGrid.Rows[a]["FCompRefNo"];
            }
        }
    }
    protected void PageLoad()
    {
        if (Session["DCart"] != null)
        {
            dtCart = (DataTable)Session["DCart"];
            for (int i = 0; dtCart.Rows.Count > i; i++)
            {
                mval = mval + "," + "'" + dtCart.Rows[i]["ProductRefNo"].ToString() + "'";
            }
            string smval = mval.Substring(1);
            ViewState["RefN"] = smval;
            dtCart = Lo.RetriveCart(smval.ToString());
            if (dtCart.Rows.Count > 0)
            {
                UpdateDtGridValue(DtGrid);
                dtCart.Columns.Add("TopImages", typeof(string));
                for (int i = 0; dtCart.Rows.Count > i; i++)
                {
                    string mProdRefTime = dtCart.Rows[i]["ProductRefNo"].ToString();
                    DataTable dtImageBind4 = Lo.RetriveProductCode("", mProdRefTime, "RetImageTop", "");
                    if (dtImageBind4.Rows.Count > 0)
                    {
                        dtCart.Rows[i]["TopImages"] = dtImageBind4.Rows[0]["ImageName"].ToString();
                    }
                    else
                    {
                        dtCart.Rows[i]["TopImages"] = "/assets/images/Noimage.png";
                    }
                }
                dlCartProd.DataSource = dtCart;
                dlCartProd.DataBind();
                lbltotalprodincart.Text = "You have " + dtCart.Rows.Count + " products in your cart";
                totalno.InnerText = dtCart.Rows.Count.ToString();
            }
            else
            {

            }
        }
        else
        {
            Response.Redirect("UProductList");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PageLoad();
        }
    }
    protected void lbclaercart_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("Uproductlist");
    }
    protected void btnsendmail_Click(object sender, EventArgs e)
    {
        if (txtname.Text != "" && txtemail.Text != "")
        {
            if (VerifyEmailID(txtemail.Text) != false)
            {
                SendEmailCode(txtemail.Text, ViewState["RefN"].ToString());
                SendEmailCodeNodalOfficer(ViewState["RefN"].ToString());
                txtemail.Text = "";
                txtname.Text = "";
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Please enter your name or email')", true);
        }
    }
    public static bool VerifyEmailID(string email)
    {
        string expresion;
        expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        if (Regex.IsMatch(email, expresion))
        {
            if (Regex.Replace(email, expresion, string.Empty).Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    public void SendEmailCode(string empid, string id)
    {
        try
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/CartDetail.html")))
            {
                body = reader.ReadToEnd();
            }
            DataTable dtMail = Lo.RetriveMailCart(id);
            StringBuilder html = new StringBuilder();
            html.Append("<table border = '1'>");
            html.Append("<tr>");
            foreach (DataColumn column in dtMail.Columns)
            {
                html.Append("<th>");
                html.Append(column.ColumnName);
                html.Append("</th>");
            }
            html.Append("</tr>");
            {
                for (int m = 0; dtMail.Rows.Count > m; m++)
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dtMail.Columns)
                    {
                        html.Append("<td>");
                        html.Append(dtMail.Rows[m][column.ColumnName]);
                        html.Append("</td>");
                    }
                    html.Append("</tr>");
                }
            }
            html.Append("</table>");
            body = body.Replace("{dt}", html.ToString());
            SendMail s;
            s = new SendMail();
            s.CreateMail("noreply-srijandefence@gov.in", empid, "Indinization Cart Product Info", body);
            s.sendMail();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Thankyou for your intrest in these product.We will revert you soon')", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('" + ex.Message + "')", true);
        }

    }
    public void SendEmailCodeNodalOfficer(string id)
    {
        try
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/NodalInfoDetailCart.html")))
            {
                body = reader.ReadToEnd();
            }
            DataTable dtMail = Lo.RetriveMailCart(id);
            StringBuilder html = new StringBuilder();
            html.Append("<table border = '1'>");
            html.Append("<tr>");
            foreach (DataColumn column in dtMail.Columns)
            {
                html.Append("<th>");
                html.Append(column.ColumnName);
                html.Append("</th>");
            }
            html.Append("</tr>");
            {
                for (int m = 0; dtMail.Rows.Count > m;m++ )
                {
                    html.Append("<tr>");
                    foreach (DataColumn column in dtMail.Columns)
                    {
                        html.Append("<td>");
                        html.Append(dtMail.Rows[m][column.ColumnName]);
                        html.Append("</td>");
                    }
                    html.Append("</tr>");
                }
            }
            html.Append("</table>");
            body = body.Replace("{dt}", html.ToString());
            body = body.Replace("{name}", txtname.Text.Trim());
            body = body.Replace("{email}", txtemail.Text.Trim());
            SendMail s;
            s = new SendMail();
            for (int n = 0; dtMail.Rows.Count > n; n++)
            {
                if (dtMail.Rows[n]["NodalOfficerEmail"].ToString() != "")
                {
                    s.CreateMail("noreply-srijandefence@gov.in", dtMail.Rows[n]["NodalOfficerEmail"].ToString(), "Indinization Product Info", body);
                }
                else
                {
                    s.CreateMail("noreply-srijandefence@gov.in", "shrishkumar.ofb@ofb.gov.in", "Indinization Product Info", body);
                }
            }
            s.sendMail();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('" + ex.Message + "')", true);
        }
    }
    protected void dlCartProd_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "removecart")
        {
            DataTable dtremove = (DataTable)Session["DCart"];
            for (int i = 0; dtremove.Rows.Count > i; i++)
            {
                DataRow dr = dtremove.Rows[i];
                if (dr["ProductRefNo"].ToString() == e.CommandArgument.ToString())
                    dtremove.Rows.Remove(dr);
            }
            dtremove.AcceptChanges();
            if (dtremove.Rows.Count > 0)
            {
                Session["DCart"] = dtremove;
                PageLoad();
            }
            else
            {
                Session["DCart"] = null;
                Session.Abandon();
                Session.Clear();
                Response.Redirect("UProductList");
            }

        }
    }
}