
namespace LeaRun.Application.Entity.MessageManage
{
    public class IMGroupModel
    {
        /// <summary>
        /// 群组Id
        /// </summary>
        public string GroupId { set; get; }
        /// <summary>
        /// 群组名称
        /// </summary>
        public string GroupName { set; get; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { set; get; }
        /// <summary>
        /// 群联系人对应表主键Id
        /// </summary>
        public string UserGroupId { set; get; }
    }
}
