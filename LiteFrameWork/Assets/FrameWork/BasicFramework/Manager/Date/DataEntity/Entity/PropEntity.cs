using System;

namespace LiteFramework {

    //道具数据实体类
    [Serializable]
    public class PropEntity : DataEntityBase
    {
        //数据实体类中的内容与Json文件中的内容保持一致
        public string name;
    }
}

