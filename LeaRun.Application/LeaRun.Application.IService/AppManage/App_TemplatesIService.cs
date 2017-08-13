namespace LeaRun.Application.IService.AppManage
{
    using LeaRun.Application.Entity.AppManage;
    using System;
    using System.Collections.Generic;

    public interface App_TemplatesIService
    {
        App_TemplatesEntity GetEntity(string keyValue);
        IEnumerable<App_TemplatesEntity> GetList(string queryJson);
        void RemoveForm(string keyValue);
        void SaveForm(string keyValue, App_TemplatesEntity entity);
    }
}

