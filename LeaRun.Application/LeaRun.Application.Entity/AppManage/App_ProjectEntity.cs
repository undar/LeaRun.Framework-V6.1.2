namespace LeaRun.Application.Entity.AppManage
{
    using LeaRun.Application.Code;
    using LeaRun.Application.Entity;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;
    using System.Collections.Generic;
    public class App_ProjectEntity : BaseEntity
    {
        public override void Create()
        {
            this.F_Id = Guid.NewGuid().ToString();
            this.F_CreateDate = new DateTime?(DateTime.Now);
            this.F_CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.F_CreateUserName = OperatorProvider.Provider.Current().UserName;
        }

        public override void Modify(string keyValue)
        {
            this.F_Id = keyValue;
        }

        [Column("F_ID")]
        public string F_Id { get; set; }

        [Column("F_NAME")]
        public string F_Name { get; set; }
        [Column("F_ICON")]
        public string F_Icon { get; set; }
        [Column("F_IsTabed")]
        public int F_IsTabed { get; set; }
        [Column("F_DESCRIPTION")]
        public string F_Description { get; set; }
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }

        [Column("F_CREATEUSERID")]
        public string F_CreateUserId { get; set; }

        [Column("F_CREATEUSERNAME")]
        public string F_CreateUserName { get; set; }
        public List<App_TemplatesEntity> F_Templates { get; set; }
    }
}

