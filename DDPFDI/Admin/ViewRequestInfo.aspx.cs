using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;
using Encryption;
public partial class Admin_ViewRequestInfo : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Cryptography Enc = new Cryptography();
    DataUtility Co = new DataUtility();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { BindRequest(); }
    }
    protected void BindRequest()
    {
        DataTable DtReq = Lo.RetriveGridViewCompany("", "", "", "retdata");
        if (DtReq.Rows.Count > 0)
        {
            dlrequest.DataSource = DtReq;
            dlrequest.DataBind();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "ErrorMssgPopup('No Record Found')", true);
        }
    }
    protected void gvRequestInfo_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void dlrequest_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            string mdate = e.CommandArgument.ToString();
            DataTable dtgetProd = Lo.RetriveGridViewCompany(mdate.ToString(), "","","RetReqProd");
            if (dtgetProd.Rows.Count > 0)
            {
                gvViewNodalOfficerAdd.DataSource = dtgetProd;
                gvViewNodalOfficerAdd.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "ProductCompany", "showPopup();", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "ErrorMssgPopup('No Record Found')", true);
            }

        }
    }
}