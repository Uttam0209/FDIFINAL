using System;
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

public partial class Vendor_V_DetailofDefence : System.Web.UI.Page
{
    #region Variable
    Logic Lo = new Logic();
    Cryptography Enc = new Cryptography();
    DataUtility Co = new DataUtility();
    HybridDictionary HySaveVendorRegisdetail = new HybridDictionary();
    private string _msg = string.Empty;
    private string _sysMsg = string.Empty;
    Int64 Mid = 0;
    #endregion
    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["User"] != null)
        {
            if (!IsPostBack)
            {

                DataTable DtCheckSavedetails = Lo.RetriveVendor(0, Enc.DecryptData(Session["VendorRefNo"].ToString()), "", "CheckRegis");
                if (DtCheckSavedetails.Rows.Count > 0)
                {

                    btnsubmit.Text = "Update";
                    ViewState["Mid"] = Convert.ToInt64(DtCheckSavedetails.Rows[0]["VendorDetailID"].ToString());
                    DataTable dtcheckmultigriddata = Lo.RetriveVendor(0, Enc.DecryptData(Session["VendorRefNo"].ToString()), "", "RetriveMultigrid");
                    if (dtcheckmultigriddata.Rows.Count > 0)
                    {
                        DataView dv = new DataView(dtcheckmultigriddata);
                        dv.RowFilter = "Type='ProductsDetails'";
                        if (dv.Count > 0)
                        {
                            gvproddetailedit.DataSource = dv;
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
                        dv.RowFilter = "Type='TechnologyDetails'";
                        if (dv.Count > 0)
                        {
                            gvtechnologyedit.DataSource = dv;
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
                        dv.RowFilter = "Type='SourceRawMaterial'";
                        if (dv.Count > 0)
                        {
                            gvSourceofRawMaterialedit.DataSource = dv;
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
                        dv.RowFilter = "Type='ItemProducedSupplied'";
                        if (dv.Count > 0)
                        {
                            gvItemProducedandSuppliededit.DataSource = dv;
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
                        dv.RowFilter = "Type='ItemProducedNotSupplied'";
                        if (dv.Count > 0)
                        {
                            gvItemSuppliedbutnotproducededit.DataSource = dv;
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
                }
                else
                {
                    btnsubmit.Text = "Save";

                    SetInitialRowProductDetails();
                    SetInitialRowTechnologyDetails();
                    SetRawmeterialDetails();
                    SetInitialRowItemProductorSupplied();
                    SetInitialRowItemProductorSupplied1();

                }
            }

        }
        else
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert",
                   "alert('Session Expired,Please login again');window.location='VendorLogin'", true);
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
    //Add Grid of Products Details
    #region  Grid of Products Details
    private void SetInitialRowProductDetails()
    {
        //Create false table
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
        //Store the DataTable in ViewState or bind or show false grid
        ViewState["ProductsDetails"] = dtProdDetail;
        gvproddetail.DataSource = dtProdDetail;
        gvproddetail.DataBind();
        //After binding the gridview, we can then extract and fill the DropDownList with Data 
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
                ViewState["ProductsDetails"] = dtCurrentTableProd;
                gvproddetail.DataSource = dtCurrentTableProd;
                gvproddetail.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousDataProductDetail();
    }
    private void SetPreviousDataProductDetail()
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
                    DropDownList DDL_1 = (DropDownList)gvproddetail.Rows[rowIndexProd].Cells[2].FindControl("ddlnatogroup");
                    DropDownList DDL_2 = (DropDownList)gvproddetail.Rows[rowIndexProd].Cells[3].FindControl("ddlnatoclass");
                    DropDownList DDL_3 = (DropDownList)gvproddetail.Rows[rowIndexProd].Cells[4].FindControl("ddlitemcode");
                    TextBox TB_2 = (TextBox)gvproddetail.Rows[rowIndexProd].Cells[5].FindControl("txthsnno");
                    TB_1.Text = dtProdPrevious.Rows[i]["Nomenclature"].ToString();
                    TB_2.Text = dtProdPrevious.Rows[i]["HSNCode"].ToString();
                    BindNatoGroup(DDL_1);
                    if (i < dtProdPrevious.Rows.Count - 1)
                    {
                        DDL_1.ClearSelection();
                        DDL_1.Items.FindByValue(dtProdPrevious.Rows[i]["NatoGroup"].ToString()).Selected = true;
                        DDL_2.ClearSelection();
                        BindNatoClassSubCategoryofNatoGroup(DDL_1, DDL_2);
                        if (DDL_2.Text != "")
                        {
                            DDL_2.Items.FindByValue(dtProdPrevious.Rows[i]["NatoClass"].ToString()).Selected = true;
                        }
                        DDL_3.ClearSelection();
                        BindItemClassSubCategoryofNatoClass(DDL_2, DDL_3);
                        if (DDL_3.Text != "")
                        {
                            DDL_3.Items.FindByValue(dtProdPrevious.Rows[i]["ItemCode"].ToString()).Selected = true;
                        }
                    }
                    rowIndexProd++;
                }
            }
        }
    }
    protected void btnProductDetailAddMore_Click(object sender, EventArgs e)
    {
        AddNewProductDetailGrid();
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
    protected void txttech2_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gvr = (GridViewRow)(((Control)sender).NamingContainer);
        int rowIndex = gvr.RowIndex;
        DropDownList ddltech2 = gvtechnology.Rows[rowIndex].FindControl("ddltech2") as DropDownList;
        DropDownList ddltech3 = gvtechnology.Rows[rowIndex].FindControl("ddltech3") as DropDownList;
        BindMasterSubTech3(ddltech2, ddltech3);
    }
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
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousDataTechnologyDetail();
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
                    DropDownList DDLtech_2 = (DropDownList)gvtechnology.Rows[rowIndexTech].Cells[3].FindControl("ddltech1");
                    DropDownList DDLtech_3 = (DropDownList)gvtechnology.Rows[rowIndexTech].Cells[4].FindControl("ddltech1");
                    TBtech_1.Text = dtTechPrevious.Rows[i]["TechNomenclature"].ToString();
                    BindMasterTechnologyCategory(DDLtech_1);
                    if (i < dtTechPrevious.Rows.Count - 1)
                    {
                        DDLtech_1.ClearSelection();
                        DDLtech_1.Items.FindByValue(dtTechPrevious.Rows[i]["Technology1"].ToString()).Selected = true;
                        DDLtech_2.ClearSelection();
                        BindMasterTechnologySubCat(DDLtech_1, DDLtech_2);
                        if (DDLtech_2.Text != "Select" && DDLtech_2.Text != "")
                        {
                            DDLtech_2.Items.FindByValue(dtTechPrevious.Rows[i]["Technology2"].ToString()).Selected = true;
                        }
                        DDLtech_3.ClearSelection();
                        BindMasterSubTech3(DDLtech_2, DDLtech_3);
                        if (DDLtech_3.Text != "Select" && DDLtech_3.Text != "")
                        {
                            DDLtech_3.Items.FindByValue(dtTechPrevious.Rows[i]["Technology3"].ToString()).Selected = true;
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
            DropDownList DDLtech_2 = (DropDownList)gvtechnology.Rows[i].Cells[3].FindControl("ddltech1");
            DropDownList DDLtech_3 = (DropDownList)gvtechnology.Rows[i].Cells[4].FindControl("ddltech1");
            if (DDLtech_1.SelectedItem.Text != "Select" && TBtech_1.Text != "")
            {
                drCurrentRowSaveCode = DtSaveTech.NewRow();
                drCurrentRowSaveCode["RowNumberTech"] = i + 1;
                drCurrentRowSaveCode["TechNomenclature"] = TBtech_1.Text;
                drCurrentRowSaveCode["Technology1"] = DDLtech_1.SelectedItem.Value;
                drCurrentRowSaveCode["Technology2"] = DDLtech_2.SelectedItem.Value;
                drCurrentRowSaveCode["Technology3"] = DDLtech_3.SelectedItem.Value;
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
                    //    dtCurrentRawMeterialDetails.Rows[i - 1]["Items"] = TextBox1.Text;
                    //    dtCurrentRawMeterialDetails.Rows[i - 1]["RawMeterial"] = TextBox2.Text;
                    //    dtCurrentRawMeterialDetails.Rows[i - 1]["SourceMeterial"] = ddl3_Raw.Text;
                    //    dtCurrentRawMeterialDetails.Rows[i - 1]["MeterailSupplier"] = TextBox4.Text;
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
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousDataItemProductorSupplied();
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
                drCurrentRowSaveCode["DateofLastSupp"] = TextBox_6.Text;
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
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousDataItemProductorSupplied1();
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
                drCurrentRowSaveCode["DateofLastSupp1"] = TextBox_6.Text;
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Successfully update company information')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Successfully save company information')", true);
            }
        }
        else
        { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Record not saved.')", true); }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        SaveRegistration();
    }
    #endregion
}