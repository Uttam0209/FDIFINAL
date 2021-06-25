using System;
using BusinessLayer;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Collections.Specialized;
using Encryption;
using context = System.Web.HttpContext;
using System.Web.UI.HtmlControls;

public partial class SHQfrmUploadExcel : System.Web.UI.Page
{
    #region Variable
    Logic Lo = new Logic();
    string str = "";
    private DataUtility Co = new DataUtility();
    HybridDictionary hy = new HybridDictionary();
    Cryptography ObjEnc = new Cryptography();
    Cryptography objEnc = new Cryptography();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["CompanyRefNo"].ToString()=="C0023" || Session["CompanyRefNo"].ToString() == "C0024"|| Session["CompanyRefNo"].ToString() == "C0025")
            {
                string companyno =Session["CompanyRefNo"].ToString();
                //hidType.Value = objEnc.DecryptData(Request.QueryString["mrcreaterole"].ToString().Trim());
                //hfcomprefno.Value = objEnc.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString().Trim());
                msg.Visible = false;


            }
            else
            {
                hidType.Value = objEnc.DecryptData(Session["Type"].ToString().Trim());
                hfcomprefno.Value = Session["CompanyRefNo"].ToString().Trim();
                msg.Visible = true;
                Response.Redirect("Dashboard");
            }

        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {

    }

    protected bool checkvalidation(string s)
    {
        if (s != "Select" && s != null)
        {
            return true;
        }
        else
        { return false; }
    }
    string chk;
    string m;

    protected void SaveExcelHy()
    {
        hy["ProductID"] = 0;
        hy["CompanyRefNo"] = Session["CompanyRefNo"].ToString();
      
            
        hy["IsProductImported"] = "Y";
       
       
        hy["IndProcess"] = "Y";
        DataTable DtSendMailForgotpassword = Lo.RetriveForgotPasswordEmail(Co.RSQandSQLInjection(ObjEnc.DecryptData(Session["User"].ToString()), "soft"), "ForgotPassword2");
        if (DtSendMailForgotpassword.Rows.Count > 0)
        {
            hy["NodelDetail"] = DtSendMailForgotpassword.Rows[0]["NodalOfficerID"].ToString();
        }
        else
        {
            hy["NodelDetail"] = null;
        }
        hy["IsShowGeneral"] = "Y";
        hy["Role"] = ObjEnc.DecryptData(Session["Type"].ToString());
        hy["CreatedBy"] = ObjEnc.DecryptData(Session["User"].ToString());
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {           
                if (rbtypefile.SelectedItem.Value == "1")
                {
                    if (fuexcel.FileName == null && fuexcel.FileName == "")  //if no file selected the give error
                    {
                        diverror.Visible = true;
                        diverror.InnerHtml = "Please select Excel file.";
                        diverror.Attributes.Add("class", "alert alert-danger");
                        return;
                    }
                    else //---------------------------------------------------if file is selected
                    {
                        string ErrText = "";
                        DataTable dtExcel = new DataTable();
                        try
                        {
                            string path = Server.MapPath("~/App_Data/") + fuexcel.PostedFile.FileName;
                            fuexcel.SaveAs(path);
                            dtExcel = Lo.CreateExcelConnection(path, "Sheet1", out ErrText);
                            if (ErrText.ToLower() != "success") // if sqlhelper returning ERROR in importing
                            {
                                diverror.Visible = true;
                                diverror.InnerHtml = ErrText;
                                diverror.Attributes.Add("class", "alert alert-danger");
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            ViewState["error"] = ex.Message;
                            diverror.Visible = true;
                            diverror.InnerHtml = ErrText + ex.Message;
                            diverror.Attributes.Add("class", "alert alert-danger");
                        }
                        // if excel import completed without error
                        if (dtExcel.Rows.Count < 1)
                        {
                            diverror.Visible = true;
                            diverror.InnerHtml = "No data imported from excel file !!" + ViewState["error"];
                            diverror.Attributes.Add("class", "alert alert-danger");
                            ViewState["error"] = null;
                            return;
                        }
                        else
                        {
                            try
                            {
                                var rowsCount = Convert.ToInt32(dtExcel.Rows.Count);
                                lblRowCount.Text = "Rows Processed:- " + rowsCount;
                                SaveExcelHy();
                                str = Lo.SaveExcelProduct1FORSHQ(dtExcel, hy);
                                if (str == "Save")
                                {
                                    diverror.Visible = true;
                                    diverror.InnerHtml = "Data imported successfully from excel file !!\n\nTotal Rows - " + dtExcel.Rows.Count.ToString() + ".";
                                    diverror.Attributes.Add("class", "alert alert-success");
                                    dtExcel.Dispose();
                                    dtExcel = null;
                                }
                                else
                                {
                                    diverror.Visible = true;
                                    diverror.InnerHtml = "User Error: Error in excel data. Technical Error: " + str + "";
                                    diverror.Attributes.Add("class", "alert alert-danger");
                                }
                            }
                            catch (Exception ex)
                            {
                                diverror.Visible = true;
                                diverror.InnerHtml = "User Error: Error in excel data. Technical Error: " + ex.Message + "";
                                diverror.Attributes.Add("class", "alert alert-danger");
                            }
                        }
                    }
                }
                else
                {
                    if (fuexcel.FileName == null && fuexcel.FileName == "")  //if no file selected the give error
                    {
                        diverror.Visible = true;
                        diverror.InnerHtml = "Please select CSV Excel file.";
                        diverror.Attributes.Add("class", "alert alert-danger");
                        return;
                    }
                    else //---------------------------------------------------if file is selected
                    {
                        string ErrText = "";
                        DataTable dtExcel = new DataTable();
                        try
                        {
                            string path = Server.MapPath("~/App_Data/") + fuexcel.PostedFile.FileName;
                            fuexcel.SaveAs(path);
                            dtExcel.Columns.AddRange(new DataColumn[3] { new DataColumn("COMPANY", typeof(string)),
                            new DataColumn("YEAR OF INDIGINISATION",typeof(string)),new DataColumn("SEARCH KEYWORDS",typeof(string)) });
                            string csvData = File.ReadAllText(path);
                            foreach (string row in csvData.Split('\n'))
                            {
                                if (!string.IsNullOrEmpty(row))
                                {
                                    dtExcel.Rows.Add();
                                    int i = 0;
                                    foreach (string cell in row.Split(','))
                                    {
                                        dtExcel.Rows[dtExcel.Rows.Count - 1][i] = cell;
                                        i++;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ViewState["error"] = ex.Message;
                            diverror.Visible = true;
                            diverror.InnerHtml = ErrText + ex.Message;
                            diverror.Attributes.Add("class", "alert alert-danger");
                        }
                        // if excel import completed without error
                        if (dtExcel.Rows.Count < 1)
                        {
                            diverror.Visible = true;
                            diverror.InnerHtml = "No data imported from excel file !!" + ViewState["error"];
                            diverror.Attributes.Add("class", "alert alert-danger");
                            ViewState["error"] = null;
                            return;
                        }
                        else
                        {
                            try
                            {
                                var rowsCount = Convert.ToInt32(dtExcel.Rows.Count);
                                lblRowCount.Text = "Rows Processed:- " + rowsCount;
                                str = Lo.SaveExcelProduct(dtExcel);
                                if (str == "Save")
                                {
                                    diverror.Visible = true;
                                    diverror.InnerHtml = "Data imported successfully from excel file !!\n\nTotal Rows - " + dtExcel.Rows.Count.ToString() + ".";
                                    diverror.Attributes.Add("class", "alert alert-success");
                                    dtExcel.Dispose();
                                    dtExcel = null;
                                }
                                else
                                {
                                    diverror.Visible = true;
                                    diverror.InnerHtml = "User Error: Error in excel data. Technical Error: " + str + "";
                                    diverror.Attributes.Add("class", "alert alert-danger");
                                }
                            }
                            catch (Exception ex)
                            {
                                diverror.Visible = true;
                                diverror.InnerHtml = "User Error: Error in excel data. Technical Error: " + ex.Message + "";
                                diverror.Attributes.Add("class", "alert alert-danger");
                            }
                        }
                    }
                }
          
            
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Please fill all mandatory fields.')", true);
        }
    }

    protected void btnProduct_Click(object sender, EventArgs e)
    {
        if (rbtypefile.SelectedItem.Value == "1")
        {
            if (fuexcel.FileName == null && fuexcel.FileName == "")  //if no file selected the give error
            {
                diverror.Visible = true;
                diverror.InnerHtml = "Please select Excel file.";
                diverror.Attributes.Add("class", "alert alert-danger");
                return;
            }
            else //---------------------------------------------------if file is selected
            {
                string ErrText = "";
                DataTable dtExcel = new DataTable();
                try
                {
                    string path = Server.MapPath("~/App_Data/") + fuexcel.PostedFile.FileName;
                    fuexcel.SaveAs(path);
                    dtExcel = Lo.CreateExcelConnection(path, "Sheet1", out ErrText);
                    if (ErrText.ToLower() != "success") // if sqlhelper returning ERROR in importing
                    {
                        diverror.Visible = true;
                        diverror.InnerHtml = ErrText;
                        diverror.Attributes.Add("class", "alert alert-danger");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    ViewState["error"] = ex.Message;
                    diverror.Visible = true;
                    diverror.InnerHtml = ErrText + ex.Message;
                    diverror.Attributes.Add("class", "alert alert-danger");
                }
                // if excel import completed without error
                if (dtExcel.Rows.Count < 1)
                {
                    diverror.Visible = true;
                    diverror.InnerHtml = "No data imported from excel file !!" + ViewState["error"];
                    diverror.Attributes.Add("class", "alert alert-danger");
                    ViewState["error"] = null;
                    return;
                }
                else
                {
                    try
                    {
                        var rowsCount = Convert.ToInt32(dtExcel.Rows.Count);
                        lblRowCount.Text = "Rows Processed:- " + rowsCount;
                        str = Lo.SaveExcelProductforSHQ(dtExcel);
                        if (str == "Save")
                        {
                            diverror.Visible = true;
                            diverror.InnerHtml = "Data imported successfully from excel file !!\n\nTotal Rows - " + dtExcel.Rows.Count.ToString() + ".";
                            diverror.Attributes.Add("class", "alert alert-success");
                            dtExcel.Dispose();
                            dtExcel = null;
                        }
                        else
                        {
                            diverror.Visible = true;
                            diverror.InnerHtml = "User Error: Error in excel data. Technical Error: " + str + "";
                            diverror.Attributes.Add("class", "alert alert-danger");
                        }
                    }
                    catch (Exception ex)
                    {
                        diverror.Visible = true;
                        diverror.InnerHtml = "User Error: Error in excel data. Technical Error: " + ex.Message + "";
                        diverror.Attributes.Add("class", "alert alert-danger");
                    }
                }
            }
        }
        else
        {
            if (fuexcel.FileName == null && fuexcel.FileName == "")  //if no file selected the give error
            {
                diverror.Visible = true;
                diverror.InnerHtml = "Please select CSV Excel file.";
                diverror.Attributes.Add("class", "alert alert-danger");
                return;
            }
            else //---------------------------------------------------if file is selected
            {
                string ErrText = "";
                DataTable dtExcel = new DataTable();
                try
                {
                    string path = Server.MapPath("~/App_Data/") + fuexcel.PostedFile.FileName;
                    fuexcel.SaveAs(path);
                    dtExcel.Columns.AddRange(new DataColumn[3] { new DataColumn("COMPANY", typeof(string)),new DataColumn("YEAR OF INDIGINISATION",typeof(string)),new DataColumn("SEARCH KEYWORDS",typeof(string)) });
                    string csvData = File.ReadAllText(path);
                    foreach (string row in csvData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            dtExcel.Rows.Add();
                            int i = 0;
                            foreach (string cell in row.Split(','))
                            {
                                dtExcel.Rows[dtExcel.Rows.Count - 1][i] = cell;
                                i++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ViewState["error"] = ex.Message;
                    diverror.Visible = true;
                    diverror.InnerHtml = ErrText + ex.Message;
                    diverror.Attributes.Add("class", "alert alert-danger");
                }
                // if excel import completed without error
                if (dtExcel.Rows.Count < 1)
                {
                    diverror.Visible = true;
                    diverror.InnerHtml = "No data imported from excel file !!" + ViewState["error"];
                    diverror.Attributes.Add("class", "alert alert-danger");
                    ViewState["error"] = null;
                    return;
                }
                else
                {
                    try
                    {
                        var rowsCount = Convert.ToInt32(dtExcel.Rows.Count);
                        lblRowCount.Text = "Rows Processed:- " + rowsCount;
                        str = Lo.SaveExcelProductforSHQ(dtExcel);
                        if (str == "Save")
                        {
                            diverror.Visible = true;
                            diverror.InnerHtml = "Data imported successfully from excel file !!\n\nTotal Rows - " + dtExcel.Rows.Count.ToString() + ".";
                            diverror.Attributes.Add("class", "alert alert-success");
                            dtExcel.Dispose();
                            dtExcel = null;
                        }
                        else
                        {
                            diverror.Visible = true;
                            diverror.InnerHtml = "User Error: Error in excel data. Technical Error: " + str + "";
                            diverror.Attributes.Add("class", "alert alert-danger");
                        }
                    }
                    catch (Exception ex)
                    {
                        diverror.Visible = true;
                        diverror.InnerHtml = "User Error: Error in excel data. Technical Error: " + ex.Message + "";
                        diverror.Attributes.Add("class", "alert alert-danger");
                    }
                }
            }
        }
        if (rbtypefile.SelectedItem.Value == "1")
        {
            if (fuexcel.FileName == null && fuexcel.FileName == "")  //if no file selected the give error
            {
                diverror.Visible = true;
                diverror.InnerHtml = "Please select Excel file.";
                diverror.Attributes.Add("class", "alert alert-danger");
                return;
            }
            else //---------------------------------------------------if file is selected
            {
                string ErrText = "";
                DataTable dtExcel = new DataTable();
                try
                {
                    string path = Server.MapPath("~/App_Data/") + fuexcel.PostedFile.FileName;
                    fuexcel.SaveAs(path);
                    dtExcel = Lo.CreateExcelConnection(path, "Sheet1", out ErrText);
                    if (ErrText.ToLower() != "success") // if sqlhelper returning ERROR in importing
                    {
                        diverror.Visible = true;
                        diverror.InnerHtml = ErrText;
                        diverror.Attributes.Add("class", "alert alert-danger");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    ViewState["error"] = ex.Message;
                    diverror.Visible = true;
                    diverror.InnerHtml = ErrText + ex.Message;
                    diverror.Attributes.Add("class", "alert alert-danger");
                }
                // if excel import completed without error
                if (dtExcel.Rows.Count < 1)
                {
                    diverror.Visible = true;
                    diverror.InnerHtml = "No data imported from excel file !!" + ViewState["error"];
                    diverror.Attributes.Add("class", "alert alert-danger");
                    ViewState["error"] = null;
                    return;
                }
                else
                {
                    try
                    {
                        var rowsCount = Convert.ToInt32(dtExcel.Rows.Count);
                        lblRowCount.Text = "Rows Processed:- " + rowsCount;
                        str = Lo.SaveExcelProductforSHQ(dtExcel);
                        if (str == "Save")
                        {
                            diverror.Visible = true;
                            diverror.InnerHtml = "Data imported successfully from excel file !!\n\nTotal Rows - " + dtExcel.Rows.Count.ToString() + ".";
                            diverror.Attributes.Add("class", "alert alert-success");
                            dtExcel.Dispose();
                            dtExcel = null;
                        }
                        else
                        {
                            diverror.Visible = true;
                            diverror.InnerHtml = "User Error: Error in excel data. Technical Error: " + str + "";
                            diverror.Attributes.Add("class", "alert alert-danger");
                        }
                    }
                    catch (Exception ex)
                    {
                        diverror.Visible = true;
                        diverror.InnerHtml = "User Error: Error in excel data. Technical Error: " + ex.Message + "";
                        diverror.Attributes.Add("class", "alert alert-danger");
                    }
                }
            }
        }
        else
        {
            if (fuexcel.FileName == null && fuexcel.FileName == "")  //if no file selected the give error
            {
                diverror.Visible = true;
                diverror.InnerHtml = "Please select CSV Excel file.";
                diverror.Attributes.Add("class", "alert alert-danger");
                return;
            }
            else //---------------------------------------------------if file is selected
            {
                string ErrText = "";
                DataTable dtExcel = new DataTable();
                try
                {
                    string path = Server.MapPath("~/App_Data/") + fuexcel.PostedFile.FileName;
                    fuexcel.SaveAs(path);
                    dtExcel.Columns.AddRange(new DataColumn[3] { new DataColumn("COMPANY", typeof(string)),new DataColumn("YEAR OF INDIGINISATION",typeof(string)),new DataColumn("SEARCH KEYWORDS",typeof(string)) });
                    string csvData = File.ReadAllText(path);
                    foreach (string row in csvData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            dtExcel.Rows.Add();
                            int i = 0;
                            foreach (string cell in row.Split(','))
                            {
                                dtExcel.Rows[dtExcel.Rows.Count - 1][i] = cell;
                                i++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ViewState["error"] = ex.Message;
                    diverror.Visible = true;
                    diverror.InnerHtml = ErrText + ex.Message;
                    diverror.Attributes.Add("class", "alert alert-danger");
                }
                // if excel import completed without error
                if (dtExcel.Rows.Count < 1)
                {
                    diverror.Visible = true;
                    diverror.InnerHtml = "No data imported from excel file !!" + ViewState["error"];
                    diverror.Attributes.Add("class", "alert alert-danger");
                    ViewState["error"] = null;
                    return;
                }
                else
                {
                    try
                    {
                        var rowsCount = Convert.ToInt32(dtExcel.Rows.Count);
                        lblRowCount.Text = "Rows Processed:- " + rowsCount;
                        str = Lo.SaveExcelProductforSHQ(dtExcel);
                        if (str == "Save")
                        {
                            diverror.Visible = true;
                            diverror.InnerHtml = "Data imported successfully from excel file !!\n\nTotal Rows - " + dtExcel.Rows.Count.ToString() + ".";
                            diverror.Attributes.Add("class", "alert alert-success");
                            dtExcel.Dispose();
                            dtExcel = null;
                        }
                        else
                        {
                            diverror.Visible = true;
                            diverror.InnerHtml = "User Error: Error in excel data. Technical Error: " + str + "";
                            diverror.Attributes.Add("class", "alert alert-danger");
                        }
                    }
                    catch (Exception ex)
                    {
                        diverror.Visible = true;
                        diverror.InnerHtml = "User Error: Error in excel data. Technical Error: " + ex.Message + "";
                        diverror.Attributes.Add("class", "alert alert-danger");
                    }
                }
            }
        }
    }

    protected void btnexcel3510_Click(object sender, EventArgs e)
    {
        if (fuexcel.FileName == null && fuexcel.FileName == "") //if no file selected the give error
        {
            diverror.Visible = true;
            diverror.InnerHtml = "Please select Excel file.";
            diverror.Attributes.Add("class", "alert alert-danger");
            return;
        }
        else //---------------------------------------------------if file is selected
        {
            diverror.Visible = true;
            diverror.InnerHtml = fuexcel.FileName;
            string ErrText = "";
            DataTable dtExcel = new DataTable();
            try
            {
                string path = Server.MapPath("~/App_Data/") + fuexcel.PostedFile.FileName;
                fuexcel.SaveAs(path);
                dtExcel = Lo.CreateExcelConnection(path, "data", out ErrText);
                if (ErrText.ToLower() == "success") // if sqlhelper returning NO ERROR in importing
                {
                    DataView dv = new DataView(dtExcel);
                    dv.RowFilter = "Status='A'";
                    dtExcel = dv.ToTable();
                }
                else // ------------------------------if sqlhelper returning ERROR in importing
                {
                    diverror.Visible = true;
                    diverror.InnerHtml = ErrText;
                    return;
                }

            }
            catch (Exception ex)
            {
                diverror.Visible = true;
                diverror.InnerHtml = ErrText + ex.Message;
                diverror.Attributes.Add("class", "alert alert-danger");
            }

            // if excel import completed without error
            if (dtExcel.Rows.Count < 1)
            {
                diverror.Visible = true;
                diverror.InnerHtml = "No data imported from excel file !!";
                diverror.Attributes.Add("class", "alert alert-danger");
                return;
            }
            else
            {
                try
                {
                    var rowsCount = Convert.ToInt32(dtExcel.Rows.Count);
                    lblRowCount.Text = "Rows Processed:- " + rowsCount;
                    DataTable dtPid = Lo.RetrivePid((txtL1.Text), (txtL2.Text));
                    if (dtPid.Rows.Count < 1)
                    {
                        diverror.Visible = true;
                        diverror.InnerHtml = "No PID exist !!";
                        diverror.Attributes.Add("class", "alert alert-danger");
                        return;
                    }
                    str = Lo.SaveExcel3510(dtExcel, (txtL1.Text), (txtL2.Text), (dtPid.Rows[0][0].ToString()));
                    if (str == "Save")
                    {
                        diverror.Visible = true;
                        diverror.InnerHtml = "Data imported successfully from excel file !!\n\nTotal Rows - " + dtExcel.Rows.Count.ToString() + ".";
                        diverror.Attributes.Add("class", "alert alert-success");
                        dtExcel.Dispose();
                        dtExcel = null;
                    }
                    else
                    {
                        diverror.Visible = true;
                        diverror.InnerHtml = "User Error: Error in excel data. Technical Error: " + str + "";
                        diverror.Attributes.Add("class", "alert alert-danger");
                    }
                }
                catch (Exception ex)
                {
                    diverror.Visible = true;
                    diverror.InnerHtml = "User Error: Error in excel data. Technical Error: " + ex.Message + "";
                    diverror.Attributes.Add("class", "alert alert-danger");
                }
            }
        }
    }

    protected void btnexcel_Click(object sender, EventArgs e)
    {
        if (fuexcel.FileName == null && fuexcel.FileName == "")  //if no file selected the give error
        {
            diverror.Visible = true;
            diverror.InnerHtml = "Please select Excel file.";
            diverror.Attributes.Add("class", "alert alert-danger");
            return;
        }
        else //---------------------------------------------------if file is selected
        {
            string ErrText = "";
            DataTable dtExcel = new DataTable();
            DataTable dtMaster = new DataTable();
            try
            {
                string path = Server.MapPath("~/App_Data/") + fuexcel.PostedFile.FileName;
                fuexcel.SaveAs(path);
                dtExcel = Lo.CreateExcelConnection(path, "data", out ErrText);
                if (ErrText.ToLower() == "success") // if sqlhelper returning NO ERROR in importing
                {
                    dtMaster = dtExcel.Copy();
                    dtMaster = dtMaster.DefaultView.ToTable(true, "GROUP", "FSG Title");
                }
                else // ------------------------------if sqlhelper returning ERROR in importing
                {
                    diverror.Visible = true;
                    diverror.InnerHtml = ErrText;
                    diverror.Attributes.Add("class", "alert alert-danger");
                    return;
                }
            }
            catch (Exception ex)
            {
                diverror.Visible = true;
                diverror.InnerHtml = ErrText + ex.Message;
                diverror.Attributes.Add("class", "alert alert-danger");
            }
            // if excel import completed without error
            if (dtMaster.Rows.Count < 1)
            {
                diverror.Visible = true;
                diverror.InnerHtml = "No data imported from excel file !!";
                diverror.Attributes.Add("class", "alert alert-danger");
                return;
            }
            else
            {
                try
                {
                    var rowsCount = Convert.ToInt32(dtExcel.Rows.Count);
                    lblRowCount.Text = "Rows Processed:- " + rowsCount;
                    str = Lo.SaveUploadExcelCompany(dtMaster, dtExcel);
                    if (str == "Save")
                    {
                        diverror.Visible = true;
                        diverror.InnerHtml = "Data imported successfully from excel file !!\n\nTotal Rows - " + dtExcel.Rows.Count.ToString() + ".";
                        diverror.Attributes.Add("class", "alert alert-success");
                        dtExcel.Dispose();
                        dtExcel = null;
                    }
                    else
                    {
                        diverror.Visible = true;
                        diverror.InnerHtml = "User Error: Error in excel data. Technical Error: " + str + "";
                        diverror.Attributes.Add("class", "alert alert-danger");
                    }
                }
                catch (Exception ex)
                {
                    diverror.Visible = true;
                    diverror.InnerHtml = "User Error: Error in excel data. Technical Error: " + ex.Message + "";
                    diverror.Attributes.Add("class", "alert alert-danger");
                }
            }
        }
    }
}