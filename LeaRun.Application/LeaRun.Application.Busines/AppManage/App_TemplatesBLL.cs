namespace LeaRun.Application.Busines.AppManage
{
    using LeaRun.Application.Entity.AppManage;
    using LeaRun.Application.IService.AppManage;
    using LeaRun.Application.Service.AppManage;
    using System;
    using System.Collections.Generic;

    public class App_TemplatesBLL
    {
        private App_TemplatesIService service = new App_TemplatesService();

        public App_TemplatesEntity GetEntity(string keyValue)
        {
            return this.service.GetEntity(keyValue);
        }

        public IEnumerable<App_TemplatesEntity> GetList(string queryJson)
        {
            return this.service.GetList(queryJson);
        }

        public void RemoveForm(string keyValue)
        {
            try
            {
                this.service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveForm(string keyValue, App_TemplatesEntity entity)
        {
            try
            {
                this.service.SaveForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

