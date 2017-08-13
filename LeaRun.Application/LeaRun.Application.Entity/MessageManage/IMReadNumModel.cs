
namespace LeaRun.Application.Entity.MessageManage
{
    public class IMReadNumModel
    {
        /// <summary>
        /// 消息发送者
        /// </summary>
        public string SendId { set; get; }
        /// <summary>
        /// 消息接收者
        /// </summary>
        public string UserId { set; get; }
        /// <summary>
        /// 消息数量
        /// </summary>
        public int unReadNum { set; get; }
        /// <summary>
        /// 联系人Id
        /// </summary>
        public string OtherId { set; get; }
    }
}
