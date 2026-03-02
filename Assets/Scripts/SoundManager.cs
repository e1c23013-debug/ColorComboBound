using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;

    [SerializeField] private AudioSource seSource;
    [SerializeField] private AudioClip ballBoundSE;
    [SerializeField] private AudioClip clickSE;
    [SerializeField] private AudioClip enemyAttackSE;
    [SerializeField] private AudioClip enemyBeatSE;
    [SerializeField] private AudioClip shotSE;
    [SerializeField] private AudioClip retrySE;
    [SerializeField] private AudioClip gimmickSE;
    [SerializeField] private AudioClip malletSwitchSE;
    [SerializeField] private AudioClip invalidAttackSE;
    [SerializeField] private AudioClip malletHitSE;
    [SerializeField] private AudioClip laserSE;
    [SerializeField] private AudioClip diffusionSE;

     [SerializeField] AudioSource audioSource; 



    void OnEnable()
    {
        GameEvents.BallBound += PlayBallBoundSE;

        GameEvents.BallBoundEnemy += PlayEnemyAttackSE;

        GameEvents.EnemyBeated += PlayEnemyBeatSE;

        GameEvents.BallShot += PlayShotSE;

        GameEvents.PushMalletSwitch += PlayMalletSwitchSE;

        GameEvents.InvalidAttack += PlayInvalidAttackSE;

        GameEvents.MalletPushBall += PlayMalletHitSE;

        GameEvents.DiffusionSwitch += PlayDiffusionSE;

        GameEvents.LaserSwitch += PlayLaserSE;

        AssignButtonSounds();
    }

    void OnDisable()
    {
        GameEvents.BallBound -= PlayBallBoundSE;

        GameEvents.BallBoundEnemy -= PlayEnemyAttackSE;

        GameEvents.EnemyBeated -= PlayEnemyBeatSE;


        GameEvents.BallShot -= PlayShotSE;

        GameEvents.PushMalletSwitch -= PlayMalletSwitchSE;

        GameEvents.InvalidAttack -= PlayInvalidAttackSE;

        GameEvents.MalletPushBall -= PlayMalletHitSE;

        GameEvents.DiffusionSwitch -= PlayDiffusionSE;

        GameEvents.LaserSwitch -= PlayLaserSE;

    }


    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
   

    private void PlayBallBoundSE()
    {
        seSource.PlayOneShot(SoundManager.Instance.ballBoundSE);

    }



    private void PlayEnemyAttackSE()
    {
        seSource.PlayOneShot(SoundManager.Instance.enemyAttackSE);

    }

    private void PlayEnemyBeatSE()
    {
        seSource.PlayOneShot(SoundManager.Instance.enemyBeatSE);

    }


    private void PlayRetrySE()
    {

        seSource.PlayOneShot(SoundManager.Instance.retrySE);

    }

    private void PlayShotSE()
    {

        seSource.PlayOneShot(SoundManager.Instance.shotSE);

    }

    private void PlayMalletSwitchSE()
    {

        seSource.PlayOneShot(SoundManager.Instance.malletSwitchSE);

    }

    private void PlayInvalidAttackSE()
    {

        seSource.PlayOneShot(SoundManager.Instance.invalidAttackSE);

    }

    private void PlayMalletHitSE()
    {

        seSource.PlayOneShot(SoundManager.Instance.malletHitSE);

    }

    private void PlayDiffusionSE()
    {

        seSource.PlayOneShot(SoundManager.Instance.diffusionSE);

    }

    private void PlayLaserSE()
    {

        seSource.PlayOneShot(SoundManager.Instance.laserSE);

    }


    //TitleManagerより呼びだし
    public void PlayClickSE()
    {

        seSource.PlayOneShot(SoundManager.Instance.clickSE);
    }
    //GamaManager,StageSelectManagerより呼び出し
    //シーン切り替え時に切替後のシーンで呼び出し，シーンのボタンUIを全て読み込み,押したときの音の再生を割り当てる
    public void AssignButtonSounds()
    {
        Button[] allButtons = Object.FindObjectsByType<Button>(
   
            FindObjectsInactive.Include,      
            FindObjectsSortMode.None     
    
     );

        foreach (Button button in allButtons)
        {
            button.onClick.RemoveListener(PlayRetrySE);
            button.onClick.RemoveListener(PlayClickSE);

            if (button.CompareTag("RetryButton"))
            {
              
                button.onClick.AddListener(PlayRetrySE);
            }
            else
            {
                button.onClick.AddListener(PlayClickSE);
            }

        }
    }

}
