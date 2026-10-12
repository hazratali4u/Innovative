using System;
using System.Collections.Generic;
using System.Text;

namespace SAMSBusinessLayer.Models
{
    public class AssetsTypeModel
    {
        #region Private Members
        private int m_AssetTypeID;
        private int m_CompanyID;
        private int m_User_ID;
        private bool m_IS_DELETED;
        private bool m_IS_ACTIVE;
        private string m_AssetTypeName;
        #endregion
        #region Public Properties
        public int AssetTypeID
        {
            set { m_AssetTypeID = value; }
            get { return m_AssetTypeID; }
        }
        public int CompanyID
        {
            set
            {
                m_CompanyID = value;
            }
            get
            {
                return m_CompanyID;
            }
        }
        public int User_ID
        {
            set
            {
                m_User_ID = value;
            }
            get
            {
                return m_User_ID;
            }
        }
        public string AssetTypeName
        {
            set
            {
                m_AssetTypeName = value;
            }
            get
            {
                return m_AssetTypeName;
            }
        }
        public bool IS_DELETED
        {
            set { m_IS_DELETED = value; }
            get { return m_IS_DELETED; }
        }
        public bool IS_ACTIVE
        {
            set { m_IS_ACTIVE = value; }
            get { return m_IS_ACTIVE; }
        }
        #endregion
    }
}