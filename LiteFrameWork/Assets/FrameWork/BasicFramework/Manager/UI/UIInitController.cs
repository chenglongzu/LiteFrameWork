using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LiteFramework
{
    public class UIInitController : MonoBehaviour
    {
        private Transform uiRoot;

        void Start()
        {
            //初始化UI获取到UI的层级
            UIManager.GetInstance().Init();

            UiAutoAdaption();
        }

        /// <summary>
        /// UI自适应
        /// </summary>
        private void UiAutoAdaption()
        {
            uiRoot = transform.Find("UIRoot");

            //通过获取组件来判断当前屏幕的分辨率
            CanvasScaler canvasScaler = transform.Find("UIRoot").GetComponent<CanvasScaler>();
            if ((float)Screen.width / (float)Screen.height > GameManager.GetInstance().standardWidth / GameManager.GetInstance().standardHeight)
            {
                //根据屏幕的高度来缩放画布
                canvasScaler.matchWidthOrHeight = 0;
            }
            else if ((float)Screen.width / (float)Screen.height < GameManager.GetInstance().standardWidth / GameManager.GetInstance().standardHeight)
            {
                //根据屏幕的宽度来缩放画布
                canvasScaler.matchWidthOrHeight = GameManager.GetInstance().standardWidth / GameManager.GetInstance().standardHeight - (float)Screen.width / (float)Screen.height;
            }

            StartCoroutine(UiPostionAdaptation());
        }
        IEnumerator UiPostionAdaptation()
        {
            yield return null;

            for (int i = 0; i < uiRoot.childCount; i++)
            {
                Transform Child = uiRoot.GetChild(i);
                Child.localPosition = new Vector2(Child.localPosition.x * (uiRoot.GetComponent<RectTransform>().rect.width / GameManager.GetInstance().standardWidth), Child.localPosition.y * (uiRoot.GetComponent<RectTransform>().rect.height / GameManager.GetInstance().standardHeight));
            }
        }
    }
}