using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using LeaRun.Util;
using LeaRun.Util.Extension;
using System.Data;
using System.Collections;
using System.Text;
using LeaRun.Data;
using System.Data.Common;
using LeaRun.Application.Code;

namespace LeaRun.Application.Service.SystemManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-03-31 15:17
    /// 描 述：Excel导入模板表
    /// </summary>
    public class ExcelImportService : RepositoryFactory, ExcelImportIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<ExcelImportEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return this.BaseRepository().FindList<ExcelImportEntity>(pagination);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ExcelImportEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<ExcelImportEntity>(keyValue);
        }
        public ExcelImportEntity GetEntityByModuleId(string keyValue)
        {
            var expression = LinqExtensions.True<ExcelImportEntity>();
            expression = expression.And(t => t.F_ModuleId.Equals(keyValue));
            return this.BaseRepository().FindEntity<ExcelImportEntity>(expression);
        }
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<ExcelImportFiledEntity> GetDetails(string keyValue)
        {
            return this.BaseRepository().FindList<ExcelImportFiledEntity>("select * from Base_ExcelImportFiled where F_ImportTemplateId='" + keyValue + "'");
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Delete<ExcelImportEntity>(keyValue);
                db.Delete<ExcelImportFiledEntity>(t => t.F_Id.Equals(keyValue));
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        public void UpdateState(string keyValue, int State)
        {
            ExcelImportEntity userEntity = new ExcelImportEntity();
            userEntity.Modify(keyValue);
            userEntity.F_EnabledMark = State;
            this.BaseRepository().Update(userEntity);
        }
        public void ImportExcel(string fid, DataTable dt, out DataTable Result)
        {
           
            //获得导入模板
            //模板主表
            ExcelImportEntity base_excellimport = GetEntity(fid);

            //string pkName = DatabaseCommon.GetKeyField("LeaRun.Entity." + base_excellimport.ImportTable).ToString();
            //模板明细表
            var listBase_ExcelImportDetail = GetDetails(fid);
            //构造导入返回结果表
            DataTable Newdt = new DataTable("Result");
            foreach (ExcelImportFiledEntity excelImportDetail in listBase_ExcelImportDetail)
            {
                if (excelImportDetail.F_RelationType == 0 || excelImportDetail.F_RelationType == 2 || excelImportDetail.F_RelationType == 3)
                {
                    Newdt.Columns.Add(excelImportDetail.F_ColName, typeof(System.String));
                }

            }
            Newdt.Columns.Add("learunColOk", typeof(System.String));                 //位置
            Newdt.Columns.Add("learunColError", typeof(System.String));                 //原因
            //取出要插入的表名
            string tableName = base_excellimport.F_DbTable;////////////////
            if (dt != null && dt.Rows.Count > 0)
            {
                IRepository db = this.BaseRepository().BeginTrans();
                try
                {
                    #region 遍历Excel数据行
                    //bool IsOk = true;
                    int learunColOk = 1;
                    string strError = "";
                    foreach (DataRow item in dt.Rows)
                    {
                        Hashtable entity = new Hashtable();//最终要插入数据库的hashtable
                        StringBuilder sb = new StringBuilder();
                        //entity[pkName] = Guid.NewGuid().ToString();//首先给主键赋值
                        DataRow dr = Newdt.NewRow();
                        dr = Newdt.NewRow();
                        
                        
                        #region 遍历模板，为每一行中每个字段找到模板列并赋值
                        int i = 0;
                        foreach (ExcelImportFiledEntity excelImportDetail in listBase_ExcelImportDetail)
                        {

                            string value = "";
                            if (excelImportDetail.F_RelationType == 0 || excelImportDetail.F_RelationType == 2 || excelImportDetail.F_RelationType == 3)
                            {
                                value = item[excelImportDetail.F_ColName].ToString();
                                dr[i] = value;
                            }
                          
                            #region 单个字段赋值
                            switch (excelImportDetail.F_RelationType)
                            {
                                //字符串
                                case 0:
                                    entity[excelImportDetail.F_FliedName] = value;
                                    i++;
                                    break;
                                //GUID
                                case 1:
                                    entity[excelImportDetail.F_FliedName] = Guid.NewGuid().ToString();
                                    break;
                                //数据字典
                                case 2:
                                    i++;
                                    entity[excelImportDetail.F_DbSaveFlied] = value;
                                    //查询Excel字符串是否存在于外表
                                    sb = DatabaseCommon.SelectSql("Base_DataItem");
                                    sb.Append(" and " + excelImportDetail.F_DataItemEncode + "='" + value + "'");
                                    DataTable dt0 = this.BaseRepository().FindTable(sb.ToString());
                                    if (dt0.Rows.Count == 0)
                                    {
                                        //不存在此外键
                                        learunColOk = 0;
                                        strError += "【 数据字典 】 找不到对应的数据";
                                    }
                                    sb.Remove(0,sb.Length);
                                    break;
                                //数据表
                                case 3:
                                    i++;
                                    entity[excelImportDetail.F_DbSaveFlied] = value;
                                    //查询Excel字符串是否存在于外表
                                    sb = DatabaseCommon.SelectSql(tableName);
                                    sb.Append(" and " + excelImportDetail.F_DbSaveFlied + "='" + value + "'");
                                    DataTable dt1 = this.BaseRepository().FindTable(sb.ToString());
                                    if (dt1.Rows.Count==0)
                                    {
                                        //不存在此外键
                                        learunColOk = 0;
                                        strError += "【" + excelImportDetail.F_ColName + "】 找不到对应的数据";
                                    }
                                    sb.Remove(0,sb.Length);
                                    break;
                                //固定数值
                                case 4:
                                    entity[excelImportDetail.F_FliedName] = excelImportDetail.F_Value;
                                    
                                    break;
                                //操作人
                                case 5:
                                    entity[excelImportDetail.F_FliedName] = OperatorProvider.Provider.Current().UserId;
                                    break;
                                //操作人名字
                                case 6:
                                    entity[excelImportDetail.F_FliedName] = OperatorProvider.Provider.Current().UserName;
                                    break;
                                //操作人时间
                                case 7:
                                    entity[excelImportDetail.F_FliedName] = DateTime.Now;
                                    break;
                                default:
                                    break;
                            }
                           
                            #endregion 单字段赋值结束
                          
                           
                        }
                        dr[i] = learunColOk;
                        dr[i + 1] = strError;
                        #endregion 遍历模板结束
                        //写入表
                        if (learunColOk == 0)
                        {
                            continue;
                        }
                        StringBuilder strSql = DatabaseCommon.InsertSql(tableName, entity);
                        DbParameter[] parameter = DatabaseCommon.GetParameter(entity);
                        Newdt.Rows.Add(dr);
                        db.ExecuteBySql(strSql.ToString(),parameter);
                        
                    }
                    #endregion 遍历Excel数据行结束
                    db.Commit();
                }
                catch (System.Exception)
                {
                    db.Rollback();
                    throw;
                }

            }
            Result = Newdt;
            
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, ExcelImportEntity entity, List<ExcelImportFiledEntity> entryList)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //主表
                    entity.Modify(keyValue);
                    db.Update(entity);
                    //明细
                    db.Delete<ExcelImportFiledEntity>(t => t.F_ImportTemplateId.Equals(keyValue));
                    foreach (ExcelImportFiledEntity item in entryList)
                    {
                        item.Create();
                        item.F_ImportTemplateId = entity.F_Id;
                        db.Insert(item);
                    }
                }
                else
                {
                    //主表
                    entity.Create();
                    db.Insert(entity);
                    //明细
                    foreach (ExcelImportFiledEntity item in entryList)
                    {
                        item.Create();
                        item.F_ImportTemplateId = entity.F_Id;
                        db.Insert(item);
                    }
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        #endregion
    }
}
