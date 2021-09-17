using System.Collections.Generic;
using UnityEngine;

namespace LiteFramework
{
    /// <summary>
    /// 通用的数据实体存储类
    /// </summary>
    [System.Serializable]
    public class DataEntityCollection
    {
        public string dataEntityCollectionName;

        public DataEntityBase dataEntity;

        public List<DataEntityBase> dataCollection;
    }
}
