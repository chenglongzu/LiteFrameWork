namespace LiteFramework
{
    /// <summary>
    /// 数据类 类的对象值就是当前玩家的值
    /// </summary>
    public class PlayerDate
    {
        public int id;
        public string name;
        public string passward;

        //构造方法
        public PlayerDate()
        {
            this.id = 1;

            this.name = "龍城";
            this.passward = "123456";
        }


        #region 修改玩家信息

        //修改自己的名称
        public void ChangeName(string name)
        {
            this.name = name;
        }

        //修改密码
        public void ChangePassward(string passward)
        {
            this.passward = passward;
        }
        #endregion

    }
}
