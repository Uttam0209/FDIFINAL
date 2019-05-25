using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Error : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["techerror"] != null && Request.QueryString["usererror"] != null &&
                Request.QueryString["page"] != null)
            {
                divtechnicalerror.InnerText = Request.QueryString["techerror"].ToString();
                divusererror.InnerText = Request.QueryString["usererror"].ToString();
                lblpagename.Text = Request.QueryString["page"].ToString();
                panerror.Visible = true;
            }
            else
            {
                panerror.Visible = false;
            }
        }
        catch (Exception ex)
        { ex.Message}
    }
}