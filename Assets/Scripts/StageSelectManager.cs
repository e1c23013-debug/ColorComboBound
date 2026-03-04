using UnityEngine;
using UnityEngine.UI;

public class StageSelectManager : MonoBehaviour

{

    [SerializeField] private MySceneManager mySceneManager;

    private static bool isFirstTime = true;//Title画面から初めてステージセレクト画面に来たか判定

    [SerializeField] private Button[] stageButtons;

    [SerializeField] private GameObject cursor;//初めてステージセレクト画面に来た時のみ初めのステージに誘導するため見える
    void Start()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.AssignButtonSounds();
        }

        if (isFirstTime == true)
        {
            for(int i=1; i<stageButtons.Length; i++)//初めのステージ(stageButtons[1])以外を選択できなくする
            {
                stageButtons[i].interactable = false;

            }
            cursor.SetActive(true);

            isFirstTime = false;
        }
        else
        {
            for (int i = 0; i < stageButtons.Length ; i++)//全てのステージが選択可能
            {
                stageButtons[i].interactable = true;

            }
            cursor.SetActive(false);
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
