using BusinessLayer;
using Encryption;
using System;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_Feedback : System.Web.UI.Page
{
    UserIPAnalytics userip = new UserIPAnalytics();
    Logic Lo = new Logic();
    private Cryptography Encrypt = new Cryptography();
    HybridDictionary hySave = new HybridDictionary();
    HybridDictionary hyfeedlog = new HybridDictionary();
    private string _msg = string.Empty;
    private string _sysMsg = string.Empty;
    private DataTable DtGrid = new DataTable();
    DataUtility Co = new DataUtility();
    protected void Page_Load(object sender, EventArgs e)
    {
        userip.GetAnalytics();
        if (!this.IsPostBack)
        {
            try
            {
                BindCompany();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Technical Error:- " + ex.Message + "');", true);
            }
        }

    }
    protected void BindCompany()
    {
        DataTable DtCompanyDDL = Lo.RetriveMasterData(0, "", "Admin", 0, "", "", "Select");

        if (DtCompanyDDL.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlcomp, DtCompanyDDL, "CompanyName", "CompanyRefNo");
            ddlcomp.Items.Insert(0, "Select");
            ddlcomp.Enabled = true;
        }
        else
        {
            ddlcomp.Enabled = false;
        }
    }
    protected void Save()
    {
        hySave["FeedBackId"] = 0;
        hySave["UserName"] = TxtBxFirstNm.Text;
        hySave["UserEmail"] = Txtemail.Text;
        hySave["MobileNo"] = Txtcontact.Text;
        hySave["Message"] = TxtBxDesc.Text;
        hySave["CompanyName"] = ddlcomp.SelectedItem.Text;
        hySave["CompanyRefNo"] = ddlcomp.SelectedItem.Value;
        try
        {
            string save = Lo.InsertFeedback(hySave, out _sysMsg, out _msg);
            if (save == "Save")
            {
                SendEmailCode();
                SaveEmailLog();
                cleartext();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Thank you for submitting your feedback. We will get back to you soon!!!')", true);
            }
            else
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Technical Error oops some error occured!!! ')", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + _sysMsg.ToString() + " ')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void ddlcompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.ddlcomp.SelectedValue))
        {
            if (ddlcomp.SelectedItem.Text == "BEL")
            {
                Txtnodalid.Text = "manojyadav@bel.co.in";
            }
            else if (ddlcomp.SelectedItem.Text == "HAL")
            {
                Txtnodalid.Text = "gm.indg@hal-india.com";
            }
            else if (ddlcomp.SelectedItem.Text == "HSL")
            {
                Txtnodalid.Text = "dgmmdo@hslvizag.in";
            }
            else if (ddlcomp.SelectedItem.Text == "MDL")
            {
                Txtnodalid.Text = "anabira@mazdock.com";
            }
            else if (ddlcomp.SelectedItem.Text == "GSL")
            {
                Txtnodalid.Text = "dspatekar@goashipyard.com";
            }
            else if (ddlcomp.SelectedItem.Text == "BDL")
            {
                Txtnodalid.Text = "bdlmnr@bdl-india.in";
            }
            else if (ddlcomp.SelectedItem.Text == "BEML")
            {
                Txtnodalid.Text = "edq@beml.co.in";
            }
            else if (ddlcomp.SelectedItem.Text == "BDL")
            {
                Txtnodalid.Text = "bdlmnr@bdl-india.in";
            }
            else if (ddlcomp.SelectedItem.Text == "GRSE")
            {
                Txtnodalid.Text = "ratan.gulshan@grse.co.in";
            }
            else if (ddlcomp.SelectedItem.Text == "MIDHANI")
            {
                Txtnodalid.Text = "ssaha@midhani-india.in";
            }
            else if (ddlcomp.SelectedItem.Text == "OFB")
            {
                Txtnodalid.Text = "pkdash.ofb@ofb.gov.in";
            }
            else if (ddlcomp.SelectedItem.Text == "SHQ (AIR FORCE)")
            {
                Txtnodalid.Text = "doi.1973@gov.in";
            }
            else if (ddlcomp.SelectedItem.Text == "SHQ (ARMY)")
            {
                Txtnodalid.Text = "doi-army@nic.in";
            }
            else if (ddlcomp.SelectedItem.Text == "SHQ (NAVY)")
            {
                Txtnodalid.Text = "doi-navy@nic.in";
            }
        }

    }
    protected void cleartext()
    {
        TxtBxDesc.Text = ""; TxtBxFirstNm.Text = "";
        Txtcontact.Text = ""; Txtemail.Text = "";
        ddlcomp.SelectedIndex = -1;
    }
    public void SaveEmailLog()
    {
        if (Txtemail.Text != "")
        {
            hyfeedlog["IsMailSend"] = "Y";
        }
        else
        {
            hyfeedlog["IsMailSend"] = "N";
        }
        string insertemaillog = Lo.InsertLogFeedbackMail(hyfeedlog, out _sysMsg, out _msg);
    }
    public void SendEmailCode()
    {
        try
        {
            string body;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/emailPage/Feedback.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Email}", Txtemail.Text);
            body = body.Replace("{Name}", TxtBxFirstNm.Text);
            body = body.Replace("{Phone}", Txtcontact.Text);
            body = body.Replace("{CompanyName}", ddlcomp.SelectedItem.Text);
            body = body.Replace("{Description}", TxtBxDesc.Text);
            SendMail s;
            if (Txtemail.Text != "")
            {
                s = new SendMail();
                s.CreateMail(Txtemail.Text, Txtnodalid.Text, "Feedback details", body);
                s.sendMail();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtBxDesc.Text != "" && TxtBxFirstNm.Text != "" && Txtcontact.Text != "" && Txtemail.Text != "")
            {
                Save();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Please fill all mandatory field!!!')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
}
