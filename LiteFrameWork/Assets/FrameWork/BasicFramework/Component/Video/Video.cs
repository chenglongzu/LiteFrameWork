public class Video
{
    /// <summary>
    /// 将帧数转换为时间
    /// </summary>
    /// <param name="frame"></param>
    /// <returns></returns>
    private string FrameTurnToTime(int frame)
    {
        //秒
        int timeSecond = frame / 24;
        //分钟
        int timeMinute = timeSecond / 60;

        //分钟之下秒
        int timeMinuteSecond = timeSecond % 60;

        if (timeMinuteSecond + 1 < 10)
        {
            return string.Format("{0}:{1}", timeMinute, "0" + timeMinuteSecond);
        }
        else
        {
            return string.Format("{0}:{1}", timeMinute, timeMinuteSecond);
        }

    }
}
