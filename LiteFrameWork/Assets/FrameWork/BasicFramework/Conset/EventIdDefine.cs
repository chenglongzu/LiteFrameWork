public class EventIdDefine
{
    #region 全局事件
    public const int OnConnectOKToMainSocket = 1;
    #endregion

    #region 游戏事件
    public const uint LightShakeCamera = 0;
    public const uint NormalShakeCamera = 1;
    public const uint HardShakeCamera = 2;
    public const uint SucceedConnectSever = 3;

    #region 输入Input
    public const uint Joysick = 4;              //接vector3类型的变量
    public const uint Horizional = 5;           //接float类型的变量
    public const uint Vertical = 6;             //接float类型的变量
    public const uint MouseX = 7;               //接float类型的变量
    public const uint MouseY = 8;               //接float类型的变量
    #endregion
    #endregion
}
