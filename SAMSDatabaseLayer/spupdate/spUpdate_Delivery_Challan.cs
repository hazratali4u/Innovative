using System;
using System.Data;
using SAMSDataAccessLayer.Classes;

using SAMSCommon.Classes;

namespace SAMSDatabaseLayer.Classes
{
	public class spUpdate_Delivery_Challan
	{
		#region Private Members
		private string sp_Name = "spUpdate_Delivery_Challan" ;
		private IDbConnection m_connection;
		private IDbTransaction m_transaction;
		private long m_PURCHASE_MASTER_ID;
		private string m_DriverName;
		private string m_VEHICLENO;
		private string m_REFERENCE;
		#endregion
		#region Public Properties
		public long PURCHASE_MASTER_ID
		{
			set
			{
				m_PURCHASE_MASTER_ID = value ;
			}
			get
			{
				return m_PURCHASE_MASTER_ID;
			}
		}
		public  string DriverName
		{
			set
			{
				m_DriverName = value ;
			}
			get
			{
				return m_DriverName;
			}
		}
		public  string VEHICLENO
		{
			set
			{
				m_VEHICLENO = value ;
			}
			get
			{
				return m_VEHICLENO;
			}
		}
		public  string REFERENCE
		{
			set
			{
				m_REFERENCE = value ;
			}
			get
			{
				return m_REFERENCE;
			}
		}


		public IDbConnection  Connection
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
		public IDbTransaction  Transaction
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
		public spUpdate_Delivery_Challan()
		{
		}
		#endregion
		#region public Methods
		public bool  ExecuteQuery()
		{
			try
			{
			    IDbCommand cmd = ProviderFactory.GetCommand(EnumProviders.SQLClient);
				cmd.CommandType =  CommandType.StoredProcedure;
				cmd.CommandText = sp_Name;
				cmd.Connection =   m_connection;
				if(m_transaction!=null)
				{
					cmd.Transaction = m_transaction;
				}
				GetParameterCollection(ref cmd);
				cmd.ExecuteNonQuery();
				return true;
			}
			catch(Exception e)
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
				if(m_transaction!=null)
				{
					command.Transaction = m_transaction;
				}
				GetParameterCollection(ref command);
				IDataReader dr = command.ExecuteReader();
				return dr;
			}
			catch(Exception exp)
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
				if(m_transaction!=null)
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
			catch(Exception exp)
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
				if(m_transaction!=null)
				{
					command.Transaction = m_transaction;
				}
				GetParameterCollection(ref command);
				object o;
				o = command.ExecuteScalar();


				return o.ToString();
			}
			catch(Exception exp)
			{
				throw exp;
			}
			finally
			{
			}
		}
		public void FirstReader(IDataReader dr)
		{
			if(dr.Read())
			{
				m_PURCHASE_MASTER_ID=Convert.ToInt64(dr["PURCHASE_MASTER_ID"]);
				m_DriverName= Convert.ToString(dr["DriverName"]);
				m_VEHICLENO= Convert.ToString(dr["VEHICLENO"]);
				m_REFERENCE= Convert.ToString(dr["REFERENCE"]);
			}
		}
		public void GetParameterCollection(ref IDbCommand cmd)
		{
			IDataParameterCollection pparams = cmd.Parameters;
			IDataParameter parameter ;
			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@PURCHASE_MASTER_ID" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.BigInt);
			if(m_PURCHASE_MASTER_ID==Constants.LongNullValue)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_PURCHASE_MASTER_ID;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@DriverName" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_DriverName== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_DriverName;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@VEHICLENO" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_VEHICLENO== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_VEHICLENO;
			}
			pparams.Add(parameter);


			parameter = ProviderFactory.GetParameter(EnumProviders.SQLClient);
			parameter.ParameterName = "@REFERENCE" ; 
			parameter.DbType = ProviderFactory.GetDBType(EnumProviders.SQLClient, EnumDBTypes.VarChar);
			if(m_REFERENCE== null)
			{
				parameter.Value = DBNull.Value;
			}
			else
			{
				parameter.Value = m_REFERENCE;
			}
			pparams.Add(parameter);


		}
		#endregion
	}
}
