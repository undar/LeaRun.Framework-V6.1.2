namespace LeaRun.Application.Service.AppManage
{
    using LeaRun.Application.Entity.AppManage;
    using LeaRun.Application.IService.AppManage;
    using LeaRun.Data.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class App_ProjectService : RepositoryFactory, App_ProjectIService
    {
        public App_ProjectEntity GetEntity(string keyValue)
        {
            return base.BaseRepository().FindEntity<App_ProjectEntity>(keyValue);
        }

        public IEnumerable<App_ProjectEntity> GetList(string queryJson)
        {
            return base.BaseRepository().FindList<App_ProjectEntity>("select * from App_Project order by F_CreateDate desc");       
        }

        public void RemoveForm(string keyValue)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Delete<App_ProjectEntity>(keyValue);
                //db.Delete<App_TemplatesEntity>(t => t.F_ProjectId.Equals(keyValue));//提示错误，异常: 列名 'App_ProjectEntity_F_Id' 无效。
                db.ExecuteBySql("delete from App_Templates where F_ProjectId='" + keyValue + "'");
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }

        public void SaveForm(string keyValue, App_ProjectEntity entity, List<App_TemplatesEntity> entryList)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //主表
                    //entity.Modify(keyValue);
                    //entity.F_Templates = null;
                    
                    //明细
                    db.ExecuteBySql("delete from App_Templates where F_ProjectId='" + keyValue + "'");
                    db.ExecuteBySql("update [App_Project] set F_Name='" + entity.F_Name + "',F_Icon='" + entity.F_Icon + "',F_IsTabed=" + entity.F_IsTabed + ",F_Description='" + entity.F_Description + "' where F_Id='"+keyValue+"'");
                    foreach (App_TemplatesEntity item in entryList)
                    {
                        item.Create();
                        item.F_ProjectId = keyValue;
                        //db.Insert(item);
                        db.ExecuteBySql("insert into App_Templates([F_Id],[F_ProjectId],[F_Name],[F_Value],[F_Type],[F_Parent],[F_level],[F_img],[F_Content],[F_CreateDate],[F_CreateUserId],[F_CreateUserName]) values('" + item.F_Id + "','" + item.F_ProjectId + "','" + item.F_Name + "','" + item.F_Value + "','" + item.F_Type + "','" + item.F_Parent + "'," + item.F_level + ",'" + item.F_img + "','" + item.F_Content + "','" + item.F_CreateDate + "','" + item.F_CreateUserId + "','" + item.F_CreateUserName + "')");
                    }
                }
                else
                {
                    //主表
                    entity.Create();
                    entity.F_Templates = null;
                    db.Insert(entity);
                    //明细
                    foreach (App_TemplatesEntity item in entryList)
                    {
                        item.Create();
                        item.F_ProjectId = entity.F_Id;
                        db.ExecuteBySql("insert into App_Templates([F_Id],[F_ProjectId],[F_Name],[F_Value],[F_Type],[F_Parent],[F_level],[F_img],[F_Content],[F_CreateDate],[F_CreateUserId],[F_CreateUserName]) values('" + item.F_Id + "','" + item.F_ProjectId + "','" + item.F_Name + "','" + item.F_Value + "','" + item.F_Type + "','" + item.F_Parent + "'," + item.F_level + ",'" + item.F_img + "','" + item.F_Content + "','" + item.F_CreateDate + "','" + item.F_CreateUserId + "','" + item.F_CreateUserName + "')");
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
    }
}

