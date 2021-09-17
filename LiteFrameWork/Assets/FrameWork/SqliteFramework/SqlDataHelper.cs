using UnityEngine;
using System.Collections.Generic;
using LiteSqlite; 

public class SqlDataBaseHelper<T> where T :SqlDataBase,new() {
 
    public static T GetById(int id)
    {
        return GetInfoWithCondition(typeof(T).Name, new object[] { "id", id });
    }
 
    public static List<T> GetAllInfos()
    {
        string tableName = typeof(T).ToString().ToLower();
        List<Dictionary<string, object>> resultList = SqlManager.GetInstance().GetTableData(tableName);
        if (resultList.Count == 0)
        {
            return default(List<T>);
        }
 
        List<T> t = new List<T>();
        for (int i = 0; i < resultList.Count; i++)
        {
            T tmp = new T();
            tmp.InitWithSqlData(resultList[i]);
            t.Add(tmp);
        }
        return t;
    }
 
    public static T GetInfoWithCondition(string tableName, object[] options)
    {
        UnityEngine.Assertions.Assert.IsTrue(options.Length % 2 == 0, "[DAO GetInfoFromTable] options error.");
        Dictionary<string, object> resultList = SqlManager.GetInstance().SelectWithCondition(tableName,options);        T tmp = new T();
        tmp.InitWithSqlData(resultList);
        return tmp;
    }
 
    public static List<T> GetInfosWithCondition(string tableName, object[] options)
    {
        UnityEngine.Assertions.Assert.IsTrue(options.Length % 2 == 0, "[DAO GetInfoFromTable] options error.");
        List<Dictionary<string, object>> resultList = SqlManager.GetInstance().SelectAllWithCondition(tableName, options);
        if (resultList.Count == 0)
        {
            return default(List<T>);
        }
 
        List<T> t = new List<T>();
        for (int i = 0; i < resultList.Count; i++)
        {
            T tmp = new T();
            tmp.InitWithSqlData(resultList[i]);
            t.Add(tmp);
        }
        return t;
    }
}
