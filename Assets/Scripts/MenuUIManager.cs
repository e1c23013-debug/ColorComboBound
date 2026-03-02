using UnityEngine;

public class MenuUIManager : MonoBehaviour
{

    [SerializeField] private GameObject menuCanvas;

    public void PushMenuButton()
    {

        menuCanvas.SetActive(true);
        GameEvents.MenuButtonPushed?.Invoke();
    }

    public void PushCloseButton()
    {

        menuCanvas.SetActive(false);
        GameEvents.MenuCloseButtonPushed?.Invoke();
    }
    public void PushRetryButton()
    {


        GameEvents.PushRetryButton?.Invoke();

    }

    public void PushStageSelectButton()
    {


        GameEvents.PushStageSelectButton?.Invoke();

    }


}
