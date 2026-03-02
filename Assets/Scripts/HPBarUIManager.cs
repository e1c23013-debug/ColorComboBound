using UnityEngine;


public class HPBarUIManager : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider redBar;
    [SerializeField] private UnityEngine.UI.Slider yellowBar;
    [SerializeField] private UnityEngine.UI.Slider greenBar;


    void OnEnable()
    {
        GameEvents.StartEnemyColor += StartColorBar;

        GameEvents.EnemyHitColor += ChangeHP;


    }

    void OnDisable()
    {
        GameEvents.StartEnemyColor -= StartColorBar;

        GameEvents.EnemyHitColor -= ChangeHP;

    
    
    }

    void Awake()
    {
        // まず全てのバーを 1 (満タン) に初期化
        redBar.value = 1f;
        yellowBar.value = 1f;
        greenBar.value = 1f;

        // 最初は全て非表示にしておく (敵がいる場合だけEnemy.Startから通知)
        redBar.gameObject.SetActive(false);
        yellowBar.gameObject.SetActive(false);
        greenBar.gameObject.SetActive(false);
    }


    private void ChangeHP(EnemyColor Color,int currentHP,int MAXHP)
    {
        switch (Color)
        {
            case EnemyColor.Red:

                redBar.value = (float)currentHP / MAXHP;


                break;
            case EnemyColor.Yellow:

                yellowBar.value = (float)currentHP / MAXHP;

                break;

            case EnemyColor.Green:

                greenBar.value = (float)currentHP / MAXHP;


                break;

        }
    }

    //存在する敵の色のbarを見えるようにする
    private void StartColorBar(EnemyColor Color)
    {
        switch (Color)
        {
            case EnemyColor.Red:

                redBar.gameObject.SetActive(true);
                break;

            case EnemyColor.Yellow:

                yellowBar.gameObject.SetActive(true);
                break;

            case EnemyColor.Green:

                greenBar.gameObject.SetActive(true);
                break;





        }


    }

}
