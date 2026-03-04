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

        mySceneManager.ChangeScene(SceneName.Stage1);

    }
    public void PushStage2Button()
    {


        mySceneManager.ChangeScene(SceneName.Stage2);

    }
    public void PushStage3Button()
    {


        mySceneManager.ChangeScene(SceneName.Stage3);

    }



}
