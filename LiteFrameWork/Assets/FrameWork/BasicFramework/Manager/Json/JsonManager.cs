using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LiteFramework
{
    /// <summary>
    /// Josn解析管理器
    /// </summary>
    public class JsonManager : BaseManager<JsonManager>
    {
        public Dictionary<int, T> ReadJsonToDic<T>(string jsonPath) where T : DataEntityBase
        {
            string jsonContent = ResourcesManager.GetInstance().LoadAsset<TextAsset>(jsonPath).text;
            string newJsonContent = "{ \"dataCollection\": " + jsonContent + "}";

            //将json数据转化为ContentCollection<V>类型的数据结构
            DataEntityCollection contentDic = JsonUtility.FromJson<DataEntityCollection>(newJsonContent);

            Dictionary<int, T> jsonInfo = new Dictionary<int, T>();

            for (int i = 0; i < contentDic.dataCollection.Count; i++)
            {
                jsonInfo.Add(contentDic.dataCollection[i].id, (T)contentDic.dataCollection[i]);
            }

            return jsonInfo;
        }

        public List<T> ReadJsonToList<T>(string jsonPath) where T : DataEntityBase
        {
            string jsonContent = ResourcesManager.GetInstance().LoadAsset<Text>(jsonPath).text;
            string newJsonContent = "{ \"dataCollection\": " + jsonContent + "}";

            DataEntityCollection contentDic = JsonUtility.FromJson<DataEntityCollection>(newJsonContent);

            List<T> jsonInfo = new List<T>();

            for (int i = 0; i < contentDic.dataCollection.Count; i++)
            {
                jsonInfo.Add(contentDic.dataCollection[i] as T);
            }

            return jsonInfo;
        }
    }
    
}