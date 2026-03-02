using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    //{ get; private set; }はBallManagerで参照
    //ボールがグラウンドについているか
    public bool IsBallGround { get; private set; }

    //ボールが動いているか
    public bool IsBallMoving { get; private set; }

    //ボールの基本ヒットダメージ
    [SerializeField] private int baseDamage = 1;
    //ボールの コンボダメージ
    [SerializeField] private int comboDamage = 2;

    //ボールの初期速度
    [SerializeField] private float moveSpeed = 10f;

    [SerializeField] private float maxSpeed = 30f;

    private Rigidbody2D rb;

    //現在のボールのダメージ
    private int damage;
    //ボールの色を変更するのに使う
    private SpriteRenderer sr;

    //真上を基準としたショット可能な角度
    private float minAngle = -80f;
    private float maxAngle = 80f;

    private const string tagMallet = "Mallet";
    private const string tagGround = "Ground";
    private const string tagEnemy = "Enemy";


 
    void OnEnable()
    {
        GameEvents.OnTurnChanged += CallDamageIni;
        GameEvents.IncreaseDamageCombo += IncreaseDamage;
        GameEvents.Diffusion += Diffision;

    }

    void OnDisable()
    {
        GameEvents.OnTurnChanged -= CallDamageIni;
        GameEvents.IncreaseDamageCombo -= IncreaseDamage;
        GameEvents.Diffusion -= Diffision;
    }


    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
       
    }
    void Start()
    {
        ChangeBallGroundfalse();
        ChangeBallMovingfalse();
        DamageIni();
        sr = GetComponent<SpriteRenderer>();
        GameEvents.NormalDamage?.Invoke(damage);
        GameEvents.BallMaxSpeed?.Invoke(maxSpeed);

    }

    void Update()
    {
        if (rb.linearVelocity.magnitude > maxSpeed) 
        {
            Debug.Log($"現在の速度: {rb.linearVelocity.magnitude}");
            // 強制ブレーキ
            rb.linearVelocity = rb.linearVelocity.normalized * moveSpeed;
        }

    }


    // FixedUpdateは物理計算の直後に呼び出される
    //物理演算によりボールがスクリプトで設定した速度より早かったり遅くなるのを防ぐ
    void FixedUpdate()
    {
        // 止まっているときは何もしない
        if (rb.linearVelocity == Vector2.zero) return;

        rb.linearVelocity = rb.linearVelocity.normalized * moveSpeed;


    }


    void OnCollisionEnter2D(Collision2D coll)
    {

        //衝突計算直後の速度ベクトルの方向を真上を基準の角度に変換
        float angle = Vector2.SignedAngle(Vector2.up, rb.linearVelocity.normalized);

        float randomAngle = Random.Range(-2.0f, 2.0f);

        float changeAngle = angle + randomAngle;

        rb.linearVelocity = Quaternion.Euler(0, 0, changeAngle) * Vector2.up;



        if (GameManager.Instance.IsGameClear && coll.gameObject.tag == "Ground")
        {

            GameEvents.BallGround?.Invoke();
        }
        else if (GameManager.Instance.IsGameClear)
        {
            return;


        }
        else if (coll.gameObject.tag == tagGround)
        {
            IsBallGround = true;

        }
        else if (coll.gameObject.tag == tagEnemy)
        {


           
              
            Enemy enemy = coll.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {

                EnemyColor color = enemy.Color;

                GameEvents.BallCollisionEnemy?.Invoke(damage,color);
            
            }


        }
        else if (coll.gameObject.tag == tagMallet)
        {

            GameEvents.BallCollisionMallet?.Invoke();

        }
        else
        {
            Vector2 dir = rb.linearVelocity.normalized;

            float angleRandom = Random.Range(-5f, 5f);

            dir = Quaternion.Euler(0, 0, angleRandom) * dir;

       
            GameEvents.BallBound?.Invoke();

        }

    }
    //publicはBallManagerより呼び出し
    public void BallDirectionChange(Vector3 Mousepos3)//world
    {

        Vector3 ballpos = rb.position;//world


        Vector2 tempDirection = (Mousepos3 - ballpos).normalized; //方角ベクトルのみ取り出す

        //真上を基準の角度に変換
        float angle = Vector2.SignedAngle(Vector2.up, tempDirection);

        //  角度を minAngle, maxAngleの間に制限
        float clampedAngle = Mathf.Clamp(angle, minAngle, maxAngle);

        //角度を方向ベクトルに戻す
        Vector2 direction = Quaternion.Euler(0, 0, clampedAngle) * Vector2.up;



        rb.linearVelocity = direction.normalized * moveSpeed;


    }



    public void ChangeBallGroundfalse()
    {
        IsBallGround = false;

    }

    public void ChangeBallMovingfalse()
    {
        IsBallMoving = false;

    }

    public void ChangeBallMovingTrue()
    {
        IsBallMoving = true;

    }

    private void DamageIni()
    {
        damage = baseDamage ;

    }

    //GameEvents.OnTurnChangedの不要な値を捨てる
    private void CallDamageIni(int x)
    {
        DamageIni();

    }

    private void IncreaseDamage()
    {

        damage = comboDamage;
        sr.color = Color.red;

    }

    private void Diffision(float newSpeed, Vector2 direction)
    {
     
        moveSpeed = newSpeed;

        if (rb.linearVelocity.sqrMagnitude > 0.001f)//ボールが動いているか
        {
            rb.linearVelocity = rb.linearVelocity.normalized * moveSpeed;
        }

        rb.linearVelocity = direction.normalized * moveSpeed;


    }



}
