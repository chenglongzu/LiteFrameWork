
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
 
public class DrawShape : MonoBehaviour
{
    public static MeshFilter mf;
    public static MeshRenderer mr;
    public static Shader shader;
 
    private void Start()
    {
        GameObject green = DrawCircleArea(3,new Color32(0,255,15,20));
        SmallScale(green);

        GameObject red = DrawCircleArea(1,new Color32(255,0,38,45));
    }


    private void SmallScale(GameObject green)
    {
        green.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.5f).OnComplete(() =>
        {
            BigScale(green);
        });
    }

    private void BigScale(GameObject green)
    {
        green.transform.DOScale(new Vector3(1, 1, 1), 0.5f).OnComplete(() =>
        {
            SmallScale(green);
        });
    }
    
    /// <summary>
    /// 绘制圆形区域
    /// </summary>
    /// <param name="t">圆形参考物</param>
    /// <param name="center">圆形的中心</param>
    /// <param name="radius">圆形的半径</param>
    public GameObject DrawCircleArea(float radius,Color32 color)
    {
        int pointAmount = 100;//点的数目，值越大曲线越平滑   
        float eachAngle = 360f / pointAmount;
        Vector3 forward = transform.forward;
 
        List<Vector3> vertices = new List<Vector3>();
        for (int i = 0; i < pointAmount; i++)
        {
            Vector3 pos = Quaternion.Euler(0f, eachAngle * i, 0f) * forward * radius;
            vertices.Add(pos);
        }
        
        return CreateMesh(vertices,color);
    }
    
    
    /// <summary>
    /// 创建Mesh
    /// </summary>
    /// <param name="vertices">存储顶点的列表</param>
    /// <returns></returns>
    private GameObject CreateMesh(List<Vector3> vertices,Color32 color)
    {
        int[] triangles;
        Mesh mesh = new Mesh();
        int triangleAmount = vertices.Count - 2;
        triangles = new int[3 * triangleAmount];
 
        //根据三角形的个数，来计算绘制三角形的顶点顺序（索引）   
        //顺序必须为顺时针或者逆时针      
        for (int i = 0; i < triangleAmount; i++)
        {
            triangles[3 * i] = 0;//固定第一个点      
            triangles[3 * i + 1] = i + 1;
            triangles[3 * i + 2] = i + 2;
        }
 
        GameObject go = new GameObject("DetectionArea");
        mf = go.AddComponent<MeshFilter>();
        mr = go.AddComponent<MeshRenderer>();
        shader = Shader.Find("Standard");

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles;
        mf.mesh = mesh;
        mr.material.shader = shader;
        mr.material.color = color;

        mr.material.SetFloat("_Mode", 3);
        mr.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mr.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mr.material.SetInt("_ZWrite", 0);
        mr.material.DisableKeyword("_ALPHATEST_ON");
        mr.material.EnableKeyword("_ALPHABLEND_ON");
        mr.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mr.material.renderQueue = 3000;

        go.transform.parent = transform;
        go.transform.localPosition =Vector3.zero;//让绘制的图形上升一点，防止被地面遮挡  

        //go.AddComponent<Skode_Glinting>()._autoStart=true;
        return go;
    }
 
}