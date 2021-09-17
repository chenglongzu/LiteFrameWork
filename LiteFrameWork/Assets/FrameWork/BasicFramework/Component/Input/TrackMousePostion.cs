using UnityEngine;

/// <summary>
/// 捕捉鼠标位置 到三维世界
/// </summary>
public class TrackMousePostion : MonoBehaviour {

	public bool isStart;

	private Camera mainCamera;

	[SerializeField] Transform targetTransform;

	private void Start()
    {
		isStart = true;
		mainCamera = Camera.main;
	}

	private void Update()
    {
        if (isStart)
        {
			Vector3 v3 = mainCamera.WorldToScreenPoint(targetTransform.position+new Vector3(0,0,-3));

			Vector3 mousePos = Input.mousePosition;
			mousePos.z = v3.z;

			Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);

			targetTransform.position = worldPos;
		}
	}
}
