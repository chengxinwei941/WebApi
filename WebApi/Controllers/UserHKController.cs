using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserHKController : ControllerBase
    {
        private IHKServer _dal;
        public UserHKController(IHKServer dal) {
            _dal = dal;
        }

        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="money">钱</param>
        /// <returns></returns>
        public int ChongZhi(string token, double money)
        {
            JWTHelper jWT = new JWTHelper();
            string json = jWT.GetPayload(token);
            UserInfo model = JsonConvert.DeserializeObject<UserInfo>(json);
            if (model != null)
            {
                return _dal.ChongZhi(model.Id, money);
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// 还款  修改余额（减少余额）、修改还款状态（登录用户）
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="hkId">还款信息</param>
        /// <returns></returns>
        public int HKManger(string token, int hkId)
        {
            JWTHelper jWT = new JWTHelper();
            string json = jWT.GetPayload(token);
            UserInfo model = JsonConvert.DeserializeObject<UserInfo>(json);
            if (model != null)
            {
                return _dal.HKManger(model.Id, hkId);
            }
            else
            {
                return -1;
            }

           
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public string Login([FromForm]UserInfo info)
        {
            UserInfo model = _dal.Login(info);
            if (model != null)
            {
                JWTHelper jWT = new JWTHelper();
                Dictionary<string, object> keys = new Dictionary<string, object>();
                keys.Add("UserName", model.UserName);
                keys.Add("Id", model.Id);
                keys.Add("UserPwd", model.UserPwd);
                string token = jWT.GetToken(keys, 300000);
                return token;
            }
            else {
                return null;
            }
        }
        /// <summary>
        /// 还款信息列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<UserHK> ShowHK(string token)
        {
            JWTHelper jWT = new JWTHelper();
            string json = jWT.GetPayload(token);
            UserInfo model = JsonConvert.DeserializeObject<UserInfo>(json);
            if (model != null)
            {
                return _dal.ShowHK(model.Id);
            }
            else {
                return null;
            }
        }

    }
}