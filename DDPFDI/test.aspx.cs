using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;

public partial class test : System.Web.UI.Page
{
    Logic Lo = new Logic();
    private DataUtility Co = new DataUtility();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindMasterCategory();
            BindMasterSubCategory();
            BindMaster3levelSubCategory();
        }
    }
    //For 3rd dropdown bind all list first
    protected void BindMasterCategory()
    {
        DataTable DtMasterCategroy = new DataTable();
        //if (ddlcompany.SelectedItem.Text != "Select")
        //{
        //    DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "NSN GROUP", "", "SelectProductCat", ddlcompany.SelectedItem.Value, "");
        //}
        //else
        //{
        DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "NSN GROUP", "", "SelectProductCat", "", "");
        // }
        if (DtMasterCategroy.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlmastercategory, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlmastercategory.Items.Insert(0, "Select");
        }
        else
        {
            DtMasterCategroy = Lo.RetriveMasterSubCategoryDate(0, "NSN GROUP", "", "SelectProductCat", "", "");
            Co.FillDropdownlist(ddlmastercategory, DtMasterCategroy, "SCategoryName", "SCategoryID");
            ddlmastercategory.Items.Insert(0, "Select");
        }
    }
    //bind second category indepent or with id
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
        }
        else
        {
            ddlsubcategory.Items.Clear();
            ddlsubcategory.Items.Insert(0, "Select");
        }
    }
    //bid fiest dropdown 3rd gen of cate
    protected void BindMaster3levelSubCategory()
    {
        DataTable DtMasterCategroyLevel3 = new DataTable();
        if (ddlsubcategory.SelectedItem.Value != null || ddlsubcategory.SelectedItem.Text != "Select")
        {
            if (ddlsubcategory.SelectedItem.Text != "Select")
            { DtMasterCategroyLevel3 = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlsubcategory.SelectedItem.Value), "", "", "SubSelectID", "", ""); }
            else
            { DtMasterCategroyLevel3 = Lo.RetriveMasterSubCategoryDate(0, "", "", "SubSelectthr", "", ""); }
            if (DtMasterCategroyLevel3.Rows.Count > 0)
            {
                Co.FillDropdownlist(ddllevel3product, DtMasterCategroyLevel3, "SCategoryName", "SCategoryId");
                ddllevel3product.Items.Insert(0, "Select");
            }
            else
            {
                ddllevel3product.Items.Clear();
                ddllevel3product.Items.Insert(0, "Select");
                ddllevel3product.Items.Insert(1, "NA");
            }
        }
    }
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
    }
    protected void ddllevel3product_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt1 = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddllevel3product.SelectedItem.Value), "", "", "3to2", "", "");
        if (dt1.Rows.Count > 0)
        {
            Co.FillDropdownlist(ddlsubcategory, dt1, "SCategoryName", "SCategoryId");
            ddlsubcategory.Items.Insert(0, "Select");
            DataTable dtbindvalue = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddllevel3product.SelectedItem.Value), "", "", "3to21", "", "");
            ddlsubcategory.SelectedValue = dtbindvalue.Rows[0]["SCategoryId"].ToString();
            DataTable dt1sr = Lo.RetriveMasterSubCategoryDate(Convert.ToInt32(ddlsubcategory.SelectedItem.Value), "", "", "3to21", "", "");
            ddlmastercategory.SelectedValue = dt1sr.Rows[0]["SCategoryId"].ToString();
        }
        else
        {
            ddllevel3product.SelectedValue = "Select";
        }
    }
}