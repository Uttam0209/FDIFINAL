using System;
using System.Collections.Specialized;
using DataAccessLayer;
using System.Data;

namespace BusinessLayer
{
    public class Logic
    {
        #region "Variables"
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        #endregion
        public DataSet FDI()
        {
            return ds;
        }
        #region Login"
        public string VerifyEmail(HybridDictionary hyLogin, out string _msg, out string mp)
        {
            return SqlHelper.Instance.VerifyEmail(hyLogin, out _msg, out  mp);
        }
        public string VerifyEmployee(HybridDictionary hyLogin, out string _msg, out string Defaultpage)
        {
            return SqlHelper.Instance.VerifyEmployee(hyLogin, out _msg, out Defaultpage);
        }
        public string VerifyVendorEmployee(HybridDictionary hyLogin, out string _msg, out string Defaultpage)
        {
            return SqlHelper.Instance.VerifyVendorEmployee(hyLogin, out _msg, out Defaultpage);
        }
        #endregion
        #region Email and Company Name"
        public string VerifyEmailandCompany(string strEmail, string strCompany, out string _msg)
        {
            return SqlHelper.Instance.VerifyEmailandCompany(strEmail, strCompany, out _msg);
        }
        #endregion
        public DataTable CreateExcelConnection(string FilePath, string SheetName, out string text)
        {
            DataTable dt = SqlHelper.Instance.CreateExcelConnection(FilePath, SheetName, out text);
            return dt;
        }
        public string SaveUploadExcelCompany(DataTable dtMaster, DataTable dtExcel)
        {
            return SqlHelper.Instance.SaveUploadExcelCompany(dtMaster, dtExcel);
        }
        public string SaveExcel3510(DataTable dtMaster, string l1, string l2, string pid)
        {
            return SqlHelper.Instance.SaveExcel3510(dtMaster, l1, l2, pid);
        }
        public string SaveExcelProduct(DataTable dtMaster)
        {
            return SqlHelper.Instance.SaveExcelProduct(dtMaster);
        }
        #region SaveCode
        public string SaveFDI(HybridDictionary HySave, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveFDI(HySave, out _sysMsg, out _msg);
        }
        public string SaveMasterCompany(HybridDictionary HyCompSave, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveMasterCompany(HyCompSave, out _sysMsg, out _msg);
        }
        public string SaveMasterDivision(HybridDictionary hysaveDivision, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveMasterDivision(hysaveDivision, out _sysMsg, out _msg);
        }
        public string SaveMasterUnit(HybridDictionary hysaveUnit, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveMasterUnit(hysaveUnit, out _sysMsg, out _msg);
        }
        public string SaveMasterComp(HybridDictionary hysavecomp, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveMasterComp(hysavecomp, out _sysMsg, out _msg);
        }
        public string SaveFactoryComp(HybridDictionary hysavecomp, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveFactoryComp(hysavecomp, out _sysMsg, out _msg);
        }
        public string SaveUnitComp(HybridDictionary hysaveunit, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveUnitComp(hysaveunit, out _sysMsg, out _msg);
        }
        public string SaveMasterCategroyMenu(HybridDictionary hyMasterCategory, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveMasterCategroyMenu(hyMasterCategory, out _sysMsg, out _msg);
        }
        public string SaveMasterNodal(HybridDictionary hySaveNodal, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveMasterNodal(hySaveNodal, out _sysMsg, out _msg);
        }
        public string SaveCodeProduct(HybridDictionary hyProduct, DataTable DtImage, DataTable dtProdInfo, DataTable dtEstimateQuantity, out string _sysMsg, out string _msg, string Criteria)
        {
            return SqlHelper.Instance.SaveCodeProduct(hyProduct, DtImage, dtProdInfo, dtEstimateQuantity, out _sysMsg, out _msg, Criteria);
        }
        public string SaveCompDesignation(HybridDictionary hysavecomp, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveCompDesignation(hysavecomp, out _sysMsg, out _msg);
        }
        #endregion
        #region UpdateCode
        public string UpdateLoginPassword(string NewPass, string OldPass, string User, string type, string MzefPass, string Salt)
        {
            return SqlHelper.Instance.UpdateLoginPassword(NewPass, OldPass, User, type, MzefPass, Salt);
        }

        #endregion
        #region retriveCode
        public DataTable RetriveGridView(string ID)
        {
            return SqlHelper.Instance.RetriveGridView(ID);
        }
        public DataTable RetriveCountry(Int64 CountryID, string text)
        {
            return SqlHelper.Instance.RetriveCountry(CountryID, text);
        }
        public DataTable RetriveState(string text)
        {
            return SqlHelper.Instance.RetriveState(text);
        }

        public DataTable RetriveProductIndig()
        {
            return SqlHelper.Instance.GetExecuteData("select * from fn_companywiseproduct() order by compName");
        }

        public DataTable RetriveAllCompany(string UnitRefNo, string Role)
        {
            return SqlHelper.Instance.RetriveAllCompany(UnitRefNo, Role);
        }
        public DataTable RetriveAllNodalOfficer(string UnitRefNo, string Role)
        {
            return SqlHelper.Instance.RetriveAllNodalOfficer(UnitRefNo, Role);
        }
        public DataTable RetriveGridViewCompany(string ID, string FactoryRefNo, string UnitRefNo, string Purpose)
        {
            return SqlHelper.Instance.RetriveGridViewCompany(ID, FactoryRefNo, UnitRefNo, Purpose);
        }
        public DataTable GetDashboardData(string Purpose, string Search)
        {
            return SqlHelper.Instance.GetDashboardData(Purpose, Search);
        }
        public DataTable RetriveProductCode(string CompanyRefNo, string ProductRefNo, string Purpose, string Type)
        {
            return SqlHelper.Instance.RetriveProductCode(CompanyRefNo, ProductRefNo, Purpose, Type);
        }
        public DataTable RetriveMasterData(Int64 Companyid, string strRefNo, string strRole, int MenuId, string strMenuUrl, string strInterestedAreaFlag, string strCriteria)
        {
            return SqlHelper.Instance.RetriveMasterData(Companyid, strRefNo, strRole, MenuId, strMenuUrl, strInterestedAreaFlag, strCriteria);
        }
        public DataTable RetriveMasterCategoryDate(Int64 CatID, string CatName, string SCatValue, string Flag, string Active, string Criteria, string CreatedBy)
        {
            return SqlHelper.Instance.RetriveMasterCategoryDate(CatID, CatName, SCatValue, Flag, Active, Criteria, CreatedBy);
        }
        public DataTable RetriveMasterSubCategoryDate(Int64 SCatID, string SCatName, string PId, string Criteria, string CompRefNo, string CreatedBy)
        {
            return SqlHelper.Instance.RetriveMasterSubCategoryDate(SCatID, SCatName, PId, Criteria, CompRefNo, CreatedBy);
        }
        public DataTable RetriveIntresteData(string CompRefNo)
        {
            string query = "select * from fn_GetInterestedInValue('" + CompRefNo + "')";
            return SqlHelper.Instance.GetDataset(query).Tables[0];
            //return SqlHelper.Instance.RetriveIntresteData(CompRefNo);
        }
        public DataTable RetriveForgotPasswordEmail(string Email, string Type)
        {
            return SqlHelper.Instance.RetriveForgotPasswordEmail(Email, Type);
        }

        #endregion
        #region DeleteCode
        public string DeleteRecord(string CompRefNo, string Criteria)
        {
            return SqlHelper.Instance.DeleteRecord(CompRefNo, Criteria);
        }
        #endregion
        #region SearchCode
        public DataTable SearchResult(string SearchText)
        {
            return SqlHelper.Instance.SearchResult(SearchText);
        }
        public DataTable SearchResultCompany(string SearchText)
        {
            return SqlHelper.Instance.SearchResultCompany(SearchText);
        }
        #endregion
        #region "DashBoard"
        public DataTable RetriveAggregateValue(string action, string role, string refno)
        {
            return SqlHelper.Instance.GetDataTable("select * from fn_GetAggregateValue('" + action + "','" + role + "','" + refno + "')");
        }
        public DataTable RetrivePid(string l1, string l2)
        {
            return SqlHelper.Instance.GetDataTable("select SCategoryId from tbl_mst_SubCategory where pid=(select SCategoryId from tbl_mst_subcategory where pid=0 and l1code='" + l1 + "') and L2Code='" + l2 + "'");
        }
        public DataTable RetriveParentNode(string role, string refno)
        {
            return SqlHelper.Instance.GetDataTable("select * from fn_ParentNode('" + role + "','" + refno + "')");
        }
        public DataTable RetriveAggregateValueWithParam(string function, string entity, string clmn, string val)
        {
            return SqlHelper.Instance.RetriveAggregateValueWithParam(function, entity, clmn, val);
        }
        public DataTable GetChartData()
        {
            return SqlHelper.Instance.GetDataTable("SELECT CompanyName, FY, ExchangeTotalAmount from vw_Chart order by fyid");
        }
        #endregion
        #region Test
        public DataTable TestGrid(string Function, string ProdRefNo, Int32 ProdInfoId, string Name, decimal Value, string Unit)
        {
            return SqlHelper.Instance.TestGrid(Function, ProdRefNo, ProdInfoId, Name, Value, Unit);
        }
        public DataTable RetriveSaveEstimateGrid(string Function, Int32 ProdInfoId, string ProdRefNo, Int32 Year, string FYear, decimal EstimateQuantity, string Unit, decimal Price)
        {
            return SqlHelper.Instance.RetriveSaveEstimateGrid(Function, ProdInfoId, ProdRefNo, Year, FYear, EstimateQuantity, Unit, Price);
        }

        #endregion
        ////////////////////////////////////////================================ Vendor Code=======================================//////////////////////////////////////
        #region Code for vendor  Save
        public string SaveVendorRegis(HybridDictionary hysavecomp, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveVendorRegis(hysavecomp, out _sysMsg, out _msg);
        }
        public string SaveVendorRegistrationDetails(HybridDictionary HySaveVendorRegisdetail, DataTable DtFristGrid, DataTable dt2, DataTable dt3, DataTable dt4, DataTable dt5, DataTable dt6, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveVendorRegistrationDetails(HySaveVendorRegisdetail, DtFristGrid, dt2, dt3, dt4, dt5, dt6, out _sysMsg, out _msg);
        }
        #endregion
        #region Code for vendor  Delete
        #endregion
        #region Code for vendor  Update
        #endregion
        #region Code for vendor  Retrive
        public DataTable RetriveVendor(Int64 Vid, string VRefNo, string VEmail, string RetFor)
        {
            return SqlHelper.Instance.RetriveVendor(Vid, VRefNo, VEmail, RetFor);
        }
        #endregion
    }
}
