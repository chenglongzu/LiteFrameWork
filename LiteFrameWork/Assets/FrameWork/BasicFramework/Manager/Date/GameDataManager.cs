using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

namespace LiteFramework
{
    /// <summary>
    /// 数据管理器
    /// </summary>
    public class GameDataManager : BaseManager<GameDataManager>
    {
        #region 内存Entity
        /// <summary>
        /// 将Json数据解析到内存当中
        /// </summary>
        public Dictionary<int, T> AddDataEntityCollection<T>(string name) where T: DataEntityBase
        {
            //加载背包数据 将背包道具实体类加载到内存当中
            string dataContent = ResourcesManager.GetInstance().LoadAsset<TextAsset>("Json/"+name).text;

            return JsonManager.GetInstance().ReadJsonToDic<T>(dataContent);
        }
        #endregion

        #region 数据库
        /// <summary>
        /// 初始化数据库 加载数据库数据到内存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T InitDataBase<T>(string name) where T: DataEntityBase,new()
        {
            string url = Application.persistentDataPath + "/"+name+ ".txt";

            T data;
            if (File.Exists(url))
            {
                byte[] bytes = File.ReadAllBytes(url);
                string getJson = Encoding.UTF8.GetString(bytes);

                data = JsonUtility.FromJson<T>(getJson);
            }
            else
            {
                data = new T();
            }

            string toJson = JsonUtility.ToJson(data);

            File.WriteAllBytes(url, Encoding.UTF8.GetBytes(toJson));

            return data;
        }

        /// <summary>
        /// 将内存当中的数据存储到本地数据库
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public void SetDataBase<T>(string name,T data) where T : DataEntityBase, new()
        {
            string url = Application.persistentDataPath + "/" + name + ".txt";

            string toJson = JsonUtility.ToJson(data);

            File.WriteAllBytes(url, Encoding.UTF8.GetBytes(toJson));
        }


        #endregion
    }
}




