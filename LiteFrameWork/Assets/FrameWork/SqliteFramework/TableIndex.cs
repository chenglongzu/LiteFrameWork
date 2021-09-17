using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiteSqlite;
using Object = System.Object;

public class TableIndex : SqlDataBase
{
    public int index;

    public TableIndex(int index)
    {
        this.index = index;
    }
    
    public override object[] DataToArray()
    {
        return new object[] {index};
    }

    public override string[] NameToArray()
    {
        return new[] {"index"};
    }

    public override string[] TypeToArray()
    {
        return new[] {"int"};
    }

    public override SqlDataBase InitWithSqlData(Dictionary<string, object> _data)
    {
        this.index = System.Convert.ToInt32(_data["index"]);

        return this;
    }
}
