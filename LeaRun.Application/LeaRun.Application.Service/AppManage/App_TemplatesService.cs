namespace LeaRun.Application.Service.AppManage
{
    using LeaRun.Application.Entity.AppManage;
    using LeaRun.Application.IService.AppManage;
    using LeaRun.Data.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class App_TemplatesService :RepositoryFactory, App_TemplatesIService
    {
        public App_TemplatesEntity GetEntity(string keyValue)
        {
            return base.BaseRepository().FindEntity<App_TemplatesEntity>(keyValue);
        }

        public IEnumerable<App_TemplatesEntity> GetList(string queryJson)
        {
            return this.BaseRepository().FindList<App_TemplatesEntity>("select * from App_Templates where F_ProjectId='" + queryJson + "'");       
        }

        public void RemoveForm(string keyValue)
        {
            base.BaseRepository().ExecuteBySql("delete from App_Templates where F_ProjectId='" + keyValue + "'");
        }

        public void SaveForm(string keyValue, App_TemplatesEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                base.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                base.BaseRepository().Insert(entity);
            }
        }
    }
}

