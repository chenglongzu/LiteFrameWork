using UnityEngine;
using UnityEngine.SceneManagement;

namespace LiteFramework
{
    /// <summary>
    /// 输入管理器 在这里统一对输入进行管理
    /// </summary>
    public class InputManager : BaseManager<InputManager>
    {
        #region 全局按键的绑定GlobalInputInit
        /// <summary>
        /// 展示控制台
        /// </summary>
        public KeyCode showConsole;

        /// <summary>
        /// 全局的按键绑定初始化  在进入游戏时就调用
        /// </summary>
        public void GlobalInputInit()
        {
            showConsole = KeyCode.B;
        }

        /// <summary>
        /// 清除全局的按键绑定   在游戏结束时调用
        /// </summary>
        public void ClearGlobalInput()
        {
            showConsole = KeyCode.None;
        }
        #endregion

        #region 游戏按键的绑定GameInputInit
        /// <summary>
        /// 游戏中常用按键的定义
        /// </summary>
        public KeyCode fire;
        public KeyCode jump;

        /// <summary>
        /// 按键的绑定 进入游戏场景时进行调用
        /// </summary>
        public void GameInputInit()
        {
            fire = KeyCode.Mouse0;
            jump = KeyCode.Space;
        }

        /// <summary>
        /// 清除按键绑定 在游戏场景切换时进行调用
        /// </summary>
        public void GameInputClear()
        {
            fire = KeyCode.None;
            jump = KeyCode.None;
        }
        #endregion

        public void MyUpdate()
        {

        }
    }
}