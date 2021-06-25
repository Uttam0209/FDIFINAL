using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Encryption;
using BusinessLayer;
using DataAccessLayer;
using System.Text;
using System.Collections.Specialized;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;
using System.Configuration;

public partial class User_ProductUpdation : System.Web.UI.Page
{
    Logic Lo = new Logic();
    private DataTable dtImage = new DataTable();
    private Cryptography objEnc = new Cryptography();
    private DataTable DtGrid = new DataTable();
    private DataUtility Co = new DataUtility();
    HybridDictionary HyPanel1 = new HybridDictionary();
    DataTable DtFilterView = new DataTable();
    private PagedDataSource pgsource = new PagedDataSource();
    string comprefno;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] != null)
        {
            if (!IsPostBack)
            {
                try
                {
                    Bindproducts();

                }
                catch (Exception ex)
                { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "(" + ex.Message + ")", true); }
            }
        }
    }

    protected void UpdateDtGridValue()
    {
        for (int a = 0; a < DtGrid.Rows.Count; a++)
        {
            if (DtGrid.Rows[a]["UCompany"].ToString() != "")
            {
                DtGrid.Rows[a]["CompanyName"] = DtGrid.Rows[a]["UCompany"];
                DtGrid.Rows[a]["FactoryName"] = DtGrid.Rows[a]["UFactory"];
                DtGrid.Rows[a]["CompanyRefNo"] = DtGrid.Rows[a]["UCompRefNo"];
                DtGrid.Rows[a]["FactoryRefNo"] = DtGrid.Rows[a]["UFactoryRefNo"];
            }
            else if (DtGrid.Rows[a]["FCompany"].ToString() != "")
            {
                DtGrid.Rows[a]["CompanyName"] = DtGrid.Rows[a]["FCompany"];
                DtGrid.Rows[a]["CompanyRefNo"] = DtGrid.Rows[a]["FCompRefNo"];
            }
        }
    }

    public void Bindproducts()
    {
        DataTable dtproducts = new DataTable();
        dtproducts = Lo.RetriveProductUpdation(Session["CompanyRefNo"].ToString(), "SelectProducts", objEnc.DecryptData(Session["Type"].ToString()));
        if (dtproducts.Rows.Count > 0)
        {
            UpdateDtGridValue();
            Session["PDatatTable"] = dtproducts;
            SeachResult();
        }
        else
        {
            divcontentproduct.Visible = false;
            gvproductupdate.DataBind();
        }
    }

    protected void gvproductupdate_RowCreated(object sender, GridViewRowEventArgs e)
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

    protected void lbllogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Remove("Type");
        Session.Remove("User");
        Session.Remove("CompanyRefNo");
        Session.Remove("SFToken");
        Session.RemoveAll();
        Session.Contents.RemoveAll();
        Session.Clear();
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cookies["DefaultDpsu"].Expires = DateTime.Now;
        Response.Buffer = true;
        Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
        Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
        Response.AppendHeader("Expires", "0"); // Proxies.
        if (Request.Cookies["User"] != null)
        {
            Response.Cookies["User"].Value = string.Empty;
            Response.Cookies["User"].Expires = DateTime.Now.AddMonths(-20);
        }
        if (Request.Cookies["SFToken"] != null)
        {
            Response.Cookies["SFToken"].Value = string.Empty;
            Response.Cookies["SFToken"].Expires = DateTime.Now.AddMonths(-20);
        }
        Response.RedirectToRoute("Productlist");
    }
    private void ReduceImageSize(double scaleFactor, Stream sourcePath, string targetPath)
    {
        using (var image = System.Drawing.Image.FromStream(sourcePath))
        {
            var newWidth = (int)(image.Width * scaleFactor);
            var newHeight = (int)(image.Height * scaleFactor);
            var thumbnailImg = new Bitmap(newWidth, newHeight);
            var thumbGraph = Graphics.FromImage(thumbnailImg);
            thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
            thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
            thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbGraph.DrawImage(image, imageRectangle);
            thumbnailImg.Save(targetPath, image.RawFormat);
        }
    }
    string insert1 = "";
    protected string Dvinsert(string sortExpression = null)
    {
        DataTable insert = new DataTable();
        insert.Columns.Add(new DataColumn("Column", typeof(string)));
        insert.Columns.Add(new DataColumn("Value", typeof(string)));
        DataRow dr;
        if (objEnc.DecryptData(Session["Type"].ToString()) == "Company")
        {
            dr = insert.NewRow();
            dr["Column"] = "CompanyRefNo=";
            dr["Value"] = "'" + Session["CompanyRefNo"].ToString() + "'";
            insert.Rows.Add(dr);
        }
        else if (objEnc.DecryptData(Session["Type"].ToString()) == "Division")
        {
            dr = insert.NewRow();
            dr["Column"] = "FactoryRefNo=";
            dr["Value"] = "'" + Session["CompanyRefNo"].ToString() + "'";
            insert.Rows.Add(dr);
        }
        else if (objEnc.DecryptData(Session["Type"].ToString()) == "Unit")
        {
            dr = insert.NewRow();
            dr["Column"] = "UnitRefNo=";
            dr["Value"] = "'" + Session["CompanyRefNo"].ToString() + "'";
            insert.Rows.Add(dr);
        }
        if (txtsearch.Text.Trim() != "")
        {
            dr = insert.NewRow();
            dr["Column"] = "((ProductRefNo like";
            dr["Value"] = "'%" + txtsearch.Text.Trim() + "%') or (CompanyName like '%" + txtsearch.Text.Trim() + "%') or (FactoryName like '%" + txtsearch.Text.Trim() + "%') or (UnitName like '%" + txtsearch.Text.Trim() + "%') or (ProductDescription like '%" + txtsearch.Text.Trim() + "%') or (IsIndeginized like '%" + txtsearch.Text.Trim() + "%') or (FeatursandDetail like '%" + txtsearch.Text.Trim() + "%'))";
            insert.Rows.Add(dr);
        }
        for (int i = 0; insert.Rows.Count > i; i++)
        {
            insert1 = insert1 + insert.Rows[i]["Column"].ToString() + " " + insert.Rows[i]["Value"].ToString() + " " + " and ";
        }
        if (insert1.ToString() != "")
        {
            insert1 = insert1.Substring(0, insert1.Length - 5);
        }
        return insert1;
    }
    protected string BindInsertfilter()
    {
        return Dvinsert();
    }
    public void SeachResult(string sortExpression = null)
    {
        try
        {
            DtFilterView = (DataTable)Session["PDatatTable"];
            if (DtFilterView.Rows.Count > 0)
            {
                DataView dv = new DataView(DtFilterView);
                DataTable dtnew = dv.ToTable();
                if (dtnew.Rows.Count > 0)
                {
                    dv.RowFilter = BindInsertfilter();
                    DataTable dtinner = dv.ToTable();
                    lbltotfilter.Text = dtinner.Rows.Count.ToString();
                    Session["ExcelDT"] = dtinner;
                    DataTable dtads = dv.ToTable();
                    if (dtads.Rows.Count > 0)
                    {
                        if (dtads.Columns.Contains("row_no"))
                        {
                            int i = 1; foreach (DataRow r in dtads.Rows) r["row_no"] = i++;
                        }
                        else
                        {
                            dtads.Columns.Add("row_no");
                            int i = 1; foreach (DataRow r in dtads.Rows) r["row_no"] = i++;
                        }
                        pgsource.DataSource = dtinner.DefaultView;
                        pgsource.AllowPaging = true;
                        pgsource.PageSize = 25;
                        pgsource.CurrentPageIndex = pagingCurrentPage;
                        lblpaging.Text = "Page " + (pagingCurrentPage + 1) + " of " + pgsource.PageCount;
                        lnkbtnPgPrevious.Enabled = !pgsource.IsFirstPage;
                        lnkbtnPgNext.Enabled = !pgsource.IsLastPage;
                        pgsource.DataSource = dtads.DefaultView;
                        gvproductupdate.DataSource = pgsource;
                        gvproductupdate.DataBind();
                        lbltotalshowpageitem.Text = pgsource.FirstIndexInPage + 1 + " - " + (pgsource.FirstIndexInPage + pgsource.Count);
                        divcontentproduct.Visible = true;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
                        divcontentproduct.Visible = false;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
                }
            }
            else
            {
                gvproductupdate.Visible = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('No Record Found')", true);
            }
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true);
        }
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        SeachResult();
    }

    #region AutoComplete Serach Box
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetSearchKeyword(string prefix)
    {
        Cryptography objCrypto1 = new Cryptography();
        List<string> customers = new List<string>();
        List<string> Finalcustomers = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = objCrypto1.DecryptData(ConfigurationManager.ConnectionStrings["connectiondb"].ConnectionString);
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct ProductRefNo from Vw_MasteRecord  where  IsActive='Y' AND IsIndeginized='Y' and ProductRefNo like @SearchText + '%' ";
                cmd.Parameters.AddWithValue("@SearchText", prefix);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["ProductRefNo"]));
                    }
                }
                cmd.CommandText = "select distinct CompanyName from Vw_MasteRecord where IsActive='Y' AND IsIndeginized='Y' and CompanyName like @SearchText + '%' ";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["CompanyName"]));
                    }
                }
                cmd.CommandText = "select distinct FactoryName from Vw_MasteRecord where IsActive='Y' AND IsIndeginized='Y' and FactoryName like @SearchText + '%' ";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["FactoryName"]));
                    }
                }
                cmd.CommandText = "select distinct UnitName from Vw_MasteRecord where IsActive='Y' AND IsIndeginized='Y' and UnitName like @SearchText + '%' ";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["UnitName"]));
                    }
                }
                cmd.CommandText = "select IsIndeginized from Vw_MasteRecord where IsIndeginized like @SearchText + '%' ";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["IsIndeginized"]));
                    }
                }
                cmd.CommandText = "select distinct FeatursandDetail from Vw_MasteRecord  where FeatursandDetail like @SearchText + '%' ";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["FeatursandDetail"]));
                    }
                }
                cmd.CommandText = "select distinct ProductDescription from Vw_MasteRecord where ProductDescription like @SearchText + '%' ";
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["ProductDescription"]));
                    }
                }
                conn.Close();
            }
        }
        return customers.Distinct().ToArray();
    }
    #endregion
    #region AutoComplete Serach Box
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string GetSearchKeywordemo(string prefix)
    {
        User_ProductUpdation u = new User_ProductUpdation();
        u.SeachResult(prefix);
        return "search";
    }
    protected void lnkbtnPgPrevious_Click(object sender, EventArgs e)
    {
        pagingCurrentPage -= 1;
        SeachResult();
    }
    protected void lnkbtnPgNext_Click(object sender, EventArgs e)
    {
        pagingCurrentPage += 1;
        SeachResult();
    }

    private int pagingCurrentPage
    {
        get
        {
            if (ViewState["pagingCurrentPage"] == null)
            {
                return 0;
            }
            else
            {
                return ((int)ViewState["pagingCurrentPage"]);
            }
        }
        set
        {
            ViewState["pagingCurrentPage"] = value;
        }
    }
    #endregion
    protected void gvproductupdate_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        foreach (GridViewRow row in gvproductupdate.Rows)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBoxList chkqa = (CheckBoxList)e.Row.FindControl("chkQAA");
                Label qa = (Label)e.Row.FindControl("lblqa");
                if (qa.Text != "")
                {
                    chkqa.SelectedValue = qa.Text.Trim();
                }
            }
            ((TextBox)row.FindControl("txtbxfeaturesanddetails")).Visible = false;
            ((FileUpload)row.FindControl("fuimages")).Visible = false;
            ((CheckBoxList)row.FindControl("chkQAA")).Visible = false;
            ((Label)row.FindControl("lblspecification")).Visible = true;
            ((Label)row.FindControl("lblimg")).Visible = true;
            ((Label)row.FindControl("lblqa")).Visible = true;
        }
    }
    #region Image Code
    private int ImageMaxCount;
    protected DataTable imagedb(FileUpload fuimg)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("ImageID", typeof(long)));
        dt.Columns.Add(new DataColumn("ImageName", typeof(string)));
        dt.Columns.Add(new DataColumn("ImageType", typeof(string)));
        dt.Columns.Add(new DataColumn("ImageActualSize", typeof(long)));
        dt.Columns.Add(new DataColumn("CompanyRefNo", typeof(string)));
        dt.Columns.Add(new DataColumn("Priority", typeof(int)));
        DataRow dr;
        {
            try
            {
                if (ImageMaxCount <= 4)
                {
                    foreach (HttpPostedFile postfiles in fuimg.PostedFiles)
                    {
                        string FileType = Path.GetExtension(postfiles.FileName);
                        int FileSize = postfiles.ContentLength;
                        if (DataUtility.Instance.GetImageFilter(postfiles.FileName) != false)
                        {
                            string FilePathName = Session["CompanyRefNo"].ToString() + "_" + DateTime.Now.ToString("hh_mm_ss") + postfiles.FileName;
                            postfiles.SaveAs(HttpContext.Current.Server.MapPath("/Upload") + "\\" + FilePathName);
                            dr = dt.NewRow();
                            dr["ImageID"] = "-1";
                            dr["ImageName"] = "Upload/" + FilePathName;
                            dr["ImageType"] = FileType.ToString();
                            dr["ImageActualSize"] = FileSize.ToString();
                            dr["CompanyRefNo"] = Session["CompanyRefNo"].ToString();
                            dr["Priority"] = ImageMaxCount++ + 1;
                            dt.Rows.Add(dr);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "ErrorMssgPopup('Invalid file format " + postfiles.FileName + "')", true);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return dt;
        }
    }
    #endregion

    string qa = string.Empty;

    protected void btnproupdate_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvproductupdate.Rows)
        {
            CheckBoxList chkqa = (CheckBoxList)row.FindControl("chkQAA");
            CheckBox chckrow = (CheckBox)row.FindControl("chckrow");
            Label lblprorefno = (Label)row.FindControl("lblitemcode");
            FileUpload img = (FileUpload)row.FindControl("fuimages");
            TextBox txtfeatures = (TextBox)row.FindControl("txtbxfeaturesanddetails");
            TextBox txtnodaloffcername = (TextBox)row.FindControl("username");
            TextBox txtnodaloffcermobile = (TextBox)row.FindControl("contactno");
            TextBox txtnodaloffceremail = (TextBox)row.FindControl("email");
            DropDownList ddlnodaloffcerdesignation = (DropDownList)row.FindControl("designation");
            if (btnproupdate.Text == "Update Image")
            {
                if (chckrow.Checked == true)

                {
                    try
                    {
                        if (img.HasFiles != false)
                        {
                            if (lblprorefno.Text != "")
                            {
                                DataTable dtImageBind = Lo.RetriveProductCode("", lblprorefno.Text, "RetriveImage", objEnc.DecryptData(Session["Type"].ToString()));
                                if (dtImageBind.Rows.Count > 0)
                                {
                                    short CountImageTotal = Convert.ToInt16(img.PostedFiles.Count);
                                    short AlreadyUploadImage = Convert.ToInt16(dtImageBind.Rows.Count);
                                    ImageMaxCount = (CountImageTotal + AlreadyUploadImage);
                                    if (ImageMaxCount <= 4)
                                    {
                                        dtImage = imagedb(img);
                                    }
                                }
                                else
                                {
                                    if (img.HasFiles != false)
                                    {
                                        ImageMaxCount = 4;
                                        dtImage = imagedb(img);
                                    }
                                }
                            }
                            else
                            {
                                ImageMaxCount = 4;
                                dtImage = imagedb(img);
                            }
                        }
                        if (txtfeatures.Text.Trim() != "")
                        {
                            HyPanel1["ProductRefNo"] = lblprorefno.Text.Trim();
                            string features = txtfeatures.Text.Trim();
                            HyPanel1["FeatursandDetail"] = features.Replace(",", "");
                            for (int i = 0; i < chkqa.Items.Count; i++)
                            {
                                if (chkqa.Items[i].Selected == true)
                                {
                                    qa = qa + chkqa.Items[i].Text.ToString() + ",";
                                }
                            }
                            if (qa != null)
                            {
                                HyPanel1["QAAgency"] = qa.ToString().Trim();
                            }
                            string StrProductDescription = Lo.UpdateSaveProduct(HyPanel1, dtImage);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully!!!');window.location ='PUpdate';", true);
                            Bindproducts();
                            ((TextBox)row.FindControl("txtbxfeaturesanddetails")).Enabled = false;
                            ((FileUpload)row.FindControl("fuimages")).Enabled = false;
                            ((CheckBoxList)row.FindControl("chkQAA")).Enabled = false;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please fill Specification value!!!');", true);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select row to update');", true);
                }
            }
            else if (btnproupdate.Text == "Update Contact")
            {
                if (txtnodaloffcername.Text.Trim() != "" && txtnodaloffcermobile.Text.Trim() != "" && ddlnodaloffcerdesignation.SelectedIndex != 0)
                {
                    Lo.UpdateNodalOfficers(Session["CompanyRefNo"].ToString(), objEnc.DecryptData(Session["Type"].ToString()), txtnodaloffcername.Text.Trim(), txtnodaloffcermobile.Text.Trim(),
                        txtnodaloffceremail.Text.Trim(), ddlnodaloffcerdesignation.SelectedValue);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully!!!');window.location ='PUpdate';", true);
                    Bindproducts();
                    ((TextBox)row.FindControl("username")).Enabled = false;
                    ((TextBox)row.FindControl("contactno")).Enabled = false;
                    ((TextBox)row.FindControl("email")).Enabled = false;
                    ((DropDownList)row.FindControl("designation")).Enabled = false;
                }
            }
        }
    }

    protected void lblimg_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in gvproductupdate.Rows)
        {
            gvproductupdate.Columns[0].Visible = true;
            gvproductupdate.Columns[6].Visible = true;
            gvproductupdate.Columns[7].Visible = true;
            gvproductupdate.Columns[8].Visible = true;
            gvproductupdate.Columns[9].Visible = false;
            gvproductupdate.Columns[10].Visible = false;
            gvproductupdate.Columns[11].Visible = false;
            gvproductupdate.Columns[12].Visible = false;
            gvproductupdate.Columns[13].Visible = false;
            ((TextBox)gvrow.FindControl("txtbxfeaturesanddetails")).Visible = false;
            ((FileUpload)gvrow.FindControl("fuimages")).Visible = false;
            ((CheckBoxList)gvrow.FindControl("chkQAA")).Visible = false;
            btnproupdate.Visible = true;
            btnproupdate.Text = "Update Image/Specification/QA";

        }
    }

    protected void lblcontact_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in gvproductupdate.Rows)
        {
            DataTable DtMasterCategroy;
            DropDownList ddlnodaloffcerdesignation = (DropDownList)gvrow.FindControl("designation");
            if (Session["CompanyRefNo"].ToString() == "0")
            {
                DtMasterCategroy = Lo.RetriveMasterData(0, "0", "", 0, "", "", "ViewDesignation");
            }
            else
            {
                DtMasterCategroy = Lo.RetriveMasterData(0, Session["CompanyRefNo"].ToString(), "", 0, "", "", "ViewDesignation");
                Co.FillDropdownlist(ddlnodaloffcerdesignation, DtMasterCategroy, "Designation", "DesignationId");
            }

            ddlnodaloffcerdesignation.Items.Insert(0, "Select");
            gvproductupdate.Columns[0].Visible = true;
            gvproductupdate.Columns[9].Visible = true;
            gvproductupdate.Columns[10].Visible = true;
            gvproductupdate.Columns[11].Visible = true;
            gvproductupdate.Columns[12].Visible = true;
            gvproductupdate.Columns[13].Visible = false;
            gvproductupdate.Columns[6].Visible = false;
            gvproductupdate.Columns[7].Visible = false;
            gvproductupdate.Columns[8].Visible = false;
            btnproupdate.Visible = true;
            btnproupdate.Text = "Update Contact";
        }


    }

    protected void chckrow_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvrow in gvproductupdate.Rows)
        {
            var chckrow = gvrow.FindControl("chckrow") as CheckBox;
            if (chckrow.Checked == true)
            {
                GridViewRow row = (GridViewRow)((Control)sender).NamingContainer;
                ((TextBox)gvrow.FindControl("txtbxfeaturesanddetails")).Visible = true;
                ((FileUpload)gvrow.FindControl("fuimages")).Visible = true;
                ((CheckBoxList)gvrow.FindControl("chkQAA")).Visible = true;
                ((Label)gvrow.FindControl("lblspecification")).Visible = false;
                ((Label)gvrow.FindControl("lblimg")).Visible = false;
                ((Label)gvrow.FindControl("lblqa")).Visible = false;
                ((TextBox)gvrow.FindControl("username")).Enabled = true;
                ((TextBox)gvrow.FindControl("contactno")).Enabled = true;
                ((TextBox)gvrow.FindControl("email")).Enabled = true;
                ((DropDownList)gvrow.FindControl("designation")).Enabled = true;
            }
            else
            {
                GridViewRow row = (GridViewRow)((Control)sender).NamingContainer;
                ((TextBox)gvrow.FindControl("txtbxfeaturesanddetails")).Visible = false;
                ((FileUpload)gvrow.FindControl("fuimages")).Visible = false;
                ((CheckBoxList)gvrow.FindControl("chkQAA")).Visible = false;
                ((Label)gvrow.FindControl("lblspecification")).Visible = true;
                ((Label)gvrow.FindControl("lblimg")).Visible = true;
                ((Label)gvrow.FindControl("lblqa")).Visible = true;
                ((TextBox)gvrow.FindControl("username")).Enabled = false;
                ((TextBox)gvrow.FindControl("contactno")).Enabled = false;
                ((TextBox)gvrow.FindControl("email")).Enabled = false;
                ((DropDownList)gvrow.FindControl("designation")).Enabled = false;
            }
        }
    }

    protected void lblindegenized_Click(object sender, EventArgs e)
    {
        gvproductupdate.Columns[0].Visible = false;
        gvproductupdate.Columns[6].Visible = true;
        gvproductupdate.Columns[7].Visible = true;
        gvproductupdate.Columns[8].Visible = true;
        gvproductupdate.Columns[9].Visible = false;
        gvproductupdate.Columns[10].Visible = false;
        gvproductupdate.Columns[11].Visible = false;
        gvproductupdate.Columns[12].Visible = false;
        gvproductupdate.Columns[13].Visible = true;
        btnproupdate.Visible = false;

    }

    protected void BindFinancialYear()
    {
        DataTable MasterFinancialYear = Lo.RetriveMasterSubCategoryDate(0, "", "", "AllFinancialYear", "", "");
        if (MasterFinancialYear.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlyearofindiginization, MasterFinancialYear, "FY", "FYID");
            ddlyearofindiginization.Items.Insert(0, "Select");
        }
    }
    Int32 mddlyr = 0;
    string makecat = "";

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (rdblindegprocess.SelectedIndex != -1 && chkinditargetyear.SelectedIndex != -1 && rdblisindig.SelectedIndex != -1)
            {
                if (ddlyearofindiginization.SelectedItem.Text == "Select")
                {
                    mddlyr = 0;
                }
                else
                {
                    mddlyr = Convert.ToInt32(ddlyearofindiginization.SelectedItem.Value);
                }
                if (rbIgCategory.SelectedIndex != -1)
                {
                    makecat = rbIgCategory.SelectedValue;
                }
                else
                { makecat = ""; }
                try
                {
                    string manufname = txtmanufacturngname.Text.Trim();
                    string manufadres = txtmanufacturngadress.Text.Trim();

                    Lo.updateSucessStory(lblprorefcode.Text, rdblindegprocess.SelectedItem.Value, mddlyr, rdblisindig.SelectedItem.Value,
                        chkinditargetyear.SelectedValue, makecat.ToString(), manufname.Replace(",", ""), manufadres.Replace(",", ""), 0, Convert.ToDateTime(DateTime.Now.ToString()));
                    SuccessStoryLog();
                }
                catch (Exception ex)
                {

                }
                success.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully!!!');window.location ='PUpdate'", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select indegenization process started value!!!');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(" + ex.Message + ")", true);
        }
    }
    #region Success Story Update Log
    protected void SuccessStoryLog()
    {
        try
        {
            DateTime Date = Convert.ToDateTime(DateTime.Now.ToString());
            string mDate = Date.ToString("dd-MM-yyyy");
            DateTime Time = Convert.ToDateTime(DateTime.Now.ToString());
            string mTime = Time.ToString("hh:mm:ss");
            Lo.saveSuccessStoryLog(lblprorefcode.Text.Trim(), Session["CompanyRefNo"].ToString(), mDate, mTime);
        }
        catch (Exception ex)
        { }
    }
    #endregion
    protected void PROCURMENTCATEGORYIndigenization()
    {
        DataTable DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "PROCURMENT CATEGORY", "", "SelectProductCat", "", "");
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillRadioBoxList(rbIgCategory, DtMasterCategroy, "SCategoryName", "SCategoryID");
        }
    }
    protected void gvproductupdate_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Status")
        {
            try
            {
                GridViewRow item = (GridViewRow)(((Control)(e.CommandSource)).NamingContainer);
                DataTable dtindeg = Lo.RetriveProductUpdation(Session["CompanyRefNo"].ToString(), "GetIndegnizedStatus", objEnc.DecryptData(Session["Type"].ToString()));
                Label lbltarget = (Label)item.FindControl("lnktarget");
                Label lblmanufname = (Label)item.FindControl("lblmanufname");
                Label lblmanufaddress = (Label)item.FindControl("lblmanufadress");
                Label prorefno = (Label)item.FindControl("lblitemcode");
                //  HiddenField hfidproc = (HiddenField)item.FindControl("hfproc");
                LinkButton lnkindegstatus = (LinkButton)item.FindControl("lnkindegstatus");
                lblprorefcode.Text = prorefno.Text.Trim();
                txtmanufacturngname.Text = lblmanufname.Text.Trim();
                txtmanufacturngadress.Text = lblmanufaddress.Text.Trim();
                try
                {
                    if (lbltarget.Text != "")
                    {
                        chkinditargetyear.SelectedValue = lbltarget.Text;
                    }
                }
                catch (Exception ex)
                { }

                if (lnkindegstatus.Text == "Y")
                {
                    rdblisindig.SelectedValue = lnkindegstatus.Text.Trim();

                }
                else
                {
                    rdblisindig.SelectedValue = lnkindegstatus.Text.Trim();

                }
                Label indegyear = (Label)item.FindControl("lblindegyear");
                BindFinancialYear();
                if (indegyear.Text != "0" && indegyear.Text != "")
                {
                    ddlyearofindiginization.SelectedIndex = ddlyearofindiginization.Items.IndexOf(ddlyearofindiginization.Items.FindByText(indegyear.Text));
                }
                else
                {
                    ddlyearofindiginization.SelectedIndex = 0;
                }
                PROCURMENTCATEGORYIndigenization();
                //try
                //{
                //    if (hfidproc.Value != "")
                //    {
                //        rbIgCategory.SelectedValue = hfidproc.Value;
                //    }
                //}
                //catch (Exception ex)
                //{ }

                ScriptManager.RegisterStartupScript(this, GetType(), "divupdate", "showPopup2();", true);

            }
            catch (Exception ex)
            { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert(" + ex.Message + ")", true); }

        }
    }
}
