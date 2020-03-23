using System;
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
        public string VerifyEmail(HybridDictionary hyLogin, out string _msg, out string _mp)
        {
            try
            {
                DbCommand _dbCmd = db.GetStoredProcCommand("sp_verify_LogEmail");
                db.AddInParameter(_dbCmd, "@UserName", DbType.String, hyLogin["UserName"]);
                db.AddOutParameter(_dbCmd, "@Salt", DbType.String, 500);
                db.AddOutParameter(_dbCmd, "@Password", DbType.String, 500);
                db.ExecuteNonQuery(_dbCmd);
                string Comp_ID = db.GetParameterValue(_dbCmd, "@Salt").ToString();
                string ID = db.GetParameterValue(_dbCmd, "@Password").ToString();
                _msg = ID;
                _mp = Comp_ID;
                return Comp_ID;

            }
            catch (SqlException ex)
            {
                _msg = "0";
                _mp = "";
                return "";
            }
        }
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
        }
        public string VerifyVendorEmployee(HybridDictionary hyLogin, out string _msg, out string Defaultpage)
        {
            try
            {
                DbCommand _dbCmd = db.GetStoredProcCommand("sp_Verify_VendorLogin");
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
        public string InsertLogProd(HybridDictionary hySaveProdInfo, out string _sysMsg, out string _msg)
        {
            using (DbConnection dbcon = db.CreateConnection())
            {
                dbcon.Open();
                DbTransaction dbTran = dbcon.BeginTransaction();
                try
                {
                    DbCommand dbcom = db.GetStoredProcCommand("sp_LogProduct");
                    db.AddInParameter(dbcom, "@ProdRefNo", DbType.String, hySaveProdInfo["ProdRefNo"]);
                    db.AddInParameter(dbcom, "@ProductChanges", DbType.String, hySaveProdInfo["ProductChanges"]);
                    db.AddInParameter(dbcom, "@ChangesBy", DbType.String, hySaveProdInfo["ChangesBy"]);
                    db.AddInParameter(dbcom, "@Mailsend", DbType.String, hySaveProdInfo["Mailsend"]);
                    db.AddInParameter(dbcom, "@Status", DbType.String, hySaveProdInfo["Status"]);
                    db.ExecuteNonQuery(dbcom, dbTran);
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
        public string SaveLog(HybridDictionary hyLog, out string _sysMsg, out string _msg)
        {
            using (DbConnection dbcon = db.CreateConnection())
            {
                dbcon.Open();
                DbTransaction dbTran = dbcon.BeginTransaction();
                try
                {
                    DbCommand dbcom = db.GetStoredProcCommand("sp_Log");
                    db.AddInParameter(dbcom, "@UserId", DbType.String, hyLog["UserId"]);
                    db.AddInParameter(dbcom, "@IPAddress", DbType.String, hyLog["IPAddress"]);
                    db.AddInParameter(dbcom, "@SystemName", DbType.String, hyLog["SystemName"]);
                    db.AddInParameter(dbcom, "@Form", DbType.String, hyLog["Form"]);
                    db.AddInParameter(dbcom, "@Activity", DbType.String, hyLog["Activity"]);
                    db.AddInParameter(dbcom, "@LoginDate", DbType.String, hyLog["LoginDate"]);
                    db.AddInParameter(dbcom, "@LoginTime", DbType.String, hyLog["LoginTime"]);
                    db.ExecuteNonQuery(dbcom, dbTran);
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
        public string SaveLogoutstatus(HybridDictionary hyLogOut, out string _sysMsg, out string _msg)
        {
            using (DbConnection dbcon = db.CreateConnection())
            {
                dbcon.Open();
                DbTransaction dbTran = dbcon.BeginTransaction();
                try
                {
                    DbCommand dbcom = db.GetStoredProcCommand("sp_LogOutStatus");
                    db.AddInParameter(dbcom, "@LoginUser", DbType.String, hyLogOut["LoginUser"]);
                    db.AddInParameter(dbcom, "@IsLogedIn", DbType.String, hyLogOut["IsLogedIn"]);
                    db.AddInParameter(dbcom, "@IsLogedOutTime", DbType.DateTime, hyLogOut["IsLogedOutTime"]);
                    db.ExecuteNonQuery(dbcom, dbTran);
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
                    db.AddInParameter(cmd, "@CreatedBy", DbType.String, hysavecomp["CreatedBy"]);
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
                    db.AddInParameter(cmd, "@CreatedBy", DbType.String, hysaveDivision["CreatedBy"]);
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
                    db.AddInParameter(cmd, "@CreatedBy", DbType.String, hysaveUnit["CreatedBy"]);
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
        public string SaveUnitComp(HybridDictionary hysaveunit, out string _sysMsg, out string _msg)
        {
            string mCurrentID = "";
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_UpdateUnit");
                    db.AddInParameter(cmd, "@UnitID", DbType.Int64, hysaveunit["CompanyID"]);
                    db.AddInParameter(cmd, "@UnitName", DbType.String, hysaveunit["CompanyName"]);
                    db.AddInParameter(cmd, "@FactoryRefNo", DbType.String, hysaveunit["CompanyRefNo"].ToString().Trim());
                    db.AddInParameter(cmd, "@UnitAddress", DbType.String, hysaveunit["UnitAddress"].ToString().Trim());
                    db.AddInParameter(cmd, "@UnitStateID", DbType.Int64, hysaveunit["UnitStateID"]);
                    db.AddInParameter(cmd, "@UnitPincode", DbType.String, hysaveunit["UnitPincode"]);
                    db.AddInParameter(cmd, "@UnitCEOName", DbType.String, hysaveunit["UnitCEOName"]);
                    db.AddInParameter(cmd, "@UnitCEOEmail", DbType.String, hysaveunit["UnitCEOEmail"]);
                    db.AddInParameter(cmd, "@UnitTelephoneNo", DbType.String, hysaveunit["UnitTelephoneNo"]);
                    db.AddInParameter(cmd, "@UnitFaxNo", DbType.String, hysaveunit["UnitFaxNo"]);
                    db.AddInParameter(cmd, "@UnitEmailID", DbType.String, hysaveunit["UnitEmailID"]);
                    db.AddInParameter(cmd, "@UnitWebsite", DbType.String, hysaveunit["UnitWebsite"]);
                    db.AddInParameter(cmd, "@NodalOfficeRefNo", DbType.String, hysaveunit["NodalOfficeRefNo"]);
                    db.AddInParameter(cmd, "@UnitNodalOfficerEmailId", DbType.String, hysaveunit["UnitNodalOfficerEmailId"]);
                    db.AddInParameter(cmd, "@UnitFacebook", DbType.String, hysaveunit["UnitFacebook"]);
                    db.AddInParameter(cmd, "@UnitTwitter", DbType.String, hysaveunit["UnitTwitter"]);
                    db.AddInParameter(cmd, "@UnitLinkedin", DbType.String, hysaveunit["UnitLinkedin"]);
                    db.AddInParameter(cmd, "@UnitInstagram", DbType.String, hysaveunit["UnitInstagram"]);
                    db.AddInParameter(cmd, "@Unitlatitude", DbType.String, hysaveunit["Unitlatitude"]);
                    db.AddInParameter(cmd, "@Unitlongitude", DbType.String, hysaveunit["Unitlongitude"]);
                    db.AddInParameter(cmd, "@Role", DbType.String, hysaveunit["Role"]);
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
                    db.AddInParameter(cmd, "@CreatedBy", DbType.String, HyCompSave["CreatedBy"].ToString().Trim());
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
                    db.AddInParameter(cmd, "@IsLoginActive", DbType.String, hySaveNodal["IsLoginActive"].ToString().Trim());
                    db.AddInParameter(cmd, "@CreatedBy", DbType.String, hySaveNodal["CreatedBy"].ToString().Trim());
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
        public string SaveCodeProduct(HybridDictionary hyProduct, DataTable dt, DataTable dtProdInfo, DataTable dtEstimateQuantity, DataTable dtFIIGNo, out string _sysMsg, out string _msg, string Criteria)
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
                    db.AddInParameter(cmd, "@ProductLevel1", DbType.Int64, hyProduct["ProductLevel1"]);
                    db.AddInParameter(cmd, "@ProductLevel2", DbType.Int64, hyProduct["ProductLevel2"]);
                    db.AddInParameter(cmd, "@ProductLevel3", DbType.Int64, hyProduct["ProductLevel3"]);
                    db.AddInParameter(cmd, "@ProductDescription", DbType.String, hyProduct["ProductDescription"]);
                    db.AddInParameter(cmd, "@NSCCode", DbType.String, hyProduct["NSCCode"]);
                    db.AddInParameter(cmd, "@NIINCode", DbType.String, hyProduct["NIINCode"]);
                    db.AddInParameter(cmd, "@OEMPartNumber", DbType.String, hyProduct["OEMPartNumber"].ToString().Trim());
                    db.AddInParameter(cmd, "@OEMName", DbType.String, hyProduct["OEMName"].ToString().Trim());
                    db.AddInParameter(cmd, "@OEMCountry", DbType.Int64, hyProduct["OEMCountry"].ToString().Trim());
                    db.AddInParameter(cmd, "@DPSUPartNumber", DbType.String, hyProduct["DPSUPartNumber"]);
                    db.AddInParameter(cmd, "@HSCode", DbType.Int64, hyProduct["HSCode"]);
                    db.AddInParameter(cmd, "@HSNCode", DbType.String, hyProduct["HSNCode"]);
                    db.AddInParameter(cmd, "@HSChapter", DbType.Int64, hyProduct["HSChapter"]);
                    db.AddInParameter(cmd, "@HSNCodeLevel1", DbType.Int64, hyProduct["HSNCodeLevel1"]);
                    db.AddInParameter(cmd, "@HSNCodeLevel2", DbType.Int64, hyProduct["HSNCodeLevel2"]);
                    db.AddInParameter(cmd, "@HSNCodeLevel3", DbType.Int64, hyProduct["HSNCodeLevel3"]);
                    db.AddInParameter(cmd, "@HsCode4digit", DbType.String, hyProduct["HsCode4digit"]);
                    db.AddInParameter(cmd, "@HsnCode8digit", DbType.String, hyProduct["HsnCode8digit"]);
                    db.AddInParameter(cmd, "@EndUserPartNumber", DbType.String, hyProduct["EndUserPartNumber"]);
                    db.AddInParameter(cmd, "@EndUser", DbType.String, hyProduct["EndUser"]);
                    db.AddInParameter(cmd, "@Platform", DbType.Int64, hyProduct["Platform"]);
                    db.AddInParameter(cmd, "@NomenclatureOfMainSystem", DbType.Int64, hyProduct["NomenclatureOfMainSystem"]);
                    db.AddInParameter(cmd, "@TechnologyLevel1", DbType.Int64, hyProduct["TechnologyLevel1"]);
                    db.AddInParameter(cmd, "@TechnologyLevel2", DbType.Int64, hyProduct["TechnologyLevel2"]);
                    db.AddInParameter(cmd, "@TechnologyLevel3", DbType.Int64, hyProduct["TechnologyLevel3"]);
                    db.AddInParameter(cmd, "@SearchKeyword", DbType.String, hyProduct["SearchKeyword"]);
                    db.AddInParameter(cmd, "@IsIndeginized", DbType.String, hyProduct["IsIndeginized"].ToString().Trim());
                    db.AddInParameter(cmd, "@ManufactureName", DbType.String, hyProduct["ManufactureName"]);
                    db.AddInParameter(cmd, "@ManufactureAddress", DbType.String, hyProduct["ManufactureAddress"]);
                    db.AddInParameter(cmd, "@YearofIndiginization", DbType.Int64, hyProduct["YearofIndiginization"]);
                    db.AddInParameter(cmd, "@IsProductImported", DbType.String, hyProduct["IsProductImported"]);
                    db.AddInParameter(cmd, "@YearofImport", DbType.String, hyProduct["YearofImport"]);
                    db.AddInParameter(cmd, "@YearofImportRemarks", DbType.String, hyProduct["YearofImportRemarks"]);
                    db.AddInParameter(cmd, "@ItemDescriptionPDFFile", DbType.String, hyProduct["ItemDescriptionPDFFile"]);
                    db.AddInParameter(cmd, "@FeatursandDetail", DbType.String, hyProduct["FeatursandDetail"]);
                    db.AddInParameter(cmd, "@ItemSpecification", DbType.String, hyProduct["ItemSpecification"]);
                    db.AddInParameter(cmd, "@AdditionalDetail", DbType.String, hyProduct["AdditionalDetail"]);
                    db.AddInParameter(cmd, "@PurposeofProcurement", DbType.String, hyProduct["PurposeofProcurement"]);
                    db.AddInParameter(cmd, "@ProcurmentCategoryRemark", DbType.String, hyProduct["ProcurmentCategoryRemark"]);
                    db.AddInParameter(cmd, "@QAAgency", DbType.String, hyProduct["QAAgency"].ToString().Trim());
                    db.AddInParameter(cmd, "@QAReamrks", DbType.String, hyProduct["QAReamrks"].ToString().Trim());
                    db.AddInParameter(cmd, "@Testing", DbType.String, hyProduct["Testing"].ToString().Trim());
                    db.AddInParameter(cmd, "@TestingRemarks", DbType.String, hyProduct["TestingRemarks"].ToString().Trim());
                    db.AddInParameter(cmd, "@Certification", DbType.String, hyProduct["Certification"].ToString().Trim());
                    db.AddInParameter(cmd, "@CertificationRemark", DbType.String, hyProduct["CertificationRemark"].ToString().Trim());
                    db.AddInParameter(cmd, "@DPSUServices", DbType.String, hyProduct["DPSUServices"].ToString().Trim());
                    db.AddInParameter(cmd, "@Remarks", DbType.String, hyProduct["Remarks"].ToString().Trim());
                    db.AddInParameter(cmd, "@FinancialSupport", DbType.String, hyProduct["FinancialSupport"].ToString().Trim());
                    db.AddInParameter(cmd, "@FinancialRemark", DbType.String, hyProduct["FinancialRemark"].ToString().Trim());
                    db.AddInParameter(cmd, "@TenderStatus", DbType.String, hyProduct["TenderStatus"].ToString().Trim());
                    db.AddInParameter(cmd, "@TenderSubmition", DbType.String, hyProduct["TenderSubmition"].ToString().Trim());
                    db.AddInParameter(cmd, "@TenderFillDate", DbType.Date, hyProduct["TenderFillDate"]);
                    db.AddInParameter(cmd, "@TenderUrl", DbType.String, hyProduct["TenderUrl"]);
                    db.AddInParameter(cmd, "@EOIStatus", DbType.String, hyProduct["EOIStatus"].ToString().Trim());
                    db.AddInParameter(cmd, "@EOISubmition", DbType.String, hyProduct["EOISubmition"].ToString().Trim());
                    db.AddInParameter(cmd, "@EOIFillDate", DbType.Date, hyProduct["EOIFillDate"]);
                    db.AddInParameter(cmd, "@EOIURL", DbType.String, hyProduct["EOIURL"]);
                    db.AddInParameter(cmd, "@NodelDetail", DbType.Int16, hyProduct["NodelDetail"]);
                    db.AddInParameter(cmd, "@NodalDetail2", DbType.Int16, hyProduct["NodalDetail2"]);
                    db.AddInParameter(cmd, "@Role", DbType.String, hyProduct["Role"]);
                    db.AddInParameter(cmd, "@CreatedBy", DbType.String, hyProduct["CreatedBy"]);
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
                    for (int j = 0; j < dtProdInfo.Rows.Count; j++)
                    {
                        DbCommand dbcom2 = db.GetStoredProcCommand("sp_trn_ProductInformation");
                        db.AddInParameter(dbcom2, "@ProdInfoId", DbType.Int64, dtProdInfo.Rows[j]["ProdInfoId"]);
                        db.AddInParameter(dbcom2, "@ProductRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom2, "@NameOfSpec", DbType.String, dtProdInfo.Rows[j]["Length"]);
                        db.AddInParameter(dbcom2, "@Value", DbType.String, dtProdInfo.Rows[j]["Value"]);
                        db.AddInParameter(dbcom2, "@Unit", DbType.String, dtProdInfo.Rows[j]["ProductUnit"]);
                        db.ExecuteNonQuery(dbcom2, dbTran);
                    }
                    for (int k = 0; k < dtEstimateQuantity.Rows.Count; k++)
                    {
                        DbCommand dbcom3 = db.GetStoredProcCommand("sp_trn_ProdQtyPrice");
                        db.AddInParameter(dbcom3, "@ProdQtyPriceId", DbType.Int64, dtEstimateQuantity.Rows[k]["ProdQtyPriceId"]);
                        db.AddInParameter(dbcom3, "@ProductRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom3, "@Year", DbType.Int64, dtEstimateQuantity.Rows[k]["Year"]);
                        db.AddInParameter(dbcom3, "@FYear", DbType.String, dtEstimateQuantity.Rows[k]["FYear"]);
                        db.AddInParameter(dbcom3, "@EstimatedQty", DbType.String, dtEstimateQuantity.Rows[k]["EstimatedQty"]);
                        db.AddInParameter(dbcom3, "@Unit", DbType.String, dtEstimateQuantity.Rows[k]["Unit"]);
                        db.AddInParameter(dbcom3, "@EstimatedPrice", DbType.String, dtEstimateQuantity.Rows[k]["EstimatedPrice"]);
                        db.ExecuteNonQuery(dbcom3, dbTran);
                    }
                    for (int L = 0; L < dtFIIGNo.Rows.Count; L++)
                    {
                        DbCommand dbcom4 = db.GetStoredProcCommand("sp_trn_FiiGSave");
                        db.AddInParameter(dbcom4, "@FiigID", DbType.Int64, dtFIIGNo.Rows[L]["FiigID"]);
                        db.AddInParameter(dbcom4, "@ProductRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom4, "@SCategoryName", DbType.String, dtFIIGNo.Rows[L]["SCategoryName"]);
                        db.AddInParameter(dbcom4, "@Remarks", DbType.String, dtFIIGNo.Rows[L]["Remarks"]);
                        db.AddInParameter(dbcom4, "@Remarks2", DbType.String, dtFIIGNo.Rows[L]["Remarks2"]);
                        db.AddInParameter(dbcom4, "@Remarks3", DbType.String, dtFIIGNo.Rows[L]["Remarks3"]);
                        db.ExecuteNonQuery(dbcom4, dbTran);
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
        public string UpdateCodeProduct(HybridDictionary HyUpdateProd, out string _sysMsg, out string _msg)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_mst_updatemainProduct");
                    db.AddInParameter(cmd, "@ProductRefNo", DbType.String, HyUpdateProd["ProductRefNo"]);
                    db.AddInParameter(cmd, "@ProductLevel1", DbType.Int64, HyUpdateProd["ProductLevel1"]);
                    db.AddInParameter(cmd, "@ProductLevel2", DbType.Int64, HyUpdateProd["ProductLevel2"]);
                    db.AddInParameter(cmd, "@ProductLevel3", DbType.Int64, HyUpdateProd["ProductLevel3"].ToString().Trim());
                    db.AddInParameter(cmd, "@ProductDescription", DbType.String, HyUpdateProd["ProductDescription"].ToString().Trim());
                    db.AddInParameter(cmd, "@NSCCode", DbType.String, HyUpdateProd["NSCCode"].ToString().Trim());
                    db.AddInParameter(cmd, "@NIINCode", DbType.String, HyUpdateProd["NIINCode"].ToString().Trim());
                    db.ExecuteNonQuery(cmd, dbTran);
                    dbTran.Commit();
                    _msg = "Update";
                    _sysMsg = "Update";
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
                    db.AddInParameter(cmd, "@CreatedBy", DbType.String, hysavecomp["CreatedBy"].ToString().Trim());
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
        public string SaveImpNews(HybridDictionary hysavecomp, out string _sysMsg, out string _msg)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                string mCurrentID = "";
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_ImpNews");
                    db.AddInParameter(cmd, "@NewsId", DbType.Int64, hysavecomp["NewsId"]);
                    db.AddInParameter(cmd, "@News", DbType.String, hysavecomp["News"]);
                    db.AddInParameter(cmd, "@Date", DbType.DateTime, hysavecomp["Date"]);
                    db.AddInParameter(cmd, "@Pages", DbType.String, hysavecomp["Pages"]);
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
        public string UpdateLoginPassword(string NewPass, string OldPass, string User, string type, string mPass, string salt)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_UpdateLoginPassword");
                    db.AddInParameter(cmd, "@Password", DbType.String, NewPass);
                    db.AddInParameter(cmd, "@OldPass", DbType.String, OldPass);
                    db.AddInParameter(cmd, "@CompanyRefNo", DbType.String, User);
                    db.AddInParameter(cmd, "@Type", DbType.String, type);
                    db.AddInParameter(cmd, "@TempRef", DbType.String, mPass);
                    db.AddInParameter(cmd, "@Salt", DbType.String, salt);
                    db.ExecuteNonQuery(cmd, dbTran);
                    dbTran.Commit();
                    return "true";
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    return "-1";
                }
            }
        }
        public string UpdateStatus(Int64 ID, string Value1, string Value2)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_Country");
                    db.AddInParameter(cmd, "@CountryID", DbType.Int64, ID);
                    db.AddInParameter(cmd, "@CountryName", DbType.String, Value1);
                    db.AddInParameter(cmd, "@WorkCodeFor", DbType.String, Value2);
                    db.ExecuteNonQuery(cmd, dbTran);
                    dbTran.Commit();
                    return "true";
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    return "-1";
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
        public DataTable GetLogOutStatus(string ID)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_LogoutTime");
                    db.AddInParameter(cmd, "@UserId", DbType.String, ID);
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
        public DataTable RetriveProductCode(string CompanyRefNo, string ProdRefNo, string Purpose, string Type)
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
                    db.AddInParameter(cmd, "@Type", DbType.String, Type);
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
        public DataTable RetriveCountry(Int64 countryid, string text)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_country");
                    db.AddInParameter(cmd, "@CountryID", DbType.Int64, countryid);
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
        public DataTable RetriveCountry(Int64 countryid, string text, string forword)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_country");
                    db.AddInParameter(cmd, "@CountryID", DbType.Int64, countryid);
                    db.AddInParameter(cmd, "@CountryName", DbType.String, text);
                    db.AddInParameter(cmd, "@WorkCodeFor", DbType.String, forword);
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
        public DataTable RetriveMasterCategoryDate(Int64 CatID, string CatName, string SCatValue, string Flag, string LavelActive, string Criteria, string CreatedBy)
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
                    db.AddInParameter(cmd, "@CreatedBy", DbType.String, CreatedBy);
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
        public DataTable RetriveMasterSubCategoryDate(Int64 SCatID, string SCatName, string PId, string Criteria, string CompRefNo, string CreatedBy)
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
                    db.AddInParameter(cmd, "@CreatedBy", DbType.String, CreatedBy);
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
        public DataTable RetriveIntresteData(string CompRefNo)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("fn_GetInterestedInValue");
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
        public DataTable RetriveFilterCode(string CompRefNo, string SearchValue, string Criteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_FilterRetriveCode");
                    db.AddInParameter(cmd, "@CompRefNo", DbType.String, CompRefNo);
                    db.AddInParameter(cmd, "@SearchValue", DbType.String, SearchValue);
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
                    DbCommand cmd = db.GetStoredProcCommand("s_AggregateValue");
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
        public string SaveUploadExcelCompany(DataTable dtMaster, DataTable dtExcel)
        {
            using (DbConnection Connection = db.CreateConnection())
            {
                Connection.Open();
                DbTransaction Transaction = Connection.BeginTransaction();
                Int32 errorRow = -1;
                try
                {
                    Int32 mEntryID;
                    for (int k = 0; k < dtMaster.Rows.Count; k++)
                    {
                        mEntryID = 0;
                        DbCommand cmd = db.GetStoredProcCommand("sp_InsertCategoryFromExcel");

                        db.AddInParameter(cmd, "@Pid", DbType.Int64, mEntryID);
                        db.AddInParameter(cmd, "@Desc", DbType.String, "");
                        db.AddInParameter(cmd, "@CatName", DbType.String, dtMaster.Rows[k]["FSG Title"].ToString() + "(" + dtMaster.Rows[k]["Group"].ToString() + ")");
                        db.AddInParameter(cmd, "@L1Code", DbType.String, dtMaster.Rows[k]["Group"].ToString());
                        db.AddInParameter(cmd, "@L2Code", DbType.String, String.Empty);
                        db.AddOutParameter(cmd, "@NewId", DbType.Int32, 50);
                        db.ExecuteNonQuery(cmd, Transaction);
                        mEntryID = Convert.ToInt32(db.GetParameterValue(cmd, "@NewId"));
                        for (int a = 0; a < dtExcel.Rows.Count; a++)
                        {
                            errorRow = a;
                            if (dtMaster.Rows[k]["Group"].ToString() == dtExcel.Rows[a]["GROUP"].ToString())
                            {
                                DbCommand cmdInner = db.GetStoredProcCommand("sp_InsertCategoryFromExcel");

                                db.AddInParameter(cmdInner, "@Pid", DbType.Int64, mEntryID);
                                db.AddInParameter(cmdInner, "@CatName", DbType.String, dtExcel.Rows[a]["FSC Title"].ToString() + "(" + dtExcel.Rows[a]["GCLS"].ToString() + ")");
                                db.AddInParameter(cmdInner, "@L1Code", DbType.String, dtExcel.Rows[a]["GROUP"].ToString());
                                db.AddInParameter(cmdInner, "@L2Code", DbType.String, dtExcel.Rows[a]["GCLS"].ToString());
                                db.AddOutParameter(cmdInner, "@NewId", DbType.Int32, 50);
                                db.ExecuteNonQuery(cmdInner, Transaction);
                                //mEntryID = Convert.ToInt32(db.GetParameterValue(cmdInner, "@NewId"));
                            }

                        }
                    }
                    Transaction.Commit();
                    return "Save";
                }
                catch (Exception ex)
                {
                    Transaction.Rollback();
                    return ex.Message + "Error Found in Excel" + errorRow;
                }
                finally
                {
                    Connection.Close();
                }
            }
        }
        public string SaveUploadExcelCompany(DataTable dtMaster)
        {
            using (DbConnection Connection = db.CreateConnection())
            {
                Connection.Open();
                DbTransaction Transaction = Connection.BeginTransaction();
                Int32 errorRow = -1;
                try
                {
                    Int32 mEntryID;
                    for (int k = 0; k < dtMaster.Rows.Count; k++)
                    {
                        mEntryID = 0;
                        DbCommand cmd = db.GetStoredProcCommand("sp_InsertCategoryFromExcel");

                        db.AddInParameter(cmd, "@Pid", DbType.Int64, "Botl ID");
                        db.AddInParameter(cmd, "@CatName", DbType.String, dtMaster.Rows[k]["FSG Title"].ToString() + "(" + dtMaster.Rows[k]["Group"].ToString() + ")");
                        db.AddInParameter(cmd, "@L1Code", DbType.String, dtMaster.Rows[k]["Group"].ToString());
                        db.AddInParameter(cmd, "@L2Code", DbType.String, String.Empty);
                        db.AddOutParameter(cmd, "@NewId", DbType.Int32, 50);
                        db.ExecuteNonQuery(cmd, Transaction);
                        mEntryID = Convert.ToInt32(db.GetParameterValue(cmd, "@NewId"));
                    }
                    Transaction.Commit();
                    return "Save";
                }
                catch (Exception ex)
                {
                    Transaction.Rollback();
                    return ex.Message + "Error Found in Excel" + errorRow;
                }
                finally
                {
                    Connection.Close();
                }
            }
        }
        public string SaveExcel3510(DataTable dtMaster, string l1, string l2, string pid)
        {
            using (DbConnection Connection = db.CreateConnection())
            {
                Connection.Open();
                DbTransaction Transaction = Connection.BeginTransaction();
                Int32 errorRow = -1;
                try
                {
                    Int32 mEntryID;
                    for (int k = 0; k < dtMaster.Rows.Count; k++)
                    {
                        mEntryID = 0;
                        DbCommand cmd = db.GetStoredProcCommand("sp_InsertCategoryFromExcel");

                        db.AddInParameter(cmd, "@Pid", DbType.Int64, pid);
                        db.AddInParameter(cmd, "@L1Code", DbType.String, l2);
                        db.AddInParameter(cmd, "@L2Code", DbType.String, dtMaster.Rows[k]["INC"].ToString());
                        db.AddInParameter(cmd, "@CatName", DbType.String, dtMaster.Rows[k]["Item Name"].ToString() + "(" + dtMaster.Rows[k]["INC"].ToString() + ")");
                        db.AddInParameter(cmd, "@Desc", DbType.String, dtMaster.Rows[k]["Item Name"].ToString());
                        db.AddOutParameter(cmd, "@NewId", DbType.Int32, 50);
                        db.ExecuteNonQuery(cmd, Transaction);
                        mEntryID = Convert.ToInt32(db.GetParameterValue(cmd, "@NewId"));
                    }
                    Transaction.Commit();
                    return "Save";
                }
                catch (Exception ex)
                {
                    Transaction.Rollback();
                    return ex.Message + "Error Found in Excel" + errorRow;
                }
                finally
                {
                    Connection.Close();
                }
            }
        }
        public string SaveExcelProduct(DataTable dtMaster)
        {
            using (DbConnection Connection = db.CreateConnection())
            {
                Connection.Open();
                DbTransaction Transaction = Connection.BeginTransaction();
                Int32 errorRow = -1;
                try
                {
                    DataTable dtIds;      //  to store ids value
                    for (int k = 0; k < dtMaster.Rows.Count; k++)
                    {
                        errorRow = k;
                        // getting related id to insert into produt table
                        DbCommand ids = db.GetSqlStringCommand("select * from fn_GetIdsForProductTable('" + dtMaster.Rows[k]["COMPANY"].ToString() + "','" + dtMaster.Rows[k]["DIVISION"].ToString() + "'," +
                      "'" + dtMaster.Rows[k]["UNIT"].ToString() + "','" + dtMaster.Rows[k]["NSN GROUP"].ToString() + "', '" + dtMaster.Rows[k]["NSN GROUP CLASS"].ToString() + "'," +
                      "'" + dtMaster.Rows[k]["ITEM CODE"].ToString() + "','" + dtMaster.Rows[k]["OEM COUNTRY"].ToString() + "','" + dtMaster.Rows[k]["END USER"].ToString() + "'," +
                      "'" + dtMaster.Rows[k]["DEFENCE PLATFORM"].ToString() + "','" + dtMaster.Rows[k]["NAME OF DEFENCE PLATFORM"].ToString() + "','" + dtMaster.Rows[k]["PRODUCT (INDUSTRY DOMAIN)"].ToString() + "'," +
                      "'" + dtMaster.Rows[k]["PRODUCT (INDUSTRY SUB DOMAIN)"].ToString() + "', '" + dtMaster.Rows[k]["PRODUCT (INDUSTRY 2ND SUB DOMAIN)"].ToString() + "'," + "'" + dtMaster.Rows[k]["YEAR OF INDIGINISATION"].ToString() + "')");

                        dtIds = db.ExecuteDataSet(ids).Tables[0].Copy();

                        // checking if ref no is not blank only then allow to enter else do not enter product
                        if (!String.IsNullOrEmpty(dtIds.Rows[0]["RefNo"].ToString().Trim()))
                        {// checking if nsn group no is not blank only then allow to enter else do not enter product
                            if (dtIds.Rows[0]["ProdLevel1"].ToString().Trim() != "-1")
                            {// checking if defence platform no is not blank only then allow to enter else do not enter product
                                if (dtIds.Rows[0]["Platform"].ToString().Trim() != "-1")
                                {// checking if PRODUCT (INDUSTRY DOMAIN) no is not blank only then allow to enter else do not enter product
                                    if (dtIds.Rows[0]["TechLevel1"].ToString().Trim() != "-1")
                                    {
                                        if (dtIds.Rows[0]["ProdLevel2"].ToString().Trim() != "-1")
                                        {// checking if nsn group class  no is not blank only then allow to enter else do not enter product
                                            if (dtIds.Rows[0]["Nomenclature"].ToString().Trim() != "-1")
                                            {// checking if name of defence paltform no is not blank only then allow to enter else do not enter product
                                                if (dtIds.Rows[0]["TechLevel2"].ToString().Trim() != "-1")
                                                {// checking if PRODUCT (INDUSTRY sub DOMAIN) no is not blank only then allow to enter else do not enter product
                                                    // Inserting values in product table
                                                    DbCommand cmd = db.GetStoredProcCommand("sp_InsertProductFromExcel");

                                                    db.AddInParameter(cmd, "@CompanyRefNo", DbType.String, dtIds.Rows[0]["RefNo"].ToString());
                                                    db.AddInParameter(cmd, "@Role", DbType.String, dtIds.Rows[0]["Role"].ToString());
                                                    db.AddInParameter(cmd, "@ProductLevel1", DbType.Int16, dtIds.Rows[0]["ProdLevel1"].ToString());
                                                    db.AddInParameter(cmd, "@ProductLevel2", DbType.Int64, dtIds.Rows[0]["ProdLevel2"].ToString());
                                                    db.AddInParameter(cmd, "@ProductLevel3", DbType.Int64, dtIds.Rows[0]["ProdLevel3"].ToString());

                                                    db.AddInParameter(cmd, "@ProductDescription", DbType.String, dtMaster.Rows[k]["ITEM DESCRIPTION"].ToString());
                                                    db.AddInParameter(cmd, "@NSCCode", DbType.String, dtMaster.Rows[k]["NSC CODE"].ToString());
                                                    db.AddInParameter(cmd, "@NIINCode", DbType.String, dtMaster.Rows[k]["NIIN CODE"].ToString());
                                                    db.AddInParameter(cmd, "@OEMPartNumber", DbType.String, dtMaster.Rows[k]["OEM PART NUMBER"].ToString());
                                                    db.AddInParameter(cmd, "@OEMName", DbType.String, dtMaster.Rows[k]["OEM NAME"].ToString());
                                                    db.AddInParameter(cmd, "@OEMCountry", DbType.Int64, dtIds.Rows[0]["OEMCountry"].ToString());
                                                    db.AddInParameter(cmd, "@DPSUPartNumber", DbType.String, dtMaster.Rows[k]["DPSU PART NUMBER"].ToString());
                                                    db.AddInParameter(cmd, "@HSNCode", DbType.String, dtMaster.Rows[k]["HSN CODE"].ToString());
                                                    // db.AddInParameter(cmd, "@HSNCode8Digit", DbType.String, dtMaster.Rows[k]["HSN CODE"].ToString());
                                                    db.AddInParameter(cmd, "@EndUserPartNumber", DbType.String, "");

                                                    db.AddInParameter(cmd, "@EndUser", DbType.Int64, dtIds.Rows[0]["EndUser"].ToString());
                                                    db.AddInParameter(cmd, "@Platform", DbType.Int64, dtIds.Rows[0]["Platform"].ToString());
                                                    db.AddInParameter(cmd, "@NomenclatureOfMainSystem", DbType.Int64, dtIds.Rows[0]["Nomenclature"].ToString());
                                                    db.AddInParameter(cmd, "@TechnologyLevel1", DbType.Int64, dtIds.Rows[0]["TechLevel1"].ToString());
                                                    db.AddInParameter(cmd, "@TechnologyLevel2", DbType.Int64, dtIds.Rows[0]["TechLevel2"].ToString());
                                                    db.AddInParameter(cmd, "@TechnologyLevel3", DbType.Int64, dtIds.Rows[0]["TechLevel3"].ToString());

                                                    db.AddInParameter(cmd, "@SearchKeyword", DbType.String, dtMaster.Rows[k]["SEARCH KEYWORDS"].ToString());
                                                    if (dtMaster.Rows[k]["MANUFACTURER NAME IF INDIGINISED"].ToString().Trim() == "" || dtMaster.Rows[k]["MANUFACTURER NAME IF INDIGINISED"].ToString().Trim() == "-")
                                                        db.AddInParameter(cmd, "@IsIndeginized", DbType.String, "N");
                                                    else
                                                        db.AddInParameter(cmd, "@IsIndeginized", DbType.String, "Y");
                                                    db.AddInParameter(cmd, "@ManufactureName", DbType.String, dtMaster.Rows[k]["MANUFACTURER NAME IF INDIGINISED"].ToString());

                                                    db.AddInParameter(cmd, "@ManufactureAddress", DbType.String, dtMaster.Rows[k]["MANUFACTURER ADD"].ToString());
                                                    db.AddInParameter(cmd, "@YearofIndiginization", DbType.Int64, dtIds.Rows[0]["YearOfIndiginisation"].ToString());
                                                    db.ExecuteNonQuery(cmd, Transaction);
                                                }
                                                else
                                                {
                                                    Transaction.Rollback();
                                                    return "PRODUCT (INDUSTRY SUB DOMAIN) not found for given data in Excel in row : " + errorRow;
                                                }
                                            }
                                            else
                                            {
                                                Transaction.Rollback();
                                                return "NAME OF DEFENCE PLATFORM not found for given data in Excel in row : " + errorRow;
                                            }
                                        }
                                        else
                                        {
                                            Transaction.Rollback();
                                            return "NSN GROUP CLASS not found for given data in Excel in row : " + errorRow;

                                        }
                                    }
                                    else
                                    {
                                        Transaction.Rollback();
                                        return "PRODUCT (INDUSTRY DOMAIN) not found for given data in Excel in row : " + errorRow;
                                    }
                                }
                                else
                                {
                                    Transaction.Rollback();
                                    return "DEFENCE PLATFORM not found for given data in Excel in row : " + errorRow;
                                }
                            }
                            else
                            {
                                Transaction.Rollback();
                                return "NSN GROUP not found for given data in Excel in row : " + errorRow;
                            }
                        }
                        else
                        {
                            Transaction.Rollback();
                            return "RefNo not found for given data in Excel in row : " + errorRow;
                        }
                    }
                    Transaction.Commit();
                    return "Save";
                }
                catch (Exception ex)
                {
                    Transaction.Rollback();
                    return ex.Message + "Error Found in Excel in row : " + errorRow;
                }
                finally
                {
                    Connection.Close();
                }
            }
        }
        public DataTable CreateExcelConnection(string FilePath, string SheetName, out string text)
        {
            try
            {
                //Connection = new OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;data source=" + FilePath + ";Extended Properties=Excel 8.0;")
                System.Data.OleDb.OleDbConnection Connection = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\";");
                string mQuery = "select * from [" + SheetName + "$]";
                System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand(mQuery, Connection);
                System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                Connection.Open();
                da.Fill(ds, "ex");
                Connection.Close();
                text = "success";
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                DataSet ds = new DataSet();
                text = ex.Message;
                return ds.Tables[0];
            }
        }
        public DataTable GetDashboardData(string Purpose, string Search)
         {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_GetDashboardData");
                    db.AddInParameter(cmd, "@Purpose", DbType.String, Purpose);
                    db.AddInParameter(cmd, "@SearchText", DbType.String, Search);
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
        public DataTable GetProductFilterData(string Purpose, string refno, string Search)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_MultipleKeywordSearchProductFilter");
                    db.AddInParameter(cmd, "@Purpose", DbType.String, Purpose);
                    db.AddInParameter(cmd, "@CompRefNo", DbType.String, refno);
                    db.AddInParameter(cmd, "@SearchText", DbType.String, Search);
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
        public DataTable GetDashboardDataApproveDisapproveItem(string Purpose, string Search, string Type)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_GetDashboardDataApproveDisapproveItem");
                    db.AddInParameter(cmd, "@Purpose", DbType.String, Purpose);
                    db.AddInParameter(cmd, "@SearchText", DbType.String, Search);
                    db.AddInParameter(cmd, "@SearchText2", DbType.String, Type);
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
        public DataTable TestGrid(string Function, string ProdRefNo, Int32 ProdInfoId, string Name, string Value, string Unit)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_InsertProductInfo");
                    db.AddInParameter(cmd, "@Function", DbType.String, Function);
                    db.AddInParameter(cmd, "@ProdRefNo", DbType.String, ProdRefNo);
                    db.AddInParameter(cmd, "@ProdInfoId", DbType.Int32, ProdInfoId);
                    db.AddInParameter(cmd, "@Name", DbType.String, Name);
                    db.AddInParameter(cmd, "@Value", DbType.String, Value);
                    db.AddInParameter(cmd, "@Unit", DbType.String, Unit);
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
        public DataTable RetriveSaveEstimateGrid(string Function, Int32 ProdInfoId, string ProdRefNo, Int32 Year, string FYear, string EstimateQuantity, string Unit, string Price)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_InsertProductPrice");
                    db.AddInParameter(cmd, "@Function", DbType.String, Function);
                    db.AddInParameter(cmd, "@ProdQtyId", DbType.Int32, ProdInfoId);
                    db.AddInParameter(cmd, "@ProdRefNo", DbType.String, ProdRefNo);
                    db.AddInParameter(cmd, "@Year", DbType.Int32, Year);
                    db.AddInParameter(cmd, "@FYear", DbType.String, FYear);
                    db.AddInParameter(cmd, "@EstimatedQty", DbType.String, EstimateQuantity);
                    db.AddInParameter(cmd, "@Unit", DbType.String, Unit);
                    db.AddInParameter(cmd, "@EstimatedPrice", DbType.String, Price);
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
        public DataTable RetrivenewcategortFIIG_No(string Value, string Criteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_RetriveFGI");
                    db.AddInParameter(cmd, "@SCategoryName", DbType.String, Value);
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
        #region Forgot password
        public DataTable RetriveForgotPasswordEmail(string email, string type)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_forgotpassword");
                    db.AddInParameter(cmd, "@NodalEmail", DbType.String, email);
                    db.AddInParameter(cmd, "@Type", DbType.String, type);
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
        ///////////////////////============================== Vendor Save Edit delete code-======================//////////////////////////////
        #region Vendor Save Code
        public string SaveVendorRegis(HybridDictionary hysavecomp, out string _sysMsg, out string _msg)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                string mCurrentID = "";
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_VendorRegistration");
                    db.AddInParameter(cmd, "@VendorID", DbType.Int64, hysavecomp["VendorID"]);
                    db.AddInParameter(cmd, "@PanNo", DbType.String, hysavecomp["PanNo"]);
                    db.AddInParameter(cmd, "@GSTNo", DbType.String, hysavecomp["GSTNo"]);
                    db.AddInParameter(cmd, "@V_CompName", DbType.String, hysavecomp["V_CompName"]);
                    db.AddInParameter(cmd, "@RegistrationCategory", DbType.String, hysavecomp["RegistrationCategory"]);
                    db.AddInParameter(cmd, "@V_RegisterdDPSU", DbType.String, hysavecomp["V_RegisterdDPSU"]);
                    db.AddInParameter(cmd, "@TypeOfBuisness", DbType.Int64, hysavecomp["TypeOfBuisness"].ToString().Trim());
                    db.AddInParameter(cmd, "@BusinessSector", DbType.Int64, hysavecomp["BusinessSector"].ToString().Trim());
                    db.AddInParameter(cmd, "@NodalOfficerName", DbType.String, hysavecomp["NodalOfficerName"].ToString().Trim());
                    db.AddInParameter(cmd, "@NodalOfficerEmail", DbType.String, hysavecomp["NodalOfficerEmail"]);
                    db.AddInParameter(cmd, "@ContactNo", DbType.String, hysavecomp["ContactNo"]);
                    db.AddInParameter(cmd, "@StreetAddress", DbType.String, hysavecomp["StreetAddress"]);
                    db.AddInParameter(cmd, "@StreetAddressLine2", DbType.String, hysavecomp["StreetAddressLine2"]);
                    db.AddInParameter(cmd, "@City", DbType.String, hysavecomp["City"]);
                    db.AddInParameter(cmd, "@State", DbType.String, hysavecomp["State"].ToString().Trim());
                    db.AddInParameter(cmd, "@ZipCode", DbType.String, hysavecomp["ZipCode"].ToString().Trim());
                    //db.AddInParameter(cmd, "@Country", DbType.Int64, hysavecomp["Country"]);
                    db.AddInParameter(cmd, "@CheckStatus", DbType.String, hysavecomp["CheckStatus"]);
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
                    _sysMsg = "";
                    return _sysMsg;
                }
                finally
                {
                    dbCon.Close();
                }
            }
        }
        public string SaveVendorRegistrationDetails(HybridDictionary HySaveVendorRegisdetail, DataTable dtEnterNameOf, DataTable dt2, DataTable dt3, DataTable dt4, DataTable dt5, DataTable dt6, out string _sysMsg, out string _msg)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                string mCurrentID = "";
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_VendorRegistrationDetails");
                    db.AddInParameter(cmd, "@VendorDetailID", DbType.Int64, HySaveVendorRegisdetail["VendorDetailID"]);
                    db.AddInParameter(cmd, "@VendorRefNo", DbType.String, HySaveVendorRegisdetail["VendorRefNo"]);
                    db.AddInParameter(cmd, "@RegistrationCategory", DbType.String, HySaveVendorRegisdetail["RegistrationCategory"]);
                    db.AddInParameter(cmd, "@FirmCompanyName", DbType.String, HySaveVendorRegisdetail["FirmCompanyName"]);
                    db.AddInParameter(cmd, "@TypeOfOwnership", DbType.Int64, HySaveVendorRegisdetail["TypeOfOwnership"].ToString().Trim());
                    db.AddInParameter(cmd, "@ScaleofBuisness", DbType.String, HySaveVendorRegisdetail["ScaleofBuisness"].ToString().Trim());
                    db.AddInParameter(cmd, "@Ownership", DbType.String, HySaveVendorRegisdetail["Ownership"]);
                    db.AddInParameter(cmd, "@PercentofOwnership", DbType.String, HySaveVendorRegisdetail["PercentofOwnership"]);
                    db.AddInParameter(cmd, "@FileofOwnership", DbType.String, HySaveVendorRegisdetail["FileofOwnership"]);
                    db.AddInParameter(cmd, "@BuisnessSector", DbType.Int64, HySaveVendorRegisdetail["BuisnessSector"].ToString().Trim());
                    db.AddInParameter(cmd, "@Date_Incorportaion_Company", DbType.Date, HySaveVendorRegisdetail["Date_Incorportaion_Company"].ToString());
                    db.AddInParameter(cmd, "@Street_Address", DbType.String, HySaveVendorRegisdetail["Street_Address"]);
                    db.AddInParameter(cmd, "@Street_Address_Line_2", DbType.String, HySaveVendorRegisdetail["Street_Address_Line_2"]);
                    db.AddInParameter(cmd, "@City", DbType.String, HySaveVendorRegisdetail["City"]);
                    db.AddInParameter(cmd, "@State", DbType.String, HySaveVendorRegisdetail["State"].ToString().Trim());
                    db.AddInParameter(cmd, "@PinCode", DbType.String, HySaveVendorRegisdetail["PinCode"].ToString().Trim());
                    db.AddInParameter(cmd, "@FirstName", DbType.String, HySaveVendorRegisdetail["FirstName"]);
                    db.AddInParameter(cmd, "@MiddleName", DbType.String, HySaveVendorRegisdetail["MiddleName"]);
                    db.AddInParameter(cmd, "@LastName", DbType.String, HySaveVendorRegisdetail["LastName"]);
                    db.AddInParameter(cmd, "@Email", DbType.String, HySaveVendorRegisdetail["Email"]);
                    db.AddInParameter(cmd, "@ContactNo", DbType.String, HySaveVendorRegisdetail["ContactNo"]);
                    db.AddInParameter(cmd, "@FaxNo", DbType.String, HySaveVendorRegisdetail["FaxNo"]);
                    db.AddInParameter(cmd, "@Type", DbType.String, HySaveVendorRegisdetail["Type"]);
                    db.AddOutParameter(cmd, "@ReturnID", DbType.String, 20);
                    db.ExecuteNonQuery(cmd, dbTran);
                    mCurrentID = db.GetParameterValue(cmd, "@ReturnID").ToString();
                    for (int i = 0; i < dtEnterNameOf.Rows.Count; i++)
                    {
                        DbCommand dbcom1 = db.GetStoredProcCommand("sp_VendorRegistrationMultiGrid");
                        db.AddInParameter(dbcom1, "@Type", DbType.String, "EnterNameof");
                        db.AddInParameter(dbcom1, "@MasterId", DbType.Int64, dtEnterNameOf.Rows[i]["RowNumber"]);
                        db.AddInParameter(dbcom1, "@VendorRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom1, "@EnterNameof", DbType.String, dtEnterNameOf.Rows[i]["EnterName"]);
                        db.AddInParameter(dbcom1, "@Name", DbType.String, dtEnterNameOf.Rows[i]["Name"]);
                        db.AddInParameter(dbcom1, "@Designation", DbType.String, dtEnterNameOf.Rows[i]["Designation"]);
                        db.AddInParameter(dbcom1, "@DIN_No", DbType.String, dtEnterNameOf.Rows[i]["DinNo"]);
                        db.AddInParameter(dbcom1, "@MobileNo", DbType.Int64, dtEnterNameOf.Rows[i]["MobileNo"]);
                        db.ExecuteNonQuery(dbcom1, dbTran);
                    }
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        DbCommand dbcom2 = db.GetStoredProcCommand("sp_VendorRegistrationMultiGrid1");
                        db.AddInParameter(dbcom2, "@Type", DbType.String, "ProductsDetails");
                        db.AddInParameter(dbcom2, "@MasterId", DbType.Int64, dt2.Rows[i]["RowNumberProd"]);
                        db.AddInParameter(dbcom2, "@VendorRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom2, "@ProductNomenClature", DbType.Int64, dt2.Rows[i]["Nomenclature"]);
                        db.AddInParameter(dbcom2, "@NatoGroup", DbType.Int64, dt2.Rows[i]["NatoGroup"]);
                        db.AddInParameter(dbcom2, "@NatoClass", DbType.Int64, dt2.Rows[i]["NatoClass"]);
                        db.AddInParameter(dbcom2, "@ItemCode", DbType.Int64, dt2.Rows[i]["ItemCode"]);
                        db.AddInParameter(dbcom2, "@HSNCode", DbType.String, dt2.Rows[i]["HSNCode"]);
                        db.ExecuteNonQuery(dbcom2, dbTran);
                    }
                    for (int i = 0; i < dt3.Rows.Count; i++)
                    {
                        DbCommand dbcom3 = db.GetStoredProcCommand("sp_VendorRegistrationMultiGrid2");
                        db.AddInParameter(dbcom3, "@Type", DbType.String, "TechnologyDetails");
                        db.AddInParameter(dbcom3, "@MasterId", DbType.Int64, dt3.Rows[i]["RowNumberTech"]);
                        db.AddInParameter(dbcom3, "@VendorRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom3, "@ProductNomenClature1", DbType.Int64, dt3.Rows[i]["TechNomenclature"]);
                        db.AddInParameter(dbcom3, "@TechnologyLevel1", DbType.Int64, dt3.Rows[i]["Technology1"]);
                        db.AddInParameter(dbcom3, "@TechnologyLevel2", DbType.Int64, dt3.Rows[i]["Technology2"]);
                        db.AddInParameter(dbcom3, "@TechnologyLevel3", DbType.Int64, dt3.Rows[i]["Technology3"]);
                        db.ExecuteNonQuery(dbcom3, dbTran);
                    }
                    for (int i = 0; i < dt4.Rows.Count; i++)
                    {
                        DbCommand dbcom4 = db.GetStoredProcCommand("sp_VendorRegistrationMultiGrid3");
                        db.AddInParameter(dbcom4, "@Type", DbType.String, "RawMaterial");
                        db.AddInParameter(dbcom4, "@MasterId", DbType.Int64, dt4.Rows[i]["SrNoRawMeterail"]);
                        db.AddInParameter(dbcom4, "@VendorRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom4, "@Items", DbType.String, dt4.Rows[i]["Items"]);
                        db.AddInParameter(dbcom4, "@BasicRawMeterial", DbType.String, dt4.Rows[i]["RawMeterial"]);
                        db.AddInParameter(dbcom4, "@SourceofMaterial", DbType.Int64, dt4.Rows[i]["SourceMeterial"]);
                        db.AddInParameter(dbcom4, "@Major_Raw_Material_Suppliers", DbType.String, dt4.Rows[i]["MeterailSupplier"]);
                        db.ExecuteNonQuery(dbcom4, dbTran);
                    }
                    for (int i = 0; i < dt5.Rows.Count; i++)
                    {
                        DbCommand dbcom5 = db.GetStoredProcCommand("sp_VendorRegistrationMultiGrid4");
                        db.AddInParameter(dbcom5, "@Type", DbType.String, "ItemProducedSupplied");
                        db.AddInParameter(dbcom5, "@MasterId", DbType.Int64, dt5.Rows[i]["SrNoSpplied"]);
                        db.AddInParameter(dbcom5, "@VendorRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom5, "@Reputed_Customer", DbType.String, dt5.Rows[i]["NameCust"]);
                        db.AddInParameter(dbcom5, "@Description", DbType.String, dt5.Rows[i]["DesStoreSupp"]);
                        db.AddInParameter(dbcom5, "@SupplyNoDate", DbType.String, dt5.Rows[i]["OderNoorDate"]);
                        db.AddInParameter(dbcom5, "@OrderQuantity", DbType.String, dt5.Rows[i]["OrderQty"]);
                        db.AddInParameter(dbcom5, "@SuppliedQuantity", DbType.String, dt5.Rows[i]["ValueQtySupp"]);
                        db.AddInParameter(dbcom5, "@Date2", DbType.Date, dt5.Rows[i]["DateofLastSupp"]);
                        db.ExecuteNonQuery(dbcom5, dbTran);
                    }
                    for (int i = 0; i < dt6.Rows.Count; i++)
                    {
                        DbCommand dbcom6 = db.GetStoredProcCommand("sp_VendorRegistrationMultiGrid5");
                        db.AddInParameter(dbcom6, "@Type", DbType.String, "ItemProducedSuppliedNot");
                        db.AddInParameter(dbcom6, "@MasterId", DbType.Int64, dt6.Rows[i]["RowNumber"]);
                        db.AddInParameter(dbcom6, "@VendorRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom6, "@Reputed_Customer1", DbType.String, dt6.Rows[i]["NameCust1"]);
                        db.AddInParameter(dbcom6, "@Description1", DbType.String, dt6.Rows[i]["DesStoreSupp1"]);
                        db.AddInParameter(dbcom6, "@SupplyNoDate1", DbType.String, dt6.Rows[i]["OderNoorDate1"]);
                        db.AddInParameter(dbcom6, "@OrderQuantity1", DbType.String, dt6.Rows[i]["OrderQty1"]);
                        db.AddInParameter(dbcom6, "@SuppliedQuantity1", DbType.String, dt6.Rows[i]["ValueQtySupp1"]);
                        db.AddInParameter(dbcom6, "@Date21", DbType.Date, dt6.Rows[i]["DateofLastSupp1"]);
                        db.ExecuteNonQuery(dbcom6, dbTran);
                    }
                    dbTran.Commit();
                    _msg = "Save";
                    _sysMsg = "Save";
                    return "Save";
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
        public string SaveVendorGeneralInfo(HybridDictionary HySaveVendorRegisdetail, DataTable dtEnterNameOf, out string _sysMsg, out string _msg)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                string mCurrentID = "";
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_VendorGeneralInfo");
                    db.AddInParameter(cmd, "@VendorDetailID", DbType.Int64, HySaveVendorRegisdetail["VendorDetailID"]);
                    db.AddInParameter(cmd, "@VendorRefNo", DbType.String, HySaveVendorRegisdetail["VendorRefNo"]);
                    db.AddInParameter(cmd, "@RegistrationCategory", DbType.String, HySaveVendorRegisdetail["RegistrationCategory"]);
                    db.AddInParameter(cmd, "@TypeOfOwnership", DbType.Int64, HySaveVendorRegisdetail["TypeOfOwnership"].ToString().Trim());
                    db.AddInParameter(cmd, "@ScaleofBuisness", DbType.String, HySaveVendorRegisdetail["ScaleofBuisness"].ToString().Trim());
                    db.AddInParameter(cmd, "@Ownership", DbType.String, HySaveVendorRegisdetail["Ownership"]);
                    db.AddInParameter(cmd, "@PercentofOwnership", DbType.String, HySaveVendorRegisdetail["PercentofOwnership"]);
                    db.AddInParameter(cmd, "@FileofOwnership", DbType.String, HySaveVendorRegisdetail["FileofOwnership"]);
                    db.AddInParameter(cmd, "@BuisnessSector", DbType.Int64, HySaveVendorRegisdetail["BuisnessSector"].ToString().Trim());
                    db.AddInParameter(cmd, "@Date_Incorportaion_Company", DbType.Date, HySaveVendorRegisdetail["Date_Incorportaion_Company"].ToString());
                    db.AddInParameter(cmd, "@ContactNo", DbType.String, HySaveVendorRegisdetail["ContactNo"]);
                    db.AddInParameter(cmd, "@FaxNo", DbType.String, HySaveVendorRegisdetail["FaxNo"]);
                    db.AddInParameter(cmd, "@Type", DbType.String, HySaveVendorRegisdetail["Type"]);
                    db.AddOutParameter(cmd, "@ReturnID", DbType.String, 20);
                    db.ExecuteNonQuery(cmd, dbTran);
                    mCurrentID = db.GetParameterValue(cmd, "@ReturnID").ToString();
                    for (int i = 0; i < dtEnterNameOf.Rows.Count; i++)
                    {
                        DbCommand dbcom1 = db.GetStoredProcCommand("sp_VendorRegistrationMultiGrid");
                        db.AddInParameter(dbcom1, "@Type", DbType.String, "EnterNameof");
                        db.AddInParameter(dbcom1, "@MasterId", DbType.Int64, dtEnterNameOf.Rows[i]["RowNumber"]);
                        db.AddInParameter(dbcom1, "@VendorRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom1, "@EnterNameof", DbType.String, dtEnterNameOf.Rows[i]["EnterName"]);
                        db.AddInParameter(dbcom1, "@Name", DbType.String, dtEnterNameOf.Rows[i]["Name"]);
                        db.AddInParameter(dbcom1, "@Designation", DbType.String, dtEnterNameOf.Rows[i]["Designation"]);
                        db.AddInParameter(dbcom1, "@DIN_No", DbType.String, dtEnterNameOf.Rows[i]["DinNo"]);
                        db.AddInParameter(dbcom1, "@MobileNo", DbType.Int64, dtEnterNameOf.Rows[i]["MobileNo"]);
                        db.AddInParameter(dbcom1, "@mProcess", DbType.String, dtEnterNameOf.Rows[i]["mProcess"]);
                        db.ExecuteNonQuery(dbcom1, dbTran);
                    }
                    dbTran.Commit();
                    _msg = "Save";
                    _sysMsg = "Save";
                    return "Save";
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
        public string SaveVendorCompanyInfo(HybridDictionary HySaveVendorRegisdetailComp, DataTable dt1, DataTable dt2, DataTable dt3, DataTable dt4, DataTable dt5, DataTable dt6, DataTable dt7, DataTable dt8, DataTable dt9, DataTable dt10, out string _sysMsg, out string _msg)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                string mCurrentID = "";
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_VendorCompInfo");
                    db.AddInParameter(cmd, "@VendorDetailID", DbType.Int64, HySaveVendorRegisdetailComp["VendorDetailID"]);
                    db.AddInParameter(cmd, "@VendorRefNo", DbType.String, HySaveVendorRegisdetailComp["VendorRefNo"]);
                    db.AddInParameter(cmd, "@Is_Lab_accredited_by_NABL", DbType.String, HySaveVendorRegisdetailComp["Is_Lab_accredited_by_NABL"].ToString().Trim());
                    db.AddInParameter(cmd, "@CertifictionValid", DbType.Date, HySaveVendorRegisdetailComp["CertifictionValid"].ToString().Trim());
                    db.AddInParameter(cmd, "@Details_of_R_D_Facilities", DbType.String, HySaveVendorRegisdetailComp["Details_of_R_D_Facilities"]);
                    db.AddInParameter(cmd, "@IsSalesOrMarketOffice", DbType.String, HySaveVendorRegisdetailComp["IsSalesOrMarketOffice"].ToString().Trim());
                    db.AddInParameter(cmd, "@NodelName", DbType.String, HySaveVendorRegisdetailComp["NodelName"]);
                    db.AddInParameter(cmd, "@MarketingOfficeAddress", DbType.String, HySaveVendorRegisdetailComp["MarketingOfficeAddress"]);
                    db.AddInParameter(cmd, "@Line2", DbType.String, HySaveVendorRegisdetailComp["Line2"].ToString().Trim());
                    db.AddInParameter(cmd, "@OfficerCity", DbType.String, HySaveVendorRegisdetailComp["OfficerCity"].ToString());
                    db.AddInParameter(cmd, "@OfficeState", DbType.String, HySaveVendorRegisdetailComp["OfficeState"]);
                    db.AddInParameter(cmd, "@OfficePincode", DbType.String, HySaveVendorRegisdetailComp["OfficePincode"]);
                    db.AddInParameter(cmd, "@PhoneNo", DbType.String, HySaveVendorRegisdetailComp["PhoneNo"].ToString().Trim());
                    db.AddInParameter(cmd, "@OfficeFaxNo", DbType.String, HySaveVendorRegisdetailComp["OfficeFaxNo"].ToString());
                    db.AddInParameter(cmd, "@OfficeEmail", DbType.String, HySaveVendorRegisdetailComp["OfficeEmail"]);
                    db.AddInParameter(cmd, "@IsAuthorisedDealer", DbType.String, HySaveVendorRegisdetailComp["IsAuthorisedDealer"]);
                    db.AddInParameter(cmd, "@FuturePlan", DbType.String, HySaveVendorRegisdetailComp["FuturePlan"]);
                    db.AddOutParameter(cmd, "@ReturnID", DbType.String, 20);
                    db.ExecuteNonQuery(cmd, dbTran);
                    mCurrentID = db.GetParameterValue(cmd, "@ReturnID").ToString();
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        DbCommand dbcom1 = db.GetStoredProcCommand("sp_VendorRegistrationMultiGridManuFac");
                        db.AddInParameter(dbcom1, "@Type", DbType.String, "CompInfo");
                        db.AddInParameter(dbcom1, "@MasterId", DbType.Int16, 0);
                        db.AddInParameter(dbcom1, "@VendorRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom1, "@Name_of_Factory", DbType.String, dt1.Rows[i]["FactoryName"]);
                        db.AddInParameter(dbcom1, "@Factory_GSTNo", DbType.String, dt1.Rows[i]["FACGSTNO"]);
                        db.AddInParameter(dbcom1, "@Comp_Postal_Address", DbType.String, dt1.Rows[i]["CAddress"]);
                        db.AddInParameter(dbcom1, "@Contact_Official_Name", DbType.String, dt1.Rows[i]["COfficialName"]);
                        db.AddInParameter(dbcom1, "@Telephone_No", DbType.String, dt1.Rows[i]["TeleNo"]);
                        db.AddInParameter(dbcom1, "@Fax_No", DbType.String, dt1.Rows[i]["FaxNo"]);
                        db.AddInParameter(dbcom1, "@Email_Id", DbType.String, dt1.Rows[i]["EmailId"]);
                        db.ExecuteNonQuery(dbcom1, dbTran);
                    }
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        DbCommand dbcom2 = db.GetStoredProcCommand("sp_VendorRegistrationMultiGridAreaDetail");
                        db.AddInParameter(dbcom2, "@Type", DbType.String, "AreaDetail");
                        db.AddInParameter(dbcom2, "@MasterId", DbType.Int16, 0);
                        db.AddInParameter(dbcom2, "@VendorRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom2, "@Area_Factory_Name", DbType.String, dt2.Rows[i]["AreaFactoryName"]);
                        db.AddInParameter(dbcom2, "@PRODUCTION_AREA", DbType.String, dt2.Rows[i]["PArea"]);
                        db.AddInParameter(dbcom2, "@INSPECTION_AREA", DbType.String, dt2.Rows[i]["InsArea"]);
                        db.AddInParameter(dbcom2, "@TOTAL_COVERED_AREA", DbType.String, dt2.Rows[i]["CoverArea"]);
                        db.AddInParameter(dbcom2, "@Total_Area", DbType.String, dt2.Rows[i]["TotalArea"]);
                        db.ExecuteNonQuery(dbcom2, dbTran);
                    }
                    for (int i = 0; i < dt3.Rows.Count; i++)
                    {
                        DbCommand dbcom3 = db.GetStoredProcCommand("sp_VendorRegistrationMultiGridAllPlantandMachines");
                        db.AddInParameter(dbcom3, "@Type", DbType.String, "AllPlantOrMachine");
                        db.AddInParameter(dbcom3, "@MasterId", DbType.Int16, 0);
                        db.AddInParameter(dbcom3, "@VendorRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom3, "@Description_Machine_Model_Specs", DbType.String, dt3.Rows[i]["MachineModelSpec"]);
                        db.AddInParameter(dbcom3, "@Make", DbType.String, dt3.Rows[i]["MakePlant"]);
                        db.AddInParameter(dbcom3, "@Quantity", DbType.String, dt3.Rows[i]["QuanPlant"]);
                        db.AddInParameter(dbcom3, "@Date_of_Purchase", DbType.String, dt3.Rows[i]["DOPPlant"]);
                        db.AddInParameter(dbcom3, "@Usage", DbType.String, dt3.Rows[i]["UsagePlant"]);
                        db.ExecuteNonQuery(dbcom3, dbTran);
                    }
                    for (int i = 0; i < dt4.Rows.Count; i++)
                    {
                        DbCommand dbcom4 = db.GetStoredProcCommand("sp_VendorRegistrationMultiGridEmpDetail");
                        db.AddInParameter(dbcom4, "@Type", DbType.String, "Employeedetail");
                        db.AddInParameter(dbcom4, "@MasterId", DbType.Int16, 0);
                        db.AddInParameter(dbcom4, "@VendorRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom4, "@TOTAL_Employees", DbType.String, dt4.Rows[i]["TEmp"]);
                        db.AddInParameter(dbcom4, "@ADMINISTRATIVE", DbType.String, dt4.Rows[i]["Admins"]);
                        db.AddInParameter(dbcom4, "@TECHNICAL", DbType.String, dt4.Rows[i]["Tech"]);
                        db.AddInParameter(dbcom4, "@NON_TECHNICAL", DbType.String, dt4.Rows[i]["NonTech"]);
                        db.AddInParameter(dbcom4, "@QC_INSPECTION", DbType.String, dt4.Rows[i]["QCIns"]);
                        db.AddInParameter(dbcom4, "@SKILLED_LABOUR", DbType.String, dt4.Rows[i]["SkLab"]);
                        db.AddInParameter(dbcom4, "@UNSKILLED_LABOUR", DbType.String, dt4.Rows[i]["USKLab"]);
                        db.ExecuteNonQuery(dbcom4, dbTran);
                    }
                    for (int i = 0; i < dt5.Rows.Count; i++)
                    {
                        DbCommand dbcom5 = db.GetStoredProcCommand("sp_VendorRegistrationMultiGridTestEquipment");
                        db.AddInParameter(dbcom5, "@Type", DbType.String, "TestEquipment");
                        db.AddInParameter(dbcom5, "@MasterId", DbType.Int16, 0);
                        db.AddInParameter(dbcom5, "@VendorRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom5, "@Type_of_GAUGE_Test_Equipment", DbType.String, dt5.Rows[i]["TestEqip"]);
                        db.AddInParameter(dbcom5, "@Test_Make", DbType.String, dt5.Rows[i]["TestEqipMake"]);
                        db.AddInParameter(dbcom5, "@Least_Count", DbType.String, dt5.Rows[i]["TestLeastCount"]);
                        db.AddInParameter(dbcom5, "@Range_of_MEASURMENT", DbType.String, dt5.Rows[i]["Rangeofmeasur"]);
                        db.AddInParameter(dbcom5, "@Unit_of_MEASURMENT", DbType.String, dt5.Rows[i]["Unitofmeasur"]);
                        db.AddInParameter(dbcom5, "@CERTIFICATION_YEAR", DbType.String, dt5.Rows[i]["CertificationYear"]);
                        db.AddInParameter(dbcom5, "@Year_of_purchase", DbType.String, dt5.Rows[i]["YearofPurchase"]);
                        db.ExecuteNonQuery(dbcom5, dbTran);
                    }
                    for (int i = 0; i < dt6.Rows.Count; i++)
                    {
                        DbCommand dbcom6 = db.GetStoredProcCommand("sp_VendorRegistrationMultiGridAuthdistri");
                        db.AddInParameter(dbcom6, "@Type", DbType.String, "Distributer");
                        db.AddInParameter(dbcom6, "@VendorRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom6, "@MasterId", DbType.Int16, 0);
                        db.AddInParameter(dbcom6, "@DistributorName", DbType.String, dt6.Rows[i]["DName"]);
                        db.AddInParameter(dbcom6, "@DistributorStreetAddress", DbType.String, dt6.Rows[i]["DAddress"]);
                        db.AddInParameter(dbcom6, "@DistributorState", DbType.String, dt6.Rows[i]["DState"]);
                        db.AddInParameter(dbcom6, "@DistributorPincode", DbType.String, dt6.Rows[i]["DPincode"]);
                        db.AddInParameter(dbcom6, "@DistributorPhone", DbType.String, dt6.Rows[i]["DPhone"]);
                        db.AddInParameter(dbcom6, "@DistributorFax", DbType.String, dt6.Rows[i]["DFax"]);
                        db.AddInParameter(dbcom6, "@DistributorEmail", DbType.String, dt6.Rows[i]["DEmail"]);
                        db.ExecuteNonQuery(dbcom6, dbTran);
                    }
                    for (int i = 0; i < dt7.Rows.Count; i++)
                    {
                        DbCommand dbcom7 = db.GetStoredProcCommand("sp_VendorRegistrationMultiGridJointVentureFacility");
                        db.AddInParameter(dbcom7, "@Type", DbType.String, "JointVentureFacility");
                        db.AddInParameter(dbcom7, "@VendorRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom7, "@MasterId", DbType.Int16, 0);
                        db.AddInParameter(dbcom7, "@JointVentureName", DbType.String, dt7.Rows[i]["JVFName"]);
                        db.AddInParameter(dbcom7, "@IsJointVentureCountry", DbType.String, dt7.Rows[i]["JVFIs"]);
                        db.AddInParameter(dbcom7, "@CompleteAddress", DbType.String, dt7.Rows[i]["JVFAddress"]);
                        db.AddInParameter(dbcom7, "@ContOfficialName", DbType.String, dt7.Rows[i]["JVFOffName"]);
                        db.AddInParameter(dbcom7, "@TelephoneNo", DbType.String, dt7.Rows[i]["JVFTel"]);
                        db.AddInParameter(dbcom7, "@FaxNo", DbType.String, dt7.Rows[i]["JVFFAX"]);
                        db.AddInParameter(dbcom7, "@EmailId", DbType.String, dt7.Rows[i]["JVFEmail"]);
                        db.ExecuteNonQuery(dbcom7, dbTran);
                    }
                    for (int i = 0; i < dt8.Rows.Count; i++)
                    {
                        DbCommand dbcom8 = db.GetStoredProcCommand("sp_VendorRegistrationMultiGridOutsourcing");
                        db.AddInParameter(dbcom8, "@Type", DbType.String, "Outsourcing");
                        db.AddInParameter(dbcom8, "@VendorRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom8, "@MasterId", DbType.Int16, 0);
                        db.AddInParameter(dbcom8, "@OutsourcingMainEquipment", DbType.String, dt8.Rows[i]["MEquipment"]);
                        db.AddInParameter(dbcom8, "@OutsourcingTestEquip", DbType.String, dt8.Rows[i]["TEquipment"]);
                        db.AddInParameter(dbcom8, "@OutsourcingProcessfacility", DbType.String, dt8.Rows[i]["PFacility"]);
                        db.AddInParameter(dbcom8, "@OutsoursingNameAddressofSubContractor", DbType.String, dt8.Rows[i]["NASub"]);
                        db.ExecuteNonQuery(dbcom8, dbTran);
                    }
                    for (int i = 0; i < dt9.Rows.Count; i++)
                    {
                        DbCommand dbcom9 = db.GetStoredProcCommand("sp_VendorRegistrationMultiGridImage");
                        db.AddInParameter(dbcom9, "@Type", DbType.String, "FCertificate");
                        db.AddInParameter(dbcom9, "@VendorRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom9, "@ImageID", DbType.Int16, dt9.Rows[i]["ImageID"]);
                        db.AddInParameter(dbcom9, "@Name", DbType.String, dt9.Rows[i]["CertificateName"]);
                        db.AddInParameter(dbcom9, "@Path", DbType.String, dt9.Rows[i]["CertificateImage"]);
                        db.ExecuteNonQuery(dbcom9, dbTran);
                    }
                    for (int i = 0; i < dt10.Rows.Count; i++)
                    {
                        DbCommand dbcom10 = db.GetStoredProcCommand("sp_VendorRegistrationMultiGridImage");
                        db.AddInParameter(dbcom10, "@Type", DbType.String, "QCertificate");
                        db.AddInParameter(dbcom10, "@VendorRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom10, "@ImageID", DbType.Int16, dt10.Rows[i]["ImageID"]);
                        db.AddInParameter(dbcom10, "@Name", DbType.String, dt10.Rows[i]["CertificateName"]);
                        db.AddInParameter(dbcom10, "@Path", DbType.String, dt10.Rows[i]["CertificateImage"]);
                        db.ExecuteNonQuery(dbcom10, dbTran);
                    }
                    dbTran.Commit();
                    _msg = "Save";
                    _sysMsg = "Save";
                    return "Save";
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
        public string SaveVendorCompanyInfo2(DataTable dt1, string mCurrentID, out string _sysMsg, out string _msg)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        DbCommand dbcom1 = db.GetStoredProcCommand("sp_VendorRegistrationMultiGridOEM");
                        db.AddInParameter(dbcom1, "@MasterId", DbType.Int16, 0);
                        db.AddInParameter(dbcom1, "@VendorRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom1, "@OEMName", DbType.String, dt1.Rows[i]["FactoryName1"]);
                        db.AddInParameter(dbcom1, "@OEMAddress", DbType.String, dt1.Rows[i]["CAddress1"]);
                        db.AddInParameter(dbcom1, "@OEMCountry", DbType.String, dt1.Rows[i]["OEM1"]);
                        db.AddInParameter(dbcom1, "@OEMOfficialName", DbType.String, dt1.Rows[i]["COfficialName1"]);
                        db.AddInParameter(dbcom1, "@OEMTelephoneNo", DbType.String, dt1.Rows[i]["TeleNo1"]);
                        db.AddInParameter(dbcom1, "@OEMFaxNo", DbType.String, dt1.Rows[i]["FaxNo1"]);
                        db.AddInParameter(dbcom1, "@OEMEmailId", DbType.String, dt1.Rows[i]["EmailId1"]);
                        db.AddInParameter(dbcom1, "@FileAuthorization", DbType.String, dt1.Rows[i]["AUTHRIZATION1"]);
                        db.ExecuteNonQuery(dbcom1, dbTran);
                    }
                    dbTran.Commit();
                    _msg = "Save";
                    _sysMsg = "Save";
                    return "Save";
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
        public string SaveVendorRegisNoDetails(HybridDictionary HySaveVendorRegisdetail, DataTable dtRegisDetails, out string _sysMsg, out string _msg)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                string mCurrentID = "";
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_VendorRegisNoDetails");
                    db.AddInParameter(cmd, "@VendorRefNo", DbType.String, HySaveVendorRegisdetail["VendorRefNo"]);
                    db.AddInParameter(cmd, "@Is_PANTAN", DbType.String, HySaveVendorRegisdetail["Is_PANTAN"]);
                    db.AddInParameter(cmd, "@PanTan_No", DbType.String, HySaveVendorRegisdetail["PanTan_No"].ToString().Trim());
                    db.AddInParameter(cmd, "@GSTNo", DbType.String, HySaveVendorRegisdetail["GSTNo"].ToString().Trim());
                    db.AddInParameter(cmd, "@UAM", DbType.String, HySaveVendorRegisdetail["UAM"]);
                    db.AddInParameter(cmd, "@CIN", DbType.String, HySaveVendorRegisdetail["CIN"]);
                    db.AddInParameter(cmd, "@IsRegisterdwithgovt", DbType.String, HySaveVendorRegisdetail["IsRegisterdwithgovt"]);
                    db.AddOutParameter(cmd, "@ReturnID", DbType.String, 20);
                    db.ExecuteNonQuery(cmd, dbTran);
                    mCurrentID = db.GetParameterValue(cmd, "@ReturnID").ToString();
                    for (int i = 0; i < dtRegisDetails.Rows.Count; i++)
                    {
                        DbCommand dbcom1 = db.GetStoredProcCommand("sp_VendorRegistrationMultiGridRegisNoDetails");
                        db.AddInParameter(dbcom1, "@VendorRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom1, "@Name_PSU_UnderGovt", DbType.String, dtRegisDetails.Rows[i]["GName"]);
                        db.AddInParameter(dbcom1, "@RegistrationNo", DbType.String, dtRegisDetails.Rows[i]["GRegNo"]);
                        db.AddInParameter(dbcom1, "@Certificate_valid_upto", DbType.String, dtRegisDetails.Rows[i]["GcertifiValid"]);
                        db.AddInParameter(dbcom1, "@Upload_Registration_Certificate", DbType.String, dtRegisDetails.Rows[i]["UCertificate"]);
                        db.ExecuteNonQuery(dbcom1, dbTran);
                    }
                    dbTran.Commit();
                    _msg = "Save";
                    _sysMsg = "Save";
                    return "Save";
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
        public string SaveVendorAccountInfo(DataTable dt1, DataTable dt2, string mCurrentID, out string _sysMsg, out string _msg)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        DbCommand dbcom1 = db.GetStoredProcCommand("sp_VendorRegistrationMultiTurnOver");
                        db.AddInParameter(dbcom1, "@VendorRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom1, "@FinancialYear", DbType.String, dt1.Rows[i]["FinancialYear"]);
                        db.AddInParameter(dbcom1, "@CurrentAsset", DbType.String, dt1.Rows[i]["CurrentAsset"]);
                        db.AddInParameter(dbcom1, "@CurrentLiblities", DbType.String, dt1.Rows[i]["CurrentLiblities"]);
                        db.AddInParameter(dbcom1, "@ProfitLoss", DbType.String, dt1.Rows[i]["ProfitLoss"]);
                        db.AddInParameter(dbcom1, "@BalanceSheet", DbType.String, dt1.Rows[i]["BalanceSheet"]);
                        db.ExecuteNonQuery(dbcom1, dbTran);
                    }
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        DbCommand dbcom2 = db.GetStoredProcCommand("sp_VendorRegistrationMultiAccount");
                        db.AddInParameter(dbcom2, "@VendorRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom2, "@NameofBank", DbType.String, dt2.Rows[i]["NameofBank"]);
                        db.AddInParameter(dbcom2, "@TypeofAccount", DbType.String, dt2.Rows[i]["TypeofAccount"]);
                        db.AddInParameter(dbcom2, "@AccountNo", DbType.String, dt2.Rows[i]["AccountNo"]);
                        db.AddInParameter(dbcom2, "@MICRCode", DbType.String, dt2.Rows[i]["MICRCode"]);
                        db.AddInParameter(dbcom2, "@IFSCCode", DbType.String, dt2.Rows[i]["IFSCCode"]);
                        db.AddInParameter(dbcom2, "@Certificate", DbType.String, dt2.Rows[i]["Certificate"]);
                        db.ExecuteNonQuery(dbcom2, dbTran);
                    }
                    dbTran.Commit();
                    _msg = "Save";
                    _sysMsg = "Save";
                    return "Save";
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
        public string SaveVendorDefence(DataTable dt1, DataTable dt2, DataTable dt3, DataTable dt4, DataTable dt5, string mCurrentID, out string _sysMsg, out string _msg)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        DbCommand dbcom1 = db.GetStoredProcCommand("sp_VendorRegistrationMultiGridProductDetails");
                        db.AddInParameter(dbcom1, "@VendorRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom1, "@ProductNomenClature", DbType.String, dt1.Rows[i]["Nomenclature"]);
                        db.AddInParameter(dbcom1, "@NatoGroup", DbType.Int64, dt1.Rows[i]["NatoGroup"]);
                        if (dt1.Rows[i]["NatoClass"].ToString() == "NA")
                        { db.AddInParameter(dbcom1, "@NatoClass", DbType.Int64, 1); }
                        else
                        {
                            db.AddInParameter(dbcom1, "@NatoClass", DbType.Int64, dt1.Rows[i]["NatoClass"]);
                        }
                        if (dt1.Rows[i]["ItemCode"].ToString() == "NA")
                        { db.AddInParameter(dbcom1, "@ItemCode", DbType.Int64, 1); }
                        else
                        {
                            db.AddInParameter(dbcom1, "@ItemCode", DbType.Int64, dt1.Rows[i]["ItemCode"]);
                        }
                        db.AddInParameter(dbcom1, "@HSNCode", DbType.String, dt1.Rows[i]["HSNCode"]);
                        db.ExecuteNonQuery(dbcom1, dbTran);
                    }
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        DbCommand dbcom2 = db.GetStoredProcCommand("sp_VendorRegistrationMultiGridTechnologyDetails");
                        db.AddInParameter(dbcom2, "@VendorRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom2, "@ProductNomenClature1", DbType.String, dt2.Rows[i]["TechNomenclature"]);
                        db.AddInParameter(dbcom2, "@TechnologyLevel1", DbType.Int64, dt2.Rows[i]["Technology1"]);
                        if (dt2.Rows[i]["Technology2"].ToString() == "NA")
                        {
                            db.AddInParameter(dbcom2, "@Technology2", DbType.Int64, 1);
                        }
                        else
                        {
                            db.AddInParameter(dbcom2, "@TechnologyLevel2", DbType.Int64, dt2.Rows[i]["Technology2"]);
                        }
                        if (dt2.Rows[i]["Technology3"].ToString() == "NA")
                        {
                            db.AddInParameter(dbcom2, "@TechnologyLevel3", DbType.Int64, 1);
                        }
                        else
                        {
                            db.AddInParameter(dbcom2, "@TechnologyLevel3", DbType.Int64, dt2.Rows[i]["Technology3"]);
                        }
                        db.ExecuteNonQuery(dbcom2, dbTran);
                    }
                    for (int i = 0; i < dt3.Rows.Count; i++)
                    {
                        DbCommand dbcom3 = db.GetStoredProcCommand("sp_VendorRegistrationMultiGridSourceRawMaterial");
                        db.AddInParameter(dbcom3, "@VendorRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom3, "@Items", DbType.String, dt3.Rows[i]["Items"]);
                        db.AddInParameter(dbcom3, "@BasicRawMeterial", DbType.String, dt3.Rows[i]["RawMeterial"]);
                        db.AddInParameter(dbcom3, "@SourceofMaterial", DbType.Int64, dt3.Rows[i]["SourceMeterial"]);
                        db.AddInParameter(dbcom3, "@Major_Raw_Material_Suppliers", DbType.String, dt3.Rows[i]["MeterailSupplier"]);
                        db.ExecuteNonQuery(dbcom3, dbTran);
                    }
                    for (int i = 0; i < dt4.Rows.Count; i++)
                    {
                        DbCommand dbcom4 = db.GetStoredProcCommand("sp_VendorRegistrationMultiGridIteProducedSupplied");
                        db.AddInParameter(dbcom4, "@VendorRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom4, "@Reputed_Customer", DbType.String, dt4.Rows[i]["NameCust"]);
                        db.AddInParameter(dbcom4, "@Description", DbType.String, dt4.Rows[i]["DesStoreSupp"]);
                        db.AddInParameter(dbcom4, "@SupplyNoDate", DbType.String, dt4.Rows[i]["OderNoorDate"]);
                        db.AddInParameter(dbcom4, "@OrderQuantity", DbType.String, dt4.Rows[i]["OrderQty"]);
                        db.AddInParameter(dbcom4, "@SuppliedQuantity", DbType.String, dt4.Rows[i]["ValueQtySupp"]);
                        db.AddInParameter(dbcom4, "@Date2", DbType.Date, dt4.Rows[i]["DateofLastSupp"]);
                        db.ExecuteNonQuery(dbcom4, dbTran);
                    }
                    for (int i = 0; i < dt5.Rows.Count; i++)
                    {
                        DbCommand dbcom5 = db.GetStoredProcCommand("sp_VendorRegistrationMultiGridIteProducedNotSupplied");
                        db.AddInParameter(dbcom5, "@VendorRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom5, "@Reputed_Customer1", DbType.String, dt5.Rows[i]["NameCust1"]);
                        db.AddInParameter(dbcom5, "@Description1", DbType.String, dt5.Rows[i]["DesStoreSupp1"]);
                        db.AddInParameter(dbcom5, "@SupplyNoDate1", DbType.String, dt5.Rows[i]["OderNoorDate1"]);
                        db.AddInParameter(dbcom5, "@OrderQuantity1", DbType.String, dt5.Rows[i]["OrderQty1"]);
                        db.AddInParameter(dbcom5, "@SuppliedQuantity1", DbType.String, dt5.Rows[i]["ValueQtySupp1"]);
                        db.AddInParameter(dbcom5, "@Date21", DbType.Date, dt5.Rows[i]["DateofLastSupp1"]);
                        db.ExecuteNonQuery(dbcom5, dbTran);
                    }
                    dbTran.Commit();
                    _msg = "Save";
                    _sysMsg = "Save";
                    return "Save";
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
        #endregion
        #region Vendor RetriveCode
        public DataTable RetriveVendor(Int64 Vid, string VRefNo, string VEmail, string RetFor)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_VendorRegisDetail");
                    db.AddInParameter(cmd, "@VendorId", DbType.Int64, Vid);
                    db.AddInParameter(cmd, "@VendorRefNo", DbType.String, VRefNo);
                    db.AddInParameter(cmd, "@Email", DbType.String, VEmail);
                    db.AddInParameter(cmd, "@WorkCodeFor", DbType.String, RetFor);
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
