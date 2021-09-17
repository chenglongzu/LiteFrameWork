using System.Collections.Generic;

namespace LiteSqlite
{
    /// <summary>
    /// 所有数据实体类的父类
    /// </summary>
    public abstract class SqlDataBase
    {
        public int id;

        //创建数据库时初始化数据库用
        public abstract string[] NameToArray();
        public abstract string[] TypeToArray();

        //数据库中插入数据库用
        public abstract object[] DataToArray();
        
        public virtual SqlDataBase InitWithSqlData(Dictionary<string, object> _data)
        {
            return null;
        }
    }
}