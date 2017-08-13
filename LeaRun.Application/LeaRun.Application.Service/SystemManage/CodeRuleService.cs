using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Data.Repository;
using LeaRun.Util;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeaRun.Application.Service.SystemManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.12.21 16:19
    /// 描 述：编号规则
    /// </summary>
    public class CodeRuleService : RepositoryFactory<CodeRuleEntity>, ICodeRuleService
    {
        #region 获取数据
        /// <summary>
        /// 规则列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<CodeRuleEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<CodeRuleEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "EnCode":              //对象编号
                        expression = expression.And(t => t.EnCode.Contains(keyword));
                        break;
                    case "FullName":            //对象名称
                        expression = expression.And(t => t.FullName.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 规则实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public CodeRuleEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除规则
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存规则表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="codeRuleEntity">规则实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, CodeRuleEntity codeRuleEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                codeRuleEntity.Modify(keyValue);
                this.BaseRepository().Update(codeRuleEntity);
            }
            else
            {
                codeRuleEntity.Create();
                this.BaseRepository().Insert(codeRuleEntity);
            }
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 规则编号不能重复
        /// </summary>
        /// <param name="enCode">编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistEnCode(string enCode, string keyValue)
        {
            var expression = LinqExtensions.True<CodeRuleEntity>();
            expression = expression.And(t => t.EnCode == enCode);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.RuleId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        /// <summary>
        /// 规则名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            var expression = LinqExtensions.True<CodeRuleEntity>();
            expression = expression.And(t => t.FullName == fullName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.RuleId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion

        #region 单据编码处理
        /// <summary>
        /// 获得指定模块或者编号的单据号
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="moduleId">模块ID</param>
        /// <param name="enCode">模板编码</param>
        /// <returns>单据号</returns>
        public string GetBillCode(string userId, string moduleId, string enCode)
        {
            IRepository db = new RepositoryFactory().BaseRepository();
            UserEntity userEntity = db.FindEntity<UserEntity>(userId);
            CodeRuleEntity coderuleentity = db.FindEntity<CodeRuleEntity>(t => t.ModuleId == moduleId || t.EnCode == enCode);
            //判断种子是否已经产生,如果没有产生种子先插入一条初始种子
            CodeRuleSeedEntity initSeed = db.FindEntity<CodeRuleSeedEntity>(t => t.RuleId == coderuleentity.RuleId);
            if (initSeed == null)
            {
                initSeed = new CodeRuleSeedEntity();
                initSeed.Create();
                initSeed.SeedValue = 1;
                initSeed.RuleId = coderuleentity.RuleId;
                db.Insert<CodeRuleSeedEntity>(initSeed);
            }
            else
            {
                db = new RepositoryFactory().BaseRepository().BeginTrans();
            }
            //获得模板ID
            string billCode = "";//单据号
            string nextBillCode = "";//单据号
            bool isOutTime = false;//是否已过期
            if (coderuleentity != null)
            {
                try
                {
                    int nowSerious = 0;
                    //取得流水号种子
                    List<CodeRuleSeedEntity> codeRuleSeedlist = db.IQueryable<CodeRuleSeedEntity>(t => t.RuleId == coderuleentity.RuleId).ToList();
                    //取得最大种子
                    CodeRuleSeedEntity maxSeed = db.FindEntity<CodeRuleSeedEntity>(t => t.UserId == null);
                    #region 处理隔天流水号归0
                    //首先确定最大种子是否是隔天未归0的
                    if ((maxSeed.ModifyDate).ToDateString() != DateTime.Now.ToString("yyyy-MM-dd"))
                    {
                        isOutTime = true;
                        maxSeed.SeedValue = 1;
                        maxSeed.ModifyDate = DateTime.Now;
                    }
                    #endregion
                    List<CodeRuleFormatEntity> codeRuleFormatList = coderuleentity.RuleFormatJson.ToList<CodeRuleFormatEntity>();
                    foreach (CodeRuleFormatEntity codeRuleFormatEntity in codeRuleFormatList)
                    {
                        switch (codeRuleFormatEntity.ItemType.ToString())
                        {
                            //自定义项
                            case "0":
                                billCode = billCode + codeRuleFormatEntity.FormatStr;
                                nextBillCode = nextBillCode + codeRuleFormatEntity.FormatStr;
                                break;
                            //日期
                            case "1":
                                //日期字符串类型
                                billCode = billCode + DateTime.Now.ToString(codeRuleFormatEntity.FormatStr.Replace("m", "M"));
                                nextBillCode = nextBillCode + DateTime.Now.ToString(codeRuleFormatEntity.FormatStr.Replace("m", "M"));

                                break;
                            //流水号
                            case "2":
                                //查找当前用户是否已有之前未用掉的种子
                                CodeRuleSeedEntity codeRuleSeedEntity = codeRuleSeedlist.Find(t => t.UserId == userId && t.RuleId == coderuleentity.RuleId);
                                //删除已过期的用户未用掉的种子
                                if (codeRuleSeedEntity != null && isOutTime)
                                {
                                    db.Delete<CodeRuleSeedEntity>(codeRuleSeedEntity);
                                    codeRuleSeedEntity = null;
                                }
                                //如果没有就取当前最大的种子
                                if (codeRuleSeedEntity != null)
                                {
                                    nowSerious = (int)codeRuleSeedEntity.SeedValue;
                                }
                                else
                                {
                                    //取得系统最大的种子
                                    int maxSerious = (int)maxSeed.SeedValue;
                                    nowSerious = maxSerious;
                                    codeRuleSeedEntity = new CodeRuleSeedEntity();
                                    codeRuleSeedEntity.Create();
                                    codeRuleSeedEntity.SeedValue = maxSerious;
                                    codeRuleSeedEntity.UserId = userId;
                                    codeRuleSeedEntity.RuleId = coderuleentity.RuleId;
                                    db.Insert<CodeRuleSeedEntity>(codeRuleSeedEntity);
                                    //处理种子更新
                                    maxSeed.SeedValue += 1;
                                    maxSeed.Modify(maxSeed.RuleSeedId);
                                    db.Update<CodeRuleSeedEntity>(maxSeed);
                                }
                                string seriousStr = new string('0', (int)(codeRuleFormatEntity.FormatStr.Length - nowSerious.ToString().Length)) + nowSerious.ToString();
                                string NextSeriousStr = new string('0', (int)(codeRuleFormatEntity.FormatStr.Length - nowSerious.ToString().Length)) + maxSeed.SeedValue.ToString();
                                billCode = billCode + seriousStr;
                                nextBillCode = nextBillCode + NextSeriousStr;

                                break;
                            //部门
                            case "3":
                                DepartmentEntity departmentEntity = db.FindEntity<DepartmentEntity>(userEntity.DepartmentId);
                                if (codeRuleFormatEntity.FormatStr == "code")
                                {
                                    billCode = billCode + departmentEntity.EnCode;
                                    nextBillCode = nextBillCode + departmentEntity.EnCode;
                                }
                                else
                                {
                                    billCode = billCode + departmentEntity.FullName;
                                    nextBillCode = nextBillCode + departmentEntity.FullName;

                                }
                                break;
                            //公司
                            case "4":
                                OrganizeEntity organizeEntity = db.FindEntity<OrganizeEntity>(userEntity.OrganizeId);
                                if (codeRuleFormatEntity.FormatStr == "code")
                                {
                                    billCode = billCode + organizeEntity.EnCode;
                                    nextBillCode = nextBillCode + organizeEntity.EnCode;

                                }
                                else
                                {
                                    billCode = billCode + organizeEntity.FullName;
                                    nextBillCode = nextBillCode + organizeEntity.FullName;

                                }
                                break;
                            //用户
                            case "5":
                                if (codeRuleFormatEntity.FormatStr == "code")
                                {
                                    billCode = billCode + userEntity.EnCode;
                                    nextBillCode = nextBillCode + userEntity.EnCode;
                                }
                                else
                                {
                                    billCode = billCode + userEntity.Account;
                                    nextBillCode = nextBillCode + userEntity.Account;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    coderuleentity.CurrentNumber = nextBillCode;
                    db.Update<CodeRuleEntity>(coderuleentity);
                }
                catch (Exception)
                {
                    db.Rollback();
                    return billCode;
                }
                db.Commit();
            }
            return billCode;
        }
        /// <summary>
        /// 获得指定模块或者编号的单据号（直接使用）
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="moduleId">模块ID</param>
        /// <param name="enCode">模板编码</param>
        /// <returns>单据号</returns>
        public string SetBillCode(string userId, string moduleId, string enCode, IRepository db = null)
        {
            IRepository dbc = null;
            if (db == null)
            {
                dbc = new RepositoryFactory().BaseRepository();
            }
            else
            {
                dbc = db;
            }
            UserEntity userEntity = db.FindEntity<UserEntity>(userId);
            CodeRuleEntity coderuleentity = db.FindEntity<CodeRuleEntity>(t => t.ModuleId == moduleId || t.EnCode == enCode);
            //判断种子是否已经产生,如果没有产生种子先插入一条初始种子
            CodeRuleSeedEntity initSeed = db.FindEntity<CodeRuleSeedEntity>(t => t.RuleId == coderuleentity.RuleId);
            if (initSeed == null)
            {
                initSeed = new CodeRuleSeedEntity();
                initSeed.Create();
                initSeed.SeedValue = 1;
                initSeed.RuleId = coderuleentity.RuleId;
                initSeed.CreateDate = null;
                //db.Insert<CodeRuleSeedEntity>(initSeed);
            }
            //获得模板ID
            string billCode = "";//单据号
            string nextBillCode = "";//单据号
            bool isOutTime = false;//是否已过期
            if (coderuleentity != null)
            {
                try
                {
                    int nowSerious = 0;
                    //取得流水号种子
                    List<CodeRuleSeedEntity> codeRuleSeedlist = db.IQueryable<CodeRuleSeedEntity>(t => t.RuleId == coderuleentity.RuleId).ToList();
                    //取得最大种子
                    CodeRuleSeedEntity maxSeed = db.FindEntity<CodeRuleSeedEntity>(t => t.UserId == null);
                    #region 处理隔天流水号归0
                    //首先确定最大种子是否是隔天未归0的
                    if ((maxSeed.ModifyDate).ToDateString() != DateTime.Now.ToString("yyyy-MM-dd"))
                    {
                        isOutTime = true;
                        maxSeed.SeedValue = 1;
                        maxSeed.ModifyDate = DateTime.Now;
                    }
                    #endregion
                    if (maxSeed == null)
                    {
                        maxSeed = initSeed;
                    }
                    List<CodeRuleFormatEntity> codeRuleFormatList = coderuleentity.RuleFormatJson.ToList<CodeRuleFormatEntity>();
                    foreach (CodeRuleFormatEntity codeRuleFormatEntity in codeRuleFormatList)
                    {
                        switch (codeRuleFormatEntity.ItemType.ToString())
                        {
                            //自定义项
                            case "0":
                                billCode = billCode + codeRuleFormatEntity.FormatStr;
                                nextBillCode = nextBillCode + codeRuleFormatEntity.FormatStr;
                                break;
                            //日期
                            case "1":
                                //日期字符串类型
                                billCode = billCode + DateTime.Now.ToString(codeRuleFormatEntity.FormatStr.Replace("m", "M"));
                                nextBillCode = nextBillCode + DateTime.Now.ToString(codeRuleFormatEntity.FormatStr.Replace("m", "M"));

                                break;
                            //流水号
                            case "2":
                                //查找当前用户是否已有之前未用掉的种子
                                CodeRuleSeedEntity codeRuleSeedEntity = codeRuleSeedlist.Find(t => t.UserId == userId && t.RuleId == coderuleentity.RuleId);
                                //删除已过期的用户未用掉的种子
                                if (codeRuleSeedEntity != null && isOutTime)
                                {
                                    db.Delete<CodeRuleSeedEntity>(codeRuleSeedEntity);
                                    codeRuleSeedEntity = null;
                                }
                                //如果没有就取当前最大的种子
                                if (codeRuleSeedEntity == null)
                                {
                                    //取得系统最大的种子
                                    int maxSerious = (int)maxSeed.SeedValue;
                                    nowSerious = maxSerious;
                                    codeRuleSeedEntity = new CodeRuleSeedEntity();
                                    codeRuleSeedEntity.Create();
                                    codeRuleSeedEntity.SeedValue = maxSerious;
                                    codeRuleSeedEntity.UserId = userId;
                                    codeRuleSeedEntity.RuleId = coderuleentity.RuleId;
                                    //db.Insert<CodeRuleSeedEntity>(codeRuleSeedEntity);
                                    //处理种子更新
                                    maxSeed.SeedValue += 1;
                                    if (maxSeed.CreateDate != null)
                                    {
                                        db.Update<CodeRuleSeedEntity>(maxSeed);
                                    }
                                    else
                                    {
                                        maxSeed.CreateDate = DateTime.Now;
                                        db.Insert<CodeRuleSeedEntity>(maxSeed);
                                    }
                                }
                                else
                                {
                                    nowSerious = (int)codeRuleSeedEntity.SeedValue;
                                }
                                string seriousStr = new string('0', (int)(codeRuleFormatEntity.FormatStr.Length - nowSerious.ToString().Length)) + nowSerious.ToString();
                                string NextSeriousStr = new string('0', (int)(codeRuleFormatEntity.FormatStr.Length - nowSerious.ToString().Length)) + maxSeed.SeedValue.ToString();
                                billCode = billCode + seriousStr;
                                nextBillCode = nextBillCode + NextSeriousStr;

                                break;
                            //部门
                            case "3":
                                DepartmentEntity departmentEntity = db.FindEntity<DepartmentEntity>(userEntity.DepartmentId);
                                if (codeRuleFormatEntity.FormatStr == "code")
                                {
                                    billCode = billCode + departmentEntity.EnCode;
                                    nextBillCode = nextBillCode + departmentEntity.EnCode;
                                }
                                else
                                {
                                    billCode = billCode + departmentEntity.FullName;
                                    nextBillCode = nextBillCode + departmentEntity.FullName;

                                }
                                break;
                            //公司
                            case "4":
                                OrganizeEntity organizeEntity = db.FindEntity<OrganizeEntity>(userEntity.OrganizeId);
                                if (codeRuleFormatEntity.FormatStr == "code")
                                {
                                    billCode = billCode + organizeEntity.EnCode;
                                    nextBillCode = nextBillCode + organizeEntity.EnCode;

                                }
                                else
                                {
                                    billCode = billCode + organizeEntity.FullName;
                                    nextBillCode = nextBillCode + organizeEntity.FullName;

                                }
                                break;
                            //用户
                            case "5":
                                if (codeRuleFormatEntity.FormatStr == "code")
                                {
                                    billCode = billCode + userEntity.EnCode;
                                    nextBillCode = nextBillCode + userEntity.EnCode;
                                }
                                else
                                {
                                    billCode = billCode + userEntity.Account;
                                    nextBillCode = nextBillCode + userEntity.Account;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    coderuleentity.CurrentNumber = nextBillCode;
                    db.Update<CodeRuleEntity>(coderuleentity);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return billCode;
        }
        /// <summary>
        /// 占用单据号
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="moduleId">模块ID</param>
        /// <param name="enCode">模板编码</param>
        /// <returns>true/false</returns>
        public bool UseRuleSeed(string userId, string moduleId, string enCode, IRepository db = null)
        {
            IRepository dbc = null;
            if (db == null)
            {
                dbc = new RepositoryFactory().BaseRepository();
            }
            else
            {
                dbc = db;
            }
            UserEntity userEntity = dbc.FindEntity<UserEntity>(userId);
            CodeRuleEntity coderuleentity = dbc.FindEntity<CodeRuleEntity>(t => t.ModuleId == moduleId || t.EnCode == enCode);
            try
            {
                if (coderuleentity != null)
                {
                    //删除用户已经用掉的种子
                    dbc.Delete<CodeRuleSeedEntity>(t => t.UserId == userId && t.RuleId == coderuleentity.RuleId);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
