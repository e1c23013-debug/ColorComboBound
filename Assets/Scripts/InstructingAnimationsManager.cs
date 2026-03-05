using UnityEngine;

public class InstructingAnimationsManager : MonoBehaviour
{

    private Animator anim;

    //ShotInstructing,MalletInstructingを数値に変換しておく
    private static readonly int shotInstructingHash = Animator.StringToHash("ShotInstructing");
    private static readonly int malletInstructingHash = Animator.StringToHash("MalletInstructing");

    //現在のステージがステージ1か
    private bool isStage1;


    void OnEnable()
    {
        GameEvents.TutorialClose += PlayShotInstructingAnimation;

    }

    void OnDisable()
    {
        GameEvents.TutorialClose -= PlayShotInstructingAnimation;

    }

    void Awake()
    {

        anim=GetComponent<Animator>();

        isStage1 = false;//初期化

    }
   
    //GameManagerより呼び出し
    public void ChangeIsStage1True()
    {
        isStage1 = true;

    }

    private void PlayShotInstructingAnimation()
    {
        GameEvents.StartShotTutorialAnimation?.Invoke();

        if (isStage1 == true)
        {
            anim.SetTrigger(shotInstructingHash);
        }
     
    }

    //Unity上のAnimationでアニメーションの最後のフレームで呼び出し 
    public void EndAnimation()
    {
        GameEvents.EndShotTutorialAnimation?.Invoke();
    }

}
