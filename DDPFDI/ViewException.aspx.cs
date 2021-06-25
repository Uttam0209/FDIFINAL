using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;

public partial class ViewException : System.Web.UI.Page
{
    Logic Lo = new Logic();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            DataTable dt = Lo.NewRetriveFilterCode("Excerption", "", "", "", "", 0, 0, 0);
            if(dt.Rows.Count>0)
            {
                gverror.DataSource = dt;
                gverror.DataBind();
            }
        }
    }
}