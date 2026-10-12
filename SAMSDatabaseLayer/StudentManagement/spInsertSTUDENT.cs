using System;
using System.Collections.Generic;
using System.Text;
using SAMSDataAccessLayer.Classes;
using SAMSCommon.Classes;
using System.Data;

namespace SAMSDatabaseLayer.Classes
{
    public class spInsertSTUDENT
    {
        #region Private Members
        private string sp_Name = "spInsertSTUDENT";
        private IDbConnection m_connection;
        private IDbTransaction m_transaction;
        private short m_GENDER;
        private int m_DOMICILE_ID;
        private int m_PROGRAM_ID;
        private int m_SESSION_ID;
        private int m_BLOOD_ID;
        private int m_USER_ID;
        private DateTime m_BIRTH_DATE;
        private DateTime m_DOCUMENT_DATE;
        private DateTime m_TIME_STAMP;
        private DateTime m_LAST_UPDATE;
        private bool m_MARITAL_STATUS;
        private bool m_IS_ACTIVE;
        private long m_STUDENT_ID2;
        private long m_STUDENT_ID;
        private string m_REG_NO;
        private string m_STUDENT_NAME;
        private string m_RELIGION;
        private string m_NATIONALITY;
        private string m_MOBILE_NO;
        private string m_CNIC;
        private string m_F_OFFICE_ADDRESS;
        private string m_F_CNIC;
        private string m_F_MOBILE_NO;
        private string m_F_OFFICE_PHONE;
        private string m_RESIDENCE;
        private string m_IMAGE_NAME;
        private string m_EMAIL;
        private string m_PERMANENT_ADDRESS;
        private string m_POSTAL_ADDRESS;
        private string m_FATHER_NAME;
        private string m_OCCUPATION;
        private string m_F_EMAIL;
        #endregion
        #region Public Properties
        public short GENDER
        {
            set
            {
                m_GENDER = value;
            }
            get
            {
                return m_GENDER;
            }
        }
         public int DOMICILE_ID 
        {
            set
            {
                m_DOMICILE_ID = value;
            }
            get
            {
                return m_DOMICILE_ID;
            }
        }
         public int PROGRAM_ID 
        {
            set
            {
                m_PROGRAM_ID = value;
            }
            get
            {
                return m_PROGRAM_ID;
            }
        }
         public int SESSION_ID 
        {
            set
            {
                m_SESSION_ID = value;
            }
            get
            {
                return m_SESSION_ID;
            }
        }
        
        public int BLOOD_ID
        {
            set
            {
                m_BLOOD_ID = value;
            }
            get
            {
                return m_BLOOD_ID;
            }
        }
        public int USER_ID
        {
            set
            {
                m_USER_ID = value;
            }
            get
            {
                return m_USER_ID;
            }
        }
        public DateTime BIRTH_DATE
        {
            set
            {
                m_BIRTH_DATE = value;
            }
            get
            {
                return m_BIRTH_DATE;
            }
        }
        public DateTime DOCUMENT_DATE
        {
            set
            {
                m_DOCUMENT_DATE = value;
            }
            get
            {
                return m_DOCUMENT_DATE;
            }
        }
        public DateTime TIME_STAMP
        {
            set
            {
                m_TIME_STAMP = value;
            }
            get
            {
                return m_TIME_STAMP;
            }
        }
        public DateTime LAST_UPDATE
        {
            set
            {
                m_LAST_UPDATE = value;
            }
            get
            {
                return m_LAST_UPDATE;
            }
        }
        public bool MARITAL_STATUS
        {
            set
            {
                m_MARITAL_STATUS = value;
            }
            get
            {
                return m_MARITAL_STATUS;
            }
        }
        public bool IS_ACTIVE
        {
            set
            {
                m_IS_ACTIVE = value;
            }
            get
            {
                return m_IS_ACTIVE;
            }
        }
        public long STUDENT_ID2
        {
            set
            {
                m_STUDENT_ID2 = value;
            }
            get
            {
                return m_STUDENT_ID2;
            }
        }
        public long STUDENT_ID
        {
            get
            {
                return m_STUDENT_ID;
            }
        }
        public string REG_NO
        {
            set
            {
                m_REG_NO = value;
            }
            get
            {
                return m_REG_NO;
            }
        }
        public string STUDENT_NAME
        {
            set
            {
                m_STUDENT_NAME = value;
            }
            get
            {
                return m_STUDENT_NAME;
            }
        }
        public string RELIGION
        {
            set
            {
                m_RELIGION = value;
            }
            get
            {
                return m_RELIGION;
            }
        }
        public string NATIONALITY
        {
            set
            {
                m_NATIONALITY = value;
            }
            get
            {
                return m_NATIONALITY;
            }
        }
        public string MOBILE_NO
        {
            set
            {
                m_MOBILE_NO = value;
            }
            get
            {
                return m_MOBILE_NO;
            }
        }
        public string CNIC
        {
            set
            {
                m_CNIC = value;
            }
            get
            {
                return m_CNIC;
            }
        }
        public string F_OFFICE_ADDRESS
        {
            set
            {
                m_F_OFFICE_ADDRESS = value;
            }
            get
            {
                return m_F_OFFICE_ADDRESS;
            }
        }
        public string F_CNIC
        {
            set
            {
                m_F_CNIC = value;
            }
            get
            {
                return m_F_CNIC;
            }
        }
        public string F_MOBILE_NO
        {
            set
            {
                m_F_MOBILE_NO = value;
            }
            get
            {
                return m_F_MOBILE_NO;
            }
        }
        public string F_OFFICE_PHONE
        {
            set
            {
                m_F_OFFICE_PHONE = value;
            }
            get
            {
                return m_F_OFFICE_PHONE;
            }
        }
        public string RESIDENCE
        {
            set
            {
                m_RESIDENCE = value;
            }
            get
            {
                return m_RESIDENCE;
            }
        }
        public string IMAGE_NAME
        {
            set
            {
                m_IMAGE_NAME = value;
            }
            get
            {
                return m_IMAGE_NAME;
            }
        }
        public string EMAIL
        {
            set
            {
                m_EMAIL = value;
            }
            get
            {
                return m_EMAIL;
            }
        }
        public string PERMANENT_ADDRESS
        {
            set
            {
                m_PERMANENT_ADDRESS = value;
            }
            get
            {
                return m_PERMANENT_ADDRESS;
            }
        }
        public string POSTAL_ADDRESS
        {
            set
            {
                m_POSTAL_ADDRESS = value;
            }
            get
            {
                return m_POSTAL_ADDRESS;
            }
        }
        public string FATHER_NAME
        {
            set
            {
                m_FATHER_NAME = value;
            }
            get
            {
                return m_FATHER_NAME;
            }
        }
        public string OCCUPATION
        {
            set
            {
                m_OCCUPATION = value;
            }
            get
            {
                return m_OCCUPATION;
            }
        }
        public string F_EMAIL
        {
            set
            {
                m_F_EMAIL = value;
            }
            get
            {
                return m_F_EMAIL;
            }
        }


        public IDbConnection Connection
        {
            set
            {
                m_connection = value;
            }
            get
            {
                return m_connection;
            }
        }
        public IDbTransaction Transaction
        {
            set
            {
                m_transaction = value;
            }
            get
            {
                return m_transaction;
            }
        }
        #endregion
        #region Constructor
        public spInsertSTUDENT()
        {
        }
        #endregion
        #region public Methods
        public long ExecuteQuery()
        {
            try
            {
                IDbCommand cmd = ProviderFactory.GetCommand(EnumProviders.SQLClient);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sp_Name;
                cmd.Connection = m_connection;
                if (m_transaction != null)
                {
                    cmd.Transaction = m_transaction;
                }
                GetParameterCollection(ref cmd);
                cmd.ExecuteNonQuery();
                m_STUDENT_ID = (long)((IDataParameter)(cmd.Parameters["@STUDENT_ID"])).Value;
                return m_STUDENT_ID;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
            }
        }
        public IDataReader ExecuteReader()
        {
            try
            {
                IDbCommand command = ProviderFactory.GetCommand(EnumProviders.SQLClient);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = sp_Name;
                command.Connection = m_connection;
                if (m_transaction != null)
                {
                    command.Transaction = m_transaction;
                }
                GetParameterCollection(ref command);
                IDataReader dr = command.ExecuteReader();
                return dr;
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
            }
        }
        public DataTable ExecuteTable()
        {
            try
            {
                IDbCommand command = ProviderFactory.GetCommand(EnumProviders.SQLClient);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = sp_Name;
                command.Connection = m_connection;
                if (m_transaction != null)
                {
                    command.Transaction = m_transaction;
                }
                GetParameterCollection(ref command);
                IDbDataAdapter da = ProviderFactory.GetAdapter(EnumProviders.SQLClient);
                da.SelectCommand = command;
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
            }
        }
        public string ExecuteScalar()
        {
            try
            {
                IDbCommand command = ProviderFactory.GetCommand(EnumProviders.SQLClient);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = sp_Name;
                command.Connection = m_connection;
                if (m_transaction != null)
                {
                    command.Transaction = m_transaction;
                }
                GetParameterCollection(ref command);
                object o;
                o = command.ExecuteScalar();


                return o.ToString();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
            }
        }
        public void FirstReader(IDataReader dr)
        {
            if (dr.Read())
            {
                m_GENDER = Convert.ToInt16(dr["GENDER"]);
                m_DOMICILE_ID = Convert.ToInt32(dr["DOMICILE_ID"]);
                m_PROGRAM_ID = Convert.ToInt32(dr["PROGRAM_ID"]);
                m_SESSION_ID = Convert.ToInt32(dr["SESSION_ID"]);
                m_BLOOD_ID = Convert.ToInt32(dr["BLOOD_ID"]);
                m_USER_ID = Convert.ToInt32(dr["USER_ID"]);
                m_BIRTH_DATE = Convert.ToDateTime(dr["BIRTH_DATE"]);
                m_DOCUMENT_DATE = Convert.ToDateTime(dr["DOCUMENT_DATE"]);
                m_TIME_STAMP = Convert.ToDateTime(dr["TIME_STAMP"]);
                m_LAST_UPDATE = Convert.ToDateTime(dr["LAST_UPDATE"]);
                m_MARITAL_STATUS = Convert.ToBoolean(dr["MARITAL_STATUS"]);
                m_IS_ACTIVE = Convert.ToBoolean(dr["IS_ACTIVE"]);
                m_STUDENT_ID2 = Convert.ToInt64(dr["STUDENT_ID2"]);
                m_STUDENT_ID = Convert.ToInt64(dr["STUDENT_ID"]);
                m_REG_NO = Convert.ToString(dr["REG_NO"]);
                m_STUDENT_NAME = Convert.ToString(dr["STUDENT_NAME"]);
                m_RELIGION = Convert.ToString(dr["RELIGION"]);
                m_NATIONALITY = Convert.ToString(dr["NATIONALITY"]);
                m_MOBILE_NO = Convert.ToString(dr["MOBILE_NO"]);
                m_CNIC = Convert.ToString(dr["CNIC"]);
                m_F_OFFICE_ADDRESS = Convert.ToString(dr["F_OFFICE_ADDRESS"]);
                m_F_CNIC = Convert.ToString(dr["F_CNIC"]);
                m_F_MOBILE_NO = Convert.ToString(dr["F_MOBILE_NO"]);
                m_F_OFFICE_PHONE = Convert.ToString(dr["F_OFFICE_PHONE"]);
                m_RESIDENCE = Convert.ToString(dr["RESIDENCE"]);
                m_IMAGE_NAME = Convert.ToString(dr["IMAGE_NAME"]);
                m_EMAIL = Convert.ToString(dr["EMAIL"]);
                m_PERMANENT_ADDRESS = Convert.ToString(dr["PERMANENT_ADDRESS"]);
                m_POSTAL_ADDRESS = Convert.ToString(dr["POSTAL_ADDRESS"]);
                m_FATHER_NAME = Convert.ToString(dr["FATHER_NAME"]);
                m_OCCUPATION = Convert.ToString(dr["OCCUPATION"]);
                m_F_EMAIL = Convert.ToString(dr["F_EMAIL"]);
            }
        }
        public void GetParameterCollection(ref IDbCommand cmd)
        {
            IDataParameterCollection pparams = cmd.Parameters;
            IDataParameter parameter;
            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@GENDER";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.SmallInt);
            if (m_GENDER == Constants.ShortNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_GENDER;
            }
            pparams.Add(parameter);

            
            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@DOMICILE_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_DOMICILE_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_DOMICILE_ID;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@PROGRAM_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_PROGRAM_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_PROGRAM_ID;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SESSION_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_SESSION_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SESSION_ID;
            }
            pparams.Add(parameter);

            

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@BLOOD_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_BLOOD_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_BLOOD_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@USER_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_USER_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_USER_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@BIRTH_DATE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
            if (m_BIRTH_DATE == Constants.DateNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_BIRTH_DATE;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@DOCUMENT_DATE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
            if (m_DOCUMENT_DATE == Constants.DateNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_DOCUMENT_DATE;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@TIME_STAMP";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
            if (m_TIME_STAMP == Constants.DateNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_TIME_STAMP;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@LAST_UPDATE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.DateTime);
            if (m_LAST_UPDATE == Constants.DateNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_LAST_UPDATE;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@MARITAL_STATUS";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_MARITAL_STATUS;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@IS_ACTIVE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Bit);
            parameter.Value = m_IS_ACTIVE;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@STUDENT_ID2";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
            if (m_STUDENT_ID2 == Constants.LongNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_STUDENT_ID2;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@STUDENT_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
            parameter.Direction = ParameterDirection.Output;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@REG_NO";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_REG_NO == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_REG_NO;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@STUDENT_NAME";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_STUDENT_NAME == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_STUDENT_NAME;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@RELIGION";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_RELIGION == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_RELIGION;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@NATIONALITY";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_NATIONALITY == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_NATIONALITY;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@MOBILE_NO";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_MOBILE_NO == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_MOBILE_NO;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@CNIC";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_CNIC == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_CNIC;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@F_OFFICE_ADDRESS";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_F_OFFICE_ADDRESS == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_F_OFFICE_ADDRESS;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@F_CNIC";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_F_CNIC == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_F_CNIC;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@F_MOBILE_NO";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_F_MOBILE_NO == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_F_MOBILE_NO;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@F_OFFICE_PHONE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_F_OFFICE_PHONE == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_F_OFFICE_PHONE;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@RESIDENCE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_RESIDENCE == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_RESIDENCE;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@IMAGE_NAME";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_IMAGE_NAME == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_IMAGE_NAME;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@EMAIL";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_EMAIL == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_EMAIL;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@PERMANENT_ADDRESS";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_PERMANENT_ADDRESS == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_PERMANENT_ADDRESS;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@POSTAL_ADDRESS";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_POSTAL_ADDRESS == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_POSTAL_ADDRESS;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@FATHER_NAME";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_FATHER_NAME == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_FATHER_NAME;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@OCCUPATION";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_OCCUPATION == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_OCCUPATION;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@F_EMAIL";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_F_EMAIL == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_F_EMAIL;
            }
            pparams.Add(parameter);


        }
        #endregion
    }
}
