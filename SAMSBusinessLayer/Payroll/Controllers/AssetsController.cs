using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;
using SAMSDatabaseLayer.Classes;
using SAMSBusinessLayer.Models;
using System.Collections.Generic;

namespace SAMSBusinessLayer.Classes
{
    /// <summary>
    /// Class For Company Related Tasks
    /// <remarks>
    /// <list type="bullet">
    /// <item>Insert Company</item>
    /// <item>Get Company</item>
    /// </list>
    /// </remarks>
    /// </summary>
    public class AssetsController
    {
        /// <summary>
        /// Constructor for CompanyController Class
        /// </summary>
        public AssetsController()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        /// <summary>
        /// Gets Company Data
        /// </summary>
        /// <remarks>
        /// Returns Company Data as Datatable
        /// </remarks>
        /// <param name="p_COMPANY_ID">Company</param>
        /// <param name="p_STATUS">Status</param>
        /// <returns>Company Data as Datatable</returns>
        public bool InsertAssetsType(AssetsTypeModel atm)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTranscation = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTranscation = ProviderFactory.GetTransaction(mConnection);
                spInsertAssetTypes loan = new spInsertAssetTypes();
                loan.Transaction = mTranscation;
                loan.Connection = mConnection;
                loan.CompanyID = atm.CompanyID;
                loan.AssetTypeName = atm.AssetTypeName;
                loan.User_ID = atm.User_ID;
                loan.ExecuteQuery();            
                mTranscation.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public bool UpdateAssetsType(AssetsTypeModel atm)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTranscation = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTranscation = ProviderFactory.GetTransaction(mConnection);
                spUpdateAssetTypes loan = new spUpdateAssetTypes();
                loan.Transaction = mTranscation;
                loan.Connection = mConnection;
                loan.AssetTypeID = atm.AssetTypeID;                
                loan.AssetTypeName = atm.AssetTypeName;
                loan.User_ID = atm.User_ID;
                loan.IS_ACTIVE = atm.IS_ACTIVE;
                loan.IS_DELETED = atm.IS_DELETED;
                loan.ExecuteQuery();
                mTranscation.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                throw exp;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
    }
}
