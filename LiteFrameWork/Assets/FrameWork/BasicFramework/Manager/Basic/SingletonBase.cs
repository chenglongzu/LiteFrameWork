namespace LiteFramework
{
    /// <summary>
    /// 单例基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonBase<T> where T : new()
    {
        private static T instance;
        public static T GetInstance()
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;

        }
    }
}
