using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;
using SAMSDatabaseLayer.Classes;

namespace SAMSBusinessLayer.Classes
{


    public class Student_Controller
    {
        IDbTransaction mTransaction;
        IDbConnection mConnection;

        #region Constructor
        public Student_Controller()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #endregion

        #region Public Methods

        public string spSelectMAXStudent()
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spSelectMAXSTUDENT mProgram = new spSelectMAXSTUDENT();
                mProgram.Connection = mConnection;

                string dt = mProgram.ExecuteScalar();
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
        public DataTable UspSelectDomicile(int p_City_Id, string p_City_Name)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectDOMICILE mDomicile = new spSelectDOMICILE();
                mDomicile.Connection = mConnection;

                mDomicile.CITY_ID = p_City_Id;
                mDomicile.CITY_NAME = p_City_Name;
                DataTable dt = mDomicile.ExecuteTable();
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
        public DataTable UspSelectProgram(int p_Program_Id, string p_Program_Name, int p_TYPE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spSelectPROGRAM mProgram = new spSelectPROGRAM();
                mProgram.Connection = mConnection;

                mProgram.PROGRAM_ID = p_Program_Id;
                mProgram.TYPE = p_TYPE;
                mProgram.PROGRAM_NAME = p_Program_Name;
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
        public DataTable UspSelectStudent(string p_FieldName, string p_Search)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                UspSelectSTUDENT mProgram = new UspSelectSTUDENT();
                mProgram.Connection = mConnection;

                mProgram.FEILDNAME = p_FieldName;
                mProgram.PARAMETER = p_Search;
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
        public DataTable UspSelectStudent2(long p_STUDENT_ID, int p_PROGRAM_ID, int p_SESSION_ID, string p_STUDENT_NAME, int p_TYPE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                UspSelectSTUDENT2 mProgram = new UspSelectSTUDENT2();
                mProgram.Connection = mConnection;
                mProgram.TYPE = p_TYPE;
                mProgram.STUDENT_ID = p_STUDENT_ID;
                mProgram.PROGRAM_ID = p_PROGRAM_ID;
                mProgram.SESSION_ID = p_SESSION_ID;
                mProgram.STUDENT_NAME = p_STUDENT_NAME;

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

        //gET pROGRAM sESSION
        public DataTable UspSelectSession(string p_SESSION_FROM, string p_SESSION_TO)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spSelectPROGRAM_SESSION mProgram = new spSelectPROGRAM_SESSION();
                mProgram.Connection = mConnection;

                mProgram.SESSION_FROM = p_SESSION_FROM;
                mProgram.SESSION_TO = p_SESSION_TO;

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

        public DataTable SelectProgramAssignment(int p_SessionID, int p_TypeiD, int p_Companyid)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                UspSelectUnAssignProgram mUserAssing = new UspSelectUnAssignProgram();
                mUserAssing.Connection = mConnection;
                
                mUserAssing.SESSION_ID = p_SessionID;
                mUserAssing.TYPE_ID = p_TypeiD;
                mUserAssing.COMPANY_ID = p_Companyid;

                DataTable dt = mUserAssing.ExecuteTable();
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

        #region Insert

        public bool InsertAdmission(long p_STUDENT_ID2, int p_PROGRAM_ID, int p_SESSION_ID, string p_REG_NO, string p_STUDENT_NAME, short p_GENDER,
          string p_RELIGION, string p_NATIONALITY, int p_DOMICILE_ID, DateTime p_BIRTH_DATE, bool p_MARITAL_STATUS,
          string p_MOBILE_NO, int p_BLOOD_ID, string p_CNIC, string p_EMAIL, string p_PERMANENT_ADDRESS, string p_POSTAL_ADDRESS,
            string p_FATHER_NAME, string p_OCCUPATION, string p_F_EMAIL, string p_F_OFFICE_ADDRESS, string p_F_CNIC,
            string p_F_MOBILE_NO, string p_F_OFFICE_PHONE, string p_RESIDENCE, DateTime p_DOCUMENT_DATE, int p_USER_ID,
             string p_IMAGE_NAME, DataTable Qualification, DataTable Subling)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                mTransaction = ProviderFactory.GetTransaction(mConnection);

                spInsertSTUDENT mStudentInfo = new spInsertSTUDENT();

                mStudentInfo.Connection = mConnection;
                mStudentInfo.Transaction = mTransaction;

                mStudentInfo.REG_NO = p_REG_NO;
                mStudentInfo.PROGRAM_ID = p_PROGRAM_ID;
                mStudentInfo.SESSION_ID = p_SESSION_ID;
                mStudentInfo.STUDENT_NAME = p_STUDENT_NAME;

                mStudentInfo.GENDER = p_GENDER;

                mStudentInfo.RELIGION = p_RELIGION;
                mStudentInfo.NATIONALITY = p_NATIONALITY;
                mStudentInfo.DOMICILE_ID = p_DOMICILE_ID;
                mStudentInfo.BIRTH_DATE = p_BIRTH_DATE;
                mStudentInfo.MARITAL_STATUS = p_MARITAL_STATUS;
                mStudentInfo.MOBILE_NO = p_MOBILE_NO;
                mStudentInfo.BLOOD_ID = p_BLOOD_ID;
                mStudentInfo.CNIC = p_CNIC;
                mStudentInfo.EMAIL = p_EMAIL;
                mStudentInfo.PERMANENT_ADDRESS = p_PERMANENT_ADDRESS;
                mStudentInfo.POSTAL_ADDRESS = p_POSTAL_ADDRESS;
                mStudentInfo.FATHER_NAME = p_FATHER_NAME;
                mStudentInfo.OCCUPATION = p_OCCUPATION;
                mStudentInfo.F_EMAIL = p_F_EMAIL;
                mStudentInfo.F_MOBILE_NO = p_F_MOBILE_NO;
                mStudentInfo.F_OFFICE_ADDRESS = p_F_OFFICE_ADDRESS;
                mStudentInfo.F_OFFICE_PHONE = p_F_OFFICE_PHONE;
                mStudentInfo.F_CNIC = p_F_CNIC;
                mStudentInfo.RESIDENCE = p_RESIDENCE;
                mStudentInfo.IMAGE_NAME = p_IMAGE_NAME;

                mStudentInfo.USER_ID = p_USER_ID;
                mStudentInfo.TIME_STAMP = System.DateTime.Now;
                mStudentInfo.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mStudentInfo.IS_ACTIVE = true;
                mStudentInfo.LAST_UPDATE = System.DateTime.Now;
                mStudentInfo.STUDENT_ID2 = p_STUDENT_ID2;

                long STUDENT_ID = mStudentInfo.ExecuteQuery();

                spInsertQUALIFICATION mQualificationDetail = new spInsertQUALIFICATION();
                mQualificationDetail.Connection = mConnection;
                mQualificationDetail.Transaction = mTransaction;

                foreach (DataRow dr in Qualification.Rows)
                {
                    mQualificationDetail.STUDENT_ID = STUDENT_ID;
                    mQualificationDetail.CERTIFICATE = dr["CERTIFICATE"].ToString();
                    mQualificationDetail.SESSION = dr["SESSION"].ToString();
                    mQualificationDetail.MARKS = dr["MARKS"].ToString();
                    mQualificationDetail.CGPA = Convert.ToDecimal(dr["CGPA"]);
                    mQualificationDetail.BOARD = dr["BOARD"].ToString();

                    mQualificationDetail.ExecuteQuery();
                }

                spInsertSUBLING mSublingDetail = new spInsertSUBLING();
                mSublingDetail.Connection = mConnection;
                mSublingDetail.Transaction = mTransaction;

                foreach (DataRow dr in Subling.Rows)
                {
                    if (dr["NAME"] != null)
                    {
                        mSublingDetail.STUDENT_ID = STUDENT_ID;
                        mSublingDetail.NAME = dr["NAME"].ToString();
                        mSublingDetail.ROLLNO = dr["ROLLNO"].ToString();
                        mSublingDetail.PROGRAM = dr["PROGRAM"].ToString();
                        mSublingDetail.YEAR = dr["YEAR"].ToString();
                        mSublingDetail.ExecuteQuery();
                    }
                }

                mTransaction.Commit();

                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                //  throw;
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

        #region FEE

        public DataTable UspSelectFEE(int p_USER_ID, int p_PROGRAM_ID, int p_SESSION_ID, int p_PAGE, int p_PAGESIZE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spSelectFeeView mProgram = new spSelectFeeView();
                mProgram.Connection = mConnection;

                mProgram.USER_ID = p_USER_ID;
                mProgram.PROGRAM_ID = p_PROGRAM_ID;
                mProgram.SESSION_ID = p_SESSION_ID;
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
        public DataTable UspSelectFEE2(long p_FEE_ID, int p_PROGRAM_ID, int p_SESSION_ID, string p_STUDENT_NAME, int p_TYPE_ID)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spSelectFeeView2 mProgram = new spSelectFeeView2();
                mProgram.Connection = mConnection;

                mProgram.FEE_ID = p_FEE_ID;
                mProgram.PROGRAM_ID = p_PROGRAM_ID;
                mProgram.SESSION_ID = p_SESSION_ID;
                mProgram.STUDENT_NAME = p_STUDENT_NAME;
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

        public bool InsertFee(int pProgramId, int pSessionId, decimal p_FINE, decimal p_FINE_TILL, decimal p_FINE_FROM, long p_FEE_ID2, DateTime p_PAYMENT_DATE, DateTime p_DOCUMENT_DATE, int p_USER_ID, int p_UPDATED_BY
            , DataTable Fee, DataTable Fee_Account)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertFEE mFeeInfo = new spInsertFEE();

                mFeeInfo.Connection = mConnection;
                mFeeInfo.Transaction = mTransaction;

                mFeeInfo.PROGRAM_ID = pProgramId;
                mFeeInfo.SESSION_ID = pSessionId;
                mFeeInfo.FINE = p_FINE;
                mFeeInfo.FINE_TILL = p_FINE_TILL;
                mFeeInfo.FINE_FROM = p_FINE_FROM;
                mFeeInfo.PAYMENT_DATE = p_PAYMENT_DATE;
                mFeeInfo.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mFeeInfo.IS_ACTIVE = true;
                mFeeInfo.LAST_UPDATE = System.DateTime.Now;
                mFeeInfo.USER_ID = p_USER_ID;
                mFeeInfo.UPDATED_BY = p_UPDATED_BY;
                mFeeInfo.FEE_ID2 = p_FEE_ID2;

                long FEE_ID = mFeeInfo.ExecuteQuery();

                spInsertFEE_DETAIL mFeeDetail = new spInsertFEE_DETAIL();
                mFeeDetail.Connection = mConnection;
                mFeeDetail.Transaction = mTransaction;

                foreach (DataRow dr in Fee.Rows)
                {
                    mFeeDetail.FEE_ID = FEE_ID;
                    mFeeDetail.STUDENT_ID = Convert.ToInt64(dr["STUDENT_ID"]);
                    mFeeDetail.BILL_NO = Convert.ToString(dr["REG_NO"]) + DateTime.Now.Year.ToString();

                    mFeeDetail.ExecuteQuery();
                }

                spInsertFEE_ACCOUNTDETAIL mFeeACCDetail = new spInsertFEE_ACCOUNTDETAIL();
                mFeeACCDetail.Connection = mConnection;
                mFeeACCDetail.Transaction = mTransaction;

                foreach (DataRow dr in Fee_Account.Rows)
                {
                    mFeeACCDetail.FEE_ID = FEE_ID;
                    mFeeACCDetail.FEE_VALUE = Convert.ToDecimal(dr["FEE_VALUE"]);
                    mFeeACCDetail.FEE_ACCOUNT_ID = Convert.ToInt64(dr["ACCOUNT_HEAD_ID"]);

                    mFeeACCDetail.ExecuteQuery();
                }



                mTransaction.Commit();

                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                //  throw;
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


        public bool InsertFeeStructure(int p_PROGRAM_ID, int p_SESSION_ID, long p_FEE_ID2, DateTime p_DOCUMENT_DATE, int p_USER_ID, int p_UPDATED_BY
           , DataTable Fee_Account,bool p_ISACTIVE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertFEE_STRUCTURE_MASTER mFeeInfo = new spInsertFEE_STRUCTURE_MASTER();

                mFeeInfo.Connection = mConnection;

                mFeeInfo.PROGRAM_ID = p_PROGRAM_ID;
                mFeeInfo.SESSION_ID = p_SESSION_ID;
               
                mFeeInfo.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mFeeInfo.IS_ACTIVE = true;
                mFeeInfo.LAST_UPDATE = System.DateTime.Now;
                mFeeInfo.USER_ID = p_USER_ID;
                mFeeInfo.UPDATED_BY = p_UPDATED_BY;
                mFeeInfo.FEE_ID2 = p_FEE_ID2;

                long FEE_ID = mFeeInfo.ExecuteQuery();

                spInsertFEE_STRUCTUREDETAIL mFeeACCDetail = new spInsertFEE_STRUCTUREDETAIL();
                mFeeACCDetail.Connection = mConnection;
                mFeeACCDetail.Transaction = mTransaction;

                foreach (DataRow dr in Fee_Account.Rows)
                {
                    mFeeACCDetail.FEE_STRUCTURE_MASTER_ID = FEE_ID;
                    mFeeACCDetail.FEE_VALUE = Convert.ToDecimal(dr["FEE_VALUE"]);
                    mFeeACCDetail.FEE_ACCOUNT_ID = Convert.ToInt64(dr["ACCOUNT_HEAD_ID"]);

                    mFeeACCDetail.ExecuteQuery();
                }



                mTransaction.Commit();

                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                //  throw;
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

        #region Study Program

        public bool InsertStudyProgram(int p_PROGRAM_ID, string p_PROGRAM_NAME, string p_PROGRAM_CODE, DateTime p_DOCUMENT_DATE, int p_USER_ID, int p_UPDATED_BY, bool p_IS_ACTIVE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertPROGRAM mProgInfo = new spInsertPROGRAM();

                mProgInfo.Connection = mConnection;
                mProgInfo.Transaction = mTransaction;

                mProgInfo.PROGRAM_ID = p_PROGRAM_ID;
                mProgInfo.PROGRAM_NAME = p_PROGRAM_NAME;
                mProgInfo.PROGRAM_CODE = p_PROGRAM_CODE;

                mProgInfo.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mProgInfo.IS_ACTIVE = p_IS_ACTIVE;
                mProgInfo.LAST_UPDATE = System.DateTime.Now;
                mProgInfo.USER_ID = p_USER_ID;
                mProgInfo.UPDATED_BY = p_UPDATED_BY;

                mProgInfo.ExecuteQuery();

                mTransaction.Commit();

                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                //  throw;
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

        #region Course

        public bool InsertCourses(int p_PROGRAM_ID, long p_COURSE_MASTER_ID2, DateTime p_DOCUMENT_DATE, int p_USER_ID, int p_UPDATED_BY
           , DataTable Course)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                mTransaction = ProviderFactory.GetTransaction(mConnection);
                spInsertCOURSE mCourseInfo = new spInsertCOURSE();

                mCourseInfo.Connection = mConnection;
                mCourseInfo.Transaction = mTransaction;

                mCourseInfo.PROGRAM_ID = p_PROGRAM_ID;

                mCourseInfo.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mCourseInfo.IS_ACTIVE = true;
                mCourseInfo.LAST_UPDATE = System.DateTime.Now;
                mCourseInfo.USER_ID = p_USER_ID;
                mCourseInfo.UPDATED_BY = p_UPDATED_BY;
                mCourseInfo.COURSE_MASTER_ID2 = p_COURSE_MASTER_ID2;

                long COURSE_MASTER_ID = mCourseInfo.ExecuteQuery();

                spInsertCOURSE_DETAIL mCourseDetail = new spInsertCOURSE_DETAIL();

                mCourseDetail.Connection = mConnection;
                mCourseDetail.Transaction = mTransaction;

                foreach (DataRow dr in Course.Rows)
                {
                    mCourseDetail.COURSE_MASTER_ID = COURSE_MASTER_ID;
                    mCourseDetail.THEORY = Convert.ToString(dr["THEORY"]);
                    mCourseDetail.COURSE_NAME = Convert.ToString(dr["COURSENAME"]);
                    mCourseDetail.MARKS = Convert.ToDecimal(dr["MARKS"]);
                    mCourseDetail.PROFESSIONAL = Convert.ToString(dr["PROFESSIONAL"]);
                    mCourseDetail.PROFESSIONAL_ID = Convert.ToInt32(dr["PROFESSIONAL_ID"]);


                    mCourseDetail.ExecuteQuery();
                }

                mTransaction.Commit();

                return true;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                //throw;
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

        #region Study Session

        public bool InsertStudySession(int p_SESSION_ID, string p_SESSION_FROM, string p_SESSION_TO, DateTime p_DOCUMENT_DATE, int p_USER_ID, int p_UPDATED_BY, bool p_IS_ACTIVE)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spInsertPROGRAM_SESSION mProgInfo = new spInsertPROGRAM_SESSION();

                mProgInfo.Connection = mConnection;
                mProgInfo.Transaction = mTransaction;

                mProgInfo.SESSION_ID = p_SESSION_ID;
                mProgInfo.SESSION_FROM = p_SESSION_FROM;
                mProgInfo.SESSION_TO = p_SESSION_TO;

                mProgInfo.DOCUMENT_DATE = p_DOCUMENT_DATE;
                mProgInfo.IS_ACTIVE = p_IS_ACTIVE;
                mProgInfo.LAST_UPDATE = System.DateTime.Now;
                mProgInfo.USER_ID = p_USER_ID;
                mProgInfo.UPDATED_BY = p_UPDATED_BY;

                mProgInfo.ExecuteQuery();

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
        public bool InsertProgramAssignment(int p_SessionID, int p_ProgramID, int p_Companyid)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertPRGORAM_ASSIGNMENT mUserAssing = new spInsertPRGORAM_ASSIGNMENT();
                mUserAssing.Connection = mConnection;
                mUserAssing.SESSION_ID = p_SessionID;
                mUserAssing.PROGRAM_ID = p_ProgramID;
                mUserAssing.COMPANY_ID = p_Companyid;
                bool dt = mUserAssing.ExecuteQuery();
                return dt;
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
        public bool DeleteProgramAssignment(int p_SessionID, int p_ProgramID, int p_Companyid)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spDeletePROGRAM_ASSIGNMENT mUserAssing = new spDeletePROGRAM_ASSIGNMENT();
                mUserAssing.Connection = mConnection;

                mUserAssing.SESSION_ID = p_SessionID;
                mUserAssing.PROGRAM_ID = p_ProgramID;
                mUserAssing.COMPANY_ID = p_Companyid;

                bool dt = mUserAssing.ExecuteQuery();
                return dt;
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

        #region Domicile

        public bool InsertDomicile(int pCityId, string pCityName, bool pIsActive)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();
                spInsertDOMICILE mIDomicile = new spInsertDOMICILE();
              
                mIDomicile.Connection = mConnection;
                mIDomicile.CityId = pCityId;
                mIDomicile.CityName = pCityName;
                mIDomicile.IsActive = pIsActive;
                
                bool dt = mIDomicile.ExecuteQuery();

                return dt;
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

        #region Charges

        public DataTable UspSelectStudentCharges(int pType)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spSelectStudent_Charges mProgram = new spSelectStudent_Charges();
                mProgram.Connection = mConnection;
                mProgram.CHARGE_TYPE = pType;
                

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
        public bool InsertStudentCharges(int pChargeId, int pChargeType, int pProgramId, int pSessionId, decimal pChargeAmount, DateTime pDocumentDate, int pUserId, int pUpdatedBy)
        {
            IDbConnection mConnection = null;
            try
            {
                mConnection = ProviderFactory.GetConnection(Configuration.ConnectionString, EnumProviders.SQLClient);
                mConnection.Open();

                spInsertStudent_Charges mChargeInfo = new spInsertStudent_Charges();

                mChargeInfo.Connection = mConnection;

                mChargeInfo.CHARGE_TYPE = pChargeType;
                mChargeInfo.CHARGE_ID = pChargeId;
                mChargeInfo.PROGRAM_ID = pProgramId;
                mChargeInfo.SESSION_ID = pSessionId;
                mChargeInfo.CHARGE_AMOUNT = pChargeAmount;
                mChargeInfo.DOCUMENT_DATE = pDocumentDate;
                mChargeInfo.LAST_UPDATE = DateTime.Now;
                mChargeInfo.TIME_STAMP = DateTime.Now;
                mChargeInfo.USER_ID = pUserId;
                mChargeInfo.UPDATE_BY = pUpdatedBy;
                mChargeInfo.IS_ACTIVE = true;

                bool dt = mChargeInfo.ExecuteQuery();

                return dt;
            }
            catch (Exception exp)
            {
                ExceptionPublisher.PublishException(exp);
                mTransaction.Rollback();
                //  throw;
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

    }

}
