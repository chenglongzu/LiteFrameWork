using UnityEngine;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using LiteFramework;
using Object = System.Object;

namespace LiteSqlite
{
    public class SqlManager : BaseManager<SqlManager>
    {
        #region 数据库工具字段
        //链接数据库
        private SqliteConnection connection;
        //数据库命令
        private SqliteCommand command;
        //数据库阅读器
        private SqliteDataReader reader;
        #endregion

        #region 数据库初始化
        /// <summary>
        /// 初始化数据库 由外部调用
        /// </summary>
        /// <param name="dbName"></param>
        public void InitDataBase(string dbName)
        {
            OpenDB(dbName);
        }

        /// <summary>
        /// 创建或连接数据库
        /// </summary>
        /// <param name="dbName"></param>
        private void OpenDB(string dbName)
        {
            try
            {   //链接数据库操作
                string dbPath = Application.streamingAssetsPath + "/" + dbName + ".db";
                //固定sqlite格式data source
                connection = new SqliteConnection(@"Data Source = " + dbPath);
                connection.Open();

                Debug.Log("DataBase Successfully Connect");
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.ToString());
            }
        }
        #endregion

        #region 增
        /// <summary>
        /// 在数据库中创建数据库表
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="数据表的模板类"></param>
        public void CreateTable(string tableName, SqlDataBase dataBase)
        {
            #region 创建数据库的表
            if (DetectionExistTable(tableName)) return;

            string sql = "CREATE TABLE " + tableName + "(";

            for (int i = 0; i < dataBase.NameToArray().Length; i++)
            {
                sql += dataBase.NameToArray()[i] + " " + dataBase.TypeToArray()[i] + ",";
            }

            sql = sql.TrimEnd(',');
            sql += ")";
            
            ExcuteSql(sql);
            #endregion

            #region 创建数据库的序列表

            string tableIndexName = tableName + "Index";
            if (DetectionExistTable(tableIndexName)) return;

            string indexSql = "CREATE TABLE " + tableIndexName + "(";

            TableIndex tableIndex = new TableIndex(0);
            
            for (int i = 0; i < tableIndex.NameToArray().Length; i++)
            {
                sql += tableIndex.NameToArray()[i] + " " + tableIndex.TypeToArray()[i] + ",";
            }

            indexSql = indexSql.TrimEnd(',');
            indexSql += ")";
            
            ExcuteSql(indexSql);
            Insert(tableIndexName,tableIndex);
            #endregion
        }

        
        /// <summary>
        /// 插入单个数据不需要 id的数据类型
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dataBase"></param>
        public void InsertSingleValue(string tableName, SqlDataBase dataBase)
        {
            List<Dictionary<string, object>> tableData = GetTableData(tableName);

            //如果表中存在这段数据则返回
            for (int i = 0; i < tableData.Count; i++)
            {
                if (tableData[i].ContainsKey(dataBase.id.ToString()))
                {
                    foreach (var item in tableData[i])
                    {
                        Debug.Log(item.ToString());
                    }

                    Debug.Log("当前插入数据已存在：id:" + dataBase.id + "名称：" + dataBase.NameToArray());
                    return;
                }

            }

            string sql = "INSERT INTO " + tableName + " VALUES(";

            foreach (object value in dataBase.DataToArray())
            {
                sql += "'" + value.ToString() + "'" + ",";
            }

            sql = sql.TrimEnd(',');
            sql += ")";

            ExcuteSql(sql);
        }
        
        /// <summary>
        /// 在数据库中的某张表中插入数据
        /// </summary>
        /// <param name="_tableName"></param>
        /// <param name="values"></param>
        public void Insert(string tableName, SqlDataBase dataBase)
        {
            int index=System.Convert.ToInt32(SelectByID(tableName + "Index",0));
            dataBase.id = index;
            List<Dictionary<string, object>> tableData = GetTableData(tableName);

            //如果表中存在这段数据则返回
            for (int i = 0; i < tableData.Count; i++)
            {
                if (tableData[i].ContainsKey(dataBase.id.ToString()))
                {
                    foreach (var item in tableData[i])
                    {
                        Debug.Log(item.ToString());
                    }

                    Debug.Log("当前插入数据已存在：id:" + dataBase.id + "名称：" + dataBase.NameToArray());
                    return;
                }

            }

            string sql = "INSERT INTO " + tableName + " VALUES(";

            foreach (object value in dataBase.DataToArray())
            {
                sql += "'" + value.ToString() + "'" + ",";
            }

            sql = sql.TrimEnd(',');
            sql += ")";

            ExcuteSql(sql);

            index++;
            UpdateValue(tableName + "Index","index",index,0);
        }
        #endregion

        #region 删
        /// <summary>
        /// 通过账户名称删除单条数据
        /// </summary>
        public void DeleteData(string tableName, string id)
        {
            ExcuteSql(string.Format("delete from {0} where id = '{1}'", tableName, id));
        }

        /// <summary>
        /// 删除表中的所有数据
        /// </summary>
        /// <param name="tableName"></param>
        public void DeleteAllData(string tableName)
        {
            List<Dictionary<string, object>> temp = GetTableData(tableName);
            for (int i = 0; i < temp.Count; i++)
            {
                if (temp[i].ContainsKey("id"))
                {
                    DeleteData(tableName, temp[i]["id"].ToString());
                }
            }
        }

        /// <summary>
        /// 从数据库中删除表
        /// </summary>
        public void DeleteTable(string tableName)
        {
            ExcuteSql(string.Format("truncate table {0}", tableName));
        }
        #endregion

        #region 改
        /// <summary>
        /// 更新数据库的数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="id"></param>
        public void UpdateValue(string tableName, string key, Object value, int id)
        {
            ExecuteNonQuery(string.Format("update {0} set {1}={2} where {3}='{4}'; ", tableName, key, value, "id", id));
        }
        #endregion

        #region 查

        /// <summary>
        /// 获取表中的所有数据 通过List和Dictionary传输数据
        /// </summary>
        /// <param name="_tableName"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetTableData(string tableName)
        {
            string sql = "SELECT * FROM " + tableName;

            List<Dictionary<string, object>> dataArr = new List<Dictionary<string, object>>();

            reader = ExcuteSql(sql);

            while (reader.Read())
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string key = reader.GetName(i);
                    object value = reader.GetValue(i);
                    data.Add(key, value);
                }

                dataArr.Add(data);
            }
            return dataArr;
        }

        /// <summary>
        /// 查询数据库中是否存在某张表
        /// </summary>
        /// <param name="tableName"></param>
        public bool DetectionExistTable(string tableName)
        {
            return ExecuteScalar("SELECT COUNT(*) FROM sqlite_master where type='table' and name='" + tableName + "';");
        }

        /// <summary>
        /// 查询数据库中表中是否存在某种数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DetectionExistData(string tableName, int id)
        {
            List<Dictionary<string, object>> dataArr = GetTableData(tableName);
            for (int i = 0; i < dataArr.Count; i++)
            {
                if (dataArr[i].ContainsKey("id"))
                {
                    if (dataArr[i]["id"].ToString() == id.ToString())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //查询单个数据（ID是主键）
        public Dictionary<string, object> SelectByID(string _tableName,int Id)
        {
            string sql = "SELECT * FROM " + _tableName +" WHERE Id ="+Id;
            reader = ExcuteSql(sql);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            //阅读电子书，翻页
            reader.Read();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                dic.Add(reader.GetName(i), reader.GetValue(i));
            }
            reader.Close();
            return dic;
        }

        //按照自定义条件查找单条数据
        public Dictionary<string,object> SelectWithCondition(string _tableName,params object[] options)
        {
            if (options == null || options.Length == 0 || options.Length % 2 == 1)
                Debug.LogError("options Length has error!!!");
 
            string sql = "SELECT * FROM " + _tableName + " WHERE ";
            Dictionary<string, object> dic = new Dictionary<string, object>();
 
            for(int i=0;i< options.Length;i+=2)
            {
                sql += options[i] + "= '" + options[i + 1]+"' AND ";
            }
            sql = sql.Remove(sql.Length-4);
 
            reader = ExcuteSql(sql);
            reader.Read();
            for(int i=0;i<reader.FieldCount;i++)
            {
                string key = reader.GetName(i);
                object value = reader.GetValue(i);
                dic.Add(key, value);
            }
            return dic;
        }

        //按照自定义条件查找整张表数据
        public List<Dictionary<string, object>> SelectAllWithCondition(string _tableName, params object[] options)
        {
            if (options == null || options.Length == 0 || options.Length % 2 == 1)
                Debug.LogError("options Length has error!!!");
 
            string sql = "SELECT * FROM " + _tableName + " WHERE ";
            List<Dictionary<string, object>> dataArr = new List<Dictionary<string, object>>();
 
            for (int i = 0; i < options.Length; i += 2)
            {
                sql += options[i] + "= '" + options[i + 1] + "' AND ";
            }
            sql = sql.Remove(sql.Length - 4);
 
            reader = ExcuteSql(sql);
            while (reader.Read())
            {
                Dictionary<string, object> data = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string key = reader.GetName(i);
                    object value = reader.GetValue(i);
                    data.Add(key, value);
                }
                dataArr.Add(data);
            }
            return dataArr;
        }


        #endregion

        #region 析构
        /// <summary>
        /// 关闭数据库
        /// </summary>
        public void CloseDataBase()
        {
            if (reader != null)
                reader.Close();

            if (command != null)
                command.Dispose();

            if (connection != null)
                connection.Close();

            Debug.Log("DataBase Close");
        }

        #endregion

        #region 工具函数 执行Sql函数
        private SqliteDataReader ExcuteSql(string _sql)
        {
            //Debug.Log("Excute Sql :" + _sql);

            //创建数据库连接命令（事务管理、命令管理：向数据库发送指令）
            command = connection.CreateCommand();

            //设置命令语句
            command.CommandText = _sql;

            reader = command.ExecuteReader();

            return reader;
        }

        private bool ExecuteScalar(string _sql)
        {
            //Debug.Log("ExecuteScalar Sql :" + _sql);

            //创建数据库连接命令（事务管理、命令管理：向数据库发送指令）
            command = connection.CreateCommand();

            //设置命令语句
            command.CommandText = _sql;

            int result = System.Convert.ToInt32(command.ExecuteScalar());

            //Debug.Log("result " + (result > 0));

            return (result > 0);
        }

        private void ExecuteNonQuery(string _sql)
        {
            //Debug.Log("ExecuteNonQuery Sql :" + _sql);

            //创建数据库连接命令（事务管理、命令管理：向数据库发送指令）
            command = connection.CreateCommand();

            //设置命令语句
            command.CommandText = _sql;

            command.ExecuteNonQuery();
        }

        #endregion
    }
}