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
using System.IO;

public partial class Admin_AddMasterCompany : System.Web.UI.Page
{
    Logic Lo = new Logic();
    Cryptography Enc = new Cryptography();
    DataUtility Co = new DataUtility();
    private Int64 id = 0;
    private string _sysMsg = string.Empty;
    private string _msg = string.Empty;
    private string comprefno = "";
    private string intrestedare = "";
    private string Masterallowed = "";
    private string role = "";
    string Mrole = "";
    HybridDictionary HySave = new HybridDictionary();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Enc.DecryptData(Request.QueryString["mu"].ToString()) == "Panel1")
            {
                mastercompany.Visible = true;
                BindMasterCompany();
                BindMasterData();
                Intrested.Visible = true;
                MenuAlot.Visible = true;
                Role.Visible = true;
            }
            else if (Enc.DecryptData(Request.QueryString["mu"].ToString()) == "Panel2")
            {
                mastercompany.Visible = true;
                BindMasterCompany();
                Intrested.Visible = false;
                MenuAlot.Visible = false;
                Role.Visible = false;

            }
            else
            {
                mastercompany.Visible = false;
                BindMasterData();
                BindMasterCompany();
                Intrested.Visible = true;
                MenuAlot.Visible = true;
                Role.Visible = true;
            }

        }
    }
    protected void BindMasterCompany()
    {
        string sType = "", sName = "", sID = "";
        if (Session["CompanyRefNo"].ToString().Substring(0, 1) == "F")
        {
            sType = "FactoryName";
            sName = "FactoryName";
            sID = "FactoryID";
        }
        else
        {
            sType = "InterestedArea";
            sName = "CompanyName";
            sID = "CompanyId";
        }
        DataTable Dtchkintrestedarea = Lo.RetriveCompany(sType, 0, Session["CompanyRefNo"].ToString(), 0);
        if (Dtchkintrestedarea != null)
        {
            Mrole = Dtchkintrestedarea.Rows[0]["Role"].ToString();
            Co.FillDropdownlist(ddlmaster, Dtchkintrestedarea,sName,sID);
            ddlmaster.Enabled = false;
            if (Mrole == "Company")
            {
                lblMastcompany.Text = "Company Name";
                lblName.Text = "Factory Name";
            }
            else if (Mrole == "Factory")
            {
                lblMastcompany.Text = "Factory Name";
                lblName.Text = "Unit Name";
            }
            else
            {
                lblMastcompany.Text = "Company Name";
                lblName.Text = "Company Name";
            }
        }
        else
        {
            ddlmaster.Items.Insert(0, "Select Company");
            ddlmaster.Enabled = false;
            lblName.Text = "Company Name";
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

        HySave["CompanyID"] = id;

        HySave["CompanyName"] = Co.RSQandSQLInjection(txtcomp.Text.Trim(), "soft");

        HySave["ContactPersonEmailID"] = Co.RSQandSQLInjection(txtemail.Text.Trim(), "soft");

        BindMasterCompany();

        string StrSaveComp = "";
        if (Mrole == "Company")
        {
            HySave["CompanyRefNo"] = Session["CompanyRefNo"].ToString();

            HySave["Role"] = "Factory";

            StrSaveComp = Lo.SaveFactoryComp(HySave, out _sysMsg, out _msg);
        }
        if (Mrole == "Factory")
        {
            HySave["CompanyRefNo"] = Session["CompanyRefNo"].ToString();

            HySave["Role"] = "Unit";

            StrSaveComp = Lo.SaveUnitComp(HySave, out _sysMsg, out _msg);
        }
        else
        {
            foreach (ListItem li in chkintrestedarea.Items)
            {
                if (li.Selected == true)
                {
                    intrestedare = intrestedare + "," + li.Value;
                }
            }
            HySave["InterestedArea"] = Co.RSQandSQLInjection(intrestedare.Substring(1).ToString(), "soft");
            foreach (ListItem chkmasallow in chkmastermenuallot.Items)
            {
                if (chkmasallow.Selected == true)
                {
                    Masterallowed = Masterallowed + "," + chkmasallow.Value;
                }
            }
            HySave["MasterAllowed"] = Co.RSQandSQLInjection(Masterallowed.Substring(1).ToString(), "soft");
            foreach (ListItem chkro in chkrole.Items)
            {
                if (chkro.Selected == true)
                {
                    role = role + "," + chkro.Value;
                }
            }
            lblName.Text = "Company Name";
            HySave["Role"] = Co.RSQandSQLInjection(role.Substring(1).ToString(), "soft");

            StrSaveComp = Lo.SaveMasterComp(HySave, out _sysMsg, out _msg);
        }
        if (StrSaveComp != "")
        {
            
            if (Mrole == "Company" || Mrole=="Factory")
            {
                try
                {
                   
                    SendEmailCode();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Mail not send error occured.')", true);
                }
            }

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

    public void SendEmailCode()
    {
        try
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/GeneratePassword.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", txtemail.Text);
            body = body.Replace("{refno}", Enc.EncryptData(_sysMsg));
            body = body.Replace("{mcurid}", Resturl(56));
            SendMail s;
            s = new SendMail();
            s.CreateMail("aeroindia-ddp@gov.in", txtemail.Text, "Create Password", body);
            s.sendMail();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Password change mail send successfully.')", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }

    }

    #region ReturnUrl Long"
    public string Resturl(int length)
    {
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        StringBuilder res = new StringBuilder();
        Random rnd = new Random();
        while (0 < length--)
        {
            res.Append(valid[rnd.Next(valid.Length)]);
        }
        return res.ToString();
    }
    #endregion
}