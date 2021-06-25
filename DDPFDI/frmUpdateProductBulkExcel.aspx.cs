using BusinessLayer;
using Encryption;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Collections.Specialized;
using context = System.Web.HttpContext;
using System.Web.UI.HtmlControls;


public partial class Admin_frmUpdateProductBulkExcel : System.Web.UI.Page
{
    Logic Lo = new Logic();
    string str = "";
    Cryptography objEnc = new Cryptography();
    DataTable DtCompanyDDL = new DataTable();
    private DataUtility Co = new DataUtility();     
    HybridDictionary hy = new HybridDictionary();
    Cryptography ObjEnc = new Cryptography();
    private Cryptography Encrypt = new Cryptography();
   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            if (Request.QueryString["mcurrentcompRefNo"] != null)
            {

                hidType.Value = objEnc.DecryptData(Request.QueryString["mrcreaterole"].ToString().Trim());
                hfcomprefno.Value = objEnc.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString().Trim());
                Session["CompanyRefNo"] = hfcomprefno.Value.ToString();
                if (Session["CompanyRefNo"].ToString() == "C0023" || Session["CompanyRefNo"].ToString() == "C0024" || Session["CompanyRefNo"].ToString() == "C0025")
                {
                    string companyno = Session["CompanyRefNo"].ToString();
                    hidType.Value = Session["CompanyRefNo"].ToString().ToString().Trim();
                    Response.Redirect("SHQUpdateBulkRecordExcel");
                }
                else
                {
                    hidType.Value = objEnc.DecryptData(Session["Type"].ToString().Trim());
                    hfcomprefno.Value = Session["CompanyRefNo"].ToString().Trim();
                }
            }
            else
            {
                hidType.Value = objEnc.DecryptData(Session["Type"].ToString().Trim());
                hfcomprefno.Value = Session["CompanyRefNo"].ToString().Trim();
                if (Session["CompanyRefNo"].ToString() == "C0023" || Session["CompanyRefNo"].ToString() == "C0024" || Session["CompanyRefNo"].ToString() == "C0025")
                {
                    string companyno = Session["CompanyRefNo"].ToString();
                    hidType.Value = Session["CompanyRefNo"].ToString().ToString().Trim();
                    Response.Redirect("SHQUpdateBulkRecordExcel");
                }
                else
                {
                    hidType.Value = objEnc.DecryptData(Session["Type"].ToString().Trim());
                    hfcomprefno.Value = Session["CompanyRefNo"].ToString().Trim();
                }
            }
            BindCompany();
           
            //divuploadsection.Visible = false;
        }
    }

    
    protected void ddlcompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcompany.SelectedItem.Text != "Select")
        {
            //if (ddlcompany.SelectedItem.Text == "SHQ(ARMY)" || ddlcompany.SelectedItem.Text == "SHQ(NAVY)" || ddlcompany.SelectedItem.Text == "SHQ(AIR FORCE)")
            //{
            //    Response.Redirect("SHQUpdateBulkRecordExcel");
            //}
            //else
            //{
                DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "", 0, "", "", "FactoryCompanyID");
                if (DtCompanyDDL.Rows.Count > 0)
                {
                    Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                    ddldivision.Items.Insert(0, "Select");
                    divlblselectdivison.Visible = true;
                    ddldivision.Visible = true;
                    hfcomprefno.Value = ddlcompany.SelectedItem.Value;
                    hidType.Value = "Company";
                }
                else
                {
                    ddldivision.Items.Insert(0, "Select");
                    ddldivision.Visible = false;
                    divlblselectdivison.Visible = false;
                }
            //}
        }
        else if (ddlcompany.SelectedItem.Text == "Select")
        {
            divlblselectdivison.Visible = false;
            divlblselectunit.Visible = false;
        }
            hfcomprefno.Value = "";
            hfcomprefno.Value = ddlcompany.SelectedItem.Value;
        
    }
    protected void ddldivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldivision.SelectedItem.Text != "Select")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "", 0, "", "", "UnitSelectID");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
                ddlunit.Items.Insert(0, "Select");
                ddlunit.Visible = true;
                divlblselectunit.Visible = true;
                if (ddlunit.SelectedItem.Text == "Select")
                {
                    ddldivision.Enabled = true;
                }
                else
                { ddldivision.Enabled = false; }
                hfcomprefno.Value = ddldivision.SelectedItem.Value;
                hidType.Value = "Divison";
            }
            else
            {
                ddlunit.Items.Insert(0, "Select");
                divlblselectunit.Visible = false;
                ddlunit.Visible = false;
            }
            hfcomprefno.Value = "";
            hfcomprefno.Value = ddldivision.SelectedItem.Value;
        }
        else if (ddldivision.SelectedItem.Text == "Select")
        {
            divlblselectunit.Visible = false;
            hfcomprefno.Value = ddlcompany.SelectedItem.Value;
            hidType.Value = "Company";
        }
    }
    protected void ddlunit_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlunit.SelectedItem.Text != "Select")
        {
            hfcomprefno.Value = ddlunit.SelectedItem.Value;
            hidType.Value = "Unit";
            hfcomprefno.Value = "";
            hfcomprefno.Value = ddlunit.SelectedItem.Value;
        }
        else
        {
            hfcomprefno.Value = ddldivision.SelectedItem.Value;
            hidType.Value = "Division";
            hfcomprefno.Value = "";
            hfcomprefno.Value = ddldivision.SelectedItem.Value;
        }
    }
    protected void BindCompany()
    {
        if (hidType.Value == "SuperAdmin" || hidType.Value == "Admin")
        {
            if (Request.QueryString["mcurrentcompRefNo"] != null)
            {
                ddlcompany.Enabled = false;
                if (objEnc.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Company")
                {
                    DtCompanyDDL = Lo.RetriveMasterData(0, HttpUtility.UrlEncode(objEnc.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString())), "Company", 0, "", "", "CompanyName");
                    ddlcompany.Enabled = false;
                    if (DtCompanyDDL.Rows.Count > 0)
                    {
                        Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                        divlblselectunit.Visible = false;
                        divlblselectdivison.Visible = false;
                    }
                }
                else if (objEnc.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Factory" || objEnc.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Division")
                {
                    DtCompanyDDL = Lo.RetriveMasterData(0, objEnc.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "Company1", 0, "", "", "CompanyName");
                    ddlcompany.Enabled = false;
                    if (DtCompanyDDL.Rows.Count > 0)
                    {
                        Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                    }
                    DtCompanyDDL = Lo.RetriveMasterData(0, objEnc.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "Factory", 0, "", "", "CompanyName");
                    DataTable DtDivisionDDL = Lo.RetriveMasterData(0, DtCompanyDDL.Rows[0]["CompanyRefNo"].ToString(), "Factory1", 0, "", "", "CompanyName");
                    if (DtDivisionDDL.Rows.Count > 0)
                    {
                        Co.FillDropdownlist(ddldivision, DtDivisionDDL, "FactoryName", "FactoryRefNo");
                        ddldivision.Enabled = false;
                        ddlcompany.Enabled = false;
                        ddldivision.Visible = true;
                        divlblselectunit.Visible = false;
                    }
                    else
                    {
                        ddldivision.Enabled = false;
                    }
                }
                else if (objEnc.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Unit")
                {
                    DtCompanyDDL = Lo.RetriveMasterData(0, objEnc.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "Company2", 0, "", "", "CompanyName");
                    ddlcompany.Enabled = false;
                    if (DtCompanyDDL.Rows.Count > 0)
                    {
                        Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                    }
                    DtCompanyDDL = Lo.RetriveMasterData(0, objEnc.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString()), "Unit", 0, "", "", "CompanyName");
                    DataTable DtDivisionDDL = Lo.RetriveMasterData(0, DtCompanyDDL.Rows[0]["CompanyRefNo"].ToString(), "Factory1", 0, "", "", "CompanyName");
                    if (DtDivisionDDL.Rows.Count > 0)
                    {
                        Co.FillDropdownlist(ddldivision, DtDivisionDDL, "FactoryName", "FactoryRefNo");
                        divlblselectdivison.Visible = true;
                        ddldivision.Enabled = false;
                        ddlcompany.Enabled = false;
                        ddldivision.Visible = true;
                        DataTable DtUnitDDL = Lo.RetriveMasterData(0, DtDivisionDDL.Rows[0]["FactoryRefNo"].ToString(), "Unit1", 0, "", "", "CompanyName");
                        if (DtUnitDDL.Rows.Count > 0)
                        {
                            Co.FillDropdownlist(ddlunit, DtUnitDDL, "UnitName", "UnitRefNo");
                            ddlunit.Enabled = true;
                            divlblselectunit.Visible = true;
                            ddlunit.Visible = true;
                            ddlunit.Enabled = false;
                        }
                        else
                        {
                            ddlunit.Enabled = false;
                        }
                    }
                    else
                    {
                        ddldivision.Enabled = false;
                    }
                }
            }
            else
            {
                DtCompanyDDL = Lo.RetriveMasterData(0, "", hidType.Value, 0, "", "", "Select");
                if (DtCompanyDDL.Rows.Count > 0)
                {
                    Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                    ddlcompany.Items.Insert(0, "Select");
                    ddlcompany.Enabled = true;
                    divlblselectdivison.Visible = false;
                    divlblselectunit.Visible = false;
                }
                else
                {
                    ddlcompany.Enabled = false;
                }
            }
        }
        else if (hidType.Value == "Company")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, hfcomprefno.Value, "Company", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = false;
                ddldivision.Items.Insert(0, "Select");
                divlblselectdivison.Visible = false;
                divlblselectunit.Visible = false;
            }
            else
            {
                ddlcompany.Enabled = false;
            }
        }
        else if (hidType.Value == "Factory" || hidType.Value == "Division")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, hfcomprefno.Value, "Company1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {

                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = false;
            }
            else
            {
                ddlcompany.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddlcompany.SelectedItem.Value, "Factory1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                DataView dvfac = new DataView(DtCompanyDDL);
                dvfac.RowFilter = "FactoryRefno = '" + hfcomprefno.Value + "'";
                DtCompanyDDL = dvfac.ToTable();
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                DataTable dt = Lo.RetriveMasterData(0, hfcomprefno.Value, "Factory2", 0, "", "", "CompanyName");
                if (dt.Rows.Count > 0)
                {
                    ddldivision.SelectedValue = dt.Rows[0]["FactoryRefNo"].ToString();
                }
                divlblselectunit.Visible = false;
                divlblselectdivison.Visible = true;
                ddldivision.Enabled = false;
            }
            else
            {
                ddldivision.Enabled = false;
            }
        }
        else if (hidType.Value == "Unit")
        {
            DtCompanyDDL = Lo.RetriveMasterData(0, hfcomprefno.Value, "Company2", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
                ddlcompany.Enabled = false;
            }
            else
            {
                ddlcompany.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, hfcomprefno.Value, "Factory3", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddldivision, DtCompanyDDL, "FactoryName", "FactoryRefNo");
                ddldivision.SelectedValue = DtCompanyDDL.Rows[0]["FactoryRefNo"].ToString();
                divlblselectdivison.Visible = true;
                ddldivision.Enabled = false;
            }
            else
            {
                ddldivision.Enabled = false;
            }
            DtCompanyDDL = Lo.RetriveMasterData(0, ddldivision.SelectedItem.Value, "Unit1", 0, "", "", "CompanyName");
            if (DtCompanyDDL.Rows.Count > 0)
            {
                DataView dvfac = new DataView(DtCompanyDDL);
                dvfac.RowFilter = "UnitRefNo = '" + hfcomprefno.Value + "'";
                DtCompanyDDL = dvfac.ToTable();
                Co.FillDropdownlist(ddlunit, DtCompanyDDL, "UnitName", "UnitRefNo");
                ddlunit.SelectedValue = hfcomprefno.Value;
                ddlunit.Enabled = false;
                divlblselectunit.Visible = true;
            }
            else
            {
                ddlunit.Enabled = false;
            }
        }
    }
    protected void ExcelExport(DataTable Dt)
    {
        try
        {
            //   DataSet DS = new DataSet();
            //  Dt = DS.Tables[0];
            string attachment = "attachment; filename=" + hidType.Value + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in Dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in Dt.Rows)
            {
                tab = "";
                for (i = 0; i < Dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }
        catch (Exception Ex)
        { }
    }
    protected void Dvinsert()
    {
        DataTable DtData = new DataTable();
        if (hidType.Value == "SuperAdmin" || hidType.Value == "Admin")
        {
            if (ddlcompany.SelectedItem.Text != "Select")
            {
                DtData = Lo.NewRetriveFilterCode("ExcelImport", ddlcompany.SelectedItem.Value, "", "", "", 0, 0, 0);
            }
            else if (ddldivision.Visible == true && ddldivision.SelectedItem.Text != "Select")
            {
                DtData = Lo.NewRetriveFilterCode("ExcelImport", ddldivision.SelectedItem.Value, "", "", "", 0, 0, 0);
            }
            else if (ddlunit.Visible == true && ddlunit.SelectedItem.Text != "Select")
            {
                DtData = Lo.NewRetriveFilterCode("ExcelImport", ddlunit.SelectedItem.Value, "", "", "", 0, 0, 0);
            }
        }
        else
        {
            if (ddlcompany.SelectedItem.Text != "Select" && hidType.Value == "Company")
            {
                DtData = Lo.NewRetriveFilterCode("ExcelImport", ddlcompany.SelectedItem.Value, "", "", "", 0, 0, 0);
            }
            else if (ddldivision.Visible == true && ddldivision.SelectedItem.Text != "Select" && hidType.Value == "Factory")
            {
                DtData = Lo.NewRetriveFilterCode("ExcelImport", ddldivision.SelectedItem.Value, "", "", "", 0, 0, 0);

            }
            else if (ddlunit.Visible == true && ddlunit.SelectedItem.Text != "Select" && hidType.Value == "Unit")
            {
                DtData = Lo.NewRetriveFilterCode("ExcelImport", ddlunit.SelectedItem.Value, "", "", "", 0, 0, 0);
            }
        }
        if (DtData.Rows.Count > 0)
        {
            ExcelExport(DtData);
            divuploadsection.Visible = true;
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
        }
    }
    protected void btndownloadTempexcel_Click(object sender, EventArgs e)
    {
        try
        {
            divuploadsection.Visible = true;
            Dvinsert();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Oops error occured')", true);
        }
    }

    protected void SaveExcelHy()
    {
        hy["ProductID"] = 0;
        hy["CompanyRefNo"] = Session["CompanyRefNo"].ToString();      
        hy["IsProductImported"] = "Y";
       
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
    
    protected void btnProduct_Click(object sender, EventArgs e)
    {
        try
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
                        str = Lo.SaveExcelProduct2(dtExcel);
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
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Please fill all mandatory fields.')", true);
        }
    }
}