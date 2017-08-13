using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.Entity.WeChatManage;
using LeaRun.Application.IService.WeChatManage;
using LeaRun.Application.Service.WeChatManage;
using LeaRun.Util;
using LeaRun.Util.Extension;
using LeaRun.Util.WeChat.Model.Request;
using System.Collections.Generic;
using System.Linq;

namespace LeaRun.Application.Busines.WeChatManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.12.23 11:31
    /// 描 述：企业号部门
    /// </summary>
    public class WeChatOrganizeBLL
    {
        private IWeChatOrganizeService service = new WeChatOrganizeService();

        #region 获取数据
        /// <summary>
        /// 部门列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WeChatDeptRelationEntity> GetList()
        {
            return service.GetList();
        }
        /// <summary>
        /// 已删除部门列表
        /// </summary>
        /// <param name="weChatDeptRelationList"></param>
        /// <returns></returns>
        private IEnumerable<WeChatDeptRelationEntity> GetDeletedList(List<OrganizeEntity> organizelist)
        {
            var data = service.GetList().ToList();
            foreach (OrganizeEntity departmentEntity in organizelist)
            {
                data.RemoveAll(t => t.DeptId == departmentEntity.OrganizeId);
            }
            return data;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 同步部门
        /// </summary>
        /// <param name="organizeListJson">机构列表Json</param>
        /// <returns></returns>
        public void Synchronization(string organizeListJson)
        {
            List<WeChatDeptRelationEntity> weChatDeptRelationList = new List<WeChatDeptRelationEntity>();
            List<OrganizeEntity> organizelist = organizeListJson.ToList<OrganizeEntity>();

            #region 删除
            IEnumerable<WeChatDeptRelationEntity> DeletedList = this.GetDeletedList(organizelist);
            foreach (var item in DeletedList)
            {
                DepartmentDelete departmentDelete = new DepartmentDelete();
                departmentDelete.id = item.WeChatDeptId.ToString();
                int IsOk = departmentDelete.Send().errcode;
                if (IsOk == 0)
                {
                    service.RemoveForm(item.DeptRelationId);
                }
            }
            #endregion

            #region 添加、编辑
            foreach (OrganizeEntity departmentEntity in organizelist)
            {
                WeChatDeptRelationEntity weChatDeptRelationEntity = service.GetEntity(departmentEntity.OrganizeId);
                if (weChatDeptRelationEntity == null)
                {
                    #region 添加
                    DepartmentCreate departmentCreate = new DepartmentCreate();
                    departmentCreate.name = departmentEntity.FullName;
                    departmentCreate.parentid = departmentEntity.ParentId == "0" ? "1" : weChatDeptRelationList.Find(t => t.DeptId == departmentEntity.ParentId).WeChatDeptId.ToString();
                    string WeChatDeptId = departmentCreate.Send().id;
                    if (WeChatDeptId != null)
                    {
                        weChatDeptRelationEntity = new WeChatDeptRelationEntity();
                        weChatDeptRelationEntity.DeptRelationId = departmentEntity.OrganizeId;
                        weChatDeptRelationEntity.DeptId = departmentEntity.OrganizeId;
                        weChatDeptRelationEntity.DeptName = departmentEntity.FullName;
                        weChatDeptRelationEntity.WeChatDeptId = WeChatDeptId.ToInt();//企业号创建部门的返回Id
                        weChatDeptRelationList.Add(weChatDeptRelationEntity);
                        service.SaveForm("", weChatDeptRelationEntity);
                    }
                    #endregion
                }
                else
                {
                    #region 编辑
                    weChatDeptRelationList.Add(weChatDeptRelationEntity);
                    DepartmentUpdate departmentUpdate = new DepartmentUpdate();
                    departmentUpdate.name = departmentEntity.FullName;
                    departmentUpdate.parentid = departmentEntity.ParentId == "0" ? "1" : weChatDeptRelationList.Find(t => t.DeptId == departmentEntity.ParentId).WeChatDeptId.ToString();
                    departmentUpdate.id = weChatDeptRelationEntity.WeChatDeptId.ToString();
                    int IsOk = departmentUpdate.Send().errcode;
                    if (IsOk == 0)
                    {
                        service.SaveForm(weChatDeptRelationEntity.DeptRelationId, weChatDeptRelationEntity);
                    }
                    #endregion
                }
            }
            #endregion
        }
        #endregion
    }
}
