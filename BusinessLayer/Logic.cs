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
        public string SaveLog(HybridDictionary hyLog, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveLog(hyLog, out _sysMsg, out _msg);
        }
        public string InsertLogProd(HybridDictionary hySaveProdInfo, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.InsertLogProd(hySaveProdInfo, out _sysMsg, out _msg);
        }
        public string SaveLogoutstatus(HybridDictionary hyLog, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveLogoutstatus(hyLog, out _sysMsg, out _msg);
        }
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
        public string UpdateCodeProduct(HybridDictionary HyUpdateProd, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.UpdateCodeProduct(HyUpdateProd, out _sysMsg, out _msg);
        }
        public string SaveCompDesignation(HybridDictionary hysavecomp, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveCompDesignation(hysavecomp, out _sysMsg, out _msg);
        }
        public string SaveImpNews(HybridDictionary hysavecomp, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveImpNews(hysavecomp, out _sysMsg, out _msg);
        }
        #endregion
        #region UpdateCode
        public string UpdateLoginPassword(string NewPass, string OldPass, string User, string type, string MzefPass, string Salt)
        {
            return SqlHelper.Instance.UpdateLoginPassword(NewPass, OldPass, User, type, MzefPass, Salt);
        }
        public string UpdateStatus(Int64 ID, string Value1, string Value2)
        {
            return SqlHelper.Instance.UpdateStatus(ID, Value1, Value2);
        }

        #endregion
        #region retriveCode
        public DataTable RetriveCount(string compref, string name)
        {
            return SqlHelper.Instance.GetExecuteData("select * from fn_Div_UnitWiseProduct('" + compref + "','" + name + "') order by Name");
        }
        public DataTable RetriveGridView(string ID)
        {
            return SqlHelper.Instance.RetriveGridView(ID);
        }
        public DataTable GetLogOutStatus(string UEID)
        {
            return SqlHelper.Instance.GetLogOutStatus(UEID);
        }
        public DataTable RetriveCountry(Int64 CountryID, string text)
        {
            return SqlHelper.Instance.RetriveCountry(CountryID, text);
        }
        public DataTable RetriveCountry(Int64 CountryID, string text, string codefor)
        {
            return SqlHelper.Instance.RetriveCountry(CountryID, text, codefor);
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
        public DataTable GetProductFilterData(DataTable dtsearchresult, string Purpose, string RefNo, string Search)
        {
            string mquery = "SELECT  TOP (100) PERCENT C.CompanyRefNo, C.CompanyName, F.FactoryRefNo, F.FactoryName, FC.CompanyRefNo AS FCompRefNo, FC.CompanyName AS FCompany, U.UnitName, U.UnitRefNo, UF.FactoryName AS UFactory," +
            "UF.FactoryRefNo AS UFactoryRefNo, UC.CompanyName AS UCompany, UC.CompanyRefNo AS UCompRefNo, P.ProductRefNo, P.ProductLevel1, P.ProductLevel2, P.ProductLevel3, P.NSCCode, P.NIINCode, P.Role," +
            "P.ProductDescription, P.OEMPartNumber, P.OEMName, P.OEMCountry, P.DPSUPartNumber, P.ItemDescriptionPDFFile, P.EndUserPartNumber, P.HSNCode, P.TechnologyLevel1, P.TechnologyLevel2, P.TechnologyLevel3," +
            "P.Platform, P.NomenclatureOfMainSystem, P.EndUser, P.PurposeofProcurement, P.ProcurmentCategoryRemark, P.IsIndeginized, P.ManufactureName, P.ManufactureAddress, P.YearofIndiginization, P.SearchKeyword, " +
            " P.DPSUServices, P.Remarks, P.FinancialSupport, P.FinancialRemark, P.TenderStatus, P.TenderSubmition, P.TenderFillDate, P.TenderUrl, P.NodelDetail, P.Testing, P.TestingRemarks, P.Certification, P.CertificationRemark, " +
            " P.IsActive, P.LastUpdated,P.IsApproved, dbo.tbl_mst_SubCategory.SCategoryName AS NSNGroup, tbl_mst_SubCategory_1.SCategoryName AS DefencePlatform, tbl_mst_SubCategory_2.SCategoryName AS ProdIndustryDoamin, " +
            " tbl_mst_SubCategory_3.SCategoryName AS NSNGroupClass,tbl_mst_Country.CountryName as Country,tbl_mst_SubCategory_4.SCategoryName AS ProdIndustrySubDomain " +
            "FROM  dbo.tbl_mst_Factory AS UF LEFT OUTER JOIN" +
            " dbo.tbl_mst_Company AS UC ON UF.CompanyRefNo = UC.CompanyRefNo RIGHT OUTER JOIN" +
            " dbo.tbl_mst_Company AS FC RIGHT OUTER JOIN" +
            " dbo.tbl_mst_Company AS C RIGHT OUTER JOIN" +
            " dbo.tbl_mst_Factory AS F RIGHT OUTER JOIN" +
            " dbo.tbl_mst_MainProduct AS P LEFT OUTER JOIN" +
            " dbo.tbl_mst_SubCategory ON P.ProductLevel1 = dbo.tbl_mst_SubCategory.SCategoryId LEFT OUTER JOIN" +
            " dbo.tbl_mst_SubCategory AS tbl_mst_SubCategory_2 ON P.TechnologyLevel1 = tbl_mst_SubCategory_2.SCategoryId LEFT OUTER JOIN" +
            " dbo.tbl_mst_SubCategory AS tbl_mst_SubCategory_1 ON P.Platform = tbl_mst_SubCategory_1.SCategoryId LEFT OUTER JOIN" +
            " dbo.tbl_mst_SubCategory AS tbl_mst_SubCategory_3 ON P.ProductLevel2 = tbl_mst_SubCategory_3.SCategoryId LEFT OUTER JOIN" +
            " dbo.tbl_mst_Country ON P.OEMCountry = dbo.tbl_mst_Country.CountryID LEFT OUTER JOIN" +
            "  dbo.tbl_mst_SubCategory AS tbl_mst_SubCategory_4 ON P.TechnologyLevel2 = tbl_mst_SubCategory_4.SCategoryId LEFT OUTER JOIN" +
            " dbo.tbl_mst_Unit AS U ON P.CompanyRefNo = U.UnitRefNo ON F.FactoryRefNo = P.CompanyRefNo ON C.CompanyRefNo = P.CompanyRefNo ON FC.CompanyRefNo = F.CompanyRefNo ON " +
            " UF.FactoryRefNo = U.FactoryRefNo WHERE '1'='1'";
            for (int i = 0; i < dtsearchresult.Rows.Count; i++)
            {
                mquery = mquery + " and (" + dtsearchresult.Rows[i][0].ToString() + "" + dtsearchresult.Rows[i][1].ToString() + ")";
            }
            mquery = mquery + " ORDER BY P.LastUpdated DESC, C.CompanyName, F.FactoryName, U.UnitName";
            return SqlHelper.Instance.GetDataset(mquery).Tables[0];
        }
        public DataTable RetriveFilterCode(string CompRefNo, string SearchValue, string Criteria)
        {
            return SqlHelper.Instance.RetriveFilterCode(CompRefNo, SearchValue, Criteria);
        }
        public DataTable RetrivenewcategortFIIG_No(string Value,string Criteria)
        {
            return SqlHelper.Instance.RetrivenewcategortFIIG_No(Value, Criteria);
        }
        public DataTable GetGraph(string CompRefNo, string techvalue, string Criteria)
        {
            if (Criteria == "ViewGraph")
            {
                return SqlHelper.Instance.GetExecuteData("select scategoryname,SUM(total) as Total,Techlevel from fn_ProductGrpah('" + CompRefNo + "') group by scategoryname,techlevel ");
            }
            else
            {
                return SqlHelper.Instance.GetExecuteData("select scategoryname,SUM(total) as Total,Techlevel from fn_ProductGrpahII('" + CompRefNo + "','" + techvalue + "') group by scategoryname,techlevel ");
            }
        }
        public DataTable GetGraphNSNGROUP(string CompRefNo, string techvalue, string Criteria)
        {
            if (Criteria == "ViewGraph")
            {
                return SqlHelper.Instance.GetExecuteData("select scategoryname,SUM(total) as Total,NSNGroup from fn_ProductGrpahNSNGroup('" + CompRefNo + "') group by scategoryname,NSNGroup order by scategoryname asc");
            }
            else
            {
                return SqlHelper.Instance.GetExecuteData("select scategoryname,SUM(total) as Total,NSNGROUPCLASS from fn_ProductNSNClassGrpahII('" + CompRefNo + "','" + techvalue + "') group by scategoryname,NSNGROUPCLASS order by scategoryname asc");
            }
        }
        public DataTable GetDashboardDataApproveDisapproveItem(string Purpose, string Search, string type)
        {
            return SqlHelper.Instance.GetDashboardDataApproveDisapproveItem(Purpose, Search, type);
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
        public DataTable TestGrid(string Function, string ProdRefNo, Int32 ProdInfoId, string Name, string Value, string Unit)
        {
            return SqlHelper.Instance.TestGrid(Function, ProdRefNo, ProdInfoId, Name, Value, Unit);
        }
        public DataTable RetriveSaveEstimateGrid(string Function, Int32 ProdInfoId, string ProdRefNo, Int32 Year, string FYear, string EstimateQuantity, string Unit, string Price)
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
