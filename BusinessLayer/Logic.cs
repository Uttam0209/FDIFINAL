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
            return SqlHelper.Instance.VerifyEmail(hyLogin, out _msg, out mp);
        }
        public string VerifyEmailVendor(HybridDictionary hyLogin, out string _msg, out string mp)
        {
            return SqlHelper.Instance.VerifyEmailVendor(hyLogin, out _msg, out mp);
        }
        public string VerifyEmployee(HybridDictionary hyLogin, out string _msg, out string Defaultpage)
        {
            return SqlHelper.Instance.VerifyEmployee(hyLogin, out _msg, out Defaultpage);
        }
        public string VerifyVendorEmployee(HybridDictionary hyLogin, out string _msg, out string Defaultpage, out string CompanyName, out string VUser)
        {
            return SqlHelper.Instance.VerifyVendorEmployee(hyLogin, out _msg, out Defaultpage, out CompanyName, out VUser);
        }
        public string VerifyHelpdeskEmployee(HybridDictionary hyLogin, out string _msg, out string Defaultpage)
        {
            return SqlHelper.Instance.VerifyHelpdeskEmployee(hyLogin, out _msg, out Defaultpage);
        }
        #endregion
        #region Email and Company Name"
        public string VerifyEmailandCompany(string strEmail, string strCompany, out string _msg)
        {
            return SqlHelper.Instance.VerifyEmailandCompany(strEmail, strCompany, out _msg);
        }
        #endregion
        #region SaveCdoe Or Excel
        public DataTable CreateExcelConnection(string FilePath, string SheetName, out string text)
        {
            DataTable dt = SqlHelper.Instance.CreateExcelConnection(FilePath, SheetName, out text);
            return dt;
        }
        public DataTable CreateExcelConnection1(string FilePath, string SheetName, out string text)
        {
            DataTable dt = SqlHelper.Instance.CreateExcelConnection1(FilePath, SheetName, out text);
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
        public string SaveExcelProduct1(DataTable dtMaster, HybridDictionary hyadvertice)
        {
            return SqlHelper.Instance.SaveExcelProduct1(dtMaster, hyadvertice);
        }
        #endregion
        #region SaveCdoe Or SHQExcel
        public string SaveExcelProduct1FORSHQ(DataTable dtMaster, HybridDictionary hyadvertice)
        {
            return SqlHelper.Instance.SaveExcelProduct1FORSHQ(dtMaster, hyadvertice);
        }
        #endregion
        #region SaveCode
        public string SaveLog(HybridDictionary hyLog, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveLog(hyLog, out _sysMsg, out _msg);
        }

        public string SaveUserIP(HybridDictionary hysaveip, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveUserIP(hysaveip, out _sysMsg, out _msg);
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
        public string SaveCodeProduct(HybridDictionary hyProduct, DataTable DtImage, DataTable DtPdf, DataTable dtEstimateQuantity, DataTable dtEstimateQuantity1, out string _sysMsg, out string _msg, string Criteria)
        {
            return SqlHelper.Instance.SaveCodeProduct(hyProduct, DtImage, DtPdf, dtEstimateQuantity, dtEstimateQuantity1, out _sysMsg, out _msg, Criteria);
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
        public DataTable RetriveProductIndig1(string m, string n, string o)
        {
            return SqlHelper.Instance.RetriveFilterCode(m, n, o);
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
            " tbl_mst_SubCategory_3.SCategoryName AS NSNGroupClass,tbl_mst_Country.CountryName as Country,tbl_mst_SubCategory_4.SCategoryName AS ProdIndustrySubDomain,tbl_mst_NodalOfficer.NodalOfficerEmail AS NodalOfficerEmail,tbl_mst_NodalOfficer.NodalOfficerTelephone AS NodalOfficerTelephone " +
            "FROM  dbo.tbl_mst_Factory AS UF LEFT OUTER JOIN " +
            " dbo.tbl_mst_Company AS UC ON UF.CompanyRefNo = UC.CompanyRefNo RIGHT OUTER JOIN " +
            " dbo.tbl_mst_Company AS FC RIGHT OUTER JOIN " +
            " dbo.tbl_mst_Company AS C RIGHT OUTER JOIN " +
            " dbo.tbl_mst_Factory AS F RIGHT OUTER JOIN " +
            " dbo.tbl_mst_MainProduct AS P LEFT OUTER JOIN" +
            " tbl_mst_NodalOfficer ON P.NodelDetail=tbl_mst_NodalOfficer.NodalOfficerID Left Outer Join " +
            " dbo.tbl_mst_SubCategory ON P.ProductLevel1 = dbo.tbl_mst_SubCategory.SCategoryId LEFT OUTER JOIN " +
            " dbo.tbl_mst_SubCategory AS tbl_mst_SubCategory_2 ON P.TechnologyLevel1 = tbl_mst_SubCategory_2.SCategoryId LEFT OUTER JOIN " +
            " dbo.tbl_mst_SubCategory AS tbl_mst_SubCategory_1 ON P.Platform = tbl_mst_SubCategory_1.SCategoryId LEFT OUTER JOIN " +
            " dbo.tbl_mst_SubCategory AS tbl_mst_SubCategory_3 ON P.ProductLevel2 = tbl_mst_SubCategory_3.SCategoryId LEFT OUTER JOIN " +
            " dbo.tbl_mst_Country ON P.OEMCountry = dbo.tbl_mst_Country.CountryID LEFT OUTER JOIN " +
            "  dbo.tbl_mst_SubCategory AS tbl_mst_SubCategory_4 ON P.TechnologyLevel2 = tbl_mst_SubCategory_4.SCategoryId LEFT OUTER JOIN " +
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
        public DataTable RetrivenewcategortFIIG_No(string Value, string Criteria)
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
                return SqlHelper.Instance.GetExecuteData("select scategoryname,SUM(total) as Total,NSNGroup from fn_ProductGrpahNSNGroup('" + CompRefNo + "') group by scategoryname,NSNGroup order by Total desc");
            }
            else
            {
                return SqlHelper.Instance.GetExecuteData("select scategoryname,SUM(total) as Total,NSNGROUPCLASS from fn_ProductNSNClassGrpahII('" + CompRefNo + "','" + techvalue + "') group by scategoryname,NSNGROUPCLASS order by Total desc");
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
        public DataTable RetriveCart(string value)
        {
            string mquery = "SELECT  TOP (100) PERCENT ROW_NUMBER() OVER( ORDER BY P.ProductId) AS [ROW_NUMBER],  C.CompanyRefNo, C.CompanyName, F.FactoryRefNo, F.FactoryName, FC.CompanyRefNo AS FCompRefNo, FC.CompanyName AS FCompany, U.UnitName, U.UnitRefNo, UF.FactoryName AS UFactory,"
                             + "UF.FactoryRefNo AS UFactoryRefNo, UC.CompanyName AS UCompany, UC.CompanyRefNo AS UCompRefNo, P.ProductRefNo, P.ProductLevel1, P.ProductLevel2, P.ProductLevel3, P.NSCCode, P.NIINCode, P.Role, "
                             + "P.ProductDescription, P.OEMPartNumber, P.OEMName, P.OEMCountry, P.DPSUPartNumber, P.ItemDescriptionPDFFile, P.EndUserPartNumber, P.HSNCode, P.TechnologyLevel1, P.TechnologyLevel2, P.TechnologyLevel3, "
                             + "P.Platform, P.NomenclatureOfMainSystem, P.EndUser, P.PurposeofProcurement, P.ProcurmentCategoryRemark, P.IsIndeginized, P.ManufactureName, P.ManufactureAddress, P.YearofIndiginization, P.SearchKeyword, "
                             + "P.DPSUServices, P.Remarks, P.FinancialSupport, P.FinancialRemark, P.TenderStatus, P.TenderSubmition, P.TenderFillDate, P.TenderUrl, P.NodelDetail, P.Testing, P.TestingRemarks, P.Certification, P.CertificationRemark,P.IsShowGeneral, "
                             + "P.IsActive, P.LastUpdated,P.IsApproved, tbl_mst_SubCategory.SCategoryName AS NSNGroup, tbl_mst_SubCategory_1.SCategoryName AS DefencePlatform, tbl_mst_SubCategory_2.SCategoryName AS ProdIndustryDoamin, "
                             + "tbl_mst_SubCategory_3.SCategoryName AS NSNGroupClass,tbl_mst_Country.CountryName as Country,tbl_mst_SubCategory_4.SCategoryName AS ProdIndustrySubDomain,tbl_mst_NodalOfficer.NodalOfficerEmail AS NodalOfficerEmail,tbl_mst_NodalOfficer.NodalOfficerTelephone AS NodalOfficerTelephone"
                            + ",IsProductImported,tbl_mst_SubCategory_5.SCategoryName as ItemCode  FROM            tbl_mst_Factory AS UF LEFT OUTER JOIN "
                             + "tbl_mst_Company AS UC ON UF.CompanyRefNo = UC.CompanyRefNo RIGHT OUTER JOIN "
                             + "tbl_mst_Company AS FC RIGHT OUTER JOIN "
                             + "tbl_mst_Company AS C RIGHT OUTER JOIN "
                             + "tbl_mst_Factory AS F RIGHT OUTER JOIN "
                             + "tbl_mst_MainProduct AS P LEFT OUTER JOIN "
                             + " tbl_mst_NodalOfficer ON P.NodelDetail=tbl_mst_NodalOfficer.NodalOfficerID Left Outer Join "
                             + "tbl_mst_SubCategory ON P.ProductLevel1 = tbl_mst_SubCategory.SCategoryId LEFT OUTER JOIN "
                             + "tbl_mst_SubCategory AS tbl_mst_SubCategory_2 ON P.TechnologyLevel1 = tbl_mst_SubCategory_2.SCategoryId LEFT OUTER JOIN "
                             + "tbl_mst_SubCategory AS tbl_mst_SubCategory_1 ON P.Platform = tbl_mst_SubCategory_1.SCategoryId LEFT OUTER JOIN "
                             + "tbl_mst_SubCategory AS tbl_mst_SubCategory_3 ON P.ProductLevel2 = tbl_mst_SubCategory_3.SCategoryId LEFT OUTER JOIN "
                             + " tbl_mst_SubCategory AS tbl_mst_SubCategory_5 ON P.ProductLevel3 = tbl_mst_SubCategory_5.SCategoryId LEFT OUTER JOIN "
                             + "tbl_mst_Country ON P.OEMCountry = tbl_mst_Country.CountryID LEFT OUTER JOIN "
                             + " tbl_mst_SubCategory AS tbl_mst_SubCategory_4 ON P.TechnologyLevel2 = tbl_mst_SubCategory_4.SCategoryId LEFT OUTER JOIN "
                             + "tbl_mst_Unit AS U ON P.CompanyRefNo = U.UnitRefNo ON F.FactoryRefNo = P.CompanyRefNo ON C.CompanyRefNo = P.CompanyRefNo ON FC.CompanyRefNo = F.CompanyRefNo ON "
                             + "UF.FactoryRefNo = U.FactoryRefNo WHERE  (P.IsActive = 'Y') and P.ProductRefNo in (" + value + ")"
                             + "ORDER BY P.LastUpdated DESC, C.CompanyName, F.FactoryName, U.UnitName";
            return SqlHelper.Instance.GetDataset(mquery).Tables[0];

        }
        public DataTable RetriveCartNew(string value)
        {
            string mquery = "SELECT * from tbl_trn_ProductFilterSearchTemp WHERE  ProductRefNo in (" + value + ")";
            return SqlHelper.Instance.GetDataset(mquery).Tables[0];
        }
        public DataTable RetriveMailCart(string value)
        {
            string mquery = "SELECT P.ProductRefNo as ItemID,P.ProductDescription as ItemName,n.NodalOfficerEmail AS NodalOfficerEmail FROM  tbl_mst_MainProduct AS P LEFT OUTER JOIN tbl_mst_NodalOfficer as n" +
                            " ON P.NodelDetail=n.NodalOfficerID WHERE  (P.IsActive = 'Y') and P.ProductRefNo in (" + value + ")";
            return SqlHelper.Instance.GetDataset(mquery).Tables[0];
        }
        public DataTable RetriveProductUser()
        {
            return SqlHelper.Instance.GetExecuteData("select * from tbl_trn_ProductFilterSearchTemp");
            // fn_ProductFilterSearch()
        }
        public DataTable RetriveProductView()
        {
            return SqlHelper.Instance.GetExecuteData("select * from fn_ProductView()");
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
        public DataTable RetriveSaveEstimateGrid(string Function, Int32 ProdInfoId, string ProdRefNo, Int32 Year, string FYear, string EstimateQuantity, string Unit, string Price, string type)
        {
            return SqlHelper.Instance.RetriveSaveEstimateGrid(Function, ProdInfoId, ProdRefNo, Year, FYear, EstimateQuantity, Unit, Price, type);
        }
        #endregion
        ////////////////////////////////////////================================ Vendor Code=======================================//////////////////////////////////////
        #region Code for vendor  Save
        public string SaveVendorRegis(HybridDictionary hysavecomp, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveVendorRegis(hysavecomp, out _sysMsg, out _msg);
        }
        public string SaveVendorGeneralInfo(HybridDictionary HySaveVendorRegisdetail, DataTable DtFristGrid, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveVendorGeneralInfo(HySaveVendorRegisdetail, DtFristGrid, out _sysMsg, out _msg);
        }
        public string SaveVendorCompanyInfo(HybridDictionary HySaveVendorRegisdetail, DataTable Dt1, DataTable Dt2, DataTable Dt3, DataTable Dt4, DataTable Dt5, DataTable Dt6, DataTable Dt7, DataTable Dt8, DataTable DtCer1, DataTable DtCer2, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveVendorCompanyInfo(HySaveVendorRegisdetail, Dt1, Dt2, Dt3, Dt4, Dt5, Dt6, Dt7, Dt8, DtCer1, DtCer2, out _sysMsg, out _msg);
        }
        public string SaveVendorCompanyInfo2(DataTable Dt1, string mCurrentID, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveVendorCompanyInfo2(Dt1, mCurrentID, out _sysMsg, out _msg);
        }
        public string SaveVendorRegistrationDetails(HybridDictionary HySaveVendorRegisdetail, DataTable DtFristGrid, DataTable dt2, DataTable dt3, DataTable dt4, DataTable dt5, DataTable dt6, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveVendorRegistrationDetails(HySaveVendorRegisdetail, DtFristGrid, dt2, dt3, dt4, dt5, dt6, out _sysMsg, out _msg);
        }

        public string SaveVendorRegisNoDetails(HybridDictionary HySaveVendorRegisdetail, DataTable Dt1, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveVendorRegisNoDetails(HySaveVendorRegisdetail, Dt1, out _sysMsg, out _msg);
        }
        public string SaveVendorAccountInfo(DataTable Dt1, DataTable Dt2, string mCurrentID, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveVendorAccountInfo(Dt1, Dt2, mCurrentID, out _sysMsg, out _msg);
        }

        public string SaveVendorDefence(DataTable Dt1, DataTable Dt2, DataTable Dt3, DataTable Dt4, DataTable Dt5, string mCurrentID, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveVendorDefence(Dt1, Dt2, Dt3, Dt4, Dt5, mCurrentID, out _sysMsg, out _msg);
        }


        #endregion
        #region Code for vendor  Delete
        #endregion
        #region Code for vendor  Update
        public Int32 InsertStatusEdit(string ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6)
        {
            string mquery = "insert into tbl_trn_VendorGeneralInfoMultiGrid " +
                "(VendorRefNo,Type,EnterNameof,Name,Designation,DIN_No,MobileNo)" +
                "Values('" + ID + "','" + Value1 + "','" + Value2 + "','" + Value3 + "','" + Value4 + "','" + Value5 + "','" + Value6 + "')";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 UpdateStatusEdit(Int64 ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6)
        {
            string mquery = "update tbl_trn_VendorGeneralInfoMultiGrid set VendorRefNo='" + Value1 + "',EnterNameof='" + Value2 + "',Name='" + Value3 + "',Designation='" + Value4 + "',DIN_No='" + Value5 + "',MobileNo='" + Value6 + "',LastUpdated='" + DateTime.Now.ToString("dd-MMM-yyyy") + "' where MasterId='" + ID + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 DeleteEditGrid(Int32 Id)
        {
            string mquery = "delete from tbl_trn_VendorGeneralInfoMultiGrid  where MasterId='" + Id + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 DeleteEditGrid1(Int32 Id)
        {
            string mquery = "delete from VendorRegistrationMultiImage  where ImageID='" + Id + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 DeleteProdDet(Int32 Id)
        {
            string mquery = "delete from tbl_trn_VendorDetailofDefence  where VDetailDefenceId='" + Id + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 DeleteTechdet(Int32 Id)
        {
            string mquery = "delete from tbl_trn_VendorTechDetails  where VTechId='" + Id + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 DeleteRawId(Int32 Id)
        {
            string mquery = "delete from tbl_trn_VendorRawMaterial  where VRawMaterialId='" + Id + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 DeleteProdSupply(Int32 Id)
        {
            string mquery = "delete from tbl_trn_VendorProducedSupplied  where VSupplyId='" + Id + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        #region Multigrid edit insert code
        public Int32 InsertMaufacfacility(string ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6, string Value7, string Value8)
        {
            string mquery = "insert into tbl_trn_VendorDetailMultiGrid (VendorRefNo,Type,Name_of_Factory,Factory_GstNo,Comp_Postal_Address,Contact_Official_Name,Telephone_No,Fax_No,Email_Id)Values('" + ID + "','" + Value1 + "','" + Value2 + "','" + Value3 + "','" + Value4 + "','" + Value5 + "','" + Value6 + "','" + Value7 + "','" + Value8 + "')";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 UpdateMaufacfacility(Int64 ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6, string Value7, string Value8)
        {
            string mquery = "update tbl_trn_VendorDetailMultiGrid set VendorRefNo='" + Value1 + "',Name_of_Factory='" + Value2 + "',Factory_GstNo='" + Value3 + "',Comp_Postal_Address='" + Value4 + "',Contact_Official_Name='" + Value5 + "',Telephone_No='" + Value6 + "',Fax_No='" + Value7 + "',Email_Id='" + Value8 + "' where MasterId='" + ID + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 InsertArea(string ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6)
        {
            string mquery = "insert into tbl_trn_VendorDetailMultiGrid (VendorRefNo,Type,Area_Factory_Name,Production_Area,Inspection_Area,Total_Covered_Area,Total_Area)Values('" + ID + "','" + Value1 + "','" + Value2 + "','" + Value3 + "','" + Value4 + "','" + Value5 + "','" + Value6 + "')";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 UpdateArea(Int64 ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6)
        {
            string mquery = "update tbl_trn_VendorDetailMultiGrid set VendorRefNo='" + Value1 + "',Area_Factory_Name='" + Value2 + "',Production_Area='" + Value3 + "',Inspection_Area='" + Value4 + "',Total_Covered_Area='" + Value5 + "',Total_Area='" + Value6 + "' where MasterId='" + ID + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 InsertPlantMachine(string ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6)
        {
            string mquery = "insert into tbl_trn_VendorDetailMultiGrid (VendorRefNo,Type,Description_Machine_Model_Specs,Make,Quantity,Date_of_Purchase,Usage)Values('" + ID + "','" + Value1 + "','" + Value2 + "','" + Value3 + "','" + Value4 + "','" + Value5 + "','" + Value6 + "')";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 UpdatePlantMachine(Int64 ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6)
        {
            string mquery = "update tbl_trn_VendorDetailMultiGrid set VendorRefNo='" + Value1 + "',Description_Machine_Model_Specs='" + Value2 + "',Make='" + Value3 + "',Quantity='" + Value4 + "',Date_of_Purchase='" + Value5 + "',Usage='" + Value6 + "' where MasterId='" + ID + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 UpdateCust(Int64 ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6, string Value7, string Value8)
        {
            string mquery = "update tbl_trn_VendorDetailMultiGrid set VendorRefNo='" + Value1 + "',TOTAL_Employees='" + Value2 + "',ADMINISTRATIVE='" + Value3 + "',TECHNICAL='" + Value4 + "',NON_TECHNICAL='" + Value5 + "',QC_INSPECTION='" + Value6 + "',SKILLED_LABOUR='" + Value7 + "',UNSKILLED_LABOUR='" + Value8 + "' where MasterId='" + ID + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 InsertCust(string ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6, string Value7, string Value8)
        {
            string mquery = "insert into tbl_trn_VendorDetailMultiGrid (VendorRefNo,Type,TOTAL_Employees,ADMINISTRATIVE,TECHNICAL,NON_TECHNICAL,QC_INSPECTION,SKILLED_LABOUR,UNSKILLED_LABOUR)Values('" + ID + "','" + Value1 + "','" + Value2 + "','" + Value3 + "','" + Value4 + "','" + Value5 + "','" + Value6 + "','" + Value7 + "','" + Value8 + "')";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 Updatetestfacilities(Int64 ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6, string Value7, string Value8)
        {
            string mquery = "update tbl_trn_VendorDetailMultiGrid set VendorRefNo='" + Value1 + "',Type_of_GAUGE_Test_Equipment='" + Value2 + "',Test_Make='" + Value3 + "',Least_Count='" + Value4 + "',Range_of_MEASURMENT='" + Value5 + "',Unit_of_MEASURMENT='" + Value8 + "',CERTIFICATION_YEAR='" + Value6 + "',Year_of_purchase='" + Value7 + "' where MasterId='" + ID + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 Inserttestfacilities(string ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6, string Value7, string Value8)
        {
            string mquery = "insert into tbl_trn_VendorDetailMultiGrid (VendorRefNo,Type,Type_of_GAUGE_Test_Equipment,Test_Make,Least_Count,Range_of_MEASURMENT,Unit_of_MEASURMENT,CERTIFICATION_YEAR,Year_of_purchase)Values('" + ID + "','" + Value1 + "','" + Value2 + "','" + Value3 + "','" + Value4 + "','" + Value5 + "','" + Value8 + "','" + Value6 + "','" + Value7 + "')";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 Updateauthdealaddress(Int64 ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6, string Value7, string Value8)
        {
            string mquery = "update tbl_trn_VendorDetailMultiGrid set VendorRefNo='" + Value1 + "',DistributorStreetAddress='" + Value2 + "',DistributorState='" + Value3 + "',DistributorPincode='" + Value4 + "',DistributorPhone='" + Value5 + "',DistributorFax='" + Value6 + "',DistributorEmail='" + Value7 + "',DistriutorName='" + Value8 + "' where MasterId='" + ID + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 Insertauthdealaddress(string ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6, string Value7, string Value8)
        {
            string mquery = "insert into tbl_trn_VendorDetailMultiGrid (VendorRefNo,Type,DistributorStreetAddress,DistributorState,DistributorPincode,DistributorPhone,DistributorFax,DistributorEmail,DistributorName)Values('" + ID + "','" + Value1 + "','" + Value2 + "','" + Value3 + "','" + Value4 + "','" + Value5 + "','" + Value6 + "','" + Value7 + "','" + Value8 + "')";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 Updateoutsourcefacility(Int64 ID, string Value1, string Value2, string Value3, string Value4, string Value5)
        {
            string mquery = "update tbl_trn_VendorDetailMultiGrid set VendorRefNo='" + Value1 + "',OutsourcingMainEquipment='" + Value2 + "',OutsourcingTestEquip='" + Value3 + "',OutsourcingProcessfacility='" + Value4 + "',OutsoursingNameAddressofSubContractor='" + Value5 + "' where MasterId='" + ID + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 Insertoutsourcefacility(string ID, string Value1, string Value2, string Value3, string Value4, string Value5)
        {
            string mquery = "insert into tbl_trn_VendorDetailMultiGrid (VendorRefNo,Type,OutsourcingMainEquipment,OutsourcingTestEquip,OutsourcingProcessfacility,OutsoursingNameAddressofSubContractor)Values('" + ID + "','" + Value1 + "','" + Value2 + "','" + Value3 + "','" + Value4 + "','" + Value5 + "')";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 Updatejointventure(Int64 ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6, string Value7, string Value8)
        {
            string mquery = "update tbl_trn_VendorDetailMultiGrid set VendorRefNo='" + Value1 + "',JointVentureName='" + Value2 + "',IsJointVentureCountry='" + Value3 + "',CompleteAddress='" + Value4 + "',ContOfficialName='" + Value5 + "',TelephoneNo='" + Value6 + "',FaxNo='" + Value7 + "',EmailId='" + Value8 + "' where MasterId='" + ID + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 Insertjointventure(string ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6, string Value7, string Value8)
        {
            string mquery = "insert into tbl_trn_VendorDetailMultiGrid (VendorRefNo,Type,JointVentureName,IsJointVentureCountry,CompleteAddress,ContOfficialName,TelephoneNo,FaxNo,EmailId)Values('" + ID + "','" + Value1 + "','" + Value2 + "','" + Value3 + "','" + Value4 + "','" + Value5 + "','" + Value6 + "','" + Value7 + "','" + Value8 + "')";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }


        public Int32 Insertcerti1(string ID, string Value1, string Value2, string Value3)
        {
            string mquery = "insert into VendorRegistrationMultiImage (VendorRefNo,Type,Name,Path)Values('" + ID + "','" + Value1 + "','" + Value2 + "','" + Value3 + "')";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 Insertcerti2(string ID, string Value1, string Value2, string Value3)
        {
            string mquery = "insert into VendorRegistrationMultiImage (VendorRefNo,Type,Name,Path)Values('" + ID + "','" + Value1 + "','" + Value2 + "','" + Value3 + "')";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }

        public Int32 UpdateOEM(Int64 ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6, string Value7, string Value8, string value9)
        {
            string mquery = "update tbl_trn_VendorDetailMultiGrid set VendorRefNo='" + Value1 + "',OEMName='" + Value2 + "',OEMAddress='" + Value3 + "',OEMCountry='" + Value4 + "',OEMOfficialName='" + Value5 + "',OEMTelephoneNo='" + Value6 + "',OEMFaxNo='" + Value7 + "',OEMEmailId='" + Value8 + "',FileAuthorization='" + value9 + "' where MasterId='" + ID + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 InsertOEM(string ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6, string Value7, string Value8, string value9)
        {
            string mquery = "insert into tbl_trn_VendorDetailMultiGrid (VendorRefNo,Type,OEMName,OEMAddress,OEMCountry,OEMOfficialName,OEMTelephoneNo,OEMFaxNo,OEMEmailId,FileAuthorization)Values('" + ID + "','" + Value1 + "','" + Value2 + "','" + Value3 + "','" + Value4 + "','" + Value5 + "','" + Value6 + "','" + Value7 + "','" + Value8 + "','" + value9 + "')";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }

        public Int32 UpdateGovt(Int64 ID, string Value1, string Value2, string Value3, string Value4, string Value5)
        {
            string mquery = "update tbl_trn_GovtApprovedCertificate set VendorRefNo='" + Value1 + "',GovtName='" + Value2 + "',RegistrationNo='" + Value3 + "',Validtill='" + Value4 + "',CertificateUpload='" + Value5 + "',LastUpdate='"+DateTime.Now.ToString("yyyy-MMM-dd")+ "' where GovtMId='" + ID + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 InsertGovt(string Value1, string Value2, string Value3, string Value4, string Value5)
        {
            string mquery = "insert into tbl_trn_GovtApprovedCertificate (VendorRefNo,GovtName,RegistrationNo,Validtill,CertificateUpload)Values('" + Value1 + "','" + Value2 + "','" + Value3 + "','" + Value4 + "','" + Value5 + "')";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 UpdateTurnOver(Int64 ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6)
        {
            string mquery = "update tbl_trn_VendorFinacialInfo set VendorRefNo='" + Value1 + "',FinancialYear='" + Value2 + "',Value_of_Current_Assets='" + Value3 + "',Value_of_Current_Liabilites='" + Value4 + "',Total_Profit_Loss='" + Value5 + "',File_Audited_Balance_account_sheet='" + Value6 + "',LastUpdate='" + DateTime.Now.ToString("yyyy-MMM-dd") + "' where FinancialInfoId='" + ID + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 InsertTurnOver(string ID,  string Value2, string Value3, string Value4, string Value5, string Value6)
        {
            string mquery = "insert into tbl_trn_VendorFinacialInfo (VendorRefNo,FinancialYear,Value_of_Current_Assets,Value_of_Current_Liabilites,Total_Profit_Loss,File_Audited_Balance_account_sheet)Values('" + ID + "','" + Value2 + "','" + Value3 + "','" + Value4 + "','" + Value5 + "','" + Value6 + "')";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 UpdateAccount(Int64 ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6, string Value7)
        {
            string mquery = "update tbl_trn_VendorFinancialBank set VendorRefNo='" + Value1 + "',NameofBank='" + Value2 + "',TypeOfAccount='" + Value3 + "',AccountNo='" + Value4 + "',MICRNo='" + Value5 + "',IFSCCode='" + Value6 + "',File_Bank_Solvency_Certificate='" + Value7 + "', LastUpdate='" + DateTime.Now.ToString("yyyy-MMM-dd") + "' where FinancialAccountId='" + ID + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 InsertAccount(string ID,  string Value2, string Value3, string Value4, string Value5, string Value6, string Value7)
        {
            string mquery = "insert into tbl_trn_VendorFinancialBank (VendorRefNo,NameofBank,TypeOfAccount,AccountNo,MICRNo,IFSCCode,File_Bank_Solvency_Certificate)Values('" + ID + "','" + Value2 + "','" + Value3 + "','" + Value4 + "','" + Value5 + "','" + Value6 + "','" + Value7 + "')";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 UpdateProducts(Int64 ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6)
        {
            string mquery = "update tbl_trn_VendorDetailofDefence set VendorRefNo='" + Value1 + "',ProductNomenClature='" + Value2 + "',NatoGroup='" + Value3 + "',NatoClass='" + Value4 + "',ItemCode='" + Value5 + "',HSNCode='" + Value6 + "',LastUpdate='"+DateTime.Now.ToString("yyyy-MMM-dd")+"' where VDetailDefenceId='" + ID + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 InsertProducts(string ID, string Value1, string Value3, string Value4, string Value5, string Value6)
        {
            string mquery = "insert into tbl_trn_VendorDetailofDefence (VendorRefNo,ProductNomenClature,NatoGroup,NatoClass,ItemCode,HSNCode)Values('" + ID + "','" + Value1 + "','" + Value3 + "','" + Value4 + "','" + Value5 + "','" + Value6 + "')";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 UpdateTechnology(Int64 ID, string Value1, string Value2, string Value3, string Value4)
        {
            string mquery = "update tbl_trn_VendorTechDetails set VendorRefNo='" + Value1 + "',TechNomenclature='" + Value2 + "',Technology1='" + Value3 + "',Technology2='" + Value4 + "',LastUpdate='" + DateTime.Now.ToString("yyyy-MMM-dd") + "' where VTechId='" + ID + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 InsertTechnology(string ID, string Value1, string Value2, string Value3)
        {
            string mquery = "insert into tbl_trn_VendorTechDetails (VendorRefNo,TechNomenclature,Technology1,Technology2)Values('" + ID + "','" + Value1 + "','" + Value2 + "','" + Value3 + "')";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 UpdateRaw(Int64 ID, string Value1, string Value2, string Value3, string Value4, string Value5)
        {
            string mquery = "update tbl_trn_VendorRawMaterial set VendorRefNo='" + Value1 + "',Items='" + Value2 + "',BasicRawMeterial='" + Value3 + "',SourceofMaterial='" + Value4 + "',Major_Raw_Material_Suppliers='" + Value5 + "',LastUpdate='"+DateTime.Now.ToString("yyyy-MMM-dd")+"' where VRawMaterialId='" + ID + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 InsertRaw(string ID,  string Value2, string Value3, string Value4, string Value5)
        {
            string mquery = "insert into tbl_trn_VendorRawMaterial (VendorRefNo,Items,BasicRawMeterial,SourceofMaterial,Major_Raw_Material_Suppliers)Values('" + ID + "','" + Value2 + "','" + Value3 + "','" + Value4 + "','" + Value5 + "')";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 Updateproducedprod(Int64 ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6, string value7)
        {
            string mquery = "update tbl_trn_VendorProducedSupplied set VendorRefNo='" + Value1 + "',Reputed_Customer='" + Value2 + "',Description='" + Value3 + "',SupplyNoDate='" + Value4 + "',OrderQuantity='" + Value5 + "',SuppliedQuantity='" + Value6 + "',Date2='" + value7 + "',LastUpdate='"+DateTime.Now.ToString("yyyy-MMM-dd")+"' where VSupplyId='" + ID + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 Insertproducedprod(string ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6, string value7)
        {
            string mquery = "insert into tbl_trn_VendorProducedSupplied (VendorRefNo,Type,Reputed_Customer,Description,SupplyNoDate,OrderQuantity,SuppliedQuantity,Date2)Values('" + ID + "','" + Value1 + "','" + Value2 + "','" + Value3 + "','" + Value4 + "','" + Value5 + "','" + Value6 + "','" + value7 + "')";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        #endregion
        #endregion
        #region Code for vendor  Retrive
        public DataTable RetriveVendor(Int64 Vid, string VRefNo, string VEmail, string RetFor)
        {
            return SqlHelper.Instance.RetriveVendor(Vid, VRefNo, VEmail, RetFor);
        }

        #endregion
        #region Feedback
        public string InsertFeedback(HybridDictionary hySavefeed, out string sysMsg, out string msg)
        {
            return SqlHelper.Instance.InsertFeedback(hySavefeed, out sysMsg, out msg);
        }
        public string InsertFeedbackReply(HybridDictionary hySavefeedrep, out string sysMsg, out string msg)
        {
            return SqlHelper.Instance.InsertFeedbackReply(hySavefeedrep, out sysMsg, out msg);
        }
        public string InsertLogFeedbackMail(HybridDictionary hyfeedlog, out string sysMsg, out string msg)
        {
            return SqlHelper.Instance.InsertLogFeedbackMail(hyfeedlog, out sysMsg, out msg);
        }
        #endregion
        #region RequestInfoCard
        public string SaveRequestInfo(HybridDictionary hyLog, DataTable dtrepprod, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveRequestInfo(hyLog, dtrepprod, out _sysMsg, out _msg);
        }
        public DataTable RetriveIntrestedProductFilter()
        {
            return SqlHelper.Instance.RetriveFilterCode("", "", "FilterIntrProd");
        }
        public DataTable RetriveIntrestedProductFilter1(string p)
        {
            string query = "select ProductRefNo,CompanyRefNo,CompanyName,FactoryRefNo,FactoryName,UnitName,UnitRefNo,ProductLevel1,ProductLevel2,ProductLevel3,NSCCode,ProductDescription,TechnologyLevel1,TechnologyLevel2,Platform,PurposeofProcurement,IsIndeginized,"
             + "IsShowGeneral,IsActive,LastUpdated,IsApproved,NSNGroup,DefencePlatform,ProdIndustryDoamin,NSNGroupClass,ItemCode,ProdIndustrySubDomain,TopPdf,TopImages,EstimateQu,EstimatePrice,EstimateQufuture,EstimatePricefuture,"
                    + "IsProductImported,EOIStatus,Role,HSNCode,EstiPriMulti,EstiPriMultiF,EstUnitOld,EstUnitfuture,importYear,importFutureYear,OEMName,OEMPartNumber,OEMCountry,EstimatePrice17,EstimatePrice18,importYear17,importYear18,"
                    + "EstimateQu17,EstimateQu18,DPSUPartNumber,IndTargetYear from tbl_trn_ProductFilterSearchTemp where ProductRefNo in (" + p + ")";
            return SqlHelper.Instance.GetDataset(query).Tables[0];
            //return SqlHelper.Instance.RetriveFilterCode(p, "", "FilterIntrProd1");
        }

        //Progress report code 
        public DataTable RetrieveProductDetails(string p, string a, string n)
        {
            string query = "select * from fn_GetListProductDetails('" + p + "','" + a + "','" + n + "')";
            return SqlHelper.Instance.GetDataset(query).Tables[0];
            //return SqlHelper.Instance.RetriveFilterCode(p, "", "FilterIntrProd1");
        }
        #endregion
        #region Shalini FeedBack Update
        public string updateeoi(string refno, string eoistatus, string strtdate, string enddate, string eoiurl)
        {
            return SqlHelper.Instance.updateEOI(refno, eoistatus, strtdate, enddate, eoiurl);
        }
        public string updatesupplyorder(string refno, string soplaced, decimal supplyordervalue, string supplydelivrydate, string supplyorderdate, string manufacturename, string manufactureaddress)
        {
            return SqlHelper.Instance.updateSupplyOrder(refno, soplaced, supplyordervalue, supplydelivrydate, supplyorderdate, manufacturename, manufactureaddress);
        }

        public string updateSucessStory(string refno, string indegprocess, int indigyear, string isindegnized, string targetyear, string indiacategory,
            string manufname, string manufaddress, decimal value, DateTime dat)
        {
            return SqlHelper.Instance.updateSuccessStory(refno, indegprocess, indigyear, isindegnized, targetyear, indiacategory, manufname, manufaddress, value, dat);
        }
        #endregion
        #region Shalini Save Company Remarks Reply
        public string SaveCompRemarkReply(string remarkrefno, string replyusername, string replyemail, string remark, string reply)
        {
            return SqlHelper.Instance.SaveCompRemarkReply(remarkrefno, replyusername, replyemail, remark, reply);
        }
        public DataTable NewRetriveFilterCode(string Criteria, string search, string value, string purpose, string role, int reqid, int refno, int id)
        {
            return SqlHelper.Instance.NewRetriveFilterCode(Criteria, search, value, purpose, role, reqid, refno, id);
        }

        public DataTable NewRetriveFilterCode12(string Criteria, string search, string value, string purpose, string role, int reqid, int refno, int id)
        {
            string mquery = "SELECT MR.RequestID, MR.RequestBy, MR.RequestMobileNo, MR.RequestEmail,MR.RequestDate,TR.ProductRefNo FROM tbl_mst_RequestInfo as MR LEFT OUTER JOIN" +
                " tbl_trn_RequestInfo as TR ON MR.RequestID = TR.RequestID " +
        "where TR.ProductRefNo in (" + search + ") and RequestEmail ='" + value + "' and RequestMobileNo = '" + purpose + "'";
            return SqlHelper.Instance.GetDataset(mquery).Tables[0];
        }

        public string saveSuccessStoryLog(string prorefno, string comprefno, string date, string time)
        {
            return SqlHelper.Instance.SaveSuccessStoryLog(prorefno, comprefno, date, time);
        }

        public string UpdateSaveProduct(HybridDictionary hysave, DataTable DtImage)
        {
            return SqlHelper.Instance.UpdateProduct(hysave, DtImage);
        }
        public DataTable RetriveProductUpdation(string RefNo, string Criteria, string type)
        {
            return SqlHelper.Instance.RetriveProductUpdation(RefNo, Criteria, type);
        }
        public string UpdateNodalOfficers(string comprefno, string type, string nofficername, string nofficermobile, string nofficeremail, string nofficerdesignation)
        {
            return SqlHelper.Instance.updateNodalOfficer(comprefno, type, nofficername, nofficermobile, nofficeremail, nofficerdesignation);
        }

        #endregion               
        #region Test Facility

        public string InsertTestdetails(string test_name, string organisation_id, string Division_id, string Unit_id, string discipline_id, string calibrationfacility, string manufacturer, string manufactureyear, string chambersize,
            string equimntrange, string productmaterial, string specifications,
           string maxdimension, string maxweight, string duration, string advancenotice, string constraints, string remarks, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.InsertTestDetails(test_name, organisation_id, Division_id, Unit_id, discipline_id, calibrationfacility, manufacturer, manufactureyear, chambersize, equimntrange, productmaterial, specifications, maxdimension, maxweight, duration, advancenotice, constraints, remarks, out _sysMsg, out _msg);
        }
        public DataTable RetriveTestDetails()
        {
            return SqlHelper.Instance.RetriveTestDetails();
        }

        public DataTable RetriveTestDetailsbyId(int id)
        {
            return SqlHelper.Instance.RetrieveTestdetailsbyId(id);
        }

        public string savebookorders(int userid, string testname, string organisationid, string discipline, string lab, int noofsample, string dimension, string weighteqmnt, string startdate, string endDate, string attachment, string description)
        {
            return SqlHelper.Instance.SaveBookOrders(userid, testname, organisationid, discipline, lab, noofsample, dimension, weighteqmnt, startdate, endDate, attachment, description);
        }

        public DataTable GetBookedOrders(string compname)
        {
            return SqlHelper.Instance.RetriveBookedOrders(compname);
        }

        public string approvebookedorder(int id)
        {
            return SqlHelper.Instance.ApproveBookedOrder(id);
        }

        public string rejectorderbyremark(int id, string remarks)
        {
            return SqlHelper.Instance.RejectOrderwithRemark(id, remarks);
        }
        public DataTable Getorganisations()
        {
            return SqlHelper.Instance.GetAllOrganisations();
        }
        public DataTable GetLabs()
        {
            return SqlHelper.Instance.GetAllLAbs();
        }

        public DataTable GetDiscipline()
        {
            return SqlHelper.Instance.GetAllDiscipline();
        }

        public DataTable GetUserdatabycompany(string compname)
        {
            return SqlHelper.Instance.GetUserdatabycompany(compname);
        }

        public DataTable getsearchdataonhomepage(string compname)
        {
            return SqlHelper.Instance.SearchTestDataonHomepage(compname);
        }

        public DataTable GetTestdetailsbycompany(string comprefno)
        {
            return SqlHelper.Instance.GetTestdetailsbycompany(comprefno);
        }
        public DataTable BookedOrders()
        {
            return SqlHelper.Instance.GetAllBookedOrders();
        }

        public DataTable pendingBookedOrders()
        {
            return SqlHelper.Instance.GetPendingBookedOrders();
        }

        public DataTable RetriveApprovendRejectBookedOrders()
        {
            return SqlHelper.Instance.GetApprovendRejectedOrders();
        }

        public DataTable Dashboarddatabycompany(string criteria, string search, int id, string compname)
        {
            return SqlHelper.Instance.GetDashboardDatabyCompanywise(criteria, search, id, compname);
        }
        #endregion
        #region other
        public string Add_ErrorLog(HybridDictionary hyLog, out string sysMsg, out string msg)
        {
            return SqlHelper.Instance.Add_ErrorLog(hyLog, out sysMsg, out msg);
        }
        public string updateintshownstatus(string refno, string requestid, string reason, string struserreason, string mrreasone)
        {
            return SqlHelper.Instance.updateIntShownStatus(refno, requestid, reason, struserreason, mrreasone);
        }
        public DataTable newsuccessstory2(string Criteria, string search, string value, string purpose, string role, int reqid, int refno, int id, string eoi, string supplyorder, string indiginized)
        {
            return SqlHelper.Instance.newsuccessstory2(Criteria, search, value, purpose, role, reqid, refno, id, eoi, supplyorder, indiginized);
        }
        public string updateprogSuccessStory(string productrefno, string indigyear, string manufname, string manufaddress)
        {
            return SqlHelper.Instance.updateprogSuccessStory(productrefno, indigyear, manufname, manufaddress);
        }
        public string updateprogSuccessStory2(string productrefno, string indigyear, string manufname, string manufaddress)
        {
            return SqlHelper.Instance.updateprogSuccessStory2(productrefno, indigyear, manufname, manufaddress);
        }
        public string SaveExcelProduct2(DataTable dtMaster)
        {
            return SqlHelper.Instance.SaveExcelProduct2(dtMaster);
        }
        public string SaveExcelProduct2FORSHQ(DataTable dtMaster)
        {
            return SqlHelper.Instance.SaveExcelProduct2FORSHQ(dtMaster);
        }
        public string SaveForgetExp(string email)
        {
            return SqlHelper.Instance.SaveForgetExp(email);
        }
        public DataTable DeleteRecord1(string CompRefNo, string Criteria)
        {
            return SqlHelper.Instance.DeleteRecord1(CompRefNo, Criteria);
        }
        public string SaveExcelProductforSHQ(DataTable dtMaster)
        {
            return SqlHelper.Instance.SaveExcelProductforSHQ(dtMaster);
        }
        public string SaveExcel3510forSHQ(DataTable dtMaster, string l1, string l2, string pid)
        {
            return SqlHelper.Instance.SaveExcel3510forSHQ(dtMaster, l1, l2, pid);
        }
        public DataTable GetInteresteddata(string CompanyRefNo, string DivisionRefNo, string UnitRefNo)
        {
            return SqlHelper.Instance.GetInteresteddata(CompanyRefNo, DivisionRefNo, UnitRefNo);
        }
        public DataTable RetriveFilterCodeupdate(string CompRefNo, string DivisionRefNo, string UnitRefNo, string SearchValue, string Criteria)
        {
            return SqlHelper.Instance.RetriveFilterCodeupdate(CompRefNo, DivisionRefNo, UnitRefNo, SearchValue, Criteria);
        }
        public string TargetValueUpdate(string hfproc, string lblComp, string TotalProd, string texttarget, string lblinhouse, string lblmakeii, string lblotherthan, string Totalindigenized, string lblannual)
        {
            return SqlHelper.Instance.TargetValueUpdate(hfproc, lblComp, TotalProd, texttarget, lblinhouse, lblmakeii, lblotherthan, Totalindigenized, lblannual);
        }
        public DataTable GetProductData(string ddlval)
        {
            return SqlHelper.Instance.GetProductData(ddlval);
        }
        public string AddUserAnalytics(HybridDictionary hysave, out string sysMsg, out string msg)
        {
            return SqlHelper.Instance.AddUserAnalytics(hysave, out sysMsg, out msg);
        }
        public DataTable Getdroppopup(string SearchValue)
        {
            return SqlHelper.Instance.Getdroppopup(SearchValue);
        }
        public DataTable Getproductforpopup(string SearchValue)
        {
            return SqlHelper.Instance.Getproductforpopup(SearchValue);
        }
        public DataTable binddataforpopup(string search)
        {
            return SqlHelper.Instance.binddataforpopup(search);
        }
        public DataTable MaketwoReport(string CompRefNo, string DivisionRefNo, string UnitRefNo, string SearchValue, string Criteria)
        {
            return SqlHelper.Instance.MaketwoReport(CompRefNo, DivisionRefNo, UnitRefNo, SearchValue, Criteria);
        }
        #endregion
        #region HelpdeskCode
        public string SaveHelpDesk(HybridDictionary hyhelp, out string sysMsg, out string msg)
        {
            return SqlHelper.Instance.SaveHelpDesk(hyhelp, out sysMsg, out msg);
        }
        public DataTable RetriveHelpdesk(Int32 id, Int32 id1, Int32 id2, string code, string code1, string code2, string code3, string criteria)
        {
            return SqlHelper.Instance.RetriveHelpdesk(id, id1, id2, code, code1, code2, code3, criteria);
        }
        public string SaveGUser(HybridDictionary HySaveGRegis, out string sysMsg, out string msg)
        {
            return SqlHelper.Instance.SaveGUser(HySaveGRegis, out sysMsg, out msg);
        }
        #endregion
        #region rofile management
        public DataTable RetriveVendorDetal(string VEmail)
        {
            return SqlHelper.Instance.RetriveVendorProfMgmt(VEmail);
        }
        public DataTable UpdateContactVendor(string Mob, string VEmail)
        {
            return SqlHelper.Instance.UpdateVendorProfMgmt(Mob, VEmail);
        }
        public DataTable UpdateEmailVendor(string email, string venderID)
        {
            return SqlHelper.Instance.UpdateVendorEmailProfMgmt(email, venderID);
        }
        public DataTable UpdateContactVendor(string Mob, string VEmail, string StreetAddress, string StreetAddressLine2, string City, string State, string ZipCode, string VendorID5)
        {
            return SqlHelper.Instance.UpdateVendorProfMgmt(Mob, VEmail, StreetAddress, StreetAddressLine2, City, State, ZipCode, VendorID5);
        }
        public DataTable UpdateAuthrizationLVendor(string filename, string email)
        {
            return SqlHelper.Instance.UpdateVendorAuthenticationProfMgmt(filename, email);
        }

        public DataTable UpdateIdentityVendor(string filename, string email)
        {
            return SqlHelper.Instance.UpdateVendorIdentityCardProfMgmt(filename, email);
        }

        public DataTable ProfileMigrationVendor(string NodalOfficerName, string Contact, string VEmail, string StreetAddress, string StreetAddressLine2, string City, string State,
            string ZipCode, string VendorIMGID)
        {
            return SqlHelper.Instance.profileMigrateVendorProfMgmt(NodalOfficerName, Contact, VEmail, StreetAddress, StreetAddressLine2, City, State, ZipCode, VendorIMGID);
        }
        #endregion
        #region Summerycode
        public DataTable RetriveSummery(string type, string company)
        {
            string mquery = "";
            if (type == "Admin" || type == "SuperAdmin")
            {
                mquery = "Select A.MntName as MntName,isnull(A.TotalProd,0) as TotalProd,A.CountMonth as SR,A.MYear as MYear,isnull(B.TotalProd, 0) as MakeII," +
                " isnull(C.TotalProd, 0) as OtherThenMakeII ,isnull(D.TotalProd, 0) as InHouse,isnull(E.TotalProd, 0) as Yettobe " +
                " from(select Count(ProductrefNo) as TotalProd, DateName(Month, LastUpdated) as MntName, Month(LastUpdated) as CountMonth, " +
                " DateName(Year, LastUpdated) as MYear from tbl_trn_ProductFilterSearchTemp where IsIndeginized = 'Y' group by DateName(Month, LastUpdated), Month(LastUpdated), DateName(Year, LastUpdated)) A " +
                " left outer join " +
                " (select  Count(ProductrefNo) as TotalProd, DateName(Month, LastUpdated) as MntName, DateName(Year, LastUpdated) as MYear  from " +
                "     tbl_trn_ProductFilterSearchTemp where PurposeofProcurement = '25' and IsIndeginized = 'Y'  group by DateName(Month, LastUpdated), DateName(Year, LastUpdated))B on A.MntName = B.MntName     " +
                " left outer join " +
                " (select  Count(ProductrefNo) as TotalProd, DateName(Month, LastUpdated) as MntName, DateName(Year, LastUpdated) as MYear  from " +
                "     tbl_trn_ProductFilterSearchTemp where PurposeofProcurement = '58265' and IsIndeginized = 'Y'  group by DateName(Month, LastUpdated), DateName(Year, LastUpdated))C on A.MntName = C.MntName     " +
                " left outer join " +
                " (select  Count(ProductrefNo) as TotalProd, DateName(Month, LastUpdated) as MntName, DateName(Year, LastUpdated) as MYear from " +
                "     tbl_trn_ProductFilterSearchTemp where PurposeofProcurement = '58270'  and IsIndeginized = 'Y' group by DateName(Month, LastUpdated), DateName(Year, LastUpdated))D on A.MntName = D.MntName  " +
                " left outer join " +
                " (select  Count(ProductRefNo) as TotalProd, DateName(Month, LastUpdated) as MntName, DateName(Year, LastUpdated) as MYear  from " +
                "     tbl_trn_ProductFilterSearchTemp where PurposeofProcurement = '' and IsIndeginized = 'Y'  group by DateName(Month, LastUpdated), DateName(Year, LastUpdated))E on A.MntName = E.MntName  order by SR";
            }
            else
            {
                mquery = "Select A.MntName as MntName,isnull(A.TotalProd,0) as TotalProd,A.CountMonth as SR,A.MYear as MYear,isnull(B.TotalProd, 0) as MakeII," +
                  " isnull(C.TotalProd, 0) as OtherThenMakeII ,isnull(D.TotalProd, 0) as InHouse,isnull(E.TotalProd, 0) as Yettobe " +
                  " from(select Count(ProductrefNo) as TotalProd, DateName(Month, LastUpdated) as MntName, Month(LastUpdated) as CountMonth, " +
                  " DateName(Year, LastUpdated) as MYear from tbl_trn_ProductFilterSearchTemp where IsIndeginized = 'Y' and " + type + "='" + company + "' group by DateName(Month, LastUpdated), Month(LastUpdated), DateName(Year, LastUpdated)) A " +
                  " left outer join " +
                  " (select  Count(ProductrefNo) as TotalProd, DateName(Month, LastUpdated) as MntName, DateName(Year, LastUpdated) as MYear  from " +
                  "     tbl_trn_ProductFilterSearchTemp where PurposeofProcurement = '25' and IsIndeginized = 'Y' and " + type + "='" + company + "'  group by DateName(Month, LastUpdated), DateName(Year, LastUpdated))B on A.MntName = B.MntName     " +
                  " left outer join " +
                  " (select  Count(ProductrefNo) as TotalProd, DateName(Month, LastUpdated) as MntName, DateName(Year, LastUpdated) as MYear  from " +
                  "     tbl_trn_ProductFilterSearchTemp where PurposeofProcurement = '58265' and IsIndeginized = 'Y' and " + type + "='" + company + "'  group by DateName(Month, LastUpdated), DateName(Year, LastUpdated))C on A.MntName = C.MntName     " +
                  " left outer join " +
                  " (select  Count(ProductrefNo) as TotalProd, DateName(Month, LastUpdated) as MntName, DateName(Year, LastUpdated) as MYear from " +
                  "     tbl_trn_ProductFilterSearchTemp where PurposeofProcurement = '58270'  and IsIndeginized = 'Y' and " + type + "='" + company + "' group by DateName(Month, LastUpdated), DateName(Year, LastUpdated))D on A.MntName = D.MntName  " +
                  " left outer join " +
                  " (select  Count(ProductRefNo) as TotalProd, DateName(Month, LastUpdated) as MntName, DateName(Year, LastUpdated) as MYear  from " +
                  "     tbl_trn_ProductFilterSearchTemp where PurposeofProcurement = '' and IsIndeginized = 'Y' and " + type + "='" + company + "'  group by DateName(Month, LastUpdated), DateName(Year, LastUpdated))E on A.MntName = E.MntName  order by SR";

            }
            return SqlHelper.Instance.GetDataTable(mquery);
        }

        #endregion
        #region ChoteyLalJi
        public DataTable GetPincode()
        {
            return SqlHelper.Instance.GetAllPincode();
        }

        public DataTable GetCitybyPin(string PinCode)
        {
            return SqlHelper.Instance.GetCityNameWithPin(PinCode);
        }
        public DataTable GetStateByCity(string CityName)
        {
            return SqlHelper.Instance.GetStateWithCity(CityName);
        }
        public DataTable GetCity()
        {
            return SqlHelper.Instance.GetAllCity();
        }
        public DataTable GetState()
        {
            return SqlHelper.Instance.GetAllState();
        }
        public string SaveVendorCheckList(string ChecklistID, string mCurrentID, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.Savechecklist(ChecklistID, mCurrentID, out _sysMsg, out _msg);
        }
        #endregion
        #region  naveen
        public DataTable vendornodelofficerDetails(string vendorID)
        {
            return SqlHelper.Instance.RetriveVendorNodalOfficerDetails(vendorID);
        }
        public DataTable AddnewuserVendor(string UserName, string Contact, string VEmail, string StreetAddress, string StreetAddressLine2, string City, string State, string ZipCode, string VendorIMGID, string Password, string userType)
        {
            return SqlHelper.Instance.AddNewUserMvendor(UserName, Contact, VEmail, StreetAddress, StreetAddressLine2, City, State, ZipCode, VendorIMGID, Password, userType);
        }
        public DataTable RetriveUsersDetals(string VendorID, string Type)
        {
            return SqlHelper.Instance.RetriveUserDetailsMgmt(VendorID, Type);
        }
        public DataTable ProfileMigrationVendor(string NodalOfficerName, string Contact, string VEmail, string StreetAddress, string StreetAddressLine2, string City, string State, string ZipCode, string VendorIMGID, string Authorization, string Identity, string emailsend, string confirmationemail)
        {
            return SqlHelper.Instance.profileMigrateVendorProfMgmt(NodalOfficerName, Contact, VEmail, StreetAddress, StreetAddressLine2, City, State, ZipCode, VendorIMGID, Authorization, Identity, emailsend, confirmationemail);
        }
        public DataTable AddNewNodalOfficerVendor(string NodalOfficerName, string Contact, string VEmail, string VendorIMGID, string Authorization, string Identity, string NOffpass, string NodalEmpCode, string NodalOfficerDepartment, string NodalOfficerDesignation, string NodalOfficerTelephone, string NodalOfficerFax, string CompanyRefNo, string Type, string Salt, string TempRef, string IsActive, string IsLoginActive, string IsNodalOfficer, string DefaultPage, string CreatedBy, string RecTime)
        {
            return SqlHelper.Instance.AddNewNODofficVendorgmt(NodalOfficerName, Contact, VEmail, VendorIMGID, Authorization, Identity, NOffpass, NodalEmpCode, NodalOfficerDepartment, NodalOfficerDesignation, NodalOfficerTelephone, NodalOfficerFax, CompanyRefNo, Type, Salt, TempRef, IsActive, IsLoginActive, IsNodalOfficer, DefaultPage, CreatedBy);
        }
        public DataTable AddLatestCity(string City, string State, string ZipCode)
        {
            return SqlHelper.Instance.AddLatestCityVendorMST(City, State, ZipCode);
        }
        public DataTable AddLatestPincode(string postaladdress, string ZipCode)
        {
            return SqlHelper.Instance.AddLatestPostalcodeVendorMST(postaladdress, ZipCode);
        }
        public DataTable RetriveProMigrationDetals(string offliceID, string newNoEmailID)
        {
            return SqlHelper.Instance.RetriveMigrationProfDetails(offliceID, newNoEmailID);
        }

        public DataTable UpdateNewNodalofficerinfo(string VendorID, string NodalOfficerName, string NodalOfficerEmail, string ContactNo, string StreetAddress, string StreetAddressLine2, string City, string vState, string ZipCode, string authrizlatt, string indetityCard)
        {
            return SqlHelper.Instance.UpdatenewNodalofficerinfoMgmt(VendorID, NodalOfficerName, NodalOfficerEmail, ContactNo, StreetAddress, StreetAddressLine2, City, vState, ZipCode, authrizlatt, indetityCard);
        }

        public DataTable NewVendorregistrationVendor(string NodalOfficerName, string Contact, string VEmail, string StreetAddress, string StreetAddressLine2, string City, string State, string ZipCode, string VendorIMGID, string Authorization, string Identity, string vendorRefferNO, string registCAT, string TypeOfBuisness, string BusinessSector, string Country, string MasterAllowed, string IsActive, string IsLoginActive, string DefaultPage, string Type, string RecInsTime, string CompanyName, string PanNO, string GSTno)
        {
            return SqlHelper.Instance.AddnewvendorprofilVMGMT(NodalOfficerName, Contact, VEmail, StreetAddress, StreetAddressLine2, City, State, ZipCode, VendorIMGID, Authorization, Identity, vendorRefferNO, registCAT, TypeOfBuisness, BusinessSector, Country, MasterAllowed, IsActive, IsLoginActive, DefaultPage, Type, RecInsTime, CompanyName, PanNO, GSTno);
        }
        public DataTable verifyemailIDVendor(string VEmail)
        {
            return SqlHelper.Instance.GetverifyemailIDVendorProfMgmt(VEmail);
        }
        public DataTable verifyemailIDProfileMigrationVendor(string VEmail)
        {
            return SqlHelper.Instance.GetverifyemailIDProfilemigratVendor(VEmail);
        }





        #endregion
        #region ProductWizard
        public DataTable ProductWizard(Int32 a, Int32 b, Int32 c, Int32 d, Int32 e, string f, string g, string h, string i, string j, string k, string l, string m, string n, string @Criteria)
        {
            return SqlHelper.Instance.ProductWizard(a, b, c, d, e, f, g, h, i, j, k, l, m, n, Criteria);
        }
        #endregion
    }
}
