using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SAMSDataAccessLayer.Classes;
using SAMSCommon.Classes;

namespace SAMSDatabaseLayer.Classes
{
    public class spInsertIGP_MASTER
    {
        #region Private Members
        private string sp_Name = "spInsertIGP_MASTER";
        private IDbConnection m_connection;
        private IDbTransaction m_transaction;

        private int m_COMPANYID;
        private int m_SUPPLIER_ID;
        private int m_USER_ID;
        private int m_IGP_TYPE;
        private DateTime m_DOCUMENT_DATE;
        private long m_IGP_MASTER_ID;
        private long m_IGP_MASTER_ID2;
        private int m_TGP_ID;
        private string m_GOODS_OUT_THROUGH;
        private string m_TRANSPORT_NO;
        #endregion
        #region Public Properties

        public int COMPANYID
        {
            set
            {
                m_COMPANYID = value;
            }
            get
            {
                return m_COMPANYID;
            }
        }
        public int SUPPLIER_ID
        {
            set
            {
                m_SUPPLIER_ID = value;
            }
            get
            {
                return m_SUPPLIER_ID;
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
        public int IGP_TYPE
        {
            set
            {
                m_IGP_TYPE = value;
            }
            get
            {
                return m_IGP_TYPE;
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
        public long IGP_MASTER_ID
        {
            get
            {
                return m_IGP_MASTER_ID;
            }
        }
        public long IGP_MASTER_ID2
        {
            set
            {
                m_IGP_MASTER_ID2 = value;
            }
            get
            {
                return m_IGP_MASTER_ID2;
            }
        }
        public int TGP_ID
        {
            set
            {
                m_TGP_ID = value;
            }
            get
            {
                return m_TGP_ID;
            }
        }
        public string GOODS_OUT_THROUGH
        {
            set
            {
                m_GOODS_OUT_THROUGH = value;
            }
            get
            {
                return m_GOODS_OUT_THROUGH;
            }
        }
        public string TRANSPORT_NO
        {
            set
            {
                m_TRANSPORT_NO = value;
            }
            get
            {
                return m_TRANSPORT_NO;
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
        public spInsertIGP_MASTER()
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
                m_IGP_MASTER_ID = (long)((IDataParameter)(cmd.Parameters["@IGP_MASTER_ID"])).Value;
                return m_IGP_MASTER_ID;
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
                m_COMPANYID = Convert.ToInt32(dr["COMPANYID"]);
                m_SUPPLIER_ID = Convert.ToInt32(dr["SUPPLIER_ID"]);
                m_USER_ID = Convert.ToInt32(dr["USER_ID"]);
                m_IGP_TYPE = Convert.ToInt32(dr["IGP_TYPE"]);
                m_DOCUMENT_DATE = Convert.ToDateTime(dr["DOCUMENT_DATE"]);
                m_IGP_MASTER_ID = Convert.ToInt64(dr["IGP_MASTER_ID"]);
                m_IGP_MASTER_ID2 = Convert.ToInt64(dr["IGP_MASTER_ID2"]);
                m_TGP_ID = Convert.ToInt32(dr["TGP_ID"]);
                m_GOODS_OUT_THROUGH = Convert.ToString(dr["GOODS_OUT_THROUGH"]);
                m_TRANSPORT_NO = Convert.ToString(dr["TRANSPORT_NO"]);
            }
        }
        public void GetParameterCollection(ref IDbCommand cmd)
        {
            IDataParameterCollection pparams = cmd.Parameters;
            IDataParameter parameter;

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@COMPANYID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_COMPANYID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_COMPANYID;
            }
            pparams.Add(parameter);

            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SUPPLIER_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_SUPPLIER_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SUPPLIER_ID;
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
            parameter.ParameterName = "@IGP_TYPE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_IGP_TYPE == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_IGP_TYPE;
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
            parameter.ParameterName = "@IGP_MASTER_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
            parameter.Direction = ParameterDirection.Output;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@IGP_MASTER_ID2";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
            if (m_IGP_MASTER_ID2 == Constants.LongNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_IGP_MASTER_ID2;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@TGP_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_TGP_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_TGP_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@GOODS_OUT_THROUGH";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_GOODS_OUT_THROUGH == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_GOODS_OUT_THROUGH;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@TRANSPORT_NO";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_TRANSPORT_NO == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_TRANSPORT_NO;
            }
            pparams.Add(parameter);


        }
        #endregion
    }
}
