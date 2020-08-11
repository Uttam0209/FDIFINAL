using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using Encryption;
using System.IO;
using System.Text;

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
                    DataTable mdt = Lo.RetriveFilterCode(objEnc.DecryptData(Session["User"].ToString()).Trim(), dt.Rows[0]["Total"].ToString(), "updatestatus");                  
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Oops some error occurs')", true);                   
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Oops some error occurs (Timeout error)')", true);                
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Oops some error occurs')", true);
        }
    }
    protected void lbldownloadexcel_Click(object sender, EventArgs e)
    {
        DataTable dt = Lo.RetriveFilterCode("", "", "ALLpRODUCT");
        if (dt.Rows.Count != 0)
        {
            Exportexcel(dt);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Oops some error occurs.')", true);
            lbldownloadexcel.Visible = false;
        }
    }
    public void Exportexcel(DataTable dt)
    {
        try
        {
            int[] iColumns = { 0,2,4,5,10,11,14,15,16,17,18,20,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55};
            RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");
            objExport.ExportDetails(dt, iColumns, RKLib.ExportData.Export.ExportFormat.Excel, "ProductList.xls");
        }
        catch (Exception ex)
        {

        }

    }
}
