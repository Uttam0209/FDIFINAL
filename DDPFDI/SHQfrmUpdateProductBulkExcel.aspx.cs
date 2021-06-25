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

public partial class SHQfrmUpdateProductBulkExcel : System.Web.UI.Page
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
            if (Session["CompanyRefNo"].ToString() == "C0023" || Session["CompanyRefNo"].ToString() == "C0024" || Session["CompanyRefNo"].ToString() == "C0025")
            {
                string companyno = Session["CompanyRefNo"].ToString();
                hidType.Value = Session["CompanyRefNo"].ToString().ToString().Trim();
                //hfcomprefno.Value = objEnc.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString().Trim());
                msg.Visible = false;
                //BindCompany();


            }
            else
            {
                hidType.Value = objEnc.DecryptData(Session["Type"].ToString().Trim());
                hfcomprefno.Value = Session["CompanyRefNo"].ToString().Trim();
                msg.Visible = true;
                Response.Redirect("Dashboard");
            }

        }
        //if (!IsPostBack)
        //{
        //    if (Request.QueryString["mcurrentcompRefNo"] != null)
        //    {
        //        hidType.Value = objEnc.DecryptData(Request.QueryString["mrcreaterole"].ToString().Trim());
        //        hfcomprefno.Value = objEnc.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString().Trim());
        //    }
        //    else
        //    {
        //        hidType.Value = objEnc.DecryptData(Session["Type"].ToString().Trim());
        //        hfcomprefno.Value = Session["CompanyRefNo"].ToString().Trim();
        //    }
        //    BindCompany();

        //    //divuploadsection.Visible = false;
        //}
    }
    //protected void BindCompany()
    //{
    //    if (hidType.Value == "SuperAdmin" || hidType.Value == "Admin")
    //    {
    //        if (Request.QueryString["mcurrentcompRefNo"] != null)
    //        {
    //            ddlcompany.Enabled = false;
    //            if (objEnc.DecryptData(Request.QueryString["mrcreaterole"].ToString()) == "Company")
    //            {
    //                DtCompanyDDL = Lo.RetriveMasterData(0, HttpUtility.UrlEncode(objEnc.DecryptData(Request.QueryString["mcurrentcompRefNo"].ToString())), "Company", 0, "", "", "CompanyName");
    //                ddlcompany.Enabled = false;
    //                if (DtCompanyDDL.Rows.Count > 0)
    //                {
    //                    Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
    //                    //divlblselectunit.Visible = false;
    //                    //divlblselectdivison.Visible = false;
    //                }
    //            }
               
    //        }
    //        else
    //        {
    //            DtCompanyDDL = Lo.RetriveMasterData(0, "", hidType.Value, 0, "", "", "Select");
    //            if (DtCompanyDDL.Rows.Count > 0)
    //            {
    //                Co.FillDropdownlist(ddlcompany, DtCompanyDDL, "CompanyName", "CompanyRefNo");
    //                ddlcompany.Items.Insert(0, "Select");
    //                ddlcompany.Enabled = true;
    //                //divlblselectdivison.Visible = false;
    //                //divlblselectunit.Visible = false;
    //            }
    //            else
    //            {
    //                ddlcompany.Enabled = false;
    //            }
    //        }
    //    }        
    //}

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
                DtData = Lo.NewRetriveFilterCode("ExcelImportforSHQ", ddlcompany.SelectedItem.Value, "", "", "", 0, 0, 0);
            }
           
        }
        else
        {
            if (ddlcompany.SelectedItem.Text != "Select" )
            {
                DtData = Lo.NewRetriveFilterCode("ExcelImportforSHQ", ddlcompany.SelectedItem.Value, "", "", "", 0, 0, 0);
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
            hy["NodelDetail"] = DtSendMailForgotpassword.Rows[0]["NodalOfficerEmail"].ToString();
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
                        str = Lo.SaveExcelProduct2FORSHQ(dtExcel);
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