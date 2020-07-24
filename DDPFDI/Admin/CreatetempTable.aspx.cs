using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using Encryption;

public partial class Admin_CreatetempTable : System.Web.UI.Page
{
    Logic Lo = new Logic();
    private Cryptography objEnc = new Cryptography();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] != null)
        {
            if (!IsPostBack)
            {
                string user = objEnc.DecryptData(Session["Type"].ToString().Trim());
                if (user.ToString() == "SuperAdmin" || user.ToString() == "Admin")
                { }
                else
                { Response.Redirect("Login"); }
            }
        }
        else
        { Response.Redirect("Login"); }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            string id = Lo.UpdateStatus(0, "", "Updatecodeproduct");
            if (id != "-1")
            {
                DataTable dt = Lo.RetriveFilterCode("", "", "tryGetUpdatecode");
                if (dt.Rows.Count != 0)
                {
                    lbl.Text = "Total rows update :- " + dt.Rows[0]["Total"].ToString();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Oops some error occurs')", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Oops some error occurs')", true);
        }
    }
}