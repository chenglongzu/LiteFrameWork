using UnityEngine;
using System.Collections;

public class ScreenShot
{
    private int screenIndex;
    
    private IEnumerator ScreenShotIe()
    {
        yield return new WaitForEndOfFrame();
        screenIndex++;
        // 先创建一个的空纹理，大小可根据实现需要来设置  
        Texture2D screenShot = new Texture2D((int)Screen.width, (int)Screen.height, TextureFormat.RGB24,false);  
  
        // 读取屏幕像素信息并存储为纹理数据，  
        screenShot.ReadPixels(new Rect(0,0,Screen.width,Screen.height), 0, 0);  
        screenShot.Apply();  
  
        // 然后将这些纹理数据，成一个png图片文件  
        byte[] bytes = screenShot.EncodeToPNG();
        if (!System.IO.Directory.Exists("c:\\游戏截图"))
        {
            System.IO.Directory.CreateDirectory("c:\\游戏截图");
        }  
        string filename = "c:\\游戏截图" + "/Screenshot_"+screenIndex+".png";

        System.IO.File.WriteAllBytes(filename, bytes);  
        Debug.Log(string.Format("截屏了一张图片: {0}", filename));  

    }
}