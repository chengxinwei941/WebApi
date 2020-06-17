using System;
using System.Collections.Generic;
using DataModel;
namespace Service
{
    public interface IHKServer
    {

        /// <summary>
        /// 登录        根据用户名和密码查询该用户所有信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        UserInfo Login(UserInfo info);

        /// <summary>
        /// //还款记录   显示登录用户还款信息表
        /// </summary>
        /// <param name="userId">登录用户Id</param>
        /// <returns></returns>
        List<UserHK> ShowHK(int userId);

        /// <summary>
        /// 还款功能   修改余额（减少余额）、修改还款状态（登录用户）
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="hkId">还款Id</param>
        /// <returns></returns>
        int HKManger(int userId,int hkId);

        /// <summary>
        /// 充值功能   修改余额（增加余额）
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="money">充值钱</param>
        /// <returns></returns>
        int ChongZhi(int userId, double money);
    }
}
