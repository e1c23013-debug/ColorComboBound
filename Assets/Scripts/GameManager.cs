using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [SerializeField]
    private BallManager ballManager;

    [SerializeField]
    private TurnManager turnManager;

    [SerializeField]
    private GameTimeManager gameTimeManager;

    [SerializeField]
    private MySceneManager mySceneManager;

    [SerializeField]
    private TutorialCanvasManager tutorialCanvasManager;

    [SerializeField] private CanvasGroup uiCanvasGroup;


    //インスペクターで指定
    [SerializeField] private string nextStage;

    public bool IsGameClear { get; private set; } = false;

    private bool isInputLocked = false;

    private bool isClickUI = false;

    private static bool isFirstTime = true;//ステージを選択した最初のプレイか判定

    private static string lastSceneName = "";

    private Camera mainCamera;

    private bool isLeftClickReleased=false;

    private Vector2 shotMousepos;



   

    void OnEnable()
    {
        GameEvents.OnTurnRetry += ChangeInputLockedtrue;


        GameEvents.Clear += ChangeInputLockedtrue;

        GameEvents.Clear += ChangeGameClearTrue;


        GameEvents.PushRetryButton += LoadScene;

        GameEvents.PushStageSelectButton += ChangeStageSelectScene;

        GameEvents.PushNextStageButton += ChangeNextStageScene;

    }

    void OnDisable()
    {
        GameEvents.OnTurnRetry -= ChangeInputLockedtrue;

    
        GameEvents.Clear -= ChangeInputLockedtrue;

        GameEvents.Clear -= ChangeGameClearTrue;

        GameEvents.PushRetryButton -= LoadScene;

        GameEvents.PushStageSelectButton -= ChangeStageSelectScene;

        GameEvents.PushNextStageButton -= ChangeNextStageScene;

    }

    void Awake()
    {

        Instance = this;

    }

    void Start()
    {
        mainCamera = Camera.main;
       
        string currentScene = SceneManager.GetActiveScene().name;

        if (lastSceneName != currentScene)
        {
            isFirstTime = true; // シーンが変わっているため
        }

        turnManager.StartTurnChange();

        if (isFirstTime)
        {
            tutorialCanvasManager.DisplayTutorial();
            isFirstTime = false; // 同じシーン内（リトライ等）ではもうチュートリアルを出さないようにする
        }
        //シーンが再読み込みされたときは前のsoundManager(最初に定義したインスタンス)は使えないためSoundManager.Instanceという書き方
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.AssignButtonSounds();
        }

        lastSceneName = currentScene; // 今のシーン名を記録
    }

    // Update is called once per frame
    void Update()
    {


        if (isInputLocked) return;

        //カーソルの座標
        Vector3 mousepos;//mouseDragging,mouseReleasedで計算し，mouseposにoutで格納



        if (Input.GetMouseButtonDown(0)) // 左クリックした瞬間
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                isClickUI = true; // UIの上で押し始めたらフラグを立て,ショットを防ぐ
            }
            else
            {
                isClickUI = false;
            }
        }

       

        //左クリック中
        if (MouseDragging(out mousepos) && isClickUI==false)
        {

            if (IsGameClear) return;
            ballManager.DrawLine(mousepos); 
                
       
        }

        //左クリックが離された時
        if (MouseReleased(out mousepos) && isClickUI == false)
        {

            if (IsGameClear) return;

            ballManager.DeleteLine();

            isClickUI = false;

            shotMousepos = mousepos;
            isLeftClickReleased = true;
        }

     
    }


    void FixedUpdate()
    {
        if (isLeftClickReleased)
        {

            ballManager.BallShot(shotMousepos);

            isLeftClickReleased = false;
        }
    }



    //左クリックが離された時のカーソルのワールド座標を取得する
    private bool MouseReleased(out Vector3 Mousepos) {

        Mousepos = Vector3.zero;
      
      
        //左クリックが離されたか判定
        if (Input.GetMouseButtonUp(0))
        {
        
            // カーソル位置を取得
            Vector3 MousePosition = Input.mousePosition;
            // カーソル位置のz座標を0に
            MousePosition.z = 10;

            // カーソル位置からワールド座標に変換
            Mousepos = mainCamera.ScreenToWorldPoint(MousePosition);

            //クリックが離され，その時の座標をMouseposに代入しtrueを返し関数終了
            return true;


        }


        //左クリックが離された時以外falseを返し関数終了
        return false;
    }

    //左クリックされているときのカーソルのワールド座標を取得する
    private bool MouseDragging(out Vector3 Mousepos)
    {
        Mousepos = Vector3.zero;
        if (Input.GetMouseButton(0))
        {

            // カーソル位置を取得
            Vector3 MousePosition = Input.mousePosition;
            // カーソル位置のz座標を0に
            MousePosition.z = 10;

            // カーソル位置からワールド座標に変換
            Mousepos = mainCamera.ScreenToWorldPoint(MousePosition);

            //左クリックされているときは，その時の座標をMouseposに代入しtrueを返し関数終了
            return true;
        }


        //左クリックされているとき以外はfalaseを返し関数終了
        return false;
    }

    
    private void ChangeInputLockedtrue()
    {

        isInputLocked = true;
    }


    private void ChangeGameClearTrue()
    {

        IsGameClear = true;
    }


    private void LoadScene()
    {
        uiCanvasGroup.interactable = false;
        isFirstTime = false;
        string currentSceneName = SceneManager.GetActiveScene().name;
        gameTimeManager.ResumeGame();
        mySceneManager.ChangeScene(currentSceneName);
       

    }


    private void ChangeStageSelectScene()
    {
        gameTimeManager.ResumeGame();
        mySceneManager.ChangeScene("StageSelect");


    }

    private void ChangeNextStageScene()
    {
        mySceneManager.ChangeScene(nextStage);

    }


}
