using System.Collections.Generic;
using UnityEngine.Events;

namespace LiteFramework
{
    /// <summary>
    /// 事件类型
    /// </summary>
    public enum EventType
    {
        None_Parameter,
        One_Parameter,
    }

    /// <summary>
    /// 事件管理器
    /// </summary>
    public class EventManager : BaseManager<EventManager>
    {
        /// <summary>
        /// 全局的委托监听事件
        /// </summary>
        private Dictionary<uint, UnityAction> globalEventDic_1 = new Dictionary<uint, UnityAction>();
        private Dictionary<uint, UnityAction<object>> globalEventDic_2 = new Dictionary<uint, UnityAction<object>>();

        /// <summary>
        /// 游戏内的委托监听事件
        /// </summary>
        private Dictionary<uint, UnityAction> gameEventDic_1 = new Dictionary<uint, UnityAction>();
        private Dictionary<uint, UnityAction<object>> gameEventDic_2 = new Dictionary<uint, UnityAction<object>>();


        #region 全局委托事件
        #region 添加全局事件AddGlobalEvent
        /// <summary>
        /// 添加全局事件 在进入游戏时就开始调用
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="action"></param>
        public void AddGlobalEvent(uint eventId,UnityAction action)
        {
            if (globalEventDic_1.ContainsKey(eventId))
            {
                globalEventDic_1[eventId] += action;
            }else
            {
                globalEventDic_1.Add(eventId, action);
            }
        }
        public void AddGlobalEvent(uint eventId, UnityAction<object> action)
        {
            if (globalEventDic_2.ContainsKey(eventId))
            {
                globalEventDic_2[eventId] += action;
            }else
            {
                globalEventDic_2.Add(eventId, action);
            }
        }
        #endregion

        #region 触发全局事件GlobalEventTrigger
        /// <summary>
        /// 触发全局事件
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="obj"></param>
        public void GlobalEventTrigger(uint eventId,object info = null)
        {
            if (info == null)
            {
                if (globalEventDic_1.ContainsKey(eventId))
                {
                    globalEventDic_1[eventId].Invoke();
                }
            }
            else
            {
                if (globalEventDic_2.ContainsKey(eventId))
                {
                    globalEventDic_2[eventId].Invoke(info);
                }
            }
        }
        #endregion

        #region 移除全局委托事件RemoveGlobalEvent
        /// <summary>
        /// 移除eventId下的所有全局委托事件
        /// </summary>
        /// <param name="eventId"></param>
        public void RemoveAllGlobalEvent(uint eventId)
        {
            if (globalEventDic_1.ContainsKey(eventId))
            {
                globalEventDic_1[eventId]=null;
            }

            if (globalEventDic_2.ContainsKey(eventId))
            {
                globalEventDic_2[eventId] = null;
            }
        }

        /// <summary>
        /// 移除委托链中特定的委托
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="action"></param>
        public void RemoveGlobalEvent(uint eventId,UnityAction action)
        {
            if (globalEventDic_1.ContainsKey(eventId))
            {
                globalEventDic_1[eventId] -= action;
            }
        }

        public void RemoveGlobalEvent(uint eventId, UnityAction<object> action)
        {
            if (globalEventDic_2.ContainsKey(eventId))
            {
                globalEventDic_2[eventId] -= action;
            }
        }
        #endregion

        #region 清除全局委托事件ClearGlobalEvent
        /// <summary>
        /// 清除所有全局委托事件 在游戏关闭时调用
        /// </summary>
        public void ClearGlobalEvent()
        {
            globalEventDic_1.Clear();
            globalEventDic_2.Clear();
        }
        #endregion
        #endregion

        #region 游戏委托事件
        #region 添加游戏委托事件
        /// <summary>
        /// 在场景初始化的时候进行调用
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="action"></param>
        public void AddEvent(uint eventId, UnityAction action)
        {
            if (gameEventDic_1.ContainsKey(eventId))
            {
                gameEventDic_1[eventId] += action;
            }
            else
            {
                gameEventDic_1.Add(eventId, action);
            }
        }
        public void AddEvent(uint eventId, UnityAction<object> action)
        {
            if (gameEventDic_2.ContainsKey(eventId))
            {
                gameEventDic_2[eventId] += action;
            }
            else
            {
                gameEventDic_2.Add(eventId, action);
            }
        }

        #endregion

        #region 触发游戏委托事件
        public void EventTrigger(uint eventId, object info=null)
        {
            if (info==null)
            {
                if (gameEventDic_1.ContainsKey(eventId))
                {
                    gameEventDic_1[eventId].Invoke();
                }
            }else
            {
                if (gameEventDic_2.ContainsKey(eventId))
                {
                    gameEventDic_2[eventId].Invoke(info);
                }
            }
        }
        #endregion

        #region 移除游戏委托事件
        /// <summary>
        /// 移除当前名称下的所有委托
        /// </summary>
        /// <param name="name"></param>
        public void RemoveAllEvent(uint eventId)
        {
            if (gameEventDic_1.ContainsKey(eventId))
            {
                gameEventDic_1[eventId] = null;
            }

            if (gameEventDic_2.ContainsKey(eventId))
            {
                gameEventDic_2[eventId] = null;
            }
        }

        /// <summary>
        /// 移除委托链中的特定委托
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="action"></param>
        public void RemoveEvent(uint eventId, UnityAction action)
        {
            if (gameEventDic_1.ContainsKey(eventId))
            {
                gameEventDic_1[eventId] -= action;
            }
        }
        public void RemoveEvent(uint eventId, UnityAction<object> action)
        {
            if (gameEventDic_2.ContainsKey(eventId))
            {
                gameEventDic_2[eventId] -= action;
            }
        }
        #endregion

        #region 清除所有游戏委托事件
        /// <summary>
        /// 清除所有游戏委托事件 在切换场景时调用
        /// </summary>
        public void ClearGameEvent()
        {
            gameEventDic_1.Clear();
            gameEventDic_2.Clear();
        }
        #endregion
        #endregion
    }

}
