using System;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using Encryption;

public partial class Admin_RegisterdAs : System.Web.UI.Page
{
    Cryptography objEnc = new Cryptography();
    DataUtility Co = new DataUtility();
    Logic Lo = new Logic();
    private string DisplayPanel;
    HybridDictionary hySave = new HybridDictionary();
    private string mRefNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != null)
        {
            if (!IsPostBack)
            {
                if (Session["User"] != null)
                {
                    try
                    {
                        string strid = Request.QueryString["id"].ToString().Replace(" ", "+");
                        string strPageName = objEnc.DecryptData(strid);
                        StringBuilder strheadPage = new StringBuilder();
                        strheadPage.Append("<ul class='breadcrumb'>");
                        string[] MCateg = strPageName.Split(new string[] { ">>" }, StringSplitOptions.RemoveEmptyEntries);
                        string MmCval = "";
                        for (int x = 0; x < MCateg.Length; x++)
                        {
                            MmCval = MCateg[x];
                            strheadPage.Append("<li class=''><span>" + MmCval + "</span></li>");
                        }
                        divHeadPage.InnerHtml = strheadPage.ToString();
                        strheadPage.Append("</ul");
                        hftype.Value = objEnc.DecryptData(Session["Type"].ToString());
                        mRefNo = Session["CompanyRefNo"].ToString();
                        ViewState["UserLoginEmail"] = Session["User"].ToString();
                        ViewState["DisplayPanel"] = objEnc.DecryptData(Request.QueryString["mu"].ToString().Replace(" ", "+"));
                        BindGridView();
                        ShowHidePanel();
                    }
                    catch (Exception exception)
                    {
                        string error = exception.ToString();
                        string Page = Request.Url.AbsolutePath.ToString();
                        Response.Redirect("Error?techerror=" + objEnc.EncryptData(error) + "&page=" + objEnc.EncryptData(Page));
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert",
                        "alert('Session Expired,Please login again');window.location='Login'", true);
                }
            }
        }
    }
    protected void BindGridView(string sortExpression = null)
    {
        try
        {
            if (hftype.Value == "SuperAdmin")
            {
                if (ddlmastercategory.Visible == false)
                {
                    DataTable DtGrid = Lo.RetriveMasterCategoryDate(0, "", "", "", "", "SelectAll", "");
                    if (DtGrid.Rows.Count > 0)
                    {
                        gvCategory.DataSource = DtGrid;
                        gvCategory.DataBind();
                        divmastercategory.Visible = true;
                    }
                    else
                    {
                        divmastercategory.Visible = false;
                    }
                }
                else
                {
                    DataTable DtGrid = Lo.RetriveMasterCategoryDate(Convert.ToInt32(ddlmastercategory.SelectedItem.Value), "", "", "", "", "SelectByID", "");
                    if (DtGrid.Rows.Count > 0)
                    {
                        gvCategory.DataSource = DtGrid;
                        gvCategory.DataBind();
                        divmastercategory.Visible = true;
                    }
                    else
                    {
                        divmastercategory.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string error = ex.ToString();
            string Page = Request.Url.AbsolutePath.ToString();
            Response.Redirect("Error?techerror=" + objEnc.EncryptData(error) + "&page=" + objEnc.EncryptData(Page));
        }
    }
    protected void ShowHidePanel()
    {
        try
        {
            if (ViewState["DisplayPanel"].ToString() == "Panel1")
            {
                divcategory1textbox.Visible = true;
                divflag.Visible = true;
                divActive.Visible = true;
                divmastercategory.Visible = true;
                divinnercat.Visible = false;
                btnsave.Text = "Save Label";
            }
            else if (ViewState["DisplayPanel"].ToString() == "Panel2")
            {
                divcategory1dropdown.Visible = true;
                divcategory2textbox.Visible = true;
                divcategory1textbox.Visible = false;
                divActive.Visible = false;
                divflag.Visible = false;
                btnsave.Text = "Save Level 1";
                BindMasterCategory();
                divmastercategory.Visible = false;
                divinnercat.Visible = true;
                ddlmastercategory.AutoPostBack = true;
            }
            else if (ViewState["DisplayPanel"].ToString() == "Panel3")
            {
                divcategory1dropdown.Visible = true;
                divActive.Visible = false;
                divcategory2ddl.Visible = true;
                divcategory3textbox.Visible = true;
                divflag.Visible = false;
                btnsave.Text = "Save Level 2";
                BindMasterCategory();
                ddlmastercategory.AutoPostBack = true;
                divmastercategory.Visible = false;
                ddlcategroy2.AutoPostBack = true;
            }
            else if (ViewState["DisplayPanel"].ToString() == "Panel4")
            {
                divcategory1dropdown.Visible = true;
                divActive.Visible = false;
                divcategory2ddl.Visible = true;
                divcategory3textbox.Visible = false;
                divflag.Visible = false;
                divlabel2drop.Visible = true;
                divlevel3.Visible = true;
                btnsave.Text = "Save Level 3";
                BindMasterCategory();
                ddlmastercategory.AutoPostBack = true;
                ddlcategroy2.AutoPostBack = true;
                divmastercategory.Visible = false;
            }
        }
        catch (Exception ex)
        {
            string error = ex.ToString();
            string Page = Request.Url.AbsolutePath.ToString();
            Response.Redirect("Error?techerror=" + objEnc.EncryptData(error) + "&page=" + objEnc.EncryptData(Page));
        }
    }
    protected void cleartext()
    {
        try
        {
            txtcategory3.Text = "";
            txtmastercategory.Text = "";
            txtsubcategory.Text = "";
            txtlevel3.Text = "";
            //ddlmastercategory.SelectedValue = "Select";
            //if (ddlcategroy2.Visible = true)
            //{
            //    ddlcategroy2.SelectedValue = "Select";
            //}
            //if (ddllabel2.Visible = true)
            //{
            //    ddllabel2.SelectedValue = "Select";
            //}
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
    protected void btncancle_Click(object sender, EventArgs e)
    {
        cleartext();
    }
    protected void SaveCode()
    {
        try
        {
            DataTable DtGetAddDropdown = Lo.RetriveMasterSubCategoryDate(0, txtmastercategory.Text.Trim(), "", "DupMasterCat", "", "");
            if (DtGetAddDropdown.Rows.Count > 0)
            { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('This category " + txtmastercategory.Text + " already inserted in database, record not inserted.')", true); }
            else
            {
                DataTable StrCat = Lo.RetriveMasterCategoryDate(0, Co.RSQandSQLInjection(txtmastercategory.Text, "soft"), "", rbflag.SelectedItem.Value, rbactive.SelectedItem.Value, "Insert", objEnc.DecryptData(ViewState["UserLoginEmail"].ToString()));
                if (StrCat != null)
                {
                    BindGridView();
                    cleartext();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record saved')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not saved')", true);
                }
            }
        }
        catch (Exception ex)
        {
            string error = ex.ToString();
            string Page = Request.Url.AbsolutePath.ToString();
            Response.Redirect("Error?techerror=" + objEnc.EncryptData(error) + "&page=" + objEnc.EncryptData(Page));
        }
    }
    protected void SaveCodeSub()
    {
        try
        {
            DataTable DtGetAddDropdown = Lo.RetriveMasterSubCategoryDate(0, txtsubcategory.Text.Trim(), "", "DupSubCat", "", "");
            if (DtGetAddDropdown.Rows.Count > 0)
            { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('This category " + txtsubcategory.Text + " already inserted in " + ddlmastercategory.SelectedItem.Text + ", record not inserted.')", true); }
            else
            {
                DataTable StrCat = Lo.RetriveMasterSubCategoryDate(
                    Convert.ToInt32(ddlmastercategory.SelectedItem.Value),
                    Co.RSQandSQLInjection(txtsubcategory.Text, "soft"), "0", "InsertInnerID", "",
                    objEnc.DecryptData(ViewState["UserLoginEmail"].ToString()));
                if (StrCat != null)
                {
                    BindMasterInnerSubCategory();
                    cleartext();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record saved')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not saved')",
                        true);
                }
            }
        }
        catch (Exception ex)
        {
            string error = ex.ToString();
            string Page = Request.Url.AbsolutePath.ToString();
            Response.Redirect("Error?techerror=" + objEnc.EncryptData(error) + "&page=" + objEnc.EncryptData(Page));
        }
    }
    protected void SaveCodeInnerSub()
    {
        try
        {
            DataTable DtGetAddDropdown = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlcategroy2.SelectedItem.Value), txtcategory3.Text.Trim(), "", "DupSubCat", "", "");
            if (DtGetAddDropdown.Rows.Count > 0)
            { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('This category " + txtcategory3.Text + " already inserted in " + ddlcategroy2.SelectedItem.Text + ",record not inserted.')", true); }
            else
            {
                DataTable StrCat = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlcategroy2.SelectedItem.Value),
                    Co.RSQandSQLInjection(txtcategory3.Text, "soft"), "0", "InsertInnerSubID", "",
                    objEnc.DecryptData(ViewState["UserLoginEmail"].ToString()));
                if (StrCat != null)
                {
                    BindMasterInnerSubCategorySub();
                    cleartext();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record saved')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not saved')",
                        true);
                }
            }
        }
        catch (Exception ex)
        {
            string error = ex.ToString();
            string Page = Request.Url.AbsolutePath.ToString();
            Response.Redirect("Error?techerror=" + objEnc.EncryptData(error) + "&page=" + objEnc.EncryptData(Page));
        }
    }
    protected void SaveCodeInnerSubSub()
    {
        try
        {
            DataTable DtGetAddDropdown = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddllabel2.SelectedItem.Value), txtlevel3.Text.Trim(), "", "DupSubCat", "", "");
            if (DtGetAddDropdown.Rows.Count > 0)
            { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('This category " + txtlevel3.Text + " already inserted in " + ddllabel2.SelectedItem.Text + ",record not inserted.')", true); }
            else
            {
                DataTable StrCat = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddllabel2.SelectedItem.Value),
                    Co.RSQandSQLInjection(txtlevel3.Text, "soft"), "0", "InsertInnerSubID", "",
                    objEnc.DecryptData(ViewState["UserLoginEmail"].ToString()));
                if (StrCat != null)
                {
                    BindMasterInnerIneerSubCat();
                    cleartext();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record saved')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not saved')",
                        true);
                }
            }
        }
        catch (Exception ex)
        {
            string error = ex.ToString();
            string Page = Request.Url.AbsolutePath.ToString();
            Response.Redirect("Error?techerror=" + objEnc.EncryptData(error) + "&page=" + objEnc.EncryptData(Page));
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnsave.Text == "Save Label")
            {
                if (txtmastercategory.Text != "")
                {
                    SaveCode();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record saved successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('All field fill mandatory')", true);
                }
            }
            else if (btnsave.Text == "Save Level 1")
            {
                if (ddlmastercategory.SelectedIndex != 0 && txtsubcategory.Text != "")
                {
                    SaveCodeSub();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record saved successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('All field fill mandatory')", true);
                }
            }
            else if (btnsave.Text == "Save Level 2")
            {
                if (ddlmastercategory.SelectedIndex != 0 && ddlcategroy2.SelectedIndex != 0 && txtcategory3.Text != "")
                {
                    SaveCodeInnerSub();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record saved successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('All field fill mandatory')", true);
                }
            }
            else if (btnsave.Text == "Save Level 3")
            {
                if (ddlmastercategory.SelectedIndex != 0 && ddlcategroy2.SelectedIndex != 0 && ddllabel2.SelectedIndex != 0 && txtlevel3.Text != "")
                {
                    SaveCodeInnerSubSub();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record saved successfully')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('All field fill mandatory')", true);
                }
            }
        }
        catch (Exception ex)
        {
            string error = ex.ToString();
            string Page = Request.Url.AbsolutePath.ToString();
            Response.Redirect("Error?techerror=" + objEnc.EncryptData(error) + "&page=" + objEnc.EncryptData(Page));
        }
    }
    protected void BindMasterCategory()
    {
        try
        {
            DataTable DtMasterCategroy = new DataTable();
            if (ViewState["DisplayPanel"].ToString() == "Panel2")
            {
                if (hftype.Value == "SuperAdmin" || hftype.Value == "Admin")
                {
                    DtMasterCategroy = Lo.RetriveMasterCategoryDate(0, "", "", "", "", hftype.Value, "");
                }
                else
                {
                    DtMasterCategroy = Lo.RetriveMasterCategoryDate(0, "", "", "", "", "Select", "");
                }
            }
            else if (ViewState["DisplayPanel"].ToString() == "Panel3")
            {
                DtMasterCategroy = Lo.RetriveMasterCategoryDate(0, "2", "", "", "", "SelectFlag", "");
            }
            else if (ViewState["DisplayPanel"].ToString() == "Panel4")
            {
                DtMasterCategroy = Lo.RetriveMasterCategoryDate(0, "3", "", "", "", "SelectFlag3", "");
            }
            if (DtMasterCategroy.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlmastercategory, DtMasterCategroy, "MCategoryName", "MCategoryID");
                ddlmastercategory.Items.Insert(0, "Select");
            }
            else
            {
                ddlmastercategory.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            string error = ex.ToString();
            string Page = Request.Url.AbsolutePath.ToString();
            Response.Redirect("Error?techerror=" + objEnc.EncryptData(error) + "&page=" + objEnc.EncryptData(Page));
        }
    }
    protected void BindMasterInnerSubCategory()
    {
        try
        {
            DataTable DtFirstLevelCategroy = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlmastercategory.SelectedItem.Value), "", "", "SelectInnerMaster", "", "");
            if (DtFirstLevelCategroy.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlcategroy2, DtFirstLevelCategroy, "SCategoryName", "SCategoryId");
                ddlcategroy2.Items.Insert(0, "Select");
                gvlevel3.DataSource = DtFirstLevelCategroy;
                gvlevel3.DataBind();
                gvlevel3.Visible = true;
                ddllabel2.Items.Clear();
                lblevel.Text = "Level 1";
            }
            else
            {
                lblevel.Text = "";
                gvlevel3.Visible = false;
                ddllabel2.Items.Clear();
                ddlcategroy2.Items.Insert(0, "Select");
            }
        }
        catch (Exception ex)
        {
            string error = ex.ToString();
            string Page = Request.Url.AbsolutePath.ToString();
            Response.Redirect("Error?techerror=" + objEnc.EncryptData(error) + "&page=" + objEnc.EncryptData(Page));
        }
    }
    protected void BindMasterInnerSubCategorySub()
    {
        try
        {
            DataTable Dt2NdLevelCategroy = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlcategroy2.SelectedItem.Value), "", "", "SubSelectID", "", "");
            if (Dt2NdLevelCategroy.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddllabel2, Dt2NdLevelCategroy, "SCategoryName", "SCategoryId");
                ddllabel2.Items.Insert(0, "Select");
                gvlevel3.DataSource = Dt2NdLevelCategroy;
                gvlevel3.DataBind();
                gvlevel3.Visible = true;
                lblevel.Text = "Level 2";
            }
            else
            {
                lblevel.Text = "";
                gvlevel3.Visible = false;
                if (ddllabel2.Visible == true)
                {
                    ddllabel2.Items.Clear();
                    ddllabel2.Items.Insert(0, "Select");
                }
            }
        }
        catch (Exception ex)
        {
            string error = ex.ToString();
            string Page = Request.Url.AbsolutePath.ToString();
            Response.Redirect("Error?techerror=" + objEnc.EncryptData(error) + "&page=" + objEnc.EncryptData(Page));
        }
    }
    protected void BindMasterInnerIneerSubCat()
    {
        try
        {
            DataTable Dt3LevelCategroy = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddllabel2.SelectedItem.Value), "", "", "SubSelectID", "", "");
            if (Dt3LevelCategroy.Rows.Count > 0)
            {
                gvlevel3.DataSource = Dt3LevelCategroy;
                gvlevel3.DataBind();
                gvlevel3.Visible = true;
                lblevel.Text = "Level 3";
            }
            else
            {
                lblevel.Text = "";
                gvlevel3.Visible = false;
            }
        }
        catch (Exception ex)
        {
            string error = ex.ToString();
            string Page = Request.Url.AbsolutePath.ToString();
            Response.Redirect("Error?techerror=" + objEnc.EncryptData(error) + "&page=" + objEnc.EncryptData(Page));
        }
    }
    protected void ddlmastercategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlmastercategory.SelectedItem.Text != "Select")
        {
            BindMasterInnerSubCategory();
        }
        else
        {
            gvlevel3.Visible = false;
            if (ViewState["DisplayPanel"].ToString() == "Panel3")
            { ddlcategroy2.Items.Clear(); }
            else if (ViewState["DisplayPanel"].ToString() == "Panel4")
            {
                ddlcategroy2.Items.Clear();
                ddllabel2.Items.Clear();
            }
        }
    }
    protected void ddlcategroy2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcategroy2.SelectedItem.Text != "Select")
        {
            BindMasterInnerSubCategorySub();
        }
        else
        {

            gvlevel3.Visible = false;
            if (ViewState["DisplayPanel"].ToString() == "Panel3")
            {
                BindMasterInnerSubCategory();
            }
            if (ViewState["DisplayPanel"].ToString() == "Panel4")
            {
                BindMasterInnerSubCategory();
                ddllabel2.Items.Clear();
            }
        }
    }
    protected void ddllabel2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddllabel2.SelectedItem.Text != "Select")
        {
            BindMasterInnerIneerSubCat();
        }
        else
        {
            gvlevel3.Visible = false;
            BindMasterInnerSubCategorySub();
        }
    }
    protected void gvCategory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblHierarchy = (Label)e.Row.FindControl("lblhiraricy");
            if (lblHierarchy.Text == "Level 2")
            {
                e.Row.Attributes.Add("Class", "bg-skyrow");
            }
            if (lblHierarchy.Text == "Level 3")
            {
                e.Row.Attributes.Add("Class", "bg-bluerow");
            }
        }
    }
    protected void gvCategory_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TableSection = TableRowSection.TableBody;
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.TableSection = TableRowSection.TableFooter;
        }
    }
    protected void gvlevel3_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.TableSection = TableRowSection.TableBody;
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.TableSection = TableRowSection.TableHeader;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.TableSection = TableRowSection.TableFooter;
        }
    }
}