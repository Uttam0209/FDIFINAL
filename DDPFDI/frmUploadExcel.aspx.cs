using System;
using BusinessLayer;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Collections.Specialized;
using Encryption;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public partial class frmUploadExcel : System.Web.UI.Page
{
    #region Variable
    Logic Lo = new Logic();
  
    string str = "";
    private DataUtility Co = new DataUtility();
    HybridDictionary hy = new HybridDictionary();
    Cryptography ObjEnc = new Cryptography();
    string Js;
    
        
    // List<Cls_ExJson> objroot = new List<Cls_ExJson>();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["CompanyRefNo"].ToString() == "C0023" || Session["CompanyRefNo"].ToString() == "C0024" || Session["CompanyRefNo"].ToString() == "C0025")
            {
                string companyno = Session["CompanyRefNo"].ToString();
                Response.Redirect("UExcelforSHQ");               
            }
            else
            {
                BindMasterCategory();
                BindCountry();
                BindEndUser();
                BindMasterPlatCategory();
                BindMasterTechnologyCategory();
                PROCURMENTCATEGORYIndigenization();
                BindQualityAssurance();
            }
        }
    }
    #region MasterDataToUploadExcel
    protected void ddlmastercategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMasterSubCategory();
    }
    protected void ddlsubcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        string m = ddlsubcategory.SelectedItem.Value;
        DataTable dt2 = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlsubcategory.SelectedItem.Value), "", "", "2to2", "", "");
        if (dt2.Rows.Count > 0)
        {
            DataTable dt1sr = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlsubcategory.SelectedItem.Value), "", "", "3to21", "", "");
            ddlmastercategory.SelectedValue = dt1sr.Rows[0]["SCategoryId"].ToString();
            Co.FillDropdownlist(ddlsubcategory, dt2, "SCategoryName", "SCategoryId");
            ddlsubcategory.Items.Insert(0, "Select");
            ddlsubcategory.SelectedValue = m;
            DataTable dtbindvalue = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlsubcategory.SelectedItem.Value), "", "", "SubSelectID", "", "");
            if (dtbindvalue.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddllevel3product, dtbindvalue, "SCategoryName", "SCategoryId");
                ddllevel3product.Items.Insert(0, "Select");
            }
            else
            {
                ddllevel3product.Items.Clear();
                ddllevel3product.Items.Insert(0, "Select");
                ddllevel3product.Items.Insert(1, "NA");
            }
        }
        NSCCode(ddlmastercategory.SelectedItem.Text, ddlsubcategory.SelectedItem.Text);
    }
    protected void BindMasterSubCategory()
    {
        DataTable DtMasterCategroy = new DataTable();
        if (ddlmastercategory.SelectedItem.Text != "Select")
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlmastercategory.SelectedItem.Value), "", "", "SubSelectID", "", "");
        }
        else
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "", "", "SubSelectSec", "", "");
        }
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlsubcategory, DtMasterCategroy, "SCategoryName", "SCategoryId");
            ddlsubcategory.Items.Insert(0, "Select");
            ddllevel3product.SelectedIndex = -1;
        }
        else
        {
            ddlsubcategory.Items.Clear();
            ddlsubcategory.Items.Insert(0, "Select");
        }
    }
    protected void BindMasterCategory()
    {
        DataTable DtMasterCategroy = new DataTable();
        DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "NSN GROUP", "", "SelectProductCat", "", "");
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlmastercategory, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlmastercategory.Items.Insert(0, "Select");
            ddlsubcategory.SelectedIndex = -1;
            ddllevel3product.SelectedIndex = -1;
        }
        else
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "NSN GROUP", "", "SelectProductCat", "", "");
            Co.FillDropdownlist(ddlmastercategory, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlmastercategory.Items.Insert(0, "Select");
            ddlsubcategory.SelectedIndex = -1;
            ddllevel3product.SelectedIndex = -1;
        }
    }
    private void NSCCode(string NSNGroupddl, string NSNClassddl)
    {
        try
        {
            string a = NSNGroupddl.Substring((NSNGroupddl.IndexOf("(") + 1), NSNGroupddl.IndexOf(")") - (NSNGroupddl.IndexOf("(") + 1));
            string b = NSNClassddl.Substring((NSNClassddl.IndexOf("(") + 1), NSNClassddl.IndexOf(")") - (NSNClassddl.IndexOf("(") + 1));
            lblnsccode.Text = a + b;
        }
        catch (Exception)
        {

        }
    }
    protected void BindCountry()
    {
        DataTable DtCountry = Lo.RetriveCountry(0, "Select");
        if (DtCountry.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlcountry, DtCountry, "CountryName", "CountryID");
            ddlcountry.Items.Insert(0, "Select");
        }
    }
    protected void BindEndUser()
    {
        DataTable DtMasterCategroy = new DataTable();
        DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, lblenduser.Text, "", "SelectInnerMaster1", "", "");
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillCheckBox(ddlenduser, DtMasterCategroy, "SCategoryName", "SCategoryID");
        }
        else
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, lblenduser.Text, "", "SelectInnerMaster1", "", "");
            Co.FillCheckBox(ddlenduser, DtMasterCategroy, "SCategoryName", "SCategoryID");
        }
    }
    protected void ddlplatform_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlplatform.SelectedItem.Text != "Select")
        {
            BindMasterProductNoenCletureCategory();
        }
        else
        {
            ddlnomnclature.Items.Clear();
        }
    }
    protected void ddltechnologycat_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMasterSubCategoryTech();
    }
    protected void BindMasterSubCategoryTech()
    {
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddltechnologycat.SelectedItem.Value), "", "", "SubSelectID", "", "");
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlsubtech, DtMasterCategroy, "SCategoryName", "SCategoryId");
            ddlsubtech.Items.Insert(0, "Select");
        }
        else
        {
            ddlsubtech.Items.Clear();
            ddlsubtech.Items.Insert(0, "Select");
        }
    }
    protected void BindMasterProductNoenCletureCategory()
    {
        DataTable DtNAMEOFDEFENCEPLATFORM = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlplatform.SelectedItem.Value), "", "", "SubSelectID", "", "");
        if (DtNAMEOFDEFENCEPLATFORM.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlnomnclature, DtNAMEOFDEFENCEPLATFORM, "SCategoryName", "SCategoryId");
            ddlnomnclature.Items.Insert(0, "Select");
        }
        else
        {
            ddlnomnclature.Items.Clear();
            ddlnomnclature.Items.Insert(0, "Select");
        }
    }
    protected void PROCURMENTCATEGORYIndigenization()
    {
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "PROCURMENT CATEGORY", "", "SelectProductCat", "", "");
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillRadioBoxList(rbIgCategory, DtMasterCategroy, "SCategoryName", "SCategoryID");
        }
    }
    protected void BindMasterPlatCategory()
    {
        DataTable DtMasterCategroy = new DataTable();
        DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "DEFENCE PLATFORM", "", "SelectProductCat", "", "");
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlplatform, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlplatform.Items.Insert(0, "Select");
        }
        else
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "DEFENCE PLATFORM", "", "SelectProductCat", "", "");
            Co.FillDropdownlist(ddlplatform, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlplatform.Items.Insert(0, "Select");
        }
    }
    protected void BindMasterTechnologyCategory()
    {
        DataTable DtMasterCategroy = new DataTable();
        DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "PRODUCT (INDUSTRY DOMAIN)", "", "SelectProductCat", "", "");
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddltechnologycat, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddltechnologycat.Items.Insert(0, "Select");
        }
        else
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "PRODUCT (INDUSTRY DOMAIN)", "", "SelectProductCat", "", "");
            Co.FillDropdownlist(ddltechnologycat, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddltechnologycat.Items.Insert(0, "Select");
        }
    }
    protected void BindQualityAssurance()
    {
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "QA AGENCY", "", "SelectProductCat", "", "");
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillCheckBox(chkQAA, DtMasterCategroy, "SCategoryName", "SCategoryID");
        }
    }
    #endregion   
    protected void btnexcelProduct_Click(object sender, EventArgs e)
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
                    dtExcel.Columns.AddRange(new DataColumn[24] { new DataColumn("COMPANY", typeof(string)),new DataColumn("DIVISION", typeof(string)), new DataColumn("UNIT",typeof(string)),
                  new DataColumn("NSN GROUP", typeof(string)), new DataColumn("NSN GROUP CLASS",typeof(string)),new DataColumn("ITEM CODE", typeof(string)),
                  new DataColumn("NSC CODE",typeof(string)),new DataColumn("NIIN CODE",typeof(string)),new DataColumn("ITEM DESCRIPTION",typeof(string)),new DataColumn("OEM PART NUMBER",typeof(string)),
                  new DataColumn("OEM NAME",typeof(string)),new DataColumn("OEM COUNTRY",typeof(string)),  new DataColumn("DPSU PART NUMBER",typeof(string)),
                  new DataColumn("HSN CODE",typeof(string)),new DataColumn("END USER", typeof(string)),new DataColumn("DEFENCE PLATFORM", typeof(string)),
                  new DataColumn("NAME OF DEFENCE PLATFORM",typeof(string)),new DataColumn("PRODUCT (INDUSTRY DOMAIN)", typeof(string)),
                  new DataColumn("PRODUCT (INDUSTRY SUB DOMAIN)",typeof(string)), new DataColumn("PRODUCT (INDUSTRY 2ND SUB DOMAIN)", typeof(string)),
                  new DataColumn("MANUFACTURER NAME IF INDIGINISED",typeof(string)),new DataColumn("MANUFACTURER ADD",typeof(string)),
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
    //fINAL UPLOAD NEW CODE OF EXCEL
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (checkvalidation(ddlenduser.SelectedItem.Value) != false && checkvalidation(ddllevel3product.SelectedItem.Value) != false && checkvalidation(ddlmastercategory.SelectedItem.Value) != false
                && checkvalidation(ddlnomnclature.SelectedItem.Value) != false
                 && checkvalidation(ddlplatform.SelectedItem.Value) != false && checkvalidation(ddlsubcategory.SelectedItem.Value) != false && checkvalidation(ddlsubtech.SelectedItem.Value) != false
                 && checkvalidation(ddltechnologycat.SelectedItem.Value) != false)
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
                        DataTable dtJson = new DataTable();
                        try
                        {
                            string path = Server.MapPath("~/App_Data/") + fuexcel.PostedFile.FileName;
                            fuexcel.SaveAs(path);
                            dtExcel = Lo.CreateExcelConnection(path, "Sheet1", out ErrText);
                             Js = DataTableToJSONWithStringBuilder(dtExcel);
                            dtJson= JsonStringToDataTable(Js);
                            

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
                        if (dtJson.Rows.Count < 1)
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
                                var rowsCount = Convert.ToInt32(dtJson.Rows.Count);
                                lblRowCount.Text = "Rows Processed:- " + rowsCount;
                                SaveExcelHy();
                                str =Lo.SaveExcelProduct1(dtJson, hy);
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Please fill all mandatory fields.')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Please fill all mandatory fields.')", true);
        }
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
        hy["ProductLevel1"] = ddlmastercategory.SelectedItem.Value;
        hy["ProductLevel2"] = ddlsubcategory.SelectedItem.Value;
        hy["ProductLevel3"] = ddllevel3product.SelectedItem.Value;
        hy["NSCCode"] = lblnsccode.Text;
        hy["OEMCountry"] = ddlcountry.SelectedItem.Value;
        if (ddlenduser.SelectedIndex != -1)
        {
            for (int o = 0; o < ddlenduser.Items.Count; o++)
            {
                if (ddlenduser.Items[o].Selected == true)
                {
                    m = m + ddlenduser.Items[o].Value + ",";
                }
            }
            hy["EndUser"] = m.ToString().Trim();
        }
        else
        {
            hy["EndUser"] = null;
        }
        hy["Platform"] = ddlplatform.SelectedItem.Value;
        hy["NomenclatureOfMainSystem"] = ddlnomnclature.SelectedItem.Value;
        hy["TechnologyLevel1"] = ddltechnologycat.SelectedItem.Value;
        hy["TechnologyLevel2"] = ddlsubtech.SelectedItem.Value;
        hy["IsProductImported"] = "Y";
        hy["PurposeofProcurement"] = rbIgCategory.SelectedItem.Value;
        for (int i = 0; i < chkQAA.Items.Count; i++)
        {
            if (chkQAA.Items[i].Selected == true)
            {
                chk = chk + chkQAA.Items[i].Value + ",";
            }
        }
        if (chk != null)
        {
            hy["QAAgency"] = chk.ToString().Trim();
        }
        else
        {
            hy["QAAgency"] = "";
        }
        hy["IndProcess"] = "Y";
        DataTable DtSendMailForgotpassword = Lo.RetriveForgotPasswordEmail(Co.RSQandSQLInjection(ObjEnc.DecryptData(Session["User"].ToString()), "soft"), "ForgotPassword");
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
    public string DataTableToJSONWithStringBuilder(DataTable table)
    {
        var JSONString = new StringBuilder();
        if (table.Rows.Count > 0)
        {
            JSONString.Append("[");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                JSONString.Append("{");
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    if (j < table.Columns.Count - 1)
                    {
                        JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                    }
                    else if (j == table.Columns.Count - 1)
                    {
                        JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                    }
                }
                if (i == table.Rows.Count - 1)
                {
                    JSONString.Append("}");
                }
                else
                {
                    JSONString.Append("},");
                }
            }
            JSONString.Append("]");
        }
        return JSONString.ToString();
    }
    // By using this method we can convert datatable to xml
    public string ConvertDatatableToXML(DataTable dt)
    {
        MemoryStream str = new MemoryStream();
        dt.WriteXml(str, true);
        str.Seek(0, SeekOrigin.Begin);
        StreamReader sr = new StreamReader(str);
        string xmlstr;
        xmlstr = sr.ReadToEnd();
        return (xmlstr);
    }
    public DataTable JsonStringToDataTable(string jsonString)
    {
        DataTable dt = new DataTable();
        string[] jsonStringArray = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");
        List<string> ColumnsName = new List<string>();
        foreach (string jSA in jsonStringArray)
        {
            string[] jsonStringData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
            foreach (string ColumnsNameData in jsonStringData)
            {
                try
                {
                    int idx = ColumnsNameData.IndexOf(":");
                    string ColumnsNameString = ColumnsNameData.Substring(0, idx - 1).Replace("\"", "");
                    if (!ColumnsName.Contains(ColumnsNameString))
                    {
                        ColumnsName.Add(ColumnsNameString);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
                }
            }
            break;
        }
        foreach (string AddColumnName in ColumnsName)
        {
            dt.Columns.Add(AddColumnName);
        }
        foreach (string jSA in jsonStringArray)
        {
            string[] RowData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
            DataRow nr = dt.NewRow();
            foreach (string rowData in RowData)
            {
                try
                {
                    int idx = rowData.IndexOf(":");
                    string RowColumns = rowData.Substring(0, idx - 1).Replace("\"", "");
                    string RowDataString = rowData.Substring(idx + 1).Replace("\"", "");
                    nr[RowColumns] = RowDataString;
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            dt.Rows.Add(nr);
        }
        return dt;
    }
}



