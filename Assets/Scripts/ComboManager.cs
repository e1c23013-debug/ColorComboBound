using UnityEngine;

public class ComboManager : MonoBehaviour
{
    //コンボの数がこの数を超えるとダメージが増加
    private int increaseDamageNumber = 3;
    private int combo;
    private int maxCombo;
 
    void OnEnable()
    {

        GameEvents.MalletPushBall += ComboIncrease;

        GameEvents.OnTurnChanged += CallComboIni;

        GameEvents.Clear += InformMaxCombo;
    }

    void OnDisable()
    {
        GameEvents.MalletPushBall -= ComboIncrease;

        GameEvents.OnTurnChanged -= CallComboIni;

        GameEvents.Clear -= InformMaxCombo;
    }

        void Start()
    {
        maxCombo = 0;
        ComboIni();


    }


    private void ComboIni()
    {

        combo = 0;

        GameEvents.ComboChageInform?.Invoke(combo);

    }
    //OnTurnChangedの不要な値を捨てる
    private void CallComboIni(int x)
    {

        ComboIni();

    }


    private void ComboIncrease()
    {

        combo = combo + 1;

        if (maxCombo < combo)
        {
            maxCombo = combo;
        }
        GameEvents.ComboChageInform?.Invoke(combo);
        if (combo > increaseDamageNumber)
        {
            GameEvents.IncreaseDamageCombo?.Invoke();

        }

    }

    private void InformMaxCombo()
    {

        GameEvents.ClearMaxCombo?.Invoke(maxCombo);

    }


}
