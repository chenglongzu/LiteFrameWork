using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

namespace LiteFramework
{
    public class GameManager : SingletonMono<GameManager>
    {
        [Header("标准分辨率")]
        public int standardWidth=1920;

        public int standardHeight=1080;

        protected override void Awake()
        {
            base.Awake();
            
            ApplicationInit();

            MonoController.GetInstance();
        }

        
        private void Start()
        {
            //添加按键检测监听
            MonoManager.GetInstance().AddUpdateAction(InputManager.GetInstance().MyUpdate);

            Screen.SetResolution(Screen.width, Screen.height, true);

        }

        
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
        
        
        
        /// <summary>
        /// 应用的初始化
        /// </summary>
        private void ApplicationInit()
        {
            Screen.SetResolution(standardWidth, standardHeight, false);

            //初始化输入管理器的全局事件
            InputManager.GetInstance().GlobalInputInit();
        }

        /// <summary>
        /// 应用结束时的调用
        /// </summary>
        private void OnApplicationQuit()
        {
            InputManager.GetInstance().ClearGlobalInput();
            EventManager.GetInstance().ClearGlobalEvent();
        }

        private void OnGUI()
        {
#if UNITY_EDITOR
#else
            GUIStyle guiStytle = new GUIStyle();
            guiStytle.normal.background = null;
            guiStytle.normal.textColor = new Color(1, 0, 0);
            guiStytle.fontSize = 40;

            GUI.Label(new Rect(50, 50, 100, 200), "测试项目", guiStytle);
#endif
        }
    }
}