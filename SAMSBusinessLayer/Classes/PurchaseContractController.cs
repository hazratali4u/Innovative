using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;
using SAMSDatabaseLayer.Classes;

namespace SAMSBusinessLayer.Classes
{
    /// <summary>
    /// Collection Class for Basket
    /// <example>
    /// <list type="bullet">
    /// <term>
    /// Basket_ID
    /// </term>
    /// <item>
    /// Promotion_ID
    /// </item>
    /// <item>
    /// Scheme_ID etc
    /// </item>
    /// </list>
    /// </example>
    /// </summary>
	public class PurchaseContractController
	{
		#region Constructor

        /// <summary>
        /// Constructor for Basket_Collection
        /// </summary>
		public PurchaseContractController()
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
        public DataTable SelectPURCHASE_CONTRACT(int p_TYPE_ID, int p_STATUS_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_CONTRACT mRequisition = new spSelectPURCHASE_CONTRACT();
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
        public DataSet SelectPURCHASE_CONTRACT_ByID(int p_TYPE_ID, int p_PURCHASE_CONTRACT_ID)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPURCHASE_CONTRACT mRequisition = new spSelectPURCHASE_CONTRACT();
                mRequisition.Connection = mConnection;
                mRequisition.TYPE_ID = p_TYPE_ID;
                mRequisition.PURCHASE_CONTRACT_ID = p_PURCHASE_CONTRACT_ID;
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
        public bool InsertPurchaseContract(int  p_SUPPLIER_ID ,int p_LOCATION_ID,DateTime p_PURCHASSE_DATE ,DateTime p_DELIVERY_DATE ,string p_REMARKS ,int p_USER_ID,int p_COMPANYID,DateTime p_DOCUMENT_DATE, DataTable dtProducts,DataTable dtProductQuality)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPURCHASE_CONTRACT_MASTER mPurchaseMaster = new spInsertPURCHASE_CONTRACT_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.COMPANYID = p_COMPANYID;
                mPurchaseMaster.DELIVERY_DATE = p_DELIVERY_DATE;
                mPurchaseMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mPurchaseMaster.LOCATION_ID = p_LOCATION_ID;
                mPurchaseMaster.PURCHASSE_DATE = p_PURCHASSE_DATE;
                mPurchaseMaster.SUPPLIER_ID = p_SUPPLIER_ID;
                mPurchaseMaster.USER_ID = p_USER_ID;
                mPurchaseMaster.REMARKS = p_REMARKS;
                mPurchaseMaster.ExecuteQuery();

                spInsertPURCHASE_CONTRACT_DETAIL mPurchaseDetail = new spInsertPURCHASE_CONTRACT_DETAIL();
                mPurchaseDetail.Connection = mConnection;
                mPurchaseDetail.Transaction = mTransaction;


                spInsertPURCHASE_CONTRACT_QUALITY mPurchaseQuality = new spInsertPURCHASE_CONTRACT_QUALITY();
                mPurchaseQuality.Connection = mConnection;
                mPurchaseQuality.Transaction = mTransaction;

                foreach (DataRow dr in dtProducts.Rows)
                {
                    mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                    mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                    mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                    mPurchaseDetail.PURCHASE_CONTRACT_ID = mPurchaseMaster.PURCHASE_CONTRACT_ID;
                    mPurchaseDetail.QUANTITY = decimal.Parse(dr["QUANTITY"].ToString());
                    mPurchaseDetail.ExecuteQuery();

                    foreach (DataRow drQuality in dtProductQuality.Rows)
                    {
                        if (int.Parse(drQuality["SKU_ID"].ToString()) == int.Parse(dr["SKU_ID"].ToString()))
                        {
                        mPurchaseQuality.PURCHASE_CONTRACT_DETAIL_ID = mPurchaseDetail.PURCHASE_CONTRACT_DETAIL_ID;
                        mPurchaseQuality.PURCHASE_CONTRACT_ID = mPurchaseMaster.PURCHASE_CONTRACT_ID;
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
        public bool UpdatePurchaseContract(int p_PURCHASE_CONTRACT_ID, int p_SUPPLIER_ID, int p_LOCATION_ID, DateTime p_PURCHASSE_DATE, DateTime p_DELIVERY_DATE, string p_REMARKS, int p_USER_ID, int p_COMPANYID, DateTime p_DOCUMENT_DATE, DataTable dtProducts, DataTable dtProductQuality, bool p_IS_DELETED)
        {
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spUpdatePURCHASE_CONTRACT_MASTER mPurchaseMaster = new spUpdatePURCHASE_CONTRACT_MASTER();
                mPurchaseMaster.Connection = mConnection;
                mPurchaseMaster.Transaction = mTransaction;
                mPurchaseMaster.PURCHASE_CONTRACT_ID = p_PURCHASE_CONTRACT_ID;
                mPurchaseMaster.COMPANYID = p_COMPANYID;
                mPurchaseMaster.DELIVERY_DATE = p_DELIVERY_DATE;
                mPurchaseMaster.LOCATION_ID = p_LOCATION_ID;
                mPurchaseMaster.PURCHASSE_DATE = p_PURCHASSE_DATE;
                mPurchaseMaster.SUPPLIER_ID = p_SUPPLIER_ID;
                mPurchaseMaster.USER_ID = p_USER_ID;
                mPurchaseMaster.REMARKS = p_REMARKS;
                mPurchaseMaster.IS_DELETED = p_IS_DELETED;
                mPurchaseMaster.ExecuteQuery();
                if (p_IS_DELETED == false)
                {
                    spInsertPURCHASE_CONTRACT_DETAIL mPurchaseDetail = new spInsertPURCHASE_CONTRACT_DETAIL();
                    mPurchaseDetail.Connection = mConnection;
                    mPurchaseDetail.Transaction = mTransaction;


                    spInsertPURCHASE_CONTRACT_QUALITY mPurchaseQuality = new spInsertPURCHASE_CONTRACT_QUALITY();
                    mPurchaseQuality.Connection = mConnection;
                    mPurchaseQuality.Transaction = mTransaction;

                    foreach (DataRow dr in dtProducts.Rows)
                    {
                        mPurchaseDetail.AMOUNT = decimal.Parse(dr["AMOUNT"].ToString());
                        mPurchaseDetail.PRICE = decimal.Parse(dr["PRICE"].ToString());
                        mPurchaseDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                        mPurchaseDetail.PURCHASE_CONTRACT_ID = p_PURCHASE_CONTRACT_ID;
                        mPurchaseDetail.QUANTITY = decimal.Parse(dr["QUANTITY"].ToString());
                        mPurchaseDetail.ExecuteQuery();

                        foreach (DataRow drQuality in dtProductQuality.Rows)
                        {
                            if (int.Parse(drQuality["SKU_ID"].ToString()) == int.Parse(dr["SKU_ID"].ToString()))
                            {
                                mPurchaseQuality.PURCHASE_CONTRACT_DETAIL_ID = mPurchaseDetail.PURCHASE_CONTRACT_DETAIL_ID;
                                mPurchaseQuality.PURCHASE_CONTRACT_ID = p_PURCHASE_CONTRACT_ID;
                                mPurchaseQuality.QUALITY_DESCRIPTION = drQuality["QUALITY_DESCRIPTION"].ToString();
                                mPurchaseQuality.QUALITY_VALUE = drQuality["QUALITY_VALUE"].ToString();
                                mPurchaseQuality.SKU_ID = int.Parse(drQuality["SKU_ID"].ToString());
                                mPurchaseQuality.ExecuteQuery();
                            }
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
	}
}
