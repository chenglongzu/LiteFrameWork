using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LiteFramework
{
    public class BasePanel : MonoBehaviour
    {

        private Dictionary<string, List<UIBehaviour>> uiControllerDic = new Dictionary<string, List<UIBehaviour>>();

        public virtual void Start()
        {
            GetChildrenControl<Text>();
            GetChildrenControl<Image>();
            GetChildrenControl<RawImage>();
            GetChildrenControl<Button>();
            GetChildrenControl<Toggle>();
            GetChildrenControl<Slider>();
            GetChildrenControl<Dropdown>();
            GetChildrenControl<InputField>();
        }

        /// <summary>
        /// 预先获得子物体中的UI组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        protected void GetChildrenControl<T>() where T : UIBehaviour
        {
            T[] uiComponent = this.GetComponentsInChildren<T>();
            string objName;
            for (int i = 0; i < uiComponent.Length; i++)
            {
                objName = uiComponent[i].gameObject.name;
                if (uiControllerDic.ContainsKey(objName))
                {
                    uiControllerDic[objName].Add(uiComponent[i]);
                }
                else
                {
                    uiControllerDic.Add(objName, new List<UIBehaviour>() { uiComponent[i] });
                }

            }

        }

        /// <summary>
        /// 在父物体数据结构中查找当前需要获得的组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        protected T GetUiController<T>(string name) where T : UIBehaviour
        {
            foreach (var item in uiControllerDic)
            {
                if (name == item.Key)
                {
                    for (int i = 0; i < uiControllerDic[name].Count; i++)
                    {
                        if (uiControllerDic[name][i] is T)
                        {
                            return uiControllerDic[name][i] as T;
                        }
                    }

                }
            }

            Debug.LogError("Framework Cannot Get Ui Component");
            return null;
        }

    }
}