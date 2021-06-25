using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_UpdateExcelData : System.Web.UI.Page
{
    #region Variable
    Logic Lo = new Logic();
    string str = "";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

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
    protected void btnexcelProduct_Click(object sender, EventArgs e)
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
                    dtExcel.Columns.AddRange(new DataColumn[24] { new DataColumn("ProductID", typeof(string)),new DataColumn("ProductRefNo", typeof(string)), new DataColumn("CompanyRefNo",typeof(string)),
                  new DataColumn("ProductLevel1", typeof(string)), new DataColumn("ProductLevel2",typeof(string)),new DataColumn("ProductLevel3", typeof(string)),
                  new DataColumn("ProductDescription",typeof(string)),new DataColumn("NSCCode",typeof(string)),new DataColumn("NIINCode",typeof(string)),new DataColumn("FeatursandDetail",typeof(string)),
                  new DataColumn("OEMPartNumber",typeof(string)),new DataColumn("OEM COUNTRY",typeof(string)),  new DataColumn("DPSU PART NUMBER",typeof(string)),
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