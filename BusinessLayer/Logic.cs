﻿using System;
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
        public string VerifyEmployee(HybridDictionary hyLogin, out string _msg)
        {
            return SqlHelper.Instance.VerifyEmployee(hyLogin, out _msg);
        }
        #endregion
        #region SaveCode
        public string SaveFDI(HybridDictionary HySave, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveFDI(HySave, out _sysMsg, out _msg);
        }
        public string SaveMasterCompany(HybridDictionary HyCompSave, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveMasterCompany(HyCompSave, out _sysMsg, out _msg);
        }
        public string SaveMasterComp(HybridDictionary hysavecomp, out string _sysMsg, out string _msg)
        {
            return SqlHelper.Instance.SaveMasterComp(hysavecomp, out _sysMsg, out _msg);
        }
        #endregion
        #region UpdateCode
        public string UpdateLoginPassword(string NewPass, string OldPass, string User,string type)
        {
            return SqlHelper.Instance.UpdateLoginPassword(NewPass, OldPass, User,type);
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
        public DataTable RetriveGridViewCompany(string ID)
        {
            return SqlHelper.Instance.RetriveGridViewCompany(ID);
        }
        public DataTable RetriveCompany(string text, Int64 id, string value, int InterestedArea)
        {
            return SqlHelper.Instance.RetriveCompany(text, id, value,InterestedArea);
        }
        
        public DataTable RetriveCompany1(string text, string id, string value)
        {
            return SqlHelper.Instance.RetriveCompany1(text, id, value);
        }
        
        
        #endregion
        #region DeleteCode
        public string DeleteRecord(Int64 ID)
        {
            return SqlHelper.Instance.DeleteRecord(ID);
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
        public DataTable RetriveAggregateValue(string function, string entity)
        {
            return SqlHelper.Instance.RetriveAggregateValue(function, entity);
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
