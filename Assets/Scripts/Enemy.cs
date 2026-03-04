using UnityEngine;

public enum EnemyColor { Red, Yellow,Green }

public class Enemy : MonoBehaviour
{

    //ヒエラルキーで設定,ボールと衝突時にBallBehviorより読み込み
    public EnemyColor Color;
    //ヒエラルキーで設定
    [SerializeField] private int maxHP;

    private int currentHP;

    //敵が残り一匹か(EnamyManagerに教えてもらう)
    private bool oneEnemyLeft = false;

    //攻撃判定があってから次の攻撃判定を受け付けるまでの時間
    private float invalidTime = 0.01f;

    //衝突時に重複して呼び出されるのを防ぐためのフラグ
    private bool isProcessingAttacked = false;

    //通常ボールヒットのダメージ(ballstからイベントで知らせてもらう)
    private int normalDamage;

    private Animator anim;
    //Attackedを数値に変換しておく
    private static readonly int attackedHash = Animator.StringToHash("Attacked");

    //条件を満たさない場合敵が維持するHP
    private int endureHP = 1;

  

    void OnEnable()
    {
        GameEvents.OneEnemyLeft += ChangeoneEnemyLeftTrue;

        GameEvents.NormalDamage += SetNormalDamage;

        GameEvents.BallCollisionEnemy += Attacked;

        GameEvents.LaserCollisionEnemy += AttackedByLaser;
    }

    void OnDisable()
    {
        GameEvents.OneEnemyLeft -= ChangeoneEnemyLeftTrue;

        GameEvents.NormalDamage -= SetNormalDamage;

        GameEvents.BallCollisionEnemy -= Attacked;

        GameEvents.LaserCollisionEnemy -= AttackedByLaser;
    }

    void Awake()
    {
        //ballstのstart時のイベントでダメージを知らせてもらうために適当な値で初期化しておく
        normalDamage = 0;
        currentHP = maxHP;

        anim = GetComponent<Animator>();
    }

    void Start()
    {
        GameEvents.StartEnemyColor?.Invoke(Color);
    }

  
    //publicはEnemyManagerより呼び出し
    private void Attacked(int damage, EnemyColor ballCollisionColor)
    {
        if (isProcessingAttacked == true) return;

        if (Color != ballCollisionColor) return;
        
        //緑はボールヒットではダメージを与えられない
        if (Color == EnemyColor.Green)
        {

            GameEvents.InvalidAttack?.Invoke();

            return;
        }
        //赤は赤以外に敵が倒されていて，かつコンボで強化されたボールでの攻撃(damageが通常ボールヒットのダメージ以上)時以外耐え続ける
        if (Color == EnemyColor.Red&&currentHP-damage<=0    &&(damage==normalDamage|| oneEnemyLeft == false))
        {
            currentHP = endureHP;
            GameEvents.InvalidAttack?.Invoke();
            return;
        }

        isProcessingAttacked = true;
        GameEvents.BallBoundEnemy?.Invoke();


        currentHP = currentHP - damage;
        GameEvents.EnemyHitColor.Invoke(Color,currentHP,maxHP);

        anim.SetTrigger(attackedHash);


        if (currentHP <= 0)
        {

            Extinction();
            GameEvents.EnemyBeated?.Invoke();
            

        }

        Invoke(nameof(ResetFlag), invalidTime);

    }

    private void ResetFlag()
    {
        isProcessingAttacked = false;

    }


    private void Extinction()
    {
     
        gameObject.SetActive(false);

  
    }

    private void AttackedByLaser(int damage, EnemyColor EnemyCollisionColor)
    {
        if (isProcessingAttacked == true) return;
        if (Color != EnemyCollisionColor) return;


        //赤は赤以外に敵が倒されていて，かつコンボで強化されたボールでの攻撃(damageが通常ボールヒットのダメージ以上)時以外耐え続ける
        if (Color == EnemyColor.Red && currentHP - damage <= 0)
        {

            GameEvents.InvalidAttack?.Invoke();
            currentHP = endureHP;
            return;
        }

        isProcessingAttacked = true;
        GameEvents.BallBoundEnemy?.Invoke();


        currentHP = currentHP - damage;
        GameEvents.EnemyHitColor.Invoke(Color, currentHP, maxHP);

        anim.SetTrigger(attackedHash);

        if (currentHP <= 0)
        {

            Extinction();
            GameEvents.EnemyBeated?.Invoke();


        }

        Invoke(nameof(ResetFlag), invalidTime);

    }

    private void ChangeoneEnemyLeftTrue()
    {
        oneEnemyLeft = true;

    }

    private void SetNormalDamage(int damage)
    {
        normalDamage = damage;

    }
}
