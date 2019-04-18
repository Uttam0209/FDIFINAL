using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using BusinessLayer;
using Encryption;

public partial class Admin_AddMasterCompany : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Cryptography Enc = new Cryptography();
    DataUtility Co = new DataUtility();
    private string _sysMsg = string.Empty;
    private string _msg = string.Empty;
    private string comprefno = "";
    private string intrestedare = "";
    private string Masterallowed = "";
    private string role = "";
    HybridDictionary hysavecomp = new HybridDictionary();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindMasterData();
        }
    }
    protected void BindMasterData()
    {
        DataTable Dtchkintrestedarea = Lo.RetriveCompany("IntrestedAreaCheck", 0, "I", 0);
        if (Dtchkintrestedarea != null)
        {
            Co.FillCheckBox(chkintrestedarea, Dtchkintrestedarea, "InterestArea", "Id");
        }
        DataTable Dtchkmastermenuallot = Lo.RetriveCompany("IntrestedAreaCheck", 0, "M", 0);
        if (Dtchkmastermenuallot != null)
        {
            Co.FillCheckBox(chkmastermenuallot, Dtchkmastermenuallot, "InterestArea", "Id");
        }
    }
    protected void Cleartext()
    {
        txtemail.Text = "";
        txtcomp.Text = "";
        comprefno = "";
        Masterallowed = "";
        role = "";
    }
    protected void SaveComp()
    {
        hysavecomp["CompanyID"] = Convert.ToInt64(0);
        hysavecomp["CompanyName"] = Co.RSQandSQLInjection(txtcomp.Text.Trim(), "soft");
        hysavecomp["ContactPersonEmailID"] = Co.RSQandSQLInjection(txtemail.Text.Trim(), "soft");
        foreach (ListItem li in chkintrestedarea.Items)
        {
            if (li.Selected == true) {
                intrestedare = intrestedare + "," + li.Value;
            }
        }
        hysavecomp["InterestedArea"] = Co.RSQandSQLInjection(intrestedare.ToString(), "soft");
        foreach (ListItem chkmasallow in chkmastermenuallot.Items)
        {
            if (chkmasallow.Selected == true)
            {
                Masterallowed = Masterallowed + "," + chkmasallow.Value;
            }
        }
        hysavecomp["MasterAllowed"] = Co.RSQandSQLInjection(Masterallowed.ToString(), "soft");
        foreach (ListItem chkro in chkrole.Items)
        {
            if (chkro.Selected == true)
            {
                role = role + "," + chkro.Value;
            }
        }
        hysavecomp["Role"] = Co.RSQandSQLInjection(role.ToString(), "soft");
        string StrSaveComp = Lo.SaveMasterComp(hysavecomp, out _sysMsg, out _msg);
        if (StrSaveComp != "")
        {
            Cleartext();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record Saved.')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not saved.')", true);
        }

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Cleartext();
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        SaveComp();
    }
}