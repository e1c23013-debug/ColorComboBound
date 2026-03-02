using UnityEngine;
using UnityEngine.EventSystems;

public class MalletManager : MonoBehaviour
{
    [SerializeField] private GameObject mallet;

    [SerializeField] private LayerMask frame;

    //壁にギリギリめり込まないようにマレットの半径より少し大きい値を設定
    private float offset;

    private Camera mainCamera;
    private Rigidbody2D rb;

    // マレットの移動できる限界値
    private float minX;
    private float maxX;

    private bool isRightClicking;


    void OnEnable()
    {
        GameEvents.PushMalletSwitch += DisplayMallet;
        GameEvents.OnTurnChanged += CallInvisibleMallet;
    }

    void OnDisable()
    {
        GameEvents.PushMalletSwitch -= DisplayMallet;
        GameEvents.OnTurnChanged -= CallInvisibleMallet;

    }

    void Start()
    {
        SpriteRenderer sr = mallet.GetComponent<SpriteRenderer>();

        offset = sr.bounds.extents.x;

        Debug.Log($"offset={offset}");

        mainCamera = Camera.main;
        if (mallet != null)
        {
            rb = mallet.GetComponent<Rigidbody2D>();

            // 起動時に限界値を計算して設定する
            CalculateBoundaries();
        }
      


    }

    void Update()
    {
        if (Input.GetMouseButton(1)) { isRightClicking = true; }
        else
        { 

        isRightClicking = false;
    
        }
    }


    void FixedUpdate()
    {
        if (rb != null && isRightClicking)
        {
            // マウスがUIの上にあるときは、無視する
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }


            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Mathf.Abs(mainCamera.transform.position.z - mallet.transform.position.z);
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);

            // clampにより，マウスの位置をminX, maxXの間に制限する
            float clampedX = Mathf.Clamp(worldPos.x, minX, maxX);

            rb.MovePosition(new Vector2(clampedX, rb.position.y));
        }
    }


    private void CalculateBoundaries()
    {
        // マレットの現在地から左右にRaycastしてframeとの衝突位置を調べる
        RaycastHit2D hitRight = Physics2D.Raycast(rb.position, Vector2.right, 50f, frame);
        RaycastHit2D hitLeft = Physics2D.Raycast(rb.position, Vector2.left, 50f, frame);

        //maxX (右の限界)を求める
        if (hitRight.collider != null)
        {
            // 壁の位置からoffset分だけ内側を最大値にする
            maxX = hitRight.point.x - offset;
        }
        else
        {
            // 壁が見つからなかった場合のデフォルト値
            maxX = 10.0f;
        }

        //  minX(左の限界)を求める
        if (hitLeft.collider != null)
        {
            // 壁の位置から offset 分だけ内側を最小値にする
            minX = hitLeft.point.x + offset;
        }
        else
        {
            // 壁が見つからなかった場合のデフォルト値
            minX = -10.0f;
        }

    }

    private void DisplayMallet()
    {

        mallet.SetActive(true);
    }



    //EnemyHitColorの不要な値を捨てる
    private void CallInvisibleMallet(int x)
    {
        InvisibleMallet();


    }

    private void InvisibleMallet()
    {

        mallet.SetActive(false);
    }
}