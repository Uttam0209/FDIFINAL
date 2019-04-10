using System;
using System.Data;
using BusinessLayer;
using Encryption;
using System.Collections.Specialized;
using System.Web.UI;
using System.Text.RegularExpressions;

public partial class frmAddFDI : System.Web.UI.Page
{
    Logic Lo = new Logic();
    HybridDictionary HySave = new HybridDictionary();
    Cryptography objCrypto = new Cryptography();
    Int64 Mid = 0;
    DataUtility Co = new DataUtility();
    string _msg = string.Empty;
    string _sysMsg = string.Empty;
    DataTable DtView = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCompany();
            BindCountry();
            EditCOde();
        }
    }
    protected void BindCompany()
    {
        DataTable Dt = Lo.RetriveCompany("Select", 0, "");
        if (Dt.Rows.Count > 0 && Dt != null)
        {
            Co.FillDropdownlist(ddlcompany, Dt, "CompanyName", "CompanyID");
            ddlcompany.Items.Insert(0, "Select Company");
        }
        else
        {
            ddlcompany.Items.Insert(0, "Select Company");
        }
    }
    protected void EditCOde()
    {
        if (Request.QueryString["mcurrentID"] != null)
        {
            hfid.Value = objCrypto.DecryptData(Request.QueryString["mcurrentID"].ToString());
            DtView = Lo.RetriveGridView(Convert.ToInt64(hfid.Value));
            if (DtView.Rows.Count > 0)
            {
                ddlcompany.Text = DtView.Rows[0]["CompanyName"].ToString();
                businesscode.Text = DtView.Rows[0]["CodeofBusiness"].ToString();
                tbdescription.Text = DtView.Rows[0]["BriefDescription"].ToString();
                casetype.Text = DtView.Rows[0]["InCaseOf"].ToString();
                if (casetype.SelectedItem.Value == "Automatic")
                {
                    divapproval.Visible = false;
                    divapprovaldate.Visible = false;
                }
                else
                {
                    divapproval.Visible = true;
                    divapprovaldate.Visible = true;
                }
                tapprovalno.Text = DtView.Rows[0]["ApprovalNo"].ToString();
                tapprovaldate.Text = DtView.Rows[0]["ApprovalDate"].ToString();
                ViewState["DAteApp"] = tapprovaldate.Text;
                tforeignname.Text = DtView.Rows[0]["ForeignCompanyName"].ToString();
                tforeignaddress.Text = DtView.Rows[0]["Address"].ToString();
                ncountry.Text = DtView.Rows[0]["Country"].ToString();
                tzipcode.Text = DtView.Rows[0]["ZipCode"].ToString();
                Select1.Text = DtView.Rows[0]["ForeignDefenceActivity"].ToString();
                nstate.Text = DtView.Rows[0]["FDIValueType"].ToString();
                Select2.Text = DtView.Rows[0]["PeriodofReporting"].ToString();
                if (Select2.SelectedItem.Value == "Quarterly")
                {
                    periodofquater.Visible = true;
                    year.Visible = true;
                }
                else if (Select2.SelectedItem.Value == "Half Yearly")
                {
                    periodofquater.Visible = false;
                    year.Visible = true;
                }
                else if (Select2.SelectedItem.Value == "Annual")
                {
                    periodofquater.Visible = false;
                    year.Visible = true;
                }
                else if (Select2.SelectedItem.Value == "")
                {
                    periodofquater.Visible = false;
                    year.Visible = false;
                }
                Select3.Text = DtView.Rows[0]["Currency"].ToString();
                fdiinflow.Text = DtView.Rows[0]["TotalFDIInFlow"].ToString();
                exchangerate.Text = DtView.Rows[0]["EquINRExchangeRate"].ToString();
                afterexchnagerate.Text = DtView.Rows[0]["ExchangeTotalAmount"].ToString();
                selsource.Text = DtView.Rows[0]["SourceofInformation"].ToString();
                tdateofreceiving.Text = DtView.Rows[0]["DateofReceivingInformation"].ToString();
                ViewState["DAteRecInfApp"] = tdateofreceiving.Text;
                selcolour.Text = DtView.Rows[0]["AuthencityofInformation"].ToString();
                tremarks.Text = DtView.Rows[0]["Remarks"].ToString();
                lblfuupdate.Text = DtView.Rows[0]["DocumentAttach"].ToString();
            }
        }
    }
    protected void BindCountry()
    {
        DataTable Dt = Lo.RetriveCountry("Select");
        if (Dt.Rows.Count > 0 && Dt != null)
        {
            Co.FillDropdownlist(ncountry, Dt, "CountryName", "CountryID");
            ncountry.Items.Insert(0, "Select Country");
        }
        else
        {
            ncountry.Items.Insert(0, "Select Country");
        }
    }
    protected void SaveFDI()
    {
        if (hfid.Value != "")
        {
            HySave["CompanyID"] = Convert.ToInt64(hfid.Value);
            HySave["MID"] = Convert.ToInt64(hfid.Value);
        }
        else
        {
            HySave["CompanyID"] = ddlcompany.SelectedItem.Value;
            HySave["MID"] = Mid;
        }
        HySave["CodeofBusiness"] = Co.RSQandSQLInjection(businesscode.Text.Trim(), "soft");
        HySave["BriefDescription"] = Co.RSQandSQLInjection(tbdescription.Text.Trim(), "soft");
        HySave["InCaseOf"] = Co.RSQandSQLInjection(casetype.SelectedItem.Value, "soft");
        if (tapprovalno.Text == "")
        {
            HySave["ApprovalNo"] = "";
        }
        else
        {
            HySave["ApprovalNo"] = Co.RSQandSQLInjection(tapprovalno.Text.Trim(), "soft");
        }
        if (hfid.Value != "" && tapprovaldate.Text == "")
        {
            HySave["ApprovalDate"] = Co.RSQandSQLInjection(ViewState["DAteApp"].ToString(), "soft");
        }
        else if (Mid == 0 && tapprovaldate.Text != "")
        {
            DateTime DateApproval = Convert.ToDateTime(tapprovaldate.Text.Trim());
            string Date = DateApproval.ToString("yyyy-MM-dd");
            HySave["ApprovalDate"] = Co.RSQandSQLInjection(Date, "soft");
        }
        else
        {
            HySave["ApprovalDate"] = null;
        }
        HySave["ForeignCompanyName"] = Co.RSQandSQLInjection(tforeignname.Text.Trim(), "soft");
        HySave["Address"] = Co.RSQandSQLInjection(tforeignaddress.Text.Trim(), "soft");
        HySave["Country"] = Co.RSQandSQLInjection(ncountry.SelectedItem.Value, "soft");
        HySave["ZipCode"] = Co.RSQandSQLInjection(tzipcode.Text.Trim(), "soft");
        HySave["ForeignDefenceActivity"] = Co.RSQandSQLInjection(Select1.SelectedItem.Value, "soft");
        HySave["FDIValueType"] = Co.RSQandSQLInjection(nstate.SelectedItem.Value, "soft");
        HySave["PeriodofReporting"] = Co.RSQandSQLInjection(Select2.SelectedItem.Value, "soft");
        HySave["PeriodOfQuater"] = Co.RSQandSQLInjection(ddlquater.SelectedItem.Value, "soft");
        if (ddlyear.SelectedValue != "")
        {
            HySave["Year"] = Co.RSQandSQLInjection(ddlyear.SelectedItem.Value, "soft");
        }
        else
        {
            HySave["Year"] = null;
        }
        HySave["Currency"] = Co.RSQandSQLInjection(Select3.SelectedItem.Value, "soft");
        HySave["TotalFDIInFlow"] = Co.RSQandSQLInjection(fdiinflow.Text.Trim(), "soft");
        HySave["EquINRExchangeRate"] = Co.RSQandSQLInjection(exchangerate.Text.Trim(), "soft");
        HySave["ExchangeTotalAmount"] = Co.RSQandSQLInjection(afterexchnagerate.Text.Trim(), "soft");
        HySave["SourceofInformation"] = Co.RSQandSQLInjection(selsource.SelectedItem.Value, "soft");
        if (hfid.Value != "" && tdateofreceiving.Text == "")
        {
            HySave["DateofReceivingInformation"] = Co.RSQandSQLInjection(ViewState["DAteRecInfApp"].ToString(), "soft");
        }
        else if (Mid == 0 && tdateofreceiving.Text != "")
        {
            DateTime DateofReceivingInformation = Convert.ToDateTime(tdateofreceiving.Text.Trim());
            string DateReciInf = DateofReceivingInformation.ToString("yyyy-MM-dd");
            HySave["DateofReceivingInformation"] = Co.RSQandSQLInjection(DateReciInf, "soft");
        }
        if (attachfile.HasFile != false && attachfile.PostedFile.FileName != "" && lblfuupdate.Text == "")
        {
            string ImgPath = Co.RSQandSQLInjection(ddlcompany.Text.Trim().Replace(" ", "-"), "soft") + "_" + DateTime.Now.ToString("ddMMMHHmm") + "_" + attachfile.PostedFile.FileName;
            attachfile.PostedFile.SaveAs(Server.MapPath("~/CompDocument/") + ImgPath);
            HySave["DocumentAttach"] = ImgPath.ToString();
        }
        else
        {
            HySave["DocumentAttach"] = Co.RSQandSQLInjection(lblfuupdate.Text, "soft");
        }
        HySave["AuthencityofInformation"] = Co.RSQandSQLInjection(selcolour.SelectedItem.Value, "soft");
        HySave["Remarks"] = Co.RSQandSQLInjection(tremarks.Text.Trim(), "soft");

    }
    public string ValidatePreview()
    {
        string mCon = "";

        if (ddlcompany.SelectedItem.Text == "Select Company")
        {
            mCon = "Contact no is mandatory";
        }
        return mCon;
    }
    public static bool IsValidEmailId(string InputEmail)
    {
        string pattern = @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}$";
        Match match = Regex.Match(InputEmail.Trim(), pattern, RegexOptions.IgnoreCase);
        if (match.Success)
            return true;
        else
            return false;
    }
    protected void btnsub_Click(object sender, EventArgs e)
    {
        if (ddlcompany.SelectedItem.Text != "Select Company")
        {
            if (attachfile.PostedFile != null && attachfile.PostedFile.FileName != "")
            {
                if (attachfile.PostedFile.FileName != "")
                {
                    if (!Co.GetFileFilter(attachfile.PostedFile.FileName))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Photograph is not in correct format (e.g. - .pdf, .txt'));", true);
                        return;
                    }
                    if (attachfile.PostedFile.ContentLength > 2000000)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Photograph size is too large');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Please select Photograph');", true);
                }
            }
            string msg = this.ValidatePreview();
            if (msg != "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + msg + "');", true);
            }
            else
            {
                SaveFDI();
                string StrSaveFDI = Lo.SaveFDI(HySave, out _msg, out _sysMsg);
                if (StrSaveFDI == "Save")
                {
                    if (hfid.Value != "")
                    {
                        cleartext();
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Record updated successfully');window.location='ViewFDI-Detail';", true);
                    }
                    else
                    {
                        cleartext();
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Record save successfully');window.location='ViewFDI-Detail';", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not save successfully.')", true);
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Please fill all mandatory field.');", true);
        }
    }
    protected void cleartext()
    {
        ddlcompany.Text = "Select Company";
        businesscode.Text = "";
        tbdescription.Text = "";
        casetype.SelectedIndex = 0;
        tapprovalno.Text = "";
        tapprovaldate.Text = "";
        tforeignname.Text = "";
        tforeignaddress.Text = "";
        ncountry.SelectedIndex = 0;
        tzipcode.Text = "";
        Select1.SelectedIndex = 0;
        nstate.SelectedIndex = 0;
        Select2.SelectedIndex = 0;
        Select3.SelectedIndex = 0;
        fdiinflow.Text = "";
        exchangerate.Text = "";
        afterexchnagerate.Text = "";
        selsource.SelectedIndex = 0;
        tdateofreceiving.Text = "";
        selcolour.SelectedIndex = 0;
        tremarks.Text = "";
        hfid.Value = "";
        ViewState["DAteApp"] = null;
        ViewState["DAteRecInfApp"] = null;
    }
    protected void casetype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (casetype.SelectedItem.Value == "Automatic")
        {
            divapproval.Visible = false;
            divapprovaldate.Visible = false;
        }
        else
        {
            divapproval.Visible = true;
            divapprovaldate.Visible = true;
        }
    }
    protected void Select2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Select2.SelectedItem.Value == "Quarterly")
        {
            periodofquater.Visible = true;
            year.Visible = true;
        }
        else if (Select2.SelectedItem.Value == "Half Yearly")
        {
            periodofquater.Visible = false;
            year.Visible = true;
        }
        else if (Select2.SelectedItem.Value == "Annual")
        {
            periodofquater.Visible = false;
            year.Visible = true;
        }
        else if (Select2.SelectedItem.Value == "")
        {
            periodofquater.Visible = false;
            year.Visible = false;
        }
    }
    protected void exchangerate_TextChanged(object sender, EventArgs e)
    {
        if (fdiinflow.Text != "" && exchangerate.Text != "0")
        {
            int Totalfdiqty = Convert.ToInt32(fdiinflow.Text);
            int exchangeraterbi = Convert.ToInt32(exchangerate.Text);
            int total = Totalfdiqty * exchangeraterbi;
            afterexchnagerate.Text = total.ToString();
        }
    }
}