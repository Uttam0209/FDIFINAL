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
    private string MRole = "";
    private DropDownList mddlControl;
    HybridDictionary HySave = new HybridDictionary();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Enc.DecryptData(Request.QueryString["mu"].ToString()) == "Panel1")
            {
                mastercompany.Visible = true;
                masterfacotry.Visible = false;
                BindMasterCompany(Session["CompanyRefNo"].ToString());
                BindMasterData();
                Intrested.Visible = true;
                lblName.Text = "Company Name";
                btnsubmit.Text = "Save Company";
                MenuAlot.Visible = true;
                Role.Visible = true;
            }
            else if (Enc.DecryptData(Request.QueryString["mu"].ToString()) == "Panel2")
            {
                mastercompany.Visible = true;
                masterfacotry.Visible = false;
                BindMasterCompany(Session["CompanyRefNo"].ToString());

                lblName.Text = "Divison/Plant Name";
                btnsubmit.Text = "Save Divison";
                Intrested.Visible = false;
                MenuAlot.Visible = false;
                Role.Visible = false;

            }
            else if (Enc.DecryptData(Request.QueryString["mu"].ToString()) == "Panel3")
            {
                mastercompany.Visible = true;
                masterfacotry.Visible = true;
                BindMasterCompany(Session["CompanyRefNo"].ToString());
                this.ddlmaster_SelectedIndexChanged(sender, e);
                lblName.Text = "Unit Name";
                btnsubmit.Text = "Save Unit";
                Intrested.Visible = false;
                MenuAlot.Visible = false;
                Role.Visible = false;

            }
            else
            {
                mastercompany.Visible = false;
                masterfacotry.Visible = false;
                BindMasterData();
                BindMasterCompany(Session["CompanyRefNo"].ToString());
                Intrested.Visible = true;
                MenuAlot.Visible = true;
                Role.Visible = true;
            }

        }
    }

    protected void BindMasterCompany(string mRefNo)
    {
        string sType = "", sName = "", sID = "", mSID = "";
        Int16 id = 0;
        if (Enc.DecryptData(Session["Type"].ToString()) == "SuperAdmin")
        {
            sType = "Select";
            sName = "CompanyName";
            sID = "CompanyRefNo";
            mSID = "";
            id = 0;

        }
        else if (Enc.DecryptData(Session["Type"].ToString()) == "Company")
        {
        
            sType = "CompanyName";
            sName = "CompanyName";
            sID = "CompanyRefNo";
            mSID = Session["CompanyRefNo"].ToString();
            id = 2;
        }
        else if (Enc.DecryptData(Session["Type"].ToString()) == "Factory")
        {

            sType = "CompanyName";
            sName = "CompanyName";
            sID = "CompanyRefNo";
            mSID = Session["CompanyRefNo"].ToString();
            id = 3;
        }
        else if (Enc.DecryptData(Session["Type"].ToString()) == "Unit")
        {

            sType = "CompanyName";
            sName = "CompanyName";
            sID = "CompanyRefNo";
            mSID = Session["CompanyRefNo"].ToString();
            id = 4;
        }
        else
        {
            sType = "CompanyName";
            sName = "CompanyName";
            sID = "CompanyRefNo";
            mSID = Session["CompanyRefNo"].ToString();
        }

        mddlControl = ddlmaster;
        DataTable Dtchkintrestedarea = Lo.RetriveCompany(sType, id, mSID, 0);
        if (Dtchkintrestedarea.Rows.Count > 0 && Dtchkintrestedarea != null)
        {
            hfrole.Value = "";
            hfrole.Value = Dtchkintrestedarea.Rows[0]["Role"].ToString();
            Co.FillDropdownlist(mddlControl, Dtchkintrestedarea, sName, sID);
        }
        else
        {
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
        hfrole.Value = "";
    }

    protected void SaveComp()
    {

        HySave["CompanyID"] = id;

        HySave["CompanyName"] = Co.RSQandSQLInjection(txtcomp.Text.Trim(), "soft");

        HySave["ContactPersonEmailID"] = Co.RSQandSQLInjection(txtemail.Text.Trim(), "soft");
        string StrSaveComp = "";

        //BindMasterCompany(ddlmaster.SelectedItem.Value);
        if (btnsubmit.Text == "Save Divison")
        {
            HySave["CompanyRefNo"] = ddlmaster.SelectedItem.Value;

            HySave["Role"] = "Factory";

            StrSaveComp = Lo.SaveFactoryComp(HySave, out _sysMsg, out _msg);
        }
        else if (btnsubmit.Text == "Save Unit")
        {
            HySave["CompanyRefNo"] = ddlfacotry.SelectedItem.Value;

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

            if (hfrole.Value == "Company" || hfrole.Value == "Factory")
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
    protected void ddlmaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable DtBindSubFactory = Lo.RetriveCompany("FactoryName", 0, ddlmaster.SelectedItem.Value, 0);
        if (DtBindSubFactory.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlfacotry, DtBindSubFactory, "FactoryName", "FactoryRefNo");
            ddlfacotry.Items.Insert(0, "Select Factory");
        }
        else
        {
            ddlfacotry.Items.Insert(0, "Select Factory");
        }

    }

}