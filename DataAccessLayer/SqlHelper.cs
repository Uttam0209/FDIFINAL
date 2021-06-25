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
            dbCommand.CommandTimeout = 0;
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
        public string VerifyEmailVendor(HybridDictionary hyLogin, out string _msg, out string _mp)
        {
            try
            {
                DbCommand _dbCmd = db.GetStoredProcCommand("sp_verify_LogEmailVendor");
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
        public string VerifyVendorEmployee(HybridDictionary hyLogin, out string _msg, out string Defaultpage, out string CompanyName, out string VUser)
        {
            try
            {
                DbCommand _dbCmd = db.GetStoredProcCommand("sp_Verify_VendorLogin");
                db.AddInParameter(_dbCmd, "@UserName", DbType.String, hyLogin["UserName"]);
                db.AddInParameter(_dbCmd, "@Password", DbType.String, hyLogin["Password"]);
                db.AddOutParameter(_dbCmd, "@CompanyRefNo", DbType.String, 100);
                db.AddOutParameter(_dbCmd, "@LType", DbType.String, 50);
                db.AddOutParameter(_dbCmd, "@Defaultpage", DbType.String, 50);
                db.AddOutParameter(_dbCmd, "@CompanyName", DbType.String, 300);
                db.AddOutParameter(_dbCmd, "@UName", DbType.String, 300);
                db.ExecuteNonQuery(_dbCmd);
                string Comp_ID = db.GetParameterValue(_dbCmd, "@CompanyRefNo").ToString();
                string ID = db.GetParameterValue(_dbCmd, "@LType").ToString();
                Defaultpage = db.GetParameterValue(_dbCmd, "@Defaultpage").ToString();
                CompanyName = db.GetParameterValue(_dbCmd, "@CompanyName").ToString();
                VUser = db.GetParameterValue(_dbCmd, "@UName").ToString();
                _msg = ID;
                return Comp_ID;

            }
            catch (SqlException ex)
            {
                _msg = "0";
                Defaultpage = "0";
                CompanyName = "";
                VUser = "";
                return "";
            }
        }

        public string VerifyHelpdeskEmployee(HybridDictionary hyLogin, out string _msg, out string Defaultpage)
        {
            try
            {
                DbCommand _dbCmd = db.GetStoredProcCommand("sp_Verify_HelpdeskLogin");
                db.AddInParameter(_dbCmd, "@UserName", DbType.String, hyLogin["UserName"]);
                db.AddInParameter(_dbCmd, "@Password", DbType.String, hyLogin["Password"]);
                db.AddInParameter(_dbCmd, "@Brow", DbType.String, hyLogin["Brow"]);
                db.AddInParameter(_dbCmd, "@ip", DbType.String, hyLogin["ip"]);
                db.AddOutParameter(_dbCmd, "@CompanyRefNo", DbType.Int64, 50);
                db.AddOutParameter(_dbCmd, "@LType", DbType.String, 50);
                db.AddOutParameter(_dbCmd, "@Defaultpage", DbType.String, 70);
                db.ExecuteNonQuery(_dbCmd);
                Int64 Comp_ID = Convert.ToInt64(db.GetParameterValue(_dbCmd, "@CompanyRefNo").ToString());
                string ID = db.GetParameterValue(_dbCmd, "@LType").ToString();
                Defaultpage = db.GetParameterValue(_dbCmd, "@Defaultpage").ToString();
                _msg = ID;
                return Comp_ID.ToString();

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
        public DataTable DeleteRecord1(string CompRefNo, string Criteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_DeleteMasterRecord");
                    db.AddInParameter(cmd, "@CompRefNo", DbType.String, CompRefNo);
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
        public string InsertLogFeedbackMail(HybridDictionary hyfeedlog, out string sysMsg, out string msg)
        {
            using (DbConnection dbcon = db.CreateConnection())
            {
                dbcon.Open();
                DbTransaction dbTran = dbcon.BeginTransaction();
                try
                {
                    DbCommand dbcom = db.GetStoredProcCommand("sp_LogFeedbackMail");
                    db.AddInParameter(dbcom, "@IsMailSend", DbType.String, hyfeedlog["IsMailSend"]);
                    db.AddInParameter(dbcom, "@RecInsTime", DbType.String, hyfeedlog["RecInsTime"]);
                    db.ExecuteNonQuery(dbcom, dbTran);
                    dbTran.Commit();
                    msg = "Save";
                    sysMsg = "Save";
                    return "Save";
                }
                catch (SqlException ex)
                {
                    dbTran.Rollback();
                    msg = ex.Message;
                    sysMsg = ex.Message;
                    return "-1";
                }
                finally
                {
                    dbcon.Close();
                }
            }
        }
        public string SaveUserIP(HybridDictionary hysaveip, out string _sysMsg, out string _msg)
        {
            using (DbConnection dbcon = db.CreateConnection())
            {
                dbcon.Open();
                DbTransaction dbTran = dbcon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("StoreIPAddressDetail");
                    db.AddInParameter(cmd, "@ProductRefNo", DbType.String, hysaveip["ProductRefNo"]);
                    db.AddInParameter(cmd, "@IPAddress", DbType.String, hysaveip["IPAddress"]);
                    db.AddInParameter(cmd, "@VisitedDate", DbType.DateTime, hysaveip["VisitedDate"]);
                    db.AddInParameter(cmd, "@VisitedTime", DbType.DateTime, hysaveip["VisitedTime"]);
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
                    db.AddInParameter(dbcom, "@IPAddress", DbType.String, hyLogOut["IPAddress"]);
                    db.AddInParameter(dbcom, "@SystemName", DbType.String, hyLogOut["SystemName"]);
                    db.AddInParameter(dbcom, "@Activity", DbType.String, hyLogOut["Activity"]);
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
        public string SaveCodeProduct(HybridDictionary hyProduct, DataTable dt, DataTable dtPdf, DataTable dtEstimateQuantity, DataTable dtEstimateQuantity1, out string _sysMsg, out string _msg, string Criteria)
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
                    db.AddInParameter(cmd, "@FeatursandDetail", DbType.String, hyProduct["FeatursandDetail"]);
                    db.AddInParameter(cmd, "@OEMPartNumber", DbType.String, hyProduct["OEMPartNumber"].ToString().Trim());
                    db.AddInParameter(cmd, "@OEMName", DbType.String, hyProduct["OEMName"].ToString().Trim());
                    db.AddInParameter(cmd, "@OEMCountry", DbType.Int64, hyProduct["OEMCountry"].ToString().Trim());
                    db.AddInParameter(cmd, "@OEMAddress", DbType.String, hyProduct["OEMAddress"].ToString().Trim());
                    db.AddInParameter(cmd, "@DPSUPartNumber", DbType.String, hyProduct["DPSUPartNumber"]);
                    db.AddInParameter(cmd, "@HsnCode8digit", DbType.String, hyProduct["HsnCode8digit"]);
                    db.AddInParameter(cmd, "@EndUser", DbType.String, hyProduct["EndUser"]);
                    db.AddInParameter(cmd, "@Platform", DbType.Int64, hyProduct["Platform"]);
                    db.AddInParameter(cmd, "@NomenclatureOfMainSystem", DbType.Int64, hyProduct["NomenclatureOfMainSystem"]);
                    db.AddInParameter(cmd, "@TechnologyLevel1", DbType.Int64, hyProduct["TechnologyLevel1"]);
                    db.AddInParameter(cmd, "@TechnologyLevel2", DbType.Int64, hyProduct["TechnologyLevel2"]);
                    db.AddInParameter(cmd, "@IsProductImported", DbType.String, hyProduct["IsProductImported"]);
                    db.AddInParameter(cmd, "@IndTargetYear", DbType.String, hyProduct["IndTargetYear"]);
                    db.AddInParameter(cmd, "@EOIStatus", DbType.String, hyProduct["EOIStatus"].ToString().Trim());
                    db.AddInParameter(cmd, "@EOIURL", DbType.String, hyProduct["EOIURL"]);
                    db.AddInParameter(cmd, "@EOIStartDate", DbType.String, hyProduct["EOIStartDate"]);
                    db.AddInParameter(cmd, "@EOIEndDate", DbType.String, hyProduct["EOIEndDate"]);
                    db.AddInParameter(cmd, "@SupplyOrderStatus", DbType.String, hyProduct["SupplyOrderStatus"].ToString().Trim());
                    db.AddInParameter(cmd, "@SupplyManfutureName", DbType.String, hyProduct["SupplyManfutureName"].ToString().Trim());
                    db.AddInParameter(cmd, "@SupplyManfutureAddress", DbType.String, hyProduct["SupplyManfutureAddress"].ToString().Trim());
                    db.AddInParameter(cmd, "@SupplyOrderValue", DbType.String, hyProduct["SupplyOrderValue"].ToString().Trim());
                    db.AddInParameter(cmd, "@SupplyDeliveryDate", DbType.String, hyProduct["SupplyDeliveryDate"].ToString().Trim());
                    db.AddInParameter(cmd, "@SupplyOrderDate", DbType.String, hyProduct["SupplyOrderDate"].ToString().Trim());
                    db.AddInParameter(cmd, "@SanctionOrderStatus", DbType.String, hyProduct["SanctionOrderStatus"].ToString().Trim());
                    db.AddInParameter(cmd, "@SanctionReason", DbType.String, hyProduct["SanctionReason"].ToString().Trim());
                    db.AddInParameter(cmd, "@SanctionOrderDate", DbType.String, hyProduct["SanctionOrderDate"].ToString().Trim());
                    db.AddInParameter(cmd, "@SanctionManfutureName", DbType.String, hyProduct["SanctionManfutureName"].ToString().Trim());
                    db.AddInParameter(cmd, "@SanctionManfutureAddress", DbType.String, hyProduct["SanctionManfutureAddress"].ToString().Trim());
                    db.AddInParameter(cmd, "@IndProcess", DbType.String, hyProduct["IndProcess"]);
                    db.AddInParameter(cmd, "@IsIndeginized", DbType.String, hyProduct["IsIndeginized"].ToString().Trim());
                    db.AddInParameter(cmd, "@IndeginizedDate", DbType.DateTime, hyProduct["IndeginizedDate"]);
                    db.AddInParameter(cmd, "@IndeginizedMaxValue", DbType.Decimal, hyProduct["IndeginizedMaxValue"]);
                    db.AddInParameter(cmd, "@ManufactureName", DbType.String, hyProduct["ManufactureName"]);
                    db.AddInParameter(cmd, "@ManufactureAddress", DbType.String, hyProduct["ManufactureAddress"]);
                    db.AddInParameter(cmd, "@YearofIndiginization", DbType.Int64, hyProduct["YearofIndiginization"]);
                    db.AddInParameter(cmd, "@PurposeofProcurement", DbType.String, hyProduct["PurposeofProcurement"]);
                    db.AddInParameter(cmd, "@QAAgency", DbType.String, hyProduct["QAAgency"].ToString().Trim());
                    db.AddInParameter(cmd, "@NodelDetail", DbType.Int16, hyProduct["NodelDetail"]);
                    db.AddInParameter(cmd, "@NodalDetail2", DbType.Int16, hyProduct["NodalDetail2"]);
                    db.AddInParameter(cmd, "@IsShowGeneral", DbType.String, hyProduct["ShowGeneralDec"]);
                    db.AddInParameter(cmd, "@ViewOnlyStatus", DbType.String, hyProduct["ViewOnlyStatus"]);
                    db.AddInParameter(cmd, "@ViewOnlyReasone", DbType.String, hyProduct["ViewOnlyReasone"]);
                    db.AddInParameter(cmd, "@Role", DbType.String, hyProduct["Role"]);
                    db.AddInParameter(cmd, "@CreatedBy", DbType.String, hyProduct["CreatedBy"]);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, Criteria);
                    db.AddOutParameter(cmd, "@ReturnID", DbType.String, 70);
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
                        db.AddInParameter(dbcom1, "@FType", DbType.String, "Image");
                        db.ExecuteNonQuery(dbcom1, dbTran);
                    }
                    for (int i = 0; i < dtPdf.Rows.Count; i++)
                    {
                        DbCommand dbcom6 = db.GetStoredProcCommand("sp_trn_Image");
                        db.AddInParameter(dbcom6, "@ImageID", DbType.Int64, dtPdf.Rows[i]["PdfID"]);
                        db.AddInParameter(dbcom6, "@ProductRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom6, "@ImageName", DbType.String, dtPdf.Rows[i]["PdfName"]);
                        db.AddInParameter(dbcom6, "@ImageType", DbType.String, dtPdf.Rows[i]["PdfType"]);
                        db.AddInParameter(dbcom6, "@ImageActualSize", DbType.Int64, dtPdf.Rows[i]["PdfActualSize"]);
                        db.AddInParameter(dbcom6, "@Priority", DbType.String, dtPdf.Rows[i]["Priority"]);
                        db.AddInParameter(dbcom6, "@CompanyRefNo", DbType.String, dtPdf.Rows[i]["PCompanyRefNo"]);
                        db.AddInParameter(dbcom6, "@FType", DbType.String, "PDF");
                        db.ExecuteNonQuery(dbcom6, dbTran);
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
                        db.AddInParameter(dbcom3, "@Type", DbType.String, dtEstimateQuantity.Rows[k]["Type"]);
                        db.ExecuteNonQuery(dbcom3, dbTran);
                    }
                    for (int x = 0; x < dtEstimateQuantity1.Rows.Count; x++)
                    {
                        DbCommand dbcom5 = db.GetStoredProcCommand("sp_trn_ProdQtyPrice");
                        db.AddInParameter(dbcom5, "@ProdQtyPriceId", DbType.Int64, dtEstimateQuantity1.Rows[x]["ProdQtyPriceId"]);
                        db.AddInParameter(dbcom5, "@ProductRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom5, "@Year", DbType.Int64, dtEstimateQuantity1.Rows[x]["Year"]);
                        db.AddInParameter(dbcom5, "@FYear", DbType.String, dtEstimateQuantity1.Rows[x]["FYear"]);
                        db.AddInParameter(dbcom5, "@EstimatedQty", DbType.String, dtEstimateQuantity1.Rows[x]["EstimatedQty"]);
                        db.AddInParameter(dbcom5, "@Unit", DbType.String, dtEstimateQuantity1.Rows[x]["Unit"]);
                        db.AddInParameter(dbcom5, "@EstimatedPrice", DbType.String, dtEstimateQuantity1.Rows[x]["EstimatedPrice"]);
                        db.AddInParameter(dbcom5, "@Type", DbType.String, dtEstimateQuantity1.Rows[x]["Type"]);
                        db.ExecuteNonQuery(dbcom5, dbTran);
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
        public string AddUserAnalytics(HybridDictionary hysave, out string _sysMsg, out string _msg)
        {
            using (DbConnection dbcon = db.CreateConnection())
            {
                dbcon.Open();
                DbTransaction dbTran = dbcon.BeginTransaction();
                try
                {
                    DbCommand dbcom = db.GetStoredProcCommand("sp_UserAnalytics");
                    db.AddInParameter(dbcom, "@clientip", DbType.String, hysave["IPAddress"]);
                    db.AddInParameter(dbcom, "@clientbrowser", DbType.String, hysave["Browser"]);
                    db.AddInParameter(dbcom, "@PageURL", DbType.String, hysave["PageURL"]);
                    db.AddInParameter(dbcom, "@UrlReferrer", DbType.String, hysave["UrlReferrer"]);
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
        public string InsertFeedback(HybridDictionary hySavefeed, out string sysMsg, out string msg)
        {
            using (DbConnection dbcon = db.CreateConnection())
            {
                dbcon.Open();
                DbTransaction dbTran = dbcon.BeginTransaction();
                try
                {
                    DbCommand dbcom = db.GetStoredProcCommand("sp_feedback_details");
                    db.AddInParameter(dbcom, "@FeedBackId", DbType.String, hySavefeed["FeedBackId"]);
                    db.AddInParameter(dbcom, "@UserName", DbType.String, hySavefeed["UserName"]);
                    db.AddInParameter(dbcom, "@UserEmail", DbType.String, hySavefeed["UserEmail"]);
                    db.AddInParameter(dbcom, "@MobileNo", DbType.String, hySavefeed["MobileNo"]);
                    db.AddInParameter(dbcom, "@Message", DbType.String, hySavefeed["Message"]);
                    db.AddInParameter(dbcom, "@CompanyRefNo", DbType.String, hySavefeed["CompanyRefNo"]);
                    db.AddInParameter(dbcom, "@CompanyName", DbType.String, hySavefeed["CompanyName"]);
                    db.AddOutParameter(dbcom, "@ReturnId", DbType.String, 50);
                    db.ExecuteNonQuery(dbcom, dbTran);
                    dbTran.Commit();
                    msg = "Save";
                    sysMsg = "Save";
                    return "Save";
                }
                catch (SqlException ex)
                {
                    dbTran.Rollback();
                    msg = ex.Message;
                    sysMsg = ex.Message;
                    return "-1";
                }
                finally
                {
                    dbcon.Close();
                }
            }
        }
        public string InsertFeedbackReply(HybridDictionary hySavefeed, out string _sysMsg, out string _msg)
        {
            using (DbConnection dbcon = db.CreateConnection())
            {
                dbcon.Open();
                DbTransaction dbTran = dbcon.BeginTransaction();
                try
                {
                    DbCommand dbcom = db.GetStoredProcCommand("sp_feedback_reply");
                    db.AddInParameter(dbcom, "@UsrName", DbType.String, hySavefeed["UsrName"]);
                    db.AddInParameter(dbcom, "@UsrMsg", DbType.String, hySavefeed["UsrMsg"]);
                    db.AddInParameter(dbcom, "@CompMsg", DbType.String, hySavefeed["CompMsg"]);
                    db.AddInParameter(dbcom, "@CompEmail", DbType.String, hySavefeed["CompEmail"]);
                    db.AddInParameter(dbcom, "@UsrEmail", DbType.String, hySavefeed["UsrEmail"]);
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
                    cmd.CommandTimeout = 0;
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
        public string Add_ErrorLog(HybridDictionary hyLog, out string _sysMsg, out string _msg)
        {
            using (DbConnection dbcon = db.CreateConnection())
            {
                dbcon.Open();
                DbTransaction dbTran = dbcon.BeginTransaction();
                try
                {
                    DbCommand dbcom = db.GetStoredProcCommand("SP_ExceptionLoggingToDataBase");
                    db.AddInParameter(dbcom, "@ExceptionMsg", DbType.String, hyLog["ExceptionMsg"]);
                    db.AddInParameter(dbcom, "@ExceptionType", DbType.String, hyLog["ExceptionType"]);
                    db.AddInParameter(dbcom, "@ExceptionSource", DbType.String, hyLog["ExceptionSource"]);
                    db.AddInParameter(dbcom, "@ExceptionURL", DbType.String, hyLog["ExceptionURL"]);
                    db.AddInParameter(dbcom, "@Exceptionclientip", DbType.String, hyLog["IPAddress"]);
                    db.AddInParameter(dbcom, "@Exceptionclientbrowser", DbType.String, hyLog["Browser"]);

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
        public DataTable NewRetriveFilterCode(string Criteria, string search, string value, string purpose, string role, int reqid, int refno, int id)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_NewRetrieveCode");
                    db.AddInParameter(cmd, "@criteria", DbType.String, Criteria);
                    db.AddInParameter(cmd, "@Search", DbType.String, search);
                    db.AddInParameter(cmd, "@Value", DbType.String, value);
                    db.AddInParameter(cmd, "@purpose", DbType.String, purpose);
                    db.AddInParameter(cmd, "@Role", DbType.String, role);
                    db.AddInParameter(cmd, "@reqid", DbType.String, reqid);
                    db.AddInParameter(cmd, "@refno", DbType.String, refno);
                    db.AddInParameter(cmd, "@Id", DbType.String, id);
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
        Int16 mFYearid;
        public string SaveExcelProduct1(DataTable dtMaster, HybridDictionary hyadvertice)
        {
            using (DbConnection Connection = db.CreateConnection())
            {
                Connection.Open();
                DbTransaction Transaction = Connection.BeginTransaction();
                Int32 errorRow = -1;
                string mCurrentID = "";
                try
                {
                    for (int k = 0; k < dtMaster.Rows.Count; k++)
                    {
                        errorRow = k;
                        DbCommand cmd = db.GetStoredProcCommand("sp_InsertProductFromExcel1");
                        db.AddInParameter(cmd, "@ProductID", DbType.Int64, hyadvertice["ProductID"]);
                        db.AddInParameter(cmd, "@CompanyRefNo", DbType.String, hyadvertice["CompanyRefNo"]);
                        db.AddInParameter(cmd, "@ProductLevel1", DbType.Int64, hyadvertice["ProductLevel1"]);
                        db.AddInParameter(cmd, "@ProductLevel2", DbType.Int64, hyadvertice["ProductLevel2"]);
                        db.AddInParameter(cmd, "@ProductLevel3", DbType.Int64, hyadvertice["ProductLevel3"]);
                        db.AddInParameter(cmd, "@NSCCode", DbType.String, hyadvertice["NSCCode"]);
                        db.AddInParameter(cmd, "@OEMCountry", DbType.Int64, hyadvertice["OEMCountry"].ToString().Trim());
                        db.AddInParameter(cmd, "@EndUser", DbType.String, hyadvertice["EndUser"]);
                        db.AddInParameter(cmd, "@Platform", DbType.Int64, hyadvertice["Platform"]);
                        db.AddInParameter(cmd, "@NomenclatureOfMainSystem", DbType.Int64, hyadvertice["NomenclatureOfMainSystem"]);
                        db.AddInParameter(cmd, "@TechnologyLevel1", DbType.Int64, hyadvertice["TechnologyLevel1"]);
                        db.AddInParameter(cmd, "@TechnologyLevel2", DbType.Int64, hyadvertice["TechnologyLevel2"]);
                        db.AddInParameter(cmd, "@IsProductImported", DbType.String, hyadvertice["IsProductImported"]);
                        db.AddInParameter(cmd, "@PurposeofProcurement", DbType.String, hyadvertice["PurposeofProcurement"]);
                        db.AddInParameter(cmd, "@QAAgency", DbType.String, hyadvertice["QAAgency"].ToString().Trim());
                        db.AddInParameter(cmd, "@IndProcess", DbType.String, hyadvertice["IndProcess"]);
                        db.AddInParameter(cmd, "@NodelDetail", DbType.Int16, hyadvertice["NodelDetail"]);
                        db.AddInParameter(cmd, "@IsShowGeneral", DbType.String, hyadvertice["ShowGeneralDec"]);
                        db.AddInParameter(cmd, "@Role", DbType.String, hyadvertice["Role"]);
                        db.AddInParameter(cmd, "@CreatedBy", DbType.String, hyadvertice["CreatedBy"]);
                        db.AddInParameter(cmd, "@ProductDescription", DbType.String, dtMaster.Rows[k]["ITEM NAME"]);
                        db.AddInParameter(cmd, "@NIINCode", DbType.String, dtMaster.Rows[k]["NIIN CODE"]);
                        db.AddInParameter(cmd, "@FeatursandDetail", DbType.String, dtMaster.Rows[k]["FEATURES DETAILS"]);
                        db.AddInParameter(cmd, "@OEMPartNumber", DbType.String, dtMaster.Rows[k]["OEM PART NUMBER"].ToString().Trim());
                        db.AddInParameter(cmd, "@OEMName", DbType.String, dtMaster.Rows[k]["OEM NAME"].ToString().Trim());
                        db.AddInParameter(cmd, "@OEMAddress", DbType.String, dtMaster.Rows[k]["OEM ADDRESS"].ToString().Trim());
                        db.AddInParameter(cmd, "@DPSUPartNumber", DbType.String, dtMaster.Rows[k]["DPSU PART NUMBER"]);
                        db.AddInParameter(cmd, "@HsnCode8digit", DbType.String, dtMaster.Rows[k]["HSN CODE"]);
                        db.AddInParameter(cmd, "@IndTargetYear", DbType.String, dtMaster.Rows[k]["STARTING INDIGENIZATION TARGET YEAR"]);
                        db.AddInParameter(cmd, "@EOIStatus", DbType.String, dtMaster.Rows[k]["EOI/RFP"].ToString().Trim());
                        db.AddInParameter(cmd, "@EOIURL", DbType.String, dtMaster.Rows[k]["EOI LINK"]);
                        db.AddInParameter(cmd, "@EOIStartDate", DbType.String, dtMaster.Rows[k]["EOI START DATE"]);
                        db.AddInParameter(cmd, "@EOIEndDate", DbType.String, dtMaster.Rows[k]["EOI END DATE"]);
                        db.AddInParameter(cmd, "@SupplyOrderStatus", DbType.String, dtMaster.Rows[k]["SUPPLY ORDER PLACED"].ToString().Trim());
                        db.AddInParameter(cmd, "@SupplyManfutureName", DbType.String, dtMaster.Rows[k]["SUPPLY ORDER MANUFACTURE NAME"].ToString().Trim());
                        db.AddInParameter(cmd, "@SupplyManfutureAddress", DbType.String, dtMaster.Rows[k]["SUPPLY ORDER ADDRESS"].ToString().Trim());
                        db.AddInParameter(cmd, "@SupplyOrderValue", DbType.String, dtMaster.Rows[k]["SUPPLY ORDER VALUE IN (RS LAKHS)"].ToString().Trim());
                        db.AddInParameter(cmd, "@SupplyDeliveryDate", DbType.String, dtMaster.Rows[k]["SUPPLY ORDER DELIVERY (COMPLIANCE DATE)"].ToString().Trim());
                        db.AddInParameter(cmd, "@SupplyOrderDate", DbType.String, dtMaster.Rows[k]["SUPPLY ORDER DATE"].ToString().Trim());
                        db.AddInParameter(cmd, "@IsIndeginized", DbType.String, dtMaster.Rows[k]["ITEM INDIGENIZED"].ToString().Trim());
                        db.AddInParameter(cmd, "@ManufactureName", DbType.String, dtMaster.Rows[k]["MANUFACTURER NAME"]);
                        db.AddInParameter(cmd, "@ManufactureAddress", DbType.String, dtMaster.Rows[k]["MANUFACTURER ADDRESS"]);
                        CheckFYear(dtMaster.Rows[k]["YEAR OF INDIGINIZATION"].ToString());
                        db.AddInParameter(cmd, "@YearofIndiginization", DbType.Int64, mFYearid.ToString());
                        db.AddOutParameter(cmd, "@ReturnID", DbType.String, 70);
                        db.ExecuteNonQuery(cmd, Transaction);
                        mCurrentID = db.GetParameterValue(cmd, "@ReturnID").ToString();
                        if (dtMaster.Rows[k]["IMPORTED YEAR 17-18"].ToString() == "2017-18" && dtMaster.Rows[k]["IMPORTED VALUE IN RS LAKH (QTY*PRICE)"].ToString() != "")
                        {
                            DbCommand dbcom3 = db.GetStoredProcCommand("sp_trn_ProdQtyPrice");
                            db.AddInParameter(dbcom3, "@ProdQtyPriceId", DbType.Int64, 0);
                            db.AddInParameter(dbcom3, "@ProductRefNo", DbType.String, mCurrentID);
                            db.AddInParameter(dbcom3, "@Year", DbType.Int64, 1);
                            db.AddInParameter(dbcom3, "@Type", DbType.String, "O");
                            db.AddInParameter(dbcom3, "@FYear", DbType.String, dtMaster.Rows[k]["IMPORTED YEAR 17-18"]);
                            db.AddInParameter(dbcom3, "@EstimatedQty", DbType.String, dtMaster.Rows[k]["IMPORTED QUANTITY"]);
                            db.AddInParameter(dbcom3, "@Unit", DbType.String, dtMaster.Rows[k]["IMPORTED UNIT"]);
                            db.AddInParameter(dbcom3, "@EstimatedPrice", DbType.String, dtMaster.Rows[k]["IMPORTED VALUE IN RS LAKH (QTY*PRICE)"]);
                            db.ExecuteNonQuery(dbcom3, Transaction);
                        }
                        if (dtMaster.Rows[k]["IMPORTED YEAR 18-19"].ToString() == "2018-19" && dtMaster.Rows[k]["IMPORTED VALUE IN RS LAKH (QTY*PRICE)1"].ToString() != "")
                        {
                            DbCommand dbcom4 = db.GetStoredProcCommand("sp_trn_ProdQtyPrice");
                            db.AddInParameter(dbcom4, "@ProdQtyPriceId", DbType.Int64, 0);
                            db.AddInParameter(dbcom4, "@ProductRefNo", DbType.String, mCurrentID);
                            db.AddInParameter(dbcom4, "@Year", DbType.Int64, 2);
                            db.AddInParameter(dbcom4, "@Type", DbType.String, "O");
                            db.AddInParameter(dbcom4, "@FYear", DbType.String, dtMaster.Rows[k]["IMPORTED YEAR 18-19"]);
                            db.AddInParameter(dbcom4, "@EstimatedQty", DbType.String, dtMaster.Rows[k]["IMPORTED QUANTITY1"]);
                            db.AddInParameter(dbcom4, "@Unit", DbType.String, dtMaster.Rows[k]["IMPORTED UNIT1"]);
                            db.AddInParameter(dbcom4, "@EstimatedPrice", DbType.String, dtMaster.Rows[k]["IMPORTED VALUE IN RS LAKH (QTY*PRICE)1"]);
                            db.ExecuteNonQuery(dbcom4, Transaction);
                        }
                        if (dtMaster.Rows[k]["IMPORTED YEAR 19-20"].ToString() == "2019-20" && dtMaster.Rows[k]["IMPORTED VALUE IN RS LAKH (QTY*PRICE)2"].ToString() != "")
                        {
                            DbCommand dbcom5 = db.GetStoredProcCommand("sp_trn_ProdQtyPrice");
                            db.AddInParameter(dbcom5, "@ProdQtyPriceId", DbType.Int64, 0);
                            db.AddInParameter(dbcom5, "@ProductRefNo", DbType.String, mCurrentID);
                            db.AddInParameter(dbcom5, "@Year", DbType.Int64, 3);
                            db.AddInParameter(dbcom5, "@Type", DbType.String, "O");
                            db.AddInParameter(dbcom5, "@FYear", DbType.String, dtMaster.Rows[k]["IMPORTED YEAR 19-20"]);
                            db.AddInParameter(dbcom5, "@EstimatedQty", DbType.String, dtMaster.Rows[k]["IMPORTED QUANTITY2"]);
                            db.AddInParameter(dbcom5, "@Unit", DbType.String, dtMaster.Rows[k]["IMPORTED UNIT2"]);
                            db.AddInParameter(dbcom5, "@EstimatedPrice", DbType.String, dtMaster.Rows[k]["IMPORTED VALUE IN RS LAKH (QTY*PRICE)2"]);
                            db.ExecuteNonQuery(dbcom5, Transaction);
                        }
                        if (dtMaster.Rows[k]["IMPORTED YEAR 20-21"].ToString() == "2020-21" && dtMaster.Rows[k]["IMPORTED VALUE IN RS LAKH (QTY*PRICE)3"].ToString() != "")
                        {
                            DbCommand dbcom6 = db.GetStoredProcCommand("sp_trn_ProdQtyPrice");
                            db.AddInParameter(dbcom6, "@ProdQtyPriceId", DbType.Int64, 0);
                            db.AddInParameter(dbcom6, "@ProductRefNo", DbType.String, mCurrentID);
                            db.AddInParameter(dbcom6, "@Year", DbType.Int64, 4);
                            db.AddInParameter(dbcom6, "@Type", DbType.String, "O");
                            db.AddInParameter(dbcom6, "@FYear", DbType.String, dtMaster.Rows[k]["IMPORTED YEAR 20-21"]);
                            db.AddInParameter(dbcom6, "@EstimatedQty", DbType.String, dtMaster.Rows[k]["IMPORTED QUANTITY3"]);
                            db.AddInParameter(dbcom6, "@Unit", DbType.String, dtMaster.Rows[k]["IMPORTED UNIT3"]);
                            db.AddInParameter(dbcom6, "@EstimatedPrice", DbType.String, dtMaster.Rows[k]["IMPORTED VALUE IN RS LAKH (QTY*PRICE)3"]);
                            db.ExecuteNonQuery(dbcom6, Transaction);
                        }
                        if (dtMaster.Rows[k]["IMPORTED YEAR 21-22"].ToString() == "2021-22" && dtMaster.Rows[k]["IMPORTED VALUE IN RS LAKH (QTY*PRICE)4"].ToString() != "")
                        {
                            DbCommand dbcom7 = db.GetStoredProcCommand("sp_trn_ProdQtyPrice");
                            db.AddInParameter(dbcom7, "@ProdQtyPriceId", DbType.Int64, 0);
                            db.AddInParameter(dbcom7, "@ProductRefNo", DbType.String, mCurrentID);
                            db.AddInParameter(dbcom7, "@Year", DbType.Int64, 1);
                            db.AddInParameter(dbcom7, "@Type", DbType.String, "F");
                            db.AddInParameter(dbcom7, "@FYear", DbType.String, dtMaster.Rows[k]["IMPORTED YEAR 21-22"]);
                            db.AddInParameter(dbcom7, "@EstimatedQty", DbType.String, dtMaster.Rows[k]["IMPORTED QUANTITY4"]);
                            db.AddInParameter(dbcom7, "@Unit", DbType.String, dtMaster.Rows[k]["IMPORTED UNIT4"]);
                            db.AddInParameter(dbcom7, "@EstimatedPrice", DbType.String, dtMaster.Rows[k]["IMPORTED VALUE IN RS LAKH (QTY*PRICE)4"]);
                            db.ExecuteNonQuery(dbcom7, Transaction);
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
        public void CheckFYear(string value)
        {
            if (value.ToString() == "2000-01")
            { mFYearid = 1; }
            else if (value.ToString() == "2001-02")
            { mFYearid = 2; }
            else if (value.ToString() == "2002-03")
            { mFYearid = 3; }
            else if (value.ToString() == "2003-04")
            { mFYearid = 4; }
            else if (value.ToString() == "2004-05")
            { mFYearid = 5; }
            else if (value.ToString() == "2005-06")
            { mFYearid = 6; }
            else if (value.ToString() == "2006-07")
            { mFYearid = 7; }
            else if (value.ToString() == "2007-08")
            { mFYearid = 8; }
            else if (value.ToString() == "2008-09")
            { mFYearid = 9; }
            else if (value.ToString() == "2009-10")
            { mFYearid = 10; }
            else if (value.ToString() == "2010-11")
            { mFYearid = 11; }
            else if (value.ToString() == "2011-12")
            { mFYearid = 12; }
            else if (value.ToString() == "2012-13")
            { mFYearid = 13; }
            else if (value.ToString() == "2013-14")
            { mFYearid = 14; }
            else if (value.ToString() == "2014-15")
            { mFYearid = 15; }
            else if (value.ToString() == "2015-16")
            { mFYearid = 16; }
            else if (value.ToString() == "2016-17")
            { mFYearid = 17; }
            else if (value.ToString() == "2017-18")
            { mFYearid = 18; }
            else if (value.ToString() == "2018-19")
            { mFYearid = 19; }
            else if (value.ToString() == "2019-20")
            { mFYearid = 20; }
            else if (value.ToString() == "2020-21")
            { mFYearid = 21; }
            else if (value.ToString() == "2021-22")
            { mFYearid = 22; }
            else if (value.ToString() == "2022-23")
            { mFYearid = 23; }
            else if (value.ToString() == "2023-24")
            { mFYearid = 24; }
            else if (value.ToString() == "2024-25")
            { mFYearid = 25; }

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
                string msg = ex.Message;
                text = ex.Message;
                return ds.Tables[0];
            }
        }

        public DataTable CreateExcelConnection1(string FilePath, string SheetName, out string text)
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
        public DataTable RetriveSaveEstimateGrid(string Function, Int32 ProdInfoId, string ProdRefNo, Int32 Year, string FYear, string EstimateQuantity, string Unit, string Price, string type)
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
        public string SaveVendorRegis(HybridDictionary hysavecomp, out string sysMsg, out string msg)
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
                    db.AddInParameter(cmd, "@V_RegisterdDPSU", DbType.String, hysavecomp["DPSU"]);
                    db.AddInParameter(cmd, "@PanNo", DbType.String, hysavecomp["PanNo"]);
                    db.AddInParameter(cmd, "@GSTNo", DbType.String, hysavecomp["GSTNo"]);
                    db.AddInParameter(cmd, "@V_CompName", DbType.String, hysavecomp["V_CompName"]);
                    db.AddInParameter(cmd, "@AuthPath", DbType.String, hysavecomp["AuthPath"].ToString().Trim());
                    db.AddInParameter(cmd, "@NodalOfficerName", DbType.String, hysavecomp["NodalOfficerName"]);
                    db.AddInParameter(cmd, "@NodalOfficerEmail", DbType.String, hysavecomp["NodalOfficerEmail"].ToString().Trim());
                    db.AddInParameter(cmd, "@IsEsignVerify", DbType.String, hysavecomp["IsEsignVerify"].ToString().Trim());
                    db.AddOutParameter(cmd, "@ReturnID", DbType.String, 20);
                    db.ExecuteNonQuery(cmd, dbTran);
                    mCurrentID = db.GetParameterValue(cmd, "@ReturnID").ToString();
                    dbTran.Commit();
                    msg = "Save";
                    sysMsg = "Save";
                    return mCurrentID;
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    msg = ex.Message;
                    sysMsg = "";
                    return sysMsg;
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
                    db.AddInParameter(cmd, "@VendorRefNo", DbType.String, HySaveVendorRegisdetail["VendorRefNo"]);
                    db.AddInParameter(cmd, "@LogoPath", DbType.String, HySaveVendorRegisdetail["LogoPath"]);
                    db.AddInParameter(cmd, "@RegistrationCategory", DbType.String, HySaveVendorRegisdetail["RegistrationCategory"]);
                    db.AddInParameter(cmd, "@TypeOfOwnership", DbType.String, HySaveVendorRegisdetail["TypeOfOwnership"]);
                    db.AddInParameter(cmd, "@BuisnessSector", DbType.String, HySaveVendorRegisdetail["BuisnessSector"].ToString().Trim());
                    if (HySaveVendorRegisdetail["Date_Incorportaion_Company"] != null)
                    {
                        db.AddInParameter(cmd, "@Date_Incorportaion_Company", DbType.Date, HySaveVendorRegisdetail["Date_Incorportaion_Company"].ToString());
                    }
                    else
                    { db.AddInParameter(cmd, "@Date_Incorportaion_Company", DbType.Date, null); }
                    db.AddInParameter(cmd, "@CompanyURL", DbType.String, HySaveVendorRegisdetail["CompanyURL"]);
                    db.AddInParameter(cmd, "@Street_Address", DbType.String, HySaveVendorRegisdetail["Street_Address"]);
                    db.AddInParameter(cmd, "@Street_Address_Line_2", DbType.String, HySaveVendorRegisdetail["Street_Address_Line_2"]);
                    db.AddInParameter(cmd, "@MobileNo", DbType.String, HySaveVendorRegisdetail["MobileNo"]);
                    db.AddInParameter(cmd, "@ContactNo", DbType.String, HySaveVendorRegisdetail["ContactNo"]);
                    db.AddInParameter(cmd, "@FaxNo", DbType.String, HySaveVendorRegisdetail["FaxNo"]);
                    db.AddInParameter(cmd, "@State", DbType.String, HySaveVendorRegisdetail["State"]);
                    db.AddInParameter(cmd, "@City", DbType.String, HySaveVendorRegisdetail["City"]);
                    db.AddInParameter(cmd, "@PinCode", DbType.String, HySaveVendorRegisdetail["PinCode"]);
                    db.AddInParameter(cmd, "@TANNo", DbType.String, HySaveVendorRegisdetail["TANNo"]);
                    db.AddInParameter(cmd, "@UAMNo", DbType.String, HySaveVendorRegisdetail["UAMNo"]);
                    db.AddInParameter(cmd, "@CINNo", DbType.String, HySaveVendorRegisdetail["CINNo"]);
                    db.AddInParameter(cmd, "@Type", DbType.String, HySaveVendorRegisdetail["Type"]);
                    db.AddOutParameter(cmd, "@ReturnID", DbType.String, 20);
                    db.ExecuteNonQuery(cmd, dbTran);
                    mCurrentID = db.GetParameterValue(cmd, "@ReturnID").ToString();
                    if (dtEnterNameOf != null)
                    {
                        DataColumnCollection columns = dtEnterNameOf.Columns;
                        if (columns.Contains("mProcess"))
                        {
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
                        }
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
                        db.AddInParameter(dbcom9, "@ImageID", DbType.Int16, 0);
                        //db.AddInParameter(dbcom9, "@ImageID", DbType.Int16, dt9.Rows[i]["ImageID"]);
                        db.AddInParameter(dbcom9, "@Name", DbType.String, dt9.Rows[i]["CertificateName"]);
                        db.AddInParameter(dbcom9, "@Path", DbType.String, dt9.Rows[i]["CertificateImage"]);
                        db.ExecuteNonQuery(dbcom9, dbTran);
                    }
                    for (int i = 0; i < dt10.Rows.Count; i++)
                    {
                        DbCommand dbcom10 = db.GetStoredProcCommand("sp_VendorRegistrationMultiGridImage");
                        db.AddInParameter(dbcom10, "@Type", DbType.String, "QCertificate");
                        db.AddInParameter(dbcom10, "@VendorRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom10, "@ImageID", DbType.Int16, 0);
                        //db.AddInParameter(dbcom10, "@ImageID", DbType.Int16, dt10.Rows[i]["ImageID"]);
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
                        db.AddInParameter(dbcom2, "@ProductNomenClature", DbType.String, dt2.Rows[i]["TechNomenclature"]);
                        db.AddInParameter(dbcom2, "@TechnologyLevel1", DbType.Int64, dt2.Rows[i]["Technology1"]);
                        if (dt2.Rows[i]["Technology2"].ToString() == "NA")
                        {
                            db.AddInParameter(dbcom2, "@Technology2", DbType.Int64, 1);
                        }
                        else
                        {
                            db.AddInParameter(dbcom2, "@TechnologyLevel2", DbType.Int64, dt2.Rows[i]["Technology2"]);
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
                        db.AddInParameter(dbcom4, "@Type", DbType.String, "SupplyYes");
                        db.ExecuteNonQuery(dbcom4, dbTran);
                    }
                    for (int i = 0; i < dt5.Rows.Count; i++)
                    {
                        DbCommand dbcom5 = db.GetStoredProcCommand("sp_VendorRegistrationMultiGridIteProducedSupplied");
                        db.AddInParameter(dbcom5, "@VendorRefNo", DbType.String, mCurrentID);
                        db.AddInParameter(dbcom5, "@Reputed_Customer", DbType.String, dt5.Rows[i]["NameCust1"]);
                        db.AddInParameter(dbcom5, "@Description", DbType.String, dt5.Rows[i]["DesStoreSupp1"]);
                        db.AddInParameter(dbcom5, "@SupplyNoDate", DbType.String, dt5.Rows[i]["OderNoorDate1"]);
                        db.AddInParameter(dbcom5, "@OrderQuantity", DbType.String, dt5.Rows[i]["OrderQty1"]);
                        db.AddInParameter(dbcom5, "@SuppliedQuantity", DbType.String, dt5.Rows[i]["ValueQtySupp1"]);
                        db.AddInParameter(dbcom5, "@Date2", DbType.Date, dt5.Rows[i]["DateofLastSupp1"]);
                        db.AddInParameter(dbcom5, "@Type", DbType.String, "SupplyNo");
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
        #region RequsetInfo Cart
        public string SaveRequestInfo(HybridDictionary hySaveProdInfo, DataTable dtreqprod, out string _sysMsg, out string _msg)
        {
            using (DbConnection dbcon = db.CreateConnection())
            {
                Int64 mCurrentID;
                dbcon.Open();
                DbTransaction dbTran = dbcon.BeginTransaction();
                try
                {
                    DbCommand dbcom = db.GetStoredProcCommand("sp_RequestInfo");
                    db.AddInParameter(dbcom, "@RequestID", DbType.Int64, hySaveProdInfo["RequestID"]);
                    db.AddInParameter(dbcom, "@RequestBy", DbType.String, hySaveProdInfo["RequestBy"]);
                    db.AddInParameter(dbcom, "@RequestProduct", DbType.String, hySaveProdInfo["RequestProduct"]);
                    db.AddInParameter(dbcom, "@RequestMCompName", DbType.String, hySaveProdInfo["RequestMCompName"]);
                    db.AddInParameter(dbcom, "@RequestMobileNo", DbType.Int64, hySaveProdInfo["RequestMobileNo"]);
                    db.AddInParameter(dbcom, "@RequestAddress", DbType.String, hySaveProdInfo["RequestAddress"]);
                    db.AddInParameter(dbcom, "@RequestEmail", DbType.String, hySaveProdInfo["RequestEmail"]);
                    db.AddInParameter(dbcom, "@RequestCompName", DbType.String, hySaveProdInfo["RequestCompName"]);
                    db.AddInParameter(dbcom, "@IsMailSend", DbType.String, hySaveProdInfo["IsMailSend"]);
                    db.AddInParameter(dbcom, "@RequestDate", DbType.Date, hySaveProdInfo["RequestDate"]);
                    db.AddInParameter(dbcom, "@Usedfor", DbType.String, hySaveProdInfo["Usedfor"]);
                    db.AddOutParameter(dbcom, "@ReturnID", DbType.Int64, 50);
                    db.ExecuteNonQuery(dbcom, dbTran);
                    mCurrentID = Convert.ToInt64(db.GetParameterValue(dbcom, "@ReturnID").ToString());
                    for (int i = 0; i < dtreqprod.Rows.Count; i++)
                    {
                        DbCommand dbcom1 = db.GetStoredProcCommand("sp_trn_RequestInfo");
                        db.AddInParameter(dbcom1, "@RequestID", DbType.Int64, mCurrentID);
                        db.AddInParameter(dbcom1, "@ProductRefNo", DbType.String, dtreqprod.Rows[i]["ProductRefNo"]);
                        db.AddInParameter(dbcom1, "@CompanyName", DbType.String, dtreqprod.Rows[i]["CompanyName"]);
                        db.AddInParameter(dbcom1, "@Remark", DbType.String, dtreqprod.Rows[i]["Remark"]);
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
        #region ShaliniUpdateCode
        public string updateEOI(string productrefno, string eoistatus, string startdate, string enddate, string eoiurl)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();

                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_updateEOI");
                    db.AddInParameter(cmd, "@productrefno", DbType.String, productrefno);
                    db.AddInParameter(cmd, "@eoistatus", DbType.String, eoistatus);
                    db.AddInParameter(cmd, "@startdate", DbType.String, startdate);
                    db.AddInParameter(cmd, "@enddate", DbType.String, enddate);
                    db.AddInParameter(cmd, "@eoiurl", DbType.String, eoiurl);
                    cmd.CommandTimeout = 0;
                    db.ExecuteNonQuery(cmd, dbTran);
                    dbTran.Commit();
                    return "true";
                }
                catch (Exception e)
                {
                    dbTran.Rollback();
                    return "-1";
                }
            }
        }
        public string updateSupplyOrder(string productrefno, string soplaced, decimal supplyordervalue, string supplydeliverydate, string supplyorderdate, string supplymanufacturename, string supplyaddress)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();

                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_updateSupplyOrder");
                    db.AddInParameter(cmd, "@productrefno", DbType.String, productrefno);
                    db.AddInParameter(cmd, "@supplyorderstatus", DbType.String, soplaced);
                    db.AddInParameter(cmd, "@supplyordervalue", DbType.Decimal, supplyordervalue);
                    db.AddInParameter(cmd, "@supplydeliverydate", DbType.String, supplydeliverydate);
                    db.AddInParameter(cmd, "@supplyorderdate", DbType.String, supplyorderdate);
                    db.AddInParameter(cmd, "@SupplyManfutureName", DbType.String, supplymanufacturename);
                    db.AddInParameter(cmd, "@Supplyaddress", DbType.String, supplyaddress);
                    cmd.CommandTimeout = 0;
                    db.ExecuteNonQuery(cmd, dbTran);
                    dbTran.Commit();
                    return "true";
                }
                catch (Exception e)
                {
                    dbTran.Rollback();
                    return "-1";
                }
            }
        }




        public string updateSuccessStory(string productrefno, string indegprocess, int indigyear, string isindegnized, string targetyear,
            string indiacategory, string manufname, string manufaddress, decimal value, DateTime dat)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();

                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_UpdateSuccesstory");
                    db.AddInParameter(cmd, "@prorefno", DbType.String, productrefno);
                    db.AddInParameter(cmd, "@indprocess", DbType.String, indegprocess);
                    db.AddInParameter(cmd, "@yearifindignization", DbType.String, indigyear);
                    db.AddInParameter(cmd, "@isindignized", DbType.String, isindegnized);
                    db.AddInParameter(cmd, "@indigtargtyr", DbType.String, targetyear);
                    db.AddInParameter(cmd, "@purposeofprocuremnt", DbType.String, indiacategory);
                    db.AddInParameter(cmd, "@manufacturename", DbType.String, manufname);
                    db.AddInParameter(cmd, "@manufactureaddress", DbType.String, manufaddress);
                    db.AddInParameter(cmd, "@MaxValue", DbType.Decimal, value);
                    db.AddInParameter(cmd, "@IndDate", DbType.DateTime, dat);
                    cmd.CommandTimeout = 0;
                    db.ExecuteNonQuery(cmd, dbTran);
                    dbTran.Commit();
                    return "true";
                }
                catch (Exception e)
                {
                    dbTran.Rollback();
                    return "-1";
                }
            }
        }
        #endregion
        #region ShaliniSaveCode
        public string SaveCompRemarkReply(string remarkrefno, string replyusername, string replyemail, string remark, string remarkreply)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();

                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_CompRemark_reply");
                    db.AddInParameter(cmd, "@RemarkRefNo", DbType.String, remarkrefno);
                    db.AddInParameter(cmd, "@Reply_UserName", DbType.String, replyusername);
                    db.AddInParameter(cmd, "@Reply_Email", DbType.String, replyemail);
                    db.AddInParameter(cmd, "@Remark", DbType.String, remark);
                    db.AddInParameter(cmd, "@Reply", DbType.String, remarkreply);
                    cmd.CommandTimeout = 0;
                    db.ExecuteNonQuery(cmd, dbTran);
                    dbTran.Commit();
                    return "true";
                }
                catch (Exception e)
                {
                    dbTran.Rollback();
                    return "-1";
                }
            }
        }
        public string SaveSuccessStoryLog(string productrefno, string companyrefno, string updateDate, string updatetime)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();

                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_SuccessStoryLog");
                    db.AddInParameter(cmd, "@productrefno", DbType.String, productrefno);
                    db.AddInParameter(cmd, "@companyrefno", DbType.String, companyrefno);
                    db.AddInParameter(cmd, "@date", DbType.String, updateDate);
                    db.AddInParameter(cmd, "@time", DbType.String, updatetime);
                    cmd.CommandTimeout = 0;
                    db.ExecuteNonQuery(cmd, dbTran);
                    dbTran.Commit();
                    return "true";
                }
                catch (Exception e)
                {
                    dbTran.Rollback();
                    return "-1";
                }
            }
        }
        #endregion
        #region minakkshi code for update shown status in progess report 2.0
        //public string updateIntShownStatus(string productrefno, string requestid, string intshownreason)
        //{
        //    using (DbConnection dbCon = db.CreateConnection())
        //    {
        //        dbCon.Open();
        //        DbTransaction dbTran = dbCon.BeginTransaction();
        //        try
        //        {
        //            DbCommand cmd = db.GetStoredProcCommand("sp_updateIntStatus");
        //            db.AddInParameter(cmd, "@productrefno", DbType.String, productrefno);
        //            db.AddInParameter(cmd, "@ReqId", DbType.String, requestid);
        //            db.AddInParameter(cmd, "@intreason", DbType.String, intshownreason);
        //            cmd.CommandTimeout = 0;
        //            db.ExecuteNonQuery(cmd, dbTran);
        //            dbTran.Commit();
        //            return "true";
        //        }
        //        catch (Exception e)
        //        {
        //            dbTran.Rollback();
        //            return "-1";
        //        }
        //    }
        //}


        public string updateIntShownStatus(string productrefno, string requestid, string intshownreason, string struserreason, string mrreasone)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_updateIntStatus");
                    db.AddInParameter(cmd, "@productrefno", DbType.String, productrefno);
                    db.AddInParameter(cmd, "@ReqId", DbType.String, requestid);
                    db.AddInParameter(cmd, "@intreason", DbType.String, intshownreason);
                    db.AddInParameter(cmd, "@userreason", DbType.String, struserreason);
                    db.AddInParameter(cmd, "@mrreasone", DbType.String, mrreasone);
                    cmd.CommandTimeout = 0;
                    db.ExecuteNonQuery(cmd, dbTran);
                    dbTran.Commit();
                    return "true";
                }
                catch (Exception e)
                {
                    dbTran.Rollback();
                    return "-1";
                }
            }
        }
        #endregion
        #region Shalini Product Updation Work
        public DataTable RetriveProductUpdation(string RefNo, string Criteria, string searchvalue)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_ProductUpdation");
                    db.AddInParameter(cmd, "@RefNo", DbType.String, RefNo);
                    db.AddInParameter(cmd, "@searchvalue", DbType.String, searchvalue);
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
        public string UpdateProduct(HybridDictionary hyProduct, DataTable dt)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                string mCurrentID = "";
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_UpdateProduct");
                    db.AddInParameter(cmd, "@ProductRefNo", DbType.String, hyProduct["ProductRefNo"]);
                    db.AddInParameter(cmd, "@FeatursandDetail", DbType.String, hyProduct["FeatursandDetail"]);
                    db.AddInParameter(cmd, "@qaAgency", DbType.String, hyProduct["QAAgency"].ToString().Trim());
                    db.AddOutParameter(cmd, "@ReturnId", DbType.String, 70);
                    db.ExecuteNonQuery(cmd, dbTran);
                    mCurrentID = db.GetParameterValue(cmd, "@ReturnId").ToString();
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
                        db.AddInParameter(dbcom1, "@FType", DbType.String, "Image");
                        db.ExecuteNonQuery(dbcom1, dbTran);
                    }
                    dbTran.Commit();
                    return mCurrentID;
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    return "-1";
                }
                finally
                {
                    dbCon.Close();
                }
            }
        }
        public string updateNodalOfficer(string comprefno, string type, string nodalofficername, string nodaloffcermobile, string nodalofficeremail, string nodalofficerdesignation)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_UpdateNodalOfficer");
                    db.AddInParameter(cmd, "@comprefno", DbType.String, comprefno);
                    db.AddInParameter(cmd, "@type", DbType.String, type);
                    db.AddInParameter(cmd, "@nodalofficername", DbType.String, nodalofficername);
                    db.AddInParameter(cmd, "@nodalofficermobile", DbType.String, nodaloffcermobile);
                    db.AddInParameter(cmd, "@nodalofficeremail", DbType.String, nodalofficeremail);
                    db.AddInParameter(cmd, "@nodalofficerdesignation", DbType.String, nodalofficerdesignation);
                    cmd.CommandTimeout = 0;
                    db.ExecuteNonQuery(cmd, dbTran);
                    dbTran.Commit();
                    return "true";
                }
                catch (Exception e)
                {
                    dbTran.Rollback();
                    return "-1";
                }
            }
        }
        #endregion
        #region SucceessStory 2.0 work
        public DataTable newsuccessstory2(string Criteria, string search, string value, string purpose, string role, int reqid, int refno, int id, string eoi, string supplyorder, string indiginized)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("new_successstory2");
                    db.AddInParameter(cmd, "@criteria", DbType.String, Criteria);
                    db.AddInParameter(cmd, "@Search", DbType.String, search);
                    db.AddInParameter(cmd, "@Value", DbType.String, value);
                    db.AddInParameter(cmd, "@purpose", DbType.String, purpose);
                    db.AddInParameter(cmd, "@Role", DbType.String, role);
                    db.AddInParameter(cmd, "@reqid", DbType.String, reqid);
                    db.AddInParameter(cmd, "@refno", DbType.String, refno);
                    db.AddInParameter(cmd, "@Id", DbType.String, id);
                    db.AddInParameter(cmd, "@EOIStatus", DbType.String, eoi);
                    db.AddInParameter(cmd, "@SupplyOrderStatus", DbType.String, supplyorder);
                    db.AddInParameter(cmd, "@indiginized", DbType.String, indiginized);
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
        #region Test Facility
        public string InsertTestDetails(string test_name, string organisation_id, string Division_id, string Unit_id, string discipline_id, string calibrationfacility,
            string manufacturer, string manufactureyear, string chambersize, string equimntrange, string productmaterial, string specifications,
            string maxdimension, string maxweight, string duration, string advancenotice, string constraints, string remarks, out string _sysMsg, out string _msg)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                string mCurrentID = "";
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    //string mid = "";
                    DbCommand cmd = db.GetStoredProcCommand("sp_Insert_Test_details");
                    // db.AddInParameter(cmd, "@test_id", DbType.String, testid);
                    db.AddInParameter(cmd, "@test_name", DbType.String, test_name);
                    db.AddInParameter(cmd, "@organisation_id", DbType.String, organisation_id);
                    db.AddInParameter(cmd, "@Division_id", DbType.String, Division_id);
                    db.AddInParameter(cmd, "@Unit_id", DbType.String, Unit_id);
                    db.AddInParameter(cmd, "@discipline_id", DbType.String, discipline_id);
                    db.AddInParameter(cmd, "@calibration_facility", DbType.String, calibrationfacility);
                    db.AddInParameter(cmd, "@manufacturer", DbType.String, manufacturer);
                    db.AddInParameter(cmd, "@manufactureryear", DbType.String, manufactureyear);
                    db.AddInParameter(cmd, "@chamber_size", DbType.String, chambersize);
                    db.AddInParameter(cmd, "@equipmnt_range", DbType.String, equimntrange);
                    db.AddInParameter(cmd, "@product_material", DbType.String, productmaterial);
                    db.AddInParameter(cmd, "@specifications", DbType.String, specifications);
                    db.AddInParameter(cmd, "@maxdimension", DbType.String, maxdimension);
                    db.AddInParameter(cmd, "@maxweight", DbType.String, maxweight);
                    db.AddInParameter(cmd, "@duration", DbType.String, duration);
                    db.AddInParameter(cmd, "@advance_notice", DbType.String, advancenotice);
                    db.AddInParameter(cmd, "@constraints", DbType.String, constraints);
                    db.AddInParameter(cmd, "@remarks", DbType.String, remarks);
                    db.AddOutParameter(cmd, "@ReturnId", DbType.String, 70);
                    db.ExecuteNonQuery(cmd, dbTran);
                    mCurrentID = db.GetParameterValue(cmd, "@ReturnId").ToString();
                    dbTran.Commit();
                    _msg = "Save";
                    _sysMsg = "Save";
                    return mCurrentID;
                }
                catch (DbException ex)
                {
                    dbTran.Rollback();
                    _msg = ex.Message;
                    _sysMsg = ex.Message;
                    return "-1";
                }
            }
        }
        public DataTable RetriveTestDetails()
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_BindTestDetails");
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

        public DataTable RetrieveTestdetailsbyId(int id)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_GetTestdetailsbyId");
                    db.AddInParameter(cmd, "@id", DbType.Int64, id);
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

        public string SaveBookOrders(int userid, string testname, string organisationid, string discipline, string lab, int noofsample, string dimension, string weighteqpmnt, string startdate, string enddate, string attachment, string description)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                string mCurrentID = "";
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_BookOrders");
                    db.AddInParameter(cmd, "@userid", DbType.Int32, userid);
                    db.AddInParameter(cmd, "@testid", DbType.String, testname);
                    db.AddInParameter(cmd, "@organisationid", DbType.String, organisationid);
                    db.AddInParameter(cmd, "@discipline", DbType.String, discipline);
                    db.AddInParameter(cmd, "@lab", DbType.String, lab);
                    db.AddInParameter(cmd, "@noofsample", DbType.Int32, noofsample);
                    db.AddInParameter(cmd, "@dimension_eqpmt", DbType.String, dimension);
                    db.AddInParameter(cmd, "@weight_eqmt", DbType.String, weighteqpmnt);
                    db.AddInParameter(cmd, "@start_date", DbType.String, startdate);
                    db.AddInParameter(cmd, "@end_date", DbType.String, enddate);
                    db.AddInParameter(cmd, "@attachment", DbType.String, attachment);
                    db.AddInParameter(cmd, "@description", DbType.String, description);
                    db.AddOutParameter(cmd, "@RETURNID", DbType.String, 20);
                    db.ExecuteNonQuery(cmd, dbTran);
                    mCurrentID = db.GetParameterValue(cmd, "@RETURNID").ToString();
                    dbTran.Commit();
                    return mCurrentID;
                }
                catch (DbException ex)
                {
                    dbTran.Rollback();
                    return "-1";
                }
            }
        }
        public DataTable RetriveBookedOrders(string compname)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_BindBookedOrder");
                    db.AddInParameter(cmd, "@orgname", DbType.String, compname);
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

        public string ApproveBookedOrder(int id)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_ApproveBookedOrder");
                    db.AddInParameter(cmd, "@id", DbType.Int32, id);
                    cmd.CommandTimeout = 0;
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

        public string RejectOrderwithRemark(int id, string remark)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_RejectBookedOrder");
                    db.AddInParameter(cmd, "@id", DbType.Int32, id);
                    db.AddInParameter(cmd, "@Rejectremarks", DbType.String, remark);
                    cmd.CommandTimeout = 0;
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
        public DataTable GetAllOrganisations()
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_GetAllOrganisations");
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

        public DataTable GetAllLAbs()
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_GetAllLab");
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

        public DataTable GetAllDiscipline()
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_GetAllDiscipline");
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
        public DataTable GetUserdatabycompany(string compname)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_GetAllDiscipline");
                    db.AddInParameter(cmd, "@compname", DbType.String, compname);
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

        public DataTable SearchTestDataonHomepage(string compname)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_SearchtestdataonHomepage");
                    db.AddInParameter(cmd, "@compname", DbType.String, compname);
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

        public DataTable GetTestdetailsbycompany(string compname)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_GetTestdetailsbyCompany");
                    db.AddInParameter(cmd, "@compname", DbType.String, compname);
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

        public DataTable GetAllBookedOrders()
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("BookedOrders");
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
        public DataTable GetPendingBookedOrders()
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("pendingBookedOrders");
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

        public DataTable GetApprovendRejectedOrders()
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_ApprovendRejectedorders");
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
        public DataTable GetDashboardDatabyCompanywise(string criteria, string Search, int id, string compname)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_TestLab");
                    db.AddInParameter(cmd, "@criteria", DbType.String, criteria);
                    db.AddInParameter(cmd, "@Search", DbType.String, Search);
                    db.AddInParameter(cmd, "@Id", DbType.Int32, id);
                    db.AddInParameter(cmd, "@compname", DbType.String, compname);
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
        #region other  
        public DataTable GetProductData(string ddlval)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("newproductfilter");
                    //db.AddInParameter(cmd, "@CompRefNo", DbType.String, CompanyRefNo);
                    //db.AddInParameter(cmd, "@SearchValue", DbType.String, search);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, ddlval);
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
        public string SaveExcelProduct2(DataTable dtMaster)
        {
            using (DbConnection Connection = db.CreateConnection())
            {
                Connection.Open();
                DbTransaction Transaction = Connection.BeginTransaction();
                Int32 errorRow = -1;
                try
                {
                    for (int k = 0; k < dtMaster.Rows.Count; k++)
                    {
                        #region code for MasterTable Update in loop
                        errorRow = k;
                        DbCommand cmd = db.GetStoredProcCommand("sp_UpdateProductFromExcel1");
                        db.AddInParameter(cmd, "@ProductRefNo", DbType.String, dtMaster.Rows[k]["ProductRefNo"].ToString());
                        db.AddInParameter(cmd, "@ProductLevel1", DbType.String, dtMaster.Rows[k]["NSNGroup"].ToString());
                        db.AddInParameter(cmd, "@ProductLevel2", DbType.String, dtMaster.Rows[k]["NSNGroupClass"]);
                        db.AddInParameter(cmd, "@ProductLevel3", DbType.String, dtMaster.Rows[k]["ItemCode"]);
                        db.AddInParameter(cmd, "@ProductDescription", DbType.String, dtMaster.Rows[k]["ProductDescription"]);
                        db.AddInParameter(cmd, "@NIINCode", DbType.String, dtMaster.Rows[k]["NIINCode"].ToString().Trim());
                        db.AddInParameter(cmd, "@FeatursandDetail", DbType.String, dtMaster.Rows[k]["FeatursandDetail"]);
                        db.AddInParameter(cmd, "@OEMPartNumber", DbType.String, dtMaster.Rows[k]["OEMPartNumber"]);
                        db.AddInParameter(cmd, "@OEMName", DbType.String, dtMaster.Rows[k]["OEMName"].ToString().Trim());
                        db.AddInParameter(cmd, "@OEMCountry", DbType.String, dtMaster.Rows[k]["OEMCountry"].ToString().Trim());
                        db.AddInParameter(cmd, "@OEMAddress", DbType.String, dtMaster.Rows[k]["OEMAddress"].ToString().Trim());
                        db.AddInParameter(cmd, "@DPSUPartNumber", DbType.String, dtMaster.Rows[k]["DPSUPartNumber"].ToString().Trim());
                        db.AddInParameter(cmd, "@HsnCode8digit", DbType.String, dtMaster.Rows[k]["HsnCode8digit"].ToString().Trim());
                        //db.AddInParameter(cmd, "@EndUser", DbType.String, dtMaster.Rows[k]["EndUser"]);
                        db.AddInParameter(cmd, "@Platform", DbType.String, dtMaster.Rows[k]["DefencePlatform"]);
                        db.AddInParameter(cmd, "@NomenclatureOfMainSystem", DbType.String, dtMaster.Rows[k]["NomenclatureOfMainSystem"]);
                        db.AddInParameter(cmd, "@TechnologyLevel1", DbType.String, dtMaster.Rows[k]["ProdIndustryDoamin"]);
                        db.AddInParameter(cmd, "@TechnologyLevel2", DbType.String, dtMaster.Rows[k]["ProdIndustrySubDomain"]);
                        db.AddInParameter(cmd, "@IsIndeginized", DbType.String, dtMaster.Rows[k]["IsIndeginized"].ToString().Trim());
                        db.AddInParameter(cmd, "@ManufactureName", DbType.String, dtMaster.Rows[k]["ManufactureName"]);
                        db.AddInParameter(cmd, "@ManufactureAddress", DbType.String, dtMaster.Rows[k]["ManufactureAddress"]);
                        db.AddInParameter(cmd, "@YearofIndiginization", DbType.String, dtMaster.Rows[k]["YearofIndiginization"]);
                        db.AddInParameter(cmd, "@PurposeofProcurement", DbType.String, dtMaster.Rows[k]["MakeInIndiaCategory"]);
                        //  db.AddInParameter(cmd, "@QAAgency", DbType.String, dtMaster.Rows[k]["QAAgency"].ToString().Trim());
                        db.AddInParameter(cmd, "@EOIStatus", DbType.String, dtMaster.Rows[k]["EOIStatus"].ToString().Trim());
                        db.AddInParameter(cmd, "@EOIURL", DbType.String, dtMaster.Rows[k]["EOIURL"]);
                        db.AddInParameter(cmd, "@EOIStartDate", DbType.String, dtMaster.Rows[k]["EOIStartDate"]);
                        db.AddInParameter(cmd, "@EOIEndDate", DbType.String, dtMaster.Rows[k]["EOIEndDate"]);
                        db.AddInParameter(cmd, "@SupplyOrderStatus", DbType.String, dtMaster.Rows[k]["SupplyOrderStatus"].ToString().Trim());
                        db.AddInParameter(cmd, "@SupplyManfutureName", DbType.String, dtMaster.Rows[k]["SupplyManfutureName"].ToString().Trim());
                        db.AddInParameter(cmd, "@SupplyManfutureAddress", DbType.String, dtMaster.Rows[k]["SupplyManfutureAddress"].ToString().Trim());
                        db.AddInParameter(cmd, "@SupplyOrderValue", DbType.String, dtMaster.Rows[k]["SupplyOrderValue"].ToString().Trim());
                        db.AddInParameter(cmd, "@SupplyDeliveryDate", DbType.String, dtMaster.Rows[k]["SupplyDeliveryDate"].ToString().Trim());
                        db.AddInParameter(cmd, "@SupplyOrderDate", DbType.String, dtMaster.Rows[k]["SupplyOrderDate"].ToString().Trim());
                        db.AddInParameter(cmd, "@IndTargetYear", DbType.String, dtMaster.Rows[k]["IndTargetYear"]);
                        db.AddInParameter(cmd, "@IndProcess", DbType.String, dtMaster.Rows[k]["IndProcess"]);
                        db.AddInParameter(cmd, "@NodelDetail", DbType.String, dtMaster.Rows[k]["NodalOfficerEmail"]);
                        db.AddInParameter(cmd, "@IsShowGeneral", DbType.String, dtMaster.Rows[k]["Display_On_Public_Portal"]);
                        #endregion
                        #region Code For Multiple row in trn table year wise
                        db.AddInParameter(cmd, "@FYear18", DbType.String, dtMaster.Rows[k]["FYear18"].ToString().Trim());
                        db.AddInParameter(cmd, "@EstimatedQty18", DbType.String, dtMaster.Rows[k]["EstimatedQty18"]);
                        db.AddInParameter(cmd, "@Unit18", DbType.String, dtMaster.Rows[k]["Unit18"]);
                        db.AddInParameter(cmd, "@EstimatedPrice18", DbType.String, dtMaster.Rows[k]["EstimatedPrice18"]);
                        db.AddInParameter(cmd, "@FYear19", DbType.String, dtMaster.Rows[k]["FYear19"].ToString().Trim());
                        db.AddInParameter(cmd, "@EstimatedQty19", DbType.String, dtMaster.Rows[k]["EstimatedQty19"]);
                        db.AddInParameter(cmd, "@Unit19", DbType.String, dtMaster.Rows[k]["Unit19"]);
                        db.AddInParameter(cmd, "@EstimatedPrice19", DbType.String, dtMaster.Rows[k]["EstimatedPrice19"]);
                        db.AddInParameter(cmd, "@FYear20", DbType.String, dtMaster.Rows[k]["FYear20"].ToString().Trim());
                        db.AddInParameter(cmd, "@EstimatedQty20", DbType.String, dtMaster.Rows[k]["EstimatedQty20"]);
                        db.AddInParameter(cmd, "@Unit20", DbType.String, dtMaster.Rows[k]["Unit20"]);
                        db.AddInParameter(cmd, "@EstimatedPrice20", DbType.String, dtMaster.Rows[k]["EstimatedPrice20"]);
                        db.AddInParameter(cmd, "@FYear21", DbType.String, dtMaster.Rows[k]["FYear21"].ToString().Trim());
                        db.AddInParameter(cmd, "@EstimatedQty21", DbType.String, dtMaster.Rows[k]["EstimatedQty21"]);
                        db.AddInParameter(cmd, "@Unit21", DbType.String, dtMaster.Rows[k]["Unit21"]);
                        db.AddInParameter(cmd, "@EstimatedPrice21", DbType.String, dtMaster.Rows[k]["EstimatedPrice21"]);
                        #endregion
                        db.ExecuteNonQuery(cmd, Transaction);
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
        public string updateprogSuccessStory(string productrefno, string indigyear, string manufname, string manufaddress)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();

                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_UpdateProgressSuccesstory");
                    db.AddInParameter(cmd, "@prorefno", DbType.String, productrefno);
                    db.AddInParameter(cmd, "@yearifindignization", DbType.String, indigyear);
                    db.AddInParameter(cmd, "@manufacturename", DbType.String, manufname);
                    db.AddInParameter(cmd, "@manufactureaddress", DbType.String, manufaddress);
                    //db.AddInParameter(cmd, "@isindignized", DbType.String, indig);
                    cmd.CommandTimeout = 0;
                    db.ExecuteNonQuery(cmd, dbTran);
                    dbTran.Commit();
                    return "true";
                }
                catch (Exception e)
                {
                    dbTran.Rollback();
                    return "-1";
                }
            }
        }
        public string updateprogSuccessStory2(string productrefno, string indigyear, string manufname, string manufaddress)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();

                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_UpdateProgressSuccesstory2");
                    db.AddInParameter(cmd, "@prorefno", DbType.String, productrefno);
                    db.AddInParameter(cmd, "@yearifindignization", DbType.String, indigyear);
                    db.AddInParameter(cmd, "@manufacturename", DbType.String, manufname);
                    db.AddInParameter(cmd, "@manufactureaddress", DbType.String, manufaddress);

                    cmd.CommandTimeout = 0;
                    db.ExecuteNonQuery(cmd, dbTran);
                    dbTran.Commit();
                    return "true";
                }
                catch (Exception e)
                {
                    dbTran.Rollback();
                    return "-1";
                }
            }
        }
        public string SaveForgetExp(string email)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();

                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_forgotpasswordexp");
                    db.AddInParameter(cmd, "@email", DbType.String, email);
                    cmd.CommandTimeout = 0;
                    db.ExecuteNonQuery(cmd, dbTran);
                    dbTran.Commit();
                    return "true";
                }
                catch (Exception e)
                {
                    dbTran.Rollback();
                    return "-1";
                }
            }
        }
        public string SaveExcelProductforSHQ(DataTable dtMaster)
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
                        DbCommand ids = db.GetSqlStringCommand("select * from fn_GetIdsForProductTable('" + dtMaster.Rows[k]["COMPANY"].ToString() + "','" + dtMaster.Rows[k]["ITEM CODE"].ToString() + "','" + dtMaster.Rows[k]["DEFENCE PLATFORM"].ToString() + "','" + dtMaster.Rows[k]["NAME OF DEFENCE PLATFORM"].ToString() + "'," + "'" + dtMaster.Rows[k]["YEAR OF INDIGINISATION"].ToString() + "')");
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
        public string SaveExcel3510forSHQ(DataTable dtMaster, string l1, string l2, string pid)
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
        public string SaveExcelProduct1FORSHQ(DataTable dtMaster, HybridDictionary hyadvertice)
        {
            using (DbConnection Connection = db.CreateConnection())
            {
                Connection.Open();
                DbTransaction Transaction = Connection.BeginTransaction();
                Int32 errorRow = -1;
                string mCurrentID = "";
                try
                {
                    for (int k = 0; k < dtMaster.Rows.Count; k++)
                    {
                        errorRow = k;
                        DbCommand cmd = db.GetStoredProcCommand("sp_InsertProductFromExcel1FORSHQ");
                        db.AddInParameter(cmd, "@ProductID", DbType.Int64, hyadvertice["ProductID"]);
                        db.AddInParameter(cmd, "@CompanyRefNo", DbType.String, hyadvertice["CompanyRefNo"]);
                        db.AddInParameter(cmd, "@IsProductImported", DbType.String, hyadvertice["IsProductImported"]);
                        db.AddInParameter(cmd, "@IndProcess", DbType.String, hyadvertice["IndProcess"]);
                        db.AddInParameter(cmd, "@IsShowGeneral", DbType.String, hyadvertice["ShowGeneralDec"]);
                        db.AddInParameter(cmd, "@Role", DbType.String, hyadvertice["Role"]);
                        db.AddInParameter(cmd, "@NodelDetail", DbType.Int64, hyadvertice["NodelDetail"]);
                        db.AddInParameter(cmd, "@CreatedBy", DbType.String, hyadvertice["CreatedBy"]);

                        //Code from data pick excel
                        db.AddInParameter(cmd, "@IndTargetYear", DbType.String, dtMaster.Rows[k]["STARTING INDIGENIZATION TARGET YEAR"]);
                        CheckFYear(dtMaster.Rows[k]["YEAR OF INDIGINIZATION"].ToString());
                        db.AddInParameter(cmd, "@YearofIndiginization", DbType.String, mFYearid.ToString());
                        db.AddOutParameter(cmd, "@ReturnID", DbType.String, 70);
                        db.ExecuteNonQuery(cmd, Transaction);
                        mCurrentID = db.GetParameterValue(cmd, "@ReturnID").ToString();
                        DbCommand dbcom3 = db.GetStoredProcCommand("sp_trn_ProdQtyPrice");
                        db.AddInParameter(dbcom3, "@ProdQtyPriceId", DbType.Int64, 0);
                        db.AddInParameter(dbcom3, "@ProductRefNo", DbType.String, mCurrentID);
                        if (dtMaster.Rows[k]["IMPORTED YEAR"].ToString() == "2017-18")
                        {
                            db.AddInParameter(dbcom3, "@Year", DbType.Int64, 3);
                            db.AddInParameter(dbcom3, "@Type", DbType.String, "O");
                        }
                        else if (dtMaster.Rows[k]["IMPORTED YEAR"].ToString() == "2018-19")
                        {
                            db.AddInParameter(dbcom3, "@Year", DbType.Int64, 2);
                            db.AddInParameter(dbcom3, "@Type", DbType.String, "O");
                        }
                        else if (dtMaster.Rows[k]["IMPORTED YEAR"].ToString() == "2019-20")
                        {
                            db.AddInParameter(dbcom3, "@Year", DbType.Int64, 1);
                            db.AddInParameter(dbcom3, "@Type", DbType.String, "O");
                        }
                        else if (dtMaster.Rows[k]["IMPORTED YEAR"].ToString() == "2020-21")
                        {
                            db.AddInParameter(dbcom3, "@Year", DbType.Int64, 1);
                            db.AddInParameter(dbcom3, "@Type", DbType.String, "F");
                        }
                        else if (dtMaster.Rows[k]["IMPORTED YEAR"].ToString() == "2021-22")
                        {
                            db.AddInParameter(dbcom3, "@Year", DbType.Int64, 2);
                            db.AddInParameter(dbcom3, "@Type", DbType.String, "F");
                        }
                        else if (dtMaster.Rows[k]["IMPORTED YEAR"].ToString() == "2022-23")
                        {
                            db.AddInParameter(dbcom3, "@Year", DbType.Int64, 3);
                            db.AddInParameter(dbcom3, "@Type", DbType.String, "F");
                        }
                        else if (dtMaster.Rows[k]["IMPORTED YEAR"].ToString() == "2023-24")
                        {
                            db.AddInParameter(dbcom3, "@Year", DbType.Int64, 4);
                            db.AddInParameter(dbcom3, "@Type", DbType.String, "F");
                        }
                        else if (dtMaster.Rows[k]["IMPORTED YEAR"].ToString() == "2024-25")
                        {
                            db.AddInParameter(dbcom3, "@Year", DbType.Int64, 5);
                            db.AddInParameter(dbcom3, "@Type", DbType.String, "F");
                        }
                        db.AddInParameter(dbcom3, "@FYear", DbType.String, dtMaster.Rows[k]["IMPORTED YEAR"]);
                        db.AddInParameter(dbcom3, "@EstimatedQty", DbType.String, dtMaster.Rows[k]["IMPORTED QUANTITY"]);
                        db.AddInParameter(dbcom3, "@Unit", DbType.String, dtMaster.Rows[k]["IMPORTED UNIT"]);
                        if (dtMaster.Rows[k]["IMPORTED VALUE IN RS LAKH (QTY*PRICE)"].ToString().Trim() == "0" || dtMaster.Rows[k]["IMPORTED VALUE IN RS LAKH (QTY*PRICE)"].ToString().Trim() == "")
                        { db.AddInParameter(dbcom3, "@EstimatedPrice", DbType.String, 0.01); }
                        else
                        {
                            db.AddInParameter(dbcom3, "@EstimatedPrice", DbType.String, dtMaster.Rows[k]["IMPORTED VALUE IN RS LAKH (QTY*PRICE)"]);
                        }
                        db.ExecuteNonQuery(dbcom3, Transaction);
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
        public string SaveExcelProduct2FORSHQ(DataTable dtMaster)
        {
            using (DbConnection Connection = db.CreateConnection())
            {
                Connection.Open();
                DbTransaction Transaction = Connection.BeginTransaction();
                Int32 errorRow = -1;
                try
                {
                    for (int k = 0; k < dtMaster.Rows.Count; k++)
                    {
                        #region code for MasterTable Update in loop
                        errorRow = k;
                        DbCommand cmd = db.GetStoredProcCommand("sp_UpdateProductFromExcel1");
                        db.AddInParameter(cmd, "@ProductRefNo", DbType.String, dtMaster.Rows[k]["ProductRefNo"].ToString());
                        db.AddInParameter(cmd, "@ProductLevel3", DbType.String, dtMaster.Rows[k]["ItemCode"]);
                        db.AddInParameter(cmd, "@YearofIndiginization", DbType.String, dtMaster.Rows[k]["YearofIndiginization"]);
                        db.AddInParameter(cmd, "@PurposeofProcurement", DbType.String, dtMaster.Rows[k]["MakeInIndiaCategory"]);
                        //  db.AddInParameter(cmd, "@QAAgency", DbType.String, dtMaster.Rows[k]["QAAgency"].ToString().Trim());                        
                        db.AddInParameter(cmd, "@IndTargetYear", DbType.String, dtMaster.Rows[k]["IndTargetYear"]);
                        db.AddInParameter(cmd, "@IndProcess", DbType.String, dtMaster.Rows[k]["IndProcess"]);
                        db.AddInParameter(cmd, "@NodelDetail", DbType.String, dtMaster.Rows[k]["NodalOfficerEmail"]);
                        db.AddInParameter(cmd, "@IsShowGeneral", DbType.String, dtMaster.Rows[k]["Display_On_Public_Portal"]);
                        #endregion
                        #region Code For Multiple row in trn table year wise
                        db.AddInParameter(cmd, "@FYear18", DbType.String, dtMaster.Rows[k]["FYear18"].ToString().Trim());
                        db.AddInParameter(cmd, "@EstimatedQty18", DbType.String, dtMaster.Rows[k]["EstimatedQty18"]);
                        db.AddInParameter(cmd, "@Unit18", DbType.String, dtMaster.Rows[k]["Unit18"]);
                        if (dtMaster.Rows[k]["FYear18"].ToString().Trim() != "" && dtMaster.Rows[k]["EstimatedPrice18"].ToString().Trim() != "0")
                        {
                            db.AddInParameter(cmd, "@EstimatedPrice18", DbType.String, dtMaster.Rows[k]["EstimatedPrice18"]);
                        }
                        else
                        {
                            if (dtMaster.Rows[k]["FYear18"].ToString().Trim() != "")
                                db.AddInParameter(cmd, "@EstimatedPrice18", DbType.String, "0.01");
                        }
                        db.AddInParameter(cmd, "@FYear19", DbType.String, dtMaster.Rows[k]["FYear19"].ToString().Trim());
                        db.AddInParameter(cmd, "@EstimatedQty19", DbType.String, dtMaster.Rows[k]["EstimatedQty19"]);
                        db.AddInParameter(cmd, "@Unit19", DbType.String, dtMaster.Rows[k]["Unit19"]);
                        if (dtMaster.Rows[k]["FYear19"].ToString().Trim() != "" && dtMaster.Rows[k]["EstimatedPrice19"].ToString().Trim() != "0")
                        {
                            db.AddInParameter(cmd, "@EstimatedPrice19", DbType.String, dtMaster.Rows[k]["EstimatedPrice19"]);
                        }
                        else
                        {
                            if (dtMaster.Rows[k]["FYear19"].ToString().Trim() != "")
                                db.AddInParameter(cmd, "@EstimatedPrice19", DbType.String, "0.01");
                        }
                        //db.AddInParameter(cmd, "@EstimatedPrice19", DbType.String, dtMaster.Rows[k]["EstimatedPrice19"]);
                        db.AddInParameter(cmd, "@FYear20", DbType.String, dtMaster.Rows[k]["FYear20"].ToString().Trim());
                        db.AddInParameter(cmd, "@EstimatedQty20", DbType.String, dtMaster.Rows[k]["EstimatedQty20"]);
                        db.AddInParameter(cmd, "@Unit20", DbType.String, dtMaster.Rows[k]["Unit20"]);
                        //  db.AddInParameter(cmd, "@EstimatedPrice20", DbType.String, dtMaster.Rows[k]["EstimatedPrice20"]);
                        if (dtMaster.Rows[k]["FYear20"].ToString().Trim() != "" && dtMaster.Rows[k]["EstimatedPrice20"].ToString().Trim() != "0")
                        {
                            db.AddInParameter(cmd, "@EstimatedPrice20", DbType.String, dtMaster.Rows[k]["EstimatedPrice20"]);
                        }
                        else
                        {
                            if (dtMaster.Rows[k]["FYear20"].ToString().Trim() != "")
                                db.AddInParameter(cmd, "@EstimatedPrice20", DbType.String, "0.01");
                        }
                        db.AddInParameter(cmd, "@FYear21", DbType.String, dtMaster.Rows[k]["FYear21"].ToString().Trim());
                        db.AddInParameter(cmd, "@EstimatedQty21", DbType.String, dtMaster.Rows[k]["EstimatedQty21"]);
                        db.AddInParameter(cmd, "@Unit21", DbType.String, dtMaster.Rows[k]["Unit21"]);
                        db.AddInParameter(cmd, "@EstimatedPrice21", DbType.String, dtMaster.Rows[k]["EstimatedPrice21"]);
                        if (dtMaster.Rows[k]["FYear21"].ToString().Trim() != "" && dtMaster.Rows[k]["EstimatedPrice21"].ToString().Trim() != "0")
                        {
                            db.AddInParameter(cmd, "@EstimatedPrice21", DbType.String, dtMaster.Rows[k]["EstimatedPrice21"]);
                        }
                        else
                        {
                            if (dtMaster.Rows[k]["FYear21"].ToString().Trim() != "")
                                db.AddInParameter(cmd, "@EstimatedPrice21", DbType.String, "0.01");
                        }
                        #endregion
                        db.ExecuteNonQuery(cmd, Transaction);
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
        public DataTable RetriveFilterCodeupdate(string CompRefNo, string DivisionRefNo, string UnitRefNo, string SearchValue, string Criteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("new_successstory");
                    db.AddInParameter(cmd, "@CompRefNo", DbType.String, CompRefNo);
                    db.AddInParameter(cmd, "@DivisionRefNo", DbType.String, DivisionRefNo);
                    db.AddInParameter(cmd, "@UnitRefNo", DbType.String, UnitRefNo);
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
        public DataTable GetInteresteddata(string CompanyRefNo, string DivisionRefNo, string UnitRefNo)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_GetInteresteddata");
                    db.AddInParameter(cmd, "@CompanyRefNo", DbType.String, CompanyRefNo);
                    db.AddInParameter(cmd, "@DivisionRefNo", DbType.String, DivisionRefNo);
                    db.AddInParameter(cmd, "@UnitRefNo", DbType.String, UnitRefNo);
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
        public string TargetValueUpdate(string hfproc, string lblComp, string TotalProd, string texttarget, string lblinhouse, string lblmakeii, string lblotherthan, string Totalindigenized, string lblannual)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_TargetValue");
                    db.AddInParameter(cmd, "@CompRefNo", DbType.String, hfproc);
                    db.AddInParameter(cmd, "@CompName", DbType.String, lblComp);
                    db.AddInParameter(cmd, "@TotalItemUploaded", DbType.String, TotalProd);
                    db.AddInParameter(cmd, "@TargetIndigenization202021", DbType.String, texttarget);
                    db.AddInParameter(cmd, "@Inhouseindigenized", DbType.String, lblinhouse);
                    db.AddInParameter(cmd, "@makeiiindigenized", DbType.String, lblmakeii);
                    db.AddInParameter(cmd, "@otherthanmakeii", DbType.String, lblotherthan);
                    db.AddInParameter(cmd, "@totalitemsindigenized202021", DbType.String, Totalindigenized);
                    db.AddInParameter(cmd, "@totalannualvalue", DbType.String, lblannual);
                    db.ExecuteNonQuery(cmd, dbTran);
                    dbTran.Commit();
                    return "Save";
                }
                catch (Exception e)
                {
                    dbTran.Rollback();
                    return "-1";
                }
            }
        }
        public DataTable Getdroppopup(string SearchValue)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("nsgpopup");
                    db.AddInParameter(cmd, "@SearchValue", DbType.String, SearchValue);
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
        public DataTable Getproductforpopup(string SearchValue)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("ProductIndusrtypopup");
                    db.AddInParameter(cmd, "@SearchValue", DbType.String, SearchValue);
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
        public DataTable binddataforpopup(string Search)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_TestLab");
                    db.AddInParameter(cmd, "@Search", DbType.String, Search);

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
        public DataTable MaketwoReport(string CompRefNo, string DivisionRefNo, string UnitRefNo, string SearchValue, string Criteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("new_successstory");
                    db.AddInParameter(cmd, "@CompRefNo", DbType.String, CompRefNo);
                    db.AddInParameter(cmd, "@DivisionRefNo", DbType.String, DivisionRefNo);
                    db.AddInParameter(cmd, "@UnitRefNo", DbType.String, UnitRefNo);
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
        #region HelpDesk
        public string SaveHelpDesk(HybridDictionary hyhelp, out string _sysMsg, out string _msg)
        {
            using (DbConnection dbcon = db.CreateConnection())
            {
                string mCurrentID;
                dbcon.Open();
                DbTransaction dbTran = dbcon.BeginTransaction();
                try
                {
                    DbCommand dbcom = db.GetStoredProcCommand("sp_HelpDesk");
                    db.AddInParameter(dbcom, "@HFrom", DbType.String, hyhelp["HFrom"]);
                    db.AddInParameter(dbcom, "@QueryFor", DbType.String, hyhelp["QueryFor"]);
                    db.AddInParameter(dbcom, "@Name", DbType.String, hyhelp["Name"]);
                    db.AddInParameter(dbcom, "@Email", DbType.String, hyhelp["Email"]);
                    db.AddInParameter(dbcom, "@MobileNo", DbType.Int64, hyhelp["MobileNo"]);
                    db.AddInParameter(dbcom, "@State", DbType.String, hyhelp["State"]);
                    db.AddInParameter(dbcom, "@Subject", DbType.String, hyhelp["Subject"]);
                    db.AddInParameter(dbcom, "@Address", DbType.String, hyhelp["Address"]);
                    db.AddInParameter(dbcom, "@Issue", DbType.String, hyhelp["Issue"]);
                    db.AddInParameter(dbcom, "@Files", DbType.String, hyhelp["Files"]);
                    db.AddInParameter(dbcom, "@SubSubjectId", DbType.Int64, hyhelp["SubSubjectId"]);
                    db.AddOutParameter(dbcom, "@ReturnID", DbType.String, 70);
                    db.ExecuteNonQuery(dbcom, dbTran);
                    mCurrentID = db.GetParameterValue(dbcom, "@ReturnID").ToString();
                    dbTran.Commit();
                    _msg = "Save";
                    _sysMsg = "Save";
                    return mCurrentID;
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
        public DataTable RetriveHelpdesk(Int32 id, Int32 id1, Int32 id2, string code, string code1, string code2, string code3, string criteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_HelpDeskRetrive");
                    db.AddInParameter(cmd, "@Value", DbType.Int32, id);
                    db.AddInParameter(cmd, "@Value2", DbType.Int32, id1);
                    db.AddInParameter(cmd, "@Value3", DbType.Int32, id2);
                    db.AddInParameter(cmd, "@String1", DbType.String, code);
                    db.AddInParameter(cmd, "@String2", DbType.String, code1);
                    db.AddInParameter(cmd, "@String3", DbType.String, code2);
                    db.AddInParameter(cmd, "@String4", DbType.String, code3);
                    db.AddInParameter(cmd, "@Criteria", DbType.String, criteria);
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

        public string SaveGUser(HybridDictionary hyhelp, out string _sysMsg, out string _msg)
        {
            using (DbConnection dbcon = db.CreateConnection())
            {
                string mCurrentID;
                dbcon.Open();
                DbTransaction dbTran = dbcon.BeginTransaction();
                try
                {
                    DbCommand dbcom = db.GetStoredProcCommand("sp_HelpDeskRegisUser");
                    db.AddInParameter(dbcom, "@Type", DbType.String, hyhelp["Type"]);
                    db.AddInParameter(dbcom, "@Name", DbType.String, hyhelp["Name"]);
                    db.AddInParameter(dbcom, "@UserName", DbType.String, hyhelp["UserName"]);
                    db.AddInParameter(dbcom, "@MobileNo", DbType.String, hyhelp["MobileNo"]);
                    db.AddOutParameter(dbcom, "@ReturnID", DbType.String, 70);
                    db.ExecuteNonQuery(dbcom, dbTran);
                    mCurrentID = db.GetParameterValue(dbcom, "@ReturnID").ToString();
                    dbTran.Commit();
                    _msg = "Save";
                    _sysMsg = "Save";
                    return mCurrentID;
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
        #region Profilemanagement
        public DataTable RetriveVendorProfMgmt(string VEmail)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_GetprofileDetail");
                    db.AddInParameter(cmd, "@Email", DbType.String, VEmail);
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
        public DataTable UpdateVendorProfMgmt(string MobileNo, string VEmail)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_UpdateprofileDetailVPM");
                    db.AddInParameter(cmd, "@Email", DbType.String, VEmail);
                    db.AddInParameter(cmd, "@Mobno", DbType.String, MobileNo);
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
        public DataTable UpdateVendorEmailProfMgmt(string EmailID, string venderID)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_UpdateprofileEmailVPM");
                    db.AddInParameter(cmd, "@NODemail", DbType.String, EmailID);
                    db.AddInParameter(cmd, "@VedtRid", DbType.String, venderID);
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
        public DataTable UpdateVendorProfMgmt(string MobileNo, string VEmail, string StreetAddress, string StreetAddressLine2, string City, string State, string ZipCode, string VendorID5)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_UpdateprofileDetailVPM");
                    db.AddInParameter(cmd, "@Email", DbType.String, VEmail);
                    db.AddInParameter(cmd, "@Mobno", DbType.String, MobileNo);
                    db.AddInParameter(cmd, "@street1", DbType.String, StreetAddress);
                    db.AddInParameter(cmd, "@street2", DbType.String, StreetAddressLine2);
                    db.AddInParameter(cmd, "@city1", DbType.String, City);
                    db.AddInParameter(cmd, "@state1", DbType.String, State);
                    db.AddInParameter(cmd, "@zipcode1", DbType.String, ZipCode);
                    db.AddInParameter(cmd, "@VedtRid", DbType.String, VendorID5);
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
        public DataTable UpdateVendorAuthenticationProfMgmt(string filename, string email)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_UpdateAuthLatterDetailVPM");
                    db.AddInParameter(cmd, "@NODemail", DbType.String, email);
                    db.AddInParameter(cmd, "@Filename", DbType.String, filename);
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
        public DataTable UpdateVendorIdentityCardProfMgmt(string filename, string email)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_UpdateIdentityCardVPM");
                    db.AddInParameter(cmd, "@NODemail", DbType.String, email);
                    db.AddInParameter(cmd, "@Filename", DbType.String, filename);
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
        public DataTable profileMigrateVendorProfMgmt(string NodalOfficerName, string contact, string VEmail, string StreetAddress, string StreetAddressLine2,
            string City, string State, string ZipCode, string VendorIMGID, string Authorization, string Identity, string emailsend, string confirmationemail)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_MigrateVendorProfilelVPM");
                    db.AddInParameter(cmd, "@NodelOffName", DbType.String, NodalOfficerName);
                    db.AddInParameter(cmd, "@Email", DbType.String, VEmail);
                    db.AddInParameter(cmd, "@Mobno", DbType.String, contact);
                    db.AddInParameter(cmd, "@street1", DbType.String, StreetAddress);
                    db.AddInParameter(cmd, "@street2", DbType.String, StreetAddressLine2);
                    db.AddInParameter(cmd, "@city1", DbType.String, City);
                    db.AddInParameter(cmd, "@state1", DbType.String, State);
                    db.AddInParameter(cmd, "@zipcode1", DbType.String, ZipCode);
                    db.AddInParameter(cmd, "@VedtRid", DbType.String, VendorIMGID);
                    db.AddInParameter(cmd, "@Auth", DbType.String, Authorization);
                    db.AddInParameter(cmd, "@Ident", DbType.String, Identity);
                    db.AddInParameter(cmd, "@flag1", DbType.String, emailsend);
                    db.AddInParameter(cmd, "@flag2", DbType.String, confirmationemail);

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
        public DataTable profileMigrateVendorProfMgmt(string NodalOfficerName, string contact, string VEmail, string StreetAddress, string StreetAddressLine2, string City, string State, string ZipCode, string VendorIMGID)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_MigrateVendorProfilelVPM");
                    db.AddInParameter(cmd, "@NodelOffName", DbType.String, NodalOfficerName);
                    db.AddInParameter(cmd, "@Email", DbType.String, VEmail);
                    db.AddInParameter(cmd, "@Mobno", DbType.String, contact);
                    db.AddInParameter(cmd, "@street1", DbType.String, StreetAddress);
                    db.AddInParameter(cmd, "@street2", DbType.String, StreetAddressLine2);
                    db.AddInParameter(cmd, "@city1", DbType.String, City);
                    db.AddInParameter(cmd, "@state1", DbType.String, State);
                    db.AddInParameter(cmd, "@zipcode1", DbType.String, ZipCode);
                    db.AddInParameter(cmd, "@VedtRid", DbType.String, VendorIMGID);
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
        public DataTable GetverifyemailIDVendorProfMgmt(string VEmail)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_GetprofileDetail");
                    db.AddInParameter(cmd, "@Email", DbType.String, VEmail);
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

        public DataTable GetverifyemailIDProfilemigratVendor(string VEmail)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_GetprofileDetail");
                    db.AddInParameter(cmd, "@Email", DbType.String, VEmail);
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
        #region choteylalji
        public DataTable GetAllPincode()
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_GetAllPinCode");
                    cmd.CommandTimeout = 54000;
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
        public DataTable GetCityNameWithPin(string PinCode)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_GetCitywithPinCode");
                    db.AddInParameter(cmd, "@PinCode", DbType.String, PinCode);
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
        public DataTable GetStateWithCity(string StateId)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_GetStatewithWithCity");
                    db.AddInParameter(cmd, "@StateId", DbType.String, StateId);
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
        public DataTable GetAllCity()
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_GetAllCity");
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
        public DataTable GetAllState()
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_GetAllState");
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
        public string Savechecklist(string checklist, string mcurrentId, out string _sysMsg, out string _msg)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                DbTransaction dbTran = dbCon.BeginTransaction();
                try
                {

                    DbCommand dbcom1 = db.GetStoredProcCommand("sp_VendorCheckList");
                    db.AddInParameter(dbcom1, "@VendorRefNo", DbType.String, mcurrentId);
                    db.AddInParameter(dbcom1, "@CheckListId", DbType.String, checklist);

                    db.ExecuteNonQuery(dbcom1, dbTran);

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
        #region naveen
        public DataTable RetriveVendorNodalOfficerDetails(string venderID)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_getvendorNodelofficerDetails");
                    db.AddInParameter(cmd, "@VedtRid", DbType.String, venderID);
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
        public DataTable AddNewUserMvendor(string UserName, string contact, string VEmail, string StreetAddress, string StreetAddressLine2, string City, string State, string ZipCode, string VendorIMGID, string Password, string userType)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_addnewuserVM");
                    db.AddInParameter(cmd, "@UserName", DbType.String, UserName);
                    db.AddInParameter(cmd, "@Email", DbType.String, VEmail);
                    db.AddInParameter(cmd, "@Mobno", DbType.String, contact);
                    db.AddInParameter(cmd, "@street1", DbType.String, StreetAddress);
                    db.AddInParameter(cmd, "@street2", DbType.String, StreetAddressLine2);
                    db.AddInParameter(cmd, "@city1", DbType.String, City);
                    db.AddInParameter(cmd, "@state1", DbType.String, State);
                    db.AddInParameter(cmd, "@zipcode1", DbType.String, ZipCode);
                    db.AddInParameter(cmd, "@VedtRid", DbType.String, VendorIMGID);
                    db.AddInParameter(cmd, "@Pass", DbType.String, Password);
                    db.AddInParameter(cmd, "@usertyp", DbType.String, userType);

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
        public DataTable RetriveUserDetailsMgmt(string VendorID, string Type)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_GetVuserDetails");
                    db.AddInParameter(cmd, "@VndorID", DbType.String, VendorID);
                    db.AddInParameter(cmd, "@Usertype", DbType.String, Type);
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
        public DataTable AddNewNODofficVendorgmt(string NodalOfficerName, string contact, string VEmail, string VendorIMGID, string Authorization, string Identity, string NOffpass, string NodalEmpCode, string NodalOfficerDepartment, string NodalOfficerDesignation, string NodalOfficerTelephone, string NodalOfficerFax, string CompanyRefNo, string Type, string Salt, string TempRef, string IsActive, string IsLoginActive, string IsNodalOfficer, string DefaultPage, string CreatedBy)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_AddNewNodalOfficerVendor");
                    db.AddInParameter(cmd, "@NodelOffName", DbType.String, NodalOfficerName);
                    db.AddInParameter(cmd, "@Email", DbType.String, VEmail);
                    db.AddInParameter(cmd, "@Mobno", DbType.String, contact);
                    db.AddInParameter(cmd, "@Empcode", DbType.String, NodalEmpCode);
                    db.AddInParameter(cmd, "@Noddep", DbType.String, NodalOfficerDepartment);
                    db.AddInParameter(cmd, "@Noddesig", DbType.String, NodalOfficerDesignation);
                    db.AddInParameter(cmd, "@Tphone", DbType.String, NodalOfficerTelephone);
                    db.AddInParameter(cmd, "@NoffFax", DbType.String, NodalOfficerFax);
                    db.AddInParameter(cmd, "@Comp", DbType.String, CompanyRefNo);
                    db.AddInParameter(cmd, "@tNype", DbType.String, Type);
                    db.AddInParameter(cmd, "@Nsalt", DbType.String, Salt);
                    db.AddInParameter(cmd, "@TempREF", DbType.String, TempRef);
                    db.AddInParameter(cmd, "@IsActive", DbType.String, IsActive);
                    db.AddInParameter(cmd, "@IsLoginActive", DbType.String, IsLoginActive);
                    db.AddInParameter(cmd, "@IsNodalOfficer", DbType.String, IsNodalOfficer);
                    db.AddInParameter(cmd, "@DfultPage", DbType.String, DefaultPage);
                    db.AddInParameter(cmd, "@CrtBy", DbType.String, CreatedBy);
                    db.AddInParameter(cmd, "@VedtRid", DbType.String, VendorIMGID);
                    db.AddInParameter(cmd, "@Auth", DbType.String, Authorization);
                    db.AddInParameter(cmd, "@Ident", DbType.String, Identity);
                    db.AddInParameter(cmd, "@pass", DbType.String, NOffpass);

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
        public DataTable AddLatestPostalcodeVendorMST(string postaladdress, string ZipCode)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_addNewpostalcodeMST");
                    db.AddInParameter(cmd, "@PosatalAddress", DbType.String, postaladdress);
                    db.AddInParameter(cmd, "@NewZipcode1", DbType.String, ZipCode);
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
        public DataTable AddLatestCityVendorMST(string City, string State, string ZipCode)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_addNewpostalnCity");
                    db.AddInParameter(cmd, "@NewCity", DbType.String, City);
                    db.AddInParameter(cmd, "@NewState", DbType.String, State);
                    db.AddInParameter(cmd, "@NewZipcode", DbType.String, ZipCode);
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
        public DataTable RetriveMigrationProfDetails(string offliceID, string newNoEmailID)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_GetNodalOfficerMigrationDetail");
                    db.AddInParameter(cmd, "@trackID", DbType.String, offliceID);
                    db.AddInParameter(cmd, "@trackEmaiID", DbType.String, newNoEmailID);
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
        public DataTable AddnewvendorprofilVMGMT(string NodalOfficerName, string Contact, string VEmail, string StreetAddress, string StreetAddressLine2, string City, string State, string ZipCode, string VendorIMGID, string Authorization, string Identity, string vendorRefferNO, string registCAT, string TypeOfBuisness, string BusinessSector, string Country, string MasterAllowed, string IsActive, string IsLoginActive, string DefaultPage, string Type, string RecInsTime, string CompanyName, string PanNO, string GSTno)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_addNewVendorVMNGMT");
                    db.AddInParameter(cmd, "@NodelOffName", DbType.String, NodalOfficerName);
                    db.AddInParameter(cmd, "@Email", DbType.String, VEmail);
                    db.AddInParameter(cmd, "@Mobno", DbType.String, Contact);
                    db.AddInParameter(cmd, "@street1", DbType.String, StreetAddress);
                    db.AddInParameter(cmd, "@street2", DbType.String, StreetAddressLine2);
                    db.AddInParameter(cmd, "@city1", DbType.String, City);
                    db.AddInParameter(cmd, "@state1", DbType.String, State);
                    db.AddInParameter(cmd, "@zipcode1", DbType.String, ZipCode);
                    db.AddInParameter(cmd, "@VedtRid", DbType.String, VendorIMGID);
                    db.AddInParameter(cmd, "@Auth", DbType.String, Authorization);
                    db.AddInParameter(cmd, "@Ident", DbType.String, Identity);
                    db.AddInParameter(cmd, "@vendorRefferNO", DbType.String, vendorRefferNO);
                    db.AddInParameter(cmd, "@registCAT", DbType.String, registCAT);
                    //db.AddInParameter(cmd, "@TypeOfBuisness", DbType.String, TypeOfBuisness);
                    //db.AddInParameter(cmd, "@BusinessSector", DbType.String, BusinessSector);
                    //db.AddInParameter(cmd, "@Country", DbType.String, Country);
                    db.AddInParameter(cmd, "@MasterAllowed", DbType.String, MasterAllowed);
                    db.AddInParameter(cmd, "@IsActive", DbType.String, IsActive);
                    db.AddInParameter(cmd, "@IsLoginActive", DbType.String, IsLoginActive);
                    db.AddInParameter(cmd, "@DefaultPage", DbType.String, DefaultPage);
                    db.AddInParameter(cmd, "@Type", DbType.String, Type);
                    db.AddInParameter(cmd, "@RecInsTime", DbType.String, RecInsTime);
                    db.AddInParameter(cmd, "@CompanyName", DbType.String, CompanyName);
                    db.AddInParameter(cmd, "@PanNO", DbType.String, PanNO);
                    db.AddInParameter(cmd, "@GSTno", DbType.String, GSTno);

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

        public DataTable UpdatenewNodalofficerinfoMgmt(string VendorID, string NodalOfficerName, string NodalOfficerEmail, string ContactNo, string StreetAddress, string StreetAddressLine2, string City, string vState, string ZipCode, string authrizlatt, string indetityCard)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_UpdateNewnodelinfoVPM");
                    db.AddInParameter(cmd, "@NooffID", DbType.String, VendorID);
                    db.AddInParameter(cmd, "@NoOffiname", DbType.String, NodalOfficerName);
                    db.AddInParameter(cmd, "@NodalOffemail", DbType.String, NodalOfficerEmail);
                    db.AddInParameter(cmd, "@NOoffcontact", DbType.String, ContactNo);
                    db.AddInParameter(cmd, "@street", DbType.String, StreetAddress);
                    db.AddInParameter(cmd, "@streetad", DbType.String, StreetAddressLine2);
                    db.AddInParameter(cmd, "@NOcity", DbType.String, City);
                    db.AddInParameter(cmd, "@NOstate", DbType.String, vState);
                    db.AddInParameter(cmd, "@NOpin", DbType.String, ZipCode);
                    db.AddInParameter(cmd, "@NOauth", DbType.String, authrizlatt);
                    db.AddInParameter(cmd, "@NOID", DbType.String, indetityCard);

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
        #region Productwizard
        public DataTable ProductWizard(Int32 a, Int32 b, Int32 c, Int32 d, Int32 e, string f, string g, string h, string i, string j, string k, string l, string m, string n, string @Criteria)
        {
            using (DbConnection dbCon = db.CreateConnection())
            {
                dbCon.Open();
                try
                {
                    DbCommand cmd = db.GetStoredProcCommand("sp_ProductWizard");
                    db.AddInParameter(cmd, "@Value", DbType.Int32, a);
                    db.AddInParameter(cmd, "@Value2", DbType.Int32, b);
                    db.AddInParameter(cmd, "@Value3", DbType.Int32, c);
                    db.AddInParameter(cmd, "@Value4", DbType.Int32, d);
                    db.AddInParameter(cmd, "@Value5", DbType.Int32, e);
                    db.AddInParameter(cmd, "@String", DbType.String, f);
                    db.AddInParameter(cmd, "@String1", DbType.String, g);
                    db.AddInParameter(cmd, "@String2", DbType.String, h);
                    db.AddInParameter(cmd, "@String3", DbType.String, i);
                    db.AddInParameter(cmd, "@String4", DbType.String, j);
                    db.AddInParameter(cmd, "@String5", DbType.String, k);
                    db.AddInParameter(cmd, "@String6", DbType.String, l);
                    db.AddInParameter(cmd, "@String7", DbType.String, m);
                    db.AddInParameter(cmd, "@String8", DbType.String, n);
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


    }
}
