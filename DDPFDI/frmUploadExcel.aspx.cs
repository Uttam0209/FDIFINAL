using System;
using BusinessLayer;
using System.Data;

public partial class frmUploadExcel : System.Web.UI.Page
{
    #region Variable
    Logic Lo = new Logic();
    string str = "";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    string msg = "";
        //    Lo.VerifyEmailandCompany("rgera@nic.in", "DDP", out msg);
        //    Page.Form.Attributes.Add("enctype", "multipart/form-data");
        //}
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
            DataTable dtMaster = new DataTable();
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
}

