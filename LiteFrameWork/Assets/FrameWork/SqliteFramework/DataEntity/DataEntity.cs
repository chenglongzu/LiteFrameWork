using System;
using LiteFramework;
using SQLite4Unity3d;

namespace LiteFramework
{
    /// <summary>
    /// 数据库实体
    /// </summary>
    [Serializable]
    public class DataEntity
    {
        [PrimaryKey, AutoIncrement]
	    public int Id { get; set; }

        public string tableName;

        //数据库存储的数据类型
        public DataEntityBase dataEntity;
    }
}
