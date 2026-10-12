using System;
using System.Data;
using SAMSCommon.Classes;
using SAMSDataAccessLayer.Classes;

namespace SAMSDatabaseLayer.Classes
{
	public class spInsertPURCHASE_CONTRACT_QUALITY
	{
        #region Private Members
        private string sp_Name = "spInsertPURCHASE_CONTRACT_QUALITY";
        private IDbConnection m_connection;
        private IDbTransaction m_transaction;
        private int m_PURCHASE_CONTRACT_DETAIL_ID;
        private int m_PURCHASE_CONTRACT_ID;
        private int m_SKU_ID;
        private long m_SKU_QUALITY_ID;
        private string m_QUALITY_DESCRIPTION;
        private string m_QUALITY_VALUE;
        #endregion
        #region Public Properties
        public int PURCHASE_CONTRACT_DETAIL_ID
        {
            set
            {
                m_PURCHASE_CONTRACT_DETAIL_ID = value;
            }
            get
            {
                return m_PURCHASE_CONTRACT_DETAIL_ID;
            }
        }
        public int PURCHASE_CONTRACT_ID
        {
            set
            {
                m_PURCHASE_CONTRACT_ID = value;
            }
            get
            {
                return m_PURCHASE_CONTRACT_ID;
            }
        }
        public int SKU_ID
        {
            set
            {
                m_SKU_ID = value;
            }
            get
            {
                return m_SKU_ID;
            }
        }
        public long SKU_QUALITY_ID
        {
            get
            {
                return m_SKU_QUALITY_ID;
            }
        }
        public string QUALITY_DESCRIPTION
        {
            set
            {
                m_QUALITY_DESCRIPTION = value;
            }
            get
            {
                return m_QUALITY_DESCRIPTION;
            }
        }
        public string QUALITY_VALUE
        {
            set
            {
                m_QUALITY_VALUE = value;
            }
            get
            {
                return m_QUALITY_VALUE;
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
        public spInsertPURCHASE_CONTRACT_QUALITY()
        {
        }
        #endregion
        #region public Methods
        public bool ExecuteQuery()
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
                m_SKU_QUALITY_ID = (long)((IDataParameter)(cmd.Parameters["@SKU_QUALITY_ID"])).Value;
                return true;
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
                m_PURCHASE_CONTRACT_DETAIL_ID = Convert.ToInt32(dr["PURCHASE_CONTRACT_DETAIL_ID"]);
                m_PURCHASE_CONTRACT_ID = Convert.ToInt32(dr["PURCHASE_CONTRACT_ID"]);
                m_SKU_ID = Convert.ToInt32(dr["SKU_ID"]);
                m_SKU_QUALITY_ID = Convert.ToInt64(dr["SKU_QUALITY_ID"]);
                m_QUALITY_DESCRIPTION = Convert.ToString(dr["QUALITY_DESCRIPTION"]);
                m_QUALITY_VALUE = Convert.ToString(dr["QUALITY_VALUE"]);
            }
        }
        public void GetParameterCollection(ref IDbCommand cmd)
        {
            IDataParameterCollection pparams = cmd.Parameters;
            IDataParameter parameter;
            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@PURCHASE_CONTRACT_DETAIL_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_PURCHASE_CONTRACT_DETAIL_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_PURCHASE_CONTRACT_DETAIL_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@PURCHASE_CONTRACT_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_PURCHASE_CONTRACT_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_PURCHASE_CONTRACT_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SKU_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.Int);
            if (m_SKU_ID == Constants.IntNullValue)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_SKU_ID;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@SKU_QUALITY_ID";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
            parameter.Direction = ParameterDirection.Output;
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@QUALITY_DESCRIPTION";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_QUALITY_DESCRIPTION == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_QUALITY_DESCRIPTION;
            }
            pparams.Add(parameter);


            parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
            parameter.ParameterName = "@QUALITY_VALUE";
            parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
            if (m_QUALITY_VALUE == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = m_QUALITY_VALUE;
            }
            pparams.Add(parameter);


        }
        #endregion
	}
}
