using System;
using System.Data;
using BusinessLayer;
using System.Collections.Specialized;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using Encryption;


public partial class Admin_frmAddFDI : System.Web.UI.Page
{
    #region Variable
    Logic Lo = new Logic();
    HybridDictionary HySave = new HybridDictionary();
    Cryptography objCrypto = new Cryptography();
    Int64 Mid = 0;
    DataUtility Co = new DataUtility();
    DataTable DtFilter = new DataTable();
    public static string Comparer;
    public static Int64 NicCode = 0;
    public static Int64 CountryID = 0;
    string _msg = string.Empty;
    string _sysMsg = string.Empty;
    DataTable DtView = new DataTable();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PanelHideShow();
            EditCOde();
        }
    }
    protected void BindFY()
    {
        DataTable Dtfy = Lo.RetriveCompany("FY", 0, "");
        if (Dtfy.Rows.Count > 0 && Dtfy != null)
        {
            Co.FillDropdownlist(ddlyear, Dtfy, "FY", "FYID");
            ddlyear.Items.Insert(0, "Select Financial Year");
        }
        else
        {
            ddlyear.Items.Insert(0, "Select Financial Year");
        }
    }
    protected void EditCOde()
    {
        if (Request.QueryString["mcurrentID"] != null)
        {
            hfid.Value = objCrypto.DecryptData(Request.QueryString["mcurrentID"].ToString());
            DtView = Lo.RetriveGridView(hfid.Value);
            if (DtView.Rows.Count > 0)
            {
                txtcomp.Text = DtView.Rows[0]["CompanyName"].ToString(); // ddlcompany.Text
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
                txtcount.Text = DtView.Rows[0]["Country"].ToString();
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
    protected void SaveFDI()
    {
        if (hfid.Value != "")
        {
            HySave["CompanyRefNo"] = Convert.ToInt64(hfid.Value);
            HySave["MID"] = Convert.ToInt64(hfid.Value);
        }
        else
        {
            HySave["CompanyRefNo"] = Comparer;
            HySave["MID"] = Mid;
        }
        HySave["NicCodeID"] = Co.RSQandSQLInjection(NicCode.ToString(), "soft");
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
        HySave["Country"] = Co.RSQandSQLInjection(CountryID.ToString(), "soft");
        HySave["ZipCode"] = Co.RSQandSQLInjection(tzipcode.Text.Trim(), "soft");
        HySave["ForeignDefenceActivity"] = Co.RSQandSQLInjection(Select1.SelectedItem.Value, "soft");
        HySave["FDIValueType"] = Co.RSQandSQLInjection(nstate.SelectedItem.Value, "soft");
        HySave["PeriodofReporting"] = Co.RSQandSQLInjection(Select2.SelectedItem.Value, "soft");
        HySave["PeriodOfQuater"] = Co.RSQandSQLInjection(ddlquater.SelectedItem.Value, "soft");
        if (ddlyear.SelectedValue != "")
        {
            HySave["FYID"] = Co.RSQandSQLInjection(ddlyear.SelectedItem.Value, "soft");
        }
        else
        {
            HySave["FYID"] = null;
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
            string ImgPath = Co.RSQandSQLInjection(txtcomp.Text.Trim().Replace(" ", "-"), "soft") + "_" + DateTime.Now.ToString("ddMMMHHmm") + "_" + attachfile.PostedFile.FileName;
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
        if (txtcomp.Text == "Select Company")
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
        if (txtcomp.Text != "Select Company")
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
        txtcomp.Text = "";
        businesscode.Text = "";
        tbdescription.Text = "";
        casetype.SelectedIndex = 0;
        tapprovalno.Text = "";
        tapprovaldate.Text = "";
        tforeignname.Text = "";
        tforeignaddress.Text = "";
        txtcount.Text = "";
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

        if (DtFilter.Rows.Count > 0)
        {
        }
        if (Select2.SelectedItem.Value == "Quarterly")
        {
            periodofquater.Visible = true;
            divhalfyear.Visible = false;
            BindFY();
            year.Visible = true;
        }
        else if (Select2.SelectedItem.Value == "Half Yearly")
        {
            periodofquater.Visible = false;
            divhalfyear.Visible = true;
            BindFY();
            year.Visible = true;
        }
        else if (Select2.SelectedItem.Value == "Annual")
        {
            periodofquater.Visible = false;
            divhalfyear.Visible = false;
            BindFY();
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
    protected void businesscode_TextChanged(object sender, EventArgs e)
    {
        DataTable DtDes = Lo.RetriveCompany("Description", 0, businesscode.Text);
        if (DtDes.Rows.Count > 0 && DtDes != null)
        {
            tbdescription.Text = DtDes.Rows[0]["NicDesc"].ToString();
            tbdescription.ReadOnly = true;
        }
    }
    #region AutoComplete NicCode
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetCustomers(string prefix)
    {
        Cryptography objCrypto1 = new Cryptography();
        List<string> customers = new List<string>();
        Int64 FinalNicCode = 0;
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = objCrypto1.DecryptData(ConfigurationManager.ConnectionStrings["connectiondb"].ConnectionString);
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select NicCode, NicCodeID from tbl_mst_NicCode where NicCode like @SearchText + '%' and isactive='Y'";
                cmd.Parameters.AddWithValue("@SearchText", prefix);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}-{1}", sdr["NicCode"], sdr["NicCodeID"]));
                        FinalNicCode = Convert.ToInt64(sdr["NicCodeID"].ToString());
                    }
                }
                conn.Close();
            }
        }
        NicCode = FinalNicCode;
        return customers.ToArray();
    }
    #endregion
    #region AutoComplete Company
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetCustomers1(string prefix)
    {
        Cryptography objCrypto1 = new Cryptography();
        List<string> customers1 = new List<string>();
        string FCompID = "";
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = objCrypto1.DecryptData(ConfigurationManager.ConnectionStrings["connectiondb"].ConnectionString);
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select CompanyName,CompanyRefNo from tbl_mst_Company where CompanyName like @SearchText + '%' and IsActive='Y'";
                cmd.Parameters.AddWithValue("@SearchText", prefix);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers1.Add(string.Format("{0}-{1}", sdr["CompanyName"], sdr["CompanyRefNo"]));
                        FCompID = sdr["CompanyRefNo"].ToString();
                    }
                }
                conn.Close();
            }
        }
        Comparer = FCompID.ToString();
        return customers1.ToArray();

    }
    #endregion
    #region AutoComplete Country
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetCountry(string prefix)
    {
        Cryptography objCrypto1 = new Cryptography();
        List<string> Country = new List<string>();
        Int64 FCountry = 0;
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = objCrypto1.DecryptData(ConfigurationManager.ConnectionStrings["connectiondb"].ConnectionString);
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select CountryName, CountryID from tbl_mst_Country where CountryName like @SearchText + '%' and IsActive='Y'";
                cmd.Parameters.AddWithValue("@SearchText", prefix);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        Country.Add(string.Format("{0}-{1}", sdr["CountryName"], sdr["CountryID"]));
                        FCountry = Convert.ToInt64(sdr["CountryID"]);
                    }
                }
                conn.Close();
            }
        }
        CountryID = FCountry;
        return Country.ToArray();
    }
    #endregion
    #region FilterFDI
    protected void FIlterFDIRegistration()
    {
        DtFilter = Lo.RetriveGridView(Comparer);
        if (DtFilter.Rows.Count > 0)
        {
            DataView DVFilter = new DataView(DtFilter);
            DVFilter.RowFilter = "FDIValueType='" + nstate.SelectedItem.Value + "' and  FYID='" + ddlyear.SelectedItem.Value + "'";

        }
    }
    #endregion
    protected void PanelHideShow()
    {
        try
        {
            if (objCrypto.DecryptData(Request.QueryString["mu"].ToString()) == "Panel1")
            {
                fdistep1.Visible = true;
            }
            else if (objCrypto.DecryptData(Request.QueryString["mu"].ToString()) == "Panel2")
            {
                fdistep2.Visible = true;
            }
            else if (objCrypto.DecryptData(Request.QueryString["mu"].ToString()) == "Panel3")
            {
                fdistep3.Visible = true;
            }
            else if (objCrypto.DecryptData(Request.QueryString["mu"].ToString()) == "Panel4")
            {
                fdistep4.Visible = true;
            }
        }
        catch (Exception ex)
        {
        }
    }
}