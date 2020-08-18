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
        #region SaveCdoe Or Excel
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
        #endregion
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
        public string SaveCodeProduct(HybridDictionary hyProduct, DataTable DtImage, DataTable DtPdf, DataTable dtProdInfo, DataTable dtEstimateQuantity, DataTable dtEstimateQuantity1, DataTable DtNSFIIG, out string _sysMsg, out string _msg, string Criteria)
        {
            return SqlHelper.Instance.SaveCodeProduct(hyProduct, DtImage, DtPdf, dtProdInfo, dtEstimateQuantity, dtEstimateQuantity1, DtNSFIIG, out _sysMsg, out _msg, Criteria);
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
        public DataTable RetriveMailCart(string value)
        {
            string mquery = "SELECT P.ProductRefNo,P.ProductDescription, P.DPSUPartNumber, s1.SCategoryName AS NSNGroup,s2.SCategoryName AS NSNGroupClass,s3.SCategoryName as ItemCode,n.NodalOfficerEmail AS NodalOfficerEmail " +
                         " FROM  tbl_mst_MainProduct AS P LEFT OUTER JOIN tbl_mst_NodalOfficer as n ON P.NodelDetail=n.NodalOfficerID Left Outer Join tbl_mst_SubCategory as s1 ON   P.ProductLevel1 = s1.SCategoryId LEFT OUTER JOIN " +
                        " tbl_mst_SubCategory AS  s2 ON P.ProductLevel2 = s2.SCategoryId LEFT OUTER JOIN tbl_mst_SubCategory AS s3 ON P.ProductLevel3 = s3.SCategoryId LEFT OUTER JOIN " +
                            " tbl_mst_MainProduct AS o ON s1.SCategoryName='' WHERE  (P.IsActive = 'Y') and P.ProductRefNo in (" + value + ")";
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
            string mquery = "insert into tbl_trn_VendorDetailMultiGrid (VendorRefNo,Type,EnterNameof,Name,Designation,DIN_No,MobileNo)Values('" + ID + "','" + Value1 + "','" + Value2 + "','" + Value3 + "','" + Value4 + "','" + Value5 + "','" + Value6 + "')";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 UpdateStatusEdit(Int64 ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6)
        {
            string mquery = "update tbl_trn_VendorDetailMultiGrid set VendorRefNo='" + Value1 + "',EnterNameof='" + Value2 + "',Name='" + Value3 + "',Designation='" + Value4 + "',DIN_No='" + Value5 + "',MobileNo='" + Value6 + "' where MasterId='" + ID + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 DeleteEditGrid(Int32 Id)
        {
            string mquery = "delete from tbl_trn_VendorDetailMultiGrid  where MasterId='" + Id + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 DeleteEditGrid1(Int32 Id)
        {
            string mquery = "delete from VendorRegistrationMultiImage  where ImageID='" + Id + "'";
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
            string mquery = "update tbl_trn_VendorDetailMultiGrid set VendorRefNo='" + Value1 + "',Name_PSU_UnderGovt='" + Value2 + "',RegistrationNo='" + Value3 + "',Certificate_valid_upto='" + Value4 + "',Upload_Registration_Certificate='" + Value5 + "' where MasterId='" + ID + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 InsertGovt(string ID, string Value1, string Value2, string Value3, string Value4, string Value5)
        {
            string mquery = "insert into tbl_trn_VendorDetailMultiGrid (VendorRefNo,Type,Name_PSU_UnderGovt,RegistrationNo,Certificate_valid_upto,Upload_Registration_Certificate)Values('" + ID + "','" + Value1 + "','" + Value2 + "','" + Value3 + "','" + Value4 + "','" + Value5 + "')";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 UpdateTurnOver(Int64 ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6)
        {
            string mquery = "update tbl_trn_VendorDetailMultiGrid set VendorRefNo='" + Value1 + "',FinancialYear='" + Value2 + "',Value_of_Current_Assets='" + Value3 + "',Value_of_Current_Liabilites='" + Value4 + "',Total_Profit_Loss='" + Value5 + "',File_Audited_Balance_account_sheet='" + Value6 + "' where MasterId='" + ID + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 InsertTurnOver(string ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6)
        {
            string mquery = "insert into tbl_trn_VendorDetailMultiGrid (VendorRefNo,Type,FinancialYear,Value_of_Current_Assets,Value_of_Current_Liabilites,Total_Profit_Loss,File_Audited_Balance_account_sheet)Values('" + ID + "','" + Value1 + "','" + Value2 + "','" + Value3 + "','" + Value4 + "','" + Value5 + "','" + Value6 + "')";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }


        public Int32 UpdateAccount(Int64 ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6, string Value7)
        {
            string mquery = "update tbl_trn_VendorDetailMultiGrid set VendorRefNo='" + Value1 + "',NameofBank='" + Value2 + "',TypeOfAccount='" + Value3 + "',AccountNo='" + Value4 + "',MICRNo='" + Value5 + "',IFSCCode='" + Value6 + "',File_Bank_Solvency_Certificate='" + Value7 + "' where MasterId='" + ID + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 InsertAccount(string ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6, string Value7)
        {
            string mquery = "insert into tbl_trn_VendorDetailMultiGrid (VendorRefNo,Type,NameofBank,TypeOfAccount,AccountNo,MICRNo,IFSCCode,File_Bank_Solvency_Certificate)Values('" + ID + "','" + Value1 + "','" + Value2 + "','" + Value3 + "','" + Value4 + "','" + Value5 + "','" + Value6 + "','" + Value7 + "')";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }



        public Int32 UpdateProducts(Int64 ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6)
        {
            string mquery = "update tbl_trn_VendorDetailMultiGrid set VendorRefNo='" + Value1 + "',ProductNomenClature='" + Value2 + "',NatoGroup='" + Value3 + "',NatoClass='" + Value4 + "',ItemCode='" + Value5 + "',HSNCode='" + Value6 + "' where MasterId='" + ID + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 InsertProducts(string ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6)
        {
            string mquery = "insert into tbl_trn_VendorDetailMultiGrid (VendorRefNo,Type,ProductNomenClature,NatoGroup,NatoClass,ItemCode,HSNCode)Values('" + ID + "','" + Value1 + "','" + Value2 + "','" + Value3 + "','" + Value4 + "','" + Value5 + "','" + Value6 + "')";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }

        public Int32 UpdateTechnology(Int64 ID, string Value1, string Value2, string Value3, string Value4, string Value5)
        {
            string mquery = "update tbl_trn_VendorDetailMultiGrid set VendorRefNo='" + Value1 + "',ProductNomenClature1='" + Value2 + "',TechnologyLevel1='" + Value3 + "',TechnologyLevel2='" + Value4 + "',TechnologyLevel3='" + Value5 + "' where MasterId='" + ID + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 InsertTechnology(string ID, string Value1, string Value2, string Value3, string Value4, string Value5)
        {
            string mquery = "insert into tbl_trn_VendorDetailMultiGrid (VendorRefNo,Type,ProductNomenClature1,TechnologyLevel1,TechnologyLevel2,TechnologyLevel3)Values('" + ID + "','" + Value1 + "','" + Value2 + "','" + Value3 + "','" + Value4 + "','" + Value5 + "')";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }

        public Int32 UpdateRaw(Int64 ID, string Value1, string Value2, string Value3, string Value4, string Value5)
        {
            string mquery = "update tbl_trn_VendorDetailMultiGrid set VendorRefNo='" + Value1 + "',Items='" + Value2 + "',BasicRawMeterial='" + Value3 + "',SourceofMaterial='" + Value4 + "',Major_Raw_Material_Suppliers='" + Value5 + "' where MasterId='" + ID + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 InsertRaw(string ID, string Value1, string Value2, string Value3, string Value4, string Value5)
        {
            string mquery = "insert into tbl_trn_VendorDetailMultiGrid (VendorRefNo,Type,Items,BasicRawMeterial,SourceofMaterial,Major_Raw_Material_Suppliers)Values('" + ID + "','" + Value1 + "','" + Value2 + "','" + Value3 + "','" + Value4 + "','" + Value5 + "')";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }

        public Int32 Updateproducedprod(Int64 ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6, string value7)
        {
            string mquery = "update tbl_trn_VendorDetailMultiGrid set VendorRefNo='" + Value1 + "',Reputed_Customer='" + Value2 + "',Description='" + Value3 + "',SupplyNoDate='" + Value4 + "',OrderQuantity='" + Value5 + "'SuppliedQuantity='" + Value6 + "',Date2='" + value7 + "' where MasterId='" + ID + "'";
            return SqlHelper.Instance.ExecuteQuery(mquery);
        }
        public Int32 Insertproducedprod(string ID, string Value1, string Value2, string Value3, string Value4, string Value5, string Value6, string value7)
        {
            string mquery = "insert into tbl_trn_VendorDetailMultiGrid (VendorRefNo,Type,Reputed_Customer,Description,SupplyNoDate,OrderQuantity,SuppliedQuantity,Date2)Values('" + ID + "','" + Value1 + "','" + Value2 + "','" + Value3 + "','" + Value4 + "','" + Value5 + "','" + Value6 + "','" + value7 + "')";
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

        #region RequestInfoCard
        public string SaveRequestInfo(HybridDictionary hyLog, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveRequestInfo(hyLog, out _sysMsg, out _msg);
        }
        #endregion
    }
}
