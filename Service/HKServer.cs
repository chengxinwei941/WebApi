using DataModel;
using System;
using System.Collections.Generic;
using System.Text;

using Dapper;
using System.Data.SqlClient;
using System.Linq;

namespace Service
{
    public class HKServer : IHKServer
    {
     
        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="money">钱</param>
        /// <returns></returns>
        public int ChongZhi(int userId, double money)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=IOT1710A;Integrated Security=True"))
            {
                return conn.Execute($"update UserYE set UYE=UYE+{money} where userId={userId}");
            }
        }
        /// <summary>
        /// 还款  修改余额（减少余额）、修改还款状态（登录用户）
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="hkId">还款信息</param>
        /// <returns></returns>
        public int HKManger(int userId, int hkId)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=IOT1710A;Integrated Security=True"))
            {
                //1.求还多少钱
                object d = conn.ExecuteScalar($" select YHBenJin+YHLiXi from UserHK where Id={hkId}");

                //2.求余额
                object ud= conn.ExecuteScalar($" select UYE from UserYE where UserId={userId}");

                if (Convert.ToDouble(d) <= Convert.ToDouble(ud))
                {
                    //3.减少余额
                    int n = conn.Execute($"update UserYE set UYE=UYE-{d} where UserId={userId}");
                    if (n > 0)
                    {
                        //4.修改状态
                        return conn.Execute($"update UserHK set HKStatus=0 where Id={hkId}");//正常
                    }
                    else {
                        return 0;//减少余额失败
                    }
                }
                else {
                    return -1;//余额不足
                }
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public UserInfo Login(UserInfo info)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=IOT1710A;Integrated Security=True"))
            {
                return conn.Query<UserInfo>($"select * from UserInfo where UserName='{info.UserName}' and UserPwd='{info.UserPwd}'").FirstOrDefault();
            }
        }
        /// <summary>
        /// 还款信息列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<UserHK> ShowHK(int userId)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=IOT1710A;Integrated Security=True"))
            {
                return conn.Query<UserHK>($"select * from UserHK where UserId={userId}").ToList();
            }
        }
    }
}
