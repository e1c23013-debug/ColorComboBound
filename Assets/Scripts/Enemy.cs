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

    //衝突時に重複して呼び出されるのを防ぐためのフラグ
    private bool isProcessingAttacked = false;

    //通常ボールヒットのダメージ(ballstからイベントで知らせてもらう)
    private int normalDamage;

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
            currentHP = 1;
            GameEvents.InvalidAttack?.Invoke();
            return;
        }

        isProcessingAttacked = true;
        GameEvents.BallBoundEnemy?.Invoke();


        currentHP = currentHP - damage;
        GameEvents.EnemyHitColor.Invoke(Color,currentHP,maxHP);


        if (currentHP <= 0)
        {

            Extinction();
            GameEvents.EnemyBeated?.Invoke();
            

        }

        Invoke(nameof(ResetFlag), 0.1f);

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
            currentHP = 1;
            return;
        }

        isProcessingAttacked = true;
        GameEvents.BallBoundEnemy?.Invoke();


        currentHP = currentHP - damage;
        GameEvents.EnemyHitColor.Invoke(Color, currentHP, maxHP);


        if (currentHP <= 0)
        {

            Extinction();
            GameEvents.EnemyBeated?.Invoke();


        }

        Invoke(nameof(ResetFlag), 0.1f);

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
