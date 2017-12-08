using System;

namespace MyWebSite.Models
{
    /// <summary>
    /// 数据库实体基类
    /// </summary>
    public abstract class Entity
    {
        private DateTime? createTime;
        public DateTime? CreateTime
        {
            get
            {
                return createTime;
            }
            set
            {
                createTime = createTime ?? DateTime.Now;
            }
        }

        private DateTime? upDateTime;
        public DateTime? UpDateTime
        {
            get
            {
                return upDateTime;
            }
            set
            {
                upDateTime = DateTime.Now;
            }
        }
    }
}
