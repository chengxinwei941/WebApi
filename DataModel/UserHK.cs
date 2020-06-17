using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel
{
    public class UserHK
    {
        public int Id { get; set; }

        /// <summary>
        /// 期数
        /// </summary>
        public string HKQS { get; set; }
        public DateTime HKDate { get; set; }
        public double YHBenJin { get; set; }
        public double YHLiXi { get; set; }

        /// <summary>
        /// 还款状态
        /// 0   已还
        /// 1   待还
        /// 2   未还
        /// </summary>
        public int HKStatus { get; set; }

        public int UserId { get; set; }

    }
}
