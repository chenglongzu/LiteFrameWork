using System;
using SQLite4Unity3d;
using UnityEngine;
using LiteFramework;

#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;
using System.Linq.Expressions;

public class SqliteManager:BaseManager<SqliteManager> {
	
	//Sqlite Connection 数据库连接器
	private SQLiteConnection _connection;

	/// <summary>
	/// InitManager 初始化管理器
	/// </summary>
	protected override void InitManager()
	{
		CreatDataBase("GameDB");
	}

	// Create DataBase 创建数据库
	public void CreatDataBase(string DatabaseName)
	{
		#if UNITY_EDITOR
            var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID 
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		
#elif UNITY_STANDALONE_OSX
		var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
		_connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Final PATH: " + dbPath);     


	}
	
	// Create DataTable 创建数据表
	public void CreateDBTable<T>()where T:DataEntity{
		_connection.DropTable<T> ();
		_connection.CreateTable<T> ();
	}

	
	//Get AllData On DB 获取表中的所有值
	public IEnumerable<DataEntity> GetDBData()
	{
		return _connection.Table<DataEntity>();
	}

	//Get Single DB Data By Condition 通过表达式获取值
	public IEnumerable<DataEntity> GetDBData(Expression<Func<DataEntity, bool>> predExpr){
		//Where(x => x.Name == "Roberto");
		//Expression<Func<T, bool>> predExpr
		//x => x.Name == "Johnny"
		return _connection.Table<DataEntity>().Where(predExpr);
	}
	
	//Get Single DB Data 扩区数据库中的单个值
	public DataEntity GetSingleDBData(Expression<Func<DataEntity, bool>> predExpr){
		//Where(x => x.Name == "Roberto");
		//Expression<Func<T, bool>> predExpr
		//x => x.Name == "Johnny"
		return _connection.Table<DataEntity>().Where(predExpr).FirstOrDefault();
	}
	
	
	//Insert Data 插入单条数据
	public void InsertData<T>(T dataEntity)where T:DataEntity{
		_connection.Insert (dataEntity);
	}

	//Insert Datas 插入多条数据
	public void InserDatas<T>(params T[] datas)where T:DataEntity
	{
		_connection.InsertAll (datas);
	}
	
	
	//Console Data 输出打印
	private void ToConsole(IEnumerable<Person> people){
		foreach (var person in people) {
			//ToConsole(person.ToString());
		}
	}
}
