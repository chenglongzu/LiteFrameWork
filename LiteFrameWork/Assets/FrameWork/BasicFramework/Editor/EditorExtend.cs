using UnityEngine;
using System;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class EditorExtend
{
    #region Print 打印数据
#if UNITY_EDITOR
    [MenuItem("Framework/Print/CodeNumber")]
    private static void PrintTotalLine()
    {
        string[] fileName = Directory.GetFiles("Assets/Scripts", "*.cs", SearchOption.AllDirectories);

        int totalLine = 0;
        foreach (var temp in fileName)
        {
            int nowLine = 0;
            StreamReader sr = new StreamReader(temp);
            while (sr.ReadLine() != null)
            {
                nowLine++;
            }

            //文件名 + 文件行数
            //Debug.Log(String.Format("{0}——{1}", temp, nowLine));

            totalLine += nowLine;
        }

        Debug.Log(String.Format("总代码行数：{0}", totalLine));
    }
#endif
#if UNITY_EDITOR
    [MenuItem("Framework/Print/PrintPersistentDataPath")]
    private static void PrintPersistentDataPath()
    {
        Debug.Log(String.Format("Application.persistentDataPath:{0}", Application.persistentDataPath));
    }
#endif
    #endregion

    #region Export打包
#if UNITY_EDITOR
    [MenuItem("Framework/Export/ExportLiteFramework")]
#endif
    private static void ExportBasic()
    {
        var assetPathName_1 = "Assets/Plugins/Common";
        var assetPathName_2 = "Assets/ThirdParty";
        var assetPathName_3 = "Assets/FrameWork/BasicFramework";
        var assetPathName_4 = "Assets/Download";

        var fileName = "LiteFramework" +"("+ DateTime.Now.ToString("yyyyMMdd_hh")+")"+ ".unitypackage";
        AssetDatabase.ExportPackage(new string[] { assetPathName_1, assetPathName_2 , assetPathName_3, assetPathName_4 }, fileName, ExportPackageOptions.Recurse);

        Application.OpenURL("file:///" + Application.dataPath.Substring(0, Application.dataPath.Length - 7));
    }

#if UNITY_EDITOR
    [MenuItem("Framework/Export/ExportSqliteFramework")]
#endif
    private static void ExportSqlite()
    {
        var assetPathName_1 = "Assets/Plugins";
        var assetPathName_2 = "Assets/ThirdParty";
        var assetPathName_3 = "Assets/FrameWork";
        var assetPathName_4 = "Assets/Download";

        var fileName = "LiteFramework(Sqlite)" +"(" +DateTime.Now.ToString("yyyyMMdd_hh")+")" + ".unitypackage";
        AssetDatabase.ExportPackage(new string[] { assetPathName_1, assetPathName_2, assetPathName_3,assetPathName_4 }, fileName, ExportPackageOptions.Recurse);

        Application.OpenURL("file:///" + Application.dataPath.Substring(0, Application.dataPath.Length - 7));
    }

#endregion
}
