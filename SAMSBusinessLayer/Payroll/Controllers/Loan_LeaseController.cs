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
    public class Loan_LeaseController
    {
        /// <summary>
        /// Constructor for CompanyController Class
        /// </summary>
        public Loan_LeaseController()
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
        public DataTable SelectLoanType(int p_CompanyID, int p_LoanTypeID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectLoanTypes objEmployee = new spSelectLoanTypes();
                objEmployee.Connection = mConnection;
                objEmployee.CompanyID = p_CompanyID;
                objEmployee.LoanTypeID = p_LoanTypeID;
                DataTable dt = objEmployee.ExecuteTable();
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
        
        public DataTable SelectAssetType(int p_CompanyID, int p_AssetTypeID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectAssetTypes objEmployee = new spSelectAssetTypes();
                objEmployee.Connection = mConnection;
                objEmployee.CompanyID = p_CompanyID;
                objEmployee.AssetTypeID = p_AssetTypeID;
                DataTable dt = objEmployee.ExecuteTable();
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
        
        public DataTable SelectAsset(int p_CompanyID, int p_AssetTypeID, int p_AssetID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectAssets objEmployee = new spSelectAssets();
                objEmployee.Connection = mConnection;
                objEmployee.CompanyID = p_CompanyID;
                objEmployee.AssetTypeID = p_AssetTypeID;
                objEmployee.AssetID = p_AssetID;
                DataTable dt = objEmployee.ExecuteTable();
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
        
        public int InsertLoan_Lease(Loan_LeaseModel ll, DataTable dtInstallments, DataTable dtManagers)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTranscation = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTranscation = ProviderFactory.GetTransaction(mConnection);
                spInsertLoan_Lease loan_lease = new spInsertLoan_Lease();
                loan_lease.Transaction = mTranscation;
                loan_lease.Connection = mConnection;
                loan_lease.Amount = ll.Amount;
                loan_lease.ApprovedDate = ll.ApprovedDate;
                loan_lease.ApprovelID = ll.ApprovelID;
                loan_lease.Comments = ll.Comments;
                loan_lease.CompanyID = ll.CompanyID;
                loan_lease.DateFrom = ll.DateFrom;
                loan_lease.DateTo = ll.DateTo;
                loan_lease.Document_Date = ll.Document_Date;
                loan_lease.EmployeeID = ll.EmployeeID;
                loan_lease.Loan_Lease_TypeID = ll.Loan_Lease_TypeID;
                loan_lease.NoOfMonth = ll.NoOfMonth;
                loan_lease.TypeID = ll.TypeID;
                loan_lease.User_ID = ll.User_ID;
                loan_lease.AssetID = ll.AssetID;
                loan_lease.CompanyContributation = ll.CompanyContributation;
                loan_lease.EmployeeContributation = ll.EmployeeContributation;
                int Loan_LeaseID = loan_lease.ExecuteQuery();
                spInsertInstallments ins = new spInsertInstallments();
                ins.Connection = mConnection;
                ins.Transaction = mTranscation;
                foreach (DataRow dr in dtInstallments.Rows)
                {
                    ins.InstallmentDate = DateTime.Parse(dr["InstallmentDate"].ToString());
                    ins.Loan_LeaseID = Loan_LeaseID;
                    ins.AmountPaid = decimal.Parse(dr["AmountPaid"].ToString());
                    ins.AmountToBePaid = decimal.Parse(dr["AmountToBePaid"].ToString());
                    ins.ExecuteQuery();
                }
                spInsertLoanApproverManager lam = new spInsertLoanApproverManager();
                lam.Connection = mConnection;
                lam.Transaction = mTranscation;
                foreach (DataRow dr in dtManagers.Rows)
                {
                    lam.LoanID = Loan_LeaseID;
                    lam.ManagerID = Int32.Parse(dr["ManagerID"].ToString());
                    lam.ExecuteQuery();
                }
                mTranscation.Commit();
                return Loan_LeaseID;
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
        
        public bool UpdateLoan_Lease(Loan_LeaseModel ll, DataTable dtInstallments, DataTable dtManagers)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTranscation = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTranscation = ProviderFactory.GetTransaction(mConnection);
                spUpdateLoan_Lease loan_lease = new spUpdateLoan_Lease();
                loan_lease.Transaction = mTranscation;
                loan_lease.Connection = mConnection;

                loan_lease.Loan_LeaseID = ll.Loan_LeaseID;
                loan_lease.Amount = ll.Amount;
                loan_lease.ApprovedDate = ll.ApprovedDate;
                loan_lease.ApprovelID = ll.ApprovelID;
                loan_lease.Comments = ll.Comments;
                loan_lease.CompanyID = ll.CompanyID;
                loan_lease.DateFrom = ll.DateFrom;
                loan_lease.DateTo = ll.DateTo;
                loan_lease.EmployeeID = ll.EmployeeID;
                loan_lease.Loan_Lease_TypeID = ll.Loan_Lease_TypeID;
                loan_lease.NoOfMonth = ll.NoOfMonth;
                loan_lease.TypeID = ll.TypeID;
                loan_lease.User_ID = ll.User_ID;
                loan_lease.AssetID = ll.AssetID;
                loan_lease.IS_DELETED = ll.IS_DELETED;
                loan_lease.IS_ACTIVE = ll.IS_ACTIVE;
                loan_lease.CompanyContributation = ll.CompanyContributation;
                loan_lease.EmployeeContributation = ll.EmployeeContributation;
                
                loan_lease.ExecuteQuery();


                spDeleteLoan_Lease loan_lease_del = new spDeleteLoan_Lease();
                loan_lease_del.Transaction = mTranscation;
                loan_lease_del.Connection = mConnection;                
                loan_lease_del.Loan_LeaseID = loan_lease.Loan_LeaseID;
                loan_lease_del.ExecuteQuery();


                spInsertInstallments ins = new spInsertInstallments();
                ins.Connection = mConnection;
                ins.Transaction = mTranscation;
                foreach (DataRow dr in dtInstallments.Rows)
                {
                    ins.InstallmentDate = DateTime.Parse(dr["InstallmentDate"].ToString());
                    ins.Loan_LeaseID = loan_lease.Loan_LeaseID;
                    ins.AmountPaid = decimal.Parse(dr["AmountPaid"].ToString());
                    ins.AmountToBePaid = decimal.Parse(dr["AmountToBePaid"].ToString());
                    ins.ExecuteQuery();
                }
                spInsertLoanApproverManager lam = new spInsertLoanApproverManager();
                lam.Connection = mConnection;
                lam.Transaction = mTranscation;
                foreach (DataRow dr in dtManagers.Rows)
                {
                    lam.LoanID = loan_lease.Loan_LeaseID;
                    lam.ManagerID = Int32.Parse(dr["ManagerID"].ToString());
                    lam.ExecuteQuery();
                }
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
        
        public bool UpdateLoan_Lease(Loan_LeaseModel ll)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTranscation = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTranscation = ProviderFactory.GetTransaction(mConnection);
                spUpdateLoan_Lease loan_lease = new spUpdateLoan_Lease();
                loan_lease.Transaction = mTranscation;
                loan_lease.Connection = mConnection;

                loan_lease.Loan_LeaseID = ll.Loan_LeaseID;
                loan_lease.Amount = ll.Amount;
                loan_lease.ApprovedDate = ll.ApprovedDate;
                loan_lease.ApprovelID = ll.ApprovelID;
                loan_lease.Comments = ll.Comments;
                loan_lease.CompanyID = ll.CompanyID;
                loan_lease.DateFrom = ll.DateFrom;
                loan_lease.DateTo = ll.DateTo;
                loan_lease.EmployeeID = ll.EmployeeID;
                loan_lease.Loan_Lease_TypeID = ll.Loan_Lease_TypeID;
                loan_lease.NoOfMonth = ll.NoOfMonth;
                loan_lease.TypeID = ll.TypeID;
                loan_lease.User_ID = ll.User_ID;
                loan_lease.AssetID = ll.AssetID;
                loan_lease.IS_DELETED = ll.IS_DELETED;
                loan_lease.IS_ACTIVE = ll.IS_ACTIVE;
                loan_lease.CompanyContributation = ll.CompanyContributation;
                loan_lease.EmployeeContributation = ll.EmployeeContributation;

                loan_lease.ExecuteQuery();
                
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
        
        public bool InsertAssets(AssetsModel model)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertAssets loan_lease = new spInsertAssets();
                loan_lease.Connection = mConnection;
                loan_lease.Amount = model.Amount;
                loan_lease.AssetTypeID = model.AssetTypeID;
                loan_lease.ChassisNo = model.ChassisNo;
                loan_lease.Color = model.Color;
                loan_lease.CompanyID = model.CompanyID;
                loan_lease.Document_Date = model.Document_Date;
                loan_lease.EngineNo = model.EngineNo;
                loan_lease.RegNo = model.RegNo;
                loan_lease.Make = model.Make;
                loan_lease.Model = model.Model;
                loan_lease.Remarks = model.Remarks;
                loan_lease.User_ID = model.User_ID;
                loan_lease.ExecuteQuery();
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
        
        public bool UpdateAssets(AssetsModel model)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spUpdateAssets loan_lease = new spUpdateAssets();
                loan_lease.Connection = mConnection;
                loan_lease.AssetID = model.AssetID;
                loan_lease.Amount = model.Amount;
                loan_lease.AssetTypeID = model.AssetTypeID;
                loan_lease.ChassisNo = model.ChassisNo;
                loan_lease.Color = model.Color;
                loan_lease.CompanyID = model.CompanyID;
                loan_lease.EngineNo = model.EngineNo;
                loan_lease.RegNo = model.RegNo;
                loan_lease.Make = model.Make;
                loan_lease.Model = model.Model;
                loan_lease.Remarks = model.Remarks;
                loan_lease.User_ID = model.User_ID;
                loan_lease.IS_ACTIVE = model.IS_ACTIVE;
                loan_lease.IS_DELETED = model.IS_DELETED;
                loan_lease.ExecuteQuery();
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
        
        public bool InsertLoanType(LoanModel ll)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTranscation = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTranscation = ProviderFactory.GetTransaction(mConnection);
                spInsertLoanTypes loan = new spInsertLoanTypes();
                loan.Transaction = mTranscation;
                loan.Connection = mConnection;
                loan.CompanyID = ll.CompanyID;
                loan.LoanTypeName = ll.LoanTypeName;
                loan.MaxAllow = ll.MaxAllow;
                loan.User_ID = ll.User_ID;
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
        
        public bool UpdateLoanType(LoanModel ll)
        {
            IDbConnection mConnection = null;
            IDbTransaction mTranscation = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                mTranscation = ProviderFactory.GetTransaction(mConnection);
                spUpdateLoanTypes loan = new spUpdateLoanTypes();
                loan.Transaction = mTranscation;
                loan.Connection = mConnection;
                loan.LoanTypeID = ll.LoanTypeID;                
                loan.LoanTypeName = ll.LoanTypeName;
                loan.MaxAllow = ll.MaxAllow;
                loan.User_ID = ll.User_ID;
                loan.IS_ACTIVE = ll.IS_ACTIVE;
                loan.IS_DELETED = ll.IS_DELETED;
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

        public DataTable GetLoan(int p_EmployeeLocationID, int p_DepartmentID, int p_EmployeeID, int p_TypeID, int p_Loan_Lease_TypeID, DateTime p_FROM_DATE, DateTime p_TO_DATE, int p_USER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spGetLoanReport objLoan = new spGetLoanReport();
                objLoan.Connection = mConnection;
                objLoan.EmployeeLocationID = p_EmployeeLocationID;
                objLoan.DepartmentID = p_DepartmentID;
                objLoan.EmployeeID = p_EmployeeID;
                objLoan.TypeID = p_TypeID;
                objLoan.Loan_Lease_TypeID = p_Loan_Lease_TypeID;
                objLoan.USER_ID = p_USER_ID;
                objLoan.FROM_DATE = p_FROM_DATE;
                objLoan.TO_DATE = p_TO_DATE;
                DataTable dt = objLoan.ExecuteTable();
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

        public DataTable SelectLoan_Lease(int p_CompanyID, int p_Loan_Lease_TypeID, int p_Loan_LeaseID, int p_TypeID, int p_USER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectLoan_Lease objEmployee = new spSelectLoan_Lease();
                objEmployee.Connection = mConnection;
                objEmployee.CompanyID = p_CompanyID;
                objEmployee.Loan_Lease_TypeID = p_Loan_Lease_TypeID;
                objEmployee.Loan_LeaseID = p_Loan_LeaseID;
                objEmployee.TypeID = p_TypeID;
                objEmployee.USER_ID = p_USER_ID;
                DataTable dt = objEmployee.ExecuteTable();
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
        
        public DataTable SelectLoan_Lease(int p_CompanyID, int p_Loan_Lease_TypeID, int p_Loan_LeaseID, int p_TypeID, DateTime p_Month, int p_USER_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectLoan_Lease objEmployee = new spSelectLoan_Lease();
                objEmployee.Connection = mConnection;
                objEmployee.CompanyID = p_CompanyID;
                objEmployee.Loan_Lease_TypeID = p_Loan_Lease_TypeID;
                objEmployee.Loan_LeaseID = p_Loan_LeaseID;
                objEmployee.TypeID = p_TypeID;
                objEmployee.Month = p_Month;
                objEmployee.USER_ID = p_USER_ID;
                DataTable dt = objEmployee.ExecuteTable();
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

        public DataTable GetLoanLeaseInstallments(int p_Loan_LeaseID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spGetLoanLeaseInstallment objInstallment = new spGetLoanLeaseInstallment();
                objInstallment.Connection = mConnection;
                objInstallment.Loan_LeaseID = p_Loan_LeaseID;
                DataTable dt = objInstallment.ExecuteTable();
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
    }
}