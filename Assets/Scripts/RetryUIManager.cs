using UnityEngine;


public class RetryUIManager : MonoBehaviour

{
    [SerializeField] private GameObject retryCanvas;


    void OnEnable()
    {
        GameEvents.OnTurnRetry += RetryUIDisplay;
 

    }

    void OnDisable()
    {
        GameEvents.OnTurnRetry -= RetryUIDisplay;


    }


    //インスペクターでリトライボタンと紐づけ
    public void PushRetryButton()
    {


        GameEvents.PushRetryButton?.Invoke();

    }

    public void PushStageSelectButton()
    {


        GameEvents.PushStageSelectButton?.Invoke();

    }

    private void RetryUIDisplay()
    {

        retryCanvas.SetActive(true);



    }




}
