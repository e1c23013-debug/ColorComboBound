using UnityEngine;
using System.Collections;
using TMPro;

public class ClearUIManager : MonoBehaviour
{


    [SerializeField] private GameObject clearCanvas;

    [SerializeField] private GameObject maxComboUI;

    private TextMeshProUGUI tmpMaxCombo;

    private float waitClearUIDisplayTime = 0.2f;



    void OnEnable()
    {
        GameEvents.Clear += ClearCall;

        GameEvents.ClearMaxCombo += SetMaxCombo;
    }
    void OnDisable()
    {
        GameEvents.Clear -= ClearCall;

        GameEvents.ClearMaxCombo -= SetMaxCombo;
    }


    void Awake()
    {
        tmpMaxCombo = maxComboUI.GetComponent<TextMeshProUGUI>();

    }

    //インスペクターでボタンに設定
    public void PushNextStageButton()
    {


        GameEvents.PushNextStageButton?.Invoke();

    }




    public void PushStageSelectButton()
    {


        GameEvents.PushStageSelectButton?.Invoke();

    }




    private void ClearUIDisplay()
    {

        clearCanvas.SetActive(true);



    }

    IEnumerator Clear()
    {
        yield return new WaitForSeconds(waitClearUIDisplayTime) ;

        ClearUIDisplay();
    }

    private void ClearCall()
    {
        StartCoroutine(Clear());

    }

    private void SetMaxCombo(int maxCombo)
    {

        tmpMaxCombo.text = "MAX COMBO : " + (maxCombo);


    }


  

}
