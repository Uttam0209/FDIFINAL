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
        public string VerifyEmployee(HybridDictionary hyLogin, out string _msg, out string Defaultpage)
        {
            return SqlHelper.Instance.VerifyEmployee(hyLogin, out _msg, out Defaultpage);
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
        public string SaveUploadExcelCompany(DataTable dtMaster,DataTable dtExcel)
        {
            return SqlHelper.Instance.SaveUploadExcelCompany(dtMaster, dtExcel);
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
        public string SaveCodeProduct(HybridDictionary hyProduct, DataTable DtImage, out string _sysMsg, out string _msg, string Criteria)
        {
            return SqlHelper.Instance.SaveCodeProduct(hyProduct, DtImage, out _sysMsg, out _msg, Criteria);
        }
        public string SaveCompDesignation(HybridDictionary hysavecomp, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveCompDesignation(hysavecomp, out _sysMsg, out _msg);
        }
        #endregion
        #region UpdateCode
        public string UpdateLoginPassword(string NewPass, string OldPass, string User, string type)
        {
            return SqlHelper.Instance.UpdateLoginPassword(NewPass, OldPass, User, type);
        }

        #endregion
        #region retriveCode
        public DataTable RetriveGridView(string ID)
        {
            return SqlHelper.Instance.RetriveGridView(ID);
        }
        public DataTable RetriveCountry(string text)
        {
            return SqlHelper.Instance.RetriveCountry(text);
        }
        public DataTable RetriveState(string text)
        {
            return SqlHelper.Instance.RetriveState(text);
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
        public DataTable GetDashboardData(string Purpose)
        {
            return SqlHelper.Instance.GetDashboardData(Purpose);
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
        public DataTable RetriveAggregateValueWithParam(string function, string entity, string clmn, string val)
        {
            return SqlHelper.Instance.RetriveAggregateValueWithParam(function, entity, clmn, val);
        }
        public DataTable GetChartData()
        {
            return SqlHelper.Instance.GetDataTable("SELECT CompanyName, FY, ExchangeTotalAmount from vw_Chart order by fyid");
        }
        #endregion
    }
}
