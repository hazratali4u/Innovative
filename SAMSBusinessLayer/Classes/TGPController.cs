using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;
using SAMSDatabaseLayer.Classes;

namespace SAMSBusinessLayer.Classes
{
	/// <summary>
	/// Class For Account Head Related Tasks
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// Insert Account Head
    /// </item>
    /// <term>
    /// Update Account Head
    /// </term>
    /// <item>
    /// Get Account Head
    /// </item>
    /// <item>
    /// Assigns/UnAssings Account Head To Principal
    /// </item>
    /// </list>
    /// </example>
	/// </summary>
	public class TGPController  
	{
		#region Constructor

        /// <summary>
        /// Constructor for AccountHeadController
        /// </summary>
        public TGPController()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#endregion

		#region Public Methods

        #region Select

        public DataTable UspSelectGATEPASS(int p_USER_ID, int p_TGP_ID, int p_TYPE_ID, int p_PAGE, int p_PAGESIZE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spSelectGATEPASSVIEW mProgram = new spSelectGATEPASSVIEW();
                mProgram.Connection = mConnection;

                mProgram.USER_ID = p_USER_ID;
                mProgram.TGP_ID = p_TGP_ID;
                mProgram.TYPE_ID = p_TYPE_ID;
                mProgram.Page = p_PAGE;
                mProgram.PageSize = p_PAGESIZE;

                DataTable dt = mProgram.ExecuteTable();
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

        public DataTable UspSelectGATEPASS(int p_TGP_ID, int p_TYPE_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spSelectGATEPASS mProgram = new spSelectGATEPASS();
                mProgram.Connection = mConnection;

                mProgram.TGP_ID = p_TGP_ID;
                mProgram.TYPE_ID = p_TYPE_ID;
 
                DataTable dt = mProgram.ExecuteTable();
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
        public DataTable UspSelectINWARDGATEPASS(long p_IGP_MASTER_ID, DateTime p_DOCUMENT_DATE,int p_TYPE_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spSelectINWARDGATEPASS mProgram = new spSelectINWARDGATEPASS();
                mProgram.Connection = mConnection;

                mProgram.IGP_MASTER_ID = p_IGP_MASTER_ID;
                mProgram.TYPE_ID = p_TYPE_ID;
                mProgram.DOCUMENT_DATE = p_DOCUMENT_DATE;

                DataTable dt = mProgram.ExecuteTable();
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
        public DataTable UspSelectINWARDGATEPASS(long p_IGP_MASTER_ID, int p_TYPE_ID)//For getting detail
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spSelectINWARDGATEPASS mProgram = new spSelectINWARDGATEPASS();
                mProgram.Connection = mConnection;

                mProgram.IGP_MASTER_ID = p_IGP_MASTER_ID;
                mProgram.TYPE_ID = p_TYPE_ID;
               

                DataTable dt = mProgram.ExecuteTable();
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
        public DataSet UspSelectGATEPASSREPORT(int p_USER_ID, int p_TGP_ID, int p_TYPE_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                
                spSelectGATEPASSVIEW mProgram = new spSelectGATEPASSVIEW();

                SAMSBusinessLayer.Reports.DsReport2 ds = new SAMSBusinessLayer.Reports.DsReport2();
                mProgram.Connection = mConnection;
                mProgram.USER_ID = p_USER_ID;
                mProgram.TGP_ID = p_TGP_ID;
                mProgram.TYPE_ID = p_TYPE_ID;

                DataTable dt = mProgram.ExecuteTable();

                foreach (DataRow dr in dt.Rows)
                {
                    ds.Tables["spSelectGATEPASSVIEW"].ImportRow(dr);
                }

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
        public DataTable SearchGatePass(int p_TGP_MASTER_ID, int p_USER_ID, int p_PageSize, int p_Page, int p_TYPE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                uspSearchGatePass mEnquiry = new uspSearchGatePass();
                mEnquiry.Connection = mConnection;

                mEnquiry.TGP_MASTER_ID = p_TGP_MASTER_ID;
                mEnquiry.USER_ID = p_USER_ID;
                //mEnquiry.TGP_TYPE = p_TGP_TYPE;
                mEnquiry.PageSize = p_PageSize;
                mEnquiry.Page = p_Page;
                mEnquiry.TYPE = p_TYPE;

                DataTable dt = mEnquiry.ExecuteTable();
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
        #region Insert, Update
        /// <summary>
        /// Inserts Account Head
        /// <remarks>
        /// Returns Inserted Account Head ID as String
        /// </remarks>
        /// </summary>
        /// <param name="p_Company_id">Company</param>
        /// <param name="p_Is_Active">Active/InActive</param>
        /// <param name="p_Time_Stamp">CreatedOn</param>
        /// <param name="p_Distributor_Id">Location</param>
        /// <param name="p_Account_Type_Id">Type</param>
        /// <param name="p_Account_Parent_id">Parent</param>
        /// <param name="p_Account_Name">Name</param>
        /// <param name="p_Account_Code">Code</param>
        /// <param name="p_Index">Category</param>
        /// <returns>Inserted Account Head ID as String</returns>
        public bool InsertTGP(int p_COMPANYID,int p_TGP_ID2, int p_SUPPLIER_ID, string p_GOODS_OUT_THROUGH, string p_TRANSPORT_NO, DateTime p_ISSUE_DATE, int p_USER_ID
            , DateTime p_RETURN_DATE, DataTable dt_TGP_Detail, int p_TGP_TYPE)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTranscation = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTranscation = ProviderFactory.GetTransaction(mConnection);
                spInsertTGP_MASTER mAccountHead = new spInsertTGP_MASTER();
                mAccountHead.Connection = mConnection;
                mAccountHead.Transaction = mTranscation;

                mAccountHead.COMPANYID = p_COMPANYID;
                mAccountHead.GOODS_OUT_THROUGH = p_GOODS_OUT_THROUGH;
                mAccountHead.ISSUE_DATE = p_ISSUE_DATE;
                mAccountHead.RETURN_DATE = p_RETURN_DATE;
                mAccountHead.SUPPLIER_ID = p_SUPPLIER_ID;
                mAccountHead.USER_ID = p_USER_ID;
                mAccountHead.TRANSPORT_NO = p_TRANSPORT_NO;
                mAccountHead.TGP_ID2 = p_TGP_ID2;
                mAccountHead.TGP_TYPE = p_TGP_TYPE;

                int p_TGP_ID= mAccountHead.ExecuteQuery();

                spInsertTGP_DETAIL TGP_Detail = new spInsertTGP_DETAIL();
                TGP_Detail.Connection = mConnection;
                TGP_Detail.Transaction = mTranscation;
                foreach (DataRow dr in dt_TGP_Detail.Rows)
                {
                    TGP_Detail.QUANTITY = Convert.ToDecimal(dr["QUANTITY"]);
                    TGP_Detail.REMARKS = dr["REMARKS"].ToString();
                    TGP_Detail.SKU_ID = Convert.ToInt32(dr["SKU_ID"]);
                    TGP_Detail.TGP_ID = mAccountHead.TGP_ID;
                    TGP_Detail.ExecuteQuery();
                }
                mTranscation.Commit();
                return true;
            }
			catch(Exception exp)
			{
				ExceptionPublisher.PublishException(exp);
                throw;
			}
			finally
			{
				if(mConnection != null && mConnection.State == ConnectionState.Open)
				{
					mConnection.Close();
				}
			}
		}

        public bool UpdateTGP(int p_TGP_ID)
        {

            IDbConnection mConnection = null;

            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spUpdateTGP_MASTER TGP_Master = new spUpdateTGP_MASTER();
                TGP_Master.Connection = mConnection;
                TGP_Master.TGP_ID = p_TGP_ID;

                TGP_Master.ExecuteQuery();
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
        
        public long InsertIGP(int p_COMPANYID, long p_IGP_ID2, int p_TGP_ID, int p_SUPPLIER_ID, string p_GOODS_OUT_THROUGH, string p_TRANSPORT_NO, DateTime p_DOCUMENT_DATE, int p_USER_ID
           , int p_IGP_TYPE)
        {
            IDbConnection mConnection = null;
           
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                

                spInsertIGP_MASTER mAccountHead = new spInsertIGP_MASTER();

                mAccountHead.Connection = mConnection;

                mAccountHead.COMPANYID = p_COMPANYID;
                mAccountHead.GOODS_OUT_THROUGH = p_GOODS_OUT_THROUGH;
                mAccountHead.SUPPLIER_ID = p_SUPPLIER_ID;
                mAccountHead.USER_ID = p_USER_ID;
                mAccountHead.TRANSPORT_NO = p_TRANSPORT_NO;
                mAccountHead.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mAccountHead.IGP_MASTER_ID2 = p_IGP_ID2;
                mAccountHead.TGP_ID = p_TGP_ID;
                mAccountHead.IGP_TYPE = p_IGP_TYPE;

                long p_IGP_ID = mAccountHead.ExecuteQuery();
                return p_IGP_ID;
            }
            catch (Exception exp)
            {
              
                ExceptionPublisher.PublishException(exp);
                throw;
            }
            finally
            {
                if (mConnection != null && mConnection.State == ConnectionState.Open)
                {
                    mConnection.Close();
                    
                }
            }
        }

        public bool InsertIGP_DETAIL(long p_IGP_MASTER_ID, int p_TGP_ID, decimal p_QUANTITY, decimal p_QUANTITY_REC, int p_SKU_ID, DateTime p_TIME_STAMP, string p_REMARKS)
        {

            IDbConnection mConnection = null;

            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spInsertIGP_DETAIL TGP_Detail = new spInsertIGP_DETAIL();
                TGP_Detail.Connection = mConnection;
                TGP_Detail.TGP_ID = p_TGP_ID;
                TGP_Detail.QUANTITY = p_QUANTITY;
                TGP_Detail.QUANTITY_RECIEVED = p_QUANTITY_REC;
                TGP_Detail.SKU_ID = p_SKU_ID;
                TGP_Detail.IGP_MASTER_ID = p_IGP_MASTER_ID;
                TGP_Detail.TIME_STAMP = p_TIME_STAMP;
                TGP_Detail.REMARKS = p_REMARKS;

                TGP_Detail.ExecuteQuery();
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
        #endregion
    }
}
