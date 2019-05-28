using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Encryption;

public partial class Error : System.Web.UI.Page
{
    Cryptography Enc = new Cryptography();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["techerror"] != null && Request.QueryString["page"] != null)
            {
                divtechnicalerror.InnerText = Enc.DecryptData(Request.QueryString["techerror"].ToString());
                panerror.Visible = true;
            }
            else
            {
                panerror.Visible = false;
            }
        }
        catch (Exception ex)
        {
        }
    }
}