using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;
using SAMSDatabaseLayer.Classes;

namespace SAMSBusinessLayer.Classes
{
    /// <summary>
    /// Class For SKU Related Tasks
    /// <example>
    /// <list type="bullet">
    /// <item>
    /// Insert SKU
    /// </item>
    /// <term>
    /// Update SKU
    /// </term>
    /// <item>
    /// Get SKU
    /// </item>
    /// </list>
    /// </example>
    /// </summary>
	public class SkuController
	{	
		#region Constructors

        /// <summary>
        /// Constructor For SkuController
        /// </summary>
		public SkuController()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
        #endregion
				
		#region public Methods

        #region Select

        /// <summary>
        /// Gets SKUS Data
        /// </summary>
        /// <remarks>
        /// Returns SKUS Data as Datatable
        /// </remarks>
        /// <param name="p_company_id">Principal</param>
        /// <param name="p_division_id">Dicision</param>
        /// <param name="p_category_id">Category</param>
        /// <param name="p_brand_id">Brand</param>
        /// <param name="Companyid">Company</param>
        /// <returns>SKUS Data as Datatable</returns>
        public DataTable SelectSkuInfo(int p_company_id, int p_division_id, int p_category_id, int p_brand_id, int Companyid)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspSelectSkuInfo mspSelectSkuInfo = new uspSelectSkuInfo();
                mspSelectSkuInfo.brand_id = p_brand_id;
                mspSelectSkuInfo.category_id = p_category_id;
                mspSelectSkuInfo.Principal_id = p_company_id;
                mspSelectSkuInfo.Connection = mConnection;
                mspSelectSkuInfo.division_id = p_division_id;
                mspSelectSkuInfo.Company_id = Companyid;

                DataTable dt = mspSelectSkuInfo.ExecuteTable();

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

        public DataTable SelectOpeningInventory(int p_OPENING_INVENTORY_ID, int p_SKU_ID, int p_COMPANY_ID, int p_CategoryID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectOPENING_INVENTORY mspSelectSkuInfo = new spSelectOPENING_INVENTORY();
                mspSelectSkuInfo.Connection = mConnection;
                mspSelectSkuInfo.COMPANY_ID = p_COMPANY_ID;
                mspSelectSkuInfo.OPENING_INVENTORY_ID = p_OPENING_INVENTORY_ID;
                mspSelectSkuInfo.SKU_ID = p_SKU_ID;
                mspSelectSkuInfo.CategoryID = p_CategoryID;
                DataTable dt = mspSelectSkuInfo.ExecuteTable();
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
        /// Gets SKU Data
        /// </summary>
        /// <remarks>
        /// Returns SKUS Data as Datatable
        /// </remarks>
        /// <param name="p_SKU_Id">SKU</param>
        /// <param name="Companyid">Company</param>
        /// <returns>SKUS Data as Datatable</returns>
        public DataTable SelectSkuData(int p_SKU_Id, int Companyid)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectSKUS mSkuInfo = new spSelectSKUS();
                mSkuInfo.Connection = mConnection;
                mSkuInfo.SKU_ID = p_SKU_Id;
                mSkuInfo.COMPANY_ID = Companyid;
                DataTable dt = mSkuInfo.ExecuteTable();
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
        /// Gets SKU UOM
        /// </summary>
        /// <param name="p_UOM_Id">UOM</param>
        /// <param name="p_UOM_Desc">Description</param>
        /// <returns>SKU UOM</returns>
        public DataTable SelectUOMs(int p_UOM_Id, string p_UOM_Desc)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spSelectUOMS mUOMs = new spSelectUOMS();
                mUOMs.Connection = mConnection;

                mUOMs.UOM_ID = p_UOM_Id;
                mUOMs.UOM_DESC = p_UOM_Desc;
                mUOMs.TIME_STAMP = Constants.DateNullValue;
                mUOMs.STATUS = Constants.IntNullValue;

                DataTable dt = mUOMs.ExecuteTable();
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
        /// Gets SKU Data
        /// </summary>
        /// <remarks>
        /// Returns SKUS Data as Datatable
        /// </remarks>
        /// <param name="p_FINISHED_SKU_ID">Finished SKU</param>
        /// <returns>SKUS Data as Datatable</returns>
        public DataTable GetFinshedDetail(int p_FINISHED_SKU_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetFinishedDetail mSkuInfo = new uspGetFinishedDetail();
                mSkuInfo.Connection = mConnection;
                mSkuInfo.FINISHED_SKU_ID = p_FINISHED_SKU_ID;
                DataTable dt = mSkuInfo.ExecuteTable();
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
        /// Gets SKU Data
        /// </summary>
        /// <remarks>
        /// Returns SKUS Data as Datatable
        /// </remarks>
        /// <param name="p_SKU_Id">SKU</param>
        /// <param name="Companyid">Company</param>
        /// <returns>SKUS Data as Datatable</returns>
        public DataTable SelectSkuData(int p_SKU_Id, int Companyid, int p_Principal_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectSKUS mSkuInfo = new spSelectSKUS();
                mSkuInfo.Connection = mConnection;
                mSkuInfo.SKU_ID = p_SKU_Id;
                mSkuInfo.COMPANY_ID = Companyid;
                mSkuInfo.PRINCIPAL_ID = p_Principal_ID;
                DataTable dt = mSkuInfo.ExecuteTable();
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

        public DataTable GetSKUQuality(int p_SKU_ID, long p_SKU_QUALITY_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspGetSKUQuality mspSelectSkuInfo = new uspGetSKUQuality();
                mspSelectSkuInfo.Connection = mConnection;
                mspSelectSkuInfo.SKU_QUALITY_ID = p_SKU_QUALITY_ID;
                mspSelectSkuInfo.SKU_ID = p_SKU_ID;
                
                DataTable dt = mspSelectSkuInfo.ExecuteTable();

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
        /// Gets SKUS Data
        /// </summary>
        /// <remarks>
        /// Returns SKUS Data as Datatable
        /// </remarks>
        /// <param name="p_company_id">Principal</param>
        /// <param name="p_division_id">Dicision</param>
        /// <param name="p_category_id">Category</param>
        /// <param name="p_brand_id">Brand</param>
        /// <param name="Companyid">Company</param>
        /// <returns>SKUS Data as Datatable</returns>
        public DataTable SelectSku_AssignUnAssign(int p_Supplier_ID, int p_category_id, int p_Companyid, int p_Type)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectProductAssignUnAssign mspSelectSkuInfo = new spSelectProductAssignUnAssign();
                mspSelectSkuInfo.Connection = mConnection;
                mspSelectSkuInfo.category_id = p_category_id;
                mspSelectSkuInfo.Supplier_ID = p_Supplier_ID;
                mspSelectSkuInfo.Company_id = p_Companyid;
                mspSelectSkuInfo.TypeID = p_Type;
                DataTable dt = mspSelectSkuInfo.ExecuteTable();
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
        /// Insert SKU
        /// </summary>
        /// <remarks>
        /// Returns Inserted SKU ID as String
        /// </remarks>
        /// <param name="p_IsExempted">IsExempted</param>
        /// <param name="p_IsActive">IsActive</param>
        /// <param name="p_Gst_On">GSTOn</param>
        /// <param name="p_Company_Id">Principal</param>
        /// <param name="p_Division_Id">Division</param>
        /// <param name="p_Category_Id">Category</param>
        /// <param name="p_Brand_Id">Brand</param>
        /// <param name="p_Variant_Id">Variant</param>
        /// <param name="p_GST_Rate_Reg">GSTReg</param>
        /// <param name="p_GST_Rate_Unreg">GSTUnReg</param>
        /// <param name="p_Units_In_Case">Units</param>
        /// <param name="p_Sku_Code">Code</param>
        /// <param name="p_Sku_Name">Name</param>
        /// <param name="p_Ip_Address">Address</param>
        /// <param name="p_packSize">Packing</param>
        /// <param name="p_UserId">InsertedBy</param>
        /// <param name="Companyid">Company</param>
        /// <returns>Inserted SKU ID as String</returns>
        public string InsertSKUS(bool p_IsExempted, bool p_IsActive, char p_Gst_On, int p_Company_Id, int p_Division_Id, int p_Category_Id, int p_Brand_Id, int p_Variant_Id, decimal p_GST_Rate_Reg, decimal p_GST_Rate_Unreg, short p_Units_In_Case, string p_Sku_Code, string p_Sku_Name, string p_Ip_Address, string p_packSize, int p_UserId, int Companyid, int p_MIN_STOCK, int p_UOM_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertSKUS mSkus = new spInsertSKUS();

                mSkus.Connection = mConnection;
                mSkus.PRINCIPAL_ID = p_Company_Id;
                mSkus.ISEXEMPTED = p_IsExempted;
                mSkus.ISACTIVE = p_IsActive;
                mSkus.GST_ON = p_Gst_On;
                mSkus.COMPANY_ID = Companyid;
                mSkus.DIVISION_ID = p_Division_Id;
                mSkus.BRAND_ID = p_Brand_Id;
                mSkus.CATEGORY_ID = p_Category_Id;
                if (!p_IsExempted)
                {
                    mSkus.GST_RATE_REG = p_GST_Rate_Reg;
                    mSkus.GST_RATE_UNREG = p_GST_Rate_Unreg;
                }
                else
                {
                    mSkus.GST_RATE_REG = 0;
                    mSkus.GST_RATE_UNREG = 0;
                }
                mSkus.UNITS_IN_CASE = p_Units_In_Case;
                mSkus.SKU_NAME = p_Sku_Name;
                mSkus.SKU_CODE = p_Sku_Code;
                mSkus.TIME_STAMP = System.DateTime.Now;
                mSkus.LASTUPDATE_DATE = System.DateTime.Now;
                mSkus.IP_ADDRESS = p_Ip_Address;
                mSkus.PACKSIZE = p_packSize;
                mSkus.USER_ID = p_UserId;
                mSkus.MIN_STOCK = p_MIN_STOCK;
                mSkus.UOM_ID = p_UOM_ID;
                mSkus.ExecuteQuery();

                return mSkus.SKU_ID.ToString();

            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                return exp.Message;
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
        /// Updates SKU
        /// </summary>
        /// <remarks>
        /// Returns "Record Updated" On Success And Null On Failure
        /// </remarks>
        /// <param name="p_IsExempted">IsExempted</param>
        /// <param name="p_IsActive">IsActive</param>
        /// <param name="p_Gst_On">GSTOn</param>
        /// <param name="p_Company_Id">Principal</param>
        /// <param name="p_Division_Id">Division</param>
        /// <param name="p_Category_Id">Category</param>
        /// <param name="p_Brand_Id">Brand</param>
        /// <param name="p_Variant_Id">Variant</param>
        /// <param name="p_GST_Rate_Reg">GSTReg</param>
        /// <param name="p_GST_Rate_Unreg">GSTUnReg</param>
        /// <param name="p_Units_In_Case">Units</param>
        /// <param name="p_Sku_Id">SKU</param>
        /// <param name="p_Sku_Code">Code</param>
        /// <param name="p_Sku_Name">Name</param>
        /// <param name="p_Ip_Address">Address</param>
        /// <param name="p_packSize">Packing</param>
        /// <param name="p_UserId">InsertedBy</param>
        /// <param name="CompanyId">Company</param>
        /// <returns>"Record Updated" On Success And Null On Failure</returns>
        public string UpdateSKUS(bool p_IsExempted, bool p_IsActive, char p_Gst_On, int p_Company_Id, int p_Division_Id, int p_Category_Id, int p_Brand_Id, int p_Variant_Id, decimal p_GST_Rate_Reg, decimal p_GST_Rate_Unreg, short p_Units_In_Case, int p_Sku_Id, string p_Sku_Code, string p_Sku_Name, string p_Ip_Address, string p_packSize, int p_UserId, int CompanyId, int p_MIN_STOCK, int p_UOM_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateSKUS mSkus = new spUpdateSKUS();

                mSkus.Connection = mConnection;
                mSkus.ISEXEMPTED = p_IsExempted;
                mSkus.ISACTIVE = p_IsActive;
                mSkus.GST_ON = p_Gst_On;
                mSkus.COMPANY_ID = CompanyId;
                mSkus.PRINCIPAL_ID = p_Company_Id;
                mSkus.DIVISION_ID = p_Division_Id;
                mSkus.BRAND_ID = p_Brand_Id;
                mSkus.CATEGORY_ID = p_Category_Id;
                if (!p_IsExempted)
                {
                    mSkus.GST_RATE_REG = p_GST_Rate_Reg;
                    mSkus.GST_RATE_UNREG = p_GST_Rate_Unreg;
                }
                else
                {
                    mSkus.GST_RATE_REG = 0;
                    mSkus.GST_RATE_UNREG = 0;
                }
                mSkus.UNITS_IN_CASE = p_Units_In_Case;
                mSkus.SKU_ID = p_Sku_Id;
                mSkus.SKU_NAME = p_Sku_Name;
                mSkus.SKU_CODE = p_Sku_Code;
                mSkus.TIME_STAMP = System.DateTime.Now;
                mSkus.LASTUPDATE_DATE = System.DateTime.Now;
                mSkus.IP_ADDRESS = p_Ip_Address;
                mSkus.PACKSIZE = p_packSize;
                mSkus.USER_ID = p_UserId;
                mSkus.MIN_STOCK = p_MIN_STOCK;
                mSkus.UOM_ID = p_UOM_ID;

                mSkus.ExecuteQuery();
                return "Record Updated";

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

        public bool InsertFinishedSKU(int p_FINISHED_SKU_ID,DateTime p_DOCUMENT_DATE,int p_USER_ID, DataTable dtFinishedDetail)
        {

            IDbConnection mConnection = null;
            IDbTransaction mTransaction = null;            
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTransaction = ProviderFactory.GetTransaction(mConnection);

                uspInsertFinishedMaster mFinishedMaster = new uspInsertFinishedMaster();
                mFinishedMaster.Connection = mConnection;
                mFinishedMaster.Transaction = mTransaction;

                //------------Insert Finished Goods Master----------

                if (dtFinishedDetail.Rows.Count > 0)
                {
                    mFinishedMaster.FINISHED_SKU_ID = p_FINISHED_SKU_ID;
                    mFinishedMaster.DOCUMENT_DATE = p_DOCUMENT_DATE;                    
                    mFinishedMaster.USER_ID = p_USER_ID;

                    mFinishedMaster.ExecuteQuery();

                    //----------------Insert Finished Goods Detail-------------
                    uspInsertFinishedDetail mFinishedDetail = new uspInsertFinishedDetail();
                    mFinishedDetail.Connection = mConnection;
                    mFinishedDetail.Transaction = mTransaction;

                    foreach (DataRow dr in dtFinishedDetail.Rows)
                    {                        
                        mFinishedDetail.FINISHED_GOOD_MASTER_ID = mFinishedMaster.FINISHED_GOOD_MASTER_ID;
                        mFinishedDetail.SKU_ID = int.Parse(dr["SKU_ID"].ToString());
                        mFinishedDetail.QUANTITY = int.Parse(dr["QUANTITY"].ToString());
                        mFinishedDetail.ExecuteQuery();                       
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

        public bool InsertSKUQuality(int p_SKU_ID, string p_QUALITY_DESCRIPTION, string p_QUALITY_VALUE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspInsertSKUQuality mSkus = new uspInsertSKUQuality();

                mSkus.Connection = mConnection;
                mSkus.SKU_ID = p_SKU_ID;
                mSkus.QUALITY_DESCRIPTION = p_QUALITY_DESCRIPTION;
                mSkus.QUALITY_VALUE = p_QUALITY_VALUE;
                mSkus.ExecuteQuery();

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

        public bool UpdateSKUQuality(long p_SKU_QUALITY_ID, int p_SKU_ID, string p_QUALITY_DESCRIPTION, string p_QUALITY_VALUE, bool p_IS_ACTIVE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                uspUpdateSKUQuality mSkus = new uspUpdateSKUQuality();
                mSkus.Connection = mConnection;

                mSkus.SKU_QUALITY_ID = p_SKU_QUALITY_ID;
                mSkus.SKU_ID = p_SKU_ID;
                mSkus.QUALITY_DESCRIPTION = p_QUALITY_DESCRIPTION;
                mSkus.QUALITY_VALUE = p_QUALITY_VALUE;
                mSkus.IS_ACTIVE = p_IS_ACTIVE;

                mSkus.ExecuteQuery();
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

        public bool InsertOpening_Inventory(int p_SKU_ID, int p_COMPANY_ID, decimal p_QUANTITY)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertOPENING_INVENTORY mSkus = new spInsertOPENING_INVENTORY();
                mSkus.Connection = mConnection;
                mSkus.COMPANY_ID = p_COMPANY_ID;
                mSkus.QUANTITY = p_QUANTITY;
                mSkus.SKU_ID = p_SKU_ID;
                return mSkus.ExecuteQuery();
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

        public bool UpdateOPENING_INVENTORY(int p_OPENING_INVENTORY_ID, int p_SKU_ID, int p_COMPANY_ID, decimal p_QUANTITY, bool p_IS_ACTIVE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateOPENING_INVENTORY mSkus = new spUpdateOPENING_INVENTORY();
                mSkus.Connection = mConnection;

                mSkus.COMPANY_ID = p_COMPANY_ID;
                mSkus.SKU_ID = p_SKU_ID;
                mSkus.IS_ACTIVE = p_IS_ACTIVE;
                mSkus.OPENING_INVENTORY_ID = p_OPENING_INVENTORY_ID;
                mSkus.QUANTITY = p_QUANTITY;
                mSkus.SKU_ID = p_SKU_ID;
                mSkus.ExecuteQuery();
                return true;
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

        #endregion

        #endregion
    }
}
