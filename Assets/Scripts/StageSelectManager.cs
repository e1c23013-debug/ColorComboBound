using UnityEngine;

public class StageSelectManager : MonoBehaviour

{

    [SerializeField] private MySceneManager mySceneManager;
    void Start()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.AssignButtonSounds();
        }

    }

  
    public void PushStage1Button()
    {

        mySceneManager.ChangeScene("Stage1");

    }
    public void PushStage2tButton()
    {


        mySceneManager.ChangeScene("Stage2");

    }
    public void PushStage3Button()
    {


        mySceneManager.ChangeScene("Stage3");

    }



}
