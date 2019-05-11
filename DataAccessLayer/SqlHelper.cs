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
                    DbCommand cmd = db.GetStoredProcCommand("sp_UpdateCompany");
                    db.AddInParameter(cmd, "@CompanyID", DbType.Int64, HyCompSave["CompanyID"]);
                    db.AddInParameter(cmd, "@IsJointVenture", DbType.String, HyCompSave["IsJointVenture"]);
                    db.AddInParameter(cmd, "@CompanyName", DbType.String, HyCompSave["CompanyName"].ToString().Trim());
                    db.AddInParameter(cmd, "@Address", DbType.String, HyCompSave["Address"].ToString().Trim());
                    db.AddInParameter(cmd, "@State", DbType.Int64, HyCompSave["State"]);
                    db.AddInParameter(cmd, "@District", DbType.String, HyCompSave["District"]);
                    db.AddInParameter(cmd, "@Pincode", DbType.String, HyCompSave["Pincode"]);
                    db.AddInParameter(cmd, "@NodalOfficeRefNo", DbType.String, HyCompSave["NodalOfficeRefNo"]);
                    db.AddInParameter(cmd, "@ContactPersonEmailID", DbType.String, HyCompSave["ContactPersonEmailID"]);
                    db.AddInParameter(cmd, "@GSTNo", DbType.String, HyCompSave["GSTNo"]);
                    db.AddInParameter(cmd, "@CINNo", DbType.String, HyCompSave["CINNo"]);
                    db.AddInParameter(cmd, "@PANNo", DbType.String, HyCompSave["PANNo"]);
                    db.AddInParameter(cmd, "@IECode", DbType.String, HyCompSave["IECode"]);
                    db.AddInParameter(cmd, "@CEOName", DbType.String, HyCompSave["CEOName"]);
                    db.AddInParameter(cmd, "@CEOEmail", DbType.String, HyCompSave["CEOEmail"]);
                    db.AddInParameter(cmd, "@TelephoneNo", DbType.String, HyCompSave["TelephoneNo"]);
                    db.AddInParameter(cmd, "@FaxNo", DbType.String, HyCompSave["FaxNo"]);
                    db.AddInParameter(cmd, "@EmailID", DbType.String, HyCompSave["EmailID"]);
                    db.AddInParameter(cmd, "@Website", DbType.String, HyCompSave["Website"]);
                    db.AddInParameter(cmd, "@Startup", DbType.String, HyCompSave["Startup"]);
                    db.AddInParameter(cmd, "@DIPPNumber", DbType.String, HyCompSave["DIPPNumber"]);
                    db.AddInParameter(cmd, "@DIPPMobile", DbType.String, HyCompSave["DIPPMobile"]);

                    db.AddInParameter(cmd, "@MSME", DbType.String, HyCompSave["MSME"]);
                    db.AddInParameter(cmd, "@VAM", DbType.String, HyCompSave["VAM"]);
                    db.AddInParameter(cmd, "@Aadhar_Mobile", DbType.String, HyCompSave["Aadhar_Mobile"]);
                    db.AddInParameter(cmd, "@Facebook", DbType.String, HyCompSave["Facebook"]);
                    db.AddInParameter(cmd, "@Twitter", DbType.String, HyCompSave["Twitter"]);
                    db.AddInParameter(cmd, "@Linkedin", DbType.String, HyCompSave["Linkedin"]);
                    db.AddInParameter(cmd, "@Instagram", DbType.String, HyCompSave["Instagram"]);
                    db.AddInParameter(cmd, "@latitude", DbType.String, HyCompSave["latitude"]);
                    db.AddInParameter(cmd, "@longitude", DbType.String, HyCompSave["longitude"]);
                    db.AddInParameter(cmd, "@IsDefenceActivity", DbType.String, HyCompSave["IsDefenceActivity"]);
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
        #region "Save Master"
        public string SaveMasterComp(HybridDictionary hysavecomp, out string _sysMsg, out string _msg)
        {
            string mCurrentID = "";
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_InsertCompany");
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
        public string SaveMasterDivision(HybridDictionary hysaveDivision, out string _sysMsg, out string _msg)
        {
            string mCurrentID = "";
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_InsertDivision");
                    db.AddInParameter(cmd, "@FactoryID", DbType.Int64, hysaveDivision["CompanyID"]);
                    db.AddInParameter(cmd, "@FactoryName", DbType.String, hysaveDivision["CompanyName"]);
                    db.AddInParameter(cmd, "@FactoryEmailId", DbType.String, hysaveDivision["ContactPersonEmailID"]);
                    db.AddInParameter(cmd, "@CompanyRefNo", DbType.String, hysaveDivision["CompanyRefNo"].ToString().Trim());
                    db.AddInParameter(cmd, "@Role", DbType.String, hysaveDivision["Role"]);
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
        public string SaveMasterUnit(HybridDictionary hysaveUnit, out string _sysMsg, out string _msg)
        {
            string mCurrentID = "";
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_InsertUnit");
                    db.AddInParameter(cmd, "@UnitID", DbType.Int64, hysaveUnit["CompanyID"]);
                    db.AddInParameter(cmd, "@UnitName", DbType.String, hysaveUnit["CompanyName"]);
                    db.AddInParameter(cmd, "@UnitEmailId", DbType.String, hysaveUnit["ContactPersonEmailID"]);
                    db.AddInParameter(cmd, "@FactoryRefNo", DbType.String, hysaveUnit["CompanyRefNo"].ToString().Trim());
                    db.AddInParameter(cmd, "@Role", DbType.String, hysaveUnit["Role"]);
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
        #endregion

        public string SaveFactoryComp(HybridDictionary hysavecomp, out string _sysMsg, out string _msg)
        {
            string mCurrentID = "";
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_UpdateDivision");
                    db.AddInParameter(cmd, "@FactoryID", DbType.Int64, hysavecomp["CompanyID"]);
                    db.AddInParameter(cmd, "@FactoryName", DbType.String, hysavecomp["CompanyName"]);
                    db.AddInParameter(cmd, "@CompanyRefNo", DbType.String, hysavecomp["CompanyRefNo"].ToString().Trim());
                    db.AddInParameter(cmd, "@FactoryAddress", DbType.String, hysavecomp["FactoryAddress"].ToString().Trim());
                    db.AddInParameter(cmd, "@FactoryStateID", DbType.Int64, hysavecomp["FactoryStateID"]);
                    db.AddInParameter(cmd, "@FactoryPincode", DbType.String, hysavecomp["FactoryPincode"]);
                    db.AddInParameter(cmd, "@FactoryCEOName", DbType.String, hysavecomp["FactoryCEOName"]);
                    db.AddInParameter(cmd, "@FactoryCEOEmail", DbType.String, hysavecomp["FactoryCEOEmail"]);
                    db.AddInParameter(cmd, "@FactoryTelephoneNo", DbType.String, hysavecomp["FactoryTelephoneNo"]);
                    db.AddInParameter(cmd, "@FactoryFaxNo", DbType.String, hysavecomp["FactoryFaxNo"]);
                    db.AddInParameter(cmd, "@FactoryEmailID", DbType.String, hysavecomp["FactoryEmailID"]);
                    db.AddInParameter(cmd, "@FactoryWebsite", DbType.String, hysavecomp["FactoryWebsite"]);
                    db.AddInParameter(cmd, "@NodalOfficeRefNo", DbType.String, hysavecomp["NodalOfficeRefNo"]);
                    db.AddInParameter(cmd, "@FactoryNodalOfficerEmailId", DbType.String, hysavecomp["FactoryNodalOfficerEmailId"]);
                    db.AddInParameter(cmd, "@FactoryGSTNo", DbType.String, hysavecomp["FactoryGSTNo"]);
                    db.AddInParameter(cmd, "@FactoryCINNo", DbType.String, hysavecomp["FactoryCINNo"]);
                    db.AddInParameter(cmd, "@FactoryPANNo", DbType.String, hysavecomp["FactoryPANNo"]);
                    db.AddInParameter(cmd, "@FactoryIECode", DbType.String, hysavecomp["FactoryIECode"]);
                    db.AddInParameter(cmd, "@FactoryFacebook", DbType.String, hysavecomp["FactoryFacebook"]);
                    db.AddInParameter(cmd, "@FactoryTwitter", DbType.String, hysavecomp["FactoryTwitter"]);
                    db.AddInParameter(cmd, "@FactoryLinkedin", DbType.String, hysavecomp["FactoryLinkedin"]);
                    db.AddInParameter(cmd, "@FactoryInstagram", DbType.String, hysavecomp["FactoryInstagram"]);
                    db.AddInParameter(cmd, "@Factorylatitude", DbType.String, hysavecomp["Factorylatitude"]);
                    db.AddInParameter(cmd, "@Factorylongitude", DbType.String, hysavecomp["Factorylongitude"]);
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
        public string SaveMasterNodal(HybridDictionary hySaveNodal, out string _sysMsg, out string _msg)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                string mCurrentID = "";
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_NodelPerson");
                    db.AddInParameter(cmd, "@NodalOfficerID", DbType.Int64, hySaveNodal["NodalOfficerID"]);
                    db.AddInParameter(cmd, "@NodalOfficerRefNo", DbType.String, hySaveNodal["NodalOfficerRefNo"]);
                    db.AddInParameter(cmd, "@NodalEmpCode", DbType.String, hySaveNodal["NodalEmpCode"]);
                    db.AddInParameter(cmd, "@NodalOficerName", DbType.String, hySaveNodal["NodalOficerName"]);
                    db.AddInParameter(cmd, "@NodalOfficerDesignation", DbType.Int16, hySaveNodal["NodalOfficerDesignation"].ToString().Trim());
                    db.AddInParameter(cmd, "@NodalOfficerEmail", DbType.String, hySaveNodal["NodalOfficerEmail"]);
                    db.AddInParameter(cmd, "@NodalOfficerMobile", DbType.String, hySaveNodal["NodalOfficerMobile"]);
                    db.AddInParameter(cmd, "@NodalOfficerTelephone", DbType.String, hySaveNodal["NodalOfficerTelephone"]);
                    db.AddInParameter(cmd, "@NodalOfficerFax", DbType.String, hySaveNodal["NodalOfficerFax"].ToString().Trim());
                    db.AddInParameter(cmd, "@CompanyRefNo", DbType.String, hySaveNodal["CompanyRefNo"].ToString().Trim());
                    db.AddInParameter(cmd, "@Type", DbType.String, hySaveNodal["Type"].ToString().Trim());
                    db.AddInParameter(cmd, "@IsNodalOfficer", DbType.String, hySaveNodal["IsNodalOfficer"].ToString().Trim());
                    db.AddOutParameter(cmd, "@ReturnID", DbType.String, 20);
                    db.ExecuteNonQuery(cmd, dbTran);
                    mCurrentID = db.GetParameterValue(cmd, "@ReturnID").ToString();
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
        public string SaveCodeProduct(HybridDictionary hyProduct, DataTable dt, out string _sysMsg, out string _msg, string Criteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                string mCurrentID = "";
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_mst_mainProduct");
                    db.AddInParameter(cmd, "@ProductID", DbType.Int64, hyProduct["ProductID"]);
                    db.AddInParameter(cmd, "@ProductRefNo", DbType.String, hyProduct["ProductRefNo"]);
                    db.AddInParameter(cmd, "@CompanyRefNo", DbType.String, hyProduct["CompanyRefNo"]);
                    db.AddInParameter(cmd, "@OEMPartNumber", DbType.String, hyProduct["OEMPartNumber"].ToString().Trim());
                    db.AddInParameter(cmd, "@DPSUPartNumber", DbType.String, hyProduct["DPSUPartNumber"]);
                    db.AddInParameter(cmd, "@EndUserPartNumber", DbType.String, hyProduct["EndUserPartNumber"]);
                    db.AddInParameter(cmd, "@HSNCode", DbType.String, hyProduct["HSNCode"]);
                    db.AddInParameter(cmd, "@NatoCode", DbType.String, hyProduct["NatoCode"].ToString().Trim());
                    db.AddInParameter(cmd, "@ERPRefNo", DbType.String, hyProduct["ERPRefNo"].ToString().Trim());
                    db.AddInParameter(cmd, "@NomenclatureOfMainSystem", DbType.Int64, hyProduct["NomenclatureOfMainSystem"].ToString().Trim());
                    db.AddInParameter(cmd, "@ProductLevel1", DbType.Int64, hyProduct["ProductLevel1"]);
                    db.AddInParameter(cmd, "@ProductLevel2", DbType.Int16, hyProduct["ProductLevel2"].ToString().Trim());
                    db.AddInParameter(cmd, "@ProductDescription", DbType.String, hyProduct["ProductDescription"]);
                    db.AddInParameter(cmd, "@TechnologyLevel1", DbType.Int64, hyProduct["TechnologyLevel1"]);
                    db.AddInParameter(cmd, "@TechnologyLevel2", DbType.Int64, hyProduct["TechnologyLevel2"]);
                    db.AddInParameter(cmd, "@EndUser", DbType.String, hyProduct["EndUser"].ToString().Trim());
                    db.AddInParameter(cmd, "@IsIndeginized", DbType.String, hyProduct["IsIndeginized"].ToString().Trim());
                    db.AddInParameter(cmd, "@ManufactureName", DbType.String, hyProduct["ManufactureName"]);
                    db.AddInParameter(cmd, "@Platform", DbType.Int64, hyProduct["Platform"].ToString().Trim());
                    db.AddInParameter(cmd, "@PurposeofProcurement", DbType.Int64, hyProduct["PurposeofProcurement"]);
                    db.AddInParameter(cmd, "@ProductRequirment", DbType.Int64, hyProduct["ProductRequirment"]);
                    db.AddInParameter(cmd, "@SearchKeyword", DbType.String, hyProduct["SearchKeyword"]);
                    db.AddInParameter(cmd, "@DPSUServices", DbType.String, hyProduct["DPSUServices"].ToString().Trim());
                    db.AddInParameter(cmd, "@Remarks", DbType.String, hyProduct["Remarks"].ToString().Trim());
                    db.AddInParameter(cmd, "@Estimatequantity", DbType.String, hyProduct["Estimatequantity"].ToString().Trim());
                    db.AddInParameter(cmd, "@EstimatePriceLLP", DbType.String, hyProduct["EstimatePriceLLP"]);
                    db.AddInParameter(cmd, "@TenderStatus", DbType.String, hyProduct["TenderStatus"].ToString().Trim());
                    db.AddInParameter(cmd, "@TenderSubmition", DbType.String, hyProduct["TenderSubmition"].ToString().Trim());
                    db.AddInParameter(cmd, "@TenderFillDate", DbType.Date, hyProduct["TenderFillDate"]);
                    db.AddInParameter(cmd, "@TenderUrl", DbType.String, hyProduct["TenderUrl"]);
                    db.AddInParameter(cmd, "@NodelDetail", DbType.String, hyProduct["NodelDetail"]);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, Criteria);
                    db.AddOutParameter(cmd, "@ReturnID", DbType.String, 20);
                    db.ExecuteNonQuery(cmd, dbTran);
                    mCurrentID = db.GetParameterValue(cmd, "@ReturnID").ToString();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DbCommand dbcom1 = db.GetStoredProcCommand("sp_trn_Image");
                        db.AddInParameter(dbcom1, "@ImageID", DbType.Int64, dt.Rows[i]["ImageID"]);
                        db.AddInParameter(dbcom1, "@ProductRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom1, "@ImageName", DbType.String, dt.Rows[i]["ImageName"]);
                        db.AddInParameter(dbcom1, "@ImageType", DbType.String, dt.Rows[i]["ImageType"]);
                        db.AddInParameter(dbcom1, "@ImageActualSize", DbType.Int64, dt.Rows[i]["ImageActualSize"]);
                        db.AddInParameter(dbcom1, "@Priority", DbType.String, dt.Rows[i]["Priority"]);
                        db.AddInParameter(dbcom1, "@CompanyRefNo", DbType.String, dt.Rows[i]["CompanyRefNo"]);
                        db.ExecuteNonQuery(dbcom1, dbTran);
                    }
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
        public string SaveCompDesignation(HybridDictionary hysavecomp, out string _sysMsg, out string _msg)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                string mCurrentID = "";
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_InsertDesignation");
                    db.AddInParameter(cmd, "@DesignationID", DbType.Int64, hysavecomp["DesignationId"]);
                    db.AddInParameter(cmd, "@CompanyRefNo", DbType.String, hysavecomp["CompanyRefNo"]);
                    db.AddInParameter(cmd, "@DesignationRefNo", DbType.String, hysavecomp["DesignationRefNo"]);
                    db.AddInParameter(cmd, "@Designation", DbType.String, hysavecomp["Designation"].ToString().Trim());
                    db.AddOutParameter(cmd, "@ReturnID", DbType.String, 20);
                    db.ExecuteNonQuery(cmd, dbTran);
                    mCurrentID = db.GetParameterValue(cmd, "@ReturnID").ToString();
                    dbTran.Commit();
                    _msg = "Save";
                    _sysMsg = "Save";
                    return _sysMsg;
                }

                catch (Exception ex)
                {
                    dbTran.Rollback();
                    _msg = ex.Message;
                    _sysMsg = "";
                    return _sysMsg;
                }
                finally
                {
                    dbCon.Close();
                }
            }
        }
        public string SaveImages(DataTable dt, out string _sysMsg, out string _msg, string Criteria)
        {
            using (DbConnection dbcon = db.CreateConnection())
            {
                dbcon.Open();
                DbTransaction dbTran = dbcon.BeginTransaction();
                try
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DbCommand dbcom1 = db.GetStoredProcCommand("sp_trn_Image");
                        db.AddInParameter(dbcom1, "@ImageID", DbType.Int64, dt.Rows[i]["ImageID"]);
                        db.AddInParameter(dbcom1, "@ImageName", DbType.String, dt.Rows[i]["ImageName"]);
                        db.AddInParameter(dbcom1, "@ImageType", DbType.String, dt.Rows[i]["ImageType"]);
                        db.AddInParameter(dbcom1, "@ImageActualSize", DbType.Int64, dt.Rows[i]["ImageActualSize"]);
                        db.AddInParameter(dbcom1, "@Priority", DbType.String, dt.Rows[i]["Priority"]);
                        db.AddInParameter(dbcom1, "@ProductRefNo", DbType.String, dt.Rows[i]["ProductRefNo"]);
                        db.AddInParameter(dbcom1, "@CompanyRefNo", DbType.String, dt.Rows[i]["CompanyRefNo"]);
                        db.ExecuteNonQuery(dbcom1, dbTran);
                    }
                    dbTran.Commit();
                    _msg = "Save";
                    _sysMsg = "Save";
                    return "Save";
                }
                catch (SqlException ex)
                {
                    dbTran.Rollback();
                    _msg = ex.Message;
                    _sysMsg = ex.Message;
                    return "-1";
                }
                finally
                {
                    dbcon.Close();
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


        public DataTable RetriveAllNodalOfficer(string RefNo, string Role)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_GetNodalCompDivUnit");
                    db.AddInParameter(cmd, "@RefNo", DbType.String, RefNo);
                    db.AddInParameter(cmd, "@Role", DbType.String, Role);
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
        public DataTable RetriveAllCompany(string RefNo, string Role)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_GetCompDivUnit");
                    db.AddInParameter(cmd, "@RefNo", DbType.String, RefNo);
                    db.AddInParameter(cmd, "@Role", DbType.String, Role);
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
        public DataTable RetriveProductCode(string CompanyRefNo, string ProdRefNo, string Purpose)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_RetriveProduct");
                   db.AddInParameter(cmd, "@CompanyRefNo", DbType.String, CompanyRefNo);
                    db.AddInParameter(cmd, "@ProductRefNo", DbType.String, ProdRefNo);
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
        public DataTable RetriveMasterCategoryDate(Int64 CatID, string CatName, string SCatValue, string Flag, string LavelActive, string Criteria)
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
                    db.AddInParameter(cmd, "@Flag", DbType.String, Flag);
                    db.AddInParameter(cmd, "@Active", DbType.String, LavelActive);
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
        public DataTable RetriveMasterSubCategoryDate(Int64 SCatID, string SCatName, string PId, string Criteria, string CompRefNo)
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
                    db.AddInParameter(cmd, "@CompRefNo", DbType.String, CompRefNo);
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
