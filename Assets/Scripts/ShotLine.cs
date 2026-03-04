using UnityEngine;


public class ShotLine : MonoBehaviour
{
    //外枠のみに当たり判定を求められるようにframeを宣言
    [SerializeField] private LayerMask frame;


    private Transform tf;

    private LineRenderer lr;

    //真上を基準にしたボールの発射可能な角度
    private float minAngle = -80f;
    private float maxAngle = 80f;

    private float searchDistance = 30f;

    void Start()
    {


        //オブジェクトのTransformコンポーネントを呼び出す
        tf = this.GetComponent<Transform>();

        //オブジェクトの LineRendererコンポーネントを呼び出す
        lr = this.GetComponent<LineRenderer>();

    }

  
    //publicはBallManagerより呼び出し
    public void DrawLine(Vector3 Mousepos3)
    {
        Vector3 pos = tf.position; //world

        Vector2 tempDirection = (Mousepos3 - pos).normalized; //方角ベクトルのみ取り出す

        //真上を基準の角度に変換
        float angle = Vector2.SignedAngle(Vector2.up, tempDirection);

        //  角度を minAngle, maxAngleの間に制限
        float clampedAngle = Mathf.Clamp(angle, minAngle, maxAngle);

        //角度を方向ベクトルに戻す
        Vector2 direction = Quaternion.Euler(0, 0, clampedAngle) * Vector2.up;


        RaycastHit2D hit = Physics2D.Raycast(pos, direction, searchDistance, frame); 

        Vector3 hitPosition = hit.point;//world

        Vector3 localHit = transform.InverseTransformPoint(hitPosition);//local

        lr.positionCount = 2;

        //始点はこのオブジェクトであり，ローカル座標で考える
        lr.SetPosition(0, Vector3.zero);

        if (hit.collider != null)
        {
          
            lr.SetPosition(1, localHit);
        }
        else
        {
            // 壁に当たっていない：マウスの方向に最大距離伸ばす
            Vector3 worldEnd = pos + (Vector3)(direction * searchDistance);
            lr.SetPosition(1, transform.InverseTransformPoint(worldEnd));
        }

    }
    public void DeleteLine()
    {

        //線を消去
        lr.positionCount = 0;

    }



}
