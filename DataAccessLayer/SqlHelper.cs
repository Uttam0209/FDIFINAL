﻿using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Specialized;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Configuration;
using Encryption;

namespace DataAccessLayer
{
    public class SqlHelper
    {
        # region "Variables"
        private static SqlHelper instance;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        Database db;
        Cryptography objCrypto = new Cryptography();
        private string constring = string.Empty;
        # endregion
        # region "Constructor"
        private SqlHelper()
        {
            constring = objCrypto.DecryptData(ConfigurationManager.ConnectionStrings["connectiondb"].ConnectionString);
            db = new SqlDatabase(constring);
        }
        # endregion
        # region "static"
        public static SqlHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SqlHelper();
                }
                return instance;
            }
        }
        # endregion
        # region "Methods"
        public DataSet GetDataset(string Query)
        {
            DataSet dataset = new DataSet();
            using (SqlConnection connection = new SqlConnection(constring))
            {
                SqlDataAdapter sda = new SqlDataAdapter(Query, connection);
                sda.Fill(dataset);
                return dataset;
            }
        }
        public DataSet GetDataset(SqlCommand command)
        {
            DataSet dataset = new DataSet();
            using (SqlConnection connection = new SqlConnection(constring))
            {
                command.Connection = connection;
                SqlDataAdapter sda = new SqlDataAdapter(command);
                sda.Fill(dataset);
                return dataset;
            }
        }
        public DataTable GetExecuteData(string Query)
        {
            DbCommand dbCommand = db.GetSqlStringCommand(Query);
            dt = db.ExecuteDataSet(dbCommand).Tables[0];
            return dt;
        }
        public Int32 ExecuteQuery(string mQuery)
        {
            DbCommand dbCommand = db.GetSqlStringCommand(mQuery);
            return Convert.ToInt32(db.ExecuteNonQuery(dbCommand));


        }
        public String NoExecuteQuery(string mQuery)
        {
            DbCommand dbCommand = db.GetSqlStringCommand(mQuery);
            return Convert.ToString(db.ExecuteNonQuery(dbCommand));
        }
        public DataTable GetDataTable(string Query)
        {
            try
            {
                DbCommand dbcommand = db.GetSqlStringCommand(Query);
                return db.ExecuteDataSet(dbcommand).Tables[0];
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                return dt;
            }
        }
        # endregion
        #region "connection String"
        public bool VerifyDataBase()
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
        #region "Login"
        public string VerifyEmployee(HybridDictionary hyLogin, out string _msg, out string Defaultpage)
        {
            try
            {
                DbCommand _dbCmd = db.GetStoredProcCommand("sp_verify_Login");
                db.AddInParameter(_dbCmd, "@UserName", DbType.String, hyLogin["UserName"]);
                db.AddInParameter(_dbCmd, "@Password", DbType.String, hyLogin["Password"]);
                db.AddOutParameter(_dbCmd, "@CompanyRefNo", DbType.String, 50);
                db.AddOutParameter(_dbCmd, "@LType", DbType.String, 50);
                db.AddOutParameter(_dbCmd, "@Defaultpage", DbType.String, 50);
                db.ExecuteNonQuery(_dbCmd);
                string Comp_ID = db.GetParameterValue(_dbCmd, "@CompanyRefNo").ToString();
                string ID = db.GetParameterValue(_dbCmd, "@LType").ToString();
                Defaultpage = db.GetParameterValue(_dbCmd, "@Defaultpage").ToString();
                _msg = ID;
                return Comp_ID;

            }
            catch (SqlException ex)
            {
                _msg = "0";
                Defaultpage = "0";
                return "";
            }
            catch (Exception ex)
            {
                Defaultpage = "0";
                _msg = "0";
                return "";
            }
        }
        #endregion
        #region "Email and Company Name"
        public string VerifyEmailandCompany(string strEmail, string strCompany, out string _msg)
        {
            try
            {
                DbCommand _dbCmd = db.GetStoredProcCommand("sp_Verify_EmailIdandCompany");
                db.AddInParameter(_dbCmd, "@EmailId", DbType.String, strEmail);
                db.AddInParameter(_dbCmd, "@Company", DbType.String, strCompany);
                db.AddOutParameter(_dbCmd, "@LType", DbType.String, 50);
                db.ExecuteNonQuery(_dbCmd);
                string ID = db.GetParameterValue(_dbCmd, "@LType").ToString();
                _msg = ID;
                return _msg;

            }
            catch (SqlException ex)
            {
                _msg = "0";
                return "";
            }
            catch (Exception ex)
            {
                _msg = "0";
                return "";
            }
        }
        #endregion
        #region SaveCode
        public string SaveFDI(HybridDictionary HySave, out string _sysMsg, out string _msg)
        {
            // string mCurrentID = "";
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmdUser = db.GetStoredProcCommand("sp_Trn_FDI");
                    db.AddInParameter(cmdUser, "@MID", DbType.Int64, HySave["MID"]);
                    db.AddInParameter(cmdUser, "@CompanyRefNo", DbType.String, HySave["CompanyRefNo"]);
                    db.AddInParameter(cmdUser, "@NicCodeID", DbType.Int64, HySave["NicCodeID"]);
                    db.AddInParameter(cmdUser, "@InCaseOf", DbType.String, HySave["InCaseOf"]);
                    db.AddInParameter(cmdUser, "@ApprovalNo", DbType.String, HySave["ApprovalNo"]);
                    db.AddInParameter(cmdUser, "@ApprovalDate", DbType.Date, HySave["ApprovalDate"]);
                    db.AddInParameter(cmdUser, "@ForeignCompanyName", DbType.String, HySave["ForeignCompanyName"]);
                    db.AddInParameter(cmdUser, "@Address", DbType.String, HySave["Address"]);
                    db.AddInParameter(cmdUser, "@Country", DbType.Int64, HySave["Country"]);
                    db.AddInParameter(cmdUser, "@ZipCode", DbType.String, HySave["ZipCode"]);
                    db.AddInParameter(cmdUser, "@ForeignDefenceActivity", DbType.String, HySave["ForeignDefenceActivity"]);
                    db.AddInParameter(cmdUser, "@FDIValueType", DbType.String, HySave["FDIValueType"]);
                    db.AddInParameter(cmdUser, "@PeriodofReporting", DbType.String, HySave["PeriodofReporting"]);
                    db.AddInParameter(cmdUser, "@PeriodOfQuater", DbType.String, HySave["PeriodOfQuater"]);
                    db.AddInParameter(cmdUser, "@FYID", DbType.Int64, HySave["FYID"]);
                    db.AddInParameter(cmdUser, "@Currency", DbType.String, HySave["Currency"]);
                    db.AddInParameter(cmdUser, "@TotalFDIInFlow", DbType.String, HySave["TotalFDIInFlow"]);
                    db.AddInParameter(cmdUser, "@EquINRExchangeRate", DbType.String, HySave["EquINRExchangeRate"]);
                    db.AddInParameter(cmdUser, "@ExchangeTotalAmount", DbType.Decimal, HySave["ExchangeTotalAmount"]);
                    db.AddInParameter(cmdUser, "@SourceofInformation", DbType.String, HySave["SourceofInformation"]);
                    db.AddInParameter(cmdUser, "@DateofReceivingInformation", DbType.Date, HySave["DateofReceivingInformation"]);
                    db.AddInParameter(cmdUser, "@DocumentAttach", DbType.String, HySave["DocumentAttach"]);
                    db.AddInParameter(cmdUser, "@AuthencityofInformation", DbType.String, HySave["AuthencityofInformation"]);
                    db.AddInParameter(cmdUser, "@Remarks", DbType.String, HySave["Remarks"]);
                    db.ExecuteNonQuery(cmdUser, dbTran);
                    dbTran.Commit();
                    _msg = "Save";
                    _sysMsg = "Save";
                    return _msg;
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    _msg = ex.Message;
                    _sysMsg = ex.Message;
                    return "-1";
                }
                finally
                {
                    dbCon.Close();
                }
            }
        }
        public string SaveMasterCompany(HybridDictionary HyCompSave, out string _sysMsg, out string _msg)
        {
            string mCurrentID = "";
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_mst_company");
                    db.AddInParameter(cmd, "@CompanyID", DbType.Int64, HyCompSave["CompanyID"]);
                    db.AddInParameter(cmd, "@IsJointVenture", DbType.String, HyCompSave["IsJointVenture"]);
                    db.AddInParameter(cmd, "@CompanyName", DbType.String, HyCompSave["CompanyName"].ToString().Trim());
                    db.AddInParameter(cmd, "@Address", DbType.String, HyCompSave["Address"].ToString().Trim());
                    db.AddInParameter(cmd, "@State", DbType.Int64, HyCompSave["State"]);
                    db.AddInParameter(cmd, "@District", DbType.String, HyCompSave["District"]);
                    db.AddInParameter(cmd, "@Pincode", DbType.String, HyCompSave["Pincode"]);
                    db.AddInParameter(cmd, "@ContactPersonName", DbType.String, HyCompSave["ContactPersonName"]);
                    db.AddInParameter(cmd, "@ContactPersonEmailID", DbType.String, HyCompSave["ContactPersonEmailID"]);
                    db.AddInParameter(cmd, "@ContactPersonContactNo", DbType.Int64, HyCompSave["ContactPersonContactNo"]);
                    db.AddInParameter(cmd, "@GSTNo", DbType.String, HyCompSave["GSTNo"]);
                    db.AddInParameter(cmd, "@CINNo", DbType.String, HyCompSave["CINNo"]);
                    db.AddInParameter(cmd, "@PANNo", DbType.String, HyCompSave["PANNo"]);
                    db.AddInParameter(cmd, "@HSNo", DbType.String, HyCompSave["HSNo"]);
                    db.AddInParameter(cmd, "@IsDefenceActivity", DbType.String, HyCompSave["IsDefenceActivity"]);
                    db.AddInParameter(cmd, "@CEOEmail", DbType.String, HyCompSave["CEOEmail"]);
                    db.AddInParameter(cmd, "@CEOName", DbType.String, HyCompSave["CEOName"]);
                    db.AddInParameter(cmd, "@InterestedArea", DbType.String, HyCompSave["InterestedArea"]);
                    db.AddInParameter(cmd, "@MasterAllowed", DbType.String, HyCompSave["MasterAllowed"]);
                    db.AddInParameter(cmd, "@Role", DbType.String, HyCompSave["Role"]);
                    db.AddOutParameter(cmd, "@ReturnID", DbType.String, 20);
                    db.ExecuteNonQuery(cmd, dbTran);
                    mCurrentID = db.GetParameterValue(cmd, "@ReturnID").ToString();
                    dbTran.Commit();
                    _msg = "Save";
                    _sysMsg = "Save";
                    return mCurrentID;
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    _msg = ex.Message;
                    _sysMsg = ex.Message;
                    return "-1";
                }
                finally
                {
                    dbCon.Close();
                }
            }
        }
        public string SaveMasterComp(HybridDictionary hysavecomp, out string _sysMsg, out string _msg)
        {
            string mCurrentID = "";
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_CompanySuperAdminEntered");
                    db.AddInParameter(cmd, "@CompanyID", DbType.Int64, hysavecomp["CompanyID"]);
                    db.AddInParameter(cmd, "@CompanyName", DbType.String, hysavecomp["CompanyName"]);
                    db.AddInParameter(cmd, "@ContactPersonEmailID", DbType.String, hysavecomp["ContactPersonEmailID"]);
                    db.AddInParameter(cmd, "@InterestedArea", DbType.String, hysavecomp["InterestedArea"].ToString().Trim());
                    db.AddInParameter(cmd, "@MasterAllowed", DbType.String, hysavecomp["MasterAllowed"].ToString().Trim());
                    db.AddInParameter(cmd, "@Role", DbType.String, hysavecomp["Role"]);
                    db.AddOutParameter(cmd, "@ReturnID", DbType.String, 20);
                    db.ExecuteNonQuery(cmd, dbTran);
                    mCurrentID = db.GetParameterValue(cmd, "@ReturnID").ToString();
                    dbTran.Commit();
                    _msg = "Save";
                    _sysMsg = "Save";
                    return mCurrentID;
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    _msg = ex.Message;
                    _sysMsg = ex.Message;
                    return "-1";
                }
                finally
                {
                    dbCon.Close();
                }
            }
        }
        public string SaveFactoryComp(HybridDictionary hysavecomp, out string _sysMsg, out string _msg)
        {
            string mCurrentID = "";
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_CompanyEntered");
                    db.AddInParameter(cmd, "@FactoryID", DbType.Int64, hysavecomp["CompanyID"]);
                    db.AddInParameter(cmd, "@FactoryName", DbType.String, hysavecomp["CompanyName"]);
                    db.AddInParameter(cmd, "@FactoryEmailId", DbType.String, hysavecomp["ContactPersonEmailID"]);
                    db.AddInParameter(cmd, "@CompanyRefNo", DbType.String, hysavecomp["CompanyRefNo"].ToString().Trim());
                    db.AddInParameter(cmd, "@Role", DbType.String, hysavecomp["Role"]);
                    db.AddOutParameter(cmd, "@ReturnID", DbType.String, 20);
                    db.ExecuteNonQuery(cmd, dbTran);
                    mCurrentID = db.GetParameterValue(cmd, "@ReturnID").ToString();
                    dbTran.Commit();
                    _msg = "Save";
                    _sysMsg = mCurrentID;
                    return mCurrentID;
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    _msg = ex.Message;
                    _sysMsg = ex.Message;
                    return "-1";
                }
                finally
                {
                    dbCon.Close();
                }
            }
        }
        public string SaveUnitComp(HybridDictionary hysavecomp, out string _sysMsg, out string _msg)
        {
            string mCurrentID = "";
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_FactoryEntered");
                    db.AddInParameter(cmd, "@UnitID", DbType.Int64, hysavecomp["CompanyID"]);
                    db.AddInParameter(cmd, "@UnitName", DbType.String, hysavecomp["CompanyName"]);
                    db.AddInParameter(cmd, "@UnitEmailId", DbType.String, hysavecomp["ContactPersonEmailID"]);
                    db.AddInParameter(cmd, "@FactoryRefNo", DbType.String, hysavecomp["CompanyRefNo"].ToString().Trim());
                    db.AddInParameter(cmd, "@Role", DbType.String, hysavecomp["Role"]);
                    db.AddOutParameter(cmd, "@ReturnID", DbType.String, 20);
                    db.ExecuteNonQuery(cmd, dbTran);
                    mCurrentID = db.GetParameterValue(cmd, "@ReturnID").ToString();
                    dbTran.Commit();
                    _msg = "Save";
                    _sysMsg = mCurrentID;
                    return mCurrentID;
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    _msg = ex.Message;
                    _sysMsg = ex.Message;
                    return "-1";
                }
                finally
                {
                    dbCon.Close();
                }
            }
        }
        public string SaveMasterCategroyMenu(HybridDictionary HyCompSave, out string _sysMsg, out string _msg)
        {
            string mCurrentID = "";
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_mst_CompanySelectedCategory");
                    db.AddInParameter(cmd, "@CompCatRelationId", DbType.Int64, HyCompSave["CompCatRelationId"]);
                    db.AddInParameter(cmd, "@CompanyRefNo", DbType.String, HyCompSave["CompanyRefNo"]);
                    db.AddInParameter(cmd, "@MCategoryId", DbType.Int16, HyCompSave["MCategoryId"].ToString().Trim());
                    db.AddInParameter(cmd, "@SCategoryId", DbType.String, HyCompSave["SCategoryId"].ToString().Trim());
                    db.ExecuteNonQuery(cmd, dbTran);
                    dbTran.Commit();
                    _msg = "Save";
                    _sysMsg = "Save";
                    return "Save";
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    _msg = ex.Message;
                    _sysMsg = ex.Message;
                    return "-1";
                }
                finally
                {
                     _msg = "Save";
                    _sysMsg = "Save";
                    dbCon.Close();
                }
            }
        }
        #endregion
        #region UpdateCode
        public string UpdateLoginPassword(string NewPass, string OldPass, string User, string type)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_UpdateLoginPassword");
                    db.AddInParameter(cmd, "@CompanyRefNo", DbType.String, User);
                    db.AddInParameter(cmd, "@Password", DbType.String, NewPass);
                    db.AddInParameter(cmd, "@OldPass", DbType.String, OldPass);
                    db.AddInParameter(cmd, "@Type", DbType.String, type);
                    db.ExecuteNonQuery(cmd, dbTran);
                    dbTran.Commit();
                    return "true";
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    return ex.Message;
                }
            }
        }
        #endregion
        #region retriveCode
        public DataTable RetriveGridView(string ID)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_SearchFDIGrid");
                    db.AddInParameter(cmd, "@CompanyRefNo", DbType.String, ID);
                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                        dt.Load(dr);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable RetriveGridViewCompany(string ID, string FactoryRefNo, string UnitRefNo, string Purpose)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_SearchCompanyGrid");
                    db.AddInParameter(cmd, "@CompanyRefNo", DbType.String, ID);
                    db.AddInParameter(cmd, "@FactoryRefNo", DbType.String, FactoryRefNo);
                    db.AddInParameter(cmd, "@UnitRefNo", DbType.String, UnitRefNo);
                    db.AddInParameter(cmd, "@Purpose", DbType.String, Purpose);
                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                        dt.Load(dr);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable RetriveCountry(string text)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_country");
                    db.AddInParameter(cmd, "@CountryID", DbType.Int64, 0);
                    db.AddInParameter(cmd, "@CountryName", DbType.String, "");
                    db.AddInParameter(cmd, "@WorkCodeFor", DbType.String, text);
                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                        dt.Load(dr);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable RetriveState(string text)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_state");
                    db.AddInParameter(cmd, "@StateID", DbType.Int64, 0);
                    db.AddInParameter(cmd, "@StateName", DbType.String, "");
                    db.AddInParameter(cmd, "@WorkCodeFor", DbType.String, text);
                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                        dt.Load(dr);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable RetriveMasterData(Int64 Companyid, string strRefNo, string strRole, int MenuId, string strMenuUrl, string strInterestedAreaFlag, string strCriteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_GetDataOnCriteria");
                    db.AddInParameter(cmd, "@CompanyID", DbType.Int64, Companyid);
                    db.AddInParameter(cmd, "@RefNo", DbType.String, strRefNo);
                    db.AddInParameter(cmd, "@Role", DbType.String, strRole);
                    db.AddInParameter(cmd, "@MenuId", DbType.Int16, MenuId);
                    db.AddInParameter(cmd, "@MenuUrl", DbType.String, strMenuUrl);
                    db.AddInParameter(cmd, "@InterestedAreaFlag", DbType.String, strInterestedAreaFlag);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, strCriteria);
                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                        dt.Load(dr);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable RetriveMasterCategoryDate(Int64 CatID, string CatName, string SCatValue, string Criteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_MasterCategory");
                    db.AddInParameter(cmd, "@CatID", DbType.Int64, CatID);
                    db.AddInParameter(cmd, "@CatName", DbType.String, CatName);
                    db.AddInParameter(cmd, "@SCatValue", DbType.String, SCatValue);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, Criteria);
                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                        dt.Load(dr);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable RetriveMasterSubCategoryDate(Int64 SCatID, string SCatName, string PId, string Criteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_SubCategory");
                    db.AddInParameter(cmd, "@SCatID", DbType.Int64, SCatID);
                    db.AddInParameter(cmd, "@SCatName", DbType.String, SCatName);
                    db.AddInParameter(cmd, "@PId", DbType.String, PId);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, Criteria);
                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                        dt.Load(dr);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion
        #region DeleteCode
        public string DeleteRecord(string CompRefNo, string Criteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_DeleteMasterRecord");
                    db.AddInParameter(cmd, "@CompRefNo", DbType.String, CompRefNo);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, Criteria);
                    db.ExecuteNonQuery(cmd, dbTran);
                    dbTran.Commit();
                    return "true";
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    return ex.Message;
                }
            }
        }
        #endregion
        #region Search
        public DataTable SearchResult(string SearchText)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_MultipleKeywordSearchFDIGRID");
                    db.AddInParameter(cmd, "@str", DbType.String, SearchText);
                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                        dt.Load(dr);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable SearchResultCompany(string SearchText)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_MultipleKeywordSearchCompanyGRID");
                    db.AddInParameter(cmd, "@str", DbType.String, SearchText);
                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                        dt.Load(dr);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion
        #region "Dashboard"
        public DataTable RetriveAggregateValue(string function, string entity)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_AggregateValue");
                    db.AddInParameter(cmd, "@Function", DbType.String, function);
                    db.AddInParameter(cmd, "@Table", DbType.String, entity);

                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                        dt.Load(dr);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public DataTable RetriveAggregateValueWithParam(string function, string entity, string clmn, string val)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_AggregateValue");
                    db.AddInParameter(cmd, "@Function", DbType.String, function);
                    db.AddInParameter(cmd, "@Table", DbType.String, entity);
                    db.AddInParameter(cmd, "@Colmn", DbType.String, clmn);
                    db.AddInParameter(cmd, "@Value", DbType.String, val);

                    IDataReader dr = db.ExecuteReader(cmd);
                    DataTable dt = new DataTable();
                    if (dr != null)
                        dt.Load(dr);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion
    }
}
