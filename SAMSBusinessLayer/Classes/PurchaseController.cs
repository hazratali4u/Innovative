using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;
using SAMSDatabaseLayer.Classes;

namespace SAMSBusinessLayer.Classes
{
    /// <summary>
    /// Class For Purchase, TranferOut, Purchase Return, TranferIn And Damage Related Tasks
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// Insert Purchase, TranferOut, Purchase Return, TranferIn And Damage
    /// </item>
    /// <term>
    /// Update Purchase, TranferOut, Purchase Return, TranferIn And Damage
    /// </term>
    /// <item>
    /// Get Purchase, TranferOut, Purchase Return, TranferIn And Damage
    /// </item>
    /// </list>
    /// </example>
    /// </summary>
    public class PurchaseController
    {
        #region Constructor

        /// <summary>
        /// Constructor for PurchaseController
        /// </summary>
        public PurchaseController()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#endregion

        #region Private Variables
        
        IDbTransaction mTransaction;
        IDbConnection mConnection;

        #endregion

        #region Public Methods

        #region Select

        /// <summary>
        /// Get Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable
        /// </remarks>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="PConnection">Connection</param>
        /// <param name="PTransaction">Transaction</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable</returns>
        public DataTable SelectPrivousePurchaseDetail(int p_DISTRIBUTOR_ID, long p_PURCHASE_MASTER_ID, IDbConnection PConnection, IDbTransaction PTransaction)
        {
            try
            {
                spSelectPURCHASE_DETAIL mPurchaseDetail = new spSelectPURCHASE_DETAIL();
                mPurchaseDetail.Connection = PConnection;
                mPurchaseDetail.Transaction = PTransaction;
                mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseDetail.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                DataTable dt = mPurchaseDetail.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
        }

        /// <summary>
        /// Gets Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No as Datatable
        /// </remarks>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="p_User_Id">InsertedBy</param>
        /// <param name="p_Posting">Posting</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage  Document No as Datatable</returns>
        public DataTable SelectPurchaseDocumentNo(int p_TYPE_ID, int p_DISTRIBUTOR_ID, long p_PURCHASE_MASTER_ID, int p_User_Id, int p_Posting)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_MASTER mPurchaseMaster = new spSelectPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.USER_ID = p_User_Id;
                mPurchaseMaster.POSTING = p_Posting;
                DataTable dt = mPurchaseMaster.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        /// <summary>
        /// Gets Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No as Datatable
        /// </remarks>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="P_DocumentDate">Date</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No as Datatable</returns>
        public DataTable SelectPurchaseDocumentNo(int p_TYPE_ID, int p_DISTRIBUTOR_ID, DateTime P_DocumentDate)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_MASTER mPurchaseMaster = new spSelectPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.DOCUMENT_DATE = P_DocumentDate;
                DataTable dt = mPurchaseMaster.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        /// <summary>
        /// Gets Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No as Datatable
        /// </remarks>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="p_User_Id">InsertedBy</param>
        /// <param name="p_Posting">Posting</param>
        /// <param name="p_SOLD_TO">SoldTo</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage Document No as Datatable</returns>
        public DataTable SelectPurchaseDocumentNo(int p_TYPE_ID, int p_DISTRIBUTOR_ID, long p_PURCHASE_MASTER_ID, int p_User_Id, int p_Posting, int p_SOLD_TO)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_MASTER mPurchaseMaster = new spSelectPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.USER_ID = Constants.IntNullValue;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                DataTable dt = mPurchaseMaster.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        /// <summary>
        /// Get Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable
        /// </remarks>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable</returns>
        public DataTable SelectPurchaseDetail(int p_DISTRIBUTOR_ID, long p_PURCHASE_MASTER_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_DETAIL mPurchaseDetail = new spSelectPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseDetail.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                DataTable dt = mPurchaseDetail.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        /// <summary>
        /// Get Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable
        /// </remarks>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable</returns>
        public DataTable GetGRNDetail(string p_GRN_MASTER_ID, int p_DISTRIBUTOR_ID, int p_TYPE_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetGRNDETAIL mPurchaseDetail = new uspGetGRNDETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.GRN_MASTER_ID = p_GRN_MASTER_ID;
                mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseDetail.TYPE_ID = p_TYPE_ID;
                DataTable dt = mPurchaseDetail.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public DataTable GetGRNNo(long p_PURCHASE_MASTER_ID, long p_PO_ID, int p_TYPE_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetGRNNo mPurchaseDetail = new uspGetGRNNo();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseDetail.PO_ID = p_PO_ID;
                mPurchaseDetail.TYPE_ID = p_TYPE_ID;
                DataTable dt = mPurchaseDetail.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        #region Material Issuance

        public DataTable GetMaterialIssuanceMaster(int p_DISTRIBUTOR_ID,DateTime p_DOCUMENT_DATE)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetMaterialIssuanceMaster mPurchase = new uspGetMaterialIssuanceMaster();
                mPurchase.Connection = mConnection;
                mPurchase.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchase.DOCUMENT_DATE = p_DOCUMENT_DATE;
                DataTable dt = mPurchase.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public DataTable GetMaterialIssuanceDetail(int p_MATERIAL_ISSUANCE_MASTER_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetMaterialIssuanceDetail mPurchaseDetail = new uspGetMaterialIssuanceDetail();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.MATERIAL_ISSUANCE_MASTER_ID = p_MATERIAL_ISSUANCE_MASTER_ID;
                DataTable dt = mPurchaseDetail.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        #endregion

        #region Purchase Requisition

        public DataTable GetRequisition(int p_TYPE_ID, int p_STATUS_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectREQUISITION_MASTER mRequisition = new spSelectREQUISITION_MASTER();
                mRequisition.Connection = mConnection;
                mRequisition.TYPE_ID = p_TYPE_ID;
                mRequisition.STATUS_ID = p_STATUS_ID;
                DataTable dt = mRequisition.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }


       



        public DataTable SelectRequisitionView(long p_REQUISITION_VIEW_ID,long p_REQUISITION_MASTER_ID,int p_ALLOCATED_USER_ID,int p_PRIORITY_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectRequisitionView mRequisition = new spSelectRequisitionView();
                mRequisition.Connection = mConnection;
                mRequisition.ALLOCATED_USER_ID = p_ALLOCATED_USER_ID;
                mRequisition.PRIORITY_ID = p_PRIORITY_ID;
                mRequisition.REQUISITION_MASTER_ID = p_REQUISITION_MASTER_ID;
                mRequisition.REQUISITION_VIEW_ID = p_REQUISITION_VIEW_ID;
                DataTable dt = mRequisition.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public DataSet GetRequisition(int p_TYPE_ID, long p_REQUISITION_MASTER_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectREQUISITION_MASTER mRequisition = new spSelectREQUISITION_MASTER();
                mRequisition.Connection = mConnection;
                mRequisition.TYPE_ID = p_TYPE_ID;
                mRequisition.REQUISITION_MASTER_ID = p_REQUISITION_MASTER_ID;
                return mRequisition.ExecuteDataSet();
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public DataTable SelectEmployeeByDepartmentName(string p_DepartmentName)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectEmployeeByName mRequisition = new spSelectEmployeeByName();
                mRequisition.Connection = mConnection;
                mRequisition.DepartmentName = p_DepartmentName;
                return mRequisition.ExecuteTable();
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        public DataTable GetRequisition(int p_TYPE_ID, int p_STATUS_ID, int p_DISTRIBUTOR_ID, int p_PRINCIPAL_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectREQUISITION_MASTER mRequisition = new spSelectREQUISITION_MASTER();
                mRequisition.Connection = mConnection;
                mRequisition.TYPE_ID = p_TYPE_ID;
                mRequisition.STATUS_ID = p_STATUS_ID;
                mRequisition.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mRequisition.PRINCIPAL_ID = p_PRINCIPAL_ID;
                DataTable dt = mRequisition.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        #endregion

        #region Comparative Statement

        public DataTable GetComparativeStatement(int p_TYPE_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectCOMPARATIVE_STATEMENT_MASTER mRequisition = new spSelectCOMPARATIVE_STATEMENT_MASTER();
                mRequisition.Connection = mConnection;
                mRequisition.TYPE_ID = p_TYPE_ID;
                DataTable dt = mRequisition.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public DataSet GetComparativeStatement(int p_TYPE_ID, long p_COMPARATIVE_STATEMENT_MASTER_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectCOMPARATIVE_STATEMENT_MASTER mRequisition = new spSelectCOMPARATIVE_STATEMENT_MASTER();
                mRequisition.Connection = mConnection;
                mRequisition.TYPE_ID = p_TYPE_ID;
                mRequisition.COMPARATIVE_STATEMENT_MASTER_ID = p_COMPARATIVE_STATEMENT_MASTER_ID;
                return mRequisition.ExecuteDataSet();
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        #endregion

        #region GRN

        public DataTable GetGRN(int p_TYPE_ID, int p_STATUS_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectGRN_MASTER mRequisition = new spSelectGRN_MASTER();
                mRequisition.Connection = mConnection;
                mRequisition.TYPE_ID = p_TYPE_ID;
                mRequisition.STATUS_ID = p_STATUS_ID;
                DataTable dt = mRequisition.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public DataSet GetGRN(int p_TYPE_ID, long p_GRN_MASTER_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectGRN_MASTER mRequisition = new spSelectGRN_MASTER();
                mRequisition.Connection = mConnection;
                mRequisition.TYPE_ID = p_TYPE_ID;
                mRequisition.GRN_MASTER_ID = p_GRN_MASTER_ID;
                return mRequisition.ExecuteDataSet();
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public DataTable GetGRN(int p_TYPE_ID, long p_GRN_MASTER_ID, long p_REQUISITION_MASTER_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectGRN_MASTER mRequisition = new spSelectGRN_MASTER();
                mRequisition.Connection = mConnection;
                mRequisition.TYPE_ID = p_TYPE_ID;
                mRequisition.GRN_MASTER_ID = p_GRN_MASTER_ID;
                mRequisition.REQUISITION_MASTER_ID = p_REQUISITION_MASTER_ID;
                return mRequisition.ExecuteTable();
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        

        public DataTable GetGRN(int p_TYPE_ID, int p_STATUS_ID, int p_DISTRIBUTOR_ID, int p_PRINCIPAL_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectGRN_MASTER mRequisition = new spSelectGRN_MASTER();
                mRequisition.Connection = mConnection;
                mRequisition.TYPE_ID = p_TYPE_ID;
                mRequisition.STATUS_ID = p_STATUS_ID;
                mRequisition.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mRequisition.PRINCIPAL_ID = p_PRINCIPAL_ID;
                DataTable dt = mRequisition.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        #endregion

        #region QC

        public DataTable GetQC(long p_QC_MASTER_ID, long p_GRN_MASTER_ID, int p_USER_ID, int p_TYPE_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectQC_MASTER mQC = new spSelectQC_MASTER();
                mQC.Connection = mConnection;
                mQC.QC_MASTER_ID = p_QC_MASTER_ID;
                mQC.GRN_MASTER_ID = p_GRN_MASTER_ID;
                mQC.USER_ID = p_USER_ID;
                mQC.TYPE_ID = p_TYPE_ID;
                DataTable dt = mQC.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public DataSet GetQCDataSet(long p_QC_MASTER_ID, long p_GRN_MASTER_ID, int p_USER_ID, int p_TYPE_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectQC_MASTER mQC = new spSelectQC_MASTER();
                mQC.Connection = mConnection;
                mQC.QC_MASTER_ID = p_QC_MASTER_ID;
                mQC.GRN_MASTER_ID = p_GRN_MASTER_ID;
                mQC.USER_ID = p_USER_ID;
                mQC.TYPE_ID = p_TYPE_ID;
                return mQC.ExecuteDataSet();
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        #endregion

        #region Purchase Invoice

        public DataTable GetPurchaseInvoice(int p_TYPE_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_INVOICE_MASTER mPur = new spSelectPURCHASE_INVOICE_MASTER();
                mPur.Connection = mConnection;
                mPur.TYPE_ID = p_TYPE_ID;                
                DataTable dt = mPur.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public DataSet GetPurchaseInvoice(int p_TYPE_ID, long p_PURCHASE_INVOICE_MASTER_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_INVOICE_MASTER mPur = new spSelectPURCHASE_INVOICE_MASTER();
                mPur.Connection = mConnection;
                mPur.TYPE_ID = p_TYPE_ID;
                mPur.PURCHASE_INVOICE_MASTER_ID = p_PURCHASE_INVOICE_MASTER_ID;
                DataSet ds = mPur.ExecuteDataSet();
                return ds;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        #endregion

        #region Purchase Invoice Return

        public DataTable GetPurchaseReturn(int p_TYPE_ID, int p_STATUS_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_RETURN_MASTER mRequisition = new spSelectPURCHASE_RETURN_MASTER();
                mRequisition.Connection = mConnection;
                mRequisition.TYPE_ID = p_TYPE_ID;
                mRequisition.STATUS_ID = p_STATUS_ID;
                DataTable dt = mRequisition.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public DataSet GetPurchaseReturn(int p_TYPE_ID, long p_PURCHASE_RETURN_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_RETURN_MASTER mRequisition = new spSelectPURCHASE_RETURN_MASTER();
                mRequisition.Connection = mConnection;
                mRequisition.TYPE_ID = p_TYPE_ID;
                mRequisition.PURCHASE_RETURN_ID = p_PURCHASE_RETURN_ID;
                return mRequisition.ExecuteDataSet();
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        #endregion

        #region Material Issue/Return

        public DataTable GetMaterial(int p_TYPE_ID, int p_TYPE_ID2)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectMATERIAL_MASTER mRequisition = new spSelectMATERIAL_MASTER();
                mRequisition.Connection = mConnection;
                mRequisition.TYPE_ID = p_TYPE_ID;
                mRequisition.TYPE_ID2 = p_TYPE_ID2;
                DataTable dt = mRequisition.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public DataSet GetMaterial(int p_TYPE_ID2, long p_MATERIAL_MASTER_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectMATERIAL_MASTER mRequisition = new spSelectMATERIAL_MASTER();
                mRequisition.Connection = mConnection;
                mRequisition.TYPE_ID2 = p_TYPE_ID2;
                mRequisition.MATERIAL_MASTER_ID = p_MATERIAL_MASTER_ID;
                return mRequisition.ExecuteDataSet();
                
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        #endregion

        #region Maintenance/Repair

        public DataTable GetMaintenance(int p_TYPE_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectREPAIR_MASTER mRequisition = new spSelectREPAIR_MASTER();
                mRequisition.Connection = mConnection;
                mRequisition.TYPE_ID = p_TYPE_ID;
                DataTable dt = mRequisition.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public DataSet GetMaintenance(int p_TYPE_ID, long p_REPAIR_MASTER_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectREPAIR_MASTER mRequisition = new spSelectREPAIR_MASTER();
                mRequisition.Connection = mConnection;
                mRequisition.TYPE_ID = p_TYPE_ID;
                mRequisition.REPAIR_MASTER_ID = p_REPAIR_MASTER_ID;
                return mRequisition.ExecuteDataSet();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        #endregion

        #endregion

        #region Insert, Update

        /// <summary>
        /// Inserts Purchase, TranferOut, Purchase Return, TranferIn And Damage Document
        /// </summary>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_ORDER_NUMBER">DocumentNo</param>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DOCUMENT_DATE">Date</param>
        /// <param name="p_SOLD_TO">SoldTo</param>
        /// <param name="p_SOLD_FROM">SoldFrom</param>
        /// <param name="p_TOTAL_AMOUNT">Amount</param>
        /// <param name="p_IS_DELETE">IsDeleted</param>
        /// <param name="dtPurchaseDetail">PurchaseDetailDatatable</param>
        /// <param name="p_Posting">Posting</param>
        /// <param name="p_BuiltyNo">Builty</param>
        /// <param name="p_UserId">InsertedBy</param>
        /// <param name="p_PrincipalId">Principal</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool InsertPurchaseDocument(int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_PrincipalId)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId;
                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = Convert.ToDateTime(dr["TIME_STAMP"]);
                    mPurchaseDetail.ExecuteQuery();

                    uspProcessSKUStock mSKUStock = new uspProcessSKUStock();
                    mSKUStock.Connection = mConnection;
                    mSKUStock.Transaction = mTransaction;
                    mSKUStock.SKU_ID = mPurchaseDetail.SKU_ID;
                    mSKUStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mSKUStock.BATCH_NO = mPurchaseDetail.BATCH_NO;
                    if (p_TYPE_ID == 2)
                    {
                        mSKUStock.UNITS_IN_STOCK = mPurchaseDetail.FREE_SKU;
                    }
                    else
                    {
                        mSKUStock.UNITS_IN_STOCK = mPurchaseDetail.QUANTITY;
                    }
                    mSKUStock.TYPE_ID = p_TYPE_ID;
                    mSKUStock.EXPIRY_DATE = p_DOCUMENT_DATE;
                    mSKUStock.ExecuteQuery();
                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        /// <summary>
        /// Inserts Purchase, TranferOut, Purchase Return, TranferIn And Damage Document
        /// </summary>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_ORDER_NUMBER">DocumentNo</param>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DOCUMENT_DATE">Date</param>
        /// <param name="p_SOLD_TO">SoldTo</param>
        /// <param name="p_SOLD_FROM">SoldFrom</param>
        /// <param name="p_TOTAL_AMOUNT">Amount</param>
        /// <param name="p_IS_DELETE">IsDeleted</param>
        /// <param name="dtPurchaseDetail">PurchaseDetailDatatable</param>
        /// <param name="p_Posting">Posting</param>
        /// <param name="p_BuiltyNo">Builty</param>
        /// <param name="p_UserId">InsertedBy</param>
        /// <param name="p_PrincipalId">Principal</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool InsertPurchaseDocument(int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_PrincipalId, long p_PO_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId;
                mPurchaseMaster.PO_ID = p_PO_ID;
                mPurchaseMaster.ExecuteQuery();
                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = decimal.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = decimal.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.DISCOUNT = decimal.Parse(dr["DISCOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    if (dr["TIME_STAMP"] != null)
                    {
                        mPurchaseDetail.TIME_STAMP = Convert.ToDateTime(dr["TIME_STAMP"]);
                    }
                    else
                    {
                        mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    }
                    mPurchaseDetail.ExecuteQuery();

                    if (p_TYPE_ID == 3)
                    {
                        uspProcessSKUStock mSKUStockReg = new uspProcessSKUStock();
                        mSKUStockReg.Connection = mConnection;
                        mSKUStockReg.Transaction = mTransaction;
                        mSKUStockReg.SKU_ID = mPurchaseDetail.SKU_ID;
                        mSKUStockReg.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                        mSKUStockReg.BATCH_NO = mPurchaseDetail.BATCH_NO;
                        mSKUStockReg.UNITS_IN_STOCK = mPurchaseDetail.QUANTITY;
                        mSKUStockReg.TYPE_ID = Constants.Document_Invoice;
                        mSKUStockReg.EXPIRY_DATE = p_DOCUMENT_DATE;
                        mSKUStockReg.ExecuteQuery();
                    }

                    uspProcessSKUStock mSKUStock = new uspProcessSKUStock();
                    mSKUStock.Connection = mConnection;
                    mSKUStock.Transaction = mTransaction;
                    mSKUStock.SKU_ID = mPurchaseDetail.SKU_ID;
                    mSKUStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mSKUStock.BATCH_NO = mPurchaseDetail.BATCH_NO;
                    if (p_TYPE_ID == 2)
                    {
                        mSKUStock.UNITS_IN_STOCK = mPurchaseDetail.FREE_SKU;
                    }
                    else
                    {
                        mSKUStock.UNITS_IN_STOCK = mPurchaseDetail.QUANTITY;
                    }
                    mSKUStock.TYPE_ID = p_TYPE_ID;
                    mSKUStock.EXPIRY_DATE = p_DOCUMENT_DATE;
                    mSKUStock.ExecuteQuery();
                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        
        public bool InsertPurchaseDocument(string DriverName, string vehicleNo, string reference, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_PrincipalId, long p_PO_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_MASTER mPurchaseMaster = new spInsertPURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.PRINCIPAL_ID = p_PrincipalId;
                mPurchaseMaster.PO_ID = p_PO_ID;
                mPurchaseMaster.ExecuteQuery();


                spInsert_Deliver_challan2 ch2 = new spInsert_Deliver_challan2();
                ch2.Connection = mConnection;
                ch2.Transaction = mTransaction;
                ch2.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                ch2.DriverName = DriverName;
                ch2.REFERENCE = reference;
                ch2.VEHICLENO = vehicleNo;
                ch2.ExecuteQuery();

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = decimal.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = int.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.DISCOUNT = decimal.Parse(dr["DISCOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    if (dr["TIME_STAMP"] != null)
                    {
                        mPurchaseDetail.TIME_STAMP = Convert.ToDateTime(dr["TIME_STAMP"]);
                    }
                    else
                    {
                        mPurchaseDetail.TIME_STAMP = p_DOCUMENT_DATE;
                    }
                    mPurchaseDetail.ExecuteQuery();

                    if (p_TYPE_ID == 3)
                    {
                        uspProcessSKUStock mSKUStockReg = new uspProcessSKUStock();
                        mSKUStockReg.Connection = mConnection;
                        mSKUStockReg.Transaction = mTransaction;
                        mSKUStockReg.SKU_ID = mPurchaseDetail.SKU_ID;
                        mSKUStockReg.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                        mSKUStockReg.BATCH_NO = mPurchaseDetail.BATCH_NO;
                        mSKUStockReg.UNITS_IN_STOCK = mPurchaseDetail.QUANTITY;
                        mSKUStockReg.TYPE_ID = Constants.Document_Invoice;
                        mSKUStockReg.EXPIRY_DATE = p_DOCUMENT_DATE;
                        mSKUStockReg.ExecuteQuery();
                    }

                    uspProcessSKUStock mSKUStock = new uspProcessSKUStock();
                    mSKUStock.Connection = mConnection;
                    mSKUStock.Transaction = mTransaction;
                    mSKUStock.SKU_ID = mPurchaseDetail.SKU_ID;
                    mSKUStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mSKUStock.BATCH_NO = mPurchaseDetail.BATCH_NO;
                    if (p_TYPE_ID == 2)
                    {
                        mSKUStock.UNITS_IN_STOCK = mPurchaseDetail.FREE_SKU;
                    }
                    else
                    {
                        mSKUStock.UNITS_IN_STOCK = mPurchaseDetail.QUANTITY;
                    }
                    mSKUStock.TYPE_ID = p_TYPE_ID;
                    mSKUStock.EXPIRY_DATE = p_DOCUMENT_DATE;
                    mSKUStock.ExecuteQuery();
                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        /// <summary>
        /// Updates Purchase, TranferOut, Purchase Return, TranferIn And Damage Document
        /// </summary>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_ORDER_NUMBER">DocumentNo</param>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DOCUMENT_DATE">Date</param>
        /// <param name="p_SOLD_TO">SoldTo</param>
        /// <param name="p_SOLD_FROM">SoldFrom</param>
        /// <param name="p_TOTAL_AMOUNT">Amount</param>
        /// <param name="p_IS_DELETE">IsDeleted</param>
        /// <param name="dtPurchaseDetail">PurchaseDetailDatatable</param>
        /// <param name="p_Posting">Posting</param>
        /// <param name="p_BuiltyNo">Builty</param>
        /// <param name="p_UserId">InsertedBy</param>
        /// <param name="p_Principal">Principal</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool UpdatePurchaseDocument(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_Principal)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.ExecuteQuery();

                //Get Privouse Update Purchase Detail and Rollback
                //LedgerController LController = new LedgerController();

                //string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID);

                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID, mConnection, mTransaction);

                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStock mPurchaseStock = new UspUpdatePurchaseDetailStock();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                    mPurchaseStock.BATCH_NO = dr["BATCH_NO"].ToString().Trim();
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.ExecuteQuery();
                }

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {

                    //update stock;
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;

                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = decimal.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = decimal.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.DISCOUNT = decimal.Parse(dr["DISCOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = Convert.ToDateTime(dr["TIME_STAMP"]);                                        
                    mPurchaseDetail.ExecuteQuery();

                    uspProcessSKUStock mSKUStock = new uspProcessSKUStock();
                    mSKUStock.Connection = mConnection;
                    mSKUStock.Transaction = mTransaction;
                    mSKUStock.SKU_ID = mPurchaseDetail.SKU_ID;
                    mSKUStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mSKUStock.BATCH_NO = mPurchaseDetail.BATCH_NO;
                    if (p_TYPE_ID == 2)
                    {
                        mSKUStock.UNITS_IN_STOCK = mPurchaseDetail.FREE_SKU;
                    }
                    else
                    {
                        mSKUStock.UNITS_IN_STOCK = mPurchaseDetail.QUANTITY;
                    }
                    mSKUStock.TYPE_ID = p_TYPE_ID;
                    mSKUStock.EXPIRY_DATE = p_DOCUMENT_DATE;
                    mSKUStock.ExecuteQuery();
                   
                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        
        public bool UpdatePurchaseDocument(string driverName, string VehicleNo, string reference,long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, string p_ORDER_NUMBER, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_SOLD_FROM, decimal p_TOTAL_AMOUNT, bool p_IS_DELETE, DataTable dtPurchaseDetail, int p_Posting, string p_BuiltyNo, int p_UserId, int p_Principal)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.ORDER_NUMBER = p_ORDER_NUMBER;
                mPurchaseMaster.SOLD_FROM = p_SOLD_FROM;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_UserId;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.BUILTY_NO = p_BuiltyNo;
                mPurchaseMaster.ExecuteQuery();



                spUpdate_Delivery_Challan upd = new spUpdate_Delivery_Challan();
                upd.Connection = mConnection;
                upd.Transaction = mTransaction;
                upd.DriverName = driverName;
                upd.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                upd.REFERENCE = reference;
                upd.VEHICLENO = VehicleNo;
                upd.ExecuteQuery();


                //Get Privouse Update Purchase Detail and Rollback
                //LedgerController LController = new LedgerController();

                //string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID);

                DataTable dt = SelectPrivousePurchaseDetail(p_DISTRIBUTOR_ID, p_PURCHASE_MASTER_ID, mConnection, mTransaction);

                foreach (DataRow dr in dt.Rows)
                {
                    UspUpdatePurchaseDetailStock mPurchaseStock = new UspUpdatePurchaseDetailStock();
                    mPurchaseStock.Connection = mConnection;
                    mPurchaseStock.Transaction = mTransaction;
                    mPurchaseStock.TYPEID = p_TYPE_ID;
                    mPurchaseStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseStock.PURCHASE_DETAIL_ID = long.Parse(dr["PURCHASE_DETAIL_ID"].ToString());
                    mPurchaseStock.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                    mPurchaseStock.BATCH_NO = dr["BATCH_NO"].ToString().Trim();
                    mPurchaseStock.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseStock.ExecuteQuery();
                }

                spInsertPURCHASE_DETAIL mPurchaseDetail = new spInsertPURCHASE_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {

                    //update stock;
                    mPurchaseDetail.PURCHASE_MASTER_ID = mPurchaseMaster.PURCHASE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;

                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.BATCH_NO = dr["BATCH_NO"].ToString();
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.QUANTITY = decimal.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.FREE_SKU = decimal.Parse(dr["FREE_SKU"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.DISCOUNT = decimal.Parse(dr["DISCOUNT"].ToString());
                    mPurchaseDetail.TYPE_ID = mPurchaseMaster.TYPE_ID;
                    mPurchaseDetail.TIME_STAMP = Convert.ToDateTime(dr["TIME_STAMP"]);
                    mPurchaseDetail.ExecuteQuery();

                    uspProcessSKUStock mSKUStock = new uspProcessSKUStock();
                    mSKUStock.Connection = mConnection;
                    mSKUStock.Transaction = mTransaction;
                    mSKUStock.SKU_ID = mPurchaseDetail.SKU_ID;
                    mSKUStock.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mSKUStock.BATCH_NO = mPurchaseDetail.BATCH_NO;
                    if (p_TYPE_ID == 2)
                    {
                        mSKUStock.UNITS_IN_STOCK = mPurchaseDetail.FREE_SKU;
                    }
                    else
                    {
                        mSKUStock.UNITS_IN_STOCK = mPurchaseDetail.QUANTITY;
                    }
                    mSKUStock.TYPE_ID = p_TYPE_ID;
                    mSKUStock.EXPIRY_DATE = p_DOCUMENT_DATE;
                    mSKUStock.ExecuteQuery();

                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        /// <summary>
        /// Posts Pending Purchase, TranferOut, Purchase Return, TranferIn And Damage Document
        /// </summary>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="p_Type_Id">Type</param>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_Posting">Posting</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool PostPendingDocument(long p_PURCHASE_MASTER_ID,int p_Type_Id,int p_Distributor_Id,int p_Posting)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdatePURCHASE_MASTER mPurchaseMaster = new spUpdatePURCHASE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_Distributor_Id;
                mPurchaseMaster.TYPE_ID = p_Type_Id;  
                mPurchaseMaster.POSTING = p_Posting;
                mPurchaseMaster.ExecuteQuery();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool UpdateREQUISITION_VIEW(long p_REQUISITION_VIEW_ID ,long p_REQUISITION_MASTER_ID ,int p_ALLOCATED_USER_ID ,int  p_PRIORITY_ID ,int p_USER_ID ,bool p_IS_DELETED )
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateREQUISITION_VIEW mPurchaseMaster = new spUpdateREQUISITION_VIEW();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.ALLOCATED_USER_ID = p_ALLOCATED_USER_ID;
                mPurchaseMaster.PRIORITY_ID = p_PRIORITY_ID;
                mPurchaseMaster.IS_DELETED = p_IS_DELETED;
                mPurchaseMaster.REQUISITION_VIEW_ID = p_REQUISITION_VIEW_ID;
                mPurchaseMaster.USER_ID = p_USER_ID;
                mPurchaseMaster.REQUISITION_MASTER_ID = p_REQUISITION_MASTER_ID;
                mPurchaseMaster.ExecuteQuery();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }
        
        #region Material Issuance

        public bool InsertMaterailIssuance(int p_CATEGORY_ID,int p_DISTRIBUTOR_ID, DateTime p_DOCUMENT_DATE, int p_USER_ID, DataTable dtIssuanceDetail)
        {

            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;
            DataTable dtRawMaterial = new DataTable();
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                uspInsertMaterialIssaunceMaster mIssuanceMaster = new uspInsertMaterialIssaunceMaster();
                mIssuanceMaster.Connection = mConnection;
                mIssuanceMaster.Transaction = mTransaction;

                //------------Insert Material Issuance Master----------

                if (dtIssuanceDetail.Rows.Count > 0)
                {
                    mIssuanceMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mIssuanceMaster.CATEGORY_ID = p_CATEGORY_ID;
                    mIssuanceMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                    mIssuanceMaster.USER_ID = p_USER_ID;

                    mIssuanceMaster.ExecuteQuery();

                    //----------------Insert Material Issuance Detail-------------
                    uspInsertMaterialIssuanceDetail mIssuanceDetail = new uspInsertMaterialIssuanceDetail();
                    mIssuanceDetail.Connection = mConnection;
                    mIssuanceDetail.Transaction = mTransaction;

                    foreach (DataRow dr in dtIssuanceDetail.Rows)
                    {
                        mIssuanceDetail.MATERIAL_ISSUANCE_MASTER_ID = mIssuanceMaster.MATERIAL_ISSUANCE_MASTER_ID;
                        mIssuanceDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                        mIssuanceDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                        mIssuanceDetail.ExecuteQuery();

                        if (p_CATEGORY_ID == Constants.FinishedGoods)
                        {
                            uspGetFinishedDetail mFinished = new uspGetFinishedDetail();
                            mFinished.Connection = mConnection;
                            mFinished.Transaction = mTransaction;
                            mFinished.FINISHED_SKU_ID = mIssuanceDetail.SKU_ID;
                            dtRawMaterial = mFinished.ExecuteTable();
                            foreach (DataRow drRaw in dtRawMaterial.Rows)
                            {
                                UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                                mStockUpdate.Connection = mConnection;
                                mStockUpdate.Transaction = mTransaction;
                                mStockUpdate.TYPE_ID = 14;
                                mStockUpdate.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                                mStockUpdate.STOCK_DATE = p_DOCUMENT_DATE;
                                mStockUpdate.SKU_ID = Convert.ToInt32(drRaw["SKU_ID"]);
                                mStockUpdate.BATCHNO = "";
                                mStockUpdate.STOCK_QTY = Convert.ToInt32(drRaw["QUANTITY"]) * mIssuanceDetail.QUANTITY;
                                mStockUpdate.FREE_QTY = 0;
                                mStockUpdate.ExecuteQuery();
                            }
                        }
                        else
                        {
                            UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                            mStockUpdate.Connection = mConnection;
                            mStockUpdate.Transaction = mTransaction;
                            mStockUpdate.TYPE_ID = 14;
                            mStockUpdate.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                            mStockUpdate.STOCK_DATE = p_DOCUMENT_DATE;
                            mStockUpdate.SKU_ID = mIssuanceDetail.SKU_ID;
                            mStockUpdate.BATCHNO = "";
                            mStockUpdate.STOCK_QTY = mIssuanceDetail.QUANTITY;
                            mStockUpdate.FREE_QTY = 0;
                            mStockUpdate.ExecuteQuery();
                        }

                    }
                    mTransaction.Commit();
                    return true;
                }
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;// exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return true;
        }

        public bool UpdateMaterailIssuance(long p_MATERIAL_ISSUANCE_MASTER_ID,int p_DISTRIBUTOR_ID, int p_CATEGORY_ID, DateTime p_DOCUMENT_DATE, int p_USER_ID, DataTable dtIssuanceDetail)
        {

            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;
            DataTable dtRawMaterial = new DataTable();
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                uspUpdateMaterialIssaunceMaster mIssuanceMaster = new uspUpdateMaterialIssaunceMaster();
                mIssuanceMaster.Connection = mConnection;
                mIssuanceMaster.Transaction = mTransaction;

                //------------Insert Material Issuance Master----------

                if (dtIssuanceDetail.Rows.Count > 0)
                {
                    mIssuanceMaster.MATERIAL_ISSUANCE_MASTER_ID = p_MATERIAL_ISSUANCE_MASTER_ID;
                    mIssuanceMaster.USER_ID = p_USER_ID;

                    mIssuanceMaster.ExecuteQuery();

                    //----------------Insert Material Issuance Detail-------------
                    uspDeleteMaterialIssuanceDetail mIssuanceDetailDel = new uspDeleteMaterialIssuanceDetail();
                    mIssuanceDetailDel.Connection = mConnection;
                    mIssuanceDetailDel.Transaction = mTransaction;

                    mIssuanceDetailDel.MATERIAL_ISSUANCE_MASTER_ID = p_MATERIAL_ISSUANCE_MASTER_ID;
                    mIssuanceDetailDel.CATEGORY_ID = p_CATEGORY_ID;
                    mIssuanceDetailDel.DOCUMENT_DATE = p_DOCUMENT_DATE;
                    mIssuanceDetailDel.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mIssuanceDetailDel.ExecuteQuery();


                    //----------------Insert Material Issuance Detail-------------
                    uspInsertMaterialIssuanceDetail mIssuanceDetail = new uspInsertMaterialIssuanceDetail();
                    mIssuanceDetail.Connection = mConnection;
                    mIssuanceDetail.Transaction = mTransaction;

                    foreach (DataRow dr in dtIssuanceDetail.Rows)
                    {
                        mIssuanceDetail.MATERIAL_ISSUANCE_MASTER_ID = mIssuanceMaster.MATERIAL_ISSUANCE_MASTER_ID;
                        mIssuanceDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                        mIssuanceDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                        mIssuanceDetail.ExecuteQuery();


                        if (p_CATEGORY_ID == Constants.FinishedGoods)
                        {
                            uspGetFinishedDetail mFinished = new uspGetFinishedDetail();
                            mFinished.Connection = mConnection;
                            mFinished.Transaction = mTransaction;
                            mFinished.FINISHED_SKU_ID = mIssuanceDetail.SKU_ID;
                            dtRawMaterial = mFinished.ExecuteTable();
                            foreach (DataRow drRaw in dtRawMaterial.Rows)
                            {
                                UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                                mStockUpdate.Connection = mConnection;
                                mStockUpdate.Transaction = mTransaction;
                                mStockUpdate.TYPE_ID = 14;
                                mStockUpdate.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                                mStockUpdate.STOCK_DATE = p_DOCUMENT_DATE;
                                mStockUpdate.SKU_ID = Convert.ToInt32(drRaw["SKU_ID"]);
                                mStockUpdate.BATCHNO = "";
                                mStockUpdate.STOCK_QTY = Convert.ToInt32(drRaw["QUANTITY"]) * mIssuanceDetail.QUANTITY;
                                mStockUpdate.FREE_QTY = 0;
                                mStockUpdate.ExecuteQuery();
                            }
                        }
                        else
                        {
                            UspProcessStockRegister mStockUpdate = new UspProcessStockRegister();
                            mStockUpdate.Connection = mConnection;
                            mStockUpdate.Transaction = mTransaction;
                            mStockUpdate.TYPE_ID = 14;
                            mStockUpdate.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                            mStockUpdate.STOCK_DATE = p_DOCUMENT_DATE;
                            mStockUpdate.SKU_ID = mIssuanceDetail.SKU_ID;
                            mStockUpdate.BATCHNO = "";
                            mStockUpdate.STOCK_QTY = mIssuanceDetail.QUANTITY;
                            mStockUpdate.FREE_QTY = 0;
                            mStockUpdate.ExecuteQuery();
                        }


                    }
                    mTransaction.Commit();
                    return true;
                }
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;// exp.Message;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
            return true;
        }

        #endregion

        #region Purchase Order

        /// <summary>
        /// Inserts Purchase, TranferOut, Purchase Return, TranferIn And Damage Document
        /// </summary>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_ORDER_NUMBER">DocumentNo</param>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DOCUMENT_DATE">Date</param>
        /// <param name="p_SOLD_TO">SoldTo</param>
        /// <param name="p_SOLD_FROM">SoldFrom</param>
        /// <param name="p_TOTAL_AMOUNT">Amount</param>
        /// <param name="p_IS_DELETE">IsDeleted</param>
        /// <param name="dtPurchaseDetail">PurchaseDetailDatatable</param>
        /// <param name="p_Posting">Posting</param>
        /// <param name="p_BuiltyNo">Builty</param>
        /// <param name="p_UserId">InsertedBy</param>
        /// <param name="p_PrincipalId">Principal</param>
        /// <returns>True On Success And False On Failure</returns>
        public bool InsertPurchaseOrder(int p_DISTRIBUTOR_ID, int p_PRINCIPAL_ID, string p_REF_NO, int p_TYPE_ID, int p_CURRENCY_ID, decimal p_EXCHANGE_RATE, DateTime p_DOCUMENT_DATE, decimal p_TOTAL_AMOUNT, int p_USER_ID,
            DataTable dtPurchaseDetail)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_ORDER mPurchaseMaster = new spInsertPURCHASE_ORDER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mPurchaseMaster.REF_NO = p_REF_NO;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.CURRENCY_ID = p_CURRENCY_ID;
                mPurchaseMaster.TOTAL_AMOUNT = p_EXCHANGE_RATE;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.USER_ID = p_USER_ID;
                mPurchaseMaster.TIME_STAMP = DateTime.Now;
                mPurchaseMaster.LAST_UPDATE = DateTime.Now;

                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_ORDER_DETAIL mPurchaseDetail = new spInsertPURCHASE_ORDER_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_ORDER_ID = mPurchaseMaster.PURCHASE_ORDER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.QUANTITY = decimal.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TIME_STAMP = DateTime.Now;
                    mPurchaseDetail.ExecuteQuery();
                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool UpdatePurchaseOrderDetail(long p_PURCHASE_ORDER_ID, int p_DISTRIBUTOR_ID, string p_REF_NO, int p_CURRENCY_ID, decimal p_TOTAL_AMOUNT, decimal p_EXCHANGE_RATE, int p_USER_ID, DataTable dtPurchaseDetail)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spDeletePURCHASE_ORDER_DETAIL mDelete = new spDeletePURCHASE_ORDER_DETAIL();
                mDelete.Connection = mConnection;
                mDelete.PURCHASE_ORDER_ID = p_PURCHASE_ORDER_ID;
                mDelete.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mDelete.ExecuteQuery();

                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdatePURCHASE_ORDER mPurchaseMaster = new spUpdatePURCHASE_ORDER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_ORDER_ID = p_PURCHASE_ORDER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.CURRENCY_ID = p_CURRENCY_ID;
                mPurchaseMaster.REF_NO = p_REF_NO;
                mPurchaseMaster.TOTAL_AMOUNT = p_TOTAL_AMOUNT;
                mPurchaseMaster.EXCHANGE_RATE = p_EXCHANGE_RATE;
                mPurchaseMaster.USER_ID = p_USER_ID;
                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_ORDER_DETAIL mPurchaseDetail = new spInsertPURCHASE_ORDER_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {

                    mPurchaseDetail.PURCHASE_ORDER_ID = mPurchaseMaster.PURCHASE_ORDER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.QUANTITY = decimal.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.TIME_STAMP = DateTime.Now;
                    mPurchaseDetail.ExecuteQuery();
                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool UpdatePurchaseOrder(long p_PURCHASE_ORDER_ID, int p_TYPE_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdatePURCHASE_ORDER mPurchaseMaster = new spUpdatePURCHASE_ORDER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_ORDER_ID = p_PURCHASE_ORDER_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;                
                mPurchaseMaster.ExecuteQuery();
                
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        /// <summary>
        /// Gets Purchase Order Document No
        /// </summary>
        /// <remarks>
        /// Returns Purchase Order Document No as Datatable
        /// </remarks>
        /// <param name="p_TYPE_ID">Type</param>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <param name="p_User_Id">InsertedBy</param>
        /// <param name="p_Posting">Posting</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage  Document No as Datatable</returns>
        public DataTable SelectPurchaseOrderDocumentNo(long p_PURCHASE_ORDER_ID, int p_DISTRIBUTOR_ID, int p_TYPE_ID, int p_PRINCIPAL_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_ORDER mPurchaseMaster = new spSelectPURCHASE_ORDER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.PURCHASE_ORDER_ID = p_PURCHASE_ORDER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.PRINCIPAL_ID = p_PRINCIPAL_ID;
                DataTable dt = mPurchaseMaster.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public DataTable SelectPurchaseOrderDocumentNo(long p_PURCHASE_ORDER_ID, int p_DISTRIBUTOR_ID, int p_TYPE_ID, DateTime p_DOCUMENT_DATE)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_ORDER mPurchaseMaster = new spSelectPURCHASE_ORDER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.PURCHASE_ORDER_ID = p_PURCHASE_ORDER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                DataTable dt = mPurchaseMaster.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        /// <summary>
        /// Get Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable
        /// </remarks>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable</returns>
        public DataTable SelectPurchaseOrderDetail(long p_PURCHASE_ORDER_ID, int p_DISTRIBUTOR_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_ORDER_DETAIL mPurchaseDetail = new spSelectPURCHASE_ORDER_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.PURCHASE_ORDER_ID = p_PURCHASE_ORDER_ID;
                mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                DataTable dt = mPurchaseDetail.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        /// <summary>
        /// Get Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail
        /// </summary>
        /// <remarks>
        /// Returns Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable
        /// </remarks>
        /// <param name="p_DISTRIBUTOR_ID">Location</param>
        /// <param name="p_PURCHASE_MASTER_ID">Purchase</param>
        /// <returns>Purchase, TranferOut, Purchase Return, TranferIn And Damage Detail as Datatable</returns>
        public DataTable SelectPurchaseOrderDetail2(long p_PURCHASE_ORDER_ID, int p_DISTRIBUTOR_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_ORDER_DETAIL2 mPurchaseDetail = new spSelectPURCHASE_ORDER_DETAIL2();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.PURCHASE_ORDER_ID = p_PURCHASE_ORDER_ID;
                mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                DataTable dt = mPurchaseDetail.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public DataTable SelectPurchaseDocumentNo(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_USER_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_MASTER2 mPurchaseMaster = new spSelectPURCHASE_MASTER2();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.USER_ID = p_USER_ID;
                DataTable dt = mPurchaseMaster.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public DataTable SelectPurchaseDocumentNo2(long p_PURCHASE_MASTER_ID, int p_DISTRIBUTOR_ID, int p_TYPE_ID, DateTime p_DOCUMENT_DATE, int p_SOLD_TO, int p_USER_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_MASTER3 mPurchaseMaster = new spSelectPURCHASE_MASTER3();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.PURCHASE_MASTER_ID = p_PURCHASE_MASTER_ID;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.TYPE_ID = p_TYPE_ID;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.SOLD_TO = p_SOLD_TO;
                mPurchaseMaster.USER_ID = p_USER_ID;
                DataTable dt = mPurchaseMaster.ExecuteTable();
                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return null;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        #endregion

        #region Purchase Invoice

        public bool InsertPurchaseInvoice(long p_GRN_NO, string p_REF_NO, int p_DISTRIBUTOR_ID, DateTime p_DOCUMENT_DATE, int p_PRINCIPAL_ID, int p_USER_ID, decimal p_NET_AMOUNT,int p_ACCOUNT_HEAD_ID, decimal p_EXPENSE_AMOUNT, string p_REMARKS, DataTable dtPurchaseDetail, DataTable dtGRNIDs)
        {
            try
            {
                decimal p_Gross = 0;
                decimal p_GST = 0;
                decimal p_Discount = 0;
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                uspInsertPurchaseInvoiceMaster mPurchaseMaster = new uspInsertPurchaseInvoiceMaster();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.GRN_NO = p_GRN_NO;
                mPurchaseMaster.REF_NO = p_REF_NO;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mPurchaseMaster.NET_AMOUNT = p_NET_AMOUNT;
                mPurchaseMaster.EXPENSE_AMOUNT = p_EXPENSE_AMOUNT;
                mPurchaseMaster.ACCOUNT_HEAD_ID = p_ACCOUNT_HEAD_ID;
                mPurchaseMaster.REMARKS = p_REMARKS;
                mPurchaseMaster.USER_ID = p_USER_ID;
                mPurchaseMaster.ExecuteQuery();

                uspInsertPurchaseInvoiceDetail mPurchaseDetail = new uspInsertPurchaseInvoiceDetail();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_INVOICE_MASTER_ID = mPurchaseMaster.PURCHASE_INVOICE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.QUANTITY = decimal.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.DISCOUNT = decimal.Parse(dr["DISCOUNT"].ToString());
                    mPurchaseDetail.GST = decimal.Parse(dr["GST"].ToString());                    
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    p_Gross += decimal.Parse(dr["AMOUNT"].ToString());
                    p_Discount += decimal.Parse(dr["DISCOUNT"].ToString());
                    p_GST += ((p_Gross - p_Discount) * decimal.Parse(dr["GST"].ToString()) / 100);                    
                    
                    mPurchaseDetail.ExecuteQuery();
                }

                uspInsertPurchaseInvoiceGRN mPurchaseGRN = new uspInsertPurchaseInvoiceGRN();
                mPurchaseGRN.Connection = mConnection;
                mPurchaseGRN.Transaction = mTransaction;

                foreach (DataRow dr in dtGRNIDs.Rows)
                {
                    mPurchaseGRN.PURCHASE_INVOICE_MASTER_ID = p_GRN_NO;
                    mPurchaseGRN.GRN_ID = long.Parse(dr["GRN_ID"].ToString());
                    mPurchaseGRN.ExecuteQuery();
                }

                AccountHeadController AHCtl = new AccountHeadController();
                DataTable dtAHDetail = AHCtl.GetAccountHeadAssignmentDetail(1);//FORM_ID = 0 for Purchase Invoice

                #region Account Posting

                LedgerController LController = new LedgerController();
                Configuration.GetAccountHead();
                DistributorController Dcontroller = new DistributorController();


                string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID);

                if (dtAHDetail.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtAHDetail.Rows)
                    {
                        if (dr["FIELD_ID"].ToString() == "0" && p_Gross > 0)//FIELD_ID = 0 for Gross Sale
                        {
                            LController.PostingInvoiceAccount_Principal(Constants.Journal_Voucher, long.Parse(VoucherNo), Convert.ToInt64(dr["ACCOUNT_HEAD_ID2"]), p_DISTRIBUTOR_ID, p_Gross, 0, p_DOCUMENT_DATE, "Purchase Gross Value", DateTime.Now, p_PRINCIPAL_ID, 0, mPurchaseMaster.PURCHASE_INVOICE_MASTER_ID, "", Constants.Document_Purchase, p_USER_ID, mTransaction, mConnection, Constants.CashPayment, "");
                        }

                        if (dr["FIELD_ID"].ToString() == "1" && p_Discount > 0) //FIELD_ID = 1 for Discount
                        {
                            LController.PostingInvoiceAccount_Principal(Constants.Journal_Voucher, long.Parse(VoucherNo), Convert.ToInt64(dr["ACCOUNT_HEAD_ID2"]), p_DISTRIBUTOR_ID, 0, p_Discount, p_DOCUMENT_DATE, "Purchase Discount Value", DateTime.Now, p_PRINCIPAL_ID, 0, mPurchaseMaster.PURCHASE_INVOICE_MASTER_ID, "", Constants.Document_Purchase, p_USER_ID, mTransaction, mConnection, Constants.CashPayment, "");
                        }

                        if (dr["FIELD_ID"].ToString() == "2" && p_GST > 0) //FIELD_ID = 2 for GST Amount
                        {
                            LController.PostingInvoiceAccount_Principal(Constants.Journal_Voucher, long.Parse(VoucherNo), Convert.ToInt64(dr["ACCOUNT_HEAD_ID2"]), p_DISTRIBUTOR_ID, p_GST, 0, p_DOCUMENT_DATE, "Purchase GST Value", DateTime.Now, p_PRINCIPAL_ID, 0, mPurchaseMaster.PURCHASE_INVOICE_MASTER_ID, "", Constants.Document_Purchase, p_USER_ID, mTransaction, mConnection, Constants.CashPayment, "");
                        }
                    }

                    if (p_NET_AMOUNT > 0)
                    {
                        LController.PostingInvoiceAccount_Principal(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(dtAHDetail.Rows[0]["ACCOUNT_HEAD_ID"].ToString()), p_DISTRIBUTOR_ID, 0, p_NET_AMOUNT, p_DOCUMENT_DATE, "Purchase Net Value", DateTime.Now, p_PRINCIPAL_ID, 0, mPurchaseMaster.PURCHASE_INVOICE_MASTER_ID, "", Constants.Document_Purchase, p_USER_ID, mTransaction, mConnection, Constants.CashPayment, "");
                    }
                }
              
                #endregion               

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool InsertPurchaseInvoice(long p_GRN_NO, string p_REF_NO, int p_DISTRIBUTOR_ID, DateTime p_DOCUMENT_DATE, int p_PRINCIPAL_ID, int p_USER_ID, decimal p_NET_AMOUNT, string p_REMARKS, DataTable dtPurchaseDetail, DataTable dtGRNIDs)
        {
            try
            {
                decimal p_Gross = 0;
                decimal p_GST = 0;
                decimal p_Discount = 0;
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                uspInsertPurchaseInvoiceMaster mPurchaseMaster = new uspInsertPurchaseInvoiceMaster();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.GRN_NO = p_GRN_NO;
                mPurchaseMaster.REF_NO = p_REF_NO;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mPurchaseMaster.NET_AMOUNT = p_NET_AMOUNT;
                mPurchaseMaster.EXPENSE_AMOUNT = 0;
                mPurchaseMaster.ACCOUNT_HEAD_ID = 0;
                mPurchaseMaster.REMARKS = p_REMARKS;
                mPurchaseMaster.USER_ID = p_USER_ID;
                mPurchaseMaster.ExecuteQuery();

                uspInsertPurchaseInvoiceDetail mPurchaseDetail = new uspInsertPurchaseInvoiceDetail();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_INVOICE_MASTER_ID = mPurchaseMaster.PURCHASE_INVOICE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.QUANTITY = decimal.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.DISCOUNT = decimal.Parse(dr["DISCOUNT"].ToString());
                    mPurchaseDetail.GST = decimal.Parse(dr["GST"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    p_Gross += decimal.Parse(dr["AMOUNT"].ToString());
                    p_Discount += decimal.Parse(dr["DISCOUNT"].ToString());
                    p_GST += ((p_Gross - p_Discount) * decimal.Parse(dr["GST"].ToString()) / 100);

                    mPurchaseDetail.ExecuteQuery();
                }

                uspInsertPurchaseInvoiceGRN mPurchaseGRN = new uspInsertPurchaseInvoiceGRN();
                mPurchaseGRN.Connection = mConnection;
                mPurchaseGRN.Transaction = mTransaction;

                foreach (DataRow dr in dtGRNIDs.Rows)
                {
                    mPurchaseGRN.PURCHASE_INVOICE_MASTER_ID = mPurchaseMaster.PURCHASE_INVOICE_MASTER_ID;
                    mPurchaseGRN.GRN_ID = long.Parse(dr["GRN_ID"].ToString());
                    mPurchaseGRN.ExecuteQuery();
                }

                AccountHeadController AHCtl = new AccountHeadController();
                DataTable dtAHDetail = AHCtl.GetAccountHeadAssignmentDetail(1);//FORM_ID = 0 for Purchase Invoice

                #region Account Posting

                LedgerController LController = new LedgerController();
                Configuration.GetAccountHead();
                DistributorController Dcontroller = new DistributorController();


                string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID);

                if (dtAHDetail.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtAHDetail.Rows)
                    {
                        if (dr["FIELD_ID"].ToString() == "0" && p_Gross > 0)//FIELD_ID = 0 for Gross Sale
                        {
                            LController.PostingInvoiceAccount_Principal(Constants.Journal_Voucher, long.Parse(VoucherNo), Convert.ToInt64(dr["ACCOUNT_HEAD_ID2"]), p_DISTRIBUTOR_ID, p_Gross, 0, p_DOCUMENT_DATE, "Purchase Gross Value", DateTime.Now, p_PRINCIPAL_ID, 0, mPurchaseMaster.PURCHASE_INVOICE_MASTER_ID, "", Constants.Document_Purchase, p_USER_ID, mTransaction, mConnection, Constants.CashPayment, "");
                        }

                        if (dr["FIELD_ID"].ToString() == "1" && p_Discount > 0) //FIELD_ID = 1 for Discount
                        {
                            LController.PostingInvoiceAccount_Principal(Constants.Journal_Voucher, long.Parse(VoucherNo), Convert.ToInt64(dr["ACCOUNT_HEAD_ID2"]), p_DISTRIBUTOR_ID, 0, p_Discount, p_DOCUMENT_DATE, "Purchase Discount Value", DateTime.Now, p_PRINCIPAL_ID, 0, mPurchaseMaster.PURCHASE_INVOICE_MASTER_ID, "", Constants.Document_Purchase, p_USER_ID, mTransaction, mConnection, Constants.CashPayment, "");
                        }

                        if (dr["FIELD_ID"].ToString() == "2" && p_GST > 0) //FIELD_ID = 2 for GST Amount
                        {
                            LController.PostingInvoiceAccount_Principal(Constants.Journal_Voucher, long.Parse(VoucherNo), Convert.ToInt64(dr["ACCOUNT_HEAD_ID2"]), p_DISTRIBUTOR_ID, p_GST, 0, p_DOCUMENT_DATE, "Purchase GST Value", DateTime.Now, p_PRINCIPAL_ID, 0, mPurchaseMaster.PURCHASE_INVOICE_MASTER_ID, "", Constants.Document_Purchase, p_USER_ID, mTransaction, mConnection, Constants.CashPayment, "");
                        }
                    }

                    if (p_NET_AMOUNT > 0)
                    {
                        LController.PostingInvoiceAccount_Principal(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(dtAHDetail.Rows[0]["ACCOUNT_HEAD_ID"].ToString()), p_DISTRIBUTOR_ID, 0, p_NET_AMOUNT, p_DOCUMENT_DATE, "Purchase Net Value", DateTime.Now, p_PRINCIPAL_ID, 0, mPurchaseMaster.PURCHASE_INVOICE_MASTER_ID, "", Constants.Document_Purchase, p_USER_ID, mTransaction, mConnection, Constants.CashPayment, "");
                    }
                }

                #endregion                

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public bool UpdatePurchaseInvoice(long p_PURCHASE_INVOICE_MASTER_ID, DateTime p_DOCUMENT_DATE, long p_GRN_NO, string p_REF_NO, int p_DISTRIBUTOR_ID, int p_PRINCIPAL_ID, int p_USER_ID, decimal p_NET_AMOUNT, string p_REMARKS, bool p_IS_ACTIVE, DataTable dtPurchaseDetail, DataTable dtGRNIDs)
        {
            try
            {
                decimal p_Gross = 0;
                decimal p_GST = 0;
                decimal p_Discount = 0;
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdatePurchase_INVOICE_MASTER mPurchaseMaster = new spUpdatePurchase_INVOICE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_INVOICE_MASTER_ID = p_PURCHASE_INVOICE_MASTER_ID;
                mPurchaseMaster.GRN_NO = p_GRN_NO;
                mPurchaseMaster.REF_NO = p_REF_NO;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mPurchaseMaster.NET_AMOUNT = p_NET_AMOUNT;
                mPurchaseMaster.EXPENSE_AMOUNT = 0;
                mPurchaseMaster.ACCOUNT_HEAD_ID = 0;
                mPurchaseMaster.REMARKS = p_REMARKS;
                mPurchaseMaster.USER_ID = p_USER_ID;
                mPurchaseMaster.IS_ACTIVE = p_IS_ACTIVE;
                mPurchaseMaster.ExecuteQuery();
                
                uspInsertPurchaseInvoiceDetail mPurchaseDetail = new uspInsertPurchaseInvoiceDetail();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtPurchaseDetail.Rows)
                {
                    mPurchaseDetail.PURCHASE_INVOICE_MASTER_ID = mPurchaseMaster.PURCHASE_INVOICE_MASTER_ID;
                    mPurchaseDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.QUANTITY = decimal.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.DISCOUNT = decimal.Parse(dr["DISCOUNT"].ToString());
                    mPurchaseDetail.GST = decimal.Parse(dr["GST"].ToString());
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    p_Gross += decimal.Parse(dr["AMOUNT"].ToString());
                    p_Discount += decimal.Parse(dr["DISCOUNT"].ToString());
                    p_GST += ((p_Gross - p_Discount) * decimal.Parse(dr["GST"].ToString()) / 100);

                    mPurchaseDetail.ExecuteQuery();
                }

                uspInsertPurchaseInvoiceGRN mPurchaseGRN = new uspInsertPurchaseInvoiceGRN();
                mPurchaseGRN.Connection = mConnection;
                mPurchaseGRN.Transaction = mTransaction;

                foreach (DataRow dr in dtGRNIDs.Rows)
                {
                    mPurchaseGRN.PURCHASE_INVOICE_MASTER_ID = mPurchaseMaster.PURCHASE_INVOICE_MASTER_ID;
                    mPurchaseGRN.GRN_ID = long.Parse(dr["GRN_ID"].ToString());
                    mPurchaseGRN.ExecuteQuery();
                }

                AccountHeadController AHCtl = new AccountHeadController();
                DataTable dtAHDetail = AHCtl.GetAccountHeadAssignmentDetail(1);//FORM_ID = 0 for Purchase Invoice

                #region Account Posting

                LedgerController LController = new LedgerController();
                Configuration.GetAccountHead();
                DistributorController Dcontroller = new DistributorController();


                string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID);

                if (dtAHDetail.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtAHDetail.Rows)
                    {
                        if (dr["FIELD_ID"].ToString() == "0" && p_Gross > 0)//FIELD_ID = 0 for Gross Sale
                        {
                            LController.PostingInvoiceAccount_Principal(Constants.Journal_Voucher, long.Parse(VoucherNo), Convert.ToInt64(dr["ACCOUNT_HEAD_ID2"]), p_DISTRIBUTOR_ID, p_Gross, 0, p_DOCUMENT_DATE, "Purchase Gross Value", DateTime.Now, p_PRINCIPAL_ID, 0, mPurchaseMaster.PURCHASE_INVOICE_MASTER_ID, "", Constants.Document_Purchase, p_USER_ID, mTransaction, mConnection, Constants.CashPayment, "");
                        }

                        if (dr["FIELD_ID"].ToString() == "1" && p_Discount > 0) //FIELD_ID = 1 for Discount
                        {
                            LController.PostingInvoiceAccount_Principal(Constants.Journal_Voucher, long.Parse(VoucherNo), Convert.ToInt64(dr["ACCOUNT_HEAD_ID2"]), p_DISTRIBUTOR_ID, 0, p_Discount, p_DOCUMENT_DATE, "Purchase Discount Value", DateTime.Now, p_PRINCIPAL_ID, 0, mPurchaseMaster.PURCHASE_INVOICE_MASTER_ID, "", Constants.Document_Purchase, p_USER_ID, mTransaction, mConnection, Constants.CashPayment, "");
                        }

                        if (dr["FIELD_ID"].ToString() == "2" && p_GST > 0) //FIELD_ID = 2 for GST Amount
                        {
                            LController.PostingInvoiceAccount_Principal(Constants.Journal_Voucher, long.Parse(VoucherNo), Convert.ToInt64(dr["ACCOUNT_HEAD_ID2"]), p_DISTRIBUTOR_ID, p_GST, 0, p_DOCUMENT_DATE, "Purchase GST Value", DateTime.Now, p_PRINCIPAL_ID, 0, mPurchaseMaster.PURCHASE_INVOICE_MASTER_ID, "", Constants.Document_Purchase, p_USER_ID, mTransaction, mConnection, Constants.CashPayment, "");
                        }
                    }

                    if (p_NET_AMOUNT > 0)
                    {
                        LController.PostingInvoiceAccount_Principal(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(dtAHDetail.Rows[0]["ACCOUNT_HEAD_ID"].ToString()), p_DISTRIBUTOR_ID, 0, p_NET_AMOUNT, p_DOCUMENT_DATE, "Purchase Net Value", DateTime.Now, p_PRINCIPAL_ID, 0, mPurchaseMaster.PURCHASE_INVOICE_MASTER_ID, "", Constants.Document_Purchase, p_USER_ID, mTransaction, mConnection, Constants.CashPayment, "");
                    }
                }

                #endregion

                #region GL Posting

                if (dtAHDetail.Rows.Count > 0)
                {
                    DataTable dtVoucher = new DataTable();
                    dtVoucher.Columns.Add("LEDGER_ID", typeof(long));
                    dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
                    dtVoucher.Columns.Add("Debit", typeof(decimal));
                    dtVoucher.Columns.Add("Credit", typeof(decimal));
                    dtVoucher.Columns.Add("Remarks", typeof(string));
                    dtVoucher.Columns.Add("Principal_Id", typeof(string));

                    foreach (DataRow dr in dtAHDetail.Rows)
                    {
                        if (dr["FIELD_ID"].ToString() == "0" && p_Gross > 0)//FIELD_ID = 0 for Gross Sale
                        {
                            DataRow dr0 = dtVoucher.NewRow();
                            dr0["Ledger_ID"] = Constants.LongNullValue.ToString();
                            dr0["ACCOUNT_HEAD_ID"] = dr["ACCOUNT_HEAD_ID2"];
                            dr0["REMARKS"] = "Default Purchase Voucher";
                            dr0["DEBIT"] = p_Gross;
                            dr0["CREDIT"] = 0;
                            dr0["Principal_Id"] = p_PRINCIPAL_ID;
                            dtVoucher.Rows.Add(dr0);
                        }

                        if (dr["FIELD_ID"].ToString() == "1" && p_Discount > 0) //FIELD_ID = 1 for Discount
                        {
                            DataRow dr0 = dtVoucher.NewRow();
                            dr0["Ledger_ID"] = Constants.LongNullValue.ToString();
                            dr0["ACCOUNT_HEAD_ID"] = dr["ACCOUNT_HEAD_ID2"];
                            dr0["REMARKS"] = "Default Purchase Voucher";
                            dr0["DEBIT"] = 0;
                            dr0["CREDIT"] = p_Discount;
                            dr0["Principal_Id"] = p_PRINCIPAL_ID;
                            dtVoucher.Rows.Add(dr0);
                        }

                        if (dr["FIELD_ID"].ToString() == "2" && p_GST > 0) //FIELD_ID = 2 for GST Amount
                        {
                            DataRow dr0 = dtVoucher.NewRow();
                            dr0["Ledger_ID"] = Constants.LongNullValue.ToString();
                            dr0["ACCOUNT_HEAD_ID"] = dr["ACCOUNT_HEAD_ID2"];
                            dr0["REMARKS"] = "Default Purchase Voucher";
                            dr0["DEBIT"] = p_GST;
                            dr0["CREDIT"] = 0;
                            dr0["Principal_Id"] = p_PRINCIPAL_ID;
                            dtVoucher.Rows.Add(dr0);
                        }
                    }

                    if (p_NET_AMOUNT > 0)
                    {
                        DataRow dr01 = dtVoucher.NewRow();
                        dr01["Ledger_ID"] = Constants.LongNullValue.ToString();
                        dr01["ACCOUNT_HEAD_ID"] = dtAHDetail.Rows[0]["ACCOUNT_HEAD_ID"];
                        dr01["REMARKS"] = "Default Purchase Voucher";
                        dr01["DEBIT"] = 0;
                        dr01["CREDIT"] = p_NET_AMOUNT;
                        dr01["Principal_Id"] = p_PRINCIPAL_ID;
                        dtVoucher.Rows.Add(dr01);
                    }

                    LedgerController mLController = new LedgerController();

                    string MaxDocumentId = mLController.SelectMaxVoucherId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID, p_DOCUMENT_DATE);

                    mLController.Add_Voucher(p_DISTRIBUTOR_ID, 0, MaxDocumentId, Constants.Journal_Voucher, p_DOCUMENT_DATE, Constants.CashPayment, 0, "Default Purchase Voucher", Constants.DateNullValue, null, dtVoucher, p_USER_ID, null, Constants.DateNullValue);

                }

                #endregion

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public bool UpdatePurchaseInvoice(long p_PURCHASE_INVOICE_MASTER_ID, DateTime p_DOCUMENT_DATE, long p_GRN_NO, string p_REF_NO, int p_DISTRIBUTOR_ID, int p_PRINCIPAL_ID, int p_USER_ID, decimal p_NET_AMOUNT, string p_REMARKS, bool p_IS_ACTIVE)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdatePurchase_INVOICE_MASTER mPurchaseMaster = new spUpdatePurchase_INVOICE_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.PURCHASE_INVOICE_MASTER_ID = p_PURCHASE_INVOICE_MASTER_ID;
                mPurchaseMaster.GRN_NO = p_GRN_NO;
                mPurchaseMaster.REF_NO = p_REF_NO;
                mPurchaseMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mPurchaseMaster.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mPurchaseMaster.NET_AMOUNT = p_NET_AMOUNT;
                mPurchaseMaster.EXPENSE_AMOUNT = 0;
                mPurchaseMaster.ACCOUNT_HEAD_ID = 0;
                mPurchaseMaster.REMARKS = p_REMARKS;
                mPurchaseMaster.USER_ID = p_USER_ID;
                mPurchaseMaster.IS_ACTIVE = p_IS_ACTIVE;
                mPurchaseMaster.ExecuteQuery();             
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);                
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        #endregion

        #region Purchase Requistion

        public bool InsertRequisiton(int p_DISTRIBUTOR_ID, int p_PRINCIPAL_ID, DateTime p_DOCUMENT_DATE, int p_STORE_ID, DateTime p_DELIVERY_DATE, string p_ORIGINATOR_NAME, string p_REVIEWD_BY, string p_APPROVED_BY,string p_REMARKS, int p_USER_ID, DataTable dtProductDetail,DataTable dtQuality)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertREQUISITION_MASTER mReqMaster = new spInsertREQUISITION_MASTER();
                mReqMaster.Connection = mConnection;
                mReqMaster.Transaction = mTransaction;

                mReqMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mReqMaster.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mReqMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mReqMaster.STORE_ID = p_STORE_ID;
                mReqMaster.DELIVERY_DATE = p_DELIVERY_DATE;
                mReqMaster.ORIGINATOR_NAME = p_ORIGINATOR_NAME;
                mReqMaster.REVIEWD_BY = p_REVIEWD_BY;
                mReqMaster.APPROVED_BY = p_APPROVED_BY;
                mReqMaster.REMARKS = p_REMARKS;
                mReqMaster.USER_ID = p_USER_ID;
                mReqMaster.ExecuteQuery();

                spInsertREQUISITION_VIEW mReqView = new spInsertREQUISITION_VIEW();
                mReqView.Connection = mConnection;
                mReqView.Transaction = mTransaction;
                mReqView.REQUISITION_MASTER_ID = mReqMaster.REQUISITION_MASTER_ID;
                mReqView.ExecuteQuery();

                spInsertREQUISITION_DETAIL mReqDetail = new spInsertREQUISITION_DETAIL();
                mReqDetail.Connection = mConnection;
                mReqDetail.Transaction = mTransaction;

                spInsertPURCHASE_REQUISITION_QUALITY mPurchaseQuality = new spInsertPURCHASE_REQUISITION_QUALITY();
                mPurchaseQuality.Connection = mConnection;
                mPurchaseQuality.Transaction = mTransaction;

                foreach (DataRow dr in dtProductDetail.Rows)
                {
                    mReqDetail.REQUISITION_MASTER_ID = mReqMaster.REQUISITION_MASTER_ID;
                    mReqDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mReqDetail.PRINCIPAL_ID = p_PRINCIPAL_ID;
                    mReqDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mReqDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mReqDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mReqDetail.ExecuteQuery();

                    foreach (DataRow drQuality in dtQuality.Rows)
                    {
                        if (int.Parse(drQuality["SKU_ID"].ToString()) == int.Parse(dr["SKU_ID"].ToString()))
                        {
                            mPurchaseQuality.PURCHASE_REQUISITION_DETAIL_ID = mReqDetail.REQUISITION_DETAIL_ID;
                            mPurchaseQuality.PURCHASE_REQUISITION_ID = mReqMaster.REQUISITION_MASTER_ID;
                            mPurchaseQuality.QUALITY_DESCRIPTION = drQuality["QUALITY_DESCRIPTION"].ToString();
                            mPurchaseQuality.QUALITY_VALUE = drQuality["QUALITY_VALUE"].ToString();
                            mPurchaseQuality.SKU_ID = int.Parse(drQuality["SKU_ID"].ToString());
                            mPurchaseQuality.ExecuteQuery();
                        }
                    }
                    
                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool UpdateRequisition(long p_REQUISITION_MASTER_ID, int p_DISTRIBUTOR_ID, int p_PRINCIPAL_ID, int p_STORE_ID, DateTime p_DELIVERY_DATE, string p_ORIGINATOR_NAME, string p_REVIEWD_BY, string p_APPROVED_BY
            , string p_REMARKS, int p_USER_ID, bool p_IS_DELETED, DataTable dtProductDetail, DataTable dtQuality)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdateREQUISITION_MASTER mReqMaster = new spUpdateREQUISITION_MASTER();
                mReqMaster.Connection = mConnection;
                mReqMaster.Transaction = mTransaction;
                mReqMaster.REQUISITION_MASTER_ID = p_REQUISITION_MASTER_ID;
                mReqMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mReqMaster.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mReqMaster.STORE_ID = p_STORE_ID;
                mReqMaster.DELIVERY_DATE = p_DELIVERY_DATE;
                mReqMaster.ORIGINATOR_NAME = p_ORIGINATOR_NAME;
                mReqMaster.REVIEWD_BY = p_REVIEWD_BY;
                mReqMaster.APPROVED_BY = p_APPROVED_BY;
                mReqMaster.REMARKS = p_REMARKS;
                mReqMaster.USER_ID = p_USER_ID;
                mReqMaster.IS_DELETED = p_IS_DELETED;
                mReqMaster.ExecuteQuery();

                spDeleteREQUISITION_DETAIL mPurchaseStock = new spDeleteREQUISITION_DETAIL();
                mPurchaseStock.Connection = mConnection;
                mPurchaseStock.Transaction = mTransaction;
                mPurchaseStock.REQUISITION_MASTER_ID = p_REQUISITION_MASTER_ID;                
                mPurchaseStock.ExecuteQuery();

                spInsertREQUISITION_DETAIL mReqDetail = new spInsertREQUISITION_DETAIL();
                mReqDetail.Connection = mConnection;
                mReqDetail.Transaction = mTransaction;


                spInsertPURCHASE_REQUISITION_QUALITY mPurchaseQuality = new spInsertPURCHASE_REQUISITION_QUALITY();
                mPurchaseQuality.Connection = mConnection;
                mPurchaseQuality.Transaction = mTransaction;

                foreach (DataRow dr in dtProductDetail.Rows)
                {
                    mReqDetail.REQUISITION_MASTER_ID = mReqMaster.REQUISITION_MASTER_ID;
                    mReqDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mReqDetail.PRINCIPAL_ID = p_PRINCIPAL_ID;
                    mReqDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mReqDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mReqDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mReqDetail.ExecuteQuery();
                    foreach (DataRow drQuality in dtQuality.Rows)
                    {
                        if (int.Parse(drQuality["SKU_ID"].ToString()) == int.Parse(dr["SKU_ID"].ToString()))
                        {
                            mPurchaseQuality.PURCHASE_REQUISITION_DETAIL_ID = mReqDetail.REQUISITION_DETAIL_ID;
                            mPurchaseQuality.PURCHASE_REQUISITION_ID = mReqMaster.REQUISITION_MASTER_ID;
                            mPurchaseQuality.QUALITY_DESCRIPTION = drQuality["QUALITY_DESCRIPTION"].ToString();
                            mPurchaseQuality.QUALITY_VALUE = drQuality["QUALITY_VALUE"].ToString();
                            mPurchaseQuality.SKU_ID = int.Parse(drQuality["SKU_ID"].ToString());
                            mPurchaseQuality.ExecuteQuery();
                        }
                    }
                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool UpdateRequisition(long p_REQUISITION_MASTER_ID, int p_DISTRIBUTOR_ID, int p_PRINCIPAL_ID, int p_STORE_ID, DateTime p_DELIVERY_DATE, string p_ORIGINATOR_NAME, string p_REVIEWD_BY, string p_APPROVED_BY
            , string p_REMARKS,int p_STATUS_ID, int p_USER_ID, bool p_IS_DELETED)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateREQUISITION_MASTER mReqMaster = new spUpdateREQUISITION_MASTER();
                mReqMaster.Connection = mConnection;
                mReqMaster.REQUISITION_MASTER_ID = p_REQUISITION_MASTER_ID;
                mReqMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mReqMaster.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mReqMaster.STORE_ID = p_STORE_ID;
                mReqMaster.DELIVERY_DATE = p_DELIVERY_DATE;
                mReqMaster.ORIGINATOR_NAME = p_ORIGINATOR_NAME;
                mReqMaster.REVIEWD_BY = p_REVIEWD_BY;
                mReqMaster.APPROVED_BY = p_APPROVED_BY;
                mReqMaster.REMARKS = p_REMARKS;
                mReqMaster.STATUS_ID = p_STATUS_ID;
                mReqMaster.USER_ID = p_USER_ID;
                mReqMaster.IS_DELETED = p_IS_DELETED;
                mReqMaster.ExecuteQuery();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        #endregion
        #region Quotation

        public bool InsertQuotation(int p_DISTRIBUTOR_ID, int p_PRINCIPAL_ID, DateTime p_DOCUMENT_DATE, int p_STORE_ID, DateTime p_DELIVERY_DATE, string p_ORIGINATOR_NAME, string p_REMARKS, int p_USER_ID, DataTable dtProductDetail)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertQUOTATION_MASTER mReqMaster = new spInsertQUOTATION_MASTER();
                mReqMaster.Connection = mConnection;
                mReqMaster.Transaction = mTransaction;

                mReqMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mReqMaster.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mReqMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mReqMaster.STORE_ID = p_STORE_ID;
               
                mReqMaster.ORIGINATOR_NAME = p_ORIGINATOR_NAME;
              
                mReqMaster.REMARKS = p_REMARKS;
                mReqMaster.USER_ID = p_USER_ID;
                mReqMaster.ExecuteQuery();


                spInsertREQUISITION_DETAIL mReqDetail = new spInsertREQUISITION_DETAIL();
                mReqDetail.Connection = mConnection;
                mReqDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtProductDetail.Rows)
                {
                    mReqDetail.REQUISITION_MASTER_ID = mReqMaster.REQUISITION_MASTER_ID;
                    mReqDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mReqDetail.PRINCIPAL_ID = p_PRINCIPAL_ID;
                    mReqDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mReqDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mReqDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mReqDetail.ExecuteQuery();

                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool UpdateQuotation(long p_REQUISITION_MASTER_ID, int p_DISTRIBUTOR_ID, int p_PRINCIPAL_ID, int p_STORE_ID, DateTime p_DELIVERY_DATE, string p_ORIGINATOR_NAME
           , string p_REMARKS, int p_USER_ID, bool p_IS_DELETED, DataTable dtProductDetail)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdateREQUISITION_MASTER mReqMaster = new spUpdateREQUISITION_MASTER();
                mReqMaster.Connection = mConnection;
                mReqMaster.Transaction = mTransaction;
                mReqMaster.REQUISITION_MASTER_ID = p_REQUISITION_MASTER_ID;
                mReqMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mReqMaster.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mReqMaster.STORE_ID = p_STORE_ID;
                mReqMaster.DELIVERY_DATE = p_DELIVERY_DATE;
                mReqMaster.ORIGINATOR_NAME = p_ORIGINATOR_NAME;
               
                mReqMaster.REMARKS = p_REMARKS;
                mReqMaster.USER_ID = p_USER_ID;
                mReqMaster.IS_DELETED = p_IS_DELETED;
                mReqMaster.ExecuteQuery();

                spDeleteREQUISITION_DETAIL mPurchaseStock = new spDeleteREQUISITION_DETAIL();
                mPurchaseStock.Connection = mConnection;
                mPurchaseStock.Transaction = mTransaction;
                mPurchaseStock.REQUISITION_MASTER_ID = p_REQUISITION_MASTER_ID;
                mPurchaseStock.ExecuteQuery();

                spInsertREQUISITION_DETAIL mReqDetail = new spInsertREQUISITION_DETAIL();
                mReqDetail.Connection = mConnection;
                mReqDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtProductDetail.Rows)
                {
                    mReqDetail.REQUISITION_MASTER_ID = mReqMaster.REQUISITION_MASTER_ID;
                    mReqDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mReqDetail.PRINCIPAL_ID = p_PRINCIPAL_ID;
                    mReqDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mReqDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mReqDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mReqDetail.ExecuteQuery();
                   
                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        #endregion
        #region Comparative Statement

        public bool InsertComparativeStatement(long p_REQUISITION_MASTER_ID, int p_DISTRIBUTOR_ID, DateTime p_DOCUMENT_DATE, int p_STORE_ID, DateTime p_DELIVERY_DATE, string p_ORIGINATOR_NAME, string p_REVIEWD_BY, string p_APPROVED_BY
            , int p_USER_ID, DataTable dtProductDetail)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertCOMPARATIVE_STATEMENT_MASTER mReqMaster = new spInsertCOMPARATIVE_STATEMENT_MASTER();
                mReqMaster.Connection = mConnection;
                mReqMaster.Transaction = mTransaction;
                mReqMaster.REQUISITION_MASTER_ID = p_REQUISITION_MASTER_ID;
                mReqMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mReqMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mReqMaster.STORE_ID = p_STORE_ID;
                mReqMaster.DELIVERY_DATE = p_DELIVERY_DATE;
                mReqMaster.ORIGINATOR_NAME = p_ORIGINATOR_NAME;
                mReqMaster.REVIEWD_BY = p_REVIEWD_BY;
                mReqMaster.APPROVED_BY = p_APPROVED_BY;
                mReqMaster.USER_ID = p_USER_ID;

                mReqMaster.ExecuteQuery();

                spInsertCOMPARATIVE_STATEMENT_DETAIL mReqDetail = new spInsertCOMPARATIVE_STATEMENT_DETAIL();
                mReqDetail.Connection = mConnection;
                mReqDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtProductDetail.Rows)
                {
                    mReqDetail.COMPARATIVE_STATEMENT_MASTER_ID = mReqMaster.COMPARATIVE_STATEMENT_MASTER_ID;
                    mReqDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mReqDetail.PRINCIPAL_ID = int.Parse(dr["SKU_HIE_ID"].ToString()); ;
                    mReqDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mReqDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mReqDetail.PRICE_PREVIOUS = decimal.Parse(dr["PRICE_PREVIOUS"].ToString());
                    mReqDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mReqDetail.PAYMENT_MODE = int.Parse(dr["PaymentModeID"].ToString());
                    mReqDetail.PAYMENT_TERM = dr["PaymentTerm"].ToString();
                    mReqDetail.ExecuteQuery();

                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool UpdateComparativeStatement(long p_COMPARATIVE_STATEMENT_MASTER_ID, int p_DISTRIBUTOR_ID, int p_STORE_ID, DateTime p_DELIVERY_DATE, string p_ORIGINATOR_NAME, string p_REVIEWD_BY, string p_APPROVED_BY
            ,int p_USER_ID, bool p_IS_DELETED, DataTable dtProductDetail)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdateCOMPARATIVE_STATEMENT_MASTER mReqMaster = new spUpdateCOMPARATIVE_STATEMENT_MASTER();
                mReqMaster.Connection = mConnection;
                mReqMaster.Transaction = mTransaction;
                mReqMaster.COMPARATIVE_STATEMENT_MASTER_ID = p_COMPARATIVE_STATEMENT_MASTER_ID;
                mReqMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mReqMaster.STORE_ID = p_STORE_ID;
                mReqMaster.DELIVERY_DATE = p_DELIVERY_DATE;
                mReqMaster.ORIGINATOR_NAME = p_ORIGINATOR_NAME;
                mReqMaster.REVIEWD_BY = p_REVIEWD_BY;
                mReqMaster.APPROVED_BY = p_APPROVED_BY;
                mReqMaster.USER_ID = p_USER_ID;
                mReqMaster.IS_DELETED = p_IS_DELETED;
                mReqMaster.ExecuteQuery();

                spDeleteCOMPARATIVE_STATEMENT_DETAIL mPurchaseStock = new spDeleteCOMPARATIVE_STATEMENT_DETAIL();
                mPurchaseStock.Connection = mConnection;
                mPurchaseStock.Transaction = mTransaction;
                mPurchaseStock.COMPARATIVE_STATEMENT_MASTER_ID = p_COMPARATIVE_STATEMENT_MASTER_ID;
                mPurchaseStock.ExecuteQuery();

                spInsertCOMPARATIVE_STATEMENT_DETAIL mReqDetail = new spInsertCOMPARATIVE_STATEMENT_DETAIL();
                mReqDetail.Connection = mConnection;
                mReqDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtProductDetail.Rows)
                {
                    mReqDetail.COMPARATIVE_STATEMENT_MASTER_ID = mReqMaster.COMPARATIVE_STATEMENT_MASTER_ID;
                    mReqDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mReqDetail.PRINCIPAL_ID = int.Parse(dr["SKU_HIE_ID"].ToString()); ;
                    mReqDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mReqDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mReqDetail.PRICE_PREVIOUS = decimal.Parse(dr["PRICE_PREVIOUS"].ToString());
                    mReqDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mReqDetail.PAYMENT_MODE = int.Parse(dr["PaymentModeID"].ToString());
                    mReqDetail.PAYMENT_TERM = dr["PaymentTerm"].ToString();
                    mReqDetail.ExecuteQuery();

                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public bool UpdateComparativeStatement(long p_COMPARATIVE_STATEMENT_MASTER_ID, int p_DISTRIBUTOR_ID, int p_STORE_ID, DateTime p_DELIVERY_DATE, string p_ORIGINATOR_NAME, string p_REVIEWD_BY, string p_APPROVED_BY
            , int p_USER_ID, bool p_IS_DELETED)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateCOMPARATIVE_STATEMENT_MASTER mReqMaster = new spUpdateCOMPARATIVE_STATEMENT_MASTER();
                mReqMaster.Connection = mConnection;
                mReqMaster.COMPARATIVE_STATEMENT_MASTER_ID = p_COMPARATIVE_STATEMENT_MASTER_ID;
                mReqMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mReqMaster.STORE_ID = p_STORE_ID;
                mReqMaster.DELIVERY_DATE = p_DELIVERY_DATE;
                mReqMaster.ORIGINATOR_NAME = p_ORIGINATOR_NAME;
                mReqMaster.REVIEWD_BY = p_REVIEWD_BY;
                mReqMaster.APPROVED_BY = p_APPROVED_BY;
                mReqMaster.USER_ID = p_USER_ID;
                mReqMaster.IS_DELETED = p_IS_DELETED;
                mReqMaster.ExecuteQuery();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        #endregion

        #region GRN

        public bool InsertGRN(int p_DISTRIBUTOR_ID, int p_PRINCIPAL_ID, DateTime p_DOCUMENT_DATE, long p_REQUISITION_MASTER_ID, string p_REVIEWED_BY, string p_APPROVED_BY, int p_USER_ID, DataTable dtGRNDetail)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertGRN_MASTER mGRNMaster = new spInsertGRN_MASTER();
                mGRNMaster.Connection = mConnection;
                mGRNMaster.Transaction = mTransaction;
                mGRNMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mGRNMaster.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mGRNMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mGRNMaster.REQUISITION_MASTER_ID = p_REQUISITION_MASTER_ID;
                mGRNMaster.REVIEWED_BY = p_REVIEWED_BY;
                mGRNMaster.APPROVED_BY = p_APPROVED_BY;
                mGRNMaster.USER_ID = p_USER_ID;
                mGRNMaster.ExecuteQuery();
                spInsertGRN_DETAIL mGRNDetail = new spInsertGRN_DETAIL();
                mGRNDetail.Connection = mConnection;
                mGRNDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtGRNDetail.Rows)
                {
                    mGRNDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mGRNDetail.GRN_MASTER_ID = mGRNMaster.GRN_MASTER_ID;
                    mGRNDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mGRNDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mGRNDetail.ExecuteQuery();

                    uspProcessSKUStock mSKUStockReg = new uspProcessSKUStock();
                    mSKUStockReg.Connection = mConnection;
                    mSKUStockReg.Transaction = mTransaction;
                    mSKUStockReg.SKU_ID = mGRNDetail.SKU_ID;
                    mSKUStockReg.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mSKUStockReg.UNITS_IN_STOCK = mGRNDetail.QUANTITY;
                    mSKUStockReg.TYPE_ID = Constants.Document_Purchase;
                    mSKUStockReg.EXPIRY_DATE = p_DOCUMENT_DATE;
                    mSKUStockReg.ExecuteQuery();
                }
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool UpdateGRN(long p_GRN_MASTER_ID, int p_DISTRIBUTOR_ID, int p_PRINCIPAL_ID, long p_REQUISITION_MASTER_ID, string p_REVIEWD_BY, string p_APPROVED_BY, int p_STATUS_ID
            , int p_USER_ID, bool p_IS_DELETED, DataTable dtProductDetail)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdateGRN_MASTER mGRMMaster = new spUpdateGRN_MASTER();
                mGRMMaster.Connection = mConnection;
                mGRMMaster.Transaction = mTransaction;
                mGRMMaster.GRN_MASTER_ID = p_GRN_MASTER_ID;
                mGRMMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mGRMMaster.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mGRMMaster.REQUISITION_MASTER_ID = p_REQUISITION_MASTER_ID;
                mGRMMaster.REVIEWED_BY = p_REVIEWD_BY;
                mGRMMaster.APPROVED_BY = p_APPROVED_BY;
                mGRMMaster.STATUS_ID = p_STATUS_ID;
                mGRMMaster.USER_ID = p_USER_ID;
                mGRMMaster.IS_DELETED = p_IS_DELETED;
                mGRMMaster.ExecuteQuery();

                spDeleteGRN_DETAIL mPurchaseStock = new spDeleteGRN_DETAIL();
                mPurchaseStock.Connection = mConnection;
                mPurchaseStock.Transaction = mTransaction;
                mPurchaseStock.GRN_MASTER_ID = p_GRN_MASTER_ID;
                mPurchaseStock.ExecuteQuery();

                spInsertGRN_DETAIL mGRNDetail = new spInsertGRN_DETAIL();
                mGRNDetail.Connection = mConnection;
                mGRNDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtProductDetail.Rows)
                {
                    mGRNDetail.GRN_MASTER_ID = mGRMMaster.GRN_MASTER_ID;
                    mGRNDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mGRNDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mGRNDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mGRNDetail.ExecuteQuery();

                    uspProcessSKUStock mSKUStockReg = new uspProcessSKUStock();
                    mSKUStockReg.Connection = mConnection;
                    mSKUStockReg.Transaction = mTransaction;
                    mSKUStockReg.SKU_ID = mGRNDetail.SKU_ID;
                    mSKUStockReg.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mSKUStockReg.UNITS_IN_STOCK = mGRNDetail.QUANTITY;
                    mSKUStockReg.TYPE_ID = Constants.Document_Purchase;
                    mSKUStockReg.EXPIRY_DATE = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
                    mSKUStockReg.ExecuteQuery();
                }
                
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public bool UpdateGRN(long p_GRN_MASTER_ID, int p_DISTRIBUTOR_ID, int p_PRINCIPAL_ID, long p_REQUISITION_MASTER_ID, string p_REVIEWD_BY, string p_APPROVED_BY, int p_STATUS_ID
            , int p_USER_ID, bool p_IS_DELETED)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateGRN_MASTER mGRMMaster = new spUpdateGRN_MASTER();
                mGRMMaster.Connection = mConnection;
                mGRMMaster.GRN_MASTER_ID = p_GRN_MASTER_ID;
                mGRMMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mGRMMaster.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mGRMMaster.REQUISITION_MASTER_ID = p_REQUISITION_MASTER_ID;
                mGRMMaster.REVIEWED_BY = p_REVIEWD_BY;
                mGRMMaster.APPROVED_BY = p_APPROVED_BY;
                mGRMMaster.STATUS_ID = p_STATUS_ID;
                mGRMMaster.USER_ID = p_USER_ID;
                mGRMMaster.IS_DELETED = p_IS_DELETED;
                mGRMMaster.ExecuteQuery();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        #endregion

        #region QC

        public bool InsertQC(long p_GRN_MASTER_ID, DateTime p_DOCUMENT_DATE, int p_USER_ID, DataTable dtQCDetail, DataTable dtQCParamter)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertQC_MASTER mQCMaster = new spInsertQC_MASTER();
                mQCMaster.Connection = mConnection;
                mQCMaster.Transaction = mTransaction;
                mQCMaster.GRN_MASTER_ID = p_GRN_MASTER_ID;
                mQCMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mQCMaster.USER_ID = p_USER_ID;
                mQCMaster.ExecuteQuery();
                spInsertQC_DETAIL mQCDetail = new spInsertQC_DETAIL();
                mQCDetail.Connection = mConnection;
                mQCDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtQCDetail.Rows)
                {
                    mQCDetail.QC_MASTER_ID = mQCMaster.QC_MASTER_ID;
                    mQCDetail.PRODUCT_ID = int.Parse(dr["SKU_ID"].ToString());
                    mQCDetail.STATUS_ID = int.Parse(dr["STATUS_ID"].ToString());
                    mQCDetail.QUANTITY_ACCEPT = int.Parse(dr["Quantity"].ToString());
                    mQCDetail.ExecuteQuery();

                    spInsertQC_DETAIL2 mQCDetail2 = new spInsertQC_DETAIL2();
                    mQCDetail2.Connection = mConnection;
                    mQCDetail2.Transaction = mTransaction;

                    foreach (DataRow dr2 in dtQCParamter.Rows)
                    {
                        if (dr2["SKU_ID"].ToString() == dr["SKU_ID"].ToString())
                        {
                            mQCDetail2.QC_DETAIL_ID = mQCDetail.QC_DETAIL_ID;
                            mQCDetail2.QC_MASTER_ID = mQCDetail.QC_MASTER_ID;
                            mQCDetail2.SKU_QUALITY_ID = Convert.ToInt32(dr2["SKU_QUALITY_ID"]);
                            mQCDetail2.VALUE = Convert.ToDecimal(dr2["VALUE"]);
                            mQCDetail2.REMARKS = dr2["REMARKS"].ToString();
                            mQCDetail2.ExecuteQuery();
                        }
                    }

                }

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool UpdateQC(long p_QC_MASTER_ID,bool p_IS_ACTIVE, int p_USER_ID, DataTable dtQCDetail, DataTable dtQCParamter)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdateQC_MASTER mQCMaster = new spUpdateQC_MASTER();
                mQCMaster.Connection = mConnection;
                mQCMaster.Transaction = mTransaction;
                mQCMaster.QC_MASTER_ID = p_QC_MASTER_ID;
                mQCMaster.USER_ID = p_USER_ID;
                mQCMaster.IS_ACTIVE = p_IS_ACTIVE;
                mQCMaster.ExecuteQuery();

                spDeleteQC_DETAIL mQCDel = new spDeleteQC_DETAIL();
                mQCDel.Connection = mConnection;
                mQCDel.Transaction = mTransaction;
                mQCDel.QC_MASTER_ID = p_QC_MASTER_ID;
                mQCDel.ExecuteQuery();

                spInsertQC_DETAIL mQCDetail = new spInsertQC_DETAIL();
                mQCDetail.Connection = mConnection;
                mQCDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtQCDetail.Rows)
                {
                    mQCDetail.QC_MASTER_ID = mQCMaster.QC_MASTER_ID;
                    mQCDetail.PRODUCT_ID = int.Parse(dr["SKU_ID"].ToString());
                    mQCDetail.STATUS_ID = int.Parse(dr["STATUS_ID"].ToString());
                    mQCDetail.QUANTITY_ACCEPT = int.Parse(dr["Quantity"].ToString());
                    mQCDetail.ExecuteQuery();

                    spInsertQC_DETAIL2 mQCDetail2 = new spInsertQC_DETAIL2();
                    mQCDetail2.Connection = mConnection;
                    mQCDetail2.Transaction = mTransaction;

                    foreach (DataRow dr2 in dtQCParamter.Rows)
                    {
                        if (dr2["SKU_ID"].ToString() == dr["SKU_ID"].ToString())
                        {
                            mQCDetail2.QC_DETAIL_ID = mQCDetail.QC_DETAIL_ID;
                            mQCDetail2.QC_MASTER_ID = mQCDetail.QC_MASTER_ID;
                            mQCDetail2.SKU_QUALITY_ID = Convert.ToInt32(dr2["SKU_QUALITY_ID"]);
                            mQCDetail2.VALUE = Convert.ToDecimal(dr2["VALUE"]);
                            mQCDetail2.REMARKS = dr2["REMARKS"].ToString();
                            mQCDetail2.ExecuteQuery();
                        }
                    }

                }

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        public bool UpdateQC(long p_QC_MASTER_ID, bool p_IS_ACTIVE, int p_USER_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateQC_MASTER mQCMaster = new spUpdateQC_MASTER();
                mQCMaster.Connection = mConnection;
                mQCMaster.QC_MASTER_ID = p_QC_MASTER_ID;
                mQCMaster.USER_ID = p_USER_ID;
                mQCMaster.IS_ACTIVE = p_IS_ACTIVE;
                mQCMaster.ExecuteQuery();

                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }
        }

        #endregion

        #region Purchase Return

        public bool InsertPurchaseReturn(int p_DISTRIBUTOR_ID, int p_PRINCIPAL_ID, DateTime p_DOCUMENT_DATE, int p_STORE_ID, long p_PO, string p_REMARKS, int p_USER_ID, DataTable dtProductDetail)
        {
            try
            {
                decimal p_Gross = 0;
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_RETURN_MASTER mReturnMaster = new spInsertPURCHASE_RETURN_MASTER();
                mReturnMaster.Connection = mConnection;
                mReturnMaster.Transaction = mTransaction;
                mReturnMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mReturnMaster.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mReturnMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mReturnMaster.STORE_ID = p_STORE_ID;
                mReturnMaster.PO = p_PO;
                mReturnMaster.REMARKS = p_REMARKS;
                mReturnMaster.USER_ID = p_USER_ID;

                mReturnMaster.ExecuteQuery();

                spInsertPURCHASE_RETURN_DETAIL mReturnDetail = new spInsertPURCHASE_RETURN_DETAIL();
                mReturnDetail.Connection = mConnection;
                mReturnDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtProductDetail.Rows)
                {
                    mReturnDetail.PURCHASE_RETURN_ID = mReturnMaster.PURCHASE_RETURN_ID;
                    mReturnDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mReturnDetail.PRINCIPAL_ID = p_PRINCIPAL_ID;
                    mReturnDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mReturnDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mReturnDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    p_Gross += decimal.Parse(dr["PRICE"].ToString()) * int.Parse(dr["QUANTITY"].ToString());
                    mReturnDetail.ExecuteQuery();

                    uspProcessSKUStock mSKUStockReg = new uspProcessSKUStock();
                    mSKUStockReg.Connection = mConnection;
                    mSKUStockReg.Transaction = mTransaction;
                    mSKUStockReg.SKU_ID = mReturnDetail.SKU_ID;
                    mSKUStockReg.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mSKUStockReg.UNITS_IN_STOCK = mReturnDetail.QUANTITY;
                    mSKUStockReg.TYPE_ID = Constants.Document_Purchase_Return;
                    mSKUStockReg.EXPIRY_DATE = p_DOCUMENT_DATE;
                    mSKUStockReg.ExecuteQuery();

                }

                #region Account Posting

                AccountHeadController AHCtl = new AccountHeadController();
                DataTable dtAHDetail = AHCtl.GetAccountHeadAssignmentDetail(3);//FORM_ID = 3 for Purchase Return

                LedgerController LController = new LedgerController();
                Configuration.GetAccountHead();
                DistributorController Dcontroller = new DistributorController();


                string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID);

                if (dtAHDetail.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtAHDetail.Rows)
                    {
                        if (dr["FIELD_ID"].ToString() == "0" && p_Gross > 0)//FIELD_ID = 0 for Gross Sale
                        {
                            LController.PostingInvoiceAccount_Principal(Constants.Journal_Voucher, long.Parse(VoucherNo), Convert.ToInt64(dr["ACCOUNT_HEAD_ID2"]), p_DISTRIBUTOR_ID, 0, p_Gross, p_DOCUMENT_DATE, "Purchase Return Gross Value", DateTime.Now, p_PRINCIPAL_ID, 0, mReturnMaster.PURCHASE_RETURN_ID, "", Constants.Document_Purchase_Return, p_USER_ID, mTransaction, mConnection, Constants.CashPayment, "");
                            LController.PostingInvoiceAccount_Principal(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(dtAHDetail.Rows[0]["ACCOUNT_HEAD_ID"].ToString()), p_DISTRIBUTOR_ID, p_Gross, 0, p_DOCUMENT_DATE, "Purchase Retrun Value", DateTime.Now, p_PRINCIPAL_ID, 0, mReturnMaster.PURCHASE_RETURN_ID, "", Constants.Document_Purchase_Return, p_USER_ID, mTransaction, mConnection, Constants.CashPayment, "");
                        }
                        
                    }
                    
                }

                #endregion               

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool UpdatePurchaseReturn(long p_REQUISITION_MASTER_ID, DateTime p_DOCUMENT_DATE, int p_DISTRIBUTOR_ID, int p_PRINCIPAL_ID, int p_STORE_ID, long p_PO, 
            string p_REMARKS, int p_USER_ID, bool p_IS_DELETED, DataTable dtProductDetail)
        {
            try
            {
                decimal p_Gross = 0;
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdatePURCHASE_RETURN_MASTER mReqMaster = new spUpdatePURCHASE_RETURN_MASTER();
                mReqMaster.Connection = mConnection;
                mReqMaster.Transaction = mTransaction;
                mReqMaster.PURCHASE_RETURN_ID = p_REQUISITION_MASTER_ID;
                mReqMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mReqMaster.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mReqMaster.STORE_ID = p_STORE_ID;
                mReqMaster.PO = p_PO;
                mReqMaster.REMARKS = p_REMARKS;
                mReqMaster.USER_ID = p_USER_ID;
                mReqMaster.IS_DELETED = p_IS_DELETED;
                mReqMaster.ExecuteQuery();

                spInsertPURCHASE_RETURN_DETAIL mReturnDetail = new spInsertPURCHASE_RETURN_DETAIL();
                mReturnDetail.Connection = mConnection;
                mReturnDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtProductDetail.Rows)
                {
                    mReturnDetail.PURCHASE_RETURN_ID = mReqMaster.PURCHASE_RETURN_ID;
                    mReturnDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mReturnDetail.PRINCIPAL_ID = p_PRINCIPAL_ID;
                    mReturnDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mReturnDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mReturnDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    p_Gross += decimal.Parse(dr["PRICE"].ToString()) * int.Parse(dr["QUANTITY"].ToString());
                    mReturnDetail.ExecuteQuery();

                    uspProcessSKUStock mSKUStockReg = new uspProcessSKUStock();
                    mSKUStockReg.Connection = mConnection;
                    mSKUStockReg.Transaction = mTransaction;
                    mSKUStockReg.SKU_ID = mReturnDetail.SKU_ID;
                    mSKUStockReg.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mSKUStockReg.UNITS_IN_STOCK = mReturnDetail.QUANTITY;
                    mSKUStockReg.TYPE_ID = Constants.Document_Purchase_Return;
                    mSKUStockReg.EXPIRY_DATE = p_DOCUMENT_DATE;
                    mSKUStockReg.ExecuteQuery();

                }

                #region Account Posting

                AccountHeadController AHCtl = new AccountHeadController();
                DataTable dtAHDetail = AHCtl.GetAccountHeadAssignmentDetail(3);//FORM_ID = 3 for Purchase Return

                LedgerController LController = new LedgerController();
                Configuration.GetAccountHead();
                DistributorController Dcontroller = new DistributorController();


                string VoucherNo = LController.SelectLedgerMaxDocumentId(Constants.Journal_Voucher, p_DISTRIBUTOR_ID);

                if (dtAHDetail.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtAHDetail.Rows)
                    {
                        if (dr["FIELD_ID"].ToString() == "0" && p_Gross > 0)//FIELD_ID = 0 for Gross Sale
                        {
                            LController.PostingInvoiceAccount_Principal(Constants.Journal_Voucher, long.Parse(VoucherNo), Convert.ToInt64(dr["ACCOUNT_HEAD_ID2"]), p_DISTRIBUTOR_ID, 0, p_Gross, p_DOCUMENT_DATE, "Purchase Return Gross Value", DateTime.Now, p_PRINCIPAL_ID, 0, mReqMaster.PURCHASE_RETURN_ID, "", Constants.Document_Purchase_Return, p_USER_ID, mTransaction, mConnection, Constants.CashPayment, "");
                            LController.PostingInvoiceAccount_Principal(Constants.Journal_Voucher, long.Parse(VoucherNo), long.Parse(dtAHDetail.Rows[0]["ACCOUNT_HEAD_ID"].ToString()), p_DISTRIBUTOR_ID, p_Gross, 0, p_DOCUMENT_DATE, "Purchase Retrun Value", DateTime.Now, p_PRINCIPAL_ID, 0, mReqMaster.PURCHASE_RETURN_ID, "", Constants.Document_Purchase_Return, p_USER_ID, mTransaction, mConnection, Constants.CashPayment, "");
                        }

                    }

                }

                #endregion               

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool UpdatePurchaseReturn(long p_REQUISITION_MASTER_ID, DateTime p_DOCUMENT_DATE, int p_DISTRIBUTOR_ID, int p_PRINCIPAL_ID, int p_STORE_ID, long p_PO,
            string p_REMARKS, int p_USER_ID, bool p_IS_DELETED)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdatePURCHASE_RETURN_MASTER mReqMaster = new spUpdatePURCHASE_RETURN_MASTER();
                mReqMaster.Connection = mConnection;
                mReqMaster.Transaction = mTransaction;
                mReqMaster.PURCHASE_RETURN_ID = p_REQUISITION_MASTER_ID;
                mReqMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mReqMaster.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mReqMaster.STORE_ID = p_STORE_ID;
                mReqMaster.PO = p_PO;
                mReqMaster.REMARKS = p_REMARKS;
                mReqMaster.USER_ID = p_USER_ID;
                mReqMaster.IS_DELETED = p_IS_DELETED;
                mReqMaster.ExecuteQuery();
                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        #endregion

        #region Material Issue/Return

        public bool InsertMaterial(int p_DISTRIBUTOR_ID, DateTime p_DOCUMENT_DATE, int p_STORE_ID, string p_REMARKS ,int p_TYPE_ID, int p_USER_ID ,DataTable dtProductDetail)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertMATERIAL_MASTER mMaterialMaster = new spInsertMATERIAL_MASTER();
                mMaterialMaster.Connection = mConnection;
                mMaterialMaster.Transaction = mTransaction;
                mMaterialMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mMaterialMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mMaterialMaster.STORE_ID = p_STORE_ID;
                mMaterialMaster.REMARKS = p_REMARKS;
                mMaterialMaster.TYPE_ID = p_TYPE_ID;
                mMaterialMaster.USER_ID = p_USER_ID;

                mMaterialMaster.ExecuteQuery();

                spInsertMATERIAL_DETAIL mMaterialDetail = new spInsertMATERIAL_DETAIL();
                mMaterialDetail.Connection = mConnection;
                mMaterialDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtProductDetail.Rows)
                {
                    mMaterialDetail.MATERIAL_MASTER_ID = mMaterialMaster.MATERIAL_MASTER_ID;
                    mMaterialDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mMaterialDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mMaterialDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mMaterialDetail.ExecuteQuery();

                }

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool UpdateMaterial(long p_MATERIAL_MASTER_ID, int p_DISTRIBUTOR_ID,int p_STORE_ID,
            string p_REMARKS,int p_TYPE_ID, int p_USER_ID, bool p_IS_DELETED, DataTable dtProductDetail)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdateMATERIAL_MASTER mMaterialMaster = new spUpdateMATERIAL_MASTER();
                mMaterialMaster.Connection = mConnection;
                mMaterialMaster.Transaction = mTransaction;
                mMaterialMaster.MATERIAL_MASTER_ID = p_MATERIAL_MASTER_ID;
                mMaterialMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mMaterialMaster.STORE_ID = p_STORE_ID;
                mMaterialMaster.REMARKS = p_REMARKS;
                mMaterialMaster.TYPE_ID = p_TYPE_ID;
                mMaterialMaster.USER_ID = p_USER_ID;
                mMaterialMaster.IS_DELETED = p_IS_DELETED;
                mMaterialMaster.ExecuteQuery();

                spInsertMATERIAL_DETAIL mMaterialDetail = new spInsertMATERIAL_DETAIL();
                mMaterialDetail.Connection = mConnection;
                mMaterialDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtProductDetail.Rows)
                {
                    mMaterialDetail.MATERIAL_MASTER_ID = mMaterialMaster.MATERIAL_MASTER_ID;
                    mMaterialDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mMaterialDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mMaterialDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mMaterialDetail.ExecuteQuery();
                }

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool UpdateMaterial(long p_MATERIAL_MASTER_ID,int p_DISTRIBUTOR_ID, int p_STORE_ID,
            string p_REMARKS, int p_TYPE_ID, int p_USER_ID, bool p_IS_DELETED)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdateMATERIAL_MASTER mMaterialMaster = new spUpdateMATERIAL_MASTER();
                mMaterialMaster.Connection = mConnection;
                mMaterialMaster.Transaction = mTransaction;
                mMaterialMaster.MATERIAL_MASTER_ID = p_MATERIAL_MASTER_ID;
                mMaterialMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mMaterialMaster.STORE_ID = p_STORE_ID;
                mMaterialMaster.REMARKS = p_REMARKS;
                mMaterialMaster.TYPE_ID = p_TYPE_ID;
                mMaterialMaster.USER_ID = p_USER_ID;
                mMaterialMaster.IS_DELETED = p_IS_DELETED;
                mMaterialMaster.ExecuteQuery();

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        #endregion

        #region Maintenance/Repair

        public bool InsertMaintenance(int p_DISTRIBUTOR_ID, DateTime p_DOCUMENT_DATE, int p_STORE_ID, string p_REMARKS, int p_PRINCIPAL_ID, int p_USER_ID, DataTable dtProductDetail)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertREPAIR_MASTER mMaterialMaster = new spInsertREPAIR_MASTER();
                mMaterialMaster.Connection = mConnection;
                mMaterialMaster.Transaction = mTransaction;
                mMaterialMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mMaterialMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mMaterialMaster.STORE_ID = p_STORE_ID;
                mMaterialMaster.REMARKS = p_REMARKS;
                mMaterialMaster.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mMaterialMaster.USER_ID = p_USER_ID;

                mMaterialMaster.ExecuteQuery();

                spInsertREPAIR_DETAIL mMaterialDetail = new spInsertREPAIR_DETAIL();
                mMaterialDetail.Connection = mConnection;
                mMaterialDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtProductDetail.Rows)
                {
                    mMaterialDetail.REPAIR_MASTER_ID = mMaterialMaster.REPAIR_MASTER_ID;
                    mMaterialDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mMaterialDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mMaterialDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mMaterialDetail.ExecuteQuery();

                }

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool UpdateMaintenance(long p_REPAIR_MASTER_ID, int p_DISTRIBUTOR_ID, int p_STORE_ID,
            string p_REMARKS, int p_PRINCIPAL_ID, int p_USER_ID, bool p_IS_DELETED, DataTable dtProductDetail)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdateREPAIR_MASTER mMaterialMaster = new spUpdateREPAIR_MASTER();
                mMaterialMaster.Connection = mConnection;
                mMaterialMaster.Transaction = mTransaction;
                mMaterialMaster.REPAIR_MASTER_ID = p_REPAIR_MASTER_ID;
                mMaterialMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mMaterialMaster.STORE_ID = p_STORE_ID;
                mMaterialMaster.REMARKS = p_REMARKS;
                mMaterialMaster.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mMaterialMaster.USER_ID = p_USER_ID;
                mMaterialMaster.IS_DELETED = p_IS_DELETED;
                mMaterialMaster.ExecuteQuery();

                spInsertREPAIR_DETAIL mMaterialDetail = new spInsertREPAIR_DETAIL();
                mMaterialDetail.Connection = mConnection;
                mMaterialDetail.Transaction = mTransaction;

                foreach (DataRow dr in dtProductDetail.Rows)
                {
                    mMaterialDetail.REPAIR_MASTER_ID = mMaterialMaster.REPAIR_MASTER_ID;
                    mMaterialDetail.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                    mMaterialDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mMaterialDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                    mMaterialDetail.ExecuteQuery();
                }

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        public bool UpdateMaintenance(long p_REPAIR_MASTER_ID, int p_DISTRIBUTOR_ID, int p_STORE_ID,
            string p_REMARKS, int p_PRINCIPAL_ID, int p_USER_ID, bool p_IS_DELETED)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdateREPAIR_MASTER mMaterialMaster = new spUpdateREPAIR_MASTER();
                mMaterialMaster.Connection = mConnection;
                mMaterialMaster.Transaction = mTransaction;
                mMaterialMaster.REPAIR_MASTER_ID = p_REPAIR_MASTER_ID;
                mMaterialMaster.DISTRIBUTOR_ID = p_DISTRIBUTOR_ID;
                mMaterialMaster.STORE_ID = p_STORE_ID;
                mMaterialMaster.REMARKS = p_REMARKS;
                mMaterialMaster.PRINCIPAL_ID = p_PRINCIPAL_ID;
                mMaterialMaster.USER_ID = p_USER_ID;
                mMaterialMaster.IS_DELETED = p_IS_DELETED;
                mMaterialMaster.ExecuteQuery();

                mTransaction.Commit();
                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                return false;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                }
            }

        }

        #endregion

        #endregion

        #endregion
    }
}
