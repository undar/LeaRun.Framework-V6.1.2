using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Application.Service.SystemManage;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace LeaRun.Application.Busines.SystemManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.11.18 11:02
    /// 描 述：数据库连接管理
    /// </summary>
    public class DataBaseLinkBLL
    {
        private IDataBaseLinkService service = new DataBaseLinkService();

        #region 获取数据
        /// <summary>
        /// 库连接列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DataBaseLinkEntity> GetList()
        {
            return service.GetList();
        }
        /// <summary>
        /// 库连接实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DataBaseLinkEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除库连接
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void TestConnection( string dbtype, string connection)
        {
            try
            {
                #region 测试连接数据库
                DbConnection dbConnection = null;
                string ServerAddress = "";
                switch (dbtype)
                {
                    case "SqlServer":
                        dbConnection = new SqlConnection(connection);
                        ServerAddress = dbConnection.DataSource;
                        dbConnection.Open();
                        break;
                    default:
                        break;
                }
                dbConnection.Close();
           
                #endregion
               
            }
            catch (Exception) 
            {
                throw new Exception("连接失败！");
            }
        }
        /// <summary>
        /// 保存库连接表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="databaseLinkEntity">库连接实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, DataBaseLinkEntity databaseLinkEntity)
        {
            try
            {
                #region 测试连接数据库
                DbConnection dbConnection = null;
                string ServerAddress = "";
                switch (databaseLinkEntity.DbType)
                {
                    case "SqlServer":
                        dbConnection = new SqlConnection(databaseLinkEntity.DbConnection);
                        ServerAddress = dbConnection.DataSource;
                        break;
                    default:
                        break;
                }
                dbConnection.Close();
                databaseLinkEntity.ServerAddress = ServerAddress;
                #endregion
                service.SaveForm(keyValue, databaseLinkEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
