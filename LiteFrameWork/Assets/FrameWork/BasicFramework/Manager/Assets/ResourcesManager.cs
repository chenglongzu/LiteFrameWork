using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace LiteFramework
{
    public class ResourcesManager : BaseManager<ResourcesManager>
    {
        #region Resources 同步加载资源
        public T LoadAsset<T>(string AssetPath) where T : Object
        {
            T ass = null;
            ass = Resources.Load<T>(AssetPath) as T;
            if (ass is GameObject)
            {
                return GameObject.Instantiate<T>(ass);
            }
            return ass;
        }

        public T LoadAsset<T>(string AssetPath, Vector3 position) where T : Object
        {
            T ass = null;
            ass = Resources.Load<T>(AssetPath) as T;
            if (ass is GameObject)
            {
                GameObject obj = GameObject.Instantiate<T>(ass) as GameObject;
                obj.transform.position = position;

                return obj as T;
            }
            return ass;
        }

        public T LoadAsset<T>(string AssetPath, Transform parent) where T : Object
        {
            T ass = null;
            ass = Resources.Load<T>(AssetPath) as T;
            if (ass is GameObject)
            {
                GameObject obj = GameObject.Instantiate<T>(ass, parent) as GameObject;

                return obj as T;
            }
            return ass;
        }

        public T LoadAsset<T>(string AssetPath, Transform parent, Vector3 position) where T : Object
        {
            T ass = null;
            ass = Resources.Load<T>(AssetPath) as T;
            if (ass is GameObject)
            {
                GameObject obj = GameObject.Instantiate<T>(ass) as GameObject;
                obj.transform.parent = parent;
                obj.transform.position = position;

                return obj as T;
            }
            return ass;
        }

        public T LoadAsset<T>(string AssetPath, Vector3 position, Quaternion quaternion, Transform parent = null) where T : Object
        {
            T ass = null;
            ass = Resources.Load<T>(AssetPath) as T;
            if (ass is GameObject)
            {
                GameObject obj = GameObject.Instantiate<T>(ass) as GameObject;
                obj.transform.parent = parent;
                obj.transform.position = position;
                obj.transform.rotation = quaternion;
                return obj as T;
            }
            return ass;
        }

        #endregion

        #region Resources异步加载资源
        public void LoadAssetAsync<T>(string AssetName, UnityAction<T> callback = null) where T : Object
        {
            MonoManager.GetInstance().StartCoroutine(ReallyLoadAsync1(AssetName, callback));
        }

        private IEnumerator ReallyLoadAsync1<T>(string AssetName, UnityAction<T> callback) where T : Object
        {
            ResourceRequest Asset = Resources.LoadAsync<T>(AssetName);
            while (!Asset.isDone)
            {
                yield return Asset.progress;
            }

            if (Asset.isDone)
            {
                if (callback != null)
                {
                    if (Asset.asset is GameObject)
                    {
                        callback(GameObject.Instantiate(Asset.asset) as T);
                    }
                    else
                    {
                        callback(Asset.asset as T);
                    }

                }
                else
                {
                    GameObject.Instantiate(Asset.asset);
                }

            }

            yield return Asset;

        }
        #endregion
    }
}
