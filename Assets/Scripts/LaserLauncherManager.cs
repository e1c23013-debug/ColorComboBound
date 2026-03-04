using UnityEngine;
using System.Collections;

public class LaserLauncherManager : MonoBehaviour
{
    Transform tf;
    LineRenderer lr;

    //外枠,敵のみに当たり判定を求められるように宣言
    [SerializeField] private LayerMask frame;

    [SerializeField] private LayerMask enemy;

    [SerializeField] private float laserLength = 30f;
    //レーザーのダメージ
    [SerializeField] private int damage = 1;

    private int positionNumber = 2;
    void Start()
    {

        //オブジェクトのTransformコンポーネントを呼び出す(switchの子オブジェクトなので同じ座標)
        tf = this.GetComponent<Transform>();

        //オブジェクトの LineRendererコンポーネントを呼び出す
        lr = this.GetComponent<LineRenderer>();



    }

    //publicは親のLaserSwitchManagerより呼び出し

    public void DrawLaser()
    {

        Vector2 directionUP = Vector2.up;

        Vector2 directionDOWN = Vector2.down;

        RaycastHit2D hitUP = Physics2D.Raycast(transform.position, directionUP, laserLength, frame);

        RaycastHit2D hitDOWN = Physics2D.Raycast(transform.position, directionDOWN, laserLength, frame);

        Vector3 hitUpPosition = hitUP.point; //world

        Vector3 hitDownPosition = hitDOWN.point; //world


        Vector3 localHitUp = transform.InverseTransformPoint(hitUpPosition);//local

        Vector3 localHitDowm = transform.InverseTransformPoint(hitDownPosition);//local

        lr.positionCount = positionNumber;

        //始点はこのオブジェクトであり，ローカル座標で考える
        lr.SetPosition(0, Vector3.zero);

        if (hitUP.collider != null|| hitDOWN.collider != null)
        {
            
            lr.SetPosition(0, localHitDowm);
            lr.SetPosition(1, localHitUp);


            RaycastHit2D[] hitUPEnemy = Physics2D.RaycastAll(transform.position, directionUP, laserLength, enemy);

            RaycastHit2D[] hitDOWNEnemy = Physics2D.RaycastAll(transform.position, directionDOWN, laserLength, enemy);


            // 上方向に当たった相手へのダメージ判定
            foreach (RaycastHit2D hit in hitUPEnemy)
            {

                if (hit.collider != null)
                {



                    Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
                    if (enemy != null)
                    {

                        EnemyColor color = enemy.Color;

                        GameEvents.LaserCollisionEnemy?.Invoke(damage, color);

                    }

                }

            }


            // 下方向に当たった相手へのダメージ判定
            foreach (RaycastHit2D hit in hitDOWNEnemy)
            {

                if (hit.collider != null)
                {
                    Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
                    if (enemy != null)
                    {

                        EnemyColor color = enemy.Color;

                        GameEvents.LaserCollisionEnemy?.Invoke(damage, color);

                    }

                }


            }

        }
        else
        {
            // 壁に当たっていない：マウスの方向に最大距離伸ばす
            Vector3 worldEndUp = transform.position + (Vector3)(directionUP * laserLength);//world
            Vector3 worldEndDown = transform.position + (Vector3)(directionDOWN * laserLength);//world

            Vector3 localEndUp = transform.InverseTransformPoint(worldEndUp);//local
            Vector3 localEndDown = transform.InverseTransformPoint(worldEndDown);//local



            lr.SetPosition(0, localEndDown);
            lr.SetPosition(1,localEndUp);


        }

        StartCoroutine(PauseDeletedLaser());


    }
    private void DeleteLaser()
    {

        //線を消去
        lr.positionCount = 0;

    }

    IEnumerator PauseDeletedLaser()
    {
        yield return new WaitForSeconds(0.15f);

        DeleteLaser();
    }


}
