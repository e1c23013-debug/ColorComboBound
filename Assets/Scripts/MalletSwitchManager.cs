using UnityEngine;

public class MalletSwitchManager : MonoBehaviour
{
    [SerializeField] private Sprite malletSwitch;
    [SerializeField] private Sprite malletSwitchTransparent;

    private SpriteRenderer sr;

    private bool isSwitchPushed=false;
  
    void OnEnable()
    {
        GameEvents.OnTurnChanged += CallSpriteReset;
    }

    void OnDisable()
    {
        GameEvents.OnTurnChanged -= CallSpriteReset;

    }
    void Awake()
    {
         sr = GetComponent<SpriteRenderer>();

    }

   
    void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.Instance.IsGameClear) return;
        if (isSwitchPushed) return;

        if (other.CompareTag("Ball"))
        {
            isSwitchPushed = true;
            sr.sprite= malletSwitchTransparent;
            GameEvents.PushMalletSwitch?.Invoke();

        }



    }


    //スイッチの透過，押されたかのフラグをリセット
    private void SpriteReset()
    {
        sr.sprite = malletSwitch;
        isSwitchPushed = false;

    }

    private void CallSpriteReset(int x)
    {
        SpriteReset();

    }
}
