﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;
using Encryption;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Configuration;

public partial class Vendor_V_DetailofDefence : System.Web.UI.Page
{
    #region Variable
    Logic Lo = new Logic();
    Cryptography Enc = new Cryptography();
    DataUtility Co = new DataUtility();
    HybridDictionary HySaveVendorRegisdetail = new HybridDictionary();
    private string _msg = string.Empty;
    private string _sysMsg = string.Empty;
    private TextBox textboxtechNomenclature;
    Int64 Mid = 0;
    #endregion
    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["VUser"] != null)
        {
            if (!IsPostBack)
            {
                lbcomp.Text = Session["VCompName"].ToString();
                LoadP();
            }
        }
        else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert",
                   "alert('Session Expired,Please login again');window.location='VendorLogin'", true);
    }
    protected void LoadP()
    {
        DataTable dtproductdetails = Lo.RetriveVendor(0, Enc.DecryptData(Session["VendorRefNo"].ToString()), "", "MultigridPDet");
        if (dtproductdetails.Rows.Count > 0)
        {
            gvproddetailedit.DataSource = dtproductdetails;
            gvproddetailedit.DataBind();
            gvproddetail.Visible = false;
            gvproddetailedit.Visible = true;
        }
        else
        {
            SetInitialRowProductDetails();
            gvproddetail.Visible = true;
            gvproddetailedit.Visible = false;
        }
        DataTable dttechnology = Lo.RetriveVendor(0, Enc.DecryptData(Session["VendorRefNo"].ToString()), "", "MultigridTecdet");
        if (dttechnology.Rows.Count > 0)
        {
            gvtechnologyedit.DataSource = dttechnology;
            gvtechnologyedit.DataBind();
            gvtechnology.Visible = false;
            gvtechnologyedit.Visible = true;
        }
        else
        {
            SetInitialRowTechnologyDetails();
            gvtechnology.Visible = true;
            gvtechnologyedit.Visible = false;
        }
        DataTable dtsourcerowmaterial = Lo.RetriveVendor(0, Enc.DecryptData(Session["VendorRefNo"].ToString()), "", "MultigridRawMat");
        if (dtsourcerowmaterial.Rows.Count > 0)
        {
            gvSourceofRawMaterialedit.DataSource = dtsourcerowmaterial;
            gvSourceofRawMaterialedit.DataBind();
            gvSourceofRawMaterial.Visible = false;
            gvSourceofRawMaterialedit.Visible = true;
        }
        else
        {
            SetRawmeterialDetails();
            gvSourceofRawMaterial.Visible = true;
            gvSourceofRawMaterialedit.Visible = false;
        }
        DataTable dtitemsuplied = Lo.RetriveVendor(0, Enc.DecryptData(Session["VendorRefNo"].ToString()), "SupplyYes", "Multigriditsupp");
        if (dtitemsuplied.Rows.Count > 0)
        {
            gvItemProducedandSuppliededit.DataSource = dtitemsuplied;
            gvItemProducedandSuppliededit.DataBind();
            gvItemProducedandSupplied.Visible = false;
            gvItemProducedandSuppliededit.Visible = true;
        }
        else
        {
            SetInitialRowItemProductorSupplied();
            gvItemProducedandSupplied.Visible = true;
            gvItemProducedandSuppliededit.Visible = false;
        }
        DataTable dtitemsupliednot = Lo.RetriveVendor(0, Enc.DecryptData(Session["VendorRefNo"].ToString()), "SupplyNo", "Multigriditsupp");
        if (dtitemsupliednot.Rows.Count > 0)
        {
            gvItemSuppliedbutnotproducededit.DataSource = dtitemsupliednot;
            gvItemSuppliedbutnotproducededit.DataBind();
            gvItemSuppliedbutnotproduced.Visible = false;
            gvItemSuppliedbutnotproducededit.Visible = true;
        }
        else
        {
            SetInitialRowItemProductorSupplied1();
            gvItemSuppliedbutnotproduced.Visible = true;
            gvItemSuppliedbutnotproducededit.Visible = false;
        }
    }
    #endregion
    #region other
    protected void BindNatoClassSubCategoryofNatoGroup(DropDownList DDLnatoPostBack, DropDownList ddlnatoclass)
    {
        if (DDLnatoPostBack.SelectedItem.Text != "Select")
        {
            DataTable DtNatoclass = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(DDLnatoPostBack.SelectedItem.Value), "", "", "SubSelectID", "", "");
            if (DtNatoclass.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlnatoclass, DtNatoclass, "SCategoryName", "SCategoryId");
                ddlnatoclass.Items.Insert(0, "Select");
            }
            else
            {
                ddlnatoclass.Items.Clear();
                ddlnatoclass.Items.Insert(0, "Select");
            }
        }
    }
    protected void ddlnatoclass_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);
        int rowIndex = gvr.RowIndex;
        DropDownList DDLNatoClassPostBack = gvproddetail.Rows[rowIndex].FindControl("ddlnatoclass") as DropDownList;
        DropDownList ddlItemCode = gvproddetail.Rows[rowIndex].FindControl("ddlitemcode") as DropDownList;
        BindItemClassSubCategoryofNatoClass(DDLNatoClassPostBack, ddlItemCode);
    }
    protected void ddlnatogroup_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddlnatogroup.SelectedItem.Text != "Select")
        {
            DataTable DtNatoclass = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlnatogroup.SelectedItem.Value), "", "", "SubSelectID", "", "");
            if (DtNatoclass.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlnatoclassedit, DtNatoclass, "SCategoryName", "SCategoryId");
                ddlnatoclassedit.Items.Insert(0, "Select");
            }
            else
            {
                ddlnatoclassedit.Items.Clear();
                ddlnatoclassedit.Items.Insert(0, "Select");
            }
        }
    }
    protected void ddlnatoclassedit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlnatogroup.Text != "" && ddlnatoclassedit.SelectedItem.Text != "Select")
        {
            DataTable dtItemCode = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlnatoclassedit.SelectedItem.Value), "", "", "SubSelectID", "", "");
            if (dtItemCode.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlitemcodeedit, dtItemCode, "SCategoryName", "SCategoryId");
                ddlitemcodeedit.Items.Insert(0, "Select");
            }
            else
            {
                ddlitemcodeedit.Items.Clear();
                ddlitemcodeedit.Items.Insert(0, "Select");
                ddlitemcodeedit.Items.Insert(1, "NA");
            }
        }
    }
    protected void ddltech1edit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltech1edit.SelectedItem.Text != "Select")
        {
            DataTable DtTech2 = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddltech1edit.SelectedItem.Value), "", "", "SubSelectID", "", "");
            if (DtTech2.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddltech2edit, DtTech2, "SCategoryName", "SCategoryId");
                ddltech2edit.Items.Insert(0, "Select");
            }
            else
            {
                ddltech2edit.Items.Clear();
                ddltech2edit.Items.Insert(0, "Select");
            }
        }
    }
    //protected void ddltech2edit_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddltech2edit.Text != "" && ddltech2edit.SelectedItem.Text != "Select")
    //    {
    //        DataTable DtMasterCatTech3 = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddltech2edit.SelectedItem.Value), "", "", "SubSelectID", "", "");
    //        if (DtMasterCatTech3.Rows.Count > 0)
    //        {
    //            Co.FillDropdownlist(ddltech3edit, DtMasterCatTech3, "SCategoryName", "SCategoryId");
    //            ddltech3edit.Items.Insert(0, "Select");
    //        }
    //        else
    //        {
    //            ddltech3edit.Items.Clear();
    //            ddltech3edit.Items.Insert(0, "Select");
    //            ddltech3edit.Items.Insert(1, "NA");
    //        }
    //    }
    //}
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Response.Redirect("https://srijandefence.gov.in/RegistrationNoDetails?mu=OIVxjUlnTVc=&id=YUM6Wog/7cKd56S2dApVEg==");
    }
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        Response.Redirect("https://srijandefence.gov.in/CompanyInformation_II?mu=KQ5FIC8PdXE=&id=YUM6Wog/7cKd56S2dApVEg==");

    }
    //Add Grid of Products Details
    #region  Grid of Products Details
    private void SetInitialRowProductDetails()
    {
        DataTable dtProdDetail = new DataTable();
        DataRow drProdDetail = null;
        dtProdDetail.Columns.Add(new DataColumn("RowNumberProd", typeof(string)));
        dtProdDetail.Columns.Add(new DataColumn("Nomenclature", typeof(string)));
        dtProdDetail.Columns.Add(new DataColumn("NatoGroup", typeof(string)));
        dtProdDetail.Columns.Add(new DataColumn("NatoClass", typeof(string)));
        dtProdDetail.Columns.Add(new DataColumn("ItemCode", typeof(string)));
        dtProdDetail.Columns.Add(new DataColumn("HSNCode", typeof(string)));
        drProdDetail = dtProdDetail.NewRow();
        drProdDetail["RowNumberProd"] = 1;
        drProdDetail["Nomenclature"] = string.Empty;
        drProdDetail["NatoGroup"] = string.Empty;
        drProdDetail["NatoClass"] = string.Empty;
        drProdDetail["ItemCode"] = string.Empty;
        drProdDetail["HSNCode"] = string.Empty;
        dtProdDetail.Rows.Add(drProdDetail);
        ViewState["ProductsDetails"] = dtProdDetail;
        gvproddetail.DataSource = dtProdDetail;
        gvproddetail.DataBind();
        DropDownList ddlnatogroup = (DropDownList)gvproddetail.Rows[0].Cells[2].FindControl("ddlnatogroup");
        BindNatoGroup(ddlnatogroup);
    }
    protected void BindNatoGroup(DropDownList DropDownValue)
    {
        DataTable DtDropDownNatoGroup = new DataTable();
        DtDropDownNatoGroup = Lo.RetriveMasterSubCategoryDate(0, "NSN GROUP", "", "SelectProductCat", "", "");
        if (DtDropDownNatoGroup.Rows.Count > 0)
        {
            Co.FillDropdownlist(DropDownValue, DtDropDownNatoGroup, "SCategoryName", "SCategoryID");
            DropDownValue.Items.Insert(0, "Select");
        }
    }
    protected void ddlnatogroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);
        int rowIndex = gvr.RowIndex;
        DropDownList DDLnatoPostBack = gvproddetail.Rows[rowIndex].FindControl("ddlnatogroup") as DropDownList;
        DropDownList ddlnatoclass = gvproddetail.Rows[rowIndex].FindControl("ddlnatoclass") as DropDownList;
        BindNatoClassSubCategoryofNatoGroup(DDLnatoPostBack, ddlnatoclass);
    }
    protected void BindItemClassSubCategoryofNatoClass(DropDownList DDLNatoClassPostBack, DropDownList ddlItemCode)
    {
        if (DDLNatoClassPostBack.Text != "" && DDLNatoClassPostBack.SelectedItem.Text != "Select")
        {
            DataTable dtItemCode = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(DDLNatoClassPostBack.SelectedItem.Value), "", "", "SubSelectID", "", "");
            if (dtItemCode.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlItemCode, dtItemCode, "SCategoryName", "SCategoryId");
                ddlItemCode.Items.Insert(0, "Select");
            }
            else
            {
                ddlItemCode.Items.Clear();
                ddlItemCode.Items.Insert(0, "Select");
                ddlItemCode.Items.Insert(1, "NA");
            }
        }
    }
    private void AddNewProductDetailGrid()
    {
        int rowIndexProd = 0;
        if (ViewState["ProductsDetails"] != null)
        {
            DataTable dtCurrentTableProd = (DataTable)ViewState["ProductsDetails"];
            DataRow drCurrentRowNameProd = null;
            if (dtCurrentTableProd.Rows.Count > 0)
            {
                if (dtCurrentTableProd.Rows.Count < 3)
                {
                    for (int i = 1; i <= dtCurrentTableProd.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        TextBox TextBox1 = (TextBox)gvproddetail.Rows[rowIndexProd].Cells[1].FindControl("txtproductnomen");
                        DropDownList ddl_1 = (DropDownList)gvproddetail.Rows[rowIndexProd].Cells[2].FindControl("ddlnatogroup");
                        DropDownList ddl_2 = (DropDownList)gvproddetail.Rows[rowIndexProd].Cells[3].FindControl("ddlnatoclass");
                        DropDownList ddl_3 = (DropDownList)gvproddetail.Rows[rowIndexProd].Cells[4].FindControl("ddlitemcode");
                        TextBox TextBox2 = (TextBox)gvproddetail.Rows[rowIndexProd].Cells[5].FindControl("txthsnno");
                        drCurrentRowNameProd = dtCurrentTableProd.NewRow();
                        drCurrentRowNameProd["RowNumberProd"] = i + 1;
                        dtCurrentTableProd.Rows[i - 1]["Nomenclature"] = TextBox1.Text;
                        dtCurrentTableProd.Rows[i - 1]["NatoGroup"] = ddl_1.Text;
                        dtCurrentTableProd.Rows[i - 1]["NatoClass"] = ddl_2.Text;
                        dtCurrentTableProd.Rows[i - 1]["ItemCode"] = ddl_3.Text;
                        dtCurrentTableProd.Rows[i - 1]["HSNCode"] = TextBox2.Text;
                        rowIndexProd++;
                    }
                    dtCurrentTableProd.Rows.Add(drCurrentRowNameProd);
                    ViewState["count1"] = Convert.ToInt32(dtCurrentTableProd.Rows.Count);
                    ViewState["ProductsDetails"] = dtCurrentTableProd;
                    gvproddetail.DataSource = dtCurrentTableProd;
                    gvproddetail.DataBind();
                    DropDownList ddl_12 = (DropDownList)gvproddetail.Rows[rowIndexProd].Cells[2].FindControl("ddlnatogroup");
                    BindNatoGroup(ddl_12);
                    SetPreviousDataProductDetail();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Could not add more then row Products Details.')", true);
                }
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
    }
    private void SetPreviousDataProductDetail()
    {
        try
        {
            int rowIndexProd = 0;
            if (ViewState["ProductsDetails"] != null)
            {
                DataTable dtProdPrevious = (DataTable)ViewState["ProductsDetails"];
                if (dtProdPrevious.Rows.Count > 0)
                {
                    for (int i = 0; i < dtProdPrevious.Rows.Count; i++)
                    {
                        TextBox TB_1 = (TextBox)gvproddetail.Rows[rowIndexProd].Cells[1].FindControl("txtproductnomen");
                        textboxtechNomenclature = (TextBox)gvtechnology.Rows[rowIndexProd].Cells[1].FindControl("txttechnomen");
                        DropDownList DDL_1 = (DropDownList)gvproddetail.Rows[rowIndexProd].Cells[2].FindControl("ddlnatogroup");
                        DropDownList DDL_2 = (DropDownList)gvproddetail.Rows[rowIndexProd].Cells[3].FindControl("ddlnatoclass");
                        DropDownList DDL_3 = (DropDownList)gvproddetail.Rows[rowIndexProd].Cells[4].FindControl("ddlitemcode");
                        TextBox TB_2 = (TextBox)gvproddetail.Rows[rowIndexProd].Cells[5].FindControl("txthsnno");
                        TB_1.Text = dtProdPrevious.Rows[i]["Nomenclature"].ToString();
                        textboxtechNomenclature.Text = dtProdPrevious.Rows[i]["Nomenclature"].ToString();
                        TB_2.Text = dtProdPrevious.Rows[i]["HSNCode"].ToString();
                        if (i < dtProdPrevious.Rows.Count - 1)
                        {
                            DataTable DtDropDownNatoGroup1 = new DataTable();
                            DtDropDownNatoGroup1 = Lo.RetriveMasterSubCategoryDate(0, "NSN GROUP", "", "SelectProductCat", "", "");
                            if (DtDropDownNatoGroup1.Rows.Count > 0)
                            {
                                Co.FillDropdownlist(DDL_1, DtDropDownNatoGroup1, "SCategoryName", "SCategoryID");
                                DDL_1.Items.Insert(0, "Select");
                                DDL_1.Items.FindByValue(dtProdPrevious.Rows[i]["NatoGroup"].ToString()).Selected = true;
                            }
                            if (DDL_1.SelectedItem.Text != "Select")
                            {
                                DataTable DtNatoclass = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(DDL_1.SelectedItem.Value), "", "", "SubSelectID", "", "");
                                if (DtNatoclass.Rows.Count > 0)
                                {
                                    Co.FillDropdownlist(DDL_2, DtNatoclass, "SCategoryName", "SCategoryId");
                                    DDL_2.Items.Insert(0, "Select");
                                    DDL_2.Items.FindByValue(dtProdPrevious.Rows[i]["NatoClass"].ToString()).Selected = true;
                                }
                            }
                            if (DDL_2.Text != "" && DDL_2.SelectedItem.Text != "Select")
                            {
                                DataTable dtItemCode = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(DDL_2.SelectedItem.Value), "", "", "SubSelectID", "", "");
                                if (dtItemCode.Rows.Count > 0)
                                {
                                    Co.FillDropdownlist(DDL_3, dtItemCode, "SCategoryName", "SCategoryId");
                                    DDL_3.Items.Insert(0, "Select");
                                    DDL_3.Items.FindByValue(dtProdPrevious.Rows[i]["ItemCode"].ToString()).Selected = true;
                                }
                                else
                                {
                                    DDL_3.Items.Insert(0, "NA");
                                }
                            }
                        }
                        rowIndexProd++;
                    }
                }
            }
        }
        catch (Exception ex)
        { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('" + ex.Message + "')", true); }
    }
    protected void btnProductDetailAddMore_Click(object sender, EventArgs e)
    {
        AddNewProductDetailGrid();
        AddNewTechnologyDetailGrid();
    }
    protected void txthsnno_TextChanged(object sender, EventArgs e)
    {
        if (gvproddetail.Rows.Count == 1)
        {
            TextBox TextBox_1 = (TextBox)gvproddetail.Rows[0].Cells[1].FindControl("txtproductnomen");
            TextBox TextBox1M2 = (TextBox)gvtechnology.Rows[0].Cells[1].FindControl("txttechnomen");
            TextBox1M2.Text = TextBox_1.Text;
        }
        if (gvproddetail.Rows.Count == 2)
        {
            TextBox TextBox_1 = (TextBox)gvproddetail.Rows[1].Cells[1].FindControl("txtproductnomen");
            TextBox TextBox1M2 = (TextBox)gvtechnology.Rows[1].Cells[1].FindControl("txttechnomen");
            TextBox1M2.Text = TextBox_1.Text;
        }
        if (gvproddetail.Rows.Count == 3)
        {
            TextBox TextBox_1 = (TextBox)gvproddetail.Rows[2].Cells[1].FindControl("txtproductnomen");
            TextBox TextBox1M2 = (TextBox)gvtechnology.Rows[2].Cells[1].FindControl("txttechnomen");
            TextBox1M2.Text = TextBox_1.Text;
        }
    }
    protected void gvproddetail_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dtgridProdrowcreated = (DataTable)ViewState["ProductsDetails"];
            LinkButton lbProd = (LinkButton)e.Row.FindControl("lbProductDetail");
            if (lbProd != null)
            {
                if (dtgridProdrowcreated.Rows.Count > 1)
                {
                    if (e.Row.RowIndex == dtgridProdrowcreated.Rows.Count - 1)
                    {
                        lbProd.Visible = false;
                    }
                }
                else
                {
                    lbProd.Visible = false;
                }
            }
        }
    }
    protected void lbProductDetail_Click(object sender, EventArgs e)
    {
        LinkButton lbProd = (LinkButton)sender;
        GridViewRow gvRowProd = (GridViewRow)lbProd.NamingContainer;
        int rowID = gvRowProd.RowIndex;
        if (ViewState["ProductsDetails"] != null)
        {
            DataTable dtremovegridprod = (DataTable)ViewState["ProductsDetails"];
            if (dtremovegridprod.Rows.Count > 1)
            {
                if (gvRowProd.RowIndex < dtremovegridprod.Rows.Count - 1)
                {
                    dtremovegridprod.Rows.Remove(dtremovegridprod.Rows[rowID]);
                    ResetRowIDProductDetail(dtremovegridprod);
                }
            }
            ViewState["ProductsDetails"] = dtremovegridprod;
            gvproddetail.DataSource = dtremovegridprod;
            gvproddetail.DataBind();
        }
        SetPreviousDataProductDetail();
    }
    private void ResetRowIDProductDetail(DataTable dtRemoveProductdetail)
    {
        int rowNumberProd = 1;
        if (dtRemoveProductdetail.Rows.Count > 0)
        {
            foreach (DataRow rowProd in dtRemoveProductdetail.Rows)
            {
                rowProd[0] = rowNumberProd;
                rowNumberProd++;
            }
        }
    }
    DataTable DtSaveProdDetails = new DataTable();
    protected void SaveCodeProdDetails()
    {
        DtSaveProdDetails.Columns.Add(new DataColumn("RowNumberProd", typeof(string)));
        DtSaveProdDetails.Columns.Add(new DataColumn("Nomenclature", typeof(string)));
        DtSaveProdDetails.Columns.Add(new DataColumn("NatoGroup", typeof(string)));
        DtSaveProdDetails.Columns.Add(new DataColumn("NatoClass", typeof(string)));
        DtSaveProdDetails.Columns.Add(new DataColumn("ItemCode", typeof(string)));
        DtSaveProdDetails.Columns.Add(new DataColumn("HSNCode", typeof(string)));
        DataRow drCurrentRowSaveCode = null;
        for (int i = 0; gvproddetail.Rows.Count > i; i++)
        {
            TextBox TB_1 = (TextBox)gvproddetail.Rows[i].Cells[1].FindControl("txtproductnomen");
            DropDownList DDL_1 = (DropDownList)gvproddetail.Rows[i].Cells[2].FindControl("ddlnatogroup");
            DropDownList DDL_2 = (DropDownList)gvproddetail.Rows[i].Cells[3].FindControl("ddlnatoclass");
            DropDownList DDL_3 = (DropDownList)gvproddetail.Rows[i].Cells[4].FindControl("ddlitemcode");
            TextBox TB_2 = (TextBox)gvproddetail.Rows[i].Cells[5].FindControl("txthsnno");
            if (DDL_1.SelectedItem.Text != "Select" && TB_2.Text != "")
            {
                drCurrentRowSaveCode = DtSaveProdDetails.NewRow();
                drCurrentRowSaveCode["RowNumberProd"] = i + 1;
                drCurrentRowSaveCode["Nomenclature"] = TB_1.Text;
                drCurrentRowSaveCode["NatoGroup"] = DDL_1.SelectedItem.Value;
                drCurrentRowSaveCode["NatoClass"] = DDL_2.SelectedItem.Value;
                drCurrentRowSaveCode["ItemCode"] = DDL_3.SelectedItem.Value;
                drCurrentRowSaveCode["HSNCode"] = TB_2.Text;
                DtSaveProdDetails.Rows.Add(drCurrentRowSaveCode);
            }
        }
        ViewState["ProductsDetails"] = DtSaveProdDetails;
    }
    #endregion
    //Add Grid of Technalogy Details
    #region Grid of Technology Details
    private void SetInitialRowTechnologyDetails()
    {
        //Create false table
        DataTable dtTechDetail = new DataTable();
        DataRow drTechDetail = null;
        dtTechDetail.Columns.Add(new DataColumn("RowNumberTech", typeof(string)));
        dtTechDetail.Columns.Add(new DataColumn("TechNomenclature", typeof(string)));
        dtTechDetail.Columns.Add(new DataColumn("Technology1", typeof(string)));
        dtTechDetail.Columns.Add(new DataColumn("Technology2", typeof(string)));
        dtTechDetail.Columns.Add(new DataColumn("Technology3", typeof(string)));
        drTechDetail = dtTechDetail.NewRow();
        drTechDetail["RowNumberTech"] = 1;
        drTechDetail["TechNomenclature"] = string.Empty;
        drTechDetail["Technology1"] = string.Empty;
        drTechDetail["Technology2"] = string.Empty;
        drTechDetail["Technology3"] = string.Empty;
        dtTechDetail.Rows.Add(drTechDetail);
        //Store the DataTable in ViewState or bind or show false grid
        ViewState["TechnologyDetails"] = dtTechDetail;
        gvtechnology.DataSource = dtTechDetail;
        gvtechnology.DataBind();
        //After binding the gridview, we can then extract and fill the DropDownList with Data 
        DropDownList ddltech1 = (DropDownList)gvtechnology.Rows[0].Cells[2].FindControl("ddltech1");
        BindMasterTechnologyCategory(ddltech1);
    }
    protected void BindMasterTechnologyCategory(DropDownList ddltech1)
    {
        DataTable DtTechDrop = new DataTable();
        DtTechDrop = Lo.RetriveMasterSubCategoryDate(0, "PRODUCT (INDUSTRY DOMAIN)", "", "SelectProductCat", "", "");
        if (DtTechDrop.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddltech1, DtTechDrop, "SCategoryName", "SCategoryID");
            ddltech1.Items.Insert(0, "Select");
        }
    }
    protected void ddltech1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvrtech = (GridViewRow)(((Control)sender).NamingContainer);
        int rowIndextech = gvrtech.RowIndex;
        DropDownList ddltech1 = gvtechnology.Rows[rowIndextech].FindControl("ddltech1") as DropDownList;
        DropDownList ddltech2 = gvtechnology.Rows[rowIndextech].FindControl("ddltech2") as DropDownList;
        BindMasterTechnologySubCat(ddltech1, ddltech2);
    }
    protected void BindMasterTechnologySubCat(DropDownList ddltech1, DropDownList ddltech2)
    {
        if (ddltech1.SelectedItem.Text != "Select")
        {
            DataTable DtTech2 = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddltech1.SelectedItem.Value), "", "", "SubSelectID", "", "");
            if (DtTech2.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddltech2, DtTech2, "SCategoryName", "SCategoryId");
                ddltech2.Items.Insert(0, "Select");
            }
            else
            {
                ddltech2.Items.Clear();
                ddltech2.Items.Insert(0, "Select");
            }
        }
    }
    //protected void txttech2_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);
    //    int rowIndex = gvr.RowIndex;
    //    DropDownList ddltech2 = gvtechnology.Rows[rowIndex].FindControl("ddltech2") as DropDownList;
    //    DropDownList ddltech3 = gvtechnology.Rows[rowIndex].FindControl("ddltech3") as DropDownList;
    //    BindMasterSubTech3(ddltech2, ddltech3);
    //}
    protected void BindMasterSubTech3(DropDownList ddltech2, DropDownList ddltech3)
    {
        if (ddltech2.Text != "" && ddltech2.SelectedItem.Text != "Select")
        {
            DataTable DtMasterCatTech3 = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddltech2.SelectedItem.Value), "", "", "SubSelectID", "", "");
            if (DtMasterCatTech3.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddltech3, DtMasterCatTech3, "SCategoryName", "SCategoryId");
                ddltech3.Items.Insert(0, "Select");
            }
            else
            {
                ddltech3.Items.Clear();
                ddltech3.Items.Insert(0, "Select");
                ddltech3.Items.Insert(1, "NA");
            }
        }
    }
    private void AddNewTechnologyDetailGrid()
    {
        int rowIndexTech = 0;
        if (ViewState["TechnologyDetails"] != null)
        {
            DataTable dtCurrentTableTech = (DataTable)ViewState["TechnologyDetails"];
            DataRow drCurrentRowNameTech = null;
            if (dtCurrentTableTech.Rows.Count > 0)
            {
                if (dtCurrentTableTech.Rows.Count < 3)
                {
                    for (int i = 1; i <= dtCurrentTableTech.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        TextBox TextBox1 = (TextBox)gvtechnology.Rows[rowIndexTech].Cells[1].FindControl("txttechnomen");
                        DropDownList ddltech_1 = (DropDownList)gvtechnology.Rows[rowIndexTech].Cells[2].FindControl("ddltech1");
                        DropDownList ddlsubtech_2 = (DropDownList)gvtechnology.Rows[rowIndexTech].Cells[3].FindControl("ddltech2");
                        DropDownList ddlInnertech_3 = (DropDownList)gvtechnology.Rows[rowIndexTech].Cells[4].FindControl("ddltech3");
                        drCurrentRowNameTech = dtCurrentTableTech.NewRow();
                        drCurrentRowNameTech["RowNumberTech"] = i + 1;
                        dtCurrentTableTech.Rows[i - 1]["TechNomenclature"] = TextBox1.Text;
                        dtCurrentTableTech.Rows[i - 1]["Technology1"] = ddltech_1.Text;
                        dtCurrentTableTech.Rows[i - 1]["Technology2"] = ddlsubtech_2.Text;
                        dtCurrentTableTech.Rows[i - 1]["Technology3"] = ddlInnertech_3.Text;
                        rowIndexTech++;
                    }
                    dtCurrentTableTech.Rows.Add(drCurrentRowNameTech);
                    ViewState["TechnologyDetails"] = dtCurrentTableTech;
                    gvtechnology.DataSource = dtCurrentTableTech;
                    gvtechnology.DataBind();
                    DropDownList ddl_13 = (DropDownList)gvtechnology.Rows[0].Cells[2].FindControl("ddltech1");
                    BindMasterTechnologyCategory(ddl_13);
                    SetPreviousDataTechnologyDetail();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Could not add more then row Technology Details')", true);
                }
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
    }
    private void SetPreviousDataTechnologyDetail()
    {
        int rowIndexTech = 0;
        if (ViewState["TechnologyDetails"] != null)
        {
            DataTable dtTechPrevious = (DataTable)ViewState["TechnologyDetails"];
            if (dtTechPrevious.Rows.Count > 0)
            {
                for (int i = 0; i < dtTechPrevious.Rows.Count; i++)
                {
                    TextBox TBtech_1 = (TextBox)gvtechnology.Rows[rowIndexTech].Cells[1].FindControl("txttechnomen");
                    DropDownList DDLtech_1 = (DropDownList)gvtechnology.Rows[rowIndexTech].Cells[2].FindControl("ddltech1");
                    DropDownList DDLtech_2 = (DropDownList)gvtechnology.Rows[rowIndexTech].Cells[3].FindControl("ddltech2");
                    DropDownList DDLtech_3 = (DropDownList)gvtechnology.Rows[rowIndexTech].Cells[4].FindControl("ddltech3");
                    TBtech_1.Text = dtTechPrevious.Rows[i]["TechNomenclature"].ToString();
                    BindMasterTechnologyCategory(DDLtech_1);
                    if (i < dtTechPrevious.Rows.Count - 1)
                    {
                        //DataTable DtTechDrop = new DataTable();
                        //DtTechDrop = Lo.RetriveMasterSubCategoryDate(0, "PRODUCT (INDUSTRY DOMAIN)", "", "SelectProductCat", "", "");
                        //if (DtTechDrop.Rows.Count > 0)
                        //{
                        //    Co.FillDropdownlist(DDLtech_1, DtTechDrop, "SCategoryName", "SCategoryID");
                        //    DDLtech_1.Items.Insert(0, "Select");
                        DDLtech_1.Items.FindByValue(dtTechPrevious.Rows[i]["Technology1"].ToString()).Selected = true;
                        // }
                        if (DDLtech_1.SelectedItem.Text != "Select")
                        {
                            DataTable DtTech2 = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(DDLtech_1.SelectedItem.Value), "", "", "SubSelectID", "", "");
                            if (DtTech2.Rows.Count > 0)
                            {
                                Co.FillDropdownlist(DDLtech_2, DtTech2, "SCategoryName", "SCategoryId");
                                DDLtech_2.Items.Insert(0, "Select");
                                DDLtech_2.Items.FindByValue(dtTechPrevious.Rows[i]["Technology2"].ToString()).Selected = true;
                            }
                        }
                        if (DDLtech_2.Text != "" && DDLtech_2.SelectedItem.Text != "Select")
                        {
                            DataTable DtMasterCatTech3 = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(DDLtech_2.SelectedItem.Value), "", "", "SubSelectID", "", "");
                            if (DtMasterCatTech3.Rows.Count > 0)
                            {
                                Co.FillDropdownlist(DDLtech_3, DtMasterCatTech3, "SCategoryName", "SCategoryId");
                                DDLtech_3.Items.Insert(0, "Select");
                                DDLtech_3.Items.FindByValue(dtTechPrevious.Rows[i]["Technology3"].ToString()).Selected = true;
                            }
                            else
                            {
                                DDLtech_3.Items.Insert(0, "NA");
                            }
                        }
                    }
                    rowIndexTech++;
                }
            }
        }
    }
    protected void btnAddTech_Click(object sender, EventArgs e)
    {
        AddNewTechnologyDetailGrid();
    }
    protected void gvtechnology_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dtgridTechrowcreated = (DataTable)ViewState["TechnologyDetails"];
            LinkButton lbTech = (LinkButton)e.Row.FindControl("lbtechremove");
            if (lbTech != null)
            {
                if (dtgridTechrowcreated.Rows.Count > 1)
                {
                    if (e.Row.RowIndex == dtgridTechrowcreated.Rows.Count - 1)
                    {
                        lbTech.Visible = false;
                    }
                }
                else
                {
                    lbTech.Visible = false;
                }
            }
        }
    }
    protected void lbtechremove_Click(object sender, EventArgs e)
    {
        LinkButton lbTech = (LinkButton)sender;
        GridViewRow gvRowTech = (GridViewRow)lbTech.NamingContainer;
        int rowID = gvRowTech.RowIndex;
        if (ViewState["TechnologyDetails"] != null)
        {
            DataTable dtremovegridTech = (DataTable)ViewState["TechnologyDetails"];
            if (dtremovegridTech.Rows.Count > 1)
            {
                if (gvRowTech.RowIndex < dtremovegridTech.Rows.Count - 1)
                {
                    dtremovegridTech.Rows.Remove(dtremovegridTech.Rows[rowID]);
                    ResetRowIDTechDetail(dtremovegridTech);
                }
            }
            ViewState["TechnologyDetails"] = dtremovegridTech;
            gvtechnology.DataSource = dtremovegridTech;
            gvtechnology.DataBind();
        }
        SetPreviousDataTechnologyDetail();
    }
    private void ResetRowIDTechDetail(DataTable dtRemoveTechdetail)
    {
        int rowNumberTech = 1;
        if (dtRemoveTechdetail.Rows.Count > 0)
        {
            foreach (DataRow rowTech in dtRemoveTechdetail.Rows)
            {
                rowTech[0] = rowNumberTech;
                rowNumberTech++;
            }
        }
    }
    DataTable DtSaveTech = new DataTable();
    protected void SaveCodeTechDetails()
    {
        DtSaveTech.Columns.Add(new DataColumn("RowNumberTech", typeof(string)));
        DtSaveTech.Columns.Add(new DataColumn("TechNomenclature", typeof(string)));
        DtSaveTech.Columns.Add(new DataColumn("Technology1", typeof(string)));
        DtSaveTech.Columns.Add(new DataColumn("Technology2", typeof(string)));
        DtSaveTech.Columns.Add(new DataColumn("Technology3", typeof(string)));
        DataRow drCurrentRowSaveCode = null;
        for (int i = 0; gvtechnology.Rows.Count > i; i++)
        {
            TextBox TBtech_1 = (TextBox)gvtechnology.Rows[i].Cells[1].FindControl("txttechnomen");
            DropDownList DDLtech_1 = (DropDownList)gvtechnology.Rows[i].Cells[2].FindControl("ddltech1");
            DropDownList DDLtech_2 = (DropDownList)gvtechnology.Rows[i].Cells[3].FindControl("ddltech2");
            DropDownList DDLtech_3 = (DropDownList)gvtechnology.Rows[i].Cells[4].FindControl("ddltech3");
            if (DDLtech_1.SelectedItem.Text != "Select" && TBtech_1.Text != "")
            {
                drCurrentRowSaveCode = DtSaveTech.NewRow();
                drCurrentRowSaveCode["RowNumberTech"] = i + 1;
                drCurrentRowSaveCode["TechNomenclature"] = TBtech_1.Text;
                drCurrentRowSaveCode["Technology1"] = DDLtech_1.SelectedItem.Value;
                drCurrentRowSaveCode["Technology2"] = DDLtech_2.SelectedItem.Value;
                drCurrentRowSaveCode["Technology3"] = "NA";//DDLtech_3.SelectedItem.Value;
                DtSaveTech.Rows.Add(drCurrentRowSaveCode);
            }
        }
        ViewState["TechnologyDetails"] = DtSaveTech;
    }
    #endregion
    // Add  Grid of Source of Raw Material
    #region Grid of Source of Raw Material
    private void SetRawmeterialDetails()
    {
        DataTable dtRawmeterialDetail = new DataTable();
        DataRow drRawmeterialDetail = null;
        dtRawmeterialDetail.Columns.Add(new DataColumn("SrNoRawMeterail", typeof(string)));
        dtRawmeterialDetail.Columns.Add(new DataColumn("Items", typeof(string)));
        dtRawmeterialDetail.Columns.Add(new DataColumn("RawMeterial", typeof(string)));
        dtRawmeterialDetail.Columns.Add(new DataColumn("SourceMeterial", typeof(string)));
        dtRawmeterialDetail.Columns.Add(new DataColumn("MeterailSupplier", typeof(string)));
        drRawmeterialDetail = dtRawmeterialDetail.NewRow();
        drRawmeterialDetail["SrNoRawMeterail"] = 1;
        drRawmeterialDetail["Items"] = string.Empty;
        drRawmeterialDetail["RawMeterial"] = string.Empty;
        drRawmeterialDetail["SourceMeterial"] = string.Empty;
        drRawmeterialDetail["MeterailSupplier"] = string.Empty;
        dtRawmeterialDetail.Rows.Add(drRawmeterialDetail);
        ViewState["RawMeterialDetails"] = dtRawmeterialDetail;
        gvSourceofRawMaterial.DataSource = dtRawmeterialDetail;
        gvSourceofRawMaterial.DataBind();
    }
    private void AddSourceofRawMeterial()
    {
        int rowIndexRawmeterial = 0;
        if (ViewState["RawMeterialDetails"] != null)
        {
            DataTable dtCurrentRawMeterialDetails = (DataTable)ViewState["RawMeterialDetails"];
            DataRow drCurrentRawMeterialDetails = null;
            if (dtCurrentRawMeterialDetails.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentRawMeterialDetails.Rows.Count; i++)
                {
                    //extract the TextBox values
                    TextBox TextBox1_Raw = (TextBox)gvSourceofRawMaterial.Rows[rowIndexRawmeterial].Cells[1].FindControl("txtitems");
                    TextBox TextBox2_Raw = (TextBox)gvSourceofRawMaterial.Rows[rowIndexRawmeterial].Cells[2].FindControl("txtbasicrawmeterial");
                    DropDownList ddl3_Raw = (DropDownList)gvSourceofRawMaterial.Rows[rowIndexRawmeterial].Cells[3].FindControl("ddlsourceofmaterial");
                    TextBox TextBox4_Raw = (TextBox)gvSourceofRawMaterial.Rows[rowIndexRawmeterial].Cells[4].FindControl("txtmaterialsupplier");
                    drCurrentRawMeterialDetails = dtCurrentRawMeterialDetails.NewRow();
                    drCurrentRawMeterialDetails["SrNoRawMeterail"] = i + 1;
                    dtCurrentRawMeterialDetails.Rows[i - 1]["Items"] = TextBox1_Raw.Text;
                    dtCurrentRawMeterialDetails.Rows[i - 1]["RawMeterial"] = TextBox2_Raw.Text;
                    dtCurrentRawMeterialDetails.Rows[i - 1]["SourceMeterial"] = ddl3_Raw.Text;
                    dtCurrentRawMeterialDetails.Rows[i - 1]["MeterailSupplier"] = TextBox4_Raw.Text;
                    rowIndexRawmeterial++;
                }
                dtCurrentRawMeterialDetails.Rows.Add(drCurrentRawMeterialDetails);
                ViewState["RawMeterialDetails"] = dtCurrentRawMeterialDetails;
                gvSourceofRawMaterial.DataSource = dtCurrentRawMeterialDetails;
                gvSourceofRawMaterial.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousDataSourceofRawMeterial();
    }
    private void SetPreviousDataSourceofRawMeterial()
    {
        int rowIndexRawmeterialPre = 0;
        if (ViewState["RawMeterialDetails"] != null)
        {
            DataTable dtPreSourceRaw = (DataTable)ViewState["RawMeterialDetails"];
            if (dtPreSourceRaw.Rows.Count > 0)
            {
                for (int i = 0; i < dtPreSourceRaw.Rows.Count; i++)
                {
                    TextBox TextBox_1Raw = (TextBox)gvSourceofRawMaterial.Rows[rowIndexRawmeterialPre].Cells[1].FindControl("txtitems");
                    TextBox TextBox_2Raw = (TextBox)gvSourceofRawMaterial.Rows[rowIndexRawmeterialPre].Cells[2].FindControl("txtbasicrawmeterial");
                    DropDownList ddl_3Raw = (DropDownList)gvSourceofRawMaterial.Rows[rowIndexRawmeterialPre].Cells[3].FindControl("ddlsourceofmaterial");
                    TextBox TextBox_4Raw = (TextBox)gvSourceofRawMaterial.Rows[rowIndexRawmeterialPre].Cells[4].FindControl("txtmaterialsupplier");
                    TextBox_1Raw.Text = dtPreSourceRaw.Rows[i]["Items"].ToString();
                    TextBox_2Raw.Text = dtPreSourceRaw.Rows[i]["RawMeterial"].ToString();
                    if (i < dtPreSourceRaw.Rows.Count - 1)
                    {
                        ddl_3Raw.ClearSelection();
                        ddl_3Raw.Items.FindByValue(dtPreSourceRaw.Rows[i]["SourceMeterial"].ToString()).Selected = true;
                    }
                    TextBox_4Raw.Text = dtPreSourceRaw.Rows[i]["MeterailSupplier"].ToString();
                    rowIndexRawmeterialPre++;
                }
            }
        }
    }
    protected void btnAddRawMeterial_Click(object sender, EventArgs e)
    {
        AddSourceofRawMeterial();
    }
    protected void lbmeterailremove_Click(object sender, EventArgs e)
    {
        LinkButton lbRaw = (LinkButton)sender;
        GridViewRow gvRowMeterial = (GridViewRow)lbRaw.NamingContainer;
        int rowID = gvRowMeterial.RowIndex;
        if (ViewState["RawMeterialDetails"] != null)
        {
            DataTable dtremovegridRawmete = (DataTable)ViewState["RawMeterialDetails"];
            if (dtremovegridRawmete.Rows.Count > 1)
            {
                if (gvRowMeterial.RowIndex < dtremovegridRawmete.Rows.Count - 1)
                {
                    dtremovegridRawmete.Rows.Remove(dtremovegridRawmete.Rows[rowID]);
                    ResetRowIDRawmeterial(dtremovegridRawmete);
                }
            }
            ViewState["RawMeterialDetails"] = dtremovegridRawmete;
            gvSourceofRawMaterial.DataSource = dtremovegridRawmete;
            gvSourceofRawMaterial.DataBind();
        }
        SetPreviousDataSourceofRawMeterial();
    }
    private void ResetRowIDRawmeterial(DataTable dtremovecountrawmete)
    {
        int rowNumbermeterial = 1;
        if (dtremovecountrawmete.Rows.Count > 0)
        {
            foreach (DataRow row in dtremovecountrawmete.Rows)
            {
                row[0] = rowNumbermeterial;
                rowNumbermeterial++;
            }
        }
    }
    protected void gvSourceofRawMaterial_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dtgridrowmetecreated = (DataTable)ViewState["RawMeterialDetails"];
            LinkButton lbraw = (LinkButton)e.Row.FindControl("lbmeterailremove");
            if (lbraw != null)
            {
                if (dtgridrowmetecreated.Rows.Count > 1)
                {
                    if (e.Row.RowIndex == dtgridrowmetecreated.Rows.Count - 1)
                    {
                        lbraw.Visible = false;
                    }
                }
                else
                {
                    lbraw.Visible = false;
                }
            }
        }
    }
    DataTable DtSaveRawMeterial = new DataTable();
    private void SaveRawmeterial()
    {
        DtSaveRawMeterial.Columns.Add(new DataColumn("SrNoRawMeterail", typeof(string)));
        DtSaveRawMeterial.Columns.Add(new DataColumn("Items", typeof(string)));
        DtSaveRawMeterial.Columns.Add(new DataColumn("RawMeterial", typeof(string)));
        DtSaveRawMeterial.Columns.Add(new DataColumn("SourceMeterial", typeof(string)));
        DtSaveRawMeterial.Columns.Add(new DataColumn("MeterailSupplier", typeof(string)));
        DataRow drCurrentRowSaveCode = null;
        for (int i = 0; gvSourceofRawMaterial.Rows.Count > i; i++)
        {
            TextBox TextBox_1Raw = (TextBox)gvSourceofRawMaterial.Rows[i].Cells[1].FindControl("txtitems");
            TextBox TextBox_2Raw = (TextBox)gvSourceofRawMaterial.Rows[i].Cells[2].FindControl("txtbasicrawmeterial");
            DropDownList ddl_3Raw = (DropDownList)gvSourceofRawMaterial.Rows[i].Cells[3].FindControl("ddlsourceofmaterial");
            TextBox TextBox_4Raw = (TextBox)gvSourceofRawMaterial.Rows[i].Cells[4].FindControl("txtmaterialsupplier");
            if (ddl_3Raw.SelectedItem.Text != "Select" && TextBox_1Raw.Text != "")
            {
                drCurrentRowSaveCode = DtSaveRawMeterial.NewRow();
                drCurrentRowSaveCode["SrNoRawMeterail"] = i + 1;
                drCurrentRowSaveCode["Items"] = TextBox_1Raw.Text;
                drCurrentRowSaveCode["RawMeterial"] = TextBox_2Raw.Text;
                drCurrentRowSaveCode["SourceMeterial"] = ddl_3Raw.SelectedItem.Value;
                drCurrentRowSaveCode["MeterailSupplier"] = TextBox_4Raw.Text;
                DtSaveRawMeterial.Rows.Add(drCurrentRowSaveCode);
            }
        }
        ViewState["RawMeterialDetails"] = DtSaveRawMeterial;
    }
    #endregion
    //Add Grid of Item Produced and Supplied
    #region Item Product and supplied
    private void SetInitialRowItemProductorSupplied()
    {
        //Create false table
        DataTable dtProdSupp = new DataTable();
        DataRow drProdSupp = null;
        dtProdSupp.Columns.Add(new DataColumn("SrNoSpplied", typeof(string)));
        dtProdSupp.Columns.Add(new DataColumn("NameCust", typeof(string)));
        dtProdSupp.Columns.Add(new DataColumn("DesStoreSupp", typeof(string)));
        dtProdSupp.Columns.Add(new DataColumn("OderNoorDate", typeof(string)));
        dtProdSupp.Columns.Add(new DataColumn("OrderQty", typeof(string)));
        dtProdSupp.Columns.Add(new DataColumn("ValueQtySupp", typeof(string)));
        dtProdSupp.Columns.Add(new DataColumn("DateofLastSupp", typeof(string)));
        drProdSupp = dtProdSupp.NewRow();
        drProdSupp["SrNoSpplied"] = 1;
        drProdSupp["NameCust"] = string.Empty;
        drProdSupp["DesStoreSupp"] = string.Empty;
        drProdSupp["OderNoorDate"] = string.Empty;
        drProdSupp["OrderQty"] = string.Empty;
        drProdSupp["ValueQtySupp"] = string.Empty;
        drProdSupp["DateofLastSupp"] = string.Empty;
        dtProdSupp.Rows.Add(drProdSupp);
        //Store the DataTable in ViewState or bind or show false grid
        ViewState["ProdSupp"] = dtProdSupp;
        gvItemProducedandSupplied.DataSource = dtProdSupp;
        gvItemProducedandSupplied.DataBind();
    }
    private void AddNewRowItemProductorSupplied()
    {
        int rowIndexProd = 0;
        if (ViewState["ProdSupp"] != null)
        {
            DataTable dtCurrentTableProd = (DataTable)ViewState["ProdSupp"];
            DataRow drCurrentRowProd = null;
            if (dtCurrentTableProd.Rows.Count > 0)
            {
                if (dtCurrentTableProd.Rows.Count > 2)
                { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Could not add more then 3 years row supply orders.')", true); }
                else
                {
                    for (int i = 1; i <= dtCurrentTableProd.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        TextBox TextBox1_Prod = (TextBox)gvItemProducedandSupplied.Rows[rowIndexProd].Cells[1].FindControl("txtnameofrepcustomer");
                        TextBox TextBox2_Prod = (TextBox)gvItemProducedandSupplied.Rows[rowIndexProd].Cells[2].FindControl("txtdescofstoresupp");
                        TextBox TextBox3_Prod = (TextBox)gvItemProducedandSupplied.Rows[rowIndexProd].Cells[3].FindControl("txtsonoanddate");
                        TextBox TextBox4_Prod = (TextBox)gvItemProducedandSupplied.Rows[rowIndexProd].Cells[4].FindControl("txtorderqty");
                        TextBox TextBox5_Prod = (TextBox)gvItemProducedandSupplied.Rows[rowIndexProd].Cells[5].FindControl("txtvalueqtysupplied");
                        TextBox TextBox6_Prod = (TextBox)gvItemProducedandSupplied.Rows[rowIndexProd].Cells[6].FindControl("txtdateoflastsupplie");
                        drCurrentRowProd = dtCurrentTableProd.NewRow();
                        drCurrentRowProd["SrNoSpplied"] = i + 1;
                        dtCurrentTableProd.Rows[i - 1]["NameCust"] = TextBox1_Prod.Text;
                        dtCurrentTableProd.Rows[i - 1]["DesStoreSupp"] = TextBox2_Prod.Text;
                        dtCurrentTableProd.Rows[i - 1]["OderNoorDate"] = TextBox3_Prod.Text;
                        dtCurrentTableProd.Rows[i - 1]["OrderQty"] = TextBox4_Prod.Text;
                        dtCurrentTableProd.Rows[i - 1]["ValueQtySupp"] = TextBox5_Prod.Text;
                        dtCurrentTableProd.Rows[i - 1]["DateofLastSupp"] = TextBox6_Prod.Text;
                        rowIndexProd++;
                    }
                    dtCurrentTableProd.Rows.Add(drCurrentRowProd);
                    ViewState["ProdSupp"] = dtCurrentTableProd;
                    gvItemProducedandSupplied.DataSource = dtCurrentTableProd;
                    gvItemProducedandSupplied.DataBind();
                    SetPreviousDataItemProductorSupplied();
                }
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

    }
    private void SetPreviousDataItemProductorSupplied()
    {
        int rowIndexPro = 0;
        if (ViewState["ProdSupp"] != null)
        {
            DataTable dtProdPer = (DataTable)ViewState["ProdSupp"];
            if (dtProdPer.Rows.Count > 0)
            {
                for (int i = 0; i < dtProdPer.Rows.Count; i++)
                {
                    TextBox TextBox_1 = (TextBox)gvItemProducedandSupplied.Rows[rowIndexPro].Cells[1].FindControl("txtnameofrepcustomer");
                    TextBox TextBox_2 = (TextBox)gvItemProducedandSupplied.Rows[rowIndexPro].Cells[2].FindControl("txtdescofstoresupp");
                    TextBox TextBox_3 = (TextBox)gvItemProducedandSupplied.Rows[rowIndexPro].Cells[3].FindControl("txtsonoanddate");
                    TextBox TextBox_4 = (TextBox)gvItemProducedandSupplied.Rows[rowIndexPro].Cells[4].FindControl("txtorderqty");
                    TextBox TextBox_5 = (TextBox)gvItemProducedandSupplied.Rows[rowIndexPro].Cells[5].FindControl("txtvalueqtysupplied");
                    TextBox TextBox_6 = (TextBox)gvItemProducedandSupplied.Rows[rowIndexPro].Cells[6].FindControl("txtdateoflastsupplie");
                    TextBox_1.Text = dtProdPer.Rows[i]["NameCust"].ToString();
                    TextBox_2.Text = dtProdPer.Rows[i]["DesStoreSupp"].ToString();
                    TextBox_3.Text = dtProdPer.Rows[i]["OderNoorDate"].ToString();
                    TextBox_4.Text = dtProdPer.Rows[i]["OrderQty"].ToString();
                    TextBox_5.Text = dtProdPer.Rows[i]["ValueQtySupp"].ToString();
                    TextBox_6.Text = dtProdPer.Rows[i]["DateofLastSupp"].ToString();
                    rowIndexPro++;
                }
            }
        }
    }
    protected void btnAddSupplied_Click(object sender, EventArgs e)
    {
        AddNewRowItemProductorSupplied();
    }
    protected void gvItemProducedandSupplied_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dtgridProd = (DataTable)ViewState["ProdSupp"];
            LinkButton lbProd = (LinkButton)e.Row.FindControl("lbSuplliedremove");
            if (lbProd != null)
            {
                if (dtgridProd.Rows.Count > 1)
                {
                    if (e.Row.RowIndex == dtgridProd.Rows.Count - 1)
                    {
                        lbProd.Visible = false;
                    }
                }
                else
                {
                    lbProd.Visible = false;
                }
            }
        }
    }
    protected void lbSuplliedremove_Click(object sender, EventArgs e)
    {
        LinkButton lbProd = (LinkButton)sender;
        GridViewRow gvRowProd = (GridViewRow)lbProd.NamingContainer;
        int rowID = gvRowProd.RowIndex;
        if (ViewState["ProdSupp"] != null)
        {
            DataTable dtremovegridProd = (DataTable)ViewState["ProdSupp"];
            if (dtremovegridProd.Rows.Count > 1)
            {
                if (gvRowProd.RowIndex < dtremovegridProd.Rows.Count - 1)
                {
                    dtremovegridProd.Rows.Remove(dtremovegridProd.Rows[rowID]);
                    ResetRowIDProd(dtremovegridProd);
                }
            }
            ViewState["ProdSupp"] = dtremovegridProd;
            gvItemProducedandSupplied.DataSource = dtremovegridProd;
            gvItemProducedandSupplied.DataBind();
        }
        SetPreviousDataItemProductorSupplied();
    }
    private void ResetRowIDProd(DataTable dtcountProd)
    {
        int rowNumberProd = 1;
        if (dtcountProd.Rows.Count > 0)
        {
            foreach (DataRow row in dtcountProd.Rows)
            {
                row[0] = rowNumberProd;
                rowNumberProd++;
            }
        }
    }
    DataTable DtSavePAS = new DataTable();
    protected void SavePAS()
    {
        DtSavePAS.Columns.Add(new DataColumn("SrNoSpplied", typeof(string)));
        DtSavePAS.Columns.Add(new DataColumn("NameCust", typeof(string)));
        DtSavePAS.Columns.Add(new DataColumn("DesStoreSupp", typeof(string)));
        DtSavePAS.Columns.Add(new DataColumn("OderNoorDate", typeof(string)));
        DtSavePAS.Columns.Add(new DataColumn("OrderQty", typeof(string)));
        DtSavePAS.Columns.Add(new DataColumn("ValueQtySupp", typeof(string)));
        DtSavePAS.Columns.Add(new DataColumn("DateofLastSupp", typeof(string)));
        DataRow drCurrentRowSaveCode = null;
        for (int i = 0; gvItemProducedandSupplied.Rows.Count > i; i++)
        {
            TextBox TextBox_1 = (TextBox)gvItemProducedandSupplied.Rows[i].Cells[1].FindControl("txtnameofrepcustomer");
            TextBox TextBox_2 = (TextBox)gvItemProducedandSupplied.Rows[i].Cells[2].FindControl("txtdescofstoresupp");
            TextBox TextBox_3 = (TextBox)gvItemProducedandSupplied.Rows[i].Cells[3].FindControl("txtsonoanddate");
            TextBox TextBox_4 = (TextBox)gvItemProducedandSupplied.Rows[i].Cells[4].FindControl("txtorderqty");
            TextBox TextBox_5 = (TextBox)gvItemProducedandSupplied.Rows[i].Cells[5].FindControl("txtvalueqtysupplied");
            TextBox TextBox_6 = (TextBox)gvItemProducedandSupplied.Rows[i].Cells[6].FindControl("txtdateoflastsupplie");
            if (TextBox_1.Text != "" && TextBox_2.Text != "")
            {
                drCurrentRowSaveCode = DtSavePAS.NewRow();
                drCurrentRowSaveCode["SrNoSpplied"] = i + 1;
                drCurrentRowSaveCode["NameCust"] = TextBox_1.Text;
                drCurrentRowSaveCode["DesStoreSupp"] = TextBox_2.Text;
                drCurrentRowSaveCode["OderNoorDate"] = TextBox_3.Text;
                drCurrentRowSaveCode["OrderQty"] = TextBox_4.Text;
                drCurrentRowSaveCode["ValueQtySupp"] = TextBox_5.Text;
                if (TextBox_6.Text != "")
                {
                    DateTime Date1 = Convert.ToDateTime(TextBox_6.Text);
                    string mDate1 = Date1.ToString("yyyy-MMM-dd");
                    drCurrentRowSaveCode["DateofLastSupp"] = mDate1.ToString();
                }
                else
                { drCurrentRowSaveCode["DateofLastSupp"] = null; }
                DtSavePAS.Rows.Add(drCurrentRowSaveCode);
            }
        }
        ViewState["ProdSupp"] = DtSavePAS;
    }
    #endregion
    // Add grid of Item Supplied but not produced
    #region item supplied but not produced
    private void SetInitialRowItemProductorSupplied1()
    {
        //Create false table
        DataTable dtProdSupp1 = new DataTable();
        DataRow drProdSupp1 = null;
        dtProdSupp1.Columns.Add(new DataColumn("SrNoSpplied1", typeof(string)));
        dtProdSupp1.Columns.Add(new DataColumn("NameCust1", typeof(string)));
        dtProdSupp1.Columns.Add(new DataColumn("DesStoreSupp1", typeof(string)));
        dtProdSupp1.Columns.Add(new DataColumn("OderNoorDate1", typeof(string)));
        dtProdSupp1.Columns.Add(new DataColumn("OrderQty1", typeof(string)));
        dtProdSupp1.Columns.Add(new DataColumn("ValueQtySupp1", typeof(string)));
        dtProdSupp1.Columns.Add(new DataColumn("DateofLastSupp1", typeof(string)));
        drProdSupp1 = dtProdSupp1.NewRow();
        drProdSupp1["SrNoSpplied1"] = 1;
        drProdSupp1["NameCust1"] = string.Empty;
        drProdSupp1["DesStoreSupp1"] = string.Empty;
        drProdSupp1["OderNoorDate1"] = string.Empty;
        drProdSupp1["OrderQty1"] = string.Empty;
        drProdSupp1["ValueQtySupp1"] = string.Empty;
        drProdSupp1["DateofLastSupp1"] = string.Empty;
        dtProdSupp1.Rows.Add(drProdSupp1);
        //Store the DataTable in ViewState or bind or show false grid
        ViewState["ProdSupp1"] = dtProdSupp1;
        gvItemSuppliedbutnotproduced.DataSource = dtProdSupp1;
        gvItemSuppliedbutnotproduced.DataBind();
    }
    private void AddNewRowItemProductorSupplied1()
    {
        int rowIndexProd1 = 0;
        if (ViewState["ProdSupp1"] != null)
        {
            DataTable dtCurrentTableProd1 = (DataTable)ViewState["ProdSupp1"];
            DataRow drCurrentRowProd1 = null;
            if (dtCurrentTableProd1.Rows.Count > 0)
            {
                if (dtCurrentTableProd1.Rows.Count > 2)
                { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Could not add more then 3 years row supply orders.')", true); }
                else
                {
                    for (int i = 1; i <= dtCurrentTableProd1.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        TextBox TextBox1_Prod = (TextBox)gvItemSuppliedbutnotproduced.Rows[rowIndexProd1].Cells[1].FindControl("txtnameofrepcustomer1");
                        TextBox TextBox2_Prod = (TextBox)gvItemSuppliedbutnotproduced.Rows[rowIndexProd1].Cells[2].FindControl("txtdescofstoresupp1");
                        TextBox TextBox3_Prod = (TextBox)gvItemSuppliedbutnotproduced.Rows[rowIndexProd1].Cells[3].FindControl("txtsonoanddate1");
                        TextBox TextBox4_Prod = (TextBox)gvItemSuppliedbutnotproduced.Rows[rowIndexProd1].Cells[4].FindControl("txtorderqty1");
                        TextBox TextBox5_Prod = (TextBox)gvItemSuppliedbutnotproduced.Rows[rowIndexProd1].Cells[5].FindControl("txtvalueqtysupplied1");
                        TextBox TextBox6_Prod = (TextBox)gvItemSuppliedbutnotproduced.Rows[rowIndexProd1].Cells[6].FindControl("txtdateoflastsupplie1");
                        drCurrentRowProd1 = dtCurrentTableProd1.NewRow();
                        drCurrentRowProd1["SrNoSpplied1"] = i + 1;
                        dtCurrentTableProd1.Rows[i - 1]["NameCust1"] = TextBox1_Prod.Text;
                        dtCurrentTableProd1.Rows[i - 1]["DesStoreSupp1"] = TextBox2_Prod.Text;
                        dtCurrentTableProd1.Rows[i - 1]["OderNoorDate1"] = TextBox3_Prod.Text;
                        dtCurrentTableProd1.Rows[i - 1]["OrderQty1"] = TextBox4_Prod.Text;
                        dtCurrentTableProd1.Rows[i - 1]["ValueQtySupp1"] = TextBox5_Prod.Text;
                        dtCurrentTableProd1.Rows[i - 1]["DateofLastSupp1"] = TextBox6_Prod.Text;
                        rowIndexProd1++;
                    }
                    dtCurrentTableProd1.Rows.Add(drCurrentRowProd1);
                    ViewState["ProdSupp1"] = dtCurrentTableProd1;
                    gvItemSuppliedbutnotproduced.DataSource = dtCurrentTableProd1;
                    gvItemSuppliedbutnotproduced.DataBind();
                    SetPreviousDataItemProductorSupplied1();
                }
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

    }
    private void SetPreviousDataItemProductorSupplied1()
    {
        int rowIndexPro1 = 0;
        if (ViewState["ProdSupp1"] != null)
        {
            DataTable dtProdPer1 = (DataTable)ViewState["ProdSupp1"];
            if (dtProdPer1.Rows.Count > 0)
            {
                for (int i = 0; i < dtProdPer1.Rows.Count; i++)
                {
                    TextBox TextBox_1 = (TextBox)gvItemSuppliedbutnotproduced.Rows[rowIndexPro1].Cells[1].FindControl("txtnameofrepcustomer1");
                    TextBox TextBox_2 = (TextBox)gvItemSuppliedbutnotproduced.Rows[rowIndexPro1].Cells[2].FindControl("txtdescofstoresupp1");
                    TextBox TextBox_3 = (TextBox)gvItemSuppliedbutnotproduced.Rows[rowIndexPro1].Cells[3].FindControl("txtsonoanddate1");
                    TextBox TextBox_4 = (TextBox)gvItemSuppliedbutnotproduced.Rows[rowIndexPro1].Cells[4].FindControl("txtorderqty1");
                    TextBox TextBox_5 = (TextBox)gvItemSuppliedbutnotproduced.Rows[rowIndexPro1].Cells[5].FindControl("txtvalueqtysupplied1");
                    TextBox TextBox_6 = (TextBox)gvItemSuppliedbutnotproduced.Rows[rowIndexPro1].Cells[6].FindControl("txtdateoflastsupplie1");
                    TextBox_1.Text = dtProdPer1.Rows[i]["NameCust1"].ToString();
                    TextBox_2.Text = dtProdPer1.Rows[i]["DesStoreSupp1"].ToString();
                    TextBox_3.Text = dtProdPer1.Rows[i]["OderNoorDate1"].ToString();
                    TextBox_4.Text = dtProdPer1.Rows[i]["OrderQty1"].ToString();
                    TextBox_5.Text = dtProdPer1.Rows[i]["ValueQtySupp1"].ToString();
                    TextBox_6.Text = dtProdPer1.Rows[i]["DateofLastSupp1"].ToString();
                    rowIndexPro1++;
                }
            }
        }
    }
    protected void btnAddSupplied1_Click(object sender, EventArgs e)
    {
        AddNewRowItemProductorSupplied1();
    }
    protected void gvItemSuppliedbutnotproduced_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dtgridProd1 = (DataTable)ViewState["ProdSupp1"];
            LinkButton lbProd1 = (LinkButton)e.Row.FindControl("lbSuplliedremove1");
            if (lbProd1 != null)
            {
                if (dtgridProd1.Rows.Count > 1)
                {
                    if (e.Row.RowIndex == dtgridProd1.Rows.Count - 1)
                    {
                        lbProd1.Visible = false;
                    }
                }
                else
                {
                    lbProd1.Visible = false;
                }
            }
        }
    }
    protected void lbSuplliedremove1_Click(object sender, EventArgs e)
    {
        LinkButton lbProd1 = (LinkButton)sender;
        GridViewRow gvRowProd1 = (GridViewRow)lbProd1.NamingContainer;
        int rowID = gvRowProd1.RowIndex;
        if (ViewState["ProdSupp1"] != null)
        {
            DataTable dtremovegridProd1 = (DataTable)ViewState["ProdSupp1"];
            if (dtremovegridProd1.Rows.Count > 1)
            {
                if (gvRowProd1.RowIndex < dtremovegridProd1.Rows.Count - 1)
                {
                    dtremovegridProd1.Rows.Remove(dtremovegridProd1.Rows[rowID]);
                    ResetRowIDProd1(dtremovegridProd1);
                }
            }
            ViewState["ProdSupp1"] = dtremovegridProd1;
            gvItemSuppliedbutnotproduced.DataSource = dtremovegridProd1;
            gvItemSuppliedbutnotproduced.DataBind();
        }
        SetPreviousDataItemProductorSupplied1();
    }
    private void ResetRowIDProd1(DataTable dtcountProd1)
    {
        int rowNumberProd1 = 1;
        if (dtcountProd1.Rows.Count > 0)
        {
            foreach (DataRow row in dtcountProd1.Rows)
            {
                row[0] = rowNumberProd1;
                rowNumberProd1++;
            }
        }
    }
    DataTable DtSaveProdSupp = new DataTable();
    protected void SaveProdSupp()
    {
        DtSaveProdSupp.Columns.Add(new DataColumn("SrNoSpplied1", typeof(string)));
        DtSaveProdSupp.Columns.Add(new DataColumn("NameCust1", typeof(string)));
        DtSaveProdSupp.Columns.Add(new DataColumn("DesStoreSupp1", typeof(string)));
        DtSaveProdSupp.Columns.Add(new DataColumn("OderNoorDate1", typeof(string)));
        DtSaveProdSupp.Columns.Add(new DataColumn("OrderQty1", typeof(string)));
        DtSaveProdSupp.Columns.Add(new DataColumn("ValueQtySupp1", typeof(string)));
        DtSaveProdSupp.Columns.Add(new DataColumn("DateofLastSupp1", typeof(string)));
        DataRow drCurrentRowSaveCode = null;
        for (int i = 0; gvItemSuppliedbutnotproduced.Rows.Count > i; i++)
        {
            TextBox TextBox_1 = (TextBox)gvItemSuppliedbutnotproduced.Rows[i].Cells[1].FindControl("txtnameofrepcustomer1");
            TextBox TextBox_2 = (TextBox)gvItemSuppliedbutnotproduced.Rows[i].Cells[2].FindControl("txtdescofstoresupp1");
            TextBox TextBox_3 = (TextBox)gvItemSuppliedbutnotproduced.Rows[i].Cells[3].FindControl("txtsonoanddate1");
            TextBox TextBox_4 = (TextBox)gvItemSuppliedbutnotproduced.Rows[i].Cells[4].FindControl("txtorderqty1");
            TextBox TextBox_5 = (TextBox)gvItemSuppliedbutnotproduced.Rows[i].Cells[5].FindControl("txtvalueqtysupplied1");
            TextBox TextBox_6 = (TextBox)gvItemSuppliedbutnotproduced.Rows[i].Cells[6].FindControl("txtdateoflastsupplie1");
            if (TextBox_1.Text != "" && TextBox_2.Text != "")
            {
                drCurrentRowSaveCode = DtSaveProdSupp.NewRow();
                drCurrentRowSaveCode["SrNoSpplied1"] = i + 1;
                drCurrentRowSaveCode["NameCust1"] = TextBox_1.Text;
                drCurrentRowSaveCode["DesStoreSupp1"] = TextBox_2.Text;
                drCurrentRowSaveCode["OderNoorDate1"] = TextBox_3.Text;
                drCurrentRowSaveCode["OrderQty1"] = TextBox_4.Text;
                drCurrentRowSaveCode["ValueQtySupp1"] = TextBox_5.Text;
                if (TextBox_6.Text != "")
                {
                    DateTime Date2 = Convert.ToDateTime(TextBox_6.Text);
                    string mDate2 = Date2.ToString("yyyy-MMM-dd");
                    drCurrentRowSaveCode["DateofLastSupp1"] = mDate2.ToString();
                }
                else
                {
                    drCurrentRowSaveCode["DateofLastSupp1"] = null;
                }
                DtSaveProdSupp.Rows.Add(drCurrentRowSaveCode);
            }
        }
        ViewState["ProdSupp1"] = DtSaveProdSupp;
    }
    #endregion
    //SaveCode or Update Code
    protected void SaveRegistration()
    {
        DataTable DtProdDetailsSave = new DataTable();
        DataTable dtTecgDetailsSave = new DataTable();
        DataTable dtRawmeterialSave = new DataTable();
        DataTable dtPasSave = new DataTable();
        DataTable DtProdSuppSave = new DataTable();
        if (btnsubmit.Text == "Update")
        {
            if (gvproddetail.Visible == true && gvtechnology.Visible == true && gvSourceofRawMaterial.Visible == true && gvItemProducedandSupplied.Visible == true && gvItemSuppliedbutnotproduced.Visible == true)
            {
                SaveCodeProdDetails();
                DtProdDetailsSave = (DataTable)ViewState["ProductsDetails"];
                SaveCodeTechDetails();
                dtTecgDetailsSave = (DataTable)ViewState["TechnologyDetails"];
                SaveRawmeterial();
                dtRawmeterialSave = (DataTable)ViewState["RawMeterialDetails"];
                SavePAS();
                dtPasSave = (DataTable)ViewState["ProdSupp"];
                SaveProdSupp();
                DtProdSuppSave = (DataTable)ViewState["ProdSupp1"];
            }
            else
            {
            }
        }
        else
        {
            SaveCodeProdDetails();
            DtProdDetailsSave = (DataTable)ViewState["ProductsDetails"];
            SaveCodeTechDetails();
            dtTecgDetailsSave = (DataTable)ViewState["TechnologyDetails"];
            SaveRawmeterial();
            dtRawmeterialSave = (DataTable)ViewState["RawMeterialDetails"];
            SavePAS();
            dtPasSave = (DataTable)ViewState["ProdSupp"];
            SaveProdSupp();
            DtProdSuppSave = (DataTable)ViewState["ProdSupp1"];
        }
        string str = Lo.SaveVendorDefence(DtProdDetailsSave, dtTecgDetailsSave, dtRawmeterialSave, dtPasSave, DtProdSuppSave, Enc.DecryptData(Session["VendorRefNo"].ToString()), out _sysMsg, out _msg);
        if (str != "")
        {
            if (btnsubmit.Text == "Update")
            {
                LoadP();
                lblmsg.Text = "Successfully update details of defence information";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
            }
            else
            {
                LoadP();
                lblmsg.Text = "Successfully save details of defence information";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
            }
        }
        else
        {
            lblmsg.Text = "Record not saved.";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            SaveRegistration();
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
        }
    }
    #endregion
    #region gridviewallrowcommand
    protected void gvproddetailedit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "addnewmfe")
        {
            lbsub.Text = "Submit";
            DataTable DtDropDownNatoGroup = new DataTable();
            DtDropDownNatoGroup = Lo.RetriveMasterSubCategoryDate(0, "NSN GROUP", "", "SelectProductCat", "", "");
            if (DtDropDownNatoGroup.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddlnatogroup, DtDropDownNatoGroup, "SCategoryName", "SCategoryID");
                ddlnatogroup.Items.Insert(0, "Select");
            }
            txtname.Text = "";
            ddlnatogroup.SelectedIndex = -1;
            txthsnedit.Text = "";
            ViewState["edit1"] = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divmodal1", "showPopup();", true);
        }
        else if (e.CommandName == "updatenewmfe")
        {
            try
            {
                lbsub.Text = "Edit & Update";
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvproddetailedit.Rows[rowIndex];
                HiddenField hfn = (HiddenField)gvproddetailedit.Rows[rowIndex].FindControl("hf1");
                HiddenField hfn1 = (HiddenField)gvproddetailedit.Rows[rowIndex].FindControl("HiddenField1");
                HiddenField hfn2 = (HiddenField)gvproddetailedit.Rows[rowIndex].FindControl("HiddenField2");
                HiddenField hfn3 = (HiddenField)gvproddetailedit.Rows[rowIndex].FindControl("HiddenField4");
                txtname.Text = row.Cells[1].Text;
                DataTable DtDropDownNatoGroup = new DataTable();
                DtDropDownNatoGroup = Lo.RetriveMasterSubCategoryDate(0, "NSN GROUP", "", "SelectProductCat", "", "");
                if (DtDropDownNatoGroup.Rows.Count > 0)
                {
                    Co.FillDropdownlist(ddlnatogroup, DtDropDownNatoGroup, "SCategoryName", "SCategoryID");
                    ddlnatogroup.Items.Insert(0, "Select");
                }
                ddlnatogroup.SelectedValue = hfn1.Value;
                ddlnatogroup_SelectedIndexChanged1(sender, e);
                ddlnatoclassedit.SelectedValue = hfn2.Value;
                ddlnatoclassedit_SelectedIndexChanged(sender, e);
                ddlitemcodeedit.SelectedValue = hfn3.Value;
                txthsnedit.Text = row.Cells[5].Text;
                ViewState["edit1"] = hfn.Value;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "divmodal1", "showPopup();", true);
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
            }
        }
        else if (e.CommandName == "deletenewmfe")
        {
            Int32 Delid = Lo.DeleteProdDet(Convert.ToInt32(e.CommandArgument.ToString()));
            if (Delid != 0)
            {
                LoadP();
                lblmsg.Text = "Record deleted successfully";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
            }
            else
            {
                lblmsg.Text = "Record not deleted.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
            }
        }
    }
    protected void gvtechnologyedit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "addtechedit")
        {
            lbsub2.Text = "Submit";
            txtnomentech.Text = "";
            DataTable DtTechDrop = new DataTable();
            DtTechDrop = Lo.RetriveMasterSubCategoryDate(0, "PRODUCT (INDUSTRY DOMAIN)", "", "SelectProductCat", "", "");
            if (DtTechDrop.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddltech1edit, DtTechDrop, "SCategoryName", "SCategoryID");
                ddltech1edit.Items.Insert(0, "Select");
            }
            ddltech1edit.SelectedIndex = -1;
            ViewState["edit2"] = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divmodal2", "showPopup1();", true);
        }
        else if (e.CommandName == "updttechedit")
        {
            lbsub2.Text = "Edit & Update";
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvtechnologyedit.Rows[rowIndex];
            HiddenField hfn = (HiddenField)gvtechnologyedit.Rows[rowIndex].FindControl("hf2");
            HiddenField hf1tech = (HiddenField)gvtechnologyedit.Rows[rowIndex].FindControl("hf1tech");
            HiddenField hf2tech = (HiddenField)gvtechnologyedit.Rows[rowIndex].FindControl("hf2tech");
            txtnomentech.Text = row.Cells[1].Text;
            DataTable DtTechDrop = new DataTable();
            DtTechDrop = Lo.RetriveMasterSubCategoryDate(0, "PRODUCT (INDUSTRY DOMAIN)", "", "SelectProductCat", "", "");
            if (DtTechDrop.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddltech1edit, DtTechDrop, "SCategoryName", "SCategoryID");
                ddltech1edit.Items.Insert(0, "Select");
            }
            ddltech1edit.SelectedValue = hf1tech.Value;
            ddltech1edit_SelectedIndexChanged(sender, e);
            ddltech2edit.SelectedValue = hf2tech.Value;
            //ddltech3edit.SelectedValue = row.Cells[4].Text;
            ViewState["edit2"] = hfn.Value;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divmodal2", "showPopup1();", true);
        }
        else if (e.CommandName == "deleedittech")
        {
            Int32 Delid = Lo.DeleteTechdet(Convert.ToInt32(e.CommandArgument.ToString()));
            if (Delid != 0)
            {
                LoadP();
                lblmsg.Text = "Record deleted successfully";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
            }
            else
            {
                lblmsg.Text = "Record not deleted successfully";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
            }
        }
    }
    protected void gvSourceofRawMaterialedit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "addsrmedit")
        {
            lb3.Text = "Submit";
            txtitemsedit.Text = "";
            txtbasicedit.Text = "";
            ddlsourceedit.SelectedIndex = -1;
            txtmajorname.Text = "";
            ViewState["edit3"] = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divmodal3", "showPopup2();", true);
        }
        else if (e.CommandName == "updateaddsrmedit")
        {
            lb3.Text = "Edit & Update";
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvSourceofRawMaterialedit.Rows[rowIndex];
            HiddenField hfn = (HiddenField)gvSourceofRawMaterialedit.Rows[rowIndex].FindControl("hf3");
            txtitemsedit.Text = row.Cells[1].Text;
            txtbasicedit.Text = row.Cells[2].Text;
            ddlsourceedit.Text = row.Cells[3].Text;
            txtmajorname.Text = row.Cells[4].Text;
            ViewState["edit3"] = hfn.Value;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divmodal3", "showPopup2();", true);
        }
        else if (e.CommandName == "deleaddsrmedit")
        {
            Int32 Delid = Lo.DeleteRawId(Convert.ToInt32(e.CommandArgument.ToString()));
            if (Delid != 0)
            {
                LoadP();
                lblmsg.Text = "Record deleted successfully";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
            }
            else
            {
                lblmsg.Text = "Record not deleted successfully";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
            }
        }

    }
    protected void gvItemProducedandSuppliededit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "additemnew")
        {
            lb4.Text = "Submit";
            txtrepcustedit.Text = "";
            txtstoreedit.Text = "";
            txtsnoedit.Text = "";
            txtorderqtyedit.Text = "";
            txtvalueqtyedit.Text = "";
            txtlastsuppedit.Text = "";
            ViewState["edit4"] = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divmodal4", "showPopup3();", true);
        }
        else if (e.CommandName == "updateitemedit")
        {
            lb4.Text = "Edit & Update";
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvItemProducedandSuppliededit.Rows[rowIndex];
            HiddenField hfn = (HiddenField)gvItemProducedandSuppliededit.Rows[rowIndex].FindControl("hf4");
            txtrepcustedit.Text = row.Cells[1].Text;
            txtstoreedit.Text = row.Cells[2].Text;
            txtsnoedit.Text = row.Cells[3].Text;
            txtorderqtyedit.Text = row.Cells[4].Text;
            txtvalueqtyedit.Text = row.Cells[5].Text;
            txtlastsuppedit.Text = row.Cells[6].Text;
            ViewState["edit4"] = hfn.Value;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divmodal4", "showPopup3();", true);
        }
        else if (e.CommandName == "deleitemedit")
        {
            Int32 Delid = Lo.DeleteProdSupply(Convert.ToInt32(e.CommandArgument.ToString()));
            if (Delid != 0)
            {
                LoadP();
                lblmsg.Text = "Record deleted successfully";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
            }
            else
            {
                lblmsg.Text = "Record not deleted successfully";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
            }
        }
    }
    protected void gvItemSuppliedbutnotproducededit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "editsuppprodedit")
        {
            lb5.Text = "Submit";
            txtrepcustedit1.Text = "";
            txtstoredit1.Text = "";
            txtdateedit1.Text = "";
            txtirderedit1.Text = "";
            txtqtysubedit1.Text = "";
            txtdatesupedit1.Text = "";
            ViewState["edit5"] = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divmodal5", "showPopup4();", true);
        }
        else if (e.CommandName == "updtsuppedit")
        {
            lb5.Text = "Edit & Update";
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvItemSuppliedbutnotproducededit.Rows[rowIndex];
            HiddenField hfn = (HiddenField)gvItemSuppliedbutnotproducededit.Rows[rowIndex].FindControl("hf5");
            txtrepcustedit1.Text = row.Cells[1].Text;
            txtstoredit1.Text = row.Cells[2].Text;
            txtdateedit1.Text = row.Cells[3].Text;
            txtirderedit1.Text = row.Cells[4].Text;
            txtqtysubedit1.Text = row.Cells[5].Text;
            txtdatesupedit1.Text = row.Cells[6].Text;
            ViewState["edit5"] = hfn.Value;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "divmodal5", "showPopup4();", true);
        }
        else if (e.CommandName == "deleedit")
        {
            Int32 Delid = Lo.DeleteProdSupply(Convert.ToInt32(e.CommandArgument.ToString()));
            if (Delid != 0)
            {
                LoadP();
                lblmsg.Text = "Record deleted successfully";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
            }
            else
            {
                lblmsg.Text = "Record not deleted successfully";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
            }
        }
    }
    #endregion
    #region popupbuttoncode
    protected void lbsub_Click(object sender, EventArgs e)
    {
        try
        {
            if (lbsub.Text == "Edit & Update")
            {
                if (txtname.Text != "")
                {
                    Int32 ESaveID = Lo.UpdateProducts(Convert.ToInt64(ViewState["edit1"]), Enc.DecryptData(Session["VendorRefNo"].ToString()), txtname.Text, ddlnatogroup.Text, ddlnatoclassedit.Text, ddlitemcodeedit.Text, txthsnedit.Text);
                    if (ESaveID != 0)
                    {
                        txtname.Text = "";
                        ddlnatogroup.SelectedIndex = -1;
                        ddlnatoclassedit.SelectedIndex = -1;
                        ddlitemcodeedit.SelectedIndex = -1;
                        txthsnedit.Text = "";
                        ViewState["edit1"] = null;
                        lblmsg.Text = "Record update successfully";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#divmodal1').modal('hide')", true);
                        LoadP();
                    }
                    else
                    {
                        lblmsg.Text = "Record not update successfully";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
                    }
                }
            }
            else if (lbsub.Text == "Submit")
            {
                Int32 ESaveID = Lo.InsertProducts(Enc.DecryptData(Session["VendorRefNo"].ToString()), txtname.Text, ddlnatogroup.Text, ddlnatoclassedit.Text, ddlitemcodeedit.Text, txthsnedit.Text);
                if (ESaveID != 0)
                {
                    txtname.Text = "";
                    ddlnatogroup.SelectedIndex = -1;
                    ddlnatoclassedit.SelectedIndex = -1;
                    ddlitemcodeedit.SelectedIndex = -1;
                    txthsnedit.Text = "";
                    lblmsg.Text = "Record save successfully";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#divmodal1').modal('hide')", true);
                    LoadP();
                }
                else
                {
                    lblmsg.Text = "Record not save successfully";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
                }
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
        }
    }
    protected void lbsub2_Click(object sender, EventArgs e)
    {
        try
        {
            if (lbsub2.Text == "Edit & Update")
            {
                if (txtnomentech.Text != "")
                {
                    Int32 ESaveID = Lo.UpdateTechnology(Convert.ToInt64(ViewState["edit2"]), Enc.DecryptData(Session["VendorRefNo"].ToString()), txtnomentech.Text, ddltech1edit.SelectedItem.Value, ddltech2edit.SelectedItem.Value);
                    if (ESaveID != 0)
                    {
                        txtnomentech.Text = "";
                        ddltech1edit.SelectedIndex = -1;
                        ddltech2edit.SelectedIndex = -1;
                        ViewState["edit2"] = null;
                        LoadP();
                        lblmsg.Text = "Record update successfully";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#divmodal2').modal('hide')", true);
                    }
                    else
                    {
                        lblmsg.Text = "Record not update successfully";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#divmodal2').modal('hide')", true);
                    }
                }
            }
            else if (lbsub2.Text == "Submit")
            {
                Int32 ESaveID = Lo.InsertTechnology(Enc.DecryptData(Session["VendorRefNo"].ToString()), txtnomentech.Text, ddltech1edit.Text, ddltech2edit.Text);
                if (ESaveID != 0)
                {
                    txtnomentech.Text = "";
                    ddltech1edit.SelectedIndex = -1;
                    ddltech2edit.SelectedIndex = -1;
                    LoadP();
                    lblmsg.Text = "Record save successfully";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#divmodal2').modal('hide')", true);
                }
                else
                {
                    lblmsg.Text = "Record not save successfully";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#divmodal2').modal('hide')", true);
                }
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.Message;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#divmodal2').modal('hide')", true);
        }
    }
    protected void lb3_Click(object sender, EventArgs e)
    {
        try
        {
            if (lb3.Text == "Edit & Update")
            {
                if (txtitemsedit.Text != "")
                {
                    Int32 ESaveID = Lo.UpdateRaw(Convert.ToInt64(ViewState["edit3"]), Enc.DecryptData(Session["VendorRefNo"].ToString()), txtitemsedit.Text, txtbasicedit.Text, ddlsourceedit.SelectedItem.Value, txtmajorname.Text);
                    if (ESaveID != 0)
                    {
                        txtitemsedit.Text = "";
                        txtbasicedit.Text = "";
                        ddlsourceedit.SelectedIndex = -1;
                        txtmajorname.Text = "";
                        ViewState["edit3"] = null;
                        LoadP();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#divmodal3').modal('hide')", true);
                        lblmsg.Text = "Record update successfully";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#divmodal3').modal('hide')", true);
                        lblmsg.Text = "Record not update successfully";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
                    }
                }
            }
            else if (lb3.Text == "Submit")
            {
                Int32 ESaveID = Lo.InsertRaw(Enc.DecryptData(Session["VendorRefNo"].ToString()), txtitemsedit.Text, txtbasicedit.Text, ddlsourceedit.SelectedItem.Value, txtmajorname.Text);
                if (ESaveID != 0)
                {
                    txtitemsedit.Text = "";
                    txtbasicedit.Text = "";
                    ddlsourceedit.SelectedIndex = -1;
                    txtmajorname.Text = "";
                    LoadP();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#divmodal3').modal('hide')", true);
                    lblmsg.Text = "Record save successfully";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#divmodal3').modal('hide')", true);
                    lblmsg.Text = "Record not save successfully";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#divmodal3').modal('hide')", true);
            lblmsg.Text = ex.Message;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
        }
    }
    protected void lb4_Click(object sender, EventArgs e)
    {
        try
        {
            if (lb4.Text == "Edit & Update")
            {
                if (txtrepcustedit.Text != "")
                {
                    DateTime mdatee = Convert.ToDateTime(txtlastsuppedit.Text);
                    string mdateef = mdatee.ToString("yyyy-MMM-dd");
                    Int32 ESaveID = Lo.Updateproducedprod(Convert.ToInt64(ViewState["edit4"]), Enc.DecryptData(Session["VendorRefNo"].ToString()),
                        txtrepcustedit.Text, txtstoreedit.Text, txtsnoedit.Text, txtorderqtyedit.Text, txtvalueqtyedit.Text, mdateef.ToString());
                    if (ESaveID != 0)
                    {
                        txtrepcustedit.Text = ""; txtstoreedit.Text = ""; txtsnoedit.Text = ""; txtorderqtyedit.Text = ""; txtvalueqtyedit.Text = "";
                        txtlastsuppedit.Text = "";
                        ViewState["edit4"] = null;
                        LoadP();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#divmodal4').modal('hide')", true);
                        lblmsg.Text = "Record update successfully";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#divmodal4').modal('hide')", true);
                        lblmsg.Text = "Record not update successfully";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
                    }
                }
            }
            else if (lb4.Text == "Submit")
            {
                DateTime mdatee1 = Convert.ToDateTime(txtlastsuppedit.Text);
                string mdateef1 = mdatee1.ToString("yyyy-MMM-dd");
                Int32 ESaveID = Lo.Insertproducedprod(Enc.DecryptData(Session["VendorRefNo"].ToString()), "SupplyYes", txtrepcustedit.Text, txtstoreedit.Text, txtsnoedit.Text, txtorderqtyedit.Text, txtvalueqtyedit.Text, mdateef1.ToString());
                if (ESaveID != 0)
                {
                    txtrepcustedit.Text = ""; txtstoreedit.Text = ""; txtsnoedit.Text = ""; txtorderqtyedit.Text = ""; txtvalueqtyedit.Text = "";
                    txtlastsuppedit.Text = "";
                    LoadP(); ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#divmodal4').modal('hide')", true);
                    lblmsg.Text = "Record save successfully";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#divmodal4').modal('hide')", true);
                    lblmsg.Text = "Record not save successfully";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#divmodal4').modal('hide')", true);
            lblmsg.Text = ex.Message;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
        }
    }
    protected void lb5_Click(object sender, EventArgs e)
    {
        try
        {
            if (lb5.Text == "Edit & Update")
            {
                DateTime mdatee11 = Convert.ToDateTime(txtdatesupedit1.Text);
                string mdateef11 = mdatee11.ToString("yyyy-MMM-dd");
                if (txtrepcustedit1.Text != "")
                {
                    Int32 ESaveID = Lo.Updateproducedprod(Convert.ToInt64(ViewState["edit5"]), Enc.DecryptData(Session["VendorRefNo"].ToString()), txtrepcustedit1.Text, txtstoredit1.Text, txtdateedit1.Text, txtirderedit1.Text, txtqtysubedit1.Text, mdateef11.ToString());
                    if (ESaveID != 0)
                    {
                        txtrepcustedit1.Text = ""; txtstoredit1.Text = ""; txtdateedit1.Text = ""; txtirderedit1.Text = ""; txtqtysubedit1.Text = "";
                        txtdatesupedit1.Text = "";
                        ViewState["edit5"] = null;
                        LoadP(); 
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#divmodal5').modal('hide')", true);
                        lblmsg.Text = "Record update successfully";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#divmodal5').modal('hide')", true);
                        lblmsg.Text = "Record not update successfully";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
                    }
                }
            }
            else if (lb5.Text == "Submit")
            {
                DateTime mdatee111 = Convert.ToDateTime(txtdatesupedit1.Text);
                string mdateef111 = mdatee111.ToString("yyyy-MMM-dd");
                Int32 ESaveID = Lo.Insertproducedprod(Enc.DecryptData(Session["VendorRefNo"].ToString()), "SupplyNo", txtrepcustedit1.Text, txtstoredit1.Text, txtdateedit1.Text, txtirderedit1.Text, txtqtysubedit1.Text, mdateef111.ToString());
                if (ESaveID != 0)
                {
                    txtrepcustedit1.Text = ""; txtstoredit1.Text = ""; txtdateedit1.Text = ""; txtirderedit1.Text = ""; txtqtysubedit1.Text = "";
                    txtdatesupedit1.Text = "";
                    LoadP(); ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#divmodal5').modal('hide')", true);
                    lblmsg.Text = "Record save successfully";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#divmodal5').modal('hide')", true);
                    lblmsg.Text = "Record not save successfully";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "$('#divmodal5').modal('hide')", true);
            lblmsg.Text = ex.Message;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "modelmsg", "showPopup5();", true);
        }
    }
    #endregion   
    #region autocomplete region
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod()]
    public static string[] GetCustomers(string prefix)
    {
        Cryptography objCrypto1 = new Cryptography();
        List<string> customers = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = objCrypto1.DecryptData(ConfigurationManager.ConnectionStrings["connectiondb"].ConnectionString);
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct top (100) ProductDescription from tbl_mst_MainProduct where ProductDescription like '%" + prefix + "%' ";
                cmd.Parameters.AddWithValue("@SearchText", prefix);
                cmd.Connection = conn;
                conn.Open();
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
        return customers.ToArray();
    }
    #endregion
}