using UnityEngine;



public class TitleManager : MonoBehaviour
{


    [SerializeField]
    private　SoundManager soundManager;

    [SerializeField]
    private MySceneManager mySceneManager;

   private bool isChanging = false; // 遷移中か

   
    void Update()
    {

        if (isChanging) return;

        if (Input.GetMouseButtonDown(0)) // 左クリックをした瞬間
        {

            soundManager.PlayClickSE();
            

        }

        if (Input.GetMouseButtonUp(0)) // 左クリックをして離したら
        {

            mySceneManager.ChangeScene(SceneName.StageSelect);



        }


    }



}
