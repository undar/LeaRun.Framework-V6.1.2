using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Application.Service.SystemManage;
using System;
using System.Collections.Generic;

namespace LeaRun.Application.Busines.SystemManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.11.25 11:02
    /// 描 述：数据库备份
    /// </summary>
    public class DataBaseBackupBLL
    {
        private IDataBaseBackupService service = new DataBaseBackupService();

        #region 获取数据
        /// <summary>
        /// 库备份列表
        /// </summary>
        /// <param name="dataBaseLinkId">连接库Id</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<DataBaseBackupEntity> GetList(string dataBaseLinkId, string queryJson)
        {
            return service.GetList(dataBaseLinkId, queryJson);
        }
        /// <summary>
        /// 库备份文件路径列表
        /// </summary>
        /// <param name="databaseBackupId">计划Id</param>
        /// <returns></returns>
        public IEnumerable<DataBaseBackupEntity> GetPathList(string databaseBackupId)
        {
            return service.GetPathList(databaseBackupId);
        }
        /// <summary>
        /// 库备份实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DataBaseBackupEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除库备份
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
        /// <summary>
        /// 保存库备份表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="dataBaseBackupEntity">库备份实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, DataBaseBackupEntity dataBaseBackupEntity)
        {
            try
            {
                service.SaveForm(keyValue, dataBaseBackupEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
