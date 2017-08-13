using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

using LeaRun.Util;

using LeaRun.Util.Extension;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using System;
using System.Text;
namespace LeaRun.Application.Service.SystemManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-04-28 16:58
    /// 描 述：数据源表
    /// </summary>
    public class DataSourceService : RepositoryFactory<DataSourceEntity>, DataSourceIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<DataSourceEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return this.BaseRepository().FindList(pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<DataSourceEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体GetTableDataList
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DataSourceEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        public DataTable GetTableDataList(string dataBaseLinkId, string tableName)
        {
            DataBaseLinkService link = new DataBaseLinkService();
            DataBaseLinkEntity dataBaseLinkEntity = link.GetEntity(dataBaseLinkId);
            if (dataBaseLinkEntity != null)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT * FROM " + tableName + "");
                return this.BaseRepository(dataBaseLinkEntity.DbConnection).FindTable(strSql.ToString());
            }
            return null;
        }
        public string TestData(string dbtype, string connection, string sql)
        {
            try
            {
                #region 测试连接
                SqlConnection dbConnection = null;
                string ServerAddress = "";
                switch (dbtype)
                {
                    case "SqlServer":
                        dbConnection = new SqlConnection(connection);
                        ServerAddress = dbConnection.DataSource;
                        
                        break;
                    default:
                        break;
                }
                SqlDataAdapter da = null;
                DataTable dt = new DataTable();
                try
                {
                    dbConnection.Open();
                    //return this.BaseRepository(dataBaseLinkEntity.DbConnection).FindTable(strSql.ToString());
                    da = new SqlDataAdapter(sql, dbConnection);
                    da.Fill(dt);
                    dbConnection.Close();
                    return dt.ToJson();
                }
                catch {

                    return "[{\"失败\":\"请检查数据连接或sql语句\"}]";
                }
             
               

                #endregion

            }
            catch (Exception)
            {
                return "[{\"失败\":\"请检查数据连接或sql语句\"}]";
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, DataSourceEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }
        #endregion
    }
}
