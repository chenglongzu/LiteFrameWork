using UnityEngine;

/// <summary>
/// 捕捉鼠标和触摸信息应用到模型身上
/// </summary>
public class ModelOperation : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private float axisX;
    private float axisY;

    private Vector3 prePos;
    private Vector3 nowPos;


    private Touch oldTouch1;  //上次触摸点1(手指1)  
    private Touch oldTouch2;  //上次触摸点2(手指2)  

    private void Start()
    {
        prePos = Vector3.zero;

    }

    void FixedUpdate()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        StandloneInput();
#else
        MobileInput();
#endif
    }

    /// <summary>
    /// PC端的输入对模型的操作
    /// </summary>
    private void StandloneInput()
    {
        //每一帧记录一次鼠标的位置
        nowPos = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            //这里判断是否绝对值大于0 意义就是当前两个位置不相同
            if (Mathf.Abs(nowPos.x - prePos.x) > 0)
            {
                axisX = -(nowPos.x - prePos.x) * Time.deltaTime * 20 * moveSpeed;
            }
            else
            {
                axisX = 0;
            }

            if (Mathf.Abs(nowPos.y - prePos.y) > 0)
            {
                axisY = -(nowPos.y - prePos.y) * Time.deltaTime * 20 * moveSpeed;
            }
            else
            {
                axisY = 0;
            }

            //每一帧计算一次偏差值 然后进行旋转
            this.transform.Rotate(new Vector3(-axisY, axisX, 0), Space.World);
        }
        else
        {
            axisX = 0;
            axisY = 0;
        }

        // 记录上次鼠标位置
        prePos = Input.mousePosition;

        //通过滚轮缩放控制当前挂载物体的scale
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            transform.localScale = new Vector3(transform.localScale.x+ Input.GetAxis("Mouse ScrollWheel")* 2 * moveSpeed,
                transform.localScale.y + Input.GetAxis("Mouse ScrollWheel")*2 * moveSpeed,
                transform.localScale.z + Input.GetAxis("Mouse ScrollWheel")* 2 * moveSpeed
            );       
        }

    }

    /// <summary>
    /// 移动端对模型的操作
    /// </summary>
    public void MobileInput()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 deltaPos = touch.deltaPosition;
            transform.Rotate(Vector3.down * deltaPos.x * moveSpeed*0.5f, Space.World);
            transform.Rotate(Vector3.right * deltaPos.y * moveSpeed*0.5f, Space.World);

        }
        else if (Input.touchCount == 2)
        {
            Touch newTouch1 = Input.GetTouch(0);
            Touch newTouch2 = Input.GetTouch(1);

            //第2点刚开始接触屏幕, 只记录，不做处理  
            if (newTouch2.phase == TouchPhase.Began)
            {
                oldTouch2 = newTouch2;
                oldTouch1 = newTouch1;
                return;
            }

            //计算老的两点距离和新的两点间距离，变大要放大模型，变小要缩放模型  
            float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
            float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);

            //两个距离之差，为正表示放大手势， 为负表示缩小手势  
            float offset = newDistance - oldDistance;

            //放大因子， 一个像素按 0.01倍来算(100可调整)  
            float scaleFactor = (offset / 100f) * moveSpeed * 3;
            Vector3 localScale = transform.localScale;
            Vector3 scale = new Vector3(localScale.x + scaleFactor,
                                        localScale.y + scaleFactor,
                                        localScale.z + scaleFactor);

            //最小缩放到 0.3 倍  
            if (scale.x > 0.3f && scale.y > 0.3f && scale.z > 0.3f)
            {
                transform.localScale = scale;
            }

            //记住最新的触摸点，下次使用  
            oldTouch1 = newTouch1;
            oldTouch2 = newTouch2;
        }

    }
}
