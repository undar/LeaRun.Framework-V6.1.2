using LeaRun.Application.Busines.MessageManage;
using LeaRun.Application.Entity.MessageManage;
using LeaRun.Util.WebControl;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace LeaRun.SOA.IM
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2015.11.25 15:48
    /// 描 述：即使通信服务
    /// 可供客户端调用的方法开头用小写
    /// </summary>
    [HubName("ChatsHub")]
    public class Chats : Hub
    {
        private IMGroupBLL imgroupbll = new IMGroupBLL();
        private IMContentBLL imcontentbll = new IMContentBLL();

        #region 重载Hub方法
        /// <summary>
        /// 建立连接
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            UpdateUserClientId(1);
            SendUserList();
            imSendLastUser();
            return base.OnConnected();
        }
        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="stopCalled">是否是客户端主动断开：true是,false超时断开</param>
        /// <returns></returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            UpdateUserClientId(0);
            //SendUserList("OnDisconnected");
            return base.OnDisconnected(stopCalled);
        }
        /// <summary>
        /// 重新建立连接
        /// </summary>
        /// <returns></returns>
        public override Task OnReconnected()
        {
            UpdateUserClientId(1);
            //SendUserList("OnReconnected");
            //SendLastUser();
            return base.OnReconnected();
        }
        #endregion

        #region 联系人列表操作
        /// <summary>
        /// 发送用户列表到前端
        /// </summary>
        private void SendUserList()
        {
            List<string> onLineList = new List<string>();
            foreach (var item in UserStorage.userOnlineList)
            {
                if (item.Key != GetUserId())
                {
                    onLineList.Add(item.Key);
                }
            }
            Clients.Caller.IMUpdateUserList(UserStorage.userAllList, onLineList);
        }
        private void SendUserListToOthers()
        {
            List<string> onLineList = new List<string>();
            foreach (var item in UserStorage.userOnlineList)
            {
                if (item.Key != GetUserId())
                {
                    onLineList.Add(item.Key);
                }
            }
            Clients.Others.IMUpdateUserList(UserStorage.userAllList, onLineList);
        }
        /// <summary>
        /// 刷新用户列表在线状态
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="isOnLine">是否在线:0离线,1在线</param>
        private void UpdateUserListState(string userId,int isOnLine)
        {
            if (UserStorage.userAllList.ContainsKey(userId))
            {
                Clients.Others.IMUpdateUserStatus(userId, isOnLine);
                UserStorage.userAllList[userId].UserOnLine = isOnLine;
            }
        }
        /// <summary>
        /// 更新用户组连接Id信息
        /// </summary>
        /// <returns></returns>
        /// <param name="isOnLine">是否在线:0离线,1在线</param>
        private void UpdateUserClientId(int isOnLine)
        {
            Console.WriteLine(GetUserId() + "：" + isOnLine);
            string clientId = Context.ConnectionId;
            if (Context.QueryString["userId"] != null)
            {
                string userId = Context.QueryString["userId"];
                if (isOnLine == 0)//离线
                {
                    if (UserStorage.userOnlineList.ContainsKey(userId))
                    {
                        if (UserStorage.userOnlineList[userId].IndexOf(clientId) > -1)
                        {
                            UserStorage.userOnlineList[userId].Remove(clientId);
                            Groups.Remove(clientId, userId);
                            if (UserStorage.userOnlineList[userId].Count <= 0)
                            {
                                UserStorage.userOnlineList.Remove(userId);
                                UpdateUserListState(userId, isOnLine);
                            }
                        }
                    }
                }
                else//上线
                {
                    if (UserStorage.userOnlineList.ContainsKey(userId))
                    {
                        if (UserStorage.userOnlineList[userId].IndexOf(clientId) == -1)
                        {
                            UserStorage.userOnlineList[userId].Add(clientId);
                            Groups.Add(clientId, userId);
                        }
                    }
                    else
                    {
                        UserStorage.userOnlineList.Add(userId, new List<string>());
                        UserStorage.userOnlineList[userId].Add(clientId);
                        Groups.Add(clientId, userId);
                        UpdateUserListState(userId, isOnLine);
                    }
                }
            }
            else if (isOnLine == 0)//离线
            {
                foreach (var item in UserStorage.userOnlineList)
                {
                    if (item.Value.IndexOf(clientId) > -1)
                    {
                        item.Value.Remove(clientId);
                        Groups.Remove(clientId, item.Key);
                        if (item.Value.Count <= 0)
                        {
                            UserStorage.userOnlineList.Remove(item.Key);
                            UpdateUserListState(item.Key, isOnLine);
                        }
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 获取登录用户Id
        /// </summary>
        /// <returns></returns>
        private string GetUserId()
        {
            string userId = "";
            if (Context.QueryString["userId"] != null)
            {
                userId = Context.QueryString["userId"];
            } 
            return userId;
        }
        /// <summary>
        /// 获取最新联系人列表
        /// </summary>
        public void imSendLastUser()
        {
            string userId = GetUserId();
            Dictionary<string,IMReadNumModel> dic = new Dictionary<string,IMReadNumModel>();
            IEnumerable<IMReadNumModel> allmodelist = imcontentbll.GetReadList(userId);
            foreach (var item in allmodelist)
            {
                item.OtherId = item.SendId == userId ? item.UserId : item.SendId;
                if (!dic.ContainsKey(item.OtherId))
                {
                    dic.Add(item.OtherId, item);
                }
            }
            Clients.Caller.IMUpdateLastUser(dic);
        }

        #endregion

        #region 群组操作
        /// <summary>
        /// 获取群组联系信息
        /// </summary>
        private IEnumerable<IMGroupModel> GetGroupList()
        {
            try
            {
                string userId = GetUserId();
                IEnumerable<IMGroupModel> model = imgroupbll.GetList(userId);
                return model;
            }
            catch (Exception)
            {
                return null;   
            }
        }
        /// <summary>
        /// 创建一个组
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="userIdList"></param>
        /// <returns></returns>
        public int createGroup(string groupName, List<string> userIdList)
        {
            try
            {
                string userId = GetUserId();
                string userName = UserStorage.userAllList[userId].RealName;
                imgroupbll.Create(groupName, userId, userName, userIdList);
            }
            catch (Exception)
            {
                return 0;
            }
            return 1;
        }
        /// <summary>
        /// 修改群组名字
        /// </summary>
        /// <returns></returns>
        public int updateGroupName(string groupId,string groupName)
        {
            try
            {
                imgroupbll.UpdateName(groupId, groupName);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        /// <summary>
        /// 往组里增加一个用户
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int addGroupUserId(string groupId, string userId)
        {
            try
            {
                string createUserId = GetUserId();
                string createUserName = UserStorage.userAllList[userId].RealName;
                imgroupbll.AddUserId(groupId, userId, createUserId, createUserName);
            }
            catch (Exception)
            {
                return 0;
            }
            return 1;
        }
        /// <summary>
        /// 移除一个用户从群组里
        /// </summary>
        /// <param name="userGroupId"></param>
        /// <returns></returns>
        public int removeGroupUserId(string userGroupId)
        {
            try
            {
                imgroupbll.RemoveUserId(userGroupId);
            }
            catch (Exception)
            {
                return 0;
            }
            return 1;
        }
        #endregion

        #region 发送消息
        /// <summary>
        /// 发送消息单对单
        /// </summary>
        /// <param name="toUser"></param>
        /// <param name="message"></param>
        public void imSendToOne(string toUser, string message)
        {
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            string userId = GetUserId();
            string userName = UserStorage.userAllList[userId].RealName;
            imcontentbll.AddOneToOne(toUser, userId, userName, message);
            Clients.Group(userId).RevMessage(userId, message, dateTime);
            Clients.Group(toUser).RevMessage(userId, message, dateTime);
        }
        /// <summary>
        /// 发送消息发送给群组
        /// </summary>
        /// <param name="toGroup"></param>
        /// <param name="message"></param>
        public void imSendToGroup(string toGroup, string message)
        {
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            string userId = GetUserId();
            string userName = UserStorage.userAllList[userId].RealName;
            DataTable dt = new DataTable();
            imcontentbll.AddGroup(toGroup, userId, userName, message, out dt);
            foreach (DataRow dr in dt.Rows)
            {
                Clients.Group(dr["UserId"].ToString()).RevGroupMessage(userId, toGroup, message, dateTime);
            }
        }
        /// <summary>
        /// 确认消息已读
        /// </summary>
        /// <param name="sendId"></param>
        public int updateMessageStatus(string sendId)
        {
            string userId = GetUserId();
            imcontentbll.UpDateSatus(userId, sendId, "2");
            return 1;
        }
        /// <summary>
        /// 获取与用户的聊天记录
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="userId"></param>
        /// <param name="sendId"></param>
        /// <returns></returns>
        public IEnumerable<IMReadModel> getMsgList(Pagination pagination, string sendId)
        {
            string userId = GetUserId();
            imcontentbll.UpDateSatus(userId, sendId, "2");
            return imcontentbll.GetListOneToOne(pagination, userId,sendId);;
        }
        /// <summary>
        /// 获取某个状态的消息条数
        /// </summary>
        /// <param name="status">消息的状态,0未读,2已读</param>
        /// <returns></returns>
        public string getMsgNumList(string status)
        {
            string userId = GetUserId();
            return imcontentbll.GetReadAllNum(userId, status);
        }
        #endregion

        #region 服务端后台调用方法
        /// <summary>
        /// 更新用户列表
        /// </summary>
        /// <returns></returns>
        public bool upDateUserList(IMUserModel userlist,bool isAdd)
        {
            if (isAdd)//增加用户信息
            {
                if (UserStorage.userAllList.ContainsKey(userlist.UserId))
                {
                    IMUserModel one = UserStorage.userAllList[userlist.UserId];
                    UserStorage.userAllList.Remove(userlist.UserId);
                    userlist.UserOnLine = one.UserOnLine;
                    UserStorage.userAllList.Add(userlist.UserId, userlist);
                }
                else
                {
                    UserStorage.userAllList.Add(userlist.UserId, userlist);
                }
                SendUserListToOthers();
            }
            else//删除用户
            {
                if (UserStorage.userAllList.ContainsKey(userlist.UserId))
                {
                    UserStorage.userAllList.Remove(userlist.UserId);
                    SendUserListToOthers();
                }
            }
            return true;
        }
        #endregion
    }
}
