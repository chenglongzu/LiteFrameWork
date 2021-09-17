using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddCollider : MonoBehaviour
{

    [ContextMenu("AddCollider")]
    private void AddColliderToChildern()
    {
        MeshRenderer[] meshRenders = GetComponentsInChildren<MeshRenderer>();

        int index = 0;
        for (int i = 0; i < meshRenders.Length; i++)
        {
            index++;

            meshRenders[i].gameObject.AddComponent<MeshCollider>().convex = true;
        }

    }


    [ContextMenu("RemoveCollider")]
    private void RemoveColliderToChildern()
    {
        MeshCollider[] meshRenders = GetComponentsInChildren<MeshCollider>();

        int index = 0;
        for (int i = 0; i < meshRenders.Length; i++)
        {
            index++;


            meshRenders[i].enabled = false;
        }

    }

}
